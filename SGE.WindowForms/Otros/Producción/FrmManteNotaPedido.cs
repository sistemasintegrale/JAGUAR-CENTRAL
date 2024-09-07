using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class FrmManteNotaPedido : DevExpress.XtraEditors.XtraForm
    {
        public ENotaPedidoCab oBe = new ENotaPedidoCab();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public List<ENotaPedidoCab> oDetail;
        List<ENotaPedidoDet> lstOrdenPedidoDet = new List<ENotaPedidoDet>();
        List<ENotaPedidoDet> lstDelete = new List<ENotaPedidoDet>();

        string strCodCliente = "";
        //int Numero_dias_vencimiento_cliente = 0;
        //public int TipodeFactura=0;
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
        public void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtNroNotaPedido.Enabled = Enabled;
                dteFechaPedido.Enabled = !Enabled;
                dteFechaEInicial.Enabled = !Enabled;
                dteFechaEFinal.Enabled = !Enabled;
                bteCliente.Enabled = !Enabled;
                txtNroOC.Enabled = !Enabled;
                txtFormaPago.Enabled = !Enabled;
                btnTransportista.Enabled = !Enabled;

            }
            else
            {
                txtNroNotaPedido.Enabled = !Enabled;
                dteFechaPedido.Enabled = !Enabled;
                dteFechaEInicial.Enabled = !Enabled;
                dteFechaEFinal.Enabled = !Enabled;
                bteCliente.Enabled = !Enabled;
                txtNroOC.Enabled = !Enabled;
                txtFormaPago.Enabled = !Enabled;
                btnTransportista.Enabled = !Enabled;
                txtTotalUnidades.Enabled = !Enabled;
                txtTotalDocenas.Enabled = !Enabled;
            }
        }

        public void setValues()
        {
            txtNroNotaPedido.Text = oBe.nopec_icod_nota_pedido;
            dteFechaPedido.EditValue = oBe.nopec_sfecha_pedido;
            dteFechaEInicial.EditValue = oBe.nopec_sfecha_entrega_inicial;
            dteFechaEFinal.EditValue = oBe.nopec_sfecha_entrega_final;
            bteCliente.Tag = oBe.nopec_icod_cliente;
            bteCliente.Text = oBe.cliec_vnombre_cliente;
            txtNroOC.Text = oBe.nopec_vnumero_OC;
            txtFormaPago.Text = oBe.nopec_vforma_pago;
            btnTransportista.Tag = oBe.tranc_icod_transportista;
            btnTransportista.Text = oBe.tranc_vnombre_transportista;
            txtTotalUnidades.Text = oBe.nopec_itotal_unidades.ToString();
            txtTotalDocenas.Text = oBe.nopec_ntotal_docenas.ToString();
            lkpSituacion.EditValue = oBe.nopec_isituacion;
            lstOrdenPedidoDet = new BCentral().ListarNotaPedidoDet(oBe.nopec_iid_nota_pedido);
            lstOrdenPedidoDet.ForEach(x => { x.strfichatecnica_Contramuestra = x.strfichatecnica + "-" + x.fitec_icorrelativo_contramuestra; });

            viewOrdenPedido.RefreshData();
        }

        public FrmManteNotaPedido()
        {
            InitializeComponent();
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            getNroDoc();
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;

        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            SetCancel();
        }

        private void cargar()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                setFecha(dteFechaPedido);
                setFecha(dteFechaEInicial);
                setFecha(dteFechaEFinal);
                txtTotalUnidades.Enabled = false;
                txtTotalDocenas.Enabled = false;
            }

            grdOrdenPedido.DataSource = lstOrdenPedidoDet;


        }
        public void CargarControles()
        {

        }

        public void getNroDoc()
        {
            try
            {
                var lst = new BVentas().getCorrelativoOP(1);

                txtNroNotaPedido.Text = (Convert.ToInt32(lst[0].pmprc_inumero_nota_pedido) + 1).ToString();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                FrmNotaPedidoDetalle frmdetalle = new FrmNotaPedidoDetalle();
                //frmdetalle.icod_motivo = Convert.ToInt32(lkpmotivo.EditValue);
                frmdetalle.txtitem.Text = (lstOrdenPedidoDet.Count + 1).ToString();
                frmdetalle.btnModificar.Enabled = false;
                frmdetalle.SetInsert();
                frmdetalle.btnAgregar.Enabled = true;
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    ENotaPedidoDet _obe = new ENotaPedidoDet();
                    _obe.noped_iitem_nota_pedido = frmdetalle._BE.noped_iitem_nota_pedido;
                    _obe.noped_iresponsable = frmdetalle._BE.noped_iresponsable;
                    _obe.NomVendedor = frmdetalle._BE.NomVendedor;
                    _obe.noped_ificha_tecnica = frmdetalle._BE.noped_ificha_tecnica;
                    _obe.strfichatecnica = frmdetalle._BE.strfichatecnica;
                    _obe.noped_imodelo = frmdetalle._BE.noped_imodelo;
                    _obe.strmodelo = frmdetalle._BE.strmodelo;
                    _obe.noped_icolor = frmdetalle._BE.noped_icolor;
                    _obe.pr_viid_color = frmdetalle._BE.pr_viid_color;
                    _obe.noped_imarca = frmdetalle._BE.noped_imarca;
                    _obe.pr_viid_marca = frmdetalle._BE.pr_viid_marca;
                    _obe.noped_itipo = frmdetalle._BE.noped_itipo;
                    _obe.pr_viid_tipo = frmdetalle._BE.pr_viid_tipo;
                    _obe.noped_serie = frmdetalle._BE.noped_serie;
                    _obe.resec_vdescripcion = frmdetalle._BE.resec_vdescripcion;
                    _obe.noped_itotal_item = frmdetalle._BE.noped_itotal_item;
                    _obe.noped_ndocenas = frmdetalle._BE.noped_ndocenas;
                    _obe.noped_talla1 = frmdetalle._BE.noped_talla1;
                    _obe.noped_talla2 = frmdetalle._BE.noped_talla2;
                    _obe.noped_talla3 = frmdetalle._BE.noped_talla3;
                    _obe.noped_talla4 = frmdetalle._BE.noped_talla4;
                    _obe.noped_talla5 = frmdetalle._BE.noped_talla5;
                    _obe.noped_talla6 = frmdetalle._BE.noped_talla6;
                    _obe.noped_talla7 = frmdetalle._BE.noped_talla7;
                    _obe.noped_talla8 = frmdetalle._BE.noped_talla8;
                    _obe.noped_talla9 = frmdetalle._BE.noped_talla9;
                    _obe.noped_talla10 = frmdetalle._BE.noped_talla10;
                    _obe.noped_cant_talla1 = frmdetalle._BE.noped_cant_talla1;
                    _obe.noped_cant_talla2 = frmdetalle._BE.noped_cant_talla2;
                    _obe.noped_cant_talla3 = frmdetalle._BE.noped_cant_talla3;
                    _obe.noped_cant_talla4 = frmdetalle._BE.noped_cant_talla4;
                    _obe.noped_cant_talla5 = frmdetalle._BE.noped_cant_talla5;
                    _obe.noped_cant_talla6 = frmdetalle._BE.noped_cant_talla6;
                    _obe.noped_cant_talla7 = frmdetalle._BE.noped_cant_talla7;
                    _obe.noped_cant_talla8 = frmdetalle._BE.noped_cant_talla8;
                    _obe.noped_cant_talla9 = frmdetalle._BE.noped_cant_talla9;
                    _obe.noped_cant_talla10 = frmdetalle._BE.noped_cant_talla10;
                    _obe.noped_vruta = frmdetalle._BE.noped_vruta;
                    _obe.intTipoOperacion = frmdetalle._BE.intTipoOperacion;
                    _obe.noped_isituacion = frmdetalle._BE.noped_isituacion;
                    _obe.strsituacion = frmdetalle._BE.strsituacion;
                    _obe.imagen2 = frmdetalle._BE.imagen2;
                    _obe.noped_nprecio_fabrica = frmdetalle._BE.noped_nprecio_fabrica;
                    _obe.noped_nprecio_cliente = frmdetalle._BE.noped_nprecio_cliente;
                    lstOrdenPedidoDet.Add(_obe);
                    setTotal();
                    grdOrdenPedido.DataSource = lstOrdenPedidoDet;
                    grdOrdenPedido.RefreshDataSource();

                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    //oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {

                if (Flag)
                {

                }
            }

        }


        private void nuevoServicio()
        {
            //using (frmManteFacturaServicioDetalle frm = new frmManteFacturaServicioDetalle())
            //{
            //    frm.SetInsert();
            //    frm.lstFacturaDetalle = lstFacturaDetalle;
            //    //frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
            //    frm.txtItem.Text = (lstFacturaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstFacturaDetalle.Count + 1);
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        lstFacturaDetalle = frm.lstFacturaDetalle;
            //        viewOrdenPedido.RefreshData();
            //        viewOrdenPedido.MoveLast();
            //        //setTotal();

            //    }
            //}
        }

        private void modificarServicio()
        {
            //EFacturaDet obe = (EFacturaDet)viewOrdenPedido.GetRow(viewOrdenPedido.FocusedRowHandle);
            //if (obe == null)
            //    return;
            //using (frmManteFacturaServicioDetalle frm = new frmManteFacturaServicioDetalle())
            //{
            //    frm.obe = obe;
            //    frm.lstFacturaDetalle = lstFacturaDetalle;
            //    frm.SetModify();
            //    //frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
            //    frm.txtItem.Text = String.Format("{0:000}", obe.favd_iitem_factura);
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        lstFacturaDetalle = frm.lstFacturaDetalle;
            //        viewOrdenPedido.RefreshData();
            //        viewOrdenPedido.MoveLast();
            //        //setTotal();
            //    }
            //}
        }

        private void listarCliente()
        {
            try
            {
                using (FrmListarCliente frm = new FrmListarCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteCliente.Tag = frm._Be.cliec_icod_cliente;
                        bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                        strCodCliente = frm._Be.cliec_vcod_cliente;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setCliente(int intCliente)
        {
            try
            {
                var _Be = new BVentas().ListarCliente().Where(x => x.cliec_icod_cliente == intCliente).ToList()[0];
                bteCliente.Tag = _Be.cliec_icod_cliente;
                bteCliente.Text = _Be.cliec_vnombre_cliente;
                strCodCliente = _Be.cliec_vcod_cliente;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private async Task setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (txtNroNotaPedido.Text == "")
                {
                    oBase = txtNroNotaPedido;
                    throw new ArgumentException("Ingrese Nro. de la Nota de Pedido");
                }

                if (Convert.ToDateTime(dteFechaPedido.Text).Year != Parametros.intEjercicio)
                {
                    oBase = dteFechaPedido;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del ejercicio");
                }

                if (Convert.ToInt32(bteCliente.Tag) == 0)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccione cliente");
                }

                oBe.nopec_icod_nota_pedido = txtNroNotaPedido.Text;
                oBe.nopec_sfecha_pedido = Convert.ToDateTime(dteFechaPedido.Text);
                oBe.nopec_sfecha_entrega_inicial = Convert.ToDateTime(dteFechaEInicial.Text);
                oBe.nopec_sfecha_entrega_final = Convert.ToDateTime(dteFechaEFinal.Text);
                oBe.nopec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.nopec_vnumero_OC = txtNroOC.Text;
                oBe.nopec_vforma_pago = txtFormaPago.Text;
                oBe.tranc_icod_transportista = Convert.ToInt32(btnTransportista.Tag);
                oBe.tranc_vnombre_transportista = btnTransportista.Text;
                oBe.nopec_itotal_unidades = Convert.ToInt32(txtTotalUnidades.Text);
                oBe.nopec_ntotal_docenas = Convert.ToDecimal(txtTotalDocenas.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.nopec_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.strsituacion = lkpSituacion.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.nopec_iid_nota_pedido = await new BCentral().InsertarNotaPedidoCab(oBe, lstOrdenPedidoDet);
                }
                else
                {
                    await new BCentral().modificarNotaPedidoCab(oBe, lstOrdenPedidoDet, lstDelete);
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
                    MiEvento(oBe.nopec_iid_nota_pedido);
                    Close();
                }
            }
        }

        private void FrmManteFactura_Load(object sender, EventArgs e)
        {
            //cargar();
            //txtNombreAnticipo.Enabled = true;
            //txtAnticipo.Enabled = true;



        }

        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void setTotal()
        {
            txtTotalUnidades.Text = lstOrdenPedidoDet.Sum(x => x.noped_itotal_item).ToString();
            txtTotalDocenas.Text = lstOrdenPedidoDet.Sum(x => x.noped_ndocenas).ToString();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void modificarItem()
        {
            ENotaPedidoDet obe = (ENotaPedidoDet)viewOrdenPedido.GetRow(viewOrdenPedido.FocusedRowHandle);
            if (obe == null)
                return;
            using (FrmNotaPedidoDetalle frmdetalle = new FrmNotaPedidoDetalle())
            {
                frmdetalle._BE = obe;
                frmdetalle.txtitem.Text = obe.noped_iitem_nota_pedido.ToString();
                frmdetalle.lkpResponsable.EditValue = obe.noped_iresponsable;
                frmdetalle.lkpResponsable.Text = obe.NomVendedor;


                frmdetalle.btnmodelo.Tag = obe.noped_imodelo;
                frmdetalle.btnmodelo.Text = obe.strmodelo;
                frmdetalle.LkpColor.EditValue = obe.noped_icolor;
                frmdetalle.LkpMarca.EditValue = obe.noped_imarca;
                frmdetalle.lkpTipo.EditValue = obe.noped_itipo;
                frmdetalle.lkpSerie.EditValue = obe.noped_serie;
                frmdetalle.txtTotal.Text = obe.noped_itotal_item.ToString();
                frmdetalle.txtDocenas.Text = obe.noped_ndocenas.ToString();
                frmdetalle.txttalla1.Text = string.Format("{0:00}", obe.noped_talla1);
                frmdetalle.txttalla2.Text = string.Format("{0:00}", obe.noped_talla2);
                frmdetalle.txttalla3.Text = string.Format("{0:00}", obe.noped_talla3);
                frmdetalle.txttalla4.Text = string.Format("{0:00}", obe.noped_talla4);
                frmdetalle.txttalla5.Text = string.Format("{0:00}", obe.noped_talla5);
                frmdetalle.txttalla6.Text = string.Format("{0:00}", obe.noped_talla6);
                frmdetalle.txttalla7.Text = string.Format("{0:00}", obe.noped_talla7);
                frmdetalle.txttalla8.Text = string.Format("{0:00}", obe.noped_talla8);
                frmdetalle.txttalla9.Text = string.Format("{0:00}", obe.noped_talla9);
                frmdetalle.txttalla10.Text = string.Format("{0:00}", obe.noped_talla10);
                frmdetalle.txtcantidad1.Text = obe.noped_cant_talla1.ToString();
                frmdetalle.txtcantidad2.Text = obe.noped_cant_talla2.ToString();
                frmdetalle.txtcantidad3.Text = obe.noped_cant_talla3.ToString();
                frmdetalle.txtcantidad4.Text = obe.noped_cant_talla4.ToString();
                frmdetalle.txtcantidad5.Text = obe.noped_cant_talla5.ToString();
                frmdetalle.txtcantidad6.Text = obe.noped_cant_talla6.ToString();
                frmdetalle.txtcantidad7.Text = obe.noped_cant_talla7.ToString();
                frmdetalle.txtcantidad8.Text = obe.noped_cant_talla8.ToString();
                frmdetalle.txtcantidad9.Text = obe.noped_cant_talla9.ToString();
                frmdetalle.txtcantidad10.Text = obe.noped_cant_talla10.ToString();
                frmdetalle.txtPrecioFabrica.Text = obe.noped_nprecio_fabrica.ToString();
                frmdetalle.txtPrecioCliente.Text = obe.noped_nprecio_cliente.ToString();
                frmdetalle.txtCodigoCliente.Text = obe.noped_vcodigo_cliente;
                frmdetalle.txtColorCliente.Text = obe.noped_vcolor_cliente;
                frmdetalle.lkpSituacion.EditValue = obe.noped_isituacion;
                frmdetalle.lkpSituacion.Text = obe.strsituacion;
                frmdetalle.btnGenerar.Enabled = false;
                frmdetalle.btnAgregar.Enabled = false;
                frmdetalle.SetModify();
                frmdetalle.Redimencionarstock();
                frmdetalle.cargarcantidadesOriginales();
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    obe.noped_itotal_item = frmdetalle._BE.noped_itotal_item;
                    obe.noped_ndocenas = frmdetalle._BE.noped_ndocenas;
                    obe.noped_talla1 = frmdetalle._BE.noped_talla1;
                    obe.noped_talla2 = frmdetalle._BE.noped_talla2;
                    obe.noped_talla3 = frmdetalle._BE.noped_talla3;
                    obe.noped_talla4 = frmdetalle._BE.noped_talla4;
                    obe.noped_talla5 = frmdetalle._BE.noped_talla5;
                    obe.noped_talla6 = frmdetalle._BE.noped_talla6;
                    obe.noped_talla7 = frmdetalle._BE.noped_talla7;
                    obe.noped_talla8 = frmdetalle._BE.noped_talla8;
                    obe.noped_talla9 = frmdetalle._BE.noped_talla9;
                    obe.noped_talla10 = frmdetalle._BE.noped_talla10;
                    obe.noped_cant_talla1 = frmdetalle._BE.noped_cant_talla1;
                    obe.noped_cant_talla2 = frmdetalle._BE.noped_cant_talla2;
                    obe.noped_cant_talla3 = frmdetalle._BE.noped_cant_talla3;
                    obe.noped_cant_talla4 = frmdetalle._BE.noped_cant_talla4;
                    obe.noped_cant_talla5 = frmdetalle._BE.noped_cant_talla5;
                    obe.noped_cant_talla6 = frmdetalle._BE.noped_cant_talla6;
                    obe.noped_cant_talla7 = frmdetalle._BE.noped_cant_talla7;
                    obe.noped_cant_talla8 = frmdetalle._BE.noped_cant_talla8;
                    obe.noped_cant_talla9 = frmdetalle._BE.noped_cant_talla9;
                    obe.noped_cant_talla10 = frmdetalle._BE.noped_cant_talla10;
                    obe.noped_vruta = frmdetalle._BE.noped_vruta;
                    obe.intTipoOperacion = frmdetalle._BE.intTipoOperacion;
                    obe.noped_isituacion = frmdetalle._BE.noped_isituacion;
                    obe.strsituacion = frmdetalle._BE.strsituacion;
                    obe.noped_nprecio_fabrica = frmdetalle._BE.noped_nprecio_fabrica;
                    obe.noped_nprecio_cliente = frmdetalle._BE.noped_nprecio_cliente;
                    obe.imagen2 = frmdetalle._BE.imagen2;
                    setTotal();
                    grdOrdenPedido.RefreshDataSource();
                }

            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ENotaPedidoDet obe = (ENotaPedidoDet)viewOrdenPedido.GetRow(viewOrdenPedido.FocusedRowHandle);
            if (obe == null)
                return;
            //lstDelete.Add(obe);
            lstOrdenPedidoDet.Remove(obe);
            viewOrdenPedido.RefreshData();
            //setTotal();
        }



        private void nuevoServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoServicio();
        }

        private void cbIncluyeIGV_CheckedChanged(object sender, EventArgs e)
        {
            //setTotal();
        }
        //private void setTotal()
        //{
        //    if (lstFacturaDetalle.Count > 0)
        //    {
        //        if (cbIncluyeIGV.Checked)
        //        {
        //            decimal total = lstFacturaDetalle.Sum(x => x.favd_nprecio_total_item);
        //            decimal igv = Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", ""), 2));
        //            decimal neto = Math.Round(total / Convert.ToDecimal("1." + txtPorcentIGV.Text.Replace(".", "")), 2);
        //            txtMontoNeto.Text = Convertir.RedondearNumero(neto).ToString();
        //            txtMontoTotal.Text = Convertir.RedondearNumero(total).ToString();
        //            txtMontoIGV.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtMontoNeto.Text)).ToString();
        //        }
        //        else
        //        {
        //            decimal neto = lstFacturaDetalle.Sum(x => x.favd_nprecio_total_item);
        //            decimal igv = Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", ""), 2));
        //            decimal total = Math.Round(neto * Convert.ToDecimal("1." + txtPorcentIGV.Text.Replace(".", "")), 2);
        //            txtMontoNeto.Text = Convertir.RedondearNumero(neto).ToString();
        //            txtMontoTotal.Text = Convertir.RedondearNumero(total).ToString();
        //            txtMontoIGV.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtMontoNeto.Text)).ToString();
        //        }
        //    }
        //}

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void grdFactura_MouseMove(object sender, MouseEventArgs e)
        {
            //this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }

        private void dteFecha_EditValueChanged(object sender, EventArgs e)
        {


        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }


        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ENotaPedidoDet obe = (ENotaPedidoDet)viewOrdenPedido.GetRow(viewOrdenPedido.FocusedRowHandle);
            if (obe == null)
                return;
            //lstDelete.Add(obe);
            lstOrdenPedidoDet.Remove(obe);
            viewOrdenPedido.RefreshData();
            //setTotal();

        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {

                getNroDoc();
            }
        }

        private void bteCCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCentroCosto();
        }
        private void ListarCentroCosto()
        {
            //using (frmListarCentroCostoProyectos Ccosto = new frmListarCentroCostoProyectos())
            //{

            //    if (Ccosto.ShowDialog() == DialogResult.OK)
            //    {
            //        bteCCosto.Text = Ccosto._Be.cecoc_vcodigo_centro_costo;//cecoc_ccodigo_centro_costo - centro_costo
            //        bteCCosto.Tag = Ccosto._Be.cecoc_icod_centro_costo;//cecoc_icod_centro_costo (correlativo) - centro_costo

            //    }
            //}
        }

        private void bteProyecto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProyecto();
        }
        public void ListarProyecto()
        {
            //using (frmListarProyectos proyecto = new frmListarProyectos())
            //{
            //    if (proyecto.ShowDialog() == DialogResult.OK)
            //    {
            //        bteProyecto.Text = proyecto._Be.pryc_vcorrelativo;
            //        bteProyecto.Tag = proyecto._Be.pryc_icod_proyecto;

            //    }

            //}
        }

        private void bteGarantiaProveedores_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarGarantiaProveedores();
        }
        public void listarGarantiaProveedores()
        {
            //using (frmListarGarantiaProveedores frm = new frmListarGarantiaProveedores())
            //{
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        bteGarantiaProveedores.Tag = frm._Be.garp_icod_garantia;
            //        bteGarantiaProveedores.Text = frm._Be.garap_vnumero_garantia;
            //        txtMontoGP.Text = frm._Be.garp_nmonto.ToString();
            //        txtMontoGarantia.Text = frm._Be.garp_nmonto.ToString();
            //    }
            //}

        }

        private void bteVendedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void bteVendedor_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void bteCliente_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void btncodigoalmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
        private void GenerarCodigo()
        {
            //int i = 0;
            //var lista = oDetail.Where(ob => ob.almac_icod_almacen == Convert.ToInt32(btncodigoalmacen.Tag)).ToList();
            //if (lista.Count > 0)
            //    i = lista.Max(ob => Convert.ToInt32(ob.favc_vnumero_factura));
            //txtnroNota.Text = String.Format("{0:000000}", i + 1).ToString();
        }

        private void btnGuardar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void FrmManteFactura_Load_1(object sender, EventArgs e)
        {
            cargar();
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(93), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            lkpSituacion.Enabled = false;
        }

        private void modificarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ENotaPedidoDet obe = (ENotaPedidoDet)viewOrdenPedido.GetRow(viewOrdenPedido.FocusedRowHandle);
            if (obe == null)
                return;

            modificarItem();
        }

        private void bteCliente_ButtonClick_2(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void eliminarToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            eliminar();
        }
        private void eliminar()
        {
            ENotaPedidoDet obe = (ENotaPedidoDet)viewOrdenPedido.GetRow(viewOrdenPedido.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstOrdenPedidoDet.Remove(obe);
            viewOrdenPedido.RefreshData();
            setTotal();
        }

        private void btnTransportista_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarTransportista frm = new frmListarTransportista())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnTransportista.Tag = frm._Be.tranc_icod_transportista;
                    btnTransportista.Text = frm._Be.tranc_vnombre_transportista;
                }
            }
        }
    }
}