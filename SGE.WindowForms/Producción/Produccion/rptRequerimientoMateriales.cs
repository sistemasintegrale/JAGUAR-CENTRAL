using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Ventas.Reporte
{
    public partial class rptRequerimientoMateriales : DevExpress.XtraReports.UI.XtraReport
    {
        public rptRequerimientoMateriales()
        {
            InitializeComponent();
        }
        public void cargar(EOrdenProduccion Obe, List<EOrdenProduccionDet> mlisdet)
        {
            /*-------------------------------------*/

            /*
             * Datos de cliente 
            */

            lblPedido.Text = "P" + "-" + Obe.orprc_vpedido + "." + Obe.orprc_vitem_pedido;
            lblFichaTecnica.Text = "FT" + "-" + Obe.orprc_vficha_tecnica;
            lblResponsable.Text = Obe.orprc_vresponsable;
            lblModelo.Text = Obe.DesModelo;
            lblMarca.Text = Obe.DesMarca;
            lblTipo.Text = Obe.strtipo;
            lblColor.Text = Obe.strcolor;
            lblSerie.Text = Obe.resec_vdescripcion.Substring((Obe.resec_vdescripcion.Length - 2), 2);
            lblLinea.Text = Obe.orprc_vlinea;
            lblTotal.Text = Obe.orprc_ntotal.ToString();
            lblDocenas.Text = Obe.orprc_ndocenas.ToString();
            lblTalla1.Text = Obe.orprc_talla1.ToString();
            lblTalla2.Text = Obe.orprc_talla2.ToString();
            lblTalla3.Text = Obe.orprc_talla3.ToString();
            lblTalla4.Text = Obe.orprc_talla4.ToString();
            lblTalla5.Text = Obe.orprc_talla5.ToString();
            lblTalla6.Text = Obe.orprc_talla6.ToString();
            lblTalla7.Text = Obe.orprc_talla7.ToString();
            lblCantidad1.Text = Obe.orprc_cant_talla1.ToString();
            lblCantidad2.Text = Obe.orprc_cant_talla2.ToString();
            lblCantidad3.Text = Obe.orprc_cant_talla3.ToString();
            lblCantidad4.Text = Obe.orprc_cant_talla4.ToString();
            lblCantidad5.Text = Obe.orprc_cant_talla5.ToString();
            lblCantidad6.Text = Obe.orprc_cant_talla6.ToString();
            lblCantidad7.Text = Obe.orprc_cant_talla7.ToString();
            /*
             * Datos de emisor
             */
            lblRuEmisor.Text = Valores.strRUC;
            lblNombreEmisor.Text = Valores.strNombreEmpresa;
            lblNroDocumento.Text = "OP" + "-" + Obe.orprc_icod_orden_produccion;


            this.DataSource = mlisdet;

            //mlisdet.ForEach(x =>
            //{
            //    if (x.orped_vruta == "" || x.orped_vruta == null)
            //    {

            //    }
            //    else
            //    {
            //        ptrimagen.Image = Image.FromFile(x.imagen);
            //    }
            //});


            //ptrimagen.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Image", mlisdet, "noped_vruta", "")});


            lblProceso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strproceso", "")});

            lbUbicacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "orprd_vubicacion", "")});

            lblMaterial.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strProducto", "")});

            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "orprd_ncantidad", "")});

            lblUMedida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strmedida", "")});

            this.ShowPreview();
        }


    }
}
