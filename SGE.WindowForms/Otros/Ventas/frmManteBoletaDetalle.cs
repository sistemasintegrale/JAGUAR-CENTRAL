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
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteBoletaDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EBoletaDet> lstFacturaDetalle = new List<EBoletaDet>();
        public EBoletaDet obe = new EBoletaDet();
        public string Categoria, SubCategoria;
        public decimal dblStockDisponible = 0;
        public int intClasificacion = 0;

        public int remic_icod_remision = 0;

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
                bteAlmacen.Enabled = Enabled;
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
        public frmManteBoletaDetalle()
        {
            InitializeComponent();
        }

        private void setValues()
        {
            bteAlmacen.Tag = obe.almac_icod_almacen;
            bteAlmacen.Text = obe.strAlmacen;
            bteProducto.Tag = obe.prdc_icod_producto;
            bteProducto.Text = obe.strCodProducto;
            txtDescuento.Text = obe.bovd_nporcentaje_descuento_item.ToString();
            txtProducto.Text = obe.strDesProducto;
            txtCantidad.Text = obe.bovd_ncantidad.ToString();
            txtUnidadMedida.Text = obe.strDesUM;
            txtPrecio.Text = obe.bovd_nprecio_unitario_item.ToString();
            txtpartNumber.Text = obe.prdc_vpart_number;

            string[] partes = partes = obe.bolvd_vobservaciones.Split('@');
            txtObservaciones.Lines = partes;

        }

        private void frmManteFacturaDetalle_Load(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
                setAlmacen();
        }

        private void setAlmacen()
        {
            var lstAlmacen = new BAlmacen().listarAlmacenes();
            if (lstAlmacen.Count > 0)
            {
                bteAlmacen.Text = lstAlmacen[0].almac_vdescripcion;
                bteAlmacen.Tag = lstAlmacen[0].almac_icod_almacen;
            }
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione almacén");
                }

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

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (lstFacturaDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                    {
                        oBase = bteProducto;
                        throw new ArgumentException("El producto seleccionado ya se encuentra en el detalle de la factura");
                    }
                }
                if (remic_icod_remision == 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (Convert.ToDecimal(txtCantidad.Text) > dblStockDisponible)
                        {
                            oBase = txtCantidad;
                            throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                        }
                    }
                    else
                    {
                        if (obe.intTipoOperacion != 1)
                        {
                            dblStockDisponible = dblStockDisponible + obe.bovd_ncantidad;
                            if (Convert.ToDecimal(txtCantidad.Text) > dblStockDisponible)
                            {
                                oBase = txtCantidad;
                                throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                            }
                        }
                        else
                            if (Convert.ToDecimal(txtCantidad.Text) > dblStockDisponible)
                        {
                            oBase = txtCantidad;
                            throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                        }
                    }
                }

                obe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                obe.bovd_iitem_boleta = Convert.ToInt32(txtItem.Text);
                obe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                obe.bovd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.bovd_vdescripcion = txtProducto.Text;
                obe.bovd_nprecio_unitario_item = Convert.ToDecimal(txtPrecio.Text);
                obe.bovd_nporcentaje_descuento_item = Convert.ToDecimal(txtDescuento.Text);
                obe.bovd_nprecio_total_item = (obe.bovd_ncantidad * obe.bovd_nprecio_unitario_item);
                obe.bovd_nprecio_total_item = obe.bovd_nprecio_total_item - (obe.bovd_nprecio_total_item * Convert.ToDecimal(String.Format("0.{0}", txtDescuento.Text.Replace(".", ""))));
                obe.bovd_nmonto_impuesto_item = obe.bovd_nprecio_total_item * Convert.ToDecimal(String.Format("0.{0}", Parametros.strPorcIGV.Replace(".", "")));
                /**/
                obe.strCodProducto = bteProducto.Text;
                obe.strDesUM = txtUnidadMedida.Text;
                obe.strDesProducto = txtProducto.Text;
                obe.strAlmacen = bteAlmacen.Text;
                obe.dblStockDisponible = dblStockDisponible;
                obe.dblMontoDescuento = (obe.bovd_ncantidad * obe.bovd_nprecio_unitario_item) - obe.bovd_nprecio_total_item;
                obe.strMoneda = txtMoneda.Text;
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;
                obe.prdc_vpart_number = txtpartNumber.Text;


                string Descripci = "";
                string DescripciExtra = "";
                string[] arraye = txtObservaciones.Lines;
                for (int i = 0; i < arraye.Length; i++)
                {
                    Descripci = Descripci + arraye[i] + "@";
                    if (arraye[i] != "")
                        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                }


                obe.bolvd_vobservaciones = Descripci;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.strCategoria = Categoria;
                    obe.strSubCategoriaUno = SubCategoria;
                    obe.intTipoOperacion = 1;
                    lstFacturaDetalle.Add(obe);
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

        private void listarAlmacen()
        {
            using (frmListarAlmacen frm = new frmListarAlmacen())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = frm._Be.almac_icod_almacen;
                    bteAlmacen.Text = frm._Be.almac_vdescripcion;
                }
            }
        }

        private void listarProducto()
        {
            BaseEdit oBase = null;
            try
            {
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione almacén");
                }
                using (frmListarStockPorAlmacen frm = new frmListarStockPorAlmacen())
                {
                    frm.intAlmacen = Convert.ToInt32(bteAlmacen.Tag);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = frm._Be.prdc_icod_producto;
                        bteProducto.Text = frm._Be.strCodProducto;
                        txtProducto.Text = frm._Be.strDesProducto;
                        txtUnidadMedida.Text = frm._Be.strDesUM;
                        Categoria = frm._Be.strCategoria;
                        SubCategoria = frm._Be.strSubCategoriaUno;
                        dblStockDisponible = frm._Be.stocc_stock_producto;
                        txtPrecio.Text = Convert.ToDecimal(frm._Be.dblPrecioVenta).ToString();
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

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }
    }
}