using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Almacen.Consultas
{
    public partial class rptResumen : DevExpress.XtraReports.UI.XtraReport
    {
        public rptResumen()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<EKardex> lista)
        {

            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;

            this.DataSource = lista;

            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("strAlmacen") });

            lblAlmacen.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strAlmacen", "")});


            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strCodProducto", "")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strProducto", "")});
            lblUnidadMedida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strUnidadMedida", "")});
            lblStockAnt.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblSaldoAnterior", "{0:N2}")});
            lblIngreso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblIngreso", "{0:N2}")});
            lblSalida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblSalida", "{0:N2}")});
            lblSaldoActual.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblSaldo", "{0:N2}")});
            this.ShowPreview();

        }
    }
}
