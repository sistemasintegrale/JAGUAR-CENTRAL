using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class rptKardexValorizadoPorFechaMotivo : DevExpress.XtraReports.UI.XtraReport
    {
        public rptKardexValorizadoPorFechaMotivo()
        {
            InitializeComponent();
        }
        public void carga(List<EKardexValorizado> Lista, string Titulo1, string Titulo2)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "COSTOS";
            lblTitulo1.Text = Titulo1;
            lblTitulo2.Text = Titulo2;
            this.DataSource = Lista;

            #region Detail
            lblAlmacen.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesAlmacenCtbl", "")});
            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_sfecha_movimiento", "")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesProducto", "")});
            //lblEstado.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "estac_vdescripcion", "")});
            //lblSituacion.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "situc_vdescripcion", "")});
            lblUnidadMedida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "unidc_vabreviatura_unidad_medida", "")});
            /*-----------------------------------------------------------------------------------*/
            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Documento", "")});
            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_icantidad_prod", "{0:N2}")});
            lblPPP.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_precio_costo_promedio", "{0:N4}")});
            lblMtoCosto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_monto_total_costo", "{0:N2}")});
            lblMtoCompra.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_monto_total_compra", "{0:N2}")});
            lblMtoUnitario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_monto_unitario_compra", "{0:N4}")});
            lblReferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_vbeneficiario", "")});
            /*-----------------------------------------------------------------------------------*/
            #endregion
            #region Footer
            lblCantidadTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_icantidad_prod", "")});
            lblMtoCostoTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_monto_total_costo", "")});
            lblMtoCompraTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_monto_total_compra", "")});

            #endregion
            this.ShowPreview();

        }
    }
}
