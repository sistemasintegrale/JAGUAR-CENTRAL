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
    public partial class frmManteHojaCostosSuministros : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteHojaCostosSuministros));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ESuministros Obe = new ESuministros();
        public List<ESuministros> lstHojaCostosSuministros = new List<ESuministros>();
        public string ItemConcepto = "";

        public frmManteHojaCostosSuministros()
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
            txtPartida.Enabled = !Enabled;
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
            txtPartida.Text = Obe.sumd_vpartida;
            txtDescripcion.Text = Obe.sumd_vdescripcion;
            txtUnidadMedida.Text = Obe.sumd_vunidad_medida;
            txtCantidad.Text = Obe.sumd_icantidad.ToString();
            txtPUnitario.Text = Obe.sumd_precio_unitario.ToString();
            txtPTotal.Text = Obe.sumd_precio_total.ToString();
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
                if (String.IsNullOrEmpty(txtPartida.Text))
                {
                    oBase = txtPartida;
                    throw new ArgumentException("Ingrese Descripcion");
                }
                Obe.sumd_vpartida = txtPartida.Text;
                Obe.sumd_vdescripcion = txtDescripcion.Text;
                Obe.sumd_vunidad_medida = txtUnidadMedida.Text;
                Obe.sumd_icantidad = Convert.ToInt32(txtCantidad.Text);
                Obe.sumd_precio_unitario = Convert.ToDecimal(txtPUnitario.Text);
                Obe.sumd_precio_total = Convert.ToDecimal(txtPTotal.Text);

                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.intTipoOperacion = 1;
                    lstHojaCostosSuministros.Add(Obe);
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