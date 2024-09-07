using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmRegistroInicialesProvPlanilla : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroInicialesProvPlanilla));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        public EInicial_Prov_planilla Obe = new EInicial_Prov_planilla();
        public List<EInicial_Prov_planilla> lstInicialesProvPlanilla = new List<EInicial_Prov_planilla>();


        //public decimal SumaPorcentaje=0;
        public frmRegistroInicialesProvPlanilla()
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
            txtNombre.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;


            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = !Enabled;
            txtNombre.Enabled = !Enabled;


        }
        public void setValues()
        {
            txtCodigo.Text = String.Format("{00:00}", Obe.ippc_iid_inicial_provision_planilla);
            txtNombre.Text = Obe.ippc_vdescripcion;

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
                if (String.IsNullOrEmpty(txtNombre.Text))
                {
                    oBase = txtNombre;
                    throw new ArgumentException("Ingrese Descripción Iniciales Provision Planilla");
                }




                Obe.ippc_iid_inicial_provision_planilla = txtCodigo.Text;
                Obe.ippc_vdescripcion = txtNombre.Text;


                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;




                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.ippc_icod_inicial_provision_planilla = new BPlanillas().InsertarInicial_Prov_Planilla(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BPlanillas().modificarInicial_Prov_Planilla(Obe);
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
                    this.MiEvento(Obe.ippc_icod_inicial_provision_planilla);
                    this.Close();
                }
            }
        }
        //private bool verificarNombreAlmacen(string strNombre)
        //{
        //    try 
        //    {
        //        bool exists = false;
        //        if (lstInicialesProvPlanilla.Count > 0)
        //        {
        //            if (Status == BSMaintenanceStatus.CreateNew)
        //            {
        //                if (lstTablaPlanilla.Where(x => x.tbpc_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
        //                    exists = true;
        //            }
        //            if (Status == BSMaintenanceStatus.ModifyCurrent)
        //            {
        //                if (lstTablaPlanilla.Where(x => x.tbpc_vdescripcion.Trim() == strNombre.Trim() && x.tbpc_icod_tabla_planilla != Obe.tbpc_icod_tabla_planilla).ToList().Count > 0)
        //                    exists = true;
        //            }
        //        }
        //        return exists;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


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