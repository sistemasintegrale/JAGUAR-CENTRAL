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
    public partial class FrmManteTablaSuntaCab : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteTablaSuntaCab));
        public List<ETablaSunatCab> mlist = new List<ETablaSunatCab>();
        public FrmManteTablaSuntaCab()
        {
            InitializeComponent();
        }
        public BSMaintenanceStatus oState;
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
        private void StatusControl()
        {
            if (Status == BSMaintenanceStatus.View)
            {
                txtCodigo.Enabled = false;
                txtDescripcion.Enabled = false;
                btnGuardar.Enabled = false;
            }
            else if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtCodigo.Enabled = false;
            }
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
            BVentas Ob = new BVentas();
            try
            {
                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código");
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (mlist.Where(x =>
                        string.Format("{0:00}", x.suntc_codigo) ==
                        string.Format("{0:00}", txtCodigo.Text)).ToList().Count > 0)
                    {
                        throw new ArgumentException("El código ya existe");
                    }
                }
                else
                {
                    if (Convert.ToInt32(mlist.Where(x => x.suntc_icod == Convert.ToInt32(txtCodigo.Tag)).ToList()[0].suntc_codigo)
                        != Convert.ToInt32(txtCodigo.Text))
                    {
                        if (mlist.Where(x =>
                        string.Format("{0:00}", x.suntc_codigo) ==
                        string.Format("{0:00}", txtCodigo.Text)).ToList().Count > 0)
                        {
                            throw new ArgumentException("El código ya existe");
                        }
                    }
                }

                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese descripción");
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (mlist.Where(x => x.suntc_vdescripcion.ToUpper().Trim() ==
                        txtDescripcion.Text.ToUpper().Trim()).ToList().Count > 0)
                    {
                        oBase = txtDescripcion;
                        throw new ArgumentException("La descripción ya existe");
                    }
                }
                else
                {
                    if (mlist.Where(x => x.suntc_icod == Convert.ToInt32(txtCodigo.Tag)).ToList()[0].suntc_vdescripcion
                        != txtDescripcion.Text)
                    {
                        if (mlist.Where(x => x.suntc_vdescripcion.ToUpper().Trim() ==
                        txtDescripcion.Text.ToUpper().Trim()).ToList().Count > 0)
                        {
                            oBase = txtDescripcion;
                            throw new ArgumentException("La descripción ya existe");
                        }
                    }
                }
                ETablaSunatCab obj = new ETablaSunatCab();
                obj.suntc_icod = Convert.ToInt32(txtCodigo.Tag);
                obj.suntc_codigo = txtCodigo.Text;
                obj.suntc_vdescripcion = txtDescripcion.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obj.suntc_iusuario_crea = Valores.intUsuario;
                    obj.suntc_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                    Ob.TablasSunatInsertar(obj);
                }
                else
                {
                    obj.suntc_iusuario_modifica = Valores.intUsuario;
                    obj.suntc_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                    Ob.TablasSunatModificar(obj);
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;
            }
            finally
            {
                if (Flag)
                    this.DialogResult = DialogResult.OK;
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

        private void FrmManteTablaSuntaCab_Load(object sender, EventArgs e)
        {

        }
    }
}