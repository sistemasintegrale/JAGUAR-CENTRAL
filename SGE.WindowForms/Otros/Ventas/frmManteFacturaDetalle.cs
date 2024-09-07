using DevExpress.XtraEditors;
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
    public partial class frmManteFacturaDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EFacturaMPDet> lstFacturaDetalle = new List<EFacturaMPDet>();
        public EFacturaMPDet obe = new EFacturaMPDet();
        public string Categoria, SubCategoria;
        public decimal dblStockDisponible = 0;
        List<EStock> lstStockProductos = new List<EStock>();
        public int tipo_moneda = 0;
        public int remic_icod_remision = 0;
        public Boolean flag_afecto_igv;
        public string PorIGV;
        public string CodigoSUNAT, strDesUM;
        public int unidc_icod_unidad_medida;
        public int TipodeFactura = 0;
        public int intAlmacen;
        public Boolean flag_exonerado;
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
        public frmManteFacturaDetalle()
        {
            InitializeComponent();
        }

        private void setValues()
        {

            bteProducto.Tag = obe.prdc_icod_producto;
            bteProducto.Text = obe.strCodProducto;
            txtProducto.Text = obe.strDesProducto;
            txtCantidad.Text = obe.favd_ncantidad.ToString();
            txtUnidadMedida.Text = obe.strDesUM;
            txtpartNumber.Text = obe.prdc_vpart_number;
            txtprecio.Text = obe.favd_nprecio_unitario_item.ToString();
            txtDescuento.Text = obe.favd_nporcentaje_descuento_item.ToString();
            txtPrecioVenta.Text = obe.favd_nprecio_total_item.ToString();

            strDesUM = obe.strDesUM;
            unidc_icod_unidad_medida = obe.unidc_icod_unidad_medida;

            CodigoSUNAT = obe.CodigoSUNAT;

            #region Factura Electronica Detalle
            obe.NumeroOrdenItem = obe.favd_iitem_factura;
            obe.cantidad = obe.favd_ncantidad;
            obe.unidadMedida = CodigoSUNAT;
            obe.ValorVentaItem = obe.favd_nprecio_total_item;
            obe.CodMotivoDescuentoItem = 0;
            obe.FactorDescuentoItem = 0;
            obe.DescuentoItem = 0;
            obe.BaseDescuentotem = 0;
            obe.CodMotivoCargoItem = 0;
            obe.FactorCargoItem = 0;
            obe.MontoCargoItem = 0;
            obe.BaseCargoItem = 0;
            obe.MontoTotalImpuestosItem = obe.favd_nmonto_impuesto_item;
            obe.MontoImpuestoIgvItem = obe.favd_nmonto_impuesto_item;

            if (obe.favd_nmonto_impuesto_item == 0)
            {
                obe.MontoInafectoItem = obe.favd_nprecio_total_item;
                obe.MontoAfectoImpuestoIgv = 0;
            }
            else
            {
                obe.MontoInafectoItem = 0;
                obe.MontoAfectoImpuestoIgv = obe.favd_nneto_igv;
            }
            obe.PorcentajeIGVItem = obe.favd_nporcentaje_descuento_item;
            obe.MontoImpuestoISCItem = 0;
            obe.MontoAfectoImpuestoIsc = 0;
            obe.PorcentajeISCtem = 0;
            obe.MontoImpuestoIVAPtem = 0;
            obe.MontoAfectoImpuestoIVAPItem = 0;
            obe.PorcentajeIVAPItem = 0;
            obe.descripcion = obe.favd_vdescripcion;
            obe.codigoItem = obe.strCodProducto;
            obe.ObservacionesItem = "";
            obe.ValorUnitarioItem = obe.favd_nprecio_unitario_item;
            if (obe.favd_nmonto_impuesto_item > 0)
            {
                obe.tipoImpuesto = "10";
            }
            else
            {
                obe.tipoImpuesto = "30";
            }
            obe.UMedida = obe.strUM;
            #endregion





        }
        public void CargarCombos()
        {



        }
        private void frmManteFacturaDetalle_Load(object sender, EventArgs e)
        {

            CargarCombos();
            if (TipodeFactura == 3)
            {
                bteProducto.Enabled = false;
                txtProducto.Enabled = true;
            }
        }


        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {

                if (TipodeFactura != 3)
                {

                    if (Convert.ToInt32(bteProducto.Tag) == 0)
                    {
                        oBase = bteProducto;
                        throw new ArgumentException("Seleccione producto");
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
                                dblStockDisponible = dblStockDisponible + obe.favd_ncantidad;
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

                }


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




                obe.almac_icod_almacen = intAlmacen;
                obe.favd_iitem_factura = Convert.ToInt32(txtItem.Text);
                obe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                obe.favd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.favd_vdescripcion = txtProducto.Text;
                if (Convert.ToBoolean(flag_afecto_igv) == true || TipodeFactura == 3)//tiene IGV
                {

                    obe.favd_nprecio_unitario_item = Convert.ToDecimal(txtprecio.Text);
                    obe.favd_nprecio_total_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    obe.favd_nporcentaje_descuento_item = Convert.ToDecimal(txtDescuento.Text);
                    PorIGV = Parametros.strPorcIGV;
                    obe.favd_nmonto_impuesto_item = Math.Round((Convert.ToDecimal((obe.favd_nprecio_total_item * Convert.ToDecimal(Parametros.strPorcIGV)) / (100 + Convert.ToDecimal(Parametros.strPorcIGV)))), 4, MidpointRounding.ToEven);
                    obe.favd_nneto_igv = Math.Round((Convert.ToDecimal(txtPrecioVenta.Text) - obe.favd_nmonto_impuesto_item), 4);
                    obe.favd_nneto_exo = 0;
                }
                else
                {
                    flag_exonerado = true;
                    obe.favd_nprecio_unitario_item = Convert.ToDecimal(txtprecio.Text);
                    obe.favd_nprecio_total_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    obe.favd_nneto_exo = Convert.ToDecimal(txtPrecioVenta.Text);
                    obe.favd_nporcentaje_descuento_item = 0;
                    obe.favd_nmonto_impuesto_item = 0;
                    obe.favd_nneto_igv = 0;

                }



                /**/
                obe.strCodProducto = bteProducto.Text;
                obe.strDesUM = txtUnidadMedida.Text;
                obe.strDesProducto = txtProducto.Text;
                obe.dblStockDisponible = dblStockDisponible;
                obe.dblMontoDescuento = (obe.favd_ncantidad * obe.favd_nprecio_unitario_item) - obe.favd_nprecio_total_item;
                obe.strMoneda = txtMoneda.Text;
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;
                obe.flagPlanilla = true;
                obe.prdc_vpart_number = txtpartNumber.Text;
                obe.unidc_icod_unidad_medida = unidc_icod_unidad_medida;



                #region Factura Electronica Detalle
                obe.NumeroOrdenItem = obe.favd_iitem_factura;
                obe.cantidad = obe.favd_ncantidad;
                if (TipodeFactura == 3)
                {
                    obe.unidadMedida = "ZZ";
                }
                else
                {
                    obe.unidadMedida = CodigoSUNAT;
                }

                obe.ValorVentaItem = Math.Round((obe.favd_nprecio_total_item - obe.favd_nmonto_impuesto_item), 2);
                obe.CodMotivoDescuentoItem = 0;
                obe.FactorDescuentoItem = 0;
                obe.DescuentoItem = 0;
                obe.BaseDescuentotem = 0;
                obe.CodMotivoCargoItem = 0;
                obe.FactorCargoItem = 0;
                obe.MontoCargoItem = 0;
                obe.BaseCargoItem = 0;
                obe.MontoTotalImpuestosItem = obe.favd_nmonto_impuesto_item;
                obe.MontoImpuestoIgvItem = obe.favd_nmonto_impuesto_item;
                if (obe.favd_nmonto_impuesto_item == 0)
                {
                    obe.MontoInafectoItem = obe.favd_nprecio_total_item;
                    obe.MontoAfectoImpuestoIgv = 0;
                }
                else
                {
                    obe.MontoInafectoItem = 0;
                    obe.MontoAfectoImpuestoIgv = obe.favd_nneto_igv;
                }
                obe.PorcentajeIGVItem = Convert.ToDecimal(Parametros.strPorcIGV);
                obe.MontoImpuestoISCItem = 0;
                obe.MontoAfectoImpuestoIsc = 0;
                obe.PorcentajeISCtem = 0;
                obe.MontoImpuestoIVAPtem = 0;
                obe.MontoAfectoImpuestoIVAPItem = 0;
                obe.PorcentajeIVAPItem = 0;
                obe.descripcion = obe.favd_vdescripcion;
                obe.codigoItem = obe.strCodProducto;
                obe.ObservacionesItem = "";
                obe.ValorUnitarioItem = Math.Round((obe.favd_nprecio_unitario_item / Convert.ToDecimal("1." + Parametros.strPorcIGV.Replace(".", ""))), 6);
                if (Convert.ToBoolean(flag_afecto_igv) == true)//tiene IGV
                {
                    obe.tipoImpuesto = "10";
                }
                else
                {
                    obe.tipoImpuesto = "30";
                }
                obe.UMedida = strDesUM;
                obe.PrecioVentaUnitarioItem = obe.favd_nprecio_total_item / obe.favd_ncantidad;
                #endregion



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


        private void listarProducto()
        {

            BaseEdit oBase = null;
            try
            {
                //if (Convert.ToInt32(intAlmacen) == 0)
                //{
                //    oBase = bteAlmacen;
                //    throw new ArgumentException("Seleccione almacén");
                //}
                using (frmListarStockPorAlmacen frm = new frmListarStockPorAlmacen())
                {
                    frm.intAlmacen = Convert.ToInt32(intAlmacen);
                    frm.afecto = flag_afecto_igv;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = frm._Be.prdc_icod_producto;
                        bteProducto.Text = frm._Be.strCodProducto;
                        txtProducto.Text = frm._Be.strDesProducto;
                        txtUnidadMedida.Text = frm._Be.strDesUM;
                        //txtCaracteristicas.Text=frm._Be.
                        Categoria = frm._Be.strCategoria;
                        SubCategoria = frm._Be.strSubCategoriaUno;
                        dblStockDisponible = frm._Be.stocc_stock_producto;
                        txtPrecioVenta.Text = Convert.ToDecimal(frm._Be.dblPrecioVenta).ToString();
                        CodigoSUNAT = frm._Be.CodigoSUNAT;
                        strDesUM = frm._Be.strDesUM;
                        unidc_icod_unidad_medida = frm._Be.unidc_icod_unidad_medida;


                        if (tipo_moneda == 3)
                        {
                            txtprecio.Text = frm._Be.prdc_nPrecio_soles.ToString();
                            txtDescuento.Text = frm._Be.prdc_nPor_Descuento.ToString();
                            txtPrecioVenta.Text = frm._Be.prdc_nPrecio_venta.ToString();

                        }
                        else
                            if (tipo_moneda == 4)
                        {
                            txtprecio.Text = frm._Be.prdc_nPrecio_dolares.ToString();
                            txtDescuento.Text = frm._Be.prdc_nPor_Descuento.ToString();
                            txtPrecioVenta.Text = frm._Be.prdc_nPrecio_venta_Dol.ToString();
                        }

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
            //decimal ddescuento = 0;
            //ddescuento = ((Convert.ToDecimal(txtprecio.Text) * Convert.ToDecimal(txtDescuento.Text)) / 100);

            //txtPrecioVenta.Text = (Convert.ToDecimal(txtprecio.Text)-ddescuento).ToString();


            decimal ddescuento = 0;
            ddescuento = ((Convert.ToDecimal(txtprecio.Text) * Convert.ToDecimal(txtDescuento.Text)) / 100);
            txtPrecioVenta.Text = ((Convert.ToDecimal(txtprecio.Text) - ddescuento) * Convert.ToDecimal(txtCantidad.Text)).ToString();
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

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
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