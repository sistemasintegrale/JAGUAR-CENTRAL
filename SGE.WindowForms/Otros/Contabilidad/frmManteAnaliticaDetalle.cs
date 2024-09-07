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
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmManteAnaliticaDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteAnaliticaDetalle));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EAnaliticaDetalle Obe = new EAnaliticaDetalle();
        public List<EAnaliticaDetalle> lstAnaliticaDetalle = new List<EAnaliticaDetalle>();
        public int intTipoAnalitica = 0;

        public frmManteAnaliticaDetalle()
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
            txtNombre.Enabled = !Enabled;
            txtPaterno.Enabled = !Enabled;
            txtMaterno.Enabled = !Enabled;
            lkpTipoPersona.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
        }
        public void setValues()
        {
            txtCodigo.Text = Obe.anad_iid_analitica;
            txtDescripcion.Text = Obe.anad_vdescripcion;
            txtNombre.Text = Obe.anad_nombre;
            txtPaterno.Text = Obe.anad_apepaterno;
            txtMaterno.Text = Obe.anad_apematerno;
            lkpTipoPersona.EditValue = (Obe.tarec_icorrelativo_tipo_persona == null) ? lkpTipoPersona.EditValue = null : Obe.tarec_icorrelativo_tipo_persona;
            lkpSituacion.EditValue = Convert.ToInt32(Obe.anad_situacion);
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
                if (verificarCodigoAnalitica(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los registros de Familia");
                }

                /*----------------------*/
                if (lkpTipoPersona.Enabled == true)
                {
                    if (string.IsNullOrEmpty(txtNombre.Text))
                    {
                        oBase = txtNombre;
                        throw new ArgumentException("Ingrese nombres");
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtDescripcion.Text))
                    {
                        oBase = txtDescripcion;
                        throw new ArgumentException("Ingrese descripción");
                    }
                }
                int? nullVall = null;
                Obe.anad_iid_analitica = txtCodigo.Text;
                Obe.anad_nombre = txtNombre.Text;
                Obe.anad_apepaterno = txtPaterno.Text;
                Obe.anad_apematerno = txtMaterno.Text;
                Obe.anad_vdescripcion = (txtDescripcion.Text == "") ? String.Format("{0} {1} {2}", txtPaterno.Text, txtMaterno.Text, txtNombre.Text) : txtDescripcion.Text;
                Obe.tarec_icorrelativo_tipo_persona = (lkpTipoPersona.EditValue == null) ? nullVall : Convert.ToInt32(lkpTipoPersona.EditValue);
                Obe.anad_situacion = Convert.ToBoolean(lkpSituacion.EditValue);
                Obe.tarec_icorrelativo_tipo_analitica = intTipoAnalitica;
                Obe.anad_origen = 1;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.anad_icod_analitica = new BContabilidad().insertarAnaliticaDetalle(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BContabilidad().modificarAnaliticaDetalle(Obe);
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
                    this.MiEvento(Obe.anad_icod_analitica);
                    this.Close();
                }
            }
        }
        private bool verificarCodigoAnalitica(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstAnaliticaDetalle.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstAnaliticaDetalle.Where(x => x.anad_iid_analitica.ToString().Trim() == strCodigo.ToString().Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstAnaliticaDetalle.Where(x => x.anad_iid_analitica.ToString().Trim() == strCodigo.ToString().Trim() && x.anad_icod_analitica != Obe.anad_icod_analitica).ToList().Count > 0)
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
        private bool verificarDescripcionAnalitica(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstAnaliticaDetalle.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstAnaliticaDetalle.Where(x => x.anad_vdescripcion.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstAnaliticaDetalle.Where(x => x.anad_vdescripcion.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim() && x.anad_icod_analitica != Obe.anad_icod_analitica).ToList().Count > 0)
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

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(lkpTipoPersona.EditValue) == 2 || Convert.ToInt32(lkpTipoPersona.EditValue) == 3)
            {
                txtNombre.Text = "";
                txtPaterno.Text = "";
                txtMaterno.Text = "";
            }
        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(lkpTipoPersona.EditValue) == 1)
                txtDescripcion.Text = "";
        }

        private void txtDescripcion_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Length > 0)
            {
                lkpTipoPersona.EditValue = null;
                lkpTipoPersona.Enabled = false;
                txtNombre.Text = string.Empty;
                txtPaterno.Text = string.Empty;
                txtMaterno.Text = string.Empty;
                txtNombre.Enabled = false;
                txtPaterno.Enabled = false;
                txtMaterno.Enabled = false;
            }
            else
            {
                txtNombre.Enabled = true;
                txtPaterno.Enabled = true;
                txtMaterno.Enabled = true;
            }
        }

        private void txtNombre_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNombre.Text.Length > 0)
            {
                txtDescripcion.Text = string.Empty;
                txtDescripcion.Enabled = false;
                lkpTipoPersona.Enabled = true;
                lkpTipoPersona.ItemIndex = 0;
            }
            else
            {
                if (string.IsNullOrEmpty(txtPaterno.Text) && string.IsNullOrEmpty(txtMaterno.Text))
                {
                    lkpTipoPersona.Enabled = false;
                    lkpTipoPersona.EditValue = null;
                    txtDescripcion.Enabled = true;
                }
            }
        }

        private void txtPaterno_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPaterno.Text.Length > 0)
            { }
            else
            {
                if (string.IsNullOrEmpty(txtNombre.Text) && string.IsNullOrEmpty(txtMaterno.Text))
                {
                    lkpTipoPersona.Enabled = false;
                    lkpTipoPersona.EditValue = null;
                    txtDescripcion.Enabled = true;
                }
            }
        }

        private void txtMaterno_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaterno.Text.Length > 0)
            { }
            else
            {
                if (string.IsNullOrEmpty(txtNombre.Text) && string.IsNullOrEmpty(txtPaterno.Text))
                {
                    lkpTipoPersona.Enabled = false;
                    lkpTipoPersona.EditValue = null;
                    txtDescripcion.Enabled = true;
                }
            }
        }

        private void frmManteAnaliticaDetalle_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpTipoPersona, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoPersona), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
        }
    }
}