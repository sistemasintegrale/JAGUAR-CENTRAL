using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Compras;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    public partial class FrmManteLiquidacionCajaDetDoc : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteLiquidacionCajaDetDoc));
        private BSMaintenanceStatus mStatus;
        public List<ELiquidacionCajaDet> oDetail;
        public decimal tipo_cambio;
        public int icod_doc_clase;
        public int? lq_icod_tipo_doc_pago;
        public int? lq_icod_clase_doc_pago;
        private decimal mto_old_saldo;
        public ELiquidacionCajaDet oBe = new ELiquidacionCajaDet();
        public decimal mto_cab_restante;
        public string nro_caja_cab;
        public string nro_liquidacion;
        public int? tipo_analitica;
        public int? icod_analitica;
        public int TipoMoneda1 = 0;
        public int TipoMoneda2 = 0;
        private List<EDocPorPagar> listDXP = new List<EDocPorPagar>();
        public FrmManteLiquidacionCajaDetDoc()
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
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                btnModificar.Enabled = false;
                GetConcepto();
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                GetConcepto();
                dtmFecha.Enabled = false;
                bteConceptoCaja.Enabled = false;
                bteProveedor.Enabled = false;
                txtProveedor.Enabled = false;
                bteDocumento.Enabled = false;
                txtTipoDoc.Enabled = false;
                btnAgregar.Enabled = false;
            }
        }
        private void GetConcepto()
        {
            List<EConceptoMovCaja> mlistConcp = new BTesoreria().ListarConceptoCaja();
            mlistConcp.ForEach(x =>
            {
                if (x.ccod_concep_mov == "PGO")
                {
                    bteConceptoCaja.Tag = x.icod_concepto_caja;
                    bteConceptoCaja.Text = x.ccod_concep_mov;
                    txtConcepto.Text = x.vdescripcion;
                    lq_icod_tipo_doc_pago = x.tdocc_icod_tipo_doc;
                    lq_icod_clase_doc_pago = x.iid_correlativo;

                }
            });
        }
        private void Cargar()
        {
        }
        public void setValues()
        {
            dtmFecha.EditValue = oBe.lqcd_sfecha_liquid;
            bteConceptoCaja.Tag = oBe.lqcd_icod_concepto_caja;
            txtConcepto.Text = oBe.lqcd_vdescripcion_movim;
            bteDocumento.Tag = oBe.docxp_icod_correlativo;
            txtMonto.Text = oBe.lqcd_nmonto_pago.ToString();
            mto_old_saldo = oBe.lqcd_nmonto_pago;
            mto_cab_restante = mto_cab_restante + mto_old_saldo;
            tipo_cambio = oBe.lqcd_ntipo_cambio_pago;
            txtTipoDoc.Tag = oBe.lqcd_iid_tipo_doc;
            icod_doc_clase = Convert.ToInt32(oBe.lqcd_iid_clase_tipo_doc);
            bteDocumento.Text = oBe.lqcd_vnumero_doc;
            bteProveedor.Tag = oBe.lqcd_iid_proveedor;
            //**//
            bteConceptoCaja.Text = oBe.concepto_abreviatura;
            bteProveedor.Text = oBe.codigo_provedor;
            txtProveedor.Text = oBe.proveedor_nombre;
            txtTipoDoc.Text = oBe.tip_doc_abreviatura;
            txtMtoTotal.Text = oBe.docxp_mto_total.ToString();
            txtMtoPagado.Text = oBe.docxp_mto_pagado.ToString();
            txtMtoSaldo.Text = oBe.docxp_mto_saldo.ToString();

            EDocPorPagar objE_DocPorPagar = new EDocPorPagar();
            objE_DocPorPagar.anio = Parametros.intEjercicio;
            objE_DocPorPagar.mesec_iid_mes = Convert.ToInt32(oBe.lqcd_sfecha_liquid.Month);
            listDXP = new BCuentasPorPagar().ListarEDocPorPagar(objE_DocPorPagar).Where(x => x.doxpc_icod_correlativo == oBe.docxp_icod_correlativo).ToList();
            TipoMoneda2 = listDXP[0].tablc_iid_tipo_moneda;
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

        }

        private void bteConceptoCaja_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarConceptoCaja();
        }

        private void bteConceptoCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarConceptoCaja();
        }

        private void ListarConceptoCaja()
        {
            using (FrmListarConceptoCaja frm = new FrmListarConceptoCaja())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteConceptoCaja.Tag = frm._Be.icod_concepto_caja;
                    bteConceptoCaja.Text = frm._Be.ccod_concep_mov;

                }
            }
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (dtmFecha.EditValue == null)
                {
                    oBase = dtmFecha;
                    throw new ArgumentException("Seleccione fecha");
                }
                if (Convert.ToDecimal(txtMonto.Text) <= 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("Ingrese monto mayor a 0.00");
                }
                if (Convert.ToDecimal(txtMonto.Text) > mto_cab_restante)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("La suma del monto en detalle, hace que la sumatoria del detalle sea mayor al importe.\n\t\t\t El monto máximo que puede ingresar es " + mto_cab_restante.ToString());
                }
                if (bteConceptoCaja.Tag == null)
                {
                    oBase = bteConceptoCaja;
                    throw new ArgumentException("No existe Concepto de Caja => PGO, debe registrar este tipo de concepto para poder continuar");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (TipoMoneda1 == TipoMoneda2)
                    {
                        if (Convert.ToDecimal(txtMonto.Text) > Convert.ToDecimal(txtMtoSaldo.Text))
                        {
                            oBase = txtMonto;
                            throw new ArgumentException("El monto no debe ser menor o igual al monto saldo del documento");
                        }
                    }
                    else
                    {
                        if (TipoMoneda1 == 3)
                        {
                            decimal convertirDolares = 0;
                            convertirDolares = Math.Round(Convert.ToDecimal(txtMonto.Text) / tipo_cambio, 2);
                            if (convertirDolares > Convert.ToDecimal(txtMtoSaldo.Text))
                            {
                                oBase = txtMonto;
                                throw new ArgumentException("El monto no debe ser menor o igual al monto saldo del documento");
                            }
                        }
                        else if (TipoMoneda1 == 4)
                        {
                            decimal convertirSoles = 0;
                            convertirSoles = Math.Round(Convert.ToDecimal(txtMonto.Text) * tipo_cambio, 2);
                            if (convertirSoles > Convert.ToDecimal(txtMtoSaldo.Text))
                            {
                                oBase = txtMonto;
                                throw new ArgumentException("El monto no debe ser menor o igual al monto saldo del documento");
                            }
                        }

                    }

                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    if (TipoMoneda1 == TipoMoneda2)
                    {
                        if (Convert.ToDecimal(txtMonto.Text) > (Convert.ToDecimal(txtMtoSaldo.Text) + mto_old_saldo))
                        {
                            oBase = txtMonto;
                            throw new ArgumentException("El monto no debe ser menor o igual al monto saldo del documento");
                        }
                    }
                    else
                    {
                        if (TipoMoneda1 == 3)
                        {
                            decimal convertirDolares = 0;
                            convertirDolares = Math.Round(Convert.ToDecimal(txtMonto.Text) / tipo_cambio, 2);
                            if (convertirDolares > (Convert.ToDecimal(txtMtoSaldo.Text) + mto_old_saldo))
                            {
                                oBase = txtMonto;
                                throw new ArgumentException("El monto no debe ser menor o igual al monto saldo del documento");
                            }
                        }
                        else if (TipoMoneda1 == 4)
                        {
                            decimal convertirSoles = 0;
                            convertirSoles = Math.Round(Convert.ToDecimal(txtMonto.Text) * tipo_cambio, 2);
                            if (convertirSoles > (Convert.ToDecimal(txtMtoSaldo.Text) + mto_old_saldo))
                            {
                                oBase = txtMonto;
                                throw new ArgumentException("El monto no debe ser menor o igual al monto saldo del documento");
                            }
                        }

                    }
                }



                oBe.lqcd_sfecha_liquid = Convert.ToDateTime(dtmFecha.EditValue);
                oBe.lqcd_icod_concepto_caja = Convert.ToInt32(bteConceptoCaja.Tag);
                oBe.lqcd_vdescripcion_movim = txtConcepto.Text;
                oBe.docxp_icod_correlativo = Convert.ToInt32(bteDocumento.Tag);
                oBe.lqcd_nmonto_pago = Convert.ToDecimal(txtMonto.Text);
                oBe.lqcd_ntipo_cambio_pago = tipo_cambio;
                oBe.lqcd_flag_estado = 1;
                oBe.lqcd_iid_proveedor = Convert.ToInt32(bteProveedor.Tag);
                oBe.lqcd_iid_tipo_doc = Convert.ToInt32(txtTipoDoc.Tag);
                oBe.lqcd_iid_clase_tipo_doc = icod_doc_clase;
                oBe.lqcd_vnumero_doc = bteDocumento.Text;
                //**//
                oBe.concepto_abreviatura = bteConceptoCaja.Text;
                oBe.codigo_provedor = bteProveedor.Text;
                oBe.proveedor_nombre = txtProveedor.Text;
                oBe.tip_doc_abreviatura = txtTipoDoc.Text;
                oBe.lq_icod_tipo_doc_pago = lq_icod_tipo_doc_pago;
                oBe.lq_icod_clase_doc_pago = lq_icod_clase_doc_pago;
                oBe.lq_nro_doc_pago = nro_caja_cab + "-" + nro_liquidacion;
                //**//
                oBe.lqcd_vtipo_movimiento = Parametros.strTipoLiqCajaPagoProvision;



                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.lqcd_iid_tipo_analitica = tipo_analitica;
                    oBe.lqcd_iid_analitica = icod_analitica;
                    oBe.docxp_mto_total = Convert.ToDecimal(txtMtoTotal.Text);
                    oBe.docxp_mto_pagado = Convert.ToDecimal(txtMtoPagado.Text);
                    oBe.docxp_mto_saldo = Convert.ToDecimal(txtMtoSaldo.Text);

                    oBe.lqcd_inro_item = oDetail.Count + 1;
                    oBe.operacion = 1;
                    oBe.intUsuario = Valores.intUsuario;
                    oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                    oDetail.Add(oBe);
                }
                else
                {
                    oBe.operacion = 2;
                    oBe.intUsuario = Valores.intUsuario;
                    oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
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

        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProveedor();
        }

        private void bteProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarProveedor();
        }
        private void ListarProveedor()
        {
            FrmListarProveedor frm = new FrmListarProveedor();
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bteProveedor.Text = frm._Be.vcod_proveedor;
                bteProveedor.Tag = frm._Be.iid_icod_proveedor;
                txtProveedor.Text = frm._Be.vnombrecompleto;
                tipo_analitica = 5;
                icod_analitica = frm._Be.anac_icod_analitica;

            }
        }

        private void bteDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarDocumento();
        }

        private void bteDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarDocumento();
        }
        private void ListarDocumento()
        {
            if (bteProveedor.Tag == null)
            {
                XtraMessageBox.Show("Seleccione un proveedor", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmListarDocumentoPorPagarProveedor frm = new FrmListarDocumentoPorPagarProveedor();
            frm.intIcodProveedor = Convert.ToInt32(bteProveedor.Tag);
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (oDetail.Where(x => x.docxp_icod_correlativo == frm._oBe.doxpc_icod_correlativo).ToList().Count > 0)
                {
                    XtraMessageBox.Show("El documento ya existe en el detalle", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bteDocumento.Tag = frm._oBe.doxpc_icod_correlativo;
                bteDocumento.Text = frm._oBe.doxpc_vnumero_doc;
                txtTipoDoc.Text = frm._oBe.tdocc_vabreviatura_tipo_doc;
                txtTipoDoc.Tag = frm._oBe.tdocc_icod_tipo_doc;
                icod_doc_clase = Convert.ToInt32(frm._oBe.tdodc_iid_correlativo);
                txtMtoTotal.Text = frm._oBe.doxpc_nmonto_total_documento.ToString();
                txtMtoPagado.Text = frm._oBe.doxpc_nmonto_total_pagado.ToString();
                txtMtoSaldo.Text = frm._oBe.doxpc_nmonto_total_saldo.ToString();
                lblTotal.Text = (frm._oBe.tablc_iid_tipo_moneda == 3) ? "Mto. Saldo S/. :" : "Mto. Saldo US$ :";
                TipoMoneda2 = frm._oBe.tablc_iid_tipo_moneda;
            }
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
            Close();
        }

        private void FrmManteLiquidacionCajaDetDoc_Load(object sender, EventArgs e)
        {

        }
    }
}