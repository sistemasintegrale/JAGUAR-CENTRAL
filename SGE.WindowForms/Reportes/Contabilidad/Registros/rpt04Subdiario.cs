using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Contabilidad.Registros
{
    public partial class rpt04Subdiario : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt04Subdiario()
        {
            InitializeComponent();
        }

        public void cargar(List<ESubDiario> lista)
        {
            lblEmpresa.Text = String.Format("{0} - AÑO {1}", Modules.Valores.strNombreEmpresa, Parametros.intEjercicio);
            lblModulo.Text = "CONTABILIDAD";
            lblTitulo1.Text = "LISTADO DE SUBDIARIOS";
            lblTitulo2.Text = "";
            this.DataSource = lista;
            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "subdi_icod_subdiario", "{0:00}")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "subdi_vdescripcion", "")});
            lblSituacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strEstado", "")});
            this.ShowPreview();
        }

    }
}
