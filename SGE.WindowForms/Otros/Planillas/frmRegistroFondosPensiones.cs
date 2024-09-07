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
    public partial class frmRegistroFondosPensiones : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroFondosPensiones));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        public EFondosPensiones Obe = new EFondosPensiones();
        public List<EFondosPensiones> lstFondosPensiones = new List<EFondosPensiones>();

        List<EFondosPensionesConceptos> lstFondosPensionesConceptos = new List<EFondosPensionesConceptos>();

        public frmRegistroFondosPensiones()
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
            txtFijo.Enabled = !Enabled;
            txtMixto.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            groupBox1.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = !Enabled;
            txtNombre.Enabled = !Enabled;
            //txtFijo.Enabled = !Enabled;
            //txtMixto.Enabled = !Enabled; 
            ckeAFP.Enabled = !Enabled;
            rbActivo.Checked = true;

        }
        public void setValues()
        {
            txtCodigo.Text = String.Format("{0:00}", Obe.fdpc_iid_vcodigo_fondo);
            txtNombre.Text = Obe.fdpc_vdescripcion;
            txtFijo.Text = Obe.fdpc_nporcentaje_fijo.ToString();
            txtMixto.Text = Obe.fdpc_nporcentaje_mixto.ToString();
            ckeAFP.Checked = Convert.ToBoolean(Obe.tablc_iid_tipo_fondo_pensiones);

            rbActivo.Checked = Convert.ToBoolean(Obe.fdpc_situacion);
            rbInactivo.Checked = Convert.ToBoolean(!Obe.fdpc_situacion);


            if (Obe.tablc_iid_tipo_fondo_pensiones == true)
            {
                ckeAFP.Checked = true;
            }
            if (Obe.tablc_iid_tipo_fondo_pensiones == false)
            {
                ckeAFP.Checked = false;
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

            try
            {
                if (String.IsNullOrEmpty(txtNombre.Text))
                {
                    oBase = txtNombre;
                    throw new ArgumentException("Ingrese Descripción Fondos de Pensiones");
                }
                if (verificarNombreAlmacen(txtNombre.Text))
                {
                    oBase = txtNombre;
                    throw new ArgumentException("La Descripción ingresado ya existe en Fondos de Pensiones");
                }
                if (String.IsNullOrEmpty(txtFijo.Text))
                {
                    oBase = txtFijo;
                    throw new ArgumentException("Ingrese % Fijo de Fondos de Pensiones");
                }


                if (Convert.ToDecimal(txtFijo.Text) >= 100)
                {
                    oBase = txtFijo;
                    throw new ArgumentException("EL % Fijo no debe ser MAYOR al 100%");
                }
                if (Convert.ToDecimal(txtMixto.Text) >= 100)
                {
                    oBase = txtMixto;
                    throw new ArgumentException("EL % Mixto no debe ser MAYOR al 100%");
                }


                Obe.fdpc_iid_vcodigo_fondo = txtCodigo.Text;
                Obe.fdpc_vdescripcion = txtNombre.Text;

                Obe.fdpc_nporcentaje_fijo = Convert.ToDecimal(txtFijo.Text);

                Obe.fdpc_nporcentaje_mixto = Convert.ToDecimal(txtMixto.Text);
                Obe.fdpc_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.tablc_iid_tipo_fondo_pensiones = ckeAFP.Checked;
                Obe.fdpc_situacion = true;


                if (rbActivo.Checked == true)
                {

                    Obe.fdpc_situacion = true;
                }
                if (rbInactivo.Checked == true)
                {
                    Obe.fdpc_situacion = false;
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.fdpc_icod_fondo_pension = new BPlanillas().insertarFondosPensiones(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BPlanillas().modificarFondosPensiones(Obe);
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
                    this.MiEvento(Obe.fdpc_icod_fondo_pension);
                    this.Close();
                }
            }
        }
        private bool verificarNombreAlmacen(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstFondosPensiones.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstFondosPensiones.Where(x => x.fdpc_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstFondosPensiones.Where(x => x.fdpc_vdescripcion.Trim() == strNombre.Trim() && x.fdpc_icod_fondo_pension != Obe.fdpc_icod_fondo_pension).ToList().Count > 0)
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

            //if(ckeAFP.Checked==true){
            //txtFijo.Enabled = false;
            //txtMixto.Enabled = false;
            //}
            //else{
            //txtFijo.Enabled = true;
            //txtMixto.Enabled = true;
            //}

        }






        private void lkpEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void ckeAFP_CheckedChanged(object sender, EventArgs e)
        {
            if (ckeAFP.Checked == true)
            {
                txtFijo.Enabled = false;
                txtMixto.Enabled = false;
            }
            else
            {
                txtFijo.Enabled = true;
                txtMixto.Enabled = true;
            }
        }






    }
}