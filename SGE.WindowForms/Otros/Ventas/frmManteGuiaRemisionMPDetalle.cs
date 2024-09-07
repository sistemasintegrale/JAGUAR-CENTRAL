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
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteGuiaRemisionMPDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EGuiaRemisionMPDet> lstDetalle = new List<EGuiaRemisionMPDet>();
        public EGuiaRemisionMPDet oBe = new EGuiaRemisionMPDet();
        public EGuiaRemision oBeCab = new EGuiaRemision();
        public int IcodAlmacen = 0;
        public int icodUM;
        List<EStock> lstStockProductos = new List<EStock>();
        public int intmoneda = 0;
        //public int intAlmacen = 0;
        public string DesProd = "";
        public string DesProd2 = "";
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public string UmSunat;

        public frmManteGuiaRemisionMPDetalle()
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
            bteProducto.Tag = oBe.prdc_icod_producto;
            bteProducto.Text = oBe.strCodProducto;
            txtCantidad.Text = oBe.dremc_ncantidad_producto.ToString();
            txtUnidadMedida.Text = oBe.strDesUM;
            //string[] partes = partes = oBe.dremc_vobservaciones.Split('@');
            //txtObservaciones.Lines = partes;
            oBe.dblStockDisponible = oBe.dblStockDisponible;
            DesProd2 = oBe.strCodProducto;
            icodUM = oBe.unidc_icod_unidad_medida;
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

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                    {
                        oBase = bteProducto;
                        throw new ArgumentException("El producto seleccionado ya se encuentra en el detalle del documento");
                    }
                }

                if (Convert.ToDecimal(txtCantidad.Text) <= 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La cantidad debe ser mayor a 0");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    //if (Convert.ToDecimal(txtCantidad.Text) > oBe.dblStockDisponible)
                    //{
                    //    oBase = txtCantidad;
                    //    throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                    //}
                }
                else
                {
                    if (oBe.intTipoOperacion != 1)
                    {
                        //oBe.dblStockDisponible = oBe.dblStockDisponible + oBe.dremc_ncantidad_producto;
                        //if (Convert.ToDecimal(txtCantidad.Text) > oBe.dblStockDisponible)
                        //{
                        //    oBase = txtCantidad;
                        //    throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                        //}
                    }

                    //if (Convert.ToDecimal(txtCantidad.Text) > oBe.dblStockDisponible)
                    //{
                    //    oBase = txtCantidad;
                    //    throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                    //}
                }
                if (oBe.intTipoOperacion != 1)
                {
                    oBe.dblStockDisponible = oBe.dblStockDisponible + oBe.dremc_ncantidad_producto - Convert.ToDecimal(txtCantidad.Text);
                }
                oBe.dremc_inro_item = Convert.ToInt16(txtItem.Text);
                oBe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                oBe.dremc_ncantidad_producto = Convert.ToDecimal(txtCantidad.Text);
                oBe.dremc_itipo_producto = (int)TipoProducto.Mercadería;
                oBe.strCodProducto = DesProd2;
                oBe.strDesUM = txtUnidadMedida.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.unidc_icod_unidad_medida = icodUM;

                string Descripci = "";
                string DescripciExtra = "";
                string[] arraye = txtObservaciones.Lines;
                for (int i = 0; i < arraye.Length; i++)
                {
                    Descripci = Descripci + arraye[i] + "";
                    if (arraye[i] != "")
                        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                }

                oBe.dremc_vobservaciones = Descripci;

                #region GuiaRemisionElectronicaDet
                oBe.electronicaDet.UnidadMedida = UmSunat;
                oBe.electronicaDet.Cantidad = Convert.ToDecimal(txtCantidad.Text);
                oBe.electronicaDet.Descripcion = oBe.strDesProducto;
                oBe.electronicaDet.CodigoItem = DesProd2;
                oBe.electronicaDet.Correlativo = oBe.dremc_inro_item;
                oBe.electronicaDet.PesoItem = 0;
                oBe.electronicaDet.UM = txtUnidadMedida.Text;
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


        public decimal dblStockDisponible = 0;
        private void listarProducto()
        {
            BaseEdit oBase = null;
            try
            {

                using (ListarStockPorAlmacenGR frm = new ListarStockPorAlmacenGR())
                {
                    frm.intAlmacen = Convert.ToInt32(oBeCab.almac_icod_almacen);
                    //frm.intAlmacen = IcodAlmacen;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = frm._Be.prdc_icod_producto;
                        bteProducto.Text = frm._Be.strDesProducto;
                        txtUnidadMedida.Text = frm._Be.strDesUM;
                        oBe.strCategoria = frm._Be.strCategoria;
                        oBe.strSubCategoriaUno = frm._Be.strSubCategoriaUno;
                        oBe.dblStockDisponible = frm._Be.stocc_stock_producto;
                        oBe.strDesProducto = frm._Be.strDesProducto;
                        DesProd2 = frm._Be.strCodProducto;
                        icodUM = frm._Be.unidc_icod_unidad_medida;
                        UmSunat = frm._Be.CodigoSUNAT;

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

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void bteProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarProducto();
        }



        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void bteProducto_EditValueChanged(object sender, EventArgs e)
        {
            if (lstStockProductos.Count > 0)
            {
                lstStockProductos = lstStockProductos.Where(ob => ob.strCodProducto == bteProducto.Text).ToList();
            }
        }

        private void frmManteGuiaRemisionDetalle_Load(object sender, EventArgs e)
        {
            lstStockProductos = new BAlmacen().listarStockPorAlmacenOptimizado(Parametros.intEjercicio, Convert.ToInt32(oBeCab.almac_icod_almacen), "", "");
        }

        private void bteProducto_EditValueChanged_1(object sender, EventArgs e)
        {
            //if (Status == BSMaintenanceStatus.CreateNew)
            //{
            //    List<EStock> MlistAux = new List<EStock>();
            //    MlistAux = lstStockProductos.Where(ob => ob.strCodProducto.Trim() == bteProducto.Text.Trim()).ToList();
            //    if (MlistAux.Count == 1)
            //    {
            //        bteProducto.Tag = MlistAux[0].prdc_icod_producto;
            //        bteProducto.Text = MlistAux[0].strDesProducto;;
            //        txtUnidadMedida.Text = MlistAux[0].strDesUM;
            //        oBe.strCategoria = MlistAux[0].strCategoria;
            //        oBe.strSubCategoriaUno = MlistAux[0].strSubCategoriaUno;
            //        oBe.dblStockDisponible = MlistAux[0].stocc_stock_producto;


            //    }
            //    else
            //    {
            //        bteProducto.Tag = 0;
            //        txtCantidad.Text = "0";

            //    }
            //}


        }




    }
}