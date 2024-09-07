using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Almacen.Consultas
{
    public partial class rptNotaSalidaPorFechasDetalle : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNotaSalidaPorFechasDetalle()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<ENotaSalidaDetalle> lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;

            this.DataSource = lista;

            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("ningc_icod_nota_ingreso") });

            #region cabecera

            lblNroNota.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cabNroNota", "")});

            lblFechaNota.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cabFechaNota", "{0:dd/MM/yyyy}")});

            lblMotivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cabMotivo", "")});

            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cabDocumento", "")});

            lblReferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cabReferencia", "")});

            lblObservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cabObservacion", "")});
            #endregion
            #region detalle
            lblItem.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dnsalc_nro_item", "{0:000}")});

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strCodeProducto", "")});

            lblProducto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strProducto", "")});

            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dnsalc_cantidad", "")});

            lblUnidaMedida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strUnidadMedida", "")});
            #endregion
            this.ShowPreview();
        }
    }
}
