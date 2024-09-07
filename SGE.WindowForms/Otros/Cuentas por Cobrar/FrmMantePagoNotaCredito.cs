using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmMantePagoNotaCredito : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantePagoAdelanto));

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public EDocXCobrar oBeDxc = new EDocXCobrar();
        public EDocXCobrar oBeDxcNc = new EDocXCobrar(); //es el dxc obtenido de lista de las notas de crédito del cliente indicado
        public EDocXCobrarPago oBeDxcPago = new EDocXCobrarPago();

        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        public decimal saldoGralDxC;
        public decimal pagoGralDxC;

        private BCuentasPorCobrar Obl;
        public long codDXCPagoNC; //código pago adelanto(SIGT_NOTA_CREDITO_PAGO)
        public long codDXCPago; //pago DxC(sigt_doc_x_cobrar_pago)


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
            lblDocumentoXCobrar.Text = "Documento por Cobrar:  " + oBeDxc.Abreviatura + "-" + oBeDxc.doxcc_vnumero_doc;
            ImprimirSaldo();
            lblTipoDocumento.Tag = Parametros.intTipoDocNotaCreditoCliente;
            cargar();
        }

        private void ImprimirSaldo()
        {
            string cad;
            cad = "Saldo:  " + oBeDxc.SimboloMoneda + " " + saldoGralDxC + "\t";
            if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
                cad += ((Convert.ToInt16(oBeDxc.tablc_iid_tipo_moneda) == Parametros.intSoles) ? ("US$ " + CalcularSaldoXMoneda(Parametros.intDolares, 1).ToString()) : ("S/. " + CalcularSaldoXMoneda(Parametros.intSoles, 1)));
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

            if (oBeDxc.tablc_iid_tipo_moneda != tipo_moneda) //si son diferentes tipos de moneda se convierte, si no se entrega el mismo valor dependiendo del tipo de operación
            {
                if (tipo_moneda == Parametros.intSoles) //la moneda del dxc está en dólares
                {
                    if (operacion == 1)
                        saldo = Convert.ToDecimal(saldoGralDxC) * Convert.ToDecimal(txtTipoCambio.Text);
                    else
                        if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
                        saldo = Convert.ToDecimal(txtMonto.Text) / Convert.ToDecimal(txtTipoCambio.Text);
                }
                else //la moneda del dxc está en nuevos soles
                {
                    if (operacion == 1)
                    {
                        if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
                            saldo = Convert.ToDecimal(saldoGralDxC) / Convert.ToDecimal(txtTipoCambio.Text);
                    }
                    else
                    {
                        saldo = Convert.ToDecimal(txtMonto.Text) * Convert.ToDecimal(txtTipoCambio.Text);
                    }
                }
            }
            else // los tipos de moneda concuerdan, se devuelve el mismo saldo al monto a cobrar(op.1), se devuelve el monto que se irá a registrar para guardarlo con el mismo tipo de moneda (op.2)
                saldo = (operacion == 1) ? Convert.ToDecimal(saldoGralDxC) : Convert.ToDecimal(txtMonto.Text);

            return Math.Round(saldo, 2);
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            ENotaCreditoPago oBe = new ENotaCreditoPago();
            EDocXCobrarPago obj = new EDocXCobrarPago();
            Obl = new BCuentasPorCobrar();
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
                if (deFechaDocumento.DateTime < oBeDxc.doxcc_sfecha_doc)
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

                if (Convert.ToDecimal(txtMonto.Text) > oBeDxcNc.doxcc_nmonto_saldo)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("El monto no puede ser mayor al saldo del adelanto con que se paga");
                }

                if (string.IsNullOrWhiteSpace(txtObservacion.Text))
                {
                    oBase = txtObservacion;
                    throw new ArgumentException("Ingrese observación");
                }

                //zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz++
                decimal saldo = CalcularSaldoXMoneda(oBeDxcNc.tablc_iid_tipo_moneda, 2);

                //verificar saldo
                if (CalcularSaldoXMoneda(Convert.ToInt16(oBeDxcNc.tablc_iid_tipo_moneda), 1) < Convert.ToDecimal(txtMonto.Text))
                {
                    oBase = txtMonto;
                    throw new ArgumentException("El monto ingresado supera al saldo");
                }
                else
                {
                    obj.pdxcc_nmonto_cobro = Convert.ToDecimal(txtMonto.Text);

                    if (CalcularSaldoXMoneda(Convert.ToInt16(oBeDxcNc.tablc_iid_tipo_moneda), 1) == Convert.ToDecimal(txtMonto.Text))
                        obj.pdxcc_nmonto_cobro_dxc = Convert.ToDecimal(saldoGralDxC);
                    else
                        obj.pdxcc_nmonto_cobro_dxc = saldo;
                }


                //datos de SIGT_NOTA_CREDITO
                oBe.doxcc_icod_correlativo_pago = oBeDxc.doxcc_icod_correlativo; //el documento a pagar
                oBe.doxcc_icod_correlativo_nota_credito = Convert.ToInt64(bteDXCNotaCredito.Tag); //correlativo de la nota de crédito   
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(oBeDxcNc.tablc_iid_tipo_moneda); //debe grabarse con el tipo de moneda del documento nota de crédito
                oBe.ncpac_nmonto_pago = Convert.ToDecimal(txtMonto.Text); //monto que se va a pagar guardado con el tipo de moneda del documento nota de crédito
                oBe.ncpac_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text); //tipo de cambio de la fecha seleccionada
                oBe.ncpac_vdescripcion = txtObservacion.Text;
                oBe.ncpac_sfecha_pago = Convert.ToDateTime(deFechaDocumento.EditValue); //fecha del pago
                oBe.ncpac_iorigen = "N"; //Nota de credito
                oBe.ncpac_flag_estado = true;
                oBe.ncpac_iusuario_crea = Valores.intUsuario;
                oBe.ncpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.ncpac_iusuario_modifica = Valores.intUsuario;
                oBe.ncpac_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                oBe.SaldoDXCNotaCredito = oBeDxcNc.doxcc_nmonto_saldo;
                oBe.doxcc_nmonto_pagado = oBeDxcNc.doxcc_nmonto_pagado;

                //datos de dxc_pago
                obj.doxcc_icod_correlativo = oBeDxc.doxcc_icod_correlativo; //el documento a pagar
                obj.tdocc_icod_tipo_doc = Parametros.intTipoDocNotaCreditoCliente; //se paga con un adelanto
                obj.pdxcc_vnumero_doc = oBeDxcNc.doxcc_vnumero_doc; //ndoc de la NC con que se va a pagar
                obj.pdxcc_sfecha_cobro = Convert.ToDateTime(deFechaDocumento.EditValue); //fecha del pago
                obj.tablc_iid_tipo_moneda = oBeDxcNc.tablc_iid_tipo_moneda; //debe grabarse con el tipo de moneda del documento adelanto
                obj.pdxcc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text); //tipo de cambio de la fecha seleccionada
                obj.pdxcc_vobservacion = txtObservacion.Text;
                obj.cliec_icod_cliente = oBeDxc.cliec_icod_cliente; //código del cliente
                obj.pdxcc_vorigen = "N"; //Nota de crédito
                obj.intUsuario = Valores.intUsuario;
                obj.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                obj.pdxcc_flag_estado = true;
                obj.doxcc_icod_correlativo_pago = oBeDxcNc.doxcc_icod_correlativo;
                oBeDxc.doxcc_nmonto_saldo = saldoGralDxC;
                oBeDxc.doxcc_nmonto_pagado = pagoGralDxC;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarPagoNotaCredito(oBe, obj, oBeDxc, oBeDxcNc);
                }
                else
                {
                    oBe.ncpac_icod_correlativo = codDXCPagoNC;
                    obj.pdxcc_icod_correlativo = codDXCPago;
                    Obl.ActualizarPagoNotaCredito(oBe, obj, oBeDxc);
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
                using (FrmListarDocXCobrar frm = new FrmListarDocXCobrar())
                {
                    frm.intCliente = Convert.ToInt32(oBeDxc.cliec_icod_cliente);
                    frm.intTipoDoc = Convert.ToInt32(lblTipoDocumento.Tag);
                    frm.filtraCliente = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        oBeDxcNc = frm._Be;
                        bteDXCNotaCredito.Tag = oBeDxcNc.doxcc_icod_correlativo;
                        bteDXCNotaCredito.Text = oBeDxcNc.doxcc_vnumero_doc;
                        lblMoneda.Text = oBeDxcNc.SimboloMoneda;
                        if (oBeDxcNc.doxcc_nmonto_saldo > CalcularSaldoXMoneda(oBeDxcNc.tablc_iid_tipo_moneda, 1))
                            txtMonto.Text = oBeDxc.doxcc_nmonto_saldo.ToString();
                        else
                        {
                            txtMonto.Text = oBeDxcNc.doxcc_nmonto_saldo.ToString();
                            txtMonto.Text = CalcularSaldoXMoneda(oBeDxcNc.tablc_iid_tipo_moneda, 2).ToString();
                        }
                    }
                }
            }
        }





    }
}