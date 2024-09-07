using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Producción.Almacenes
{
    public partial class rptNotaIngresoReporte : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNotaIngresoReporte()
        {
            InitializeComponent();
        }
        public void cargar(List<EProdNotaIngresoDetalle> lista)
        {
            xrlblEmpresa.Text = "EMPRESA " + Modules.Valores.strNombreEmpresa;
            xrlblTitulo1.Text = "";
            xrlblTitulo2.Text = "NOTA DE INGRESO Nº: " + lista[0].ningc_vnumero_nota_ingreso;
            xrLabel1.Text = "CENTRAL";


            this.DataSource = lista;

            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("ningc_icod_nota_ingreso") });




            lblmotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "descripcionMotivo", "")});

            lblReferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_vreferencia", "")});

            lblObservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_vobservaciones", "")});

            lbldocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "Documento", "")});



            lblitem.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningcd_num_item", "{0:000}")});

            lblcodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "pr_vcodigo_externo", "")});

            lblProducto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "pr_vdescripcion_producto", "")});

            lblCant.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "pr_ncant_total_producto", "")});

            lbltallas.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "TallaAcumulada", "")});

            lblCantiTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "pr_ncant_total_producto", "")});

            this.ShowPreview();
        }
    }
}
