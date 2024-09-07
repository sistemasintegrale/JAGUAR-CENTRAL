using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema.Listados;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmMantePagoDirectoDocumentosXPagar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantePagoDirectoDocumentosXPagar));

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public EDocPorPagar obeDocXPagar = new EDocPorPagar();
        public EDocPorPagarPago obeDocXPagarPago = new EDocPorPagarPago();
        public decimal saldoGral;
        public decimal pagoGral;

        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();

        //****
        private List<ETablaRegistro> auxA = new List<ETablaRegistro>();
        private List<ECuentaContable> aux = new List<ECuentaContable>();
        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        private List<ETablaRegistro> ListaAnalitica = new List<ETablaRegistro>();
        private List<EAnaliticaDetalle> ListaSubAnalitica = new List<EAnaliticaDetalle>();
        private List<ECentroCosto> auxCC = new List<ECentroCosto>();
        private List<ECentroCosto> ListaCentroCosto = new List<ECentroCosto>();
        //**** 

        private BCuentasPorPagar Obl;

        public FrmMantePagoDirectoDocumentosXPagar()
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
            bool Enabled = (Status == BSMaintenanceStatus.CreateNew);

            bteAnalitica.Enabled = Enabled;
            txtNumeroDocumento.Properties.ReadOnly = !Enabled;
            txtTipoCambio.Properties.ReadOnly = true;

            Enabled = (Status == BSMaintenanceStatus.View);

            LkpTipoMoneda.Properties.ReadOnly = Enabled;
            txtMonto.Properties.ReadOnly = Enabled;
            deFechaDocumento.Properties.ReadOnly = Enabled;
            txtObservacion.Properties.ReadOnly = Enabled;
        }

        private void FrmMantePagoDirectoDocumentosXPagar_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        private void FrmMantePagoDirectoDocumentosXPagar_Load(object sender, EventArgs e)
        {
            lblDocumentoXCobrar.Text = "Documento por Pagar:  " + obeDocXPagar.tdocc_vabreviatura_tipo_doc + "-" + obeDocXPagar.doxpc_vnumero_doc;
            ImprimirSaldo();
            if (Status == BSMaintenanceStatus.CreateNew)
                deFechaDocumento.EditValue = DateTime.Now.ToShortDateString();
            bteTipoDocumento.Tag = 56;
            bteTipoDocumento.Text = "VCO";

        }

        private void ImprimirSaldo()
        {
            string cad;
            cad = "Saldo:  " + obeDocXPagar.vMoneda + " " + saldoGral + "\t";
            if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
                cad += ((Convert.ToInt16(obeDocXPagar.tablc_iid_tipo_moneda) == Parametros.intSoles) ? ("US$ " + CalcularSaldoXMoneda(Parametros.intDolares, 1).ToString()) : ("S/. " + CalcularSaldoXMoneda(Parametros.intSoles, 1)));
            lblSaldo.Text = cad;
        }

        public void cargar()
        {
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            BSControls.LoaderLook(LkpTipoMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);


            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            ListaAnalitica = new BGeneral().listarTablaRegistro(24);
            ListaCentroCosto = new BContabilidad().listarCentroCosto();
            LoadMask();
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                LkpTipoMoneda.ItemIndex = obeDocXPagarPago.tablc_iid_tipo_moneda - 1;
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


        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private decimal CalcularSaldoXMoneda(int tipo_moneda, int operacion)
        {
            decimal saldo = 0;

            if (obeDocXPagar.tablc_iid_tipo_moneda != tipo_moneda)
            {
                if (tipo_moneda == Parametros.intSoles)
                {
                    if (operacion == 1)
                        saldo = Convert.ToDecimal(saldoGral) * Convert.ToDecimal(txtTipoCambio.Text);
                    else
                        if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
                        saldo = Convert.ToDecimal(txtMonto.Text) / Convert.ToDecimal(txtTipoCambio.Text);
                }
                else
                {
                    if (operacion == 1)
                    {
                        if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
                            saldo = Convert.ToDecimal(saldoGral) / Convert.ToDecimal(txtTipoCambio.Text);
                    }
                    else
                    {
                        saldo = Convert.ToDecimal(txtMonto.Text) * Convert.ToDecimal(txtTipoCambio.Text);
                    }
                }
            }
            else
                saldo = (operacion == 1) ? Convert.ToDecimal(saldoGral) : Convert.ToDecimal(txtMonto.Text);

            return Math.Round(saldo, 2);
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EDocPorPagarPago oBe = new EDocPorPagarPago();
            Obl = new BCuentasPorPagar();
            try
            {
                if (bteTipoDocumento.Tag == null)
                {
                    oBase = bteTipoDocumento;
                    throw new ArgumentException("Seleccione un tipo de documento");
                }
                if (txtNumeroDocumento.Text == "")
                {
                    oBase = txtNumeroDocumento;
                    throw new ArgumentException("Ingrese el número del documento");
                }
                if (deFechaDocumento.EditValue == null)
                {
                    oBase = deFechaDocumento;
                    throw new ArgumentException("Seleccione la fecha del Pago");
                }
                if (deFechaDocumento.DateTime < obeDocXPagar.doxpc_sfecha_doc)
                {
                    oBase = deFechaDocumento;
                    throw new ArgumentException("La fecha del Pago no debe ser menor a la fecha del Documento por Cobrar");
                }
                if (Convert.ToDecimal(txtMonto.Text) == 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("Ingrese el monto");
                }
                //if (Convert.ToDecimal(txtMonto.Text) < 0)
                //{
                //    oBase = txtMonto;
                //    throw new ArgumentException("El monto ingresado es negativo");
                //}
                if (Convert.ToDecimal(txtTipoCambio.Text) == 0)
                {
                    oBase = txtTipoCambio;
                    throw new ArgumentException("Ingrese el tipo de cambio");
                }

                if (string.IsNullOrWhiteSpace(txtObservacion.Text))
                {
                    oBase = txtObservacion;
                    throw new ArgumentException("Ingrese una observación");
                }

                if (bteCuenta.Tag == null)
                {
                    oBase = bteCuenta;
                    throw new ArgumentException("Seleccione Cuenta Contable");
                }

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
                        throw new ArgumentException("Seleccione Analítica");
                    }
                    if (bteSubAnalitica.Tag == null)
                    {
                        oBase = bteSubAnalitica;
                        throw new ArgumentException("Seleccione Sub - Analítca");
                    }
                }

                decimal saldo = CalcularSaldoXMoneda(Convert.ToInt16(LkpTipoMoneda.EditValue), 2);

                //verificar saldo
                if (CalcularSaldoXMoneda(Convert.ToInt16(LkpTipoMoneda.EditValue), 1) < Convert.ToDecimal(txtMonto.Text))
                {
                    oBase = txtMonto;
                    throw new ArgumentException("El monto ingresado supera al saldo");
                }
                else
                {
                    oBe.pdxpc_nmonto_pago = Convert.ToDecimal(txtMonto.Text);
                    if (CalcularSaldoXMoneda(Convert.ToInt16(LkpTipoMoneda.EditValue), 1) == Convert.ToDecimal(txtMonto.Text))
                        oBe.pdxpc_nmonto_pago_dxp = Convert.ToDecimal(saldoGral);
                    else
                        oBe.pdxpc_nmonto_pago_dxp = saldo;
                }

                oBe.vcocc_iid_voucher_contable = obeDocXPagarPago.vcocc_iid_voucher_contable;
                oBe.doxpc_icod_correlativo = obeDocXPagar.doxpc_icod_correlativo;
                oBe.tdocc_icod_tipo_doc = Convert.ToInt32(bteTipoDocumento.Tag);
                oBe.intTipoDoc = obeDocXPagar.tdocc_icod_tipo_doc;
                oBe.strNroDoc = obeDocXPagar.doxpc_vnumero_doc;
                oBe.tdodc_iid_correlativo = obeDocXPagar.tdodc_iid_correlativo;
                /*---------------------------------------------------------------*/
                oBe.tdocc_vabreviatura_tipo_doc = bteTipoDocumento.Text;
                oBe.pdxpc_vnumero_doc = txtNumeroDocumento.Text;
                oBe.pdxpc_sfecha_pago = Convert.ToDateTime(deFechaDocumento.EditValue);
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(LkpTipoMoneda.EditValue);
                oBe.pdxpc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                oBe.pdxpc_vobservacion = txtObservacion.Text;
                oBe.efctc_icod_enti_financiera_cuenta = null;
                oBe.ctacc_iid_cuenta_contable = Convert.ToInt32(bteCuenta.Tag);
                oBe.pdxpc_mes = obeDocXPagar.mesec_iid_mes;
                oBe.proc_vcod_proveedor = obeDocXPagar.proc_icod_proveedor.ToString();
                oBe.pdxpc_flag_estado = true;
                oBe.saldoDxP = saldoGral;
                oBe.pagoDxP = pagoGral;
                oBe.anio = Parametros.intEjercicio;
                if (bteCCosto.Tag == null)
                    oBe.cecoc_icod_centro_costo = null;
                else
                    oBe.cecoc_icod_centro_costo = Convert.ToInt32(bteCCosto.Tag);
                if (bteAnalitica.Tag == null)
                {
                    oBe.anac_icod_analitica = null;
                }
                else
                {
                    oBe.anac_icod_analitica = Convert.ToInt32(bteSubAnalitica.Tag);
                }
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.pdxpc_vorigen = "D"; //Pago Directo

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.insertarDxpPagoDirecto(oBe);
                }
                else
                {
                    oBe.pdxpc_icod_correlativo = obeDocXPagarPago.pdxpc_icod_correlativo;
                    Obl.modificarDxpPagoDirecto(oBe);
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
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        private void bteTipoDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarDocumento();
        }

        private void ListarDocumento()
        {
            frmListarTipoDocumento Documentos = new frmListarTipoDocumento();
            Documentos.bModulo = true;
            Documentos.intIdModulo = Parametros.intModuloCtasPorPagar;
            if (Documentos.ShowDialog() == DialogResult.OK)
            {
                bteTipoDocumento.Tag = Documentos._Be.tdocc_icod_tipo_doc;
                bteTipoDocumento.Text = Documentos._Be.tdocc_vabreviatura_tipo_doc;
            }
            txtNumeroDocumento.Focus();
        }


        private void deFechaDocumento_EditValueChanged(object sender, EventArgs e)
        {
            txtTipoCambio.Text = "0.0000";
            txtTipoCambio.Properties.ReadOnly = false;
            var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(deFechaDocumento.EditValue).ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                txtTipoCambio.Properties.ReadOnly = true;
            });
            ImprimirSaldo();
            txtMonto.Text = CalcularSaldoXMoneda(Convert.ToInt16(LkpTipoMoneda.EditValue), 1).ToString();
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
                        bteAnalitica.Text = String.Format("{0:00}", frm._Be.tablc_iid_tipo_analitica);
                    }
                    else
                    {
                        bteAnalitica.Enabled = false;
                        bteSubAnalitica.Enabled = false;
                    }
                }
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

        private void bteCuenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (bteCuenta.Text == "")
            {
                clearcta();
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
            }
            else
            {
                clearcta();
            }
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

        private void ListarAnalitica()
        {
            using (frmListarAnalitica frm = new frmListarAnalitica())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAnalitica.Tag = frm._Be.tarec_icorrelativo_registro;//tarec_icorrelativo_registro
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
                    //bteSubAnalitica.Text = frm._Be.descripcion;
                    bteSubAnalitica.Text = frm._Be.anad_iid_analitica;
                    //ana_cod = frm._Be.iid_Analitica;
                }
            }
        }

        private void LkpTipoMoneda_EditValueChanged(object sender, EventArgs e)
        {
            if (LkpTipoMoneda.ContainsFocus)
            {
                txtMonto.Text = CalcularSaldoXMoneda(Convert.ToInt16(LkpTipoMoneda.EditValue), 1).ToString();
                ImprimirSaldo();
            }
        }

        private void txtTipoCambio_EditValueChanged(object sender, EventArgs e)
        {
            if (txtTipoCambio.ContainsFocus)
            {
                txtMonto.Text = CalcularSaldoXMoneda(Convert.ToInt16(LkpTipoMoneda.EditValue), 1).ToString();
                ImprimirSaldo();
            }
        }

        private void bteAnalitica_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarAnalitica();
        }


    }
}