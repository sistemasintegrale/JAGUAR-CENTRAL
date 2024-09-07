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

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmManteCentroCosto : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteCentroCosto));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ECentroCosto Obe = new ECentroCosto();
        public List<ECentroCosto> lstCentroCosto = new List<ECentroCosto>();

        public frmManteCentroCosto()
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
            lkpSituacion.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
        }
        public void setValues()
        {
            txtCodigo.Text = Obe.cecoc_vcodigo_centro_costo;
            txtDescripcion.Text = Obe.cecoc_vdescripcion;
            lkpSituacion.EditValue = Convert.ToInt32(Obe.cecoc_situacion_centro_costo);
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
                if (String.IsNullOrEmpty(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código de Familia");
                }
                if (verificarCodigoCCosto(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los registros de Familia");
                }

                /*----------------------*/
                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese nombre de Familia");
                }
                //if (verificarDescripcionCCosto(txtDescripcion.Text))
                //{
                //    oBase = txtDescripcion;
                //    throw new ArgumentException("El nombre ingresado ya existe en los registros de Familia");
                //}
                Obe.cecoc_vcodigo_centro_costo = txtCodigo.Text;
                Obe.cecoc_vdescripcion = txtDescripcion.Text;
                Obe.cecoc_situacion_centro_costo = Convert.ToBoolean(lkpSituacion.EditValue);
                Obe.cecoc_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.cecoc_icod_centro_costo = new BContabilidad().insertarCentroCosto(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BContabilidad().modificarCentroCosto(Obe);
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
                    this.MiEvento(Obe.cecoc_icod_centro_costo);
                    this.Close();
                }
            }
        }
        private bool verificarCodigoCCosto(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstCentroCosto.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstCentroCosto.Where(x => x.cecoc_vcodigo_centro_costo.ToString().Trim() == strCodigo.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstCentroCosto.Where(x => x.cecoc_vcodigo_centro_costo.ToString().Trim() == strCodigo.Trim() && x.cecoc_icod_centro_costo != Obe.cecoc_icod_centro_costo).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool verificarDescripcionCCosto(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstCentroCosto.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstCentroCosto.Where(x => x.cecoc_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstCentroCosto.Where(x => x.cecoc_vdescripcion.Trim() == strNombre.Trim() && x.cecoc_icod_centro_costo != Obe.cecoc_icod_centro_costo).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
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

        private void frmManteCentroCosto_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
        }


        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {

        }




    }
}