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

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteRecepcionComprasSuministros : DevExpress.XtraEditors.XtraForm
    {
        public ERecepcionComprasSuministros oBe = new ERecepcionComprasSuministros();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public List<ERecepcionComprasSuministrosDetalle> lstDetalle = new List<ERecepcionComprasSuministrosDetalle>();
        List<ERecepcionComprasSuministrosDetalle> lstDelete = new List<ERecepcionComprasSuministrosDetalle>();

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
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            //txtSerie.Enabled = !Enabled;
            txtNumero.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;
            //lkpSituacion.Enabled = !Enabled;
            bteProveedor.Enabled = !Enabled;
            txtNombreComercial.Enabled = !Enabled;
            txtLicencia.Enabled = !Enabled;
            txtMatricula.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                //txtSerie.Enabled = !Enabled;
                txtNumero.Enabled = !Enabled;
                dteFecha.Enabled = !Enabled;
                btnConsultar.Enabled = Enabled;
                bteProveedor.Enabled = !Enabled;
                btnOrdenCompra.Enabled = !Enabled;
            }
        }
        private void setValues()
        {
            txtSerie.Text = oBe.rcsc_vnumero_rcs.Substring(0, 4);
            txtNumero.Text = oBe.rcsc_vnumero_rcs.Substring(4, oBe.rcsc_vnumero_rcs.Length - 4);
            dteFecha.EditValue = oBe.rcsc_sfecha_rcs;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion_rcs; ;
            dteFechaIngreso.EditValue = oBe.rcsc_sfecha_ingreso;
            bteProveedor.Tag = oBe.proc_icod_proveedor;
            bteProveedor.Text = oBe.NomProveedor;
            btnOrdenCompra.Tag = oBe.ococ_icod_orden_compra;
            btnOrdenCompra.Text = oBe.NumOC;
            btnAlmacenIngreso.Tag = oBe.almac_icod_almacen;
            btnAlmacenIngreso.Text = oBe.DesAlmacen;

            lstDetalle = new BCompras().listarRecepcionComprasSuministrosDetalle(oBe.rcsc_icod_rcs);
            lstDetalle.OrderBy(E => E.rcsd_iid_detalle);
            grdGuiaRemision.DataSource = lstDetalle;
            lstDetalle.ForEach(x =>
            {
                x.rcsd_ncantidad2 = x.rcsd_ncantidad;
                x.CantidadRecibidaGR = x.CantidadRecibidaOC - x.rcsd_ncantidad;
            });
        }

        public frmManteRecepcionComprasSuministros()
        {
            InitializeComponent();
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            SetFecha();
            GetRCS();
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();
            //ConsultarDetalleModificar();           
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            setValues();
        }
        private void frmManteGuiaRemisionCompras_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void GetRCS()
        {
            string correlativo = new BAdministracionSistema().ObtenerCorrelativoRCS(Convert.ToInt32(txtSerie.Text));
            txtNumero.Text = correlativo;

        }
        private void cargar()
        {
            CargarControles();

        }
        private void CargarControles()
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionGuiaRemision), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }
        private void SetFecha()
        {
            dteFecha.EditValue = DateTime.Now;
            dteFechaIngreso.EditValue = DateTime.Now;
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmManteRecepcionComprasSuministrosDetalle frm = new frmManteRecepcionComprasSuministrosDetalle())
            {
                frm.oBeCab = oBe;
                frm.SetInsert();
                frm.lstDetalle = lstDetalle;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewGuiaRemision.RefreshData();
                    viewGuiaRemision.MoveLast();
                    CalcularMontos();
                }
            }
        }
        public void CalcularMontos()
        {
            txtCantTotal.Text = lstDetalle.Sum(ob => ob.CantidadSaldo).ToString();
            txtItems.Text = lstDetalle.Count().ToString();
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (txtSerie.Text == "0")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese Nro. de Serie de Guía de Remisión");
                }

                if (txtSerie.Text == "000")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("N° de Serie no registrado, registrar N° serie en REGISTRO DE TIPOS DE DOCUMENTOS");
                }

                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Guía de Remisión");
                }

                if (Convert.ToDateTime(dteFecha.Text).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del ejercicio");
                }

                if (Convert.ToInt32(bteProveedor.Tag) == 0)
                {
                    oBase = bteProveedor;
                    throw new ArgumentException("Seleccione el Proveedor");
                }
                if (Convert.ToInt32(btnAlmacenIngreso.Tag) == 0)
                {
                    oBase = btnAlmacenIngreso;
                    throw new ArgumentException("Seleccione Almacen");
                }
                oBe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                oBe.ococ_icod_orden_compra = Convert.ToInt32(btnOrdenCompra.Tag);
                oBe.rcsc_vnumero_rcs = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.rcsc_sfecha_rcs = Convert.ToDateTime(dteFecha.Text);
                oBe.tablc_iid_motivo = 97;//COMPRAS
                oBe.tablc_iid_situacion_rcs = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.rcsc_ncantidad = Convert.ToDecimal(txtCantTotal.Text);
                oBe.rcsc_sfecha_ingreso = Convert.ToDateTime(dteFechaIngreso.EditValue);
                oBe.rcsc_ncantidad = Convert.ToDecimal(txtCantTotal.Text);
                oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacenIngreso.Tag);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.rcsc_flag_estatdo = true;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.rcsc_icod_rcs = new BCompras().insertarRecepcionComprasSuministros(oBe, lstDetalle);
                }
                else
                {
                    new BCompras().modificarRecepcionComprasSuministros(oBe, lstDetalle, lstDelete);
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
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    MiEvento(oBe.rcsc_icod_rcs);
                    Close();
                }
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ERecepcionComprasSuministrosDetalle obe = (ERecepcionComprasSuministrosDetalle)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteRecepcionComprasSuministrosDetalle frm = new frmManteRecepcionComprasSuministrosDetalle())
            {

                frm.oBe = obe;
                frm.lstDetalle = lstDetalle;
                frm.Cantidad = obe.rcsd_ncantidad;
                frm.Cantidad_Recibida_GR = obe.CantidadRecibidaGR;
                frm.Cantidad_Recibida_OC = obe.CantidadSaldo;
                frm.SetModify();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewGuiaRemision.RefreshData();
                    viewGuiaRemision.MoveLast();
                    CalcularMontos();
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ERecepcionComprasSuministrosDetalle obe = (ERecepcionComprasSuministrosDetalle)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            viewGuiaRemision.RefreshData();
            CalcularMontos();
        }
        private void btnAlmacenIngreso_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacenIngreso();
        }
        private void listarAlmacenIngreso()
        {
            using (frmListarAlmacen frm = new frmListarAlmacen())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnAlmacenIngreso.Tag = frm._Be.almac_icod_almacen;
                    btnAlmacenIngreso.Text = frm._Be.almac_vdescripcion;

                }
            }
        }
        private void txtItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }
        private void txtdias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }
        private void txtPorIGV_EditValueChanged(object sender, EventArgs e)
        {
            CalcularMontos();
        }
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacenIngreso();
        }
        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProveedores();
        }
        private void ListarProveedores()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                bteProveedor.Tag = Proveedor._Be.iid_icod_proveedor;
                bteProveedor.Text = Proveedor._Be.vnombrecompleto;
            }
        }
        private void btnOrdenCompra_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarOrdenCompra();
        }
        private void ListarOrdenCompra()
        {
            FrmListarOrdenCompra OrdenCompra = new FrmListarOrdenCompra();
            OrdenCompra.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
            OrdenCompra.Carga();
            if (OrdenCompra.ShowDialog() == DialogResult.OK)
            {
                btnOrdenCompra.Tag = OrdenCompra._Be.ococ_icod_orden_compra;
                btnOrdenCompra.Text = OrdenCompra._Be.ococ_numero_orden_compra;
            }
        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            ConsultarDetalle();
        }
        public void ConsultarDetalle()
        {
            List<EOrdenCompra> lstOrdenCompraDetalle = new List<EOrdenCompra>();
            lstOrdenCompraDetalle = new BCompras().ListarOrdenCompraDetalle(Convert.ToInt32(btnOrdenCompra.Tag));
            foreach (var _bee in lstOrdenCompraDetalle)
            {
                ERecepcionComprasSuministrosDetalle BGRCOMPRAS = new ERecepcionComprasSuministrosDetalle();
                BGRCOMPRAS.rcsd_iid_detalle = _bee.ocod_iitem;
                BGRCOMPRAS.strCodProd = _bee.strCodigoProducto;
                BGRCOMPRAS.DesProducto = _bee.strDescProducto;
                BGRCOMPRAS.Unidad = _bee.strAbrevUniMed;
                BGRCOMPRAS.ocod_icod_detalle_oc = _bee.ocod_icod_detalle_oc;
                BGRCOMPRAS.prdc_icod_producto = Convert.ToInt32(_bee.prdc_icod_producto);
                BGRCOMPRAS.rcsd_flag_estado = _bee.ocod_flag_estado;
                BGRCOMPRAS.rcsd_ncantidad = BGRCOMPRAS.rcsd_ncantidad;
                BGRCOMPRAS.CantidadSaldo = _bee.ocod_ncantidad;
                lstDetalle.Add(BGRCOMPRAS);
                lstDetalle.OrderBy(E => E.rcsd_iid_detalle).ToList();
                grdGuiaRemision.DataSource = lstDetalle;
                CalcularMontos();
            }
            btnConsultar.Enabled = false;
            grdGuiaRemision.RefreshDataSource();

        }
        public void ConsultarDetalleModificar()
        {
            List<EOrdenCompra> lstOrdenCompraDetalle = new List<EOrdenCompra>();
            List<ERecepcionComprasSuministrosDetalle> lstDetalleOrdenada = new List<ERecepcionComprasSuministrosDetalle>();
            lstOrdenCompraDetalle = new BCompras().ListarOrdenCompraDetalle(Convert.ToInt32(btnOrdenCompra.Tag)).Where(x => x.ocod_ncantidad_recibida == 0).ToList();
            foreach (var _bee in lstOrdenCompraDetalle)
            {
                ERecepcionComprasSuministrosDetalle BGRCOMPRAS = new ERecepcionComprasSuministrosDetalle();
                BGRCOMPRAS.rcsd_iid_detalle = _bee.ocod_iitem;
                BGRCOMPRAS.strCodProd = _bee.strCodigoProducto;
                BGRCOMPRAS.DesProducto = _bee.strDescProducto;
                BGRCOMPRAS.Unidad = _bee.strAbrevUniMed;
                BGRCOMPRAS.ocod_icod_detalle_oc = _bee.ocod_icod_detalle_oc;
                BGRCOMPRAS.prdc_icod_producto = Convert.ToInt32(_bee.prdc_icod_producto);
                BGRCOMPRAS.rcsd_flag_estado = _bee.ocod_flag_estado;
                BGRCOMPRAS.CantidadSaldo = _bee.ocod_ncantidad;
                BGRCOMPRAS.CantidadRecibidaOC = _bee.ocod_ncantidad_recibida;
                BGRCOMPRAS.rcsd_ncantidad = BGRCOMPRAS.rcsd_ncantidad;
                lstDetalle.Add(BGRCOMPRAS);
                lstDetalleOrdenada = lstDetalle.OrderBy(x => x.rcsd_iid_detalle).ToList();
                grdGuiaRemision.DataSource = lstDetalleOrdenada;
                CalcularMontos();
            }
            btnConsultar.Enabled = false;
            grdGuiaRemision.RefreshDataSource();
        }
    }
}