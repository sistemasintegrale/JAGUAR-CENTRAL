using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Administracion_del_Sistema.Listados;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmManteSaldosIniciales : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteSaldosIniciales));
        public List<CuentaContableDetalleBE> mListaCuentaContableDetalleOrigen = new List<CuentaContableDetalleBE>();
        List<EDocPorPagarDetalleCuentaContable> mListaDetalle = new List<EDocPorPagarDetalleCuentaContable>();
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        List<ECuentaContable> mListaCuenta = new List<ECuentaContable>();
        public delegate void DelegadoMensaje(long Cab_icod_correlativo);
        public event DelegadoMensaje MiEvento;
        public int anio = 0;
        public int mes = 0;
        private int xposition = 0;
        public List<EDocPorPagar> oDetail;
        public BSMaintenanceStatus oState;
        private BCuentasPorPagar Obl;
        public long Cab_icod_correlativo = 0;
        public int Moneda = 0;
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

        public enum Operacion
        {
            Nuevo = 1,
            Modificar = 2,
            Eliminar = 3,
            Consultar = 4
        }

        #endregion

        #region "Eventos"

        public FrmManteSaldosIniciales()
        {
            InitializeComponent();
        }

        private void FrmManteRegistroPorPagar_Load(object sender, EventArgs e)
        {
            CargaControles();
            Carga();
        }

        private void btnProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProveedor();
        }

        private void btnDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarDocumento();
        }

        private void btnClaseDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarClaseDocumento();
        }

        private void dtmFechaDocumento_EditValueChanged(object sender, EventArgs e)
        {
            var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtmFechaDocumento.EditValue).ToShortDateString()).ToList();
            txtTipoDeCambio.Enabled = true;
            Lista.ForEach(obe =>
            {
                txtTipoDeCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                txtTipoDeCambio.Enabled = false;
            });
        }

        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            btnProveedor.Enabled = !Enabled;
            btnDocumento.Enabled = !Enabled;
            btnClaseDocumento.Enabled = !Enabled;
            txtSerie.Enabled = !Enabled;
            txtNumeroDocumento.Enabled = !Enabled;
            txtNroDocumento.Enabled = !Enabled;
            dtmFechaDocumento.Enabled = !Enabled;
            dtmFechaVencimiento.Enabled = !Enabled;
            lkpMoneda.Enabled = !Enabled;
            txtTipoDeCambio.Enabled = !Enabled;
            txtConcepto.Enabled = !Enabled;
            txtCorrelativo.Enabled = false;
            txtNoGravada.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                btnDocumento.Enabled = Enabled;
                btnClaseDocumento.Enabled = Enabled;
                txtSerie.Enabled = Enabled;
                txtNumeroDocumento.Enabled = Enabled;
                txtNroDocumento.Enabled = Enabled;
                dtmFechaDocumento.Enabled = !Enabled;
                dtmFechaVencimiento.Enabled = !Enabled;
                lkpMoneda.Enabled = Enabled;
                txtTipoDeCambio.Enabled = Enabled;
            }
        }

        private void clearControl()
        {
            List<EParametro> parametros = new BAdministracionSistema().listarParametro();
            txtConcepto.Text = "";
            btnDocumento.Text = "";
            btnDocumento.Tag = null;
            btnClaseDocumento.Text = "";
            btnClaseDocumento.Tag = null;
            btnProveedor.Text = "";
            btnProveedor.Tag = null;
            //txtIgv.EditValue = Parametros.intIGV;
            txtNoGravada.Text = "0.00";
            lblPrecioCompra.Text = "0.00";
            lblProveedor.Text = "";
            lblSaldo.Text = "0.00";
        }

        public void CargaControles()
        {
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);


            if (Status == BSMaintenanceStatus.CreateNew)
            {
                dtmFechaDocumento.EditValue = "31/12/" + (Parametros.intEjercicio - 1);
                dtmFechaVencimiento.EditValue = DateTime.Now;

            }
            else
            {
                lkpMoneda.EditValue = Moneda;
            }

        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
        }
        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            totalizar();
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EDocPorPagar oBe = new EDocPorPagar();
            Obl = new BCuentasPorPagar();
            try
            {
                if (btnProveedor.Tag == null)
                {
                    oBase = btnProveedor;
                    throw new ArgumentException("Ingrese el Proveedor");
                }

                if (btnDocumento.Tag == null)
                {
                    oBase = btnDocumento;
                    throw new ArgumentException("Ingrese el Tipo de Documento");
                }

                if (btnClaseDocumento.Tag == null)
                {
                    oBase = btnClaseDocumento;
                    throw new ArgumentException("Ingrese la Clase del Documento");
                }

                if (Convert.ToInt32(txtNumeroDocumento.Text) == 0 || (txtSerie.Text) == "")
                {
                    if (string.IsNullOrWhiteSpace(txtNroDocumento.Text))
                    {
                        oBase = txtNumeroDocumento;
                        throw new ArgumentException("Ingrese el N° de Documento");
                    }
                }

                if (dtmFechaVencimiento.DateTime.Date < dtmFechaDocumento.DateTime.Date)
                {
                    oBase = dtmFechaVencimiento;
                    throw new ArgumentException("La fecha de vencimiento no puede ser menor a la fecha del documento.");
                }

                if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                {
                    oBase = txtTipoDeCambio;
                    throw new ArgumentException("Ingrese el Tipo de Cambio");
                }

                if (string.IsNullOrEmpty(txtConcepto.Text))
                {
                    oBase = txtConcepto;
                    throw new ArgumentException("Ingresar el concepto");
                }

                if (Convert.ToDecimal(txtNoGravada.Text) == 0)
                {
                    oBase = txtNoGravada;
                    throw new ArgumentException("Ingrese el monto");
                }

                oBe.anio = Parametros.intEjercicio;//periodo actual
                oBe.mesec_iid_mes = mes;//el mes debe ser cero por ser saldo inicial
                oBe.tdocc_icod_tipo_doc = Convert.ToInt32(btnDocumento.Tag);//id del tipo de documento
                oBe.tdodc_iid_correlativo = Convert.ToInt32(btnClaseDocumento.Tag);//id de la clase de documento
                oBe.doxpc_iid_correlativo = Convert.ToInt64(txtCorrelativo.Text);//correlativo del documento
                if (txtNroDocumento.Text.ToString().Trim().Length > 0)
                {
                    oBe.doxpc_vnumero_doc = txtNroDocumento.Text;//nro de documento diferente al formato serie-número
                    oBe.doxpc_numdoc_tipo = 2;//indica que no es nro de documento con formato serie-número
                }
                else
                {
                    oBe.doxpc_vnumero_doc = txtSerie.Text + txtNumeroDocumento.Text;//nro de documento con formato serie-número
                    oBe.doxpc_numdoc_tipo = 1;//indica que es nro de documento con formato serie-número
                }
                oBe.doxpc_sfecha_doc = Convert.ToDateTime(dtmFechaDocumento.EditValue);//fecha del documento
                oBe.doxpc_sfecha_vencimiento_doc = Convert.ToDateTime(dtmFechaVencimiento.EditValue);//fecha de vencimiento
                oBe.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);//id del proveedor
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);//id de la moneda
                oBe.doxpc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);//tipo de cambio
                oBe.doxpc_vdescrip_transaccion = txtConcepto.Text;//descripción del movimiento
                oBe.doxpc_nmonto_destino_gravado = 0;
                oBe.doxpc_nmonto_destino_mixto = 0;
                oBe.doxpc_nmonto_destino_nogravado = 0;
                oBe.doxpc_nmonto_referencial_cif = 0;
                oBe.doxpc_nmonto_servicio_no_domic = 0;
                oBe.doxpc_nmonto_imp_destino_gravado = 0;
                oBe.doxpc_nmonto_imp_destino_mixto = 0;
                oBe.doxpc_nmonto_imp_destino_nogravado = 0;
                oBe.doxpc_nmonto_total_documento = Convert.ToDecimal(lblPrecioCompra.Text);//monto total
                oBe.doxpc_nmonto_total_saldo = Convert.ToDecimal(lblSaldo.Text);//saldo
                oBe.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;//situación del docuento - GENERADO cuando recién se crea
                oBe.doxpc_tipo_comprobante_referencia = 0;
                oBe.doxpc_num_serie_referencia = "";
                oBe.doxpc_num_comprobante_referencia = "";
                oBe.doxpc_sfecha_emision_referencia = null;
                oBe.doxpc_nporcentaje_isc = 0;
                oBe.doxpc_nmonto_isc = 0;
                oBe.doxpc_vnro_deposito_detraccion = "";
                oBe.intUsuario = Valores.intUsuario;//usuario del sistema SGI que crea
                oBe.strPc = WindowsIdentity.GetCurrent().Name;//nombre usuario windows que crea                
                oBe.doxpc_flag_estado = true;
                oBe.doxpc_nmonto_total_pagado = 0;
                oBe.doxpc_nmonto_nogravado = Convert.ToDecimal(txtNoGravada.Text);//adq.no gravada
                oBe.doxpc_origen = "2";


                oBe.doxpc_nporcentaje_imp_renta = 0;
                oBe.doxpc_nmonto_retencion_rh = 0;
                oBe.doxpc_nmonto_retenido = 0;
                oBe.doxpc_nporcentaje_igv = 0;

                //sólo se le envía el doc x pagar sin las cuentas detalles ya que es saldo inicial
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Cab_icod_correlativo = Obl.InsertarEDocPorPagarSI(oBe);
                }
                else
                {
                    oBe.doxpc_icod_correlativo = Cab_icod_correlativo;
                    Obl.ActualizarEDocPorPagarSI(oBe);
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
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                        Status = BSMaintenanceStatus.View;
                    else
                        Status = BSMaintenanceStatus.View;

                    Status = BSMaintenanceStatus.View;
                    this.MiEvento(Cab_icod_correlativo);
                    this.Close();
                }
            }
        }

        private void ListarProveedor()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                btnProveedor.Tag = Proveedor._Be.iid_icod_proveedor;
                btnProveedor.Text = Proveedor._Be.vcod_proveedor;
                lblProveedor.Text = Proveedor._Be.vnombrecompleto;
            }
            btnDocumento.Focus();
        }

        private void ListarDocumento()
        {
            frmListarTipoDocumento Documentos = new frmListarTipoDocumento();
            Documentos.bModuloall = true;
            Documentos.intIdModulo = Parametros.intModuloCtasPorPagar;
            Documentos.bSaldosIniciales = true;
            if (Documentos.ShowDialog() == DialogResult.OK)
            {
                btnDocumento.Tag = Documentos._Be.tdocc_icod_tipo_doc;
                btnDocumento.Text = Documentos._Be.tdocc_vabreviatura_tipo_doc;

            }
            btnClaseDocumento.Focus();
        }

        private void ListarClaseDocumento()
        {
            frmListarClaseDocumento Clase = new frmListarClaseDocumento();
            Clase.intTipoDoc = Convert.ToInt32(btnDocumento.Tag);
            if (Clase.ShowDialog() == DialogResult.OK)
            {
                btnClaseDocumento.Tag = Clase._Be.tdocd_iid_correlativo;
                btnClaseDocumento.Text = Clase._Be.tdocd_iid_codigo_doc_det.ToString();
                lblClaseDocumento.Text = Clase._Be.tdocd_descripcion;
            }
            txtSerie.Focus();
        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
        }

        private void Carga()
        {
            List<EDocPorPagarDetalleCuentaContable> lstTmpCuentaContableDetalle = new List<EDocPorPagarDetalleCuentaContable>();
            lstTmpCuentaContableDetalle = new BCuentasPorPagar().listarDXPDetCtaContable(Cab_icod_correlativo);
        }

        #endregion

        public class CuentaContableDetalleBE
        {
            public long cdxpc_icod_correlativo { get; set; }
            public long doxpc_icod_correlativo { get; set; }
            public int ctacc_iid_cuenta_contable { get; set; }
            public string NumeroCuentaContable { get; set; }
            public string DescripcionCuentaContable { get; set; }
            public int cecoc_icod_centro_costo { get; set; }
            public string CodigoCentroCosto { get; set; }
            public string DescripcionCentroCosto { get; set; }
            public int anac_icod_analitica { get; set; }
            public int IdTipoAnalitica { get; set; }
            public string TipoAnalitica { get; set; }
            public string NumeroAnalitica { get; set; }
            public decimal cdxpc_nmonto_cuenta { get; set; }
            public string cdxpc_vglosa { get; set; }
            public bool cdxpc_flag_estado { get; set; }
            public int TipOper { get; set; }

            public CuentaContableDetalleBE()
            {

            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtNroDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSerie.Text = "000";
            txtNumeroDocumento.Text = "0000000";
        }

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtNroDocumento.Text = "";
        }

        private void txtNumeroDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtNroDocumento.Text = "";
        }

        private void totalizar()
        {
            lblPrecioCompra.Text = txtNoGravada.Text;
            lblSaldo.Text = txtNoGravada.Text;
        }

        private void btnProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarProveedor();
        }

        private void btnDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarDocumento();
        }

        private void btnClaseDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarClaseDocumento();
        }

        private void txtNoGravada_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void txtNoGravada_KeyUp(object sender, KeyEventArgs e)
        {
            totalizar();
        }




    }
}
#endregion