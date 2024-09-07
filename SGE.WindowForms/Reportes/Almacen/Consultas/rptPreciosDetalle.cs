using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Almacen.Consultas
{
    public partial class rptPreciosDetalle : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPreciosDetalle()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<EDocCompraDet> lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;

            this.DataSource = lista;


            lblProveedor.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strProveedor", "")});

            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "facd_ncantidad", "{0:N2}")});

            lblMoneda.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strMoneda", "")});

            lblMontoUnitario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "facd_nmonto_unit", "{0:N6}")});

            lblMTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "facd_nmonto_total", "{0:N2}")});

            lblTippoDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strTipoDoc", "")});

            lblNroDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strNroDoc", "")});

            lblFechaDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dtFecha", "{0:dd/MM/yyyy}")});


            this.ShowPreview();
        }
    }
}
