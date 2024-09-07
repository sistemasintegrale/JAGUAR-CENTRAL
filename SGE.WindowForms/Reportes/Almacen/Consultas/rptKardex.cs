using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Almacen.Consultas
{
    public partial class rptKardex : DevExpress.XtraReports.UI.XtraReport
    {
        public rptKardex()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<EKardex> detalle)
        {

            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;

            this.DataSource = detalle;

            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "kardc_fecha_movimiento", "")});
            lbldocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "strDocumento", "")});
            lblmotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "strMotivo", "")});
            lblobservacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "kardc_observaciones", "")});
            lblreferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "kardc_beneficiario", "")});
            lblIngreso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "dblIngreso", "")});
            lblsalida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "dblSalida", "")});
            lblSaldo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "dblSaldo", "")});
            this.ShowPreview();

        }
    }
}
