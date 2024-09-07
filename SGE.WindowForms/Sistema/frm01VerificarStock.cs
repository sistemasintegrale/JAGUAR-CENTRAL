using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Sistema
{
    public partial class frm01VerificarStock : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm01VerificarStock));
        List<EKardex> lstKardex = new List<EKardex>();
        DateTime f1, f2;
        public frm01VerificarStock()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            setFechas();
        }
        private void setFechas()
        {

        }

        private void imprimir()
        {


        }
        private void buscar()
        {
            BaseEdit oBase = null;
            try
            {
                lstKardex = new BAlmacen().listarVerificarStock(Parametros.intEjercicio);
                grdKardex.DataSource = lstKardex;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }
        private void listarAlmacen()
        {

        }
        private void listarProducto()
        {

        }

        private void verKardex()
        {
            try
            {
                EKardex Obe = (EKardex)viewKardex.GetRow(viewKardex.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmConsultaKardexVerificarStock frm = new frmConsultaKardexVerificarStock();
                frm.Text = String.Format("Kardex: {0} - {1}", Obe.strAlmacen, Obe.strProducto);
                frm.intAlmacen = Obe.almac_icod_almacen;
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
            verKardex();
        }

        private void btnKardex_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            verKardex();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewKardex.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewKardex.ClearColumnsFilter();
        }

        private void filtrar()
        {
            grdKardex.DataSource = lstKardex.Where(x => x.strCodProducto.Contains(txtCodigo.Text.ToUpper()) &&
                x.strProducto.Contains(txtDescripcion.Text.ToUpper())).ToList();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtDescripcion_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void viewKardex_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string Stock = View.GetRowCellDisplayText(e.RowHandle, View.Columns["dblSaldo"]);
                string StockReal = View.GetRowCellDisplayText(e.RowHandle, View.Columns["dblSaldoReal"]);
                decimal Stock2 = Convert.ToDecimal(Stock);
                decimal StockReal2 = Convert.ToDecimal(StockReal);
                if (Stock2 != StockReal2)
                {
                    e.Appearance.BackColor = Color.LightSalmon;

                }
            }
        }

        private void actualizarStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EKardex> lstKardexVerificar = new List<EKardex>();

            lstKardexVerificar = new BAlmacen().listarVerificarStock(Parametros.intEjercicio).Where(x => x.dblSaldo != x.dblSaldoReal).ToList();
            //lstKardexVerificar = lstKardex.Where(x => x.dblSaldo != x.dblSaldoReal).ToList();
            lstKardexVerificar.ForEach(x =>
            {

                #region Actualización de Stock
                EStock stck = new EStock();
                stck.stocc_ianio = Parametros.intEjercicio;
                stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                stck.stocc_stock_producto = Convert.ToInt32(x.dblSaldo);
                stck.intTipoMovimiento = 1;
                new BAlmacen().ActualizarStockReal(stck);
                #endregion


            });

        }




    }
}