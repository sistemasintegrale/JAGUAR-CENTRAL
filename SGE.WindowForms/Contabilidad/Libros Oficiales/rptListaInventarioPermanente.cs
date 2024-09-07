using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Contabilidad.Libros_Oficiales
{
    public partial class rptListaInventarioPermanente : DevExpress.XtraReports.UI.XtraReport
    {
        public rptListaInventarioPermanente()
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


            //lblAlmacen.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", lista, "strAlmacen", "")});

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strCodProducto", "")});

            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strProducto", "")});

            lblUnidadMedida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strUnidadMedida", "")});

            lblSaldoActual.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblSaldo", "{0:N2}")});

            lblSaldoAnterior.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "SaldoAnterior", "{0:N2}")});

            lblIngreso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cantidadIngreso", "{0:N2}")});

            lblSalida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cantidadSalida", "{0:N2}")});

            this.ShowPreview();
        }
    }
}
