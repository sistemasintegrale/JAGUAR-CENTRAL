using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class frmMantePlantillaMateriales : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMantePlantillaMateriales));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        //public int icod_almacen;
        //public int icod_punto_venta;
        //public int icod_motivo;
        //public int indicador;
        public int icodModelo;
        public Boolean flag_exonerado;
        public Boolean flag_afecto_igv;
        public string PorIGV;
        public int codigo;
        public List<EPlantillaMateriales> oPedido;

        public EPlantillaMateriales _BE = new EPlantillaMateriales();
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
        public frmMantePlantillaMateriales()
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpArea.EditValue = _BE.fited_iarea;
                lkpUbicacion.EditValue = _BE.fited_iubicacion;
            }

        }
        private void getNroDoc()
        {

        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EProdModelo oBe = new EProdModelo();
            try
            {

                //if (Convert.ToInt32(bteProducto.Tag) <= 0)
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
                _BE.strUnidadMedida = txtUM.Text;
                _BE.fited_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    _BE.mo_icod_modelo = icodModelo;
                    _BE.fited_icod_item_ficha_tecnica = new BCentral().insertarPlantillaMateriales(_BE);
                    //Obl.InsertarProdModelo(oBe);
                }
                else
                {
                    new BCentral().modificarPlantillaMateriales(_BE);
                }



            }
            catch (Exception ex)
            {
                oBase.Focus();
                oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                oBase.ErrorText = ex.Message;
                oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        Status = BSMaintenanceStatus.View;
                    }
                    else
                    {
                        Status = BSMaintenanceStatus.View;
                    }
                    Status = BSMaintenanceStatus.View;
                    this.MiEvento(_BE.fited_icod_item_ficha_tecnica);
                    this.Close();
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
                    txtUM.Text = frm._Be.StrUnidadMedida;
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

        private void btnAgregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }
    }
}