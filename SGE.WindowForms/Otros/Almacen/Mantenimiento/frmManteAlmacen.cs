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
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteAlmacen : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteAlmacen));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        public EAlmacen Obe = new EAlmacen();
        public List<EAlmacen> lstAlmacenes = new List<EAlmacen>();


        public frmManteAlmacen()
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
            txtDireccion.Enabled = !Enabled;
            txtRepresentante.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = Enabled;
        }
        public void setValues()
        {
            txtCodigo.Text = String.Format("{0:00}", Obe.almac_iid_almacen);
            txtNombre.Text = Obe.almac_vdescripcion;
            txtDireccion.Text = Obe.almac_vdireccion;
            txtRepresentante.Text = Obe.almac_vrepresentante;
            txtUbigeo.Text = Obe.almac_vubigeo;
            lkpEstado.EditValue = Obe.almac_iestado_almacen;


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
                    throw new ArgumentException("Ingrese nombre del Almacén");
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (verificarNombreAlmacen(txtNombre.Text))
                    {
                        oBase = txtNombre;
                        throw new ArgumentException("El nombre ingresado ya existe en los registros de Almacenes");
                    }
                }
                if (String.IsNullOrEmpty(txtDireccion.Text))
                {
                    oBase = txtDireccion;
                    throw new ArgumentException("Ingrese dirección del Almacén");
                }
                if (String.IsNullOrEmpty(txtRepresentante.Text))
                {
                    oBase = txtRepresentante;
                    throw new ArgumentException("Ingrese representante del Almacén");
                }

                Obe.almac_iid_almacen = Convert.ToInt32(txtCodigo.Text);
                Obe.almac_vdescripcion = txtNombre.Text;
                Obe.almac_vdireccion = txtDireccion.Text;
                Obe.almac_vrepresentante = txtRepresentante.Text;
                Obe.almac_sflag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.almac_iestado_almacen = Convert.ToInt32(lkpEstado.EditValue);
                Obe.almac_vubigeo = txtUbigeo.Text;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.almac_icod_almacen = new BAlmacen().insertarAlmacen(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BAlmacen().modificarAlmacen(Obe);
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
                    this.MiEvento(Obe.almac_icod_almacen);
                    this.Close();
                }
            }
        }
        private bool verificarNombreAlmacen(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstAlmacenes.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstAlmacenes.Where(x => x.almac_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstAlmacenes.Where(x => x.almac_vdescripcion.Trim() == strNombre.Trim() && x.almac_icod_almacen != Obe.almac_icod_almacen).ToList().Count > 0)
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
        public void CargarCombos()
        {

            int index = new BGeneral().listarTablaRegistro(Parametros.intTipoEstado).FindIndex(x => x.tarec_iid_tabla_registro == 262);
            BSControls.LoaderLook(lkpEstado, new BGeneral().listarTablaRegistro(Parametros.intTipoEstado).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            lkpEstado.ItemIndex = index;



        }
        private void frmManteAlmacen_Load(object sender, EventArgs e)
        {
            //BSControls.LoaderLook(lkpEstado, new BGeneral().listarTablaRegistro(59), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            CargarCombos();


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