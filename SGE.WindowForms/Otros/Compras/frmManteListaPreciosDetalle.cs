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
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteListaPreciosDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EListaPrecioDetalle> lstDetalle = new List<EListaPrecioDetalle>();
        public EListaPrecioDetalle obe = new EListaPrecioDetalle();

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        string prdc_vAutor = "";
        string strEditorial = "";

        public frmManteListaPreciosDetalle()
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
            bteProducto.Tag = obe.prdc_icod_producto;
            bteProducto.Text = obe.prdc_vcode_producto;
            txtDescripcion.Text = obe.prdc_vdescripcion_larga;
            lkpmoneda.EditValue = obe.lpred_icod_moneda;
            txtPrecioLista.Text = obe.lpred_nprecio_lista.ToString();
            txtDesc.Text = obe.lpred_nperso_desc.ToString();
            txtPrecioNeto.Text = obe.lpred_nprecio_neto.ToString();

            prdc_vAutor = obe.prdc_vAutor;
            strEditorial = obe.strEditorial;
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

                if (Convert.ToDecimal(txtDesc.Text) <= 0)
                {
                    oBase = txtDesc;
                    throw new ArgumentException("La cantidad debe ser mayor a 0");
                }

                if (Convert.ToDecimal(txtPrecioLista.Text) <= 0)
                {
                    oBase = txtPrecioLista;
                    throw new ArgumentException("El precio unitario debe ser mayor a 0");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                    {
                        oBase = bteProducto;
                        throw new ArgumentException("El producto seleccionado ya se encuentra en el detalle del documento");
                    }
                }

                obe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                obe.prdc_vcode_producto = bteProducto.Text;
                obe.prdc_vdescripcion_larga = txtDescripcion.Text;
                obe.lpred_icod_moneda = Convert.ToInt32(lkpmoneda.EditValue);
                obe.lpred_nprecio_lista = Convert.ToDecimal(txtPrecioLista.Text);
                obe.lpred_nperso_desc = Convert.ToDecimal(txtDesc.Text);
                obe.lpred_nprecio_neto = Convert.ToDecimal(txtPrecioNeto.Text);
                obe.lpred_vdescripcion_moneda = lkpmoneda.Text;
                obe.prdc_vAutor = prdc_vAutor;
                obe.strEditorial = strEditorial;
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = "";
                /**/
                obe.lpred_sflag_estado = true;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
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
                using (frmListarProducto frm = new frmListarProducto())
                {
                    frm.flag_solo_prods = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = frm._Be.prdc_icod_producto;
                        bteProducto.Text = frm._Be.prdc_vcode_producto;
                        txtDescripcion.Text = frm._Be.prdc_vdescripcion_larga;
                        prdc_vAutor = frm._Be.prdc_vAutor;
                        strEditorial = frm._Be.strEditorial;
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
            txtPrecioLista.Focus();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
        public void CargarControles()
        {
            BSControls.LoaderLook(lkpmoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda).Where(x => x.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }
        private void frmManteFacCompraDetalle_Load(object sender, EventArgs e)
        {

        }
    }
}