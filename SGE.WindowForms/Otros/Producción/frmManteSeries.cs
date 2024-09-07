using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class frmManteSeries : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteSeries));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ERegistroSerie oBe = new ERegistroSerie();
        public List<ERegistroSerie> lstregistroserie = new List<ERegistroSerie>();


        public frmManteSeries()
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
            txtcodigo.Enabled = !Enabled;
            txtdescripcion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtcodigo.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtcodigo.Enabled = !Enabled;
        }
        public void setValues()
        {
            txtcodigo.Text = oBe.resec_icod_registro_serie;
            txtdescripcion.Text = oBe.resec_vdescripcion;
            txtTallaI.Text = oBe.resec_vtalla_inicial;
            txtTallaF.Text = oBe.resec_vtalla_final;
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
                if (string.IsNullOrEmpty(txtcodigo.Text))
                {
                    oBase = txtcodigo;
                    throw new ArgumentException("Ingresar Codigo");
                }

                if (string.IsNullOrEmpty(txtdescripcion.Text))
                {
                    oBase = txtdescripcion;
                    throw new ArgumentException("Ingresar Descripcion");
                }
                if (string.IsNullOrEmpty(txtTallaI.Text))
                {
                    oBase = txtTallaI;
                    throw new ArgumentException("Ingresar Talla Inicial");
                }
                if (string.IsNullOrEmpty(txtTallaF.Text))
                {
                    oBase = txtTallaF;
                    throw new ArgumentException("Ingresar Talla Final");
                }
                if (Convert.ToInt32(txtTallaI.Text) > Convert.ToInt32(txtTallaF.Text))
                {
                    oBase = txtTallaF;
                    throw new ArgumentException("la Talla final debe ser mayor a la talla inicial");
                }

                oBe.resec_icod_registro_serie = txtcodigo.Text;
                oBe.resec_vdescripcion = txtdescripcion.Text;
                oBe.resec_vtalla_inicial = txtTallaI.Text;
                oBe.resec_vtalla_final = txtTallaF.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.vpc = WindowsIdentity.GetCurrent().Name.ToString();

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.resec_iid_registro_serie = new BCentral().InsertarRegistroSerie(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCentral().ModificarRegistroSerie(oBe);
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
                    this.MiEvento(oBe.resec_iid_registro_serie);
                    this.Close();
                }
            }
        }
        private bool verificarCodigoUnidadM(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstregistroserie.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstregistroserie.Where(x => x.resec_icod_registro_serie.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstregistroserie.Where(x => x.resec_icod_registro_serie.Trim() == strNombre.Trim() && x.resec_iid_registro_serie != oBe.resec_iid_registro_serie).ToList().Count > 0)
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
        private bool verificarDescripcionUnidadM(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstregistroserie.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstregistroserie.Where(x => x.resec_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstregistroserie.Where(x => x.resec_vdescripcion.Trim() == strNombre.Trim() && x.resec_iid_registro_serie != oBe.resec_iid_registro_serie).ToList().Count > 0)
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

        private void frmManteSeries_Load(object sender, EventArgs e)
        {

        }
    }
}