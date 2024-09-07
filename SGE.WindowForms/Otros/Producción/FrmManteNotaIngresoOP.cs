using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static SGE.Common.Enums;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class FrmManteNotaIngresoOP : XtraForm
    {
        public ENotaIngresoOP objCab = new ENotaIngresoOP();
        public List<ENotaIngresoOPDetalle> listDetalle = new List<ENotaIngresoOPDetalle>();
        public List<ENotaIngresoOPDetalle> listEliminar = new List<ENotaIngresoOPDetalle>();
        public delegate void DelegadoMensaje(int icod);
        public event DelegadoMensaje MiEvento;
        public FrmManteNotaIngresoOP() => InitializeComponent();
        private void FrmManteNotaIngresoOP_Load(object sender, EventArgs e) => Cargar();
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => Nuevo();
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => Modificar();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => Eliminar();
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e) => Dispose();
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e) => SetSave();

        private void Cargar()
        {
            if (objCab.niop_icod_nota_ingreso == 0)
            {
                dteFecha.DateTime = DateTime.Now;
                btnAlmacen.Tag = 5;
                btnAlmacen.Text = string.Format("{0:00}", 1);
                txtAlmcén.Text = "ALMACEN CENTRAL";
            }
            else
            {
                SetValues();
            }



        }
        private void SetValues()
        {
            btnAlmacen.Tag = objCab.niop_ialmacen;
            btnAlmacen.Text = objCab.NumeroAlmacen;
            txtAlmcén.Text = objCab.NombreAlmacen;
            txtNumero.Text = objCab.niop_inumero.ToString("d4");
            dteFecha.DateTime = objCab.niop_sfecha;
            txtReferencia.Text = objCab.niop_vreferencia;
            txtObservacion.Text = objCab.niop_vobservacion;
            btnPedido.Tag = objCab.orpec_iid_orden_pedido;
            btnPedido.Text = objCab.NumeroPedido;
            listDetalle = new BCentral().NotaIngresoOpDetalleListar(objCab.niop_icod_nota_ingreso);
            grdItem.DataSource = listDetalle;
            grdItem.RefreshDataSource();
        }
        private void Nuevo()
        {
            if (!ValidarSelecNotaIngreso()) return;
            var frm = new frmManteNotaIngresoOPItem();
            frm.Text = "Nuevo Item";
            frm.txtItem.Text = (listDetalle.Any() ? (listDetalle.Max(x => x.niopd_iitem_nota_ingreso_detalle) + 1) : 1).ToString("d2");
            frm.listDetalle = listDetalle;
            frm.objDetalle.Operacion = Operacion.Nuevo;
            frm.objCab.orpec_iid_orden_pedido = Convert.ToInt32(btnPedido.Tag);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                listDetalle.Add(frm.objDetalle);
                grdItem.DataSource = listDetalle;
                grdItem.RefreshDataSource();

            }
        }

        private bool ValidarSelecNotaIngreso()
        {
            if (Convert.ToInt32(btnPedido.Tag) == 0)
            {
                Services.SetErrorInput(btnPedido, "Seleccione el Orden de Pedido");
                return false;
            }
            return true;
        }

        private void Modificar()
        {
            var objDetalle = viewItem.GetFocusedRow() as ENotaIngresoOPDetalle;
            if (objDetalle is null) return;
            var frm = new frmManteNotaIngresoOPItem();
            frm.Text = $"Modificando Item : {objDetalle.niopd_iitem_nota_ingreso_detalle}";
            frm.listDetalle = listDetalle;
            frm.objDetalle = objDetalle;
            frm.objDetalle.Operacion = Operacion.Ver;
            frm.objCab.orpec_iid_orden_pedido = Convert.ToInt32(btnPedido.Tag);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                listDetalle = frm.listDetalle;
                grdItem.DataSource = listDetalle;
                grdItem.RefreshDataSource();
            }
        }

        private void Eliminar()
        {
            var objDetalle = viewItem.GetFocusedRow() as ENotaIngresoOPDetalle;
            if (objDetalle is null) return;
            if (Services.MessageQuestion("Está seguro de eliminar el Item ?") != DialogResult.Yes) return;
            listEliminar.Add(objDetalle);
            listDetalle.Remove(objDetalle);
            viewItem.RefreshData();
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            bool Flag = true;
            try
            {
                if (Convert.ToInt32(btnPedido.Tag) == 0)
                {
                    oBase = btnPedido;
                    throw new ArgumentException("Seleccione la Orden de Pedido");
                }
                objCab.niop_ialmacen = Convert.ToInt32(btnAlmacen.Tag);
                objCab.niop_inumero = Convert.ToInt32(txtNumero.Text);
                objCab.niop_sfecha = dteFecha.DateTime;
                objCab.niop_vobservacion = txtObservacion.Text;
                objCab.niop_vreferencia = txtReferencia.Text;
                objCab.orpec_iid_orden_pedido = Convert.ToInt32(btnPedido.Tag);
                objCab.intUsuario = Valores.intUsuario;
                objCab.NombreAlmacen = txtAlmcén.Text;
                if (objCab.niop_icod_nota_ingreso == 0)
                    objCab.niop_icod_nota_ingreso = new BCentral().NotaIngresoOpInsertar(objCab, listDetalle);
                else
                    new BCentral().NotaIngresoOpModificar(objCab, listDetalle, listEliminar);
            }
            catch (Exception ex)
            {
                Services.SetErrorInput(oBase, ex.Message);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    MiEvento(objCab.niop_icod_nota_ingreso);
                    Close();
                }
            }
        }

        private void btnAlmacen_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            using (FrmListarAlmacen frm = new FrmListarAlmacen())
            {
                frm.puvec_icod_punto_venta = 2;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnAlmacen.Tag = frm._Be.id_almacen;
                    btnAlmacen.Text = frm._Be.idf_almacen;
                    txtAlmcén.Text = frm._Be.descripcion;

                }
            }
        }

        private void btnPedido_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            using (ListarOrdenPedido frm = new ListarOrdenPedido())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnPedido.Tag = frm._Be.orpec_iid_orden_pedido;
                    btnPedido.Text = frm._Be.orpec_icod_orden_pedido;
                }
            }
        }


    }
}