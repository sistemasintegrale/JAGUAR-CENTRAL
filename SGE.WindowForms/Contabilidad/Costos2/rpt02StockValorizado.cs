using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using SGE.Entity;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class rpt02StockValorizado : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt02StockValorizado()
        {
            InitializeComponent();
        }

        public void Cargar(EKardexValorizado cab, List<EKardexValorizado> Lista, string titulo2)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblModulo.Text = "CONTABILIDAD";
            lblTitulo1.Text = "KARDEX VALORIZADO POR PRODUCTO";
            lblTitulo2.Text = titulo2;
            //lblRptCodigo.Text = codigo;
            this.DataSource = Lista;           

            #region Header
            lblCodigoPr.Text = cab.prdc_icod_producto.ToString();
            lblDescripcionPr.Text = cab.strDesProducto;
            lblUM.Text = cab.unidc_vabreviatura_unidad_medida;
            lblSituacion.Text = cab.situc_vdescripcion;
            //lblEstado.Text = cab.estac_vdescripcion;           
            #endregion           

            #region Detail
            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_sfecha_movimiento", "{0:dd/MM/yyyy}")});
            lblMotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Motivo", "")});
            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strTipoNroDoc", "")});            
            lblIngresoU.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dcmlIngreso", "{0:N2}")});
            lblSalidasU.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dcmlSalida", "{0:N2}")});
            lblSaldoU.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Stock", "{0:N2}")});
            lblPpp.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_precio_costo_promedio", "")});
            lblIngresosC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_monto_total_compra", "{0:N2}")});
            lblSalidasC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "SalidaCosto", "{0:N2}")});
            lblSaldoC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "StockCosto", "{0:N2}")});            
            lblBeneficiario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_vbeneficiario", "")});
            lblObservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardv_vobservaciones", "")});

            
            #endregion

            #region Footer
            lblIngresosUTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dcmlIngreso", "{0:N2}")});
            lblSalidasUTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dcmlSalida", "{0:N2}")});

            lblIngresosCTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "IngresoCosto", "{0:N2}")});
            lblSalidasCTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "SalidaCosto", "{0:N2}")});
           
            #endregion
            this.ShowPreview();
        }

    }
}
