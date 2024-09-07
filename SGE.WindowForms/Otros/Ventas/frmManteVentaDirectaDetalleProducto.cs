using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Operaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteVentaDirectaDetalleProducto : DevExpress.XtraEditors.XtraForm
    {
        public List<EVentaDirectaDet> lstDetalle = new List<EVentaDirectaDet>();
        public EVentaDirectaDet obe = new EVentaDirectaDet();
        public string Categoria, SubCategoria;
        public decimal dblStockDisponible = 0;

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
        public frmManteVentaDirectaDetalleProducto()
        {
            InitializeComponent();
        }

        private void setValues()
        {
            bteAlmacen.Tag = obe.dvdd_iid_almacen;
            bteAlmacen.Text = obe.strAlmacen;
            bteProducto.Tag = obe.dvdd_iid_producto;
            bteProducto.Text = obe.strCodProducto;
            txtProducto.Text = obe.strDesProducto;
            txtCantidad.Text = obe.dvdd_ncantidad.ToString();
            txtUnidadMedida.Text = obe.strDesUM;
            txtPrecio.Text = obe.dvdd_nprecio_unitario_item.ToString();
            txtTotal.Text = obe.dvdd_nprecio_total_item.ToString();

            txtProductividad.Text = obe.dvdd_nmonto_productividad.ToString();
            btePersonal.Tag = obe.dvdd_icod_personal;
            btePersonal.Text = obe.strPersonal;
            txtAreaPersonal.Text = obe.dvdd_area_personal;
            dtFecha.EditValue = obe.dvdd_fecha_servicio;
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
                    if (lstDetalle.Where(x => x.dvdd_iid_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                    {
                        oBase = bteProducto;
                        throw new ArgumentException("El producto seleccionado ya se encuentra en el detalle de la venta directa");
                    }
                }
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
                        dblStockDisponible = dblStockDisponible + obe.dvdd_ncantidad;
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

                if (Convert.ToDecimal(txtProductividad.Text) > 0)
                {
                    if (Convert.ToInt32(btePersonal.Tag) == 0)
                    {
                        oBase = btePersonal;
                        throw new ArgumentException("Seleccione la persona encargada que realiza el servicio");
                    }
                }

                obe.dvdd_iitem_doc_venta_directa = Convert.ToInt32(txtItem.Text);
                obe.dvdd_iid_almacen = Convert.ToInt32(bteAlmacen.Tag);
                obe.dvdd_iid_producto = Convert.ToInt32(bteProducto.Tag);
                obe.dvdd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.dvdd_vdescripcion = txtProducto.Text;
                obe.dvdd_bbonificacion = cbBonificacion.Checked;
                obe.dvdd_nprecio_unitario_item = Convert.ToDecimal(txtPrecio.Text);
                obe.dvdd_nprecio_total_item = (obe.dvdd_ncantidad * obe.dvdd_nprecio_unitario_item);
                obe.dvdd_nmonto_impuesto_item = obe.dvdd_nprecio_total_item - Math.Round((obe.dvdd_nprecio_total_item / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")))), 2);
                /**/
                obe.strPersonal = btePersonal.Text;
                obe.dvdd_icod_personal = Convert.ToInt32(btePersonal.Tag);
                obe.dvdd_area_personal = txtAreaPersonal.Text;
                obe.dvdd_nmonto_productividad = Convert.ToDecimal(txtProductividad.Text);
                obe.dvdd_clasificacion = 1;// clasificación 1 :: productos
                obe.dvdd_fecha_servicio = Convert.ToDateTime(dtFecha.EditValue);
                /**/
                obe.strCodProducto = bteProducto.Text;
                obe.strDesUM = txtUnidadMedida.Text;
                obe.strDesProducto = txtProducto.Text;
                obe.strAlmacen = bteAlmacen.Text;
                obe.dblStockDisponible = dblStockDisponible;
                obe.strMoneda = txtMoneda.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.strCategoria = Categoria;
                    obe.strSubCategoriaUno = SubCategoria;
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

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            txtTotal.Text = (Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text)).ToString();
        }

        private void txtPrecio_EditValueChanged(object sender, EventArgs e)
        {
            txtTotal.Text = (Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text)).ToString();
        }

        private void btePersonal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarPersonal();
        }

        private void listarPersonal()
        {
            frmListarPersonal frm = new frmListarPersonal();
            frm.flag_personal_all = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btePersonal.Tag = frm._Be.perc_icod_personal;
                btePersonal.Text = frm._Be.perc_vapellidos_nombres;
                txtAreaPersonal.Text = frm._Be.strArea;
            }
        }

        private void btePersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarPersonal();
        }
    }
}