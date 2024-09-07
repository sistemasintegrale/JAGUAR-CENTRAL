using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Producción.Consulta
{
    public partial class rptNotaIngresoSinDetalle : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNotaIngresoSinDetalle()
        {
            InitializeComponent();
        }
        public void cargar(List<EProdNotaIngreso> lista, string fechaInic, string fechafin, string almac)
        {
            xrlblEmpresa.Text = "EMPRESA " + Modules.Valores.strNombreEmpresa;
            xrlblTitulo1.Text = " DEL " + fechaInic + " AL " + fechafin;
            xrlblTitulo2.Text = "NOTA DE INGRESO DEL " + almac;
            xrLabel3.Text = "CENTRAL";

            this.DataSource = lista;

            lblnota.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_vnumero_nota_ingreso", "")});

            lblfecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_sfecha_nota_ingreso", "{0:dd/MM/yyyy}")});

            lblmotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "descripcionMotivo", "")});

            lbldocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "Documento", "")});

            lblReferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_vreferencia", "")});

            lblObservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_vobservaciones", "")});

            lblcantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cantidadNota", "")});

            lbltotalcantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cantidadNota", "")});

            this.ShowPreview();
        }
    }
}
