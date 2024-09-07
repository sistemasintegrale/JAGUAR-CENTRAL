using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Tesorería.Bancos
{
    public partial class rtpComprobanteBanco1 : DevExpress.XtraReports.UI.XtraReport
    {
        public rtpComprobanteBanco1()
        {
            InitializeComponent();
        }
        public void cargar(ELibroBancos Obe, EVoucherContableCab ObeComp, List<EVoucherContableDet> lista, string mes, string banco, string cuenta, string rutaImg)
        {
            string ruta = "";
            int CountRuta = 0;
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio;
            lblModulo.Text = "TESORERIA";
            string comprobante = string.Format("{0:00}", ObeComp.strSubdiario) + "." + ObeComp.vcocc_numero_vcontable;
            //lblTitulo1.Text = "VOUCHER " + Obe.TipoDocumento + " [" + comprobante + "] " + mes.ToUpper() + " DE " + Obe.iid_anio;
            lblTitulo1.Text = "VOUCHER " + Obe.TipoDocumento + " - " + mes.ToUpper() + " DE " + Obe.iid_anio;
            lblTitulo2.Text = String.Format("N° Correlativo: [{0:00000}]", Obe.iid_correlativo);
            if (Obe.iid_situacion_movimiento_banco == 2)//anterior 2
            {
                pxAnulado.Visible = true;
                this.pxAnulado.Image = Properties.Resources.anulado2;
            }

            string ordenDe = "";

            if (Obe.proc_vnombrecompleto != null)
                ordenDe = Obe.proc_vnombrecompleto;
            else if (Obe.cliec_vnombre_cliente != null)
                ordenDe = Obe.cliec_vnombre_cliente;
            else
                ordenDe = Obe.vdescripcion_beneficiario;

            this.DataSource = lista;
            #region Header
            lblOrdenDe.Text = ordenDe;
            lblTipoMoneda.Text = (Obe.iid_tipo_moneda == 3) ? "S/." : "US$";
            lblMontoNumero.Text = "*****" + Obe.nmonto_movimiento.ToString("N2");
            lblMontoLetras.Text = Convertir.ConvertNumeroEnLetras(Obe.nmonto_movimiento.ToString());
            lblBancoCuenta.Text = banco + " - " + cuenta;
            lblConcepto.Text = Obe.vglosa;
            lblDocumentoCab.Text = Obe.TipoDocAbreviado + " - " + Obe.vnro_documento;
            lblFechaMovimiento.Text = String.Format("{0:dd/MM/yy}", Obe.dfecha_movimiento);
            lblTipoCambio.Text = Obe.nmonto_tipo_cambio.ToString("n4");

            #endregion
            #region Detail
            lblSec.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocd_nro_item_det", "{0:000}")});
            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strTipNroDocumento", "")});
            lblCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strNroCuenta", "")});
            lblAnalisis.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strAnalisis", "")});
            lblCCosto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strCodCCosto", "")});
            lblGlosa.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocd_vglosa_linea", "")});
            lblDebeSol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblHaberSol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblDebeDol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocd_nmto_tot_debe_dol", "{0:N2}")});
            lblHaberDol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocd_nmto_tot_haber_dol", "{0:N2}")});
            #endregion
            #region Footer
            lblDebeSolTot.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocd_nmto_tot_debe_sol", "")});

            lblHaberSolTot.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocd_nmto_tot_haber_sol", "")});

            lblDebeDolTot.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocd_nmto_tot_debe_dol", "")});

            lblHaberDolTot.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocd_nmto_tot_haber_dol", "")});
            PxLetra.Visible = Obe.imagen is null ? false : true;
            var pictureBox = (PxLetra);
            pictureBox.Image = Obe.imagen;





            #endregion
            this.ShowPreview();
        }
    }
}
