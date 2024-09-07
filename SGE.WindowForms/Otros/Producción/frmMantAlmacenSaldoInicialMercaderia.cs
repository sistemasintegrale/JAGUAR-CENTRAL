using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class frmMantAlmacenSaldoInicialMercaderia : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMantAlmacenSaldoInicialMercaderia));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EProdKardex oBe = new EProdKardex();
        public List<EProdKardex> lstKardex = new List<EProdKardex>();

        public frmMantAlmacenSaldoInicialMercaderia()
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
            bteAlmacen.Tag = oBe.iid_almacen;
            bteAlmacen.Text = oBe.strAlmacen;

            bteProducto.Tag = oBe.iid_producto;
            bteProducto.Text = oBe.strProducto;

            txtUnidadMedida.Text = oBe.strUnidadMedida;

            txtCantidad.Text = oBe.Cantidad.ToString();

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
                oBe.kardx_sfecha = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1);
                oBe.puvec_icod_punto_venta = 2;
                oBe.iid_almacen = Convert.ToInt32(bteAlmacen.Tag);
                oBe.iid_producto = Convert.ToInt32(bteProducto.Tag);
                oBe.Cantidad = Convert.ToDecimal(txtCantidad.Text);
                oBe.iid_tipo_doc = Parametros.intTipoDocAperturaKardex;
                oBe.NroDocumento = "000001";
                oBe.movimiento = Parametros.intKardexIn;
                oBe.Motivo = 100;//INGRESO A ALMACEN POR SALDO INICIAL
                oBe.Beneficiario = String.Format("SALDO INICIAL DEL PRODUCTO {0}", bteProducto.Text);
                oBe.Observaciones = "";
                oBe.intUsuario = Valores.intUsuario;
                //oBe.strPc = Valores.strUsuario;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.kardc_iid_correlativo = new BCentral().InsertarKardexpvt(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCentral().modificarKardexPvt(oBe);
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
                    this.MiEvento(oBe.kardc_iid_correlativo);
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
                        if (lstKardex.Where(x => x.iid_almacen == intAlmacen && x.iid_producto == intProducto).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstKardex.Where(x => x.iid_almacen == intAlmacen && x.iid_producto == intProducto &&
                            x.kardc_iid_correlativo != oBe.kardc_iid_correlativo).ToList().Count > 0)
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
                using (FrmListarAlmacen Almacen = new FrmListarAlmacen())
                {
                    Almacen.puvec_icod_punto_venta = 2;
                    if (Almacen.ShowDialog() == DialogResult.OK)
                    {
                        bteAlmacen.Tag = Almacen._Be.id_almacen;
                        bteAlmacen.Text = Almacen._Be.descripcion;
                    }
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
                using (FrmListarProducto Producto = new FrmListarProducto())
                {
                    if (Producto.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = Producto._Be.pr_icod_producto;
                        bteProducto.Text = Producto._Be.pr_vcodigo_externo.Substring(0, 13);
                    }
                }
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