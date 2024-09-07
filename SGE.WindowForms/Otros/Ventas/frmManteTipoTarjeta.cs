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

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteTipoTarjeta : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteTipoTarjeta));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ETipoTarjeta Obe = new ETipoTarjeta();
        public List<ETipoTarjeta> lstTipoTarjeta = new List<ETipoTarjeta>();


        public frmManteTipoTarjeta()
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
            txtDescripcion.Enabled = !Enabled;
            txtComision.Enabled = !Enabled;
            lkpBanco.Enabled = !Enabled;
            lkpCuenta.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = !Enabled;
        }
        public void setValues()
        {
            txtCodigo.Text = Obe.tcrc_iid_tipo_tarjeta_cred.ToString();
            txtDescripcion.Text = Obe.tcrc_vdescripcion_tipo_tarjeta_cred;
            txtComision.Text = Obe.tcrc_nporcentaje_comision.ToString();
            lkpBanco.EditValue = Obe.bcoc_icod_banco;
            lkpCuenta.EditValue = Obe.bcod_icod_banco_cuenta;
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
                if (txtCodigo.Text == "00")
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código del tipo de tarjeta");
                }
                if (verificarCodigo(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los registros de tipos de tarjeta");
                }

                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese la descripción del tipo de tarjeta");
                }
                if (verificarDescripcion(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("La descripción ingresada ya existe en los registros de tipos de tarjeta");
                }

                Obe.tcrc_iid_tipo_tarjeta_cred = Convert.ToInt32(txtCodigo.Text);
                Obe.tcrc_vdescripcion_tipo_tarjeta_cred = txtDescripcion.Text;
                Obe.tcrc_nporcentaje_comision = Convert.ToDecimal(txtComision.Text.Substring(0, 5));
                Obe.bcoc_icod_banco = Convert.ToInt32(lkpBanco.EditValue);
                Obe.bcod_icod_banco_cuenta = Convert.ToInt32(lkpCuenta.EditValue);
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.tcrc_icod_tipo_tarjeta_cred = new BVentas().insertarTipoTarjeta(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarTipoTarjeta(Obe);
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
                    this.MiEvento(Obe.tcrc_icod_tipo_tarjeta_cred);
                    this.Close();
                }
            }
        }
        private bool verificarCodigo(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstTipoTarjeta.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTipoTarjeta.Where(x => String.Format("{0:00}", x.tcrc_iid_tipo_tarjeta_cred) == strCodigo.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTipoTarjeta.Where(x => String.Format("{0:00}", x.tcrc_iid_tipo_tarjeta_cred) == strCodigo.Trim() && x.tcrc_icod_tipo_tarjeta_cred != Obe.tcrc_icod_tipo_tarjeta_cred).ToList().Count > 0)
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
                if (lstTipoTarjeta.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTipoTarjeta.Where(x => x.tcrc_vdescripcion_tipo_tarjeta_cred.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTipoTarjeta.Where(x => x.tcrc_vdescripcion_tipo_tarjeta_cred.Trim() == strNombre.Trim() && x.tcrc_icod_tipo_tarjeta_cred != Obe.tcrc_icod_tipo_tarjeta_cred).ToList().Count > 0)
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

        private void frmManteTipoTarjeta_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpBanco, new BTesoreria().listarBancos(), "bcoc_vnombre_banco", "bcoc_icod_banco", true);

        }

        private void lkpBanco_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpCuenta, new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBanco.EditValue)), "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);
        }

    }
}