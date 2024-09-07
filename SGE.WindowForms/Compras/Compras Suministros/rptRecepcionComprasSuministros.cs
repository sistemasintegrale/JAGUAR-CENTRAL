using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;


namespace SGE.WindowForms.Compras.ComprasSuministros
{
    public partial class rptRecepcionComprasSuministros : DevExpress.XtraReports.UI.XtraReport
    {
        public rptRecepcionComprasSuministros()
        {
            InitializeComponent();
        }
        public void cargar(ERecepcionComprasSuministros Obe, List<ERecepcionComprasSuministrosDetalle> lstDetalle)
        {
            /*------------------------------------------------------*/

            lblNombreEmisor.Text = Valores.strNombreEmpresa;
            lblRuEmisor.Text = Valores.strRUC;
            lblModulo.Text = "COMPRAS";
            lblNroDocumento.Text = Obe.rcsc_vnumero_rcs.Substring(0, 4) + " -" + Obe.rcsc_vnumero_rcs.Substring(4, Obe.rcsc_vnumero_rcs.Length - 4);


            /*------------------------------------------------------*/

            lblProveedor.Text = Obe.NomProveedor.ToUpper();
            lblFecha.Text = Obe.rcsc_sfecha_rcs.ToString();
            lblFechaInic.Text = Obe.rcsc_sfecha_ingreso.ToString();
            lblAlmacen.Text = Obe.DesAlmacen;
            lblOC.Text = Obe.NumOC;
            lblCantidadTotal.Text = Obe.rcsc_ncantidad.ToString();

            /*------------------------------------------------------*/

            this.DataSource = lstDetalle;

            lblItem.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "rcsd_iid_detalle", "{0:000}")});

            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "DesProducto", "")});

            lblUMedida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "Unidad", "")});

            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "rcsd_ncantidad", "{0:N2}")});

            this.ShowPreview();

        }
    }
}
