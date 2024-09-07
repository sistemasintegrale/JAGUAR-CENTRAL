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

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmRegistroLocalidades : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroLocalidades));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        public ELocalidades Obe = new ELocalidades();
        public List<ELocalidades> lstLocalidades = new List<ELocalidades>();


        public frmRegistroLocalidades()
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
            txtDescripcion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtDescripcion.Enabled = !Enabled;

            }

        }
        public void setValues()
        {
            txtCodigo.Text = String.Format("{00:00}", Obe.lafc_iid_localidades);
            txtDescripcion.Text = Obe.lafc_vdescripcion;

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
                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese la Localidad");
                }
                if (verificarNombreCargo(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("La Localidad ya existe");
                }



                Obe.lafc_iid_localidades = txtCodigo.Text;
                Obe.lafc_vdescripcion = txtDescripcion.Text;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.lafc_flag_estado = true;



                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.lafc_icod_localidades = new BContabilidad().InsertarLocalidades(Obe);
                }
                if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BContabilidad().modificarLocalidades(Obe);
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
                    if (Obe.lafc_icod_localidades > 0)
                        this.MiEvento(Obe.lafc_icod_localidades);
                    this.Close();

                }
            }
        }


        private bool verificarNombreCargo(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstLocalidades.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstLocalidades.Where(x => x.lafc_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstLocalidades.Where(x => x.lafc_vdescripcion.Trim() == strNombre.Trim() && x.lafc_icod_localidades != Obe.lafc_icod_localidades).ToList().Count > 0)
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

        private void frmManteAlmacen_Load(object sender, EventArgs e)
        {


        }



        private void lkpEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }




    }
}