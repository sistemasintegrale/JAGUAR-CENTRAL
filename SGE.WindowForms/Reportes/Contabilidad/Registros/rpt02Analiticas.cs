using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Contabilidad.Registros
{
    public partial class rpt02Analiticas : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt02Analiticas()
        {
            InitializeComponent();
        }

        public void cargar(List<ETablaRegistro> lista)
        {
            lblEmpresa.Text = String.Format("{0} - AÑO {1}", Modules.Valores.strNombreEmpresa, Parametros.intEjercicio);
            lblModulo.Text = "CONTABILIDAD";
            lblTitulo1.Text = "LISTADO DE ANALÍTICAS";
            lblTitulo2.Text = "";
            this.DataSource = lista;
            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "tarec_icorrelativo_registro", "{0:00}")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "tarec_vdescripcion", "")});
            lblSituacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strEstado", "")});
            this.ShowPreview();
        }

    }
}
