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

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmMantePagoDirectoDocumentosXCobrar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantePagoDirectoDocumentosXCobrar));

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public EDocXCobrar obeDocXCobrar = new EDocXCobrar();
        public EDocXCobrarPago _BE = new EDocXCobrarPago();
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

        private BCuentasPorCobrar Obl;

        public FrmMantePagoDirectoDocumentosXCobrar()
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                LkpTipoMoneda.Enabled = false;
                bteTipoDocumento.Enabled = false;
            }
        }
        public void SetValues()
        {
            if (_BE.tablc_iid_tipo_moneda == obeDocXCobrar.tablc_iid_tipo_moneda)
            {
                saldoGral = Convert.ToDecimal(obeDocXCobrar.doxcc_nmonto_saldo + _BE.pdxcc_nmonto_cobro);
                pagoGral = Convert.ToDecimal(obeDocXCobrar.doxcc_nmonto_pagado - _BE.pdxcc_nmonto_cobro);
            }
            else
            {
                if (obeDocXCobrar.tablc_iid_tipo_moneda == Parametros.intSoles)
                {
                    saldoGral = Convert.ToDecimal(obeDocXCobrar.doxcc_nmonto_saldo + (_BE.pdxcc_nmonto_cobro * _BE.pdxcc_nmonto_tipo_cambio));
                    pagoGral = Convert.ToDecimal(obeDocXCobrar.doxcc_nmonto_pagado - (_BE.pdxcc_nmonto_cobro * _BE.pdxcc_nmonto_tipo_cambio));
                }
                else
                {
                    saldoGral = Convert.ToDecimal(obeDocXCobrar.doxcc_nmonto_saldo + (_BE.pdxcc_nmonto_cobro / _BE.pdxcc_nmonto_tipo_cambio));
                    pagoGral = Convert.ToDecimal(obeDocXCobrar.doxcc_nmonto_pagado - (_BE.pdxcc_nmonto_cobro / _BE.pdxcc_nmonto_tipo_cambio));
                }
            }

            if (saldoGral > obeDocXCobrar.doxcc_nmonto_total)
            {
                saldoGral = Convert.ToDecimal(obeDocXCobrar.doxcc_nmonto_total);
                pagoGral = 0;
            }

            bteTipoDocumento.Tag = _BE.tdocc_icod_tipo_doc;
            bteTipoDocumento.Text = _BE.Abreviatura;
            txtNumeroDocumento.Text = _BE.pdxcc_vnumero_doc;
            deFechaDocumento.EditValue = _BE.pdxcc_sfecha_cobro;
            txtMonto.Text = _BE.pdxcc_nmonto_cobro.ToString();
            txtTipoCambio.Text = _BE.pdxcc_nmonto_tipo_cambio.ToString();
            txtObservacion.Text = _BE.pdxcc_vobservacion;
            bteCuenta.Tag = _BE.ctacc_iid_cuenta_contable;
            bteCuenta.Text = _BE.CuentaContable;
            txtCuentaDes.Text = _BE.DescripcionCuentaContable;
            LkpTipoMoneda.EditValue = _BE.tablc_iid_tipo_moneda;
            if (_BE.cecoc_icod_centro_costo != 0 && _BE.cecoc_icod_centro_costo != null)
            {
                bteCCosto.Tag = _BE.cecoc_icod_centro_costo;
                bteCCosto.Text = _BE.CentroCosto;
                txtcentrocosto.Text = _BE.CentroCostoDesc;
                bteCCosto.Enabled = true;
            }

            if (_BE.anac_icod_analitica != 0 && _BE.anac_icod_analitica != null)
            {
                bteAnalitica.Tag = _BE.anac_icod_analitica;
                bteAnalitica.Text = _BE.TipoAnalitica;
                bteSubAnalitica.Tag = _BE.anac_icod_analitica_det;
                bteSubAnalitica.Text = _BE.Analitica;
                bteAnalitica.Enabled = true;
                bteSubAnalitica.Enabled = true;
            }
        }
        private void FrmMantePagoDirectoDocumentosXCobrar_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        private void FrmMantePagoDirectoDocumentosXCobrar_Load(object sender, EventArgs e)
        {
            lblDocumentoXCobrar.Text = "Documento por Cobrar:  " + obeDocXCobrar.Abreviatura + "-" + obeDocXCobrar.doxcc_vnumero_doc;
            ImprimirSaldo();
            txtMonto.Text = CalcularSaldoXMoneda(Convert.ToInt16(LkpTipoMoneda.EditValue), 1).ToString();
            if (Status == BSMaintenanceStatus.CreateNew)
                deFechaDocumento.EditValue = DateTime.Now.ToShortDateString();

        }

        private void ImprimirSaldo()
        {
            string cad;
            cad = "Saldo:  " + obeDocXCobrar.SimboloMoneda + " " + saldoGral + "\t";
            if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
                cad += ((Convert.ToInt16(obeDocXCobrar.tablc_iid_tipo_moneda) == Parametros.intSoles) ? ("US$ " + CalcularSaldoXMoneda(Parametros.intDolares, 1).ToString()) : ("S/. " + CalcularSaldoXMoneda(Parametros.intSoles, 1)));
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
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                bteTipoDocumento.Tag = 56;//VCO
                bteTipoDocumento.Text = "VCO";
                LkpTipoMoneda.EditValue = obeDocXCobrar.tablc_iid_tipo_moneda;
                deFechaDocumento.EditValue = DateTime.Now;
            }
            else
            {
                txtMonto.Text = _BE.pdxcc_nmonto_cobro.ToString();
            }
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
            txtMonto.Text = _BE.pdxcc_nmonto_cobro.ToString();
        }


        // si la operación es 1 se quiere calcular el saldo del dxc pero en el tipo de moneda con que se quiere pagar
        // si la operación es 2 se quiere calcular el monto a pagar en el tipo de moneda del dxc        

        private decimal CalcularSaldoXMoneda(int tipo_moneda, int operacion)
        {
            decimal saldo = 0;

            if (obeDocXCobrar.tablc_iid_tipo_moneda != tipo_moneda) //si son diferentes tipos de moneda se convierte, si no se entrega el mismo valor dependiendo del tipo de operación
            {
                if (tipo_moneda == Parametros.intSoles) //la moneda del dxc está en dólares
                {
                    if (operacion == 1)
                        saldo = Convert.ToDecimal(saldoGral) * Convert.ToDecimal(txtTipoCambio.Text);
                    else
                        if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
                        saldo = Convert.ToDecimal(txtMonto.Text) / Convert.ToDecimal(txtTipoCambio.Text);
                }
                else //la moneda del dxc está en nuevos soles
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
            else // los tipos de moneda concuerdan, se devuelve el mismo saldo al monto a pagar(op.1), se devuelve el monto que se irá a registrar para guardarlo con el mismo tipo de moneda (op.2)
                saldo = (operacion == 1) ? Convert.ToDecimal(saldoGral) : Convert.ToDecimal(txtMonto.Text);

            return Math.Round(saldo, 2);
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            Obl = new BCuentasPorCobrar();
            try
            {
                if (bteTipoDocumento.Tag == null)
                {
                    oBase = bteTipoDocumento;
                    throw new ArgumentException("Seleccionar un tipo de documento");
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
                if (deFechaDocumento.DateTime < obeDocXCobrar.doxcc_sfecha_doc)
                {
                    oBase = deFechaDocumento;
                    throw new ArgumentException("La fecha del Pago no debe ser menor a la fecha del Documento por Cobrar");
                }
                if (Convert.ToDecimal(txtMonto.Text) == 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("Ingrese el monto");
                }

                if (Convert.ToDecimal(txtTipoCambio.Text) == 0)
                {
                    oBase = txtTipoCambio;
                    throw new ArgumentException("Ingrese el tipo de cambio");
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
                        throw new ArgumentException("Seleccione Sub - Analítica");
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
                    _BE.pdxcc_nmonto_cobro = Convert.ToDecimal(txtMonto.Text);
                    // si son iguales se pasa el saldo del dxc al monto a cobrar, ya que por conversión puede ser que el monto saldo calculado en el tipo
                    // de moneda del dxc se exceda por milésimas, lo que no permitiría que al actualizar el saldo sea 0 y por consiguiente no permitiría
                    //actualizar la situación a generado
                    if (CalcularSaldoXMoneda(Convert.ToInt16(LkpTipoMoneda.EditValue), 3) == Convert.ToDecimal(txtMonto.Text))
                        _BE.pdxcc_nmonto_cobro_dxc = Convert.ToDecimal(saldoGral);
                    else
                        _BE.pdxcc_nmonto_cobro_dxc = saldo;
                }

                _BE.tdodc_iid_correlativo = obeDocXCobrar.tdodc_iid_correlativo;
                _BE.intTipoDoc = obeDocXCobrar.tdocc_icod_tipo_doc;
                _BE.strNroDoc = obeDocXCobrar.doxcc_vnumero_doc;
                _BE.cliec_icod_cliente = obeDocXCobrar.cliec_icod_cliente;
                /*---------------------------------------------------------------*/
                /*---------------------------------------------------------------*/
                _BE.doxcc_icod_correlativo = obeDocXCobrar.doxcc_icod_correlativo;
                _BE.tdocc_icod_tipo_doc = Convert.ToInt32(bteTipoDocumento.Tag);
                _BE.Abreviatura = bteTipoDocumento.Text;
                _BE.pdxcc_vnumero_doc = txtNumeroDocumento.Text;
                _BE.pdxcc_sfecha_cobro = Convert.ToDateTime(deFechaDocumento.EditValue);
                _BE.tablc_iid_tipo_moneda = Convert.ToInt32(LkpTipoMoneda.EditValue);
                _BE.pdxcc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                _BE.pdxcc_vobservacion = txtObservacion.Text;
                _BE.cliec_icod_cliente = obeDocXCobrar.cliec_icod_cliente;
                _BE.ctacc_iid_cuenta_contable = Convert.ToInt32(bteCuenta.Tag);
                _BE.CuentaContable = bteCuenta.Text;
                _BE.pdxcc_flag_estado = true;
                _BE.saldoDxP = saldoGral;
                _BE.pagoDxP = pagoGral;
                if (bteCCosto.Tag == null)
                    _BE.cecoc_icod_centro_costo = null;
                else
                    _BE.cecoc_icod_centro_costo = Convert.ToInt32(bteCCosto.Tag);
                if (bteAnalitica.Tag == null)
                {
                    _BE.anac_icod_analitica = null;
                    _BE.anac_icod_analitica_det = null;
                }
                else
                {
                    _BE.anac_icod_analitica = Convert.ToInt32(bteAnalitica.Tag);
                    _BE.anac_icod_analitica_det = Convert.ToInt32(bteSubAnalitica.Tag);
                }
                _BE.intUsuario = Valores.intUsuario;
                _BE.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                _BE.pdxcc_vorigen = "D"; //Pago Directo


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarPagoDirectoDocumentoXCobrar(_BE);
                }
                else
                {
                    Obl.ActualizarPagoDirectoDocumentoXCobrar(_BE);
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
                    Status = BSMaintenanceStatus.View;
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
            frmListarTipoDocumento Documentos = new frmListarTipoDocumento();
            Documentos.bModuloall = true;
            Documentos.intIdModulo = Parametros.intModuloCtasPorCobrar;
            if (Documentos.ShowDialog() == DialogResult.OK)
            {
                bteTipoDocumento.Tag = Documentos._Be.tdocc_icod_tipo_doc;
                bteTipoDocumento.Text = Documentos._Be.tdocc_vabreviatura_tipo_doc;
                lblDescripcionClaseDocumento.Text = string.Empty;
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
            //ImprimirSaldo();
            //txtMonto.Text = CalcularSaldoXMoneda(Convert.ToInt16(LkpTipoMoneda.EditValue),1).ToString();
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
                //txtMonto.Text = CalcularSaldoXMoneda(Convert.ToInt16(LkpTipoMoneda.EditValue), 1).ToString();
                ImprimirSaldo();
            }
        }

        private void bteAnalitica_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarAnalitica();
        }

        private void txtMonto_EditValueChanged(object sender, EventArgs e)
        {
            //txtMonto.Text = CalcularSaldoXMoneda(Convert.ToInt16(LkpTipoMoneda.EditValue), 1).ToString();
            ImprimirSaldo();

        }
        private void gcDatos_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}