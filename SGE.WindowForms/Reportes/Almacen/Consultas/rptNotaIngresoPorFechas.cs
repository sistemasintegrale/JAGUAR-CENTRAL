using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Almacen.Consultas
{
    public partial class rptNotaIngresoPorFechas : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNotaIngresoPorFechas()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<ENotaIngreso> lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;

            this.DataSource = lista;

            lblNroNota.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_numero_nota_ingreso", "")});

            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_fecha_nota_ingreso", "{0:dd/MM/yyyy}")});

            lblMotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strMotivo", "")});

            lblNroDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strTipoNroDoc", "")});

            lblReferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_referencia", "")});

            lblObservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ningc_observaciones", "")});

            this.ShowPreview();
        }
    }
}
