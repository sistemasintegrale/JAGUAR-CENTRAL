using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Compras;
using SGE.WindowForms.Otros.Tesoreria.Caja;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class FrmManteCtaxPagarLiquidacion : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteMovVariosDet));
        private BSMaintenanceStatus mStatus;
        public ELibroBancosDetalle Obj = new ELibroBancosDetalle();
        public int tip_mon = 0;
        public int Cab_icod_correlativo = 0;
        public List<ELibroBancosDetalle> oDetailList = new List<ELibroBancosDetalle>();
        string ana_iid = "";
        public decimal saldo = 0;
        public decimal TipoCambio = 0;
        public int icodTipo;
        public int TipoMoneda2 = 0;


        public FrmManteCtaxPagarLiquidacion()
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
        private void CargaModify()
        {

            txtMonto.Text = Obj.mnto.ToString();
            txtConcepto.Text = Obj.vglosa;

            dtmFecha.EditValue = Obj.mobdc_sfecha;
            bteConceptoCaja.Tag = Obj.mobdc_icod_concepto_caja;
            bteConceptoCaja.Text = Obj.concepto_abreviatura;
        }
        private void StatusControl()
        {
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                btnAgregar.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                btnModificar.Enabled = false;
            }

        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            txtMonto.Text = saldo.ToString();
        }
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            CargaModify();
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToDecimal(txtMonto.Text) == 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("Ingrese monto");
                }
                if (string.IsNullOrEmpty(txtConcepto.Text))
                {
                    oBase = txtConcepto;
                    throw new ArgumentException("Ingrese concepto");
                }
                Obj.icod_correlativo_cabecera = Cab_icod_correlativo;
                Obj.vnumero_doc = null;
                Obj.mobdc_icod_proveedor = null;
                Obj.mobdc_icod_cliente = null;
                Obj.mto_mov_soles = (tip_mon == 3) ? Convert.ToDecimal(txtMonto.Text) : Math.Round(Convert.ToDecimal(txtMonto.Text) * TipoCambio, 2);
                Obj.mto_mov_dolar = (tip_mon == 4) ? Convert.ToDecimal(txtMonto.Text) : Math.Round(Convert.ToDecimal(txtMonto.Text) / TipoCambio, 2);
                Obj.mto_retenido_soles = 0;
                Obj.mto_retenido_dolar = 0;
                Obj.mto_detalle_soles = 0;
                Obj.mto_detalle_dolar = 0;
                Obj.vglosa = txtConcepto.Text;
                Obj.iusuario_crea = Valores.intUsuario;
                Obj.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                Obj.mobdc_flag_estado = true;
                //
                Obj.anac_iid_analitica = ana_iid;
                Obj.mnto = Convert.ToDecimal(txtMonto.Text);

                Obj.mobdc_sfecha = Convert.ToDateTime(dtmFecha.EditValue);
                Obj.mobdc_icod_concepto_caja = Convert.ToInt32(bteConceptoCaja.Tag);

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obj.TipOper = 1;
                    oDetailList.Add(Obj);
                }
                else
                {
                    if (Obj.TipOper != 1)
                    {
                        Obj.TipOper = 2;
                    }
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
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }





        private List<ETablaRegistro> auxA = new List<ETablaRegistro>();
        private List<ECuentaContable> aux = new List<ECuentaContable>();
        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        private List<ETablaRegistro> ListaAnalitica = new List<ETablaRegistro>();
        private List<EAnaliticaDetalle> ListaSubAnalitica = new List<EAnaliticaDetalle>();
        private List<ECentroCosto> auxCC = new List<ECentroCosto>();
        private List<ECentroCosto> ListaCentroCosto = new List<ECentroCosto>();




        private void FrmManteMovVariosDet_Load(object sender, EventArgs e)
        {
            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            ListaAnalitica = new BGeneral().listarTablaRegistro(24);
            ListaCentroCosto = new BContabilidad().listarCentroCosto();

        }






        private void bteConceptoCaja_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarConceptoCaja();
        }
        private void ListarConceptoCaja()
        {
            using (FrmListarConceptoCajaEfectivo frm = new FrmListarConceptoCajaEfectivo())
            {
                frm.flagConDoc = false;
                frm.icodTipo = icodTipo;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteConceptoCaja.Tag = frm._Be.icod_concepto_caja;
                    bteConceptoCaja.Text = frm._Be.ccod_concep_mov;
                }
            }
        }

        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
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
                //tipo_analitica = 5;
                //icod_analitica = frm._Be.anac_icod_analitica;

            }
        }

        private void bteDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
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
                if (oDetailList.Where(x => x.doxpc_icod_correlativo == frm._oBe.doxpc_icod_correlativo).ToList().Count > 0)
                {
                    XtraMessageBox.Show("El documento ya existe en el detalle", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bteDocumento.Tag = frm._oBe.doxpc_icod_correlativo;
                bteDocumento.Text = frm._oBe.doxpc_vnumero_doc;
                txtTipoDoc.Text = frm._oBe.tdocc_vabreviatura_tipo_doc;
                txtTipoDoc.Tag = frm._oBe.tdocc_icod_tipo_doc;
                //icod_doc_clase = Convert.ToInt32(frm._oBe.tdodc_iid_correlativo);
                txtMtoTotal.Text = frm._oBe.doxpc_nmonto_total_documento.ToString();
                txtMtoPagado.Text = frm._oBe.doxpc_nmonto_total_pagado.ToString();
                txtMtoSaldo.Text = frm._oBe.doxpc_nmonto_total_saldo.ToString();
                lblTotal.Text = (frm._oBe.tablc_iid_tipo_moneda == 3) ? "Mto. Saldo S/. :" : "Mto. Saldo US$ :";
                TipoMoneda2 = frm._oBe.tablc_iid_tipo_moneda;
            }
        }
    }
}