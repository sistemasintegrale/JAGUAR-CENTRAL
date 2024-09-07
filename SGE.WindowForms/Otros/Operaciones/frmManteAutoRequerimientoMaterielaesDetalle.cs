using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmManteAutoRequerimientoMaterielaesDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteRequerimientoMaterielaesDetalle));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ERequerimientoMaterialesDetalle Obe = new ERequerimientoMaterialesDetalle();
        public List<ERequerimientoMaterialesDetalle> lstRequerimientoMaterialesDetalle = new List<ERequerimientoMaterialesDetalle>();
        public string ItemSubConcepto = "";
        public int IcodRubros = 0;
        public frmManteAutoRequerimientoMaterielaesDetalle()
        {
            InitializeComponent();
        }
        private void frmManteBanco_Load(object sender, EventArgs e)
        {
            //BSControls.LoaderLook(lkpUnidadMedida, new BAlmacen().listarUnidadMedida(), "unidc_vdescripcion", "unidc_icod_unidad_medida", true);         

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

            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCantAprovada.Enabled = true;

        }
        public void setValues()
        {
            txtCantAprovada.Text = (Obe.rqmd_cantidad_aprobada).ToString();
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



                Obe.rqmd_cantidad_aprobada = Convert.ToDecimal(txtCantAprovada.Text);
                Obe.rqmd_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.intTipoOperacion = 1;
                    lstRequerimientoMaterialesDetalle.Add(Obe);
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



        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }



        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtCUnitario_EditValueChanged(object sender, EventArgs e)
        {

        }




    }
}