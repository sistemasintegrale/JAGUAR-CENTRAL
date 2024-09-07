using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class FrmManteCtaxCobrar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteMovVariosDet));

        public int intIdCliente = 0;
        public int intIdProveedor = 0;
        public int? intIdAnaliticaProveedor;

        public int? intIdTipoDocumento;
        public int? intIdClaseTipoDocumento;
        public decimal TipoCambio = 0;
        private BSMaintenanceStatus mStatus;
        public List<ELibroBancosDetalle> oDetailList = new List<ELibroBancosDetalle>();

        public ELibroBancosDetalle Obj = new ELibroBancosDetalle();
        public int tip_mon = 0;
        public int Cab_icod_correlativo = 0;

        public string iid_relacion;
        public int? icod_tipo_relacion;
        public int? intIdAnaliticaCliente;
        public bool cta_cobrar = false;
        public bool adelanto_prov = false;

        public long? intIdDocPorCobrar;
        public long? intIdDocPorPagar;
        public long? intIdAdelantoPago_P;
        public long? intIdNotaCreditoPago_P;
        public long? intIdDocumentoPorPagarPago;
        public long? intIdDocPorPagar_AdelNC;
        public long? intIdDocPorCobrar_AdelNC;
        public long? intIdAdelantoPago_C;
        public long? intIdNotaCreditoPago_C;


        public FrmManteCtaxCobrar()
        {
            InitializeComponent();
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteCuenta.Enabled = false;
                btnAgregar.Enabled = false;
                bteNroDocumento.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                btnModificar.Enabled = false;
            }
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            CargaModify();
        }
        private void CargaModify()
        {
            bteCuenta.Tag = Obj.iid_cuenta_contable;
            bteCuenta.Text = Obj.NumeroCuentaContable;
            txtCuentaDes.Text = Obj.DescripcionCuentaContable;

            bteAnalitica.Tag = Obj.tablc_icod_tipo_analitica;
            bteAnalitica.Text = string.Format("{0:00}", Obj.tablc_icod_tipo_analitica);

            bteSubAnalitica.Tag = Obj.icod_analitica;
            bteSubAnalitica.Text = Obj.anac_iid_analitica;

            bteCCosto.Tag = Obj.icod_centro_costo;
            bteCCosto.Text = Obj.CodigoCentroCosto;

            txtDocumento.Text = Obj.tdocc_vabreviatura_tipo_doc;
            bteNroDocumento.Text = Obj.vnumero_doc;

            txtMontoSoles.Text = Obj.mto_mov_soles.ToString();
            txtMontoDolares.Text = Obj.mto_mov_dolar.ToString();

            txtConcepto.Text = Obj.vglosa;


            deFechaPago.EditValue = (Obj.doxcc_sfecha_doc == null) ? Obj.doxpc_sfecha_doc : Obj.doxcc_sfecha_doc;

            intIdTipoDocumento = Obj.tdocc_icod_tipo_doc;
            intIdClaseTipoDocumento = Obj.tdodc_iid_correlativo;

            intIdAdelantoPago_P = Obj.adprov_icod_pago;
            intIdNotaCreditoPago_P = Obj.ncprov_icod_pago;
            intIdDocPorPagar = Obj.doxpc_icod_correlativo;
            intIdDocPorPagar_AdelNC = Obj.doxpc_icod_documento;
            //*-*-*-*-*-*//
            intIdDocPorCobrar = Obj.doxcc_icod_correlativo;
            intIdDocPorCobrar_AdelNC = Obj.docxc_icod_documento;
            intIdAdelantoPago_C = Obj.adclie_icod_pago;
            intIdNotaCreditoPago_C = Obj.ncclie_icod_pago;
        }
        private void clear()
        {
            bteCCosto.Text = string.Empty;
            bteCCosto.Tag = null;
            txtcentrocosto.Text = string.Empty;
            bteAnalitica.Text = string.Empty;
            bteAnalitica.Tag = null;
            bteSubAnalitica.Text = string.Empty;
            bteSubAnalitica.Tag = null;
        }
        private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCuenta();
        }
        private void ListarCuenta()
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clear();
                    bteCuenta.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;
                    bteCCosto.Enabled = frm._Be.ctacc_iccosto;

                    if (frm._Be.tablc_iid_tipo_analitica != 0)
                    {
                        bteAnalitica.Enabled = true;
                        bteSubAnalitica.Enabled = true;
                        bteAnalitica.Tag = frm._Be.tablc_iid_tipo_analitica;
                        bteAnalitica.Text = string.Format("{0:00}", frm._Be.tablc_iid_tipo_analitica);
                    }
                    else
                    {
                        bteAnalitica.Enabled = false;
                        bteSubAnalitica.Enabled = false;
                    }
                    bteNroDocumento.Enabled = false;
                }
            }
        }
        private void bteCCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCentroCosto();
        }
        private void ListarCentroCosto()
        {
            using (frmListarCentroCosto Ccosto = new frmListarCentroCosto())
            {
                if (Ccosto.ShowDialog() == DialogResult.OK)
                {
                    bteCCosto.Text = Ccosto._Be.cecoc_vcodigo_centro_costo;
                    bteCCosto.Tag = Ccosto._Be.cecoc_icod_centro_costo;
                    txtcentrocosto.Text = Ccosto._Be.cecoc_vdescripcion;
                }
            }
        }

        private void bteAnalitica_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarAnalitica();
        }
        private void ListarAnalitica()
        {
            using (frmListarAnalitica frm = new frmListarAnalitica())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAnalitica.Tag = frm._Be.tarec_icorrelativo_registro;
                    bteAnalitica.Text = String.Format("{0:00}", frm._Be.tarec_icorrelativo_registro);

                    bteSubAnalitica.Tag = null;
                    bteSubAnalitica.Text = string.Empty;
                }
            }
        }
        private void bteSubAnalitica_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarSubAnalitica();
        }
        private void ListarSubAnalitica()
        {
            using (frmListarAnaliticaDetalle frm = new frmListarAnaliticaDetalle())
            {
                frm.intTipoAnalitica = Convert.ToInt32(bteAnalitica.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteSubAnalitica.Tag = frm._Be.anad_icod_analitica;
                    bteSubAnalitica.Text = frm._Be.anad_vdescripcion;
                }
            }
        }
        private void bteNroDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (adelanto_prov == true)
            {
                if (intIdProveedor == 0)
                {
                    XtraMessageBox.Show("Seleccione un proveedor", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                FrmListarDocumentoPorPagarProveedor frm = new FrmListarDocumentoPorPagarProveedor();
                frm.intIcodProveedor = intIdProveedor;
                frm.bDocFacBol = false;
                frm.intMoneda = tip_mon;
                frm.Carga();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    intIdTipoDocumento = Convert.ToInt32(frm._oBe.tdocc_icod_tipo_doc);
                    intIdClaseTipoDocumento = Convert.ToInt32(frm._oBe.tdodc_iid_correlativo);
                    intIdDocPorPagar = Convert.ToInt64(frm._oBe.doxpc_icod_correlativo);
                    //intIdDocPorPagar_AdelNC = Convert.ToInt32(frm._oBe.doxpc_icod_documento);                    
                    txtDocumento.Text = frm._oBe.tdocc_vabreviatura_tipo_doc;
                    bteNroDocumento.Text = frm._oBe.doxpc_vnumero_doc;
                    deFechaPago.EditValue = frm._oBe.doxpc_sfecha_doc;

                    bteAnalitica.Text = string.Format("{0:00}", icod_tipo_relacion);
                    bteAnalitica.Tag = icod_tipo_relacion;

                    bteSubAnalitica.Text = iid_relacion;
                    bteSubAnalitica.Tag = intIdAnaliticaProveedor;

                    if (frm._oBe.tablc_iid_tipo_moneda == 3)
                    {
                        txtSaldoSoles.Text = frm._oBe.doxpc_nmonto_total_saldo.ToString();
                        txtSaldoDolares.Text = Math.Round(Convert.ToDecimal(frm._oBe.doxpc_nmonto_total_saldo / TipoCambio), 2).ToString();
                    }
                    else
                    {
                        txtSaldoDolares.Text = frm._oBe.doxpc_nmonto_total_saldo.ToString();
                        txtSaldoSoles.Text = Math.Round(Convert.ToDecimal(frm._oBe.doxpc_nmonto_total_saldo * TipoCambio), 2).ToString();
                    }
                    bteCuenta.Enabled = false;
                    bteCCosto.Enabled = false;
                    bteAnalitica.Enabled = false;
                    bteSubAnalitica.Enabled = false;
                }
            }
            else
            {
                if (intIdCliente == 0)
                {
                    XtraMessageBox.Show("Seleccione un cliente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                FrmListarDocxCobrar frm = new FrmListarDocxCobrar();
                if (cta_cobrar == false) frm.bDocFacBol = false;
                frm.intIcodCliente = intIdCliente;


                if (frm.ShowDialog() == DialogResult.OK)
                {
                    intIdTipoDocumento = frm.EDocPorCobrar.tdocc_icod_tipo_doc;
                    intIdClaseTipoDocumento = Convert.ToInt32(frm.EDocPorCobrar.tdodc_iid_correlativo);
                    intIdDocPorCobrar = Convert.ToInt64(frm.EDocPorCobrar.doxcc_icod_correlativo);
                    //intIdDocPorCobrar_AdelNC = Convert.ToInt32(frm.EDocPorCobrar.docxc_icod_documento);         
                    txtDocumento.Text = frm.EDocPorCobrar.tdocc_vabreviatura_tipo_doc;
                    bteNroDocumento.Text = frm.EDocPorCobrar.doxcc_vnumero_doc;
                    deFechaPago.EditValue = frm.EDocPorCobrar.doxcc_sfecha_doc;

                    bteAnalitica.Text = string.Format("{0:00}", icod_tipo_relacion);
                    bteAnalitica.Tag = icod_tipo_relacion;

                    bteSubAnalitica.Text = iid_relacion;
                    bteSubAnalitica.Tag = intIdAnaliticaCliente;

                    if (frm.EDocPorCobrar.tablc_iid_tipo_moneda == 3)
                    {
                        txtSaldoSoles.Text = frm.EDocPorCobrar.doxcc_nmonto_saldo.ToString();
                        txtSaldoDolares.Text = Math.Round(Convert.ToDecimal(frm.EDocPorCobrar.doxcc_nmonto_saldo / TipoCambio), 2).ToString();
                    }
                    else
                    {
                        txtSaldoDolares.Text = frm.EDocPorCobrar.doxcc_nmonto_saldo.ToString();
                        txtSaldoSoles.Text = Math.Round(Convert.ToDecimal(frm.EDocPorCobrar.doxcc_nmonto_saldo * TipoCambio), 2).ToString();
                    }

                    bteCuenta.Enabled = false;
                    bteCCosto.Enabled = false;
                    bteAnalitica.Enabled = false;
                    bteSubAnalitica.Enabled = false;
                }
            }


        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (bteCuenta.Tag != null)
                {
                    if (bteCCosto.Enabled == true)
                    {
                        if (bteCCosto.Tag == null)
                        {
                            oBase = bteCCosto;
                            throw new ArgumentException("Seleccione Centro de Costo");
                        }
                    }
                    if (bteAnalitica.Enabled == true)
                    {
                        if (bteAnalitica.Tag == null)
                        {
                            oBase = bteAnalitica;
                            throw new ArgumentException("Selecciones Analítica");
                        }
                        if (bteSubAnalitica.Tag == null)
                        {
                            oBase = bteSubAnalitica;
                            throw new ArgumentException("Seleccione Sub - Analítca");
                        }
                    }
                }
                if (Convert.ToDecimal(txtMontoSoles.Text) == 0 && Convert.ToDecimal(txtMontoDolares.Text) == 0)
                {
                    if (tip_mon == 3)
                    {
                        oBase = txtMontoSoles;
                        throw new ArgumentException("Ingrese monto");
                    }
                    if (tip_mon == 4)
                    {
                        oBase = txtMontoDolares;
                        throw new ArgumentException("Ingrese monto");
                    }
                }

                int? NullVal;
                NullVal = null;

                long? NullVal2;
                NullVal2 = null;

                Obj.iid_cuenta_contable = (bteCuenta.Tag != null) ? Convert.ToInt32(bteCuenta.Tag) : NullVal;
                Obj.tablc_icod_tipo_analitica = (bteAnalitica.Text == "") ? NullVal : Convert.ToInt32(bteAnalitica.Text);
                Obj.icod_analitica = (bteSubAnalitica.Tag != null) ? Convert.ToInt32(bteSubAnalitica.Tag) : NullVal;
                Obj.anac_iid_analitica = (bteSubAnalitica.Tag != null) ? bteSubAnalitica.Text : "";
                Obj.icod_centro_costo = (bteCCosto.Tag != null) ? Convert.ToInt32(bteCCosto.Tag) : NullVal;

                Obj.icod_correlativo_cabecera = Cab_icod_correlativo;
                Obj.tdocc_icod_tipo_doc = intIdTipoDocumento;
                Obj.tdodc_iid_correlativo = intIdClaseTipoDocumento;
                Obj.vnumero_doc = bteNroDocumento.Text;
                Obj.doxpc_vnumero_doc = bteNroDocumento.Text;
                Obj.mobdc_icod_cliente = (adelanto_prov) ? NullVal : intIdCliente;
                Obj.mobdc_icod_proveedor = (adelanto_prov) ? intIdProveedor : NullVal;

                Obj.mto_mov_soles = Convert.ToDecimal(txtMontoSoles.Text);
                Obj.mto_mov_dolar = Convert.ToDecimal(txtMontoDolares.Text);
                Obj.mto_retenido_soles = 0;
                Obj.mto_retenido_dolar = 0;
                Obj.mto_detalle_soles = 0;
                Obj.mto_detalle_dolar = 0;
                Obj.vglosa = txtConcepto.Text;
                Obj.iusuario_crea = Valores.intUsuario;
                Obj.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                Obj.mobdc_flag_estado = true;
                Obj.doxcc_icod_correlativo = (intIdDocPorCobrar != 0) ? intIdDocPorCobrar : NullVal2;
                Obj.mobdc_iid_anio = Parametros.intEjercicio;
                Obj.mobdc_iid_mes_periodo = Convert.ToDateTime(deFechaPago.EditValue).Month;

                Obj.NumeroCuentaContable = bteCuenta.Text;
                Obj.DescripcionCuentaContable = txtCuentaDes.Text;
                Obj.DescripcionCentroCosto = bteCCosto.Text;
                Obj.docxc_icod_documento = intIdDocPorCobrar_AdelNC;
                Obj.adclie_icod_pago = intIdAdelantoPago_C;
                Obj.ncclie_icod_pago = intIdNotaCreditoPago_C;

                Obj.mobdc_iid_anio = Parametros.intEjercicio;
                Obj.mobdc_iid_mes_periodo = Convert.ToDateTime(deFechaPago.EditValue).Month;

                if (txtMontoSoles.Enabled == true)
                    Obj.tablc_iid_tipo_moneda = 3;
                if (txtMontoDolares.Enabled == true)
                    Obj.tablc_iid_tipo_moneda = 4;


                if (deFechaPago.EditValue != null)
                {
                    if (adelanto_prov)
                        Obj.doxpc_sfecha_doc = Convert.ToDateTime(deFechaPago.EditValue);
                    else
                        Obj.doxcc_sfecha_doc = Convert.ToDateTime(deFechaPago.EditValue);
                }
                Obj.tdocc_vabreviatura_tipo_doc = txtDocumento.Text;
                Obj.doxcc_vnumero_doc = bteNroDocumento.Text;
                if (adelanto_prov)
                {
                    Obj.adprov_icod_pago = intIdAdelantoPago_P;
                    Obj.ncprov_icod_pago = intIdNotaCreditoPago_P;
                    Obj.doxpc_icod_correlativo = intIdDocPorPagar;
                    Obj.doxpc_icod_documento = intIdDocPorPagar_AdelNC;

                }
                Obj.mobdc_sfecha = null;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obj.TipOper = 1;
                    oDetailList.Add(Obj);
                }
                else
                {
                    if (Obj.TipOper != 1)
                        Obj.TipOper = 2;
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
                    this.DialogResult = DialogResult.OK;
            }
        }
        private void btnAgregar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FrmManteCtaxCobrar_Load(object sender, EventArgs e)
        {
            txtTipoCambio.Text = TipoCambio.ToString();
            if (tip_mon == 3)
                txtMontoDolares.Enabled = false;
            else if (tip_mon == 4)
                txtMontoSoles.Enabled = false;
            cargar();
        }

        private void txtMontoSoles_EditValueChanged(object sender, EventArgs e)
        {
            if (tip_mon == 3)
            {
                if (txtMontoSoles.Enabled == true)
                    txtMontoDolares.Text = Math.Round(Convert.ToDecimal(txtMontoSoles.Text) / TipoCambio, 2).ToString();
            }
        }

        private void txtMontoDolares_EditValueChanged(object sender, EventArgs e)
        {
            if (tip_mon == 4)
            {
                if (txtMontoDolares.Enabled == true)
                    txtMontoSoles.Text = Math.Round(Convert.ToDecimal(txtMontoDolares.Text) * TipoCambio, 2).ToString();
            }
        }
        private List<ETablaRegistro> auxA = new List<ETablaRegistro>();
        private List<ECuentaContable> aux = new List<ECuentaContable>();
        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        private List<ETablaRegistro> ListaAnalitica = new List<ETablaRegistro>();
        private List<EAnaliticaDetalle> ListaSubAnalitica = new List<EAnaliticaDetalle>();
        private List<ECentroCosto> auxCC = new List<ECentroCosto>();
        private List<ECentroCosto> ListaCentroCosto = new List<ECentroCosto>();
        private void cargar()
        {
            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            ListaAnalitica = new BGeneral().listarTablaRegistro(24);
            ListaCentroCosto = new BContabilidad().listarCentroCosto();
            LoadMask();
        }
        private void LoadMask()
        {
            List<EParametroContable> mlista = (new BContabilidad()).listarParametroContable();
            mlista.ForEach(obe =>
            {
                this.bteCuenta.Properties.Mask.BeepOnError = true;
                this.bteCuenta.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCuenta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCuenta.Properties.Mask.ShowPlaceHolders = false;
            });
        }
        private void clearcta()
        {
            txtCuentaDes.Text = string.Empty;
            bteCuenta.Tag = null;
            //
            txtcentrocosto.Text = string.Empty;
            bteCCosto.Tag = null;
            bteCCosto.Text = string.Empty;
            //                
            bteAnalitica.Tag = null;
            bteAnalitica.Text = string.Empty;
            //
            bteSubAnalitica.Tag = null;
            bteSubAnalitica.Text = string.Empty;
            //
            bteCCosto.Enabled = false;
            bteAnalitica.Enabled = false;
            bteSubAnalitica.Enabled = false;
            //
            txtDocumento.Text = string.Empty;
            bteNroDocumento.Tag = null;
            bteNroDocumento.Text = string.Empty;
            deFechaPago.EditValue = null;
        }
        private void bteCuenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (bteCuenta.Text == "")
            {
                clearcta();
                bteNroDocumento.Enabled = true;
                return;
            }

            aux = mlistCuenta.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuenta.Text.Replace(".", ""))).ToList();

            if (aux.Count == 1)
            {
                bteCuenta.Tag = aux[0].ctacc_icod_cuenta_contable;
                txtCuentaDes.Text = aux[0].ctacc_nombre_descripcion;
                bteCCosto.Enabled = aux[0].ctacc_iccosto;

                bteAnalitica.Enabled = (aux[0].tablc_iid_tipo_analitica != 0) ? true : false;
                bteSubAnalitica.Enabled = (aux[0].tablc_iid_tipo_analitica != 0) ? true : false;

                auxA = ListaAnalitica.Where(x => Convert.ToInt32(x.tarec_icorrelativo_registro) == aux[0].tablc_iid_tipo_analitica).ToList();
                if (auxA.Count == 1)
                {
                    bteAnalitica.Tag = auxA[0].tarec_icorrelativo_registro;
                    bteAnalitica.Text = String.Format("{0:00}", auxA[0].tarec_icorrelativo_registro);
                    ListaSubAnalitica = new BContabilidad().listarAnaliticaDetalle(Convert.ToInt32(bteAnalitica.Tag));
                }
                //txtDocumento.Enabled = false;
                bteNroDocumento.Enabled = false;
                //deFechaPago.Enabled = false;
            }
            else
            {
                clearcta();
            }
        }

        private void bteCCosto_KeyUp(object sender, KeyEventArgs e)
        {
            if (bteCCosto.Text == "")
                return;
            auxCC = ListaCentroCosto.Where(x => x.cecoc_vcodigo_centro_costo == bteCCosto.Text).ToList();

            if (auxCC.Count == 1)
            {
                txtcentrocosto.Text = auxCC[0].cecoc_vdescripcion;
                bteCCosto.Tag = auxCC[0].cecoc_icod_centro_costo;
            }
            else
            {
                txtcentrocosto.Text = string.Empty;
                bteCCosto.Tag = null;
            }
        }

        private void bteCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarCuenta();
        }

        private void bteCCosto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarCentroCosto();
        }

        private void bteAnalitica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarAnalitica();
        }

        private void bteSubAnalitica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarSubAnalitica();
        }
    }
}