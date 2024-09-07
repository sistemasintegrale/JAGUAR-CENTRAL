using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Reportes.Almacen.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Almacén.Consultas
{
    public partial class frm02StockConsolidado : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm02StockConsolidado));
        List<EKardex> lstKardex = new List<EKardex>();

        public frm02StockConsolidado()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }


        private void imprimir()
        {
            if (lstKardex.Count > 0)
            {
                rptStockConsolidado rpt = new rptStockConsolidado();
                rpt.cargar("STOCK CONSOLIDADO", "", lstKardex);
            }
        }
        private void cargar()
        {
            lstKardex = new BAlmacen().listarStockConsolidado(Parametros.intEjercicio);
            grdKardex.DataSource = lstKardex;
        }


        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void verAlmacen()
        {
            try
            {
                EKardex Obe = (EKardex)viewKardex.GetRow(viewKardex.FocusedRowHandle);
                if (Obe == null)
                    return;

                frmListrStockPorAlmacen frm = new frmListrStockPorAlmacen();
                frm.f1 = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1);
                frm.f2 = DateTime.Now;
                frm.intProducto = Obe.prdc_icod_producto;
                frm.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void verKardexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verAlmacen();
        }

        private void btnKardex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            verAlmacen();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewKardex.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewKardex.ClearColumnsFilter();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtDescripcion_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdKardex.DataSource = lstKardex.Where(x => x.strCodProducto.Contains(txtCodigo.Text.ToUpper()) &&
                x.strProducto.Contains(txtDescripcion.Text.ToUpper())).ToList();
        }
    }
}