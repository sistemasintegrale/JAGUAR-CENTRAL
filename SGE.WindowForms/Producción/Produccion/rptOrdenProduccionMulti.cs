using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Entity;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SGE.WindowForms.Producción.Produccion
{
    public partial class rptOrdenProduccionMulti : XtraReport
    {
        private List<EAreaProduccion> lstTabla = new List<EAreaProduccion>();
        public rptOrdenProduccionMulti()
        {
            InitializeComponent();
        }
        public void Cargar(EOrdenProduccion Obe, List<EOrdenProduccionDet> mlisdet)
        {
            lstTabla = new BCentral().listarAreaProduccion().Where(x => x.arprc_iid_codigo != 11).ToList();
            lblNumOPR.Text = Obe.orprc_icod_orden_produccion;
            lblPedido.Text = "P" + "-" + Obe.orprc_vpedido + "." + Obe.orprc_vitem_pedido;
            lblFT.Text = "FT" + "-" + Obe.orprc_vficha_tecnica;
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



            this.DataSource = mlisdet;

            lblProceso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strproceso", "")});


            lblMaterial.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "orprd_vmaterial", "")});


            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "orprd_ncantidad", "")});

            lblUm.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strmedida", "")});


            lblDestino.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "orprd_vubicacion", "")});
        }

        private void lblProceso_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel procesoLabel = (XRLabel)sender;
            string procesoValue = procesoLabel.Text;
            var currentArea = lstTabla.Where(x => x.arprc_vdescripcion == procesoValue).FirstOrDefault();

            // Ahora, puedes cambiar el color de todas las etiquetas del detalle
            foreach (XRControl control in Detail.Controls)
            {
                if (control is XRLabel)
                {
                    XRLabel detalleLabel = (XRLabel)control;

                    detalleLabel.BackColor = Color.FromArgb(currentArea.arprc_icolor ?? 0); // Puedes establecer el color que desees
                }
            }
        }
    }
}
