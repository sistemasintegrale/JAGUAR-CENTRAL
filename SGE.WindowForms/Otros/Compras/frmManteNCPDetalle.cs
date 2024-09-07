using DevExpress.XtraEditors;
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

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteNCPDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<ENotaCreditoProveedorDet> lstDetalle = new List<ENotaCreditoProveedorDet>();
        public ENotaCreditoProveedorDet obe = new ENotaCreditoProveedorDet();
        public string strLinea, strSubLinea;
        public int intClasificacion = 0;
        public int intAlmacen = 1;

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public frmManteNCPDetalle()
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
            txtDescripcion.Text = obe.ncpd_vdescripcion_item;
            txtCantidad.Text = obe.ncpd_ncantidad.ToString();
            txtUnidadMedida.Text = obe.strDesUM;
            txtPrecio.Text = obe.ncpd_nmonto_unit.ToString();
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

                obe.ncpd_iitem = Convert.ToInt32(txtItem.Text);
                obe.prd_icod_producto = Convert.ToInt32(bteProducto.Tag);
                if (Status == BSMaintenanceStatus.CreateNew)
                    obe.prd_iid_clasificacion_prod = intClasificacion;
                obe.ncpd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.ncpd_vdescripcion_item = txtDescripcion.Text;
                obe.ncpd_nmonto_unit = Convert.ToDecimal(txtPrecio.Text);
                obe.ncpd_nmonto_total = (Convert.ToDecimal(obe.ncpd_ncantidad) * Convert.ToDecimal(obe.ncpd_nmonto_unit));
                /**/
                obe.strCodProducto = bteProducto.Text;
                obe.strDesUM = txtUnidadMedida.Text;
                obe.strMoneda = txtMoneda.Text;
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;
                /**/
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

        private void listarProducto()
        {
            BaseEdit oBase = null;
            try
            {
                if (Convert.ToInt32(lkpTipoProd.EditValue) == 2)
                {

                }
                else
                {
                    using (frmListarProducto frm = new frmListarProducto())
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            bteProducto.Tag = frm._Be.prdc_icod_producto;
                            bteProducto.Text = frm._Be.prdc_vcode_producto;
                            txtDescripcion.Text = frm._Be.prdc_vdescripcion_larga;
                            txtUnidadMedida.Text = frm._Be.StrUnidadMedida;

                        }
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

        private void frmManteNCPDetalle_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            List<TIpoProd> lst = new List<TIpoProd>();
            lst.Add(new TIpoProd { inTipo = 1, strTipo = "Repuesto" });
            lst.Add(new TIpoProd { inTipo = 2, strTipo = "Servicio" });
            BSControls.LoaderLook(lkpTipoProd, lst.OrderBy(x => x.inTipo).ToList(), "strTipo", "inTipo", true);
        }

        class TIpoProd
        {
            public int inTipo { get; set; }
            public string strTipo { get; set; }
        }
    }
}