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
    public partial class frmManteRequerimientoMaterielaesDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteRequerimientoMaterielaesDetalle));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ERequerimientoMaterialesDetalle Obe = new ERequerimientoMaterialesDetalle();
        public List<ERequerimientoMaterialesDetalle> lstRequerimientoMaterialesDetalle = new List<ERequerimientoMaterialesDetalle>();
        public string ItemSubConcepto = "";
        public int IcodRubros = 0;
        public int prdc_icod_producto = 0;
        public frmManteRequerimientoMaterielaesDetalle()
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
            txtItem.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtItem.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtItem.Enabled = Enabled;
        }
        public void setValues()
        {
            txtItem.Text = String.Format("{0:00}", Obe.rqmd_vcodigo_item_requerim);
            txtCantidad.Text = (Obe.Cantidad).ToString();
            txtDescripcion.Text = Obe.DescripcionRubro;
            txtUnidad.Text = Obe.Medida;
            btnRubro.Text = Obe.CodigoRubro;
            txtCantRequeridad.Text = Obe.rqmd_cantidad_pedida.ToString();
            prdc_icod_producto = Obe.prdc_icod_producto;
            btnRubro.Tag = Obe.hjcd3_icod_rubros_hc;
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
                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese Descripcion");
                }
                if (String.IsNullOrEmpty(txtCantidad.Text))
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingrese Cantidad");
                }
                if (String.IsNullOrEmpty(txtCantRequeridad.Text))
                {
                    oBase = txtCantRequeridad;
                    throw new ArgumentException("Ingrese Cantidad Requeridad");
                }
                if (verificarRubro(Convert.ToInt32(btnRubro.Tag)))
                {
                    oBase = btnRubro;
                    throw new ArgumentException("El Producto ya fue Registrado.");
                }
                Obe.rqmd_vcodigo_item_requerim = txtItem.Text;
                Obe.hjcd3_icod_rubros_hc = Convert.ToInt32(btnRubro.Tag);
                Obe.rqmd_cantidad_pedida = Convert.ToDecimal(txtCantRequeridad.Text);
                Obe.DescripcionRubro = txtDescripcion.Text;
                Obe.Medida = txtUnidad.Text;
                Obe.Cantidad = Convert.ToDecimal(txtCantidad.Text);
                Obe.CodigoRubro = btnRubro.Text;
                Obe.prdc_icod_producto = prdc_icod_producto;
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


        private void btnProducto_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarRubros();
        }
        private void ListarRubros()
        {
            try
            {
                using (frmListarRubros frm = new frmListarRubros())
                {
                    frm.IcodRubros = IcodRubros;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnRubro.Tag = frm._Be.hjcd3_icod_rubros_hc;
                        btnRubro.Text = frm._Be.hjcd3_vcodigo_relacion;
                        txtUnidad.Text = frm._Be.Unidad;
                        txtDescripcion.Text = frm._Be.hjcd3_vdescripcion;
                        txtCantidad.Text = frm._Be.hjcd3_ncantidad.ToString();
                        prdc_icod_producto = frm._Be.prdc_icod_producto;
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtCantRequeridad_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtCantRequeridad.Text) > Convert.ToDecimal(txtCantidad.Text))
            {
                XtraMessageBox.Show("la cantidad no puede ser mayor");
            }
        }
        private bool verificarRubro(int IcosRubros)
        {
            try
            {
                bool exists = false;
                if (lstRequerimientoMaterialesDetalle.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstRequerimientoMaterialesDetalle.Where(x => x.hjcd3_icod_rubros_hc == IcosRubros).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstRequerimientoMaterialesDetalle.Where(x => x.hjcd3_icod_rubros_hc == IcosRubros).ToList().Count > 0)
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

    }
}