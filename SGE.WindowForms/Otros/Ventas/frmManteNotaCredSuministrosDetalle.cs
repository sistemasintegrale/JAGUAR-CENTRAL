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
    public partial class frmManteNotaCredSuministrosDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<ENotaCreditoSuministrosDet> lstDetalle = new List<ENotaCreditoSuministrosDet>();
        public ENotaCreditoSuministrosDet oBe = new ENotaCreditoSuministrosDet();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public Boolean flag_Arrox;
        public decimal valor_IGV;
        /*Datos IVAP*/
        public Boolean flag_afecto_ivap;
        public string PorIVAP;
        public decimal PorcentajeIVAP;
        /*Datos IGV*/
        public Boolean flag_afecto_igv;
        public string PorIGV;
        public decimal PorcentajeIGV;
        public string CodigoSUNAT;
        /**/
        public Boolean flag_exonerado;
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
        public frmManteNotaCredSuministrosDetalle()
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
            txtPrecio.Text = oBe.dcrec_nmonto_unitario.ToString();
            txtDPorImpArroz.Text = "0";
            //txtPorIGV.Text = oBe.dcrec_nporcentaje_impuesto.ToString();

            #region Factura Electronica Detalle
            oBe.NumeroOrdenItem = oBe.dcrec_inro_item;
            oBe.cantidad = oBe.dcrec_ncantidad_producto;
            oBe.unidadMedida = CodigoSUNAT;
            oBe.ValorVentaItem = oBe.dcrec_nmonto_item;
            oBe.CodMotivoDescuentoItem = 0;
            oBe.FactorDescuentoItem = 0;
            oBe.DescuentoItem = 0;
            oBe.BaseDescuentotem = 0;
            oBe.CodMotivoCargoItem = 0;
            oBe.FactorCargoItem = 0;
            oBe.MontoCargoItem = 0;
            oBe.BaseCargoItem = 0;
            oBe.MontoTotalImpuestosItem = oBe.dcrec_nmonto_impuesto;
            oBe.MontoImpuestoIgvItem = oBe.dcrec_nmonto_impuesto;
            oBe.MontoAfectoImpuestoIgv = oBe.dcrec_nneto_igv;

            //if (oBe.dcrec_nmonto_impuesto == 0 && oBe.dcrec_nmonto_imp_arroz == 0)
            //{
            //    //obe.MontoAfectoImpuestoIgv = obe.favd_nneto_igv;
            //    oBe.MontoInafectoItem = oBe.dcrec_nmonto_item;
            //    oBe.MontoAfectoImpuestoIgv = 0;
            //}
            //else
            //{
            //    oBe.MontoInafectoItem = 0;
            //    oBe.MontoAfectoImpuestoIgv = oBe.dcrec_nneto_igv;
            //}



            oBe.PorcentajeIGVItem = Convert.ToDecimal(Parametros.strPorcIGV);
            oBe.MontoImpuestoISCItem = 0;
            oBe.MontoAfectoImpuestoIsc = 0;
            oBe.PorcentajeISCtem = 0;
            oBe.MontoImpuestoIVAPtem = 0;
            oBe.MontoAfectoImpuestoIVAPItem = 0;
            oBe.PorcentajeIVAPItem = 0;
            oBe.descripcion = txtProducto.Text;
            oBe.codigoItem = oBe.strCodProducto;
            oBe.ObservacionesItem = "";
            oBe.ValorUnitarioItem = oBe.dcrec_nmonto_unitario;
            //if (Convert.ToBoolean(flag_afecto_ivap) == true)//tiene IVA
            //{
            //    oBe.tipoImpuesto = "17";
            //}
            //else if (Convert.ToBoolean(flag_afecto_igv) == true)//tiene IGV
            //{
            //    oBe.tipoImpuesto = "10";
            //}
            //else
            //{
            //    oBe.tipoImpuesto = "30";
            //}
            #endregion



            //string[] partes = partes = oBe.dcrec_vdescripcion.Split('@');
            //txtObservaciones.Lines = partes;
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
                    if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                    {
                        oBase = bteProducto;
                        throw new ArgumentException("El producto seleccionado ya se encuentra en el detalle de la nota de crédito");
                    }
                }
                if (flag_Arrox != flag_afecto_ivap)
                {
                    throw new ArgumentException("El producto no puede ser agregado, ya que no continen IGV");
                }
                PorIVAP = txtDPorImpArroz.Text;
                oBe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                oBe.dcrec_inro_item = Convert.ToInt16(txtItem.Text);
                oBe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                oBe.dcrec_ncantidad_producto = Convert.ToDecimal(txtCantidad.Text);
                oBe.dcrec_vdescripcion = txtProducto.Text;
                //oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtPrecio.Text);                     
                //oBe.dcrec_nmonto_item = Math.Round((oBe.dcrec_ncantidad_producto * oBe.dcrec_nmonto_unitario),2);
                //oBe.dcrec_nmonto_impuesto = oBe.dcrec_nmonto_item * Convert.ToDecimal(String.Format("0.{0}", PorIVAP.Replace(".", "")));
                /**/
                oBe.strCodProducto = bteProducto.Text;
                oBe.strDesUM = txtUnidadMedida.Text;
                oBe.strDesProducto = txtProducto.Text;
                oBe.strAlmacen = bteAlmacen.Text;
                oBe.strMoneda = txtMoneda.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.intClasificacion = intClasificacion;




                if (Convert.ToBoolean(flag_afecto_igv) == true)//tiene IGV
                {
                    oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtPrecio.Text);
                    oBe.dcrec_nmonto_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    PorIGV = Parametros.strPorcIGV;
                    oBe.dcrec_nmonto_impuesto = Math.Round((Convert.ToDecimal((oBe.dcrec_nmonto_item * Convert.ToDecimal(Parametros.strPorcIGV)) / (100 + Convert.ToDecimal(Parametros.strPorcIGV)))), 4, MidpointRounding.ToEven);
                    oBe.dcrec_nneto_igv = Math.Round((Convert.ToDecimal(txtPrecioVenta.Text) - oBe.dcrec_nmonto_impuesto), 4);
                }
                else
                {
                    flag_exonerado = true;
                    oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtPrecio.Text);
                    oBe.dcrec_nmonto_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    //oBe.favd_nneto_exo = Convert.ToDecimal(txtPrecioVenta.Text);
                }




                #region Factura Electronica Detalle
                oBe.NumeroOrdenItem = oBe.dcrec_inro_item;
                oBe.cantidad = oBe.dcrec_ncantidad_producto;
                oBe.unidadMedida = CodigoSUNAT;
                oBe.ValorVentaItem = oBe.dcrec_nmonto_item;
                oBe.CodMotivoDescuentoItem = 0;
                oBe.FactorDescuentoItem = 0;
                oBe.DescuentoItem = 0;
                oBe.BaseDescuentotem = 0;
                oBe.CodMotivoCargoItem = 0;
                oBe.FactorCargoItem = 0;
                oBe.MontoCargoItem = 0;
                oBe.BaseCargoItem = 0;
                oBe.MontoTotalImpuestosItem = oBe.dcrec_nmonto_impuesto;
                oBe.MontoImpuestoIgvItem = oBe.dcrec_nmonto_impuesto;
                oBe.MontoAfectoImpuestoIgv = Convert.ToDecimal(txtPrecioVenta.Text) - oBe.dcrec_nmonto_impuesto;
                oBe.PorcentajeIGVItem = Convert.ToDecimal(Parametros.strPorcIGV);
                oBe.MontoImpuestoISCItem = 0;
                oBe.MontoAfectoImpuestoIsc = 0;
                oBe.PorcentajeISCtem = 0;
                oBe.MontoImpuestoIVAPtem = 0;
                oBe.MontoAfectoImpuestoIVAPItem = 0;
                oBe.PorcentajeIVAPItem = 0;
                oBe.descripcion = oBe.dcrec_vdescripcion;
                oBe.codigoItem = oBe.strCodProducto;
                oBe.ObservacionesItem = "";
                oBe.ValorUnitarioItem = Math.Round((oBe.MontoAfectoImpuestoIgv / oBe.dcrec_ncantidad_producto), 4);
                oBe.UMedida = oBe.strDesUM;
                #endregion







                //string Descripci = "";
                //string DescripciExtra = "";
                //string[] arraye = txtObservaciones.Lines;
                //for (int i = 0; i < arraye.Length; i++)
                //{
                //    Descripci = Descripci + arraye[i] + "@";
                //    if (arraye[i] != "")
                //        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                //}
                oBe.dcrec_vdescripcion = "";


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
            using (frmListarAlmacen frm = new frmListarAlmacen())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = frm._Be.almac_icod_almacen;
                    bteAlmacen.Text = frm._Be.almac_vdescripcion;
                }
            }
        }
        public int intClasificacion;
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
                using (frmListarProducto frm = new frmListarProducto())
                {
                    frm.flag_solo_prods = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = frm._Be.prdc_icod_producto;
                        bteProducto.Text = frm._Be.prdc_vcode_producto;
                        txtProducto.Text = frm._Be.prdc_vdescripcion_larga;
                        txtUnidadMedida.Text = frm._Be.StrUnidadMedida;
                        /*Datos IVAP*/
                        //txtDPorImpArroz.Text = frm._Be.PorcentajeIVAP.ToString();
                        //PorcentajeIVAP = frm._Be.PorcentajeIVAP;
                        //flag_afecto_ivap = frm._Be.AfectoIVAP;
                        /*Datos IGV*/
                        //txtPorIGV.Text = frm._Be.prdc_nporcentaje_igv.ToString();
                        //PorcentajeIGV = frm._Be.prdc_nporcentaje_igv;
                        //flag_afecto_igv = frm._Be.prdc_afecto_igv;
                        /**/
                        oBe.strDesUM = frm._Be.StrUnidadMedida;
                        oBe.strLinea = frm._Be.strCategoria;
                        oBe.strSubLinea = frm._Be.strSubCategoriaUno;
                        CodigoSUNAT = frm._Be.CodigoSUNAT;
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

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void groupControl1_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }

        private void txtPrecio_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }
        public void Totalizar()
        {
            txtPrecioVenta.Text = ((Convert.ToDecimal(txtPrecio.Text)) * Convert.ToDecimal(txtCantidad.Text)).ToString();
        }
    }
}