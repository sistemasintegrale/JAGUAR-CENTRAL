using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Producción;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmManteOrdenProduciónPersonal : DevExpress.XtraEditors.XtraForm
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(frmManteOrdenProduciónPersonal));
        public EOrdenProduccion oBe;
        private List<EOrdenProduccionAreas> lstOrdenProduccionAreas;
        private List<EAreaProduccion> lstProceso;

        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        public frmManteOrdenProduciónPersonal() => InitializeComponent();
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => this.Dispose();

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => SetSave();

        private void SetSave()
        {
            Boolean Flag = true;
            BaseEdit oBase = null;
            try
            {
                new BCentral().ModificarOrdenProduccion(oBe, null, lstOrdenProduccionAreas, null);
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(oBe.orprc_iid_orden_produccion);
                    this.Close();
                }
            }
        }

        internal void SetModify()
        {
            lstOrdenProduccionAreas = new BCentral().listarOrdenPrduccionArea(oBe.orprc_iid_orden_produccion);
            if (lstOrdenProduccionAreas.Count == 0)
            {
                lstProceso = new BCentral().listarAreaProduccion();
                foreach (var objd in lstProceso)
                {
                    EOrdenProduccionAreas op = new EOrdenProduccionAreas();
                    op.orprac_icod_proceso = objd.arprc_iid_codigo;
                    op.strproceso = objd.arprc_vdescripcion;
                    op.orprac_sfecha_asignacion = "";
                    op.intTipoOperacion = 1;
                    op.orprc_isituacion = Constantes.EstadoTareaPersonalPendiente;
                    op.strEstado = "PENDIENTE";
                    lstOrdenProduccionAreas.Add(op);
                }
                grdPersonal.DataSource = lstOrdenProduccionAreas;
                viewPersonal.RefreshData();
            }
            else
            {
                grdPersonal.DataSource = lstOrdenProduccionAreas;
                viewPersonal.RefreshData();
            }
            viewPersonal.BestFitColumns();
        }

        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EOrdenProduccionAreas obe = (EOrdenProduccionAreas)viewPersonal.GetRow(viewPersonal.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteOrdenProduccionAreas frm = new frmManteOrdenProduccionAreas())
            {
                frm.Text = $"Orden de Producción {oBe.orprc_icod_orden_produccion} - {obe.strproceso}";
                frm.obe = obe;
                frm.FromAsignacion = true;
                frm.lstOrdenProduccionAreas = lstOrdenProduccionAreas;
                frm.SetModify();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstOrdenProduccionAreas = frm.lstOrdenProduccionAreas;
                    viewPersonal.RefreshData();

                }
            }
        }
    }
}