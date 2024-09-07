using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Contabilidad.Registros
{
    public partial class rpt05Comprobantes : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt05Comprobantes()
        {
            InitializeComponent();
        }
        public void cargar(EVoucherContableCab cab, List<EVoucherContableDet> lista, string mes)
        {
            lblEmpresa.Text = String.Format("{0} - AÑO {1}", Modules.Valores.strNombreEmpresa, Parametros.intEjercicio);
            lblModulo.Text = "CONTABILIDAD";
            lblTitulo1.Text = "MOVIMIENTO DE COMPROBANTE - " + mes.ToUpper() + " DE " + cab.anioc_iid_anio;
            lblTitulo2.Text = "";
            this.DataSource = lista;
            #region Header
            string TipoMoneda = (cab.tablc_iid_moneda == 1) ? "M.N." : "M.E.";
            string Fecha = String.Format("{0:dd/MM/yyyy}", cab.vcocc_fecha_vcontable);
            lblDescripcion.Text = String.Format("{0:00}.{1} - {2} - {3} - {4}", cab.subdi_icod_subdiario, cab.vcocc_numero_vcontable,
                cab.vcocc_glosa, TipoMoneda, cab.vcocc_tipo_cambio);
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
            #endregion
            this.ShowPreview();
        }

    }
}
