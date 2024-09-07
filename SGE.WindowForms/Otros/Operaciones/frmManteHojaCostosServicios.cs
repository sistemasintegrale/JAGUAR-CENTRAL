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
    public partial class frmManteHojaCostosServicios : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteHojaCostosServicios));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EServicios Obe = new EServicios();
        public List<EServicios> lstHojaCostosServicios = new List<EServicios>();
        public string ItemConcepto = "";

        public frmManteHojaCostosServicios()
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
            //txtPartida.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            txtUnidadMedida.Enabled = !Enabled;
            txtCantidad.Enabled = !Enabled;
            txtPUnitario.Enabled = !Enabled;
            txtPTotal.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            //if (Status == BSMaintenanceStatus.ModifyCurrent)
            //    txtCodigo.Enabled = Enabled;
            //if (Status == BSMaintenanceStatus.CreateNew)
            //    txtCodigo.Enabled = Enabled;        
        }
        public void setValues()
        {
            txtDescripcion.Text = Obe.servd_vdescripcion;
            txtUnidadMedida.Text = Obe.servd_vunidad_medida;
            txtCantidad.Text = Obe.servd_icantidad.ToString();
            txtPUnitario.Text = Obe.servd_precio_unitario.ToString();
            txtPTotal.Text = Obe.servd_precio_total.ToString();
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
                //if (String.IsNullOrEmpty(txtPartida.Text))
                //{
                //    oBase = txtPartida;
                //    throw new ArgumentException("Ingrese Descripcion");
                //}
                //Obe.hjcd2_vdescripcion = txtPartida.Text;
                Obe.servd_vdescripcion = txtDescripcion.Text;
                Obe.servd_vunidad_medida = txtUnidadMedida.Text;
                Obe.servd_icantidad = Convert.ToInt32(txtCantidad.Text);
                Obe.servd_precio_unitario = Convert.ToDecimal(txtPUnitario.Text);
                Obe.servd_precio_total = Convert.ToDecimal(txtPTotal.Text);
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    //Obe.hjcc1_iiten = ItemConcepto;
                    Obe.intTipoOperacion = 1;
                    lstHojaCostosServicios.Add(Obe);
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

    }
}