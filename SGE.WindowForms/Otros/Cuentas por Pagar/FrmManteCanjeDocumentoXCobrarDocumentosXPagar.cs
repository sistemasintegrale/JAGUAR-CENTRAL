using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmManteCanjeDocumentoXCobrarDocumentosXPagar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteCanjeDocumentoXCobrarDocumentosXPagar));

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public EDocPorPagarPago objDXPPago = new EDocPorPagarPago();
        public int vcocc_iid_voucher_contable;
        public string Mon = "";
        public int IcodMon = 0;
        public decimal MontoSaldo = 0;
        public int doxpc_icod_correlativo = 0;
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();

        private BDocXCobrarDocxPagarCanje Obl;

        public FrmManteCanjeDocumentoXCobrarDocumentosXPagar()
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
            btecliente.Enabled = !Enabled;
            bteDxC.Enabled = !Enabled;
            deFechaDocumento.Enabled = !Enabled;
            txtMonto.Enabled = !Enabled;
            txtTipoCambio.Enabled = !Enabled;
            txtObservacion.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                btecliente.Enabled = Enabled;
                bteDxC.Enabled = Enabled;
            }

        }

        private void FrmManteCanjeDocumentoXCobrarDocumentosXPagar_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        private void FrmManteCanjeDocumentoXCobrarDocumentosXPagar_Load(object sender, EventArgs e)
        {
            gcDatos.Text = "Documento por Cobrar:  " + objDXPPago.Abreviatura + "-" + objDXPPago.doxpc_vnumero_doc;
            ImprimirSaldo();
            cargar();
        }

        private void ImprimirSaldo()
        {
            string cad;
            cad = "Saldo:  " + Mon + " " + MontoSaldo.ToString("n2") + "\t";
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

        //private decimal CalcularSaldoXMoneda(int tipo_moneda, int operacion)
        //{
        //decimal saldo = 0;

        //if (obeCanje.tipo_moneda_dxp != tipo_moneda) //si son diferentes tipos de moneda se convierte, si no se entrega el mismo valor dependiendo del tipo de operación
        //{
        //    if (tipo_moneda == Parametros.intSoles) //la moneda del dxc está en dólares
        //    {
        //        if (operacion == 1)
        //            saldo = Convert.ToDecimal(obeCanje.doxpc_nmonto_total_saldo) * Convert.ToDecimal(txtTipoCambio.Text);
        //        else
        //            if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
        //                saldo = Convert.ToDecimal(txtMonto.Text) / Convert.ToDecimal(txtTipoCambio.Text);
        //    }
        //    else //la moneda del dxc está en nuevos soles
        //    {
        //        if (operacion == 1)
        //        {
        //            if (Convert.ToDecimal(txtTipoCambio.Text) != 0)
        //                saldo = Convert.ToDecimal(obeCanje.doxpc_nmonto_total_saldo) / Convert.ToDecimal(txtTipoCambio.Text);
        //        }
        //        else
        //        {
        //            saldo = Convert.ToDecimal(txtMonto.Text) * Convert.ToDecimal(txtTipoCambio.Text);
        //        }
        //    }
        //}
        //else // los tipos de moneda concuerdan, se devuelve el mismo saldo al monto a cobrar(op.1), se devuelve el monto que se irá a registrar para guardarlo con el mismo tipo de moneda (op.2)
        //    saldo = (operacion == 1) ? Convert.ToDecimal(obeCanje.doxpc_nmonto_total_saldo) : Convert.ToDecimal(txtMonto.Text);

        //return Math.Round(saldo, 2);
        //}

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            //EDocXCobrarPago objDxCPago = new EDocXCobrarPago();
            //EDocPorPagarPago objDxPPago = new EDocPorPagarPago();
            Obl = new BDocXCobrarDocxPagarCanje();
            decimal MontoX = 0;
            try
            {
                if (btecliente.Tag == null)
                {
                    oBase = btecliente;
                    throw new ArgumentException("Seleccione un Cliente");
                }
                if (bteDxC.Tag == null)
                {
                    oBase = bteDxC;
                    throw new ArgumentException("Seleccione un Documento por Cobrar");
                }
                if (deFechaDocumento.EditValue == null)
                {
                    oBase = deFechaDocumento;
                    throw new ArgumentException("Seleccione la fecha del Pago");
                }
                if (deFechaDocumento.DateTime < objDXPPago.pdxpc_sfecha_pago)
                {
                    oBase = deFechaDocumento;
                    throw new ArgumentException("La fecha del Pago no debe ser menor a la fecha del Documento");
                }
                if (Convert.ToDecimal(txtMonto.Text) == 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("Ingrese el monto");
                }
                //if (Convert.ToDecimal(txtMonto.Text) > obeCanje.doxcc_nmonto_saldo)
                //{
                //    oBase = txtMonto;
                //    throw new ArgumentException("El monto no puede ser mayor al saldo del documento por cobrar");
                //}
                //if (Convert.ToDecimal(txtMonto.Text) > MontoSaldo)
                //{
                //    oBase = txtMonto;
                //    throw new ArgumentException("El monto no puede ser mayor al saldo del documento por cobrar");
                //}

                //if (IcodMon == Convert.ToInt32(lblMoneda.Tag))
                //{
                //    if (Convert.ToDecimal(txtMonto.Text) > MontoSaldo)
                //    {
                //        oBase = txtMonto;
                //        throw new ArgumentException("El monto no puede ser mayor al saldo del documento por cobrar");
                //    }
                //}
                //else if (IcodMon == 3)
                //{
                //     MontoX = Convert.ToDecimal(txtMonto.Text) / Convert.ToDecimal(txtTipoCambio.Text);
                //     if (MontoX > MontoSaldo)
                //    {
                //        oBase = txtMonto;
                //        throw new ArgumentException("El monto no puede ser mayor al saldo del documento por cobrar");
                //    }
                //}
                //else
                //{
                //     MontoX = Convert.ToDecimal(txtMonto.Text) * Convert.ToDecimal(txtTipoCambio.Text);
                //     if (MontoX > MontoSaldo)
                //    {
                //        oBase = txtMonto;
                //        throw new ArgumentException("El monto no puede ser mayor al saldo del documento por cobrar");
                //    }
                //}
                /**/
                if (string.IsNullOrWhiteSpace(txtObservacion.Text))
                {
                    oBase = txtObservacion;
                    throw new ArgumentException("Ingrese observación");
                }

                /*Datos DXC Pago*/
                if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    objDXPPago.doxcc_icod_correlativo_pago = Convert.ToInt32(objDXPPago.doxcc_icod_correlativo);
                }
                objDXPPago.doxcc_icod_correlativo = Convert.ToInt64(bteDxC.Tag);
                objDXPPago.IcodTD = Convert.ToInt32(lblTipoDocumento.Tag);
                objDXPPago.NumDXC = bteDxC.Text;
                objDXPPago.MonedaDXC = Convert.ToInt32(lblMoneda.Tag);
                //if (IcodMon == Convert.ToInt32(lblMoneda.Tag))
                //{
                objDXPPago.pdxcc_nmonto_cobro = Convert.ToDecimal(txtMonto.Text);
                //}
                //else
                //{
                //    objDXPPago.pdxcc_nmonto_cobro = MontoX;
                //}
                /*Datos DXP Pago*/
                objDXPPago.pdxpc_vobservacion = txtObservacion.Text;
                objDXPPago.pdxpc_sfecha_pago = deFechaDocumento.DateTime;
                objDXPPago.tablc_iid_tipo_moneda = IcodMon;
                //objDXPPago.tablc_iid_tipo_moneda = Convert.ToInt32(lblMoneda.Tag);
                //if (IcodMon == Convert.ToInt32(lblMoneda.Tag))
                //{
                objDXPPago.pdxpc_nmonto_pago = Convert.ToDecimal(txtMonto.Text);
                //}
                //else
                //{
                //    objDXPPago.pdxpc_nmonto_pago = MontoX;
                //}
                //objDXPPago.pdxpc_nmonto_pago = Convert.ToDecimal(txtMonto.Text); ;
                objDXPPago.pdxpc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text); ;
                objDXPPago.pdxpc_vorigen = "X";
                objDXPPago.intUsuario = Valores.intUsuario;
                objDXPPago.strPc = WindowsIdentity.GetCurrent().Name;
                objDXPPago.intUsuario = Valores.intUsuario;
                objDXPPago.strPc = WindowsIdentity.GetCurrent().Name;
                objDXPPago.pdxpc_flag_estado = true;




                objDXPPago.pdxpc_mes = deFechaDocumento.DateTime.Month;
                objDXPPago.anio = Parametros.intEjercicio;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarCanjeDXCconDXP(objDXPPago, 1);
                }
                else
                {
                    Obl.ActualizarCanjeDXCconDXP(objDXPPago, 1);
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

        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmListarCliente Cliente = new FrmListarCliente();
            if (Cliente.ShowDialog() == DialogResult.OK)
            {
                btecliente.Tag = Cliente._Be.cliec_icod_cliente;
                btecliente.Text = Cliente._Be.cliec_vnombre_cliente;
            }
            bteDxC.Focus();
        }

        private void bteDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (btecliente.Tag != null)
            {
                if (e.Button.Index == 0)
                {
                    using (FrmListarDocXCobrar frm = new FrmListarDocXCobrar())
                    {
                        frm.filtraCliente = false;
                        frm.flagCanje = true;
                        frm.intCliente = Convert.ToInt32(btecliente.Tag);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            bteDxC.Tag = frm._Be.doxcc_icod_correlativo;
                            bteDxC.Text = frm._Be.doxcc_vnumero_doc;
                            lblMoneda.Text = frm._Be.SimboloMoneda;
                            lblMoneda.Tag = frm._Be.tablc_iid_tipo_moneda;
                            lblTipoDocumento.Text = frm._Be.Abreviatura.ToString();
                            lblTipoDocumento.Tag = frm._Be.tdocc_icod_tipo_doc;
                            //obeCanje.doxcc_nmonto_saldo = frm._Be.doxcc_nmonto_saldo;
                            //obeCanje.clase_documento_dxc = frm._Be.tdodc_iid_correlativo;
                            //if (frm._Be.doxcc_nmonto_saldo > CalcularSaldoXMoneda(frm._Be.tablc_iid_tipo_moneda, 1))
                            //    txtMonto.Text = CalcularSaldoXMoneda(frm._Be.tablc_iid_tipo_moneda, 1).ToString();
                            //else
                            //{
                            //    txtMonto.Text = obeCanje.doxcc_nmonto_saldo.ToString();
                            //    txtMonto.Text = frm._Be.doxcc_nmonto_saldo.ToString();
                            //}
                        }
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Selecione un Cliente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btecliente.Focus();
            }
        }





    }
}