using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using System.Linq;
using System.Security.Principal;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using DevExpress.XtraBars;
using DevExpress.XtraBars.ViewInfo;
using SGE.WindowForms.Tesorería.Bancos;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class FrmCajaEfectivo : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCajaEfectivo));

        private List<ELibroBancos> lstMovimientos = new List<ELibroBancos>();        
        public List<ELibroBancos> oDetail;        
        public int codeCuenta;
        public int codeMes;
        public double nt;
        public int IdSituacion = 0;        
        public int icod_movimiento = 0;

        public int CodeMes
        {
            get
            {
                return codeMes;
            }
            set
            {
                if (codeMes == value)
                    return;
                codeMes = value;
            }
        }

        #endregion

        #region Eventos

        public FrmCajaEfectivo()
        {
            InitializeComponent();
        }

        private void FrmMovLibroBancos_Load(object sender, EventArgs e)
        {
            
            BSControls.LoaderLook(lkpBanco, new BTesoreria().listarBancos(), "bcoc_vnombre_banco", "bcoc_icod_banco", true);
            var lstMeses = new BGeneral().listarTablaRegistro(4);
            BSControls.LoaderLook(lkpMes, lstMeses, "tarec_vdescripcion", "tarec_icorrelativo_registro", true);                      
            lkpMes.EditValue = DateTime.Now.Month;
            Carga();
            viewLibroBancos.Focus();
            lkpBanco.Enabled = false;
            lkpBanco.EditValue = Parametros.intBancoCajaEfectivo;
        }
        private void view()
        {
            if (lstMovimientos.Count > 0)
            {
                ELibroBancos Oble = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                if (Oble != null)
                {
                    //SaveLog(Oble.icod_correlativo, Parametros.intConsulta);                   
                    #region Motivo Varios
                    if (int.Parse(viewLibroBancos.GetFocusedRowCellValue("iid_motivo_mov_banco").ToString()) == Parametros.intMotivoVarios)
                    {
                        FrmManteMovimientoVarios frm = new FrmManteMovimientoVarios();
                        frm.MiEvento += new FrmManteMovimientoVarios.DelegadoMensaje(Modify);
                        if (Convert.ToInt32(lkpMes.EditValue) == 0)
                            frm.grdDetalle.Enabled = false;
                        frm.IdMotivoMovimientoBanco = Parametros.intMotivoVarios;
                        frm.Cab_icod_correlativo = Oble.icod_correlativo;
                        frm.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                        frm.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                        frm.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                        frm.txtMes.Text = Convert.ToString(lkpMes.Text);
                        frm.lblMes.Text = lkpMes.EditValue.ToString();
                        frm.txtMovimientoBanco.Text = variosToolStripMenuItem.Text.ToString();
                        frm.Show();
                        frm.SetCancel();
                        frm.lkpSituacion.EditValue = Oble.iid_situacion_movimiento_banco;
                        frm.lkpTipoDocumento.EditValue = Oble.ii_tipo_doc;
                        frm.txtNumeroDeDocumento.Text = Oble.vnro_documento;
                        frm.lkpMoneda.EditValue = Oble.iid_tipo_moneda;
                        frm.txtGlosa.Text = Oble.vglosa;
                        frm.dtmFecha.EditValue = Oble.dfecha_movimiento;
                        frm.txtTipoDeCambio.Text = Oble.nmonto_tipo_cambio.ToString();
                        frm.txtBeneficia.Text = Oble.vdescripcion_beneficiario;
                        frm.txtMonto.EditValue = Oble.nmonto_movimiento;
                        frm.rdTipoMovimiento.SelectedIndex = Convert.ToInt32(Oble.cflag_tipo_movimiento);
                        frm.calcular();
                    }
                    #endregion
                    #region Motivo Cuentas por Cobrar
                    if (int.Parse(viewLibroBancos.GetFocusedRowCellValue("iid_motivo_mov_banco").ToString()) == Parametros.intMotivoCuentasPorCobrar)
                    {
                        FrmManteCuentasPorCobrar Cuentas = new FrmManteCuentasPorCobrar();
                        //Cuentas.MiEvento += new FrmManteCuentasPorCobrar.DelegadoMensaje(Modify);
                        ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                        Cuentas.IdCodCorrelativo = Obe.icod_correlativo;
                        Cuentas.Correlative = Obe.iid_correlativo;
                        Cuentas.Show();
                        Cuentas.SetCancel();
                        Cuentas.IdMotivoMovimientoBanco = Parametros.intMotivoCuentasPorCobrar;
                        Cuentas.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                        Cuentas.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                        Cuentas.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                        Cuentas.txtMes.Text = Convert.ToString(lkpMes.Text);
                        Cuentas.lblMes.Text = lkpMes.EditValue.ToString();
                        Cuentas.txtMovimientoBanco.Text = Convert.ToString(ctaporcobrartoolStripMenuItem.Text);
                        Cuentas.lkpTipoDocumento.EditValue = Obe.ii_tipo_doc;
                        Cuentas.lkpSituacion.EditValue = Obe.iid_situacion_movimiento_banco;
                        Cuentas.txtNumeroDeDocumento.Text = Obe.vnro_documento;
                        Cuentas.intIdAnaliticaCliente = Obe.anac_icod_analitica_cliente;
                        Cuentas.bteCliente.Tag = Obe.cliec_icod_cliente;
                        Cuentas.bteCliente.Text = Obe.cliec_vnombre_cliente;
                        Cuentas.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                        Cuentas.txtGlosa.Text = Obe.vglosa;
                        Cuentas.dtmFecha.EditValue = Obe.dfecha_movimiento;
                        Cuentas.txtBeneficia.Text = Obe.vdescripcion_beneficiario;
                        Cuentas.txtMonto.EditValue = Obe.nmonto_movimiento;
                        Cuentas.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();
                        Cuentas.grdLibroBancosDetalle.Enabled = true;
                        Cuentas.calcular();
                    }
                    #endregion
                    #region Motivo Cuentas por Pagar
                    if (int.Parse(viewLibroBancos.GetFocusedRowCellValue("iid_motivo_mov_banco").ToString()) == Parametros.intMotivoCuentasPorPagar)
                    {
                        FrmManteCuentasPorPagar Cuentas = new FrmManteCuentasPorPagar();
                        //Cuentas.MiEvento += new FrmManteCuentasPorPagar.DelegadoMensaje(Modify);
                        ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                        Cuentas.IdCodCorrelativo = Obe.icod_correlativo;
                        Cuentas.Correlative = Obe.iid_correlativo;
                        Cuentas.Show();
                        Cuentas.SetCancel();
                        Cuentas.IdMotivoMovimientoBanco = Parametros.intMotivoCuentasPorPagar;
                        Cuentas.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                        Cuentas.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                        Cuentas.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                        Cuentas.txtMes.Text = Convert.ToString(lkpMes.Text);
                        Cuentas.lblMes.Text = lkpMes.EditValue.ToString();
                        Cuentas.txtMovimientoBanco.Text = Convert.ToString(ctaporpagartoolStripMenuItem.Text);
                        Cuentas.lkpTipoDocumento.EditValue = Obe.ii_tipo_doc;
                        Cuentas.lkpSituacion.EditValue = Obe.iid_situacion_movimiento_banco;
                        Cuentas.txtNumeroDeDocumento.Text = Obe.vnro_documento;
                        Cuentas.intIdAnaliticaProveedor = Obe.anac_icod_analitica;
                        Cuentas.btnProveedor.Tag = Obe.proc_icod_proveedor;
                        Cuentas.btnProveedor.Text = Obe.proc_vnombrecompleto;
                        Cuentas.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                        Cuentas.txtGlosa.Text = Obe.vglosa;
                        Cuentas.dtmFecha.EditValue = Obe.dfecha_movimiento;
                        Cuentas.txtBeneficia.Text = Obe.vdescripcion_beneficiario;
                        Cuentas.txtMonto_.EditValue = Obe.nmonto_movimiento;
                        Cuentas.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();
                        Cuentas.grdLibroBancosDetalle.Enabled = true;

                    }
                    #endregion
                    #region Motivo Adelanto a Proveedor
                    if (int.Parse(viewLibroBancos.GetFocusedRowCellValue("iid_motivo_mov_banco").ToString()) == Parametros.intMotivoAdelantosProveedores)
                    {
                        FrmManteAdelantoProveedor AdelantoProveedor = new FrmManteAdelantoProveedor();
                        //AdelantoProveedor.MiEvento += new FrmManteAdelantoProveedor.DelegadoMensaje(Modify);
                        ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                        AdelantoProveedor.IdCorrelativoCabecera = Obe.icod_correlativo;
                        AdelantoProveedor.Correlative = Obe.iid_correlativo;
                        AdelantoProveedor.txtMes.Text = Convert.ToString(lkpMes.Text);
                        AdelantoProveedor.lblMes.Text = lkpMes.EditValue.ToString();
                        AdelantoProveedor.IdMotivoMovimientoBanco = Parametros.intMotivoAdelantosProveedores;
                        AdelantoProveedor.Show();
                        AdelantoProveedor.SetCancel();
                        AdelantoProveedor.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                        AdelantoProveedor.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                        AdelantoProveedor.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                        AdelantoProveedor.dtmFecha.EditValue = Oble.dfecha_movimiento;
                        AdelantoProveedor.txtMovimientoBanco.Text = Convert.ToString(adelantoproveedortoolStripMenuItem.Text);
                        AdelantoProveedor.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();

                    }
                    #endregion
                    #region Motivo Adelato a Clientes
                    if (int.Parse(viewLibroBancos.GetFocusedRowCellValue("iid_motivo_mov_banco").ToString()) == Parametros.intMotivoAdelantosClientes)
                    {
                        FrmManteAdelantoClientes AdelantoCliente = new FrmManteAdelantoClientes();
                        //AdelantoCliente.MiEvento += new FrmManteAdelantoClientes.DelegadoMensaje(Modify);
                        ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                        AdelantoCliente.mes = Convert.ToInt32(lkpMes.EditValue);
                        AdelantoCliente.IdCorrelativoCabecera = Obe.icod_correlativo;
                        AdelantoCliente.Correlative = Obe.iid_correlativo;
                        AdelantoCliente.txtMes.Text = Convert.ToString(lkpMes.Text);
                        AdelantoCliente.lblMes.Text = lkpMes.EditValue.ToString();
                        AdelantoCliente.IdMotivoMovimientoBanco = Parametros.intMotivoAdelantosClientes;
                        AdelantoCliente.Show();
                        AdelantoCliente.SetCancel();
                        AdelantoCliente.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                        AdelantoCliente.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                        AdelantoCliente.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                        AdelantoCliente.dtmFecha.EditValue = Obe.dfecha_movimiento;
                        AdelantoCliente.txtMovimientoBanco.Text = Convert.ToString(adelantoclientetoolStripMenuItem.Text);
                        AdelantoCliente.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();

                    }
                    #endregion
                    #region Motivo Detracciones
                    //if (int.Parse(viewLibroBancos.GetFocusedRowCellValue("iid_motivo_mov_banco").ToString()) == Parametros.intMotivoPagoDetraccionesProveedores)
                    //{
                    //    FrmMantePagoDetraccion EDetraccion = new FrmMantePagoDetraccion();
                    //    //EDetraccion.MiEvento += new FrmMantePagoDetraccion.DelegadoMensaje(Modify);
                    //    ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                    //    EDetraccion.IdCodCorrelativo = Obe.icod_correlativo;
                    //    EDetraccion.Correlative = Obe.iid_correlativo;
                    //    EDetraccion.Show();
                    //    EDetraccion.SetCancel();
                    //    EDetraccion.IdMotivoMovimientoBanco = Parametros.intMotivoPagoDetraccionesProveedores;
                    //    EDetraccion.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                    //    EDetraccion.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                    //    EDetraccion.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                    //    EDetraccion.txtMes.Text = Convert.ToString(lkpMes.Text);
                    //    EDetraccion.lblMes.Text = lkpMes.EditValue.ToString();
                    //    EDetraccion.txtMovimientoBanco.Text = Convert.ToString(pagodetracciontoolStripMenuItem.Text);
                    //    EDetraccion.lkpTipoDocumento.EditValue = Obe.ii_tipo_doc;
                    //    EDetraccion.lkpSituacion.EditValue = Obe.iid_situacion_movimiento_banco;
                    //    EDetraccion.txtNumeroDeDocumento.Text = Obe.vnro_documento;
                    //    EDetraccion.intIdAnaliticaProveedor = Obe.anac_icod_analitica;
                    //    EDetraccion.btnProveedor.Tag = Obe.proc_icod_proveedor;
                    //    EDetraccion.btnProveedor.Text = Obe.proc_vnombrecompleto;
                    //    EDetraccion.txtNumeroDeposito.Text = Obe.vnro_deposito_detraccion;
                    //    EDetraccion.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                    //    EDetraccion.txtGlosa.Text = Obe.vglosa;
                    //    EDetraccion.dtmFecha.EditValue = Obe.dfecha_movimiento;
                    //    EDetraccion.txtBeneficia.Text = Obe.vdescripcion_beneficiario;
                    //    EDetraccion.txtMonto.EditValue = Obe.nmonto_movimiento;
                    //    EDetraccion.grdLibroBancosDetalle.Enabled = true;
                    //    EDetraccion.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();
                    //}
                    #endregion
                    #region Motivo Adelanto / Nota de Credito Clientes
                    if (int.Parse(viewLibroBancos.GetFocusedRowCellValue("iid_motivo_mov_banco").ToString()) == Parametros.intMotivoPagoAdelantadoClientes)
                    {
                        FrmMantePagoAdelantoNotaCreditoCliente PagoAdelantoCliente = new FrmMantePagoAdelantoNotaCreditoCliente();
                        //PagoAdelantoCliente.MiEvento += new FrmMantePagoAdelantoNotaCreditoCliente.DelegadoMensaje(Modify);
                        ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                        PagoAdelantoCliente.IdCodCorrelativo = Obe.icod_correlativo;
                        PagoAdelantoCliente.Correlative = Obe.iid_correlativo;
                        PagoAdelantoCliente.lblMes.Text = lkpMes.EditValue.ToString();
                        PagoAdelantoCliente.Show();
                        PagoAdelantoCliente.SetCancel();
                        PagoAdelantoCliente.IdMotivoMovimientoBanco = Parametros.intMotivoPagoAdelantadoClientes;
                        PagoAdelantoCliente.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                        PagoAdelantoCliente.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                        PagoAdelantoCliente.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                        PagoAdelantoCliente.txtMes.Text = Convert.ToString(lkpMes.Text);
                        PagoAdelantoCliente.lblMes.Text = lkpMes.EditValue.ToString();
                        PagoAdelantoCliente.txtMovimientoBanco.Text = Convert.ToString(pagoadenotacreditoclientestoolStripMenuItem.Text);
                        PagoAdelantoCliente.lkpTipoDocumento.EditValue = Obe.ii_tipo_doc;
                        PagoAdelantoCliente.lkpSituacion.EditValue = Obe.iid_situacion_movimiento_banco;
                        PagoAdelantoCliente.txtNumeroDeDocumento.Text = Obe.vnro_documento;
                        PagoAdelantoCliente.intIdAnaliticaCliente = Obe.anac_icod_analitica_cliente;
                        PagoAdelantoCliente.btnCliente.Tag = Obe.cliec_icod_cliente;
                        PagoAdelantoCliente.btnCliente.Text = Obe.cliec_vnombre_cliente;
                        PagoAdelantoCliente.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                        PagoAdelantoCliente.txtGlosa.Text = Obe.vglosa;
                        PagoAdelantoCliente.dtmFecha.EditValue = Obe.dfecha_movimiento;
                        PagoAdelantoCliente.txtBeneficia.Text = Obe.vdescripcion_beneficiario;
                        PagoAdelantoCliente.txtMonto.EditValue = Obe.nmonto_movimiento;
                        PagoAdelantoCliente.grdLibroBancosDetalle.Enabled = true;
                        PagoAdelantoCliente.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();

                    }
                    #endregion
                    #region Adelanto / Nota de Credito Proveedores
                    if (int.Parse(viewLibroBancos.GetFocusedRowCellValue("iid_motivo_mov_banco").ToString()) == Parametros.intMotivoPagoAdelantadoProveedores)
                    {
                        FrmMantePagoAdelantoNotaCreditoProveedor PagoAdelantoProveedor = new FrmMantePagoAdelantoNotaCreditoProveedor();
                        //PagoAdelantoProveedor.MiEvento += new FrmMantePagoAdelantoNotaCreditoProveedor.DelegadoMensaje(Modify);
                        ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                        PagoAdelantoProveedor.IdCodCorrelativo = Obe.icod_correlativo;
                        PagoAdelantoProveedor.Correlative = Obe.iid_correlativo;
                        PagoAdelantoProveedor.lblMes.Text = lkpMes.EditValue.ToString();
                        PagoAdelantoProveedor.Show();
                        PagoAdelantoProveedor.SetCancel();
                        PagoAdelantoProveedor.IdMotivoMovimientoBanco = Parametros.intMotivoPagoAdelantadoProveedores;
                        PagoAdelantoProveedor.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                        PagoAdelantoProveedor.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                        PagoAdelantoProveedor.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                        PagoAdelantoProveedor.txtMes.Text = Convert.ToString(lkpMes.Text);
                        PagoAdelantoProveedor.lblMes.Text = lkpMes.EditValue.ToString();
                        PagoAdelantoProveedor.txtMovimientoBanco.Text = Convert.ToString(pagoadenotacreditoproveedortoolStripMenuItem.Text);
                        PagoAdelantoProveedor.lkpTipoDocumento.EditValue = Obe.ii_tipo_doc;
                        PagoAdelantoProveedor.lkpSituacion.EditValue = Obe.iid_situacion_movimiento_banco;
                        PagoAdelantoProveedor.txtNumeroDeDocumento.Text = Obe.vnro_documento;
                        PagoAdelantoProveedor.intIdAnaliticaProveedor = Obe.anac_icod_analitica;
                        PagoAdelantoProveedor.btnProveedor.Tag = Obe.proc_icod_proveedor;
                        PagoAdelantoProveedor.btnProveedor.Text = Obe.proc_vnombrecompleto;
                        PagoAdelantoProveedor.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                        PagoAdelantoProveedor.txtGlosa.Text = Obe.vglosa;
                        PagoAdelantoProveedor.dtmFecha.EditValue = Obe.dfecha_movimiento;
                        PagoAdelantoProveedor.txtBeneficia.Text = Obe.vdescripcion_beneficiario;
                        PagoAdelantoProveedor.txtMonto.EditValue = Obe.nmonto_movimiento;
                        PagoAdelantoProveedor.grdLibroBancosDetalle.Enabled = true;
                        PagoAdelantoProveedor.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();

                    }
                    #endregion
                    #region Letra Rechazada
                    //if (int.Parse(viewLibroBancos.GetFocusedRowCellValue("iid_motivo_mov_banco").ToString()) == Parametros.intMotivoCargoPorLetraRechazada)
                    //{
                    //    FrmManteLetraRechazada LetraRechazada = new FrmManteLetraRechazada();
                    //    //LetraRechazada.MiEvento += new FrmManteLetraRechazada.DelegadoMensaje(Modify);
                    //    ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                    //    LetraRechazada.IdCorrelativoCabecera = Obe.icod_correlativo;
                    //    LetraRechazada.Correlative = Obe.iid_correlativo;
                    //    LetraRechazada.IdMotivoMovimientoBanco = Parametros.intMotivoCargoPorLetraRechazada;
                    //    LetraRechazada.lblMes.Text = lkpMes.EditValue.ToString();
                    //    LetraRechazada.Show();
                    //    LetraRechazada.SetCancel();                        
                    //    LetraRechazada.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                    //    LetraRechazada.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                    //    LetraRechazada.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                    //    LetraRechazada.txtMes.Text = Convert.ToString(lkpMes.Text);
                    //    LetraRechazada.lblMes.Text = lkpMes.EditValue.ToString();
                    //    LetraRechazada.txtMovimientoBanco.Text = Convert.ToString(letrarechazadatoolStripMenuItem.Text);
                    //    LetraRechazada.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();

                    //}
                    #endregion
                }
            }   
        }

        private void viewLibroBancos_DoubleClick(object sender, EventArgs e)
        {
            view();
        }
        
        private void lkpBanco_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpBanco.EditValue != null)
            {
                var lstCuentas = new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBanco.EditValue));
                BSControls.LoaderLook(lkpCuenta, lstCuentas, "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);
                if (lstCuentas.Count == 0)
                {
                    lstMovimientos.Clear();
                    viewLibroBancos.RefreshData();
                    mnuLibroBancos.Enabled = false;
                }
                else
                    mnuLibroBancos.Enabled = true;
            }           
        }
        #region Menu - Opciones
        private void ctaPorPagar()
        {
            try
            {
                if (String.IsNullOrEmpty(lkpBanco.Text))
                    throw new ArgumentException("Banco no seleccionado");
                if (String.IsNullOrEmpty(lkpCuenta.Text))
                    throw new ArgumentException("Cuenta no seleccionada");
                FrmManteCuentasPorPagar cuentas = new FrmManteCuentasPorPagar();
                cuentas.MiEvento += new FrmManteCuentasPorPagar.DelegadoMensaje(Modify);
                cuentas.IdMotivoMovimientoBanco = Parametros.intMotivoCuentasPorPagar;
                cuentas.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                cuentas.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                cuentas.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                cuentas.txtMes.Text = Convert.ToString(lkpMes.Text);
                cuentas.lblMes.Text = lkpMes.EditValue.ToString();
                cuentas.txtMovimientoBanco.Text = ctaporpagartoolStripMenuItem.Text.ToString();
                cuentas.Show();
                cuentas.SetInsert();
                cuentas.lkpSituacion.EditValue = Parametros.intSitLibroBancosRegistrado;
                cuentas.lkpMoneda.EditValue = Convert.ToInt32(lkpCuenta.GetColumnValue("tablc_iid_tipo_moneda"));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ctaPorCobrar()
        {
            try
            {
                if (String.IsNullOrEmpty(lkpBanco.Text))
                    throw new ArgumentException("Banco no seleccionado");
                if (String.IsNullOrEmpty(lkpCuenta.Text))
                    throw new ArgumentException("Cuenta no seleccionada");
                FrmManteCuentasPorCobrar cuentas = new FrmManteCuentasPorCobrar();
                cuentas.MiEvento += new FrmManteCuentasPorCobrar.DelegadoMensaje(Modify);
                cuentas.IdMotivoMovimientoBanco = Parametros.intMotivoCuentasPorCobrar;
                cuentas.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                cuentas.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                cuentas.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                cuentas.txtMes.Text = Convert.ToString(lkpMes.Text);
                cuentas.lblMes.Text = lkpMes.EditValue.ToString();
                cuentas.txtMovimientoBanco.Text = ctaporcobrartoolStripMenuItem.Text.ToString();
                cuentas.Show();
                cuentas.SetInsert();
                cuentas.lkpSituacion.EditValue = Parametros.intSitLibroBancosRegistrado;
                cuentas.lkpMoneda.EditValue = Convert.ToInt32(lkpCuenta.GetColumnValue("tablc_iid_tipo_moneda"));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void adelantoProveedor()
        {
            try
            {
                if (String.IsNullOrEmpty(lkpBanco.Text))
                    throw new ArgumentException("Banco no seleccionado");
                if (String.IsNullOrEmpty(lkpCuenta.Text))
                    throw new ArgumentException("Cuenta no seleccionada");
                FrmManteAdelantoProveedor AdelantoProveedor = new FrmManteAdelantoProveedor();
                AdelantoProveedor.MiEvento += new FrmManteAdelantoProveedor.DelegadoMensaje(Modify);
                AdelantoProveedor.CargaLista();
                ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                int i = 0;
                if (lstMovimientos.Count > 0)
                    i = lstMovimientos.Max(ob => Convert.ToInt32(ob.iid_correlativo));

                AdelantoProveedor.Correlative = Convert.ToInt32(i) + 1;
                AdelantoProveedor.IdMotivoMovimientoBanco = Parametros.intMotivoAdelantosProveedores;
                AdelantoProveedor.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                AdelantoProveedor.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                AdelantoProveedor.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                AdelantoProveedor.txtMes.Text = Convert.ToString(lkpMes.Text);
                AdelantoProveedor.lblMes.Text = lkpMes.EditValue.ToString();
                AdelantoProveedor.txtMovimientoBanco.Text = adelantoproveedortoolStripMenuItem.Text.ToString();
                AdelantoProveedor.Show();
                AdelantoProveedor.lkpSituacion.EditValue = Parametros.intSitLibroBancosRegistrado;
                AdelantoProveedor.lkpMoneda.EditValue = Convert.ToInt32(lkpCuenta.GetColumnValue("tablc_iid_tipo_moneda"));
                AdelantoProveedor.SetInsert();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void adelantoCliente()
        {
            try
            {
                if (String.IsNullOrEmpty(lkpBanco.Text))
                    throw new ArgumentException("Banco no seleccionado");
                if (String.IsNullOrEmpty(lkpCuenta.Text))
                    throw new ArgumentException("Cuenta no seleccionada");
                FrmManteAdelantoClientes AdelantoCliente = new FrmManteAdelantoClientes();
                AdelantoCliente.MiEvento += new FrmManteAdelantoClientes.DelegadoMensaje(Modify);
                ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                int i = 0;
                if (lstMovimientos.Count > 0)
                    i = lstMovimientos.Max(ob => Convert.ToInt32(ob.iid_correlativo));

                AdelantoCliente.Correlative = Convert.ToInt32(i) + 1;
                AdelantoCliente.IdMotivoMovimientoBanco = Parametros.intMotivoAdelantosClientes;
                AdelantoCliente.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                AdelantoCliente.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                AdelantoCliente.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                AdelantoCliente.txtMes.Text = Convert.ToString(lkpMes.Text);
                AdelantoCliente.lblMes.Text = lkpMes.EditValue.ToString();
                AdelantoCliente.txtMovimientoBanco.Text = adelantoclientetoolStripMenuItem.Text.ToString();
                AdelantoCliente.Show();
                AdelantoCliente.SetInsert();
                AdelantoCliente.lkpSituacion.EditValue = Parametros.intSitLibroBancosRegistrado;
                AdelantoCliente.lkpMoneda.EditValue = Convert.ToInt32(lkpCuenta.GetColumnValue("tablc_iid_tipo_moneda"));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ingresoVarios()
        {
            try
            {
                if (String.IsNullOrEmpty(lkpBanco.Text))
                    throw new ArgumentException("Banco no seleccionado");
                if (String.IsNullOrEmpty(lkpCuenta.Text))
                    throw new ArgumentException("Cuenta no seleccionada");
                FrmManteMovimientoVarios frm = new FrmManteMovimientoVarios();
                frm.MiEvento += new FrmManteMovimientoVarios.DelegadoMensaje(Modify);
                if (Convert.ToInt32(lkpMes.EditValue) == 0)
                    frm.grdDetalle.Enabled = false;
                frm.IdMotivoMovimientoBanco = Parametros.intMotivoVarios;
                frm.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                frm.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                frm.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                frm.txtMes.Text = Convert.ToString(lkpMes.Text);
                frm.lblMes.Text = lkpMes.EditValue.ToString();
                frm.txtMovimientoBanco.Text = variosToolStripMenuItem.Text.ToString();
                frm.id_tipo_moneda = Convert.ToInt32(lkpCuenta.GetColumnValue("tablc_iid_tipo_moneda"));
                frm.Show();
                frm.SetInsert();
                frm.lkpSituacion.EditValue = Parametros.intSitLibroBancosRegistrado;
                frm.lkpMoneda.EditValue = Convert.ToInt32(lkpCuenta.GetColumnValue("tablc_iid_tipo_moneda"));
                frm.rdTipoMovimiento.SelectedIndex = 1;
                icod_movimiento = frm.icod_movimiento;
                frm.Text = "Mantenimiento Movimiento Bancos - Ingresos Varios";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void egresoVarios() 
        {
            try
            {
                if (String.IsNullOrEmpty(lkpBanco.Text))
                    throw new ArgumentException("Banco no seleccionado");
                if (String.IsNullOrEmpty(lkpCuenta.Text))
                    throw new ArgumentException("Cuenta no seleccionada");
                FrmManteMovimientoVarios frm = new FrmManteMovimientoVarios();
                frm.MiEvento += new FrmManteMovimientoVarios.DelegadoMensaje(Modify);
                if (Convert.ToInt32(lkpMes.EditValue) == 0)
                    frm.grdDetalle.Enabled = false;
                frm.IdMotivoMovimientoBanco = Parametros.intMotivoVarios;
                frm.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                frm.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                frm.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                frm.txtMes.Text = Convert.ToString(lkpMes.Text);
                frm.lblMes.Text = lkpMes.EditValue.ToString();
                frm.txtMovimientoBanco.Text = variosToolStripMenuItem.Text.ToString();
                frm.id_tipo_moneda = Convert.ToInt32(lkpCuenta.GetColumnValue("tablc_iid_tipo_moneda"));
                frm.Show();
                frm.SetInsert();
                frm.lkpSituacion.EditValue = Parametros.intSitLibroBancosRegistrado;
                frm.lkpMoneda.EditValue = Convert.ToInt32(lkpCuenta.GetColumnValue("tablc_iid_tipo_moneda"));
                frm.rdTipoMovimiento.SelectedIndex = 0;
                icod_movimiento = frm.icod_movimiento;
                frm.Text = "Mantenimiento Movimiento Bancos - Egresos Varios";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pagoAdelantoNotaCreditoCliente()
        {
            try
            {
                if (String.IsNullOrEmpty(lkpBanco.Text))
                    throw new ArgumentException("Banco no seleccionado");
                if (String.IsNullOrEmpty(lkpCuenta.Text))
                    throw new ArgumentException("Cuenta no seleccionada");
                FrmMantePagoAdelantoNotaCreditoCliente PagoAdelantoCliente = new FrmMantePagoAdelantoNotaCreditoCliente();
                PagoAdelantoCliente.MiEvento += new FrmMantePagoAdelantoNotaCreditoCliente.DelegadoMensaje(Modify);
                ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                int i = 0;
                if (lstMovimientos.Count > 0)
                    i = lstMovimientos.Max(ob => Convert.ToInt32(ob.iid_correlativo));

                PagoAdelantoCliente.Correlative = Convert.ToInt32(i) + 1;
                PagoAdelantoCliente.IdMotivoMovimientoBanco = Parametros.intMotivoPagoAdelantadoClientes;
                PagoAdelantoCliente.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                PagoAdelantoCliente.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                PagoAdelantoCliente.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                PagoAdelantoCliente.txtMes.Text = Convert.ToString(lkpMes.Text);
                PagoAdelantoCliente.lblMes.Text = lkpMes.EditValue.ToString();
                PagoAdelantoCliente.txtMovimientoBanco.Text = pagoadenotacreditoclientestoolStripMenuItem.Text.ToString();
                PagoAdelantoCliente.Show();
                PagoAdelantoCliente.SetInsert();
                PagoAdelantoCliente.lkpSituacion.EditValue = Parametros.intSitLibroBancosRegistrado;
                PagoAdelantoCliente.lkpMoneda.EditValue = Convert.ToInt32(lkpCuenta.GetColumnValue("tablc_iid_tipo_moneda"));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pagoAdelantoNotaCreditoProveedor()
        {
            try
            {
                if (String.IsNullOrEmpty(lkpBanco.Text))
                    throw new ArgumentException("Banco no seleccionado");
                if (String.IsNullOrEmpty(lkpCuenta.Text))
                    throw new ArgumentException("Cuenta no seleccionada");
                /**/
                FrmMantePagoAdelantoNotaCreditoProveedor frm = new FrmMantePagoAdelantoNotaCreditoProveedor();
                frm.MiEvento += new FrmMantePagoAdelantoNotaCreditoProveedor.DelegadoMensaje(Modify);
                ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                int i = 0;
                if (lstMovimientos.Count > 0)
                    i = lstMovimientos.Max(ob => Convert.ToInt32(ob.iid_correlativo));

                frm.Correlative = Convert.ToInt32(i) + 1;
                frm.IdMotivoMovimientoBanco = Parametros.intMotivoPagoAdelantadoProveedores;
                frm.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                frm.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                frm.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                frm.txtMes.Text = Convert.ToString(lkpMes.Text);
                frm.lblMes.Text = lkpMes.EditValue.ToString();
                frm.txtMovimientoBanco.Text = pagoadenotacreditoproveedortoolStripMenuItem.Text.ToString();
                frm.Show();
                frm.SetInsert();
                frm.lkpSituacion.EditValue = Parametros.intSitLibroBancosRegistrado;
                frm.lkpMoneda.EditValue = Convert.ToInt32(lkpCuenta.GetColumnValue("tablc_iid_tipo_moneda"));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void variosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ingresoVarios();
        }
        private void adelantoproveedortoolStripMenuItem_Click(object sender, EventArgs e)
        {
            adelantoProveedor();
        }
        private void adelantoclientetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            adelantoCliente();
        }  
        private void ctaporpagartoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ctaPorPagar();
        }
        private void ctaporcobrartoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ctaPorCobrar();
        }
        private void pagoadenotacreditoclientestoolStripMenuItem_Click(object sender, EventArgs e)
        {
            pagoAdelantoNotaCreditoCliente();
        }
        private void pagoadenotacreditoproveedortoolStripMenuItem_Click(object sender, EventArgs e)
        {
            pagoAdelantoNotaCreditoProveedor();
        }        
        #endregion
       
        private void modificarMovimiento(ELibroBancos Obe, bool Process)
        {
            if (Obe != null)
            {
                #region Motivo Apertura
                if (Obe.iid_motivo_mov_banco == Parametros.intMotivoApertura)
                {
                    XtraMessageBox.Show("Movimiento de Apertura, solo puede ser modificado mediante Registros de Saldos Iniciales de Cuentas Bancarias", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                #endregion
               //SaveLog(Obe.icod_correlativo, Parametros.intModifica);
                #region Motivo Varios
                if (Obe.iid_motivo_mov_banco == Parametros.intMotivoVarios)
                {
                    if (Obe.iid_situacion_movimiento_banco == 2)
                    {
                        XtraMessageBox.Show("El movimiento no puede ser modificado , porque el documento esta anulado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    FrmManteMovimientoVarios frm = new FrmManteMovimientoVarios();
                    frm.MiEvento += new FrmManteMovimientoVarios.DelegadoMensaje(Modify);
                    if (Convert.ToInt32(lkpMes.EditValue) == 0)
                        frm.grdDetalle.Enabled = false;
                    frm.IdMotivoMovimientoBanco = Parametros.intMotivoVarios;
                    frm.flag_conciliacion = Obe.cflag_conciliacion;
                    
                    frm.Cab_icod_correlativo = Obe.icod_correlativo;
                    frm.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                    frm.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                    frm.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                    frm.txtMes.Text = Convert.ToString(lkpMes.Text);
                    frm.lblMes.Text = lkpMes.EditValue.ToString();
                    frm.id_tipo_moneda = Obe.iid_tipo_moneda;
                    frm.txtMovimientoBanco.Text = variosToolStripMenuItem.Text.ToString();
                    frm.Show();
                    frm.SetModify();
                    frm.lkpSituacion.EditValue = Obe.iid_situacion_movimiento_banco;
                    frm.lkpTipoDocumento.EditValue = Obe.ii_tipo_doc;
                    frm.txtNumeroDeDocumento.Text = Obe.vnro_documento;
                    frm.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                    frm.txtGlosa.Text = Obe.vglosa;
                    frm.dtmFecha.EditValue = Obe.dfecha_movimiento;
                    frm.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();
                    frm.txtBeneficia.Text = Obe.vdescripcion_beneficiario;
                    frm.txtMonto.EditValue = Obe.nmonto_movimiento;
                    frm.rdTipoMovimiento.SelectedIndex = Convert.ToInt32(Obe.cflag_tipo_movimiento);
                    frm.calcular();
                    if (Process)
                        frm.generarVoucher2();
                    frm.dtFechaDiferida.EditValue = Obe.mobac_sfecha_diferida;
                    frm.dtFechaDiferida.Enabled = (Obe.mobac_sfecha_diferida != null) ? true : false;
                }
                #endregion
                #region Motivo Cuentas por Cobrar
                if (Obe.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorCobrar)
                {
                    if (Obe.iid_situacion_movimiento_banco == 2)
                    {
                        XtraMessageBox.Show("El movimiento no puede ser modificado , porque el documento esta anulado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    FrmManteCuentasPorCobrar frm_CtaPorCobrar = new FrmManteCuentasPorCobrar();
                    frm_CtaPorCobrar.MiEvento += new FrmManteCuentasPorCobrar.DelegadoMensaje(Modify);
                    frm_CtaPorCobrar.flag_conciliacion = Obe.cflag_conciliacion;
                    frm_CtaPorCobrar.IdCodCorrelativo = Obe.icod_correlativo;
                    frm_CtaPorCobrar.Correlative = Obe.iid_correlativo;
                    frm_CtaPorCobrar.Show();
                    frm_CtaPorCobrar.SetModify();
                    frm_CtaPorCobrar.IdMotivoMovimientoBanco = Parametros.intMotivoCuentasPorCobrar;
                    frm_CtaPorCobrar.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                    frm_CtaPorCobrar.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                    frm_CtaPorCobrar.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                    frm_CtaPorCobrar.txtMes.Text = Convert.ToString(lkpMes.Text);
                    frm_CtaPorCobrar.lblMes.Text = lkpMes.EditValue.ToString();
                    frm_CtaPorCobrar.txtMovimientoBanco.Text = Convert.ToString(ctaporcobrartoolStripMenuItem.Text);
                    frm_CtaPorCobrar.lkpTipoDocumento.EditValue = Obe.ii_tipo_doc;
                    frm_CtaPorCobrar.lkpSituacion.EditValue = Obe.iid_situacion_movimiento_banco;
                    frm_CtaPorCobrar.txtNumeroDeDocumento.Text = Obe.vnro_documento;
                    frm_CtaPorCobrar.intIdAnaliticaCliente = Obe.anac_icod_analitica_cliente;
                    frm_CtaPorCobrar.bteCliente.Tag = Obe.cliec_icod_cliente;
                    frm_CtaPorCobrar.bteCliente.Text = Obe.cliec_vnombre_cliente;
                    frm_CtaPorCobrar.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                    frm_CtaPorCobrar.txtGlosa.Text = Obe.vglosa;
                    frm_CtaPorCobrar.dtmFecha.EditValue = Obe.dfecha_movimiento;
                    frm_CtaPorCobrar.txtBeneficia.Text = Obe.vdescripcion_beneficiario;
                    frm_CtaPorCobrar.txtMonto.EditValue = Obe.nmonto_movimiento;
                    frm_CtaPorCobrar.grdLibroBancosDetalle.Enabled = true;
                    frm_CtaPorCobrar.btnGuardar.Enabled = true;
                    frm_CtaPorCobrar.calcular();
                    frm_CtaPorCobrar.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();
                    if (Process)
                        frm_CtaPorCobrar.generarVoucher2();
                }
                #endregion
                #region Motivo Cuentas por Pagar
                if (Obe.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorPagar)
                {
                    if (Obe.iid_situacion_movimiento_banco == 2)
                    {
                        XtraMessageBox.Show("El movimiento no puede ser modificado , porque el documento esta anulado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    FrmManteCuentasPorPagar frm = new FrmManteCuentasPorPagar();
                    frm.MiEvento += new FrmManteCuentasPorPagar.DelegadoMensaje(Modify);
                    frm.flag_conciliacion = Obe.cflag_conciliacion;
                    frm.IdCodCorrelativo = Obe.icod_correlativo;
                    frm.Correlative = Obe.iid_correlativo;
                    frm.Show();
                    frm.SetModify();
                    frm.IdMotivoMovimientoBanco = Parametros.intMotivoCuentasPorPagar;
                    frm.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                    frm.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                    frm.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                    frm.txtMes.Text = Convert.ToString(lkpMes.Text);
                    frm.lblMes.Text = lkpMes.EditValue.ToString();
                    frm.txtMovimientoBanco.Text = Convert.ToString(ctaporpagartoolStripMenuItem.Text);
                    frm.lkpTipoDocumento.EditValue = Obe.ii_tipo_doc;
                    frm.lkpSituacion.EditValue = Obe.iid_situacion_movimiento_banco;
                    frm.txtNumeroDeDocumento.Text = Obe.vnro_documento;
                    frm.intIdAnaliticaProveedor = Obe.anac_icod_analitica;
                    frm.btnProveedor.Tag = Obe.proc_icod_proveedor;
                    frm.btnProveedor.Text = Obe.proc_vnombrecompleto;
                    frm.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                    frm.txtGlosa.Text = Obe.vglosa;
                    frm.dtmFecha.EditValue = Obe.dfecha_movimiento;
                    frm.txtBeneficia.Text = Obe.vdescripcion_beneficiario;
                    frm.txtMonto_.EditValue = Obe.nmonto_movimiento;
                    frm.grdLibroBancosDetalle.Enabled = true;
                    frm.btnGuardar.Enabled = true;
                    frm.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();
                    if (Process)
                        frm.generarVoucher2();
                    frm.dtFechaDiferida.EditValue = Obe.mobac_sfecha_diferida;
                    frm.dtFechaDiferida.Enabled = (Obe.mobac_sfecha_diferida != null) ? true : false;
                }
                #endregion
                #region Motivo Adelanto a Proveedor
                if (Obe.iid_motivo_mov_banco == Parametros.intMotivoAdelantosProveedores)
                {
                    if (Obe.iid_situacion_movimiento_banco == 2)
                    {
                        XtraMessageBox.Show("El movimiento no puede ser modificado , porque el documento esta anulado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    FrmManteAdelantoProveedor frm = new FrmManteAdelantoProveedor();
                    frm.MiEvento += new FrmManteAdelantoProveedor.DelegadoMensaje(Modify);
                    
                    frm.flag_conciliacion = Obe.cflag_conciliacion;
                    frm.IdCorrelativoCabecera = Obe.icod_correlativo;
                    frm.Correlative = Obe.iid_correlativo;
                    frm.CargaLista();
                    frm.txtNumeroAdelanto.Text = Obe.inumero_orden;
                    frm.txtMes.Text = Convert.ToString(lkpMes.Text);
                    frm.lblMes.Text = lkpMes.EditValue.ToString();
                    frm.IdMotivoMovimientoBanco = Parametros.intMotivoAdelantosProveedores;
               

                    frm.Show();
                    frm.SetModify();
                    frm.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                    frm.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                    frm.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                    frm.dtmFecha.EditValue = Obe.dfecha_movimiento;
                    frm.txtMovimientoBanco.Text = Convert.ToString(adelantoproveedortoolStripMenuItem.Text);
                    frm.btnGuardar.Enabled = true;
                    frm.DisableModify();
                    frm.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();
                    frm.dtFechaDiferida.EditValue = Obe.mobac_sfecha_diferida;
                    frm.dtFechaDiferida.Enabled = (Obe.mobac_sfecha_diferida != null) ? true : false;
                    
                }
                #endregion
                #region Motivo Adelato a Clientes
                if (Obe.iid_motivo_mov_banco == Parametros.intMotivoAdelantosClientes)
                {
                    if (Obe.iid_situacion_movimiento_banco == 2)
                    {
                        XtraMessageBox.Show("El movimiento no puede ser modificado , porque el documento esta anulado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    FrmManteAdelantoClientes frm_AdelCliente = new FrmManteAdelantoClientes();
                  
                    frm_AdelCliente.MiEvento += new FrmManteAdelantoClientes.DelegadoMensaje(Modify);
                    frm_AdelCliente.flag_conciliacion = Obe.cflag_conciliacion;
                    frm_AdelCliente.mes = Convert.ToInt32(lkpMes.EditValue);
                    frm_AdelCliente.IdCorrelativoCabecera = Obe.icod_correlativo;
                    frm_AdelCliente.txtMes.Text = Convert.ToString(lkpMes.Text);
                    frm_AdelCliente.lblMes.Text = lkpMes.EditValue.ToString();
                    frm_AdelCliente.Correlative = Obe.iid_correlativo;
                    frm_AdelCliente.IdMotivoMovimientoBanco = Parametros.intMotivoAdelantosClientes;
                    frm_AdelCliente.Show();
                    frm_AdelCliente.SetModify();
                    frm_AdelCliente.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                    frm_AdelCliente.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                    frm_AdelCliente.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                    frm_AdelCliente.dtmFecha.EditValue = Obe.dfecha_movimiento;
                    frm_AdelCliente.txtMovimientoBanco.Text = Convert.ToString(adelantoclientetoolStripMenuItem.Text);
                    frm_AdelCliente.btnGuardar.Enabled = true;
                    frm_AdelCliente.DisableModify();
                    frm_AdelCliente.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();
                    frm_AdelCliente.lkpTipoDocumento.EditValue = Obe.ii_tipo_doc;
                    frm_AdelCliente.txtNumeroDeDocumento.Text = Obe.vnro_documento;
                    frm_AdelCliente.btnCliente.Text = Obe.cliec_vnombre_cliente;
                    frm_AdelCliente.btnCliente.Tag = Obe.cliec_icod_cliente;
                    frm_AdelCliente.lkpSituacion.EditValue = Obe.iid_situacion_movimiento_banco;
                    frm_AdelCliente.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                    frm_AdelCliente.txtGlosa.Text = Obe.vglosa;
                    frm_AdelCliente.txtMonto.Text = Obe.nmonto_movimiento.ToString();
                    frm_AdelCliente.txtBeneficia.Text = Obe.vdescripcion_beneficiario;
                }
                #endregion
                #region Motivo Detracciones
                //if (Obe.iid_motivo_mov_banco == Parametros.intMotivoPagoDetraccionesProveedores)
                //{
                //    if (Obe.iid_situacion_movimiento_banco == 2)
                //    {
                //        XtraMessageBox.Show("El movimiento no puede ser modificado , porque el documento esta anulado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //    FrmMantePagoDetraccion frm_Detraccion = new FrmMantePagoDetraccion();
                //    frm_Detraccion.MiEvento += new FrmMantePagoDetraccion.DelegadoMensaje(Modify);
                //    frm_Detraccion.flag_conciliacion = Obe.cflag_conciliacion;
                //    frm_Detraccion.vcocc_iid_voucher_contable = Convert.ToInt32(Obe.vcocc_iid_voucher_contable);
                //    frm_Detraccion.IdCodCorrelativo = Obe.icod_correlativo;
                //    frm_Detraccion.Correlative = Obe.iid_correlativo;
                //    frm_Detraccion.Show();
                //    frm_Detraccion.SetModify();
                //    frm_Detraccion.IdMotivoMovimientoBanco = Parametros.intMotivoPagoDetraccionesProveedores;
                //    frm_Detraccion.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                //    frm_Detraccion.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                //    frm_Detraccion.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                //    frm_Detraccion.txtMes.Text = Convert.ToString(lkpMes.Text);
                //    frm_Detraccion.lblMes.Text = lkpMes.EditValue.ToString();
                //    frm_Detraccion.txtMovimientoBanco.Text = Convert.ToString(pagodetracciontoolStripMenuItem.Text);
                //    frm_Detraccion.lkpTipoDocumento.EditValue = Obe.ii_tipo_doc;
                //    frm_Detraccion.lkpSituacion.EditValue = Obe.iid_situacion_movimiento_banco;
                //    frm_Detraccion.txtNumeroDeDocumento.Text = Obe.vnro_documento;
                //    frm_Detraccion.intIdAnaliticaProveedor = Obe.anac_icod_analitica;
                //    frm_Detraccion.btnProveedor.Tag = Obe.proc_icod_proveedor;
                //    frm_Detraccion.btnProveedor.Text = Obe.proc_vnombrecompleto;
                //    frm_Detraccion.txtNumeroDeposito.Text = Obe.vnro_deposito_detraccion;
                //    frm_Detraccion.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                //    frm_Detraccion.txtGlosa.Text = Obe.vglosa;
                //    frm_Detraccion.dtmFecha.EditValue = Obe.dfecha_movimiento;
                //    frm_Detraccion.txtBeneficia.Text = Obe.vdescripcion_beneficiario;
                //    frm_Detraccion.txtMonto.EditValue = Obe.nmonto_movimiento;
                //    frm_Detraccion.grdLibroBancosDetalle.Enabled = true;
                //    frm_Detraccion.btnGuardar.Enabled = true;
                //    frm_Detraccion.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();
                //    if (Process)
                //        frm_Detraccion.generarVoucher2();
                //}
                #endregion
                #region Motivo Adelanto / Nota de Credito Clientes
                if (Obe.iid_motivo_mov_banco == Parametros.intMotivoPagoAdelantadoClientes)
                {
                    if (Obe.iid_situacion_movimiento_banco == 2)
                    {
                        XtraMessageBox.Show("El movimiento no puede ser modificado , porque el documento esta anulado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    FrmMantePagoAdelantoNotaCreditoCliente frm_PagoAdel_Cliente = new FrmMantePagoAdelantoNotaCreditoCliente();
                    frm_PagoAdel_Cliente.MiEvento += new FrmMantePagoAdelantoNotaCreditoCliente.DelegadoMensaje(Modify);
                    frm_PagoAdel_Cliente.flag_conciliacion = Obe.cflag_conciliacion;
                    frm_PagoAdel_Cliente.IdCodCorrelativo = Obe.icod_correlativo;
                    frm_PagoAdel_Cliente.Correlative = Obe.iid_correlativo;
                    frm_PagoAdel_Cliente.lblMes.Text = lkpMes.EditValue.ToString();
                    frm_PagoAdel_Cliente.Show();
                    frm_PagoAdel_Cliente.SetModify();
                    frm_PagoAdel_Cliente.IdMotivoMovimientoBanco = Parametros.intMotivoPagoAdelantadoClientes;
                    frm_PagoAdel_Cliente.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                    frm_PagoAdel_Cliente.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                    frm_PagoAdel_Cliente.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                    frm_PagoAdel_Cliente.txtMes.Text = Convert.ToString(lkpMes.Text);
                    frm_PagoAdel_Cliente.lblMes.Text = lkpMes.EditValue.ToString();
                    frm_PagoAdel_Cliente.txtMovimientoBanco.Text = Convert.ToString(pagoadenotacreditoclientestoolStripMenuItem.Text);
                    frm_PagoAdel_Cliente.lkpTipoDocumento.EditValue = Obe.ii_tipo_doc;
                    frm_PagoAdel_Cliente.lkpSituacion.EditValue = Obe.iid_situacion_movimiento_banco;
                    frm_PagoAdel_Cliente.txtNumeroDeDocumento.Text = Obe.vnro_documento;
                    frm_PagoAdel_Cliente.intIdAnaliticaCliente = Obe.anac_icod_analitica_cliente;
                    frm_PagoAdel_Cliente.btnCliente.Tag = Obe.cliec_icod_cliente;
                    frm_PagoAdel_Cliente.btnCliente.Text = Obe.cliec_vnombre_cliente;
                    frm_PagoAdel_Cliente.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                    frm_PagoAdel_Cliente.txtGlosa.Text = Obe.vglosa;
                    frm_PagoAdel_Cliente.dtmFecha.EditValue = Obe.dfecha_movimiento;
                    frm_PagoAdel_Cliente.txtBeneficia.Text = Obe.vdescripcion_beneficiario;
                    frm_PagoAdel_Cliente.txtMonto.EditValue = Obe.nmonto_movimiento;
                    frm_PagoAdel_Cliente.grdLibroBancosDetalle.Enabled = true;
                    frm_PagoAdel_Cliente.btnGuardar.Enabled = true;
                    frm_PagoAdel_Cliente.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();
                }
                #endregion
                #region Adelanto / Nota de Credito Proveedores
                if (Obe.iid_motivo_mov_banco == Parametros.intMotivoPagoAdelantadoProveedores)
                {
                    if (Obe.iid_situacion_movimiento_banco == 2)
                    {
                        XtraMessageBox.Show("El movimiento no puede ser modificado , porque el documento esta anulado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    FrmMantePagoAdelantoNotaCreditoProveedor frm = new FrmMantePagoAdelantoNotaCreditoProveedor();
                    frm.MiEvento += new FrmMantePagoAdelantoNotaCreditoProveedor.DelegadoMensaje(Modify);
                    frm.flag_conciliacion = Obe.cflag_conciliacion;
                    frm.IdCodCorrelativo = Obe.icod_correlativo;
                    frm.Correlative = Obe.iid_correlativo;
                    frm.lblMes.Text = lkpMes.EditValue.ToString();
                    frm.Show();
                    frm.SetModify();
                    frm.IdMotivoMovimientoBanco = Parametros.intMotivoPagoAdelantadoProveedores;
                    frm.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                    frm.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                    frm.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                    frm.txtMes.Text = Convert.ToString(lkpMes.Text);
                    frm.lblMes.Text = lkpMes.EditValue.ToString();
                    frm.txtMovimientoBanco.Text = Convert.ToString(pagoadenotacreditoproveedortoolStripMenuItem.Text);
                    frm.lkpTipoDocumento.EditValue = Obe.ii_tipo_doc;
                    frm.lkpSituacion.EditValue = Obe.iid_situacion_movimiento_banco;
                    frm.txtNumeroDeDocumento.Text = Obe.vnro_documento;
                    frm.intIdAnaliticaProveedor = Obe.anac_icod_analitica;
                    frm.btnProveedor.Tag = Obe.proc_icod_proveedor;
                    frm.btnProveedor.Text = Obe.proc_vnombrecompleto;
                    frm.lkpMoneda.EditValue = Obe.iid_tipo_moneda;
                    frm.txtGlosa.Text = Obe.vglosa;
                    frm.dtmFecha.EditValue = Obe.dfecha_movimiento;
                    frm.txtBeneficia.Text = Obe.vdescripcion_beneficiario;
                    frm.txtMonto.EditValue = Obe.nmonto_movimiento;
                    frm.grdLibroBancosDetalle.Enabled = true;
                    frm.btnGuardar.Enabled = true;
                    frm.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();
                    frm.dtFechaDiferida.EditValue = Obe.mobac_sfecha_diferida;
                    frm.dtFechaDiferida.Enabled = (Obe.mobac_sfecha_diferida != null) ? true : false;
                }
                #endregion
                #region Letra Rechazada
                //if (Obe.iid_motivo_mov_banco == Parametros.intMotivoCargoPorLetraRechazada)
                //{
                //    if (Obe.iid_situacion_movimiento_banco == 2)
                //    {
                //        XtraMessageBox.Show("El movimiento no puede ser modificado , porque el documento esta anulado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //    FrmManteLetraRechazada frm_LetraRechazada = new FrmManteLetraRechazada();
                //    frm_LetraRechazada.MiEvento += new FrmManteLetraRechazada.DelegadoMensaje(Modify);
                //    frm_LetraRechazada.flag_conciliacion = Obe.cflag_conciliacion;
                //    frm_LetraRechazada.vcocc_iid_voucher_contable = Convert.ToInt32(Obe.vcocc_iid_voucher_contable);
                //    frm_LetraRechazada.IdCorrelativoCabecera = Obe.icod_correlativo;
                //    frm_LetraRechazada.Correlative = Obe.iid_correlativo;
                //    frm_LetraRechazada.IdMotivoMovimientoBanco = Parametros.intMotivoCargoPorLetraRechazada;
                //    frm_LetraRechazada.lblMes.Text = lkpMes.EditValue.ToString();                    
                //    frm_LetraRechazada.Show();
                //    frm_LetraRechazada.SetModify();
                //    frm_LetraRechazada.txtBanco.Text = Convert.ToString(lkpBanco.Text);
                //    frm_LetraRechazada.lblCuenta.Text = lkpCuenta.EditValue.ToString();
                //    frm_LetraRechazada.txtCuenta.Text = Convert.ToString(lkpCuenta.Text);
                //    frm_LetraRechazada.txtMes.Text = Convert.ToString(lkpMes.Text);
                //    frm_LetraRechazada.lblMes.Text = lkpMes.EditValue.ToString();
                //    frm_LetraRechazada.txtMovimientoBanco.Text = Convert.ToString(letrarechazadatoolStripMenuItem.Text);
                //    frm_LetraRechazada.btnGuardar.Enabled = true;
                //    frm_LetraRechazada.DisableModify();
                //    frm_LetraRechazada.txtTipoDeCambio.Text = Obe.nmonto_tipo_cambio.ToString();
                //}
                #endregion
                #region Transferencia
                if (Obe.iid_motivo_mov_banco == Parametros.intMotivoTransferenciaCuentas)
                {
                    if (Obe.Abono > 0)
                        return;
                    frmManteTransferencia frm = new frmManteTransferencia();
                    frm.MiEvento += new frmManteTransferencia.DelegadoMensaje(Modify);
                    frm.oBe = Obe;
                    frm.intBanco = Convert.ToInt32(lkpBanco.EditValue);
                    frm.intCuenta = Convert.ToInt32(lkpCuenta.EditValue);
                    frm.intMes = Convert.ToInt32(lkpMes.EditValue);
                    frm.SetModify();
                    frm.Show();
                    frm.setValues();
                }
                #endregion
            }
        }

        private void eliminar()
        {
            try
            {
                
                if (lstMovimientos.Count > 0)
                {

                    ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                    Obe.iusuario_modifica = Valores.intUsuario;
                    Obe.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                    int index = viewLibroBancos.FocusedRowHandle;

                    if (Obe.mobac_origen_regitro == "PLN")
                    {
                        XtraMessageBox.Show("El registro ha sido generado desde un Planilla de Venta, no puede ser eliminado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    #region Motivo Apertura
                    if (Obe.iid_motivo_mov_banco == Parametros.intMotivoApertura)
                    {
                        XtraMessageBox.Show("Movimiento de Apertura, solo puede ser ", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    #endregion
                    int IdLibroBancos = Obe.icod_correlativo;
                    int IdMotivoBanco = Obe.iid_motivo_mov_banco;
                    #region Motivo Varios
                    if (IdMotivoBanco == Parametros.intMotivoVarios)
                    {
                        if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el documento " + Obe.TipoDocAbreviado + "-" + Obe.vnro_documento + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            BTesoreria oBl = new BTesoreria();
                            oBl.eliminarLibroBancos(Obe);
                            Carga();
                            //SaveLog(Obe.icod_correlativo, Parametros.intElimina);
                            //eliminarVoucher(Obe);
                        }
                    }
                    #endregion
                    #region Adelanto a Proveedores
                    if (IdMotivoBanco == Parametros.intMotivoAdelantosProveedores)
                    {
                        if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el documento " + Obe.TipoDocAbreviado + "-" + Obe.vnro_documento + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            BCuentasPorPagar objBL_AdelantoProveedor = new BCuentasPorPagar();
                            EAdelantoProveedor objE_AdelantoProveedor = new EAdelantoProveedor();

                            objE_AdelantoProveedor = objBL_AdelantoProveedor.ListarAdelantoProveedor(IdLibroBancos);
                            IdSituacion = objE_AdelantoProveedor.nsituacion_adelanto_proveedor;

                            if (IdSituacion == Parametros.intSitProveedorGenerado || IdSituacion == Parametros.intSitProveedorAnulado)
                            {
                                BTesoreria oBl = new BTesoreria();
                                oBl.eliminarLibroBancos(Obe);
                                Carga();
                              
                            }
                            else
                            {
                                string sSituaion = "";
                                if (IdSituacion == Parametros.intSitProveedorPagadoParcial) { sSituaion = "Pagado Parcial"; }
                                if (IdSituacion == Parametros.intSitProveedorCancelado) { sSituaion = "Cancelado"; }
                                XtraMessageBox.Show("No se puede eliminar el adelanto \n." + "Situación : " + sSituaion, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    #endregion
                    #region Adelanto a Clientes
                    if (IdMotivoBanco == Parametros.intMotivoAdelantosClientes)
                    {
                        if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el documento " + Obe.TipoDocAbreviado + "-" + Obe.vnro_documento + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            BCuentasPorCobrar objBL_AdelantoCliente = new BCuentasPorCobrar();
                            EAdelantoCliente objE_AdelantoCliente = new EAdelantoCliente();

                            objE_AdelantoCliente = objBL_AdelantoCliente.ListarAdelantoCliente(IdLibroBancos);
                            IdSituacion = objE_AdelantoCliente.nsituacion_adelanto_cliente;

                            if (IdSituacion == Parametros.intSitClienteGenerado || IdSituacion == Parametros.intSitClienteAnulado)
                            {
                                BTesoreria oBl = new BTesoreria();
                                oBl.eliminarLibroBancos(Obe);
                                Carga();
                                
                            }
                            else
                            {
                                string sSituaion = "";
                                if (IdSituacion == Parametros.intSitClientePagadoParcial) { sSituaion = "Pagado Parcial"; }
                                if (IdSituacion == Parametros.intSitClienteCancelado) { sSituaion = "Cancelado"; }
                                XtraMessageBox.Show("No se puede eliminar el adelanto \n." + "Situación : " + sSituaion, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    #endregion                    
                    #region Cuentas por Cobrar
                    if (IdMotivoBanco == Parametros.intMotivoCuentasPorCobrar)
                    {
                        if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el documento " + Obe.TipoDocAbreviado + "-" + Obe.vnro_documento + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            BTesoreria oBl = new BTesoreria();
                            oBl.eliminarLibroBancos(Obe);
                            Carga();
                            //SaveLog(Obe.icod_correlativo, Parametros.intElimina);
                            //eliminarVoucher(Obe);
                        }
                    }
                    #endregion
                    #region Cuentas por Pagar
                    if (IdMotivoBanco == Parametros.intMotivoCuentasPorPagar)
                    {
                        if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el documento " + Obe.TipoDocAbreviado + "-" + Obe.vnro_documento + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            BTesoreria oBl = new BTesoreria();
                            oBl.eliminarLibroBancos(Obe);
                            Carga();
                         
                        }
                    }
                    #endregion
                    #region Pago Adelantado a Clientes
                    if (IdMotivoBanco == Parametros.intMotivoPagoAdelantadoClientes)
                    {
                        if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el documento " + Obe.TipoDocAbreviado + "-" + Obe.vnro_documento + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            BTesoreria oBl = new BTesoreria();
                            oBl.eliminarLibroBancos(Obe);
                            Carga();
                            //SaveLog(Obe.icod_correlativo, Parametros.intElimina);
                            //eliminarVoucher(Obe);
                        }
                    }
                    #endregion
                    #region Pago Adalantado a Proveedores
                    if (IdMotivoBanco == Parametros.intMotivoPagoAdelantadoProveedores)
                    {
                        if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el documento " + Obe.TipoDocAbreviado + "-" + Obe.vnro_documento + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            BTesoreria oBl = new BTesoreria();
                            oBl.eliminarLibroBancos(Obe);
                            Carga();
                            //SaveLog(Obe.icod_correlativo, Parametros.intElimina);
                            //eliminarVoucher(Obe);
                        }
                    }
                    #endregion
                    #region Transferencia
                    if (IdMotivoBanco == Parametros.intMotivoTransferenciaCuentas)
                    {
                        if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el documento " + Obe.TipoDocAbreviado + "-" + Obe.vnro_documento + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            BTesoreria oBl = new BTesoreria();
                            oBl.eliminarLibroBancos(Obe);
                            Carga();
                            //SaveLog(Obe.icod_correlativo, Parametros.intElimina);
                            //eliminarVoucher(Obe);
                        }
                    }
                    #endregion

                    if (lstMovimientos.Count >= index + 1)
                        viewLibroBancos.FocusedRowHandle = index;
                    else
                        viewLibroBancos.FocusedRowHandle = index - 1;
                }
                
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void anulartoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstMovimientos.Count > 0)
                {
                    ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                    int index = viewLibroBancos.FocusedRowHandle;

                    if (Obe.mobac_origen_regitro == "PLN")
                    {
                        XtraMessageBox.Show("El registro ha sido generado desde un Planilla de Venta, no puede ser anulado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    
                    if (Obe.iid_situacion_movimiento_banco == 2)
                    {
                        XtraMessageBox.Show("El documeto ya fue anulado previamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (XtraMessageBox.Show("¿Esta seguro que desea anular el documento " + Obe.TipoDocAbreviado + "-" + Obe.vnro_documento + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        BTesoreria oBl = new BTesoreria();
                        Obe.iusuario_modifica = Valores.intUsuario;
                        Obe.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                        oBl.anularLibroBancos(Obe);
                        //SaveLog(Obe.icod_correlativo, Parametros.intAnula);
                        Carga();
                        if (lstMovimientos.Count >= index + 1)
                            viewLibroBancos.FocusedRowHandle = index;
                        else
                            viewLibroBancos.FocusedRowHandle = index - 1;
                        //eliminarVoucher(Obe);

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void eliminarVoucher(ELibroBancos Obe)
        {
            //if (Convert.ToInt32(Obe.vcocc_iid_voucher_contable) > 0)
            //{
            //    EComprobante obj = new EComprobante();
            //    obj.iid_voucher_contable = Convert.ToInt32(Obe.vcocc_iid_voucher_contable);
            //    BComprobantes oBl = new BComprobantes();
            //    oBl.EliminarComprobante(obj);
            //}
        }
        private void conciliartoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstMovimientos.Count > 0)
            {
                ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
                if (Obe.iid_motivo_mov_banco == 287)
                {
                    XtraMessageBox.Show("Saldo Inicial no puede ser modificado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (Obe.iid_situacion_movimiento_banco == 2)
                {
                    XtraMessageBox.Show("Acción no permitida , porque el documento esta anulado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                BTesoreria oBl = new BTesoreria();
                if (Obe.cflag_conciliacion)
                    oBl.conciliarLibroBancos(Obe.icod_correlativo, false, 0, WindowsIdentity.GetCurrent().Name.ToString());
                else
                    oBl.conciliarLibroBancos(Obe.icod_correlativo, true, 0, WindowsIdentity.GetCurrent().Name.ToString());
                //SaveLog(Obe.icod_correlativo, Parametros.intModifica);
                Carga();
                int index = lstMovimientos.FindIndex(x => x.icod_correlativo == Obe.icod_correlativo);
                viewLibroBancos.FocusedRowHandle = index;       
                
            }          
        }
        private void SaveLog(int icod_correlativo, int icod_actividad)
        {
            //BSControls.SaveLog(Parametros.intModTesoreria, null, Parametros.intEntidadFinancieraMovimiento, icod_correlativo, icod_actividad);            
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpMes.EditValue != null)
            {
                Carga();
                if (Convert.ToInt32(lkpMes.EditValue) == 0)                
                    EnableMenu(false);                
                else                 
                    EnableMenu(true);
            }
        }
        private void EnableMenu(bool Enable)
        {
            ctaporpagartoolStripMenuItem.Enabled = Enable;
            ctaporcobrartoolStripMenuItem.Enabled = Enable;
            adelantoproveedortoolStripMenuItem.Enabled = Enable;
            adelantoclientetoolStripMenuItem.Enabled = Enable;
            pagoadenotacreditoclientestoolStripMenuItem.Enabled = Enable;
            pagoadenotacreditoproveedortoolStripMenuItem.Enabled = Enable;  
        }

        private void lkpCuenta_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpCuenta.EditValue != null)
            {
                Carga();
            }
        }

        #endregion

        #region Metodos
        
        private void Carga()
        {
            try
            {
                if (Convert.ToInt32(lkpMes.EditValue) == 0)
                {
                    gridColumn9.FieldName = "";
                    
                }
                else
                {
                    gridColumn9.FieldName = "SaldoLibro";
                }
                lstMovimientos = new BTesoreria().ListarLibroBancos(Parametros.intEjercicio, 
                    Convert.ToInt32(lkpMes.EditValue), 
                    Convert.ToInt32(lkpCuenta.EditValue));

                if (Convert.ToInt32(lkpMes.EditValue) == 0)
                    lstMovimientos.ForEach(x => { x.Dia = 0; });
                
                dgrLibroBancos.DataSource = lstMovimientos;

                if (lstMovimientos.Count > 0)
                {
                    decimal dmlSaldoLibro = (from p in lstMovimientos
                                             select p.SaldoLibro).Last();

                    txtSaldoAnterior.EditValue = lstMovimientos[0].SaldoAnterior;
                    txtSaldoDisponible.EditValue = lstMovimientos[0].SaldoDisponible;
                    txtSaldoLibro.EditValue = dmlSaldoLibro;
                    txtRegistros.EditValue = lstMovimientos.Count;
                }
                else
                {
                    if (lkpMes.EditValue != null)
                    {
                        List<ELibroBancos> ListaSaldoAnterior = new List<ELibroBancos>();
                        List<ELibroBancos> ListaSaldoDisponible = new List<ELibroBancos>();

                        ListaSaldoAnterior = new BTesoreria().ListarLibroBancosSaldoAnterior(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue), Convert.ToInt32(lkpCuenta.EditValue));
                        ListaSaldoDisponible = new BTesoreria().ListarLibroBancosSaldoDisponible(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue), Convert.ToInt32(lkpCuenta.EditValue));

                        if (ListaSaldoAnterior.Count > 0)
                        {
                            txtSaldoAnterior.EditValue = ListaSaldoAnterior[0].SaldoAnterior;
                            if (ListaSaldoDisponible.Count > 0)
                                txtSaldoDisponible.EditValue = ListaSaldoDisponible[0].SaldoDisponible;
                            else
                                txtSaldoDisponible.EditValue = 0;
                            txtSaldoLibro.EditValue = ListaSaldoAnterior[0].SaldoAnterior;
                            txtRegistros.EditValue = 0;
                        }
                        else
                        {
                            txtSaldoAnterior.EditValue = 0;
                            txtSaldoDisponible.EditValue = 0;
                            txtSaldoLibro.EditValue = 0;
                            txtRegistros.EditValue = 0;
                        }
                    }
                }
                viewLibroBancos.Focus();
            }               
            catch (Exception ex)
            {                
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Modify(int cab_correlativo)
        {           
            Carga();

            int index = lstMovimientos.FindIndex(x => x.icod_correlativo == cab_correlativo);
                viewLibroBancos.FocusedRowHandle = index;            
            
        }       

      
        #endregion

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
            if (Obe == null)
                return;
            try
            {
                #region esto es cuando se imprime del voucher ya registrado en los comprobantes contables
                //var EVoucherCab = new BContabilidad().getVoucherContableCab("BANCOS", Obe.icod_correlativo);
                //if (EVoucherCab.Count == 0)
                //    throw new ArgumentException("No existe comprobante contable generado para este movimiento");

                //var lst = new BContabilidad().listarVoucherContableDet(EVoucherCab[0].vcocc_icod_vcontable);
                //imp(Obe, EVoucherCab[0], lst);
                #endregion
                #region se genera un voucher temporal para la impresión
                if (Obe.iid_situacion_movimiento_banco == 2)
                    throw new ArgumentException("Acción no permitida , debido a que el documento está ANULADO");
                //////////if (Convert.ToInt32(Obe.vcocc_iid_voucher_contable) > 0)
                //////////    imprimirDetalle(Obe, Convert.ToInt32(Obe.vcocc_iid_voucher_contable));

                if (Obe.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorPagar || Obe.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorCobrar)
                {
                    var lst = new BTesoreria().ListarEntidadFinancieraDetalle(Obe.icod_correlativo);
                    CrearVoucherContableBancosCtaPor_Pagar_Cobrar(Obe, lst, Obe.iid_motivo_mov_banco);
                }
                else if (Obe.iid_motivo_mov_banco == Parametros.intMotivoVarios)
                {
                    var lst = new BTesoreria().ListarEntidadFinancieraDetalle(Obe.icod_correlativo);
                    CrearVoucherContableBancosVarios(Obe, lst);
                }
                else if (Obe.iid_motivo_mov_banco == Parametros.intMotivoTransferenciaCuentas)
                {
                    if (Obe.cflag_tipo_movimiento == "1")
                        throw new ArgumentException("La impresión debe realizarse desde la CTA. BANCARIA que genera el movimiento de transferencia");
                    crearVoucherTransferencia(Obe);
                }
                #endregion
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }            
        }
        private void imprimirDetalle(ELibroBancos Obe,int idVoucher)
        {
            //var lstComprobante = new BComprobantes().ListarComprobantesUnitario(idVoucher);
            //List<EComprobanteDetalle> lista = new BComprobantes().ListarComprobanteDetalle(idVoucher);
            //lista.ForEach(x =>
            //{
            //    if (x.iid_tipo_relacion != null)
            //    {
            //        x.vdes_Analisis = string.Format("{0:00}", x.iid_tipo_relacion) + "." + x.viid_relacion;
            //    }
            //});
            //rtpComprobanteBanco1 reporte = new rtpComprobanteBanco1();
            //reporte.cargar(Obe, lstComprobante[0], lista.Where(x=> 
            //    Convert.ToInt32(x.ctacc_iid_cuenta_contable_ref) == 0).ToList(), lkpMes.Text, lkpBanco.Text, lkpCuenta.Text);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            generarVoucher();
        }
        private void generarVoucher()
        {            
            int contador = 0;
            lstMovimientos.ForEach(x =>
            {
                if (x.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorPagar)
                {
                    contador += 1;
                    lblnro.Text = contador.ToString();
                    modificarMovimiento(x, true);
                }
            });

        }

        private void paseDeSaldosDeBancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("El siguiente Proceso generará el Pase de los Saldos de las Cuentas Bancarias.\n\t\t\t\t\t\t\t\t\t\t¿Desea Continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    var lstAnio = new BContabilidad().listarAnioEjercicio();
                    if (lstAnio.Where(x => x.anioc_icod_anio_ejercicio == Parametros.intEjercicio + 1).ToList().Count == 0)
                        throw new ArgumentException("No se encontro Año de Ejercicio siguiente registrado");

                    if (lstAnio.Where(x => x.anioc_icod_anio_ejercicio == Parametros.intEjercicio + 1).ToList().
                        Where(z=> z.anioc_iactivo == true).ToList().Count == 0)
                        throw new ArgumentException("El Año de Ejercicio siguiente no se encuentra activo");                   

                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void mnuLibroBancos_Opening(object sender, CancelEventArgs e)
        {
            ELibroBancos Obe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
            if (Obe != null)
            {
                if (Obe.cflag_pase)
                {
                    modificarToolStripMenuItem.Enabled = false;
                    anulartoolStripMenuItem.Enabled = false;
                    imprimirToolStripMenuItem.Enabled = false;
                }
                else
                {
                    modificarToolStripMenuItem.Enabled = true;
                    anulartoolStripMenuItem.Enabled = true;
                    imprimirToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void modificar()
        {
            ELibroBancos oBe = (ELibroBancos)viewLibroBancos.GetRow(viewLibroBancos.FocusedRowHandle);
            if (oBe == null)
                return;
            if (oBe.mobac_origen_regitro == "PLN")
            {
                XtraMessageBox.Show("El registro ha sido generado desde un Planilla de Venta, no puede ser modificado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                view();
            }
            else
                modificarMovimiento(oBe, false);
            
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void barSubItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Link == null)
                OpenSubItem(e.Item);
        }

        void OpenSubItem(BarItem item)
        {
            if (!(item is BarSubItem) || item.Links.Count == 0) return;
            BarSelectionInfo info;
            info = item.Manager.InternalGetService(typeof(BarSelectionInfo)) as BarSelectionInfo;
            if (info != null)
                info.PressLink(item.Links[0]);
        }

        private void btnCtaPorPagar_ItemClick(object sender, ItemClickEventArgs e)
        {
            ctaPorPagar();
        }

        private void btnCtaPorCobrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            ctaPorCobrar();
        }

        private void btnAdelantoProveedor_ItemClick(object sender, ItemClickEventArgs e)
        {
            adelantoProveedor();
        }

        private void btnAdelantoCliente_ItemClick(object sender, ItemClickEventArgs e)
        {
            adelantoCliente();
        }

        private void btnVarios_ItemClick(object sender, ItemClickEventArgs e)
        {
            ingresoVarios();
        }

        private void btnPagoAdeNCCliente_ItemClick(object sender, ItemClickEventArgs e)
        {
            pagoAdelantoNotaCreditoCliente();
        }

        private void btnPagoAdeNCProveedor_ItemClick(object sender, ItemClickEventArgs e)
        {
            pagoAdelantoNotaCreditoProveedor();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            modificar();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            eliminar();
        }

        private void transferencaEntreCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transferenciaBancos();
        }

        private void transferenciaBancos()
        {
            frmManteTransferencia frm = new frmManteTransferencia();
            frm.MiEvento += new frmManteTransferencia.DelegadoMensaje(Modify);
            frm.intBanco = Convert.ToInt32(lkpBanco.EditValue);
            frm.intCuenta = Convert.ToInt32(lkpCuenta.EditValue);
            frm.intMes = Convert.ToInt32(lkpMes.EditValue);
            frm.SetInsert();
            frm.Show(); 
        }

        private void egresosVariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            egresoVarios();
        }
        private void imp(ELibroBancos Obe, EVoucherContableCab oBe, List<EVoucherContableDet> lst)
        {
            rtpComprobanteBanco1 reporte = new rtpComprobanteBanco1();
            reporte.cargar(Obe, oBe, lst.Where(x =>
                Convert.ToInt32(x.ctacc_iid_cuenta_contable_ref) == 0).ToList(), lkpMes.Text, lkpBanco.Text, lkpCuenta.Text);
        }

        private void CrearVoucherContableBancosCtaPor_Pagar_Cobrar(ELibroBancos obj_LibroBanco, List<ELibroBancosDetalle> lstMovBancoDet, int motivo)
        {
            try
            {
                #region ...
                if (obj_LibroBanco.iid_mes == 0)
                    return;
                var lstParamentros = new BContabilidad().listarParametroContable();//(new ContabilidadData()).ListarParametroContable();
                var mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
                #region cabecera del voucher
                EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                obj_CompCab.anioc_iid_anio = obj_LibroBanco.iid_anio;
                obj_CompCab.mesec_iid_mes = obj_LibroBanco.iid_mes;
                obj_CompCab.vcocc_fecha_vcontable = Convert.ToDateTime(obj_LibroBanco.dfecha_movimiento);
                obj_CompCab.vcocc_glosa = obj_LibroBanco.vglosa;
                obj_CompCab.vcocc_observacion = obj_LibroBanco.vglosa;
                obj_CompCab.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                obj_CompCab.tablc_iid_moneda = obj_LibroBanco.iid_tipo_moneda;
                obj_CompCab.intUsuario = obj_LibroBanco.iusuario_crea;
                obj_CompCab.strPc = obj_LibroBanco.vpc_crea;
                //obj_CompCab.cestado = 'A';
                obj_CompCab.vcocc_tipo_cambio = obj_LibroBanco.nmonto_tipo_cambio;
                obj_CompCab.tbl_origen = "BANCOS";
                obj_CompCab.tbl_origen_icod = obj_LibroBanco.icod_correlativo;
                if (motivo == Parametros.intMotivoCuentasPorCobrar)
                {
                    obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_ingbco);
                }
                if (motivo == Parametros.intMotivoCuentasPorPagar)
                {
                    obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_egrbco);
                }


                if (Convert.ToDecimal(obj_LibroBanco.nmonto_tipo_cambio) <= 0)
                {
                    throw new ArgumentException("Tipo de cambio no válido para la generación del voucher contable");
                }
                #endregion
                //  CREANDO VOUCHER DETALLE
                EBancoCuenta obj_BancoCuenta = new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBanco.EditValue)).Where(x => x.bcod_icod_banco_cuenta == obj_LibroBanco.icod_enti_financiera_cuenta).ToList()[0];

                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();

                var lstDocumento = new BAdministracionSistema().listarTipoDocumento();
                ETipoDocumento obj_Documento = new ETipoDocumento();

                //(HABER/DEBE) DE LA CABECERA
                decimal mto_sol = 0;
                decimal mto_dol = 0;
                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(obj_LibroBanco.ii_tipo_doc);
                /*/*/
                obj_Documento = lstDocumento.Where(d => d.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0];
                obj_item_CompDet_01.vcocd_numero_doc = obj_LibroBanco.vnro_documento;
                /*/*/
                if (Convert.ToInt32(obj_BancoCuenta.ctacc_icod_cuenta_contable) == 0)
                    throw new ArgumentException("No se ha registrado un número de CTA. CONTABLE para la cuenta bancaria");

                obj_item_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_BancoCuenta.ctacc_icod_cuenta_contable);
                obj_item_CompDet_01.strNroCuenta = obj_BancoCuenta.strCodCtaContable;
                var Lista = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                });
                obj_item_CompDet_01.tablc_iid_tipo_analitica = 1;
                obj_item_CompDet_01.anad_icod_analitica = obj_BancoCuenta.anad_icod_analitica;
                obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", 1, obj_BancoCuenta.strCodAnalitica);
                obj_item_CompDet_01.vcocd_vglosa_linea = obj_LibroBanco.vglosa;
                if (motivo == Parametros.intMotivoCuentasPorPagar)
                {
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = (obj_LibroBanco.iid_tipo_moneda == 1) ? obj_LibroBanco.nmonto_movimiento : Math.Round(obj_LibroBanco.nmonto_movimiento * Convert.ToDecimal(obj_LibroBanco.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = (obj_LibroBanco.iid_tipo_moneda == 2) ? obj_LibroBanco.nmonto_movimiento : Math.Round(obj_LibroBanco.nmonto_movimiento / Convert.ToDecimal(obj_LibroBanco.nmonto_tipo_cambio), 2);
                    mto_sol = Convert.ToDecimal(obj_item_CompDet_01.vcocd_nmto_tot_haber_sol);
                    mto_dol = Convert.ToDecimal(obj_item_CompDet_01.vcocd_nmto_tot_haber_dol);
                }
                if (motivo == Parametros.intMotivoCuentasPorCobrar)
                {
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = (obj_LibroBanco.iid_tipo_moneda == 1) ? obj_LibroBanco.nmonto_movimiento : Math.Round(obj_LibroBanco.nmonto_movimiento * Convert.ToDecimal(obj_LibroBanco.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = (obj_LibroBanco.iid_tipo_moneda == 2) ? obj_LibroBanco.nmonto_movimiento : Math.Round(obj_LibroBanco.nmonto_movimiento / Convert.ToDecimal(obj_LibroBanco.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                    mto_sol = Convert.ToDecimal(obj_item_CompDet_01.vcocd_nmto_tot_debe_sol);
                    mto_dol = Convert.ToDecimal(obj_item_CompDet_01.vcocd_nmto_tot_debe_dol);
                }
                obj_item_CompDet_01.intTipoOperacion = 1;
                //obj_item_CompDet_01.cestado = 'A';
                obj_item_CompDet_01.vcocd_tipo_cambio = obj_LibroBanco.nmonto_tipo_cambio;
                lstCompDetalle.Add(obj_item_CompDet_01);
                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                    lstCompDetalle = AddCuentaAutomatica(obj_item_CompDet_01, lstCompDetalle, mto_sol, mto_dol, mlistCuenta);
                #endregion
                #region detalle n
                lstMovBancoDet.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet = new EVoucherContableDet();

                    if (x.iid_cuenta_contable > 0)
                    {
                        obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                        obj_item_CompDet.tdocc_icod_tipo_doc = 56;//ID DOC VCO
                        //*/
                        obj_Documento = lstDocumento.Where(d => d.tdocc_icod_tipo_doc == obj_item_CompDet.tdocc_icod_tipo_doc).ToList()[0];
                        obj_item_CompDet.vcocd_numero_doc = obj_LibroBanco.vnro_documento;
                        /**/
                        obj_item_CompDet.ctacc_icod_cuenta_contable = Convert.ToInt32(x.iid_cuenta_contable);
                        obj_item_CompDet.strNroCuenta = x.NumeroCuentaContable;
                        Lista = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        });
                        obj_item_CompDet.cecoc_icod_centro_costo = x.icod_centro_costo;
                        obj_item_CompDet.tablc_iid_tipo_analitica = x.tablc_icod_tipo_analitica;
                        obj_item_CompDet.anad_icod_analitica = x.icod_analitica;
                        obj_item_CompDet.vcocd_vglosa_linea = x.vglosa;
                        obj_item_CompDet.intTipoOperacion = 1;
                        //obj_item_CompDet.cestado = 'A';
                        obj_item_CompDet.vcocd_tipo_cambio = obj_LibroBanco.nmonto_tipo_cambio;
                    }
                    else
                    {

                        obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                        obj_item_CompDet.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                        obj_Documento = lstDocumento.Where(d => d.tdocc_icod_tipo_doc == obj_item_CompDet.tdocc_icod_tipo_doc).ToList()[0];

                        var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.tdocc_icod_tipo_doc));

                        if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList().Count == 0)
                            throw new ArgumentException("SGE - Clase de Documento no válida");
                        ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList()[0];

                        obj_item_CompDet.vcocd_numero_doc = x.vnumero_doc;

                        if (motivo == Parametros.intMotivoCuentasPorCobrar)
                        {
                            if (obj_LibroBanco.iid_tipo_moneda == 1)
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException("No se encontró cuenta contable NACIONAL para la generación del voucher contable");

                            if (obj_LibroBanco.iid_tipo_moneda == 2)
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException("No se encontró cuenta contable EXTRANJERA para la generación del voucher contable");
                            obj_item_CompDet.ctacc_icod_cuenta_contable = (obj_LibroBanco.iid_tipo_moneda == 1) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                        }
                        if (motivo == Parametros.intMotivoCuentasPorPagar)
                        {
                            if (obj_LibroBanco.iid_tipo_moneda == 1)
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                    throw new ArgumentException("No se encontró cuenta contable NACIONAL para la generación del voucher contable");

                            if (obj_LibroBanco.iid_tipo_moneda == 2)
                                if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                    throw new ArgumentException("No se encontró cuenta contable EXTRANJERA para la generación del voucher contable");
                            obj_item_CompDet.ctacc_icod_cuenta_contable = (obj_LibroBanco.iid_tipo_moneda == 1) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                        }

                        //**//                                             
                        Lista = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_item_CompDet.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        });
                        //**//
                        obj_item_CompDet.cecoc_icod_centro_costo = null;
                        obj_item_CompDet.tablc_iid_tipo_analitica = x.tablc_icod_tipo_analitica;
                        obj_item_CompDet.anad_icod_analitica = x.icod_analitica;
                        obj_item_CompDet.strAnalisis = x.vdes_analisis;
                        obj_item_CompDet.vcocd_vglosa_linea = x.vglosa;
                        obj_item_CompDet.intTipoOperacion = 1;
                        //obj_item_CompDet.cestado = 'A';
                        obj_item_CompDet.vcocd_tipo_cambio = obj_LibroBanco.nmonto_tipo_cambio;
                    }

                    if (x.mto_mov_soles < 0 || x.mto_mov_dolar < 0)
                    {
                        if (motivo == Parametros.intMotivoCuentasPorPagar)
                        {
                            obj_item_CompDet.vcocd_nmto_tot_debe_sol = 0;
                            obj_item_CompDet.vcocd_nmto_tot_haber_sol = x.mto_mov_soles * -1;
                            obj_item_CompDet.vcocd_nmto_tot_debe_dol = 0;
                            obj_item_CompDet.vcocd_nmto_tot_haber_dol = x.mto_mov_dolar * -1;
                            if (x.mto_retenido_soles < 0)
                                obj_item_CompDet.vcocd_nmto_tot_haber_sol = obj_item_CompDet.vcocd_nmto_tot_haber_sol + (x.mto_retenido_soles * -1);
                            else
                                obj_item_CompDet.vcocd_nmto_tot_haber_sol = obj_item_CompDet.vcocd_nmto_tot_haber_sol + x.mto_retenido_soles;

                            if (x.mto_retenido_soles < 0)
                                obj_item_CompDet.vcocd_nmto_tot_haber_dol = obj_item_CompDet.vcocd_nmto_tot_haber_dol + (x.mto_retenido_dolar * -1);
                            else
                                obj_item_CompDet.subdi_banc_nmto_tot_haber_sol = obj_item_CompDet.vcocd_nmto_tot_haber_dol + x.mto_retenido_dolar;

                            mto_sol = Convert.ToDecimal(obj_item_CompDet.vcocd_nmto_tot_haber_sol);
                            mto_dol = Convert.ToDecimal(obj_item_CompDet.vcocd_nmto_tot_haber_dol);
                        }
                        if (motivo == Parametros.intMotivoCuentasPorCobrar)
                        {
                            obj_item_CompDet.vcocd_nmto_tot_debe_sol = x.mto_mov_soles * -1;
                            obj_item_CompDet.vcocd_nmto_tot_haber_sol = 0;
                            obj_item_CompDet.vcocd_nmto_tot_debe_dol = x.mto_mov_dolar * -1;
                            obj_item_CompDet.vcocd_nmto_tot_haber_dol = 0;
                            mto_sol = Convert.ToDecimal(obj_item_CompDet.vcocd_nmto_tot_debe_sol);
                            mto_dol = Convert.ToDecimal(obj_item_CompDet.vcocd_nmto_tot_debe_dol);
                        }
                    }
                    else
                    {
                        if (motivo == Parametros.intMotivoCuentasPorPagar)
                        {
                            obj_item_CompDet.vcocd_nmto_tot_debe_sol = x.mto_mov_soles + x.mto_retenido_soles;
                            obj_item_CompDet.vcocd_nmto_tot_haber_sol = 0;
                            obj_item_CompDet.vcocd_nmto_tot_debe_dol = x.mto_mov_dolar + x.mto_retenido_dolar;
                            obj_item_CompDet.vcocd_nmto_tot_haber_dol = 0;
                            mto_sol = Convert.ToDecimal(obj_item_CompDet.vcocd_nmto_tot_debe_sol);
                            mto_dol = Convert.ToDecimal(obj_item_CompDet.vcocd_nmto_tot_debe_dol);
                        }
                        if (motivo == Parametros.intMotivoCuentasPorCobrar)
                        {
                            obj_item_CompDet.vcocd_nmto_tot_debe_sol = 0;
                            obj_item_CompDet.vcocd_nmto_tot_haber_sol = x.mto_mov_soles;
                            obj_item_CompDet.vcocd_nmto_tot_debe_dol = 0;
                            obj_item_CompDet.vcocd_nmto_tot_haber_dol = x.mto_mov_dolar;
                            mto_sol = Convert.ToDecimal(obj_item_CompDet.vcocd_nmto_tot_haber_sol);
                            mto_dol = Convert.ToDecimal(obj_item_CompDet.vcocd_nmto_tot_haber_dol);
                        }
                    }

                    lstCompDetalle.Add(obj_item_CompDet);
                    if (obj_item_CompDet.ctacc_icod_cuenta_debe_auto != null)
                        lstCompDetalle = AddCuentaAutomatica(obj_item_CompDet, lstCompDetalle, mto_sol, mto_dol, mlistCuenta);
                });

                if (motivo == Parametros.intMotivoCuentasPorPagar)
                {
                    lstMovBancoDet.ForEach(x =>
                    {

                        if (Convert.ToInt32(x.iid_cuenta_contable) == 0 && x.mto_retenido_soles > 0)
                        {
                            EVoucherContableDet obj_item_CompDet_R = new EVoucherContableDet();
                           
                            obj_item_CompDet_R.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                            obj_item_CompDet_R.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                            obj_Documento = lstDocumento.Where(d => d.tdocc_icod_tipo_doc == obj_item_CompDet_R.tdocc_icod_tipo_doc).ToList()[0];
                            var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.tdocc_icod_tipo_doc));
                            ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.tdodc_iid_correlativo).ToList()[0];
                            obj_item_CompDet_R.vcocd_numero_doc = x.vnumero_doc;
                            if (Convert.ToInt32(lstParamentros[0].parac_id_cta_retencion) == 0)
                                throw new ArgumentException("No existe Cuenta Contable para la Retención : Parámetros Contables");
                            obj_item_CompDet_R.ctacc_icod_cuenta_contable = Convert.ToInt32(lstParamentros[0].parac_id_cta_retencion);
                            /******************************************************************************************/
                            obj_item_CompDet_R.strNroCuenta = lstParamentros[0].parac_id_cta_retencion.ToString();
                            /******************************************************************************************/
                            Lista = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_R.ctacc_icod_cuenta_contable).ToList();
                            Lista.ForEach(Obe =>
                            {
                                obj_item_CompDet_R.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                                obj_item_CompDet_R.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            });
                            //**//
                            obj_item_CompDet_R.cecoc_icod_centro_costo = null;
                            obj_item_CompDet_R.tablc_iid_tipo_analitica = x.tablc_icod_tipo_analitica;
                            obj_item_CompDet_R.anad_icod_analitica = x.icod_analitica;
                            obj_item_CompDet_R.vcocd_vglosa_linea = "RETENCIÓN DEL IGV";
                            obj_item_CompDet_R.vcocd_nmto_tot_debe_sol = 0;
                            obj_item_CompDet_R.vcocd_nmto_tot_haber_sol = x.mto_retenido_soles;
                            obj_item_CompDet_R.vcocd_nmto_tot_debe_dol = 0;
                            obj_item_CompDet_R.vcocd_nmto_tot_haber_dol = x.mto_retenido_dolar;
                            obj_item_CompDet_R.intTipoOperacion = 1;
                            //obj_item_CompDet_R.cestado = 'A';
                            obj_item_CompDet_R.vcocd_tipo_cambio = obj_LibroBanco.nmonto_tipo_cambio;
                            lstCompDetalle.Add(obj_item_CompDet_R);
                            if (obj_item_CompDet_R.ctacc_icod_cuenta_debe_auto != null)
                                lstCompDetalle = AddCuentaAutomatica(obj_item_CompDet_R, lstCompDetalle, Convert.ToDecimal(obj_item_CompDet_R.vcocd_nmto_tot_haber_sol),
                                                 Convert.ToDecimal(obj_item_CompDet_R.vcocd_nmto_tot_haber_dol), mlistCuenta);
                        }
                    });
                }
                #endregion
                #region totales y situación del voucher
                obj_CompCab.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                obj_CompCab.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                obj_CompCab.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                obj_CompCab.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));

                if (lstCompDetalle.Count > 0)
                {
                    if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_haber_sol &&
                        obj_CompCab.vcocc_nmto_tot_debe_dol == obj_CompCab.vcocc_nmto_tot_haber_dol)
                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = 1;
                    else
                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = 2;
                }
                else
                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = 4;
                #endregion
                
                imp(obj_LibroBanco, obj_CompCab, lstCompDetalle);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private List<EVoucherContableDet> AddCuentaAutomatica(EVoucherContableDet oBe_Orig, List<EVoucherContableDet> lstCompDetalle,
           decimal monto_sol, decimal monto_dol, List<ECuentaContable> mlistCuenta)
        {
            /*
             * ESTE METODO DE AÑADIR LA CUENTA AUTOMATICA YA EST ACTUALIZADO.
             * CONSISTE EN UBICAR EL MONTO DE LA CTA. AUTOMATICA(DEBE O HABER) SEGUN LA CUENTA ORIGEN
             * EDGAR ALFARO.
             */
            for (int x = 0; x < 2; x++)
            {
                EVoucherContableDet oBe_Auto = new EVoucherContableDet();
                oBe_Auto.vcocc_icod_vcontable = oBe_Orig.vcocc_icod_vcontable;
                oBe_Auto.tdocc_icod_tipo_doc = oBe_Orig.tdocc_icod_tipo_doc;
                oBe_Auto.strTipoDoc = oBe_Orig.strTipoDoc;
                oBe_Auto.vcocd_numero_doc = oBe_Orig.vcocd_numero_doc;

                if (x == 0)
                {
                    oBe_Auto.vcocd_nro_item_det = oBe_Orig.vcocd_nro_item_det + 1;
                    oBe_Auto.strDesCuenta = oBe_Orig.ctacc_icod_cuenta_debe_auto.ToString();
                    oBe_Auto.ctacc_icod_cuenta_contable = Convert.ToInt32(oBe_Orig.ctacc_icod_cuenta_debe_auto);
                    /*-------------------------------------------------------------------------------------------*/
                    var cta = mlistCuenta.Where(a => a.ctacc_icod_cuenta_contable == oBe_Auto.ctacc_icod_cuenta_contable).ToList();
                    if (cta.Count == 0)
                    {
                        throw new ArgumentException(String.Format("Cuenta contable automática {0} no figura en los registros de SUBCUENTAS para la generación del voucher contable ", oBe_Orig.ctacc_icod_cuenta_debe_auto));
                    }
                    if (Convert.ToInt32(cta[0].tablc_iid_tipo_analitica) > 0)
                    {
                        oBe_Auto.tablc_iid_tipo_analitica = oBe_Orig.tablc_iid_tipo_analitica;
                        oBe_Auto.anad_icod_analitica = oBe_Orig.anad_icod_analitica;
                    }
                    if (Convert.ToInt32(cta[0].ctacc_iccosto) == 1)
                        oBe_Auto.cecoc_icod_centro_costo = oBe_Orig.cecoc_icod_centro_costo;
                    /*-------------------------------------------------------------------------------------------*/

                    if (Convert.ToDecimal(oBe_Orig.vcocd_nmto_tot_debe_sol) > 0)
                    {
                        oBe_Auto.vcocd_nmto_tot_debe_sol = monto_sol;
                        oBe_Auto.vcocd_nmto_tot_debe_dol = monto_dol;
                    }
                    else
                    {
                        oBe_Auto.vcocd_nmto_tot_haber_sol = monto_sol;
                        oBe_Auto.vcocd_nmto_tot_haber_dol = monto_dol;
                    }
                }
                if (x == 1)
                {
                    oBe_Auto.vcocd_nro_item_det = oBe_Orig.vcocd_nro_item_det + 2;
                    oBe_Auto.strDesCuenta = oBe_Orig.ctacc_icod_cuenta_haber_auto.ToString();
                    oBe_Auto.ctacc_icod_cuenta_contable = Convert.ToInt32(oBe_Orig.ctacc_icod_cuenta_haber_auto);
                    /*-------------------------------------------------------------------------------------------*/
                    var cta = mlistCuenta.Where(a => a.ctacc_icod_cuenta_contable == oBe_Auto.ctacc_icod_cuenta_contable).ToList();
                    if (cta.Count == 0)
                    {
                        throw new ArgumentException(String.Format("Cuenta contable automática {0} no figura en los registros de SUBCUENTAS para la generación del voucher contable ", oBe_Orig.ctacc_icod_cuenta_debe_auto));
                    }
                    if (Convert.ToInt32(cta[0].tablc_iid_tipo_analitica) > 0)
                    {
                        oBe_Auto.tablc_iid_tipo_analitica = oBe_Orig.tablc_iid_tipo_analitica;
                        oBe_Auto.anad_icod_analitica = oBe_Orig.anad_icod_analitica;
                    }
                    if (Convert.ToInt32(cta[0].ctacc_iccosto) == 1)
                        oBe_Auto.cecoc_icod_centro_costo = oBe_Orig.cecoc_icod_centro_costo;
                    /*-------------------------------------------------------------------------------------------*/
                    if (Convert.ToDecimal(oBe_Orig.vcocd_nmto_tot_debe_sol) > 0)
                    {
                        oBe_Auto.vcocd_nmto_tot_haber_sol = monto_sol;
                        oBe_Auto.vcocd_nmto_tot_haber_dol = monto_dol;

                    }
                    else
                    {
                        oBe_Auto.vcocd_nmto_tot_debe_sol = monto_sol;
                        oBe_Auto.vcocd_nmto_tot_debe_dol = monto_dol;
                    }
                }
                oBe_Auto.vcocd_vglosa_linea = oBe_Orig.vcocd_vglosa_linea;
                oBe_Auto.strTipoAnalitica = "";
                //oBe_Auto.cestado = 'A';
                oBe_Auto.intTipoOperacion = 1;
                oBe_Auto.ctacc_iid_cuenta_contable_ref = oBe_Orig.ctacc_icod_cuenta_contable;
                oBe_Auto.vcocd_tipo_cambio = oBe_Orig.vcocd_tipo_cambio;
                lstCompDetalle.Add(oBe_Auto);
            }
            return lstCompDetalle;
        }
        private void crearVoucherTransferencia(ELibroBancos oBeLB)
        {
            try
            {
                //EBancoCuenta oBeBC,EBancoCuenta oBeBCTransf
                if (oBeLB.iid_mes == 0)
                    return;
                var lstParamentros = new BContabilidad().listarParametroContable();
                var lstPlanCuentas = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                #region cabecera
                EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                obj_CompCab.anioc_iid_anio = oBeLB.iid_anio;
                obj_CompCab.mesec_iid_mes = oBeLB.iid_mes;
                obj_CompCab.vcocc_fecha_vcontable = Convert.ToDateTime(oBeLB.dfecha_movimiento);
                obj_CompCab.vcocc_glosa = oBeLB.vglosa;
                obj_CompCab.vcocc_observacion = oBeLB.vglosa;
                obj_CompCab.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                obj_CompCab.tablc_iid_moneda = oBeLB.iid_tipo_moneda;
                obj_CompCab.intUsuario = oBeLB.iusuario_crea;
                obj_CompCab.strPc = oBeLB.vpc_crea;
                //obj_CompCab.cestado = 'A';
                obj_CompCab.vcocc_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                obj_CompCab.tbl_origen = "BANCOS";
                obj_CompCab.tbl_origen_icod = oBeLB.icod_correlativo;
                obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_egrbco);

                if (Convert.ToDecimal(oBeLB.nmonto_tipo_cambio) <= 0)
                {
                    throw new ArgumentException("Tipo de cambio no válido para la generación del voucher contable");
                }
                #endregion
                int intTranfCtaBancaria = new BTesoreria().getBancoCuenta(Convert.ToInt32(oBeLB.id_transferencia));
                var oBeBC = new BTesoreria().listarBancoCuentas(null).Where(z => z.bcod_icod_banco_cuenta == oBeLB.icod_enti_financiera_cuenta).ToList()[0];
                var oBeBCTransf = new BTesoreria().listarBancoCuentas(null).Where(z => z.bcod_icod_banco_cuenta == intTranfCtaBancaria).ToList()[0];
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();

                #region detalle 01
                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                //obj_item_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(oBeLB.ii_tipo_doc);
                obj_item_CompDet_01.vcocd_numero_doc = oBeLB.vnro_documento;
                obj_item_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, oBeLB.vnro_documento);

                if (Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable) == 0)
                    throw new ArgumentException(String.Format("No se ha registrado un número de CTA. CONTABLE para la cuenta bancaria:{0} <<{1}>>", oBeBC.strBanco, oBeBC.bcod_vnumero_cuenta));

                obj_item_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeBC.ctacc_icod_cuenta_contable);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_item_CompDet_01.anad_icod_analitica = oBeBC.anad_icod_analitica;
                        obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", 1, oBeBC.strCodAnalitica);
                    }
                });

                obj_item_CompDet_01.vcocd_vglosa_linea = oBeLB.vglosa;
                obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = (oBeLB.iid_tipo_moneda == 1) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = (oBeLB.iid_tipo_moneda == 2) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);

                obj_item_CompDet_01.intTipoOperacion = 1;
                obj_item_CompDet_01.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;
                lstCompDetalle.Add(obj_item_CompDet_01);


                //if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                //{
                //    var tuple = addCtaAutomatica(obj_item_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                //    lstCompDetalle = tuple.Item1;
                //    lstDetGeneral = tuple.Item2;
                //}
                #endregion
                #region detalle 02
                EVoucherContableDet obj_item_CompDet_02 = new EVoucherContableDet();
                //obj_item_CompDet_02.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_item_CompDet_02.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_02.tdocc_icod_tipo_doc = Convert.ToInt32(oBeLB.ii_tipo_doc);
                obj_item_CompDet_02.vcocd_numero_doc = oBeLB.vnro_documento;
                obj_item_CompDet_02.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(x => x.tdocc_icod_tipo_doc == obj_item_CompDet_02.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, oBeLB.vnro_documento);

                if (Convert.ToInt32(oBeBCTransf.ctacc_icod_cuenta_contable) == 0)
                    throw new ArgumentException(String.Format("No se ha registrado un número de CTA. CONTABLE para la cuenta bancaria:{0} <<{1}>>", oBeBCTransf.strBanco, oBeBCTransf.bcod_vnumero_cuenta));

                obj_item_CompDet_02.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeBCTransf.ctacc_icod_cuenta_contable);

                Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_02.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_02.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_02.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_item_CompDet_02.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaBancos;
                        obj_item_CompDet_02.anad_icod_analitica = oBeBCTransf.anad_icod_analitica;
                        obj_item_CompDet_02.strAnalisis = String.Format("{0:00}.{1}", 1, oBeBCTransf.strCodAnalitica);
                    }

                });

                obj_item_CompDet_02.vcocd_vglosa_linea = oBeLB.vglosa;
                obj_item_CompDet_02.vcocd_nmto_tot_debe_sol = (oBeLB.iid_tipo_moneda == 1) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento * Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_sol = 0;
                obj_item_CompDet_02.vcocd_nmto_tot_debe_dol = (oBeLB.iid_tipo_moneda == 2) ? oBeLB.nmonto_movimiento : Math.Round(oBeLB.nmonto_movimiento / Convert.ToDecimal(oBeLB.nmonto_tipo_cambio), 2);
                obj_item_CompDet_02.vcocd_nmto_tot_haber_dol = 0;
                obj_item_CompDet_02.intTipoOperacion = 1;
                obj_item_CompDet_02.vcocd_tipo_cambio = oBeLB.nmonto_tipo_cambio;

                lstCompDetalle.Add(obj_item_CompDet_02);
                //lstDetGeneral.Add(obj_item_CompDet_02);/***********************************************************/

                //if (obj_item_CompDet_02.ctacc_icod_cuenta_debe_auto != null)
                //{
                //    var tuple = addCtaAutomatica(obj_item_CompDet_02, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                //    lstCompDetalle = tuple.Item1;
                //    lstDetGeneral = tuple.Item2;
                //}
                #endregion
                #region totales
                obj_CompCab.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                obj_CompCab.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                obj_CompCab.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                obj_CompCab.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));

                if (lstCompDetalle.Count > 0)
                {
                    if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_haber_sol &&
                        obj_CompCab.vcocc_nmto_tot_debe_dol == obj_CompCab.vcocc_nmto_tot_haber_dol)
                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = 1;
                    else
                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = 2;
                }
                else
                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = 4;

                imp(oBeLB, obj_CompCab, lstCompDetalle);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CrearVoucherContableBancosVarios(ELibroBancos obj_LibroBanco, List<ELibroBancosDetalle> lstMovBancoDet)
        {
            try
            {
                if (obj_LibroBanco.iid_mes == 0)
                    return;
                var lstParamentros = new BContabilidad().listarParametroContable();
                var mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
                //CREANDO VOUCHER CABECERA
                EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                obj_CompCab.anioc_iid_anio = obj_LibroBanco.iid_anio;
                obj_CompCab.mesec_iid_mes = obj_LibroBanco.iid_mes;
                obj_CompCab.vcocc_fecha_vcontable = Convert.ToDateTime(obj_LibroBanco.dfecha_movimiento);
                obj_CompCab.vcocc_glosa = obj_LibroBanco.vglosa;
                obj_CompCab.vcocc_observacion = obj_LibroBanco.vglosa;
                obj_CompCab.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                obj_CompCab.tablc_iid_moneda = obj_LibroBanco.iid_tipo_moneda;
                obj_CompCab.intUsuario = obj_LibroBanco.iusuario_crea;
                obj_CompCab.strPc = obj_LibroBanco.vpc_crea;
                //obj_CompCab.cestado = 'A';
                obj_CompCab.vcocc_tipo_cambio = obj_LibroBanco.nmonto_tipo_cambio;
                obj_CompCab.tbl_origen = "BANCOS";
                obj_CompCab.tbl_origen_icod = obj_LibroBanco.icod_correlativo;

                if (obj_LibroBanco.cflag_tipo_movimiento == Parametros.intTipoMovimientoAbono)
                {
                    obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_ingbco);
                }
                if (obj_LibroBanco.cflag_tipo_movimiento == Parametros.intTipoMovimientoCargo)
                {
                    obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_egrbco);
                }
                if (Convert.ToDecimal(obj_LibroBanco.nmonto_tipo_cambio) <= 0)
                {
                    throw new ArgumentException("Tipo de cambio no válido para la generación del voucher contable");
                }
                //CREANDO EL DETALLE DEL VOUCHER
                EBancoCuenta obj_BancoCuenta = new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBanco.EditValue)).Where(x => x.bcod_icod_banco_cuenta == obj_LibroBanco.icod_enti_financiera_cuenta).ToList()[0];
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                //

                //(DEBE/HABER) DE LA CABECERA
                decimal mto_sol = 0;
                decimal mto_dol = 0;

                EVoucherContableDet obj_item_CompDet_01 = new EVoucherContableDet();
                obj_item_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_item_CompDet_01.tdocc_icod_tipo_doc = Convert.ToInt32(obj_LibroBanco.ii_tipo_doc);
                var lstDocumento = new BAdministracionSistema().listarTipoDocumento();
                ETipoDocumento obj_Documento = lstDocumento.Where(d => d.tdocc_icod_tipo_doc == obj_item_CompDet_01.tdocc_icod_tipo_doc).ToList()[0];
                obj_item_CompDet_01.vcocd_numero_doc = obj_LibroBanco.vnro_documento;

                if (Convert.ToInt32(obj_BancoCuenta.ctacc_icod_cuenta_contable) == 0)
                    throw new ArgumentException("No se ha registrado un número de CTA. CONTABLE para la cuenta bancaria");
                obj_item_CompDet_01.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_BancoCuenta.ctacc_icod_cuenta_contable);
                obj_item_CompDet_01.strNroCuenta = obj_BancoCuenta.strCodCtaContable;
                var Lista = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_item_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                });
                obj_item_CompDet_01.tablc_iid_tipo_analitica = 1;
                obj_item_CompDet_01.anad_icod_analitica = obj_BancoCuenta.anad_icod_analitica;
                obj_item_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", 1, obj_BancoCuenta.strCodAnalitica);
                obj_item_CompDet_01.vcocd_vglosa_linea = obj_LibroBanco.vglosa;
                //
                if (obj_LibroBanco.cflag_tipo_movimiento == Parametros.intTipoMovimientoAbono)
                {
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = (obj_LibroBanco.iid_tipo_moneda == 1) ? obj_LibroBanco.nmonto_movimiento : Math.Round(obj_LibroBanco.nmonto_movimiento * Convert.ToDecimal(obj_LibroBanco.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = (obj_LibroBanco.iid_tipo_moneda == 2) ? obj_LibroBanco.nmonto_movimiento : Math.Round(obj_LibroBanco.nmonto_movimiento / Convert.ToDecimal(obj_LibroBanco.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                    mto_sol = Convert.ToDecimal(obj_item_CompDet_01.vcocd_nmto_tot_debe_sol);
                    mto_dol = Convert.ToDecimal(obj_item_CompDet_01.vcocd_nmto_tot_debe_dol);
                }
                if (obj_LibroBanco.cflag_tipo_movimiento == Parametros.intTipoMovimientoCargo)
                {
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_sol = (obj_LibroBanco.iid_tipo_moneda == 1) ? obj_LibroBanco.nmonto_movimiento : Math.Round(obj_LibroBanco.nmonto_movimiento * Convert.ToDecimal(obj_LibroBanco.nmonto_tipo_cambio), 2);
                    obj_item_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                    obj_item_CompDet_01.vcocd_nmto_tot_haber_dol = (obj_LibroBanco.iid_tipo_moneda == 2) ? obj_LibroBanco.nmonto_movimiento : Math.Round(obj_LibroBanco.nmonto_movimiento / Convert.ToDecimal(obj_LibroBanco.nmonto_tipo_cambio), 2);
                    mto_sol = Convert.ToDecimal(obj_item_CompDet_01.vcocd_nmto_tot_haber_sol);
                    mto_dol = Convert.ToDecimal(obj_item_CompDet_01.vcocd_nmto_tot_haber_dol);
                }
                //
                obj_item_CompDet_01.intTipoOperacion = 1;
                //obj_item_CompDet_01.cestado = 'A';
                obj_item_CompDet_01.vcocd_tipo_cambio = obj_LibroBanco.nmonto_tipo_cambio;
                lstCompDetalle.Add(obj_item_CompDet_01);
                if (obj_item_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                    lstCompDetalle = AddCuentaAutomatica(obj_item_CompDet_01, lstCompDetalle, mto_sol, mto_dol, mlistCuenta);

                //(DEBE/HABER DEL DETALLE)
                lstMovBancoDet.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet = new EVoucherContableDet();
                    obj_item_CompDet.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    obj_item_CompDet.tdocc_icod_tipo_doc = 56;//ID DOC VCO
                    obj_item_CompDet.vcocd_numero_doc = obj_LibroBanco.vnro_documento;
                    obj_item_CompDet.ctacc_icod_cuenta_contable = Convert.ToInt32(x.iid_cuenta_contable);
                    Lista = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet.ctacc_icod_cuenta_contable).ToList();
                    Lista.ForEach(Obe =>
                    {
                        obj_item_CompDet.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_item_CompDet.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                        obj_item_CompDet.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    });
                    obj_item_CompDet.cecoc_icod_centro_costo = x.icod_centro_costo;
                    obj_item_CompDet.tablc_iid_tipo_analitica = x.tablc_icod_tipo_analitica;
                    obj_item_CompDet.anad_icod_analitica = x.icod_analitica;
                    obj_item_CompDet.strAnalisis = x.vdes_analisis;
                    obj_item_CompDet.vcocd_vglosa_linea = x.vglosa;
                    obj_item_CompDet.intTipoOperacion = 1;
                    //obj_item_CompDet.cestado = 'A';
                    obj_item_CompDet.vcocd_tipo_cambio = obj_LibroBanco.nmonto_tipo_cambio;
                    //
                    if (obj_LibroBanco.cflag_tipo_movimiento == Parametros.intTipoMovimientoAbono)
                    {
                        obj_item_CompDet.vcocd_nmto_tot_debe_sol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_haber_sol = x.mto_mov_soles;
                        obj_item_CompDet.vcocd_nmto_tot_debe_dol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_haber_dol = Math.Round((x.mto_mov_soles / obj_LibroBanco.nmonto_tipo_cambio), 2);
                        mto_sol = Convert.ToDecimal(obj_item_CompDet.vcocd_nmto_tot_haber_sol);
                        mto_dol = Convert.ToDecimal(obj_item_CompDet.vcocd_nmto_tot_haber_dol);
                    }
                    if (obj_LibroBanco.cflag_tipo_movimiento == Parametros.intTipoMovimientoCargo)
                    {
                        obj_item_CompDet.vcocd_nmto_tot_debe_sol = x.mto_mov_soles;
                        obj_item_CompDet.vcocd_nmto_tot_haber_sol = 0;
                        obj_item_CompDet.vcocd_nmto_tot_debe_dol = Math.Round((x.mto_mov_soles / obj_LibroBanco.nmonto_tipo_cambio), 2);
                        obj_item_CompDet.vcocd_nmto_tot_haber_dol = 0;
                        mto_sol = Convert.ToDecimal(obj_item_CompDet.vcocd_nmto_tot_debe_sol);
                        mto_dol = Convert.ToDecimal(obj_item_CompDet.vcocd_nmto_tot_debe_dol);
                    }
                    //                       

                    lstCompDetalle.Add(obj_item_CompDet);
                    if (obj_item_CompDet.ctacc_icod_cuenta_debe_auto != null)
                        lstCompDetalle = AddCuentaAutomatica(obj_item_CompDet, lstCompDetalle, mto_sol, mto_dol, mlistCuenta);
                });
                //
                //TOTALES DE LA CABECERA DEL VOUCHER                
                obj_CompCab.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                obj_CompCab.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                obj_CompCab.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                obj_CompCab.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));

                if (lstCompDetalle.Count > 0)
                {
                    if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_haber_sol &&
                        obj_CompCab.vcocc_nmto_tot_debe_dol == obj_CompCab.vcocc_nmto_tot_haber_dol)
                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = 1;
                    else
                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = 2;
                }
                else
                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = 4;

                imp(obj_LibroBanco, obj_CompCab, lstCompDetalle);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}