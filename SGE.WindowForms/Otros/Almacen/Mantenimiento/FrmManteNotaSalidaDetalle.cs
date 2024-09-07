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
    public partial class FrmManteNotaSalidaDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteNotaSalidaDetalle));

        public List<ENotaSalidaDetalle> lstDetalle = new List<ENotaSalidaDetalle>();
        public ENotaSalidaDetalle oBe = new ENotaSalidaDetalle();
        public ENotaSalida obeCab = new ENotaSalida();
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

        public FrmManteNotaSalidaDetalle()
        {
            InitializeComponent();
        }

        private void FrmManteNotaSalidaDetalle_Load(object sender, EventArgs e)
        {

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
            bteProducto.Text = oBe.strCodeProducto;
            txtDescripcion.Text = oBe.strProducto;
            txtUM.Text = oBe.strUnidadMedida;
            txtCantidad.Text = oBe.dnsalc_cantidad.ToString();
            Categoria = oBe.strCategoria;
            SubCategoria = oBe.strSubCategoriaUno;
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
                    throw new ArgumentException("El producto seleccionado ya existe el detalle de la Nota de salida");
                }
                if (Convert.ToDecimal(txtCantidad.Text) <= 0)
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
                        decimal dblStock = oBe.dblStockDisponible + oBe.dnsalc_cantidad;
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
                    oBe.dblStockDisponible = oBe.dblStockDisponible + oBe.dnsalc_cantidad - Convert.ToDecimal(txtCantidad.Text);
                }

                oBe.nsalc_icod_nota_salida = obeCab.nsalc_icod_nota_salida;
                oBe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                oBe.strCodeProducto = bteProducto.Text;
                oBe.strProducto = txtDescripcion.Text;
                oBe.strUnidadMedida = txtUM.Text;
                oBe.dnsalc_cantidad = Convert.ToDecimal(txtCantidad.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.strCategoria = Categoria;
                oBe.strSubCategoriaUno = SubCategoria;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.dnsalc_nro_item = lstDetalle.Count + 1;
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
                if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList()[0].dnsalc_nro_item != oBe.dnsalc_nro_item)
                    flag = true;
            }
            return flag;
        }
        string Categoria;
        string SubCategoria;
        private void listarProducto()
        {
            try
            {
                if (obeCab.almac_icod_almacen == 0)
                {
                    throw new ArgumentException("No ha seleccionado el Almacén de salida");
                }

                using (frmListarStockPorAlmacen frm = new frmListarStockPorAlmacen())
                {
                    frm.intAlmacen = obeCab.almac_icod_almacen;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = frm._Be.prdc_icod_producto;
                        bteProducto.Text = frm._Be.strCodProducto;
                        txtDescripcion.Text = frm._Be.strDesProducto;
                        oBe.dblStockDisponible = frm._Be.stocc_stock_producto;
                        txtUM.Text = frm._Be.strDesUM;
                        Categoria = frm._Be.strCategoria;
                        SubCategoria = frm._Be.strSubCategoriaUno;
                    }
                }
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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