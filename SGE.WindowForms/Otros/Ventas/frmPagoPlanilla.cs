using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmPagoPlanilla : DevExpress.XtraEditors.XtraForm
    {
        public EPlanillaCobranzaCab oBeC = new EPlanillaCobranzaCab();
        public EPagoDocVenta oBe = new EPagoDocVenta();
        public List<EPagoDocVenta> lstPagos = new List<EPagoDocVenta>();
        public decimal dblTotal = 0;


        public frmPagoPlanilla()
        {
            InitializeComponent();
        }

        private void frmPagoPlanilla_Load(object sender, EventArgs e)
        {
            cargar();
        }

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpTipoPago.Enabled = Enabled;
            }
        }

        private void cargar()
        {
            BSControls.LoaderLook(lkpTipoPago, new BGeneral().listarTablaRegistro(41), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpTipoTarjeta, new BVentas().listarTipoTarjeta(), "tcrc_vdescripcion_tipo_tarjeta_cred", "tcrc_icod_tipo_tarjeta_cred", false);
            BSControls.LoaderLook(lkpTipoMoneda, new BGeneral().listarTablaRegistro(5).Where(x => x.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpBancoCheque, new BGeneral().listarTablaRegistro(49), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
            BSControls.LoaderLook(lkpBancoTransferencia, new BTesoreria().listarBancos(), "bcoc_vnombre_banco", "bcoc_icod_banco", false);
            BSControls.LoaderLook(lkpBancoTransferenciaCheque, new BTesoreria().listarBancos(), "bcoc_vnombre_banco", "bcoc_icod_banco", false);

        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        private void lkpFormaPago_EditValueChanged(object sender, EventArgs e)
        {
            if (Status != BSMaintenanceStatus.CreateNew)
                return;
            if (Convert.ToInt32(lkpTipoPago.EditValue) == 1)// 1 : FORM. PAGO (EFECTIVO)
            {
                enableClear(Convert.ToInt32(lkpTipoPago.EditValue));
            }
            else if (Convert.ToInt32(lkpTipoPago.EditValue) == 2)// 2 : FORM. PAGO (TARJETA)
            {
                enableClear(Convert.ToInt32(lkpTipoPago.EditValue));
            }
            else if (Convert.ToInt32(lkpTipoPago.EditValue) == 3) // 3 : FORM. PAGO (NOTA CREDITO)
            {
                enableClear(Convert.ToInt32(lkpTipoPago.EditValue));
            }
            else if (Convert.ToInt32(lkpTipoPago.EditValue) == 4) // 4 : FORM. PAGO (CHEQUE)
            {
                enableClear(Convert.ToInt32(lkpTipoPago.EditValue));
            }
            else if (Convert.ToInt32(lkpTipoPago.EditValue) == 5) // 5 : FORM. PAGO (TRANSFERENCIA BANCARIA)
            {
                enableClear(Convert.ToInt32(lkpTipoPago.EditValue));
            }
            else if (Convert.ToInt32(lkpTipoPago.EditValue) == 6) // 6 : FORM. PAGO (CREDITO)
            {
                enableClear(Convert.ToInt32(lkpTipoPago.EditValue));
            }
            else if (Convert.ToInt32(lkpTipoPago.EditValue) == 7) // 7 : FORM. PAGO (ANTICIPO)
            {
                enableClear(Convert.ToInt32(lkpTipoPago.EditValue));
            }
        }

        private void enableClear(int intOpcion)
        {
            switch (intOpcion)
            {
                case 1:
                    grpTarjeta.Enabled = false;
                    grpNotaCredito.Enabled = false;
                    grpCheque.Enabled = false;
                    grpTransferencia.Enabled = false;
                    grpCredito.Enabled = false;
                    grpAnticipo.Enabled = false;

                    lkpTipoTarjeta.EditValue = null;
                    lkpBancoCheque.EditValue = null;
                    txtNroCheque.Text = String.Empty;
                    dtFechaCobroCheque.EditValue = null;
                    lkpBancoTransferencia.EditValue = null;
                    lkpCuenta.EditValue = null;
                    bteNotaCredito.Tag = null;
                    bteNotaCredito.Text = String.Empty;
                    bteAncticipo.Tag = null;
                    bteAncticipo.Text = String.Empty;
                    dtFechaVencCredito.EditValue = null;
                    break;
                case 2:
                    grpTarjeta.Enabled = true;
                    grpNotaCredito.Enabled = false;
                    grpCheque.Enabled = false;
                    grpTransferencia.Enabled = false;
                    grpCredito.Enabled = false;
                    grpAnticipo.Enabled = false;

                    lkpTipoTarjeta.EditValue = 1;
                    lkpBancoCheque.EditValue = null;
                    txtNroCheque.Text = String.Empty;
                    dtFechaCobroCheque.EditValue = null;
                    lkpBancoTransferencia.EditValue = null;
                    lkpCuenta.EditValue = null;
                    bteNotaCredito.Tag = null;
                    bteNotaCredito.Text = String.Empty;
                    bteAncticipo.Tag = null;
                    bteAncticipo.Text = String.Empty;
                    dtFechaVencCredito.EditValue = null;
                    break;
                case 3:
                    grpTarjeta.Enabled = false;
                    grpNotaCredito.Enabled = true;
                    grpCheque.Enabled = false;
                    grpTransferencia.Enabled = false;
                    grpCredito.Enabled = false;
                    grpAnticipo.Enabled = false;

                    lkpTipoTarjeta.EditValue = null;
                    lkpBancoCheque.EditValue = null;
                    txtNroCheque.Text = String.Empty;
                    dtFechaCobroCheque.EditValue = null;
                    lkpBancoTransferencia.EditValue = null;
                    lkpCuenta.EditValue = null;
                    bteAncticipo.Tag = null;
                    bteAncticipo.Text = String.Empty;
                    dtFechaVencCredito.EditValue = null;
                    break;
                case 4:
                    grpTarjeta.Enabled = false;
                    grpNotaCredito.Enabled = false;
                    grpCheque.Enabled = true;
                    grpTransferencia.Enabled = false;
                    grpCredito.Enabled = false;
                    grpAnticipo.Enabled = false;

                    lkpTipoTarjeta.EditValue = null;
                    lkpBancoCheque.EditValue = 1;
                    dtFechaCobroCheque.EditValue = DateTime.Now;
                    lkpBancoTransferencia.EditValue = null;
                    lkpCuenta.EditValue = null;
                    bteNotaCredito.Tag = null;
                    bteNotaCredito.Text = String.Empty;
                    bteAncticipo.Tag = null;
                    bteAncticipo.Text = String.Empty;
                    dtFechaVencCredito.EditValue = null;
                    break;
                case 5:
                    grpTarjeta.Enabled = false;
                    grpNotaCredito.Enabled = false;
                    grpCheque.Enabled = false;
                    grpTransferencia.Enabled = true;
                    grpCredito.Enabled = false;
                    grpAnticipo.Enabled = false;

                    lkpTipoTarjeta.EditValue = null;
                    lkpBancoCheque.EditValue = null;
                    txtNroCheque.Text = String.Empty;
                    dtFechaCobroCheque.EditValue = null;
                    lkpBancoTransferencia.EditValue = 9;
                    bteNotaCredito.Tag = null;
                    bteNotaCredito.Text = String.Empty;
                    bteAncticipo.Tag = null;
                    bteAncticipo.Text = String.Empty;
                    dtFechaVencCredito.EditValue = null;
                    break;
                case 6:
                    grpTarjeta.Enabled = false;
                    grpNotaCredito.Enabled = false;
                    grpCheque.Enabled = false;
                    grpTransferencia.Enabled = false;
                    grpCredito.Enabled = true;
                    grpAnticipo.Enabled = false;

                    lkpTipoTarjeta.EditValue = null;
                    lkpBancoCheque.EditValue = null;
                    txtNroCheque.Text = String.Empty;
                    dtFechaCobroCheque.EditValue = null;
                    lkpBancoTransferencia.EditValue = null;
                    lkpCuenta.EditValue = null;
                    bteNotaCredito.Tag = null;
                    bteNotaCredito.Text = String.Empty;
                    bteAncticipo.Tag = null;
                    bteAncticipo.Text = String.Empty;
                    dtFechaVencCredito.EditValue = DateTime.Now;
                    break;
                case 7:
                    grpTarjeta.Enabled = false;
                    grpNotaCredito.Enabled = false;
                    grpCheque.Enabled = false;
                    grpTransferencia.Enabled = false;
                    grpCredito.Enabled = false;
                    grpAnticipo.Enabled = true;

                    lkpTipoTarjeta.EditValue = null;
                    lkpBancoCheque.EditValue = null;
                    txtNroCheque.Text = String.Empty;
                    dtFechaCobroCheque.EditValue = null;
                    lkpBancoTransferencia.EditValue = null;
                    lkpCuenta.EditValue = null;
                    bteNotaCredito.Tag = null;
                    bteNotaCredito.Text = String.Empty;
                    dtFechaVencCredito.EditValue = null;
                    break;
            }
        }
        private string getDescripcion(int intOpcion)
        {
            string strDescripcion = "";
            switch (intOpcion)
            {
                case 1:
                    strDescripcion = "Efectivo";
                    break;
                case 2:
                    strDescripcion = lkpTipoTarjeta.Text;
                    break;
                case 3:
                    strDescripcion = bteNotaCredito.Text;
                    break;
                case 4:
                    strDescripcion = txtNroCheque.Text;
                    break;
                case 5:
                    strDescripcion = lkpCuenta.Text;
                    break;
                case 6:
                    strDescripcion = "Crédito";
                    break;
                case 7:
                    strDescripcion = bteAncticipo.Text;
                    break;
            }
            return strDescripcion;
        }

        private void setSave()
        {

            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToInt32(lkpTipoMoneda.EditValue) == Parametros.intTipoMonedaDolares)
                {
                    if (oBe.pgoc_tipo_cambio == 0)
                    {
                        oBase = lkpTipoMoneda;
                        throw new ArgumentException("No existe Tipo de Cambio registrado, para la fecha del documento de venta");
                    }
                }

                if (Convert.ToInt32(lkpTipoPago.EditValue) == 2)
                {
                    if (Convert.ToInt32(lkpTipoTarjeta.EditValue) == 0)
                    {
                        oBase = lkpTipoTarjeta;
                        throw new ArgumentException("Seleccione Tipo de Tarjeta con la cual se esta efectuando el pago");
                    }

                }

                if (Convert.ToInt32(lkpTipoPago.EditValue) == 3)
                {
                    if (Convert.ToInt32(bteNotaCredito.Tag) == 0)
                    {
                        oBase = bteNotaCredito;
                        throw new ArgumentException("Seleccione la Nota de Crédito con la cual se esta efectuando el pago");
                    }


                }

                if (Convert.ToInt32(lkpTipoPago.EditValue) == 4)
                {
                    if (String.IsNullOrWhiteSpace(txtNroCheque.Text))
                    {
                        oBase = txtNroCheque;
                        throw new ArgumentException("Ingrese el Nro. de Cheque");
                    }

                    if (Convert.ToInt32(lkpBancoTransferenciaCheque.EditValue) == 0)
                    {
                        oBase = lkpBancoTransferenciaCheque;
                        throw new ArgumentException("Seleccione Banco con la cual se esta ejecutando el pago");
                    }

                    if (Convert.ToInt32(lkpCuentaCheque.EditValue) == 0)
                    {
                        oBase = lkpCuentaCheque;
                        throw new ArgumentException("Seleccione Cuenta con la cual se esta ejecuando el pago");
                    }
                }

                if (Convert.ToInt32(lkpTipoPago.EditValue) == 5)
                {


                    if (Convert.ToInt32(lkpBancoTransferencia.EditValue) == 0)
                    {
                        oBase = lkpBancoTransferencia;
                        throw new ArgumentException("Seleccione Banco con la cual se esta ejecutando el pago");
                    }

                    if (Convert.ToInt32(lkpCuenta.EditValue) == 0)
                    {
                        oBase = lkpCuenta;
                        throw new ArgumentException("Seleccione Cuenta con la cual se esta ejecuando el pago");
                    }
                }

                if (Convert.ToInt32(lkpTipoPago.EditValue) == 7)
                {
                    if (Convert.ToInt32(bteAncticipo.Tag) == 0)
                    {
                        oBase = bteAncticipo;
                        throw new ArgumentException("Seleccione el Anticipo con el cual se esta efectuando el pago");
                    }
                }

                if (Convert.ToDecimal(txtMonto.Text) == 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("Ingrese el monto del pago");
                }

                //if (Convert.ToDecimal(txtMonto.Text) > dblTotal)
                //{
                //    oBase = txtMonto;
                //    throw new ArgumentException("El monto total de pago(s) no debe sobrepasar el monto total del documento");
                //}





                int? intNull = null;
                oBe.pgoc_tipo_pago = Convert.ToInt32(lkpTipoPago.EditValue);
                oBe.pgoc_icod_nota_credito = (Convert.ToInt32(bteNotaCredito.Tag) == 0) ? intNull : Convert.ToInt32(bteNotaCredito.Tag);
                oBe.pgoc_icod_tipo_tarjeta = (Convert.ToInt32(lkpTipoTarjeta.EditValue) == 0) ? intNull : Convert.ToInt32(lkpTipoTarjeta.EditValue);
                oBe.pgoc_icod_tipo_moneda = Convert.ToInt32(lkpTipoMoneda.EditValue);
                oBe.strTipoMoneda = (Convert.ToInt32(lkpTipoMoneda.EditValue) == 3) ? "S/." : "US$";
                oBe.pgoc_nmonto = Convert.ToDecimal(txtMonto.Text);
                oBe.pgoc_descripcion = getDescripcion(Convert.ToInt32(lkpTipoPago.EditValue));
                oBe.strTipoPago = lkpTipoPago.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.pgoc_nmonto = Convert.ToDecimal(txtMonto.Text);

                DateTime? dtNull = null;
                oBe.pgoc_fecha_venc_credito = (dtFechaVencCredito.EditValue != null) ? Convert.ToDateTime(dtFechaVencCredito.EditValue) : dtNull;
                oBe.tblc_iid_banco_cheque = (Convert.ToInt32(lkpBancoCheque.EditValue) == 0) ? intNull : Convert.ToInt32(lkpBancoCheque.EditValue);
                oBe.pgoc_nro_cheque = txtNroCheque.Text;
                oBe.pgoc_fecha_cob_cheque = (dtFechaCobroCheque.EditValue != null) ? Convert.ToDateTime(dtFechaCobroCheque.EditValue) : dtNull;
                if (Convert.ToInt32(lkpTipoPago.EditValue) == 4)
                {
                    oBe.bcoc_icod_banco = (Convert.ToInt32(lkpBancoTransferenciaCheque.EditValue) == 0) ? intNull : Convert.ToInt32(Convert.ToInt32(lkpBancoTransferenciaCheque.EditValue));
                    oBe.bcod_icod_banco_cuenta = (Convert.ToInt32(lkpCuentaCheque.EditValue) == 0) ? intNull : Convert.ToInt32(Convert.ToInt32(lkpCuentaCheque.EditValue));
                }
                else
                {
                    if (Convert.ToInt32(lkpTipoPago.EditValue) == 5)
                    {
                        oBe.bcoc_icod_banco = (Convert.ToInt32(lkpBancoTransferencia.EditValue) == 0) ? intNull : Convert.ToInt32(Convert.ToInt32(lkpBancoTransferencia.EditValue));
                        oBe.bcod_icod_banco_cuenta = (Convert.ToInt32(lkpCuenta.EditValue) == 0) ? intNull : Convert.ToInt32(Convert.ToInt32(lkpCuenta.EditValue));
                    }
                }


                oBe.pgoc_icod_anticipo = (Convert.ToInt32(bteAncticipo.Tag) == 0) ? intNull : Convert.ToInt32(Convert.ToInt32(bteAncticipo.Tag));

                oBe.strNroNotaCredito = bteNotaCredito.Text;
                oBe.strNroAnticipo = bteAncticipo.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.intTipoOperacion = 1;
                    lstPagos.Add(oBe);
                }
                else
                {
                    if (oBe.intTipoOperacion != 1)
                        oBe.intTipoOperacion = 2;
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                Flag = false;
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Flag)
                {
                    this.DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void lkpBancoTransferencia_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpCuenta, new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBancoTransferencia.EditValue)), "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);
        }

        private void listarNotaCredAnticipo(int intOpcion)
        {
            //intOpcion ==> 1 es Nota de Credito, 2 es Anticipo
            FrmListarDocxCobrar frm = new FrmListarDocxCobrar();
            frm.intOpcionPlanillaVenta = intOpcion;
            frm.bDocFacBol = false;
            frm.intIcodCliente = oBe.pgoc_icod_cliente;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                oBe.tdocc_icoc_tipo_documento = Convert.ToInt32(frm.EDocPorCobrar.tdocc_icod_tipo_doc);
                if (intOpcion == 1)
                {
                    bteNotaCredito.Tag = Convert.ToInt32(frm.EDocPorCobrar.doxcc_icod_correlativo);
                    bteNotaCredito.Text = frm.EDocPorCobrar.doxcc_vnumero_doc;
                }
                else if (intOpcion == 2)
                {
                    bteAncticipo.Tag = Convert.ToInt32(frm.EDocPorCobrar.doxcc_icod_correlativo);
                    bteAncticipo.Text = frm.EDocPorCobrar.doxcc_vnumero_doc;
                }
                //intIdTipoDocumento = frm.EDocPorCobrar.tdocc_icod_tipo_doc;
                //intIdClaseTipoDocumento = Convert.ToInt32(frm.EDocPorCobrar.tdodc_iid_correlativo);
                //oBe.pgoc_icod_nota_credito = (intOpcion == 1) ? Convert.ToInt32(frm.EDocPorCobrar.doxcc_icod_correlativo) : intNull;
                //oBe.pgoc_icod_anticipo = (intOpcion == 2) ? Convert.ToInt32(frm.EDocPorCobrar.doxcc_icod_correlativo) : intNull;
                ////intIdDocPorCobrar_AdelNC = Convert.ToInt32(frm.EDocPorCobrar.docxc_icod_documento);
                ////txtDocumento.Text = frm.EDocPorCobrar.tdocc_vabreviatura_tipo_doc;                
                //bteNroDocumento.Text = frm.EDocPorCobrar.doxcc_vnumero_doc;
                //deFechaPago.EditValue = frm.EDocPorCobrar.doxcc_sfecha_doc;

            }
        }

        private void bteNotaCredito_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (oBe.pgoc_icod_cliente == 0)
            {
                XtraMessageBox.Show("Seleccione el cliente a facturar", "Información del Sistema");
                return;
            }
            listarNotaCredAnticipo(1);

        }

        private void bteAncticipo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (oBe.pgoc_icod_cliente == 0)
            {
                XtraMessageBox.Show("Seleccione el cliente a facturar", "Información del Sistema");
                return;
            }
            listarNotaCredAnticipo(2);
        }

        private void lkpBancoTransferenciaCheque_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpCuentaCheque, new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBancoTransferenciaCheque.EditValue)), "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);
        }


    }
}