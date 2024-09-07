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
    public partial class frm09RptMayorPorAnaliticaAcumulado : DevExpress.XtraEditors.XtraForm
    {
        List<EVoucherContableDet> lst01 = new List<EVoucherContableDet>();
        List<EVoucherContableDet> lst02 = new List<EVoucherContableDet>();
        int intMes = -1;
        int? NullVal, intTipoAnalitica;
        string strAnaliticaInicio, strAnaliticaFin, strMes;

        public frm09RptMayorPorAnaliticaAcumulado()
        {
            InitializeComponent();
        }

        private void frm09RptMayorPorAnaliticaAcumulado_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMeses, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            lkpMeses.EditValue = DateTime.Now.Month;
            LoadMask();
        }

        private void LoadMask()
        {
            List<EParametroContable> mlista = (new BContabilidad()).listarParametroContable();
            mlista.ForEach(obe =>
            {
                this.bteCuenta.Properties.Mask.BeepOnError = true;
                this.bteCuenta.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCuenta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCuenta.Properties.Mask.ShowPlaceHolders = false;
            });
        }

        private void bteTipoAnlitica_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarAnalitica frm = new frmListarAnalitica())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteTipoAnlitica.Tag = frm._Be.tarec_icorrelativo_registro;
                    bteTipoAnlitica.Text = frm._Be.tarec_icorrelativo_registro.ToString();
                    txtTipoAnalitica.Text = frm._Be.tarec_vdescripcion;
                    /*---------------------------*/
                    bteAnaliticaI.Tag = null;
                    bteAnaliticaI.Text = String.Empty;
                    txtAnaliticaI.Text = String.Empty;
                    /*---------------------------*/
                    bteAnaliticaF.Tag = null;
                    bteAnaliticaF.Text = String.Empty;
                    txtAnaliticaF.Text = String.Empty;
                    /*---------------------------*/
                }
            }
        }

        private void bteAnaliticaI_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (bteTipoAnlitica.Tag == null)
                XtraMessageBox.Show("Seleccione Tipo de Analítica", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                using (frmListarAnaliticaDetalle frm = new frmListarAnaliticaDetalle())
                {
                    frm.intTipoAnalitica = Convert.ToInt32(bteTipoAnlitica.Tag);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteAnaliticaI.Tag = frm._Be.anad_icod_analitica;
                        bteAnaliticaI.Text = frm._Be.anad_iid_analitica;
                        txtAnaliticaI.Text = frm._Be.anad_vdescripcion;

                        bteAnaliticaF.Tag = frm._Be.anad_icod_analitica;
                        bteAnaliticaF.Text = frm._Be.anad_iid_analitica;
                        txtAnaliticaF.Text = frm._Be.anad_vdescripcion;
                    }
                }
            }
        }
        private void bteAnaliticaF_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (bteTipoAnlitica.Tag == null)
                XtraMessageBox.Show("Seleccione Tipo de Analítica", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                using (frmListarAnaliticaDetalle frm = new frmListarAnaliticaDetalle())
                {
                    frm.intTipoAnalitica = Convert.ToInt32(bteTipoAnlitica.Tag);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteAnaliticaF.Tag = frm._Be.anad_icod_analitica;
                        bteAnaliticaF.Text = frm._Be.anad_iid_analitica;
                        txtAnaliticaF.Text = frm._Be.anad_vdescripcion;
                    }
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            NullVal = null;
            limpiarListas();
            controlEnable(true);
            /*-----------------------------------------------------------------------------*/
            intTipoAnalitica = (bteTipoAnlitica.Text == "") ? NullVal : Convert.ToInt32(bteTipoAnlitica.Text);
            strAnaliticaInicio = bteAnaliticaI.Text;
            strAnaliticaFin = bteAnaliticaF.Text;
            /*-----------------------------------------------------------------------------*/
            intMes = Convert.ToInt32(lkpMeses.EditValue);
            strMes = lkpMeses.Text;
            backgroundWorker1.RunWorkerAsync();
        }
        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EVoucherContableDet Obe = (EVoucherContableDet)viewMayor.GetRow(viewMayor.FocusedRowHandle);
            if (Obe != null)
            {
                var lstMovimientos = lst02.Where(x => x.ctacc_icod_cuenta_contable == Obe.ctacc_icod_cuenta_contable && x.tablc_iid_tipo_analitica == Obe.tablc_iid_tipo_analitica && x.anad_icod_analitica == Obe.anad_icod_analitica).ToList();
                FrmMayorCCostoMov frm = new FrmMayorCCostoMov();
                frm.mlist = lstMovimientos;
                frm.Text = "Movimientos de la Analítica " + Obe.strAnalisis + " " + "Cuenta " + Obe.strNroCuenta;
                frm.Show();
            }
        }

        private void sinDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*var lstReporte = lst01.Where(x => x.detFlag == true).ToList();*/
            calcularSinDetalle();
            rptMayorPorAnaliticasSinDet rpt = new rptMayorPorAnaliticasSinDet();
            rpt.Cargar(lst01/*.Where(x => x.detFlag == true).ToList()*/, strMes, String.Format("{0:00}", intTipoAnalitica) + "." + strAnaliticaInicio,
                 String.Format("{0:00}", intTipoAnalitica) + "." + strAnaliticaFin, "MENSUAL", "<RFRM08>");
        }

        private void calcularSinDetalle()
        {

            lst01.ForEach(x =>
            {
                x.anac_cecoc_vdescripcion = (x.anac_cecoc_vdescripcion.Length > 47) ? x.anac_cecoc_vdescripcion.Substring(0, 47) : x.anac_cecoc_vdescripcion;
            });
        }

        private void conDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal? mtoTotalSaldoAnteriorSol = 0, mtoTotalSaldoAnteriorDol = 0, mtoTotalSaldoActualSol = 0, mtoTotalSaldoActualDol = 0;
            calcularConDetalle(ref mtoTotalSaldoAnteriorSol, ref mtoTotalSaldoAnteriorDol, ref mtoTotalSaldoActualSol, ref mtoTotalSaldoActualDol);

            rptMayorPorAnaliticasConDet rpt = new rptMayorPorAnaliticasConDet();
            rpt.Cargar(lst02, strMes, String.Format("{0:00}", intTipoAnalitica) + "." + strAnaliticaInicio,
                 String.Format("{0:00}", intTipoAnalitica) + "." + strAnaliticaFin, "MENSUAL", "<RFRM08>",
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
                z.cuenta_iid_cuenta_contable_acumulado_sol = lst01.Where(g => g.tablc_iid_tipo_analitica == z.tablc_iid_tipo_analitica && g.anad_icod_analitica == z.anad_icod_analitica/* && g.detFlag == true*/).ToList().Sum(f => f.ctacc_iid_cuenta_contable_acumulado_sol);
                z.cuenta_iid_cuenta_contable_acumulado_dol = lst01.Where(g => g.tablc_iid_tipo_analitica == z.tablc_iid_tipo_analitica && g.anad_icod_analitica == z.anad_icod_analitica/* && g.detFlag == true*/).ToList().Sum(f => f.ctacc_iid_cuenta_contable_acumulado_dol);
                z.cuenta_iid_cuenta_contable_saldo_sol = lst01.Where(g => g.tablc_iid_tipo_analitica == z.tablc_iid_tipo_analitica && g.anad_icod_analitica == z.anad_icod_analitica/* && g.detFlag == true*/).ToList().Sum(f => f.ctacc_iid_cuenta_contable_saldo_sol);
                z.cuenta_iid_cuenta_contable_saldo_dol = lst01.Where(g => g.tablc_iid_tipo_analitica == z.tablc_iid_tipo_analitica && g.anad_icod_analitica == z.anad_icod_analitica/* && g.detFlag == true*/).ToList().Sum(f => f.ctacc_iid_cuenta_contable_saldo_dol);
                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
                lst02.ForEach(a =>
                {
                    a.strDesCuenta = (a.strDesCuenta.Length > 38) ? a.strDesCuenta.Substring(0, 38) : a.strDesCuenta;
                    a.vcocd_vglosa_linea = (a.vcocd_vglosa_linea != null) ? (a.vcocd_vglosa_linea.Length > 24) ? a.vcocd_vglosa_linea.Substring(0, 24) : a.vcocd_vglosa_linea : "";
                    /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
                    if (z.ctacc_icod_cuenta_contable == a.ctacc_icod_cuenta_contable && z.tablc_iid_tipo_analitica == a.tablc_iid_tipo_analitica && z.anad_icod_analitica == a.anad_icod_analitica)
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
            bteTipoAnlitica.Enabled = !Enable;
            mnuComprobantes.Enabled = !Enable;
            btnRefresh.Enabled = !Enable;
            btnBuscar.Enabled = !Enable;
            btnLimpiar.Enabled = !Enable;
            bteAnaliticaI.Enabled = !Enable;
            bteAnaliticaF.Enabled = !Enable;
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
            viewMayor.GroupPanelText = "MAYOR POR ANALITICAS ACUMULADO - " + strMes.ToUpper() + " - ANALITICAS: DE " + String.Format("{0:00}", intTipoAnalitica) + "." + strAnaliticaInicio + " A " + String.Format("{0:00}", intTipoAnalitica) + "." + strAnaliticaFin;

        }
        private void limpiarBotones()
        {
            bteTipoAnlitica.Tag = null;
            bteTipoAnlitica.Text = String.Empty;
            txtTipoAnalitica.Text = String.Empty;
            /*---------------------------*/
            bteAnaliticaI.Tag = null;
            bteAnaliticaI.Text = String.Empty;
            txtAnaliticaI.Text = String.Empty;
            /*---------------------------*/
            bteAnaliticaF.Tag = null;
            bteAnaliticaF.Text = String.Empty;
            txtAnaliticaF.Text = String.Empty;
            /*---------------------------*/
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
            if (bteCuenta.Text == "")
            {
                lst01 = new BContabilidad().listarMayorAnaliticaMensual(Parametros.intEjercicio, intMes, intTipoAnalitica, (strAnaliticaInicio == "") ? null : strAnaliticaInicio, (strAnaliticaFin == "") ? null : strAnaliticaFin, 2);
                lst02 = new BContabilidad().listarMayorAnaliticaMensual_2(Parametros.intEjercicio, intMes, intTipoAnalitica, (strAnaliticaInicio == "") ? null : strAnaliticaInicio, (strAnaliticaFin == "") ? null : strAnaliticaFin, 2);
            }
            else
            {
                lst01 = new BContabilidad().listarMayorAnaliticaMensual(Parametros.intEjercicio, intMes, intTipoAnalitica, (strAnaliticaInicio == "") ? null : strAnaliticaInicio, (strAnaliticaFin == "") ? null : strAnaliticaFin, 2).Where(z => z.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuenta.Tag)).ToList();
                lst02 = new BContabilidad().listarMayorAnaliticaMensual_2(Parametros.intEjercicio, intMes, intTipoAnalitica, (strAnaliticaInicio == "") ? null : strAnaliticaInicio, (strAnaliticaFin == "") ? null : strAnaliticaFin, 2).Where(z => z.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuenta.Tag)).ToList();
            }



        }
        private void calcularMayor()
        {
            lst01.ForEach(x =>
            {
                x.vcocd_nmto_tot_debe_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica).ToList().Sum(b => b.vcocd_nmto_tot_debe_sol);
                if (Convert.ToDecimal(x.vcocd_nmto_tot_debe_sol) > 0)
                    x.detFlag = true;

                x.vcocd_nmto_tot_haber_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica).ToList().Sum(b => b.vcocd_nmto_tot_haber_sol);
                if (Convert.ToDecimal(x.vcocd_nmto_tot_haber_sol) > 0)
                    x.detFlag = true;

                x.vcocd_nmto_tot_debe_dol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica).ToList().Sum(b => b.vcocd_nmto_tot_debe_dol);
                if (Convert.ToDecimal(x.vcocd_nmto_tot_debe_dol) > 0)
                    x.detFlag = true;

                x.vcocd_nmto_tot_haber_dol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica).ToList().Sum(b => b.vcocd_nmto_tot_haber_dol);
                if (Convert.ToDecimal(x.vcocd_nmto_tot_haber_dol) > 0)
                    x.detFlag = true;
                x.ctacc_iid_cuenta_contable_saldo_sol = x.ctacc_iid_cuenta_contable_acumulado_sol + (x.vcocd_nmto_tot_debe_sol - x.vcocd_nmto_tot_haber_sol);
                x.ctacc_iid_cuenta_contable_saldo_dol = x.ctacc_iid_cuenta_contable_acumulado_dol + (x.vcocd_nmto_tot_debe_dol - x.vcocd_nmto_tot_haber_dol);

            });
        }
        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intMes == -1)
                return;
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

        private void SinDetalleExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {
                    calcularSinDetalle();
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
                    grdExcelCD.DataSource = lst02.Where(obe => (obe.vcocd_nmto_tot_debe_sol + obe.vcocd_nmto_tot_haber_sol + obe.vcocd_nmto_tot_debe_dol + obe.vcocd_nmto_tot_haber_dol) != 0).ToList();
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

        private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {

                    bteCuenta.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;


                }
            }
        }



        //private List<ETablaRegistro> auxA = new List<ETablaRegistro>();
        private List<ECuentaContable> aux = new List<ECuentaContable>();
        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        //private List<ETablaRegistro> ListaAnalitica = new List<ETablaRegistro>();
        //private List<EAnaliticaDetalle> ListaSubAnalitica = new List<EAnaliticaDetalle>();
        //private List<ECentroCosto> auxCC = new List<ECentroCosto>();
        //private List<ECentroCosto> ListaCentroCosto = new List<ECentroCosto>();

        private void clearcta()
        {
            txtCuentaDes.Text = string.Empty;
            bteCuenta.Tag = null;
        }
        private void bteCuenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (bteCuenta.Text == "")
            {
                clearcta();
                return;
            }

            aux = mlistCuenta.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuenta.Text.Replace(".", ""))).ToList();


            if (aux.Count == 1)
            {
                bteCuenta.Tag = aux[0].ctacc_icod_cuenta_contable;
                txtCuentaDes.Text = aux[0].ctacc_nombre_descripcion;




                //auxA = ListaAnalitica.Where(x => Convert.ToInt32(x.tarec_icorrelativo_registro) == aux[0].tablc_iid_tipo_analitica).ToList();
                //if (auxA.Count == 1)
                //{
                //    bteAnalitica.Tag = auxA[0].tarec_icorrelativo_registro;
                //    bteAnalitica.Text = String.Format("{0:00}", auxA[0].tarec_icorrelativo_registro);
                //    ListaSubAnalitica = new BContabilidad().listarAnaliticaDetalle(Convert.ToInt32(bteAnalitica.Tag));
                //}
            }
            else
            {
                clearcta();
            }
        }


        private void ListarCuenta()
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {

                    bteCuenta.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;


                }
            }
        }

        private void bteCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarCuenta();
        }

    }
}