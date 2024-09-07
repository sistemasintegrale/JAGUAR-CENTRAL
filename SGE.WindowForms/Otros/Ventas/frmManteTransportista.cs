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

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteTransportista : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteTipoTarjeta));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ETransportista Obe = new ETransportista();
        public List<ETransportista> lstTransportista = new List<ETransportista>();


        public frmManteTransportista()
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
            txtNombre.Enabled = !Enabled;
            txtDireccion.Enabled = !Enabled;
            txtTelefono.Enabled = !Enabled;
            txtMICTI.Enabled = !Enabled;
            txtConductor.Enabled = !Enabled;
            txtLicencia.Enabled = !Enabled;
            txtDNI.Enabled = !Enabled;
            txtMatricula.Enabled = !Enabled;
            txtRUC.Enabled = !Enabled;
            txtPlaca.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;

        }
        public void setValues()
        {
            txtCodigo.Text = Obe.tranc_iid_transportista;
            lkpSituacion.EditValue = Obe.tranc_iid_situacion_transporte;
            txtNombre.Text = Obe.tranc_vnombre_transportista;
            txtDireccion.Text = Obe.tranc_vdireccion;
            txtTelefono.Text = Obe.tranc_vnumero_telefono;
            txtMICTI.Text = Obe.tranc_vmicti;
            txtConductor.Text = Obe.tranc_vnombre_conductor;
            txtLicencia.Text = Obe.tranc_vnum_licencia_conducir;
            txtDNI.Text = Obe.tranc_cnumero_dni;
            txtMatricula.Text = Obe.tranc_vnum_matricula;
            txtRUC.Text = Obe.tranc_vruc;
            txtPlaca.Text = Obe.tranc_vnum_placa;
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
                if (txtCodigo.Text == "000")
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código del transportista");
                }
                if (verificarCodigo(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los registros de transportistas");
                }

                if (String.IsNullOrEmpty(txtNombre.Text))
                {
                    oBase = txtNombre;
                    throw new ArgumentException("Ingrese el nombre de la empresa transportista");
                }

                if (verificarDescripcion(txtNombre.Text))
                {
                    oBase = txtNombre;
                    throw new ArgumentException("La nombre ingresado ya existe en los registros de transportista");
                }

                if (verificarRUC(txtRUC.Text))
                {
                    oBase = txtNombre;
                    throw new ArgumentException("La RUC ingresado ya existe en los registros de transportista");
                }

                Obe.tranc_iid_transportista = txtCodigo.Text;
                Obe.tranc_iid_situacion_transporte = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.tranc_vnombre_transportista = txtNombre.Text;
                Obe.tranc_vdireccion = txtDireccion.Text;
                Obe.tranc_vnumero_telefono = txtTelefono.Text;
                Obe.tranc_vmicti = txtMICTI.Text;
                Obe.tranc_vnombre_conductor = txtConductor.Text;
                Obe.tranc_vnum_licencia_conducir = txtLicencia.Text;
                Obe.tranc_cnumero_dni = txtDNI.Text;
                Obe.tranc_vnum_matricula = txtMatricula.Text;
                Obe.tranc_vruc = txtRUC.Text;
                Obe.tranc_vnum_placa = txtPlaca.Text;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.tranc_icod_transportista = new BVentas().insertarTransportista(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarTransportista(Obe);
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
                    this.MiEvento(Obe.tranc_icod_transportista);
                    this.Close();
                }
            }
        }
        private bool verificarCodigo(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstTransportista.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTransportista.Where(x => String.Format("{0:000}", x.tranc_iid_transportista) == strCodigo.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTransportista.Where(x => String.Format("{0:000}", x.tranc_iid_transportista) == strCodigo.Trim() && x.tranc_icod_transportista != Obe.tranc_icod_transportista).ToList().Count > 0)
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
        private bool verificarDescripcion(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstTransportista.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTransportista.Where(x => x.tranc_vnombre_transportista.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTransportista.Where(x => x.tranc_vnombre_transportista.Trim() == strNombre.Trim() && x.tranc_icod_transportista != Obe.tranc_icod_transportista).ToList().Count > 0)
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

        private bool verificarRUC(string strRUC)
        {
            try
            {
                bool exists = false;
                if (lstTransportista.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTransportista.Where(x => x.tranc_vruc.Trim() == strRUC.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTransportista.Where(x => x.tranc_vruc.Trim() == strRUC.Trim() && x.tranc_icod_transportista != Obe.tranc_icod_transportista).ToList().Count > 0)
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

        private void frmManteTransportista_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
        }
    }
}