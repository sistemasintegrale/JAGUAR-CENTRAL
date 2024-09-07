using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Reportes.Contabilidad.Consultas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class frm04RptMayorAuxiliarPorCuentaMensual : DevExpress.XtraEditors.XtraForm
    {
        int? ctaInicio, ctaFin, nullVal;
        int intMes = -1;
        string strMes, strCtaInicio, strCtaFin;
        List<EVoucherContableDet> lst01 = new List<EVoucherContableDet>();
        List<EVoucherContableDet> lst02 = new List<EVoucherContableDet>();
        public frm04RptMayorAuxiliarPorCuentaMensual()
        {
            InitializeComponent();
        }
        private void FrmRptMayorAuxiliarMensual_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMeses, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMeses.EditValue = DateTime.Now.Month;
        }
        private void bteCuentaI_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarCuentaContable frmSubcuenta = new frmListarCuentaContable())
            {
                frmSubcuenta.flagSeleccionImpresion = false;
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
            using (frmListarCuentaContable frmSubcuenta = new frmListarCuentaContable())
            {
                frmSubcuenta.flagSeleccionImpresion = true;
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
            EVoucherContableDet Obe = (EVoucherContableDet)viewAuxiliar.GetRow(viewAuxiliar.FocusedRowHandle);
            if (Obe != null)
            {
                lstMovimientos = lst02.Where(x => x.ctacc_icod_cuenta_contable == Obe.ctacc_icod_cuenta_contable && x.tablc_iid_tipo_analitica == Obe.tablc_iid_tipo_analitica &&
                    x.anad_icod_analitica == Obe.anad_icod_analitica && x.cecoc_icod_centro_costo == Obe.cecoc_icod_centro_costo).ToList();
                lstMovimientos.ForEach(x =>
                {
                    x.strNroCuenta = Obe.strNroCuenta;
                    x.strCodAnaliica = Obe.strCodAnaliica;
                    x.strCodCCosto = Obe.strCodCCosto;
                    x.strAnalisis = Obe.strAnalisis;
                });

                FrmMayorAuxMesMov frm = new FrmMayorAuxMesMov();
                frm.mlist = lstMovimientos;
                frm.Text = "Movimientos de la Cuenta " + Obe.strNroCuenta + "  " + Obe.strCodAnaliica + "  " + Obe.strCodCCosto;
                frm.Show();
            }
        }
        private void sinDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptMayorAuxiliarMensual rpt = new rptMayorAuxiliarMensual();
            calcularSinDetalle();
            rpt.cargar(lst01, strMes, strCtaInicio, strCtaFin, "<RFRM04>", "MENSUAL");
        }

        private void calcularSinDetalle()
        {
            lst01.ForEach(x =>
            {
                x.anac_cecoc_tipo = (x.tablc_iid_tipo_analitica > 0) ? "ANL:" : (x.cecoc_icod_centro_costo > 0) ? "C.CST:" : "";
                x.anac_cecoc_code = (x.tablc_iid_tipo_analitica > 0) ? x.strAnalisis : (x.cecoc_icod_centro_costo > 0) ? x.strCodCCosto : "";
                x.anac_cecoc_vdescripcion = (x.anac_cecoc_vdescripcion.Length > 46) ? x.anac_cecoc_vdescripcion.Substring(0, 46).ToUpper() : x.anac_cecoc_vdescripcion.ToUpper();
                x.anac_cecoc_vdescripcion = (x.anac_cecoc_vdescripcion == "" || x.anac_cecoc_vdescripcion == null) ? "SIN ANL / SIN C.C" : x.anac_cecoc_vdescripcion;
            });

        }

        private void conDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal mtoTotalSaldoAnteriorSol = 0, mtoTotalSaldoAnteriorDol = 0, mtoTotalSaldoActualSol = 0, mtoTotalSaldoActualDol = 0;
            calcularConDetalle(ref mtoTotalSaldoAnteriorSol, ref mtoTotalSaldoAnteriorDol, ref mtoTotalSaldoActualSol, ref mtoTotalSaldoActualDol);

            rptMayorAuxiliarMensualConDet rpt = new rptMayorAuxiliarMensualConDet();
            rpt.Cargar(lst02, strMes, strCtaInicio, strCtaFin, "", "MENSUAL", "<RFRM04>", mtoTotalSaldoAnteriorSol,
                mtoTotalSaldoAnteriorDol, mtoTotalSaldoActualSol, mtoTotalSaldoActualDol);
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
                            y.anac_cecoc_code = (x.tablc_iid_tipo_analitica > 0) ? x.strAnalisis : (x.cecoc_icod_centro_costo > 0) ? x.strCodCCosto : "";
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
            grdAuxiliar.DataSource = lst01;
            viewAuxiliar.GroupPanelText = "MAYOR AUXILIAR MENSUAL - " + strMes + " - CUENTAS: DE " + bteCuentaI.Text + " A " + bteCuentaF.Text;
            viewAuxiliar.Focus();
        }

        private void cargaMayor()
        {
            lst01 = new BContabilidad().listarMayorAuxiliarMensual(Parametros.intEjercicio, intMes, ctaInicio, ctaFin, 1);
            lst02 = new BContabilidad().listarMayorAuxiliarMensual_2(Parametros.intEjercicio, intMes, ctaInicio, ctaFin, 1);
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
            viewAuxiliar.GroupPanelText = "Resultado de la Búsqueda";
            viewAuxiliar.RefreshData();
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

        private void SinDetalleExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                calcularSinDetalle();

                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {
                    grdExcelSD.DataSource = lst01;
                    string fileName = sfdRuta.FileName;
                    if (!fileName.Contains(".xlsx"))
                    {
                        grdExcelSD.ExportToXlsx(fileName + ".xlsx");
                        System.Diagnostics.Process.Start(fileName + ".xlsx");
                    }
                    else
                    {
                        grdExcelSD.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                    grdExcelSD.DataSource = null;
                }
                sfdRuta.FileName = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConDetalleExcelToolStripMenuItem_Click(object sender, EventArgs e)
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
                    grdExcelCD.DataSource = lstTemp;//.OrderBy(obe => obe.strNroCuenta).ThenBy(obe => obe.anac_cecoc_tipo).ThenBy(obe => obe.anac_cecoc_code).ThenBy(obe => obe.fec_cab).ToList();
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

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewAuxiliar.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewAuxiliar.ClearColumnsFilter();
        }
    }
}



