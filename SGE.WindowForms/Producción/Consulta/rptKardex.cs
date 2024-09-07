using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Producción.Consulta
{
    public partial class rptKardex : DevExpress.XtraReports.UI.XtraReport
    {
        public rptKardex()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<EProdKardex> detalle)
        {

            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;

            this.DataSource = detalle;

            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "kardx_sfecha", "")});
            lbldocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "NroDocumento", "")});
            lblmotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "vmotivo", "")});
            lblobservacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "Observaciones", "")});
            lblreferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "Beneficiario", "")});
            lblIngreso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "cantidad_ingreso", "")});
            lblsalida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "cantidad_salida", "")});
            lblSaldo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "cantidad_saldo_prod", "")});
            this.ShowPreview();

        }
    }
}
