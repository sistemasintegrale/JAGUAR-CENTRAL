using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Almacen.Registros
{
    public partial class rptFamilia : DevExpress.XtraReports.UI.XtraReport
    {
        public rptFamilia()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<EFamiliaCab> lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;
            this.DataSource = lista;

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "famic_vabreviatura", "{0:00}")});

            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "famic_vdescripcion", "")});


            this.ShowPreview();

        }
    }
}
