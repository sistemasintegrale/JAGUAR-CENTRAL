using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class FrmManteAdelantoProveedor : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteMovimientoVarios));
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        public delegate void DelegadoMensaje(int cab_correl);
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BTesoreria Obl;
        public int IdCorrelativoCabecera = 0;
        public int Correlative = 0;
        public int IdAdelantoProveedor = 0;
        public Int64 IdDocumentoPorPagar = 0;
        public int IdMotivoMovimientoBanco = 0;
        public int IdSituacion = 0;
        public BAdministracionSistema objBtipoDoc = new BAdministracionSistema();
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

        #region Eventos

        public FrmManteAdelantoProveedor()
        {
            InitializeComponent();
        }

        private void FrmManteAdelantoProveedor_Load(object sender, EventArgs e)
        {

        }

        private void dtmFecha_EditValueChanged(object sender, EventArgs e)
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
        private void CheckFecha()
        {
            if (Convert.ToInt32(lblMes.Text) == DateTime.Now.Month && Parametros.intEjercicio == DateTime.Now.Year)
                dtmFecha.EditValue = DateTime.Now;
            else
                dtmFecha.EditValue =
                    DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(Convert.ToInt32(lblMes.Text) - 1);
        }
        private void btnProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            lista_proveedores();
        }
        private void lista_proveedores()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                btnProveedor.Tag = Proveedor._Be.iid_icod_proveedor;
                btnProveedor.Text = Proveedor._Be.vnombrecompleto;
                txtBeneficia.Text = Proveedor._Be.vnombrecompleto;
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

        #region Metodos

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
            btnProveedor.Enabled = !Enabled;
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
                btnProveedor.Enabled = false;
                txtNumeroDeDocumento.Enabled = false;
            }
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            //lkpTipoDocumento.ItemIndex = 0;
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
                    if (btnProveedor.Tag == null)
                    {
                        oBase = btnProveedor;
                        throw new ArgumentException("Seleccionar el proveedor.");
                    }
                    if (string.IsNullOrEmpty(txtNumeroDeDocumento.Text))
                    {
                        oBase = txtNumeroDeDocumento;
                        throw new ArgumentException("Ingrese Nro. de Documento");
                    }
                    if (string.IsNullOrEmpty(txtGlosa.Text))
                    {
                        oBase = txtGlosa;
                        throw new ArgumentException("Ingrese Glosa");
                    }
                    if (string.IsNullOrEmpty(txtBeneficia.Text))
                    {
                        oBase = txtBeneficia;
                        throw new ArgumentException("Ingrese Beneficiario");
                    }

                    if (dtFechaDiferida.Enabled)
                    {
                        if (dtFechaDiferida.EditValue == null)
                        {
                            oBase = dtFechaDiferida;
                            throw new ArgumentException("Ingrese la fecha diferida del documento");
                        }
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
                    oBe.cflag_tipo_movimiento = Parametros.intTipoMovimientoCargo;
                    oBe.vnro_documento = txtNumeroDeDocumento.Text;
                    oBe.cflag_conciliacion = flag_conciliacion;
                    oBe.iusuario_crea = Valores.intUsuario;
                    oBe.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                    oBe.vglosa = txtGlosa.Text;
                    oBe.iid_motivo_mov_banco = IdMotivoMovimientoBanco;
                    oBe.mobac_flag_estado = true;
                    oBe.inumero_orden = txtNumeroAdelanto.Text;
                    oBe.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
                    oBe.TipoDocumento = lkpTipoDocumento.Text;



                    /**/
                    DateTime? dtNullVal = null;
                    oBe.mobac_sfecha_diferida = (dtFechaDiferida.Enabled) ? Convert.ToDateTime(dtFechaDiferida.EditValue) : dtNullVal;


                    //Datos Adelanto Proveedor
                    EAdelantoProveedor objE_AdelantoProveedor = new EAdelantoProveedor();
                    objE_AdelantoProveedor.icod_correlativo = IdAdelantoProveedor;
                    objE_AdelantoProveedor.icod_correlativo_cabecera = IdCorrelativoCabecera;
                    objE_AdelantoProveedor.icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
                    objE_AdelantoProveedor.iid_tipo_doc = Parametros.intTipoDocAdelantoProveedor;
                    objE_AdelantoProveedor.vnumero_documento = txtNumeroDeDocumento.Text;
                    objE_AdelantoProveedor.sfecha_adelanto = Convert.ToDateTime(dtmFecha.Text);
                    objE_AdelantoProveedor.iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                    objE_AdelantoProveedor.nmonto_tipo_cambio = dmlTipoCambio;
                    objE_AdelantoProveedor.nmonto_adelanto = dmlMonto;
                    objE_AdelantoProveedor.nmonto_canjeado = 0;
                    objE_AdelantoProveedor.vobservacion = txtObservacion.Text;
                    objE_AdelantoProveedor.nsituacion_adelanto_proveedor = Parametros.intSitProveedorGenerado;
                    objE_AdelantoProveedor.iusuario_crea = Valores.intUsuario;
                    objE_AdelantoProveedor.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                    objE_AdelantoProveedor.flag_estado = true;

                    //Datos Documento Por Pagar
                    EDocPorPagar objE_DocPorPagar = new EDocPorPagar();
                    objE_DocPorPagar.doxpc_icod_correlativo = IdDocumentoPorPagar;
                    objE_DocPorPagar.anio = Parametros.intEjercicio;
                    objE_DocPorPagar.mesec_iid_mes = Convert.ToInt32(lblMes.Text);
                    objE_DocPorPagar.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoProveedor;
                    objE_DocPorPagar.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoProveedor;
                    objE_DocPorPagar.doxpc_sfecha_doc = Convert.ToDateTime(dtmFecha.Text);
                    objE_DocPorPagar.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
                    objE_DocPorPagar.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                    objE_DocPorPagar.doxpc_nmonto_tipo_cambio = dmlTipoCambio;
                    objE_DocPorPagar.doxpc_vdescrip_transaccion = txtGlosa.Text;
                    objE_DocPorPagar.doxpc_nmonto_destino_gravado = 0;
                    objE_DocPorPagar.doxpc_nmonto_destino_mixto = 0;
                    objE_DocPorPagar.doxpc_nmonto_destino_nogravado = dmlMonto;
                    objE_DocPorPagar.doxpc_nmonto_referencial_cif = 0;
                    objE_DocPorPagar.doxpc_nmonto_servicio_no_domic = 0;
                    objE_DocPorPagar.doxpc_nmonto_imp_destino_gravado = 0;
                    objE_DocPorPagar.doxpc_nmonto_imp_destino_mixto = 0;
                    objE_DocPorPagar.doxpc_nmonto_imp_destino_nogravado = 0;
                    objE_DocPorPagar.doxpc_nmonto_retencion_rh = 0;
                    objE_DocPorPagar.doxpc_nmonto_total_documento = dmlMonto;
                    objE_DocPorPagar.doxpc_nmonto_retenido = 0;
                    objE_DocPorPagar.doxpc_nmonto_total_pagado = 0;
                    objE_DocPorPagar.doxpc_nmonto_total_saldo = dmlMonto;
                    objE_DocPorPagar.doxpc_nporcentaje_igv = 0;
                    objE_DocPorPagar.doxpc_nporcentaje_imp_renta = 0;
                    objE_DocPorPagar.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objE_DocPorPagar.doxpc_nmonto_nogravado = 0;

                    objE_DocPorPagar.doxpc_tipo_comprobante_referencia = Convert.ToInt32(lkpTipoDocumento.EditValue);
                    objE_DocPorPagar.doxpc_num_serie_referencia = txtNumeroDeDocumento.Text;
                    objE_DocPorPagar.doxpc_num_comprobante_referencia = txtNumeroDeDocumento.Text;
                    objE_DocPorPagar.doxpc_sfecha_emision_referencia = null;

                    objE_DocPorPagar.doxpc_nporcentaje_isc = 0;
                    objE_DocPorPagar.doxpc_nmonto_isc = 0;
                    objE_DocPorPagar.doxpc_vnro_deposito_detraccion = null;
                    objE_DocPorPagar.doxpc_sfec_deposito_detraccion = null;
                    objE_DocPorPagar.intUsuario = Valores.intUsuario;
                    objE_DocPorPagar.strPc = WindowsIdentity.GetCurrent().Name;
                    objE_DocPorPagar.doxpc_iid_correlativo = 0;
                    objE_DocPorPagar.doxpc_origen = null;
                    objE_DocPorPagar.doxpc_flag_estado = true;
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        BCuentasPorPagar bp = new BCuentasPorPagar();
                        List<EAdelantoProveedor> ListaAdelantosProveedor = bp.ListarAdelantoProveedoresCorrelativo(Parametros.intEjercicio);
                        if (ListaAdelantosProveedor.Count == 0)
                        {
                            objE_AdelantoProveedor.vnumero_adelanto = Parametros.intEjercicio.ToString().Remove(0, 1) + "000001";
                            objE_DocPorPagar.doxpc_vnumero_doc = objE_AdelantoProveedor.vnumero_adelanto;
                            oBe.inumero_orden = objE_AdelantoProveedor.vnumero_adelanto;
                            objE_AdelantoProveedor.vnumero_documento = oBe.vnro_documento;
                        }
                        else
                        {
                            objE_AdelantoProveedor.vnumero_adelanto = Parametros.intEjercicio.ToString().Remove(0, 1) + string.Format("{0:000000}", (Convert.ToInt32(ListaAdelantosProveedor.Max(max => max.vnumero_adelanto).Remove(0, 3)) + 1));
                            objE_DocPorPagar.doxpc_vnumero_doc = objE_AdelantoProveedor.vnumero_adelanto;
                            oBe.inumero_orden = objE_AdelantoProveedor.vnumero_adelanto;
                            objE_AdelantoProveedor.vnumero_documento = oBe.vnro_documento;
                        }

                        IdCorrelativoCabecera = Obl.insertarLibroBancos(oBe, null, objE_AdelantoProveedor, objE_DocPorPagar, null, null, null);

                        ActualizarCorrelativo();
                    }
                    else
                    {
                        if (IdSituacion == Parametros.intSitProveedorGenerado)
                        {
                            //Datos Entidad Financiera Movimiento
                            oBe.iusuario_modifica = Valores.intUsuario;
                            oBe.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();

                            //Datos Adelanto Proveedor
                            objE_AdelantoProveedor.vnumero_adelanto = txtNumeroAdelanto.Text;
                            objE_AdelantoProveedor.iusuario_modifica = Valores.intUsuario;
                            objE_AdelantoProveedor.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();

                            ////Datos Documento Por Pagar
                            objE_DocPorPagar.doxpc_vnumero_doc = txtNumeroAdelanto.Text;
                            objE_DocPorPagar.intUsuario = Valores.intUsuario;
                            objE_DocPorPagar.strPc = WindowsIdentity.GetCurrent().Name;
                            Obl.actualizarLibroBancos(oBe, null, objE_AdelantoProveedor, objE_DocPorPagar, null, null, null);
                        }
                        else
                        {
                            XtraMessageBox.Show("El Adelanto Proveedor no puede ser Modificado, por que ya existen pagos asociados", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        void EventoKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
            if (e.KeyChar == (char)Keys.Enter)
                this.btnGrabar_Click(sender, e);
        }

        public void CargaLista()
        {
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(36), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            /**/
            var lstTipoDocTesoreria = new BAdministracionSistema().listarTipoDocumentoPorModulo(intIcodModulo);
            BSControls.LoaderLook(lkpTipoDocumento, lstTipoDocTesoreria, "tdocc_vdescripcion", "tdocc_icod_tipo_doc", false);
            /**/
        }

        private void EstablecerDatos()
        {
            BCuentasPorPagar objBL_AdelantoProveedor = new BCuentasPorPagar();
            EAdelantoProveedor objE_AdelantoProveedor = new EAdelantoProveedor();

            objE_AdelantoProveedor = objBL_AdelantoProveedor.ListarAdelantoProveedor(IdCorrelativoCabecera);

            lkpTipoDocumento.EditValue = objE_AdelantoProveedor.ii_tipo_doc;
            txtNumeroDeDocumento.Text = objE_AdelantoProveedor.vnro_documento;
            txtTipoDeCambio.Text = objE_AdelantoProveedor.nmonto_tipo_cambio.ToString();
            lkpMoneda.EditValue = objE_AdelantoProveedor.iid_tipo_moneda;
            txtGlosa.Text = objE_AdelantoProveedor.vglosa;
            dtmFecha.EditValue = objE_AdelantoProveedor.sfecha_adelanto;
            txtBeneficia.Text = objE_AdelantoProveedor.vdescripcion_beneficiario;
            txtMonto.EditValue = objE_AdelantoProveedor.nmonto_movimiento;
            chkConciliacion.Checked = objE_AdelantoProveedor.cflag_conciliacion;
            lkpSituacion.EditValue = objE_AdelantoProveedor.iid_situacion_movimiento_banco;

            IdAdelantoProveedor = objE_AdelantoProveedor.icod_correlativo;
            btnProveedor.Tag = objE_AdelantoProveedor.icod_proveedor;
            btnProveedor.Text = objE_AdelantoProveedor.proc_vnombrecompleto;
            txtNumeroAdelanto.Text = objE_AdelantoProveedor.vnumero_adelanto;
            txtObservacion.Text = objE_AdelantoProveedor.vobservacion;
            IdSituacion = objE_AdelantoProveedor.nsituacion_adelanto_proveedor;
            IdDocumentoPorPagar = Convert.ToInt64(objE_AdelantoProveedor.doxpc_icod_correlativo);
        }
        public void DisableModify()
        {
            if (IdSituacion != Parametros.intSitClienteGenerado)
            {
                if (IdSituacion == Parametros.intSitProveedorPagadoParcial)
                {
                    SetCancel();
                    XtraMessageBox.Show("El movimiento no puede ser modificado debido a su situación : PAGADO PARCIAL", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else if (IdSituacion == Parametros.intSitProveedorCancelado)
                {
                    SetCancel();
                    XtraMessageBox.Show("El movimiento no puede ser modificado debido a su situación : CANCELADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
        }

        #endregion

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelarr_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
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

                if (lkpTipoDocumento.Text == "CHEQUE")
                    dtFechaDiferida.Enabled = true;
                else
                {
                    dtFechaDiferida.Enabled = false;
                    dtFechaDiferida.Text = String.Empty;
                    dtFechaDiferida.EditValue = null;
                }
            }
        }
        private void ActualizarCorrelativo()
        {
            objBtipoDoc.ActualizarCorrelativoDocumentoBancos(Parametros.intEjercicio, Convert.ToInt32(lblMes.Text), Convert.ToInt32(lblCuenta.Text), Convert.ToInt32(lkpTipoDocumento.EditValue), txtNumeroDeDocumento.Text);
        }

        private void btnProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                lista_proveedores();
        }
    }
}