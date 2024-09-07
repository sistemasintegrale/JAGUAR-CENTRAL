using DevExpress.XtraReports.UI;
using System.Data;

namespace SGE.WindowForms.Producción.Consulta
{
    public partial class rptRelaciondeCatalogo : DevExpress.XtraReports.UI.XtraReport
    {
        public rptRelaciondeCatalogo()
        {
            InitializeComponent();
        }
        public void cargar(DataTable lista, string fecha, string numeroinventario, string tienda)
        {
            xrlblEmpresa.Text = "EMPRESA: " + tienda;
            xrlblTitulo1.Text = "CATÁLOGO DE PRODUCTOS";
            xrLabel1.Text = "SISTEMA PUNTO VENTA";

            //Agrupación por Producto Específico
            this.DataSource = lista;
            lblcodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "pr_vcodigo_externo", "")});
            //Enlaces con el Detalle


            lbldescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "pr_vdescripcion_producto", "")});

            lblUm.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "abreviaUnidadMedi", "")});

            lblobservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "observaciones", "")});


        }
    }
}
