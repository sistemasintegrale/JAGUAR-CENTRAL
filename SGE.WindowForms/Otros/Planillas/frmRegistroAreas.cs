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

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmRegistroAreas : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroCargo));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        public EAreas Obe = new EAreas();
        public List<EAreas> lstAreas = new List<EAreas>();


        public frmRegistroAreas()
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
            txtAbreviado.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtDescripcion.Enabled = !Enabled;
                txtAbreviado.Enabled = !Enabled;

            }

        }
        public void setValues()
        {
            txtDescripcion.Text = Obe.arec_vdescripcion;
            txtAbreviado.Text = Obe.arec_vabreviado;
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
            //Boolean Flag = true;

            try
            {
                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese la Descripción del Area");
                }
                if (verificarNombreCargo(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("La Descripción ya existe en los registros de Area");
                }
                if (verificarAbreviadoCargo(txtAbreviado.Text))
                {
                    oBase = txtAbreviado;
                    throw new ArgumentException("El Abreviado ya existe en los registros de Area");
                }
                if (String.IsNullOrEmpty(txtAbreviado.Text))
                {
                    oBase = txtAbreviado;
                    throw new ArgumentException("Ingrese Abreviado del Area");
                }



                Obe.arec_vdescripcion = txtDescripcion.Text;
                Obe.arec_vabreviado = txtAbreviado.Text;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.arec_sflag_estado = true;



                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.arec_icod_cargo = new BPlanillas().insertarAreas(Obe);
                }
                if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BPlanillas().modificarAreas(Obe);
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
                //Flag = false;
            }
            finally
            {
                //if (Flag)
                //{
                if (Obe.arec_icod_cargo > 0)
                    this.MiEvento(Obe.arec_icod_cargo);
                this.Close();

                //}
            }
        }


        private bool verificarNombreCargo(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstAreas.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstAreas.Where(x => x.arec_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstAreas.Where(x => x.arec_vdescripcion.Trim() == strNombre.Trim() && x.arec_icod_cargo != Obe.arec_icod_cargo).ToList().Count > 0)
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

        private bool verificarAbreviadoCargo(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstAreas.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstAreas.Where(x => x.arec_vabreviado.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstAreas.Where(x => x.arec_vabreviado.Trim() == strNombre.Trim() && x.arec_icod_cargo != Obe.arec_icod_cargo).ToList().Count > 0)
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