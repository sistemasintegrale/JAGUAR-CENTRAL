using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Reportes.Almacen.Consultas;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Almacén.Consultas
{
    public partial class frm07MovimientosProd : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm07MovimientosProd));
        List<EKardex> lstKardex = new List<EKardex>();
        DateTime f1, f2;
        public frm07MovimientosProd()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            setFechas();
        }
        private void setFechas()
        {
            if (Parametros.intEjercicio == DateTime.Now.Year)
            {
                dteFechaDesde.EditValue = Convert.ToDateTime("01/01/" + Parametros.intEjercicio.ToString());
                dteFechaHasta.EditValue = DateTime.Now;
            }
            else
            {
                dteFechaDesde.EditValue = Convert.ToDateTime("01/01/" + Parametros.intEjercicio.ToString());
                dteFechaHasta.EditValue = Convert.ToDateTime("31/12/" + Parametros.intEjercicio.ToString());
            }
        }

        private void imprimir()
        {
            if (lstKardex.Count > 0)
            {
                rptMovimientosProductos rpt = new rptMovimientosProductos();
                rpt.cargar("MOVIMIENTOS DE SUMINISTROS", String.Format("DEL {0} AL {1}", f1.ToShortDateString(), f2.ToShortDateString()), lstKardex);
            }
        }
        private void cargar()
        {
            BaseEdit oBase = null;
            try
            {
                f1 = (DateTime)dteFechaDesde.EditValue;
                f2 = (DateTime)dteFechaHasta.EditValue;
                if (f1.Year != Parametros.intEjercicio)
                {
                    oBase = dteFechaDesde;
                    throw new ArgumentException("La fecha no esta dentro el año de ejercicio");
                }

                if (f2.Year != Parametros.intEjercicio)
                {
                    oBase = dteFechaHasta;
                    throw new ArgumentException("La fecha no esta dentro el año de ejercicio");
                }

                if (Convert.ToDateTime(f2.ToShortDateString()) < Convert.ToDateTime(f1.ToShortDateString()))
                {
                    oBase = dteFechaHasta;
                    throw new ArgumentException("La fecha inicial no puede ser mayor que fecha final");
                }

                //if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                //{
                //    oBase = bteAlmacen;
                //    throw new ArgumentException("Seleccione el almacén y producto a consultar");
                //}

                //if (Convert.ToInt32(bteProducto.Tag) == 0)
                //{
                //    oBase = bteProducto;
                //    throw new ArgumentException("Seleccione el producto a consultar");
                //}

                lstKardex = new BAlmacen().listarKardexPorFechaAlmacenProducto(f1, f2, null, null);
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
            cargar();
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
                //if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                //    throw new ArgumentException("Seleccione el almacén a consultar");

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

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewKardex.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewKardex.ClearColumnsFilter();
        }
    }
}