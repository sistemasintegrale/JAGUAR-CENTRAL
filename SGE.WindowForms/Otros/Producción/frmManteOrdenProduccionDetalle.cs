using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Producción
{
    public partial class frmManteOrdenProduccionDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EOrdenProduccionDet> lstOrdenProduccionDetalle = new List<EOrdenProduccionDet>();
        public EOrdenProduccionDet obe = new EOrdenProduccionDet();
        public string Categoria, SubCategoria;
        public decimal dblStockDisponible = 0;
        public int tipo_moneda = 0;
        public int remic_icod_remision = 0;
        public Boolean afecto;
        public string CodigoSUNAT, strDesUM;
        public int unidc_icod_unidad_medida;

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;


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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

            }
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
        public frmManteOrdenProduccionDetalle()
        {
            InitializeComponent();
        }

        private void setValues()
        {

            lkpArea.EditValue = obe.orprd_iproceso;
            lkpArea.Text = obe.strproceso;
            lkpUbicacion.EditValue = obe.orprd_iubicacion;
            lkpUbicacion.Text = obe.orprd_vubicacion;
            bteProducto.Tag = obe.prdc_icod_producto;
            bteProducto.Text = obe.strCodeProducto;
            txtDescProd.Text = obe.strProducto;
            txtUM.Text = obe.strmedida;
            txtDescripcion.Text = obe.orprd_vmaterial;
            txtCantidad.Text = obe.orprd_ncantidad.ToString();

            lkpArea.Enabled = false;
            lkpUbicacion.Enabled = false;
            bteProducto.Enabled = false;
            txtDescProd.Enabled = false;
            txtUM.Enabled = false;
            txtDescripcion.Enabled = false;
            txtCantidad.Enabled = false;
        }
        public void CargarCombos()
        {

            BSControls.LoaderLook(lkpArea, new BCentral().listarAreaProduccion(), "arprc_vdescripcion", "arprc_iid_codigo", true);
            EAreaUbicacion ob = new EAreaUbicacion();
            ob.arprc_iid_codigo = Convert.ToInt32(lkpArea.EditValue);
            BSControls.LoaderLook(lkpUbicacion, new BCentral().ListarAreaUbicacion(ob), "descripcion", "arubd_iid_area_ubicacion", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpArea.EditValue = obe.orprd_iproceso;
                lkpUbicacion.EditValue = obe.orprd_iubicacion;
            }

        }
        private void frmManteFacturaDetalle_Load(object sender, EventArgs e)
        {
            CargarCombos();
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpArea.EditValue = obe.orprd_iproceso;

            }



        }


        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {

                obe.orprd_iitem_orden_produccion = Convert.ToInt32(txtitem.Text);
                obe.orprd_iproceso = Convert.ToInt32(lkpArea.EditValue);
                obe.strproceso = lkpArea.Text;
                obe.orprd_iubicacion = Convert.ToInt32(lkpUbicacion.EditValue);
                obe.orprd_vubicacion = lkpUbicacion.Text;
                obe.orprd_vmaterial = txtDescripcion.Text;
                obe.orprd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                obe.strCodeProducto = bteProducto.Text;
                obe.strProducto = txtDescProd.Text;
                obe.strmedida = txtUM.Text;
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (obe.orprd_icod_item_orden_produccion == 0)
                {
                    obe.intTipoOperacion = 1;

                }
                else
                {
                    if (obe.intTipoOperacion != 1)
                        obe.intTipoOperacion = 2;
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }


        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }





        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void groupControl1_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtprecio_EditValueChanged(object sender, EventArgs e)
        {
        }


        private void txtDescuento_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void bteProducto_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtItem_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void lkpArea_EditValueChanged(object sender, EventArgs e)
        {
            EAreaUbicacion ob = new EAreaUbicacion();
            ob.arprc_iid_codigo = Convert.ToInt32(lkpArea.EditValue);
            BSControls.LoaderLook(lkpUbicacion, new BCentral().ListarAreaUbicacion(ob), "descripcion", "arubd_iid_area_ubicacion", true);
        }

        private void lkpArea_EditValueChanged_1(object sender, EventArgs e)
        {
            EAreaUbicacion ob = new EAreaUbicacion();
            ob.arprc_iid_codigo = Convert.ToInt32(lkpArea.EditValue);
            BSControls.LoaderLook(lkpUbicacion, new BCentral().ListarAreaUbicacion(ob), "descripcion", "arubd_iid_area_ubicacion", true);
        }

        private void bteProducto_Click(object sender, EventArgs e)
        {
            listarProducto();
        }
        private void listarProducto()
        {
            try
            {
                frmListarProducto frm = new frmListarProducto();
                //frm.flag_solo_prods = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteProducto.Tag = frm._Be.prdc_icod_producto;
                    bteProducto.Text = frm._Be.prdc_vcode_producto;
                    txtDescProd.Text = frm._Be.prdc_vdescripcion_larga;
                    txtUM.Text = frm._Be.StrUnidadMedida;
                }
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void bteAlmacen_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lkpTipoVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }


    }
}