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


namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmManteParametrosContables : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteParametrosContables));
        private List<ESubDiario> lstSubDiario = new List<ESubDiario>();
        private List<ECuentaContable> lstCuentaContable = new List<ECuentaContable>();
        private List<ECentroCosto> lstCCosto = new List<ECentroCosto>();
        private BSMaintenanceStatus mStatus;
        private List<EParametroContable> lstParametroCont = new List<EParametroContable>();
        BContabilidad objContabilidadData = new BContabilidad();

        public FrmManteParametrosContables()
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
            if (lstCuentaContable.Count > 0)
            {
                txtEC1.Enabled = !Enabled;
                txtEC2.Enabled = !Enabled;
                txtEC3.Enabled = !Enabled;
                txtEC4.Enabled = !Enabled;
                txtEC5.Enabled = !Enabled;
                txtEC6.Enabled = !Enabled;
                txtEC7.Enabled = !Enabled;
                txtEC8.Enabled = !Enabled;
            }
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            StatusControl();
        }
        private void cargar()
        {
            /*----------------------------------------------------------------------------*/
            lstParametroCont = objContabilidadData.listarParametroContable();
            lstSubDiario = objContabilidadData.listarSubDiario();
            lstCuentaContable = objContabilidadData.listarCuentaContable();
            lstCCosto = objContabilidadData.listarCentroCosto();
            /*----------------------------------------------------------------------------*/
            if (lstParametroCont.Count > 0)
                SetModify();
            else
                SetInsert();
            /*----------------------------------------------------------------------------*/
            BSControls.LoaderLook(lkpIngBancos, lstSubDiario, "subdi_vdescripcion", "subdi_icod_subdiario", false);
            BSControls.LoaderLook(lkpEgrBancos, lstSubDiario, "subdi_vdescripcion", "subdi_icod_subdiario", false);
            BSControls.LoaderLook(lkpCajaChica, lstSubDiario, "subdi_vdescripcion", "subdi_icod_subdiario", false);
            BSControls.LoaderLook(lkpDocXPagar, lstSubDiario, "subdi_vdescripcion", "subdi_icod_subdiario", false);
            BSControls.LoaderLook(lkpDocXCobrar, lstSubDiario, "subdi_vdescripcion", "subdi_icod_subdiario", false);
            BSControls.LoaderLook(lkpApertura, lstSubDiario, "subdi_vdescripcion", "subdi_icod_subdiario", false);
            BSControls.LoaderLook(LkpCierreAnual, lstSubDiario, "subdi_vdescripcion", "subdi_icod_subdiario", false);
            BSControls.LoaderLook(lkpCostoVenta, lstSubDiario, "subdi_vdescripcion", "subdi_icod_subdiario", false);
            BSControls.LoaderLook(lkpPlanillas, lstSubDiario, "subdi_vdescripcion", "subdi_icod_subdiario", false);
            /*----------------------------------------------------------------------------*/
        }

        private void FrmManteParametrosContables_Load(object sender, EventArgs e)
        {



            cargar();
            string cadena = lstParametroCont[0].parac_vvalor_texto;
            string[] arreglo = cadena.Split('-');

            txtEC1.Text = arreglo[0];
            for (int i = 0; i < arreglo.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        txtEC1.Text = arreglo[i].ToString();
                        break;
                    case 1:
                        txtEC2.Text = arreglo[i].ToString();
                        break;
                    case 2:
                        txtEC3.Text = arreglo[i].ToString();
                        break;
                    case 3:
                        txtEC4.Text = arreglo[i].ToString();
                        break;
                    case 4:
                        txtEC5.Text = arreglo[i].ToString();
                        break;
                    case 5:
                        txtEC6.Text = arreglo[i].ToString();
                        break;
                    case 6:
                        txtEC7.Text = arreglo[i].ToString();
                        break;
                    case 7:
                        txtEC8.Text = arreglo[i].ToString();
                        break;
                }
            }
            lkpIngBancos.EditValue = Convert.ToInt32(lstParametroCont[0].parac_id_sd_ingbco);
            lkpEgrBancos.EditValue = lstParametroCont[0].parac_id_sd_egrbco;
            lkpCajaChica.EditValue = lstParametroCont[0].parac_id_sd_cjachic;
            lkpCostoVenta.EditValue = lstParametroCont[0].parac_id_sd_cosvta;
            lkpDocXPagar.EditValue = lstParametroCont[0].parac_id_sd_docxpag;
            lkpDocXCobrar.EditValue = lstParametroCont[0].parac_id_sd_docxcob;
            lkpApertura.EditValue = lstParametroCont[0].parac_id_sd_apert;
            LkpCierreAnual.EditValue = lstParametroCont[0].parac_id_sd_cieanual;
            bteCtaGananciaMN.Tag = lstParametroCont[0].parac_id_cta_gdifc_mn;
            bteCtaGananciaMN.Text = lstParametroCont[0].parac_id_cta_gdifc_mn.ToString();
            bteCtaGananciaME.Tag = lstParametroCont[0].parac_id_cta_gdifc_me;
            bteCtaGananciaME.Text = lstParametroCont[0].parac_id_cta_gdifc_me.ToString();
            bteCtaPerdidaMN.Tag = lstParametroCont[0].parac_id_cta_pdifc_mn;
            bteCtaPerdidaMN.Text = lstParametroCont[0].parac_id_cta_pdifc_mn.ToString();
            bteCtaPerdidaME.Tag = lstParametroCont[0].parac_id_cta_pdifc_me;
            bteCtaPerdidaME.Text = lstParametroCont[0].parac_id_cta_pdifc_me.ToString();
            //
            bteComprobanteMN.Tag = lstParametroCont[0].parac_id_sd_difc_mn.ToString();
            bteComprobanteMN.Text = lstParametroCont[0].parac_id_sd_difc_mn.ToString();
            //
            bteComprobanteME.Tag = lstParametroCont[0].parac_id_sd_difc_me.ToString();
            bteComprobanteME.Text = lstParametroCont[0].parac_id_sd_difc_me.ToString();
            //
            txtNroComprobanteMN.Text = lstParametroCont[0].parac_nro_comp_difc_mn;
            txtNroComprobanteME.Text = lstParametroCont[0].parac_nro_comp_difc_me;
            bteCentroCosto.Tag = lstParametroCont[0].parac_id_ccosto_difc;
            bteCentroCosto.Text = lstParametroCont[0].strCodeCCostoDifCambio;
            txtCentroCosto.Text = lstParametroCont[0].strDesCCostoDifCambio;
            bteCtaGananciaRedMN.Tag = lstParametroCont[0].parac_id_cta_gredo_mn;
            bteCtaGananciaRedMN.Text = lstParametroCont[0].parac_id_cta_gredo_mn.ToString();
            bteCtaGananciaRedME.Tag = lstParametroCont[0].parac_id_cta_gredo_me;
            bteCtaGananciaRedME.Text = lstParametroCont[0].parac_id_cta_gredo_me.ToString();
            bteCtaPerdidaRedMN.Tag = lstParametroCont[0].parac_id_cta_predo_mn;
            bteCtaPerdidaRedMN.Text = lstParametroCont[0].parac_id_cta_predo_mn.ToString();
            bteCtaPerdidaRedME.Tag = lstParametroCont[0].parac_id_cta_predo_me;
            bteCtaPerdidaRedME.Text = lstParametroCont[0].parac_id_cta_predo_me.ToString();
            bteCtaRetencion.Text = lstParametroCont[0].parac_id_cta_retencion.ToString();
            bteCtaRetencion.Tag = lstParametroCont[0].parac_id_cta_retencion;
            bteCtaPlanilla.Text = lstParametroCont[0].parac_id_cta_planilla.ToString();
            bteCtaPlanilla.Tag = lstParametroCont[0].parac_id_cta_planilla;
            bteCostoBase.Tag = lstParametroCont[0].parac_id_ccos_base;
            bteCostoBase.Text = lstParametroCont[0].strCodeCCostoBase;
            txtCostoBase.Text = lstParametroCont[0].strDesCCostoBase;
            txtPorRetencion.Text = lstParametroCont[0].Porcentaje_de_Retencion.ToString();
            btnCta4taCat.Tag = lstParametroCont[0].parac_id_cta_4ta_cat;
            btnCta4taCat.Text = lstParametroCont[0].strCod4taCategoria;
            txtRetencionVentas.Text = lstParametroCont[0].parac_Porcentaje_Retencion_ventas.ToString();
            lkpPlanillas.EditValue = lstParametroCont[0].parac_id_sd_planillas;
        }

        private void CuentaContable_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();
                }
            }
        }

        private void CentroCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarCentroCosto frm = new frmListarCentroCosto())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.cecoc_icod_centro_costo;
                    opcion.Text = frm._Be.cecoc_vcodigo_centro_costo;
                    if (opcion.Name == "bteCentroCosto")
                        txtCentroCosto.Text = frm._Be.cecoc_vdescripcion;
                    if (opcion.Name == "bteCostoBase")
                        txtCostoBase.Text = frm._Be.cecoc_vdescripcion;
                }
            }
        }

        private void CuentaContable_KeyUp(object sender, KeyEventArgs e)
        {
            List<ECuentaContable> auxList = new List<ECuentaContable>();
            ButtonEdit opcion = (ButtonEdit)sender;
            auxList = lstCuentaContable.Where(x => x.ctacc_numero_cuenta_contable.ToUpper() == opcion.Text.ToUpper()).ToList();
            if (auxList.Count == 1)
            {
                opcion.Tag = auxList[0].ctacc_icod_cuenta_contable;
                opcion.Text = auxList[0].ctacc_numero_cuenta_contable.ToString();
            }
            else
                opcion.Tag = null;
        }
        private void CentroCosto_KeyUp(object sender, KeyEventArgs e)
        {
            List<ECentroCosto> auxList = new List<ECentroCosto>();
            ButtonEdit opcion = (ButtonEdit)sender;
            auxList = lstCCosto.Where(x => x.cecoc_vcodigo_centro_costo.ToUpper() == opcion.Text.ToUpper()).ToList();
            if (auxList.Count == 1)
            {
                opcion.Tag = auxList[0].cecoc_icod_centro_costo;
                opcion.Text = auxList[0].cecoc_vcodigo_centro_costo.ToString();
                if (opcion.Name == "bteCentroCosto")
                    txtCentroCosto.Text = auxList[0].cecoc_vdescripcion;
                if (opcion.Name == "bteCostoBase")
                    txtCostoBase.Text = auxList[0].cecoc_vdescripcion;
            }
            else
            {
                opcion.Tag = null;
                if (opcion.Name == "bteCentroCosto")
                    txtCentroCosto.Text = string.Empty;
                if (opcion.Name == "bteCostoBase")
                    txtCostoBase.Text = string.Empty;
            }

        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (XtraMessageBox.Show("¿Desea GUARDAR los datos ingresados?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            //{
            //    SetSave();
            //}
            SetSave();
        }
        private int EstructuraCuenta()
        {
            int count = 0;
            foreach (TextEdit x in grpEstructura.Controls.OfType<TextEdit>())
            {
                count = count + Convert.ToInt32(x.Text);
            }
            return count;
        }


        private void SetSave()
        {
            BaseEdit oBase = null;
            bool Flag = false;
            string mask = "";
            try
            {
                foreach (TextEdit obj in grpEstructura.Controls.OfType<TextEdit>())
                {
                    switch (obj.Name)
                    {
                        case "txtEC1":
                            mask = mask + ((txtEC1.Text == "0") ? "" : txtEC1.Text);
                            break;
                        case "txtEC2":
                            mask = mask + ((txtEC2.Text == "0") ? "" : txtEC2.Text);
                            break;
                        case "txtEC3":
                            mask = mask + ((txtEC3.Text == "0") ? "" : txtEC3.Text);
                            break;
                        case "txtEC4":
                            mask = mask + ((txtEC4.Text == "0") ? "" : txtEC4.Text);
                            break;
                        case "txtEC5":
                            mask = mask + ((txtEC5.Text == "0") ? "" : txtEC5.Text);
                            break;
                        case "txtEC6":
                            mask = mask + ((txtEC6.Text == "0") ? "" : txtEC6.Text);
                            break;
                        case "txtEC7":
                            mask = mask + ((txtEC7.Text == "0") ? "" : txtEC7.Text);
                            break;
                        case "txtEC8":
                            mask = mask + ((txtEC8.Text == "0") ? "" : txtEC8.Text);
                            break;
                    }
                }
                string mask2 = "";
                string mask3 = "";

                for (int i = mask.Length - 1; i > -1; i--)
                {
                    int k;
                    k = i;
                    if (i == 0)
                    {
                        mask2 = mask2 + "\\d{" + mask.Substring(0, 1) + "}?";
                        mask3 = mask3 + mask.Substring(i, 1);
                    }
                    else
                    {
                        mask2 = mask2 + "\\d{" + mask.Substring(i, 1) + "}?\\.";
                        mask3 = mask3 + mask.Substring(i, 1) + "-";
                    }

                }

                if (EstructuraCuenta() > 15)
                {
                    oBase = txtEC8;
                    throw new ArgumentException("La suma de los valores de la Estructura de Cuenta no puede ser mayor a 15");
                }
                if (EstructuraCuenta() == 0)
                {
                    oBase = txtEC8;
                    throw new ArgumentException("Ingrese Estructura de Cuenta");
                }
                if (lkpIngBancos.EditValue == null)
                {
                    oBase = lkpIngBancos;
                    throw new ArgumentException("Seleccione Sub-Diario de Ing. de Bancos");
                }
                if (lkpEgrBancos.EditValue == null)
                {
                    oBase = lkpEgrBancos;
                    throw new ArgumentException("Seleccione Sub-Diario de Egr. de Bancos");
                }
                if (lkpCajaChica.EditValue == null)
                {
                    oBase = lkpCajaChica;
                    throw new ArgumentException("Seleccione Sub-Diario de Caja Chica");
                }
                if (lkpCostoVenta.EditValue == null)
                {
                    oBase = lkpCostoVenta;
                    throw new ArgumentException("Seleccione Sub-Diario de Costo de Venta");
                }
                if (lkpDocXPagar.EditValue == null)
                {
                    oBase = lkpDocXPagar;
                    throw new ArgumentException("Seleccione Sub-Diario de Docs. por Pagar");
                }
                if (lkpDocXCobrar.EditValue == null)
                {
                    oBase = lkpDocXCobrar;
                    throw new ArgumentException("Seleccione Sub-Diario de Docs. por Cobrar");
                }
                if (lkpApertura.EditValue == null)
                {
                    oBase = lkpApertura;
                    throw new ArgumentException("Seleccione Sub-Diario de Apertura");
                }
                if (LkpCierreAnual.EditValue == null)
                {
                    oBase = LkpCierreAnual;
                    throw new ArgumentException("Seleccione Sub-Diario de Cierre Anual");
                }
                if (bteCtaGananciaMN.Tag == null)
                {
                    oBase = bteCtaGananciaMN;
                    throw new ArgumentException("Seleccione Cuenta de Ganancia de Moneda Nacional");
                }
                if (bteCtaGananciaME.Tag == null)
                {
                    oBase = bteCtaGananciaME;
                    throw new ArgumentException("Seleccione Cuenta de Ganancia de Moneda Extranjera");
                }
                if (bteCtaPerdidaMN.Tag == null)
                {
                    oBase = bteCtaPerdidaMN;
                    throw new ArgumentException("Seleccione Cuenta de Pérdida de Moneda Nacional");
                }
                if (bteCtaPerdidaME.Tag == null)
                {
                    oBase = bteCtaPerdidaME;
                    throw new ArgumentException("Seleccione Cuenta de Pérdida de Moneda Extranjera");
                }
                if (string.IsNullOrEmpty(bteComprobanteMN.Text))
                {
                    oBase = bteComprobanteMN;
                    throw new ArgumentException("Ingrese un Comprobante de Moneda Nacional");
                }
                if (bteComprobanteMN.Tag == null)
                {
                    oBase = bteComprobanteMN;
                    throw new ArgumentException("Ingrese un Comprobante de Moneda Nacional valido");
                }
                if (string.IsNullOrEmpty(bteComprobanteME.Text))
                {
                    oBase = bteComprobanteME;
                    throw new ArgumentException("Ingrese un Comprobante de Moneda Extranjera");
                }
                if (bteComprobanteME.Tag == null)
                {
                    oBase = bteComprobanteME;
                    throw new ArgumentException("Ingrese un Comprobante de Moneda Extranjera valido");
                }
                if (string.IsNullOrEmpty(txtNroComprobanteMN.Text))
                {
                    oBase = txtNroComprobanteMN;
                    throw new ArgumentException("Ingrese Nro de Comprobante de Moneda Nacional");
                }
                if (string.IsNullOrEmpty(txtNroComprobanteME.Text))
                {
                    oBase = txtNroComprobanteME;
                    throw new ArgumentException("Ingrese Nro de Comprobante de Moneda Extranjera");
                }
                if (bteCentroCosto.Tag == null)
                {
                    oBase = bteCentroCosto;
                    throw new ArgumentException("Seleccione Centro de Costo");
                }
                if (bteCtaGananciaRedMN.Tag == null)
                {
                    oBase = bteCtaGananciaRedMN;
                    throw new ArgumentException("Seleccione Cuenta de Ganancia de Moneda Nacional");
                }
                if (bteCtaGananciaRedME.Tag == null)
                {
                    oBase = bteCtaGananciaRedME;
                    throw new ArgumentException("Seleccione Cuenta de Ganancia de Moneda Extranjera");
                }
                if (bteCtaPerdidaRedMN.Tag == null)
                {
                    oBase = bteCtaPerdidaRedMN;
                    throw new ArgumentException("Seleccione Cuenta de Pérdida de Moneda Nacional");
                }
                if (bteCtaPerdidaRedME.Tag == null)
                {
                    oBase = bteCtaPerdidaRedME;
                    throw new ArgumentException("Seleccione Cuenta de Pérdida de Moneda Extranjera");
                }
                if (bteCostoBase.Tag == null)
                {
                    oBase = bteCostoBase;
                    throw new ArgumentException("Seleccione Centro de Costo Base");
                }
                //if (bteCtaRetencion.Tag == null)
                //{
                //    oBase = bteCtaRetencion;
                //    throw new ArgumentException("Selecione Cuenta de Retención");
                //}
                //if (bteCtaPlanilla.Tag == null)
                //{
                //    oBase = bteCtaPlanilla;
                //    throw new ArgumentException("Seleccione Cuenta para Planilla sin Documento");
                //}
                EParametroContable Obe = new EParametroContable();
                Obe.parac_iid_parametro = lstParametroCont[0].parac_iid_parametro;
                Obe.parac_vvalor_texto = "";
                Obe.parac_id_sd_ingbco = Convert.ToInt32(lkpIngBancos.EditValue);
                Obe.parac_id_sd_egrbco = Convert.ToInt32(lkpEgrBancos.EditValue);
                Obe.parac_id_sd_cjachic = Convert.ToInt32(lkpCajaChica.EditValue);
                Obe.parac_id_sd_cosvta = Convert.ToInt32(lkpCostoVenta.EditValue);
                Obe.parac_id_sd_docxpag = Convert.ToInt32(lkpDocXPagar.EditValue);
                Obe.parac_id_sd_docxcob = Convert.ToInt32(lkpDocXCobrar.EditValue);
                Obe.parac_id_sd_apert = Convert.ToInt32(lkpApertura.EditValue);
                Obe.parac_id_sd_cieanual = Convert.ToInt32(LkpCierreAnual.EditValue);
                Obe.parac_id_cta_gdifc_mn = Convert.ToInt32(bteCtaGananciaMN.Tag);
                Obe.parac_id_cta_gdifc_me = Convert.ToInt32(bteCtaGananciaME.Tag);
                Obe.parac_id_cta_pdifc_mn = Convert.ToInt32(bteCtaPerdidaMN.Tag);
                Obe.parac_id_cta_pdifc_me = Convert.ToInt32(bteCtaPerdidaME.Tag);
                Obe.parac_id_sd_difc_mn = Convert.ToInt32(bteComprobanteMN.Tag);
                Obe.parac_id_sd_difc_me = Convert.ToInt32(bteComprobanteME.Tag);
                Obe.parac_nro_comp_difc_mn = txtNroComprobanteMN.Text;
                Obe.parac_nro_comp_difc_me = txtNroComprobanteME.Text;
                Obe.parac_id_ccosto_difc = Convert.ToInt32(bteCentroCosto.Tag);
                Obe.parac_id_cta_gredo_mn = Convert.ToInt32(bteCtaGananciaRedMN.Tag);
                Obe.parac_id_cta_gredo_me = Convert.ToInt32(bteCtaGananciaRedME.Tag);
                Obe.parac_id_cta_predo_mn = Convert.ToInt32(bteCtaPerdidaRedMN.Tag);
                Obe.parac_id_cta_predo_me = Convert.ToInt32(bteCtaPerdidaRedME.Tag);
                ////Obe.parac_id_cta_retencion = Convert.ToInt32(bteCtaRetencion.Tag);
                ////Obe.parac_id_cta_planilla = Convert.ToInt32(bteCtaPlanilla.Tag);
                Obe.parac_id_ccos_base = Convert.ToInt32(bteCostoBase.Tag);
                Obe.tablc_iid_modulo = 1;//preguntar
                Obe.parac_cestado = 'A';
                Obe.parac_vmascara = mask2.TrimEnd('.');
                Obe.parac_nvalor_numerico = Convert.ToDecimal(mask.Length);
                Obe.parac_vvalor_texto = mask3;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.Porcentaje_de_Retencion = Convert.ToDecimal(txtPorRetencion.Text);
                Obe.parac_id_cta_4ta_cat = Convert.ToInt32(btnCta4taCat.Tag);
                Obe.parac_Porcentaje_Retencion_ventas = Convert.ToDecimal(txtRetencionVentas.Text);
                Obe.parac_id_sd_planillas = Convert.ToInt32(lkpPlanillas.EditValue);
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    objContabilidadData.insertarParametroContable(Obe);
                    Flag = true;
                }
                else
                {
                    if (XtraMessageBox.Show("\t\t\t\tLos datos serán actualizados\n ¿Está seguro que desea continuar con la grabación?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        objContabilidadData.modificarParamentroContable(Obe);
                        Flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
            }
            finally
            {
                if (Flag)
                    this.Close();
            }
        }

        private void FrmManteParametrosContables_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void calculate(object sender, int count)
        {
            TextEdit objsender = (TextEdit)sender;
            int restante;
            if (count < 15)
            {
                switch (objsender.Name)
                {
                    case "txtEC1":
                        restante = 15 - count;
                        if (restante > 0)
                        { txtEC2.Text = restante.ToString(); }
                        break;
                    case "txtEC2":
                        restante = 15 - count;
                        if (restante > 0)
                        { txtEC3.Text = restante.ToString(); }
                        break;
                    case "txtEC3":
                        restante = 15 - count;
                        if (restante > 0)
                        { txtEC4.Text = restante.ToString(); }
                        break;
                    case "txtEC4":
                        restante = 15 - count;
                        if (restante > 0)
                        { txtEC5.Text = restante.ToString(); }
                        break;
                    case "txtEC5":
                        restante = 15 - count;
                        if (restante > 0)
                        { txtEC6.Text = restante.ToString(); }
                        break;
                    case "txtEC6":
                        restante = 15 - count;
                        if (restante > 0)
                        { txtEC7.Text = restante.ToString(); }
                        break;
                    case "txtEC7":
                        restante = 15 - count;
                        if (restante > 0)
                        { txtEC8.Text = restante.ToString(); }
                        break;
                }

            }
        }

        private void txtEC1_EditValueChanged(object sender, EventArgs e)
        {
            int count = 0;
            foreach (Control obj in grpEstructura.Controls)
            {
                if (obj is TextEdit)
                {
                    count = count + Convert.ToInt32(obj.Text);
                }
            }
            calculate(sender, count);
        }

        private void bteComprobante_KeyUp(object sender, KeyEventArgs e)
        {
            List<ESubDiario> aux = new List<ESubDiario>();
            ButtonEdit opcion = (ButtonEdit)sender;
            aux = lstSubDiario.Where(x => x.subdi_icod_subdiario.ToString() == opcion.Text).ToList();
            if (aux.Count == 1)
            {
                opcion.Tag = aux[0].subdi_icod_subdiario;
            }
            else
            {
                opcion.Tag = null;
            }
        }

        private void bteComprobante_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarSubDiario frm = new frmListarSubDiario())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.subdi_icod_subdiario;
                    opcion.Text = frm._Be.subdi_icod_subdiario.ToString();
                }
            }
        }

        private void btnCta4taCat_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();
                }
            }
        }


    }
}
