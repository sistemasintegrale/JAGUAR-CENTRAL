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

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmMantAlmacenSaldoInicial : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMantAlmacenSaldoInicial));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EKardex oBe = new EKardex();
        public List<EKardex> lstKardex = new List<EKardex>();

        public frmMantAlmacenSaldoInicial()
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
            bteAlmacen.Enabled = !Enabled;
            bteProducto.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteAlmacen.Enabled = Enabled;
                bteProducto.Enabled = Enabled;
            }

        }
        public void setValues()
        {
            bteAlmacen.Tag = oBe.almac_icod_almacen;
            bteAlmacen.Text = oBe.strAlmacen;

            bteProducto.Tag = oBe.prdc_icod_producto;
            bteProducto.Text = oBe.strProducto;

            txtUnidadMedida.Text = oBe.strUnidadMedida;

            txtCantidad.Text = oBe.kardc_icantidad_prod.ToString();

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

                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione un almacén");
                }

                if (Convert.ToInt32(bteProducto.Tag) == 0)
                {
                    oBase = bteProducto;
                    throw new ArgumentException("Seleccione un producto");
                }

                if (verificarProducto(Convert.ToInt32(bteAlmacen.Tag), Convert.ToInt32(bteProducto.Tag)))
                {
                    throw new ArgumentException("El producto ya cuenta con registro de saldo inicial");
                }

                if (Convert.ToDecimal(txtCantidad.Text) < 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingrese la cantidad mayor a 0");
                }

                oBe.kardc_ianio = Parametros.intEjercicio;
                oBe.kardc_fecha_movimiento = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1);
                oBe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                oBe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                oBe.kardc_icantidad_prod = Convert.ToDecimal(txtCantidad.Text);
                oBe.tdocc_icod_tipo_doc = Parametros.intTipoDocAperturaKardex;
                oBe.kardc_numero_doc = "000001";
                oBe.kardc_tipo_movimiento = Parametros.intKardexIn;
                oBe.kardc_iid_motivo = 100;//INGRESO A ALMACEN POR SALDO INICIAL
                oBe.kardc_beneficiario = String.Format("SALDO INICIAL DEL PRODUCTO {0}", bteProducto.Text);
                oBe.kardc_observaciones = "";
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = Valores.strUsuario;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.kardc_icod_correlativo = new BAlmacen().insertarKardex(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    oBe.kardc_icod_correlativo = new BAlmacen().modificarKardex(oBe);
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
                    this.MiEvento(oBe.kardc_icod_correlativo);
                    this.Close();
                }
            }
        }

        private bool verificarProducto(int intAlmacen, int intProducto)
        {
            try
            {
                bool exists = false;
                if (lstKardex.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstKardex.Where(x => x.almac_icod_almacen == intAlmacen && x.prdc_icod_producto == intProducto).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstKardex.Where(x => x.almac_icod_almacen == intAlmacen && x.prdc_icod_producto == intProducto &&
                            x.kardc_icod_correlativo != oBe.kardc_icod_correlativo).ToList().Count > 0)
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

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void bteAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarAlmacen();
        }

        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }

        private void bteProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarProducto();
        }

        private void listarAlmacen()
        {
            try
            {
                frmListarAlmacen frm = new frmListarAlmacen();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = frm._Be.almac_icod_almacen;
                    bteAlmacen.Text = frm._Be.almac_vdescripcion;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listarProducto()
        {
            try
            {
                frmListarProducto frm = new frmListarProducto();
                frm.flag_solo_prods = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteProducto.Tag = frm._Be.prdc_icod_producto;
                    bteProducto.Text = frm._Be.prdc_vdescripcion_larga;
                    txtUnidadMedida.Text = frm._Be.StrUnidadMedida;
                }
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}