using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
    public partial class frm05RptMayorAuxiliarDetallado : DevExpress.XtraEditors.XtraForm
    {
        List<EVoucherContableDet> lst01 = new List<EVoucherContableDet>();
        List<EVoucherContableDet> lst02 = new List<EVoucherContableDet>();
        int intMes = -1;
        int? ctaInicio, ctaFin, nullVal;
        string strCtaInicio, strCtaFin;
        string strMes = "";
        bool printPre = true;

        public frm05RptMayorAuxiliarDetallado()
        {
            InitializeComponent();
        }
        private void frmRptMayorAuxiliarDetallado_Load(object sender, EventArgs e)
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
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            nullVal = null;
            ctaInicio = (bteCuentaI.Tag != null) ? Convert.ToInt32(bteCuentaI.Tag) : nullVal;
            ctaFin = (bteCuentaF.Tag != null) ? Convert.ToInt32(bteCuentaF.Tag) : nullVal;
            strCtaInicio = bteCuentaI.Text;
            strCtaFin = bteCuentaF.Text;
            strMes = lkpMeses.Text;
            intMes = Convert.ToInt32(lkpMeses.EditValue);
            limpiarListas();
            controlEnable(true);
            backgroundWorker1.RunWorkerAsync();
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarBotones();
            limpiarListas();
            lkpMeses.Focus();
        }
        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EVoucherContableDet Obe = (EVoucherContableDet)viewAuxiliar.GetRow(viewAuxiliar.FocusedRowHandle);
            if (Obe != null)
            {
                List<EVoucherContableDet> lstMovimientos = new List<EVoucherContableDet>();
                lstMovimientos = lst02.Where(x => x.ctacc_icod_cuenta_contable == Obe.ctacc_icod_cuenta_contable).ToList();

                FrmMayorAuxMesMov frm = new FrmMayorAuxMesMov();
                frm.mlist = lstMovimientos;
                frm.Text = "Movimientos de la Cuenta " + Obe.strNroCuenta;
                frm.Show();
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
        private void sinDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (lst01.Count == 0)
                return;
            rptMayorAuxiliarDetalladoSinDet rpt = new rptMayorAuxiliarDetalladoSinDet();
            rpt.Cargar(lst01, strMes.ToUpper(), strCtaInicio, strCtaFin, "", "MENSUAL", "<RFRM05>");

        }
        private void conDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal? mtoTotalSaldoAnteriorSol = 0, mtoTotalSaldoAnteriorDol = 0, mtoTotalSaldoActualSol = 0, mtoTotalSaldoActualDol = 0;
            calcularConDetalle(ref mtoTotalSaldoAnteriorSol, ref mtoTotalSaldoAnteriorDol, ref mtoTotalSaldoActualSol, ref mtoTotalSaldoActualDol);
            imprimirConDetalle(mtoTotalSaldoAnteriorSol, mtoTotalSaldoAnteriorDol, mtoTotalSaldoActualSol, mtoTotalSaldoActualDol);
        }

        private void imprimirConDetalle(decimal? mtoTotalSaldoAnteriorSol, decimal? mtoTotalSaldoAnteriorDol, decimal? mtoTotalSaldoActualSol, decimal? mtoTotalSaldoActualDol)
        {
            rptMayorAuxiliarDetalladoConDet rpt = new rptMayorAuxiliarDetalladoConDet();
            rpt.Cargar(lst02.Where(g => g.detFlag == true).ToList(), strMes.ToUpper(), strCtaInicio, strCtaFin, " - ", "MENSUAL", "<RFRM05>",
                Convert.ToDecimal(mtoTotalSaldoAnteriorSol), Convert.ToDecimal(mtoTotalSaldoAnteriorDol),
                Convert.ToDecimal(mtoTotalSaldoActualSol), Convert.ToDecimal(mtoTotalSaldoActualDol));
            do
            {
                if (printPre)
                {
                    printPre = false;
                    rpt.ClosePreview();
                    imprimirConDetalle(mtoTotalSaldoAnteriorSol, mtoTotalSaldoAnteriorDol, mtoTotalSaldoActualSol, mtoTotalSaldoActualDol);
                }
                else
                    rpt.ShowPreview();
            } while (printPre);
        }

        private void calcularConDetalle(ref decimal? mtoTotalSaldoAnteriorSol, ref decimal? mtoTotalSaldoAnteriorDol, ref decimal? mtoTotalSaldoActualSol, ref decimal? mtoTotalSaldoActualDol)
        {
            foreach (var x in lst01/*.Where(s=> s.detFlag == true).ToList()*/)
            {
                mtoTotalSaldoAnteriorSol += x.ctacc_iid_cuenta_contable_acumulado_sol;
                mtoTotalSaldoAnteriorDol += x.ctacc_iid_cuenta_contable_acumulado_dol;
                mtoTotalSaldoActualSol += x.ctacc_iid_cuenta_contable_saldo_sol;
                mtoTotalSaldoActualDol += x.ctacc_iid_cuenta_contable_saldo_dol;
                lst02.ForEach(y =>
                {
                    y.strDesCuenta = (y.strDesCuenta.Length > 45) ? y.strDesCuenta.Substring(0, 45) : y.strDesCuenta;
                    y.vcocd_vglosa_linea = (y.vcocd_vglosa_linea != null) ? (y.vcocd_vglosa_linea.Length > 28) ? y.vcocd_vglosa_linea.Substring(0, 28) : y.vcocd_vglosa_linea : "";
                    if (x.ctacc_icod_cuenta_contable == y.ctacc_icod_cuenta_contable)
                    {
                        y.ctacc_iid_cuenta_contable_acumulado_sol = x.ctacc_iid_cuenta_contable_acumulado_sol;
                        y.ctacc_iid_cuenta_contable_acumulado_dol = x.ctacc_iid_cuenta_contable_acumulado_dol;
                        y.ctacc_iid_cuenta_contable_saldo_sol = x.ctacc_iid_cuenta_contable_saldo_sol;
                        y.ctacc_iid_cuenta_contable_saldo_dol = x.ctacc_iid_cuenta_contable_saldo_dol;
                        y.detFlag = true;
                    }
                });
                if (!x.detFlag)
                {
                    /*********************************************************************************************/
                    lst02.Add(x);
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intMes == -1)
                return;
            limpiarListas();
            controlEnable(true);
            backgroundWorker1.RunWorkerAsync();
        }
        private void controlEnable(bool Enable)
        {
            panel1.Visible = Enable;
            lkpMeses.Enabled = !Enable;
            bteCuentaI.Enabled = !Enable;
            bteCuentaF.Enabled = !Enable;
            mnuComprobantes.Enabled = !Enable;
            btnRefresh.Enabled = !Enable;
            btnBuscar.Enabled = !Enable;
            btnLimpiar.Enabled = !Enable;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                getMayorListas();
                calcularMayorDetallado();
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
            viewAuxiliar.GroupPanelText = "MAYOR AUXILIAR DETALLADO - " + strMes.ToUpper() + " - CUENTAS: DE " + strCtaInicio + " A " + strCtaFin;
        }
        private void getMayorListas()
        {
            lst01 = new BContabilidad().listarMayorAuxiliarDetallado(Parametros.intEjercicio, intMes, ctaInicio, ctaFin, 1);
            lst02 = new BContabilidad().listarBalanceComprobacion_2(Parametros.intEjercicio, intMes, 1);
        }
        private void calcularMayorDetallado()
        {
            lst01.ForEach(x =>
            {
                x.vcocd_nmto_tot_debe_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable).ToList().Sum(b => b.vcocd_nmto_tot_debe_sol);
                if (Convert.ToDecimal(x.vcocd_nmto_tot_debe_sol) > 0)
                    x.detFlag = true;

                x.vcocd_nmto_tot_haber_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable).ToList().Sum(b => b.vcocd_nmto_tot_haber_sol);
                if (Convert.ToDecimal(x.vcocd_nmto_tot_haber_sol) > 0)
                    x.detFlag = true;

                x.vcocd_nmto_tot_debe_dol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable).ToList().Sum(b => b.vcocd_nmto_tot_debe_dol);
                if (Convert.ToDecimal(x.vcocd_nmto_tot_debe_dol) > 0)
                    x.detFlag = true;

                x.vcocd_nmto_tot_haber_dol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable).ToList().Sum(b => b.vcocd_nmto_tot_haber_dol);
                if (Convert.ToDecimal(x.vcocd_nmto_tot_haber_dol) > 0)
                    x.detFlag = true;

                x.ctacc_iid_cuenta_contable_saldo_sol = x.ctacc_iid_cuenta_contable_acumulado_sol + (x.vcocd_nmto_tot_debe_sol - x.vcocd_nmto_tot_haber_sol);
                x.ctacc_iid_cuenta_contable_saldo_dol = x.ctacc_iid_cuenta_contable_acumulado_dol + (x.vcocd_nmto_tot_debe_dol - x.vcocd_nmto_tot_haber_dol);
            });
        }

        private void SinDetalleExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
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
                    sfdRuta.FileName = string.Empty;
                }
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
                decimal? mtoTotalSaldoAnteriorSol = 0, mtoTotalSaldoAnteriorDol = 0, mtoTotalSaldoActualSol = 0, mtoTotalSaldoActualDol = 0;
                calcularConDetalle(ref mtoTotalSaldoAnteriorSol, ref mtoTotalSaldoAnteriorDol, ref mtoTotalSaldoActualSol, ref mtoTotalSaldoActualDol);

                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {
                    grdExcelCD.DataSource = lst02.Where(g => g.detFlag == true).ToList();
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



