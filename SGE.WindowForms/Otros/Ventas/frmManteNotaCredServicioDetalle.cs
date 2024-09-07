using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
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
    public partial class frmManteNotaCredServicioDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<ENotaCreditoDet> lstDetalle = new List<ENotaCreditoDet>();

        public ENotaCreditoDet oBe = new ENotaCreditoDet();

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public frmManteNotaCredServicioDetalle()
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

        public void setValues()
        {
            bteProducto.Tag = oBe.prdc_icod_producto;
            bteProducto.Text = oBe.strCodProducto;
            txtDescripcion.Text = oBe.strDesProducto;
            txtCantidad.Text = oBe.dcrec_ncantidad_producto.ToString();
            txtPrecio.Text = oBe.dcrec_nmonto_unitario.ToString();

            string[] partes = partes = oBe.dcrec_vdescripcion.Split('@');
            txtObservaciones.Lines = partes;
            //lkpMoneda.EditValue = oBeDetFav.;
            //btePersonal.Tag = oBeDetFav.otss_icod_personal;
            //btePersonal.Text = oBeDetFav.strPersonal;
            //txtAreaPersonal.Text = oBeDetFav.otss_area_personal;
            //dtFecha.EditValue = oBeDetFav.otss_fecha_servicio;
            //txtProductividad.Text = oBeDetFav.otss_nporc_productividad.ToString();
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
                    if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
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

                oBe.dcrec_inro_item = Convert.ToInt16(txtItem.Text);
                oBe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                oBe.dcrec_ncantidad_producto = Convert.ToDecimal(txtCantidad.Text);
                oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtPrecio.Text);
                oBe.dcrec_nmonto_item = Math.Round((oBe.dcrec_ncantidad_producto * oBe.dcrec_nmonto_unitario), 2);
                oBe.intClasificacion = Parametros.intTipoPrdServicio;
                oBe.strCodProducto = bteProducto.Text;
                oBe.strDesProducto = txtDescripcion.Text;
                oBe.strMoneda = lkpMoneda.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                string Descripci = "";
                string DescripciExtra = "";
                string[] arraye = txtObservaciones.Lines;
                for (int i = 0; i < arraye.Length; i++)
                {
                    Descripci = Descripci + arraye[i] + "@";
                    if (arraye[i] != "")
                        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                }


                oBe.dcrec_vdescripcion = Descripci;

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

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void groupControl1_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }
    }
}