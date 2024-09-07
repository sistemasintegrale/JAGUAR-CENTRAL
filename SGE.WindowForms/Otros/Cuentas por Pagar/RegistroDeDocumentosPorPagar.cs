using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.Entity.FacturaElectronica;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Administracion_del_Sistema.Listados;
using SGE.WindowForms.Otros.Compras;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmMantedocumentosXPagar : DevExpress.XtraEditors.XtraForm
    {

        List<EDocPorPagarDetalleCuentaContable> Lista = new List<EDocPorPagarDetalleCuentaContable>();
        List<EDocPorPagarDetalleCuentaContable> ListaEliminados = new List<EDocPorPagarDetalleCuentaContable>();
        string rucProveedor;

        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantedocumentosXPagar));
        public List<CuentaContableDetalleBE> mListaCuentaContableDetalleOrigen = new List<CuentaContableDetalleBE>();
        List<EDocPorPagarDetalleCuentaContable> mListaDetalle = new List<EDocPorPagarDetalleCuentaContable>();
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        List<ECuentaContable> mListaCuenta = new List<ECuentaContable>();
        public delegate void DelegadoMensaje(long Cab_icod_correlativo);
        public event DelegadoMensaje MiEvento;
        public int anio = 0;
        public int mes = 0;
        public List<EDocPorPagar> oDetail;
        public EDocPorPagar obeDocXPagar;
        public long Cab_icod_correlativo;
        public BSMaintenanceStatus oState;
        private BCuentasPorPagar Obl;
        public long? Correlativo = 0;
        public int? vcocc_iid_voucher_contable;
        private BSMaintenanceStatus mStatus;
        List<long?> ListaDxPOcultar = new List<long?>(); //cuando se elimina una cuenta ya no se podrá mostrar el mismo documento
        public List<EDXPImportacion> lstDXPImportacion = new List<EDXPImportacion>();
        List<EDXPImportacion> DeletelstDXPImportacion = new List<EDXPImportacion>();
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

        public FrmMantedocumentosXPagar()
        {
            InitializeComponent();
        }

        private void FrmMantedocumentosXPagar_Load(object sender, EventArgs e)
        {
            if (obeDocXPagar != null || Status == BSMaintenanceStatus.CreateNew)
                CargaControles();
            Carga();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal detalle;
            detalle = (Lista.Count > 0) ? Lista.Sum(cuentas => cuentas.cdxpc_nmonto_cuenta) : 0;
            detalle = ((string.IsNullOrEmpty(txtSubtotal.Text)) ? 0 : Convert.ToDecimal(txtSubtotal.Text)) - detalle;
            using (FrmManteDxPDet frm = new FrmManteDxPDet())
            {
                frm.bteDocPagar.Visible = true;
                frm.lblTipoDoc.Visible = true;
                frm.fechaDoc = dtmFechaDocumento.DateTime;
                frm.tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                frm.tipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                frm.tipo_doc = Parametros.intTipoDocLiquidacionCobranza;
                frm.ListaDxPOcultar = ListaDxPOcultar;

                frm.saldoDetalle = (detalle < 0) ? 0 : detalle;
                frm.SetInsert();
                frm.txtConcepto.Text = txtConcepto.Text;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Lista.Add(frm.objDet);
                    viewCuentaContableDetalle.RefreshData();
                    viewCuentaContableDetalle.MoveLast();
                }
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocPorPagarDetalleCuentaContable obj = (EDocPorPagarDetalleCuentaContable)viewCuentaContableDetalle.GetRow(viewCuentaContableDetalle.FocusedRowHandle);

                using (FrmManteDxPDet frm = new FrmManteDxPDet())
                {
                    frm.bteDocPagar.Visible = true;
                    frm.lblTipoDoc.Visible = true;
                    frm.fechaDoc = dtmFechaDocumento.DateTime;
                    frm.tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                    frm.tipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                    frm.tipo_doc = Parametros.intTipoDocLiquidacionCobranza;
                    frm.ListaDxPOcultar = ListaDxPOcultar;
                    //frm.obeProveedor = obeProveedor;

                    if (obj.TipOper != 1)
                    {
                        ActualizarPago(obj);
                        frm.obeDocXPagar = new EDocPorPagar() { tablc_iid_tipo_moneda = Convert.ToInt32(obj.tablc_iid_tipo_moneda), doxpc_nmonto_total_saldo = obj.doxpc_nmonto_total_saldo };
                    }

                    frm.objDet = obj;
                    frm.SetModify();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        viewCuentaContableDetalle.RefreshData();
                        viewCuentaContableDetalle.MoveLast();
                    }
                    //frm.bteCuenta.Enabled = true;
                    //frm.bteCCosto.Enabled = true;
                    //frm.bteAnalitica.Enabled = true;
                }
            }
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                if (XtraMessageBox.Show("Está seguro de eliminar", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    EliminarDetalle();
                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void EliminarDetalle()
        {
            EDocPorPagarDetalleCuentaContable obj = (EDocPorPagarDetalleCuentaContable)viewCuentaContableDetalle.GetRow(viewCuentaContableDetalle.FocusedRowHandle);
            if (obj.TipOper == 1)
            {
                Lista.Remove(obj);
                viewCuentaContableDetalle.RefreshData();
                viewCuentaContableDetalle.MovePrev();
            }
            else
            {
                if (obeDocXPagar.tdocc_icod_tipo_doc == Parametros.intTipoDocLiquidacionCobranza)
                    ActualizarPago(obj);
                obj.TipOper = 3;
                obj.intUsuario = Valores.intUsuario;
                obj.strPc = WindowsIdentity.GetCurrent().Name;
                ListaEliminados.Add(obj);
                Lista.Remove(obj);
                viewCuentaContableDetalle.RefreshData();
                viewCuentaContableDetalle.MovePrev();
                ListaDxPOcultar.Add(obj.doxpc_icod_correlativo_dxp);
            }
        }
        #endregion

        public void SetValues()
        {
            if (obeDocXPagar.tdocc_icod_tipo_doc != 106)
            {
                if (obeDocXPagar.doxpc_vnumero_doc.Length == 12)
                {
                    txtSerie.Text = obeDocXPagar.doxpc_vnumero_doc.Substring(0, 4);
                    txtNumeroDocumento.Text = obeDocXPagar.doxpc_vnumero_doc.Substring(4, 8);
                }
                else
                    txtNroDoc2.Text = obeDocXPagar.doxpc_vnumero_doc;
            }
            else
                txtNroDoc2.Text = obeDocXPagar.doxpc_vnumero_doc;
            rucProveedor = obeDocXPagar.proc_vruc;
            Cab_icod_correlativo = obeDocXPagar.doxpc_icod_correlativo;
            //CARGAR CONTROLES
            vcocc_iid_voucher_contable = obeDocXPagar.vcocc_iid_voucher_contable;
            Correlativo = obeDocXPagar.doxpc_iid_correlativo;
            txtCorrelativo.Text = obeDocXPagar.doxpc_viid_correlativo;
            mes = Convert.ToInt32(obeDocXPagar.mesec_iid_mes);
            btnProveedor.Tag = obeDocXPagar.proc_icod_proveedor;
            btnProveedor.Text = obeDocXPagar.proc_vcod_proveedor;
            lblProveedor.Text = obeDocXPagar.proc_vnombrecompleto;
            bteTipoDoc.Tag = obeDocXPagar.tdocc_icod_tipo_doc;
            bteTipoDoc.Text = obeDocXPagar.tdocc_vabreviatura_tipo_doc;
            bteClaseDoc.Tag = obeDocXPagar.tdodc_iid_correlativo;
            bteClaseDoc.Text = obeDocXPagar.clase_viid_correlativo;
            lblClaseDocumento.Text = obeDocXPagar.tdodc_descripcion;
            dtmFechaDocumento.EditValue = obeDocXPagar.doxpc_sfecha_doc;
            dtmFechaVencimiento.EditValue = obeDocXPagar.doxpc_sfecha_vencimiento_doc;
            lkpMoneda.EditValue = obeDocXPagar.tablc_iid_tipo_moneda;
            //txtSerie.Text = obeDocXPagar.doxpc_vnumero_doc.Substring(0, 4);
            //txtNumeroDocumento.Text = obeDocXPagar.doxpc_vnumero_doc.Substring(4);
            txtIgv.EditValue = obeDocXPagar.doxpc_nporcentaje_igv;
            txtTipoDeCambio.Text = obeDocXPagar.doxpc_nmonto_tipo_cambio.ToString();
            txtConcepto.Text = obeDocXPagar.doxpc_vdescrip_transaccion;
            txtDesGrav.EditValue = obeDocXPagar.doxpc_nmonto_destino_gravado;
            txtDestMixto.EditValue = obeDocXPagar.doxpc_nmonto_destino_mixto;
            txtDesNoGrav.EditValue = obeDocXPagar.doxpc_nmonto_destino_nogravado;
            txtNoGravada.EditValue = obeDocXPagar.doxpc_nmonto_nogravado;
            txtReferencia.EditValue = obeDocXPagar.doxpc_nmonto_referencial_cif;
            txtServicios.EditValue = obeDocXPagar.doxpc_nmonto_servicio_no_domic;
            txtIgvAdqDesGrav.EditValue = obeDocXPagar.doxpc_nmonto_imp_destino_gravado;
            txtIgvDestMixto.EditValue = obeDocXPagar.doxpc_nmonto_imp_destino_mixto;
            txtIgvDesNoGrav.EditValue = obeDocXPagar.doxpc_nmonto_imp_destino_nogravado;
            txtSelcCons.EditValue = obeDocXPagar.doxpc_nmonto_isc;
            txtSubtotal.Text = obeDocXPagar.Valorcompra.ToString();
            txtPrecioCompra.Text = obeDocXPagar.doxpc_nmonto_total_documento.ToString();
            txtSaldo.Text = obeDocXPagar.doxpc_nmonto_total_saldo.ToString();
            txtDetraccion.Text = obeDocXPagar.doxpc_vnro_deposito_detraccion;
            dtmFechaDetraccion.EditValue = obeDocXPagar.doxpc_sfec_deposito_detraccion;
            lkpTipoAdquisicion.EditValue = obeDocXPagar.doxpc_itipo_adquisicion;
            lkpClasCompras.EditValue = obeDocXPagar.tablc_iid_clasificacion_compras;
            if (obeDocXPagar.tdocc_icod_tipo_doc == 49 && obeDocXPagar.tdodc_iid_correlativo == 47)
            {
                txtDesGrav.Text = obeDocXPagar.doxpc_nmonto_nogravado.ToString();
                txtNoGravada.Text = obeDocXPagar.doxpc_nmonto_destino_gravado.ToString();
            }
            lkpTipoDocRef.EditValue = obeDocXPagar.doxpc_tipo_comprobante_referencia;
            txtseriereferencia.Text = obeDocXPagar.doxpc_num_serie_referencia == null ? "000" : obeDocXPagar.doxpc_num_serie_referencia;
            txtCorrelativoreferencia.Text = obeDocXPagar.doxpc_num_comprobante_referencia == null ? "0000000" : obeDocXPagar.doxpc_num_comprobante_referencia;
            dtFechaReferencia.EditValue = obeDocXPagar.doxpc_sfecha_emision_referencia;
            /*DUA*/
            if (obeDocXPagar.tdocc_icod_tipo_doc == 106 || obeDocXPagar.tdocc_icod_tipo_doc == 109)
            {
                txtCodAduana.Text = obeDocXPagar.doxpc_codigo_aduana;
                txtAnio.Text = obeDocXPagar.doxpc_anio;
                txtNumDeclaracion.Text = obeDocXPagar.doxpc_numero_declaracion;
            }
            if (obeDocXPagar.tdocc_icod_tipo_doc == 32)
            {
                txtCodAduana.Text = obeDocXPagar.doxpc_codigo_aduana;
            }
            txtDetraccion.Properties.ReadOnly = false;
            dtmFechaDetraccion.Properties.ReadOnly = false;
            txtReferencia.Properties.ReadOnly = true;

            btnOrdenCompra.Tag = obeDocXPagar.ocsc_icod_ocs;
            btnOrdenCompra.Text = obeDocXPagar.NumOCS;

            if (obeDocXPagar.tablc_iid_situacion_documento == 8)
            {
                txtTipoDeCambio.Properties.ReadOnly = false;
            }
            else
            {
                int soles = 0;
                int dolares = 0;
                List<EDocPorPagarPago> lIS = new BCuentasPorPagar().listarDxpPagos(Convert.ToInt32(obeDocXPagar.doxpc_icod_correlativo), Parametros.intEjercicio);
                foreach (var item in lIS)
                {
                    if (item.tablc_iid_tipo_moneda == 1)
                    {
                        soles = soles + 1;
                    }
                    else
                    {
                        dolares = dolares + 1;
                    }
                }
                if ((obeDocXPagar.tablc_iid_tipo_moneda) == 1)
                {
                    if (dolares != 0)
                    {
                        txtTipoDeCambio.Properties.ReadOnly = true;
                    }
                    else
                    {
                        txtTipoDeCambio.Properties.ReadOnly = false;
                    }
                }
                else
                {
                    if (soles != 0)
                    {
                        txtTipoDeCambio.Properties.ReadOnly = true;
                    }
                    else
                    {
                        txtTipoDeCambio.Properties.ReadOnly = false;
                    }
                }
            }

        }
        #region "Metodos"

        private void ActualizarPago(EDocPorPagarDetalleCuentaContable obj)
        {
            if (obj.tablc_iid_tipo_moneda == Convert.ToInt32(lkpMoneda.EditValue))
            {
                obj.doxpc_nmonto_total_saldo = obj.doxpc_nmonto_total_saldo + obj.cdxpc_nmonto_cuenta;
                obj.doxpc_nmonto_total_pagado = obj.doxpc_nmonto_total_pagado - obj.cdxpc_nmonto_cuenta;
            }
            else
            {
                if (obj.tablc_iid_tipo_moneda == Parametros.intSoles)
                {
                    obj.doxpc_nmonto_total_saldo = obj.doxpc_nmonto_total_saldo + (obj.cdxpc_nmonto_cuenta * Convert.ToDecimal(txtTipoDeCambio.Text));
                    obj.doxpc_nmonto_total_pagado = obj.doxpc_nmonto_total_pagado - (obj.cdxpc_nmonto_cuenta * Convert.ToDecimal(txtTipoDeCambio.Text));
                }
                else
                {
                    obj.doxpc_nmonto_total_saldo = obj.doxpc_nmonto_total_saldo + (obj.cdxpc_nmonto_cuenta / Convert.ToDecimal(txtTipoDeCambio.Text));
                    obj.doxpc_nmonto_total_pagado = obj.doxpc_nmonto_total_pagado - (obj.cdxpc_nmonto_cuenta / Convert.ToDecimal(txtTipoDeCambio.Text));
                }
            }

            if (obj.doxpc_nmonto_total_saldo > obj.doxpc_nmonto_total_documento)
            {
                obj.doxpc_nmonto_total_saldo = Convert.ToDecimal(obj.doxpc_nmonto_total_documento);
                obj.doxpc_nmonto_total_pagado = 0;
            }
        }

        private void StatusControl()
        {
            int dxp_situ = (obeDocXPagar == null) ? Parametros.intSitDocGenerado : Convert.ToInt32(obeDocXPagar.tablc_iid_situacion_documento);
            bool Enabled = (Status == BSMaintenanceStatus.CreateNew || (Status == BSMaintenanceStatus.ModifyCurrent && dxp_situ == Parametros.intSitDocGenerado));
            int tdoc = (obeDocXPagar == null) ? 0 : Convert.ToInt32(obeDocXPagar.tdocc_icod_tipo_doc);

            btnProveedor.Enabled = Enabled;
            bteTipoDoc.Enabled = Enabled;
            bteClaseDoc.Enabled = Enabled;

            txtSerie.Properties.ReadOnly = !Enabled;
            txtNumeroDocumento.Properties.ReadOnly = !Enabled;
            txtNroDoc2.Properties.ReadOnly = !Enabled;

            //dtmFechaDocumento.Properties.ReadOnly = Enabled;
            dtmFechaVencimiento.Properties.ReadOnly = !Enabled;
            //dtmFechaDocumento.Enabled = Enabled;
            lkpMoneda.Properties.ReadOnly = !Enabled;
            txtConcepto.Properties.ReadOnly = !Enabled;
            txtCorrelativo.Properties.ReadOnly = false;
            txtTipoDeCambio.Properties.ReadOnly = !Enabled;
            txtDesGrav.Properties.ReadOnly = !Enabled;
            txtDestMixto.Properties.ReadOnly = !Enabled;
            txtDesNoGrav.Properties.ReadOnly = !Enabled;
            txtNoGravada.Properties.ReadOnly = !Enabled;
            txtServicios.Properties.ReadOnly = !Enabled;

            txtIgvAdqDesGrav.Properties.ReadOnly = !Enabled;
            txtIgvDestMixto.Properties.ReadOnly = !Enabled;
            txtIgvDesNoGrav.Properties.ReadOnly = !Enabled;
            txtSelcCons.Properties.ReadOnly = !Enabled;

            txtDetraccion.Properties.ReadOnly = !Enabled;
            dtmFechaDetraccion.Properties.ReadOnly = !Enabled;
            btnOrdenCompra.Enabled = false;
            //su habilitación depende del tipo de documento
            if (tdoc == Parametros.intTipoDocFacturaCompra || tdoc == Parametros.intTipoDocDeclaracionUnicaAduana || tdoc == Parametros.intTipoDocDeclaracionImportacionCourier || tdoc == Parametros.intTipoDocLiquidacionCobranzaAduana || tdoc == 109 || tdoc == 32)
            {
                txtReferencia.Properties.ReadOnly = !(dxp_situ == Parametros.intSitDocGenerado);
                //txtCodAduana.Enabled = true;
                //txtAnio.Enabled = true;
                //txtNumDeclaracion.Enabled = true;
            }
            else
                txtReferencia.Properties.ReadOnly = true;

            if (tdoc == Parametros.intTipoDocDeclaracionUnicaAduana || tdoc == Parametros.intTipoDocDeclaracionImportacionCourier || tdoc == Parametros.intTipoDocLiquidacionCobranzaAduana)
            {
                txtCodAduana.Enabled = true;
                txtAnio.Enabled = true;
                txtNumDeclaracion.Enabled = true;
            }

            Enabled = (Status == BSMaintenanceStatus.View && obeDocXPagar == null);
            mnuCuentaContable.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            Enabled = (Status == BSMaintenanceStatus.View);
            //txtDetraccion.Properties.ReadOnly = Enabled;
            //dtmFechaDetraccion.Properties.ReadOnly = Enabled;
        }

        public void StatusControl2()
        {
            int dxp_situ = (obeDocXPagar == null) ? Parametros.intSitDocGenerado : Convert.ToInt32(obeDocXPagar.tablc_iid_situacion_documento);
            bool Enabled = (Status == BSMaintenanceStatus.CreateNew || (Status == BSMaintenanceStatus.ModifyCurrent && dxp_situ == Parametros.intSitDocGenerado));
            int tdoc = (obeDocXPagar == null) ? 0 : Convert.ToInt32(obeDocXPagar.tdocc_icod_tipo_doc);

            btnProveedor.Enabled = Enabled;
            bteTipoDoc.Enabled = Enabled;
            bteClaseDoc.Enabled = Enabled;

            txtSerie.Properties.ReadOnly = !Enabled;
            txtNumeroDocumento.Properties.ReadOnly = !Enabled;
            txtTipoDeCambio.Enabled = Enabled;
            txtNroDoc2.Properties.ReadOnly = !Enabled;
            dtmFechaDocumento.Properties.ReadOnly = !Enabled;
            dtmFechaVencimiento.Properties.ReadOnly = !Enabled;
            dtmFechaDocumento.Enabled = Enabled;
            dtmFechaVencimiento.Enabled = Enabled;

            lkpMoneda.Properties.ReadOnly = !Enabled;
            txtConcepto.Properties.ReadOnly = !Enabled;
            txtCorrelativo.Properties.ReadOnly = false;

            txtDesGrav.Properties.ReadOnly = !Enabled;
            txtDestMixto.Properties.ReadOnly = !Enabled;
            txtDesNoGrav.Properties.ReadOnly = !Enabled;
            txtNoGravada.Properties.ReadOnly = !Enabled;
            txtServicios.Properties.ReadOnly = !Enabled;

            txtIgvAdqDesGrav.Properties.ReadOnly = !Enabled;
            txtIgvDestMixto.Properties.ReadOnly = !Enabled;
            txtIgvDesNoGrav.Properties.ReadOnly = !Enabled;
            txtSelcCons.Properties.ReadOnly = !Enabled;
            btnOrdenCompra.Enabled = false;
            txtDetraccion.Enabled = !Enabled;
            dtmFechaDetraccion.Enabled = !Enabled;

            //su habilitación depende del tipo de documento
            if (tdoc == Parametros.intTipoDocFacturaCompra || tdoc == Parametros.intTipoDocDeclaracionUnicaAduana || tdoc == Parametros.intTipoDocDeclaracionImportacionCourier || tdoc == Parametros.intTipoDocLiquidacionCobranzaAduana || tdoc == 109)
            {
                txtReferencia.Properties.ReadOnly = !(dxp_situ == Parametros.intSitDocGenerado);
                txtCodAduana.Enabled = true;
                txtAnio.Enabled = true;
                txtNumDeclaracion.Enabled = true;
            }
            else
                txtReferencia.Properties.ReadOnly = true;

            Enabled = (Status == BSMaintenanceStatus.View && obeDocXPagar == null);
            mnuCuentaContable.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            Enabled = (Status == BSMaintenanceStatus.View);
            //txtDetraccion.Properties.ReadOnly = Enabled;
            //dtmFechaDetraccion.Properties.ReadOnly = Enabled;
        }

        private void clearControl()
        {
            List<EParametro> parametros = new BAdministracionSistema().listarParametro();
            txtConcepto.Text = "";
            txtCorrelativo.Text = String.Format("{0:0000000}", Convert.ToInt32(Correlativo));
            txtDesGrav.Text = "0.00";
            txtDesNoGrav.Text = "0.00";
            txtDestMixto.Text = "0.00";
            bteTipoDoc.Text = "";
            bteClaseDoc.Text = "";
            txtIgv.EditValue = parametros[0].pm_nigv_parametro;
            txtNoGravada.Text = "0.00";
            txtPrecioCompra.Text = "0.00";
            lblProveedor.Text = "";
            txtReferencia.Text = "0.00";
            txtSaldo.Text = "0.00";
            txtSelcCons.Text = "0.00";
            txtServicios.Text = "0.00";
            txtIgvAdqDesGrav.Text = "0.00";
            txtIgvDesNoGrav.Text = "0.00";
            txtIgvDestMixto.Text = "0.00";
        }

        public void CargaControles()
        {
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            //var lstMoneda = new BGeneral().listarTablaRegistro(5);
            //BSControls.LoaderLook(lkpMoneda, lstMoneda, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpTipoAdquisicion, new BGeneral().listarTablaRegistro(81), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpClasCompras, new BGeneral().listarTablaRegistro(88), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            List<TipoDoc> lst = new List<TipoDoc>();
            lst.Add(new TipoDoc { intCodigo = 0, strTipoDoc = ".......Eligir un documento....." });
            lst.Add(new TipoDoc { intCodigo = 24, strTipoDoc = "FAC" });
            lst.Add(new TipoDoc { intCodigo = 84, strTipoDoc = "BOC" });

            BSControls.LoaderLook(lkpTipoDocRef, lst, "strTipoDoc", "intCodigo", true);
            if (obeDocXPagar != null)
            {
                lkpTipoDocRef.EditValue = obeDocXPagar.doxpc_tipo_comprobante_referencia;
            }
            else
            {
                lkpTipoDocRef.EditValue = 0;
            }

            if (Status == BSMaintenanceStatus.CreateNew)
            {
                dtmFechaDocumento.EditValue = DateTime.Now;
                dtmFechaDetraccion.EditValue = DateTime.Now;
                dtmFechaVencimiento.EditValue = DateTime.Now;
            }
            else
            {
                lkpMoneda.EditValue = obeDocXPagar.tablc_iid_tipo_moneda;
                lkpTipoAdquisicion.EditValue = obeDocXPagar.doxpc_itipo_adquisicion;
                lkpClasCompras.EditValue = obeDocXPagar.tablc_iid_clasificacion_compras;
                //lkpMoneda.ItemIndex = obeDocXPagar.tablc_iid_tipo_moneda - 1;
                //lkpMoneda.Properties.ReadOnly = true;
            }
        }

        public class TipoDoc
        {
            public int intCodigo { get; set; }
            public string strTipoDoc { get; set; }
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
        }

        private async Task<bool> SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EDocPorPagar Obe = new EDocPorPagar();
            Obl = new BCuentasPorPagar();
            try
            {
                if (btnProveedor.Tag == null)
                {
                    oBase = btnProveedor;
                    throw new ArgumentException("Ingrese el Proveedor");
                }

                if (bteTipoDoc.Tag == null)
                {
                    oBase = bteTipoDoc;
                    throw new ArgumentException("Ingrese el Tipo de Documento");
                }


                if (bteClaseDoc.Tag == null)
                {
                    oBase = bteClaseDoc;
                    throw new ArgumentException("Ingrese la Clase del Documento");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (Convert.ToInt32(bteTipoDoc.Tag) != 106)
                    {
                        if (txtSerie.Enabled)
                        {
                            if (txtSerie.Text == "")
                            {
                                txtSerie.Focus();
                                throw new ArgumentException("Ingrese nro. de Serie de la factura");
                            }

                            if (txtNumeroDocumento.Text == "00000000")
                            {
                                txtNumeroDocumento.Focus();
                                throw new ArgumentException("Ingrese nro. de la factura");
                            }

                        }
                        else
                        {
                            if (String.IsNullOrWhiteSpace(txtNroDoc2.Text))
                            {
                                oBase = txtNroDoc2;
                                throw new ArgumentException("Ingrese nro. de la factura");
                            }
                        }
                    }

                }
                if (dtmFechaVencimiento.EditValue != null)
                {
                    if (dtmFechaVencimiento.DateTime.Date < dtmFechaDocumento.DateTime.Date)
                    {
                        oBase = dtmFechaVencimiento;
                        throw new ArgumentException("La fecha de vencimiento no puede ser menor a la fecha del documento.");
                    }
                }
                else
                {
                    dtmFechaVencimiento.EditValue = null;
                }

                if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    if (Correlativo != Convert.ToInt64(txtCorrelativo.Text))
                    {
                        if (oDetail.Exists(obe => obe.anio == Parametros.intEjercicio && obe.mesec_iid_mes == mes && obe.doxpc_iid_correlativo == Convert.ToInt64(txtCorrelativo.Text)))
                        {
                            oBase = txtCorrelativo;
                            throw new ArgumentException("El número correlativo " + txtCorrelativo.Text + " ya fue utilizado");
                        }
                    }
                }

                //if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                //{
                //    oBase = txtTipoDeCambio;
                //    throw new ArgumentException("Ingrese el Tipo de Cambio");
                //}
                if (Status != BSMaintenanceStatus.View)
                {
                    if (string.IsNullOrEmpty(txtConcepto.Text))
                    {
                        oBase = txtConcepto;
                        throw new ArgumentException("Ingrese el concepto");
                    }
                }

                if (Convert.ToDecimal(txtPrecioCompra.Text) == 0)
                {
                    throw new ArgumentException("Ingrese el Precio");
                }
                decimal SumDetalle;
                if (Obe.doxpc_origen != "9")
                {
                    if (Lista.Count == 0)
                    {
                        if (XtraMessageBox.Show("No ha ingresado el detalle de las cuentas ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                            throw new ArgumentException(string.Empty);
                    }
                    else if (Lista.Sum(cuentas => cuentas.cdxpc_nmonto_cuenta) != Convert.ToDecimal(txtSubtotal.Text))
                    {
                        SumDetalle = Math.Round(Lista.Sum(cuentas => cuentas.cdxpc_nmonto_cuenta), 2);
                        if (XtraMessageBox.Show("La suma de los montos de las cuentas no es igual al valor compra ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                            throw new ArgumentException(string.Empty);
                    }
                }

                if (txtNroDoc2.Text.ToString().Trim().Length > 0) //número del documento
                {//otro formato
                    Obe.doxpc_vnumero_doc = txtNroDoc2.Text;
                    Obe.doxpc_numdoc_tipo = 2;
                }
                else
                {//formato serie número
                    Obe.doxpc_vnumero_doc = txtSerie.Text + txtNumeroDocumento.Text;
                    Obe.doxpc_numdoc_tipo = 1;
                }
                Obe.doxpc_vnumero_serio = txtSerie.Text;
                Obe.doxpc_vnumero_correlativo = txtNumeroDocumento.Text;
                Obe.vcocc_iid_voucher_contable = vcocc_iid_voucher_contable;
                Obe.anio = Parametros.intEjercicio;
                Obe.mesec_iid_mes = mes;
                Obe.tdocc_icod_tipo_doc = Convert.ToInt32(bteTipoDoc.Tag);
                Obe.tdodc_iid_correlativo = Convert.ToInt32(bteClaseDoc.Tag);
                Obe.doxpc_iid_correlativo = Convert.ToInt64(txtCorrelativo.Text);
                //Obe.doxpc_vnumero_doc = txtSerie.Text + txtNumeroDocumento.Text;
                //Obe.doxpc_numdoc_tipo = 1;


                Obe.doxpc_sfecha_doc = Convert.ToDateTime(dtmFechaDocumento.EditValue);
                if (dtmFechaVencimiento.EditValue != null)
                {
                    Obe.doxpc_sfecha_vencimiento_doc = Convert.ToDateTime(dtmFechaVencimiento.EditValue);
                }
                else
                {
                    Obe.doxpc_sfecha_vencimiento_doc = null;
                }

                Obe.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag); //correlativo del proveedor
                Obe.proc_vnombrecompleto = lblProveedor.Text; //nombre completo del proveedor
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.doxpc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                Obe.doxpc_vdescrip_transaccion = txtConcepto.Text;
                Obe.doxpc_nmonto_destino_gravado = Convert.ToDecimal(txtDesGrav.Text); //adq. destino gravado
                Obe.doxpc_nmonto_destino_mixto = Convert.ToDecimal(txtDestMixto.Text); //adq. destino mixto
                Obe.doxpc_nmonto_destino_nogravado = Convert.ToDecimal(txtDesNoGrav.Text); //adq. destino no gravada
                Obe.doxpc_nmonto_referencial_cif = Convert.ToDecimal(txtReferencia.Text); //monto referencia dependiendo el tipo de documento
                Obe.doxpc_nmonto_servicio_no_domic = Convert.ToDecimal(txtServicios.Text); //monto servicio
                Obe.doxpc_nmonto_imp_destino_gravado = Convert.ToDecimal(txtIgvAdqDesGrav.Text); //impuesto adq.destino gravado
                Obe.doxpc_nmonto_imp_destino_mixto = Convert.ToDecimal(txtIgvDestMixto.Text); //impuesto mixto
                Obe.doxpc_nmonto_imp_destino_nogravado = Convert.ToDecimal(txtIgvDesNoGrav.Text); //impuesto adq. destino no gravado
                Obe.doxpc_nmonto_total_documento = Convert.ToDecimal(txtPrecioCompra.Text); //precio compra
                Obe.doxpc_nmonto_total_pagado = 0; //monto pagado cero, porque recién se está creando
                Obe.doxpc_nmonto_total_saldo = Convert.ToDecimal(txtSaldo.Text); //saldo
                Obe.doxpc_nporcentaje_igv = Convert.ToDecimal(txtIgv.Text); //IGV
                Obe.tablc_iid_situacion_documento = Parametros.intSitDocGenerado; //292 indica la situación del documento GENERADO
                Obe.doxpc_itipo_adquisicion = Convert.ToInt32(lkpTipoAdquisicion.EditValue);
                Obe.tablc_iid_clasificacion_compras = Convert.ToInt32(lkpClasCompras.EditValue);
                Obe.doxpc_tipo_comprobante_referencia = Convert.ToInt32(lkpTipoDocRef.EditValue);
                if (txtseriereferencia.Text != "0")
                {
                    Obe.doxpc_num_serie_referencia = txtseriereferencia.Text;
                    Obe.doxpc_num_comprobante_referencia = txtCorrelativoreferencia.Text;
                }
                else
                {
                    Obe.doxpc_num_serie_referencia = "";
                    Obe.doxpc_num_comprobante_referencia = "";
                }
                if (dtFechaReferencia.EditValue != null)
                    Obe.doxpc_sfecha_emision_referencia = Convert.ToDateTime(dtFechaReferencia.EditValue);
                else
                {
                    Obe.doxpc_sfecha_emision_referencia = null;
                }

                //Obe.doxpc_nmonto_isc = Convert.ToDecimal(txtSelcCons.Text); //monto correspondiente al ISC
                Obe.doxpc_vnro_deposito_detraccion = txtDetraccion.Text; //número de depósito de detracción

                if (dtmFechaDetraccion.EditValue != null)
                    Obe.doxpc_sfec_deposito_detraccion = Convert.ToDateTime(dtmFechaDetraccion.EditValue);
                else
                {
                    Obe.doxpc_sfec_deposito_detraccion = null;
                }


                Obe.intUsuario = Valores.intUsuario; //código de usuario que está creando el documento
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.doxpc_origen = "2"; //origen del documento, en este caso 2 representa que ha sido creado directamente
                Obe.doxpc_flag_estado = true;
                Obe.doxpc_nmonto_nogravado = Convert.ToDecimal(txtNoGravada.Text); //monto no gravado

                //Obe.DOXPC
                if (Obe.tdocc_icod_tipo_doc == 49 && Obe.tdodc_iid_correlativo == 47)
                {
                    Obe.doxpc_nmonto_destino_gravado = Convert.ToDecimal(txtNoGravada.Text); //adq. destino gravado
                    Obe.doxpc_nmonto_nogravado = Convert.ToDecimal(txtDesGrav.Text); //monto no gravado
                }

                Obe.doxpc_nporcentaje_imp_renta = 0;
                Obe.doxpc_nmonto_retencion_rh = 0;
                Obe.doxpc_nmonto_retenido = 0;
                /*DUA*/
                Obe.doxpc_codigo_aduana = txtCodAduana.Text;
                Obe.doxpc_anio = txtAnio.Text;
                Obe.doxpc_numero_declaracion = txtNumDeclaracion.Text;
                if (Obe.tdocc_icod_tipo_doc == 106)
                {
                    Obe.doxpc_vnumero_doc = txtCodAduana.Text + txtAnio.Text + txtNumDeclaracion.Text;
                    Obe.doxpc_numdoc_tipo = 2;
                }
                Obe.ocsc_icod_ocs = Convert.ToInt32(btnOrdenCompra.Tag);
                if (Status == BSMaintenanceStatus.CreateNew)
                {

                    if (Convert.ToInt32(bteTipoDoc.Tag) == Parametros.intTipoDocFacturaCompra)
                    {
                        if (await ValidarCRE() == "NO EXISTE")
                            if (XtraMessageBox.Show("La FAC no existe en SUNAT, Desea Continuar ?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                return false;
                    }
                    Cab_icod_correlativo = Obl.InsertarEDocPorPagar(Obe, Lista, null, lstDXPImportacion);
                }
                else
                {
                    Obe.doxpc_icod_correlativo = obeDocXPagar.doxpc_icod_correlativo;
                    Obl.ActualizarEDocPorPagar(Obe, Lista, null, ListaEliminados, null, DeletelstDXPImportacion, lstDXPImportacion);
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
                if (!String.IsNullOrEmpty(ex.Message))
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Cab_icod_correlativo);
                    this.DialogResult = DialogResult.OK;
                }
            }
            return Flag;
        }


        private void ListarProveedor()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                btnProveedor.Tag = Proveedor._Be.iid_icod_proveedor;//proc_icod_proveedor
                btnProveedor.Text = Proveedor._Be.vcod_proveedor;
                lblProveedor.Text = Proveedor._Be.vnombrecompleto;
                rucProveedor = Proveedor._Be.vruc;
            }
            bteTipoDoc.Focus();
        }

        private void ListarDocumento()
        {
            frmListarTipoDocumento frm = new frmListarTipoDocumento();
            frm.bModuloall = true;
            frm.intIdModulo = Parametros.intModuloCtasPorPagar;
            //Documentos.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bteTipoDoc.Tag = frm._Be.tdocc_icod_tipo_doc;
                bteTipoDoc.Text = frm._Be.tdocc_vabreviatura_tipo_doc;
                /**/
                bteClaseDoc.Tag = null;
                bteClaseDoc.Text = String.Empty;
                /**/
                int tdoc = frm._Be.tdocc_icod_tipo_doc;
                if (tdoc == 51)
                {
                    btnGuardar.Enabled = false;
                    XtraMessageBox.Show("RHO Debe ser Registrado por la opción correspondiente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if (tdoc == Parametros.intTipoDocFacturaCompra || tdoc == Parametros.intTipoDocDeclaracionUnicaAduana || tdoc == Parametros.intTipoDocDeclaracionImportacionCourier || tdoc == Parametros.intTipoDocLiquidacionCobranzaAduana)
                    txtReferencia.Properties.ReadOnly = false;
                else
                    txtReferencia.Properties.ReadOnly = true;
                if (Convert.ToInt32(bteTipoDoc.Tag) == 106)
                {
                    txtCodAduana.Enabled = true;
                    txtAnio.Enabled = true;
                    txtNumDeclaracion.Enabled = true;
                    /**/
                    txtSerie.Enabled = false;
                    txtNumeroDocumento.Enabled = false;
                    txtNroDoc2.Enabled = false;
                }
                if (Convert.ToInt32(bteTipoDoc.Tag) == 32)
                {
                    txtCodAduana.Enabled = true;
                    txtAnio.Enabled = true;
                    txtNumDeclaracion.Enabled = true;
                }

            }
            bteClaseDoc.Focus();
        }

        private void ListarClaseDocumento()
        {


            frmListarClaseDocumento Clase = new frmListarClaseDocumento();
            Clase.intTipoDoc = Convert.ToInt32(bteTipoDoc.Tag);
            //Clase.Carga();
            if (Clase.ShowDialog() == DialogResult.OK)
            {
                bteClaseDoc.Tag = Clase._Be.tdocd_iid_correlativo;
                bteClaseDoc.Text = Clase._Be.tdocd_iid_codigo_doc_det.ToString();
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

        void AddEvent()
        {
            this.viewCuentaContableDetalle.DoubleClick += new System.EventHandler(this.viewCuentaContableDetalle_DoubleClick);
        }

        private void Carga()
        {
            Lista = new BCuentasPorPagar().listarDXPDetCtaContable(Cab_icod_correlativo);
            grdCuentaContableDetalle.DataSource = Lista;
            ListaDxPOcultar.Add(Cab_icod_correlativo);
            /**/

            foreach (var item in Lista)
            {
                ListaDxPOcultar.Add(item.doxpc_icod_correlativo_dxp);
            }

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


        }

        private async void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtmFechaDocumento.DateTime.Year > Parametros.intEjercicio)
            {
                XtraMessageBox.Show("La fecha del documento corresponde a un ejercicio mayor al actual", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (dtmFechaDocumento.DateTime.Month > mes && dtmFechaDocumento.DateTime.Year == Parametros.intEjercicio)
            {
                XtraMessageBox.Show("La fecha del documento no debe exceder al mes del proceso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (await SetSave())
                    if (dtmFechaDocumento.DateTime.Year < Parametros.intEjercicio)
                        XtraMessageBox.Show("Se registró el documento con un ejercicio anterior al acutal", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else if (Convert.ToDateTime(dtmFechaDocumento.EditValue).Month < mes)
                        XtraMessageBox.Show("Se registró el documento con fecha anterior a la fecha del proceso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void totalizar()
        {
            decimal montos, impuestos;
            montos = Convert.ToDecimal(txtDesGrav.Text) + Convert.ToDecimal(txtDestMixto.Text) + Convert.ToDecimal(txtDesNoGrav.Text) + Convert.ToDecimal(txtNoGravada.Text);
            txtSubtotal.Text = montos.ToString("n2");

            impuestos = CalcularImpuestos();
            txtPrecioCompra.Text = (Math.Round((montos + impuestos), 2)).ToString("n2");
            txtSaldo.Text = txtPrecioCompra.Text;
        }

        private decimal calcularImpuesto(decimal monto)
        {
            decimal imp = Convert.ToDecimal(txtIgv.Text);
            return (monto * (imp / 100));
        }

        private void txtDesGrav_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDesGrav.ContainsFocus)
            {
                if (Convert.ToDecimal(txtDesGrav.Text) != 0)
                {
                    if (txtReferencia.Properties.ReadOnly)
                        txtIgvAdqDesGrav.Text = Math.Round(calcularImpuesto(Convert.ToDecimal(txtDesGrav.Text)), 2).ToString();
                    else
                        txtIgvAdqDesGrav.Text = Math.Round(calcularImpuesto(Convert.ToDecimal(txtDesGrav.Text) + Convert.ToDecimal(txtReferencia.Text)), 2).ToString();
                    txtServicios.Text = "0.00";
                }
                else
                {
                    if (txtReferencia.Properties.ReadOnly)
                        txtIgvAdqDesGrav.Text = "0.00";
                    else
                        txtIgvAdqDesGrav.Text = Math.Round(calcularImpuesto(Convert.ToDecimal(txtReferencia.Text)), 2).ToString();
                }
                totalizar();
            }
        }

        private void txtDestMixto_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDestMixto.ContainsFocus)
            {
                if (Convert.ToDecimal(txtDestMixto.Text) != 0)
                {
                    txtIgvDestMixto.Text = Math.Round(calcularImpuesto(Convert.ToDecimal(txtDestMixto.Text)), 2).ToString();
                    txtServicios.Text = "0.00";
                }
                else
                {
                    //if (Convert.ToDecimal(txtIgvDestMixto.Text) != 0)
                    txtIgvDestMixto.Text = "0.00";
                }
                totalizar();
            }
        }

        private void txtDesNoGrav_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDesNoGrav.ContainsFocus)
            {
                if (Convert.ToDecimal(txtDesNoGrav.Text) != 0)
                {
                    txtIgvDesNoGrav.Text = Math.Round(calcularImpuesto(Convert.ToDecimal(txtDesNoGrav.Text)), 2).ToString();
                    txtServicios.Text = "0.00";
                }
                else
                {
                    //if (Convert.ToDecimal(txtIgvDesNoGrav.Text) != 0)
                    txtIgvDesNoGrav.Text = "0.00";
                }
                totalizar();
            }
        }

        private void txtServicios_EditValueChanged(object sender, EventArgs e)
        {
            if (txtServicios.ContainsFocus)
            {
                txtIgvAdqDesGrav.Text = Math.Round(calcularImpuesto(Convert.ToDecimal(txtDesGrav.Text) + Convert.ToDecimal(txtServicios.Text)), 2).ToString();
                txtServicios.Text = "0.00";
                totalizar();
            }
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

        private void txtReferencia_EditValueChanged(object sender, EventArgs e)
        {
            if (txtReferencia.ContainsFocus)
            {
                txtIgvAdqDesGrav.Text = Math.Round(calcularImpuesto(Convert.ToDecimal(txtDesGrav.Text) + Convert.ToDecimal(txtReferencia.Text)), 2).ToString();
                txtServicios.Text = "0.00";
                totalizar();
            }
        }
        private void txtNoGravada_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNoGravada.ContainsFocus)
                totalizar();
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {


        }

        private void txtNumeroDocumento_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNroDocumento_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtIgv_EditValueChanged(object sender, EventArgs e)
        {
            if (txtIgv.ContainsFocus)
            {
                txtIgvAdqDesGrav.Text = Math.Round(calcularImpuesto(Convert.ToDecimal(txtDesGrav.Text) + Convert.ToDecimal(txtReferencia.Text)), 2).ToString();
                txtIgvDestMixto.Text = Math.Round(calcularImpuesto(Convert.ToDecimal(txtDestMixto.Text)), 2).ToString();
                txtDesNoGrav.Text = Math.Round(calcularImpuesto(Convert.ToDecimal(txtDesNoGrav.Text)), 2).ToString();
            }
        }

        private void dtmFechaDocumento_EditValueChanged(object sender, EventArgs e)
        {
            if (dtmFechaDocumento.ContainsFocus || Status == BSMaintenanceStatus.CreateNew)
            {
                txtTipoDeCambio.Text = "0.0000";
                txtTipoDeCambio.Properties.ReadOnly = false;
                var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtmFechaDocumento.EditValue).ToShortDateString()).ToList();
                Lista.ForEach(obe =>
                {
                    txtTipoDeCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                    txtTipoDeCambio.Properties.ReadOnly = true;
                });
            }
        }

        private void txtIgvAdqDesGrav_EditValueChanged(object sender, EventArgs e)
        {
            if (txtIgvAdqDesGrav.ContainsFocus)
                totalizar();
        }

        private void txtIgvDestMixto_EditValueChanged(object sender, EventArgs e)
        {
            if (txtIgvDestMixto.ContainsFocus)
                totalizar();
        }

        private void txtIgvDesNoGrav_EditValueChanged(object sender, EventArgs e)
        {
            if (txtIgvDesNoGrav.ContainsFocus)
                totalizar();
        }

        private void txtSelcCons_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSelcCons.ContainsFocus)
                totalizar();
        }

        private decimal CalcularImpuestos()
        {
            return (Convert.ToDecimal(txtIgvAdqDesGrav.Text) + Convert.ToDecimal(txtIgvDestMixto.Text) + Convert.ToDecimal(txtIgvDesNoGrav.Text));
        }

        private void viewCuentaContableDetalle_DoubleClick(object sender, EventArgs e)
        {

        }

        private void bteNroDocRef_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarDocReferencia();
        }
        private void listarDocReferencia()
        {
            try
            {
                if (Convert.ToInt32(btnProveedor.Tag) == 0)
                    throw new ArgumentException("Seleccione el proveedor");
                FrmListarDocXPagar frm = new FrmListarDocXPagar();
                frm.intProveedor = Convert.ToInt32(btnProveedor.Tag);
                frm.flag_no_pendiente = false;
                frm.flag_docs_para_ncp = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lkpTipoDocRef.EditValue = frm._Be.tdocc_icod_tipo_doc;
                    //bteNroDocRef.Text = frm._Be.doxpc_vnumero_doc;
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bteTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(bteTipoDoc.Tag) == Parametros.intTipoDocNotaCreditoProveedor || Convert.ToInt32(bteTipoDoc.Tag) == 91)//nota de debito=91
            {
                lkpTipoDocRef.Enabled = true;
                txtseriereferencia.Enabled = true;
                txtCorrelativoreferencia.Enabled = true;
                dtFechaReferencia.Enabled = true;
            }
            else
            {
                lkpTipoDocRef.Enabled = false;
                txtseriereferencia.Enabled = false;
                txtCorrelativoreferencia.Enabled = false;
                dtFechaReferencia.Enabled = false;
            }
        }

        private void txtSerie_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSerie.Text != "")
            {
                txtNroDoc2.Enabled = false;
                txtNroDoc2.Text = String.Empty;
            }
            else
            {
                if (Convert.ToInt32(txtNumeroDocumento.Text) == 0)
                    txtNroDoc2.Enabled = true;
            }
        }

        private void txtNumeroDocumento_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(txtNumeroDocumento.Text) != 0)
            {
                txtNroDoc2.Enabled = false;
                txtNroDoc2.Text = String.Empty;
            }
            else
            {
                if (txtSerie.Text == "")
                    txtNroDoc2.Enabled = true;
            }
        }

        private void txtNroDoc2_KeyUp(object sender, KeyEventArgs e)
        {
            if (String.IsNullOrEmpty(txtNroDoc2.Text))
            {
                txtSerie.Enabled = true;
                txtNumeroDocumento.Enabled = true;
            }
            else
            {
                txtSerie.Enabled = false;
                txtNumeroDocumento.Enabled = false;
            }
        }


        private void dtmFechaDocumento_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtFechaReferencia_EditValueChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(bteTipoDoc.Tag) == 86)
            {

                if (Status == BSMaintenanceStatus.CreateNew || Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    txtTipoDeCambio.Text = "0.0000";
                    txtTipoDeCambio.Properties.ReadOnly = false;
                    var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtFechaReferencia.EditValue).ToShortDateString()).ToList();
                    Lista.ForEach(obe =>
                    {
                        txtTipoDeCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                        txtTipoDeCambio.Properties.ReadOnly = true;
                    });
                }
            }
        }

        private void btnOrdenCompra_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmListarOrdenCompraServicio OrdenCompra = new FrmListarOrdenCompraServicio();
            OrdenCompra.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
            OrdenCompra.Carga();
            if (OrdenCompra.ShowDialog() == DialogResult.OK)
            {
                btnOrdenCompra.Tag = OrdenCompra._Be.ocsc_icod_ocs;
                btnOrdenCompra.Text = OrdenCompra._Be.ocsc_vnumero_ocs;
            }
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Text = "Validando...";
            simpleButton1.Enabled = false;
            string mensaje = await ValidarCRE();
            XtraMessageBox.Show(mensaje, "Estado de Comprobante", MessageBoxButtons.OK, MessageBoxIcon.Information);
            simpleButton1.Text = "Validar CPE";
            simpleButton1.Enabled = true;
        }

        public async Task<string> ValidarCRE()
        {
            var model = new DocumentoElectronicoResponse();
            var request = new ConsultaCPERequest
            {
                numRuc = rucProveedor,
                codComp = "01",
                numeroSerie = txtSerie.Text,
                numero = Convert.ToInt32(txtNumeroDocumento.Text).ToString(),
                fechaEmision = dtmFechaDocumento.DateTime.ToString("dd/MM/yyyy"),
                monto = txtPrecioCompra.Text.Replace(",", ""),
                rucConsulta = rucProveedor
            };
            return await model.ValidarCPE(request);

        }
    }
}