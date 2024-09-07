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
    public partial class frmManteOrdenCompraImportacionDetalle : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public List<EOrdenCompraImportacion> oDetail = new List<EOrdenCompraImportacion>();
        public List<EArchivos> lstArchivos = new List<EArchivos>();

        public EOrdenCompraImportacion oBE;
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
            txtItem.EditValue = oBE.ocid_iitem;
            txtUM.Text = oBE.strMedida;
            btnProducto.Tag = oBE.prdc_icod_producto;
            btnProducto.Text = oBE.strCodigoProducto;
            txtDescripcion.Text = oBE.strDescProducto;
            txtCantidad.Text = oBE.ocid_ncantidad.ToString();
            txtPUnitario.Text = oBE.ocid_ncunitario.ToString();
            txtDescripcionOCDetalle.Text = oBE.ocid_vdescripcion;
            txtCaracteristica.Text = oBE.ocid_vcaracteristicas;
            txtDescuento.Text = oBE.ocid_ndescuento_item.ToString();
            txtCodFabricante.Text = oBE.prdc_vcodigo_fabricante;
            txtPVenta.Text = oBE.ocid_nmonto_total.ToString();
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

        public frmManteOrdenCompraImportacionDetalle()
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
                txtPUnitario.Focus();
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
            oBE = new EOrdenCompraImportacion();
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
                oBE.ocid_icod_detalle_oci = ocod_icod_detalle_oc;
                oBE.ocic_icod_oci = ococ_icod_orden_compra;
                oBE.ocid_iitem = Convert.ToInt32(txtItem.EditValue);
                oBE.prdc_icod_producto = Convert.ToInt32(btnProducto.Tag);
                oBE.prdc_vcodigo_fabricante = txtCodFabricante.Text;
                oBE.ocid_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                oBE.ocid_ncunitario = Convert.ToDecimal(txtPUnitario.Text);
                oBE.ocid_ndescuento_item = Convert.ToDecimal(txtDescuento.Text);
                oBE.ocid_nmonto_total = Convert.ToDecimal(txtPVenta.Text);
                oBE.ocid_vcaracteristicas = txtCaracteristica.Text;
                oBE.ocid_vdescripcion = txtDescripcionOCDetalle.Text;
                oBE.intUsuario = Valores.intUsuario;
                oBE.strPc = WindowsIdentity.GetCurrent().Name;

                oBE.orpdi_nprecio_soles = Monto_soles;
                oBE.orpdi_nprecio_dolares = Monto_dolares;

                oBE.strCodigoProducto = btnProducto.Text;
                oBE.strDescProducto = txtDescripcion.Text;
                oBE.strMedida = txtUM.Text;
                oBE.ocid_flag_estado = true;

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
                    oBE = new EOrdenCompraImportacion();
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

                        oBE.ocid_iitem = Convert.ToInt32(txtItem.EditValue);
                        oBE.ocid_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                        oBE.ocid_ncunitario = Convert.ToDecimal(txtPUnitario.Text);
                        oBE.ocid_nmonto_total = oBE.ocid_ncantidad * oBE.ocid_ncunitario;
                        oBE.ocid_vcaracteristicas = txtCaracteristica.Text;
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
            dtmFechaEntrega.EditValue = DateTime.Now;
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
            txtPVenta.Text = ((Convert.ToDecimal(txtPUnitario.Text) - ddescuento) * Convert.ToDecimal(txtCantidad.Text)).ToString();
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
            lstArchivos = frmdetalle.lstArchivos;
            frmdetalle.returnSeleccion();
            if (frmdetalle.ShowDialog() == DialogResult.OK)
            {
                lstArchivos = frmdetalle.lstArchivos;
            }
        }

    }
}