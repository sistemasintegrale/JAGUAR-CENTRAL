using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmManteLetraPorCobrarDet : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteLetraPorCobrarDet));

        private BSMaintenanceStatus mStatus;

        public List<ELetraPorCobrarDet> lstDetalle = new List<ELetraPorCobrarDet>();

        public ELetraPorCobrar oBe = new ELetraPorCobrar();
        public ELetraPorCobrarDet obe = new ELetraPorCobrarDet();

        public FrmManteLetraPorCobrarDet()
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

        public void setValues()
        {
            bteCuenta.Tag = obe.ctacc_iid_cuenta_contable;
            bteCuenta.Text = (obe.ctacc_iid_cuenta_contable != null) ? obe.ctacc_iid_cuenta_contable.ToString() : "";
            txtCuentaDes.Text = obe.strDesCuenta;

            bteCCosto.Tag = obe.cecoc_icod_centro_costo;
            bteCCosto.Text = obe.strCodCCosto;
            txtcentrocosto.Text = obe.strDesCCosto;

            bteAnalitica.Tag = obe.strCodAnalitica;
            bteAnalitica.Text = obe.strCodAnalitica;
            bteSubAnalitica.Tag = obe.anac_icod_analitica;
            bteSubAnalitica.Text = obe.strCodSubAnalitica;

            txtDocumento.Text = obe.strTipoDoc;
            bteNroDocumento.Tag = obe.doxcc_icod_correlativo;
            bteNroDocumento.Text = obe.pdxcc_vnumero_doc;

            dteFechaDoc.EditValue = obe.lxcpc_sfecha_doc;
            txtMontoDoc.Text = obe.dblMontoDoc.ToString();
            txtSaldoDoc.Text = obe.dblSaldoDoc.ToString();

            txtMontoPago.Text = obe.lxcpc_nmonto_pago.ToString();
            txtConcepto.Text = obe.lxcpc_vconcepto;
            txtGlosa.Text = obe.lxcpc_vglosa;

            lkpTipoCuenta.EditValue = obe.tablc_iid_tipo_cuenta_debe_haber;
            lkpTipoCuenta.Text = obe.strDebeHaber;
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
                    lkpTipoCuenta.Enabled = true;
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
            FrmListarDocxCobrar frm = new FrmListarDocxCobrar();
            frm.intIcodCliente = oBe.cliec_icod_cliente;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                obe.tdocc_icod_tipo_doc = frm.EDocPorCobrar.tdocc_icod_tipo_doc;
                obe.tdocc_iid_clase_doc = Convert.ToInt32(frm.EDocPorCobrar.tdodc_iid_correlativo);
                obe.doxcc_icod_correlativo = Convert.ToInt64(frm.EDocPorCobrar.doxcc_icod_correlativo);
                obe.strTipoDoc = frm.EDocPorCobrar.tdocc_vabreviatura_tipo_doc;
                txtDocumento.Text = frm.EDocPorCobrar.tdocc_vabreviatura_tipo_doc;
                bteNroDocumento.Text = frm.EDocPorCobrar.doxcc_vnumero_doc;
                dteFechaDoc.EditValue = frm.EDocPorCobrar.doxcc_sfecha_doc;

                txtSaldoDoc.Text = frm.EDocPorCobrar.doxcc_nmonto_saldo.ToString();
                txtMontoDoc.Text = frm.EDocPorCobrar.doxcc_nmonto_total.ToString();

                bteCuenta.Enabled = false;
                bteCCosto.Enabled = false;
                bteAnalitica.Enabled = false;
                bteSubAnalitica.Enabled = false;
                lkpTipoCuenta.Enabled = false;
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
                    if (Convert.ToInt32(lkpTipoCuenta.EditValue) == 0)
                    {
                        oBase = lkpTipoCuenta;
                        throw new ArgumentException("Seleccione tipo de cuenta");
                    }
                }

                if (Convert.ToDecimal(txtMontoPago.Text) == 0)
                {
                    oBase = txtMontoPago;
                    throw new ArgumentException("Ingrese monto mayor a 0.00");
                }

                int? NullVal;
                NullVal = null;
                DateTime? dtNull = null;

                obe.lexcc_icod_correlativo = oBe.lexcc_icod_correlativo;
                obe.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                obe.lxcpc_nmonto_pago = Convert.ToDecimal(txtMontoPago.Text);
                obe.lxcpc_nmonto_tipo_cambio = oBe.lexcc_nmonto_tipo_cambio;
                obe.lxcpc_sfecha_doc = (dteFechaDoc.Text.Length > 0) ? Convert.ToDateTime(dteFechaDoc.Text) : dtNull;
                obe.lxcpc_sfecha_pago = (dteFechaDoc.Text.Length > 0) ? oBe.lexcc_sfecha_letra : dtNull;
                obe.lxcpc_vconcepto = txtConcepto.Text;
                obe.pdxcc_vnumero_doc = bteNroDocumento.Text;
                obe.lxcpc_tipo_pago = "D";

                obe.tablc_iid_tipo_cuenta_debe_haber = (bteCuenta.Text.Length > 0) ? Convert.ToInt32(lkpTipoCuenta.EditValue) : NullVal;
                obe.ctacc_iid_cuenta_contable = (bteCuenta.Text.Length > 0) ? Convert.ToInt32(bteCuenta.Tag) : NullVal;
                obe.cecoc_icod_centro_costo = (bteCCosto.Text.Length > 0) ? Convert.ToInt32(bteCCosto.Tag) : NullVal;
                obe.anac_icod_analitica = (bteSubAnalitica.Text.Length > 0) ? Convert.ToInt32(bteSubAnalitica.Tag) : NullVal;

                obe.strCodCCosto = bteCCosto.Text;
                obe.strCodAnalitica = bteAnalitica.Text;
                obe.strCodSubAnalitica = bteSubAnalitica.Text;
                obe.strDebeHaber = lkpTipoCuenta.Text;

                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.intTipoOperacion = 1;
                    lstDetalle.Add(obe);
                }
                else
                {
                    if (obe.intTipoOperacion != 1)
                        obe.intTipoOperacion = 2;
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
            cargar();
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
            cargaLkp();

        }

        private void cargaLkp()
        {
            List<ETipoCuenta> lstTipoCuenta = new List<ETipoCuenta>();
            for (int i = 0; i < 2; i++)
            {
                ETipoCuenta obeTipoCta = new ETipoCuenta();
                obeTipoCta.intTipoCta = i + 1;
                obeTipoCta.strTipoCta = (obeTipoCta.intTipoCta == 1) ? "Debe" : "Haber";
                lstTipoCuenta.Add(obeTipoCta);
            }

            BSControls.LoaderLook(lkpTipoCuenta, lstTipoCuenta, "strTipoCta", "intTipoCta", false);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                setValues();
            }
        }


        public class ETipoCuenta
        {
            public int intTipoCta { get; set; }
            public string strTipoCta { get; set; }
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
            dteFechaDoc.EditValue = null;
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

                bteNroDocumento.Enabled = false;

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