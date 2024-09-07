using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Producción.Consulta
{
    public partial class rptNotaIngresoConDetalle : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNotaIngresoConDetalle()
        {
            InitializeComponent();
        }
        public void cargar(List<EProdNotaIngresoDetalle> lista, string fechaInicio, string fechafinal, string desalmacen)
        {
            xrlblEmpresa.Text = "EMPRESA " + Modules.Valores.strNombreEmpresa;
            xrlblTitulo1.Text = " DEL " + fechaInicio + " AL " + fechafinal;
            xrlblTitulo2.Text = "NOTA DE INGRESO DEL " + desalmacen;
            xrLabel1.Text = "CENTRAL";


            this.DataSource = lista;

            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("ningc_icod_nota_ingreso") });


            lblnota.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_vnumero_nota_ingreso", "")});

            lblfecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_sfecha_nota_ingreso", "{0:dd/MM/yyyy}")});

            lblmotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "descripcionMotivo", "")});

            lblReferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_vreferencia", "")});

            lbldocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "Documento", "")});

            lblObservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_vobservaciones", "")});




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

            lbltotalcantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "pr_ncant_total_producto", "")});

            this.ShowPreview();
        }
    }
}
