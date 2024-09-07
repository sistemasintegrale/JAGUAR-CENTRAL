using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class FrmManteOrdenPedido : DevExpress.XtraEditors.XtraForm
    {
        public EOrdenPedidoCab oBe = new EOrdenPedidoCab();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public List<EOrdenPedidoCab> oDetail;
        List<EOrdenPedidoDet> lstOrdenPedidoDet = new List<EOrdenPedidoDet>();
        List<EOrdenPedidoDet> lstDelete = new List<EOrdenPedidoDet>();
        public int correlativoFT = 0;
        string strCodCliente = "";
        public int addCorrelativoFT = 1;
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
        private void bteCliente_ButtonClick_2(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) => listarCliente();
        private void eliminarToolStripMenuItem1_Click_1(object sender, EventArgs e) => eliminar();
        public FrmManteOrdenPedido() => InitializeComponent();
        public void SetModify() => Status = BSMaintenanceStatus.ModifyCurrent;
        public void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtNroOrdenPedido.Enabled = Enabled;
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
                txtNroOrdenPedido.Enabled = !Enabled;
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
            txtNroOrdenPedido.Text = oBe.orpec_icod_orden_pedido;
            dteFechaPedido.EditValue = oBe.orpec_sfecha_pedido;
            dteFechaEInicial.EditValue = oBe.orpec_sfecha_entrega_inicial;
            dteFechaEFinal.EditValue = oBe.orpec_sfecha_entrega_final;
            bteCliente.Tag = oBe.orpec_icod_cliente;
            bteCliente.Text = oBe.cliec_vnombre_cliente;
            txtNroOC.Text = oBe.orpec_vnumero_OC;
            txtFormaPago.Text = oBe.orpec_vforma_pago;
            btnTransportista.Tag = oBe.tranc_icod_transportista;
            btnTransportista.Text = oBe.tranc_vnombre_transportista;
            txtTotalUnidades.Text = oBe.orpec_itotal_unidades.ToString();
            txtTotalDocenas.Text = oBe.orpec_ntotal_docenas.ToString();
            lkpSituacion.EditValue = oBe.orpec_isituacion;
            lstOrdenPedidoDet = new BCentral().ListarOrdenPedidoDet(oBe.orpec_iid_orden_pedido);
            viewOrdenPedido.RefreshData();
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            getNroDoc();
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


        private void getNroDoc()
        {
            var lst = new BAdministracionSistema().listarParametroProduccion();

            //txtPedido.Text = (Convert.ToInt32(lst[0].pmprc_inumero_orden_pedido) + 1).ToString();
            txtNroOrdenPedido.Text = (Convert.ToInt32(lst[0].pmprc_inumero_orden_pedido) + 1).ToString();
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

            using (FrmOrdenPedidoDetalle frmdetalle = new FrmOrdenPedidoDetalle())
            {

                frmdetalle.txtitem.Text = (lstOrdenPedidoDet.Count + 1).ToString();
                frmdetalle.btnModificar.Enabled = false;
                frmdetalle.SetInsert();
                frmdetalle.btnAgregar.Enabled = true;
                frmdetalle.marcaRef = Convert.ToInt32(LkpMarca.EditValue);
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    EOrdenPedidoDet _obe = new EOrdenPedidoDet();
                    _obe.orped_iitem_orden_pedido = frmdetalle._BE.orped_iitem_orden_pedido;
                    _obe.orped_iresponsable = frmdetalle._BE.orped_iresponsable;
                    _obe.NomVendedor = frmdetalle._BE.NomVendedor;
                    _obe.orped_ificha_tecnica = frmdetalle._BE.orped_ificha_tecnica;
                    _obe.strfichatecnica = frmdetalle._BE.strfichatecnica;
                    _obe.orped_imodelo = frmdetalle._BE.orped_imodelo;
                    _obe.strmodelo = frmdetalle._BE.strmodelo;
                    _obe.orped_icolor = frmdetalle._BE.orped_icolor;
                    _obe.pr_viid_color = frmdetalle._BE.pr_viid_color;
                    _obe.orped_imarca = frmdetalle._BE.orped_imarca;
                    _obe.pr_viid_marca = frmdetalle._BE.pr_viid_marca;
                    _obe.orped_itipo = frmdetalle._BE.orped_itipo;
                    _obe.pr_viid_tipo = frmdetalle._BE.pr_viid_tipo;
                    _obe.orped_serie = frmdetalle._BE.orped_serie;
                    _obe.resec_vdescripcion = frmdetalle._BE.resec_vdescripcion;
                    _obe.orped_itotal_item = frmdetalle._BE.orped_itotal_item;
                    _obe.orped_ndocenas = frmdetalle._BE.orped_ndocenas;
                    _obe.orped_talla1 = frmdetalle._BE.orped_talla1;
                    _obe.orped_talla2 = frmdetalle._BE.orped_talla2;
                    _obe.orped_talla3 = frmdetalle._BE.orped_talla3;
                    _obe.orped_talla4 = frmdetalle._BE.orped_talla4;
                    _obe.orped_talla5 = frmdetalle._BE.orped_talla5;
                    _obe.orped_talla6 = frmdetalle._BE.orped_talla6;
                    _obe.orped_talla7 = frmdetalle._BE.orped_talla7;
                    _obe.orped_talla8 = frmdetalle._BE.orped_talla8;
                    _obe.orped_talla9 = frmdetalle._BE.orped_talla9;
                    _obe.orped_talla10 = frmdetalle._BE.orped_talla10;
                    _obe.orped_cant_talla1 = frmdetalle._BE.orped_cant_talla1;
                    _obe.orped_cant_talla2 = frmdetalle._BE.orped_cant_talla2;
                    _obe.orped_cant_talla3 = frmdetalle._BE.orped_cant_talla3;
                    _obe.orped_cant_talla4 = frmdetalle._BE.orped_cant_talla4;
                    _obe.orped_cant_talla5 = frmdetalle._BE.orped_cant_talla5;
                    _obe.orped_cant_talla6 = frmdetalle._BE.orped_cant_talla6;
                    _obe.orped_cant_talla7 = frmdetalle._BE.orped_cant_talla7;
                    _obe.orped_cant_talla8 = frmdetalle._BE.orped_cant_talla8;
                    _obe.orped_cant_talla9 = frmdetalle._BE.orped_cant_talla9;
                    _obe.orped_cant_talla10 = frmdetalle._BE.orped_cant_talla10;
                    _obe.orped_vruta = frmdetalle._BE.orped_vruta;
                    _obe.intTipoOperacion = frmdetalle._BE.intTipoOperacion;
                    _obe.orped_isituacion = frmdetalle._BE.orped_isituacion;
                    _obe.strsituacion = frmdetalle._BE.strsituacion;
                    _obe.imagen2 = frmdetalle._BE.imagen2;
                    _obe.orped_vnum_ficha_tecnica = generarNumFT(_obe.orped_iitem_orden_pedido);
                    _obe.orped_dprecio_total = frmdetalle._BE.orped_dprecio_total;
                    _obe.orped_dprecio_cliente = frmdetalle._BE.orped_dprecio_cliente;
                    lstOrdenPedidoDet.Add(_obe);
                    setTotal();
                    grdOrdenPedido.DataSource = lstOrdenPedidoDet;
                    grdOrdenPedido.RefreshDataSource();


                }
            }


        }

        public string generarNumFT(int orped_iitem_orden_pedido)
        {
            string num = "000000";
            string correlativo = (correlativoFT + addCorrelativoFT).ToString();
            addCorrelativoFT++;
            return num.Substring(0, (num.Length - correlativo.Length)) + correlativo;
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



        private async Task setSaveAsync()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (txtNroOrdenPedido.Text == "")
                {
                    oBase = txtNroOrdenPedido;
                    throw new ArgumentException("Ingrese Nro. de Serie de Factura");
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

                oBe.orpec_icod_orden_pedido = txtNroOrdenPedido.Text;
                oBe.orpec_sfecha_pedido = Convert.ToDateTime(dteFechaPedido.Text);
                oBe.orpec_sfecha_entrega_inicial = Convert.ToDateTime(dteFechaEInicial.Text);
                oBe.orpec_sfecha_entrega_final = Convert.ToDateTime(dteFechaEFinal.Text);
                oBe.orpec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.orpec_vnumero_OC = txtNroOC.Text;
                oBe.orpec_vforma_pago = txtFormaPago.Text;
                oBe.tranc_icod_transportista = Convert.ToInt32(btnTransportista.Tag);
                oBe.tranc_vnombre_transportista = btnTransportista.Text;
                oBe.orpec_itotal_unidades = Convert.ToInt32(txtTotalUnidades.Text);
                oBe.orpec_ntotal_docenas = Convert.ToDecimal(txtTotalDocenas.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.orpec_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.strsituacion = lkpSituacion.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.orpec_iid_orden_pedido = await new BCentral().InsertarOrdenPedidoCab(oBe, lstOrdenPedidoDet);
                }
                else
                {
                    await new BCentral().modificarOrdenPedidoCab(oBe, lstOrdenPedidoDet, lstDelete);
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
                    MiEvento(oBe.orpec_iid_orden_pedido);
                    Close();
                }
            }
        }


        private void setTotal()
        {
            txtTotalUnidades.Text = lstOrdenPedidoDet.Sum(x => x.orped_itotal_item).ToString();
            txtTotalDocenas.Text = lstOrdenPedidoDet.Sum(x => x.orped_ndocenas).ToString();
        }



        private void modificarItem()
        {
            EOrdenPedidoDet obe = (EOrdenPedidoDet)viewOrdenPedido.GetRow(viewOrdenPedido.FocusedRowHandle);
            if (obe == null)
                return;
            using (FrmOrdenPedidoDetalle frmdetalle = new FrmOrdenPedidoDetalle())
            {
                frmdetalle._BE = obe;
                frmdetalle.txtitem.Text = obe.orped_iitem_orden_pedido.ToString();
                frmdetalle.lkpResponsable.EditValue = obe.orped_iresponsable;
                frmdetalle.lkpResponsable.Text = obe.NomVendedor;
                frmdetalle.btnFichaTecnica.Tag = obe.orped_ificha_tecnica;
                frmdetalle.btnFichaTecnica.Text = obe.strfichatecnica;
                frmdetalle.btnmodelo.Tag = obe.orped_imodelo;
                frmdetalle.btnmodelo.Text = obe.strmodelo;
                frmdetalle.LkpColor.EditValue = obe.orped_icolor;
                frmdetalle.LkpMarca.EditValue = obe.orped_imarca;
                frmdetalle.lkpTipo.EditValue = obe.orped_itipo;
                frmdetalle.lkpSerie.EditValue = obe.orped_serie;
                frmdetalle.txtTotal.Text = obe.orped_itotal_item.ToString();
                frmdetalle.txtDocenas.Text = obe.orped_ndocenas.ToString();
                frmdetalle.txttalla1.Text = string.Format("{0:00}", obe.orped_talla1);
                frmdetalle.txttalla2.Text = string.Format("{0:00}", obe.orped_talla2);
                frmdetalle.txttalla3.Text = string.Format("{0:00}", obe.orped_talla3);
                frmdetalle.txttalla4.Text = string.Format("{0:00}", obe.orped_talla4);
                frmdetalle.txttalla5.Text = string.Format("{0:00}", obe.orped_talla5);
                frmdetalle.txttalla6.Text = string.Format("{0:00}", obe.orped_talla6);
                frmdetalle.txttalla7.Text = string.Format("{0:00}", obe.orped_talla7);
                frmdetalle.txttalla8.Text = string.Format("{0:00}", obe.orped_talla8);
                frmdetalle.txttalla9.Text = string.Format("{0:00}", obe.orped_talla9);
                frmdetalle.txttalla10.Text = string.Format("{0:00}", obe.orped_talla10);
                frmdetalle.txtcantidad1.Text = obe.orped_cant_talla1.ToString();
                frmdetalle.txtcantidad2.Text = obe.orped_cant_talla2.ToString();
                frmdetalle.txtcantidad3.Text = obe.orped_cant_talla3.ToString();
                frmdetalle.txtcantidad4.Text = obe.orped_cant_talla4.ToString();
                frmdetalle.txtcantidad5.Text = obe.orped_cant_talla5.ToString();
                frmdetalle.txtcantidad6.Text = obe.orped_cant_talla6.ToString();
                frmdetalle.txtcantidad7.Text = obe.orped_cant_talla7.ToString();
                frmdetalle.txtcantidad8.Text = obe.orped_cant_talla8.ToString();
                frmdetalle.txtcantidad9.Text = obe.orped_cant_talla9.ToString();
                frmdetalle.txtcantidad10.Text = obe.orped_cant_talla10.ToString();
                frmdetalle.txtPrecioCliente.Text = obe.orped_dprecio_cliente.ToString();
                frmdetalle.txtPrecioTotal.Text = obe.orped_dprecio_total.ToString();
                frmdetalle.txtHorma.Text = obe.orped_vhorma;
                frmdetalle.lkpSituacion.EditValue = obe.orped_isituacion;
                frmdetalle.lkpSituacion.Text = obe.strsituacion;
                frmdetalle.btnGenerar.Enabled = false;
                frmdetalle.btnAgregar.Enabled = false;
                frmdetalle.ckTercerizado.Checked = obe.orped_btercerizado;
                if (obe.orped_icod_item_orden_pedido != 0)
                {
                    try { obe.imagen2 = Image.FromStream(WebDavTest.Get($"{Rutas.RutaImagenes}{Rutas.RutaOrdenPedido}{obe.orpec_iid_orden_pedido}/", $"{obe.orped_icod_item_orden_pedido}.png")); }
                    catch { }
                }
                frmdetalle.ptrimagen.Image = obe.imagen2;
                frmdetalle.SetModify();
                frmdetalle.Redimencionarstock();
                frmdetalle.cargarcantidadesOriginales();
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    obe.orped_itotal_item = frmdetalle._BE.orped_itotal_item;
                    obe.orped_ndocenas = frmdetalle._BE.orped_ndocenas;
                    obe.orped_talla1 = frmdetalle._BE.orped_talla1;
                    obe.orped_talla2 = frmdetalle._BE.orped_talla2;
                    obe.orped_talla3 = frmdetalle._BE.orped_talla3;
                    obe.orped_talla4 = frmdetalle._BE.orped_talla4;
                    obe.orped_talla5 = frmdetalle._BE.orped_talla5;
                    obe.orped_talla6 = frmdetalle._BE.orped_talla6;
                    obe.orped_talla7 = frmdetalle._BE.orped_talla7;
                    obe.orped_talla8 = frmdetalle._BE.orped_talla8;
                    obe.orped_talla9 = frmdetalle._BE.orped_talla9;
                    obe.orped_talla10 = frmdetalle._BE.orped_talla10;
                    obe.orped_cant_talla1 = frmdetalle._BE.orped_cant_talla1;
                    obe.orped_cant_talla2 = frmdetalle._BE.orped_cant_talla2;
                    obe.orped_cant_talla3 = frmdetalle._BE.orped_cant_talla3;
                    obe.orped_cant_talla4 = frmdetalle._BE.orped_cant_talla4;
                    obe.orped_cant_talla5 = frmdetalle._BE.orped_cant_talla5;
                    obe.orped_cant_talla6 = frmdetalle._BE.orped_cant_talla6;
                    obe.orped_cant_talla7 = frmdetalle._BE.orped_cant_talla7;
                    obe.orped_cant_talla8 = frmdetalle._BE.orped_cant_talla8;
                    obe.orped_cant_talla9 = frmdetalle._BE.orped_cant_talla9;
                    obe.orped_cant_talla10 = frmdetalle._BE.orped_cant_talla10;
                    obe.orped_vruta = frmdetalle._BE.orped_vruta;
                    obe.intTipoOperacion = frmdetalle._BE.intTipoOperacion;
                    obe.orped_isituacion = frmdetalle._BE.orped_isituacion;
                    obe.strsituacion = frmdetalle._BE.strsituacion;
                    obe.orped_dprecio_cliente = frmdetalle._BE.orped_dprecio_cliente;
                    obe.imagen2 = frmdetalle._BE.imagen2;
                    setTotal();
                    grdOrdenPedido.RefreshDataSource();
                }
            }


        }

        private void btnGuardar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => setSaveAsync();

        private void FrmManteFactura_Load_1(object sender, EventArgs e)
        {
            cargar();
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(93), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            lkpSituacion.Enabled = false;
            cargarCorrelativoFT();
            BSControls.LoaderLook(LkpMarca, new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro() { iid_tipo_tabla = 11 }), "descripcion", "icod_tabla", true);
        }

        void cargarCorrelativoFT()
        {
            var param = new BAdministracionSistema().listarParametroProduccion().FirstOrDefault();

            correlativoFT = param.pmprc_inumero_ficha_tecnica;
        }

        private void modificarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            EOrdenPedidoDet obe = (EOrdenPedidoDet)viewOrdenPedido.GetRow(viewOrdenPedido.FocusedRowHandle);
            if (obe == null)
                return;

            modificarItem();
        }


        private void eliminar()
        {
            EOrdenPedidoDet obe = (EOrdenPedidoDet)viewOrdenPedido.GetRow(viewOrdenPedido.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.orped_isituacion_entrega != (int)SituacionEntregaOrdenPedidoItem.Pendiente)
            {
                Services.MessageError($"No se puede eliminar el item, su situación es {obe.SituacionEntrega}");
                return;
            }
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