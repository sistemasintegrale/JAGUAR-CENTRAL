using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Compras.Consultas_de_Compras
{
    public partial class rpt01ConsultaDocCompra : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt01ConsultaDocCompra()
        {
            InitializeComponent();
        }


        public void cargar(List<EDocCompraDet> lista, string fecha1, string fecha2)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa + "- AÑO " + Parametros.intEjercicio;
            xrlblTitulo1.Text = "RELACIÓN DE DOCUMENTOS DE COMPRA";
            xrlblTitulo2.Text = (fecha1 == "") ? "" : String.Format("DESDE: {0} HASTA:{1}", fecha1, fecha2);



            this.DataSource = lista;

            #region Detalle del Reporte
            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dtFecha", "{0:dd/MM/yyyy}")});

            lblTipoDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strTipoDoc", "")});

            lblNroDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strNroDoc", "")});

            lblNroReg.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "intCorrelativo", "{0:0000}")});

            lblProveedor.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strProveedor", "")});

            lblPlaca.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "facd_nro_placa", "")});

            lblProducto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "facd_vdescripcion_item", "")});

            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "facd_ncantidad", "{0:N2}")});

            lblPrecio.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "facd_nmonto_unit", "{0:N5}")});

            lblTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "facd_nmonto_total", "{0:N2}")});

            lblComprador.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strComprador", "")});
            #endregion
            #region Pie del Reporte

            lblTotalCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "facd_ncantidad", "{0:N2}")});

            lblMontoTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "facd_nmonto_total", "{0:N2}")});

            #endregion
            this.ShowPreview();
        }
    }
}
