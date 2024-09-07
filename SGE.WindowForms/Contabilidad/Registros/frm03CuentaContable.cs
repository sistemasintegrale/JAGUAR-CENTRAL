using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Reportes.Contabilidad.Registros;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Registros
{
    public partial class frm03CuentaContable : DevExpress.XtraEditors.XtraForm
    {
        private List<ECuentaContable> lstCuentasContables = new List<ECuentaContable>();
        private List<EParametroContable> lstParametroContable = new List<EParametroContable>();
        private List<ECuentaContable> lstCuentasContablesTXT = new List<ECuentaContable>();
        public frm03CuentaContable()
        {
            InitializeComponent();
        }
        private void FrmCuentaContable_Load(object sender, EventArgs e)
        {
            cargar();
            loadMask();
            var lstMeses = new BGeneral().listarTablaRegistro(4);
            BSControls.LoaderLook(lkpMes, lstMeses.Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
            lkpMes.EditValue = DateTime.Now.Month;
        }

        private void cargar()
        {
            lstCuentasContables = new BContabilidad().listarCuentaContable();
            grdCuentaContable.DataSource = lstCuentasContables;
            /*-----------------------------------------------------------------------------------------------*/
            lstParametroContable = new BContabilidad().listarParametroContable();
            /*TXT*/
            lstCuentasContablesTXT = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
        }
        void reload(long intIcod)
        {
            cargar();
            long index = lstCuentasContables.FindIndex(x => x.ctacc_icod_cuenta_contable == intIcod);
            viewCuentaContable.FocusedRowHandle = Convert.ToInt32(index);
            viewCuentaContable.Focus();
        }

        private void loadMask()
        {
            lstParametroContable = new BContabilidad().listarParametroContable();
            if (lstParametroContable.Count > 0)
            {
                lstParametroContable.ForEach(obe =>
                {
                    this.txtCodigo.Properties.Mask.EditMask = obe.parac_vmascara;
                    this.txtCodigo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                    this.txtCodigo.Properties.Mask.ShowPlaceHolders = false;
                });
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }
        private void nuevo()
        {
            frmManteCuentaContable frm = new frmManteCuentaContable();
            frm.lstCuentasContables = lstCuentasContables;
            frm.lstParametroContable = lstParametroContable;
            frm.MiEvento += new frmManteCuentaContable.DelegadoMensaje(reload);
            frm.Show();
            frm.SetInsert();
        }

        private void modificar()
        {
            ECuentaContable Obe = (ECuentaContable)viewCuentaContable.GetRow(viewCuentaContable.FocusedRowHandle);
            if (Obe != null)
            {
                frmManteCuentaContable frm = new frmManteCuentaContable();
                frm.MiEvento += new frmManteCuentaContable.DelegadoMensaje(reload);
                frm.lstParametroContable = lstParametroContable;
                frm.lstCuentasContables = lstCuentasContables;
                frm.Obe = Obe;
                frm.Show();
                frm.SetModify();
                frm.setValues();
            }
        }
        private void eliminar()
        {
            try
            {
                ECuentaContable Obe = (ECuentaContable)viewCuentaContable.GetRow(viewCuentaContable.FocusedRowHandle);
                if (Obe != null)
                {
                    int index = viewCuentaContable.FocusedRowHandle;
                    new BContabilidad().eliminarCuentaContable(Obe);
                    cargar();
                    if (lstCuentasContables.Count >= index + 1)
                        viewCuentaContable.FocusedRowHandle = index;
                    else
                        viewCuentaContable.FocusedRowHandle = index - 1;

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
            //if (lstCuentasContables.Count > 0)
            //{
            //    using (frmRangoCuentas frm = new frmRangoCuentas())
            //    {
            //        frm.mlista = lstCuentasContables;
            //        if (frm.ShowDialog() == DialogResult.OK)
            //            cargar();
            //    }                
            //}
            List<ECuentaContable> aux = new List<ECuentaContable>();

            aux = new BContabilidad().listarCuentaContable();
            aux.ForEach(x =>
            {

                if (x.ctacc_nivel_cuenta == 2)
                {
                    x.ctacc_numero_cuenta_contable = "   " + x.ctacc_numero_cuenta_contable;
                    x.ctacc_nombre_descripcion = "   " + x.ctacc_nombre_descripcion;
                }
                else if (x.ctacc_nivel_cuenta == 3)
                {
                    x.ctacc_numero_cuenta_contable = "      " + x.ctacc_numero_cuenta_contable;
                    x.ctacc_nombre_descripcion = "   " + x.ctacc_nombre_descripcion;
                }
                else if (x.ctacc_nivel_cuenta == 4)
                {
                    x.ctacc_numero_cuenta_contable = "        " + x.ctacc_numero_cuenta_contable;
                    x.ctacc_nombre_descripcion = "   " + x.ctacc_nombre_descripcion;
                }
                else if (x.ctacc_nivel_cuenta == 5)
                {
                    x.ctacc_numero_cuenta_contable = "          " + x.ctacc_numero_cuenta_contable;
                    x.ctacc_nombre_descripcion = "   " + x.ctacc_nombre_descripcion;
                }
            });

            rpt03CuentaContableTodo reporte = new rpt03CuentaContableTodo();
            reporte.cargar(aux);

            this.DialogResult = DialogResult.OK;
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            ECuentaContable Obe = (ECuentaContable)viewCuentaContable.GetRow(viewCuentaContable.FocusedRowHandle);
            if (Obe == null)
                return;

            frmManteCuentaContable frm = new frmManteCuentaContable();
            frm.lstParametroContable = lstParametroContable;
            frm.lstCuentasContables = lstCuentasContables;
            frm.Obe = Obe;
            frm.Show();
            frm.setValues();
            frm.SetCancel();
        }

        private void buscarCriterio()
        {
            grdCuentaContable.DataSource = lstCuentasContables.Where(obj =>
                                                   obj.ctacc_nombre_descripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.ctacc_numero_cuenta_contable.Replace(".", "").ToUpper().StartsWith(txtCodigo.Text.Replace(".", "").ToUpper())).ToList();

        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }


        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cargar();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void exportarTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string ConSinInfo, Mes;
                if (lstCuentasContablesTXT.Count > 0)
                {
                    ConSinInfo = "1";
                }
                else
                {
                    ConSinInfo = "2";
                }
                if (lkpMes.EditValue.ToString().Trim().Length == 1)
                {
                    Mes = "0" + lkpMes.EditValue.ToString();
                }
                else
                {
                    Mes = lkpMes.EditValue.ToString();
                }
                string Nombre = "LE" + Valores.strRUC + Parametros.intEjercicio.ToString() + "01" + "00" + "050300" + "00" + "1" + ConSinInfo + "1" + "1" + ".txt";
                sfdTXT.FileName = Nombre;
                if (sfdTXT.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = sfdTXT.FileName;
                    if (!fileName.Contains(".txt"))
                    {
                        ExportarATXT(fileName + ".txt");
                        System.Diagnostics.Process.Start(fileName + ".txt");
                    }
                    else
                    {
                        ExportarATXT(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                }
                sfdTXT.FileName = string.Empty;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportarATXT(String ruta)
        {
            StreamWriter sw = new StreamWriter(ruta);
            string error = string.Empty;
            int cont = 0;
            string columna = string.Empty;
            try
            {
                int totFilas = lstCuentasContablesTXT.Count;
                foreach (ECuentaContable item in lstCuentasContablesTXT)
                {
                    cont++;
                    error = item.ctacc_icod_cuenta_contable + " " + item.ctacc_numero_cuenta_contable;
                    columna = "1";
                    sw.Write(Parametros.intEjercicio.ToString() + "01" + "01|"); // 1
                    columna = "2";
                    sw.Write(item.ctacc_icod_cuenta_contable + "|"); // 2
                    columna = "3";
                    sw.Write(item.ctacc_nombre_descripcion.TrimEnd(' ') + "|"); // 3
                    columna = "4";
                    sw.Write("01" + "|"); // 4
                    columna = "5";
                    sw.Write("" + "|"); // 5                   
                    columna = "6";
                    sw.Write("" + "|"); // 6
                    columna = "7";
                    sw.Write("" + "|"); // 7
                    columna = "8";
                    sw.Write("1" + "|"); // 8         
                    if (totFilas != cont)
                    {
                        sw.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message + "\nFila " + cont + "\nDocumento " + error + "\nColumna Nº " + columna);
            }
            finally
            {
                sw.Close();
            }
        }

        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ECuentaContable Obe = (ECuentaContable)viewCuentaContable.GetRow(viewCuentaContable.FocusedRowHandle);
            if (Obe != null)
            {
                try
                {

                    if (sdfxl.ShowDialog(this) == DialogResult.OK)
                    {
                        string fileName = sdfxl.FileName;
                        if (!fileName.Contains(".xlsx"))
                        {
                            grdCuentaContable.ExportToXlsx(fileName + ".xlsx");
                            System.Diagnostics.Process.Start(fileName + ".xlsx");
                        }
                        else
                        {
                            grdCuentaContable.ExportToXlsx(fileName);
                            System.Diagnostics.Process.Start(fileName);
                        }
                        grdCuentaContable.DataSource = null;
                        sdfxl.FileName = string.Empty;
                    }

                    else
                        throw new ArgumentException("No hay registros para exportar");
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}