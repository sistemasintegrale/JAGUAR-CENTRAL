using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmMantePagoNotaCredito : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantePagoNotaCredito));

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public EDocPorPagar obeDocXPagar = new EDocPorPagar();
        public EDocPorPagar obeDocXPagarNC = new EDocPorPagar(); //es el dxp obtenido de lista de las notas de crédito del cliente indicado

        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        public decimal saldoGralDxP;
        public decimal pagoGralDxP;

        private BCuentasPorPagar Obl;
        public long codDXPPagoNC;//código pago nota crédito
        public long codDXPPago;//pago DxP(sigt_doc_x_pagar_pago)

        public int TipoDoc = 0;


        public FrmMantePagoNotaCredito()
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
            bool Enabled = (Status == BSMaintenanceStatus.View);
            bteDXCNotaCredito.Enabled = !Enabled;
            deFechaDocumento.Enabled = !Enabled;
            txtMonto.Enabled = !Enabled;
            txtTipoCambio.Enabled = !Enabled;
            txtObservacion.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteDXCNotaCredito.Enabled = Enabled;
            }

        }

        private void FrmMantePagoNotaCredito_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        private void FrmMantePagoNotaCredito_Load(object sender, EventArgs e)
        {
            lblDocumentoXCobrar.Text = "Documento por Pagar:  " + obeDocXPagar.tdocc_vabreviatura_tipo_doc + "-" + obeDocXPagar.doxpc_vnumero_doc;
            ImprimirSaldo();
            lblTipoDocumento.Tag = Parametros.intTipoDocNotaCreditoProveedor;
            cargar();
        }

        private void ImprimirSaldo()
        {
            string cad;
            cad = "Saldo:  " + obeDocXPagar.SimboloMoneda + " " + saldoGralDxP + "\t";
            if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
                cad += ((Convert.ToInt16(obeDocXPagar.tablc_iid_tipo_moneda) == Parametros.intSoles) ? ("US$ " + CalcularSaldoXMoneda(Parametros.intDolares, 1).ToString()) : ("S/. " + CalcularSaldoXMoneda(Parametros.intSoles, 1)));
            lblSaldo.Text = cad;
        }

        public void cargar()
        {
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            if (Status == BSMaintenanceStatus.CreateNew)
                deFechaDocumento.EditValue = DateTime.Today.ToShortDateString();
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

        // si la operación es 1 se quiere calcular el saldo del dxc pero en el tipo de moneda con que se quiere cobrar
        // si la operación es 2 se quiere calcular el monto a cobrar en el tipo de moneda del dxc        

        private decimal CalcularSaldoXMoneda(int tipo_moneda, int operacion)
        {
            decimal saldo = 0;

            if (obeDocXPagar.tablc_iid_tipo_moneda != tipo_moneda) //si son diferentes tipos de moneda se convierte, si no se entrega el mismo valor dependiendo del tipo de operación
            {
                if (tipo_moneda == Parametros.intSoles) //la moneda del dxc está en dólares
                {
                    if (operacion == 1)
                        saldo = Convert.ToDecimal(saldoGralDxP) * Convert.ToDecimal(txtTipoCambio.Text);
                    else
                        if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
                        saldo = Convert.ToDecimal(txtMonto.Text) / Convert.ToDecimal(txtTipoCambio.Text);
                }
                else //la moneda del dxc está en nuevos soles
                {
                    if (operacion == 1)
                    {
                        if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
                            saldo = Convert.ToDecimal(saldoGralDxP) / Convert.ToDecimal(txtTipoCambio.Text);
                    }
                    else
                    {
                        saldo = Convert.ToDecimal(txtMonto.Text) * Convert.ToDecimal(txtTipoCambio.Text);
                    }
                }
            }
            else // los tipos de moneda concuerdan, se devuelve el mismo saldo al monto a cobrar(op.1), se devuelve el monto que se irá a registrar para guardarlo con el mismo tipo de moneda (op.2)
                saldo = (operacion == 1) ? Convert.ToDecimal(saldoGralDxP) : Convert.ToDecimal(txtMonto.Text);

            return Math.Round(saldo, 2);
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EDocPorPagarNotaCredito oBe = new EDocPorPagarNotaCredito();
            EDocPorPagarPago obj = new EDocPorPagarPago();
            Obl = new BCuentasPorPagar();
            try
            {
                if (bteDXCNotaCredito.Tag == null)
                {
                    oBase = bteDXCNotaCredito;
                    throw new ArgumentException("Seleccione un Documento");
                }
                if (deFechaDocumento.EditValue == null)
                {
                    oBase = deFechaDocumento;
                    throw new ArgumentException("Seleccione la fecha del Pago");
                }
                if (deFechaDocumento.DateTime < obeDocXPagar.doxpc_sfecha_doc)
                {
                    oBase = deFechaDocumento;
                    throw new ArgumentException("La fecha del Pago no debe ser menor a la fecha del Documento por Pagar");
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

                if (Convert.ToDecimal(txtMonto.Text) > obeDocXPagarNC.doxpc_nmonto_total_saldo)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("El monto no puede ser mayor al saldo de la nota de crédito con que se paga");
                }

                if (string.IsNullOrWhiteSpace(txtObservacion.Text))
                {
                    oBase = txtObservacion;
                    throw new ArgumentException("Ingrese observación");
                }

                //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz++
                decimal saldo = CalcularSaldoXMoneda(obeDocXPagarNC.tablc_iid_tipo_moneda, 2);

                //verificar saldo
                if (CalcularSaldoXMoneda(Convert.ToInt16(obeDocXPagarNC.tablc_iid_tipo_moneda), 1) < Convert.ToDecimal(txtMonto.Text))
                {
                    oBase = txtMonto;
                    throw new ArgumentException("El monto ingresado supera al saldo");
                }
                else
                {
                    obj.pdxpc_nmonto_pago = Convert.ToDecimal(txtMonto.Text);

                    if (CalcularSaldoXMoneda(Convert.ToInt16(obeDocXPagarNC.tablc_iid_tipo_moneda), 1) == Convert.ToDecimal(txtMonto.Text))
                        obj.pdxpc_nmonto_pago_dxp = Convert.ToDecimal(saldoGralDxP);
                    else
                        obj.pdxpc_nmonto_pago_dxp = saldo;
                }


                //datos de SIGT_NOTA_CREDITO
                oBe.doxpc_icod_correlativo_pago = obeDocXPagar.doxpc_icod_correlativo; //el documento a pagar
                oBe.doxpc_icod_correlativo_nota_credito = Convert.ToInt64(bteDXCNotaCredito.Tag); //correlativo de la nota de crédito

                oBe.ncpap_nmonto_pago = Convert.ToDecimal(txtMonto.Text); //monto que se va a pagar guardado con el tipo de moneda del documento nota de crédito
                oBe.ncpap_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text); //tipo de cambio de la fecha seleccionada
                oBe.ncpap_vdescripcion = txtObservacion.Text;
                oBe.ncpap_sfecha_pago = Convert.ToDateTime(deFechaDocumento.EditValue); //fecha del pago
                oBe.ncpap_iorigen = "N"; //Nota de credito
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.ncpap_flag_estado = true;
                oBe.SaldoDXCNotaCredito = obeDocXPagarNC.doxpc_nmonto_total_saldo;
                oBe.doxpc_nmonto_total_pagado = obeDocXPagarNC.doxpc_nmonto_total_pagado;
                oBe.anio = Parametros.intEjercicio;
                oBe.iid_moneda_nota_credito = obeDocXPagarNC.tablc_iid_tipo_moneda;
                //datos de dxp_pago
                obj.doxpc_icod_correlativo = obeDocXPagar.doxpc_icod_correlativo; //el documento a pagar
                //obj.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoProveedor; //se paga con un adelanto
                obj.tdocc_icod_tipo_doc = TipoDoc; //se paga con un adelanto
                obj.pdxpc_vnumero_doc = obeDocXPagarNC.doxpc_vnumero_doc; //ndoc del adelanto con que se va a pagar
                obj.pdxpc_sfecha_pago = Convert.ToDateTime(deFechaDocumento.EditValue); //fecha del pago
                obj.tablc_iid_tipo_moneda = obeDocXPagarNC.tablc_iid_tipo_moneda; //debe grabarse con el tipo de moneda del documento adelanto
                obj.pdxpc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text); //tipo de cambio de la fecha seleccionada
                obj.pdxpc_vobservacion = txtObservacion.Text;
                obj.proc_vcod_proveedor = obeDocXPagar.proc_icod_proveedor.ToString(); //código del proveedor
                obj.pdxpc_vorigen = "N"; //Nota de Crédito
                obj.intUsuario = Valores.intUsuario;
                obj.strPc = WindowsIdentity.GetCurrent().Name;
                obj.pdxpc_flag_estado = true;
                obj.anio = Parametros.intEjercicio;
                obj.doxpc_icod_correlativo_pago = obeDocXPagarNC.doxpc_icod_correlativo;
                obeDocXPagar.doxpc_nmonto_total_saldo = saldoGralDxP;
                obeDocXPagar.doxpc_nmonto_total_pagado = pagoGralDxP;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarPagoNotaCredito(oBe, obj, obeDocXPagar, obeDocXPagarNC);
                }
                else
                {
                    oBe.ncpap_icod_correlativo = codDXPPagoNC;
                    obj.pdxpc_icod_correlativo = codDXPPago;
                    oBe.pdxpc_icod_correlativo = codDXPPago;
                    Obl.ActualizarPagoNotaCredito(oBe, obj, obeDocXPagar);
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


        private void deFechaDocumento_EditValueChanged(object sender, EventArgs e)
        {
            txtTipoCambio.Text = string.Empty;
            txtTipoCambio.Properties.ReadOnly = false;
            var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(deFechaDocumento.EditValue).ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                txtTipoCambio.Properties.ReadOnly = true;
            });
        }

        private void bteDXCNotaCredito_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                using (FrmListarDocXPagar frm = new FrmListarDocXPagar())
                {
                    frm.flag_canje = false;
                    frm.intTipoDoc = Convert.ToInt32(lblTipoDocumento.Tag);
                    frm.intProveedor = obeDocXPagar.proc_icod_proveedor;
                    frm.flag_filtrar_proveedor = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        obeDocXPagarNC = frm._Be;
                        bteDXCNotaCredito.Tag = obeDocXPagarNC.doxpc_icod_correlativo;
                        bteDXCNotaCredito.Text = obeDocXPagarNC.doxpc_vnumero_doc;
                        TipoDoc = obeDocXPagarNC.tdocc_icod_tipo_doc;
                        lblMoneda.Text = obeDocXPagarNC.SimboloMoneda;
                        if (obeDocXPagarNC.doxpc_nmonto_total_saldo > CalcularSaldoXMoneda(obeDocXPagarNC.tablc_iid_tipo_moneda, 1))
                            txtMonto.Text = obeDocXPagar.doxpc_nmonto_total_saldo.ToString();
                        else
                        {
                            txtMonto.Text = obeDocXPagarNC.doxpc_nmonto_total_saldo.ToString();
                            txtMonto.Text = CalcularSaldoXMoneda(obeDocXPagarNC.tablc_iid_tipo_moneda, 2).ToString();
                        }

                    }
                }
            }
        }




    }
}