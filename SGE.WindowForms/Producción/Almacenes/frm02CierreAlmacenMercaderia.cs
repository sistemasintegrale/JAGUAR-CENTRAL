using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Almacenes
{
    public partial class frm02CierreAlmacenMercaderia : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm02CierreAlmacenMercaderia));
        List<EProdKardex> lstKardex = new List<EProdKardex>();

        public frm02CierreAlmacenMercaderia()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }


        private void imprimir()
        {
            //if (lstKardex.Count > 0)
            //{
            //    rptStockConsolidado rpt = new rptStockConsolidado();
            //    rpt.cargar("STOCK CONSOLIDADO", "", lstKardex);
            //}
        }
        private void cargar()
        {
            lstKardex = new BCentral().listarStockConsolidadoxAlmacen(Parametros.intEjercicio);
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

        private void cierreAnualDeSldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cierre();
        }

        private void cierre()
        {
            bool flag = true;
            try
            {
                //verificamos que este registrado el siguiente año de ejercicio
                var lstAnio = new BContabilidad().listarAnioEjercicio();
                if (lstAnio.Where(x => x.anioc_iid_anio_ejercicio == Parametros.intEjercicio + 1).ToList().Count == 0)
                    throw new ArgumentException(String.Format("Año de ejercicio {0}, no se encuentra registrado", Parametros.intEjercicio + 1));
                //
                if (XtraMessageBox.Show("Esta opción servirá únicamente para efectuar la inicialización de stock para el siguiente año \n\t\t\t\t\t\t\t\t\t¿Esta seguro que desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    flag = false;
                    return;
                }
                #region Datatable Comprobante Cabecera
                DataTable dteComproCab = new DataTable();
                DataColumn column;
                DataRow row;
                int i = 1;

                // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_iid_correlativo"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_ianio"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.DateTime"); column.ColumnName = "kardc_sfecha_movimiento"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_iid_almacen"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_iid_producto"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "puvec_icod_punto_venta"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Decimal"); column.ColumnName = "kardc_icantidad_prod"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.String"); column.ColumnName = "kardc_inumero_nota"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_iid_tipo_doc"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.String"); column.ColumnName = "kardc_inumero_doc"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.Int32"); column.ColumnName = "kardc_itipo_movimiento"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.Int32"); column.ColumnName = "kardc_iid_motivo"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.String"); column.ColumnName = "kardc_vbeneficiario"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.String"); column.ColumnName = "kardc_vobservaciones"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.Int32"); column.ColumnName = "kardc_iusuario_crea"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.DateTime"); column.ColumnName = "kardc_sfecha_crea"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Decimal"); column.ColumnName = "year_orden_despacho"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.Decimal"); column.ColumnName = "numero_orden_despacho"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.Decimal"); column.ColumnName = "monto_total_compra"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.Decimal"); column.ColumnName = "precio_costo_promedio"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.Decimal"); column.ColumnName = "monto_saldo_valorizado"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.Decimal"); column.ColumnName = "monto_unitario_compra"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.Decimal"); column.ColumnName = "year_presupuesto_importacion"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.Decimal"); column.ColumnName = "numero_presupuesto_importacion"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_iactivo"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_iid_item"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Boolean"); column.ColumnName = "kardc_flag_pase"; dteComproCab.Columns.Add(column);

                int correlatico = new BCentral().Obtener_Kardex_Max_Correlativo();
                correlatico++;
                foreach (var obj in lstKardex)
                {
                    row = dteComproCab.NewRow();
                    row["kardc_iid_correlativo"] = correlatico;
                    row["kardc_ianio"] = Parametros.intEjercicio + 1;
                    row["kardc_sfecha_movimiento"] = Convert.ToDateTime("01/01/" + (Parametros.intEjercicio + 1).ToString());
                    row["kardc_iid_almacen"] = obj.iid_almacen;
                    row["kardc_iid_producto"] = obj.iid_producto;
                    row["puvec_icod_punto_venta"] = 2;
                    row["kardc_icantidad_prod"] = (obj.cantidad_saldo_prod < 0) ? obj.cantidad_saldo_prod * -1 : obj.cantidad_saldo_prod;
                    row["kardc_inumero_nota"] = "";
                    row["kardc_iid_tipo_doc"] = Parametros.intTipoDocAperturaKardex;
                    row["kardc_inumero_doc"] = "000001";
                    row["kardc_itipo_movimiento"] = (obj.Cantidad < 0) ? Parametros.intKardexOut : Parametros.intKardexIn;
                    row["kardc_iid_motivo"] = 100;
                    row["kardc_vbeneficiario"] = "SALDO INICIAL";
                    row["kardc_vobservaciones"] = String.Format("PASE POR CIERRE ANUAL DEL EJERCICIO {0}", Parametros.intEjercicio);
                    row["kardc_iusuario_crea"] = DBNull.Value;
                    row["kardc_sfecha_crea"] = DBNull.Value;
                    row["year_orden_despacho"] = DBNull.Value;
                    row["numero_orden_despacho"] = DBNull.Value;
                    row["monto_total_compra"] = DBNull.Value;
                    row["precio_costo_promedio"] = DBNull.Value;
                    row["monto_saldo_valorizado"] = DBNull.Value;
                    row["monto_unitario_compra"] = DBNull.Value;
                    row["year_presupuesto_importacion"] = DBNull.Value;
                    row["numero_presupuesto_importacion"] = DBNull.Value;
                    row["kardc_iactivo"] = true;
                    row["kardc_iid_item"] = DBNull.Value;
                    row["kardc_flag_pase"] = true;
                    dteComproCab.Rows.Add(row);
                    i++;
                    correlatico = correlatico + 1;
                }
                #endregion

                new BCentral().cierreAnualAlmacenes(dteComproCab);
                //new BAlmacen().cierreAnualAlmacenes(Valores.intUsuario, WindowsIdentity.GetCurrent().Name);

            }
            catch (Exception ex)
            {
                flag = false;
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (flag)
                    XtraMessageBox.Show("El proceso de cierre ha sido ejecutado con éxito", "Información del Sistema");
            }
        }
    }
}