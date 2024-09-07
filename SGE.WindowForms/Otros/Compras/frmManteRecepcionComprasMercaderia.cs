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
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteRecepcionComprasMercaderia : DevExpress.XtraEditors.XtraForm
    {
        public ERecepcionComprasMercaderia oBe = new ERecepcionComprasMercaderia();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public List<ERecepcionComprasMercaderiaDetalle> lstDetalle = new List<ERecepcionComprasMercaderiaDetalle>();
        List<ERecepcionComprasMercaderiaDetalle> lstDelete = new List<ERecepcionComprasMercaderiaDetalle>();
        public int Indicador;
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
            txtSerie.Enabled = !Enabled;
            txtNumero.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;
            //lkpSituacion.Enabled = !Enabled;
            bteProveedor.Enabled = !Enabled;
            txtNombreComercial.Enabled = !Enabled;
            txtLicencia.Enabled = !Enabled;
            txtMatricula.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.Enabled = !Enabled;
                txtNumero.Enabled = !Enabled;
                dteFecha.Enabled = !Enabled;
                //btnConsultar.Enabled = Enabled;
                bteProveedor.Enabled = !Enabled;
                btnOrdenCompra.Enabled = !Enabled;
            }
        }
        private void setValues()
        {
            txtSerie.Text = oBe.rcmc_vnumero_rcm.Substring(0, 4);
            txtNumero.Text = oBe.rcmc_vnumero_rcm.Substring(4, oBe.rcmc_vnumero_rcm.Length - 4);
            dteFecha.EditValue = oBe.rcmc_sfecha_rcm;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion_rcm; ;
            dteFechaIngreso.EditValue = oBe.rcmc_sfecha_ingreso;
            bteProveedor.Tag = oBe.proc_icod_proveedor;
            bteProveedor.Text = oBe.NomProveedor;
            btnOrdenCompra.Tag = oBe.ococ_icod_orden_compra;
            btnOrdenCompra.Text = oBe.NumOC;
            lkpAlmacen.EditValue = oBe.almac_icod_almacen;
            //lkpAlmacen.Text = oBe.DesAlmacen;

            lstDetalle = new BCompras().listarRecepcionComprasMercaderiaDetalle(oBe.rcmc_icod_rcm);
            lstDetalle.OrderBy(E => E.rcmd_iid_detalle);
            grdGuiaRemision.DataSource = lstDetalle;
            lstDetalle.ForEach(x =>
            {
                x.rcmd_ncantidad2 = x.rcmd_ncantidad;
                x.CantidadRecibidaGR = x.CantidadRecibidaOC - x.rcmd_ncantidad;
            });
        }

        public frmManteRecepcionComprasMercaderia()
        {
            InitializeComponent();
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
        private void cargar()
        {
            CargarControles();

        }
        private void CargarControles()
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionGuiaRemision), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

            if (Status == BSMaintenanceStatus.CreateNew)
            {
                //int index = new BCentral().ListarProdAlmacen().FindIndex(x => x.id_almacen == 11);
                BSControls.LoaderLook(lkpAlmacen, new BCentral().ListarProdAlmacen().Where(x => x.puvec_icod_punto_venta == 2).ToList(), "descripcion", "id_almacen", true);
                //lkpAlmacen.ItemIndex = index;
            }
            else
            {
                BSControls.LoaderLook(lkpAlmacen, new BCentral().ListarProdAlmacen(), "descripcion", "id_almacen", true);
            }

        }
        private void SetFecha()
        {
            dteFecha.EditValue = DateTime.Now;
            dteFechaIngreso.EditValue = DateTime.Now;
        }
        public void CalcularMontos()
        {
            txtCantTotal.Text = lstDetalle.Sum(ob => ob.rcmd_ncantidad).ToString();
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
                if (Convert.ToInt32(lkpAlmacen.EditValue) == 0)
                {
                    oBase = lkpAlmacen;
                    throw new ArgumentException("Seleccione Almacen");
                }
                oBe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                oBe.ococ_icod_orden_compra = Convert.ToInt32(btnOrdenCompra.Tag);
                oBe.rcmc_vnumero_rcm = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.rcmc_sfecha_rcm = Convert.ToDateTime(dteFecha.Text);
                oBe.tablc_iid_motivo = 222;//COMPRAS
                oBe.tablc_iid_situacion_rcm = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.rcmc_ncantidad = Convert.ToDecimal(txtCantTotal.Text);
                oBe.rcmc_sfecha_ingreso = Convert.ToDateTime(dteFechaIngreso.EditValue);
                oBe.rcmc_ncantidad = Convert.ToDecimal(txtCantTotal.Text);
                oBe.almac_icod_almacen = Convert.ToInt32(lkpAlmacen.EditValue);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.rcmc_flag_estatdo = true;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.rcmc_icod_rcm = new BCompras().insertarRecepcionComprasMercaderia(oBe, lstDetalle);
                }
                else
                {
                    new BCompras().modificarRecepcionComprasMercaderia(oBe, lstDetalle, lstDelete);
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
                    MiEvento(oBe.rcmc_icod_rcm);
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

        private void btnAlmacenIngreso_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacenIngreso();
        }
        private void listarAlmacenIngreso()
        {
            //using (FrmListarAlmacen Almacen = new FrmListarAlmacen())
            //{
            //    //Almacen.puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
            //    if (Almacen.ShowDialog() == DialogResult.OK)
            //    {
            //        btnAlmacenIngreso.Tag = Almacen._Be.id_almacen;
            //        btnAlmacenIngreso.Text = Almacen._Be.idf_almacen;
            //    }
            //}
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
            FrmListarOrdenCompraMercaderia OrdenCompra = new FrmListarOrdenCompraMercaderia();
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
            if (lstDetalle.Count == 0)
            {
                List<EOrdenCompraMercaderiaDetalle> lstOrdenCompraDetalle = new List<EOrdenCompraMercaderiaDetalle>();
                lstOrdenCompraDetalle = new BCompras().ListarOrdenCompraMercaderiaDetalle(Convert.ToInt32(btnOrdenCompra.Tag));
                foreach (var _bee in lstOrdenCompraDetalle)
                {
                    ERecepcionComprasMercaderiaDetalle BGRCOMPRAS = new ERecepcionComprasMercaderiaDetalle();
                    BGRCOMPRAS.rcmd_iid_detalle = _bee.ocod_iitem;
                    BGRCOMPRAS.strCodProd = _bee.strCodigoProducto.Substring(0, 13);
                    BGRCOMPRAS.DesProducto = _bee.strDescProducto;
                    BGRCOMPRAS.Unidad = _bee.strAbrevUniMed;
                    BGRCOMPRAS.ocod_icod_detalle_oc = _bee.ocod_icod_detalle_oc;
                    BGRCOMPRAS.prdc_icod_producto = Convert.ToInt32(_bee.prdc_icod_producto);
                    BGRCOMPRAS.rcmd_flag_estado = _bee.ocod_flag_estado;
                    //BGRCOMPRAS.grcd_ncantidad = BGRCOMPRAS.grcd_ncantidad;
                    //BGRCOMPRAS.CantidadSaldo = _bee.ocod_ncantidad;
                    //BGRCOMPRAS.CantidadRecibidaGR = _bee.ocod_ncantidad_recibida;
                    BGRCOMPRAS.rcmd_ncantidad = _bee.ocod_ncantidad;
                    BGRCOMPRAS.intUsuario = Valores.intUsuario;
                    BGRCOMPRAS.strPc = WindowsIdentity.GetCurrent().Name;
                    BGRCOMPRAS.rcmd_rango_talla = _bee.ocod_rango_talla;
                    BGRCOMPRAS.rcmd_talla1 = _bee.ocod_talla1;
                    BGRCOMPRAS.rcmd_talla2 = _bee.ocod_talla2;
                    BGRCOMPRAS.rcmd_talla3 = _bee.ocod_talla3;
                    BGRCOMPRAS.rcmd_talla4 = _bee.ocod_talla4;
                    BGRCOMPRAS.rcmd_talla5 = _bee.ocod_talla5;
                    BGRCOMPRAS.rcmd_talla6 = _bee.ocod_talla6;
                    BGRCOMPRAS.rcmd_talla7 = _bee.ocod_talla7;
                    BGRCOMPRAS.rcmd_talla8 = _bee.ocod_talla8;
                    BGRCOMPRAS.rcmd_talla9 = _bee.ocod_talla9;
                    BGRCOMPRAS.rcmd_talla10 = _bee.ocod_talla10;

                    BGRCOMPRAS.CantOC1 = _bee.ocod_cant_talla1;
                    BGRCOMPRAS.CantOC2 = _bee.ocod_cant_talla2;
                    BGRCOMPRAS.CantOC3 = _bee.ocod_cant_talla3;
                    BGRCOMPRAS.CantOC4 = _bee.ocod_cant_talla4;
                    BGRCOMPRAS.CantOC5 = _bee.ocod_cant_talla5;
                    BGRCOMPRAS.CantOC6 = _bee.ocod_cant_talla6;
                    BGRCOMPRAS.CantOC7 = _bee.ocod_cant_talla7;
                    BGRCOMPRAS.CantOC8 = _bee.ocod_cant_talla8;
                    BGRCOMPRAS.CantOC9 = _bee.ocod_cant_talla9;
                    BGRCOMPRAS.CantOC10 = _bee.ocod_cant_talla10;

                    BGRCOMPRAS.SaldoOC1 = _bee.ocod_cant_saldo1;
                    BGRCOMPRAS.SaldoOC2 = _bee.ocod_cant_saldo2;
                    BGRCOMPRAS.SaldoOC3 = _bee.ocod_cant_saldo3;
                    BGRCOMPRAS.SaldoOC4 = _bee.ocod_cant_saldo4;
                    BGRCOMPRAS.SaldoOC5 = _bee.ocod_cant_saldo5;
                    BGRCOMPRAS.SaldoOC6 = _bee.ocod_cant_saldo6;
                    BGRCOMPRAS.SaldoOC7 = _bee.ocod_cant_saldo7;
                    BGRCOMPRAS.SaldoOC8 = _bee.ocod_cant_saldo8;
                    BGRCOMPRAS.SaldoOC9 = _bee.ocod_cant_saldo9;
                    BGRCOMPRAS.SaldoOC10 = _bee.ocod_cant_saldo10;

                    lstDetalle.Add(BGRCOMPRAS);
                    lstDetalle.OrderBy(E => E.rcmd_iid_detalle).ToList();
                    grdGuiaRemision.DataSource = lstDetalle;
                    CalcularMontos();
                }
                btnConsultar.Enabled = false;
                nuevoToolStripMenuItem.Visible = false;
                eliminarToolStripMenuItem.Visible = false;
                btnOrdenCompra.Enabled = false;
                Indicador = 1;
                grdGuiaRemision.RefreshDataSource();
            }
        }
        public void ConsultarDetalleModificar()
        {
            List<EOrdenCompra> lstOrdenCompraDetalle = new List<EOrdenCompra>();
            List<ERecepcionComprasMercaderiaDetalle> lstDetalleOrdenada = new List<ERecepcionComprasMercaderiaDetalle>();
            lstOrdenCompraDetalle = new BCompras().ListarOrdenCompraDetalle(Convert.ToInt32(btnOrdenCompra.Tag)).Where(x => x.ocod_ncantidad_recibida == 0).ToList();
            foreach (var _bee in lstOrdenCompraDetalle)
            {
                ERecepcionComprasMercaderiaDetalle BGRCOMPRAS = new ERecepcionComprasMercaderiaDetalle();
                BGRCOMPRAS.rcmd_iid_detalle = _bee.ocod_iitem;
                BGRCOMPRAS.strCodProd = _bee.strCodigoProducto;
                BGRCOMPRAS.DesProducto = _bee.strDescProducto;
                BGRCOMPRAS.Unidad = _bee.strAbrevUniMed;
                BGRCOMPRAS.ocod_icod_detalle_oc = _bee.ocod_icod_detalle_oc;
                BGRCOMPRAS.prdc_icod_producto = Convert.ToInt32(_bee.prdc_icod_producto);
                BGRCOMPRAS.rcmd_flag_estado = _bee.ocod_flag_estado;
                BGRCOMPRAS.CantidadSaldo = _bee.ocod_ncantidad;
                BGRCOMPRAS.CantidadRecibidaOC = _bee.ocod_ncantidad_recibida;
                BGRCOMPRAS.rcmd_ncantidad = BGRCOMPRAS.rcmd_ncantidad;
                lstDetalle.Add(BGRCOMPRAS);
                lstDetalleOrdenada = lstDetalle.OrderBy(x => x.rcmd_iid_detalle).ToList();
                grdGuiaRemision.DataSource = lstDetalleOrdenada;
                CalcularMontos();
            }
            btnConsultar.Enabled = false;
            grdGuiaRemision.RefreshDataSource();
        }

        private void nuevoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (frmManteRecepcionComprasMercaderiaDetalle frm = new frmManteRecepcionComprasMercaderiaDetalle())
                {
                    frm.SetInsert();
                    frm.lstDetalle = lstDetalle;
                    frm.txtitem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);

                    //frm.CodOC = CodOC;
                    //frm.OCL = OCL;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        grdGuiaRemision.DataSource = lstDetalle;
                        viewGuiaRemision.RefreshData();
                        viewGuiaRemision.MoveLast();
                    }
                    CalcularMontos();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ERecepcionComprasMercaderiaDetalle obe = (ERecepcionComprasMercaderiaDetalle)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            int index = viewGuiaRemision.FocusedRowHandle;
            using (frmManteRecepcionComprasMercaderiaDetalle frm = new frmManteRecepcionComprasMercaderiaDetalle())
            {
                frm._BE = obe;
                frm.lstDetalle = lstDetalle;
                frm.SetModify();
                frm.txtitem.Text = String.Format("{0:000}", obe.rcmd_iid_detalle);
                frm.btncodigoProducto.Enabled = false;
                frm.txtgrupo.Enabled = false;
                frm.txtgrupo2.Enabled = false;
                frm.btngenerar.Enabled = false;
                frm.Indicador = Indicador;
                //frm.btnGuardar.Enabled = false;
                frm.modificarDeta();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewGuiaRemision.RefreshData();
                    viewGuiaRemision.FocusedRowHandle = index;
                }
                CalcularMontos();
            }
        }

        private void eliminarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (lstDetalle.Count > 0)
            {
                if (XtraMessageBox.Show("Esta seguro de eliminar", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Delete();
                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void Delete()
        {
            ERecepcionComprasMercaderiaDetalle obj = (ERecepcionComprasMercaderiaDetalle)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obj.intTipoOperacion == 1)
            {
                lstDetalle.Remove(obj);
                viewGuiaRemision.RefreshData();
                viewGuiaRemision.MovePrev();
            }
            else
            {
                //creo listado de eliminados para mandarlo a la BD para actualizar su estado
                if (lstDetalle.Count > 0)
                {
                    obj.intTipoOperacion = 3;
                    lstDelete.Add(obj);
                    lstDetalle.Remove(obj);
                    viewGuiaRemision.RefreshData();
                    viewGuiaRemision.MovePrev();
                }

            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}