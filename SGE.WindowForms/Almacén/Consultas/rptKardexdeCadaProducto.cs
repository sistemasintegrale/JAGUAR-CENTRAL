using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Almacén.Consultas
{
    public partial class rptKardexdeCadaProducto : DevExpress.XtraReports.UI.XtraReport
    {
        public rptKardexdeCadaProducto()
        {
            InitializeComponent();
        }
        public void carga(List<EKardex> Lista, string FechaI, string FechaF)
        {
            string[] almacen = Lista.Where(y => y.strAlmacen != "").Select(x => x.strAlmacen).ToArray();
            string strAlmacen = almacen.GetValue(0).ToString();
            lblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "MODULO: ALMACEN  ";
            lblTitulo1.Text = "RESUMEN DE KARDEX POR PRODUCTO";
            lblTitulo2.Text = "DEL " + FechaI + " AL " + FechaF;

            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("prdc_icod_producto") });
            lblproducto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strProducto", "")});

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strCodProducto", "")});

            lblUM.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardc_unidMed", "")});

            lblStock.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardc_stock", "")});

            this.DataSource = Lista;
            #region Detalle Grupo
            lblNumDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDocumento", "")});
            lblIngreso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dblIngreso", "{0:N2}")});
            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardc_fecha_movimiento", "{0:dd/MM/yy}")});
            lblMotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strMotivo", "")});
            lblSalida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dblSalida", "{0:N2}")});
            lblSaldo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dblSaldo", "{0:N2}")});
            lblReferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardc_beneficiario", "{0:N2}")});
            lblObservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "kardc_observaciones", "{0:N2}")});



            #endregion
            #region Footer Grupo

            lblTotalSalida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dblSalida", "")});

            lblTotalIngreso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dblIngreso", "")});

            lbltotalSaldo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dblSaldo", "{0:N2}")});
            #endregion
            this.ShowPreview();
        }

        private void lblTotalSalida_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            lblTotalSalida.Text = lblTotalSalida.Text;
        }


    }
}
