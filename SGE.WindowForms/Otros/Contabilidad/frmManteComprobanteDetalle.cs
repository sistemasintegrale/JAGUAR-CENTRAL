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
    public partial class frmManteComprobanteDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteComprobanteDetalle));
        /*-------------------------------------------------------------------------------------------------*/
        private BContabilidad objContabilidadData = new BContabilidad();
        private List<ECentroCosto> lstCentroCosto = new List<ECentroCosto>();
        private BSMaintenanceStatus mStatus;
        public List<ECuentaContable> lstCuentaContable = new List<ECuentaContable>();
        public List<EVoucherContableDet> lstDetalle = new List<EVoucherContableDet>();
        public EVoucherContableDet Obe = new EVoucherContableDet();
        public decimal dblTipoCambio = 0;
        public bool flagSaldoInicial = false;
        public int intTipoMoneda = 0;
        /*-------------------------------------------------------------------------------------------------*/
        public bool indicador = false;
        /*-------------------------------------------------------------------------------------------------*/

        public frmManteComprobanteDetalle()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            var lstTipoDocContabilidad = new BAdministracionSistema().listarTipoDocumentoPorModulo(Parametros.intModuloContabilidad);
            BSControls.LoaderLook(LkpTipoDocumento, lstTipoDocContabilidad, "tdocc_vabreviatura_tipo_doc", "tdocc_icod_tipo_doc", true);

            lstCentroCosto = objContabilidadData.listarCentroCosto();
            lstCuentaContable = objContabilidadData.listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();

            if (flagSaldoInicial)
            {
                labelControl1.Visible = true;
                labelControl2.Visible = true;
                txtTC.Visible = true;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                if (lstTipoDocContabilidad.Where(x => x.tdocc_vabreviatura_tipo_doc == "VCO").ToList().Count > 0)
                    LkpTipoDocumento.EditValue = lstTipoDocContabilidad.Where(x => x.tdocc_vabreviatura_tipo_doc == "VCO").ToList()[0].tdocc_icod_tipo_doc;

                if (lstDetalle.Count > 0)
                {
                    txtNroDoc.Text = lstDetalle[lstDetalle.Count - 1].vcocd_numero_doc;
                }
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                SetValues();
            }

        }
        private void loadMask()
        {
            var lstParametroContable = new BContabilidad().listarParametroContable();
            lstParametroContable.ForEach(obe =>
            {
                this.bteCuenta.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCuenta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCuenta.Properties.Mask.ShowPlaceHolders = false;
                this.bteCuenta.Properties.Mask.UseMaskAsDisplayFormat = true;
            });
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

        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }


        public void SetValues()
        {
            LkpTipoDocumento.EditValue = Obe.tdocc_icod_tipo_doc;
            txtNroDoc.Text = Obe.vcocd_numero_doc;
            /*---------------------------------------------------------------------------------------------*/
            bteCuenta.Text = Obe.ctacc_icod_cuenta_contable.ToString();
            bteCuenta.Tag = Obe.ctacc_icod_cuenta_contable;
            bteCuenta.Enabled = false;
            txtCuentaDes.Text = Obe.strDesCuenta;
            /*---------------------------------------------------------------------------------------------*/
            bteCCosto.Text = Obe.strCodCCosto;
            bteCCosto.Tag = Obe.cecoc_icod_centro_costo;
            txtCCosto.Text = Obe.strDesCCosto;
            /*---------------------------------------------------------------------------------------------*/
            bteAnalitica.Text = (Obe.tablc_iid_tipo_analitica != null) ? Obe.tablc_iid_tipo_analitica.ToString() : "";
            bteAnalitica.Tag = Obe.tablc_iid_tipo_analitica;
            bteSubAnalitica.Text = Obe.strCodAnaliica;
            bteSubAnalitica.Tag = Obe.anad_icod_analitica;
            /*---------------------------------------------------------------------------------------------*/
            if (!flagSaldoInicial)
            {
                if (Obe.tablc_iid_moneda == Parametros.intTipoMonedaSoles)
                {
                    txtdebe.Text = Convert.ToDecimal(Obe.vcocd_nmto_tot_debe_sol).ToString();
                    txthaber.Text = Convert.ToDecimal(Obe.vcocd_nmto_tot_haber_sol).ToString();
                }
                if (Obe.tablc_iid_moneda == Parametros.intTipoMonedaDolares)
                {
                    txtdebe.Text = Convert.ToDecimal(Obe.vcocd_nmto_tot_debe_dol).ToString();
                    txthaber.Text = Convert.ToDecimal(Obe.vcocd_nmto_tot_haber_dol).ToString();
                }
            }
            else
            {
                txtdebe.Text = Convert.ToDecimal(Obe.vcocd_nmto_tot_debe_sol).ToString();
                txthaber.Text = Convert.ToDecimal(Obe.vcocd_nmto_tot_haber_sol).ToString();
                txtTC.Text = Obe.vcocd_tipo_cambio.ToString();
            }

            txtglosa.Text = Obe.vcocd_vglosa_linea;
        }
        private void clear()
        {
            bteCCosto.Text = string.Empty;
            bteCCosto.Tag = null;
            txtCCosto.Text = string.Empty;
            bteAnalitica.Text = string.Empty;
            bteAnalitica.Tag = null;
            bteSubAnalitica.Text = string.Empty;
            bteSubAnalitica.Tag = null;
        }
        private void FrmManteDetComp_Load(object sender, EventArgs e)
        {
            loadMask();
            cargar();
        }

        private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCuentas();
        }
        private void listarCuentas()
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    clear();
                    bteCuenta.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;
                    bteCCosto.Enabled = frm._Be.ctacc_iccosto;

                    if (Convert.ToInt32(frm._Be.tablc_iid_tipo_analitica) > 0)
                    {
                        bteAnalitica.Enabled = true;
                        bteSubAnalitica.Enabled = true;
                        bteAnalitica.Tag = frm._Be.tablc_iid_tipo_analitica;
                        bteAnalitica.Text = string.Format("{0:00}", frm._Be.tablc_iid_tipo_analitica);
                    }
                    else
                    {
                        bteAnalitica.Enabled = false;
                        bteSubAnalitica.Enabled = false;
                        bteAnalitica.Tag = null;
                        bteAnalitica.Text = "";
                    }
                }
            }
        }

        private void bteCCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCCostos();
        }
        private void ListarCCostos()
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
        private void bteAnalitica_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAnaliticas();
        }
        private void listarAnaliticas()
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
            listarSubAnaliticas();
        }
        private void








            listarSubAnaliticas()
        {
            using (frmListarAnaliticaDetalle frm = new frmListarAnaliticaDetalle())
            {
                frm.intTipoAnalitica = Convert.ToInt32(bteAnalitica.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteSubAnalitica.Tag = frm._Be.anad_icod_analitica;
                    bteSubAnalitica.Text = frm._Be.anad_iid_analitica;
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
        private void SetSave()
        {
            BaseEdit oBase = null;
            bool flag = true;
            decimal mto_sol = 0;
            decimal mto_dol = 0;
            try
            {
                if (string.IsNullOrEmpty(txtNroDoc.Text))
                {
                    oBase = txtNroDoc;
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
                if (bteCCosto.Enabled == true && bteCCosto.Tag == null)
                {
                    oBase = bteCCosto;
                    throw new ArgumentException("Ingrese Centro de Costo");
                }
                if (bteAnalitica.Tag != null && bteSubAnalitica.Tag == null)
                {
                    oBase = bteSubAnalitica;
                    throw new ArgumentException("Ingrese Sub - Analítica");
                }
                if (Convert.ToDecimal(txtdebe.Text) == 0 && Convert.ToDecimal(txthaber.Text) == 0)
                {
                    throw new ArgumentException("Ingrese un monto para campo Debe o Haber");
                }
                if (flagSaldoInicial)
                {
                    if (Convert.ToDecimal(txtTC.Text) == 0)
                    {
                        oBase = txtTC;
                        throw new ArgumentException("Ingrese Tipo de Cambio");
                    }
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.intTipoOperacion = Parametros.intOperacionNuevo;
                    //Obe.vcocd_nro_item_det = lstDetalle.Count + 1;  
                    if (lstDetalle.Count == 0)
                    {
                        Obe.vcocd_nro_item_det = 1;
                    }
                    else
                        Obe.vcocd_nro_item_det = lstDetalle.Max(x => x.vcocd_nro_item_det) + 1;
                }

                if (Status == BSMaintenanceStatus.ModifyCurrent)
                    Obe.intTipoOperacion = (Obe.intTipoOperacion == Parametros.intOperacionNuevo) ? Parametros.intOperacionNuevo : Parametros.intOperacionModificar;
                int? nullVal = null;

                Obe.vcocd_numero_doc = txtNroDoc.Text;
                Obe.strTipNroDocumento = LkpTipoDocumento.Text + "-" + txtNroDoc.Text;
                Obe.tdocc_icod_tipo_doc = Convert.ToInt32(LkpTipoDocumento.EditValue);
                Obe.ctacc_icod_cuenta_contable = Convert.ToInt64(bteCuenta.Tag);
                Obe.strNroCuenta = bteCuenta.Text;
                Obe.strDesCuenta = txtCuentaDes.Text;
                Obe.tablc_iid_tipo_analitica = (bteAnalitica.Tag == null) ? nullVal : Convert.ToInt32(bteAnalitica.Tag);
                Obe.anad_icod_analitica = (bteSubAnalitica.Tag == null) ? nullVal : Convert.ToInt32(bteSubAnalitica.Tag);
                Obe.strCodAnaliica = bteSubAnalitica.Text;
                Obe.strCodCCosto = bteCCosto.Text;
                Obe.cecoc_icod_centro_costo = (bteCCosto.Tag == null) ? nullVal : Convert.ToInt32(bteCCosto.Tag);
                Obe.strAnalisis = (bteAnalitica.Tag == null) ? "" : String.Format("{0:00}.{1}", bteAnalitica.Text, bteSubAnalitica.Text);
                Obe.vcocd_vglosa_linea = txtglosa.Text;
                Obe.vcocd_flag_estado = true;
                Obe.ctacc_iid_cuenta_contable_ref = null;
                Obe.vcocd_tipo_cambio = dblTipoCambio;
                Obe.tablc_iid_moneda = intTipoMoneda;
                Obe.tarec_icorrelativo_origen_vcontable = Parametros.intOriVcoManual;

                if (!flagSaldoInicial)
                {
                    if (intTipoMoneda == Parametros.intTipoMonedaSoles)
                    {
                        if (Convert.ToDecimal(txtdebe.Text) > 0 && Convert.ToDecimal(txthaber.Text) == 0)
                        {
                            Obe.vcocd_nmto_tot_debe_sol = Convert.ToDecimal(txtdebe.Text);
                            Obe.vcocd_nmto_tot_debe_dol = Math.Round(Convert.ToDecimal(txtdebe.Text) / Convert.ToDecimal(dblTipoCambio), 2);
                            Obe.vcocd_nmto_tot_haber_sol = 0;
                            Obe.vcocd_nmto_tot_haber_dol = 0;
                            mto_sol = Convert.ToDecimal(Obe.vcocd_nmto_tot_debe_sol);
                            mto_dol = Convert.ToDecimal(Obe.vcocd_nmto_tot_debe_dol);
                        }
                        if (Convert.ToDecimal(txthaber.Text) > 0 && Convert.ToDecimal(txtdebe.Text) == 0)
                        {
                            Obe.vcocd_nmto_tot_debe_sol = 0;
                            Obe.vcocd_nmto_tot_debe_dol = 0;
                            Obe.vcocd_nmto_tot_haber_sol = Convert.ToDecimal(txthaber.Text); ;
                            Obe.vcocd_nmto_tot_haber_dol = Math.Round(Convert.ToDecimal(txthaber.Text) / Convert.ToDecimal(dblTipoCambio), 2);
                            mto_sol = Convert.ToDecimal(Obe.vcocd_nmto_tot_haber_sol);
                            mto_dol = Convert.ToDecimal(Obe.vcocd_nmto_tot_haber_dol);
                        }
                    }
                    else if (intTipoMoneda == Parametros.intTipoMonedaDolares)
                    {
                        if (Convert.ToDecimal(txtdebe.Text) > 0 && Convert.ToDecimal(txthaber.Text) == 0)
                        {
                            Obe.vcocd_nmto_tot_debe_sol = Math.Round(Convert.ToDecimal(txtdebe.Text) * Convert.ToDecimal(dblTipoCambio), 2);
                            Obe.vcocd_nmto_tot_debe_dol = Convert.ToDecimal(txtdebe.Text);
                            Obe.vcocd_nmto_tot_haber_sol = 0;
                            Obe.vcocd_nmto_tot_haber_dol = 0;
                            mto_sol = Convert.ToDecimal(Obe.vcocd_nmto_tot_debe_sol);
                            mto_dol = Convert.ToDecimal(Obe.vcocd_nmto_tot_debe_dol);
                        }
                        if (Convert.ToDecimal(txthaber.Text) > 0 && Convert.ToDecimal(txtdebe.Text) == 0)
                        {
                            Obe.vcocd_nmto_tot_debe_sol = 0;
                            Obe.vcocd_nmto_tot_debe_dol = 0;
                            Obe.vcocd_nmto_tot_haber_sol = Math.Round(Convert.ToDecimal(txthaber.Text) * Convert.ToDecimal(dblTipoCambio), 2);
                            Obe.vcocd_nmto_tot_haber_dol = Convert.ToDecimal(txthaber.Text);
                            mto_sol = Convert.ToDecimal(Obe.vcocd_nmto_tot_haber_sol);
                            mto_dol = Convert.ToDecimal(Obe.vcocd_nmto_tot_haber_dol);
                        }
                    }
                }
                else
                {
                    Obe.vcocd_nmto_tot_debe_sol = Convert.ToDecimal(txtdebe.Text);
                    Obe.vcocd_nmto_tot_haber_sol = Convert.ToDecimal(txthaber.Text);
                    Obe.vcocd_nmto_tot_debe_dol = Math.Round(Convert.ToDecimal(txtdebe.Text) / Convert.ToDecimal(txtTC.Text), 2);
                    Obe.vcocd_nmto_tot_haber_dol = Math.Round(Convert.ToDecimal(txthaber.Text) / Convert.ToDecimal(txtTC.Text), 2);
                    Obe.vcocd_tipo_cambio = Convert.ToDecimal(txtTC.Text);
                }

                var Lista = lstCuentaContable.Where(Ob => Ob.ctacc_icod_cuenta_contable == Convert.ToInt64(bteCuenta.Tag)).ToList();
                Lista.ForEach(x =>
                {
                    Obe.ctacc_icod_cuenta_debe_auto = x.ctacc_icod_cuenta_debe_auto;
                    Obe.ctacc_icod_cuenta_haber_auto = x.ctacc_icod_cuenta_haber_auto;
                });

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    lstDetalle.Add(Obe);
                    if (!flagSaldoInicial)
                        if (Convert.ToInt32(Obe.ctacc_icod_cuenta_debe_auto) > 0)
                            agregarCtaAutomatica(Obe, mto_sol, mto_dol);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    if (!flagSaldoInicial)
                        if (Convert.ToInt32(Obe.ctacc_icod_cuenta_debe_auto) > 0)
                            modificarCtaAutomatica(Obe, mto_sol, mto_dol);

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
                flag = false;
            }
            finally
            {
                if (flag)
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void agregarCtaAutomatica(EVoucherContableDet oBeOri, decimal monto_sol, decimal monto_dol)
        {
            for (int x = 0; x < 2; x++)
            {
                EVoucherContableDet oBeAuto = new EVoucherContableDet();
                oBeAuto.tarec_icorrelativo_origen_vcontable = oBeOri.tarec_icorrelativo_origen_vcontable;
                oBeAuto.tdocc_icod_tipo_doc = oBeOri.tdocc_icod_tipo_doc;
                oBeAuto.strTipNroDocumento = oBeOri.strTipNroDocumento;
                oBeAuto.vcocd_numero_doc = oBeOri.vcocd_numero_doc;

                if (x == 0)
                {
                    oBeAuto.vcocd_nro_item_det = oBeOri.vcocd_nro_item_det + 1;
                    oBeAuto.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeOri.ctacc_icod_cuenta_debe_auto);
                    /*-------------------------------------------------------------------------------------------*/
                    var cta = lstCuentaContable.Where(a => a.ctacc_icod_cuenta_contable == oBeAuto.ctacc_icod_cuenta_contable).ToList();
                    if (cta.Count == 0)
                    {
                        throw new ArgumentException(String.Format("Cuenta contable automática {0} no figura en los registros de SUBCUENTAS", oBeOri.ctacc_icod_cuenta_debe_auto));
                    }
                    if (Convert.ToInt32(cta[0].tablc_iid_tipo_analitica) > 0)
                    {
                        oBeAuto.tablc_iid_tipo_analitica = oBeOri.tablc_iid_tipo_analitica;
                        oBeAuto.anad_icod_analitica = oBeOri.anad_icod_analitica;
                    }
                    if (cta[0].ctacc_iccosto)
                        oBeAuto.cecoc_icod_centro_costo = oBeOri.cecoc_icod_centro_costo;
                    /*-------------------------------------------------------------------------------------------*/

                    if (Obe.vcocd_nmto_tot_haber_sol > 0)
                    {
                        oBeAuto.vcocd_nmto_tot_haber_sol = monto_sol;
                        oBeAuto.vcocd_nmto_tot_haber_dol = monto_dol;
                    }
                    if (Obe.vcocd_nmto_tot_debe_sol > 0)
                    {
                        oBeAuto.vcocd_nmto_tot_debe_sol = monto_sol;
                        oBeAuto.vcocd_nmto_tot_debe_dol = monto_dol;
                    }

                }
                if (x == 1)
                {
                    oBeAuto.vcocd_nro_item_det = oBeOri.vcocd_nro_item_det + 2;
                    oBeAuto.ctacc_icod_cuenta_contable = Convert.ToInt32(oBeOri.ctacc_icod_cuenta_haber_auto);
                    /*-------------------------------------------------------------------------------------------*/
                    var cta = lstCuentaContable.Where(a => a.ctacc_icod_cuenta_contable == oBeAuto.ctacc_icod_cuenta_contable).ToList();
                    if (cta.Count == 0)
                    {
                        throw new ArgumentException(String.Format("Cuenta contable automática {0} no figura en los registros de SUBCUENTAS", oBeOri.ctacc_icod_cuenta_haber_auto));
                    }
                    if (Convert.ToInt32(cta[0].tablc_iid_tipo_analitica) > 0)
                    {
                        oBeAuto.tablc_iid_tipo_analitica = oBeOri.tablc_iid_tipo_analitica;
                        oBeAuto.anad_icod_analitica = oBeOri.anad_icod_analitica;
                    }
                    if (cta[0].ctacc_iccosto)
                        oBeAuto.cecoc_icod_centro_costo = oBeOri.cecoc_icod_centro_costo;
                    /*-------------------------------------------------------------------------------------------*/
                    if (Obe.vcocd_nmto_tot_debe_sol > 0)
                    {
                        oBeAuto.vcocd_nmto_tot_haber_sol = monto_sol;
                        oBeAuto.vcocd_nmto_tot_haber_dol = monto_dol;
                    }
                    if (Obe.vcocd_nmto_tot_haber_sol > 0)
                    {
                        oBeAuto.vcocd_nmto_tot_debe_sol = monto_sol;
                        oBeAuto.vcocd_nmto_tot_debe_dol = monto_dol;
                    }
                }

                oBeAuto.tablc_iid_moneda = oBeOri.tablc_iid_moneda;
                oBeAuto.vcocd_vglosa_linea = oBeOri.vcocd_vglosa_linea;
                oBeAuto.vcocd_flag_estado = true;
                oBeAuto.intTipoOperacion = 1;
                oBeAuto.ctacc_iid_cuenta_contable_ref = oBeOri.ctacc_icod_cuenta_contable;
                oBeAuto.vcocd_tipo_cambio = oBeOri.vcocd_tipo_cambio;
                lstDetalle.Add(oBeAuto);
            }
        }
        private void modificarCtaAutomatica(EVoucherContableDet ob, decimal monto_sol, decimal monto_dol)
        {
            int location;
            location = lstDetalle.FindIndex(x => x.vcocd_nro_item_det == ob.vcocd_nro_item_det);

            for (int i = 1; i < 3; i++)
            {
                lstDetalle[location + i].tdocc_icod_tipo_doc = ob.tdocc_icod_tipo_doc;
                lstDetalle[location + i].strTipNroDocumento = ob.strTipNroDocumento;
                lstDetalle[location + i].vcocd_numero_doc = ob.vcocd_numero_doc;
                if (i == 1)
                {
                    if (ob.vcocd_nmto_tot_haber_sol > 0)
                    {
                        //    /*Eliminar Anterio*/
                        lstDetalle[location + i].vcocd_nmto_tot_debe_sol = 0;
                        lstDetalle[location + i].vcocd_nmto_tot_debe_dol = 0;
                        //    /*Agregar Nuevos*/
                        lstDetalle[location + i].vcocd_nmto_tot_haber_sol = monto_sol;
                        lstDetalle[location + i].vcocd_nmto_tot_haber_dol = monto_dol;
                    }
                    if (ob.vcocd_nmto_tot_debe_sol > 0)
                    {
                        /*Eliminar Anterios*/
                        lstDetalle[location + i].vcocd_nmto_tot_haber_sol = 0;
                        lstDetalle[location + i].vcocd_nmto_tot_haber_dol = 0;
                        /*Agregar Nuevo*/
                        lstDetalle[location + i].vcocd_nmto_tot_debe_sol = monto_sol;
                        lstDetalle[location + i].vcocd_nmto_tot_debe_dol = monto_dol;
                    }

                }
                if (i == 2)
                {
                    if (ob.vcocd_nmto_tot_haber_sol > 0)
                    {
                        //    /*Eliminar Anterior*/
                        lstDetalle[location + i].vcocd_nmto_tot_haber_sol = 0;
                        lstDetalle[location + i].vcocd_nmto_tot_haber_dol = 0;
                        //    /*Agregar Nuevo*/
                        lstDetalle[location + i].vcocd_nmto_tot_debe_sol = monto_sol;
                        lstDetalle[location + i].vcocd_nmto_tot_debe_dol = monto_dol;
                    }
                    if (ob.vcocd_nmto_tot_debe_sol > 0)
                    {
                        /**Eliminar Anterior*/
                        lstDetalle[location + i].vcocd_nmto_tot_debe_sol = 0;
                        lstDetalle[location + i].vcocd_nmto_tot_debe_dol = 0;
                        /*Agregar Nuevo*/
                        lstDetalle[location + i].vcocd_nmto_tot_haber_sol = monto_sol;
                        lstDetalle[location + i].vcocd_nmto_tot_haber_dol = monto_dol;
                    }
                }
                lstDetalle[location + i].vcocd_vglosa_linea = ob.vcocd_vglosa_linea;
                lstDetalle[location + i].intTipoOperacion = ob.intTipoOperacion;
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
        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();

        }

        private void bteCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                listarCuentas();
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
                listarAnaliticas();
            }
        }

        private void bteSubAnalitica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                listarSubAnaliticas();
            }
        }
        private void clearcta()
        {
            txtCuentaDes.Text = string.Empty;
            bteCuenta.Tag = null;
            //
            txtCCosto.Text = string.Empty;
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

            var aux = lstCuentaContable.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt64(bteCuenta.Text.Replace(".", ""))).ToList();

            if (aux.Count == 1)
            {
                bteCuenta.Tag = aux[0].ctacc_icod_cuenta_contable;
                txtCuentaDes.Text = aux[0].ctacc_nombre_descripcion;
                bteCCosto.Enabled = aux[0].ctacc_iccosto;

                bteAnalitica.Enabled = (Convert.ToInt32(aux[0].tablc_iid_tipo_analitica) > 0) ? true : false;
                bteSubAnalitica.Enabled = (Convert.ToInt32(aux[0].tablc_iid_tipo_analitica) > 0) ? true : false;
                if (Convert.ToInt32(aux[0].tablc_iid_tipo_analitica) > 0)
                {
                    bteAnalitica.Tag = aux[0].tablc_iid_tipo_analitica;
                    bteAnalitica.Text = aux[0].tablc_iid_tipo_analitica.ToString();
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
            var aux = lstCentroCosto.Where(x => x.cecoc_vcodigo_centro_costo == bteCCosto.Text).ToList();

            if (aux.Count == 1)
            {
                txtCCosto.Text = aux[0].cecoc_vdescripcion;
                bteCCosto.Tag = aux[0].cecoc_icod_centro_costo;
            }
            else
            {
                txtCCosto.Text = string.Empty;
                bteCCosto.Tag = null;
            }
        }
    }
}

