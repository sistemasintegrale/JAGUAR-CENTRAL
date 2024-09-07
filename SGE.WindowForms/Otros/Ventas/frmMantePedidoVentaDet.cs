using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Ventas
{
    public partial class frmMantePedidoVentaDet : DevExpress.XtraEditors.XtraForm
    {
        public List<EPedidoClienDet> lstDetalle = new List<EPedidoClienDet>();
        public EPedidoClienDet obe = new EPedidoClienDet();

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        string prdc_vAutor = "";
        string strEditorial = "";

        public frmMantePedidoVentaDet()
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteProducto.Enabled = Enabled;
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

        private void setValues()
        {
            bteProducto.Tag = obe.prdc_icod_producto;
            bteProducto.Text = obe.prdc_vcode_producto;
            txtDescripcion.Text = obe.prdc_vdescripcion_larga;
            lkpmoneda.EditValue = obe.lpedid_icod_moneda;
            txtCantidad.Text = obe.lpedid_nCant_pedido.ToString();
            txtPrecioUni.Text = obe.lpedid_nprecio_uni.ToString();
            txtItem.Text = obe.lpedid_iitem.ToString();
            prdc_vAutor = obe.prdc_vAutor;
            strEditorial = obe.strEditorial;
            txtstock.Text = obe.lpedid_nstock_producto.ToString();
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {

                if (Convert.ToInt32(bteProducto.Tag) == 0)
                {
                    oBase = bteProducto;
                    throw new ArgumentException("Seleccione producto");
                }

                if (Convert.ToDecimal(txtPrecioUni.Text) <= 0)
                {
                    oBase = txtPrecioUni;
                    throw new ArgumentException("El precio unitario debe ser mayor a 0");
                }
                if (Convert.ToDecimal(txtCantidad.Text) <= 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La Cantidad o debe ser mayor a 0");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                    {
                        oBase = bteProducto;
                        throw new ArgumentException("El producto seleccionado ya se encuentra en el detalle del documento");
                    }
                }

                obe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                obe.prdc_vcode_producto = bteProducto.Text;
                obe.prdc_vdescripcion_larga = txtDescripcion.Text;
                obe.lpedid_icod_moneda = Convert.ToInt32(lkpmoneda.EditValue);
                obe.lpedid_vDesc_moneda = lkpmoneda.Text;
                obe.lpedid_nstock_producto = Convert.ToDecimal(txtstock.Text);
                obe.lpedid_nCant_pedido = Convert.ToInt32(txtCantidad.Text);
                obe.lpedid_nprecio_uni = Convert.ToDecimal(txtPrecioUni.Text);
                obe.prdc_vAutor = prdc_vAutor;
                obe.strEditorial = strEditorial;
                obe.intUsuario = Valores.intUsuario;
                obe.lpedid_iitem = Convert.ToInt32(txtItem.Text);
                obe.strPc = "";
                /**/
                obe.lpedid_sflag_estado = true;
                obe.lpedid_nTotal_precio = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecioUni.Text);


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.intTipoOperacion = 1;
                    lstDetalle.Add(obe);
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

        private void listarProducto()
        {
            BaseEdit oBase = null;
            try
            {
                using (frmListarProducto frm = new frmListarProducto())
                {
                    frm.flag_solo_prods = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = frm._Be.prdc_icod_producto;
                        bteProducto.Text = frm._Be.prdc_vcode_producto;
                        txtDescripcion.Text = frm._Be.prdc_vdescripcion_larga;
                        prdc_vAutor = frm._Be.prdc_vAutor;
                        strEditorial = frm._Be.strEditorial;
                        txtstock.Text = frm._Be.prdc_nMonto_stock.ToString();
                    }
                }

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

        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
            txtCantidad.Focus();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
        public void CargarControles()
        {
            BSControls.LoaderLook(lkpmoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda).Where(x => x.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }
        private void frmManteFacCompraDetalle_Load(object sender, EventArgs e)
        {

        }
    }
}