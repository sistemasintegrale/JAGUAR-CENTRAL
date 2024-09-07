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

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class FrmManteNotaIngresoDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteNotaIngresoDetalle));

        public List<ENotaIngresoDetalle> lstDetalle = new List<ENotaIngresoDetalle>();
        public ENotaIngresoDetalle oBe = new ENotaIngresoDetalle();
        public ENotaIngreso obeCab = new ENotaIngreso();
        /*-----------------------------------------------------------------------------*/
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        string Categoria;
        string SubCategoria;


        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        public FrmManteNotaIngresoDetalle()
        {
            InitializeComponent();
        }

        private void FrmManteNotaIngresoDetalle_Load(object sender, EventArgs e)
        {

        }

        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            btnProducto.Enabled = !Enabled;
            txtCantidad.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                btnProducto.Enabled = Enabled;
            btnProducto.Focus();
        }

        public void setValues()
        {
            btnProducto.Tag = oBe.prdc_icod_producto;
            btnProducto.Text = oBe.strCodeProducto;
            txtDescripcion.Text = oBe.strProducto;
            txtUM.Text = oBe.strUnidadMedida;
            txtCantidad.Text = oBe.dninc_cantidad.ToString();
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
                if (string.IsNullOrEmpty(btnProducto.Text))
                {
                    oBase = btnProducto;
                    throw new ArgumentException("Seleccione producto");
                }
                if (verificarLista())
                {
                    oBase = btnProducto;
                    throw new ArgumentException("El producto seleccionado ya existe el detalle de la Nota de Ingreso");
                }
                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingrese la cantidad ");
                }
                oBe.ningc_icod_nota_ingreso = obeCab.ningc_icod_nota_ingreso;
                oBe.prdc_icod_producto = Convert.ToInt32(btnProducto.Tag);
                oBe.strCodeProducto = btnProducto.Text;
                oBe.strProducto = txtDescripcion.Text;
                oBe.strUnidadMedida = txtUM.Text;
                oBe.dninc_cantidad = Convert.ToDecimal(txtCantidad.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.strCategoria = Categoria;
                oBe.strSubCategoriaUno = SubCategoria;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.dninc_nro_item = lstDetalle.Count + 1;
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    if (oBe.intTipoOperacion == 0)
                        oBe.intTipoOperacion = 2;
                }
                //intro();
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
                if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(btnProducto.Tag)).ToList().Count > 0)
                    flag = true;
            }
            else if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                if (lstDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(btnProducto.Tag)).ToList()[0].dninc_nro_item != oBe.dninc_nro_item)
                    flag = true;
            }
            return flag;
        }
        private void intro()
        {
            var lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion).ToList();
            lstProductos.ForEach(x =>
            {
                ENotaIngresoDetalle oBe_ = new ENotaIngresoDetalle();
                oBe_.ningc_icod_nota_ingreso = obeCab.ningc_icod_nota_ingreso;
                oBe_.prdc_icod_producto = x.prdc_icod_producto;
                oBe_.strCodeProducto = x.prdc_vcode_producto;
                oBe_.strProducto = x.prdc_vdescripcion_larga;
                oBe_.strUnidadMedida = x.StrUnidadMedida;
                oBe_.dninc_cantidad = 1000;
                oBe_.intUsuario = Valores.intUsuario;
                oBe_.strPc = WindowsIdentity.GetCurrent().Name;
                oBe_.dninc_nro_item = lstDetalle.Count + 1;
                oBe_.intTipoOperacion = 1;
                lstDetalle.Add(oBe_);
            });
        }

        private void listarProducto()
        {
            try
            {
                frmListarProducto frm = new frmListarProducto();
                //frm.flag_solo_prods = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnProducto.Tag = frm._Be.prdc_icod_producto;
                    btnProducto.Text = frm._Be.prdc_vcode_producto;
                    txtDescripcion.Text = frm._Be.prdc_vdescripcion_larga;
                    txtUM.Text = frm._Be.StrUnidadMedida;
                    Categoria = frm._Be.strCategoria;
                    SubCategoria = frm._Be.strSubCategoriaUno;
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