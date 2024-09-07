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

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmManteHojaCostosRubros : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteHojaCostosConceptos));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EHojaCostosRubros Obe = new EHojaCostosRubros();
        public List<EHojaCostosRubros> lstHojaCostosRubros = new List<EHojaCostosRubros>();
        public string ItemSubConcepto = "";
        public string CodCompleto = "";
        public frmManteHojaCostosRubros()
        {
            InitializeComponent();
        }
        private void frmManteBanco_Load(object sender, EventArgs e)
        {
            CargarControles();
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpTipo.EditValue = Obe.tablc_icod_tipo_rubro;
                lkpMoneda.EditValue = Obe.tablc_icod_tipo_moneda;
                lkpUnidadMedida.EditValue = Obe.hjcd3_unidc_icod_unidad_medida;

            }
        }
        public void CargarControles()
        {
            BSControls.LoaderLook(lkpUnidadMedida, new BAlmacen().listarUnidadMedida(), "unidc_vdescripcion", "unidc_icod_unidad_medida", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda).Where(x => x.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaRegistro(75), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
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
            txtCodigo.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = Enabled;
        }
        public void setValues()
        {
            //txtCodigo.Text = String.Format("{0:00}", Obe.hjcd3_icod_rubros_hc);
            txtOrden.Text = String.Format("{0:00}", Obe.hjcd3_iitem_orden);
            txtDescripcion.Text = Obe.hjcd3_vdescripcion;
            btnProducto.Text = Obe.str_producto;
            txtCantidad.Text = Obe.hjcd3_ncantidad.ToString();
            txtCUnitario.Text = Obe.hjcd3_nmonto_unitario.ToString();
            txtCTotal.Text = Obe.hjcd3_nmonto_real.ToString();
            btnProducto.Tag = Obe.prdc_icod_producto;
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
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese Descripcion");
                }
                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingrese Cantidad");
                }
                if (Convert.ToDecimal(txtCUnitario.Text) == 0)
                {
                    oBase = txtCUnitario;
                    throw new ArgumentException("Ingrese Costo Unitario");
                }
                if (Convert.ToDecimal(txtCTotal.Text) == 0)
                {
                    oBase = txtCTotal;
                    throw new ArgumentException("Ingrese Costo Total");
                }
                Obe.hjcd3_vcodigo_relacion = CodCompleto;
                Obe.hjcd3_iitem_orden = txtOrden.Text;
                Obe.hjcd3_vdescripcion = txtDescripcion.Text;
                Obe.hjcd3_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                Obe.hjcd3_nmonto_unitario = Convert.ToDecimal(txtCUnitario.Text);
                Obe.hjcd3_nmonto_real = Math.Round(Convert.ToDecimal(txtCTotal.Text), 4);
                Obe.hjcd3_unidc_icod_unidad_medida = Convert.ToInt32(lkpUnidadMedida.EditValue);
                Obe.Unidad = lkpUnidadMedida.Text;
                Obe.tablc_icod_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.TipoModena = (Convert.ToInt32(lkpMoneda.EditValue) == 3) ? "S/." : "US$";
                Obe.tablc_icod_tipo_rubro = Convert.ToInt32(lkpTipo.EditValue);
                if (Convert.ToInt32(lkpTipo.EditValue) == 313)
                {
                    Obe.prdc_icod_producto = Convert.ToInt32(btnProducto.Tag);
                }
                else
                {
                    Obe.prdc_icod_producto = 0;
                }
                Obe.hjcd3_ncantidad_saldo = Obe.hjcd3_ncantidad - Obe.hjcd3_ncantidad_requerida;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.hjcd3_flag_estado = true;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.hjcd2_iitem = ItemSubConcepto;
                    Obe.intTipoOperacion = 1;
                    lstHojaCostosRubros.Add(Obe);
                }
                else
                {
                    if (Obe.intTipoOperacion != 1)
                    {
                        Obe.intTipoOperacion = 2;
                    }
                }

            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
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
                    this.Close();
                }
            }
        }
        private bool verificarNombreBanco(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstHojaCostosRubros.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstHojaCostosRubros.Where(x => x.hjcd3_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstHojaCostosRubros.Where(x => x.hjcd3_vdescripcion.Trim() == strNombre.Trim() && x.hjcd3_icod_rubros_hc != Obe.hjcd3_icod_rubros_hc).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void Total()
        {
            decimal cantidad = Convert.ToDecimal(txtCantidad.Text);
            decimal Unitario = Convert.ToDecimal(txtCUnitario.Text);
            txtCTotal.Text = (cantidad * Unitario).ToString();
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            Total();
        }

        private void txtCUnitario_EditValueChanged(object sender, EventArgs e)
        {
            Total();
        }

        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }
        private void listarProducto()
        {
            try
            {
                frmListarProducto frm = new frmListarProducto();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnProducto.Tag = frm._Be.prdc_icod_producto;
                    btnProducto.Text = frm._Be.prdc_vcode_producto;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkpTipo_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipo.EditValue) == 313)
            {
                btnProducto.Enabled = true;
            }
            else
            {
                btnProducto.Enabled = false;
            }
        }
    }
}