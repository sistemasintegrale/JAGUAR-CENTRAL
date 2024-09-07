using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Compras;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    public partial class FrmManteLiquidacionCajaDetGeneDoc : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteLiquidacionCajaDetDoc));
        private BSMaintenanceStatus mStatus;
        public List<ELiquidacionCajaDet> oDetail;
        public decimal tipo_cambio;



        private decimal mto_old_saldo;
        public ELiquidacionCajaDet oBe = new ELiquidacionCajaDet();
        public decimal mto_cab_restante;
        public string nro_caja_cab;
        public string nro_liquidacion;
        //public int? tipo_analitica;
        //public int? icod_analitica;

        List<ECuentaContable> lstCuentaContable = new List<ECuentaContable>();

        public FrmManteLiquidacionCajaDetGeneDoc()
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

        }

        public void setValues()
        {
            dtmFecha.EditValue = oBe.lqcd_sfecha_liquid;
            /**************************************************************************/
            bteConceptoCaja.Text = oBe.concepto_abreviatura;
            bteConceptoCaja.Tag = oBe.lqcd_icod_concepto_caja;
            txtConcepto.Text = oBe.lqcd_vdescripcion_movim;
            /**************************************************************************/
            txtMonto.Text = oBe.lqcd_nmonto_pago.ToString();
            mto_old_saldo = oBe.lqcd_nmonto_pago;
            mto_cab_restante = mto_cab_restante + mto_old_saldo;
            tipo_cambio = oBe.lqcd_ntipo_cambio_pago;
            txtTipoDoc.Tag = oBe.lqcd_iid_tipo_doc;
            /**************************************************************************/
            bteProveedor.Tag = oBe.lqcd_iid_proveedor;
            bteProveedor.Text = oBe.codigo_provedor;
            txtProveedor.Text = oBe.proveedor_nombre;
            /**************************************************************************/
            txtTipoDoc.Text = oBe.tip_doc_abreviatura;
            txtTipoDoc.Tag = oBe.lqcd_iid_tipo_doc;
            txtClaseDoc.Text = String.Format("{0:00}", oBe.intIidClaseDoc);
            txtClaseDoc.Tag = oBe.lqcd_iid_clase_tipo_doc;
            txtNroDoc.Text = oBe.lqcd_vnumero_doc;
            /**************************************************************************/
            txtPorcIGV.Text = oBe.lqcd_nporcent_igv.ToString();
            txtPorcRenta.Text = oBe.lqcd_nporc_rta_cuarta.ToString();
            /**************************************************************************/
            txtDestGravado.Text = oBe.lqcd_nmonto_afecto.ToString();
            txtDesInaf.Text = oBe.lqcd_nmonto_inafecto.ToString();
            txtDesMixto.Text = oBe.lqcd_nmonto_dest_mixto.ToString();
            /**************************************************************************/
            txtMontoIGV.Text = oBe.lqcd_nmonto_igv.ToString();
            txtMontoRenta.Text = oBe.lqcd_nmonto_rta_cuarta.ToString();
            /**************************************************************************/
            bteCuenta.Tag = oBe.lqcd_iid_cuenta_contable;
            bteCuenta.Text = oBe.numero_cuenta_contable;
            txtCuentaDes.Text = oBe.cuenta_descripcion;
            /**************************************************************************/
            bteCCosto.Tag = oBe.lqcd_iid_centro_costo;
            bteCCosto.Text = oBe.codigo_ccosto;
            txtCCosto.Text = oBe.ccosto_descripcion;
            bteCCosto.Enabled = (Convert.ToInt32(oBe.lqcd_iid_centro_costo) > 0) ? true : false;
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private void bteConceptoCaja_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarConceptoCaja();
        }

        private void bteConceptoCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarConceptoCaja();
        }

        private void listarConceptoCaja()
        {
            ECuentaContable oBeCtaCtbl = new ECuentaContable();
            using (FrmListarConceptoCaja frm = new FrmListarConceptoCaja())
            {
                frm.flagConDoc = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    /*****************************************************/
                    bteConceptoCaja.Tag = frm._Be.icod_concepto_caja;
                    bteConceptoCaja.Text = frm._Be.ccod_concep_mov;
                    txtConcepto.Text = frm._Be.vdescripcion;
                    /*****************************************************/
                    txtTipoDoc.Text = frm._Be.tdocc_vabreviatura_tipo_doc;
                    txtTipoDoc.Tag = frm._Be.tdocc_icod_tipo_doc;
                    txtClaseDoc.Text = String.Format("{0:00}", frm._Be.tdodc_iid_codigo_doc_det);
                    txtClaseDoc.Tag = frm._Be.iid_correlativo;
                    /*****************************************************/
                    if (Convert.ToInt32(frm._Be.iid_cuenta_contable) > 0)
                    {
                        oBeCtaCtbl = lstCuentaContable.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(frm._Be.iid_cuenta_contable)).ToList()[0];
                        bteCuenta.Tag = oBeCtaCtbl.ctacc_icod_cuenta_contable;
                        bteCuenta.Text = oBeCtaCtbl.ctacc_numero_cuenta_contable;
                        txtCuentaDes.Text = oBeCtaCtbl.ctacc_nombre_descripcion;
                        /*****************************************************/
                        bteCCosto.Enabled = oBeCtaCtbl.ctacc_iccosto;
                    }
                    /*****************************************************/

                    if (frm._Be.tdocc_vabreviatura_tipo_doc == "RHO")
                    {
                        txtMontoRenta.Enabled = true;
                    }
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

                if (String.IsNullOrEmpty(txtNroDoc.Text))
                {
                    oBase = txtNroDoc;
                    throw new ArgumentException("Ingrese el Nro. de Documento");
                }

                if (Convert.ToInt32(bteCuenta.Tag) == 0)
                {
                    oBase = bteCuenta;
                    throw new ArgumentException("Ingrese el Nro. de Cuenta Contable, que será utilizado para la creación del Doc. Por Pagar");
                }

                if (bteCCosto.Enabled)
                    if (Convert.ToInt32(bteCCosto.Tag) == 0)
                    {
                        oBase = bteCCosto;
                        throw new ArgumentException("Ingrese el Centro de Costo, requerido por la Cuenta Contable");
                    }
                int? intNullVal = null;
                oBe.lqcd_sfecha_liquid = Convert.ToDateTime(dtmFecha.EditValue);
                oBe.lqcd_icod_concepto_caja = Convert.ToInt32(bteConceptoCaja.Tag);
                oBe.lqcd_vdescripcion_movim = txtConcepto.Text;

                oBe.lqcd_nmonto_pago = Convert.ToDecimal(txtMonto.Text);
                oBe.lqcd_ntipo_cambio_pago = tipo_cambio;
                oBe.lqcd_flag_estado = 1;
                oBe.lqcd_iid_proveedor = Convert.ToInt32(bteProveedor.Tag);
                oBe.lqcd_iid_tipo_doc = Convert.ToInt32(txtTipoDoc.Tag);
                oBe.tip_doc_abreviatura = txtTipoDoc.Text;
                oBe.lqcd_iid_clase_tipo_doc = Convert.ToInt32(txtClaseDoc.Tag);
                oBe.lqcd_vnumero_doc = txtNroDoc.Text;
                //**//
                oBe.concepto_abreviatura = bteConceptoCaja.Text;
                oBe.codigo_provedor = bteProveedor.Text;
                oBe.proveedor_nombre = txtProveedor.Text;
                oBe.tip_doc_abreviatura = txtTipoDoc.Text;

                oBe.lq_icod_tipo_doc_pago = Convert.ToInt32(txtTipoDoc.Tag);
                oBe.lq_icod_clase_doc_pago = Convert.ToInt32(txtClaseDoc.Tag);

                oBe.lq_nro_doc_pago = nro_caja_cab + "-" + nro_liquidacion;
                //**//
                oBe.lqcd_vtipo_movimiento = Parametros.strTipoLiqCajaGenProvision;
                //**//
                oBe.lqcd_nmonto_afecto = Convert.ToDecimal(txtDestGravado.Text);
                oBe.lqcd_nmonto_inafecto = Convert.ToDecimal(txtDesInaf.Text);
                oBe.lqcd_nmonto_dest_mixto = Convert.ToDecimal(txtDesMixto.Text);

                oBe.lqcd_nporcent_igv = Convert.ToDecimal(txtPorcIGV.Text);
                oBe.lqcd_nmonto_igv = Convert.ToDecimal(txtMontoIGV.Text);

                oBe.lqcd_nporc_rta_cuarta = Convert.ToDecimal(txtPorcRenta.Text);
                oBe.lqcd_nmonto_rta_cuarta = Convert.ToDecimal(txtMontoRenta.Text);

                oBe.lqcd_iid_cuenta_contable = Convert.ToInt32(bteCuenta.Tag);
                oBe.lqcd_iid_centro_costo = (Convert.ToInt32(bteCCosto.Tag) != 0) ? Convert.ToInt32(bteCCosto.Tag) : intNullVal;

                if (Status == BSMaintenanceStatus.CreateNew)
                {

                    //oBe.lqcd_iid_tipo_analitica = tipo_analitica;
                    //oBe.lqcd_iid_analitica = icod_analitica;

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
            listarProveedor();
        }

        private void bteProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarProveedor();
        }
        private void listarProveedor()
        {
            try
            {
                FrmListarProveedor frm = new FrmListarProveedor();
                frm.Carga();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteProveedor.Text = frm._Be.vcod_proveedor;
                    bteProveedor.Tag = frm._Be.iid_icod_proveedor;
                    txtProveedor.Text = frm._Be.vnombrecompleto;
                    /*****************************************************/
                    oBe.lqcd_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                    oBe.lqcd_iid_analitica = frm._Be.anac_icod_analitica;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void FrmManteLiquidacionCajaDetGeneDoc_Load(object sender, EventArgs e)
        {
            lstCuentaContable = new BContabilidad().listarCuentaContable();
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtPorcIGV.Text = Parametros.strPorcIGV;
                txtPorcRenta.Text = Parametros.strPorcRenta4taCat;
            }
        }

        private void txtDestGravado_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDestGravado.Text != "0.00")
            {
                txtDesInaf.Text = "0.00";
                txtDesMixto.Text = "0.00";
            }
            /**/
            calcularIGV();
        }

        private void txtDesMixto_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDesMixto.Text != "0.00")
            {
                txtDesInaf.Text = "0.00";
                txtDestGravado.Text = "0.00";
            }
            /**/
            calcularIGV();
        }

        private void calcularIGV()
        {
            decimal dcmlTotal = Convert.ToDecimal(txtDestGravado.Text) + Convert.ToDecimal(txtDesMixto.Text);
            txtMontoIGV.Text = (dcmlTotal * (Convert.ToDecimal(txtPorcIGV.Text) / 100)).ToString();
            txtMonto.Text = (dcmlTotal + Convert.ToDecimal(txtMontoIGV.Text)).ToString();
            if (txtTipoDoc.Text == "RHO")
            {
                txtMontoIGV.Text = "0.00";
                txtMontoRenta.Text = (dcmlTotal * (Convert.ToDecimal(txtPorcRenta.Text) / 100)).ToString();
                txtMonto.Text = (dcmlTotal + Convert.ToDecimal(txtMontoIGV.Text) - Convert.ToDecimal(txtMontoRenta.Text)).ToString();
            }
        }

        private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCuenta();
        }

        private void clear()
        {
            txtCuentaDes.Text = string.Empty;
            bteCuenta.Tag = null;
            bteCuenta.Text = string.Empty;
            //
            txtCCosto.Text = string.Empty;
            bteCCosto.Tag = null;
            bteCCosto.Text = string.Empty;
            bteCCosto.Enabled = false;
        }

        private void listarCuenta()
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clear();
                    /*************************************************************/
                    bteCuenta.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;
                    /*************************************************************/
                    bteCCosto.Enabled = frm._Be.ctacc_iccosto;
                }
            }
        }

        private void bteCCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCentroCosto();
        }

        private void listarCentroCosto()
        {
            using (frmListarCentroCosto Ccosto = new frmListarCentroCosto())
            {
                if (Ccosto.ShowDialog() == DialogResult.OK)
                {
                    bteCCosto.Text = Ccosto._Be.cecoc_vcodigo_centro_costo;
                    bteCCosto.Tag = Ccosto._Be.cecoc_icod_centro_costo;
                    txtCCosto.Text = Ccosto._Be.cecoc_vdescripcion;
                }
            }
        }

        private void txtDesInaf_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDesInaf.Text != "0.00")
            {
                txtDestGravado.Text = "0.00";
                txtDesMixto.Text = "0.00";
            }
            /**/
            txtMonto.Text = txtDesInaf.Text;
        }
    }
}