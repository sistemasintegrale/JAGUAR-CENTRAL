using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmManteClasificacion : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteClasificacion));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        public EClasificacionProducto Obe = new EClasificacionProducto();
        public List<EClasificacionProducto> lstClasificacionProducto = new List<EClasificacionProducto>();


        public frmManteClasificacion()
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
            txtCodigo.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            bteCuenta.Enabled = !Enabled;
            bteCtaCompras.Enabled = !Enabled;
            bteCtaCostos.Enabled = !Enabled;
            txtCuentaCompras.Enabled = !Enabled;
            txtCuentaCostos.Enabled = !Enabled;
            txtCuentaDes.Enabled = !Enabled;
            lkpAlmacen.Enabled = !Enabled;

            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = Enabled;
        }
        public void setValues()
        {
            txtCodigo.Text = String.Format("{0:00}", Obe.clasc_iid_clasificacion);
            txtDescripcion.Text = Obe.clasc_vdescripcion;
            bteCuenta.Tag = Obe.ctacc_icod_cuenta_contable_producto;
            bteCtaCompras.Tag = Obe.ctacc_icod_cuenta_contable_compra;
            bteCtaCostos.Tag = Obe.ctacc_icod_cuenta_contable_costos;

            bteCuenta.Text = Obe.strCuentaProducto;
            bteCtaCompras.Text = Obe.strCuentaCompra;
            bteCtaCostos.Text = Obe.strCuentaCostos;

            txtCuentaCompras.Text = Obe.strDesCuentaCompra;
            txtCuentaCostos.Text = Obe.strDesCuentaCostos;
            txtCuentaDes.Text = Obe.strDesCuentaProducto;
            lkpAlmacen.EditValue = Obe.almcc_icod_almacen;
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
                    throw new ArgumentException("Ingrese descripción de la Clasificación");
                }

                if (verificarDescripcion(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("La descripción ingresada ya existe en los Registros de Clasificación de Productos");
                }

                //if (Convert.ToInt32(bteCuenta.Tag) == 0)
                //{
                //    oBase = bteCuenta;
                //    throw new ArgumentException("Seleccione la cuenta contable correspondiente");
                //}

                //if (Convert.ToInt32(bteCtaCompras.Tag) == 0)
                //{
                //    oBase = bteCtaCompras;
                //    throw new ArgumentException("Seleccione la cuenta contable correspondiente");
                //}

                //if (Convert.ToInt32(bteCtaCostos.Tag) == 0)
                //{
                //    oBase = bteCtaCostos;
                //    throw new ArgumentException("Seleccione la cuenta contable correspondiente");
                //}

                int? intNullVal = null;

                Obe.clasc_iid_clasificacion = Convert.ToInt32(txtCodigo.Text);
                Obe.clasc_vdescripcion = txtDescripcion.Text;
                Obe.ctacc_icod_cuenta_contable_producto = (Convert.ToInt32(bteCuenta.Tag) == 0) ? intNullVal : Convert.ToInt32(bteCuenta.Tag);
                Obe.ctacc_icod_cuenta_contable_compra = (Convert.ToInt32(bteCtaCompras.Tag) == 0) ? intNullVal : Convert.ToInt32(bteCtaCompras.Tag);
                Obe.ctacc_icod_cuenta_contable_costos = (Convert.ToInt32(bteCtaCostos.Tag) == 0) ? intNullVal : Convert.ToInt32(bteCtaCostos.Tag);
                Obe.almcc_icod_almacen = Convert.ToInt32(lkpAlmacen.EditValue);
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.clasc_icod_clasificacion = new BContabilidad().insertarClasificacionProducto(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BContabilidad().modificarClasificacionProducto(Obe);
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
                    this.MiEvento(Obe.clasc_icod_clasificacion);
                    this.Close();
                }
            }
        }
        private bool verificarDescripcion(string strCadena)
        {
            try
            {
                bool exists = false;
                if (lstClasificacionProducto.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstClasificacionProducto.Where(x => x.clasc_vdescripcion.Trim() == strCadena.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstClasificacionProducto.Where(x => x.clasc_vdescripcion.Trim() == strCadena.Trim() && x.clasc_icod_clasificacion != Obe.clasc_icod_clasificacion).ToList().Count > 0)
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

        private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCuenta(sender);
        }

        private void bteCtaCompras_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCuenta(sender);
        }

        private void bteCtaCostos_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCuenta(sender);
        }

        private void listarCuenta(object sender)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();
                    switch (opcion.Name)
                    {
                        case "bteCuenta":
                            txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;
                            break;
                        case "bteCtaCompras":
                            txtCuentaCompras.Text = frm._Be.ctacc_nombre_descripcion;
                            break;
                        case "bteCtaCostos":
                            txtCuentaCostos.Text = frm._Be.ctacc_nombre_descripcion;
                            break;
                    }
                }
            }
        }

        private void frmManteClasificacion_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpAlmacen, new BContabilidad().listarAlmacenContable(), "almcc_vdescripcion", "almcc_icod_almacen", true);
        }
    }
}