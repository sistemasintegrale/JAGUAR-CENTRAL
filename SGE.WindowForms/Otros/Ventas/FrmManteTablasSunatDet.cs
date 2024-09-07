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
    public partial class FrmManteTablasSunatDet : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteTablasSunatDet));
        public List<ETablaSunatDet> mlist = new List<ETablaSunatDet>();
        public int cod_cab;
        public int det_cab;
        public FrmManteTablasSunatDet()
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
                    if (mlist.Where(x => x.suntd_codigo.ToUpper().Trim() ==
                        txtCodigo.Text.ToUpper().Trim()).ToList().Count > 0)
                    {
                        oBase = txtCodigo;
                        throw new ArgumentException("El código ya existe");
                    }
                }
                else
                {
                    if (mlist.Where(x => x.suntd_icod == Convert.ToInt32(txtCodigo.Tag)).ToList()[0].suntd_codigo.ToUpper().Trim()
                        != txtCodigo.Text.ToUpper().Trim())
                    {
                        if (mlist.Where(x => x.suntd_codigo.ToUpper().Trim() ==
                        txtCodigo.Text.ToUpper().Trim()).ToList().Count > 0)
                        {
                            oBase = txtCodigo;
                            throw new ArgumentException("El código ya existe");
                        }
                    }
                }
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese la descripción");
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (mlist.Where(x => x.suntd_vdescripcion.ToUpper().Trim() ==
                        txtDescripcion.Text.ToUpper().Trim()).ToList().Count > 0)
                    {
                        oBase = txtCodigo;
                        throw new ArgumentException("La descripción ya existe");
                    }
                }
                else
                {
                    if (mlist.Where(x => x.suntd_icod == Convert.ToInt32(txtCodigo.Tag)).ToList()[0].suntd_vdescripcion.ToUpper().Trim()
                        != txtDescripcion.Text.ToUpper().Trim())
                    {
                        if (mlist.Where(x => x.suntd_vdescripcion.ToUpper().Trim() ==
                        txtDescripcion.Text.ToUpper().Trim()).ToList().Count > 0)
                        {
                            oBase = txtDescripcion;
                            throw new ArgumentException("La descripción ya existe");
                        }
                    }
                }
                ETablaSunatDet obj = new ETablaSunatDet();
                obj.suntc_icod = cod_cab;
                obj.suntd_icod = det_cab;
                obj.suntd_codigo = txtCodigo.Text;
                obj.suntd_vdescripcion = txtDescripcion.Text;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obj.suntd_iusuario_crea = Valores.intUsuario;
                    obj.suntd_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                    Ob.TablasSunatDetInsertar(obj);
                }
                else
                {
                    obj.suntd_iusuario_modifica = Valores.intUsuario;
                    obj.suntd_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                    Ob.TablasSunatDetModificar(obj);
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

        private void FrmManteTablasSunatDet_Load(object sender, EventArgs e)
        {

        }
    }
}