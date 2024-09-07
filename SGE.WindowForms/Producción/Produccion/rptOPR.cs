using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Producción.Produccion
{
    public partial class rptOPR : XtraReport
    {
        public rptOPR()
        {
            InitializeComponent();
        }
        public void Cargar(List<ORPRReporte> mlisdet)
        {
            this.DataSource = mlisdet;

            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("orprc_icod_orden_produccion") });
            lblNumOPR.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_icod_orden_produccion", "") });
            lblProceso.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_vitem_pedido", "") });
            lblFT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_vficha_tecnica", "") });
            lblResponsable.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_vresponsable", "") });
            lblModelo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "DesModelo", "") });
            lblMarca.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "DesMarca", "") });
            lblTipo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "strtipo", "") });
            lblColor.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "strcolor", "") });
            lblSerie.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "resec_vdescripcion", "") });
            lblLinea.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_vlinea", "") });
            lblTotal.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_ntotal", "") });
            lblDocenas.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_ndocenas", "") });
            lblTalla1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_talla1", "") });
            lblTalla2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_talla2", "") });
            lblTalla3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_talla3", "") });
            lblTalla4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_talla4", "") });
            lblTalla5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_talla5", "") });
            lblTalla6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_talla6", "") });
            lblTalla7.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_talla7", "") });
            lblCantidad1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_cant_talla1", "") });
            lblCantidad2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_cant_talla2", "") });
            lblCantidad3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_cant_talla3", "") });
            lblCantidad4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_cant_talla4", "") });
            lblCantidad5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_cant_talla5", "") });
            lblCantidad6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_cant_talla6", "") });
            lblCantidad7.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprc_cant_talla7", "") });
            lblProceso.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "strproceso", "") });
            lblMaterial.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "strProducto", "") });
            lblCantidad.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orprd_ncantidad", "") });
            lblUm.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "strmedida", "") });

        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
