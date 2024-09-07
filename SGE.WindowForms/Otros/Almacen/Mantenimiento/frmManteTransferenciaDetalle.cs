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

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteTransferenciaDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteNotaIngresoDetalle));

        public List<ETransferenciaAlmacenDet> lstDetalle = new List<ETransferenciaAlmacenDet>();
        public ETransferenciaAlmacenDet oBe = new ETransferenciaAlmacenDet();
        public ETransferenciaAlmacen obeCab = new ETransferenciaAlmacen();
        /*-----------------------------------------------------------------------------*/
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

        public frmManteTransferenciaDetalle()
        {
            InitializeComponent();
        }

        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            bteProducto.Enabled = !Enabled;
            txtCantidad.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                bteProducto.Enabled = Enabled;
            bteProducto.Focus();
        }

        public void setValues()
        {
            bteProducto.Tag = oBe.prdc_icod_producto;
            bteProducto.Text = oBe.strCodProducto;
            txtDescripcion.Text = oBe.strDesProducto;
            txtUM.Text = oBe.strCodUnidadMedida;
            txtCantidad.Text = oBe.trfd_ncantidad.ToString();
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
            Boolean flag = true;
            try
            {
                if (string.IsNullOrEmpty(bteProducto.Text))
                {
                    oBase = bteProducto;
                    throw new ArgumentException("Seleccione producto");
                }
                if (verificarLista())
                {
                    oBase = bteProducto;
                    throw new ArgumentException("El producto seleccionado ya existe el detalle de la Nota de Ingreso");
                }
                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingrese la cantidad ");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (Convert.ToDecimal(txtCantidad.Text) > oBe.dblStockDisponible)
                    {
                        oBase = txtCantidad;
                        throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                    }
                }
                else
                {
                    if (oBe.intTipoOperacion != 1)
                    {
                        decimal dblStock = oBe.dblStockDisponible + oBe.trfd_ncantidad;
                        if (Convert.ToDecimal(txtCantidad.Text) > dblStock)
                        {
                            oBase = txtCantidad;
                            throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                        }
                    }
                    else
                        if (Convert.ToDecimal(txtCantidad.Text) > oBe.dblStockDisponible)
                    {
                        oBase = txtCantidad;
                        throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                    }
                }

                if (oBe.intTipoOperacion != 1)
                {
                    oBe.dblStockDisponible = oBe.dblStockDisponible + oBe.trfd_ncantidad - Convert.ToDecimal(txtCantidad.Text);
                }

                oBe.trfc_icod_transf = obeCab.trfc_icod_transf;
                oBe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                oBe.strCodProducto = bteProducto.Text;
                oBe.strDesProducto = txtDescripcion.Text;
                oBe.strCodUnidadMedida = txtUM.Text;
                oBe.trfd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.trfd_nro_item = lstDetalle.Count + 1;
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    if (oBe.intTipoOperacion == 0)
                        oBe.intTipoOperacion = 2;
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
                flag = false;
            }
            finally
            {
                if (flag)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private bool verificarLista()
        {
            bool flag = false;
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                    flag = true;
            }
            else if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList()[0].trfd_nro_item != oBe.trfd_nro_item)
                    flag = true;
            }
            return flag;
        }


        private void listarProducto()
        {
            BaseEdit oBase = null;
            try
            {
                if (obeCab.almac_icod_alm_sal == 0)
                {
                    throw new ArgumentException("No ha seleccionado el Almacén de salida");
                }

                using (frmListarStockPorAlmacen frm = new frmListarStockPorAlmacen())
                {
                    frm.intAlmacen = obeCab.almac_icod_alm_sal;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = frm._Be.prdc_icod_producto;
                        bteProducto.Text = frm._Be.strCodProducto;
                        txtDescripcion.Text = frm._Be.strDesProducto;
                        oBe.dblStockDisponible = frm._Be.stocc_stock_producto;
                        txtUM.Text = frm._Be.strDesUM;
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

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}