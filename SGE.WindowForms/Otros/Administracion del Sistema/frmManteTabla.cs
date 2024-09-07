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

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmManteTabla : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteTabla));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ETabla Obe = new ETabla();
        public List<ETabla> lstTabla = new List<ETabla>();


        public frmManteTabla()
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
            txtCodigo.Enabled = Enabled;
            txtDescripcion.Enabled = !Enabled;
            lkpSituacion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Text = (lstTabla.Max(x => x.tablc_iid_tipo_tabla) + 1).ToString();

        }
        public void setValues()
        {
            txtCodigo.Text = Obe.tablc_iid_tipo_tabla.ToString();
            txtDescripcion.Text = Obe.tablc_vdescripcion;
            lkpSituacion.EditValue = (Obe.tablc_cestado == 'A') ? 1 : 0;
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
                    throw new ArgumentException("Ingrese código de tabla");
                }

                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese nombre de la tabla");
                }
                if (verificarDescripcionUnidadM(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("El nombre ingresado ya existe en los registros de tabla");
                }


                Obe.tablc_iid_tipo_tabla = Convert.ToInt32(txtCodigo.Text);
                Obe.tablc_vdescripcion = txtDescripcion.Text;
                Obe.tablc_cestado = (Convert.ToInt32(lkpSituacion.EditValue) == 1) ? 'A' : 'I';

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.tablc_iid_tipo_tabla = new BAdministracionSistema().insertarTabla(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BAdministracionSistema().modificarTabla(Obe);
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
                    this.MiEvento(Obe.tablc_iid_tipo_tabla);
                    this.Close();
                }
            }
        }

        private bool verificarDescripcionUnidadM(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstTabla.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTabla.Where(x => x.tablc_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTabla.Where(x => x.tablc_vdescripcion.Trim() == strNombre.Trim() && x.tablc_iid_tipo_tabla != Obe.tablc_iid_tipo_tabla).ToList().Count > 0)
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

        private void frmManteTabla_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
        }

    }
}