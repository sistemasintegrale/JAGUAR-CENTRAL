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
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class FrmManteAdelantoClientes : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteMovimientoVarios));
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        public delegate void DelegadoMensaje(int cab_correl);
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public BAdministracionSistema objBtipoDoc = new BAdministracionSistema();
        private BTesoreria Obl;
        public int IdCorrelativoCabecera = 0;
        public int Correlative = 0;
        public int IdAdelantoCliente = 0;
        public Int64 IdDocumentoPorCobrar = 0;
        public int IdMotivoMovimientoBanco = 0;
        public int IdSituacion = 0;
        public int mes;
        public bool flag_conciliacion = false;
        public int vcocc_iid_voucher_contable;
        public int intIcodModulo = 0;
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        #endregion

        #region "Eventos"

        public FrmManteAdelantoClientes()
        {
            InitializeComponent();
        }

        private void dtmFecha_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                if (Convert.ToDateTime(dtmFecha.EditValue).Month == Convert.ToInt32(lblMes.Text) && Convert.ToDateTime(dtmFecha.EditValue).Year == Parametros.intEjercicio)
                {
                    var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtmFecha.EditValue).ToShortDateString()).ToList();
                    if (Lista.Count > 0)
                    {
                        Lista.ForEach(obe =>
                        {
                            txtTipoDeCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                        });
                    }
                    else
                        txtTipoDeCambio.Text = "0.0000";
                }
                else
                {
                    XtraMessageBox.Show("La fecha seleccionada no está dentro del mes o año de ejercicio", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CheckFecha();
                    dtmFecha.Focus();
                }
            }
        }
        private void CheckFecha()
        {
            if (Convert.ToInt32(lblMes.Text) == DateTime.Now.Month && Parametros.intEjercicio == DateTime.Now.Year)
                dtmFecha.EditValue = DateTime.Now;
            else
                dtmFecha.EditValue =
                    DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(Convert.ToInt32(lblMes.Text) - 1);
        }
        private void btnCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                lista_clientes();
            }
        }
        private void lista_clientes()
        {
            using (FrmListarCliente frmCliente = new FrmListarCliente())
            {
                if (frmCliente.ShowDialog() == DialogResult.OK)
                {
                    btnCliente.Text = frmCliente._Be.cliec_vnombre_cliente;
                    btnCliente.Tag = frmCliente._Be.cliec_icod_cliente;
                    txtBeneficia.Text = frmCliente._Be.cliec_vnombre_cliente;
                }
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            this.SetSave();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        #endregion

        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtBeneficia.Enabled = !Enabled;
            txtGlosa.Enabled = !Enabled;
            txtMonto.Enabled = !Enabled;
            txtNumeroDeDocumento.Enabled = !Enabled;
            txtTipoDeCambio.Enabled = Enabled;
            lkpTipoDocumento.Enabled = !Enabled;
            txtTipoDeCambio.Enabled = !Enabled;
            dtmFecha.Enabled = !Enabled;
            chkConciliacion.Enabled = !Enabled;
            btnCliente.Enabled = !Enabled;
            txtNumeroAdelanto.Enabled = !Enabled;
            txtObservacion.Enabled = !Enabled;
            txtNumeroDeDocumento.Focus();
            if (Status == BSMaintenanceStatus.View)
                btnGuardar.Enabled = false;
            if (Status != BSMaintenanceStatus.CreateNew)
                this.EstablecerDatos();
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpTipoDocumento.Enabled = false;
                btnCliente.Enabled = false;
                txtNumeroDeDocumento.Enabled = false;
            }
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            lkpTipoDocumento.ItemIndex = 0;
            CheckFecha();
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
            {
                BaseEdit oBase = null;
                Boolean Flag = true;
                ELibroBancos oBe = new ELibroBancos();
                Obl = new BTesoreria();
                try
                {
                    if (btnCliente.Tag == null)
                    {
                        oBase = btnCliente;
                        throw new ArgumentException("Seleccionar el cliente.");
                    }
                    if (string.IsNullOrEmpty(txtGlosa.Text))
                    {
                        oBase = txtGlosa;
                        throw new ArgumentException("Ingrese Glosa");
                    }
                    if (txtBeneficia.Text.ToString().Trim() == "")
                    {
                        oBase = txtBeneficia;
                        throw new ArgumentException("Ingresar Beneficiario");
                    }
                    if (Convert.ToInt32(btnCliente.Tag) == 0)
                    {
                        oBase = btnCliente;
                        throw new ArgumentException("Ingresar Cliente");
                    }
                    if (txtNumeroDeDocumento.Text.ToString().Trim() == "")
                    {
                        oBase = txtNumeroDeDocumento;
                        throw new ArgumentException("Ingresar el Número de Doc");
                    }
                    if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                    {
                        oBase = dtmFecha;
                        throw new ArgumentException("No existe Tipo de Cambio, para la fecha selecciona");
                    }
                    if (Convert.ToDecimal(txtMonto.Text) <= 0)
                    {
                        oBase = txtMonto;
                        throw new ArgumentException("El monto no puede ser 0 o menor que 0");
                    }

                    decimal dmlMonto = 0, dmlTipoCambio = 0;
                    decimal.TryParse(txtMonto.Text, out dmlMonto);
                    decimal.TryParse(txtTipoDeCambio.Text, out dmlTipoCambio);

                    //Datos Entidad Financiera Movimiento
                    oBe.icod_correlativo = IdCorrelativoCabecera;
                    oBe.iid_correlativo = Correlative;
                    oBe.iid_anio = Parametros.intEjercicio;
                    oBe.iid_mes = Convert.ToInt32(lblMes.Text);
                    oBe.icod_enti_financiera_cuenta = Convert.ToInt32(lblCuenta.Text);
                    oBe.ii_tipo_doc = Convert.ToInt32(lkpTipoDocumento.EditValue);
                    oBe.vdescripcion_beneficiario = txtBeneficia.Text;
                    oBe.iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                    oBe.nmonto_tipo_cambio = dmlTipoCambio;
                    oBe.nmonto_movimiento = dmlMonto;
                    oBe.nmonto_saldo_banco = 0;
                    oBe.iid_situacion_movimiento_banco = Convert.ToInt32(lkpSituacion.EditValue);
                    oBe.dfecha_movimiento = Convert.ToDateTime(dtmFecha.Text);
                    oBe.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                    oBe.vnro_documento = txtNumeroDeDocumento.Text;
                    oBe.cflag_conciliacion = flag_conciliacion;
                    oBe.iusuario_crea = Valores.intUsuario;
                    oBe.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                    oBe.vglosa = txtGlosa.Text;
                    oBe.iid_motivo_mov_banco = IdMotivoMovimientoBanco;
                    oBe.mobac_flag_estado = true;
                    oBe.inumero_orden = txtNumeroAdelanto.Text;
                    oBe.cliec_icod_cliente = Convert.ToInt32(btnCliente.Tag);
                    oBe.TipoDocumento = lkpTipoDocumento.Text;

                    //Datos Adelanto Cliente
                    EAdelantoCliente objE_AdelantoCliente = new EAdelantoCliente();
                    objE_AdelantoCliente.icod_correlativo = IdAdelantoCliente;
                    objE_AdelantoCliente.icod_correlativo_cabecera = IdCorrelativoCabecera;
                    objE_AdelantoCliente.icod_cliente = Convert.ToInt32(btnCliente.Tag);
                    objE_AdelantoCliente.iid_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                    //objE_AdelantoCliente.vnumero_documento = txtNumeroDeDocumento.Text;
                    objE_AdelantoCliente.sfecha_adelanto = Convert.ToDateTime(dtmFecha.Text);
                    objE_AdelantoCliente.iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                    objE_AdelantoCliente.nmonto_tipo_cambio = dmlTipoCambio;
                    objE_AdelantoCliente.nmonto_adelanto = dmlMonto;
                    objE_AdelantoCliente.nmonto_pagado = 0;
                    objE_AdelantoCliente.vobservacion = txtObservacion.Text;
                    objE_AdelantoCliente.nsituacion_adelanto_cliente = Parametros.intSitClienteGenerado;
                    objE_AdelantoCliente.iusuario_crea = Valores.intUsuario;
                    objE_AdelantoCliente.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                    objE_AdelantoCliente.flag_estado = true;

                    //Datos Documento Por Cobrar
                    EDocXCobrar obj_DXC = new EDocXCobrar();
                    obj_DXC.doxcc_icod_correlativo = IdDocumentoPorCobrar;
                    obj_DXC.anio = Parametros.intEjercicio;
                    obj_DXC.mesec_iid_mes = Convert.ToInt16(lblMes.Text);
                    obj_DXC.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                    obj_DXC.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoCliente;
                    obj_DXC.doxcc_sfecha_doc = Convert.ToDateTime(dtmFecha.Text);
                    obj_DXC.cliec_icod_cliente = Convert.ToInt32(btnCliente.Tag);
                    obj_DXC.cliec_vnombre_cliente = btnCliente.Text;
                    obj_DXC.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                    obj_DXC.tablc_iid_tipo_pago = 1;
                    obj_DXC.doxcc_nmonto_tipo_cambio = dmlTipoCambio;
                    obj_DXC.doxcc_vdescrip_transaccion = txtGlosa.Text;
                    obj_DXC.doxcc_nmonto_afecto = 0;
                    obj_DXC.doxcc_nmonto_inafecto = 0;
                    obj_DXC.doxcc_nporcentaje_igv = 0;
                    obj_DXC.doxcc_nmonto_impuesto = 0;
                    obj_DXC.doxcc_nmonto_total = dmlMonto;
                    obj_DXC.doxcc_nmonto_saldo = dmlMonto;
                    obj_DXC.doxcc_nmonto_pagado = 0;
                    obj_DXC.doxcc_sfecha_vencimiento_doc = DateTime.Now;
                    obj_DXC.tablc_iid_situacion_documento = Parametros.intSitDocCobrarGenerado;
                    obj_DXC.doxcc_vobservaciones = txtObservacion.Text;
                    obj_DXC.doxc_bind_cuenta_corriente = false;
                    obj_DXC.doxcc_sfecha_entrega = null;
                    obj_DXC.doxcc_bind_impresion_nogerencia = false;
                    obj_DXC.doxc_bind_situacion_legal = false;
                    obj_DXC.doxc_bind_cierre_cuenta_corriente = false;
                    obj_DXC.intUsuario = Valores.intUsuario;
                    obj_DXC.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                    obj_DXC.doxcc_tipo_comprobante_referencia = 0;
                    obj_DXC.doxcc_num_serie_referencia = "";
                    obj_DXC.doxcc_num_comprobante_referencia = "";
                    obj_DXC.doxcc_sfecha_emision_referencia = null;
                    // obj_DXC.docxc_icod_documento = IdAdelantoCliente;
                    obj_DXC.doxcc_flag_estado = true;
                    obj_DXC.doxcc_origen = "B";


                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        BCuentasPorCobrar oob = new BCuentasPorCobrar();
                        List<EAdelantoCliente> ListaAdelantosCliente = oob.ListarAdelantoClietneXAñoTodos(Parametros.intEjercicio);
                        if (ListaAdelantosCliente.Count == 0)
                        {
                            objE_AdelantoCliente.vnumero_adelanto = Parametros.intEjercicio.ToString().Remove(0, 1) + "000001";
                            obj_DXC.doxcc_vnumero_doc = objE_AdelantoCliente.vnumero_adelanto;
                            objE_AdelantoCliente.vnumero_documento = oBe.vnro_documento;
                            oBe.inumero_orden = objE_AdelantoCliente.vnumero_adelanto;
                        }
                        else
                        {
                            objE_AdelantoCliente.vnumero_adelanto = Parametros.intEjercicio.ToString().Remove(0, 1) + string.Format("{0:000000}", (Convert.ToInt32(ListaAdelantosCliente.Max(max => max.vnumero_adelanto).Remove(0, 3)) + 1));
                            obj_DXC.doxcc_vnumero_doc = objE_AdelantoCliente.vnumero_adelanto;
                            objE_AdelantoCliente.vnumero_documento = oBe.vnro_documento;
                            oBe.inumero_orden = objE_AdelantoCliente.vnumero_adelanto;
                        }

                        IdCorrelativoCabecera = Obl.insertarLibroBancos(oBe, null, null, null, objE_AdelantoCliente, obj_DXC, null);

                        ActualizarCorrelativo();
                    }
                    else
                    {
                        if (Parametros.intSitClienteGenerado == Convert.ToInt32(IdSituacion))
                        {
                            //Datos Entidad Financiera Movimiento
                            oBe.iusuario_modifica = Valores.intUsuario;
                            oBe.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();

                            //Datos Adelanto Cliente
                            objE_AdelantoCliente.vnumero_adelanto = txtNumeroAdelanto.Text;
                            objE_AdelantoCliente.iusuario_modifica = Valores.intUsuario;
                            objE_AdelantoCliente.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();

                            ////Datos Documento Por Pagar
                            obj_DXC.doxcc_vnumero_doc = txtNumeroAdelanto.Text;
                            obj_DXC.intUsuario = Valores.intUsuario;
                            obj_DXC.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                            Obl.actualizarLibroBancos(oBe, null, null, null, objE_AdelantoCliente, obj_DXC, null);
                        }
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
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Flag = false;
                }
                finally
                {
                    if (Flag)
                    {
                        this.MiEvento(IdCorrelativoCabecera);
                        this.Close();
                    }
                }
            }
        }

        private void CargaLista()
        {
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(36), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            /**/
            var lstTipoDocContabilidad = new BAdministracionSistema().listarTipoDocumentoPorModulo(intIcodModulo);
            BSControls.LoaderLook(lkpTipoDocumento, lstTipoDocContabilidad, "tdocc_vdescripcion", "tdocc_icod_tipo_doc", false);
            /**/
        }

        private void EstablecerDatos()
        {
            BCuentasPorCobrar objBL_AdelantoCliente = new BCuentasPorCobrar();
            EAdelantoCliente objE_AdelantoCliente = new EAdelantoCliente();

            objE_AdelantoCliente = objBL_AdelantoCliente.ListarAdelantoCliente(IdCorrelativoCabecera);
            IdSituacion = objE_AdelantoCliente.nsituacion_adelanto_cliente;
            IdDocumentoPorCobrar = objE_AdelantoCliente.doxcc_icod_correlativo;
        }

        public void DisableModify()
        {
            if (IdSituacion != Parametros.intSitClienteGenerado)
            {
                if (IdSituacion == Parametros.intSitClientePagadoParcial)
                {
                    SetCancel();
                    XtraMessageBox.Show("El movimiento no puede ser modificado debido a su situación : PAGADO PARCIAL", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (IdSituacion == Parametros.intSitClienteCancelado)
                {
                    SetCancel();
                    XtraMessageBox.Show("El movimiento no puede ser modificado debido a su situación : CANCELADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        #endregion

        private void FrmManteAdelantoClientes_Load(object sender, EventArgs e)
        {
            CargaLista();
        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelarr_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                lista_clientes();
            }
        }

        private void lkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            GetCorrelativo();
        }
        private void GetCorrelativo()
        {
            string nro_correlativo = "";
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtNumeroDeDocumento.Text = string.Empty;
                var lst = objBtipoDoc.GetCorrelativoDocumentoBancos(Parametros.intEjercicio, Convert.ToInt32(lblMes.Text), Convert.ToInt32(lblCuenta.Text), Convert.ToInt32(lkpTipoDocumento.EditValue));

                if (lst != null)
                {
                    nro_correlativo = lst;
                    int nro;
                    bool bNum = int.TryParse(nro_correlativo, out nro);
                    if (bNum)
                        txtNumeroDeDocumento.Text = (nro_correlativo == null) ? "" : String.Format("{0:000000}", Convert.ToInt32(nro_correlativo) + 1);
                }
            }
        }
        private void ActualizarCorrelativo()
        {
            objBtipoDoc.ActualizarCorrelativoDocumentoBancos(Parametros.intEjercicio, Convert.ToInt32(lblMes.Text), Convert.ToInt32(lblCuenta.Text), Convert.ToInt32(lkpTipoDocumento.EditValue), txtNumeroDeDocumento.Text);
        }

    }
}