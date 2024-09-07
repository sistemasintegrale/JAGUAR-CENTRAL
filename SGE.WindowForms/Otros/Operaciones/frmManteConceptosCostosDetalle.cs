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

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmManteConceptosCostosDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteHojaCostosConceptos));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EConceptosCostosDetalle Obe = new EConceptosCostosDetalle();
        public List<EConceptosCostosDetalle> lstConceptosCostosDetalle = new List<EConceptosCostosDetalle>();
        public string ItemConcepto = "";
        public int intIcodConcepto = 0;

        public frmManteConceptosCostosDetalle()
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
            txtCodigo.Text = String.Format("{0:00}", Obe.chcd_icod_subconcepto_hc);
            txtOrden.Text = String.Format("{0:00}", Obe.chcd_iitem_orden);
            txtDescripcion.Text = Obe.chcd_vdescripcion;
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
                Obe.chc_icod_concepto_hc = intIcodConcepto;
                Obe.chcd_vcodigo_concepto_hc = txtCodigo.Text;
                Obe.chcd_iitem_orden = txtOrden.Text;
                Obe.chcd_vdescripcion = txtDescripcion.Text;
                Obe.chcd_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.chc_iiten = ItemConcepto;
                    Obe.chcd_icod_subconcepto_hc = new BVentas().insertarConceptosCostosDetalle(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarConceptosCostosDetalle(Obe);
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
                    this.MiEvento(Obe.chcd_icod_subconcepto_hc);
                    this.Close();
                }
            }
        }
        private bool verificarNombreBanco(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstConceptosCostosDetalle.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstConceptosCostosDetalle.Where(x => x.chcd_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstConceptosCostosDetalle.Where(x => x.chcd_vdescripcion.Trim() == strNombre.Trim() && x.chcd_icod_subconcepto_hc != Obe.chcd_icod_subconcepto_hc).ToList().Count > 0)
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