using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmBoletaDetalle : DevExpress.XtraEditors.XtraForm
    {
        public int favd_icod_item_factura;//referencia
        public int icod_almacen;
        public int icod_punto_venta;
        public int icod_motivo;
        public int indicador;
        public Boolean flag_exonerado;
        public Boolean flag_afecto_igv;
        public string PorIGV;
        public int remic_icod_remision = 0;
        public decimal dblStockDisponible = 0;
        public int intClasificacion = 0;
        public string CodigoSUNAT { get; set; }
        public string strUM;
        public int? icodUM;
        public List<EFacturaCab> oProducto;
        public List<EProdProducto> OPRODUCTO;
        public List<EBoletaDet> lstBoletaDetalle = new List<EBoletaDet>();
        public EBoletaDet obe = new EBoletaDet();
        public BSMaintenanceStatus oState;
        public string descripcion { get; set; }
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
            //setValues();
        }
        public FrmBoletaDetalle()
        {
            InitializeComponent();
        }
        public void setValues()
        {
            txtitem.Text = obe.bovd_iitem_boleta.ToString();

            #region Factura Electronica Detalle
            obe.NumeroOrdenItem = obe.bovd_iitem_boleta;
            obe.cantidad = obe.bovd_ncantidad;
            obe.unidadMedida = CodigoSUNAT;
            obe.ValorVentaItem = obe.bovd_nprecio_total_item;
            obe.CodMotivoDescuentoItem = 0;
            obe.FactorDescuentoItem = 0;
            obe.DescuentoItem = 0;
            obe.BaseDescuentotem = 0;
            obe.CodMotivoCargoItem = 0;
            obe.FactorCargoItem = 0;
            obe.MontoCargoItem = 0;
            obe.BaseCargoItem = 0;
            obe.MontoTotalImpuestosItem = obe.bovd_nmonto_impuesto_item;
            obe.MontoImpuestoIgvItem = obe.bovd_nmonto_impuesto_item;
            if (obe.bovd_nmonto_impuesto_item == 0)
            {
                obe.MontoInafectoItem = obe.bovd_nprecio_total_item;
                obe.MontoAfectoImpuestoIgv = 0;
            }
            else
            {
                obe.MontoInafectoItem = 0;
                obe.MontoAfectoImpuestoIgv = obe.bovd_nneto_igv;
            }
            obe.PorcentajeIGVItem = obe.bovd_nporcentaje_descuento_item;
            obe.MontoImpuestoISCItem = 0;
            obe.MontoAfectoImpuestoIsc = 0;
            obe.PorcentajeISCtem = 0;
            obe.MontoImpuestoIVAPtem = 0;
            obe.MontoAfectoImpuestoIVAPItem = 0;
            obe.PorcentajeIVAPItem = 0;
            obe.descripcion = obe.bovd_vdescripcion;
            obe.codigoItem = obe.strCodProducto;
            obe.ObservacionesItem = obe.ObservacionesItem;
            obe.ValorUnitarioItem = obe.MontoAfectoImpuestoIgv / obe.bovd_ncantidad_total_producto;
            if (obe.bovd_nmonto_impuesto_item > 0)
            {
                obe.tipoImpuesto = "10";
            }
            else
            {
                obe.tipoImpuesto = "30";
            }
            obe.UMedida = obe.strDesUM;
            #endregion
        }
        private void btncodigoProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //using (FrmListarProductosDeStock frm = new FrmListarProductosDeStock())
            //{
            //    frm.stocc_iid_almacen = Convert.ToInt32(icod_almacen);
            //    frm.puvec_icod_punto_venta = Convert.ToInt32(icod_punto_venta);
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        btncodigoProducto.Tag = frm._Be.pr_icod_producto;
            //        btncodigoProducto.Text = frm._Be.pr_vcodigo_externo.Substring(0, frm._Be.pr_vcodigo_externo.Length - 2);
            //        txtdescripcion.Text = frm._Be.pr_vdescripcion_producto.Substring(0, frm._Be.pr_vdescripcion_producto.Length - 2) + lkpSerie.Text;
            //        descripcion = frm._Be.pr_vdescripcion_producto.Substring(0, frm._Be.pr_vdescripcion_producto.Length - 2);
            //        flag_afecto_igv = frm._Be.pr_afecto_igv;
            //        CodigoSUNAT = frm._Be.CodigoSUNAT;
            //    }
            //}

            using (FrmListarProducto Producto = new FrmListarProducto())
            {
                if (Producto.ShowDialog() == DialogResult.OK)
                {
                    btncodigoProducto.Tag = Producto._Be.pr_icod_producto;
                    btncodigoProducto.Text = Producto._Be.pr_vcodigo_externo.Substring(0, Producto._Be.pr_vcodigo_externo.Length - 2);
                    txtdescripcion.Text = Producto._Be.pr_vdescripcion_producto.Substring(0, Producto._Be.pr_vdescripcion_producto.Length - 2) + lkpSerie.Text;
                    descripcion = Producto._Be.pr_vdescripcion_producto.Substring(0, Producto._Be.pr_vdescripcion_producto.Length - 2);
                    //flag_afecto_igv = Producto._Be.pr_afecto_igv;
                    CodigoSUNAT = Producto._Be.CodigoSUNAT;
                    strUM = Producto._Be.strUM;
                    icodUM = Producto._Be.unidc_icod_unidad_medida;
                }
            }
        }
        public int[] icodProducto = new int[10]; //---tener los icod de todos los productos
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_iid_registro_serie == Convert.ToInt32(lkpSerie.EditValue)).ToList();
            if (lstSerie[0].resec_vtalla_inicial != "0" && lstSerie[0].resec_vtalla_final != "0")
            {
                List<EProdProducto> oProducto = new List<EProdProducto>();
                List<EProdProducto> ostockproducto = new List<EProdProducto>();

                if ((btncodigoProducto.Tag) == null)
                {
                    XtraMessageBox.Show("Ingrese el Producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    //BorrarCantidades();
                    //BorrarTalla();
                    int i = -1;
                    TextEdit[] textoCantidad = GetTextoCantidad();
                    TextEdit[] textoTalla = GetTextoTalla();
                    TextEdit[] textoStock = GetTextoCantidadStock();
                    for (int x = Convert.ToInt32(lstSerie[0].resec_vtalla_inicial); x <= Convert.ToInt32(lstSerie[0].resec_vtalla_final); x++)
                    {
                        //i++;
                        //textoTalla[i].Text = string.Format("{0:00}", x).ToString();
                        //textoCantidad[i].Text = "0";

                        //string codigoexterno = btncodigoProducto.Text + textoTalla[i].Text;
                        ////oProducto = new BCentral().VerificarProdStockProducto(codigoexterno, Convert.ToInt32(icod_almacen), Convert.ToInt32(icod_punto_venta));

                        //if (oProducto.Count > 0)
                        //{
                        //    textoCantidad[i].Enabled = true;
                        //    textoStock[i].Text = Convert.ToInt32(oProducto[0].stocc_nstock_prod).ToString();
                        //}
                        //else
                        //    textoCantidad[i].Enabled = false;

                        i++;
                        textoTalla[i].Text = string.Format("{0:00}", x).ToString();
                        textoCantidad[i].Text = "0";

                        string codigoexterno = btncodigoProducto.Text + textoTalla[i].Text;
                        oProducto = new BCentral().VerificarProductoPvt(codigoexterno);
                        if (oProducto.Count > 0)
                        {
                            ostockproducto = new BCentral().VerificarProdStockProducto(oProducto[0].pr_icod_producto, Convert.ToInt32(icod_almacen), Parametros.intEjercicio);
                        }



                        if (oProducto.Count > 0)
                        {
                            if (ostockproducto.Count > 0)
                            {
                                textoStock[i].Text = Convert.ToInt32(ostockproducto[0].stocc_nstock_prod).ToString();
                            }
                            icodProducto[i] = oProducto[0].pr_icod_producto;
                        }
                        //if (Convert.ToInt32(textoStock[i].Text) > 0)
                        //{
                        textoCantidad[i].Enabled = true;
                        //}
                        //else
                        //{
                        //    textoCantidad[i].Enabled = false;
                        //} 

                    }
                    lkpSerie.Enabled = false;
                    btncodigoProducto.Enabled = false;
                }
            }
            else
            {
                XtraMessageBox.Show("Ingrese Serie", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void BorrarCantidades()
        {
            TextEdit[] texto = GetTextoCantidad();
            for (int x = 0; x < texto.Length; x++)
            {
                texto[x].Text = "0";
                texto[x].Tag = 0;
                texto[x].Enabled = false;
            }
        }
        private void BorrarTalla()
        {
            TextEdit[] texto = GetTextoTalla();
            for (int x = 0; x < texto.Length; x++)
            {
                texto[x].Text = "0";
                texto[x].Tag = 0;
            }

        }
        private TextEdit[] GetTextoCantidad()
        {
            TextEdit[] texto = new TextEdit[] { txtcantidad1, txtcantidad2, txtcantidad3, txtcantidad4,
                            txtcantidad5, txtcantidad6, txtcantidad7, txtcantidad8, txtcantidad9, txtcantidad10};
            return texto;
        }
        private TextEdit[] GetTextoCantidadStock()
        {
            TextEdit[] texto = new TextEdit[] { txtStock1, txtStock2, txtStock3 , txtStock4,
                            txtStock5, txtStock6, txtStock7, txtStock8, txtStock9, txtStock10};
            return texto;
        }

        private TextEdit[] GetTextoTalla()
        {
            TextEdit[] texto = new TextEdit[] { txttalla1, txttalla2, txttalla3, txttalla4,
                            txttalla5, txttalla6, txttalla7, txttalla8, txttalla9, txttalla10};
            return texto;
        }
        private void FrmNotaSalidaDetalle_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSerie, new BCentral().ListarRegistroSerie(), "resec_vdescripcion", "resec_iid_registro_serie", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpSerie.EditValue = obe.bovd_rango_talla;
                txtitem.Text = Convert.ToString(obe.bovd_iitem_boleta);
            }
        }
        public void Redimencionarstock()
        {
            List<EProdProducto> ostockproducto;
            TextEdit[] textoCantidad = GetTextoCantidad();
            TextEdit[] textoTalla = GetTextoTalla();
            TextEdit[] textostock = GetTextoCantidadStock();

            if (favd_icod_item_factura != 0)
            {
                for (int x = 0; x <= 9; x++)
                {
                    string codigo_producto = btncodigoProducto.Text + textoTalla[x].Text;
                    OPRODUCTO = new BCentral().VerificarProductoPvt(codigo_producto);
                    ostockproducto = new BCentral().VerificarProdStockProducto(OPRODUCTO[0].pr_icod_producto, Convert.ToInt32(icod_almacen), Convert.ToInt32(icod_punto_venta));
                    if (ostockproducto.Count > 0)
                        textostock[x].Text = Convert.ToInt32(ostockproducto[0].stocc_nstock_prod).ToString();
                    else
                        textostock[x].Text = "0";

                    if (ostockproducto.Count > 0)
                        textoCantidad[x].Enabled = true;
                    else
                        textoCantidad[x].Enabled = false;
                }
            }
            else
            {
                for (int x = 0; x <= 9; x++)
                {
                    string codigoexterno = btncodigoProducto.Text + textoTalla[x].Text;
                    OPRODUCTO = new BCentral().VerificarProductoPvt(codigoexterno);
                    ostockproducto = new BCentral().VerificarProdStockProducto(OPRODUCTO[0].pr_icod_producto, Convert.ToInt32(icod_almacen), Convert.ToInt32(icod_punto_venta));
                    if (ostockproducto.Count > 0)
                        textostock[x].Text = (Convert.ToInt32(ostockproducto[0].stocc_nstock_prod) - Convert.ToInt32(textoCantidad[x].Text)).ToString();
                    else
                        textostock[x].Text = "0";

                    if (ostockproducto.Count > 0)
                        textoCantidad[x].Enabled = true;
                    else
                        textoCantidad[x].Enabled = false;
                }
            }
        }
        private void btnAgregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AgregarDetalle();
        }
        private void AgregarDetalle()
        {
            bool flag = true;
            BaseEdit oBase = null;
            try
            {
                if ((btncodigoProducto.Tag) == null)
                {
                    oBase = btncodigoProducto;
                    XtraMessageBox.Show("Ingrese el Producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (Convert.ToDecimal(txtcantidaddetalle.Text) <= 0)
                {
                    oBase = txtcantidaddetalle;
                    throw new ArgumentException("La cantidad debe ser mayor a 0");
                }
                if (Convert.ToDecimal(txtPrecioVenta.Text) <= 0)
                {
                    oBase = txtPrecioVenta;
                    throw new ArgumentException("El precio unitario debe ser mayor a 0");
                }

                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();

                obe.almac_icod_almacen = Convert.ToInt32(icod_almacen);
                obe.bovd_iitem_boleta = Convert.ToInt32(txtitem.Text);
                obe.tablc_iid_motivo = Convert.ToInt32(icod_motivo);
                obe.prdc_icod_producto = Convert.ToInt32(btncodigoProducto.Tag);
                obe.bovd_vcodigo_extremo_prod = btncodigoProducto.Text;
                obe.bovd_vdescripcion_prod = txtdescripcion.Text;
                obe.bovd_icod_serie = Convert.ToInt32(lkpSerie.EditValue);
                obe.bovd_rango_talla = txtrangotallas.Text;
                obe.resec_vdescripcion = lkpSerie.Text;
                obe.bovd_ncantidad_total_producto = Convert.ToDecimal(txtcantidaddetalle.Text);
                obe.dblMontoDescuento = (obe.bovd_ncantidad_total_producto * obe.bovd_nprecio_unitario_item) - obe.bovd_nprecio_total_item;
                obe.bovd_talla1 = Convert.ToInt32((textoTalla[0].Text == "") ? null : textoTalla[0].Text);
                obe.bovd_talla2 = Convert.ToInt32((textoTalla[1].Text == "") ? null : textoTalla[1].Text);
                obe.bovd_talla3 = Convert.ToInt32((textoTalla[2].Text == "") ? null : textoTalla[2].Text);
                obe.bovd_talla4 = Convert.ToInt32((textoTalla[3].Text == "") ? null : textoTalla[3].Text);
                obe.bovd_talla5 = Convert.ToInt32((textoTalla[4].Text == "") ? null : textoTalla[4].Text);
                obe.bovd_talla6 = Convert.ToInt32((textoTalla[5].Text == "") ? null : textoTalla[5].Text);
                obe.bovd_talla7 = Convert.ToInt32((textoTalla[6].Text == "") ? null : textoTalla[6].Text);
                obe.bovd_talla8 = Convert.ToInt32((textoTalla[7].Text == "") ? null : textoTalla[7].Text);
                obe.bovd_talla9 = Convert.ToInt32((textoTalla[8].Text == "") ? null : textoTalla[8].Text);
                obe.bovd_talla10 = Convert.ToInt32((textoTalla[9].Text == "") ? null : textoTalla[9].Text);
                obe.bovd_cant_talla1 = Convert.ToInt32((textoCantidad[0].Text == "") ? null : textoCantidad[0].Text);
                obe.bovd_cant_talla2 = Convert.ToInt32((textoCantidad[1].Text == "") ? null : textoCantidad[1].Text);
                obe.bovd_cant_talla3 = Convert.ToInt32((textoCantidad[2].Text == "") ? null : textoCantidad[2].Text);
                obe.bovd_cant_talla4 = Convert.ToInt32((textoCantidad[3].Text == "") ? null : textoCantidad[3].Text);
                obe.bovd_cant_talla5 = Convert.ToInt32((textoCantidad[4].Text == "") ? null : textoCantidad[4].Text);
                obe.bovd_cant_talla6 = Convert.ToInt32((textoCantidad[5].Text == "") ? null : textoCantidad[5].Text);
                obe.bovd_cant_talla7 = Convert.ToInt32((textoCantidad[6].Text == "") ? null : textoCantidad[6].Text);
                obe.bovd_cant_talla8 = Convert.ToInt32((textoCantidad[7].Text == "") ? null : textoCantidad[7].Text);
                obe.bovd_cant_talla9 = Convert.ToInt32((textoCantidad[8].Text == "") ? null : textoCantidad[8].Text);
                obe.bovd_cant_talla10 = Convert.ToInt32((textoCantidad[9].Text == "") ? null : textoCantidad[9].Text);
                obe.bovd_icod_producto1 = icodProducto[0];
                obe.bovd_icod_producto2 = icodProducto[1];
                obe.bovd_icod_producto3 = icodProducto[2];
                obe.bovd_icod_producto4 = icodProducto[3];
                obe.bovd_icod_producto5 = icodProducto[4];
                obe.bovd_icod_producto6 = icodProducto[5];
                obe.bovd_icod_producto7 = icodProducto[6];
                obe.bovd_icod_producto8 = icodProducto[7];
                obe.bovd_icod_producto9 = icodProducto[8];
                obe.bovd_icod_producto10 = icodProducto[9];
                obe.flag_afecto_igv = flag_afecto_igv;


                if (Convert.ToBoolean(flag_afecto_igv) == true)//tiene IGV
                {
                    obe.bovd_nprecio_unitario_item = Convert.ToDecimal(txtprecio.Text);
                    obe.bovd_nprecio_total_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    obe.bovd_nporcentaje_descuento_item = Convert.ToDecimal(Parametros.strPorcIGV);
                    PorIGV = Parametros.strPorcIGV;
                    obe.bovd_nmonto_impuesto_item = Math.Round((Convert.ToDecimal((obe.bovd_nprecio_total_item * Convert.ToDecimal(Parametros.strPorcIGV)) / (100 + Convert.ToDecimal(Parametros.strPorcIGV)))), 4, MidpointRounding.ToEven);
                    obe.bovd_nneto_igv = Math.Round((Convert.ToDecimal(txtPrecioVenta.Text) - obe.bovd_nmonto_impuesto_item), 4);
                    obe.bovd_nneto_exo = 0;
                }
                else
                {
                    flag_exonerado = true;
                    obe.bovd_nprecio_unitario_item = Convert.ToDecimal(txtprecio.Text);
                    obe.bovd_nprecio_total_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    obe.bovd_nneto_exo = Convert.ToDecimal(txtPrecioVenta.Text);
                    /*---*/
                    obe.bovd_nporcentaje_descuento_item = 0;
                    obe.bovd_nmonto_impuesto_item = 0;
                    obe.bovd_nneto_igv = 0;
                }
                obe.strDesUM = strUM;
                obe.unidc_icod_unidad_medida = Convert.ToInt32(icodUM);

                #region Factura Electronica Detalle
                obe.NumeroOrdenItem = obe.bovd_iitem_boleta;
                obe.cantidad = obe.bovd_ncantidad_total_producto;
                obe.unidadMedida = CodigoSUNAT;

                if (Convert.ToBoolean(flag_afecto_igv) == true)
                {
                    obe.ValorVentaItem = Math.Round(obe.bovd_nprecio_total_item - obe.bovd_nmonto_impuesto_item, 2);
                }
                else
                {
                    obe.ValorVentaItem = obe.bovd_nprecio_total_item;
                }
                obe.CodMotivoDescuentoItem = 0;
                obe.FactorDescuentoItem = 0;
                obe.DescuentoItem = 0;
                obe.BaseDescuentotem = 0;
                obe.CodMotivoCargoItem = 0;
                obe.FactorCargoItem = 0;
                obe.MontoCargoItem = 0;
                obe.BaseCargoItem = 0;
                obe.MontoTotalImpuestosItem = obe.bovd_nmonto_impuesto_item + obe.bovd_nmonto_imp_arroz;
                obe.MontoImpuestoIgvItem = obe.bovd_nmonto_impuesto_item;

                //obe.MontoAfectoImpuestoIgv = obe.favd_nneto_igv;
                if (obe.bovd_nmonto_impuesto_item == 0 && obe.bovd_nmonto_imp_arroz == 0)
                {
                    //obe.MontoAfectoImpuestoIgv = obe.favd_nneto_igv;
                    obe.MontoInafectoItem = obe.bovd_nprecio_total_item;
                    obe.MontoAfectoImpuestoIgv = 0;
                }
                else
                {
                    obe.MontoInafectoItem = 0;
                    obe.MontoAfectoImpuestoIgv = obe.bovd_nneto_igv;
                }

                obe.PorcentajeIGVItem = obe.bovd_nporcentaje_descuento_item;
                obe.MontoImpuestoISCItem = 0;
                obe.MontoAfectoImpuestoIsc = 0;
                obe.PorcentajeISCtem = 0;
                obe.MontoImpuestoIVAPtem = obe.bovd_nmonto_imp_arroz;
                obe.MontoAfectoImpuestoIVAPItem = obe.bovd_nneto_ivap;
                obe.PorcentajeIVAPItem = obe.bovd_npor_imp_arroz;
                obe.descripcion = obe.bovd_vdescripcion_prod;
                obe.codigoItem = obe.bovd_vcodigo_extremo_prod;
                obe.ObservacionesItem = "";

                if (Convert.ToBoolean(flag_afecto_igv) == true)
                {
                    obe.ValorUnitarioItem = Math.Round((obe.bovd_nprecio_unitario_item / Convert.ToDecimal("1." + Parametros.strPorcIGV.Replace(".", ""))), 6);
                }
                else
                {
                    obe.ValorUnitarioItem = obe.bovd_nprecio_unitario_item;
                }

                obe.UMedida = obe.strUM;
                obe.PrecioVentaUnitarioItem = obe.bovd_nprecio_total_item / obe.bovd_ncantidad_total_producto;
                if (Convert.ToBoolean(flag_afecto_igv) == true)//tiene IGV
                {
                    obe.tipoImpuesto = "10";
                }
                else
                {
                    obe.tipoImpuesto = "30";//tiene exo
                }
                obe.UMedida = strUM;
                #endregion
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {

            }
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Modificar();
        }
        private void Modificar()
        {
            BaseEdit oBase = null;
            try
            {
                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();

                obe.almac_icod_almacen = Convert.ToInt32(icod_almacen);
                obe.bovd_iitem_boleta = Convert.ToInt32(txtitem.Text);
                obe.tablc_iid_motivo = Convert.ToInt32(icod_motivo);
                obe.prdc_icod_producto = Convert.ToInt32(btncodigoProducto.Tag);
                obe.bovd_vcodigo_extremo_prod = btncodigoProducto.Text;
                obe.bovd_vdescripcion_prod = txtdescripcion.Text;
                obe.bovd_icod_serie = Convert.ToInt32(lkpSerie.EditValue);
                obe.bovd_rango_talla = txtrangotallas.Text;
                obe.resec_vdescripcion = lkpSerie.Text;

                obe.bovd_ncantidad_total_producto = Convert.ToDecimal(txtcantidaddetalle.Text);
                //obe.bovd_monto_unit = 0;
                //obe.bovd_monto_total = 0;
                //obe.bovd_nprecio_unitario_item = Convert.ToDecimal(txtprecio.Text);
                //obe.bovd_nprecio_total_item = Convert.ToDecimal(txtPrecioVenta.Text);
                obe.dblMontoDescuento = (obe.bovd_ncantidad_total_producto * obe.bovd_nprecio_unitario_item) - obe.bovd_nprecio_total_item;
                obe.bovd_talla1 = Convert.ToInt32((textoTalla[0].Text == "") ? null : textoTalla[0].Text);
                obe.bovd_talla2 = Convert.ToInt32((textoTalla[1].Text == "") ? null : textoTalla[1].Text);
                obe.bovd_talla3 = Convert.ToInt32((textoTalla[2].Text == "") ? null : textoTalla[2].Text);
                obe.bovd_talla4 = Convert.ToInt32((textoTalla[3].Text == "") ? null : textoTalla[3].Text);
                obe.bovd_talla5 = Convert.ToInt32((textoTalla[4].Text == "") ? null : textoTalla[4].Text);
                obe.bovd_talla6 = Convert.ToInt32((textoTalla[5].Text == "") ? null : textoTalla[5].Text);
                obe.bovd_talla7 = Convert.ToInt32((textoTalla[6].Text == "") ? null : textoTalla[6].Text);
                obe.bovd_talla8 = Convert.ToInt32((textoTalla[7].Text == "") ? null : textoTalla[7].Text);
                obe.bovd_talla9 = Convert.ToInt32((textoTalla[8].Text == "") ? null : textoTalla[8].Text);
                obe.bovd_talla10 = Convert.ToInt32((textoTalla[9].Text == "") ? null : textoTalla[9].Text);
                obe.bovd_cant_talla1 = Convert.ToInt32((textoCantidad[0].Text == "") ? null : textoCantidad[0].Text);
                obe.bovd_cant_talla2 = Convert.ToInt32((textoCantidad[1].Text == "") ? null : textoCantidad[1].Text);
                obe.bovd_cant_talla3 = Convert.ToInt32((textoCantidad[2].Text == "") ? null : textoCantidad[2].Text);
                obe.bovd_cant_talla4 = Convert.ToInt32((textoCantidad[3].Text == "") ? null : textoCantidad[3].Text);
                obe.bovd_cant_talla5 = Convert.ToInt32((textoCantidad[4].Text == "") ? null : textoCantidad[4].Text);
                obe.bovd_cant_talla6 = Convert.ToInt32((textoCantidad[5].Text == "") ? null : textoCantidad[5].Text);
                obe.bovd_cant_talla7 = Convert.ToInt32((textoCantidad[6].Text == "") ? null : textoCantidad[6].Text);
                obe.bovd_cant_talla8 = Convert.ToInt32((textoCantidad[7].Text == "") ? null : textoCantidad[7].Text);
                obe.bovd_cant_talla9 = Convert.ToInt32((textoCantidad[8].Text == "") ? null : textoCantidad[8].Text);
                obe.bovd_cant_talla10 = Convert.ToInt32((textoCantidad[9].Text == "") ? null : textoCantidad[9].Text);

                obe.bovd_icod_producto1 = icodProducto[0];
                obe.bovd_icod_producto2 = icodProducto[1];
                obe.bovd_icod_producto3 = icodProducto[2];
                obe.bovd_icod_producto4 = icodProducto[3];
                obe.bovd_icod_producto5 = icodProducto[4];
                obe.bovd_icod_producto6 = icodProducto[5];
                obe.bovd_icod_producto7 = icodProducto[6];
                obe.bovd_icod_producto8 = icodProducto[7];
                obe.bovd_icod_producto9 = icodProducto[8];
                obe.bovd_icod_producto10 = icodProducto[9];
                obe.flag_afecto_igv = flag_afecto_igv;

                if (Convert.ToBoolean(flag_afecto_igv) == true)//tiene IGV
                {
                    obe.bovd_nprecio_unitario_item = Convert.ToDecimal(txtprecio.Text);
                    obe.bovd_nprecio_total_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    obe.bovd_nporcentaje_descuento_item = Convert.ToDecimal(Parametros.strPorcIGV);
                    PorIGV = Parametros.strPorcIGV;
                    obe.bovd_nmonto_impuesto_item = Math.Round((Convert.ToDecimal((obe.bovd_nprecio_total_item * Convert.ToDecimal(Parametros.strPorcIGV)) / (100 + Convert.ToDecimal(Parametros.strPorcIGV)))), 2, MidpointRounding.ToEven);
                    obe.bovd_nneto_igv = Convert.ToDecimal(txtPrecioVenta.Text) - obe.bovd_nmonto_impuesto_item;
                    obe.bovd_nneto_exo = 0;
                }
                else
                {
                    flag_exonerado = true;
                    obe.bovd_nprecio_unitario_item = Convert.ToDecimal(txtprecio.Text);
                    obe.bovd_nprecio_total_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    obe.bovd_nneto_exo = Convert.ToDecimal(txtPrecioVenta.Text);
                    /*---*/
                    obe.bovd_nporcentaje_descuento_item = 0;
                    obe.bovd_nmonto_impuesto_item = 0;
                    obe.bovd_nneto_igv = 0;
                }
                obe.strDesUM = strUM;
                obe.unidc_icod_unidad_medida = Convert.ToInt32(icodUM);

                #region Factura Electronica Detalle
                obe.NumeroOrdenItem = obe.bovd_iitem_boleta;
                obe.cantidad = obe.bovd_ncantidad_total_producto;
                obe.unidadMedida = CodigoSUNAT;

                if (Convert.ToBoolean(flag_afecto_igv) == true)
                {
                    obe.ValorVentaItem = Math.Round(obe.bovd_nprecio_total_item - obe.bovd_nmonto_impuesto_item, 2);
                }
                else
                {
                    obe.ValorVentaItem = obe.bovd_nprecio_total_item;
                }
                obe.CodMotivoDescuentoItem = 0;
                obe.FactorDescuentoItem = 0;
                obe.DescuentoItem = 0;
                obe.BaseDescuentotem = 0;
                obe.CodMotivoCargoItem = 0;
                obe.FactorCargoItem = 0;
                obe.MontoCargoItem = 0;
                obe.BaseCargoItem = 0;
                obe.MontoTotalImpuestosItem = obe.bovd_nmonto_impuesto_item + obe.bovd_nmonto_imp_arroz;
                obe.MontoImpuestoIgvItem = obe.bovd_nmonto_impuesto_item;

                //obe.MontoAfectoImpuestoIgv = obe.favd_nneto_igv;
                if (obe.bovd_nmonto_impuesto_item == 0 && obe.bovd_nmonto_imp_arroz == 0)
                {
                    //obe.MontoAfectoImpuestoIgv = obe.favd_nneto_igv;
                    obe.MontoInafectoItem = obe.bovd_nprecio_total_item;
                    obe.MontoAfectoImpuestoIgv = 0;
                }
                else
                {
                    obe.MontoInafectoItem = 0;
                    obe.MontoAfectoImpuestoIgv = obe.bovd_nneto_igv;
                }

                obe.PorcentajeIGVItem = obe.bovd_nporcentaje_descuento_item;
                obe.MontoImpuestoISCItem = 0;
                obe.MontoAfectoImpuestoIsc = 0;
                obe.PorcentajeISCtem = 0;
                obe.MontoImpuestoIVAPtem = obe.bovd_nmonto_imp_arroz;
                obe.MontoAfectoImpuestoIVAPItem = obe.bovd_nneto_ivap;
                obe.PorcentajeIVAPItem = obe.bovd_npor_imp_arroz;
                obe.descripcion = obe.bovd_vdescripcion_prod;
                obe.codigoItem = obe.bovd_vcodigo_extremo_prod;
                obe.ObservacionesItem = "";

                if (Convert.ToBoolean(flag_afecto_igv) == true)
                {
                    obe.ValorUnitarioItem = Math.Round((obe.bovd_nprecio_unitario_item / Convert.ToDecimal("1." + Parametros.strPorcIGV.Replace(".", ""))), 6);
                }
                else
                {
                    obe.ValorUnitarioItem = obe.bovd_nprecio_unitario_item;
                }

                obe.UMedida = obe.strUM;
                obe.PrecioVentaUnitarioItem = obe.bovd_nprecio_total_item / obe.bovd_ncantidad_total_producto;
                if (Convert.ToBoolean(flag_afecto_igv) == true)//tiene IGV
                {
                    obe.tipoImpuesto = "10";
                }
                else
                {
                    obe.tipoImpuesto = "30";//tiene exo
                }
                obe.UMedida = strUM;
                #endregion

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtcantidad1_EditValueChanged(object sender, EventArgs e)
        {
            if (indicador == 0)
            {
                txtStock1.Text = (txtcantidad1.Text == "0") ? (cantotales[0]).ToString() : (cantotales[0] - Convert.ToInt32(txtcantidad1.Text)).ToString();
                txtStock2.Text = (txtcantidad2.Text == "0") ? (cantotales[1]).ToString() : (cantotales[1] - Convert.ToInt32(txtcantidad2.Text)).ToString();
                txtStock3.Text = (txtcantidad3.Text == "0") ? (cantotales[2]).ToString() : (cantotales[2] - Convert.ToInt32(txtcantidad3.Text)).ToString();
                txtStock4.Text = (txtcantidad4.Text == "0") ? (cantotales[3]).ToString() : (cantotales[3] - Convert.ToInt32(txtcantidad4.Text)).ToString();
                txtStock5.Text = (txtcantidad5.Text == "0") ? (cantotales[4]).ToString() : (cantotales[4] - Convert.ToInt32(txtcantidad5.Text)).ToString();
                txtStock6.Text = (txtcantidad6.Text == "0") ? (cantotales[5]).ToString() : (cantotales[5] - Convert.ToInt32(txtcantidad6.Text)).ToString();
                txtStock7.Text = (txtcantidad7.Text == "0") ? (cantotales[6]).ToString() : (cantotales[6] - Convert.ToInt32(txtcantidad7.Text)).ToString();
                txtStock8.Text = (txtcantidad8.Text == "0") ? (cantotales[7]).ToString() : (cantotales[7] - Convert.ToInt32(txtcantidad8.Text)).ToString();
                txtStock9.Text = (txtcantidad9.Text == "0") ? (cantotales[8]).ToString() : (cantotales[8] - Convert.ToInt32(txtcantidad9.Text)).ToString();
                txtStock10.Text = (txtcantidad10.Text == "0") ? (cantotales[9]).ToString() : (cantotales[9] - Convert.ToInt32(txtcantidad10.Text)).ToString();

                int Suma = 0;
                Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text) +
                Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text) +
                Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text) +
                Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text) +
                Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text) +
                Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text) +
                Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text) +
                Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text) +
                Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text) +
                Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text);
                txtcantidaddetalle.Text = Suma.ToString();
            }
            else if (indicador == 1)
            {
                //int Suma = 0;
                //Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text = (Convert.ToInt32(txtcantidad1.Text) > Convert.ToInt32(txtStock1.Text)) ? "0" : txtcantidad1.Text) +
                //Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text = (Convert.ToInt32(txtcantidad2.Text) > Convert.ToInt32(txtStock2.Text)) ? "0" : txtcantidad2.Text) +
                //Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text = (Convert.ToInt32(txtcantidad3.Text) > Convert.ToInt32(txtStock3.Text)) ? "0" : txtcantidad3.Text) +
                //Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text = (Convert.ToInt32(txtcantidad4.Text) > Convert.ToInt32(txtStock4.Text)) ? "0" : txtcantidad4.Text) +
                //Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text = (Convert.ToInt32(txtcantidad5.Text) > Convert.ToInt32(txtStock5.Text)) ? "0" : txtcantidad5.Text) +
                //Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text = (Convert.ToInt32(txtcantidad6.Text) > Convert.ToInt32(txtStock6.Text)) ? "0" : txtcantidad6.Text) +
                //Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text = (Convert.ToInt32(txtcantidad7.Text) > Convert.ToInt32(txtStock7.Text)) ? "0" : txtcantidad7.Text) +
                //Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text = (Convert.ToInt32(txtcantidad8.Text) > Convert.ToInt32(txtStock8.Text)) ? "0" : txtcantidad8.Text) +
                //Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text = (Convert.ToInt32(txtcantidad9.Text) > Convert.ToInt32(txtStock9.Text)) ? "0" : txtcantidad9.Text) +
                //Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text = (Convert.ToInt32(txtcantidad10.Text) > Convert.ToInt32(txtStock10.Text)) ? "0" : txtcantidad10.Text);
                //txtcantidaddetalle.Text = Suma.ToString();

                int Suma = 0;
                Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text) +
                Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text) +
                Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text) +
                Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text) +
                Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text) +
                Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text) +
                Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text) +
                Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text) +
                Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text) +
                Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text);
                txtcantidaddetalle.Text = Suma.ToString();

            }
            else if (indicador == 3)
            {

                int Suma = 0;
                Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text) +
                Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text) +
                Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text) +
                Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text) +
                Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text) +
                Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text) +
                Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text) +
                Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text) +
                Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text) +
                Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text);
                txtcantidaddetalle.Text = Suma.ToString();
            }
        }
        int[] cantotales = new int[10];
        public void cargarcantidadesOriginales()
        {

            cantotales[0] = Convert.ToInt32(txtcantidad1.Text) + Convert.ToInt32(txtStock1.Text);
            cantotales[1] = Convert.ToInt32(txtcantidad2.Text) + Convert.ToInt32(txtStock2.Text);
            cantotales[2] = Convert.ToInt32(txtcantidad3.Text) + Convert.ToInt32(txtStock3.Text);
            cantotales[3] = Convert.ToInt32(txtcantidad4.Text) + Convert.ToInt32(txtStock4.Text);
            cantotales[4] = Convert.ToInt32(txtcantidad5.Text) + Convert.ToInt32(txtStock5.Text);
            cantotales[5] = Convert.ToInt32(txtcantidad6.Text) + Convert.ToInt32(txtStock6.Text);
            cantotales[6] = Convert.ToInt32(txtcantidad7.Text) + Convert.ToInt32(txtStock7.Text);
            cantotales[7] = Convert.ToInt32(txtcantidad8.Text) + Convert.ToInt32(txtStock8.Text);
            cantotales[8] = Convert.ToInt32(txtcantidad9.Text) + Convert.ToInt32(txtStock9.Text);
            cantotales[9] = Convert.ToInt32(txtcantidad10.Text) + Convert.ToInt32(txtStock10.Text);
        }

        private void txtcantidad1_Leave(object sender, EventArgs e)
        {
            if (indicador == 0)
            {
                int Suma = 0;
                Suma = Convert.ToInt32(txtcantidad1.Text = (Convert.ToInt32(txtcantidad1.Text) > cantotales[0] || Convert.ToInt32(txtcantidad1.Text) < 0) ? "0" : txtcantidad1.Text) +
                Convert.ToInt32(txtcantidad2.Text = (Convert.ToInt32(txtcantidad2.Text) > cantotales[1] || Convert.ToInt32(txtcantidad2.Text) < 0) ? "0" : txtcantidad2.Text) +
                Convert.ToInt32(txtcantidad3.Text = (Convert.ToInt32(txtcantidad3.Text) > cantotales[2] || Convert.ToInt32(txtcantidad3.Text) < 0) ? "0" : txtcantidad3.Text) +
                Convert.ToInt32(txtcantidad4.Text = (Convert.ToInt32(txtcantidad4.Text) > cantotales[3] || Convert.ToInt32(txtcantidad4.Text) < 0) ? "0" : txtcantidad4.Text) +
                Convert.ToInt32(txtcantidad5.Text = (Convert.ToInt32(txtcantidad5.Text) > cantotales[4] || Convert.ToInt32(txtcantidad5.Text) < 0) ? "0" : txtcantidad5.Text) +
                Convert.ToInt32(txtcantidad6.Text = (Convert.ToInt32(txtcantidad6.Text) > cantotales[5] || Convert.ToInt32(txtcantidad6.Text) < 0) ? "0" : txtcantidad6.Text) +
                Convert.ToInt32(txtcantidad7.Text = (Convert.ToInt32(txtcantidad7.Text) > cantotales[6] || Convert.ToInt32(txtcantidad7.Text) < 0) ? "0" : txtcantidad7.Text) +
                Convert.ToInt32(txtcantidad8.Text = (Convert.ToInt32(txtcantidad8.Text) > cantotales[7] || Convert.ToInt32(txtcantidad8.Text) < 0) ? "0" : txtcantidad8.Text) +
                Convert.ToInt32(txtcantidad9.Text = (Convert.ToInt32(txtcantidad9.Text) > cantotales[8] || Convert.ToInt32(txtcantidad9.Text) < 0) ? "0" : txtcantidad9.Text) +
                Convert.ToInt32(txtcantidad10.Text = (Convert.ToInt32(txtcantidad10.Text) > cantotales[9] || Convert.ToInt32(txtcantidad10.Text) < 0) ? "0" : txtcantidad10.Text);
                txtcantidaddetalle.Text = Suma.ToString();
            }
            else if (indicador == 1)
            {
                //int Suma = 0;
                //Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text = (Convert.ToInt32(txtcantidad1.Text) > Convert.ToInt32(txtStock1.Text)) ? "0" : txtcantidad1.Text) +
                //Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text = (Convert.ToInt32(txtcantidad2.Text) > Convert.ToInt32(txtStock2.Text)) ? "0" : txtcantidad2.Text) +
                //Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text = (Convert.ToInt32(txtcantidad3.Text) > Convert.ToInt32(txtStock3.Text)) ? "0" : txtcantidad3.Text) +
                //Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text = (Convert.ToInt32(txtcantidad4.Text) > Convert.ToInt32(txtStock4.Text)) ? "0" : txtcantidad4.Text) +
                //Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text = (Convert.ToInt32(txtcantidad5.Text) > Convert.ToInt32(txtStock5.Text)) ? "0" : txtcantidad5.Text) +
                //Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text = (Convert.ToInt32(txtcantidad6.Text) > Convert.ToInt32(txtStock6.Text)) ? "0" : txtcantidad6.Text) +
                //Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text = (Convert.ToInt32(txtcantidad7.Text) > Convert.ToInt32(txtStock7.Text)) ? "0" : txtcantidad7.Text) +
                //Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text = (Convert.ToInt32(txtcantidad8.Text) > Convert.ToInt32(txtStock8.Text)) ? "0" : txtcantidad8.Text) +
                //Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text = (Convert.ToInt32(txtcantidad9.Text) > Convert.ToInt32(txtStock9.Text)) ? "0" : txtcantidad9.Text) +
                //Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text = (Convert.ToInt32(txtcantidad10.Text) > Convert.ToInt32(txtStock10.Text)) ? "0" : txtcantidad10.Text);
                //txtcantidaddetalle.Text = Suma.ToString();
                int Suma = 0;
                Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text) +
                Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text) +
                Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text) +
                Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text) +
                Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text) +
                Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text) +
                Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text) +
                Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text) +
                Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text) +
                Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text);
                txtcantidaddetalle.Text = Suma.ToString();
            }
            else if (indicador == 3)
            {

                int Suma = 0;
                Suma = Convert.ToInt32(txtcantidad1.Text = (txtcantidad1.Text == "") ? "0" : txtcantidad1.Text) +
                Convert.ToInt32(txtcantidad2.Text = (txtcantidad2.Text == "") ? "0" : txtcantidad2.Text) +
                Convert.ToInt32(txtcantidad3.Text = (txtcantidad3.Text == "") ? "0" : txtcantidad3.Text) +
                Convert.ToInt32(txtcantidad4.Text = (txtcantidad4.Text == "") ? "0" : txtcantidad4.Text) +
                Convert.ToInt32(txtcantidad5.Text = (txtcantidad5.Text == "") ? "0" : txtcantidad5.Text) +
                Convert.ToInt32(txtcantidad6.Text = (txtcantidad6.Text == "") ? "0" : txtcantidad6.Text) +
                Convert.ToInt32(txtcantidad7.Text = (txtcantidad7.Text == "") ? "0" : txtcantidad7.Text) +
                Convert.ToInt32(txtcantidad8.Text = (txtcantidad8.Text == "") ? "0" : txtcantidad8.Text) +
                Convert.ToInt32(txtcantidad9.Text = (txtcantidad9.Text == "") ? "0" : txtcantidad9.Text) +
                Convert.ToInt32(txtcantidad10.Text = (txtcantidad10.Text == "") ? "0" : txtcantidad10.Text);
                txtcantidaddetalle.Text = Suma.ToString();
            }
        }

        private void txtprecio_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }
        public void Totalizar()
        {
            decimal ddescuento = 0;
            ddescuento = ((Convert.ToDecimal(txtprecio.Text) * Convert.ToDecimal(txtDescuento.Text)) / 100);
            txtPrecioVenta.Text = ((Convert.ToDecimal(txtprecio.Text) - ddescuento) * Convert.ToDecimal(txtcantidaddetalle.Text)).ToString();
        }

        private void txtcantidaddetalle_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void txtDescuento_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void lkpSerie_EditValueChanged(object sender, EventArgs e)
        {
            List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_iid_registro_serie == Convert.ToInt32(lkpSerie.EditValue)).ToList();
            txtrangotallas.Text = lstSerie[0].resec_vtalla_inicial + "/" + lstSerie[0].resec_vtalla_final;
            if (txtdescripcion.Text != "")
            {

                txtdescripcion.Text = descripcion + lkpSerie.Text;
            }
        }
    }
}