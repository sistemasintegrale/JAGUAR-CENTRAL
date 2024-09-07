using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using SGE.Entity;
using SGE.WindowForms.Modules;

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
            lblEmpresa.Text = Valores.strNombreEmpresa  + " - AÑO " + Parametros.intEjercicio.ToString();
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
            new XRBinding("Text", Lista, "prdc_precio_costo", "{0:N2}")});
            lblMtoTotalDet.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "precio_compra_total", "{0:N2}")});

            lblMtoTotalGrp.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "precio_compra_total", "")});
            lblMtoTotalGen.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "precio_compra_total", "")});


            this.ShowPreview();

        }

    }
}
