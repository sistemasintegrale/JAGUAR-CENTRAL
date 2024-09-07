using DevExpress.XtraEditors;
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
    public partial class frmManteNotaDebitoDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<ENotaDebitoDet> lstDetalle = new List<ENotaDebitoDet>();
        public ENotaDebitoDet oBe = new ENotaDebitoDet();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public int indicador;
        public string descripcion { get; set; }
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
                //bteAlmacen.Enabled = Enabled;
                //bteProducto.Enabled = Enabled;
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
        public frmManteNotaDebitoDetalle()
        {
            InitializeComponent();
        }

        private void setValues()
        {
            txtItem.Text = oBe.dcrec_inro_item.ToString();
            txtCantidad.Text = oBe.dcrec_ncantidad_producto.ToString();
            txtPrecio.Text = oBe.dcrec_nmonto_unitario.ToString();
            txtPrecioVenta.Text = oBe.dcrec_nmonto_item.ToString();
            //string[] partes = partes = oBe.dcrec_vdescripcion.Split('@');
            txtObservaciones.Text = oBe.dcrec_vdescripcion;



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



                oBe.dcrec_inro_item = Convert.ToInt16(txtItem.Text);
                oBe.dcrec_ncantidad_producto = Convert.ToDecimal(txtCantidad.Text);
                //oBe.dcrec_vdescripcion = txtProducto.Text;


                //oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtPrecio.Text);
                //oBe.dcrec_nmonto_item = Math.Round((oBe.dcrec_ncantidad_producto * oBe.dcrec_nmonto_unitario), 2);
                //oBe.dcrec_nmonto_impuesto = oBe.dcrec_nmonto_item * Convert.ToDecimal(String.Format("0.{0}", Parametros.strPorcIGV.Replace(".", "")));

                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.intClasificacion = intClasificacion;

                //string Descripci = "";
                //string DescripciExtra = "";
                //string[] arraye = txtObservaciones.Lines;
                //for (int i = 0; i < arraye.Length; i++)
                //{
                //    Descripci = Descripci + arraye[i] + "@";
                //    if (arraye[i] != "")
                //        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                //}


                oBe.dcrec_vdescripcion = txtObservaciones.Text;




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
                oBe.unidadMedida = "ZZ";
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
                oBe.PorcentajeIGVItem = Convert.ToDecimal(Parametros.strPorcIGV);
                oBe.MontoImpuestoISCItem = 0;
                oBe.MontoAfectoImpuestoIsc = 0;
                oBe.PorcentajeISCtem = 0;
                oBe.MontoImpuestoIVAPtem = 0;
                oBe.MontoAfectoImpuestoIVAPItem = 0;
                oBe.PorcentajeIVAPItem = 0;
                oBe.descripcion = oBe.dcrec_vdescripcion;
                oBe.codigoItem = oBe.dcrec_inro_item.ToString();
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


        public int intClasificacion;


        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
        }

        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

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




        public void Totalizar()
        {
            txtPrecioVenta.Text = ((Convert.ToDecimal(txtPrecio.Text)) * Convert.ToDecimal(txtCantidad.Text)).ToString();
        }

        private void txtPrecio_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }




        int[] cantotales = new int[10];
        //public void cargarcantidadesOriginales()
        //{

        //    cantotales[0] = Convert.ToInt32(txtcantidad1.Text) + Convert.ToInt32(txtStock1.Text);
        //    cantotales[1] = Convert.ToInt32(txtcantidad2.Text) + Convert.ToInt32(txtStock2.Text);
        //    cantotales[2] = Convert.ToInt32(txtcantidad3.Text) + Convert.ToInt32(txtStock3.Text);
        //    cantotales[3] = Convert.ToInt32(txtcantidad4.Text) + Convert.ToInt32(txtStock4.Text);
        //    cantotales[4] = Convert.ToInt32(txtcantidad5.Text) + Convert.ToInt32(txtStock5.Text);
        //    cantotales[5] = Convert.ToInt32(txtcantidad6.Text) + Convert.ToInt32(txtStock6.Text);
        //    cantotales[6] = Convert.ToInt32(txtcantidad7.Text) + Convert.ToInt32(txtStock7.Text);
        //    cantotales[7] = Convert.ToInt32(txtcantidad8.Text) + Convert.ToInt32(txtStock8.Text);
        //    cantotales[8] = Convert.ToInt32(txtcantidad9.Text) + Convert.ToInt32(txtStock9.Text);
        //    cantotales[9] = Convert.ToInt32(txtcantidad10.Text) + Convert.ToInt32(txtStock10.Text);
        //}


    }
}