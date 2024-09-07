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
    public partial class frmManteGuiaRemisionCompras : DevExpress.XtraEditors.XtraForm
    {
        public EGuiaRemisionCompras oBe = new EGuiaRemisionCompras();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public List<EGuiaRemisionComprasDetalle> lstDetalle = new List<EGuiaRemisionComprasDetalle>();
        List<EGuiaRemisionComprasDetalle> lstDelete = new List<EGuiaRemisionComprasDetalle>();

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        private void frmManteGuiaRemisionCompras_Load(object sender, EventArgs e) => cargar();
        private void cargar() => CargarControles();
        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) => ListarProveedores();
        private void btnOrdenCompra_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) => ListarOrdenCompra();
        private void btnConsultar_Click(object sender, EventArgs e) => ConsultarDetalle();
        private void btnOrdenCompra_EditValueChanged(object sender, EventArgs e) { }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => setSave();
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => Close();
        public frmManteGuiaRemisionCompras() => InitializeComponent();

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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.Enabled = false;
                txtNumero.Enabled = false;
                dteFecha.Enabled = false;
                btnConsultar.Enabled = false;
                bteProveedor.Enabled = false;
                btnOrdenCompra.Enabled = false;
                dteFechaIngreso.Enabled = false;
            }

            if (Status == BSMaintenanceStatus.View)
            {
                txtSerie.Enabled = false;
                txtNumero.Enabled = false;
                dteFecha.Enabled = false;
                btnConsultar.Enabled = false;
                bteProveedor.Enabled = false;
                btnOrdenCompra.Enabled = false;
                dteFechaIngreso.Enabled = false;
                btnGuardar.Enabled = false;
            }
        }
        private void setValues()
        {
            txtSerie.Text = oBe.grcc_vnumero_grc.Substring(0, 4);
            txtNumero.Text = oBe.grcc_vnumero_grc.Substring(4, oBe.grcc_vnumero_grc.Length - 4);
            dteFecha.EditValue = oBe.grcc_sfecha_grc;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion_grc; ;
            dteFechaIngreso.EditValue = oBe.grcc_sfecha_ingreso;
            bteProveedor.Tag = oBe.proc_icod_proveedor;
            bteProveedor.Text = oBe.NomProveedor;
            btnOrdenCompra.Tag = oBe.ococ_icod_orden_compra;
            btnOrdenCompra.Text = oBe.NumOC;
            btnAlmacenIngreso.Tag = oBe.almac_icod_almacen;
            btnAlmacenIngreso.Text = oBe.DesAlmacen;

            lstDetalle = new BCompras().listarGuiaRemisionComprasDetalle(oBe.grcc_icod_grc);
            lstDetalle.OrderBy(E => E.grcd_iid_detalle);
            grdGuiaRemision.DataSource = lstDetalle;
            lstDetalle.ForEach(x =>
            {
                x.grcd_ncantidad2 = x.grcd_ncantidad;
                x.CantidadRecibidaGR = x.CantidadRecibidaOC - x.grcd_ncantidad;
            });
            btnConsultar.Enabled = lstDetalle.Count() == 0;
        }


        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            SetFecha();

        }




        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();

        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            setValues();
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

        public void CalcularMontos()
        {
            txtCantTotal.Text = lstDetalle.Sum(ob => ob.CantidadSaldo).ToString();
            txtItems.Text = lstDetalle.Count().ToString();
            txtRecibido.Text = lstDetalle.Sum(x => x.grcd_ncantidad).ToString();
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
                if (Convert.ToDecimal(txtRecibido.Text) == 0)
                {
                    oBase = txtRecibido;
                    throw new ArgumentException("No se Ingresó Ninguna Cantidad Recibida");
                }
                oBe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                oBe.ococ_icod_orden_compra = Convert.ToInt32(btnOrdenCompra.Tag);
                oBe.grcc_vnumero_grc = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.grcc_sfecha_grc = Convert.ToDateTime(dteFecha.Text);
                oBe.tablc_iid_motivo = 97;//COMPRAS
                oBe.tablc_iid_situacion_grc = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.grcc_ncantidad = Convert.ToDecimal(txtCantTotal.Text);
                oBe.grcc_sfecha_ingreso = Convert.ToDateTime(dteFechaIngreso.EditValue);
                oBe.grcc_ncantidad = Convert.ToDecimal(txtCantTotal.Text);
                oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacenIngreso.Tag);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.grcc_flag_estatdo = true;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.grcc_icod_grc = new BCompras().insertarGuiaRemisionCompras(oBe, lstDetalle);
                }
                else
                {
                    new BCompras().modificarGuiaRemisionCompras(oBe, lstDetalle, lstDelete);
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
                    MiEvento(oBe.grcc_icod_grc);
                    Close();
                }
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemisionComprasDetalle obe = (EGuiaRemisionComprasDetalle)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteGuiaRemisionDetalleCompras frm = new frmManteGuiaRemisionDetalleCompras())
            {

                frm.oBe = obe;
                frm.lstDetalle = lstDetalle;
                frm.Cantidad = obe.grcd_ncantidad;
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

        private void ListarOrdenCompra()
        {
            FrmListarOrdenCompra OrdenCompra = new FrmListarOrdenCompra();
            OrdenCompra.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
            OrdenCompra.Carga();
            if (OrdenCompra.ShowDialog() == DialogResult.OK)
            {
                btnOrdenCompra.Tag = OrdenCompra._Be.ococ_icod_orden_compra;
                btnOrdenCompra.Text = OrdenCompra._Be.ococ_numero_orden_compra;
                btnConsultar.Enabled = true;
            }
        }

        public void ConsultarDetalle()
        {
            if (lstDetalle.Count == 0)
            {
                List<EOrdenCompra> lstOrdenCompraDetalle = new List<EOrdenCompra>();
                lstOrdenCompraDetalle = new BCompras().ListarOrdenCompraDetalle(Convert.ToInt32(btnOrdenCompra.Tag));
                foreach (var _bee in lstOrdenCompraDetalle)
                {
                    EGuiaRemisionComprasDetalle BGRCOMPRAS = new EGuiaRemisionComprasDetalle();
                    BGRCOMPRAS.grcd_iid_detalle = _bee.ocod_iitem;
                    BGRCOMPRAS.strCodProd = _bee.strCodigoProducto;
                    BGRCOMPRAS.DesProducto = _bee.strDescProducto;
                    BGRCOMPRAS.Unidad = _bee.strAbrevUniMed;
                    BGRCOMPRAS.ocod_icod_detalle_oc = _bee.ocod_icod_detalle_oc;
                    BGRCOMPRAS.prdc_icod_producto = Convert.ToInt32(_bee.prdc_icod_producto);
                    BGRCOMPRAS.grcd_flag_estado = _bee.ocod_flag_estado;
                    BGRCOMPRAS.grcd_ncantidad = BGRCOMPRAS.grcd_ncantidad;
                    BGRCOMPRAS.CantidadSaldo = _bee.ocod_ncantidad;
                    BGRCOMPRAS.CantidadRecibidaGR = _bee.ocod_ncantidad_recibida;
                    BGRCOMPRAS.intUsuario = Valores.intUsuario;
                    BGRCOMPRAS.strPc = WindowsIdentity.GetCurrent().Name;
                    lstDetalle.Add(BGRCOMPRAS);
                    lstDetalle.OrderBy(E => E.grcd_iid_detalle).ToList();
                    grdGuiaRemision.DataSource = lstDetalle;
                    CalcularMontos();
                }
                btnConsultar.Enabled = false;
                grdGuiaRemision.RefreshDataSource();
            }
        }



    }
}