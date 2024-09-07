using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Tesorería.Consultas
{
    public partial class rpt04PagosClientes : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt04PagosClientes()
        {
            InitializeComponent();
        }
        public void carga(List<ECobranzaAbonoCuentaContable> Lista, string FechaI, string FechaF)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "TESORERIA";
            lblTitulo1.Text = "PAGOS EFECTUADOS POR CLIENTES";
            lblTitulo2.Text = "DEL " + FechaI + " AL " + FechaF;
            this.DataSource = Lista;
            #region Detalle Detalle            
            lblCliente.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cliec_vnombre", "")});
            lblFechaPago.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "coabc_sfecha_pago_planilla_cobranza", "{0:dd/MM/yyyy}")});
            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "coabc_vnumero_documento", "")});
            lblFechaDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "coabc_sfecha_documento", "{0:dd/MM/yyyy}")});
            lblPagoSoles.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "coabc_nmonto_pago_soles", "{0:N2}")});
            lblPagoDolares.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "coabc_nmonto_pago_dolares", "{0:N2}")});
            lblObservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "coabc_vobservacion", "")});
            #endregion
            this.ShowPreview();
        }


    }
}
