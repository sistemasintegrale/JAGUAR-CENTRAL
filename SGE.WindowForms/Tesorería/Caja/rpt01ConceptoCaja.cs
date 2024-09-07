using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Tesorería.Caja
{
    public partial class rpt01ConceptoCaja : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt01ConceptoCaja()
        {
            InitializeComponent();
        }
        public void carga(List<EConceptoMovCaja> Lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "TESORERIA";
            lblTitulo1.Text = "TIPOS DE MOVIMIENTOS";
            lblTitulo2.Text = "TESORERIA - CAJA";
            this.DataSource = Lista;


            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ccod_concep_mov", "")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vdescripcion", "")});
            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "doc_vdes", "")});
            lblCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cuenta_ambos", "")});

            this.ShowPreview();

        }
    }
}
