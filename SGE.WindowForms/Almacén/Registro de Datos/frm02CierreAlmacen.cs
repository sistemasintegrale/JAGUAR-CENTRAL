using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Reportes.Almacen.Consultas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class frm02CierreAlmacen : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm02CierreAlmacen));
        List<EKardex> lstKardex = new List<EKardex>();

        public frm02CierreAlmacen()
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
            lstKardex = new BAlmacen().listarStockConsolidadoxAlmacen(Parametros.intEjercicio);
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
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_icod_correlativo"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_ianio"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.DateTime"); column.ColumnName = "kardc_fecha_movimiento"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "almac_icod_almacen"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "prdc_icod_producto"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Decimal"); column.ColumnName = "kardc_icantidad_prod"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "tdocc_icod_tipo_doc"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.String"); column.ColumnName = "kardc_numero_doc"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_tipo_movimiento"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_iid_motivo"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.String"); column.ColumnName = "kardc_beneficiario"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.String"); column.ColumnName = "kardc_observaciones"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_usuario_crea"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.DateTime"); column.ColumnName = "kardc_fecha_crea"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.String"); column.ColumnName = "kardc_pc_crea"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Int32"); column.ColumnName = "kardc_usuario_modifica"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.DateTime"); column.ColumnName = "kardc_fecha_modifica"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = Type.GetType("System.String"); column.ColumnName = "kardc_pc_modifica"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Boolean"); column.ColumnName = "kardc_flag_estado"; dteComproCab.Columns.Add(column);
                column = new DataColumn(); column.DataType = System.Type.GetType("System.Boolean"); column.ColumnName = "kardc_flag_pase"; dteComproCab.Columns.Add(column);

                int correlatico = new BAlmacen().Obtener_Kardex_Max_Correlativo();
                correlatico++;
                foreach (var obj in lstKardex)
                {
                    row = dteComproCab.NewRow();
                    row["kardc_icod_correlativo"] = correlatico;
                    row["kardc_ianio"] = Parametros.intEjercicio + 1;
                    row["kardc_fecha_movimiento"] = Convert.ToDateTime("01/01/" + (Parametros.intEjercicio + 1).ToString());
                    row["almac_icod_almacen"] = obj.almac_icod_almacen;
                    row["prdc_icod_producto"] = obj.prdc_icod_producto;
                    row["kardc_icantidad_prod"] = (obj.dblSaldo < 0) ? obj.dblSaldo * -1 : obj.dblSaldo;
                    row["tdocc_icod_tipo_doc"] = Parametros.intTipoDocAperturaKardex;
                    row["kardc_numero_doc"] = "000001";
                    row["kardc_tipo_movimiento"] = (obj.kardc_icantidad_prod < 0) ? Parametros.intKardexOut : Parametros.intKardexIn;
                    row["kardc_iid_motivo"] = 100;//INGRESO A ALMACEN POR SALDO INICIAL
                    row["kardc_beneficiario"] = "SALDO INICIAL";
                    row["kardc_observaciones"] = String.Format("PASE POR CIERRE ANUAL DEL EJERCICIO {0}", Parametros.intEjercicio);
                    row["kardc_usuario_crea"] = DBNull.Value;
                    row["kardc_fecha_crea"] = DBNull.Value;
                    row["kardc_pc_crea"] = DBNull.Value;
                    row["kardc_usuario_modifica"] = DBNull.Value;
                    row["kardc_fecha_modifica"] = DBNull.Value;
                    row["kardc_pc_modifica"] = DBNull.Value;
                    row["kardc_flag_estado"] = true;
                    row["kardc_flag_pase"] = true;
                    dteComproCab.Rows.Add(row);
                    i++;
                    correlatico = correlatico + 1;
                }
                #endregion

                new BAlmacen().cierreAnualAlmacenes(dteComproCab);
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