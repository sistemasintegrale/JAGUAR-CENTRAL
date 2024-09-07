using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteFacturaDetalleServicio : DevExpress.XtraEditors.XtraForm
    {
        public List<EFacturaDet> lstFacturaDetalle = new List<EFacturaDet>();
        public EFacturaDet obe = new EFacturaDet();
        public string Categoria, SubCategoria;
        public decimal dblStockDisponible = 0;
        List<EStock> lstStockProductos = new List<EStock>();
        public int tipo_moneda = 0;
        public int remic_icod_remision = 0;
        public Boolean afecto;

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
                //bteAlmacen.Enabled = Enabled;
                //bteProducto.Enabled = Enabled;
                //lkpTipoVenta.Enabled = Enabled;
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
        public frmManteFacturaDetalleServicio()
        {
            InitializeComponent();
        }

        private void setValues()
        {

            //bteAlmacen.Tag = obe.almac_icod_almacen;
            //bteAlmacen.Text = obe.strAlmacen;
            //bteProducto.Tag = obe.prdc_icod_producto;
            //bteProducto.Text = obe.strCodProducto;
            txtProducto.Text = obe.favd_vdescripcion;
            txtCantidad.Text = obe.favd_ncantidad.ToString();
            txtUnidadMedida.Text = obe.strDesUM;
            //txtpartNumber.Text = obe.prdc_vpart_number;

            //lkpTipoVenta.Text = obe.tablc_iid_tipo_venta.ToString();


            txtprecio.Text = obe.favd_nmonto_impuesto_item.ToString();
            txtDescuento.Text = obe.favd_nporcentaje_descuento_item.ToString();
            txtPrecioVenta.Text = obe.favd_nprecio_unitario_item.ToString();
            txtCaracteristicas.Text = obe.favd_vcaracteristicas;


            //string[] partes = partes = obe.favd_nobservaciones.Split('@');
            //txtObservaciones.Lines = partes;



        }
        public void CargarCombos()
        {

            int index = new BGeneral().listarTablaRegistro(Parametros.intTipoventa).FindIndex(x => x.tarec_iid_tabla_registro == 266);
            //BSControls.LoaderLook(lkpTipoVenta, new BGeneral().listarTablaRegistro(Parametros.intTipoventa).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            //lkpTipoVenta.ItemIndex = index;


        }
        private void frmManteFacturaDetalle_Load(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
                setAlmacen();


            //lstStockProductos = new BAlmacen().listarStockPorAlmacenOptimizado(Parametros.intEjercicio, Convert.ToInt32(bteAlmacen.Tag), "", "");

            //CargarCombos();

        }

        private void setAlmacen()
        {
            //var lstAlmacen = new BAlmacen().listarAlmacenes();
            //if (lstAlmacen.Count > 0)
            //{
            //    bteAlmacen.Text = lstAlmacen[0].almac_vdescripcion;
            //    bteAlmacen.Tag = lstAlmacen[0].almac_icod_almacen;
            //}

        }

        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {
                //if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                //{
                //    oBase = bteAlmacen;
                //    throw new ArgumentException("Seleccione almacén");
                //}

                //if (Convert.ToInt32(bteProducto.Tag) == 0)
                //{
                //    oBase = bteProducto;
                //    throw new ArgumentException("Seleccione producto");
                //}

                if (Convert.ToDecimal(txtCantidad.Text) <= 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La cantidad debe ser mayor a 0");
                }

                if (Convert.ToDecimal(txtPrecioVenta.Text) <= 0)
                {
                    oBase = txtPrecioVenta;
                    throw new ArgumentException("El precio unitario debe ser mayor a 0");
                }

                //if (Status == BSMaintenanceStatus.CreateNew)
                //{
                //    if (lstFacturaDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                //    {
                //        oBase = bteProducto;
                //        throw new ArgumentException("El producto seleccionado ya se encuentra en el detalle de la factura");
                //    }
                //}

                //-----
                //if (remic_icod_remision == 0)
                //{
                //    if (Status == BSMaintenanceStatus.CreateNew)
                //    {
                //if (Convert.ToDecimal(txtCantidad.Text) > dblStockDisponible)
                //{
                //    oBase = txtCantidad;
                //    throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                //}
                //}
                //else
                //{
                //    if (obe.intTipoOperacion != 1)
                //    {
                //dblStockDisponible = dblStockDisponible + obe.favd_ncantidad;
                //if (Convert.ToDecimal(txtCantidad.Text) > dblStockDisponible)
                //{
                //    oBase = txtCantidad;
                //    throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                //}
                //}
                //else
                //    if (Convert.ToDecimal(txtCantidad.Text) > dblStockDisponible)
                //    {
                //oBase = txtCantidad;
                //throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                //            }
                //    }
                //}

                //obe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                obe.favd_iitem_factura = Convert.ToInt32(txtItem.Text);
                //obe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                obe.favd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.favd_vdescripcion = txtProducto.Text;
                //obe.strTipoVenta = lkpTipoVenta.Text;
                //obe.tablc_iid_tipo_venta = Convert.ToInt32(lkpTipoVenta.EditValue);




                obe.favd_nmonto_impuesto_item = Convert.ToDecimal(txtprecio.Text);
                obe.favd_nporcentaje_descuento_item = Convert.ToDecimal(txtDescuento.Text);

                obe.favd_nprecio_unitario_item = Convert.ToDecimal(txtPrecioVenta.Text);


                obe.favd_nprecio_total_item = Convert.ToDecimal(obe.favd_nprecio_unitario_item * Convert.ToDecimal(txtCantidad.Text));
                /**/
                //obe.strCodProducto = bteProducto.Text;              
                obe.strDesUM = txtUnidadMedida.Text;
                obe.strDesProducto = txtProducto.Text;
                //obe.strAlmacen = bteAlmacen.Text;
                obe.dblStockDisponible = dblStockDisponible;
                obe.dblMontoDescuento = (obe.favd_ncantidad * obe.favd_nprecio_unitario_item) - obe.favd_nprecio_total_item;
                obe.strMoneda = txtMoneda.Text;
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;
                obe.flagPlanilla = true;
                //obe.prdc_vpart_number = txtpartNumber.Text;
                obe.favd_vcaracteristicas = txtCaracteristicas.Text;


                string Descripci = "";
                string DescripciExtra = "";
                string[] arraye = txtCaracteristicas.Lines;
                for (int i = 0; i < arraye.Length; i++)
                {
                    Descripci = Descripci + arraye[i] + "@";
                    if (arraye[i] != "")
                        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                }


                obe.favd_nobservaciones = Descripci;

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
            //using (frmListarAlmacen frm = new frmListarAlmacen())
            //{
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        bteAlmacen.Tag = frm._Be.almac_icod_almacen;
            //        bteAlmacen.Text = frm._Be.almac_vdescripcion;
            //        lstStockProductos = new BAlmacen().listarStockPorAlmacenOptimizado(Parametros.intEjercicio, Convert.ToInt32(bteAlmacen.Tag), "", "");
            //    }
            //}
        }

        private void listarProducto()
        {

            //BaseEdit oBase = null;
            //try 
            //{
            //    if (Convert.ToInt32(bteAlmacen.Tag) == 0)
            //    {
            //        oBase = bteAlmacen;
            //        throw new ArgumentException("Seleccione almacén");
            //    }
            //    using (frmListarStockPorAlmacen frm = new frmListarStockPorAlmacen())
            //    {
            //        frm.intAlmacen = Convert.ToInt32(bteAlmacen.Tag);
            //        frm.afecto = afecto;
            //        if (frm.ShowDialog() == DialogResult.OK)
            //        {
            //            bteProducto.Tag = frm._Be.prdc_icod_producto;
            //            bteProducto.Text = frm._Be.strCodProducto;
            //            txtProducto.Text = frm._Be.strDesProducto;
            //            txtUnidadMedida.Text = frm._Be.strDesUM;
            //            Categoria = frm._Be.strCategoria;
            //            SubCategoria = frm._Be.strSubCategoriaUno;
            //            dblStockDisponible = frm._Be.stocc_stock_producto;
            //            txtPrecioVenta.Text = Convert.ToDecimal(frm._Be.dblPrecioVenta).ToString();
            //            if (tipo_moneda == 3)
            //            {
            //                txtprecio.Text = frm._Be.prdc_nPrecio_soles.ToString();
            //                txtDescuento.Text = frm._Be.prdc_nPor_Descuento.ToString();
            //                txtPrecioVenta.Text = frm._Be.prdc_nPrecio_venta.ToString();

            //            }else
            //                if (tipo_moneda ==4)
            //                {
            //                txtprecio.Text = frm._Be.prdc_nPrecio_dolares.ToString();
            //                txtDescuento.Text = frm._Be.prdc_nPor_Descuento.ToString();
            //                txtPrecioVenta.Text = frm._Be.prdc_nPrecio_venta_Dol.ToString();
            //                }

            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    if (oBase != null)
            //    {
            //        oBase.Focus();
            //        oBase.ErrorText = ex.Message;
            //        oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            //    }
            //    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            //}
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
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
            Totalizar();
        }
        private void Totalizar()
        {
            decimal ddescuento = 0;
            ddescuento = ((Convert.ToDecimal(txtprecio.Text) * Convert.ToDecimal(txtDescuento.Text)) / 100);

            txtPrecioVenta.Text = (Convert.ToDecimal(txtprecio.Text) - ddescuento).ToString();
        }

        private void txtDescuento_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void bteProducto_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                //List<EStock> MlistAux = new List<EStock>();
                //MlistAux = lstStockProductos.Where(ob => ob.strCodProducto.Trim() == bteProducto.Text.Trim()).ToList();
                //if (MlistAux.Count == 1)
                //{
                //    bteProducto.Tag = MlistAux[0].prdc_icod_producto;
                //    bteProducto.Text = MlistAux[0].strCodProducto;
                //    txtProducto.Text = MlistAux[0].strDesProducto;
                //    txtUnidadMedida.Text = MlistAux[0].strDesUM;
                //    Categoria = MlistAux[0].strCategoria;
                //    SubCategoria = MlistAux[0].strSubCategoriaUno;
                //    dblStockDisponible = MlistAux[0].stocc_stock_producto;
                //    txtPrecioVenta.Text = Convert.ToDecimal(MlistAux[0].dblPrecioVenta).ToString();
                //    if (tipo_moneda == 3)
                //    {
                //        txtprecio.Text = MlistAux[0].prdc_nPrecio_soles.ToString();
                //        txtDescuento.Text = MlistAux[0].prdc_nPor_Descuento.ToString();
                //        txtPrecioVenta.Text = MlistAux[0].prdc_nPrecio_venta.ToString();

                //    }
                //    else
                //        txtPrecioVenta.Text = MlistAux[0].prdc_nPrecio_dolares.ToString();

                //}
                //else
                //{
                //    bteProducto.Tag = 0;
                //    txtProducto.Text = "";
                //    txtCantidad.Text = "0";
                //    txtprecio.Text = "0";
                //    txtDescuento.Text = "0";
                //    txtPrecioVenta.Text = "0";
                //}
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