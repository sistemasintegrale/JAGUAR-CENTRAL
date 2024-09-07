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
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteNotaCredDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<ENotaCreditoDet> lstDetalle = new List<ENotaCreditoDet>();
        public ENotaCreditoDet oBe = new ENotaCreditoDet();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
      
        public string descripcion { get; set; }
        public string porcentaje_igv;
        public Boolean flag_afecto_igv;
        public Boolean flag_exonerado;
        public string CodigoSUNAT { get; set; }
        public string PorIGV { get; set; }
        public List<EProdProducto> oProducto;
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
                lkpSerie.Enabled = Enabled;
                txtcantidad1.Enabled = true;
                txtcantidad2.Enabled = true;
                txtcantidad3.Enabled = true;
                txtcantidad4.Enabled = true;
                txtcantidad5.Enabled = true;
                txtcantidad6.Enabled = true;
                txtcantidad7.Enabled = true;
                txtcantidad8.Enabled = true;
                txtcantidad9.Enabled = true;
                txtcantidad10.Enabled = true;
                btnGenerar.Enabled = false;
                txtCantidad.Enabled = Enabled;
                txtPrecioVenta.Enabled = Enabled;
                txtrangotallas.Enabled = Enabled;
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
        public frmManteNotaCredDetalle()
        {
            InitializeComponent();
        }

        private void setValues()
        {
            bteAlmacen.Tag = oBe.almac_icod_almacen;
            bteAlmacen.Text = oBe.strAlmacen;
            bteProducto.Tag = oBe.prdc_icod_producto;
            bteProducto.Text = oBe.strCodProducto;
            txtProducto.Text = oBe.strDesProducto;
            txtCantidad.Text = oBe.dcrec_ncantidad_producto.ToString();
            txtUnidadMedida.Text = oBe.strDesUM;
            txtPrecioUnitario.Text = oBe.dcrec_nmonto_unitario.ToString();

            bteProducto.Text = oBe.dcrec_vcodigo_extremo_prod;
            //txtCodigo.Text = oBe.dremc_icodigo.ToString();
            //txtCantidad.Text = oBe.dremc_ncantidad_producto.ToString();
            descripcion = oBe.dcrec_vdescripcion;
            txtProducto.Text = oBe.dcrec_vdescripcion;
            lkpSerie.EditValue = oBe.dcrec_icod_serie;
            CodigoSUNAT = oBe.CodigoSUNAT;


            txttalla1.Text = string.Format("{0:00}", oBe.dcrec_talla1);
            txttalla2.Text = string.Format("{0:00}", oBe.dcrec_talla2);
            txttalla3.Text = string.Format("{0:00}", oBe.dcrec_talla3);
            txttalla4.Text = string.Format("{0:00}", oBe.dcrec_talla4);
            txttalla5.Text = string.Format("{0:00}", oBe.dcrec_talla5);
            txttalla6.Text = string.Format("{0:00}", oBe.dcrec_talla6);
            txttalla7.Text = string.Format("{0:00}", oBe.dcrec_talla7);
            txttalla8.Text = string.Format("{0:00}", oBe.dcrec_talla8);
            txttalla9.Text = string.Format("{0:00}", oBe.dcrec_talla9);
            txttalla10.Text = string.Format("{0:00}", oBe.dcrec_talla10);
            txtcantidad1.Text = oBe.dcrec_cant_talla1.ToString();
            txtcantidad2.Text = oBe.dcrec_cant_talla2.ToString();
            txtcantidad3.Text = oBe.dcrec_cant_talla3.ToString();
            txtcantidad4.Text = oBe.dcrec_cant_talla4.ToString();
            txtcantidad5.Text = oBe.dcrec_cant_talla5.ToString();
            txtcantidad6.Text = oBe.dcrec_cant_talla6.ToString();
            txtcantidad7.Text = oBe.dcrec_cant_talla7.ToString();
            txtcantidad8.Text = oBe.dcrec_cant_talla8.ToString();
            txtcantidad9.Text = oBe.dcrec_cant_talla9.ToString();
            txtcantidad10.Text = oBe.dcrec_cant_talla10.ToString();
            icodProducto[0] = Convert.ToInt32(oBe.dcrec_icod_producto1);
            icodProducto[1] = Convert.ToInt32(oBe.dcrec_icod_producto2);
            icodProducto[2] = Convert.ToInt32(oBe.dcrec_icod_producto3);
            icodProducto[3] = Convert.ToInt32(oBe.dcrec_icod_producto4);
            icodProducto[4] = Convert.ToInt32(oBe.dcrec_icod_producto5);
            icodProducto[5] = Convert.ToInt32(oBe.dcrec_icod_producto6);
            icodProducto[6] = Convert.ToInt32(oBe.dcrec_icod_producto7);
            icodProducto[7] = Convert.ToInt32(oBe.dcrec_icod_producto8);
            icodProducto[8] = Convert.ToInt32(oBe.dcrec_icod_producto9);
            icodProducto[9] = Convert.ToInt32(oBe.dcrec_icod_producto10);



            //string[] partes = partes = oBe.dcrec_vdescripcion.Split('@');
            //txtObservaciones.Lines = partes;
        }

        private void frmManteFacturaDetalle_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSerie, new BCentral().ListarRegistroSerie(), "resec_vdescripcion", "resec_iid_registro_serie", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpSerie.EditValue = oBe.dcrec_icod_serie;
                txtrangotallas.Text = oBe.dcrec_rango_talla;
                //txtitem.Text = Convert.ToString(_BE.favd_iitem_factura);
                lkpSerie.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
                setAlmacen();
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
        public void Redimencionarstock()
        {
            List<EProdProducto> ostockproducto = new List<EProdProducto>();
            TextEdit[] textoCantidad = GetTextoCantidad();
            TextEdit[] textoTalla = GetTextoTalla();
            //TextEdit[] textostock = GetTextoCantidadStock();


            for (int x = 0; x <= 9; x++)
            {

                string codigoexterno = bteProducto.Text + textoTalla[x].Text;
                oProducto = new BCentral().VerificarProductoPvt(codigoexterno);
                if (oProducto.Count > 0)
                {
                    ostockproducto = new BCentral().VerificarProdStockProducto(oProducto[0].pr_icod_producto, Convert.ToInt32(bteAlmacen.Tag), Parametros.intEjercicio);
                }

                if (textoTalla[x].ToString() != "00")
                    textoCantidad[x].Enabled = true;
                else
                    textoCantidad[x].Enabled = false;

                oProducto.Clear();
                ostockproducto.Clear();
            }
            bteProducto.Enabled = false;
            lkpSerie.Enabled = false;

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

                if (Convert.ToDecimal(txtPrecioUnitario.Text) <= 0)
                {
                    oBase = txtPrecioUnitario;
                    throw new ArgumentException("El precio unitario debe ser mayor a 0");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                    {
                        oBase = bteProducto;
                        throw new ArgumentException("El producto seleccionado ya se encuentra en el detalle de la nota de crédito");
                    }
                }

                TextEdit[] textoCantidad = GetTextoCantidad();
                TextEdit[] textoTalla = GetTextoTalla();

                oBe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                oBe.dcrec_inro_item = Convert.ToInt16(txtItem.Text);
                oBe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                oBe.dcrec_ncantidad_producto = Convert.ToDecimal(txtCantidad.Text);
                oBe.dcrec_vcodigo_extremo_prod = bteProducto.Text;
                oBe.dcrec_vdescripcion = txtProducto.Text;
                oBe.dcrec_icod_serie = Convert.ToInt32(lkpSerie.EditValue);
                oBe.dcrec_rango_talla = txtrangotallas.Text;
                oBe.Serie = lkpSerie.Text;

                //oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtPrecio.Text);                     
                //oBe.dcrec_nmonto_item = Math.Round((oBe.dcrec_ncantidad_producto * oBe.dcrec_nmonto_unitario),2);                
                //oBe.dcrec_nmonto_impuesto = oBe.dcrec_nmonto_item * Convert.ToDecimal(String.Format("0.{0}", Parametros.strPorcIGV.Replace(".", "")));
                /**/
                oBe.strCodProducto = bteProducto.Text;
                oBe.strDesUM = txtUnidadMedida.Text;
                oBe.strDesProducto = txtProducto.Text;
                oBe.strAlmacen = bteAlmacen.Text;
                oBe.strMoneda = txtMoneda.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.intClasificacion = intClasificacion;

                oBe.dcrec_talla1 = Convert.ToInt32((textoTalla[0].Text == "") ? null : textoTalla[0].Text);
                oBe.dcrec_talla2 = Convert.ToInt32((textoTalla[1].Text == "") ? null : textoTalla[1].Text);
                oBe.dcrec_talla3 = Convert.ToInt32((textoTalla[2].Text == "") ? null : textoTalla[2].Text);
                oBe.dcrec_talla4 = Convert.ToInt32((textoTalla[3].Text == "") ? null : textoTalla[3].Text);
                oBe.dcrec_talla5 = Convert.ToInt32((textoTalla[4].Text == "") ? null : textoTalla[4].Text);
                oBe.dcrec_talla6 = Convert.ToInt32((textoTalla[5].Text == "") ? null : textoTalla[5].Text);
                oBe.dcrec_talla7 = Convert.ToInt32((textoTalla[6].Text == "") ? null : textoTalla[6].Text);
                oBe.dcrec_talla8 = Convert.ToInt32((textoTalla[7].Text == "") ? null : textoTalla[7].Text);
                oBe.dcrec_talla9 = Convert.ToInt32((textoTalla[8].Text == "") ? null : textoTalla[8].Text);
                oBe.dcrec_talla10 = Convert.ToInt32((textoTalla[9].Text == "") ? null : textoTalla[9].Text);
                oBe.dcrec_cant_talla1 = Convert.ToInt32((textoCantidad[0].Text == "") ? null : textoCantidad[0].Text);
                oBe.dcrec_cant_talla2 = Convert.ToInt32((textoCantidad[1].Text == "") ? null : textoCantidad[1].Text);
                oBe.dcrec_cant_talla3 = Convert.ToInt32((textoCantidad[2].Text == "") ? null : textoCantidad[2].Text);
                oBe.dcrec_cant_talla4 = Convert.ToInt32((textoCantidad[3].Text == "") ? null : textoCantidad[3].Text);
                oBe.dcrec_cant_talla5 = Convert.ToInt32((textoCantidad[4].Text == "") ? null : textoCantidad[4].Text);
                oBe.dcrec_cant_talla6 = Convert.ToInt32((textoCantidad[5].Text == "") ? null : textoCantidad[5].Text);
                oBe.dcrec_cant_talla7 = Convert.ToInt32((textoCantidad[6].Text == "") ? null : textoCantidad[6].Text);
                oBe.dcrec_cant_talla8 = Convert.ToInt32((textoCantidad[7].Text == "") ? null : textoCantidad[7].Text);
                oBe.dcrec_cant_talla9 = Convert.ToInt32((textoCantidad[8].Text == "") ? null : textoCantidad[8].Text);
                oBe.dcrec_cant_talla10 = Convert.ToInt32((textoCantidad[9].Text == "") ? null : textoCantidad[9].Text);
                oBe.dcrec_icod_producto1 = icodProducto[0];
                oBe.dcrec_icod_producto2 = icodProducto[1];
                oBe.dcrec_icod_producto3 = icodProducto[2];
                oBe.dcrec_icod_producto4 = icodProducto[3];
                oBe.dcrec_icod_producto5 = icodProducto[4];
                oBe.dcrec_icod_producto6 = icodProducto[5];
                oBe.dcrec_icod_producto7 = icodProducto[6];
                oBe.dcrec_icod_producto8 = icodProducto[7];
                oBe.dcrec_icod_producto9 = icodProducto[8];
                oBe.dcrec_icod_producto10 = icodProducto[9];

                if (Convert.ToBoolean(flag_afecto_igv) == true)//tiene IGV
                {
                    oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtPrecioUnitario.Text);
                    oBe.dcrec_nmonto_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    PorIGV = Parametros.strPorcIGV;
                    oBe.dcrec_nmonto_impuesto = Math.Round((Convert.ToDecimal((oBe.dcrec_nmonto_item * Convert.ToDecimal(Parametros.strPorcIGV)) / (100 + Convert.ToDecimal(Parametros.strPorcIGV)))), 4, MidpointRounding.ToEven);
                    oBe.dcrec_nneto_igv = Math.Round((Convert.ToDecimal(txtPrecioVenta.Text) - oBe.dcrec_nmonto_impuesto), 4);
                }
                else
                {
                    flag_exonerado = true;
                    oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtPrecioUnitario.Text);
                    oBe.dcrec_nmonto_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    //oBe.favd_nneto_exo = Convert.ToDecimal(txtPrecioVenta.Text);
                }

                oBe.operacion = Enums.Operacion.Modificar;
                #region Factura Electronica Detalle
                oBe.NumeroOrdenItem = oBe.dcrec_inro_item;
                oBe.cantidad = oBe.dcrec_ncantidad_producto;
                oBe.unidadMedida = CodigoSUNAT;
                oBe.ValorVentaItem = oBe.dcrec_nneto_igv;
                oBe.CodMotivoDescuentoItem = 0;
                oBe.FactorDescuentoItem = 0;
                oBe.DescuentoItem = 0;
                oBe.BaseDescuentotem = 0;
                oBe.CodMotivoCargoItem = 0;
                oBe.FactorCargoItem = 0;
                oBe.MontoCargoItem = 0;
                oBe.BaseCargoItem = 0;
                oBe.MontoTotalImpuestosItem = Math.Round(oBe.dcrec_nmonto_impuesto, 2);
                oBe.MontoImpuestoIgvItem = Math.Round(oBe.dcrec_nmonto_impuesto, 2);
                oBe.MontoAfectoImpuestoIgv = oBe.dcrec_nneto_igv;
                oBe.PorcentajeIGVItem = Convert.ToDecimal(Parametros.strPorcIGV);
                oBe.MontoImpuestoISCItem = 0;
                oBe.MontoAfectoImpuestoIsc = 0;
                oBe.PorcentajeISCtem = 0;
                oBe.MontoImpuestoIVAPtem = 0;
                oBe.MontoAfectoImpuestoIVAPItem = 0;
                oBe.PorcentajeIVAPItem = 0;
                oBe.descripcion = oBe.dcrec_vdescripcion;
                oBe.codigoItem = oBe.dcrec_vcodigo_extremo_prod;
                oBe.ObservacionesItem = "";
                oBe.ValorUnitarioItem = Math.Round((oBe.MontoAfectoImpuestoIgv / oBe.dcrec_ncantidad_producto), 4);
                oBe.UMedida = oBe.strDesUM;
                #endregion

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                }
                else
                {
                    if (oBe.intTipoOperacion != 1)
                        oBe.intTipoOperacion = 2;
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
            //ButtonEdit texto = (ButtonEdit)sender;
            //if (texto.Name == "btncodigoalmacen")
            //{
            using (FrmListarAlmacen Almacen = new FrmListarAlmacen())
            {
                Almacen.puvec_icod_punto_venta = 2;
                if (Almacen.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = Almacen._Be.id_almacen;
                    //btncodigoalmacen.Text = Almacen._Be.idf_almacen;
                    bteAlmacen.Text = Almacen._Be.descripcion;
                    //GenerarCodigo();
                }
            }
            //}
        }
        public int intClasificacion;
        private void listarProducto()
        {
            using (FrmListarProducto frm = new FrmListarProducto())
            {
                //frm.stocc_iid_almacen = Convert.ToInt32(bteAlmacen.Tag);
                //frm.puvec_icod_punto_venta = 2;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteProducto.Tag = frm._Be.pr_icod_producto;
                    bteProducto.Text = frm._Be.pr_vcodigo_externo.Substring(0, frm._Be.pr_vcodigo_externo.Length - 2);
                    txtProducto.Text = frm._Be.pr_vdescripcion_producto.Substring(0, frm._Be.pr_vdescripcion_producto.Length - 2) + lkpSerie.Text;
                    descripcion = frm._Be.pr_vdescripcion_producto.Substring(0, frm._Be.pr_vdescripcion_producto.Length - 2);
                    flag_afecto_igv = frm._Be.pr_afecto_igv;
                    CodigoSUNAT = frm._Be.CodigoSUNAT;
                    txtUnidadMedida.Text = frm._Be.strUM;
                }
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

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void groupControl1_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }
        public int[] icodProducto = new int[10]; //---tener los icod de todos los productos
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_iid_registro_serie == Convert.ToInt32(lkpSerie.EditValue)).ToList();
            if (lstSerie[0].resec_vtalla_inicial != "0" && lstSerie[0].resec_vtalla_final != "0")
            {
                List<EProdProducto> ostockproducto = new List<EProdProducto>();


                if ((bteProducto.Tag) == null)
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
                    //TextEdit[] textoStock = GetTextoCantidadStock();
                    for (int x = Convert.ToInt32(lstSerie[0].resec_vtalla_inicial); x <= Convert.ToInt32(lstSerie[0].resec_vtalla_final); x++)
                    {
                        i++;
                        textoTalla[i].Text = string.Format("{0:00}", x).ToString();
                        textoCantidad[i].Text = "0";

                        string codigoexterno = bteProducto.Text + textoTalla[i].Text;
                        oProducto = new BCentral().VerificarProductoPvt(codigoexterno);
                        if (oProducto.Count > 0)
                        {
                            ostockproducto = new BCentral().VerificarProdStockProducto(oProducto[0].pr_icod_producto, Convert.ToInt32(bteAlmacen.Tag), Parametros.intEjercicio);
                        }

                        if (oProducto.Count > 0)
                        {
                            icodProducto[i] = oProducto[0].pr_icod_producto;
                            textoCantidad[i].Enabled = true;
                        }
                        else
                        {
                            textoCantidad[i].Enabled = false;
                        }

                    }
                    lkpSerie.Enabled = false;
                    bteProducto.Enabled = false;
                }
            }
            else
            {
                XtraMessageBox.Show("Ingrese Serie", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private TextEdit[] GetTextoCantidad()
        {
            TextEdit[] texto = new TextEdit[] { txtcantidad1, txtcantidad2, txtcantidad3, txtcantidad4,
                            txtcantidad5, txtcantidad6, txtcantidad7, txtcantidad8, txtcantidad9, txtcantidad10};
            return texto;
        }
        //private TextEdit[] GetTextoCantidadStock()
        //{
        //    TextEdit[] texto = new TextEdit[] { txtStock1, txtStock2, txtStock3 , txtStock4,
        //                    txtStock5, txtStock6, txtStock7, txtStock8, txtStock9, txtStock10};
        //    return texto;
        //}

        private TextEdit[] GetTextoTalla()
        {
            TextEdit[] texto = new TextEdit[] { txttalla1, txttalla2, txttalla3, txttalla4,
                            txttalla5, txttalla6, txttalla7, txttalla8, txttalla9, txttalla10};
            return texto;
        }
        public void Totalizar()
        {
            txtPrecioVenta.Text = ((Convert.ToDecimal(txtPrecioUnitario.Text)) * Convert.ToDecimal(txtCantidad.Text)).ToString();
        }

        private void txtPrecio_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void lkpSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(x => x.resec_iid_registro_serie == Convert.ToInt32(lkpSerie.EditValue)).ToList();
                txtrangotallas.Text = lstSerie[0].resec_vtalla_inicial + "/" + lstSerie[0].resec_vtalla_final;
                if (txtProducto.Text != "")
                {

                    txtProducto.Text = descripcion + lkpSerie.Text;
                }
            }
        }

        private void txtcantidad1_EditValueChanged(object sender, EventArgs e)
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
            txtCantidad.Text = Suma.ToString();
            Totalizar();

        }
        int[] cantotales = new int[10];
        public void cargarcantidadesOriginales()
        {

            //cantotales[0] = Convert.ToInt32(txtcantidad1.Text) + Convert.ToInt32(txtStock1.Text);
            //cantotales[1] = Convert.ToInt32(txtcantidad2.Text) + Convert.ToInt32(txtStock2.Text);
            //cantotales[2] = Convert.ToInt32(txtcantidad3.Text) + Convert.ToInt32(txtStock3.Text);
            //cantotales[3] = Convert.ToInt32(txtcantidad4.Text) + Convert.ToInt32(txtStock4.Text);
            //cantotales[4] = Convert.ToInt32(txtcantidad5.Text) + Convert.ToInt32(txtStock5.Text);
            //cantotales[5] = Convert.ToInt32(txtcantidad6.Text) + Convert.ToInt32(txtStock6.Text);
            //cantotales[6] = Convert.ToInt32(txtcantidad7.Text) + Convert.ToInt32(txtStock7.Text);
            //cantotales[7] = Convert.ToInt32(txtcantidad8.Text) + Convert.ToInt32(txtStock8.Text);
            //cantotales[8] = Convert.ToInt32(txtcantidad9.Text) + Convert.ToInt32(txtStock9.Text);
            //cantotales[9] = Convert.ToInt32(txtcantidad10.Text) + Convert.ToInt32(txtStock10.Text);
        }

        private void txtcantidad1_Leave(object sender, EventArgs e)
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
            txtCantidad.Text = Suma.ToString();
            Totalizar();

        }

        private void txtPrecioUnitario_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }
    }
}