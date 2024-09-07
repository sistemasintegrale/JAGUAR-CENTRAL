using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Producción.Almacenes
{
    public partial class rptProducto : DevExpress.XtraReports.UI.XtraReport
    {
        public rptProducto()
        {
            InitializeComponent();
        }
        public void cargar(List<EProdProducto> Listing)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa;
            xrlblTitulo1.Text = "CATÁLOGO DE PRODUCTOS";
            xrlblTitulo2.Text = "";
            xrLabel1.Text = "CENTRAL";

            this.DataSource = Listing;

            lblcodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "pr_vcodigo_externo", "")});

            lbldescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "pr_vdescripcion_producto", "")});

            lblAbreviatura.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "pr_vabreviatura_producto", "")});

            lblmarca.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "pr_viid_marca", "")});

            lblmodelo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "pr_viid_modelo", "")});

            lblcolor.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "pr_viid_color", "")});

            lbltalla.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "pr_viid_talla", "")});

            lblum.DataBindings.AddRange(new XRBinding[]{
            new XRBinding("Text",Listing,"abreviaUnidadMedi","")});

            this.ShowPreview();
        }

        //internal static void cargar(List<EProdProducto> mlist)
        //{
        //    //throw new NotImplementedException();
        //}
    }
}
