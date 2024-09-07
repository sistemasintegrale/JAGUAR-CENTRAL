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

namespace SGE.WindowForms.Otros.Producción
{
    public partial class frmManteRegistroPersonal : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteRegistroPersonal));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EPVTPersonal oBe = new EPVTPersonal();
        public int anac_icod_analitica;
        public List<EPVTPersonal> lstRegistroPersonal = new List<EPVTPersonal>();
        public frmManteRegistroPersonal()
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
            dteFechaNacimiento.Enabled = !Enabled;
            lkpCargo.Enabled = !Enabled;
            lkpAreaEmpresa.Enabled = !Enabled;
            lkpAreaProceso.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                dteFechaNacimiento.Enabled = !Enabled;
                lkpCargo.Enabled = !Enabled;
                lkpAreaEmpresa.Enabled = !Enabled;
                lkpAreaProceso.Enabled = !Enabled;
            }
        }
        public void setValues()
        {
            txtCodigo.Text = oBe.perc_vcod_personal;
            txtNombreApellidos.Text = oBe.perc_vnombres_apellidos;
            txtDNI.Text = oBe.perc_vDNI;
            txtDomicilio.Text = oBe.perc_vdomicillo;
            txtTelefono.Text = oBe.perc_vtelefono;
            lkpAreaEmpresa.EditValue = oBe.perc_iarea_empresa;
            lkpSubAreaEmpresa.EditValue = oBe.perc_isub_area_empresa;
            lkpCargo.EditValue = oBe.perc_icargo;
            lkpAreaProceso.EditValue = oBe.perc_iarea_proceso;
            dteFechaNacimiento.EditValue = oBe.perc_sfecha_nacimiento;
            lkpSexo.EditValue = oBe.perc_isexo;
            lkpRelacionLaboral.EditValue = oBe.perc_irelacion_laboral;
            anac_icod_analitica = oBe.perc_icod_analitica;
            lkpSituacion.EditValue = oBe.perc_isituacion;
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
            setValues();
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (txtCodigo.Text == "")
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Llenar el Campos Codigo");
                }
                if (txtNombreApellidos.Text == "")
                {
                    oBase = txtNombreApellidos;
                    throw new ArgumentException("Llenar el campo Nombre Y Apellido");
                }
                oBe.perc_vcod_personal = txtCodigo.Text;
                oBe.perc_vnombres_apellidos = txtNombreApellidos.Text;
                oBe.perc_vDNI = txtDNI.Text;
                oBe.perc_vdomicillo = txtDomicilio.Text;
                oBe.perc_vtelefono = txtTelefono.Text;
                oBe.perc_iarea_empresa = Convert.ToInt32(lkpAreaEmpresa.EditValue);
                oBe.perc_isub_area_empresa = Convert.ToInt32(lkpSubAreaEmpresa.EditValue);
                oBe.perc_icargo = Convert.ToInt32(lkpCargo.EditValue);
                oBe.perc_iarea_proceso = Convert.ToInt32(lkpAreaProceso.EditValue);
                oBe.perc_sfecha_nacimiento = Convert.ToDateTime(dteFechaNacimiento.Text);
                oBe.perc_isexo = Convert.ToInt32(lkpSexo.EditValue);
                oBe.perc_irelacion_laboral = Convert.ToInt32(lkpRelacionLaboral.EditValue);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.perc_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.perc_iid_personal = new BCentral().InsertarRegistroPersonal(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    oBe.perc_icod_analitica = anac_icod_analitica;
                    new BCentral().ModificarRegistroPersonal(oBe);
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
                    this.MiEvento(oBe.perc_iid_personal);
                    this.Close();
                }
            }
        }
        private bool verificarCodigoUnidadM(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstRegistroPersonal.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstRegistroPersonal.Where(x => x.perc_vcod_personal.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstRegistroPersonal.Where(x => x.perc_vcod_personal.Trim() == strNombre.Trim() && x.perc_iid_personal != oBe.perc_iid_personal).ToList().Count > 0)
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
            EProdTablaRegistro ob = new EProdTablaRegistro();
            ob.iid_tipo_tabla = 28;
            BSControls.LoaderLook(lkpSituacion, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            BSControls.LoaderLook(lkpAreaEmpresa, new BCentral().listarAreaEmpresa(), "aremc_vdescripcion", "aremc_iid_codigo", true);

            var lstAreaProceso = new BCentral().listarAreaProduccion();
            lstAreaProceso.Insert(0, new EAreaProduccion() { arprc_vdescripcion = "NINGUNO", arprc_iid_codigo = 0 });
            BSControls.LoaderLook(lkpAreaProceso, lstAreaProceso, "arprc_vdescripcion", "arprc_iid_codigo", true);
            ob.iid_tipo_tabla = 16;
            BSControls.LoaderLook(lkpCargo, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            ob.iid_tipo_tabla = 18;
            var lstRelacionLaboral = new BCentral().ListarProdTablaRegistro(ob);
            lstRelacionLaboral.Insert(0, new EProdTablaRegistro() { descripcion = "NINGUNO", icod_tabla = 0 });
            BSControls.LoaderLook(lkpRelacionLaboral, lstRelacionLaboral, "descripcion", "icod_tabla", true);
            BSControls.LoaderLook(lkpSexo, new BGeneral().listarTablaRegistro(92), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            ESubAreaEmpresa obj = new ESubAreaEmpresa();
            obj.aremc_iid_codigo = Convert.ToInt32(lkpAreaEmpresa.EditValue);
            BSControls.LoaderLook(lkpSubAreaEmpresa, new BCentral().ListarSubAreaEmpresa(obj), "descripcion", "aremd_iid_sub_area_empresa", true);
            setFecha(dteFechaNacimiento);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpAreaEmpresa.EditValue = oBe.perc_iarea_empresa;
                lkpAreaProceso.EditValue = oBe.perc_iarea_proceso;
                lkpCargo.EditValue = oBe.perc_icargo;
                lkpRelacionLaboral.EditValue = oBe.perc_irelacion_laboral;
                lkpSexo.EditValue = oBe.perc_isexo;
                lkpSubAreaEmpresa.EditValue = oBe.perc_isub_area_empresa;
                lkpSituacion.EditValue = oBe.perc_isituacion;
            }
        }
        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void lkpRelacionLaboral_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void lkpAreaEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            ESubAreaEmpresa obj = new ESubAreaEmpresa();
            obj.aremc_iid_codigo = Convert.ToInt32(lkpAreaEmpresa.EditValue);
            BSControls.LoaderLook(lkpSubAreaEmpresa, new BCentral().ListarSubAreaEmpresa(obj), "descripcion", "aremd_iid_sub_area_empresa", true);
        }
    }
}