using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteDocCompraDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EDocCompraDet> lstDetalle = new List<EDocCompraDet>();
        public EDocCompraDet obe = new EDocCompraDet();
        public string strLinea, strSubLinea;
        public int intClasificacion = 0;
        public int intAlmacen = 1;

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public frmManteDocCompraDetalle()
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
            bteProducto.Tag = obe.prd_icod_producto;
            bteProducto.Text = obe.strCodProducto;
            txtDescripcion.Text = obe.facd_vdescripcion_item;
            txtCantidad.Text = obe.facd_ncantidad.ToString();
            txtUnidadMedida.Text = obe.strDesUM;
            txtPrecio.Text = obe.facd_nmonto_unit.ToString();
            marca = obe.marc_vdescripcion;
            modelo = obe.modc_vdescripcion;

        }

        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {
                //if (Convert.ToInt32(bteNroOT.Tag) == 0)
                //{
                //    if (String.IsNullOrWhiteSpace(txtPlaca.Text))
                //    {
                //        oBase = bteNroOT;
                //        throw new ArgumentException("Seleccione Nro de Orden de Trabajo o Nro de Placa");
                //    }
                //}
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
                    if (lstDetalle.Where(x => x.prd_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                    {
                        oBase = bteProducto;
                        throw new ArgumentException("El producto seleccionado ya se encuentra en el detalle del documento");
                    }
                }

                //if (!String.IsNullOrWhiteSpace(txtPlaca.Text))
                //{
                //    var lstPlaca = new BOperaciones().listarVehiculo().Where(x => x.vehd_vplaca.Trim() == txtPlaca.Text).ToList();
                //    if (lstPlaca.Count == 0)
                //    {
                //        oBase = txtPlaca;
                //        throw new ArgumentException("El número de placa ingresado no existe en los registros de vehículos");                       
                //    }                    
                //}
                int? intNullVal = null;

                obe.facd_iitem = Convert.ToInt32(txtItem.Text);
                obe.prd_icod_producto = Convert.ToInt32(bteProducto.Tag);
                obe.facd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.facd_vdescripcion_item = txtDescripcion.Text;
                obe.facd_nmonto_unit = Convert.ToDecimal(txtPrecio.Text);
                obe.facd_nmonto_total = Math.Round((Convert.ToDecimal(obe.facd_ncantidad) * Convert.ToDecimal(obe.facd_nmonto_unit)), 2);
                /**/
                obe.strCodProducto = bteProducto.Text;
                obe.strDesUM = txtUnidadMedida.Text;
                obe.strMoneda = txtMoneda.Text;
                obe.intClasificacion = intClasificacion;
                obe.marc_vdescripcion = marca;
                obe.modc_vdescripcion = modelo;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.strLinea = strLinea;
                    obe.strSubLinea = strSubLinea;
                    obe.intTipoOperacion = 1;
                    lstDetalle.Add(obe);
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
        string marca;
        string modelo;
        private void listarProducto()
        {
            BaseEdit oBase = null;
            try
            {
                using (frmListarProducto frm = new frmListarProducto())
                {
                    frm.flag_solo_prods = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = frm._Be.prdc_icod_producto;
                        bteProducto.Text = frm._Be.prdc_vcode_producto;
                        txtDescripcion.Text = frm._Be.prdc_vdescripcion_larga;
                        txtUnidadMedida.Text = frm._Be.StrUnidadMedida;
                        //strLinea = frm._Be.strDesFamilia;
                        //strSubLinea = frm._Be.strDesSubFamilia;                        
                        //intClasificacion = Convert.ToInt32(frm._Be.tarec_icorrelativo_registro_tipo);
                        //marca = frm._Be.strMarca;
                        //modelo = frm._Be.strModelo;
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
            txtCantidad.Focus();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}