using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteFacCompraDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EFacturaCompraDet> lstDetalle = new List<EFacturaCompraDet>();
        public EFacturaCompraDet obe = new EFacturaCompraDet();
        public string strLinea, strSubLinea;
        public int intClasificacion = 0;
        public int intAlmacen = 1;
        //public int CodOC = 0;
        public int ocod_icod_detalle_oc = 0;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        //public int OCL = 0;
        public int tipo_Producto;
        public int CodGR = 0;
        public int GR = 0;
        public frmManteFacCompraDetalle()
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
            bteProducto.Tag = obe.prd_icod_producto;
            bteProducto.Text = obe.strCodProducto;
            txtDescripcion.Text = obe.fcod_vdescripcion_item;
            txtCantidad.Text = obe.fcod_ncantidad.ToString();
            txtUnidadMedida.Text = obe.strDesUM;
            txtPrecio.Text = obe.fcod_nmonto_unit.ToString();
            strLinea = obe.strLinea;
            strSubLinea = obe.strSubLinea;
            txtDescuento.Text = obe.fcod_ndescuento.ToString();


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

                if (Convert.ToDecimal(txtCantidad.Text) <= 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La cantidad debe ser mayor a 0");
                }

                if (Convert.ToDecimal(txtPrecio.Text) <= 0)
                {
                    oBase = txtPrecio;
                    throw new ArgumentException("El precio unitario debe ser mayor a 0");
                }

                //if (Status == BSMaintenanceStatus.CreateNew)
                //{
                //    if (lstDetalle.Where(x => x.prd_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                //    {
                //        oBase = bteProducto;
                //        throw new ArgumentException("El producto seleccionado ya se encuentra en el detalle del documento");
                //    }
                //}

                obe.fcod_iitem = Convert.ToInt32(txtItem.Text);
                obe.prd_icod_producto = Convert.ToInt32(bteProducto.Tag);
                obe.fcod_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.fcod_vdescripcion_item = txtDescripcion.Text;
                obe.fcod_nmonto_unit = Convert.ToDecimal(txtPrecio.Text);
                obe.fcod_ndescuento = Convert.ToDecimal(txtDescuento.Text);
                obe.fcod_nmonto_total = Convert.ToDecimal(txtPVenta.Text);

                obe.ocod_icod_detalle_oc = ocod_icod_detalle_oc;
                /**/
                obe.strCodProducto = bteProducto.Text;
                obe.strDesUM = txtUnidadMedida.Text;
                obe.strMoneda = txtMoneda.Text;
                obe.strLinea = strLinea;
                obe.strSubLinea = strSubLinea;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.strLinea = strLinea;
                    obe.strSubLinea = strSubLinea;
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
                        txtUnidadMedida.Text = frm._Be.StrUnidadMedida;
                        strLinea = frm._Be.strCategoria;
                        strSubLinea = frm._Be.strSubCategoriaUno;

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
            if (tipo_Producto == 1)
            {


                //if (OCL == 1)
                //{
                //    ListarOCL();
                //}
                if (GR == 1)
                {
                    ListarOCL();
                }
                else
                {
                    listarProducto();
                }
                txtCantidad.Focus();
            }
            else if (tipo_Producto == 2)
            {

            }
        }
        private void ListarOCL()
        {
            try
            {
                using (frmListarOCLDetalle frm = new frmListarOCLDetalle())
                {
                    //frm.CodOC =CodOC;
                    frm.CodGR = CodGR;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = frm._Be.prdc_icod_producto;
                        bteProducto.Text = frm._Be.strCodigoProducto;
                        txtCantidad.Text = frm._Be.ocod_ncantidad_saldo.ToString();
                        txtPrecio.Text = frm._Be.ocod_ncunitario.ToString();
                        txtDescripcion.Text = frm._Be.strDescProducto;
                        txtUnidadMedida.Text = frm._Be.strAbrevUniMed;
                        ocod_icod_detalle_oc = frm._Be.ocod_icod_detalle_oc;
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void frmManteFacCompraDetalle_Load(object sender, EventArgs e)
        {

        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void txtPrecio_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }
        private void Totalizar()
        {
            decimal ddescuento = 0;
            ddescuento = ((Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(txtDescuento.Text)) / 100);
            txtPVenta.Text = (Math.Round((Convert.ToDecimal(txtPrecio.Text) - ddescuento) * Convert.ToDecimal(txtCantidad.Text), 2)).ToString();
        }

        private void txtDescuento_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

    }
}