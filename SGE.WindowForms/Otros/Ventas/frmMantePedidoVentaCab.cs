using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Operaciones;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Ventas
{
    public partial class frmMantePedidoVentaCab : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        /*--------------*/
        public EPedidoClienCab Obe = new EPedidoClienCab();
        public List<EPedidoClienDet> lstDetalle = new List<EPedidoClienDet>();
        public List<EPedidoClienDet> lstDelete = new List<EPedidoClienDet>();
        /*--------------*/

        public frmMantePedidoVentaCab()
        {
            InitializeComponent();
        }


        public void CargarCombos()
        {

            int index = new BGeneral().listarTablaRegistro(Parametros.intTipoPedido).FindIndex(x => x.tarec_iid_tabla_registro == 269);
            BSControls.LoaderLook(lkpTipoPedido, new BGeneral().listarTablaRegistro(Parametros.intTipoPedido).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            lkpTipoPedido.ItemIndex = index;



        }
        private void frmManteFacturaCompra_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(55), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            cargar();
            TotalizarCanti();
            TotalizarCanti();
            CargarCombos();

        }
        private void TotalizarCanti()
        {
            int cantidadTotal = 0;
            decimal PrecioTotal = 0;
            foreach (var _beee in lstDetalle)
            {
                cantidadTotal = cantidadTotal + _beee.lpedid_nCant_pedido;
                PrecioTotal = Convert.ToDecimal(PrecioTotal + (Convert.ToDecimal(_beee.lpedid_nCant_pedido) * _beee.lpedid_nprecio_uni));
            }
            txtCantidadTotal.Text = cantidadTotal.ToString();
            txtprecioTotal.Text = PrecioTotal.ToString();

        }
        public void setGuardar()
        {
            SetSave();
        }
        private void cargar()
        {
            lstDetalle = new BVentas().listarPedidoVentaDet(Convert.ToInt32(Obe.lpedi_icod_cliente), Parametros.intEjercicio);
            grdDetalle.DataSource = lstDetalle;
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
            btePcliente.Enabled = Enabled;

        }

        public void setValues()
        {
            txtNºPedido.Text = Obe.lpedi_Numerolista;
            btePcliente.Tag = Obe.cliec_icod_cliente;
            btePcliente.Text = Obe.cliec_vnombre_cliente;
            dtFecha.EditValue = Obe.lpedi_sfecha_cliente;
            txtObservacion.Text = Obe.lpedi_Observaciones;
            btnVendedor.Tag = Obe.perc_icod_personal;
            btnVendedor.Text = Obe.perc_vapellidos_nombres;
            lkpSituacion.EditValue = Obe.StrSituacion;
            lkpTipoPedido.EditValue = Obe.tablc_iid_tipo_ped;

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
                if (btePcliente.Tag == null)
                {
                    oBase = btePcliente;
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

                Obe.cliec_icod_cliente = Convert.ToInt32(btePcliente.Tag);
                Obe.lpedi_Numerolista = txtNºPedido.Text;
                Obe.perc_icod_personal = Convert.ToInt32(btnVendedor.Tag);
                Obe.lpedi_sfecha_cliente = Convert.ToDateTime(dtFecha.EditValue);
                Obe.lpedi_Observaciones = txtObservacion.Text;
                Obe.lpedi_sflag_estado = true;
                Obe.StrTipoPedido = lkpTipoPedido.Text;
                Obe.tablc_iid_tipo_ped = Convert.ToInt32(lkpTipoPedido.EditValue);

                /**/
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.lpedi_isituacion_prov = Convert.ToInt32(lkpSituacion.EditValue);
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.lpedi_icod_cliente = new BVentas().insertarPedidoVenta(Obe, lstDetalle);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarPedidoVenta(Obe, lstDetalle, lstDelete);
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
                    this.MiEvento(Obe.lpedi_icod_cliente);
                    this.Close();
                }
            }
        }

        private void listarCliente()
        {
            try
            {
                using (FrmListarCliente frm = new FrmListarCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btePcliente.Tag = frm._Be.cliec_icod_cliente;
                        btePcliente.Text = frm._Be.cliec_vnombre_cliente;

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listarPersonal()
        {
            try
            {
                using (frmListarPersonal frm = new frmListarPersonal())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnVendedor.Tag = frm._Be.perc_icod_personal;
                        btnVendedor.Text = frm._Be.perc_vapellidos_nombres;

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
            txtObservacion.Focus();
        }

        private void bteProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarCliente();
        }



        private void nuevo()
        {
            BaseEdit oBase = null;
            try
            {

                using (frmMantePedidoVentaDet frm = new frmMantePedidoVentaDet())
                {
                    frm.CargarControles();
                    frm.SetInsert();
                    frm.lstDetalle = lstDetalle;
                    frm.lkpmoneda.EditValue = 3;//dolares
                    if (lstDetalle.Count == 0)
                    {
                        frm.txtItem.Text = "01";
                    }
                    else
                        frm.txtItem.Text = (lstDetalle.Max(on => on.lpedid_iitem) + 1).ToString();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewDetalle.RefreshData();
                        viewDetalle.MoveLast();
                        TotalizarCanti();
                    }
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
            }
        }

        private void modificar()
        {
            EPedidoClienDet obe = (EPedidoClienDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;

            int index = viewDetalle.FocusedRowHandle;
            using (frmMantePedidoVentaDet frm = new frmMantePedidoVentaDet())
            {
                frm.CargarControles();
                frm.obe = obe;
                frm.lstDetalle = lstDetalle;
                frm.SetModify();
                frm.lkpmoneda.Enabled = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();

                    viewDetalle.FocusedRowHandle = index;
                    TotalizarCanti();
                }
            }
        }

        private void eliminar()
        {
            EPedidoClienDet obe = (EPedidoClienDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            viewDetalle.RefreshData();
            TotalizarCanti();
        }

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

        private void btnVendedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarPersonal();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void lkpTipoPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}