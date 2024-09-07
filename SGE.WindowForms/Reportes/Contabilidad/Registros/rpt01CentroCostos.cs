using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Contabilidad.Registros
{
    public partial class rpt01CentroCostos : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt01CentroCostos()
        {
            InitializeComponent();
        }

        public void cargar(List<ECentroCosto> lista)
        {
            lblEmpresa.Text = String.Format("{0} - AÑO {1}", Modules.Valores.strNombreEmpresa, Parametros.intEjercicio);
            lblModulo.Text = "CONTABILIDAD";
            lblTitulo1.Text = "LISTADO DE CENTROS DE COSTOS";
            lblTitulo2.Text = "";
            this.DataSource = lista;
            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cecoc_vcodigo_centro_costo", "")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cecoc_vdescripcion", "")});
            lblSituacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strEstado", "")});
            this.ShowPreview();
        }

    }
}
