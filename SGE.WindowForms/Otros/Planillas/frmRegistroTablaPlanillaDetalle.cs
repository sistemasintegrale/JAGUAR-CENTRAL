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
    public partial class frmRegistroTablaPlanillaDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroTablaPlanillaDetalle));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        public ETablaPlanillaDetalle Obe = new ETablaPlanillaDetalle();
        public List<ETablaPlanillaDetalle> lstTablaPlanillaDetalle = new List<ETablaPlanillaDetalle>();
        public int intIcodFondosPensiones = 0;

        public frmRegistroTablaPlanillaDetalle()
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
            txtDescripcion.Enabled = !Enabled;
            txtOtros.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;


            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)

                txtNombre.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            txtOtros.Enabled = !Enabled;


        }
        public void setValues()
        {
            txtCodigo.Text = Obe.tbpd_iid_vcodigo_tabla_planilla_detalle;
            txtNombre.Text = Obe.tbpd_vabreviado_detalle;
            txtDescripcion.Text = Obe.tbpd_vdescripcion_detalle.ToString();
            txtOtros.Text = Obe.tbpd_votros_datos.ToString();







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


                if (String.IsNullOrEmpty(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese Codigo Tabla Planilla Detalle");
                }
                if (verificarCodigoTablaPlanillaDetalle(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El Codigo ingresado ya existe en Tabla Planilla Detalle");
                }

                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese Descripción Tabla Planilla Detalle");
                }

                Obe.tbpd_iid_vcodigo_tabla_planilla_detalle = txtCodigo.Text;
                Obe.tbpd_vabreviado_detalle = txtNombre.Text;
                Obe.tbpd_vdescripcion_detalle = txtDescripcion.Text;
                Obe.tbpd_votros_datos = txtOtros.Text;
                Obe.tbpd_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.tbpc_icod_tabla_planilla = intIcodFondosPensiones;




                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.tbpd_icod_tabla_planilla_detalle = new BPlanillas().insertarTablaPlanillaDetalle(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BPlanillas().modificarTablaPlanillaDetalle(Obe);
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
                    this.MiEvento(Convert.ToInt32(Obe.tbpd_icod_tabla_planilla_detalle));
                    this.Close();
                }
            }
        }
        private bool verificarCodigoTablaPlanillaDetalle(string strcodigo)
        {
            try
            {
                bool exists = false;
                if (lstTablaPlanillaDetalle.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTablaPlanillaDetalle.Where(x => x.tbpd_iid_vcodigo_tabla_planilla_detalle.Trim() == strcodigo.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTablaPlanillaDetalle.Where(x => x.tbpd_iid_vcodigo_tabla_planilla_detalle.Trim() == strcodigo.Trim() && x.tbpd_icod_tabla_planilla_detalle != Obe.tbpd_icod_tabla_planilla_detalle).ToList().Count > 0)
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