using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmManteHojaCostosConceptos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteHojaCostosConceptos));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EHojaCostosConceptos Obe = new EHojaCostosConceptos();
        public List<EHojaCostosConceptos> lstHojaCostosConceptos = new List<EHojaCostosConceptos>();



        public frmManteHojaCostosConceptos()
        {
            InitializeComponent();
        }
        private void frmManteBanco_Load(object sender, EventArgs e)
        {

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
            txtDescripcion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = Enabled;
        }
        public void setValues()
        {
            txtCodigo.Text = String.Format("{0:00}", Obe.hjcd1_icod_detalle_hc);
            txtOrden.Text = String.Format("{0:00}", Obe.hjcd1_iitem_orden);
            txtDescripcion.Text = Obe.hjcd1_vdescripcion;
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
                    throw new ArgumentException("Ingrese Descripcion");
                }
                Obe.hjcd1_vcodigo_concepto_hc = txtCodigo.Text;
                Obe.hjcd1_iitem_orden = txtOrden.Text;
                Obe.hjcd1_vdescripcion = txtDescripcion.Text;
                Obe.hjcd1_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.intTipoOperacion = 1;
                    lstHojaCostosConceptos.Add(Obe);
                }
                else
                {
                    if (Obe.intTipoOperacion != 1)
                    {
                        Obe.intTipoOperacion = 2;
                    }
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
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
        private bool verificarNombreBanco(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstHojaCostosConceptos.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstHojaCostosConceptos.Where(x => x.hjcd1_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstHojaCostosConceptos.Where(x => x.hjcd1_vdescripcion.Trim() == strNombre.Trim() && x.hjcd1_icod_detalle_hc != Obe.hjcd1_icod_detalle_hc).ToList().Count > 0)
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

    }
}