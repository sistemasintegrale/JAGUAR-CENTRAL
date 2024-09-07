using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Almacen.Consultas
{
    public partial class rptPrecios : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPrecios()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<EProducto> lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;

            this.DataSource = lista;


            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "prdc_vcode_producto", "")});

            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "prdc_vdescripcion_larga", "")});

            lblUnidadMedida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strUnidadMedida", "")});

            lblCostoSol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dcmlCostoSol", "{0:N6}")});

            lblCostoDol.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dcmlCostoDol", "{0:N6}")});

            this.ShowPreview();
        }
    }
}
