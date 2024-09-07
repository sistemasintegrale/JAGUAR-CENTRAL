using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class rptStockValorizadoFecha : DevExpress.XtraReports.UI.XtraReport
    {
        public rptStockValorizadoFecha()
        {
            InitializeComponent();
        }
        public void carga(List<EKardexValorizado> Lista, string Titulo1, string Titulo2)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "TESORERIA";
            lblTitulo1.Text = Titulo1;
            lblTitulo2.Text = Titulo2;
            this.DataSource = Lista;
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("almcc_icod_almacen") });

            lblAlmacenCod.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "alco_iid_almacen_contable", "")});
            lblAlmacenDes.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesAlmacenCtbl", "")});

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "prdc_vcode_producto", "")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesProducto", "")});
            //lblEstado.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "estac_vdescripcion", "")});
            //lblSituacion.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "situc_vdescripcion", "")});
            lblStockActual.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Stock", "{0:N2}")});
            lblUme.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "unidc_vabreviatura_unidad_medida", "")});
            lblPcp.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_precio_costo_promedio", "{0:N2}")});
            lblMtoTotalDet.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_monto_total_compra", "{0:N2}")});

            lblMtoTotalGrp.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_monto_total_compra", "")});
            lblMtoTotalGen.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_monto_total_compra", "")});


            this.ShowPreview();

        }

    }
}
