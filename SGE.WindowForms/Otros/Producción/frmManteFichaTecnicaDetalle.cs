using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class frmManteFichaTecnicaDetalle : DevExpress.XtraEditors.XtraForm
    {

        //public int icod_almacen;
        //public int icod_punto_venta;
        //public int icod_motivo;
        //public int indicador;
        public Boolean flag_exonerado;
        public Boolean flag_afecto_igv;
        public string PorIGV;
        public int codigo;
        public List<EFichaTecnicaCab> oPedido;

        public EFichaTecnicaDet _BE = new EFichaTecnicaDet();
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
                lkpArea.Enabled = !Enabled;
                lkpUbicacion.Enabled = !Enabled;
                txtCantidad.Enabled = !Enabled;
                txtitem.Enabled = Enabled;
                bteProducto.Enabled = !Enabled;
            }
            else
            {
                lkpArea.Enabled = !Enabled;
                lkpUbicacion.Enabled = !Enabled;
                bteProducto.Enabled = !Enabled;
            }
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            getNroDoc();
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
        public frmManteFichaTecnicaDetalle()
        {
            InitializeComponent();
        }
        public void setValues()
        {


        }
        private void FrmNotaSalidaDetalle_Load(object sender, EventArgs e)
        {

            BSControls.LoaderLook(lkpArea, new BCentral().listarAreaProduccion(), "arprc_vdescripcion", "arprc_iid_codigo", true);
            EAreaUbicacion ob = new EAreaUbicacion();
            ob.arprc_iid_codigo = Convert.ToInt32(lkpArea.EditValue);
            BSControls.LoaderLook(lkpUbicacion, new BCentral().ListarAreaUbicacion(ob), "descripcion", "arubd_iid_area_ubicacion", true);
            BSControls.LoaderLook(LkpUnidad, new BAlmacen().listarUnidadMedida(), "unidc_vdescripcion", "unidc_icod_unidad_medida", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpArea.EditValue = _BE.fited_iarea;
                lkpUbicacion.EditValue = _BE.fited_iubicacion;
                LkpUnidad.EditValue = Convert.ToInt32(_BE.fited_imedida);
            }


        }
        private void getNroDoc()
        {

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

                if (Convert.ToDecimal(txtCantidad.Text) <= 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("El Total debe ser mayor a 0");
                }
                //if (Convert.ToInt32(bteProducto.Tag) <=0)
                //{
                //    oBase = bteProducto;
                //    throw new ArgumentException("Selecionar Producto");
                //}
                _BE.fited_iitem_ficha_tecnica = Convert.ToInt32(txtitem.Text);
                _BE.fited_iarea = Convert.ToInt32(lkpArea.EditValue);
                _BE.strarea = lkpArea.Text;
                _BE.fited_iubicacion = Convert.ToInt32(lkpUbicacion.EditValue);
                _BE.strubicacion = lkpUbicacion.Text;
                _BE.fited_vdescripcion = txtDescripcion.Text;
                _BE.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                _BE.strCodeProducto = bteProducto.Text;
                _BE.strProducto = txtDescProd.Text;
                _BE.fited_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                _BE.fited_imedida = Convert.ToInt32(LkpUnidad.EditValue);



                List<EUnidadMedida> abrevUnidmed = new BAlmacen().listarUnidadMedida().Where(x => x.unidc_icod_unidad_medida == _BE.fited_imedida).ToList();

                _BE.strUnidadMedida = abrevUnidmed[0].unidc_vabreviatura_unidad_medida;
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
                _BE.fited_iarea = Convert.ToInt32(lkpArea.EditValue);
                _BE.strarea = lkpArea.Text;
                _BE.fited_iubicacion = Convert.ToInt32(lkpUbicacion.EditValue);
                _BE.strubicacion = lkpUbicacion.Text;
                _BE.fited_vdescripcion = txtDescripcion.Text;
                _BE.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                _BE.strCodeProducto = bteProducto.Text;
                _BE.strProducto = txtDescProd.Text;
                _BE.fited_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                _BE.fited_imedida = Convert.ToInt32(LkpUnidad.EditValue);
                List<EUnidadMedida> lst = new BAlmacen().listarUnidadMedida().Where(x => x.unidc_icod_unidad_medida == _BE.fited_imedida).ToList();
                _BE.strUnidadMedida = lst[0].unidc_vabreviatura_unidad_medida;
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

        }

        private void txtcantidaddetalle_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtDescuento_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void bteVendedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void lkpArea_EditValueChanged(object sender, EventArgs e)

        {


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
                    LkpUnidad.ItemIndex = 0;
                    LkpUnidad.ItemIndex = Convert.ToInt32(frm._Be.unidc_icod_unidad_medida - 1);
                    LkpUnidad.Enabled = false;
                }
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkpArea_EditValueChanged_1(object sender, EventArgs e)
        {

            EAreaUbicacion ob = new EAreaUbicacion();
            ob.arprc_iid_codigo = Convert.ToInt32(lkpArea.EditValue);
            BSControls.LoaderLook(lkpUbicacion, new BCentral().ListarAreaUbicacion(ob), "descripcion", "arubd_iid_area_ubicacion", true);
        }

        private void bteProducto_Click(object sender, EventArgs e)
        {
            listarProducto();
        }

        private void lkpUbicacion_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            bteProducto.EditValue = 0;
            bteProducto.Text = "";
            LkpUnidad.EditValue = 0;
            LkpUnidad.Text = "";
            txtDescProd.Text = "";
        }

        private void lkpUbicacion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption != "add") return;
            frmManteDestinos frm = new frmManteDestinos();
            EAreaUbicacion ob = new EAreaUbicacion();
            ob.arprc_iid_codigo = Convert.ToInt32(lkpArea.EditValue);
            var listaDestinos = new BCentral().ListarAreaUbicacion(ob);
            frm.txtCodigo.Text = listaDestinos.Count != 0 ? (listaDestinos.Max(x => x.arubd_codigo) + 1).ToString() : "1";
            frm.icodAreaRef = Convert.ToInt32(lkpArea.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {

                BSControls.LoaderLook(lkpUbicacion, new BCentral().ListarAreaUbicacion(ob), "descripcion", "arubd_iid_area_ubicacion", true);
            }
        }
    }
}