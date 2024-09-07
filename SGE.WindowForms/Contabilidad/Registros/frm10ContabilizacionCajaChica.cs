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
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Registros
{
    public partial class frm10ContabilizacionCajaChica : DevExpress.XtraEditors.XtraForm
    {
        /*-------------------------------*/
        int intMes = -1;
        string strMes = "";
        int intIndicador = 0; // 1 = Cargar , 2 = Actualizar
        /*-------------------------------*/
        List<EVoucherContableCab> lstVCOCab = new List<EVoucherContableCab>();
        List<EVoucherContableDet> lstVCOdet = new List<EVoucherContableDet>();
        BContabilidad objBContabilidad = new BContabilidad();
        /*-------------------------------*/
        public frm10ContabilizacionCajaChica()
        {
            InitializeComponent();
        }

        private void frm05GeneracionVCODXP_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
        }

        private void cargar()
        {
            try
            {
                var tuple = objBContabilidad.generarVouchersCajaChica(intMes, Valores.intUsuario, WindowsIdentity.GetCurrent().Name);
                lstVCOCab = tuple.Item1;
                lstVCOdet = tuple.Item2;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema");
            }
        }

        private void insertarVoucherContable()
        {
            try
            {
                objBContabilidad.insertarVoucherContableListasNoOptimizadas(lstVCOCab, lstVCOdet, intMes, "CAJA");
            }
            catch (Exception ex)
            {
                backgroundWorker1.CancelAsync();
                throw ex;
            }
        }
        private void enableLoading(bool flag)
        {
            picLoading.Visible = flag;
            lkpMes.Enabled = !flag;
            btnGenerar.Enabled = !flag;
            mnu.Enabled = !flag;
            if (flag)
                picLoading.Dock = DockStyle.Fill;
            else
                picLoading.Dock = DockStyle.None;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (intIndicador == 1)
                    cargar();
                if (intIndicador == 2)
                {
                    insertarVoucherContable();
                    if (backgroundWorker1.CancellationPending == true)
                        e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Infomarción del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (intIndicador == 1)
                {
                    enableLoading(false);
                    grdVCO.DataSource = lstVCOCab;
                }
                if (intIndicador == 2)
                {
                    enableLoading(false);
                    limpiarListas();
                    XtraMessageBox.Show("La actualización se realizó satisfactoriamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Infomarción del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limpiarListas()
        {
            lstVCOCab.Clear();
            lstVCOdet.Clear();
            viewVCO.RefreshData();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            /*-------------------------------------*/
            limpiarListas();
            /*-------------------------------------*/
            intMes = Convert.ToInt32(lkpMes.EditValue);
            intIndicador = 1;//Indicador 1 = Generar VCO (solamente)
            enableLoading(true);
            backgroundWorker1.RunWorkerAsync();
        }

        private void verDetalle()
        {
            try
            {
                EVoucherContableCab oBe = (EVoucherContableCab)viewVCO.GetRow(viewVCO.FocusedRowHandle);
                if (oBe == null)
                    return;
                frmMovimientoVoucher frm = new frmMovimientoVoucher();
                var lst = lstVCOdet.Where(a => a.vcocc_icod_vcontable == oBe.vcocc_icod_vcontable).ToList();
                frm.lstDetalle = lst;
                frm.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstVCOdet.Count > 0)
                {
                    intIndicador = 2;//Indicador 2 = Ingresar VCO (solamente)
                    enableLoading(true);
                    backgroundWorker1.WorkerSupportsCancellation = true;
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Infomarción del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                backgroundWorker1.CancelAsync();
            }
        }

        private void verDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                verDetalle();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewVCO.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewVCO.ClearColumnsFilter();
        }
    }
}