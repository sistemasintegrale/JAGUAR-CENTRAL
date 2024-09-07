using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmPagoAdelanto : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPagoAdelanto));

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        private List<EAdelantoPago> Lista = new List<EAdelantoPago>();

        public EDocXCobrar eDocXCobrar = new EDocXCobrar();


        private int xposition = 0;
        public int mes;


        public FrmPagoAdelanto()
        {
            InitializeComponent();
        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
            grv.FocusedRowHandle = xposition;
        }

        private void FrmPagoAdelanto_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        private void FrmPagoAdelanto_Load(object sender, EventArgs e)
        {
            Carga();
            this.Text = "Pagos efectuados por Adelantos de " + eDocXCobrar.Abreviatura + "-" + eDocXCobrar.doxcc_vnumero_doc;
            grv.GroupPanelText = "Pagos efectuados por Adelantos de " + eDocXCobrar.Abreviatura + "-" + eDocXCobrar.doxcc_vnumero_doc;
        }

        private void Carga()
        {
            Lista = new BCuentasPorCobrar().ListarAdelantoPago(eDocXCobrar.doxcc_icod_correlativo, 0);
            grd.DataSource = Lista;
        }


        private void Nuevo_Click(object sender, EventArgs e)
        {
            if (eDocXCobrar.doxcc_nmonto_saldo == 0)
            {
                XtraMessageBox.Show("El documento ya se encuentra cancelado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                using (FrmMantePagoAdelanto p = new FrmMantePagoAdelanto())
                {
                    p.MiEvento += new FrmMantePagoAdelanto.DelegadoMensaje(form2_MiEvento);
                    p.obeDocXCobrar = eDocXCobrar;
                    p.saldoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_saldo);
                    grv.MoveLast();
                    p.SetInsert();
                    p.ShowDialog();
                }
            }
        }


        private void Modificar_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                Datos();
            }
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void Datos()
        {
            EAdelantoPago Obe = (EAdelantoPago)grv.GetRow(grv.FocusedRowHandle);
            FrmMantePagoAdelanto frm = new FrmMantePagoAdelanto();
            frm.MiEvento += new FrmMantePagoAdelanto.DelegadoMensaje(form2_MiEvento);

            if (Obe.tablc_iid_tipo_moneda == eDocXCobrar.tablc_iid_tipo_moneda)
            {
                frm.saldoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_saldo + Obe.adpac_nmonto_pago);
                frm.pagoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_pagado - Obe.adpac_nmonto_pago);
            }
            else
            {
                if (eDocXCobrar.tablc_iid_tipo_moneda == Parametros.intSoles)
                {
                    frm.saldoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_saldo + (Obe.adpac_nmonto_pago * Obe.adpac_nmonto_tipo_cambio));
                    frm.pagoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_pagado - (Obe.adpac_nmonto_pago * Obe.adpac_nmonto_tipo_cambio));
                }
                else
                {
                    frm.saldoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_saldo + (Obe.adpac_nmonto_pago / Obe.adpac_nmonto_tipo_cambio));
                    frm.pagoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_pagado - (Obe.adpac_nmonto_pago / Obe.adpac_nmonto_tipo_cambio));
                }
            }

            if (frm.saldoGralDxC > eDocXCobrar.doxcc_nmonto_total)
            {
                frm.saldoGralDxC = Convert.ToDecimal(eDocXCobrar.doxcc_nmonto_total);
                frm.pagoGralDxC = 0;
            }

            frm.obeDocXCobrar = eDocXCobrar;
            frm.obeDocXCobrarAD = new EDocXCobrar() { doxcc_icod_correlativo = Obe.doxcc_icod_correlativo_adelanto, doxcc_vnumero_doc = Obe.doxcc_vnumero_doc, tablc_iid_tipo_moneda = Obe.tablc_iid_tipo_moneda, doxcc_nmonto_saldo = (Obe.SaldoDXCAdelanto + Obe.adpac_nmonto_pago), doxcc_nmonto_pagado = (Obe.doxcc_nmonto_pagado - Obe.adpac_nmonto_pago), SimboloMoneda = Obe.SimboloMoneda, tdodc_iid_correlativo = Obe.tdocc_iid_correlativo_adelanto, cliec_icod_cliente = Obe.cliec_icod_cliente };
            frm.codDXCPagoAD = Obe.adpac_icod_correlativo;
            frm.codDXCPago = Convert.ToInt64(Obe.pdxcc_icod_correlativo);
            frm.bteDXCAdelanto.Tag = Obe.doxcc_icod_correlativo_adelanto;
            frm.bteDXCAdelanto.Text = Obe.vnumero_doc_adelanto;
            frm.deFechaDocumento.EditValue = Obe.adpac_sfecha_pago;
            frm.lblMoneda.Text = Obe.SimboloMoneda;
            frm.txtMonto.Text = Obe.adpac_nmonto_pago.ToString();
            frm.txtTipoCambio.Text = Obe.adpac_nmonto_tipo_cambio.ToString();
            frm.txtObservacion.Text = Obe.adpac_vdescripcion;

            frm.SetModify();
            frm.ShowDialog();
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EAdelantoPago ObeAD = (EAdelantoPago)grv.GetRow(grv.FocusedRowHandle);
                EDocXCobrarPago Obe = new EDocXCobrarPago()
                {

                    doxcc_icod_correlativo = ObeAD.doxcc_icod_correlativo_pago,
                    doxcc_icod_correlativo_pago = ObeAD.doxcc_icod_correlativo_adelanto,
                    pdxcc_nmonto_cobro = ObeAD.adpac_nmonto_pago,
                    pdxcc_nmonto_tipo_cambio = ObeAD.adpac_nmonto_tipo_cambio,
                    tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente,
                    tablc_iid_tipo_moneda = ObeAD.tablc_iid_tipo_moneda
                };
                if (XtraMessageBox.Show("¿Está seguro de Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    EliminarPago(ObeAD, Obe);
                Lista.Remove(ObeAD);
                grv.RefreshData();
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void EliminarPago(EAdelantoPago ObeAD, EDocXCobrarPago Obe)
        {

            Obe.strPc = WindowsIdentity.GetCurrent().Name;
            Obe.intUsuario = Valores.intUsuario;
            ObeAD.adpac_vpc_modifica = WindowsIdentity.GetCurrent().Name;
            ObeAD.adpac_iusuario_modifica = Valores.intUsuario;
            new BCuentasPorCobrar().EliminarPagoAdelanto(ObeAD, Obe);
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}