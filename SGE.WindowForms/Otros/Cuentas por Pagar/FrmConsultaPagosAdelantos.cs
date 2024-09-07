using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{

    public partial class FrmConsultaPagosAdelantos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaPagosAdelantos));

        private List<EDocxPagarPagoAdelanto> Lista = new List<EDocxPagarPagoAdelanto>();

        public delegate void DelegadoMensaje(long Cab_icod_correlativo);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public int mes;
        public long Cab_icod_correlativo;
        public EDocPorPagar eDocXPagar = new EDocPorPagar();
        public int IcodDXP = 0;
        public int Moneda = 0;

        public FrmConsultaPagosAdelantos()
        {
            InitializeComponent();
        }

        private void Carga()
        {
            /*Gerson*/
            Lista = new BCuentasPorPagar().listarDxpPagosConAdelantos(eDocXPagar.doxpc_icod_correlativo, 0, Parametros.intEjercicio);
            grdDetalle.DataSource = Lista;

            new BTesoreria().ActualizarMontoDXPPagadoSaldo(IcodDXP, Moneda);

        }

        private void FrmConsultaPagosAdelantos_Load(object sender, EventArgs e)
        {
            Carga();
            this.Text = "Pagos efectuados por Adelantos de " + eDocXPagar.tdocc_vabreviatura_tipo_doc + "-" + eDocXPagar.doxpc_vnumero_doc;
            viewDetalle.GroupPanelText = "Pagos efectuados por Adelantos de " + eDocXPagar.tdocc_vabreviatura_tipo_doc + "-" + eDocXPagar.doxpc_vnumero_doc;
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void Nuevo_Click(object sender, EventArgs e)
        {
            if (eDocXPagar.doxpc_nmonto_total_saldo == 0)
            {
                XtraMessageBox.Show("El documento ya se encuentra cancelado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                using (FrmMantePagoAdelanto p = new FrmMantePagoAdelanto())
                {
                    p.MiEvento += new FrmMantePagoAdelanto.DelegadoMensaje(form2_MiEvento);
                    p.obeDocXPagar = eDocXPagar;
                    p.saldoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo);
                    viewDetalle.MoveLast();
                    p.SetInsert();
                    p.ShowDialog();
                }
            }
        }

        void form2_MiEvento()
        {
            Carga();
        }



        private void Modificar_Click(object sender, EventArgs e)
        {
            if (Lista.Count != 0)
            {
                Datos();
            }
            else
                XtraMessageBox.Show("No hay registros por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void Datos()
        {
            EDocxPagarPagoAdelanto Obe = (EDocxPagarPagoAdelanto)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            FrmMantePagoAdelanto frm = new FrmMantePagoAdelanto();
            frm.MiEvento += new FrmMantePagoAdelanto.DelegadoMensaje(form2_MiEvento);

            if (Obe.id_tipo_moneda_adelanto == eDocXPagar.tablc_iid_tipo_moneda)
            {
                frm.saldoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo + Obe.adpap_nmonto_pago);
                frm.pagoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_pagado - Obe.adpap_nmonto_pago);
            }
            else
            {
                if (eDocXPagar.tablc_iid_tipo_moneda == Parametros.intSoles)
                {
                    frm.saldoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo + (Obe.adpap_nmonto_pago * Obe.adpap_nmonto_tipo_cambio));
                    frm.pagoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_pagado - (Obe.adpap_nmonto_pago * Obe.adpap_nmonto_tipo_cambio));
                }
                else
                {
                    frm.saldoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo + (Obe.adpap_nmonto_pago / Obe.adpap_nmonto_tipo_cambio));
                    frm.pagoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_pagado - (Obe.adpap_nmonto_pago / Obe.adpap_nmonto_tipo_cambio));
                }
            }

            if (frm.saldoGralDxP > eDocXPagar.doxpc_nmonto_total_documento)
            {
                frm.saldoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_documento);
                frm.pagoGralDxP = 0;
            }

            frm.obeDocXPagar = eDocXPagar;
            frm.vcocc_iid_voucher_contable = Convert.ToInt32(Obe.vcocc_iid_voucher_contable);
            frm.obeDocXPagarAD = new EDocPorPagar()
            {
                doxpc_icod_correlativo = Obe.doxpc_icod_correlativo_adelanto,
                doxpc_vnumero_doc = Obe.vnumero_adelanto,
                tablc_iid_tipo_moneda = Convert.ToInt32(Obe.id_tipo_moneda_adelanto),
                doxpc_nmonto_total_saldo = (Obe.SaldoDXCAdelanto + Obe.adpap_nmonto_pago),
                doxpc_nmonto_total_pagado = (Obe.doxpc_nmonto_total_pagado - Obe.adpap_nmonto_pago),
                SimboloMoneda = Obe.SimboloMoneda,
                tdodc_iid_correlativo = Convert.ToInt32(Obe.doxpc_icod_correlativo_adelanto)
            };
            frm.codDXPPagoAD = Obe.adpap_icod_correlativo;
            frm.codDXPPago = Convert.ToInt64(Obe.pdxpc_icod_correlativo);
            frm.bteDXPAdelanto.Tag = Obe.doxpc_icod_correlativo_adelanto;
            frm.bteDXPAdelanto.Text = Obe.vnumero_adelanto;
            frm.deFechaDocumento.EditValue = Obe.adpap_sfecha_pago;
            frm.lblMoneda.Text = Obe.SimboloMoneda;
            frm.txtMonto.Text = Obe.adpap_nmonto_pago.ToString();
            frm.txtTipoCambio.Text = Obe.adpap_nmonto_tipo_cambio.ToString();
            frm.txtObservacion.Text = Obe.adpap_vdescripcion;

            frm.SetModify();
            frm.ShowDialog();
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocxPagarPagoAdelanto ObeAD = (EDocxPagarPagoAdelanto)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                EDocPorPagarPago Obe = new EDocPorPagarPago()
                {
                    pdxpc_icod_correlativo = Convert.ToInt64(ObeAD.pdxpc_icod_correlativo),
                    doxpc_icod_correlativo = ObeAD.doxpc_icod_correlativo_adelanto,
                    doxpc_icod_correlativo_pago = ObeAD.doxpc_icod_correlativo_pago,
                    pdxpc_nmonto_pago = ObeAD.adpap_nmonto_pago,
                    pdxpc_nmonto_tipo_cambio = ObeAD.adpap_nmonto_tipo_cambio,
                    tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoProveedor,
                    tablc_iid_tipo_moneda = Convert.ToInt32(ObeAD.id_tipo_moneda_adelanto)
                };
                if (XtraMessageBox.Show("¿Está seguro de Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {

                    EliminarPago(ObeAD, Obe);
                    Lista.Remove(ObeAD);
                    viewDetalle.RefreshData();
                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void EliminarPago(EDocxPagarPagoAdelanto ObeAD, EDocPorPagarPago Obe)
        {
            if (Obe.tablc_iid_tipo_moneda == eDocXPagar.tablc_iid_tipo_moneda)
            {
                Obe.saldoDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo + Obe.pdxpc_nmonto_pago);
                Obe.pagoDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_pagado - Obe.pdxpc_nmonto_pago);
            }
            else
            {
                if (eDocXPagar.tablc_iid_tipo_moneda == Parametros.intSoles)
                {
                    Obe.saldoDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo + (Obe.pdxpc_nmonto_pago * Obe.pdxpc_nmonto_tipo_cambio));
                    Obe.pagoDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_pagado - (Obe.pdxpc_nmonto_pago * Obe.pdxpc_nmonto_tipo_cambio));
                }
                else
                {
                    Obe.saldoDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo + (Obe.pdxpc_nmonto_pago / Obe.pdxpc_nmonto_tipo_cambio));
                    Obe.pagoDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_pagado - (Obe.pdxpc_nmonto_pago / Obe.pdxpc_nmonto_tipo_cambio));
                }
            }

            if (Obe.saldoDxP > eDocXPagar.doxpc_nmonto_total_documento)
            {
                Obe.saldoDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_documento);
                Obe.pagoDxP = 0;
            }
            Obe.strPc = WindowsIdentity.GetCurrent().Name;
            Obe.intUsuario = Valores.intUsuario;
            ObeAD.adpap_vpc_modifica = WindowsIdentity.GetCurrent().Name;
            ObeAD.adpap_iusuario_modifica = Valores.intUsuario;
            new BCuentasPorPagar().EliminarPagoAdelanto(ObeAD, Obe);
        }
    }
}