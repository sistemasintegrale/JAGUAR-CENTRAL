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

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmManteIngresoStockInicialValorizado : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteIngresoStockInicialValorizado));
        public delegate void DelegadoMensaje(Int64 intIcod);
        public event DelegadoMensaje MiEvento;

        public List<EKardexValorizado> lstKardexVal = new List<EKardexValorizado>();
        public EKardexValorizado oBe = new EKardexValorizado();

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

        public FrmManteIngresoStockInicialValorizado()
        {
            InitializeComponent();
        }

        private void FrmManteIngresoStockInicialValorizado_Load(object sender, EventArgs e)
        {
            dtmFechaDocumento.EditValue = DateTime.Parse("01/01/" + Parametros.intEjercicio);
            btnDocumento.Tag = Parametros.intTipoDocAperturaKardex;
            btnDocumento.Text = "APE";
            txtNroDocumento.Text = "0001";
        }

        public void setValues()
        {
            dtmFechaDocumento.EditValue = oBe.kardv_sfecha_movimiento;
            txtNroDocumento.Text = oBe.kardv_inumero_doc;
            btnAlmacen.Tag = oBe.almcc_icod_almacen;
            btnAlmacen.Text = oBe.strDesAlmacenCtbl;
            btnProducto.Tag = oBe.prdc_icod_producto;
            btnProducto.Text = oBe.strDesProducto;
            txtCantidad.Text = oBe.kardv_icantidad_prod.ToString();
            txtPrecioUnitario.Text = oBe.kardv_precio_costo_promedio.ToString();
        }
        private void btnAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacenContable();
        }

        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            //txtNroDocumento.Enabled = !Enabled;
            btnAlmacen.Enabled = !Enabled;
            btnProducto.Enabled = !Enabled;
            txtCantidad.Enabled = !Enabled;
            txtPrecioUnitario.Enabled = !Enabled;
            dtmFechaDocumento.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                dtmFechaDocumento.Enabled = Enabled;
                btnDocumento.Enabled = Enabled;
                //txtNroDocumento.Enabled = Enabled;
                btnAlmacen.Enabled = Enabled;
                btnProducto.Enabled = Enabled;
                btnGuardar.Enabled = !Enabled;
            }
        }

        private void clearControl()
        {
            txtNroDocumento.Text = "";
            btnAlmacen.Text = "";
            btnProducto.Text = "";
            txtNroDocumento.Focus();
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
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
                if (string.IsNullOrEmpty(txtNroDocumento.Text))
                {
                    oBase = txtNroDocumento;
                    throw new ArgumentException("Ingresar número de documento");
                }

                if (string.IsNullOrEmpty(btnAlmacen.Text))
                {
                    oBase = btnAlmacen;
                    throw new ArgumentException("Seleccionar almacén");
                }

                if (string.IsNullOrEmpty(btnProducto.Text))
                {
                    oBase = btnProducto;
                    throw new ArgumentException("Seleccionar producto");
                }

                if (txtCantidad.Text == "0.00")
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La cantidad no puede ser 0");
                }

                if (txtPrecioUnitario.Text == "0.00")
                {
                    oBase = txtPrecioUnitario;
                    throw new ArgumentException("El precio unitario no puede ser 0");
                }

                var lstAux = lstKardexVal.Where(oB => oB.prdc_icod_producto == Convert.ToInt32(btnProducto.Tag)).ToList();

                if (Status == BSMaintenanceStatus.CreateNew)
                    if (lstAux.Count > 0)
                    {
                        oBase = btnProducto;
                        throw new ArgumentException("El Producto seleccionado ya existe en el registro de Stock Valorizado Inicial");
                    }



                oBe.kardv_ianio = Parametros.intEjercicio;
                oBe.kardv_sfecha_movimiento = Convert.ToDateTime(dtmFechaDocumento.DateTime);
                oBe.almcc_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                oBe.prdc_icod_producto = Convert.ToInt32(btnProducto.Tag);
                oBe.kardv_icantidad_prod = Convert.ToDecimal(txtCantidad.EditValue);
                oBe.tdocc_icod_tipo_doc = Convert.ToInt32(btnDocumento.Tag);
                oBe.kardv_inumero_doc = txtNroDocumento.Text;
                oBe.kardv_itipo_movimiento = Parametros.intKardexIn;
                oBe.kardv_iid_motivo = Parametros.intMotivoKrdSaldoInicial;
                oBe.kardv_vbeneficiario = "SALDO INICIAL DEL " + Parametros.intEjercicio.ToString();
                oBe.kardv_vobservaciones = "SALDO INICIAL";
                oBe.kardv_monto_total_compra = Convert.ToDecimal(txtPrecioUnitario.EditValue) * Convert.ToDecimal(txtCantidad.EditValue);
                oBe.kardv_precio_costo_promedio = Convert.ToDecimal(txtPrecioUnitario.Text);
                oBe.kardv_monto_saldo_valorizado = 0;
                oBe.kardv_monto_unitario_compra = Convert.ToDecimal(txtPrecioUnitario.Text);
                oBe.kardv_nmonto_ingreso_manual = 0;
                oBe.dcmlIngreso = (oBe.kardv_itipo_movimiento == Parametros.intKardexIn) ? oBe.kardv_icantidad_prod : 0;
                oBe.dcmlSalida = (oBe.kardv_itipo_movimiento == Parametros.intKardexOut) ? oBe.kardv_icantidad_prod : 0;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.kardv_icod_correlativo = new BAlmacen().insertarKardexValorizado(oBe);
                }
                else
                {
                    new BAlmacen().modificarKardexValorizado(oBe);
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(oBe.kardv_icod_correlativo);
                    this.Close();
                }
            }
        }

        private void listarAlmacenContable()
        {
            frmListarAlmacenContable frm = new frmListarAlmacenContable();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnAlmacen.Tag = frm._Be.almcc_icod_almacen;
                btnAlmacen.Text = frm._Be.almcc_vdescripcion;
            }
        }

        private void listarProducto()
        {
            frmListarProducto frm = new frmListarProducto();
            frm.flag_solo_prods = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnProducto.Tag = frm._Be.prdc_icod_producto;
                btnProducto.Text = frm._Be.prdc_vdescripcion_larga;
            }
        }


        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar__ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarAlmacenContable();
        }

        private void btnProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarProducto();
        }
    }
}