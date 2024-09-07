using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Ventas.Consultas_de_Cuentas_Corrientes
{
    public partial class rptEstadoCuentaRangoFecha : DevExpress.XtraReports.UI.XtraReport
    {
        public rptEstadoCuentaRangoFecha()
        {
            InitializeComponent();
        }
        public void cargar(DateTime FechaInicio, List<ECliente> lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "VENTAS";
            lblTitulo1.Text = "ESTADO DE CUENTA DE LOS CLIENTES AL " + FechaInicio.Date.ToShortDateString();


            this.DataSource = lista;


            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cliec_vcod_cliente", "")});

            lblRazonSocial.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cliec_vnombre_cliente", "")});

            lblMontoUS.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "MontoUS", "{0:N2}")});

            lbl0_30.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dias_0_30", "{0:N2}")});

            lbl31_60.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dias_31_60", "{0:N2}")});

            lbl61_90.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dias_61_90", "{0:N2}")});

            lbl91_120.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dias_91_120", "{0:N2}")});

            lbl121_180.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dias_121_180", "{0:N2}")});

            lbl180_mas.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dias_181_mas", "{0:N2}")});

            /*Totales*/
            lblMontoUSSuma.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "MontoUS", "{0:N2}")});

            lbl0_30Suma.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dias_0_30", "{0:N2}")});

            lbl31_60Suma.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dias_31_60", "{0:N2}")});

            lbl61_90Suma.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dias_61_90", "{0:N2}")});

            lbl91_120Suma.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dias_91_120", "{0:N2}")});

            lbl121_180Suma.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dias_121_180", "{0:N2}")});

            lbl180_masSuma.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dias_181_mas", "{0:N2}")});

            this.ShowPreview();
        }
    }
}
