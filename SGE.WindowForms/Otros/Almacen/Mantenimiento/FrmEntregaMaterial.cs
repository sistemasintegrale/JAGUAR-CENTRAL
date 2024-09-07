using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class FrmEntregaMaterial : XtraForm
    {
        public EEntregaMaterial obj = new EEntregaMaterial();
        ComponentResourceManager resources = new ComponentResourceManager(typeof(frmManteEntregaMaterial));

        public ERequerimientoMaterialDet objItem;
        private decimal CantidadStock;

        public FrmEntregaMaterial() => InitializeComponent();
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e) => Dispose();
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e) => SetSave();
        private void FrmEntregaMaterial_Load(object sender, EventArgs e) => CargarInicial();
        private void simpleButton1_Click(object sender, EventArgs e) => LimpiarProducto();
        private void btnProducto_ButtonClick(object sender, ButtonPressedEventArgs e) => SelectProducto();
        private void btnPersonal_ButtonClick(object sender, ButtonPressedEventArgs e) => SelectPersonal();
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (string.IsNullOrEmpty(dteFechaEntrega.Text))
                {
                    oBase = dteFechaEntrega;
                    throw new ArgumentException("Ingrese la fecha de entrega");
                }
                if (string.IsNullOrEmpty(btnProducto.Text))
                {
                    oBase = btnProducto;
                    throw new ArgumentException("Ingrese producto");
                }
                if (string.IsNullOrEmpty(btnPersonal.Text))
                {
                    oBase = btnPersonal;
                    throw new ArgumentException("Ingrese el personal");
                }
                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("la cantidad debe ser mayor a 0");
                }
                if (Convert.ToDecimal(txtCantidad.Text) > objItem.rqmad_ncantidad_requerida)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException($"la cantidad no debe ser mayor a la cantidad requerida de {objItem.rqmad_ncantidad_requerida}");
                }
                if (Convert.ToDecimal(txtCantidad.Text) > CantidadStock)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException($"la cantidad no debe ser mayor a la cantidad requerida de {objItem.rqmad_ncantidad_requerida}");
                }

                obj.em_sfecha_entrega = dteFechaEntrega.DateTime;
                obj.prdc_icod_producto = Convert.ToInt32(btnProducto.Tag);
                obj.em_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obj.perc_icod_personal = Convert.ToInt32(btnPersonal.Tag);
                obj.intUsuario = Valores.intUsuario;
                new BAlmacen().EntregaMaterialesGuardar(obj, objItem);

            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = resources.GetObject("Warning") as Image;
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }

        }

        private void LimpiarProducto()
        {
            btnProducto.Tag = 0;
            btnProducto.Text = string.Empty;
            txtDescripcionProducto.Text = string.Empty;
        }

        private void CargarInicial()
        {

            dteFechaEntrega.DateTime = DateTime.Now;
            BSControls.LoaderLook(lkpUnidadMedida, new BAlmacen().listarUnidadMedida(), "unidc_vdescripcion", "unidc_icod_unidad_medida", true);
            if (obj.icod_entrega_material != 0)
                SetValues();

        }

        void SetValues()
        {
            obj = new BAlmacen().EntregaMaterialesGeyById(obj.icod_entrega_material);
            dteFechaEntrega.DateTime = obj.em_sfecha_entrega;
            btnProducto.Tag = obj.prdc_icod_producto;
            btnProducto.Text = obj.CodigoProducto;
            txtDescripcionProducto.Text = obj.DescripcionProducto;
            lkpUnidadMedida.EditValue = obj.IdUnidadMedida;
            btnPersonal.Text = obj.NombrePersonal;
            btnPersonal.Tag = obj.perc_icod_personal;
            txtCantidad.Text = obj.em_ncantidad.ToString();
            CantidadStock = new BAlmacen().listarStockPorAlmacenOptimizado(Parametros.intEjercicio, 120, "", "").Where(x => x.prdc_icod_producto == obj.prdc_icod_producto).FirstOrDefault().stocc_stock_producto + obj.em_ncantidad;
            btnProducto.Enabled = false;
            btnLimpiar.Visible = false;
        }

        private void SelectProducto()
        {
            frmListarProducto frm = new frmListarProducto();
            frm.stock = true;
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                btnProducto.Tag = frm._Be.prdc_icod_producto;
                btnProducto.Text = frm._Be.prdc_vcode_producto;
                txtDescripcionProducto.Text = frm._Be.prdc_vdescripcion_larga;
                lkpUnidadMedida.EditValue = frm._Be.unidc_icod_unidad_medida;
                CantidadStock = frm._Be.prdc_nMonto_stock;
                txtCantidad.Focus();
            }

        }



        private void SelectPersonal()
        {

            using (ListarPersonal frm = new ListarPersonal())
            {

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnPersonal.Tag = frm._Be.perc_iid_personal;
                    btnPersonal.Text = frm._Be.perc_vnombres_apellidos;
                }
            }
        }
    }
}