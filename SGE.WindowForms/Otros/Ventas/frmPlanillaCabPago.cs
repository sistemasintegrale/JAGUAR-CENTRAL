using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using System;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmPlanillaCabPago : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod, EPlanillaCobranzaCab oBePlnCabReload);
        public event DelegadoMensaje MiEvento;
        public EPagoDocVenta oBe = new EPagoDocVenta();
        public EPlanillaCobranzaCab oBePlnCab = new EPlanillaCobranzaCab();
        public EPlanillaCobranzaDet oBePlnDet = new EPlanillaCobranzaDet();
        public EDocXCobrar oBeDXC = new EDocXCobrar();

        //int id_cliente = 0;
        //Int64 id_doc_x_cobrar = 0;
        //int id_tipo_doc = 0;        

        public frmPlanillaCabPago()
        {
            InitializeComponent();
        }

        private void frmPlanillaCabPago_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            /**/
            BSControls.LoaderLook(lkpTipoPago, new BGeneral().listarTablaRegistro(41).Where(x => x.tarec_icorrelativo_registro != 6).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpTipoTarjeta, new BVentas().listarTipoTarjeta(), "tcrc_vdescripcion_tipo_tarjeta_cred", "tcrc_icod_tipo_tarjeta_cred", false);
            BSControls.LoaderLook(lkpTipoMoneda, new BGeneral().listarTablaRegistro(5).Where(ob => ob.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

            BSControls.LoaderLook(lkpBancoCheque, new BGeneral().listarTablaRegistro(49), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
            BSControls.LoaderLook(lkpBancoTransferencia, new BTesoreria().listarBancos(), "bcoc_vnombre_banco", "bcoc_icod_banco", false);
            BSControls.LoaderLook(lkpBancoTransferenciaCheque, new BTesoreria().listarBancos(), "bcoc_vnombre_banco", "bcoc_icod_banco", false);

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

            lkpTipoPago.Enabled = !Enabled;
            bteNroDoc.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;
            lkpTipoMoneda.Enabled = !Enabled;
            txtMonto.Enabled = !Enabled;
            lkpTipoTarjeta.Enabled = !Enabled;
            dtFechaVencCredito.Enabled = !Enabled;
            lkpBancoCheque.Enabled = !Enabled;
            txtNroCheque.Enabled = !Enabled;
            dtFechaCobroCheque.Enabled = !Enabled;
            lkpBancoTransferencia.Enabled = !Enabled;
            lkpCuenta.Enabled = !Enabled;
            bteNotaCredito.Enabled = !Enabled;
            bteAncticipo.Enabled = !Enabled;
            txtObservaciones.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            lkpBancoTransferenciaCheque.Enabled = !Enabled;
            lkpCuentaCheque.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpTipoPago.Enabled = Enabled;
                bteNroDoc.Enabled = Enabled;
                dteFecha.Enabled = Enabled;
                lkpTipoMoneda.Enabled = Enabled;
                txtMonto.Enabled = !Enabled;
                lkpTipoTarjeta.Enabled = Enabled;
                dtFechaVencCredito.Enabled = Enabled;
                lkpBancoCheque.Enabled = Enabled;
                txtNroCheque.Enabled = Enabled;
                dtFechaCobroCheque.Enabled = Enabled;
                lkpBancoTransferencia.Enabled = Enabled;
                lkpCuenta.Enabled = Enabled;
                bteNotaCredito.Enabled = Enabled;
                bteAncticipo.Enabled = Enabled;
                txtObservaciones.Enabled = !Enabled;
                btnGuardar.Enabled = !Enabled;
                lkpBancoTransferenciaCheque.Enabled = Enabled;
                lkpCuentaCheque.Enabled = Enabled;
            }
        }

        public void setValues()
        {
            oBe = new BVentas().getDatosPago(Convert.ToInt32(oBePlnDet.pgoc_icod_pago))[0];

            /*************************/
            bteNroDoc.Text = oBePlnDet.plnd_vnumero_doc;
            bteNroDoc.Tag = oBePlnDet.plnd_icod_documento;
            //id_tipo_doc = Convert.ToInt32(oBePlnDet.plnd_icod_tipo_doc);
            //id_doc_x_cobrar = Convert.ToInt64(oBePlnDet.pgoc_dxc_icod_documento);
            //id_cliente = oBePlnDet.intCliente;
            txtMonto.Text = Convert.ToDecimal(oBePlnDet.plnd_nmonto_pagado).ToString();
            dteFecha.EditValue = oBePlnDet.plnd_sfecha_doc;
            lkpTipoPago.EditValue = oBePlnDet.intTipoPago;
            lkpTipoTarjeta.EditValue = oBePlnDet.intTipoTarjeta;
            lkpTipoMoneda.EditValue = oBePlnDet.tablc_iid_tipo_moneda;
            txtObservaciones.Text = oBePlnDet.strPagoDescripcion;

            dtFechaVencCredito.EditValue = oBe.pgoc_fecha_venc_credito;
            lkpBancoCheque.EditValue = oBe.tblc_iid_banco_cheque;
            txtNroCheque.Text = oBe.pgoc_nro_cheque;
            dtFechaCobroCheque.EditValue = oBe.pgoc_fecha_cob_cheque;

            bteAncticipo.Tag = oBe.pgoc_icod_anticipo;
            bteAncticipo.Text = oBe.strNroAnticipo;
            bteNotaCredito.Tag = oBe.pgoc_icod_nota_credito;
            bteNotaCredito.Text = oBe.strNroNotaCredito;
            if (oBePlnDet.intTipoPago == 4)
            {
                lkpBancoTransferenciaCheque.EditValue = oBe.bcoc_icod_banco;
                lkpCuentaCheque.EditValue = oBe.bcod_icod_banco_cuenta;
            }
            else
            {
                if (oBePlnDet.intTipoPago == 5)
                {
                    lkpBancoTransferencia.EditValue = oBe.bcoc_icod_banco;
                    lkpCuenta.EditValue = oBe.bcod_icod_banco_cuenta;
                }
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

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {

                if (Convert.ToInt32(oBe.pgoc_dxc_icod_doc) == 0)
                {
                    oBase = bteNroDoc;
                    throw new ArgumentException("Seleccione el documento a pagar");
                }

                if (Convert.ToInt32(lkpTipoPago.EditValue) == Parametros.intTipoPgoTarjetaCredito)
                {
                    if (Convert.ToInt32(lkpTipoTarjeta.EditValue) == 0)
                    {
                        oBase = lkpTipoTarjeta;
                        throw new ArgumentException("Seleccione Tipo de Tarjeta con la cual se esta efectuando el pago");
                    }
                }

                if (Convert.ToInt32(lkpTipoPago.EditValue) == Parametros.intTipoPgoNotaCredito)
                {
                    if (Convert.ToInt32(bteNotaCredito.Tag) == 0)
                    {
                        oBase = bteNotaCredito;
                        throw new ArgumentException("Seleccione la Nota de Crédito con la cual se esta efectuando el pago");
                    }
                }

                if (Convert.ToInt32(lkpTipoPago.EditValue) == Parametros.intTipoPgoCheque)
                {
                    if (String.IsNullOrWhiteSpace(txtNroCheque.Text))
                    {
                        oBase = txtNroCheque;
                        throw new ArgumentException("Ingrese el Nro. de Cheque");
                    }
                }

                if (Convert.ToInt32(lkpTipoPago.EditValue) == Parametros.intTipoPgoAnticipo)
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
                //if(lkpTipoPago.EditValue)
                #region Planilla Det
                int? intNull = null;
                oBePlnDet.plnc_icod_planilla = oBePlnCab.plnc_icod_planilla;
                oBePlnDet.tablc_iid_tipo_mov = Parametros.intPlnPago;
                oBePlnDet.plnd_sfecha_doc = Convert.ToDateTime(dteFecha.EditValue);
                if (Status == BSMaintenanceStatus.CreateNew)
                    oBePlnDet.plnd_icod_tipo_doc = oBe.tdocc_icoc_tipo_documento;
                oBePlnDet.plnd_icod_documento = Convert.ToInt32(bteNroDoc.Tag);
                oBePlnDet.plnd_vnumero_doc = bteNroDoc.Text;
                oBePlnDet.plnd_nmonto = nmonto_total;
                oBePlnDet.plnd_nmonto_pagado = Convert.ToDecimal(txtMonto.Text);
                oBePlnDet.tablc_iid_tipo_moneda = Convert.ToInt32(lkpTipoMoneda.EditValue);
                oBePlnDet.intUsuario = Valores.intUsuario;
                oBePlnDet.strPc = WindowsIdentity.GetCurrent().Name;
                #endregion
                #region Pago Doc Venta

                oBe.pgoc_dxc_icod_pago = Convert.ToInt64(oBePlnDet.pgoc_dxc_icod_pago);
                oBe.pgoc_sfecha_pago = Convert.ToDateTime(dteFecha.EditValue);
                oBe.pgoc_tipo_pago = Convert.ToInt32(lkpTipoPago.EditValue);
                oBe.tdocc_icoc_tipo_documento = (Convert.ToInt32(lkpTipoPago.EditValue) == Parametros.intTipoPgoNotaCredito) ? Convert.ToInt32(bteNotaCredito.Tag) : (Convert.ToInt32(lkpTipoPago.EditValue) == Parametros.intTipoPgoAnticipo) ? Convert.ToInt32(bteAncticipo.Tag) : Parametros.intTipoDocPlanillaVenta;
                oBe.pgoc_icod_nota_credito = (Convert.ToInt32(bteNotaCredito.Tag) == 0) ? intNull : Convert.ToInt32(bteNotaCredito.Tag);
                oBe.pgoc_icod_tipo_tarjeta = (Convert.ToInt32(lkpTipoTarjeta.EditValue) == 0) ? intNull : Convert.ToInt32(lkpTipoTarjeta.EditValue);
                oBe.pgoc_icod_tipo_moneda = Convert.ToInt32(lkpTipoMoneda.EditValue);
                oBe.pgoc_descripcion = (String.IsNullOrEmpty(txtObservaciones.Text)) ? lkpTipoPago.Text : txtObservaciones.Text;
                oBe.pgoc_nmonto = Convert.ToDecimal(txtMonto.Text);
                oBe.intTipoOperacion = 1;
                oBe.pgoc_vnumero_planilla = oBePlnCab.plnc_vnumero_planilla;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                DateTime? dtNull = null;
                oBe.pgoc_fecha_venc_credito = (dtFechaVencCredito.EditValue != null) ? Convert.ToDateTime(dtFechaVencCredito.EditValue) : dtNull;
                oBe.tblc_iid_banco_cheque = (Convert.ToInt32(lkpBancoCheque.EditValue) == 0) ? intNull : Convert.ToInt32(lkpBancoCheque.EditValue);
                oBe.pgoc_nro_cheque = txtNroCheque.Text;
                oBe.pgoc_fecha_cob_cheque = (dtFechaCobroCheque.EditValue != null) ? Convert.ToDateTime(dtFechaCobroCheque.EditValue) : dtNull;
                //oBe.bcoc_icod_banco = (Convert.ToInt32(lkpBancoTransferencia.EditValue) == 0) ? intNull : Convert.ToInt32(Convert.ToInt32(lkpBancoTransferencia.EditValue));
                //oBe.bcod_icod_banco_cuenta = (Convert.ToInt32(lkpCuenta.EditValue) == 0) ? intNull : Convert.ToInt32(Convert.ToInt32(lkpCuenta.EditValue));
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
                #endregion

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBePlnDet.plnd_icod_detalle = new BVentas().insertarPagoPln(oBePlnCab, oBePlnDet, oBe, oBeDXC);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarPagoPln(oBePlnCab, oBePlnDet, oBe, oBeDXC);
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    MiEvento(oBe.pgoc_icod_pago, oBePlnCab);
                    Close();
                }
            }
        }

        decimal nmonto_total = 0;
        private void listarDocumento()
        {
            using (frmListarDXCNoCancelado frm = new frmListarDXCNoCancelado())
            {
                frm.flagSoloFavBov = true;
                int? intNullVal = null;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteNroDoc.Tag = frm._oBe.doxcc_icod_correlativo;
                    bteNroDoc.Text = frm._oBe.doxcc_vnumero_doc;
                    lkpTipoMoneda.EditValue = frm._oBe.tablc_iid_tipo_moneda;
                    txtMonto.Text = Convert.ToDecimal(frm._oBe.doxcc_nmonto_saldo).ToString();
                    nmonto_total = Convert.ToDecimal(frm._oBe.doxcc_nmonto_total);

                    oBe.pgoc_icod_cliente = Convert.ToInt32(frm._oBe.cliec_icod_cliente);
                    oBe.tdocc_icoc_tipo_documento = Convert.ToInt32(frm._oBe.tdocc_icod_tipo_doc);
                    oBe.pgoc_dxc_icod_doc = frm._oBe.doxcc_icod_correlativo;


                    oBeDXC = frm._oBe;
                }
            }

        }

        private void lkpTipoPago_EditValueChanged(object sender, EventArgs e)
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

        private void bteNroDoc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarDocumento();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void bteAncticipo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Convert.ToInt32(bteNroDoc.Tag) == 0)
            {
                XtraMessageBox.Show("Seleccione el documento a pagar", "Información del Sistema");
                return;
            }
            listarNotaCredAnticipo(2);
        }

        private void bteNotaCredito_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Convert.ToInt32(bteNroDoc.Tag) == 0)
            {
                XtraMessageBox.Show("Seleccione el documento a pagar", "Información del Sistema");
                return;
            }
            listarNotaCredAnticipo(1);
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
                //oBe.tdocc_icoc_tipo_documento = Convert.ToInt32(frm.EDocPorCobrar.tdocc_icod_tipo_doc);
                if (intOpcion == 1)
                {
                    bteNotaCredito.Tag = Convert.ToInt32(frm.EDocPorCobrar.doxcc_icod_correlativo);
                    bteNotaCredito.Text = frm.EDocPorCobrar.doxcc_vnumero_doc;
                    oBe.strNroNotaCredito = frm.EDocPorCobrar.doxcc_vnumero_doc;
                }
                else if (intOpcion == 2)
                {
                    bteAncticipo.Tag = Convert.ToInt32(frm.EDocPorCobrar.doxcc_icod_correlativo);
                    bteAncticipo.Text = frm.EDocPorCobrar.doxcc_vnumero_doc;
                    oBe.strNroAnticipo = frm.EDocPorCobrar.doxcc_vnumero_doc;
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

        private void lkpBancoTransferenciaCheque_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpCuentaCheque, new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBancoTransferenciaCheque.EditValue)), "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);
        }


    }
}