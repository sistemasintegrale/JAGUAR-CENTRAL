using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Producción.Almacenes
{
    public partial class rptTransferencia : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTransferencia()
        {
            InitializeComponent();
        }
        public void cargar(EProdTransferencia cabecera, List<EProdTransferenciaDetalle> detalle)
        {

            xrlblEmpresa.Text = "EMPRESA: " + Modules.Valores.strNombreEmpresa;
            xrLabel1.Text = "CENTRAL";
            xrlblTitulo2.Text = "DE " + cabecera.almac_vdescripcion_salida + " A " + cabecera.almac_vdescripcion_ingreso;
            xrlblTitulo1.Text = "TRANSFERENCIA Nº " + string.Format("{0:00000}", cabecera.trfc_inum_transf);
            this.DataSource = detalle;


            //grupoAlmacen.GroupFields.AddRange(new GroupField[] { new GroupField("kardc_iidalmacen") });           


            lblObservaciones.Text = cabecera.trfc_vobservaciones;


            lblItem.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "trfcd_num_item", "")});
            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "trfcd_vcodigo_externo", "")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "pr_vdescripcion_producto", "")});
            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", detalle, "trfcd_ncantidad", "")});
            lblUM.Text = "PAR";
            this.ShowPreview();

        }
    }
}
