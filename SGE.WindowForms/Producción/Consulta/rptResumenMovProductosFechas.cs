using DevExpress.XtraReports.UI;
using System.Data;

namespace SGE.WindowForms.Producción.Consulta
{
    public partial class rptResumenMovProductosFechas : DevExpress.XtraReports.UI.XtraReport
    {
        public rptResumenMovProductosFechas()
        {
            InitializeComponent();
        }
        public void cargar(DataTable lista, string fechaInic, string fechafin)
        {
            xrlblEmpresa.Text = "EMPRESA " + Modules.Valores.strNombreEmpresa;
            xrlblTitulo1.Text = "MOVIMIENTOS DEL " + fechaInic + " AL " + fechafin;
            xrLabel1.Text = "SISTEMA PUNTO VENTA";

            //Agrupación por Producto Específico
            this.DataSource = lista;

            //Enlaces con el Detalle


            grupoAlmacen.GroupFields.AddRange(new GroupField[] { new GroupField("kardc_iidalmacen") });
            lblAlmacen.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "descripcionAlmacen", "")});


            lbldescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "pr_vdescripcion_producto", "")});

            lblUm.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "DesUnidadMedida", "{0:dd/MM/yyyy}")});


            lblstkAnt.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "stockaAnterior", "")});


            lblStkSalida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "stockActual", "")});


            lblIngreso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "tipo_movimiento_ingreso", "")});

            lblsalida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "tipo_movimiento_salida", "")});




            lblstkAnttotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "stockaAnterior", "")});


            lblStkSalidatotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "stockActual", "")});


            lblIngresototal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "tipo_movimiento_ingreso", "")});

            lblsalidatotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "tipo_movimiento_salida", "")});
        }
    }
}
