using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Listados
{
    public partial class frmListrStockPorAlmacen : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListrStockPorAlmacen));
        List<EKardex> lstKardex = new List<EKardex>();
        public DateTime f1, f2;
        public int? intProducto = null;
        public frmListrStockPorAlmacen()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstKardex = new BAlmacen().listarKardexStockPorFechaAlmacenProducto(f1, f2, null, intProducto);
            grdKardex.DataSource = lstKardex;
        }

        private void verKardex()
        {
            try
            {
                EKardex Obe = (EKardex)viewKardex.GetRow(viewKardex.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmConsultaKardexPorFecAlmProd frm = new frmConsultaKardexPorFecAlmProd();
                frm.Text = String.Format("Kardex: {0} - {1}", Obe.strAlmacen, Obe.strProducto);
                frm.f1 = f1;
                frm.f2 = f2;
                frm.intAlmacen = Obe.almac_icod_almacen;
                frm.intProducto = Obe.prdc_icod_producto;
                frm.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void verKardexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verKardex();
        }

        private void btnKardex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            verKardex();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}