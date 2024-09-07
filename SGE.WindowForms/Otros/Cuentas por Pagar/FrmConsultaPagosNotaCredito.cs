using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmConsultaPagosNotaCredito : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaPagosNotaCredito));

        private List<EDocPorPagarNotaCredito> Lista = new List<EDocPorPagarNotaCredito>();

        public delegate void DelegadoMensaje(long Cab_icod_correlativo);
        public event DelegadoMensaje MiEvento;
        public int mes;
        public long Cab_icod_correlativo;
        public EDocPorPagar eDocXPagar = new EDocPorPagar();


        public FrmConsultaPagosNotaCredito()
        {
            InitializeComponent();
        }

        private void Carga()
        {
            Lista = new BCuentasPorPagar().listarDxpPagosConNc(eDocXPagar.doxpc_icod_correlativo, 0, Parametros.intEjercicio);
            grd.DataSource = Lista;
        }

        private void FrmConsultaPagosNotaCredito_Load(object sender, EventArgs e)
        {
            Carga();
            this.Text = "Pagos efectuados con Notas de Crédito de " + eDocXPagar.tdocc_vabreviatura_tipo_doc + " - " + eDocXPagar.doxpc_vnumero_doc;
            grv.GroupPanelText = "Pagos efectuados con Notas de Crédito de " + eDocXPagar.tdocc_vabreviatura_tipo_doc + " - " + eDocXPagar.doxpc_vnumero_doc;
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
                using (FrmMantePagoNotaCredito p = new FrmMantePagoNotaCredito())
                {
                    p.MiEvento += new FrmMantePagoNotaCredito.DelegadoMensaje(form2_MiEvento);
                    p.obeDocXPagar = eDocXPagar;
                    p.saldoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo);
                    grv.MoveLast();
                    p.SetInsert();
                    p.ShowDialog();
                }
            }
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
            EDocPorPagarNotaCredito Obe = (EDocPorPagarNotaCredito)grv.GetRow(grv.FocusedRowHandle);
            FrmMantePagoNotaCredito frm = new FrmMantePagoNotaCredito();
            frm.MiEvento += new FrmMantePagoNotaCredito.DelegadoMensaje(form2_MiEvento);

            if (Obe.iid_moneda_nota_credito == eDocXPagar.tablc_iid_tipo_moneda)
            {
                frm.saldoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo + Obe.ncpap_nmonto_pago);
                frm.pagoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_pagado - Obe.ncpap_nmonto_pago);
            }
            else
            {
                if (eDocXPagar.tablc_iid_tipo_moneda == Parametros.intSoles)
                {
                    frm.saldoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo + (Obe.ncpap_nmonto_pago * Obe.ncpap_nmonto_tipo_cambio));
                    frm.pagoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_pagado - (Obe.ncpap_nmonto_pago * Obe.ncpap_nmonto_tipo_cambio));
                }
                else
                {
                    frm.saldoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo + (Obe.ncpap_nmonto_pago / Obe.ncpap_nmonto_tipo_cambio));
                    frm.pagoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_pagado - (Obe.ncpap_nmonto_pago / Obe.ncpap_nmonto_tipo_cambio));
                }
            }

            if (frm.saldoGralDxP > eDocXPagar.doxpc_nmonto_total_documento)
            {
                frm.saldoGralDxP = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_documento);
                frm.pagoGralDxP = 0;
            }

            frm.obeDocXPagar = eDocXPagar;
            frm.obeDocXPagarNC = new EDocPorPagar()
            {
                doxpc_icod_correlativo = Obe.doxpc_icod_correlativo_nota_credito,
                doxpc_vnumero_doc = Obe.doc_vnumero_nota_credito,
                tablc_iid_tipo_moneda = Obe.iid_moneda_nota_credito,
                doxpc_nmonto_total_saldo = (Obe.SaldoDXCNotaCredito + Obe.ncpap_nmonto_pago),
                doxpc_nmonto_total_pagado = (Obe.doxpc_nmonto_total_pagado - Obe.ncpap_nmonto_pago),
                SimboloMoneda = Obe.SimboloMoneda,
                tdodc_iid_correlativo = Obe.idd_correlativo_nota_credito
            };
            frm.codDXPPagoNC = Obe.ncpap_icod_correlativo;
            frm.codDXPPago = Convert.ToInt64(Obe.pdxpc_icod_correlativo);
            frm.bteDXCNotaCredito.Tag = Obe.doxpc_icod_correlativo_nota_credito;
            frm.bteDXCNotaCredito.Text = Obe.doc_vnumero_nota_credito;
            frm.deFechaDocumento.EditValue = Obe.ncpap_sfecha_pago;
            frm.lblMoneda.Text = Obe.SimboloMoneda;
            frm.txtMonto.Text = Obe.ncpap_nmonto_pago.ToString();
            frm.txtTipoCambio.Text = Obe.ncpap_nmonto_tipo_cambio.ToString();
            frm.txtObservacion.Text = Obe.ncpap_vdescripcion;
            frm.SetModify();
            frm.ShowDialog();
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocPorPagarNotaCredito ObeNC = (EDocPorPagarNotaCredito)grv.GetRow(grv.FocusedRowHandle);
                EDocPorPagarPago Obe = new EDocPorPagarPago()
                {
                    pdxpc_icod_correlativo = Convert.ToInt64(ObeNC.pdxpc_icod_correlativo),
                    doxpc_icod_correlativo = ObeNC.doxpc_icod_correlativo_nota_credito,
                    doxpc_icod_correlativo_pago = ObeNC.doxpc_icod_correlativo_pago,
                    pdxpc_nmonto_pago = ObeNC.ncpap_nmonto_pago,
                    pdxpc_nmonto_tipo_cambio = ObeNC.ncpap_nmonto_tipo_cambio,
                    tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoProveedor,
                    tablc_iid_tipo_moneda = ObeNC.iid_moneda_nota_credito
                };
                if (XtraMessageBox.Show("¿Está seguro de Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    /******EDGAR******/
                    //if (Convert.ToInt32(ObeNC.) > 0)
                    //{
                    //    EComprobante obj_EComprobante = new EComprobante();
                    //    obj_EComprobante.iid_voucher_contable = Convert.ToInt32(ObeNC.vcocc_iid_voucher_contable);
                    //    BComprobantes oBl = new BComprobantes();
                    //    oBl.EliminarComprobante(obj_EComprobante);
                    //}
                    /******EDGAR******/
                    EliminarPago(ObeNC, Obe);
                    Lista.Remove(ObeNC);
                    grv.RefreshData();
                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void EliminarPago(EDocPorPagarNotaCredito ObeNC, EDocPorPagarPago Obe)
        {

            Obe.strPc = WindowsIdentity.GetCurrent().Name;
            Obe.intUsuario = Valores.intUsuario;
            ObeNC.strPc = WindowsIdentity.GetCurrent().Name;
            ObeNC.intUsuario = Valores.intUsuario;
            new BCuentasPorPagar().EliminarPagoNotaCredito(ObeNC, Obe);
        }



        void form2_MiEvento()
        {
            Carga();
        }

        private void FrmConsultaPagosNotaCredito_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.MiEvento(Cab_icod_correlativo);
        }
    }
}