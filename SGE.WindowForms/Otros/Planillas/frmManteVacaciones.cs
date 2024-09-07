using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmManteVacaciones : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteVacaciones));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EVacaciones Obe = new EVacaciones();
        public List<EVacaciones> lstFamiliaDet = new List<EVacaciones>();
        public int intIcodFamiliaCab = 0;


        public frmManteVacaciones()
        {
            InitializeComponent();
        }
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtCodigo.Enabled = !Enabled;
            txtDias.Enabled = !Enabled;
            dteFechaInicio.Enabled = !Enabled;
            dteFechaFin.Enabled = !Enabled;
            lkpMes.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = false;
            txtDias.Enabled = true;
            dteFechaInicio.Enabled = true;
            dteFechaFin.Enabled = true;
            lkpMes.Enabled = true;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = false;
            txtDias.Enabled = true;
            dteFechaInicio.Enabled = true;
            dteFechaFin.Enabled = true;
            lkpMes.Enabled = true;
        }
        public void setValues()
        {

            txtCodigo.Text = Obe.vacd_iid_vacaciones;
            txtDias.Text = Obe.vacd_ndias.ToString();
            dteFechaFin.DateTime = Convert.ToDateTime(Obe.vacd_sfecha_fin);
            dteFechaInicio.DateTime = Convert.ToDateTime(Obe.vacd_sfecha_ini);
            lkpMes.EditValue = Obe.vacd_mes;

        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {




                Obe.vacd_iid_vacaciones = txtCodigo.Text;
                Obe.vacd_icod_personal = intIcodFamiliaCab;
                Obe.vacd_ndias = Convert.ToInt32(txtDias.Text);
                Obe.vacd_sfecha_ini = dteFechaInicio.DateTime;
                Obe.vacd_sfecha_fin = dteFechaFin.DateTime;
                Obe.vacd_mes = Convert.ToInt32(lkpMes.EditValue);
                Obe.vacd_año = Parametros.intEjercicio;



                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;



                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.vacd_icod_vacaciones = new BPlanillas().insertarVacaciones(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BPlanillas().modificarVacaciones(Obe);
                }
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
                    this.MiEvento(Convert.ToInt32(Obe.vacd_icod_vacaciones));
                    this.Close();
                }
            }
        }


        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmManteFamiliaDet_Load(object sender, EventArgs e)
        {
            dteFechaInicio.DateTime = DateTime.Now;
            dteFechaFin.DateTime = DateTime.Now;

            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = Convert.ToInt32(DateTime.Now.Month);
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
            {
                lkpMes.EditValue = Convert.ToInt32(Obe.vacd_mes);
            }

        }

        private void dteFechaFin_EditValueChanged(object sender, EventArgs e)
        {
            TimeSpan ts = dteFechaFin.DateTime - (dteFechaInicio.DateTime.AddDays(-1));
            txtDias.Text = ts.Days.ToString();
        }


    }
}