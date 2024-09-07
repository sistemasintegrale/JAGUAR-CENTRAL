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
    public partial class frm12BalanceComprobacion : DevExpress.XtraEditors.XtraForm
    {
        string strDigitos = "";
        string strMoneda = "";
        string strMes = "";
        /***************/
        int intModena = 0;
        int intMes = 0;

        List<EDigito> ListarDigitos = new List<EDigito>();
        List<EVoucherContableDet> lst01 = new List<EVoucherContableDet>();
        List<EVoucherContableDet> lst02 = new List<EVoucherContableDet>();

        public frm12BalanceComprobacion()
        {
            InitializeComponent();
        }
        private void frm12BalanceComprobacion_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMeses, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5).Where(x => x.tarec_icorrelativo_registro != 3).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            getDigitos();
            BSControls.LoaderLook(lkpDigitos, ListarDigitos, "Idvdes_cod", "Idcod", false);
            lkpMeses.EditValue = DateTime.Now.Month;
            lkpMoneda.ItemIndex = 0;
            lkpDigitos.ItemIndex = 0;
        }
        private void getDigitos()
        {
            List<EParametroContable> ParametroContList = new List<EParametroContable>();
            ParametroContList = new BContabilidad().listarParametroContable();
            string[] array = ParametroContList[0].parac_vvalor_texto.Split('-');
            for (int i = 0; i < array.Length; i++)
            {
                EDigito ob = new EDigito();
                if (i != 0)
                {
                    ob.Idcod = Convert.ToInt32(array[i]) + ListarDigitos[i - 1].Idcod;
                    ob.Idvdes_cod = (Convert.ToInt32(array[i]) + ListarDigitos[i - 1].Idcod).ToString();
                    ListarDigitos.Add(ob);
                }
                else
                {
                    ob.Idcod = Convert.ToInt32(array[i]);
                    ob.Idvdes_cod = (Convert.ToInt32(array[i])).ToString();
                    ListarDigitos.Add(ob);
                }

            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            limpiarListas();
            strDigitos = lkpDigitos.Text;
            strMoneda = lkpMoneda.Text;
            intModena = Convert.ToInt32(lkpMoneda.EditValue);
            intMes = Convert.ToInt32(lkpMeses.EditValue);
            strMes = lkpMeses.Text;
            controlEnable(true);
            backgroundWorker1.RunWorkerAsync();
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (strDigitos == "")
                return;
            limpiarListas();
            controlEnable(true);
            backgroundWorker1.RunWorkerAsync();
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            lkpDigitos.ItemIndex = 0;
            lkpMeses.EditValue = DateTime.Now.Month;
            lkpDigitos.ItemIndex = 0;
            limpiarListas();
        }
        private void limpiarListas()
        {
            lst01.Clear();
            lst02.Clear();
            viewAuxiliar.RefreshData();
            viewAuxiliar.GroupPanelText = "Resultado de la Búsqueda";
        }
        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EVoucherContableDet> lstMovimientos = new List<EVoucherContableDet>();
            EVoucherContableDet Obe = (EVoucherContableDet)viewAuxiliar.GetRow(viewAuxiliar.FocusedRowHandle);
            if (Obe != null)
            {
                lstMovimientos = lst02.Where(a => a.ctacc_icod_cuenta_contable.ToString().StartsWith(Obe.ctacc_icod_cuenta_contable.ToString())).ToList();
                FrmBalanceComprobacionMov frm = new FrmBalanceComprobacionMov();
                frm.mlist = lstMovimientos;
                frm.Text = "Movimientos de la Cuenta " + Obe.strNroCuenta;
                frm.Show();
            }

        }
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (strDigitos == "2")
            {
                rptBalanceComprobacion2Digitos reporte = new rptBalanceComprobacion2Digitos();
                reporte.cargar(lst01, Parametros.intEjercicio.ToString(), strMes, "2", (intModena == 1) ? "M.N." : "M.E.", "<RFRM12>");

            }
            else
            {
                rptBalanceComprobacion reporte = new rptBalanceComprobacion();
                reporte.cargar(lst01, Parametros.intEjercicio.ToString(), strMes, strDigitos, (intModena == 1) ? "M.N." : "M.E.", "<RFRM12>");
            }
        }
        private void controlEnable(bool Enable)
        {
            panel1.Visible = Enable;
            lkpMeses.Enabled = !Enable;
            mnuComprobantes.Enabled = !Enable;
            btnRefresh.Enabled = !Enable;
            btnBuscar.Enabled = !Enable;
            btnLimpiar.Enabled = !Enable;
            lkpMoneda.Enabled = !Enable;
            lkpDigitos.Enabled = !Enable;
        }
        private void getBalanceListas()
        {
            lst01 = new BContabilidad().listarBalanceComprobacion(Parametros.intEjercicio, intMes, intModena, Convert.ToInt32(strDigitos), 1);
            lst02 = new BContabilidad().listarBalanceComprobacion_2(Parametros.intEjercicio, intMes, 1);
        }
        private void calcularBalanceSoles()
        {
            try
            {
                lst01.ForEach(x =>
                {
                    x.vcocd_nmto_tot_debe_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable.ToString().StartsWith(x.ctacc_icod_cuenta_contable.ToString())).ToList().Sum(b => b.vcocd_nmto_tot_debe_sol);
                    if (Convert.ToDecimal(x.vcocd_nmto_tot_debe_sol) > 0)
                        x.detFlag = true;

                    x.vcocd_nmto_tot_haber_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable.ToString().StartsWith(x.ctacc_icod_cuenta_contable.ToString())).ToList().Sum(b => b.vcocd_nmto_tot_haber_sol);
                    if (Convert.ToDecimal(x.vcocd_nmto_tot_haber_sol) > 0)
                        x.detFlag = true;

                    x.ctacc_iid_cuenta_contable_saldo_sol = x.ctacc_iid_cuenta_contable_acumulado_sol + (x.vcocd_nmto_tot_debe_sol - x.vcocd_nmto_tot_haber_sol);

                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void calcularBalanceDolares()
        {
            try
            {
                lst01.ForEach(x =>
                {
                    x.vcocd_nmto_tot_debe_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable.ToString().StartsWith(x.ctacc_icod_cuenta_contable.ToString())).ToList().Sum(b => b.vcocd_nmto_tot_debe_dol);
                    if (Convert.ToDecimal(x.vcocd_nmto_tot_debe_dol) > 0)
                        x.detFlag = true;

                    x.vcocd_nmto_tot_haber_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable.ToString().StartsWith(x.ctacc_icod_cuenta_contable.ToString())).ToList().Sum(b => b.vcocd_nmto_tot_haber_dol);
                    if (Convert.ToDecimal(x.vcocd_nmto_tot_haber_dol) > 0)
                        x.detFlag = true;

                    x.ctacc_iid_cuenta_contable_saldo_sol = x.ctacc_iid_cuenta_contable_acumulado_sol + (x.vcocd_nmto_tot_debe_sol - x.vcocd_nmto_tot_haber_sol);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                getBalanceListas();
                switch (intModena)
                {
                    case 1:
                        calcularBalanceSoles();
                        break;
                    case 2:
                        calcularBalanceDolares();
                        break;
                }
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
            viewAuxiliar.GroupPanelText = "BALANCE DE COMPROBACIÓN - " + strMes.ToUpper() + " - " + strMoneda + " - A: " + strDigitos + " DÍGITOS";
        }

        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //string fileName = sfdRuta.FileName;
                //if (!fileName.Contains(".xlsx"))
                //{
                //    grdAuxiliar.ExportToXlsx(fileName + ".xlsx");
                //    System.Diagnostics.Process.Start(fileName + ".xlsx");
                //}
                //else
                //{
                //    grdAuxiliar.ExportToXlsx(fileName);
                //    System.Diagnostics.Process.Start(fileName);
                //}

                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {

                    string fileName = sfdRuta.FileName;
                    if (!fileName.Contains(".xlsx"))
                    {
                        grdAuxiliar.ExportToXlsx(fileName + ".xlsx");
                        System.Diagnostics.Process.Start(fileName + ".xlsx");
                    }
                    else
                    {
                        grdAuxiliar.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                    grdExcel.DataSource = null;
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