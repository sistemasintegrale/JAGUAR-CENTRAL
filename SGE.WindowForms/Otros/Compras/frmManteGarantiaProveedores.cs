using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Otros.Operaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteGarantiaProveedores : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteGarantiaProveedores));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EGarantiaProveedores Obe = new EGarantiaProveedores();
        public List<EGarantiaProveedores> lstGarantiaProveedores = new List<EGarantiaProveedores>();
        public int Almacen = 0;
        public int Proyecto = 0;

        public frmManteGarantiaProveedores()
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
            //bool Enabled = (Status == BSMaintenanceStatus.ModifyCurrent);
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                dteFecha.Enabled = Enabled;
                lkpSituacion.Enabled = Enabled;
                btnGuardar.Enabled = Enabled;
                bteProveedores.Enabled = Enabled;
                bteProyecto.Enabled = Enabled;
                bteOCS.Enabled = Enabled;
                bteFactura.Enabled = Enabled;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                dteFecha.Enabled = Enabled;
                lkpSituacion.Enabled = !Enabled;
                btnGuardar.Enabled = Enabled;
                bteProveedores.Enabled = !Enabled;
                bteProyecto.Enabled = !Enabled;
                bteOCS.Enabled = !Enabled;
                bteFactura.Enabled = !Enabled;
            }
            if (Status == BSMaintenanceStatus.View)
            {
                txtNumero.Enabled = !Enabled;
                dteFecha.Enabled = !Enabled;
                lkpSituacion.Enabled = !Enabled;
                bteProveedores.Enabled = !Enabled;
                bteProyecto.Enabled = !Enabled;
                bteOCS.Enabled = !Enabled;
                bteFactura.Enabled = !Enabled;
                btnGuardar.Enabled = !Enabled;
                lkpMoneda.Enabled = !Enabled;
                txtMonto.Enabled = !Enabled;
            }
        }
        public void setValues()
        {

            txtNumero.Text = Obe.garap_vnumero_garantia;
            dteFecha.EditValue = Obe.garp_sfecha_garantia;
            lkpSituacion.EditValue = Obe.tablc_iid_situacion;
            bteProveedores.Tag = Obe.proc_icod_proveedor;
            bteProveedores.Text = Obe.NomProv;
            bteProyecto.Tag = Obe.pryc_icod_proyecto;
            bteCCosto.Text = Obe.CentroCostos;
            bteProyecto.Text = Obe.DesProyecto;
            bteOCS.Tag = Obe.ocsc_icod_ocs;
            bteOCS.Text = Obe.NumOCS;
            lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
            txtMonto.Text = Obe.garp_nmonto.ToString();
            bteFactura.Tag = Obe.fcoc_icod_doc;
            bteFactura.Text = Obe.NumDoc;

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
                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Garantia");
                }
                if (Convert.ToInt32(bteProveedores.Tag) == 0)
                {
                    oBase = bteProveedores;
                    throw new ArgumentException("Seleccione Proveedor");
                }
                if (Convert.ToInt32(bteFactura.Tag) == 0)
                {
                    oBase = bteProveedores;
                    throw new ArgumentException("Seleccione Factura");
                }
                Obe.garap_vnumero_garantia = string.Format("{0:000000}", txtNumero.Text);
                Obe.garp_sfecha_garantia = Convert.ToDateTime(dteFecha.EditValue);
                Obe.tablc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.proc_icod_proveedor = Convert.ToInt32(bteProveedores.Tag);
                Obe.pryc_icod_proyecto = Convert.ToInt32(bteProyecto.Tag);
                Obe.ocsc_icod_ocs = Convert.ToInt32(bteOCS.Tag);
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.fcoc_icod_doc = Convert.ToInt32(bteFactura.Tag);
                Obe.garp_nmonto = Convert.ToDecimal(txtMonto.Text);
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.garp_flag_estado = true;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.pryc_icod_proyecto = new BCompras().insertarGarantiaProveedores(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {

                    new BCompras().modificarGarantiaProveedores(Obe);
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
                    this.MiEvento(Obe.pryc_icod_proyecto);
                    this.Close();
                }
            }
        }

        private bool verificarNombre(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstGarantiaProveedores.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstGarantiaProveedores.Where(x => x.garap_vnumero_garantia.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstGarantiaProveedores.Where(x => x.garap_vnumero_garantia.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim() && x.pryc_icod_proyecto != Obe.pryc_icod_proyecto).ToList().Count > 0)
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
        private bool verificarCodigo(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstGarantiaProveedores.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        //if (lstGarantiaProveedores.Where(x => x.pryc_vcorrelativo == (strCodigo)).ToList().Count > 0)
                        //    exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        //if (lstGarantiaProveedores.Where(x => x.pryc_vcorrelativo == (strCodigo) && x.pryc_icod_proyecto != Obe.pryc_icod_proyecto).ToList().Count > 0)
                        //    exists = true;
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

        private void frmMantePersonal_Load(object sender, EventArgs e)
        {
            dteFecha.EditValue = DateTime.Now;
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(82), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

        }


        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProveedores();
        }
        private void ListarProveedores()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                bteProveedores.Tag = Proveedor._Be.iid_icod_proveedor;
                bteProveedores.Text = Proveedor._Be.vnombrecompleto;
            }
        }

        private void bteCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCentroCosto();
        }
        private void ListarCentroCosto()
        {
            using (frmListarCentroCostoProyectos Ccosto = new frmListarCentroCostoProyectos())
            {

                if (Ccosto.ShowDialog() == DialogResult.OK)
                {
                    bteCCosto.Text = Ccosto._Be.cecoc_vcodigo_centro_costo;//cecoc_ccodigo_centro_costo - centro_costo
                    bteCCosto.Tag = Ccosto._Be.cecoc_icod_centro_costo;//cecoc_icod_centro_costo (correlativo) - centro_costo

                }
            }
        }

        private void bteOCS_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarOrdenCompraServicios();
        }
        private void ListarOrdenCompraServicios()
        {
            FrmListarOrdenCompraServicio OrdenCompra = new FrmListarOrdenCompraServicio();
            OrdenCompra.proc_icod_proveedor = Convert.ToInt32(bteProveedores.Tag);
            OrdenCompra.Carga();
            if (OrdenCompra.ShowDialog() == DialogResult.OK)
            {
                bteOCS.Tag = OrdenCompra._Be.ocsc_icod_ocs;
                bteOCS.Text = OrdenCompra._Be.ocsc_vnumero_ocs;
            }
        }

        private void bteProyecto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProyecto();
        }
        private void ListarProyecto()
        {
            using (frmListarProyectos frm = new frmListarProyectos())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteProyecto.Tag = frm._Be.pryc_icod_proyecto;
                    bteProyecto.Text = frm._Be.pryc_vdescripcion;
                    bteCCosto.Text = frm._Be.CentroCosto;
                }
            }
        }

        private void bteFactura_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarFacCompraGP();
        }
        private void ListarFacCompraGP()
        {
            try
            {
                using (FrmListarDXPGarantiaProveedores frm = new FrmListarDXPGarantiaProveedores())
                {
                    frm.proc_icod_proveedor = Convert.ToInt32(bteProveedores.Tag);
                    frm.Carga();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteFactura.Tag = frm._oBe.doxpc_icod_correlativo;
                        bteFactura.Text = frm._oBe.doxpc_vnumero_doc;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}