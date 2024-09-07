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
    public partial class FrmManteCtaxPagar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteCtaxPagar));


        public long? intIdDocumentoPorPagar;
        public int? intIdTipoDocumento;
        public int? IcodProveedor;
        public int? intIdClaseTipoDocumento;
        public decimal TipoCambio = 0;
        private BSMaintenanceStatus mStatus;
        public List<ELibroBancosDetalle> lstDetalle = new List<ELibroBancosDetalle>();
        private int? icod_proveedor;
        public ELibroBancosDetalle Obj = new ELibroBancosDetalle();
        public int tip_mon = 0;
        public int Cab_icod_correlativo = 0;
        public decimal saldo = 0;
        public int? flag_exceptuado;
        public decimal mnto_total = 0;
        public int flag_retencion = 1;
        bool carga = false;
        public int? icod_enti_financiera_cuenta;
        public FrmManteCtaxPagar()
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
            carga = true;
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

            txtRetencionSoles.Text = Obj.mto_retenido_soles.ToString();
            txtRetencionDolares.Text = Obj.mto_retenido_dolar.ToString();
            if (bteCuenta.Tag == null)
            {
                txtPagoSoles.Text = (Obj.mto_mov_soles + Obj.mto_retenido_soles).ToString();
                txtPagoDolares.Text = (Obj.mto_mov_dolar + Obj.mto_retenido_dolar).ToString();
            }
            txtConcepto.Text = Obj.vglosa;

            deFechaPago.EditValue = Obj.doxpc_sfecha_doc;
            intIdDocumentoPorPagar = Obj.doxpc_icod_correlativo;
            intIdTipoDocumento = Obj.tdocc_icod_tipo_doc;
            intIdClaseTipoDocumento = Obj.tdodc_iid_correlativo;
            icod_proveedor = Obj.mobdc_icod_proveedor;

            carga = false;

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
                if (bteCuenta.Tag == null)
                {
                    if (Convert.ToDecimal(txtPagoSoles.Text) == 0 && Convert.ToDecimal(txtPagoDolares.Text) == 0)
                    {
                        if (tip_mon == 3)
                        {
                            oBase = txtPagoSoles;
                            throw new ArgumentException("Ingrese monto");
                        }
                        if (tip_mon == 4)
                        {
                            oBase = txtPagoDolares;
                            throw new ArgumentException("Ingrese monto");
                        }
                    }
                }
                else
                {
                    if (Convert.ToDecimal(txtPagoSoles.Text) == 0 && Convert.ToDecimal(txtPagoDolares.Text) == 0)
                    {
                        if (tip_mon == 3)
                        {
                            oBase = txtPagoSoles;
                            throw new ArgumentException("Ingrese monto");
                        }
                        if (tip_mon == 4)
                        {
                            oBase = txtPagoDolares;
                            throw new ArgumentException("Ingrese monto");
                        }
                    }

                    //if (Convert.ToDecimal(txtMontoSoles.Text) == 0 && Convert.ToDecimal(txtMontoDolares.Text) == 0)
                    //{
                    //    if (tip_mon == 3)
                    //    {
                    //        oBase = txtMontoSoles;
                    //        throw new ArgumentException("Ingrese monto");
                    //    }
                    //    if (tip_mon == 4)
                    //    {
                    //        oBase = txtMontoDolares;
                    //        throw new ArgumentException("Ingrese monto");
                    //    }
                    //}
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (bteNroDocumento.Text != "")
                    {
                        if (lstDetalle.Where(x => x.tdocc_icod_tipo_doc == intIdTipoDocumento
                            && x.vnumero_doc == bteNroDocumento.Text && x.mobdc_icod_proveedor == IcodProveedor).ToList().Count() > 0)
                        {
                            oBase = bteNroDocumento;
                            throw new ArgumentException("EL documento seleccionado ya se encuentra en el registro de detalle");
                        }
                    }
                }
                /**/
                if (bteCuenta.Tag != null) { Obj.iid_cuenta_contable = Convert.ToInt32(bteCuenta.Tag); }
                Obj.icod_correlativo_cabecera = Cab_icod_correlativo;
                Obj.vnumero_doc = bteNroDocumento.Text;
                Obj.mobdc_icod_cliente = null;
                Obj.mobdc_icod_proveedor = icod_proveedor;
                if (bteAnalitica.Tag != null)
                    Obj.tablc_icod_tipo_analitica = Convert.ToInt32(bteAnalitica.Text);
                if (bteSubAnalitica.Tag != null)
                    Obj.anac_iid_analitica = bteSubAnalitica.Text;

                if (bteAnalitica.Tag != null)
                {
                    Obj.tablc_icod_tipo_analitica = Convert.ToInt32(bteAnalitica.Tag);
                }
                else Obj.tablc_icod_tipo_analitica = null;

                if (bteSubAnalitica.Tag != null) { Obj.icod_analitica = Convert.ToInt32(bteSubAnalitica.Tag); }
                else Obj.icod_analitica = null;

                Obj.tdocc_icod_tipo_doc = intIdTipoDocumento;
                Obj.tdodc_iid_correlativo = intIdClaseTipoDocumento;
                Obj.mto_mov_soles = Convert.ToDecimal(txtMontoSoles.Text);
                Obj.mto_mov_dolar = Convert.ToDecimal(txtMontoDolares.Text);
                Obj.mto_retenido_soles = Convert.ToDecimal(txtRetencionSoles.Text);
                Obj.mto_retenido_dolar = Convert.ToDecimal(txtRetencionDolares.Text);
                Obj.mto_detalle_soles = Convert.ToDecimal(txtPagoSoles.Text);
                Obj.mto_detalle_dolar = Convert.ToDecimal(txtPagoDolares.Text);
                Obj.docxp_nmonto_total_documento = mnto_total;
                Obj.vglosa = txtConcepto.Text;
                if (bteCCosto.Tag != null) { Obj.icod_centro_costo = Convert.ToInt32(bteCCosto.Tag); }
                else Obj.icod_centro_costo = null;
                Obj.iusuario_crea = Valores.intUsuario;
                Obj.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                Obj.mobdc_flag_estado = true;
                //
                Obj.doxpc_icod_correlativo = intIdDocumentoPorPagar;
                Obj.NumeroCuentaContable = bteCuenta.Text;
                Obj.DescripcionCuentaContable = txtCuentaDes.Text;
                Obj.DescripcionCentroCosto = bteCCosto.Text;
                if (deFechaPago.EditValue != null) { Obj.doxpc_sfecha_doc = Convert.ToDateTime(deFechaPago.EditValue); }
                else { Obj.doxpc_sfecha_doc = null; }
                Obj.tdocc_vabreviatura_tipo_doc = txtDocumento.Text;
                Obj.doxcc_vnumero_doc = bteNroDocumento.Text;

                Obj.mobdc_sfecha = null;

                Obj.mobdc_iid_anio = Parametros.intEjercicio;
                Obj.mobdc_iid_mes_periodo = Convert.ToDateTime(deFechaPago.EditValue).Month;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obj.TipOper = 1;
                    lstDetalle.Add(Obj);
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

                    bteNroDocumento.Enabled = false;
                    txtPagoSoles.Enabled = false;
                    txtPagoDolares.Enabled = false;
                    txtRetencionSoles.Enabled = false;
                    txtRetencionDolares.Enabled = false;
                    if (tip_mon == 3)
                    {
                        txtPagoSoles.Enabled = true;
                        txtPagoDolares.Enabled = false;
                    }
                    else
                    {
                        txtPagoDolares.Enabled = true;
                        txtPagoSoles.Enabled = false;
                    }
                    ClearMontos();
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
            ListarDoc();
        }
        private void ListarDoc()
        {
            FrmListarDocumentoPorPagarProveedor frm = new FrmListarDocumentoPorPagarProveedor();
            frm.flag_list_all = true;
            frm.bDocFacBol = false;
            frm.intMoneda = tip_mon;
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                intIdDocumentoPorPagar = Convert.ToInt64(frm._oBe.doxpc_icod_correlativo);
                intIdTipoDocumento = Convert.ToInt32(frm._oBe.tdocc_icod_tipo_doc);
                IcodProveedor = Convert.ToInt32(frm._oBe.proc_icod_proveedor);
                intIdClaseTipoDocumento = Convert.ToInt32(frm._oBe.tdodc_iid_correlativo);
                txtDocumento.Text = frm._oBe.tdocc_vabreviatura_tipo_doc;
                bteNroDocumento.Text = frm._oBe.doxpc_vnumero_doc;
                deFechaPago.EditValue = frm._oBe.doxpc_sfecha_doc;
                mnto_total = Convert.ToDecimal(frm._oBe.doxpc_nmonto_total_documento);

                bteAnalitica.Text = string.Format("{0:00}", frm._oBe.tipo_analitica);
                bteAnalitica.Tag = frm._oBe.tipo_analitica;

                bteSubAnalitica.Text = frm._oBe.anac_iid_analitica;
                bteSubAnalitica.Tag = frm._oBe.anac_icod_analitica;

                icod_proveedor = frm._oBe.proc_icod_proveedor;

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

                ClearMontos();
                check_retencion();
                if (mnto_total > 700)
                { flag_retencion = 1; enable_retencion(); }
            }
        }
        private void ClearMontos()
        {
            txtMontoSoles.Text = "0.00";
            txtMontoDolares.Text = "0.00";
            txtRetencionSoles.Text = "0.00";
            txtRetencionDolares.Text = "0.00";
            txtPagoSoles.Text = "0.00";
            txtPagoDolares.Text = "0.00";
        }
        private void btnAgregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void txtMontoSoles_EditValueChanged(object sender, EventArgs e)
        {
            if (carga == true)
                return;
            if (txtPagoSoles.Enabled == true && bteCuenta.Tag == null)
            {
                txtRetencionSoles.Text = (txtRetencionSoles.Enabled == true) ? (Convert.ToDecimal(txtPagoSoles.Text) * Convert.ToDecimal(Parametros.dblPorRetencion)).ToString() : "0.00";
                txtMontoSoles.Text =
                    (Convert.ToDecimal(txtPagoSoles.Text) - Convert.ToDecimal(txtRetencionSoles.Text)).ToString();
                if (tip_mon == 3)
                {
                    txtPagoDolares.Text =
                        Math.Round(Convert.ToDecimal(txtPagoSoles.Text) / TipoCambio, 2).ToString();
                    txtMontoDolares.Text =
                   (Convert.ToDecimal(txtPagoDolares.Text) - Convert.ToDecimal(txtRetencionDolares.Text)).ToString();
                }

            }
            else if (txtPagoSoles.Enabled == true && bteCuenta.Tag != null)
            {
                if (tip_mon == 3)
                    txtPagoDolares.Text =
                        Math.Round(Convert.ToDecimal(txtPagoSoles.Text) / TipoCambio, 2).ToString();
            }
        }

        private void txtMontoDolares_EditValueChanged(object sender, EventArgs e)
        {
            if (carga == true)
                return;
            if (txtPagoDolares.Enabled == true && bteCuenta.Tag == null)
            {
                txtRetencionDolares.Text = (Convert.ToDecimal(txtPagoDolares.Text) * Convert.ToDecimal(Parametros.dblPorRetencion)).ToString();
                txtMontoDolares.Text =
                    (Convert.ToDecimal(txtPagoDolares.Text) - Convert.ToDecimal(txtRetencionDolares.Text)).ToString();
                if (tip_mon == 4)
                {
                    txtPagoSoles.Text =
                        Math.Round(Convert.ToDecimal(txtPagoDolares.Text) * TipoCambio, 2).ToString();
                    txtMontoSoles.Text =
                    (Convert.ToDecimal(txtPagoSoles.Text) - Convert.ToDecimal(txtRetencionSoles.Text)).ToString();
                }
            }
            else if (txtPagoDolares.Enabled == true && bteCuenta.Tag != null)
            {
                if (tip_mon == 4)
                    txtPagoSoles.Text =
                        Math.Round(Convert.ToDecimal(txtPagoDolares.Text) * TipoCambio, 2).ToString();
            }
        }
        private void check_enable()
        {
            txtPagoSoles.Enabled = (tip_mon == 3 && bteCuenta.Tag == null) ? true : false;
            txtPagoDolares.Enabled = (tip_mon == 4 && bteCuenta.Tag == null) ? true : false;
            txtRetencionSoles.Enabled = (tip_mon == 3) ? true : false;
            txtRetencionDolares.Enabled = (tip_mon == 4) ? true : false;
        }
        private void FrmManteCtaxPagar_Load(object sender, EventArgs e)
        {
            txtTipoCambio.Text = TipoCambio.ToString();
            check_enable();
            check_retencion();
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                if (tip_mon == 3)
                {
                    txtRetencionSoles.Enabled = (txtRetencionSoles.Text != "0.00") ? true : false;
                    txtRetencionDolares.Enabled = false;
                }
                if (tip_mon == 4)
                {
                    txtRetencionDolares.Enabled = (txtRetencionDolares.Text != "0.00") ? true : false;
                    txtRetencionSoles.Enabled = false;
                }
                if (bteCuenta.Tag != null)
                {
                    if (tip_mon == 3)
                    {
                        txtMontoSoles.Enabled = true;
                        txtMontoDolares.Enabled = false;
                    }
                    if (tip_mon == 4)
                    {
                        txtMontoSoles.Enabled = false;
                        txtMontoDolares.Enabled = true;
                    }
                }
            }
            cargar();
        }
        private void check_retencion()
        {
            if (flag_exceptuado == 1 || txtDocumento.Text.Trim() == "RHO" || txtDocumento.Text.Trim() == "LCO" ||
                mnto_total <= 700)
            {
                flag_retencion = 0;
                enable_retencion();
            }
        }
        private void enable_retencion()
        {
            if (flag_retencion == 0)
            {
                txtRetencionSoles.Enabled = false;
                txtRetencionDolares.Enabled = false;
            }
            else
            {
                if (tip_mon == 3)
                    txtRetencionSoles.Enabled = true;
                else if (tip_mon == 4)
                    txtRetencionDolares.Enabled = true;
            }
        }

        private void txtRetencionSoles_EditValueChanged(object sender, EventArgs e)
        {
            if (carga == true)
                return;
            if (txtRetencionSoles.Enabled == true)
            {
                txtMontoSoles.Text =
                    (Convert.ToDecimal(txtPagoSoles.Text) - Convert.ToDecimal(txtRetencionSoles.Text)).ToString();
                txtRetencionDolares.Text = (Math.Round(Convert.ToDecimal(txtRetencionSoles.Text) / TipoCambio, 2)).ToString();
                txtMontoDolares.Text =
                    (Convert.ToDecimal(txtPagoDolares.Text) - Convert.ToDecimal(txtRetencionDolares.Text)).ToString();
            }
        }

        private void txtRetencionDolares_EditValueChanged(object sender, EventArgs e)
        {
            if (carga == true)
                return;
            if (txtRetencionDolares.Enabled == true)
            {
                txtMontoDolares.Text =
                     (Convert.ToDecimal(txtPagoDolares.Text) - Convert.ToDecimal(txtRetencionDolares.Text)).ToString();
                txtRetencionSoles.Text = (Math.Round(Convert.ToDecimal(txtRetencionDolares.Text) * TipoCambio, 2)).ToString();
                txtMontoSoles.Text =
                    (Convert.ToDecimal(txtPagoSoles.Text) - Convert.ToDecimal(txtRetencionSoles.Text)).ToString();
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
                check_enable();
                check_retencion();
                txtMontoDolares.Enabled = false;
                txtMontoSoles.Enabled = false;
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

                bteNroDocumento.Enabled = false;

                bteNroDocumento.Enabled = false;
                txtPagoSoles.Enabled = false;
                txtPagoDolares.Enabled = false;
                txtRetencionSoles.Enabled = false;
                txtRetencionDolares.Enabled = false;
                if (tip_mon == 3)
                {
                    txtMontoSoles.Enabled = true;
                    txtMontoDolares.Enabled = false;
                }
                else
                {
                    txtMontoDolares.Enabled = true;
                    txtMontoSoles.Enabled = false;
                }
                ClearMontos();
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

        private void bteNroDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                ListarDoc();
            }
        }

        private void txtMontoSoles_EditValueChanged_1(object sender, EventArgs e)
        {
            if (carga == true)
                return;
            if (bteCuenta.Text.Length > 0)
                if (tip_mon == 3)
                    txtMontoDolares.Text =
                            Math.Round(Convert.ToDecimal(txtMontoSoles.Text) / TipoCambio, 2).ToString();
        }

        private void txtMontoDolares_EditValueChanged_1(object sender, EventArgs e)
        {
            if (carga == true)
                return;
            if (bteCuenta.Text.Length > 0)
                if (tip_mon == 4)
                    txtMontoSoles.Text =
                            Math.Round(Convert.ToDecimal(txtMontoDolares.Text) * TipoCambio, 2).ToString();
        }
    }
}