using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Otros.Tesoreria.Caja;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class FrmManteMovVariosDetLiquidacion : DevExpress.XtraEditors.XtraForm
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



        public FrmManteMovVariosDetLiquidacion()
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
            bteCuenta.Tag = Obj.iid_cuenta_contable;
            bteCuenta.Text = Obj.NumeroCuentaContable;
            txtCuentaDes.Text = Obj.DescripcionCuentaContable;

            bteAnalitica.Tag = Obj.tablc_icod_tipo_analitica;
            bteAnalitica.Text = string.Format("{0:00}", Obj.tablc_icod_tipo_analitica);

            bteSubAnalitica.Tag = Obj.icod_analitica;
            bteSubAnalitica.Text = Obj.anac_vdescripcion;
            ana_iid = Obj.anac_iid_analitica;
            bteCCosto.Tag = Obj.icod_centro_costo;
            bteCCosto.Text = Obj.CodigoCentroCosto;
            txtcentrocosto.Text = Obj.DescripcionCentroCosto;

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
                bteCuenta.Enabled = false;
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
                if (bteAnalitica.Tag != null)
                {
                    Obj.tablc_icod_tipo_analitica = Convert.ToInt32(bteAnalitica.Tag);
                    Obj.vdes_analisis = string.Format("{0:00}", bteAnalitica.Tag) + "." + ana_iid;
                }
                else Obj.tablc_icod_tipo_analitica = null;
                if (bteSubAnalitica.Tag != null) { Obj.icod_analitica = Convert.ToInt32(bteSubAnalitica.Tag); }
                else Obj.icod_analitica = null;
                Obj.iid_cuenta_contable = Convert.ToInt64(bteCuenta.Tag);
                Obj.mto_mov_soles = (tip_mon == 3) ? Convert.ToDecimal(txtMonto.Text) : Math.Round(Convert.ToDecimal(txtMonto.Text) * TipoCambio, 2);
                Obj.mto_mov_dolar = (tip_mon == 4) ? Convert.ToDecimal(txtMonto.Text) : Math.Round(Convert.ToDecimal(txtMonto.Text) / TipoCambio, 2);
                Obj.mto_retenido_soles = 0;
                Obj.mto_retenido_dolar = 0;
                Obj.mto_detalle_soles = 0;
                Obj.mto_detalle_dolar = 0;
                Obj.vglosa = txtConcepto.Text;
                if (bteCCosto.Tag != null) { Obj.icod_centro_costo = Convert.ToInt32(bteCCosto.Tag); }
                else Obj.icod_centro_costo = null;
                Obj.iusuario_crea = Valores.intUsuario;
                Obj.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                Obj.mobdc_flag_estado = true;
                //
                Obj.NumeroCuentaContable = bteCuenta.Text;
                Obj.DescripcionCuentaContable = txtCuentaDes.Text;
                Obj.DescripcionCentroCosto = bteCCosto.Text;
                Obj.anac_vdescripcion = bteSubAnalitica.Text;
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
        private void clear()
        {
            bteCCosto.Text = string.Empty;
            bteCCosto.Tag = null;
            txtcentrocosto.Text = string.Empty;
            bteAnalitica.Text = string.Empty;
            bteAnalitica.Tag = null;
            bteSubAnalitica.Text = string.Empty;
            bteSubAnalitica.Tag = null;
            bteCCosto.Enabled = false;
            bteAnalitica.Enabled = false;
            bteSubAnalitica.Enabled = false;
        }
        private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCuenta();
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

        private void bteCCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCentroCosto();
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
        private void ListarSubAnalitica()
        {
            using (frmListarAnaliticaDetalle frm = new frmListarAnaliticaDetalle())
            {
                frm.intTipoAnalitica = Convert.ToInt32(bteAnalitica.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteSubAnalitica.Tag = frm._Be.anad_icod_analitica;
                    bteSubAnalitica.Text = frm._Be.anad_vdescripcion;
                    ana_iid = frm._Be.anad_iid_analitica;
                }
            }
        }
        private List<ETablaRegistro> auxA = new List<ETablaRegistro>();
        private List<ECuentaContable> aux = new List<ECuentaContable>();
        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        private List<ETablaRegistro> ListaAnalitica = new List<ETablaRegistro>();
        private List<EAnaliticaDetalle> ListaSubAnalitica = new List<EAnaliticaDetalle>();
        private List<ECentroCosto> auxCC = new List<ECentroCosto>();
        private List<ECentroCosto> ListaCentroCosto = new List<ECentroCosto>();

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
            bteAnalitica.Text = string.Empty;
            //
            bteSubAnalitica.Tag = null;
            bteSubAnalitica.Text = string.Empty;
            //
            bteCCosto.Enabled = false;
            bteAnalitica.Enabled = false;
            bteSubAnalitica.Enabled = false;
        }
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

        private void FrmManteMovVariosDet_Load(object sender, EventArgs e)
        {
            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            ListaAnalitica = new BGeneral().listarTablaRegistro(24);
            ListaCentroCosto = new BContabilidad().listarCentroCosto();
            LoadMask();
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
            });
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

        private void bteCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarCuenta();
        }

        private void bteCCosto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarCentroCosto();
        }

        private void bteAnalitica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarAnalitica();
        }

        private void bteSubAnalitica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarSubAnalitica();
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
    }
}