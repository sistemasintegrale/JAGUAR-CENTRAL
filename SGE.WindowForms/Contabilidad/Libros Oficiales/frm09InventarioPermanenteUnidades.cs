using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Libros_Oficiales
{
    public partial class frm09InventarioPermanenteUnidades : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Almacén.Consultas.frm05KardexPorFecAlmProd));
        List<EKardex> lstKardex = new List<EKardex>();
        List<EKardex> lstKardexVerificar = new List<EKardex>();
        List<EKardex> lstKardexActualizar = new List<EKardex>();
        DateTime f1, f2;
        public frm09InventarioPermanenteUnidades()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
            bteAlmacen.Text = "ALMACEN DE SUMINISTROS";
            bteAlmacen.Tag = 120;
            //setFechas();
        }
        private void setFechas()
        {
            if (Parametros.intEjercicio == DateTime.Now.Year)
            {
                dteFechaHasta.EditValue = DateTime.Now;
            }
            else
            {
                dteFechaHasta.EditValue = Convert.ToDateTime("31/12/" + Parametros.intEjercicio.ToString());
            }
        }

        private void imprimir()
        {
            rptListaInventarioPermanente rpt = new rptListaInventarioPermanente();
            rpt.cargar(String.Format("STOCK DEL ALMACÉN: {0}", bteAlmacen.Text.ToUpper()), String.Format("A LA FECHA: {0}", f2.ToShortDateString()), (List<EKardex>)viewKardex.DataSource);

        }
        private void buscar()
        {
            BaseEdit oBase = null;
            try
            {
                f1 = DateTime.MinValue.AddMonths(Convert.ToInt32(lkpMes.EditValue) - 1).AddYears(Parametros.intEjercicio - 1);

                f2 = f1.AddDays(-1).AddMonths(+1);




                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione el almacén a consultar");
                }

                lstKardex = new BAlmacen().listarInventarioPermanenteUnidades(f1, f2, Convert.ToInt32(bteAlmacen.Tag)/*,null*/);
                grdKardex.DataSource = lstKardex;
                #region Verificar Stock
                //lstKardexVerificar = new BAlmacen().listarVerificarStock(Parametros.intEjercicio);
                //lstKardexActualizar = new BAlmacen().listarVerificarStock(Parametros.intEjercicio).Where(x => x.dblSaldo != x.dblSaldoReal).ToList();
                //lstKardexActualizar.ForEach(x =>
                //{
                //    #region Actualización de Stock
                //    EStock stck = new EStock();
                //    stck.stocc_ianio = Parametros.intEjercicio;
                //    stck.almac_icod_almacen = Convert.ToInt32(x.almac_icod_almacen);
                //    stck.prdc_icod_producto = Convert.ToInt32(x.prdc_icod_producto);
                //    stck.stocc_stock_producto = Convert.ToInt32(x.dblSaldo);
                //    stck.intTipoMovimiento = 1;
                //    new BAlmacen().ActualizarStockReal(stck);
                //    #endregion
                //});
                #endregion
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
            try
            {
                frmListarAlmacen Almacen = new frmListarAlmacen();

                if (Almacen.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = Almacen._Be.almac_icod_almacen;
                    bteAlmacen.Text = Almacen._Be.almac_vdescripcion;
                    /*----------------------------------------------------*/
                    lstKardex.Clear();
                    viewKardex.RefreshData();
                    /*----------------------------------------------------*/
                    bteProducto.Text = "";
                    bteProducto.Tag = null;
                    /*----------------------------------------------------*/
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listarProducto()
        {
            try
            {
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                    throw new ArgumentException("Seleccione el almacén a consultar");

                frmListarProducto Producto = new frmListarProducto();
                if (Producto.ShowDialog() == DialogResult.OK)
                {
                    bteProducto.Tag = Producto._Be.prdc_icod_producto;
                    bteProducto.Text = Producto._Be.prdc_vdescripcion_larga;
                    lstKardex.Clear();
                    viewKardex.RefreshData();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                frm.Obe = Obe;
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

        private void imprimirListaConDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = new BAlmacen().listarKardexPorFechaAlmacenProducto(f1, f2, Convert.ToInt32(bteAlmacen.Tag), 0);
            rptListaInventarioPermanenteConDetalle rpt = new rptListaInventarioPermanenteConDetalle();
            rpt.cargar("", "", list, f1);

        }

        private void txtDescripcion_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }




    }
}