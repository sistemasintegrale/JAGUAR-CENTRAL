using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;


namespace SGE.WindowForms.Compras.ComprasMercaderia
{
    public partial class rptRecepcionComprasMercaderia : DevExpress.XtraReports.UI.XtraReport
    {
        public rptRecepcionComprasMercaderia()
        {
            InitializeComponent();
        }
        public void cargar(ERecepcionComprasMercaderia Obe, List<ERecepcionComprasMercaderiaDetalle> lstDetalle)
        {
            /*------------------------------------------------------*/

            lblNombreEmisor.Text = Valores.strNombreEmpresa;
            lblRuEmisor.Text = Valores.strRUC;
            lblModulo.Text = "COMPRAS";
            lblNroDocumento.Text = Obe.rcmc_vnumero_rcm;

            /*------------------------------------------------------*/

            lblProveedor.Text = Obe.NomProveedor.ToUpper();
            lblFecha.Text = Obe.rcmc_sfecha_rcm.ToString();
            lblFechaInic.Text = Obe.rcmc_sfecha_ingreso.ToString();
            lblAlmacen.Text = Obe.DesAlmacen;
            lblOC.Text = Obe.NumOC;
            lblCantidadTotal.Text = Obe.rcmc_ncantidad.ToString();

            /*------------------------------------------------------*/

            this.DataSource = lstDetalle;

            lblItem.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "rcmd_iid_detalle", "{0:000}")});

            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "DesProducto", "")});

            lblSerie.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "rcmd_rango_talla", "")});

            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "rcmd_ncantidad", "{0:N2}")});

            this.ShowPreview();

        }
    }
}
