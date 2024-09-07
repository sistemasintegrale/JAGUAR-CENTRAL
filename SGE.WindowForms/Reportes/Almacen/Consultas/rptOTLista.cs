using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Almacen.Consultas
{
    public partial class rptOTLista : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOTLista()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<EOrdenTrabajo> lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "OPERACIONES";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;

            this.DataSource = lista;


            lblNroOT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "otrc_vnumero_orden", "")});

            lblNroProforma.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strNroProforma", "")});

            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "otrc_sfecha_orden", "{0:dd/MM/yyyy}")});

            lblCliente.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strDesCliente", "")});

            lblPlaca.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strPlaca", "")});

            lblMarca.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strMarca", "")});

            lblModelo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strModelo", "")});

            lblAnio.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strAnioVehiculo", "")});

            lblSituacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strSituacion", "")});

            lblTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "otrc_nmonto_total", "{0:N2}")});

            lblTotalGlobal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "otrc_nmonto_total", "{0:N2}")});

            this.ShowPreview();
        }
    }
}
