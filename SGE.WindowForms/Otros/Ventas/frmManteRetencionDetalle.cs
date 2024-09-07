using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteRetencionDetalle : DevExpress.XtraEditors.XtraForm
    {
        public int id_tipo_moneda_Cab = 0;
        public int id_tipo_moneda_Det = 0;
        public decimal TipoCambioCab = 0;
        public int id_cliente = 0;
        private BSMaintenanceStatus mStatus;
        public ERetencionDet oBe = new ERetencionDet();
        public List<ERetencionDet> lstDetalle = new List<ERetencionDet>();

        public frmManteRetencionDetalle()
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                bteNroDocumento.Enabled = Enabled;
        }
        private void bteNroDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarDoc();
        }
        public void setValues()
        {
            txtDocumento.Text = oBe.strTipoDoc;
            txtDocumento.Tag = oBe.tdoc_icod_tipo_documento;
            bteNroDocumento.Text = oBe.retd_vnumero_documento;
            bteNroDocumento.Tag = oBe.intIcodDXC;
            dtFecha.EditValue = oBe.retd_sfec_documento;
            lkpTipoMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            txtTotal.Text = oBe.retd_nmto_total_doc.ToString();
            txtPago.Text = oBe.retd_nmto_pago_doc.ToString();
            txtRetencion.Text = oBe.retd_nmto_retencion.ToString();
        }

        private void listarDoc()
        {
            decimal? Pago = 0;
            List<EParametroContable> lstParametroCont = new List<EParametroContable>();
            lstParametroCont = new BContabilidad().listarParametroContable();

            frmListarDXCNoCancelado frm = new frmListarDXCNoCancelado();
            frm.IcodCliente = id_cliente;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bteNroDocumento.Tag = Convert.ToInt64(frm._oBe.doxcc_icod_correlativo);
                bteNroDocumento.Text = frm._oBe.doxcc_vnumero_doc;
                /**************************************************************************/
                txtDocumento.Tag = Convert.ToInt32(frm._oBe.tdocc_icod_tipo_doc);
                txtDocumento.Text = frm._oBe.Abreviatura;
                /**************************************************************************/
                dtFecha.EditValue = frm._oBe.doxcc_sfecha_doc;
                lkpTipoMoneda.EditValue = frm._oBe.tablc_iid_tipo_moneda;

                txtSaldo.Text = Convert.ToDecimal(frm._oBe.doxcc_nmonto_saldo).ToString();
                txtTotal.Text = Convert.ToDecimal(frm._oBe.doxcc_nmonto_total).ToString();
                id_tipo_moneda_Det = frm._oBe.tablc_iid_tipo_moneda;
                if (id_tipo_moneda_Cab == id_tipo_moneda_Det)
                {
                    txtPago.Text = Convert.ToDecimal(frm._oBe.doxcc_nmonto_total).ToString();
                }
                else
                {
                    if (id_tipo_moneda_Cab == 3)
                    {
                        Pago = frm._oBe.doxcc_nmonto_total * TipoCambioCab;
                        txtPago.Text = Pago.ToString();
                    }
                    else if (id_tipo_moneda_Cab == 4)
                    {
                        Pago = frm._oBe.doxcc_nmonto_total / TipoCambioCab;
                        txtPago.Text = Pago.ToString();
                    }
                }

                //txtPago.Text = Convert.ToDecimal(frm._oBe.doxcc_nmonto_total).ToString();
                /*Calculando la RETENCION, el porcentaje debe ser jalado desde parámetros*/
                var dblRetencion = Math.Round((Convert.ToDecimal(txtPago.Text) * Convert.ToDecimal(lstParametroCont[0].parac_Porcentaje_Retencion_ventas) / 100), 2);
                txtRetencion.Text = dblRetencion.ToString();
            }
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

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            decimal Total = 0;
            try
            {
                if (Convert.ToDecimal(txtRetencion.Text) == 0)
                {
                    oBase = txtRetencion;
                    throw new ArgumentException("Ingresar el monto de Retencion");
                }

                //if (Convert.ToDecimal(txtRetencion.Text) > Convert.ToDecimal(txtTotal.Text))
                //{
                //    oBase = txtRetencion;
                //    throw new ArgumentException("El monto de Retencion no debe ser mayor al monto del documento");
                //}
                if (id_tipo_moneda_Cab == id_tipo_moneda_Det)
                {
                    if (Convert.ToDecimal(txtRetencion.Text) > Convert.ToDecimal(txtTotal.Text))
                    {
                        oBase = txtRetencion;
                        throw new ArgumentException("El monto de Retencion no debe ser mayor al monto del documento");
                    }
                }
                else
                {
                    if (id_tipo_moneda_Cab == 3)
                    {
                        Total = Math.Round(Convert.ToDecimal(txtTotal.Text) * TipoCambioCab, 2);

                        if (Convert.ToDecimal(txtRetencion.Text) > Total)
                        {
                            oBase = txtRetencion;
                            throw new ArgumentException("El monto de Retencion no debe ser mayor al monto del documento");
                        }
                    }
                    else if (id_tipo_moneda_Cab == 4)
                    {
                        Total = Math.Round(Convert.ToDecimal(txtTotal.Text) / TipoCambioCab, 2);
                        if (Convert.ToDecimal(txtRetencion.Text) > Total)
                        {
                            oBase = txtRetencion;
                            throw new ArgumentException("El monto de Retencion no debe ser mayor al monto del documento");
                        }
                    }
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                    if (lstDetalle.Where(x => x.pdxpc_icod_correlativo == Convert.ToInt32(bteNroDocumento.Tag)).ToList().Count > 0)
                    {
                        oBase = bteNroDocumento;
                        throw new ArgumentException("El documento seleccionado ya fue añadido en el detalle");
                    }

                oBe.tdoc_icod_tipo_documento = Convert.ToInt32(txtDocumento.Tag);
                oBe.intIcodDXC = Convert.ToInt64(bteNroDocumento.Tag);
                oBe.retd_vnumero_documento = bteNroDocumento.Text;
                oBe.retd_sfec_documento = Convert.ToDateTime(dtFecha.EditValue);
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpTipoMoneda.EditValue);
                oBe.retd_nmto_total_doc = Convert.ToDecimal(txtTotal.Text);
                oBe.retd_nmto_pago_doc = Convert.ToDecimal(txtPago.Text);
                oBe.retd_nmto_retencion = Convert.ToDecimal(txtRetencion.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                /**/
                oBe.strMoneda = lkpTipoMoneda.Text;
                oBe.strTipoDoc = txtDocumento.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                    if (oBe.intTipoOperacion != 1)
                        oBe.intTipoOperacion = 2;

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
                    this.DialogResult = DialogResult.OK;
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

        private void frmMantePercepcionDetalle_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpTipoMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                setValues();
        }

        private void txtPago_EditValueChanged(object sender, EventArgs e)
        {
            /*Calculando la RETENCION, el porcentaje debe ser jalado desde parámetros*/
            List<EParametroContable> lstParametroCont = new List<EParametroContable>();
            lstParametroCont = new BContabilidad().listarParametroContable();

            var dblRetencion = Math.Round((Convert.ToDecimal(txtPago.Text) * Convert.ToDecimal(lstParametroCont[0].parac_Porcentaje_Retencion_ventas) / 100), 2);
            txtRetencion.Text = dblRetencion.ToString();
        }

        private void txtTotal_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}