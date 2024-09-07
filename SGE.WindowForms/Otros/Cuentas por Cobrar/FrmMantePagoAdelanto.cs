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
    public partial class FrmMantePagoAdelanto : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantePagoAdelanto));

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public EDocXCobrar obeDocXCobrar = new EDocXCobrar();
        public EDocXCobrar obeDocXCobrarAD = new EDocXCobrar(); //es el dxc obtenido de lista de los adelantos del cliente indicado

        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        public decimal saldoGralDxC;
        public decimal pagoGralDxC;

        private BCuentasPorCobrar Obl;
        public long codDXCPagoAD;//código pago adelanto(SIGT_ADELANTO_PAGO)
        public long codDXCPago;//pago DxC(sigt_doc_x_cobrar_pago)

        public FrmMantePagoAdelanto()
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

            deFechaDocumento.Properties.ReadOnly = Enabled;
            txtMonto.Properties.ReadOnly = Enabled;
            txtTipoCambio.Properties.ReadOnly = true;
            txtObservacion.Properties.ReadOnly = Enabled;

            bteDXCAdelanto.Enabled = (Status == BSMaintenanceStatus.CreateNew);
        }


        private void FrmMantePagoAdelanto_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }


        private void FrmMantePagoAdelanto_Load(object sender, EventArgs e)
        {
            lblDocumentoXCobrar.Text = "Documento por Cobrar:  " + obeDocXCobrar.Abreviatura + "-" + obeDocXCobrar.doxcc_vnumero_doc;
            ImprimirSaldo();
            lblTipoDocumento.Tag = Parametros.intTipoDocAdelantoCliente;
            cargar();
        }

        private void ImprimirSaldo()
        {
            string cad;
            cad = "Saldo:  " + obeDocXCobrar.SimboloMoneda + " " + saldoGralDxC + "\t";
            if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
                cad += ((Convert.ToInt16(obeDocXCobrar.tablc_iid_tipo_moneda) == Parametros.intSoles) ? ("US$ " + CalcularSaldoXMoneda(Parametros.intDolares, 1).ToString()) : ("S/. " + CalcularSaldoXMoneda(Parametros.intSoles, 1)));
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

            if (obeDocXCobrar.tablc_iid_tipo_moneda != tipo_moneda) //si son diferentes tipos de moneda se convierte, si no se entrega el mismo valor dependiendo del tipo de operación
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
            EAdelantoPago oBe = new EAdelantoPago();
            EDocXCobrarPago obj = new EDocXCobrarPago();
            Obl = new BCuentasPorCobrar();
            try
            {
                if (bteDXCAdelanto.Tag == null)
                {
                    oBase = bteDXCAdelanto;
                    throw new ArgumentException("Seleccione un Documento");
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

                if (Convert.ToDecimal(txtMonto.Text) > obeDocXCobrarAD.doxcc_nmonto_saldo)
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
                decimal saldo = CalcularSaldoXMoneda(obeDocXCobrarAD.tablc_iid_tipo_moneda, 2);

                //verificar saldo
                if (CalcularSaldoXMoneda(Convert.ToInt16(obeDocXCobrarAD.tablc_iid_tipo_moneda), 1) < Convert.ToDecimal(txtMonto.Text))
                {
                    oBase = txtMonto;
                    throw new ArgumentException("El monto ingresado supera al saldo");
                }
                else
                {
                    obj.pdxcc_nmonto_cobro = Convert.ToDecimal(txtMonto.Text);
                    // si son iguales se pasa el saldo del dxc al monto a cobrar, ya que por conversión puede ser que el monto saldo calculado en el tipo
                    // de moneda del dxc se exceda por milésimas, lo que no permitiría que al actualizar el saldo sea 0 y por consiguiente no permitiría
                    //actualizar la situación a generado
                    if (CalcularSaldoXMoneda(Convert.ToInt16(obeDocXCobrarAD.tablc_iid_tipo_moneda), 1) == Convert.ToDecimal(txtMonto.Text))
                        obj.pdxcc_nmonto_cobro_dxc = Convert.ToDecimal(saldoGralDxC);
                    else
                        obj.pdxcc_nmonto_cobro_dxc = saldo;
                }


                //datos de dxc_adelanto_pago
                oBe.doxcc_icod_correlativo_pago = obeDocXCobrar.doxcc_icod_correlativo; //el documento a pagar
                oBe.doxcc_icod_correlativo_adelanto = obeDocXCobrarAD.doxcc_icod_correlativo; //correlativo del adelanto
                oBe.tdocc_icod_tipo_doc = obeDocXCobrar.tdocc_icod_tipo_doc; //tipo documento(adelanto)
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(obeDocXCobrarAD.tablc_iid_tipo_moneda); //debe grabarse con el tipo de moneda del documento adelanto
                oBe.adpac_nmonto_pago = Convert.ToDecimal(txtMonto.Text); //monto que se va a pagar guardado con el tipo de moneda del documento adelanto
                oBe.adpac_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text); //tipo de cambio de la fecha seleccionada
                oBe.adpac_vdescripcion = txtObservacion.Text;
                oBe.adpac_sfecha_pago = Convert.ToDateTime(deFechaDocumento.EditValue); //fecha del pago
                oBe.adpac_iorigen = "A"; //Adelanto
                oBe.adpac_iusuario_crea = Valores.intUsuario;
                oBe.adpac_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.adpac_flag_estado = true;
                oBe.SaldoDXCAdelanto = obeDocXCobrarAD.doxcc_nmonto_saldo;
                oBe.doxcc_nmonto_pagado = obeDocXCobrarAD.doxcc_nmonto_pagado;

                //datos de dxc_pago
                obj.doxcc_icod_correlativo = obeDocXCobrar.doxcc_icod_correlativo; //el documento a pagar
                obj.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente; //se paga con un adelanto
                obj.pdxcc_vnumero_doc = obeDocXCobrarAD.doxcc_vnumero_doc; //ndoc del adelanto con que se va a pagar
                obj.pdxcc_sfecha_cobro = Convert.ToDateTime(deFechaDocumento.EditValue); //fecha del pago
                obj.tablc_iid_tipo_moneda = obeDocXCobrarAD.tablc_iid_tipo_moneda; //debe grabarse con el tipo de moneda del documento adelanto
                obj.pdxcc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text); //tipo de cambio de la fecha seleccionada
                obj.pdxcc_vobservacion = txtObservacion.Text;
                obj.cliec_icod_cliente = obeDocXCobrar.cliec_icod_cliente; //código del cliente
                obj.pdxcc_vorigen = "A"; //Adelanto
                obj.intUsuario = Valores.intUsuario;
                obj.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                obj.pdxcc_flag_estado = true;
                obj.doxcc_icod_correlativo_pago = obeDocXCobrarAD.doxcc_icod_correlativo;
                obeDocXCobrar.doxcc_nmonto_saldo = saldoGralDxC;
                obeDocXCobrar.doxcc_nmonto_pagado = pagoGralDxC;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarPagoAdelanto(oBe, obj);
                }
                else
                {
                    oBe.pdxcc_icod_correlativo = codDXCPago;
                    oBe.adpac_icod_correlativo = codDXCPagoAD;
                    Obl.ActualizarPagoAdelanto(oBe, obj, obeDocXCobrar);
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
            txtTipoCambio.Text = "";
            txtTipoCambio.Properties.ReadOnly = false;
            var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(deFechaDocumento.EditValue).ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                txtTipoCambio.Properties.ReadOnly = true;
            });
        }

        private void bteDXCAdelanto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                using (FrmListarDocXCobrar frm = new FrmListarDocXCobrar())
                {
                    frm.intCliente = Convert.ToInt32(obeDocXCobrar.cliec_icod_cliente);
                    frm.intTipoDoc = Convert.ToInt32(lblTipoDocumento.Tag);
                    frm.filtraCliente = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        obeDocXCobrarAD = frm._Be;
                        bteDXCAdelanto.Tag = obeDocXCobrarAD.doxcc_icod_correlativo;
                        bteDXCAdelanto.Text = obeDocXCobrarAD.doxcc_vnumero_doc;
                        lblMoneda.Text = obeDocXCobrarAD.SimboloMoneda;

                    }
                }
            }
        }








    }
}