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

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmManteAreaUbicación : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteAreaUbicación));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EAreaUbicacion Obe = new EAreaUbicacion();
        public List<EAreaUbicacion> lstTabla = new List<EAreaUbicacion>();
        public int intTabla = 0;
        public bool flagExterno = false;

        public FrmManteAreaUbicación()
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
            //txtCodigo.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            lkpSituacion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;

        }
        public void setValues()
        {
            txtCodigo.Text = Obe.arubd_codigo.ToString();
            txtDescripcion.Text = Obe.descripcion;
            lkpSituacion.EditValue = (Obe.Estado == 'A') ? 1 : 0;
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
                    throw new ArgumentException("Ingrese código de tabla");
                }

                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese nombre de la tabla");
                }
                if (verificarDescripcionUnidadM(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("El nombre ingresado ya existe en los registros de tabla");
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    //List<EProdTablaRegistro> Lstver = new BCompras().getVerificarColor(Convert.ToInt32(txtCodigo.Text));
                    //if (Lstver.Count > 0)
                    //{

                    //    //oBase = txtNumero;
                    //    XtraMessageBox.Show("El Número " + Convert.ToInt32(txtCodigo.Text) + " N°: Ya Existia, Se Agrego Nuevo");
                    //    GetOCL();
                    //}
                }

                Obe.arprc_iid_codigo = intTabla;
                Obe.arubd_codigo = Convert.ToInt32(txtCodigo.Text);
                Obe.descripcion = txtDescripcion.Text;
                Obe.Estado = (Convert.ToInt32(lkpSituacion.EditValue) == 1) ? 'A' : 'I';

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BCentral().InsertarAreaUbicacion(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCentral().ModificarAreaUbicacion(Obe);
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
                    if (flagExterno)
                        this.MiEvento(Convert.ToInt32(Obe.descripcion));
                    else
                        this.MiEvento(Obe.arubd_iid_area_ubicacion);
                    this.Close();
                }
            }
        }
        private void GetOCL()
        {
            int correlativo = new BAdministracionSistema().ObtenerCorrelativoTablaRegistro(Convert.ToInt32(intTabla));
            txtCodigo.Text = correlativo.ToString();

        }
        private bool verificarDescripcionUnidadM(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstTabla.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTabla.Where(x => x.descripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTabla.Where(x => x.descripcion.Trim() == strNombre.Trim() && x.arubd_iid_area_ubicacion != Obe.arubd_iid_area_ubicacion).ToList().Count > 0)
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

        private void frmManteTabla_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
        }

    }
}