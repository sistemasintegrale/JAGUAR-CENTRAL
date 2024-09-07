using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Producción.Consulta
{
    public partial class rptKardex2 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptKardex2()
        {
            InitializeComponent();
        }
        public void cargar(string descPro, string fechaINI, string fechaFIN, List<EProdKardex> detalle)
        {

            xrlblEmpresa.Text = "EMPRESA: " + Modules.Valores.strNombreEmpresa;
            xrLabel1.Text = "ALMACÉN";
            if (fechaFIN == "01/01/0001 12:00:00 a.m.")
            {
                xrlblTitulo2.Text = "HASTA " + fechaINI.Substring(0, 10);
            }
            else
            {
                xrlblTitulo2.Text = "DESDE " + fechaINI.Substring(0, 10) + " HASTA " + fechaFIN.Substring(0, 10);
            }

            xrlblTitulo1.Text = "KARDEX DEL PRODUCTO: " + descPro;
            this.DataSource = detalle;

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

            lblIngresoTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "cantidad_ingreso", "")});
            lblsalidatotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "cantidad_salida", "")});
            lblSaldototal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "cantidad_saldo_prod", "")});
            this.ShowPreview();

        }
    }
}
