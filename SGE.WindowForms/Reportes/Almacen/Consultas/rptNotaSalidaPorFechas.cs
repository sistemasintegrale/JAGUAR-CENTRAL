using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Almacen.Consultas
{
    public partial class rptNotaSalidaPorFechas : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNotaSalidaPorFechas()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<ENotaSalida> lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;

            this.DataSource = lista;

            lblNroNota.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nsalc_numero_nota_salida", "")});

            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nsalc_fecha_nota_salida", "{0:dd/MM/yyyy}")});

            lblMotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strMotivo", "")});

            lblNroDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strTipoNroDoc", "")});

            lblReferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nsalc_referencia", "")});

            lblObservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "nsalc_observaciones", "")});

            this.ShowPreview();
        }
    }
}
