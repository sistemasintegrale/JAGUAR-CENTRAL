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

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmManteVendedor : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteVendedor));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EVendedor Obe = new EVendedor();
        public List<EVendedor> lstPersonal = new List<EVendedor>();

        public frmManteVendedor()
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
            txtCodigo.Enabled = false;
            txtNombres.Enabled = !Enabled;
            txtDNI.Enabled = !Enabled;
            txtDireccion.Enabled = !Enabled;
            txtCorreo.Enabled = !Enabled;
            txtTelefono.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
        }
        public void setValues()
        {
            txtCodigo.Text = String.Format("{0:0000}", Obe.vdrsc_vcod_vendedor);
            txtNombres.Text = Obe.vdrsc_vnombre_vendedor;
            txtDNI.Text = Obe.vdrsc_vnumero_dni;
            txtDireccion.Text = Obe.vdrsc_vdireccion;
            txtCorreo.Text = Obe.vdrsc_vcorreo;
            txtTelefono.Text = Obe.vdrsc_vnumero_telefono;
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
                if (Convert.ToInt32(txtCodigo.Text) == 0)
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código");
                }
                if (verificarCodigo(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los registros de personal");
                }
                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtNombres.Text))
                {
                    oBase = txtNombres;
                    throw new ArgumentException("Ingrese Apellidos y Nombres");
                }
                /*----------------------*/

                if (verificarNombre(txtNombres.Text))
                {
                    oBase = txtNombres;
                    throw new ArgumentException("El nombre ingresado ya existe en los registros de personal");
                }

                /*----------------------*/

                Obe.vdrsc_vcod_vendedor = txtCodigo.Text;
                Obe.vdrsc_vnombre_vendedor = txtNombres.Text;
                Obe.vdrsc_vdireccion = txtDireccion.Text;
                Obe.vdrsc_vcorreo = txtCorreo.Text;
                Obe.vdrsc_vnumero_dni = txtDNI.Text;
                Obe.vdrsc_vnumero_telefono = txtTelefono.Text;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.vdrsc_icod_vendedor = new BOperaciones().insertarVendedor(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BOperaciones().modificarVendedor(Obe);
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
                    this.MiEvento(Obe.vdrsc_icod_vendedor);
                    this.Close();
                }
            }
        }

        private bool verificarNombre(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstPersonal.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstPersonal.Where(x => x.vdrsc_vnombre_vendedor.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstPersonal.Where(x => x.vdrsc_vnombre_vendedor.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim() && x.vdrsc_icod_vendedor != Obe.vdrsc_icod_vendedor).ToList().Count > 0)
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
        private bool verificarCodigo(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstPersonal.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstPersonal.Where(x => x.vdrsc_vcod_vendedor == strCodigo).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstPersonal.Where(x => x.vdrsc_vcod_vendedor == strCodigo && x.vdrsc_icod_vendedor != Obe.vdrsc_icod_vendedor).ToList().Count > 0)
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

        private void frmMantePersonal_Load(object sender, EventArgs e)
        {

        }

        private void lkpArea_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}