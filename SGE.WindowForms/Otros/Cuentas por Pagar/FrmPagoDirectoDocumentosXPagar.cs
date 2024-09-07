using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmPagoDirectoDocumentosXPagar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPagoDirectoDocumentosXPagar));

        public delegate void DelegadoMensaje(long Cab_icod_correlativo);
        public event DelegadoMensaje MiEvento;

        private List<EDocPorPagarPago> Lista = new List<EDocPorPagarPago>();
        public EDocPorPagar eDocXPagar = new EDocPorPagar();
        private int xposition = 0;
        public int mes;
        public long Cab_icod_correlativo;

        public FrmPagoDirectoDocumentosXPagar()
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
            gv.FocusedRowHandle = xposition;
        }

        private void FrmPagoDirectoDocumentosXPagar_Load(object sender, EventArgs e)
        {
            Carga();
            this.Text = "Pagos efectuados por " + eDocXPagar.tdocc_vabreviatura_tipo_doc + "-" + eDocXPagar.doxpc_vnumero_doc;
            gv.GroupPanelText = "Pagos efectuados por " + eDocXPagar.tdocc_vabreviatura_tipo_doc + "-" + eDocXPagar.doxpc_vnumero_doc;
        }

        private void Carga()
        {
            Lista = (new BCuentasPorPagar().listarDxpPagos(eDocXPagar.doxpc_icod_correlativo, Parametros.intEjercicio)).Where(ob => ob.pdxpc_vorigen == "D").ToList();
            grd.DataSource = Lista;
        }

        private void Nuevo_Click(object sender, EventArgs e)
        {
            if (eDocXPagar.doxpc_nmonto_total_saldo == 0)
            {
                XtraMessageBox.Show("El documento ya se encuentra cancelado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                using (FrmMantePagoDirectoDocumentosXPagar p = new FrmMantePagoDirectoDocumentosXPagar())
                {
                    p.MiEvento += new FrmMantePagoDirectoDocumentosXPagar.DelegadoMensaje(form2_MiEvento);
                    p.obeDocXPagar = eDocXPagar;
                    p.saldoGral = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo);
                    gv.MoveLast();
                    p.SetInsert();
                    p.cargar();
                    p.ShowDialog();
                }
            }
        }

        private void FrmPagoDirectoDocumentosXPagar_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento(Cab_icod_correlativo);
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
            EDocPorPagarPago Obe = (EDocPorPagarPago)gv.GetRow(gv.FocusedRowHandle);
            FrmMantePagoDirectoDocumentosXPagar p = new FrmMantePagoDirectoDocumentosXPagar();
            p.MiEvento += new FrmMantePagoDirectoDocumentosXPagar.DelegadoMensaje(Modify);

            p.cargar();
            if (Obe.tablc_iid_tipo_moneda == eDocXPagar.tablc_iid_tipo_moneda)
            {
                p.saldoGral = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo + Obe.pdxpc_nmonto_pago);
                p.pagoGral = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_pagado - Obe.pdxpc_nmonto_pago);
            }
            else
            {
                if (eDocXPagar.tablc_iid_tipo_moneda == Parametros.intSoles)
                {
                    p.saldoGral = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo + (Obe.pdxpc_nmonto_pago * Obe.pdxpc_nmonto_tipo_cambio));
                    p.pagoGral = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_pagado - (Obe.pdxpc_nmonto_pago * Obe.pdxpc_nmonto_tipo_cambio));
                }
                else
                {
                    p.saldoGral = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo + (Obe.pdxpc_nmonto_pago / Obe.pdxpc_nmonto_tipo_cambio));
                    p.pagoGral = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_pagado - (Obe.pdxpc_nmonto_pago / Obe.pdxpc_nmonto_tipo_cambio));
                }
            }

            if (p.saldoGral > eDocXPagar.doxpc_nmonto_total_documento)
            {
                p.saldoGral = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_documento);
                p.pagoGral = 0;
            }

            p.obeDocXPagar = eDocXPagar;
            p.obeDocXPagarPago = Obe;
            p.bteTipoDocumento.Tag = Obe.tdocc_icod_tipo_doc;
            p.bteTipoDocumento.Text = Obe.tdocc_vabreviatura_tipo_doc;
            p.txtNumeroDocumento.Text = Obe.pdxpc_vnumero_doc;
            p.deFechaDocumento.EditValue = Obe.pdxpc_sfecha_pago;
            p.LkpTipoMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
            p.txtMonto.Text = Obe.pdxpc_nmonto_pago.ToString();
            p.txtTipoCambio.Text = Obe.pdxpc_nmonto_tipo_cambio.ToString();
            p.txtObservacion.Text = Obe.pdxpc_vobservacion;
            p.bteCuenta.Tag = Obe.ctacc_iid_cuenta_contable;
            p.bteCuenta.Text = Obe.ctacc_numero_cuenta_contable;
            p.txtCuentaDes.Text = Obe.DescripcionCuentaContable;

            if (Obe.cecoc_icod_centro_costo != 0 && Obe.cecoc_icod_centro_costo != null)
            {
                p.bteCCosto.Tag = Obe.cecoc_icod_centro_costo;
                p.bteCCosto.Text = Obe.cecoc_ccodigo_centro_costo;
                p.txtcentrocosto.Text = Obe.cecoc_ccodigo_centro_costo;

                p.bteCCosto.Enabled = true;
            }

            if (Obe.anac_icod_analitica != 0 && Obe.anac_icod_analitica != null)
            {
                p.bteAnalitica.Tag = Obe.anac_icod_analitica;
                p.bteAnalitica.Text = Obe.TipoAnalitica.ToString();
                p.bteSubAnalitica.Tag = Obe.anac_icod_analitica;
                p.bteSubAnalitica.Text = Obe.anac_viid_analitica;

                p.bteAnalitica.Enabled = true;
                p.bteSubAnalitica.Enabled = true;
            }

            p.SetModify();
            p.LkpTipoMoneda.Enabled = false;
            p.bteTipoDocumento.Enabled = false;
            p.txtNumeroDocumento.Properties.ReadOnly = true;
            p.ShowDialog();
            xposition = gv.FocusedRowHandle;
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocPorPagarPago Obe = (EDocPorPagarPago)gv.GetRow(gv.FocusedRowHandle);
                if (XtraMessageBox.Show("¿Está seguro de Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    EliminarPago(Obe);

                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void EliminarPago(EDocPorPagarPago Obe)
        {
            Obe.doxpc_icod_correlativo_pago = Obe.doxpc_icod_correlativo;
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
            new BCuentasPorPagar().eliminarDxpPagoCanje(Obe);
            Lista.Remove(Obe);
            gv.RefreshData();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}