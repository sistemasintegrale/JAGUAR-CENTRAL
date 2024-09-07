using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Tesorería.Caja
{
    public partial class rpt02CajaChica : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt02CajaChica()
        {
            InitializeComponent();
        }
        public void carga(List<ECajaChica> Lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "TESORERIA";
            lblTitulo1.Text = "RELACIÓN DE CAJAS CHICAS";
            lblTitulo2.Text = "";
            this.DataSource = Lista;

            lblNumero.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vnro_caja_liquida", "{0:00}")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vdescrip_caja_liquida", "")});
            lblMon.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Moneda", "")});
            lblCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vnumero_cuenta_contable", "")});
            lblAnalisis.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "analisis", "")});
            lblRepresentante.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vnom_responsable", "")});
            this.ShowPreview();
        }
    }
}
