using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    public partial class FrmManteLiquidacionCajaDet : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteLiquidacionCajaDet));
        private BSMaintenanceStatus mStatus;
        public List<ELiquidacionCajaDet> oDetail;
        public decimal tipo_cambio;
        public ELiquidacionCajaDet oBe = new ELiquidacionCajaDet();
        public string codigo_anlitica = "";
        public decimal mto_cab_restante;



        public FrmManteLiquidacionCajaDet()
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
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                btnAgregar.Enabled = false;
            }
        }

        private void Cargar()
        {
            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            ListaAnalitica = new BGeneral().listarTablaRegistro(24);
            ListaCentroCosto = new BContabilidad().listarCentroCosto();
            LoadMask();
        }
        public void CargarModify()
        {
            dtmFecha.EditValue = oBe.lqcd_sfecha_liquid;
            bteConceptoCaja.Tag = oBe.lqcd_icod_concepto_caja;
            txtConcepto.Text = oBe.lqcd_vdescripcion_movim;
            txtMonto.Text = oBe.lqcd_nmonto_pago.ToString();
            mto_cab_restante = mto_cab_restante + oBe.lqcd_nmonto_pago;
            bteCuenta.Tag = oBe.lqcd_iid_cuenta_contable;
            bteCCosto.Tag = oBe.lqcd_iid_centro_costo;
            bteAnalitica.Tag = oBe.lqcd_iid_tipo_analitica;
            bteSubAnalitica.Tag = oBe.lqcd_iid_analitica;
            tipo_cambio = oBe.lqcd_ntipo_cambio_pago;
            //**//
            bteCuenta.Enabled = (bteCuenta.Tag == null) ? false : true;
            bteCCosto.Enabled = (bteCCosto.Tag == null) ? false : true;
            bteAnalitica.Enabled = (bteAnalitica.Tag == null) ? false : true;
            bteSubAnalitica.Enabled = (bteSubAnalitica.Tag == null) ? false : true;
            //*//
            bteConceptoCaja.Text = oBe.concepto_abreviatura;
            bteCuenta.Text = oBe.numero_cuenta_contable;
            txtCuentaDes.Text = oBe.cuenta_descripcion;
            bteCCosto.Text = oBe.codigo_ccosto;
            txtcentrocosto.Text = oBe.codigo_ccosto;
            codigo_anlitica = oBe.iid_analitica;
            bteAnalitica.Text = oBe.lqcd_iid_tipo_analitica.ToString();
            bteSubAnalitica.Text = oBe.analitica_descripcion;
        }
        private void LoadMask()
        {
            List<EParametroContable> mlista = (new BContabilidad()).listarParametroContable();
            mlista.ForEach(obe =>
            {
                this.bteCuenta.Properties.Mask.BeepOnError = true;
                this.bteCuenta.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCuenta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCuenta.Properties.Mask.ShowPlaceHolders = false;
                this.bteCuenta.Properties.Mask.UseMaskAsDisplayFormat = true;
            });
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
                frm.flagConDoc = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteConceptoCaja.Tag = frm._Be.icod_concepto_caja;
                    bteConceptoCaja.Text = frm._Be.ccod_concep_mov;
                    bteCuenta.Tag = frm._Be.iid_cuenta_contable;
                    bteCuenta.Text = frm._Be.iid_cuenta_contable.ToString();
                    txtCuentaDes.Text = frm._Be.cuenta_vdes;
                    bteCCosto.Enabled = frm._Be.ccosto_flag;
                    bteAnalitica.Enabled = (frm._Be.tipo_analitica != null) ? true : false;
                    bteAnalitica.Tag = (frm._Be.tipo_analitica != null) ? frm._Be.tipo_analitica : null;
                    bteAnalitica.Text = (frm._Be.tipo_analitica != null) ? frm._Be.tipo_analitica : null;
                    bteSubAnalitica.Enabled = (frm._Be.tipo_analitica != null) ? true : false;
                }
            }
        }

        private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCuenta();
        }

        private void bteCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarCuenta();
        }
        private List<ETablaRegistro> auxA = new List<ETablaRegistro>();
        private List<ECuentaContable> aux = new List<ECuentaContable>();
        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        private List<ETablaRegistro> ListaAnalitica = new List<ETablaRegistro>();
        private List<EAnaliticaDetalle> ListaSubAnalitica = new List<EAnaliticaDetalle>();
        private List<ECentroCosto> auxCC = new List<ECentroCosto>();
        private List<ECentroCosto> ListaCentroCosto = new List<ECentroCosto>();

        private void bteCuenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (bteCuenta.Text == "")
            {
                clearcta();
                return;
            }

            aux = mlistCuenta.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuenta.Text.Replace(".", ""))).ToList();


            if (aux.Count == 1)
            {
                bteCuenta.Tag = aux[0].ctacc_icod_cuenta_contable;
                txtCuentaDes.Text = aux[0].ctacc_nombre_descripcion;
                bteCCosto.Enabled = aux[0].ctacc_iccosto;

                bteAnalitica.Enabled = (aux[0].tablc_iid_tipo_analitica != 0) ? true : false;
                bteSubAnalitica.Enabled = (aux[0].tablc_iid_tipo_analitica != 0) ? true : false;

                auxA = ListaAnalitica.Where(x => Convert.ToInt32(x.tarec_icorrelativo_registro) == aux[0].tablc_iid_tipo_analitica).ToList();
                if (auxA.Count == 1)
                {
                    bteAnalitica.Tag = auxA[0].tarec_icorrelativo_registro;
                    bteAnalitica.Text = String.Format("{0:00}", auxA[0].tarec_icorrelativo_registro);
                    ListaSubAnalitica = new BContabilidad().listarAnaliticaDetalle(Convert.ToInt32(bteAnalitica.Tag));
                }
            }
            else
            {
                clearcta();
            }
        }
        private void ListarCuenta()
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clear();
                    bteCuenta.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;
                    bteCCosto.Enabled = frm._Be.ctacc_iccosto;

                    if (frm._Be.tablc_iid_tipo_analitica != 0)
                    {
                        bteAnalitica.Enabled = true;
                        bteSubAnalitica.Enabled = true;
                        bteAnalitica.Tag = frm._Be.tablc_iid_tipo_analitica;
                        bteAnalitica.Text = string.Format("{0:00}", frm._Be.tablc_iid_tipo_analitica);
                    }
                }
            }
        }
        private void clear()
        {
            txtCuentaDes.Text = string.Empty;
            bteCuenta.Tag = null;
            bteCuenta.Text = string.Empty;
            //
            txtcentrocosto.Text = string.Empty;
            bteCCosto.Tag = null;
            bteCCosto.Text = string.Empty;
            //                
            bteAnalitica.Tag = null;
            bteAnalitica.Text = null;
            //
            bteSubAnalitica.Tag = null;
            bteSubAnalitica.Text = null;
            //
            bteCCosto.Enabled = false;
            bteAnalitica.Enabled = false;
            bteSubAnalitica.Enabled = false;
        }
        private void clearcta()
        {
            txtCuentaDes.Text = string.Empty;
            bteCuenta.Tag = null;
            //
            txtcentrocosto.Text = string.Empty;
            bteCCosto.Tag = null;
            bteCCosto.Text = string.Empty;
            //                
            bteAnalitica.Tag = null;
            bteAnalitica.Text = null;
            //
            bteSubAnalitica.Tag = null;
            bteSubAnalitica.Text = null;
            //
            bteCCosto.Enabled = false;
            bteAnalitica.Enabled = false;
            bteSubAnalitica.Enabled = false;
        }

        private void bteCCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCentroCosto();
        }

        private void bteCCosto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarCentroCosto();
        }

        private void bteCCosto_KeyUp(object sender, KeyEventArgs e)
        {
            if (bteCCosto.Text == "")
                return;
            auxCC = ListaCentroCosto.Where(x => x.cecoc_vcodigo_centro_costo == bteCCosto.Text).ToList();

            if (auxCC.Count == 1)
            {
                txtcentrocosto.Text = auxCC[0].cecoc_vdescripcion;
                bteCCosto.Tag = auxCC[0].cecoc_icod_centro_costo;
            }
            else
            {
                txtcentrocosto.Text = string.Empty;
                bteCCosto.Tag = null;
            }
        }
        private void ListarCentroCosto()
        {
            using (frmListarCentroCosto Ccosto = new frmListarCentroCosto())
            {
                if (Ccosto.ShowDialog() == DialogResult.OK)
                {
                    bteCCosto.Text = Ccosto._Be.cecoc_vcodigo_centro_costo;
                    bteCCosto.Tag = Ccosto._Be.cecoc_icod_centro_costo;
                    txtcentrocosto.Text = Ccosto._Be.cecoc_vdescripcion;
                }
            }
        }

        private void bteAnalitica_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarAnalitica();
        }

        private void bteAnalitica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarAnalitica();
        }

        private void ListarAnalitica()
        {
            using (frmListarAnalitica frm = new frmListarAnalitica())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAnalitica.Tag = frm._Be.tarec_icorrelativo_registro;
                    bteAnalitica.Text = String.Format("{0:00}", frm._Be.tarec_icorrelativo_registro);

                    bteSubAnalitica.Tag = null;
                    bteSubAnalitica.Text = string.Empty;
                }
            }
        }

        private void bteSubAnalitica_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarSubAnalitica();
        }

        private void bteSubAnalitica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarSubAnalitica();
        }
        private void ListarSubAnalitica()
        {
            using (frmListarAnaliticaDetalle frm = new frmListarAnaliticaDetalle())
            {
                frm.intTipoAnalitica = Convert.ToInt32(bteAnalitica.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteSubAnalitica.Tag = frm._Be.anad_icod_analitica;
                    bteSubAnalitica.Text = frm._Be.anad_vdescripcion;
                    codigo_anlitica = frm._Be.anad_iid_analitica;
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
                    throw new ArgumentException("Seleccione Tipo Movimiento");
                }
                if (bteCuenta.Tag == null)
                {
                    oBase = bteCuenta;
                    throw new ArgumentException("Seleccione Cuenta Contable");
                }
                if (bteCCosto.Enabled == true)
                {
                    if (bteCCosto.Tag == null)
                    {
                        oBase = bteCCosto;
                        throw new ArgumentException("Ingrese Centro de Costo");
                    }
                }
                if (bteAnalitica.Enabled == true)
                {
                    if (bteAnalitica.Tag == null)
                    {
                        oBase = bteAnalitica;
                        throw new ArgumentException("Seleccione Analítica");
                    }
                    if (bteAnalitica.Tag != null)
                    {
                        if (bteSubAnalitica.Tag == null)
                        {
                            oBase = bteSubAnalitica;
                            throw new ArgumentException("Seleccione Sub - Analítica");
                        }
                    }
                }
                if (string.IsNullOrEmpty(txtConcepto.Text))
                {
                    oBase = txtConcepto;
                    throw new ArgumentException("Ingrese concepto");
                }
                int? ValNull = null;

                oBe.lqcd_sfecha_liquid = Convert.ToDateTime(dtmFecha.EditValue);
                oBe.lqcd_icod_concepto_caja = Convert.ToInt32(bteConceptoCaja.Tag);
                oBe.lqcd_vdescripcion_movim = txtConcepto.Text;
                oBe.lqcd_nmonto_pago = Convert.ToDecimal(txtMonto.Text);
                oBe.lqcd_iid_cuenta_contable = Convert.ToInt32(bteCuenta.Tag);
                oBe.lqcd_iid_centro_costo = (bteCCosto.Tag == null) ? ValNull : Convert.ToInt32(bteCCosto.Tag);
                oBe.lqcd_iid_tipo_analitica = (bteAnalitica.Tag == null) ? ValNull : Convert.ToInt32(bteAnalitica.Tag);
                oBe.lqcd_iid_analitica = (bteSubAnalitica.Tag == null) ? ValNull : Convert.ToInt32(bteSubAnalitica.Tag);
                oBe.lqcd_ntipo_cambio_pago = tipo_cambio;
                oBe.lqcd_flag_estado = 1;
                //**//
                oBe.concepto_abreviatura = bteConceptoCaja.Text;
                oBe.numero_cuenta_contable = bteCuenta.Text;
                oBe.cuenta_descripcion = txtCuentaDes.Text;
                oBe.codigo_ccosto = bteCCosto.Text;
                oBe.ccosto_descripcion = txtcentrocosto.Text;
                oBe.analisis = (bteAnalitica.Enabled == true) ? bteAnalitica.Text + "." + codigo_anlitica : "";
                oBe.iid_analitica = codigo_anlitica;
                oBe.analitica_descripcion = bteSubAnalitica.Text;
                //**//
                oBe.lqcd_vtipo_movimiento = Parametros.strTipoLiqCajaCContable;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
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

        private void FrmManteLiquidacionCajaDet_Load(object sender, EventArgs e)
        {
            Cargar();
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
    }
}