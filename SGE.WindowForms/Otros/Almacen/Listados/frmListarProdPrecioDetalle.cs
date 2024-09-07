using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Reportes.Almacen.Consultas;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Almacen.Listados
{
    public partial class frmListarProdPrecioDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListrStockPorAlmacen));
        List<EDocCompraDet> lstProdPrecioDet = new List<EDocCompraDet>();
        public string DesProducto;

        public int intProducto = 0;

        public frmListarProdPrecioDetalle()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstProdPrecioDet = new BAlmacen().listarProductoPreciosDetalle(intProducto, Parametros.intEjercicio);

            grdCompras.DataSource = lstProdPrecioDet;
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rptPreciosDetalle rpt = new rptPreciosDetalle();
            rpt.cargar("LISTA COMPRA DE PRODUCTOS CON PRECIO DE COMPRA", DesProducto, lstProdPrecioDet);

        }
    }
}