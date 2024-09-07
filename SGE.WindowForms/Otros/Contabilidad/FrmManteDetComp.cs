using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmManteDetComp : DevExpress.XtraEditors.XtraForm
    {


        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        private List<ETablaRegistro> ListaAnalitica = new List<ETablaRegistro>();
        private List<ECentroCosto> ListaCentroCosto = new List<ECentroCosto>();
        public int opcion = 0;
        private BSMaintenanceStatus mStatus;
        public FrmManteDetComp()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
        }
        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        #region SenderParameters

        public List<EComprobanteDetalle> mlistDetail = new List<EComprobanteDetalle>();
        public int code;
        public string TipoMoneda = "";
        public decimal TipoCambio = 0;
        public string NumDoc = null;
        public bool indicador = false;
        #endregion

        #region ReplyParameters
        public decimal monto_sol = 0;
        public decimal monto_dol = 0;
        public EComprobanteDetalle obj = new EComprobanteDetalle();
        #endregion

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

        }
        public void SetAdd()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private void cargar()
        {
            BSControls.LoaderLook(LkpTipoDocumento, new BAdministracionSistema().listarTipoDocumentoPorModulo(8), "tdocc_vabreviatura_tipo_doc", "tdocc_icod_tipo_doc", false);

            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            ListaAnalitica = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoAnalitica);
            ListaCentroCosto = new BContabilidad().listarCentroCosto();

            if (opcion == 1)
            {
                labelControl1.Visible = true;
                labelControl2.Visible = true;
                txtTC.Visible = true;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                LkpTipoDocumento.EditValue = 56;
                if (NumDoc != null)
                {
                    txtnrodocumento.Text = obj.vnumero_documento;
                }
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                CargarModify();
            }

        }
        private void CargarModify()
        {
            LkpTipoDocumento.EditValue = obj.icod_tipo_doc;
            //            string[] arreglo = obj.vnumero_documento.Split('-');
            txtnrodocumento.Text = obj.vnumero_documento;
            bteCuenta.Text = obj.viid_cuenta_contable;
            bteCuenta.Tag = obj.iid_cuenta_contable;
            bteCuenta.Enabled = false;
            //
            txtCuentaDes.Text = obj.vdescripcion_cuenta_contable;
            //
            bteCCosto.Text = obj.vcode_centro_costo;
            bteCCosto.Tag = obj.icod_centro_costo;
            txtcentrocosto.Text = obj.cecoc_vdescripcion;
            bteAnalitica.Text = obj.viid_tipo_relacion;
            bteAnalitica.Tag = obj.iid_tipo_relacion;
            bteSubAnalitica.Text = obj.anac_vdescripcion;
            bteSubAnalitica.Tag = obj.iid_relacion;
            if (opcion == 0)
            {
                if (TipoMoneda.Trim() == "NUEVOS SOLES")
                {
                    txtdebe.Text = (string.IsNullOrEmpty(obj.nmto_tot_debe_sol.ToString())) ? "0.00" : obj.nmto_tot_debe_sol.ToString();
                    txthaber.Text = (string.IsNullOrEmpty(obj.nmto_tot_haber_sol.ToString())) ? "0.00" : obj.nmto_tot_haber_sol.ToString();
                }
                if (TipoMoneda.Trim() == "DOLARES")
                {
                    txtdebe.Text = (string.IsNullOrEmpty(obj.nmto_tot_debe_dol.ToString())) ? "0.00" : obj.nmto_tot_debe_dol.ToString();
                    txthaber.Text = (string.IsNullOrEmpty(obj.nmto_tot_haber_dol.ToString())) ? "0.00" : obj.nmto_tot_haber_dol.ToString();
                }
            }
            else
            {
                txtdebe.Text = obj.nmto_tot_debe_sol.ToString();
                txthaber.Text = obj.nmto_tot_haber_sol.ToString();
                txtTC.Text = obj.tipocambio;
            }

            txtglosa.Text = obj.vglosa_linea;
        }
        private void clear()
        {
            //txtnrodocumento.Text = string.Empty;
            //bteCuenta.Text = string.Empty;
            //bteCuenta.Tag = null;
            //txtCuentaDes.Text = string.Empty;
            bteCCosto.Text = string.Empty;
            bteCCosto.Tag = null;
            txtcentrocosto.Text = string.Empty;
            bteAnalitica.Text = string.Empty;
            bteAnalitica.Tag = null;
            bteSubAnalitica.Text = string.Empty;
            bteSubAnalitica.Tag = null;
            //txtdebe.Text = "0.00";
            //txthaber.Text = "0.00";
        }
        private void FrmManteDetComp_Load(object sender, EventArgs e)
        {
            LoadMask();
            cargar();
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                if (mlistDetail.Count > 0)
                {
                    LkpTipoDocumento.EditValue = mlistDetail[mlistDetail.Count - 1].icod_tipo_doc;
                    txtnrodocumento.Text = mlistDetail[mlistDetail.Count - 1].vnumero_documento;
                }
            }
        }

        private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCuentas();
        }
        private void ListarCuentas()
        {
            using (FrmListaCuentaContableC frm = new FrmListaCuentaContableC())
            {
                frm.tipocuenta = true;
                if (opcion == 1) frm.saldo_inicial = 1;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clear();
                    bteCuenta.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;
                    bteCCosto.Enabled = (frm._Be.ctacc_iccosto);

                    if (frm._Be.ctacc_cflag_relacion != 0)
                    {
                        bteAnalitica.Enabled = true;
                        bteSubAnalitica.Enabled = true;
                        bteAnalitica.Tag = frm._Be.tablc_iid_tipo_analitica;
                        bteAnalitica.Text = string.Format("{0:00}", frm._Be.tablc_iid_tipo_cuenta);
                    }
                }
            }
        }
        private void bteCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((e.KeyChar >= (char)48 && e.KeyChar <= (char)57 || e.KeyChar <= (char)8))
            //    e.Handled = false;
            //else
            //    e.Handled = true;
        }
        private void bteCCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCCostos();
        }
        private void ListarCCostos()
        {
            using (FrmListarCentroCostoC Ccosto = new FrmListarCentroCostoC())
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
            ListarAnaliticas();
        }
        private void ListarAnaliticas()
        {
            using (frmListarAnalitica frm = new frmListarAnalitica())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAnalitica.Tag = frm._Be.tarec_icorrelativo_registro;
                    bteAnalitica.Text = frm._Be.tarec_icorrelativo_registro.ToString();

                    bteSubAnalitica.Tag = null;
                    bteSubAnalitica.Text = string.Empty;
                }
            }
        }

        private void bteSubAnalitica_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarSubAnaliticas();
        }
        private void ListarSubAnaliticas()
        {
            using (frmListarAnaliticaDetalle frm = new frmListarAnaliticaDetalle())
            {
                frm.intTipoAnalitica = Convert.ToInt32(bteAnalitica.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteSubAnalitica.Tag = frm._Be.anad_icod_analitica;
                    bteSubAnalitica.Text = frm._Be.anad_vdescripcion;
                }
            }
        }
        private void txtdebe_KeyPress(object sender, KeyPressEventArgs e)
        {
            txthaber.Text = "0.00";
        }

        private void txthaber_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtdebe.Text = "0.00";
        }
        private void Agree()
        {
            BaseEdit oBase = null;
            bool flag = true;
            try
            {
                if (string.IsNullOrEmpty(txtnrodocumento.Text))
                {
                    oBase = txtnrodocumento;
                    throw new ArgumentException("Ingrese Nro. de Documento");
                }
                if (string.IsNullOrEmpty(txtglosa.Text))
                {
                    oBase = txtglosa;
                    throw new ArgumentException("Ingrese glosa");
                }
                if (bteCuenta.Text == string.Empty)
                {
                    oBase = bteCuenta;
                    throw new ArgumentException("Ingrese Número de Cuenta");
                }
                if (bteCuenta.Tag == null)
                {
                    oBase = bteCuenta;
                    throw new ArgumentException("Ingrese un Número de Cuenta válido");
                }
                if (bteCCosto.Enabled == true)
                {
                    if (bteCCosto.Tag == null)
                    {
                        oBase = bteCCosto;
                        throw new ArgumentException("Ingrese Centro de Costo");
                    }
                }
                if (bteAnalitica.Tag != null)
                {
                    if (bteSubAnalitica.Tag == null)
                    {
                        oBase = bteSubAnalitica;
                        throw new ArgumentException("Ingrese Sub - Analítica");
                    }
                }
                if (Convert.ToDecimal(txtdebe.Text) == 0 && Convert.ToDecimal(txthaber.Text) == 0)
                {
                    throw new ArgumentException("Ingrese un monto para campo Debe o Haber");
                }
                if (opcion == 1)
                {
                    if (Convert.ToDecimal(txtTC.Text) == 0)
                    {
                        oBase = txtTC;
                        throw new ArgumentException("Ingrese Tipo de Cambio");
                    }
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obj.operacion = 1;
                    if (mlistDetail.Count == 0)
                        obj.nro_item_det = 1;
                    else
                    {
                        obj.nro_item_det = mlistDetail.Max(x => x.nro_item_det) + 1;
                    }
                    //obj.nro_item_det = mlistDetail.Count + 1;
                }

                if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    //if (indicador) { obj.operacion = 1; }
                    //else { obj.operacion = 2; }
                    if (obj.operacion == 1) { obj.operacion = 1; }
                    else { obj.operacion = 2; }
                }

                obj.vnumero_documento = txtnrodocumento.Text;
                obj.tipo_numero_documento = LkpTipoDocumento.Text + "-" + txtnrodocumento.Text;
                obj.icod_tipo_doc = Convert.ToInt32(LkpTipoDocumento.EditValue);
                obj.vdescripcion_tipo_doc = LkpTipoDocumento.Text;
                obj.iid_cuenta_contable = Convert.ToInt32(bteCuenta.Tag);
                obj.viid_cuenta_contable = bteCuenta.Text;
                obj.vdescripcion_cuenta_contable = txtCuentaDes.Text;
                obj.viid_tipo_relacion = (bteAnalitica.Tag == null) ? "" : bteAnalitica.Tag.ToString();
                if (bteAnalitica.Tag != null) { obj.iid_tipo_relacion = Convert.ToInt32(bteAnalitica.Tag); }
                else { obj.iid_tipo_relacion = null; }
                obj.vcode_centro_costo = txtcentrocosto.Text;
                if (bteCCosto.Tag != null) { obj.icod_centro_costo = Convert.ToInt32(bteCCosto.Tag); }
                else { obj.icod_centro_costo = null; }
                obj.vicod_centro_costo = bteCCosto.Text;
                if (bteSubAnalitica.Tag != null) { obj.iid_relacion = Convert.ToInt32(bteSubAnalitica.Tag); }
                else { obj.iid_relacion = null; }
                obj.viid_relacion = (bteSubAnalitica.Tag == null) ? "" : bteSubAnalitica.Tag.ToString();
                obj.anac_vdescripcion = bteSubAnalitica.Text;
                obj.vglosa_linea = txtglosa.Text;
                obj.cestado = 'A';
                obj.iid_voucher_contable = code;
                obj.ctacc_iid_cuenta_contable_ref = null;
                obj.tipocambio = TipoCambio.ToString();

                if (opcion == 0)
                {
                    if (TipoMoneda.Trim() == "NUEVOS SOLES")
                    {
                        if (Convert.ToDecimal(txtdebe.Text) > 0 && Convert.ToDecimal(txthaber.Text) == 0)
                        {
                            obj.nmto_tot_debe_sol = Convert.ToDecimal(txtdebe.Text);
                            obj.nmto_tot_debe_dol = Math.Round(Convert.ToDecimal(txtdebe.Text) / Convert.ToDecimal(TipoCambio), 2);
                            obj.nmto_tot_haber_dol = 0;
                            obj.nmto_tot_haber_sol = 0;
                            monto_sol = Convert.ToDecimal(obj.nmto_tot_debe_sol);
                            monto_dol = Convert.ToDecimal(obj.nmto_tot_debe_dol);
                        }
                        if (Convert.ToDecimal(txthaber.Text) > 0 && Convert.ToDecimal(txtdebe.Text) == 0)
                        {
                            obj.nmto_tot_debe_sol = 0;
                            obj.nmto_tot_debe_dol = 0;
                            obj.nmto_tot_haber_dol = Math.Round(Convert.ToDecimal(txthaber.Text) / Convert.ToDecimal(TipoCambio), 2);
                            obj.nmto_tot_haber_sol = Convert.ToDecimal(txthaber.Text);
                            monto_sol = Convert.ToDecimal(obj.nmto_tot_haber_sol);
                            monto_dol = Convert.ToDecimal(obj.nmto_tot_haber_dol);
                        }
                    }
                    else if (TipoMoneda.Trim() == "DOLARES")
                    {
                        if (Convert.ToDecimal(txtdebe.Text) > 0 && Convert.ToDecimal(txthaber.Text) == 0)
                        {
                            obj.nmto_tot_debe_sol = Math.Round(Convert.ToDecimal(txtdebe.Text) * Convert.ToDecimal(TipoCambio), 2);
                            obj.nmto_tot_debe_dol = Convert.ToDecimal(txtdebe.Text);
                            obj.nmto_tot_haber_dol = 0;
                            obj.nmto_tot_haber_sol = 0;
                            monto_sol = Convert.ToDecimal(obj.nmto_tot_debe_sol);
                            monto_dol = Convert.ToDecimal(obj.nmto_tot_debe_dol);
                        }
                        if (Convert.ToDecimal(txthaber.Text) > 0 && Convert.ToDecimal(txtdebe.Text) == 0)
                        {
                            obj.nmto_tot_debe_sol = 0;
                            obj.nmto_tot_debe_dol = 0;
                            obj.nmto_tot_haber_dol = Convert.ToDecimal(txthaber.Text);
                            obj.nmto_tot_haber_sol = Math.Round(Convert.ToDecimal(txthaber.Text) * Convert.ToDecimal(TipoCambio), 2);
                            monto_sol = Convert.ToDecimal(obj.nmto_tot_haber_sol);
                            monto_dol = Convert.ToDecimal(obj.nmto_tot_haber_dol);
                        }
                    }
                }
                else
                {
                    obj.nmto_tot_debe_sol = Convert.ToDecimal(txtdebe.Text);
                    obj.nmto_tot_haber_sol = Convert.ToDecimal(txthaber.Text);
                    obj.nmto_tot_debe_dol = Math.Round(Convert.ToDecimal(txtdebe.Text) / Convert.ToDecimal(txtTC.Text), 2);
                    obj.nmto_tot_haber_dol = Math.Round(Convert.ToDecimal(txthaber.Text) / Convert.ToDecimal(txtTC.Text), 2);
                    obj.tipocambio = txtTC.Text;
                }

                var Lista = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuenta.Tag)).ToList();
                Lista.ForEach(Obe =>
                {
                    obj.iid_cautomatica_debe = Obe.ctacc_icod_cuenta_debe_auto;
                    obj.iid_cautomatica_haber = Obe.ctacc_icod_cuenta_haber_auto;
                });
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    mlistDetail.Add(obj);
                    if (Convert.ToInt32(obj.iid_cautomatica_debe) > 0)
                    {
                        agreeCtaAutomatica(obj, monto_sol, monto_dol);
                    }
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    if (Convert.ToInt32(obj.iid_cautomatica_debe) > 0)
                    {
                        ActualizaCtaAutomatica(obj, monto_sol, monto_dol);
                    }
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
            }
            finally
            {
                if (flag)
                    this.DialogResult = DialogResult.OK;
            }
        }
        private void agreeCtaAutomatica(EComprobanteDetalle ob, decimal monto_sol, decimal monto_dol)
        {
            for (int x = 0; x < 2; x++)
            {
                EComprobanteDetalle obj = new EComprobanteDetalle();

                obj.icod_tipo_doc = ob.icod_tipo_doc;
                obj.vdescripcion_tipo_doc = ob.vdescripcion_tipo_doc;
                obj.vnumero_documento = ob.vnumero_documento;

                if (x == 0)
                {
                    obj.nro_item_det = ob.nro_item_det + 1;
                    obj.viid_cuenta_contable = ob.iid_cautomatica_debe.ToString();
                    obj.iid_cuenta_contable = Convert.ToInt32(ob.iid_cautomatica_debe);
                    /*-------------------------------------------------------------------------------------------*/
                    var cta = mlistCuenta.Where(a => a.ctacc_icod_cuenta_contable == obj.iid_cuenta_contable).ToList();
                    if (cta.Count == 0)
                    {
                        throw new ArgumentException(String.Format("Cuenta contable automática {0} no figura en los registros de SUBCUENTAS", ob.iid_cautomatica_debe));
                    }
                    if (Convert.ToInt32(cta[0].tablc_iid_tipo_cuenta) > 0)
                    {
                        obj.iid_tipo_relacion = ob.iid_tipo_relacion;
                        obj.iid_relacion = ob.iid_relacion;
                    }
                    if (Convert.ToInt32(cta[0].strFlagCCosto) == 1)
                        obj.icod_centro_costo = ob.icod_centro_costo;
                    /*-------------------------------------------------------------------------------------------*/

                    obj.nmto_tot_debe_sol = ob.nmto_tot_debe_sol;
                    obj.nmto_tot_debe_dol = ob.nmto_tot_debe_dol;
                    obj.nmto_tot_haber_sol = ob.nmto_tot_haber_sol;
                    obj.nmto_tot_haber_dol = ob.nmto_tot_haber_dol;

                    //obj.nmto_tot_debe_sol = monto_sol;
                    //obj.nmto_tot_debe_dol = monto_dol;
                }
                if (x == 1)
                {
                    obj.nro_item_det = ob.nro_item_det + 2;
                    obj.viid_cuenta_contable = ob.iid_cautomatica_haber.ToString();
                    obj.iid_cuenta_contable = Convert.ToInt32(ob.iid_cautomatica_haber);
                    /*-------------------------------------------------------------------------------------------*/
                    var cta = mlistCuenta.Where(a => a.ctacc_icod_cuenta_contable == obj.iid_cuenta_contable).ToList();
                    if (cta.Count == 0)
                    {
                        throw new ArgumentException(String.Format("Cuenta contable automática {0} no figura en los registros de SUBCUENTAS", ob.iid_cautomatica_haber));
                    }
                    if (Convert.ToInt32(cta[0].tablc_iid_tipo_cuenta) > 0)
                    {
                        obj.iid_tipo_relacion = ob.iid_tipo_relacion;
                        obj.iid_relacion = ob.iid_relacion;
                    }
                    if (Convert.ToInt32(cta[0].strFlagCCosto) == 1)
                        obj.icod_centro_costo = ob.icod_centro_costo;
                    /*-------------------------------------------------------------------------------------------*/
                    obj.nmto_tot_debe_sol = ob.nmto_tot_haber_sol;
                    obj.nmto_tot_debe_dol = ob.nmto_tot_haber_dol;
                    obj.nmto_tot_haber_sol = ob.nmto_tot_debe_sol;
                    obj.nmto_tot_haber_dol = ob.nmto_tot_debe_dol;
                    //obj.nmto_tot_haber_sol = monto_sol;
                    //obj.nmto_tot_haber_dol = monto_dol;
                }
                obj.vglosa_linea = ob.vglosa_linea;
                obj.viid_tipo_relacion = "";
                obj.cestado = 'A';
                obj.iid_voucher_contable = code;
                obj.operacion = 1;
                obj.ctacc_iid_cuenta_contable_ref = ob.iid_cuenta_contable;
                obj.tipocambio = ob.tipocambio;
                mlistDetail.Add(obj);
            }
        }
        private void ActualizaCtaAutomatica(EComprobanteDetalle ob, decimal monto_sol, decimal monto_dol)
        {
            int location;
            location = mlistDetail.FindIndex(x => x.nro_item_det == ob.nro_item_det);

            for (int i = 1; i < 3; i++)
            {
                mlistDetail[location + i].icod_tipo_doc = ob.icod_tipo_doc;
                mlistDetail[location + i].vdescripcion_tipo_doc = ob.vdescripcion_tipo_doc;
                mlistDetail[location + i].vnumero_documento = ob.vnumero_documento;
                if (i == 1)
                {
                    mlistDetail[location + i].nmto_tot_debe_sol = ob.nmto_tot_debe_sol;
                    mlistDetail[location + i].nmto_tot_debe_dol = ob.nmto_tot_debe_dol;
                    mlistDetail[location + i].nmto_tot_haber_sol = ob.nmto_tot_haber_sol;
                    mlistDetail[location + i].nmto_tot_haber_dol = ob.nmto_tot_haber_dol;
                }
                if (i == 2)
                {

                    mlistDetail[location + i].nmto_tot_debe_sol = ob.nmto_tot_haber_sol;
                    mlistDetail[location + i].nmto_tot_debe_dol = ob.nmto_tot_haber_dol;
                    mlistDetail[location + i].nmto_tot_haber_sol = ob.nmto_tot_debe_sol;
                    mlistDetail[location + i].nmto_tot_haber_dol = ob.nmto_tot_debe_dol;
                }
                mlistDetail[location + i].vglosa_linea = ob.vglosa_linea;
                mlistDetail[location + i].operacion = ob.operacion;
            }
        }

        private void btnAgregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Agree();
        }
        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Agree();
        }
        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();

        }

        private void bteCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                ListarCuentas();
            }
        }

        private void bteCCosto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                ListarCCostos();
            }
        }

        private void bteAnalitica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                ListarAnaliticas();
            }
        }

        private void bteSubAnalitica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                ListarSubAnaliticas();
            }
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
            bteAnalitica.Text = string.Empty;
            //
            bteSubAnalitica.Tag = null;
            bteSubAnalitica.Text = string.Empty;
            //
            bteCCosto.Enabled = false;
            bteAnalitica.Enabled = false;
            bteSubAnalitica.Enabled = false;
        }
        private List<ECuentaContable> aux = new List<ECuentaContable>();
        private List<ETablaRegistro> auxA = new List<ETablaRegistro>();
        private List<ECentroCosto> auxCC = new List<ECentroCosto>();
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

                bteAnalitica.Enabled = (aux[0].ctacc_cflag_relacion != 0) ? true : false;
                bteSubAnalitica.Enabled = (aux[0].ctacc_cflag_relacion != 0) ? true : false;

                auxA = ListaAnalitica.Where(x => Convert.ToInt32(x.tarec_icorrelativo_registro) == aux[0].ctacc_cflag_relacion).ToList();
                if (auxA.Count == 1)
                {
                    bteAnalitica.Tag = auxA[0].tarec_icorrelativo_registro;
                    bteAnalitica.Text = auxA[0].tarec_icorrelativo_registro.ToString();
                    //ListaSubAnalitica = new BAnalitica().ListarAnaliticaDetalle(Convert.ToInt32(bteAnalitica.Tag));
                }
            }
            else
            {
                clearcta();
            }
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

        private void bteCuenta_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}

