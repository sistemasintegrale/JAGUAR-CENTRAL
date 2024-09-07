using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmMantePedidoCompraCab : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        /*--------------*/
        public EPedidoProvCab Obe = new EPedidoProvCab();
        public List<EPedidoProvDet> lstDetalle = new List<EPedidoProvDet>();
        public List<EPedidoProvDet> lstDelete = new List<EPedidoProvDet>();
        /*--------------*/

        public frmMantePedidoCompraCab()
        {
            InitializeComponent();
        }

        private void frmManteFacturaCompra_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(55), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            cargar();
        }

        public void setGuardar()
        {
            SetSave();
        }
        private void cargar()
        {
            lstDetalle.Clear();
            lstDetalle = new BCompras().listarPedidoCompraDet(Convert.ToInt32(Obe.lpedi_icod_proveedor), Parametros.intEjercicio);
            foreach (var _be in lstDetalle)
            {
                if (_be.prdc_icod_producto == 0)
                {
                    List<EProducto> MlisProdu = new BAlmacen().listarProductoXCodigp(Parametros.intEjercicio, _be.prdc_vcode_producto);
                    if (MlisProdu.Count() == 1)
                    {
                        _be.prdc_icod_producto = MlisProdu[0].prdc_icod_producto;
                        _be.intTipoOperacion = 2;
                    }
                }
            }

            grdDetalle.DataSource = lstDetalle.OrderBy(i => i.lpedid_item).ToList();

            txtTotalRegistro.Text = lstDetalle.Count().ToString();

            viewDetalle.ClearSorting();
            viewDetalle.Columns["prdc_vdescripcion_larga"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

        }

        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
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
            btnGuardar.Enabled = !Enabled;
            mnu.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
                enableControls(false);

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                //bteRefreshTipoCambio.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.View)
            {
                txtObservacion.Properties.ReadOnly = true;

            }

        }

        private void enableControls(bool Enabled)
        {

            dtFecha.Enabled = Enabled;
            bteProveedor.Enabled = Enabled;

        }

        public void setValues()
        {
            txtNºPedido.Text = Obe.lpedi_Numerolista;
            bteProveedor.Tag = Obe.proc_icod_proveedor;
            bteProveedor.Text = Obe.proc_vnombrecompleto;
            dtFecha.EditValue = Obe.lpedi_sfecha_proveedor;
            txtObservacion.Text = Obe.lpedi_Observaciones;
            btnListaPedido.Tag = Obe.lprec_icod_proveedor;
            btnListaPedido.Text = Obe.lprec_Numerolista;
            lkpSituacion.EditValue = Obe.lpedi_isituacion_prov;
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
                if (bteProveedor.Tag == null)
                {
                    oBase = bteProveedor;
                    throw new ArgumentException("Seleccione proveedor");
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {


                }

                if (lstDetalle.Count == 0)
                {
                    XtraMessageBox.Show("La Factura de Compra, debe tener al menos un registro de un producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Flag = false;
                    return;

                }

                /**/
                DateTime? dtNullVal = null;
                int? intNullVal = null;
                Int16? intNullVal16 = null;

                txtObservacion.Focus();
                Obe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                Obe.lpedi_Numerolista = txtNºPedido.Text;
                Obe.lprec_icod_proveedor = Convert.ToInt32(btnListaPedido.Tag);
                Obe.lpedi_sfecha_proveedor = Convert.ToDateTime(dtFecha.EditValue);
                Obe.lpedi_Observaciones = txtObservacion.Text;
                Obe.lpedi_sflag_estado = true;
                /**/
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.lpedi_isituacion_prov = Convert.ToInt32(lkpSituacion.EditValue);

                foreach (var _be in lstDetalle)
                {
                    _be.intTipoOperacion = 2;
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.lprec_icod_proveedor = new BCompras().insertarPedidoCompra(Obe, lstDetalle);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCompras().modificarPedidoCompra(Obe, lstDetalle, lstDelete);
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
                    this.MiEvento(Obe.lprec_icod_proveedor);
                    this.Close();
                }
            }
        }

        private void listarProveedor()
        {
            FrmListarProveedorMercaderia frm = new FrmListarProveedorMercaderia();
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bteProveedor.Tag = frm._Be.iid_icod_proveedor;
                bteProveedor.Text = frm._Be.vnombrecompleto;
            }
        }


        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProveedor();
            txtObservacion.Focus();
        }

        private void bteProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarProveedor();
        }



        //private void nuevo()
        //{
        //    BaseEdit oBase = null;
        //    try
        //    {

        //        using (frmManteListaPreciosDetalle frm = new frmManteListaPreciosDetalle())
        //        {
        //            frm.CargarControles();

        //            frm.SetInsert();
        //            frm.lstDetalle = lstDetalle;
        //            frm.lkpmoneda.EditValue = 4;//dolares
        //            if (frm.ShowDialog() == DialogResult.OK)
        //            {
        //                lstDetalle = frm.lstDetalle;
        //                viewDetalle.RefreshData();
        //                viewDetalle.MoveLast();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (oBase != null)
        //        {
        //            oBase.Focus();
        //            oBase.ErrorText = ex.Message;
        //            oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
        //        }
        //        XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void modificar()
        //{
        //    EListaPrecioDetalle obe = (EListaPrecioDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
        //    if (obe == null)
        //        return;

        //    int index = viewDetalle.FocusedRowHandle;
        //    using (frmManteListaPreciosDetalle frm = new frmManteListaPreciosDetalle())
        //    {
        //        frm.CargarControles();
        //        frm.obe = obe;
        //        frm.lstDetalle = lstDetalle;
        //        frm.SetModify();
        //        frm.lkpmoneda.Enabled = false;
        //        if (frm.ShowDialog() == DialogResult.OK)
        //        {
        //            lstDetalle = frm.lstDetalle;
        //            viewDetalle.RefreshData();

        //            viewDetalle.FocusedRowHandle = index;
        //        }
        //    }
        //}

        //private void eliminar()
        //{
        //    EListaPrecioDetalle obe = (EListaPrecioDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
        //    if (obe == null)
        //        return;
        //    lstDelete.Add(obe);
        //    lstDetalle.Remove(obe);
        //    viewDetalle.RefreshData();
        //}

        //private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    nuevo();
        //}

        //private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    modificar();
        //}

        //private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    eliminar();
        //}

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void bteRefreshTipoCambio_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    grdDetalle.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    grdDetalle.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }
                grdDetalle.DataSource = null;
                sfdRuta.FileName = string.Empty;
            }
        }

        private void btnListaPedido_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarListaPrecio();
        }
        private void CagarDetalleXGenera()
        {
            lstDetalle.Clear();

            List<EListaPrecioDetalle> mlist = new List<EListaPrecioDetalle>();
            mlist = new BCompras().listarPrecioCompraDet(Convert.ToInt32(btnListaPedido.Tag), 0, Parametros.intEjercicio);
            foreach (var __BE in mlist)
            {
                EPedidoProvDet _BE = new EPedidoProvDet();
                _BE.prdc_icod_producto = __BE.prdc_icod_producto;
                _BE.lpedid_icod_moneda = __BE.lpred_icod_moneda;
                _BE.lpedid_vDesc_moneda = __BE.lpred_vdescripcion_moneda;
                _BE.lpedid_nprecio_lista = __BE.lpred_nprecio_lista;
                _BE.lpedid_nperso_desc = __BE.lpred_nperso_desc;
                _BE.lpedid_nprecio_neto = __BE.lpred_nprecio_neto;
                _BE.lpedid_nstock_producto = __BE.lpedid_nstock_producto;
                _BE.lpedid_ncompras_sem1 = __BE.lpedid_ncompras_sem1;
                _BE.lpedid_ncompras_sem2 = __BE.lpedid_ncompras_sem2;
                _BE.lpedid_ncompras_sem3 = __BE.lpedid_ncompras_sem3;
                _BE.lpedid_ncompras_sem4 = __BE.lpedid_ncompras_sem4;
                _BE.lpedid_nCant_pedido = 0;
                _BE.lpedid_nCosto_pedido = 0;
                _BE.prdc_vdescripcion_larga = __BE.prdc_vdescripcion_larga;
                _BE.prdc_vAutor = __BE.prdc_vAutor;
                _BE.strEditorial = __BE.strEditorial;
                _BE.lpedid_sflag_estado = true;
                _BE.prdc_vcode_producto = __BE.prdc_vcode_producto;

                if (lstDetalle.Count() > 0)
                {
                    _BE.lpedid_item = (lstDetalle.Max(ov => ov.lpedid_item) + 1);
                }
                else
                {
                    _BE.lpedid_item = 1;
                }
                lstDetalle.Add(_BE);
            }
            lstDetalle = lstDetalle.OrderBy(o => o.prdc_vdescripcion_larga).ToList();
            viewDetalle.RefreshData();
            grdDetalle.DataSource = lstDetalle;

        }
        private void listarListaPrecio()
        {
            FrmListarPrecioProveedor frm = new FrmListarPrecioProveedor();
            frm.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnListaPedido.Tag = frm._Be.lprec_icod_proveedor;
                btnListaPedido.Text = frm._Be.lprec_Numerolista;
                CagarDetalleXGenera();

            }
            txtTotalRegistro.Text = lstDetalle.Count().ToString();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }
        private void modificar()
        {
            EPedidoProvDet obe = (EPedidoProvDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;

            int index = viewDetalle.FocusedRowHandle;
            using (frmMantePedidoCompraDetalle frm = new frmMantePedidoCompraDetalle())
            {
                frm.CargarControles();
                frm.obe = obe;
                frm.lstDetalle = lstDetalle;
                frm.SetModify();
                frm.lkpmoneda.Enabled = false;
                frm.txtcantidad.Focus();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();

                    viewDetalle.FocusedRowHandle = index;
                }
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void viewDetalle_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string lpedid_nCant_pedido = View.GetRowCellDisplayText(e.RowHandle, View.Columns["lpedid_nCant_pedido"]);

                if (Convert.ToDecimal(lpedid_nCant_pedido) != 0)
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                }
            }
        }

        private void viewDetalle_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            TotalGrilla();
        }
        private void TotalGrilla()
        {
            txtTotalPedido.Text = lstDetalle.Sum(ot => ot.lpedid_nCant_pedido).ToString();
            foreach (var _be in lstDetalle)
            {
                _be.lpedid_nCosto_pedido = _be.lpedid_nCant_pedido * _be.lpedid_nprecio_lista;
            }

        }

        private void viewDetalle_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TotalGrilla();
        }
    }
}