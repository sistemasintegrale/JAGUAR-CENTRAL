using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.WindowForms.Maintenance;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmManteCierrePresupuestoNacional : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteCierrePresupuestoNacional));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        public int IdPresupuestoNacional = 0;

        private BSMaintenanceStatus mStatus;

        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        #endregion

        #region "Eventos"

        public FrmManteCierrePresupuestoNacional()
        {
            InitializeComponent();
        }

        private void FrmManteCierrePresupuestoNacional_Load(object sender, EventArgs e)
        {
            dtmFechaCierre.EditValue = DateTime.Now;
            dtmFechaCierre.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            dtmFechaCierre.Enabled = !Enabled;
            dtmFechaCierre.Focus();
        }

        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                new BCompras().ActualizarCierrePresupuestoNacional(IdPresupuestoNacional, Convert.ToDateTime(dtmFechaCierre.EditValue));

            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Flag = false;
                }
            }
            finally
            {
                if (Flag)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                        Status = BSMaintenanceStatus.View;
                    else
                        Status = BSMaintenanceStatus.View;

                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }
        }


        #endregion

        private void btnGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void btnSalir_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }


    }
}