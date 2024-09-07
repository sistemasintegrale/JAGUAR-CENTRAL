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
    public partial class frm07RptMayorPorCCostoAcumulado : DevExpress.XtraEditors.XtraForm
    {
        List<EVoucherContableDet> lst01 = new List<EVoucherContableDet>();
        List<EVoucherContableDet> lst02 = new List<EVoucherContableDet>();
        int intMes = -1;
        int? ctaInicio, ctaFin, NullVal;
        string cCostoInicio, cCostoFin, strMes, strCuentaI, strCuentaF;
        public frm07RptMayorPorCCostoAcumulado()
        {
            InitializeComponent();
        }


        private void frmRptMayorPorCCostoMensual_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMeses, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMeses.EditValue = DateTime.Now.Month;
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            NullVal = null;
            limpiarListas();
            controlEnable(true);
            /*-----------------------------------------------------------------------------*/
            ctaInicio = (bteCuentaI.Tag == null) ? NullVal : Convert.ToInt32(bteCuentaI.Tag);
            ctaFin = (bteCuentaF.Tag == null) ? NullVal : Convert.ToInt32(bteCuentaF.Tag);
            strCuentaI = bteCuentaI.Text;
            strCuentaF = bteCuentaF.Text;
            /*-----------------------------------------------------------------------------*/
            cCostoInicio = bteCCostoI.Text;
            cCostoFin = bteCCostoF.Text;
            /*-----------------------------------------------------------------------------*/
            intMes = Convert.ToInt32(lkpMeses.EditValue);
            strMes = lkpMeses.Text;
            backgroundWorker1.RunWorkerAsync();
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
        private void bteCCostoI_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmListarCentroCosto frmCCosto = new frmListarCentroCosto();

            if (frmCCosto.ShowDialog() == DialogResult.OK)
            {
                bteCCostoI.Text = frmCCosto._Be.cecoc_vcodigo_centro_costo;
                bteCCostoI.Tag = frmCCosto._Be.cecoc_icod_centro_costo;
                txtCCostoI.Text = frmCCosto._Be.cecoc_vdescripcion;

                bteCCostoF.Text = frmCCosto._Be.cecoc_vcodigo_centro_costo;
                bteCCostoF.Tag = frmCCosto._Be.cecoc_icod_centro_costo;
                txtCCostoF.Text = frmCCosto._Be.cecoc_vdescripcion;
            }
        }
        private void bteCCostoF_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmListarCentroCosto frmCCosto = new frmListarCentroCosto();

            if (frmCCosto.ShowDialog() == DialogResult.OK)
            {
                bteCCostoF.Text = frmCCosto._Be.cecoc_vcodigo_centro_costo;
                bteCCostoF.Tag = frmCCosto._Be.cecoc_icod_centro_costo;
                txtCCostoF.Text = frmCCosto._Be.cecoc_vdescripcion;
            }
        }
        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EVoucherContableDet Obe = (EVoucherContableDet)viewMayor.GetRow(viewMayor.FocusedRowHandle);
            if (Obe != null)
            {
                var lstMovimientos = lst02.Where(x => x.ctacc_icod_cuenta_contable == Obe.ctacc_icod_cuenta_contable && x.cecoc_icod_centro_costo == Obe.cecoc_icod_centro_costo).ToList();

                FrmMayorCCostoMov frm = new FrmMayorCCostoMov();
                frm.mlist = lstMovimientos;
                frm.Text = "Detalle del Centro de Costo " + Obe.strCodCCosto + " " + "Cuenta " + Obe.strNroCuenta;
                frm.Show();
            }
        }
        private void sinDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptMayorCCostoMesSinDet rpt = new rptMayorCCostoMesSinDet();
            rpt.Cargar(lst01/*.Where(x => x.detFlag == true).ToList()*/, strMes, strCuentaI, strCuentaF, cCostoInicio, cCostoFin, "ACUMULADO", "<RFRM06>");
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intMes == -1)
                return;
            limpiarListas();
            controlEnable(true);
            backgroundWorker1.RunWorkerAsync();
        }
        private void conDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal? mtoTotalSaldoAnteriorSol = 0, mtoTotalSaldoAnteriorDol = 0, mtoTotalSaldoActualSol = 0, mtoTotalSaldoActualDol = 0;
            calcularConDetalle(ref mtoTotalSaldoAnteriorSol, ref mtoTotalSaldoAnteriorDol, ref mtoTotalSaldoActualSol, ref mtoTotalSaldoActualDol);

            rptMayorCCostoMesConDet rpt = new rptMayorCCostoMesConDet();
            rpt.Cargar(lst02, strMes, strCuentaI, strCuentaF, cCostoInicio, cCostoFin, "ACUMULADO", "<RFRM06>",
                Convert.ToDecimal(mtoTotalSaldoAnteriorSol), Convert.ToDecimal(mtoTotalSaldoAnteriorDol),
                Convert.ToDecimal(mtoTotalSaldoActualSol), Convert.ToDecimal(mtoTotalSaldoActualDol));
        }

        private void calcularConDetalle(ref decimal? mtoTotalSaldoAnteriorSol, ref decimal? mtoTotalSaldoAnteriorDol, ref decimal? mtoTotalSaldoActualSol, ref decimal? mtoTotalSaldoActualDol)
        {
            foreach (var z in lst01/*.Where(x => x.detFlag == true).ToList()*/)
            {
                mtoTotalSaldoAnteriorSol += z.ctacc_iid_cuenta_contable_acumulado_sol;
                mtoTotalSaldoAnteriorDol += z.ctacc_iid_cuenta_contable_acumulado_dol;
                mtoTotalSaldoActualSol += z.ctacc_iid_cuenta_contable_saldo_sol;
                mtoTotalSaldoActualDol += z.ctacc_iid_cuenta_contable_saldo_dol;
                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
                z.cuenta_iid_cuenta_contable_acumulado_sol = lst01.Where(g => g.cecoc_icod_centro_costo == z.cecoc_icod_centro_costo/* && g.detFlag == true*/).ToList().Sum(f => f.ctacc_iid_cuenta_contable_acumulado_sol);
                z.cuenta_iid_cuenta_contable_acumulado_dol = lst01.Where(g => g.cecoc_icod_centro_costo == z.cecoc_icod_centro_costo/* && g.detFlag == true*/).ToList().Sum(f => f.ctacc_iid_cuenta_contable_acumulado_dol);
                z.cuenta_iid_cuenta_contable_saldo_sol = lst01.Where(g => g.cecoc_icod_centro_costo == z.cecoc_icod_centro_costo/* && g.detFlag == true*/).ToList().Sum(f => f.ctacc_iid_cuenta_contable_saldo_sol);
                z.cuenta_iid_cuenta_contable_saldo_dol = lst01.Where(g => g.cecoc_icod_centro_costo == z.cecoc_icod_centro_costo/* && g.detFlag == true*/).ToList().Sum(f => f.ctacc_iid_cuenta_contable_saldo_dol);
                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
                lst02.ForEach(a =>
                {
                    a.strDesCuenta = (a.strDesCuenta.Length > 38) ? a.strDesCuenta.Substring(0, 38) : a.strDesCuenta;
                    a.vcocd_vglosa_linea = (a.vcocd_vglosa_linea != null) ? (a.vcocd_vglosa_linea.Length > 24) ? a.vcocd_vglosa_linea.Substring(0, 24) : a.vcocd_vglosa_linea : "";
                    /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
                    if (z.ctacc_icod_cuenta_contable == a.ctacc_icod_cuenta_contable && z.cecoc_icod_centro_costo == a.cecoc_icod_centro_costo)
                    {
                        a.cuenta_iid_cuenta_contable_acumulado_sol = z.cuenta_iid_cuenta_contable_acumulado_sol;
                        a.cuenta_iid_cuenta_contable_acumulado_dol = z.cuenta_iid_cuenta_contable_acumulado_dol;
                        a.cuenta_iid_cuenta_contable_saldo_sol = z.cuenta_iid_cuenta_contable_saldo_sol;
                        a.cuenta_iid_cuenta_contable_saldo_dol = z.cuenta_iid_cuenta_contable_saldo_dol;
                        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
                        a.anac_cecoc_vdescripcion = z.anac_cecoc_vdescripcion;
                        a.ctacc_iid_cuenta_contable_acumulado_sol = z.ctacc_iid_cuenta_contable_acumulado_sol;
                        a.ctacc_iid_cuenta_contable_acumulado_dol = z.ctacc_iid_cuenta_contable_acumulado_dol;
                        a.ctacc_iid_cuenta_contable_saldo_sol = z.ctacc_iid_cuenta_contable_saldo_sol;
                        a.ctacc_iid_cuenta_contable_saldo_dol = z.ctacc_iid_cuenta_contable_saldo_dol;
                    }
                });
                if (!z.detFlag)
                {
                    z.strDesCuenta = (z.strDesCuenta.Length > 38) ? z.strDesCuenta.Substring(0, 38) : z.strDesCuenta;
                    lst02.Add(z);
                }
            }
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
            bteCCostoI.Enabled = !Enable;
            bteCCostoF.Enabled = !Enable;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                getMayorListas();
                calcularMayor();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            controlEnable(false);
            grdMayor.DataSource = lst01;
            viewMayor.GroupPanelText = "MAYOR POR C.COSTO ACUMULADO - " + strMes.ToUpper() + " - CUENTAS: DE " + strCuentaI + " A " + strCuentaF + " - C.COSTO: DE " + cCostoInicio + " A " + cCostoFin;

        }
        private void limpiarBotones()
        {
            bteCuentaI.Tag = null;
            bteCuentaI.Text = String.Empty;
            txtCuentaI.Text = String.Empty;
            bteCuentaF.Tag = null;
            bteCuentaF.Text = String.Empty;
            txtCuentaF.Text = String.Empty;
            /*---------------------------*/
            bteCCostoI.Tag = null;
            bteCCostoI.Text = String.Empty;
            txtCCostoI.Text = String.Empty;
            /*---------------------------*/
            bteCCostoF.Tag = null;
            bteCCostoF.Text = String.Empty;
            txtCCostoF.Text = String.Empty;

        }
        private void limpiarListas()
        {
            lst01.Clear();
            lst02.Clear();
            viewMayor.RefreshData();
            viewMayor.GroupPanelText = "Resultado de la Búsqueda";
        }
        private void getMayorListas()
        {
            lst01 = new BContabilidad().listarMayorCCostoMensual(Parametros.intEjercicio, intMes, ctaInicio, ctaFin, (cCostoInicio == "") ? null : cCostoInicio, (cCostoFin == "") ? null : cCostoFin, 2);
            lst02 = new BContabilidad().listarMayorCCostoMensual_2(Parametros.intEjercicio, intMes, ctaInicio, ctaFin, (cCostoInicio == "") ? null : cCostoInicio, (cCostoFin == "") ? null : cCostoFin, 2);
        }
        private void calcularMayor()
        {
            lst01.ForEach(x =>
            {
                x.vcocd_nmto_tot_debe_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable && a.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList().Sum(b => b.vcocd_nmto_tot_debe_sol);
                if (Convert.ToDecimal(x.vcocd_nmto_tot_debe_sol) > 0)
                    x.detFlag = true;

                x.vcocd_nmto_tot_haber_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable && a.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList().Sum(b => b.vcocd_nmto_tot_haber_sol);
                if (Convert.ToDecimal(x.vcocd_nmto_tot_haber_sol) > 0)
                    x.detFlag = true;

                x.vcocd_nmto_tot_debe_dol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable && a.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList().Sum(b => b.vcocd_nmto_tot_debe_dol);
                if (Convert.ToDecimal(x.vcocd_nmto_tot_debe_dol) > 0)
                    x.detFlag = true;

                x.vcocd_nmto_tot_haber_dol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable && a.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList().Sum(b => b.vcocd_nmto_tot_haber_dol);
                if (Convert.ToDecimal(x.vcocd_nmto_tot_haber_dol) > 0)
                    x.detFlag = true;
                x.ctacc_iid_cuenta_contable_saldo_sol = x.ctacc_iid_cuenta_contable_acumulado_sol + (x.vcocd_nmto_tot_debe_sol - x.vcocd_nmto_tot_haber_sol);
                x.ctacc_iid_cuenta_contable_saldo_dol = x.ctacc_iid_cuenta_contable_acumulado_dol + (x.vcocd_nmto_tot_debe_dol - x.vcocd_nmto_tot_haber_dol);

            });
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarBotones();
            limpiarListas();
            lkpMeses.Focus();
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
                    grdExcelCD.DataSource = lst02.Where(obe => (obe.vcocd_nmto_tot_debe_sol + obe.vcocd_nmto_tot_haber_sol + obe.vcocd_nmto_tot_debe_dol + obe.vcocd_nmto_tot_haber_sol) != 0).ToList();
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
            viewMayor.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewMayor.ClearColumnsFilter();
        }
    }
}

