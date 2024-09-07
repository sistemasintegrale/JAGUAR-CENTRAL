using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Contabilidad.Registros
{
    public partial class rpt05ComprobanteRelacion : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt05ComprobanteRelacion()
        {
            InitializeComponent();
        }
        public void cargar(List<EVoucherContableCab> lista, string mes)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio;
            lblModulo.Text = "CONTABILIDAD";
            lblTitulo1.Text = "COMPROBANTES - " + mes.ToUpper() + " DE " + Parametros.intEjercicio;
            lblTitulo2.Text = "";

            this.DataSource = lista;
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("subdi_icod_subdiario") });

            lblCodSubdiario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "subdi_icod_subdiario", "{0:00}")});

            lblSubdiarioDes.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strSubdiario", "")});

            lblNumComp.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strNroVco", "")});

            lblGlosa.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_glosa", "")});

            lblMoneda.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strTipoMoneda", "")});

            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_fecha_vcontable", "{0:dd/MM/yyyy}")});

            lblTipoCambio.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_tipo_cambio", "")});

            lblEstado.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strVcoSituacion", "")});

            lblMovimientos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "intMovimientos", "")});

            lblDebeSol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_nmto_tot_debe_sol", "{0:N2}")});

            lblHaberSol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_nmto_tot_haber_sol", "{0:N2}")});

            lblDebeDol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_nmto_tot_debe_dol", "{0:N2}")});

            lblHaberDol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_nmto_tot_haber_dol", "{0:N2}")});

            lblDebeSolTotop.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_nmto_tot_debe_sol", "{0:N2}")});

            lblHaberSolTotop.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_nmto_tot_haber_sol", "{0:N2}")});

            lblDebeDolTotop.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_nmto_tot_debe_dol", "{0:N2}")});

            lblHaberDolTotop.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_nmto_tot_haber_dol", "{0:N2}")});

            //


            lblTotGenDebeSol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_nmto_tot_debe_sol", "{0:N2}")});

            lblTotGenHaberSol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_nmto_tot_haber_sol", "{0:N2}")});

            lblTotGenDebeDol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_nmto_tot_debe_dol", "{0:N2}")});

            lblTotGenHaberDol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vcocc_nmto_tot_haber_dol", "{0:N2}")});

            this.ShowPreview();
        }

        private void lblMoneda_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("tablc_iid_moneda").ToString() == "1")
                ((XRControl)sender).Text = "S/.";
            else if (GetCurrentColumnValue("tablc_iid_moneda").ToString() == "2")
                ((XRControl)sender).Text = "US$";
            else if (GetCurrentColumnValue("tablc_iid_moneda").ToString() == "3")
                ((XRControl)sender).Text = "HIS";
        }
    }
}
