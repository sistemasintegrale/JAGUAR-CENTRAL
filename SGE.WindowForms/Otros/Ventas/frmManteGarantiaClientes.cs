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
namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteGarantiaClientes : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteGarantiaClientes));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EGarantiaClientes Obe = new EGarantiaClientes();
        public List<EGarantiaClientes> lstGarantiaClientes = new List<EGarantiaClientes>();
        public int Almacen = 0;
        public int Proyecto = 0;

        public frmManteGarantiaClientes()
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
                bteClientes.Enabled = Enabled;
                bteProyecto.Enabled = Enabled;
                bteFactura.Enabled = Enabled;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                dteFecha.Enabled = Enabled;
                lkpSituacion.Enabled = !Enabled;
                btnGuardar.Enabled = Enabled;
                bteClientes.Enabled = !Enabled;
                bteProyecto.Enabled = !Enabled;
                bteFactura.Enabled = !Enabled;
            }
            if (Status == BSMaintenanceStatus.View)
            {
                txtNumero.Enabled = !Enabled;
                dteFecha.Enabled = !Enabled;
                lkpSituacion.Enabled = !Enabled;
                bteClientes.Enabled = !Enabled;
                bteProyecto.Enabled = !Enabled;
                bteFactura.Enabled = !Enabled;
                btnGuardar.Enabled = !Enabled;
                lkpMoneda.Enabled = !Enabled;
                txtMonto.Enabled = !Enabled;
            }
        }
        public void setValues()
        {

            txtNumero.Text = Obe.garc_vnumero_garantia;
            dteFecha.EditValue = Obe.garc_sfecha_garantia;
            lkpSituacion.EditValue = Obe.tablc_iid_situacion;
            bteClientes.Tag = Obe.cliec_icod_cliente;
            bteClientes.Text = Obe.NomClie;
            bteProyecto.Tag = Obe.pryc_icod_proyecto;
            bteCCosto.Text = Obe.CentroCostos;
            bteProyecto.Text = Obe.DesProyecto;
            lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
            txtMonto.Text = Obe.garc_nmonto.ToString();
            bteFactura.Tag = Obe.favc_icod_factura;
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
                    throw new ArgumentException("Ingrese Nro. de Proyecto");
                }
                if (Convert.ToInt32(bteClientes.Tag) == 0)
                {
                    oBase = bteClientes;
                    throw new ArgumentException("Seleccione Cliente");
                }
                if (Convert.ToInt32(bteFactura.Tag) == 0)
                {
                    oBase = bteClientes;
                    throw new ArgumentException("Seleccione Factura");
                }
                Obe.garc_vnumero_garantia = string.Format("{0:000000}", txtNumero.Text);
                Obe.garc_sfecha_garantia = Convert.ToDateTime(dteFecha.EditValue);
                Obe.tablc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.cliec_icod_cliente = Convert.ToInt32(bteClientes.Tag);
                Obe.pryc_icod_proyecto = Convert.ToInt32(bteProyecto.Tag);
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.favc_icod_factura = Convert.ToInt32(bteFactura.Tag);
                Obe.garc_nmonto = Convert.ToDecimal(txtMonto.Text);
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.garc_flag_estado = true;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.pryc_icod_proyecto = new BVentas().insertarGarantiaClientes(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarGarantiaClientes(Obe);
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
                if (lstGarantiaClientes.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstGarantiaClientes.Where(x => x.garc_vnumero_garantia.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstGarantiaClientes.Where(x => x.garc_vnumero_garantia.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim() && x.pryc_icod_proyecto != Obe.pryc_icod_proyecto).ToList().Count > 0)
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
                if (lstGarantiaClientes.Count > 0)
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
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(83), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

        }


        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarClientes();
        }
        private void ListarClientes()
        {
            FrmListarCliente Proveedor = new FrmListarCliente();
            //Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                bteClientes.Tag = Proveedor._Be.cliec_icod_cliente;
                bteClientes.Text = Proveedor._Be.cliec_vnombre_cliente;
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
            ListarFacVentas();
        }
        private void ListarFacVentas()
        {
            try
            {

                using (FrmListarDXCGarantiaClientes frm = new FrmListarDXCGarantiaClientes())
                {
                    frm.favc_icod_cliente = Convert.ToInt32(bteClientes.Tag);
                    frm.cargar();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteFactura.Tag = frm.EDocPorCobrar.doxcc_icod_correlativo;
                        bteFactura.Text = frm.EDocPorCobrar.doxcc_vnumero_doc;
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