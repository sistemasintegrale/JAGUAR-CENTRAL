using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Almacen.Consultas
{
    public partial class rptKardexProducto : DevExpress.XtraReports.UI.XtraReport
    {
        public rptKardexProducto()
        {
            InitializeComponent();
        }
        public void cargar(EKardex obe, List<EKardex> lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblAnio.Text = DateTime.Now.Year.ToString();
            lblNombreProducto.Text = obe.strProducto;

            this.DataSource = lista;


            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "kardc_fecha_movimiento", "{0:MM/dd/yyyy}")});

            lblMotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strMotivo", "")});

            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strDocumento", "")});

            ///////////////////////////////////////////////////

            lblIngreso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblIngreso", "{0:N2}")});

            lblSalidas.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblSalida", "{0:N2}")});

            lblSaldo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblSaldo", "{0:N2}")});

            ///////////////////////////////////////////////////

            lblBeneficiario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "kardc_beneficiario", "")});

            lblObservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "kardc_Observaciones", "")});

            ///////////////////////////////////////////////////

            decimal ingreso = 0;
            decimal salida = 0;

            lista.ForEach(x =>
            {
                ingreso += x.dblIngreso;
                salida += x.dblSalida;
            });




            lblTotalIngresos.Text = ingreso.ToString();

            lblTotalSalidas.Text = salida.ToString();

            this.ShowPreview();
        }
    }
}
