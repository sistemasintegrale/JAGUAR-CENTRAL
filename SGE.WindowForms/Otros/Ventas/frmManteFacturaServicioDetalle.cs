using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Operaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteFacturaServicioDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EFacturaDet> lstFacturaDetalle = new List<EFacturaDet>();
        public string Categoria, SubCategoria;
        public EFacturaDet obe = new EFacturaDet();

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public frmManteFacturaServicioDetalle()
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

        }

        private void setValues()
        {
            bteProducto.Tag = obe.prdc_icod_producto;
            bteProducto.Text = obe.strCodProducto;
            txtDescripcion.Text = obe.strDesProducto;
            txtCantidad.Text = obe.favd_ncantidad.ToString();
            txtPrecio.Text = obe.favd_nprecio_unitario_item.ToString();
            string[] partes = partes = obe.favd_nobservaciones.Split('@');
            txtObservaciones.Lines = partes;
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {


                if (Convert.ToInt32(bteProducto.Tag) == 0)
                {
                    oBase = bteProducto;
                    throw new ArgumentException("Seleccione el servicio");
                }


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (lstFacturaDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                    {
                        oBase = bteProducto;
                        throw new ArgumentException("El servicio seleccionado ya se encuentra en el detalle del documento");
                    }
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

                obe.almac_icod_almacen = 0;
                obe.favd_iitem_factura = Convert.ToInt32(txtItem.Text);
                obe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                obe.favd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.favd_vdescripcion = txtDescripcion.Text;
                obe.favd_nprecio_unitario_item = Convert.ToDecimal(txtPrecio.Text);
                obe.favd_nprecio_total_item = Math.Round((obe.favd_ncantidad * obe.favd_nprecio_unitario_item), 2);

                obe.strCodProducto = bteProducto.Text;
                obe.strDesProducto = txtDescripcion.Text;
                obe.strMoneda = lkpMoneda.Text;
                obe.flagPlanilla = true;

                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;


                string Descripci = "";
                string DescripciExtra = "";
                string[] arraye = txtObservaciones.Lines;
                for (int i = 0; i < arraye.Length; i++)
                {
                    Descripci = Descripci + arraye[i] + "@";
                    if (arraye[i] != "")
                        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                }


                obe.favd_nobservaciones = Descripci;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.strCategoria = Categoria;
                    obe.strSubCategoriaUno = SubCategoria;
                    obe.intTipoOperacion = 1;
                    lstFacturaDetalle.Add(obe);
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

        private void listarPersonal()
        {
            frmListarPersonal frm = new frmListarPersonal();
            frm.flag_personal_all = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btePersonal.Tag = frm._Be.perc_icod_personal;
                btePersonal.Text = frm._Be.perc_vapellidos_nombres;
                txtAreaPersonal.Text = frm._Be.strArea;
            }
        }

        private void listarServicios()
        {
            BaseEdit oBase = null;
            try
            {


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
            listarServicios();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmManteOTSSolicitado_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                setValues();
        }

        private void bteProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarServicios();
        }

        private void btePersonal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarPersonal();
        }

        private void btePersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarPersonal();
        }

        private void calcularProductividad()
        {
            decimal porc_productividad = Convert.ToDecimal(txtProductividad.Text.Substring(0, 5));
            txtMtoProductividad.Text = (Math.Round(Convert.ToDecimal(txtPrecio.Text) * porc_productividad / 100, 2)).ToString();
        }

        private void txtProductividad_EditValueChanged(object sender, EventArgs e)
        {
            calcularProductividad();
        }

        private void txtPrecio_EditValueChanged(object sender, EventArgs e)
        {
            calcularProductividad();
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            calcularProductividad();
        }

        private void txtItem_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtItem_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }
    }
}