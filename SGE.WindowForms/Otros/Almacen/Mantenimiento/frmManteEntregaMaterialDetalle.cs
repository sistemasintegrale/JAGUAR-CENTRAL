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

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteEntregaMaterialDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<ERequerimientoMaterialDet> lstRequerimientoMaterialDet = new List<ERequerimientoMaterialDet>();
        public ERequerimientoMaterialDet obe = new ERequerimientoMaterialDet();
        public List<EEntregaMaterialDet> lstEntergaMaterialDet = new List<EEntregaMaterialDet>();
        public EEntregaMaterialDet _obe = new EEntregaMaterialDet();
        public List<EEntregaMaterialDet> lstDelete = new List<EEntregaMaterialDet>();

        List<EStock> lststock = new List<EStock>();
        public string Categoria, SubCategoria;
        public decimal dblStockDisponible = 0;
        public int tipo_moneda = 0;
        public int remic_icod_remision = 0;
        public Boolean afecto;
        public string CodigoSUNAT, strDesUM;
        public int unidc_icod_unidad_medida;
        public int cod_proceso = 0;
        public int icod_almacen = 0;
        public int icod_requerimiento_material = 0;
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
        public frmManteEntregaMaterialDetalle()
        {
            InitializeComponent();
        }

        private void setValues()
        {

            lkpUbicacion.EditValue = obe.rqmad_iubicacion;
            lkpUbicacion.Text = obe.strubicacion;
            bteProducto.Tag = obe.prdc_icod_producto;
            bteProducto.Text = obe.strCodeProducto;
            txtDescProd.Text = obe.strProducto;
            txtUM.Text = obe.strmedida;
            txtDescripcion.Text = obe.rqmad_vmaterial;
            txtCantidadRequerida.Text = obe.rqmad_ncantidad_requerida.ToString();
            txtTotalCantidadEntregada.Text = obe.rqmad_ncantidad_entregada.ToString();
            Stock();
            if (lstEntergaMaterialDet.Count > 0)
            {
                grdMateriales.DataSource = lstEntergaMaterialDet;
                viewMateriales.RefreshData();
            }
            else
            {
                lstEntergaMaterialDet = new BCentral().listarEntregaMaterialDetalle(obe.rqmad_icod_item_requerimiento_material);
                grdMateriales.DataSource = lstEntergaMaterialDet;
                viewMateriales.RefreshData();
            }




        }
        public void Stock()
        {
            lststock = new BAlmacen().listarStockPorAlmacenOptimizado(Parametros.intEjercicio, icod_almacen, "", "").Where(x => x.prdc_icod_producto == obe.prdc_icod_producto).ToList();
            foreach (var objd in lststock)
            {
                txtStock.Text = objd.stocc_stock_producto.ToString();
            }
        }
        public void CargarCombos()
        {
            EAreaUbicacion ob = new EAreaUbicacion();
            ob.arprc_iid_codigo = cod_proceso;
            BSControls.LoaderLook(lkpUbicacion, new BCentral().ListarAreaUbicacion(ob), "descripcion", "arubd_iid_area_ubicacion", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpUbicacion.EditValue = obe.rqmad_iubicacion;
            }

        }
        private void frmManteFacturaDetalle_Load(object sender, EventArgs e)
        {
            CargarCombos();
        }


        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {
                if (Convert.ToDecimal(txtCantidadRequerida.Text) < Convert.ToDecimal(txtTotalCantidadEntregada.Text))
                {

                    oBase = txtTotalCantidadEntregada;
                    throw new ArgumentException("La Cantidad Por Entregar debe ser Menor Igual que la Cantidad Pedida");
                }
                obe.rqmad_iubicacion = Convert.ToInt32(lkpUbicacion.EditValue);
                obe.strubicacion = lkpUbicacion.Text;
                obe.rqmad_vmaterial = txtDescripcion.Text;
                obe.rqmad_ncantidad_requerida = Convert.ToDecimal(txtCantidadRequerida.Text);
                obe.rqmad_ncantidad_entregada = Convert.ToDecimal(txtTotalCantidadEntregada.Text);
                obe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                obe.strCodeProducto = bteProducto.Text;
                obe.strProducto = txtDescProd.Text;
                obe.strmedida = txtUM.Text;
                obe.rqmad_cantidad_saldo = obe.rqmad_ncantidad_requerida - obe.rqmad_ncantidad_entregada;
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.intTipoOperacion = 1;
                    lstRequerimientoMaterialDet.Add(obe);

                }
                else
                {
                    if (obe.intTipoOperacion != 1)
                        obe.intTipoOperacion = 1;
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

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtprecio_EditValueChanged(object sender, EventArgs e)
        {
        }


        private void txtDescuento_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void bteProducto_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtItem_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void bteProducto_Click(object sender, EventArgs e)
        {
            listarProducto();
        }
        private void listarProducto()
        {
            try
            {
                frmListarProducto frm = new frmListarProducto();
                //frm.flag_solo_prods = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteProducto.Tag = frm._Be.prdc_icod_producto;
                    bteProducto.Text = frm._Be.prdc_vcode_producto;
                    txtDescProd.Text = frm._Be.prdc_vdescripcion_larga;
                    txtUM.Text = frm._Be.StrUnidadMedida;
                }
                txtCantidadRequerida.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                FrmManteEntegaMaterialPorFechaDetalle frmdetalle = new FrmManteEntegaMaterialPorFechaDetalle();
                frmdetalle.cantida_pedida = Convert.ToDecimal(txtCantidadRequerida.Text);
                frmdetalle.cantidad_entregada = Convert.ToDecimal(txtTotalCantidadEntregada.Text);
                frmdetalle.stock = Convert.ToDecimal(txtStock.Text);
                frmdetalle.icod_item_requerimiento_material = icod_requerimiento_material;
                frmdetalle.btnModificar.Enabled = false;
                frmdetalle.SetInsert();
                frmdetalle.btnAgregar.Enabled = true;
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    _obe.rqmed_sfecha_entrega = frmdetalle._BE.rqmed_sfecha_entrega;
                    _obe.rqmad_icod_item_requerimiento_material = frmdetalle._BE.rqmad_icod_item_requerimiento_material;
                    _obe.rqmed_ncantidad_entregada = frmdetalle._BE.rqmed_ncantidad_entregada;
                    _obe.intTipoOperacion = frmdetalle._BE.intTipoOperacion;
                    lstEntergaMaterialDet.Add(_obe);
                    setTotal();
                    grdMateriales.DataSource = lstEntergaMaterialDet;
                    grdMateriales.RefreshDataSource();

                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    //oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {

                if (Flag)
                {

                }
            }
        }
        private void setTotal()
        {

            txtTotalCantidadEntregada.Text = lstEntergaMaterialDet.Sum(x => x.rqmed_ncantidad_entregada).ToString();

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEntregaMaterialDet obe = (EEntregaMaterialDet)viewMateriales.GetRow(viewMateriales.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstEntergaMaterialDet.Remove(obe);
            setTotal();
            viewMateriales.RefreshData();
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