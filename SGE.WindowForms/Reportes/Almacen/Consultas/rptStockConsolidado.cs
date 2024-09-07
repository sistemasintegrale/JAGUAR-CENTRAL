using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Almacen.Consultas
{
    public partial class rptStockConsolidado : DevExpress.XtraReports.UI.XtraReport
    {
        public rptStockConsolidado()
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

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strCodProducto", "")});

            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strProducto", "")});

            lblUnidadMedida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strUnidadMedida", "")});

            lblStock.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblSaldo", "{0:N2}")});

            lblMarca.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "marc_vabreviatura", "")});

            lblModelo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "modc_vabreviatura", "")});

            this.ShowPreview();
        }
    }
}
