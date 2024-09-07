using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Libros_Oficiales
{
    public partial class FrmConsultaLibroMayor : DevExpress.XtraEditors.XtraForm
    {
        int? ctaInicio, ctaFin, nullVal;
        int intMes = -1;
        string strMes, strCtaInicio, strCtaFin;
        List<EVoucherContableDet> lst01 = new List<EVoucherContableDet>();
        List<EVoucherContableDet> lst02 = new List<EVoucherContableDet>();
        List<EComprobante> listaTXT = new List<EComprobante>();
        public FrmConsultaLibroMayor()
        {
            InitializeComponent();
        }
        private void FrmRptMayorAuxiliarMensual_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMeses, new BGeneral().listarTablaRegistro(4).Where(ob => ob.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMeses.EditValue = DateTime.Now.Month;
        }
        private void bteCuentaI_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FrmListaCuentaContableC frmSubcuenta = new FrmListaCuentaContableC())
            {
                frmSubcuenta.tipocuenta = true;
                if (frmSubcuenta.ShowDialog() == DialogResult.OK)
                {
                    bteCuentaI.Text = frmSubcuenta._Be.ctacc_numero_cuenta_contable;
                    bteCuentaI.Tag = frmSubcuenta._Be.ctacc_icod_cuenta_contable;
                    txtCuentaI.Text = frmSubcuenta._Be.ctacc_nombre_descripcion;

                    bteCuentaF.Text = frmSubcuenta._Be.ctacc_numero_cuenta_contable;
                    bteCuentaF.Tag = frmSubcuenta._Be.ctacc_icod_cuenta_contable;
                    txtCuentaF.Text = frmSubcuenta._Be.ctacc_nombre_descripcion;
                }
            }

        }
        private void bteCuentaF_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FrmListaCuentaContableC frmSubcuenta = new FrmListaCuentaContableC())
            {
                frmSubcuenta.tipocuenta = true;
                if (frmSubcuenta.ShowDialog() == DialogResult.OK)
                {
                    bteCuentaF.Text = frmSubcuenta._Be.ctacc_numero_cuenta_contable;
                    bteCuentaF.Tag = frmSubcuenta._Be.ctacc_icod_cuenta_contable;
                    txtCuentaF.Text = frmSubcuenta._Be.ctacc_nombre_descripcion;
                }
            }
        }
        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EVoucherContableDet> lstMovimientos = new List<EVoucherContableDet>();
            EVoucherContableDet Obe = (EVoucherContableDet)gv.GetRow(gv.FocusedRowHandle);
            if (Obe != null)
            {
                lstMovimientos = lst02.Where(x => x.ctacc_icod_cuenta_contable == Obe.ctacc_icod_cuenta_contable && x.tablc_iid_tipo_analitica == Obe.tablc_iid_tipo_analitica &&
                    x.anad_icod_analitica == Obe.anad_icod_analitica && x.cecoc_icod_centro_costo == Obe.cecoc_icod_centro_costo).ToList();
                lstMovimientos.ForEach(x =>
                {
                    x.strNroCuenta = Obe.strNroCuenta;
                    x.strDesAnalitica = Obe.strDesAnalitica;
                    x.strCodCCosto = Obe.strCodCCosto;
                    x.strAnalisis = Obe.strAnalisis;
                });

                FrmMayorAuxMesMov frm = new FrmMayorAuxMesMov();
                frm.mlist = lstMovimientos;
                frm.NoMostrarMndExt = true;
                frm.Text = "Movimientos de la Cuenta " + Obe.strNroCuenta + "  " + Obe.strCodAnaliica + "  " + Obe.strDesCCosto;
                frm.Show();
            }
        }

        private void calcularSinDetalle()
        {
            lst01.ForEach(x =>
            {
                x.anac_cecoc_tipo = (x.tablc_iid_tipo_analitica > 0) ? "ANL:" : (x.cecoc_icod_centro_costo > 0) ? "C.CST:" : "";
                x.anac_cecoc_code = (x.tablc_iid_tipo_analitica > 0) ? x.strAnalisis : (x.cecoc_icod_centro_costo > 0) ? x.strDesCCosto : "";
                x.anac_cecoc_vdescripcion = (x.anac_cecoc_vdescripcion.Length > 46) ? x.anac_cecoc_vdescripcion.Substring(0, 46).ToUpper() : x.anac_cecoc_vdescripcion.ToUpper();
                x.anac_cecoc_vdescripcion = (x.anac_cecoc_vdescripcion == "" || x.anac_cecoc_vdescripcion == null) ? "SIN ANL / SIN C.C" : x.anac_cecoc_vdescripcion;
            });

            //lstReporteSin.ForEach(x => 
            //{
            //    x.anac_cecoc_tipo = (x.iid_tipo_relacion > 0) ? "ANL:" : (x.icod_centro_costo > 0) ? "C.CST:" : "";
            //    x.anac_cecoc_code = (x.iid_tipo_relacion > 0) ? x.vdes_Analisis : (x.icod_centro_costo > 0) ? x.vcode_centro_costo : "";
            //    x.anac_cecoc_vdescripcion = (x.anac_cecoc_vdescripcion.Length > 48) ? x.anac_cecoc_vdescripcion.Substring(0, 48) : x.anac_cecoc_vdescripcion;
            //});
        }

        private void calcularConDetalle(ref decimal mtoTotalSaldoAnteriorSol, ref decimal mtoTotalSaldoAnteriorDol, ref decimal mtoTotalSaldoActualSol, ref decimal mtoTotalSaldoActualDol)
        {
            foreach (var x in lst01)
            {
                foreach (var y in lst02)
                {
                    if (!y.detFlag)
                    {
                        if (x.ctacc_icod_cuenta_contable == y.ctacc_icod_cuenta_contable && x.tablc_iid_tipo_analitica == y.tablc_iid_tipo_analitica &&
                        x.anad_icod_analitica == y.anad_icod_analitica && x.cecoc_icod_centro_costo == y.cecoc_icod_centro_costo)
                        {
                            y.anac_cecoc_tipo = (x.tablc_iid_tipo_analitica > 0) ? "ANL:" : (x.cecoc_icod_centro_costo > 0) ? "C.CST:" : "";
                            y.anac_cecoc_code = (x.tablc_iid_tipo_analitica > 0) ? x.strAnalisis : (x.cecoc_icod_centro_costo > 0) ? x.strDesCCosto : "";
                            /*********************************************************************************************/
                            y.ctacc_iid_cuenta_contable_acumulado_sol = x.ctacc_iid_cuenta_contable_acumulado_sol;
                            y.ctacc_iid_cuenta_contable_acumulado_dol = x.ctacc_iid_cuenta_contable_acumulado_dol;
                            y.ctacc_iid_cuenta_contable_saldo_sol = x.ctacc_iid_cuenta_contable_saldo_sol;
                            y.ctacc_iid_cuenta_contable_saldo_dol = x.ctacc_iid_cuenta_contable_saldo_dol;
                            /*********************************************************************************************/
                            y.cuenta_iid_cuenta_contable_acumulado_sol = lst01.Where(p => p.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable).ToList().Sum(s => s.ctacc_iid_cuenta_contable_acumulado_sol);
                            y.cuenta_iid_cuenta_contable_acumulado_dol = lst01.Where(p => p.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable).ToList().Sum(s => s.ctacc_iid_cuenta_contable_acumulado_dol);
                            y.cuenta_iid_cuenta_contable_saldo_sol = lst01.Where(p => p.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable).ToList().Sum(s => s.ctacc_iid_cuenta_contable_saldo_sol);
                            y.cuenta_iid_cuenta_contable_saldo_dol = lst01.Where(p => p.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable).ToList().Sum(s => s.ctacc_iid_cuenta_contable_saldo_dol);
                            /*********************************************************************************************/
                            y.strNroCuenta = x.strNroCuenta;
                            y.strDesCuenta = (x.strDesCuenta.Length > 41) ? x.strDesCuenta.Substring(0, 41) : x.strDesCuenta;
                            y.anac_cecoc_vdescripcion = (x.anac_cecoc_vdescripcion.Length > 37) ? x.anac_cecoc_vdescripcion.Substring(0, 37) : x.anac_cecoc_vdescripcion;
                            y.vcocd_vglosa_linea = (y.vcocd_vglosa_linea != null) ? (y.vcocd_vglosa_linea.Length > 33) ? y.vcocd_vglosa_linea.Substring(0, 33) : y.vcocd_vglosa_linea : "";
                            y.detFlag = true;
                        }
                    }
                }
                if (!x.detFlag)
                {
                    x.cuenta_iid_cuenta_contable_acumulado_sol = x.ctacc_iid_cuenta_contable_acumulado_sol;
                    x.cuenta_iid_cuenta_contable_acumulado_dol = x.ctacc_iid_cuenta_contable_acumulado_dol;
                    x.cuenta_iid_cuenta_contable_saldo_sol = x.ctacc_iid_cuenta_contable_saldo_sol;
                    x.cuenta_iid_cuenta_contable_saldo_dol = x.ctacc_iid_cuenta_contable_saldo_dol;

                    /*********************************************************************************************/
                    x.strDesCuenta = (x.strDesCuenta.Length > 41) ? x.strDesCuenta.Substring(0, 41) : x.strDesCuenta;
                    x.anac_cecoc_vdescripcion = (x.anac_cecoc_vdescripcion.Length > 37) ? x.anac_cecoc_vdescripcion.Substring(0, 37) : x.anac_cecoc_vdescripcion;
                    lst02.Add(x);
                }
            }
            //mtoTotalSaldoAnteriorSol = lst01.Where(x => x.detFlag == true).ToList().Sum(y => Convert.ToDecimal(y.ctacc_iid_cuenta_contable_acumulado_sol));

            mtoTotalSaldoAnteriorSol = lst01.Sum(y => Convert.ToDecimal(y.ctacc_iid_cuenta_contable_acumulado_sol));
            mtoTotalSaldoAnteriorDol = lst01.Sum(y => Convert.ToDecimal(y.ctacc_iid_cuenta_contable_acumulado_dol));
            mtoTotalSaldoActualSol = lst01.Sum(y => Convert.ToDecimal(y.ctacc_iid_cuenta_contable_saldo_sol));
            mtoTotalSaldoActualDol = lst01.Sum(y => Convert.ToDecimal(y.ctacc_iid_cuenta_contable_saldo_dol));
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intMes == -1)
                return;
            controlEnable(true);
            limpiarListas();
            backgroundWorker1.RunWorkerAsync();
        }
        private void controlEnable(bool Enable)
        {
            panel1.Visible = Enable;
            lkpMeses.Enabled = !Enable;
            bteCuentaI.Enabled = !Enable;
            bteCuentaF.Enabled = !Enable;
            txtCuentaI.Enabled = !Enable;
            txtCuentaF.Enabled = !Enable;
            mnuComprobantes.Enabled = !Enable;
            btnRefresh.Enabled = !Enable;
            btnLimpiar.Enabled = !Enable;
            btnBuscar.Enabled = !Enable;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                cargaMayor();
                calcularMayorAuxiliar();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            controlEnable(false);
            grd.DataSource = lst01;
            gv.GroupPanelText = "MAYOR AUXILIAR MENSUAL - " + strMes + " - CUENTAS: DE " + bteCuentaI.Text + " A " + bteCuentaF.Text;
            gv.Focus();
        }

        private void cargaMayor()
        {
            lst01 = new BContabilidad().listarMayorAuxiliarMensual(Parametros.intEjercicio, intMes, ctaInicio, ctaFin, 1);
            lst02 = new BContabilidad().listarMayorAuxiliarMensual_2(Parametros.intEjercicio, intMes, ctaInicio, ctaFin, 1);
            listaTXT = new BContabilidad().ListarComprobantesxSubdiario_TXT(Parametros.intEjercicio, intMes);
        }
        private void calcularMayorAuxiliar()
        {
            try
            {
                lst01.ForEach(x =>
                {
                    /********************ACUMULADO CUENTAS DEBE / HABER**********************/
                    x.vcocd_nmto_tot_debe_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable
                        && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica
                        && a.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList().Sum(y => y.vcocd_nmto_tot_debe_sol);
                    if (Convert.ToDecimal(x.vcocd_nmto_tot_debe_sol) > 0)
                        x.detFlag = true;

                    x.vcocd_nmto_tot_haber_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable
                        && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica
                        && a.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList().Sum(y => y.vcocd_nmto_tot_haber_sol);
                    if (Convert.ToDecimal(x.vcocd_nmto_tot_haber_sol) > 0)
                        x.detFlag = true;

                    x.vcocd_nmto_tot_debe_dol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable
                        && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica
                        && a.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList().Sum(y => y.vcocd_nmto_tot_debe_dol);
                    if (Convert.ToDecimal(x.vcocd_nmto_tot_debe_dol) > 0)
                        x.detFlag = true;

                    x.vcocd_nmto_tot_haber_dol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable
                        && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica
                        && a.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList().Sum(y => y.vcocd_nmto_tot_haber_dol);
                    if (Convert.ToDecimal(x.vcocd_nmto_tot_haber_dol) > 0)
                        x.detFlag = true;
                    /********************SALDO ACTUAL**********************/
                    x.ctacc_iid_cuenta_contable_saldo_sol = x.ctacc_iid_cuenta_contable_acumulado_sol + (x.vcocd_nmto_tot_debe_sol - x.vcocd_nmto_tot_haber_sol);
                    x.ctacc_iid_cuenta_contable_saldo_dol = x.ctacc_iid_cuenta_contable_acumulado_dol + (x.vcocd_nmto_tot_debe_dol - x.vcocd_nmto_tot_haber_dol);
                });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void limpiarListas()
        {
            lst01.Clear();
            lst02.Clear();
            gv.GroupPanelText = "Resultado de la Búsqueda";
            gv.RefreshData();
        }
        private void limpiarBotones()
        {
            bteCuentaI.Tag = null;
            bteCuentaI.Text = String.Empty;
            txtCuentaI.Text = String.Empty;
            bteCuentaF.Tag = null;
            bteCuentaF.Text = String.Empty;
            txtCuentaF.Text = String.Empty;
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarBotones();
            limpiarListas();
            lkpMeses.Focus();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            nullVal = null;
            ctaInicio = (bteCuentaI.Tag != null) ? Convert.ToInt32(bteCuentaI.Tag) : nullVal;
            ctaFin = (bteCuentaF.Tag != null) ? Convert.ToInt32(bteCuentaF.Tag) : nullVal;
            strCtaInicio = bteCuentaI.Text;
            strCtaFin = bteCuentaF.Text;
            intMes = Convert.ToInt32(lkpMeses.EditValue);
            strMes = lkpMeses.Text;
            controlEnable(true);
            limpiarListas();
            backgroundWorker1.RunWorkerAsync();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            bool activo = gv.OptionsView.ShowAutoFilterRow;
            gv.ClearColumnsFilter();
            if (activo)
            {
                btnFiltrar.Text = "Mostrar filtro";
                gv.OptionsView.ShowAutoFilterRow = !activo;
            }
            else
            {
                btnFiltrar.Text = "Ocultar filtro";
                gv.OptionsView.ShowAutoFilterRow = !activo;
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                decimal mtoTotalSaldoAnteriorSol = 0, mtoTotalSaldoAnteriorDol = 0, mtoTotalSaldoActualSol = 0, mtoTotalSaldoActualDol = 0;
                calcularConDetalle(ref mtoTotalSaldoAnteriorSol, ref mtoTotalSaldoAnteriorDol, ref mtoTotalSaldoActualSol, ref mtoTotalSaldoActualDol);
                rptConsultaLibroMayor rpt = new rptConsultaLibroMayor();
                foreach (EVoucherContableDet item in lst02)
                {
                    if (!(string.IsNullOrEmpty(item.iid_subdiario_vnum_voucher) || item.vcocd_nro_item_det == null))
                    {
                        //item.rpt_num_corre = Parametros.intEjercicio.ToString().Substring(Parametros.intEjercicio.ToString().Length - 2, 2) + string.Format("{0:00}", lkpMeses.EditValue) + item.iid_subdiario_vnum_voucher.Replace(".", string.Empty) + string.Format("{0:00}", Convert.ToInt32(item.vcocd_nro_item_det));
                    }
                }
                rpt.Cargar(lst02, strMes, strCtaInicio, strCtaFin, mtoTotalSaldoAnteriorSol, mtoTotalSaldoAnteriorDol, mtoTotalSaldoActualSol, mtoTotalSaldoActualDol);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema");
            }
        }

        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                decimal mtoTotalSaldoAnteriorSol = 0, mtoTotalSaldoAnteriorDol = 0, mtoTotalSaldoActualSol = 0, mtoTotalSaldoActualDol = 0;
                calcularConDetalle(ref mtoTotalSaldoAnteriorSol, ref mtoTotalSaldoAnteriorDol, ref mtoTotalSaldoActualSol, ref mtoTotalSaldoActualDol);
                List<EVoucherContableDet> lstTemp = new List<EVoucherContableDet>();
                EVoucherContableDet obj;
                foreach (EVoucherContableDet item in lst02.Where(obe => obe.vcocd_numero_doc != null).ToList())
                {
                    obj = new EVoucherContableDet();
                    obj.strNroCuenta = item.strNroCuenta;
                    obj.anac_cecoc_tipo = item.anac_cecoc_tipo;
                    obj.anac_cecoc_code = item.anac_cecoc_code;
                    obj.anac_cecoc_vdescripcion = item.anac_cecoc_vdescripcion;
                    obj.vcocd_numero_doc = item.vcocd_numero_doc;
                    obj.fec_cab = item.fec_cab;
                    obj.vcocd_vglosa_linea = item.vcocd_vglosa_linea;
                    obj.strMonedaVContable = item.strMonedaVContable;
                    obj.vcocd_tipo_cambio = item.vcocd_tipo_cambio;
                    obj.vcocd_nmto_tot_debe_sol = item.vcocd_nmto_tot_debe_sol;
                    obj.vcocd_nmto_tot_haber_sol = item.vcocd_nmto_tot_haber_sol;
                    obj.iid_subdiario_vnum_voucher = item.iid_subdiario_vnum_voucher;
                    obj.vcocd_nro_item_det = item.vcocd_nro_item_det;
                    lstTemp.Add(obj);
                }

                lstTemp.Where(obe => obe.anac_cecoc_tipo.Contains("CTA")).ToList().ForEach(obe => { obe.anac_cecoc_tipo = string.Empty; obe.anac_cecoc_code = string.Empty; obe.anac_cecoc_vdescripcion = string.Empty; });

                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {
                    grdExcelCD.DataSource = lstTemp.OrderBy(obe => obe.strNroCuenta).ThenBy(obe => obe.anac_cecoc_tipo).ThenBy(obe => obe.anac_cecoc_code).ThenBy(obe => obe.fec_cab).ToList();
                    string fileName = sfdRuta.FileName;
                    if (!fileName.Contains(".xlsx"))
                    {
                        grdExcelCD.ExportToXlsx(fileName + ".xlsx");
                        System.Diagnostics.Process.Start(fileName + ".xlsx");
                    }
                    else
                    {
                        grdExcelCD.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                    grdExcelCD.DataSource = null;
                    sfdRuta.FileName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void exportarTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
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
        #region Anterior
        //private void ExportarATXT(String ruta)
        //{
        //    StreamWriter sw = new StreamWriter(ruta);
        //    string error = string.Empty;
        //    int cont = 0;
        //    string columna = string.Empty;
        //    try
        //    {
        //        int totFilas = lst02.Count;
        //        foreach (EVoucherContableDet item in lst02)
        //        {
        //            cont++;
        //            //error = item.tdocc_vabreviatura_tipo_doc + " " + item.doxpc_vnumero_doc;

        //            columna = "1";
        //            sw.Write(Convert.ToDateTime(item.fec_cab).Year + String.Format("{0:00}", Convert.ToDateTime(item.fec_cab).Month) + "00|"); // 1
        //            columna = "2";
        //            sw.Write(item.vcocc_icod_vcontable + "|"); // 2
        //            columna = "3";
        //            sw.Write("M" + item.iid_subdiario_vnum_voucher +string.Format("{0:000}",item.vcocd_nro_item_det)+ "|"); // A: Aptertura M: Mes C:Cierre // 3
        //            columna = "4";
        //            sw.Write("01|"); // 4
        //            columna = "5";
        //            sw.Write(item.ctacc_icod_cuenta_contable.ToString() + "|"); // A: Aptertura M: Mes C:Cierre // 3

        //            columna = "6";
        //                sw.Write(Convert.ToDateTime(item.fec_cab).ToString("dd/MM/yyyy") + "|"); // 5
        //            columna = "7";
        //            sw.Write((item.vcocd_vglosa_linea == "" ? item.vcocd_vglosa_linea : item.vcocd_vglosa_linea) + "|"); // 6

        //            columna = "8";
        //            sw.Write(item.vcocd_nmto_tot_debe_sol + "|"); // 7


        //            columna = "9";
        //            sw.Write(item.vcocd_nmto_tot_haber_sol + "|"); // 8 NO SE APLICA

        //            columna = "10";
        //            sw.Write("|"); // 8 NO SE APLICA
        //            columna = "11";
        //            sw.Write("|"); // 8 NO SE APLICA
        //            columna = "12";
        //            sw.Write("|"); // 8 NO SE APLICA
        //            columna = "13";
        //            sw.Write("1|"); // 8 NO SE APLICA
        //            columna = "14";
        //            if (Convert.ToDateTime(item.fec_cab).Year <= Parametros.intEjercicio && Convert.ToDateTime(item.fec_cab).Month < Convert.ToInt32(lkpMeses.EditValue))
        //            {
        //                sw.Write("8|"); // 8 NO SE APLICA
        //            }
        //            else
        //            {
        //                sw.Write("1|"); // 8 NO SE APLICA
        //            }



        //            if (totFilas != cont)
        //            {
        //                sw.WriteLine();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ArgumentException(ex.Message + "\nFila " + cont + "\nDocumento " + error + "\nColumna Nº " + columna);
        //    }
        //    finally
        //    {
        //        sw.Close();
        //    }
        //}
        #endregion

        private void ExportarATXT(String ruta)
        {
            StreamWriter sw = new StreamWriter(ruta);
            string error = string.Empty;
            int cont = 0;
            string columna = string.Empty;
            try
            {
                int totFilas = listaTXT.Count;
                foreach (EComprobante item in listaTXT)
                {
                    cont++;

                    columna = "1";
                    sw.Write(item.vcocd_AnioDocu.ToString() + "|"); // 1

                    columna = "2";
                    sw.Write(item.vcocd_formato_txt.ToString() + "|"); // 2


                    columna = "3";
                    sw.Write(item.vnumero_voucher_contable.ToString() + "|"); // A: Aptertura M: Mes C:Cierre // 3  

                    columna = "4";
                    sw.Write(item.ctacc_icod_cuenta_contable.ToString() + "|"); // 4

                    columna = "5";
                    sw.Write("|"); // 5

                    columna = "6";
                    sw.Write("|"); // 6


                    columna = "7";
                    sw.Write(item.vcocd_moneda.ToString() + "|"); // 7  


                    columna = "8";
                    if (item.anad_icod_analitica > 0)
                    {
                        if (item.tarec_icorrelativo_tipo_analitica == 5 || item.tarec_icorrelativo_tipo_analitica == 2)
                        {
                            if (item.anad_iid_analitica.Substring(0, 2) == "20" || item.anad_iid_analitica.Substring(0, 2) == "10" || item.anad_iid_analitica.Substring(0, 2) == "15" || item.anad_iid_analitica.Substring(0, 2) == "17")
                            {
                                if (item.anad_iid_analitica.Trim().Length == 11)
                                {
                                    sw.Write("6" + "|"); // 8 
                                }
                                else if (item.anad_iid_analitica.Trim().Length == 8)
                                {
                                    sw.Write("1" + "|"); // 8 
                                }
                            }
                            else
                            {
                                //sw.Write("" + "|"); // 8 
                                sw.Write("|"); // 8 
                            }
                        }
                        else
                        {
                            sw.Write("|"); // 8 
                        }
                    }
                    else
                    {
                        sw.Write("|"); // 8 
                    }
                    columna = "9";
                    if (item.anad_icod_analitica > 0)
                    {
                        if (item.tarec_icorrelativo_tipo_analitica == 5 || item.tarec_icorrelativo_tipo_analitica == 2)
                        {
                            if (item.anad_iid_analitica.Substring(0, 2) == "20" || item.anad_iid_analitica.Substring(0, 2) == "10" || item.anad_iid_analitica.Substring(0, 2) == "15" || item.anad_iid_analitica.Substring(0, 2) == "17")
                            {
                                if (item.anad_iid_analitica.Trim().Length == 11)
                                {
                                    sw.Write(item.anad_iid_analitica + "|"); // 9 
                                }
                                else if (item.anad_iid_analitica.Trim().Length == 8)
                                {
                                    sw.Write(item.anad_iid_analitica + "|"); // 9 
                                }
                            }
                            else
                            {
                                sw.Write("|"); // 9 
                            }
                        }
                        else
                        {
                            sw.Write("|"); // 9 
                        }
                    }
                    else
                    {
                        sw.Write("|"); // 9 
                    }

                    columna = "10";
                    sw.Write(item.tdocc_coa.ToString() + "|"); // 10


                    columna = "11";
                    if (item.tdocc_coa == "05")
                    {
                        sw.Write("1" + "|");
                    }
                    else if (item.tdocc_coa == "54")
                    {
                        sw.Write(item.doxpc_codigo_aduana + "|");
                    }
                    else if (item.tdocc_coa == "20")
                    {
                        sw.Write(item.vcocd_numero_doc.Substring(0, 4) + "|");
                    }
                    else
                        sw.Write(item.vcocd_numero_doc__show.ToString() + "|");

                    columna = "12";
                    if (item.tdocc_coa == "05")
                    {
                        sw.Write(item.vcocd_numero_doc.ToString() + "|");
                    }
                    else if (item.tdocc_coa == "20")
                    {
                        sw.Write(item.vcocd_numero_doc.Substring(4) + "|");
                    }
                    else
                        sw.Write(item.vcocd_numero_doc.ToString() + "|");


                    columna = "13";
                    sw.Write("|");

                    columna = "14";
                    sw.Write("|");

                    columna = "15";
                    sw.Write(Convert.ToDateTime(item.sfec_nota_contable).ToString("dd/MM/yyyy") + "|"); // 8 


                    columna = "16";
                    sw.Write(item.vglosa.ToString() + "|"); // 8 



                    columna = "17";
                    sw.Write("|"); // 8 


                    columna = "18";
                    sw.Write(item.nmto_tot_debe_sol.ToString() + "|"); // 8



                    columna = "19";
                    sw.Write(item.nmto_tot_haber_dol.ToString() + "|"); // 8 



                    columna = "20";
                    sw.Write("|"); // 8 


                    columna = "21";
                    sw.Write(item.vcocd_Vperido_fech.ToString() + "|"); // 8   



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
    }
}



