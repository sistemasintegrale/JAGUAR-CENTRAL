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
    public partial class frmManteAnalitica : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteAnalitica));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ETablaRegistro Obe = new ETablaRegistro();
        public List<ETablaRegistro> lstTipoAnaliticas = new List<ETablaRegistro>();

        public frmManteAnalitica()
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
            txtCodigo.Text = Obe.tarec_icorrelativo_registro.ToString();
            txtDescripcion.Text = Obe.tarec_vdescripcion;
            //lkpSituacion.EditValue = Convert.ToInt32(Obe.cecoc_situacion_centro_costo);
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
                if (verificarDescripcionCCosto(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("El nombre ingresado ya existe en los registros de Familia");
                }
                Obe.tarec_icorrelativo_registro = Convert.ToInt32(txtCodigo.Text);
                Obe.tarec_vdescripcion = txtDescripcion.Text;
                Obe.tablc_iid_tipo_tabla = Parametros.intTipoTablaTipoAnalitica;
                Obe.tarec_cestado = (Convert.ToInt32(lkpSituacion.EditValue) == 1) ? 'A' : 'I';

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.tarec_iid_tabla_registro = new BAdministracionSistema().insertarTablaRegistro(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BAdministracionSistema().modificarTablaRegistro(Obe);
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
                    this.MiEvento(Obe.tarec_iid_tabla_registro);
                    this.Close();
                }
            }
        }
        private bool verificarCodigoCCosto(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstTipoAnaliticas.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTipoAnaliticas.Where(x => x.tarec_icorrelativo_registro.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTipoAnaliticas.Where(x => x.tarec_icorrelativo_registro.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim() && x.tarec_iid_tabla_registro != Obe.tarec_iid_tabla_registro).ToList().Count > 0)
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
                if (lstTipoAnaliticas.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTipoAnaliticas.Where(x => x.tarec_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTipoAnaliticas.Where(x => x.tarec_vdescripcion.Trim() == strNombre.Trim() && x.tarec_iid_tabla_registro != Obe.tarec_iid_tabla_registro).ToList().Count > 0)
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

        private void frmManteAnalitica_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
        }
    }
}