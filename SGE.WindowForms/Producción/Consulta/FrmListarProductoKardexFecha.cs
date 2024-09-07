using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Consulta
{
    public partial class FrmListarProductoKardexFecha : DevExpress.XtraEditors.XtraForm
    {
        string decrip_producto;
        List<EProdKardex> mlistKar = new List<EProdKardex>();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListarProductoKardexFecha));
        DateTime f1, f2;
        public FrmListarProductoKardexFecha()
        {
            InitializeComponent();
        }

        private void btncodigoProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                using (FrmListarProducto Producto = new FrmListarProducto())
                {
                    if (Producto.ShowDialog() == DialogResult.OK)
                    {
                        btncodigoProducto.Tag = Producto._Be.pr_icod_producto;
                        btncodigoProducto.Text = Producto._Be.pr_vdescripcion_producto;
                        decrip_producto = Producto._Be.pr_vdescripcion_producto;
                    }
                }
            }
        }

        private void FrmListarProductoKardexFecha_Load(object sender, EventArgs e)
        {

            BSControls.LoaderLook(LkpAlmacen, new BCentral().ListarProdAlmacen().Where(ob => ob.puvec_icod_punto_venta == Convert.ToInt32(2)).ToList(), "descripcion", "id_almacen", true);

            dtpFechaIni.EditValue = DateTime.Now;
            dtpFechaFin.EditValue = DateTime.Now.AddDays(+1);


        }
        private void Buscar()
        {
            f1 = (DateTime)dtpFechaIni.EditValue;
            f2 = (DateTime)dtpFechaFin.EditValue;

            EProdStockProducto obj = new EProdStockProducto
            {
                fecha = Convert.ToDateTime(dtpFechaIni.EditValue),
                fechafin = Convert.ToDateTime(dtpFechaFin.EditValue),
                stocc_iid_almacen = Convert.ToInt32(LkpAlmacen.EditValue),
                stocc_iid_producto = Convert.ToInt32(btncodigoProducto.Tag)
            };
            mlistKar = new BCentral().ListarProdKardexProductoFechaAlmacen(obj);
            dgrStockAlmacen.DataSource = mlistKar;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //BaseEdit oBase = null;
            //Boolean Flag = true;
            //try
            //{
            //     if (btncodigoProducto.Text == "")
            //     {
            //        oBase = btncodigoProducto;
            //        throw new ArgumentException("Seleccionar Producto");
            //     }
            //     Buscar();
            //}catch(Exception ex)
            //{
            //      if(oBase!=null)
            //    {
            //        oBase.Focus();
            //        oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
            //        oBase.ErrorText = ex.Message;
            //        oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            //        XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        Flag = false;
            //    }
            //}


            BaseEdit oBase = null;
            try
            {
                f1 = (DateTime)dtpFechaIni.EditValue;
                f2 = (DateTime)dtpFechaFin.EditValue;
                if (f1.Year != Parametros.intEjercicio)
                {
                    oBase = dtpFechaIni;
                    throw new ArgumentException("La fecha no esta dentro el año de ejercicio");
                }

                if (f2.Year != Parametros.intEjercicio)
                {
                    oBase = dtpFechaFin;
                    throw new ArgumentException("La fecha no esta dentro el año de ejercicio");
                }

                if (Convert.ToDateTime(f2.ToShortDateString()) < Convert.ToDateTime(f1.ToShortDateString()))
                {
                    oBase = dtpFechaFin;
                    throw new ArgumentException("La fecha inicial no puede ser mayor que fecha final");
                }

                if (Convert.ToInt32(LkpAlmacen.EditValue) == 0)
                {
                    oBase = LkpAlmacen;
                    throw new ArgumentException("Seleccione el almacén y producto a consultar");
                }

                if (Convert.ToInt32(btncodigoProducto.Tag) == 0)
                {
                    oBase = btncodigoProducto;
                    throw new ArgumentException("Seleccione el producto a consultar");
                }

                //mlistKar = new BAlmacen().listarKardexPorFechaAlmacenProducto(f1, f2, Convert.ToInt32(LkpAlmacen.Tag), Convert.ToInt32(btncodigoProducto.Tag));
                //grdKardex.DataSource = lstKardex;

                EProdStockProducto obj = new EProdStockProducto
                {
                    fecha = Convert.ToDateTime(dtpFechaIni.EditValue),
                    fechafin = Convert.ToDateTime(dtpFechaFin.EditValue),
                    stocc_iid_almacen = Convert.ToInt32(LkpAlmacen.EditValue),
                    stocc_iid_producto = Convert.ToInt32(btncodigoProducto.Tag)
                };
                mlistKar = new BCentral().ListarProdKardexProductoFechaAlmacen(obj);
                dgrStockAlmacen.DataSource = mlistKar;


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }









        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlistKar.Count > 0)
            {
                //rptKardex rptKar = new rptKardex();
                //rptKar.cargar(decrip_producto, dtpFechaIni.Text, dtpFechaIni.Text, mlistKar);

                rptKardex rpt = new rptKardex();
                rpt.cargar(String.Format("KARDEX DEL PRODUCTO: {0}", btncodigoProducto.Text.ToUpper()), String.Format("DEL {0} AL {1}", f1.ToShortDateString(), f2.ToShortDateString()), mlistKar);

            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e) => Services.ExportarExcel(dgrStockAlmacen);

        private void actualizarAnioStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EProdKardex> lstKardex = new List<EProdKardex>();
            lstKardex = new BCentral().listarkardex(Parametros.intEjercicio);
            int count = 0;
            lstKardex.ForEach(x =>
            {
                count++;
                EProdStockProducto obe = new EProdStockProducto();

                obe.stocc_ianio = x.kardc_ianio;
                obe.stocc_iid_almacen = x.iid_almacen;
                obe.stocc_iid_producto = x.iid_producto;
                obe.stocc_nstock_prod = x.Cantidad;
                obe.intTipoMovimiento = x.movimiento;
                new BCentral().actualizarStockAnio(obe);

            });

        }
    }
}