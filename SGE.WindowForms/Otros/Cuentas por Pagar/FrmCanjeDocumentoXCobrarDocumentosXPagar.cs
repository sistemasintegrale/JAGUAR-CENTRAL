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
    public partial class FrmCanjeDocumentoXCobrarDocumentosXPagar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCanjeDocumentoXCobrarDocumentosXPagar));

        public delegate void DelegadoMensaje(long Cab_icod_correlativo);
        public event DelegadoMensaje MiEvento;

        private List<EDocPorPagarPago> Lista = new List<EDocPorPagarPago>();
        public EDocPorPagar eDocXPagar = new EDocPorPagar();
        public long Cab_icod_correlativo;

        private int xposition = 0;
        public int mes;
        public string Mon = "";
        public int IcodMon = 0;


        public FrmCanjeDocumentoXCobrarDocumentosXPagar()
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

        private void FrmCanjeDocumentoXCobrarDocumentosXPagar_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento(Cab_icod_correlativo);
        }

        private void FrmCanjeDocumentoXCobrarDocumentosXPagar_Load(object sender, EventArgs e)
        {
            Carga();
            this.Text = "Pagos efectuados por Canje de " + eDocXPagar.tdocc_vabreviatura_tipo_doc + "-" + eDocXPagar.doxpc_vnumero_doc;
            grv.GroupPanelText = "Pagos efectuados por Canje de " + eDocXPagar.tdocc_vabreviatura_tipo_doc + "-" + eDocXPagar.doxpc_vnumero_doc;
        }

        private void Carga()
        {
            Lista = new BCuentasPorPagar().listarDxpPagos(eDocXPagar.doxpc_icod_correlativo, Parametros.intEjercicio).Where(ob => ob.pdxpc_vorigen == "X").ToList(); //X identifica que es canje
            grd.DataSource = Lista;
        }


        private void Nuevo_Click(object sender, EventArgs e)
        {
            using (FrmManteCanjeDocumentoXCobrarDocumentosXPagar p = new FrmManteCanjeDocumentoXCobrarDocumentosXPagar())
            {
                p.MiEvento += new FrmManteCanjeDocumentoXCobrarDocumentosXPagar.DelegadoMensaje(form2_MiEvento);
                p.objDXPPago = new EDocPorPagarPago()
                {
                    doxpc_icod_correlativo = eDocXPagar.doxpc_icod_correlativo,
                    tdocc_icod_tipo_doc = eDocXPagar.tdocc_icod_tipo_doc,
                    tdocc_vabreviatura_tipo_doc = eDocXPagar.Abreviatura,
                    tablc_iid_tipo_moneda = eDocXPagar.tablc_iid_tipo_moneda,
                    pdxpc_sfecha_pago = eDocXPagar.doxpc_sfecha_doc,
                    pdxpc_vobservacion = eDocXPagar.doxpc_vdescrip_transaccion,
                    doxpc_vnumero_doc = eDocXPagar.doxpc_vnumero_doc
                };
                //p.lblMoneda.Text = Mon;
                p.Mon = Mon;
                p.IcodMon = IcodMon;
                p.MontoSaldo = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo);
                grv.MoveLast();
                p.SetInsert();
                p.ShowDialog();
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
            if (Lista.Where(ob => ob.pdxpc_vorigen == "X").ToList().Count > 0)
            {
                EDocPorPagarPago Obe = (EDocPorPagarPago)grv.GetRow(grv.FocusedRowHandle);
                using (FrmManteCanjeDocumentoXCobrarDocumentosXPagar frm = new FrmManteCanjeDocumentoXCobrarDocumentosXPagar())
                {
                    frm.MiEvento += new FrmManteCanjeDocumentoXCobrarDocumentosXPagar.DelegadoMensaje(form2_MiEvento);
                    //EDocXCobrarDocxPagarCanje obeCanje = new BCuentasPorPagar().ListaDatosCanjexIcodDxpPago(Obe.pdxpc_icod_correlativo);
                    //EDocPorPagarPago objDXC = new BCuentasPorPagar().listarDxpPagos(Obe.pdxpc_icod_correlativo, Parametros.intEjercicio).Where(ob => ob.pdxpc_vorigen == "X").ToList();

                    frm.objDXPPago = Obe;
                    frm.vcocc_iid_voucher_contable = Convert.ToInt32(Obe.vcocc_iid_voucher_contable);
                    frm.btecliente.Tag = Obe.cliec_icod_cliente;
                    frm.btecliente.Text = Obe.cliec_vnombre_cliente;
                    frm.lblTipoDocumento.Tag = Obe.IcodTD;
                    frm.lblTipoDocumento.Text = Obe.TDDXC;
                    frm.bteDxC.Tag = Obe.IcodDXC;
                    frm.bteDxC.Text = Obe.NumDXC;
                    frm.lblMoneda.Tag = Obe.tablc_iid_tipo_moneda;
                    frm.lblMoneda.Text = Obe.Moneda;
                    frm.txtMonto.Text = Obe.pdxpc_nmonto_pago.ToString();
                    frm.deFechaDocumento.EditValue = Obe.pdxpc_sfecha_pago;
                    frm.txtTipoCambio.Text = Obe.pdxpc_nmonto_tipo_cambio.ToString();
                    frm.txtObservacion.Text = Obe.pdxpc_vobservacion;
                    frm.MontoSaldo = Convert.ToDecimal(eDocXPagar.doxpc_nmonto_total_saldo) + Convert.ToDecimal(Obe.pdxpc_nmonto_pago);
                    frm.doxpc_icod_correlativo = Convert.ToInt32(Obe.doxpc_icod_correlativo);
                    grv.MoveLast();
                    frm.SetModify();
                    frm.ShowDialog();
                }
            }
            else
            {
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {

                EDocPorPagarPago Obe = (EDocPorPagarPago)grv.GetRow(grv.FocusedRowHandle);
                if (XtraMessageBox.Show("¿Está seguro de Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //List<EDocPorPagarPago> obeCanje = new BCuentasPorPagar().listarDxpPagos(Obe.doxpc_icod_correlativo, Parametros.intEjercicio).Where(ob => ob.pdxpc_vorigen == "X").ToList();                    

                    //if (Convert.ToInt32(obeCanje.voucher_cont_dxp) > 0)
                    //{
                    //    if (XtraMessageBox.Show("El voucher contable correspondiente al canje también será eliminado\n\t\t\t\t¿Está seguro que desea continuar con la eliminación?", "Información del Sistema : Contabilidad", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                    //        return;
                    //}
                    int index = grv.FocusedRowHandle;
                    //new BCuentasPorPagar().eliminarDxpPagoDirecto(Obe, obeCanje);
                    new BCuentasPorPagar().eliminarDxpPagoDirecto(Obe);
                    Carga();
                    grv.FocusedRowHandle = index;

                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void EliminarPago(EDocXCobrarDocxPagarCanje obeCanje)
        {


            if (obeCanje.tipo_moneda_canje == obeCanje.tipo_moneda_dxp)
            {
                obeCanje.doxpc_nmonto_total_saldo += obeCanje.canjec_nmonto_pago;
                obeCanje.doxpc_nmonto_total_pagado -= obeCanje.canjec_nmonto_pago;
            }
            else
            {
                if (obeCanje.tipo_moneda_dxp == Parametros.intSoles)
                {
                    obeCanje.doxpc_nmonto_total_saldo += (obeCanje.canjec_nmonto_pago * obeCanje.canjec_nmonto_tipo_cambio);
                    obeCanje.doxpc_nmonto_total_pagado -= (obeCanje.canjec_nmonto_pago * obeCanje.canjec_nmonto_tipo_cambio);
                }
                else
                {
                    obeCanje.doxpc_nmonto_total_saldo += (obeCanje.canjec_nmonto_pago / obeCanje.canjec_nmonto_tipo_cambio);
                    obeCanje.doxpc_nmonto_total_pagado -= (obeCanje.canjec_nmonto_pago / obeCanje.canjec_nmonto_tipo_cambio);
                }
            }

            if (obeCanje.doxpc_nmonto_total_saldo > obeCanje.doxpc_nmonto_total_documento)
            {
                obeCanje.doxpc_nmonto_total_saldo = obeCanje.doxpc_nmonto_total_documento;
                obeCanje.doxpc_nmonto_total_pagado = 0;
            }

            obeCanje.doxcc_nmonto_saldo += obeCanje.canjec_nmonto_pago;
            obeCanje.doxcc_nmonto_pagado -= obeCanje.canjec_nmonto_pago;

            if (obeCanje.doxcc_nmonto_saldo > obeCanje.doxcc_nmonto_total)
            {
                obeCanje.doxcc_nmonto_saldo = obeCanje.doxcc_nmonto_total;
                obeCanje.doxcc_nmonto_pagado = 0;
            }
            obeCanje.strPc = WindowsIdentity.GetCurrent().Name;
            obeCanje.intUsuario = Valores.intUsuario;
            new BDocXCobrarDocxPagarCanje().EliminarCanjeDXCconDXP(obeCanje);
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}