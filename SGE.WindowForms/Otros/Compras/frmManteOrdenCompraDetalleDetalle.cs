using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Operaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteOrdenCompraDetalleDetalle : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public List<EOrdenCompra> oDetail = new List<EOrdenCompra>();
        public List<EArchivos> lstArchivos = new List<EArchivos>();

        public EOrdenCompra oBE;
        public BSMaintenanceStatus oState;
        public int ococ_icod_orden_compra = 0;
        public int ocod_icod_detalle_oc = 0;
        public int Correlativo = 0;
        public long intIdKardex = 0;
        public int IntMoneda;

        decimal Monto_soles;
        decimal Monto_dolares;



        public int funcion;//0:insertar:1:modificar

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
            btnProducto.Enabled = !Enabled;
            txtCantidad.Enabled = !Enabled;
            btnProducto.Focus();
        }

        private void clearControl()
        {
            txtItem.EditValue = Correlativo;
            txtUM.Text = "";
            btnProducto.Text = "";
        }
        public void setValues()
        {
            txtItem.EditValue = oBE.ocod_iitem;
            txtUM.Text = oBE.strMedida;
            btnProducto.Tag = oBE.prdc_icod_producto;
            btnProducto.Text = oBE.strCodigoProducto;
            txtDescripcion.Text = oBE.strDescProducto;
            txtCantidad.Text = oBE.ocod_ncantidad.ToString();
            txtPUnitario.Text = oBE.ocod_ncunitario.ToString();
            txtDescripcionOCDetalle.Text = oBE.ocod_vdescripcion;
            txtCaracteristica.Text = oBE.ocod_vcaracteristicas;
            txtDescuento.Text = oBE.ocod_ndescuento_item.ToString();
            txtCodFabricante.Text = oBE.prdc_vcodigo_fabricante;
            txtPVenta.Text = oBE.ocod_nmonto_total.ToString();
            lkpMoneda.EditValue = oBE.tablc_iid_tipo_moneda;

        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public frmManteOrdenCompraDetalleDetalle()
        {
            InitializeComponent();
        }

        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.ListarProductoEspecifico();
        }
        private void ListarProductoEspecifico()
        {
            try
            {
                frmListarProducto Producto = new frmListarProducto();
                if (Producto.ShowDialog() == DialogResult.OK)
                {
                    btnProducto.Tag = Producto._Be.prdc_icod_producto;
                    btnProducto.Text = Producto._Be.prdc_vcode_producto.ToString();
                    txtDescripcion.Text = Producto._Be.prdc_vdescripcion_larga;
                    txtUM.Text = Producto._Be.StrUnidadMedida;
                    txtDescripcionOCDetalle.Text = Producto._Be.prdc_vdescripcion_larga;
                    txtCodFabricante.Text = Producto._Be.prdc_vcodigo_fabricante;
                    txtCaracteristica.Text = Producto._Be.prdc_vcaracteristica;
                    //if(IntMoneda==Parametros.intSoles)
                    //txtPUnitario.Text = Producto._Be.prdc_nmonto_precio_venta.ToString();
                    //else
                    //  txtPUnitario.Text = Producto._Be.prdc_nmonto_precio_venta_dolar.ToString();

                    //Monto_soles = Convert.ToDecimal(Producto._Be.prdc_nmonto_precio_venta);
                    //Monto_dolares = Convert.ToDecimal(Producto._Be.prdc_nmonto_precio_venta_dolar);
                }
                //txtPUnitario.Focus();
                txtCantidad.Text = "1";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AgregarDetalle();
        }
        private void AgregarDetalle()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            oBE = new EOrdenCompra();
            try
            {
                if (string.IsNullOrEmpty(btnProducto.Text))
                {
                    oBase = btnProducto;
                    throw new ArgumentException("Seleccionar un producto.");
                }

                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La cantidad no puede ser 0.");
                }
                var BuscarDocumento = oDetail.Where(oB => oB.prdc_icod_producto == Convert.ToInt32(btnProducto.Tag)).ToList();
                if (BuscarDocumento.Count > 0)
                {
                    oBase = btnProducto;
                    throw new ArgumentException("El producto ya existe en la Orden de Compra");
                }
                if (Convert.ToDecimal(txtPUnitario.Text) == 0)
                {
                    oBase = txtPUnitario;
                    throw new ArgumentException("Ingrese Precio Unitario");
                }
                oBE.ocod_icod_detalle_oc = ocod_icod_detalle_oc;
                oBE.ococ_icod_orden_compra = ococ_icod_orden_compra;
                oBE.ocod_iitem = Convert.ToInt32(txtItem.EditValue);
                oBE.prdc_icod_producto = Convert.ToInt32(btnProducto.Tag);
                oBE.prdc_vcodigo_fabricante = txtCodFabricante.Text;
                oBE.ocod_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                oBE.ocod_ncunitario = Convert.ToDecimal(txtPUnitario.Text);
                oBE.ocod_ndescuento_item = Convert.ToDecimal(txtDescuento.Text);
                oBE.ocod_nmonto_total = Convert.ToDecimal(txtPVenta.Text);
                oBE.ocod_vcaracteristicas = txtCaracteristica.Text;
                oBE.ocod_vdescripcion = txtDescripcionOCDetalle.Text;
                oBE.ocod_ncantidad_saldo = oBE.ocod_ncantidad - oBE.ocod_ncantidad_recibida;
                oBE.kardc_iid_correlativo = 0;
                oBE.intUsuario = Valores.intUsuario;
                oBE.strPc = WindowsIdentity.GetCurrent().Name;

                oBE.orpdi_nprecio_soles = Monto_soles;
                oBE.orpdi_nprecio_dolares = Monto_dolares;

                oBE.strCodigoProducto = btnProducto.Text;
                oBE.strDescProducto = txtDescripcion.Text;
                oBE.strMedida = txtUM.Text;
                oBE.ocod_flag_estado = true;
                if (lstArchivos.Count > 0)
                {
                    oBE.ocod_vdireccion_documento = lstArchivos[0].arch_vruta;
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
                    XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Flag = false;
                }
            }
            finally
            {
                if (Flag)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                        Status = BSMaintenanceStatus.View;
                    else
                        Status = BSMaintenanceStatus.View;

                    Status = BSMaintenanceStatus.View;
                    this.Close();
                }
            }
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ModificarDetalle();
        }
        private void ModificarDetalle()
        {
            {
                {
                    BaseEdit oBase = null;
                    Boolean Flag = true;
                    oBE = new EOrdenCompra();
                    try
                    {
                        if (Convert.ToDecimal(txtCantidad.Text) == 0)
                        {
                            oBase = txtCantidad;
                            throw new ArgumentException("La cantidad no puede ser 0.");
                        }
                        if (Convert.ToDecimal(txtPUnitario.Text) == 0)
                        {
                            oBase = txtCantidad;
                            throw new ArgumentException("El precio Unitario no puede ser 0.");
                        }

                        oBE.ocod_iitem = Convert.ToInt32(txtItem.EditValue);
                        oBE.ocod_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                        oBE.ocod_ncunitario = Convert.ToDecimal(txtPUnitario.Text);
                        oBE.ocod_nmonto_total = Convert.ToDecimal(txtPVenta.Text);
                        oBE.ocod_ndescuento_item = Convert.ToDecimal(txtDescuento.Text);
                        oBE.ocod_vcaracteristicas = txtCaracteristica.Text;
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        if (oBase != null)
                        {
                            oBase.Focus();

                            oBase.ErrorText = ex.Message;
                            oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                            XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Flag = false;
                        }
                    }
                    finally
                    {
                        if (Flag)
                        {
                            if (Status == BSMaintenanceStatus.CreateNew)
                                Status = BSMaintenanceStatus.View;
                            else
                                Status = BSMaintenanceStatus.View;

                            Status = BSMaintenanceStatus.View;
                            this.Close();
                        }
                    }
                }
            }
        }

        private void frmManteOrdenCompraDetalleDetalle_Load(object sender, EventArgs e)
        {

            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            lkpMoneda.EditValue = IntMoneda;
        }

        private void txtPUnitario_EditValueChanged(object sender, EventArgs e)
        {

            Totalizar();
        }

        private void Totalizar()
        {
            decimal ddescuento = 0;
            ddescuento = ((Convert.ToDecimal(txtPUnitario.Text) * Convert.ToDecimal(txtDescuento.Text)) / 100);
            txtPVenta.Text = (Math.Round((Convert.ToDecimal(txtPUnitario.Text) - ddescuento) * Convert.ToDecimal(txtCantidad.Text), 2)).ToString();
        }

        private void txtDescuento_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }
        private void Cantidad()
        {
            txtPVenta.Text = Math.Round((Convert.ToDecimal(txtPUnitario.Text) * Convert.ToDecimal(txtCantidad.Text)), 4).ToString();
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {

            Totalizar();
        }

        private void btnVerDoc_Click(object sender, EventArgs e)
        {
            frm01Archivos frmdetalle = new frm01Archivos();
            frmdetalle.ocod_icod_detalle_oc = ocod_icod_detalle_oc;
            frmdetalle.lstArchivos = lstArchivos;
            frmdetalle.returnSeleccion();
            if (frmdetalle.ShowDialog() == DialogResult.OK)
            {
                lstArchivos = frmdetalle.lstArchivos;
            }
        }

    }
}