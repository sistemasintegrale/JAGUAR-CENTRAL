using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Reportes.Almacen.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Compras.Consultas_de_Compras
{
    public partial class frm09ProductosPrecios : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm09ProductosPrecios));
        List<EProducto> lstProductoPrecio = new List<EProducto>();

        public frm09ProductosPrecios()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }


        private void cargar()
        {
            lstProductoPrecio = new BAlmacen().listarProductoPrecios(Parametros.intEjercicio);
            grdPrecios.DataSource = lstProductoPrecio;
        }
        private void imprimir()
        {
            if (lstProductoPrecio.Count > 0)
            {
                rptPrecios rpt = new rptPrecios();
                rpt.cargar("LISTA DE PRODUCTOS CON PRECIO DE COMPRA", "", lstProductoPrecio);
            }
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }


        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void filtrar()
        {
            grdPrecios.DataSource = lstProductoPrecio.Where(x => x.prdc_vcode_producto.Contains(txtCodigo.Text.ToUpper()) &&
                x.prdc_vdescripcion_larga.Contains(txtDescripcion.Text.ToUpper())).ToList();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtDescripcion_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void cbActivarFiltro_CheckedChanged_1(object sender, EventArgs e)
        {
            viewPrecios.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewPrecios.ClearColumnsFilter();
        }

        private void verPreciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EProducto Obe = (EProducto)viewPrecios.GetRow(viewPrecios.FocusedRowHandle);
            if (Obe == null)
                return;
            frmListarProdPrecioDetalle frm = new frmListarProdPrecioDetalle();
            frm.Text = String.Format("COMPRAS DEL PRODUCTO: {0}", Obe.prdc_vdescripcion_larga);
            frm.intProducto = Obe.prdc_icod_producto;
            frm.DesProducto = Obe.prdc_vdescripcion_larga;
            frm.Show();
        }
    }
}