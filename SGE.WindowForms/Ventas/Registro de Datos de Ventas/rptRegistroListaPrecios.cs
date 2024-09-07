using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace GE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class rptRegistroListaPrecios : DevExpress.XtraReports.UI.XtraReport
    {
        public rptRegistroListaPrecios()
        {
            InitializeComponent();
        }
        public void cargar(List<EProdListaPrecios> Listing)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa;
            xrlblTitulo1.Text = "LISTA DE PRECIOS";
            xrlblTitulo2.Text = "";
            xrLabel1.Text = "CENTRAL";

            this.DataSource = Listing;

            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("lprec_icod_precio") });

            lbldescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "pr_vdescripcion_producto", "")});

            llbPrecioCostocabecera.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "lprec_nprecio_costo", "")});

            lblmargen.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "lprec_nporc_utilidad", "")});

            lblprecionMinoriCabecera.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "lprec_nprecio_vtamin", "")});

            lblprecioMayorCabecera.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "lprec_nprecio_vtamay", "")});

            lblPrecioRemCab.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "lprec_nprecio_vtaotros", "")});


            lblserie.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "lpred_vrango_tallas", "")});

            lblprecioMinordet.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "lpred_nprecio_vtamin", "")});

            lblprecioMayordet.DataBindings.AddRange(new XRBinding[]{
            new XRBinding("Text",Listing,"lpred_nprecio_vtamay","")});

            lblprecioRemdet.DataBindings.AddRange(new XRBinding[]{
            new XRBinding("Text",Listing,"lpred_nprecio_vtaotros","")});

            this.ShowPreview();
        }
    }
}
