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
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmManteDxPDet : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteDxPDet));
        private BSMaintenanceStatus mStatus;
        public ELibroBancosDetalle Obj = new ELibroBancosDetalle();
        public int tip_mon = 0;
        public int Cab_icod_correlativo = 0;
        public List<ELibroBancosDetalle> oDetailList = new List<ELibroBancosDetalle>();
        public DateTime fechaDoc;
        public int tipo_doc;
        string ana_cod = string.Empty;
        public int tipo_moneda;
        public decimal tipo_cambio;
        public List<long?> ListaDxCOcultar = new List<long?>();

        public EDocPorPagarDetalleCuentaContable objDet = new EDocPorPagarDetalleCuentaContable();
        public decimal saldoDetalle;
        public EDocPorPagar obeDocXPagar = new EDocPorPagar();
        public List<long?> ListaDxPOcultar = new List<long?>(); //cuando se elimina una cuenta ya no se podrá mostrar el mismo documento
        public EProveedor obeProveedor = new EProveedor();

        public FrmManteDxPDet()
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

        private void CargaInsert()
        {
            txtMonto.Text = saldoDetalle.ToString();
        }

        private void CargaModify()
        {
            bool Enabled;
            bteCuenta.Tag = objDet.ctacc_iid_cuenta_contable;
            bteCuenta.Text = objDet.NumeroCuentaContable;
            txtCuentaDes.Text = objDet.DescripcionCuentaContable;

            Enabled = (objDet.cecoc_icod_centro_costo != null && objDet.cecoc_icod_centro_costo != 0);
            bteCCosto.Enabled = Enabled;
            bteCCosto.Tag = objDet.cecoc_icod_centro_costo;
            bteCCosto.Text = (Enabled) ? objDet.CodigoCentroCosto : string.Empty;
            txtcentrocosto.Text = objDet.DescripcionCentroCosto;

            Enabled = (objDet.anac_icod_analitica != null && objDet.anac_icod_analitica != 0);
            bteAnalitica.Enabled = Enabled;
            bteSubAnalitica.Enabled = Enabled;
            bteAnalitica.Tag = objDet.IdTipoAnalitica;
            bteAnalitica.Text = (Enabled) ? string.Format("{0:00}", objDet.IdTipoAnalitica) : string.Empty;
            bteSubAnalitica.Tag = objDet.anac_icod_analitica;
            bteSubAnalitica.Text = (Enabled) ? objDet.NumeroAnalitica : string.Empty;

            txtMonto.Text = objDet.cdxpc_nmonto_cuenta.ToString();
            txtConcepto.Text = objDet.cdxpc_vglosa;


            btnPresupuesto.Tag = objDet.prep_icod_presupuesto;
            btnPresupuesto.Text = objDet.prep_cod_presupuesto;

            btnRugro.Text = objDet.cpnd_vdescripcion;
            btnRugro.Tag = objDet.prepd_icod_detalle;



            lblTipoDoc.Text = objDet.tdocc_vabreviatura_tipo_doc;
            bteDocPagar.Tag = objDet.doxpc_icod_correlativo_dxp;
            bteDocPagar.Text = objDet.doxpc_vnumero_doc;
            //if (tipo_doc == Parametros.intTipoDocLiquidacionCobranza)
            //{
            //    bteCuenta.Enabled = true;
            //    bteAnalitica.Enabled = true;
            //    bteSubAnalitica.Enabled = true;
            //    bteCCosto.Enabled = true;
            //    bteCCosto.Enabled = true;
            //}
        }

        private void StatusControl()
        {
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                btnAgregar.Enabled = false;
                btnModificar.Enabled = true;
                bteDocPagar.Enabled = false;
            }

        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            CargaInsert();
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
                    if (bteSubAnalitica.Tag == null)
                    {
                        oBase = bteSubAnalitica;
                        throw new ArgumentException("Seleccione Sub - Analítica");
                    }
                }
                if (Convert.ToDecimal(txtMonto.Text) == 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("Ingrese monto");
                }
                //if (Convert.ToDecimal(txtMonto.Text) < 0)
                //{
                //    oBase = txtMonto;
                //    throw new ArgumentException("El monto Ingresado es Negativo");
                //}


                if (string.IsNullOrEmpty(txtConcepto.Text))
                {
                    oBase = txtConcepto;
                    throw new ArgumentException("Ingrese concepto");
                }
                int idAnalitica; int.TryParse((bteAnalitica.Tag == null) ? string.Empty : bteAnalitica.Tag.ToString(), out idAnalitica);
                if (idAnalitica == 5)
                {
                    //if (bteDocPagar.Tag == null)
                    //{
                    //    oBase = bteDocPagar;
                    //    throw new ArgumentException("Ingrese el documento");
                    //}

                    //decimal saldo = CalcularSaldoXMoneda(tipo_moneda, 2);

                    ////verificar saldo
                    //if (CalcularSaldoXMoneda(tipo_moneda, 1) < Convert.ToDecimal(txtMonto.Text))
                    //{
                    //    oBase = txtMonto;
                    //    throw new ArgumentException("El monto ingresado supera al saldo");
                    //}
                    //else
                    //{
                    //    if (CalcularSaldoXMoneda(tipo_moneda, 1) == Convert.ToDecimal(txtMonto.Text))
                    //        objDet.pdxpc_nmonto_pago_dxp = Convert.ToDecimal(obeDocXPagar.doxpc_nmonto_total_saldo);
                    //    else
                    //        objDet.pdxpc_nmonto_pago_dxp = saldo;
                    //}

                    objDet.doxpc_vnumero_doc = bteDocPagar.Text;
                    objDet.tdocc_vabreviatura_tipo_doc = lblTipoDoc.Text;
                    objDet.doxpc_icod_correlativo_dxp = Convert.ToInt64(bteDocPagar.Tag);
                }

                objDet.ctacc_iid_cuenta_contable = Convert.ToInt64(bteCuenta.Tag); //id de la cuenta contable(GRABAR EN BD)
                objDet.NumeroCuentaContable = bteCuenta.Text; //se mostrará en la grilla***
                objDet.DescripcionCuentaContable = txtCuentaDes.Text; //descripción explícita***
                if (bteCCosto.Tag == null)
                    objDet.cecoc_icod_centro_costo = null; //NINGUNO
                else
                    objDet.cecoc_icod_centro_costo = Convert.ToInt32(bteCCosto.Tag); //correlativo del centro de costo(GRABAR EN BD)
                objDet.CodigoCentroCosto = (string.IsNullOrEmpty(bteCCosto.Text)) ? "  -  " : bteCCosto.Text; //se mostrará en la grilla***
                objDet.DescripcionCentroCosto = txtcentrocosto.Text;
                if (bteSubAnalitica.Tag == null)
                    objDet.anac_icod_analitica = null; //NINGUNO
                else
                    objDet.anac_icod_analitica = Convert.ToInt32(bteSubAnalitica.Tag); //correlativo de la analítica(GRABAR EN BD)

                objDet.IdTipoAnalitica = (bteAnalitica.Tag == null) ? "  -  " : bteAnalitica.Text; //se mostrará en la grilla***
                objDet.NumeroAnalitica = (string.IsNullOrEmpty(bteSubAnalitica.Text)) ? "  -  " : bteSubAnalitica.Text; //se mostrará en la grilla***
                objDet.cdxpc_nmonto_cuenta = Convert.ToDecimal(txtMonto.Text); //monto a ingresar(GRABAR BD)
                objDet.cdxpc_vglosa = txtConcepto.Text; //descripción (GRABAR BD)
                objDet.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                objDet.intUsuario = Valores.intUsuario;
                objDet.strPc = WindowsIdentity.GetCurrent().Name;
                objDet.cdxpc_flag_estado = true; //estado del detalle

                objDet.prep_icod_presupuesto = Convert.ToInt32(btnPresupuesto.Tag);
                objDet.prep_cod_presupuesto = (btnPresupuesto.Text);
                objDet.prepd_icod_detalle = Convert.ToInt32(btnRugro.Tag);
                objDet.cpnd_vdescripcion = btnRugro.Text;



                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    objDet.TipOper = 1;
                    ListaDxPOcultar.Add(obeDocXPagar.doxpc_icod_correlativo);
                }
                else //modificación
                {
                    if (objDet.TipOper != 1)// indica si se ha creado recién(null ó 1) o se está modificando una que ha sido cargado de la base de datos(4)
                    {
                        objDet.TipOper = 2;
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
                        bteAnalitica.Text = String.Format("{0:00}", frm._Be.tablc_iid_tipo_analitica);
                    }
                    btnPresupuesto.Enabled = false;
                    btnRugro.Enabled = false;
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
                //Ccosto.intIdModulo = Parametros.intModCuentasPorPagar;
                if (Ccosto.ShowDialog() == DialogResult.OK)
                {
                    bteCCosto.Text = Ccosto._Be.cecoc_vcodigo_centro_costo;//cecoc_ccodigo_centro_costo - centro_costo
                    bteCCosto.Tag = Ccosto._Be.cecoc_icod_centro_costo;//cecoc_icod_centro_costo (correlativo) - centro_costo
                    txtcentrocosto.Text = Ccosto._Be.cecoc_vdescripcion;//cecoc_vdescripcion - centro_costo
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
                    bteAnalitica.Tag = frm._Be.tarec_icorrelativo_registro;//tarec_icodrrelativo_registro(código/int) - tabla_registro / se encuentra con el formato string 0x
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
                //frm.intIdModulo = Parametros.intModCuentasPorPagar;
                frm.intTipoAnalitica = Convert.ToInt32(bteAnalitica.Tag);//envía el código de la analítica para que filtre en el campo tarec_icorrelativo de la tabla analitica_detalle
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteSubAnalitica.Tag = frm._Be.anad_icod_analitica;//anac_icod_analitica(correlativo) - analitica_detalle
                    bteSubAnalitica.Text = frm._Be.anad_vdescripcion;
                    ana_cod = frm._Be.anad_iid_analitica;
                    if (Convert.ToInt32(bteAnalitica.Tag) == 5)
                        obeProveedor = new EProveedor() { iid_icod_proveedor = Convert.ToInt32(frm._Be.id_entidad), vcod_proveedor = frm._Be.anad_iid_analitica, vnombrecompleto = frm._Be.anad_vdescripcion, anac_icod_analitica = frm._Be.anad_icod_analitica };
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

        }

        private void FrmManteMovVariosDet_Load(object sender, EventArgs e)
        {
            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            ListaAnalitica = new BGeneral().listarTablaRegistro(24);
            ListaCentroCosto = new BContabilidad().listarCentroCosto();
            LoadMask();
        }
        string mask;
        private void LoadMask()
        {
            List<EParametroContable> lstParametroContable = (new BContabilidad()).listarParametroContable();
            lstParametroContable.ForEach(obe =>
            {
                //this.bteCuenta.Properties.Mask.BeepOnError = true;
                mask = obe.parac_vmascara;
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

        private void bteDocPagar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarDocumentos();
        }

        private void bteDocPagar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarDocumentos();
        }

        private void ListarDocumentos()
        {
            if (bteSubAnalitica.Tag != null)
            {
                using (FrmConsultaDxPPendxFecha frm = new FrmConsultaDxPPendxFecha())
                {
                    frm.fechaDoc = fechaDoc;
                    frm.ListaDxPOcultar = ListaDxPOcultar;
                    frm.obeProveedor = obeProveedor;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        obeDocXPagar = frm.obeDocxPagar;
                        lblTipoDoc.Text = frm.obeDocxPagar.tdocc_vabreviatura_tipo_doc;
                        bteDocPagar.Text = frm.obeDocxPagar.doxpc_vnumero_doc;
                        bteDocPagar.Tag = frm.obeDocxPagar.doxpc_icod_correlativo;
                        if (CalcularSaldoXMoneda(tipo_moneda, 1) > saldoDetalle)
                            txtMonto.Text = saldoDetalle.ToString();
                        else
                            txtMonto.Text = CalcularSaldoXMoneda(tipo_moneda, 1).ToString();
                    }
                }
            }
            else
                XtraMessageBox.Show("Seleccione un proveedor", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private decimal CalcularSaldoXMoneda(int tipo_moneda, int operacion)
        {
            decimal saldo = 0;

            if (obeDocXPagar.tablc_iid_tipo_moneda != tipo_moneda)
            {
                if (tipo_moneda == Parametros.intSoles)
                {
                    if (operacion == 1)
                        saldo = Convert.ToDecimal(obeDocXPagar.doxpc_nmonto_total_saldo) * tipo_cambio;
                    else
                        if (tipo_cambio != 0)
                        saldo = Convert.ToDecimal(txtMonto.Text) / tipo_cambio;
                }
                else
                {
                    if (operacion == 1)
                    {
                        if (tipo_cambio != 0)
                            saldo = Convert.ToDecimal(obeDocXPagar.doxpc_nmonto_total_saldo) / tipo_cambio;
                    }
                    else
                    {
                        saldo = Convert.ToDecimal(txtMonto.Text) * tipo_cambio;
                    }
                }
            }
            else
                saldo = (operacion == 1) ? Convert.ToDecimal(obeDocXPagar.doxpc_nmonto_total_saldo) : Convert.ToDecimal(txtMonto.Text);

            return Math.Round(saldo, 2);
        }

        private void bteAnalitica_EditValueChanged(object sender, EventArgs e)
        {
            int idAnalitica; int.TryParse((bteAnalitica.Tag == null) ? string.Empty : bteAnalitica.Tag.ToString(), out idAnalitica);
            if (idAnalitica == 5 && Status != BSMaintenanceStatus.ModifyCurrent)
                bteDocPagar.Enabled = true;
            else
                bteDocPagar.Enabled = false;
        }

        private void btnPresupuesto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarPresupuesto();
        }
        private void ListarPresupuesto()
        {
            using (FrmListarPresupuestoImportacion frm = new FrmListarPresupuestoImportacion())
            {

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnPresupuesto.Tag = frm._Be.prep_icod_presupuesto;
                    btnPresupuesto.Text = frm._Be.prep_cod_presupuesto;
                }
            }
        }

        private void ListarPresupuestoDet()
        {
            using (FrmListarPresupuestoImportacionDet frm = new FrmListarPresupuestoImportacionDet())
            {
                frm.IdPresupuestoNacional = Convert.ToInt32(btnPresupuesto.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnRugro.Tag = frm.movDetalle.prepd_icod_detalle;
                    btnRugro.Text = frm.movDetalle.cpnd_vdescripcion;

                    List<ECuentaContable> lstCuentasContables = new List<ECuentaContable>();
                    lstCuentasContables = new BContabilidad().listarCuentaContable().Where(on => on.ctacc_icod_cuenta_contable == frm.movDetalle.ctacc_iid_cuenta_contable).ToList();
                    if (lstCuentasContables.Count == 1)
                    {
                        clear();
                        bteCuenta.Text = lstCuentasContables[0].ctacc_numero_cuenta_contable;
                        bteCuenta.Tag = lstCuentasContables[0].ctacc_icod_cuenta_contable;
                        txtCuentaDes.Text = lstCuentasContables[0].ctacc_nombre_descripcion;
                        bteCCosto.Enabled = lstCuentasContables[0].ctacc_iccosto;

                        if (lstCuentasContables[0].tablc_iid_tipo_analitica != 0)
                        {
                            bteAnalitica.Enabled = true;
                            bteSubAnalitica.Enabled = true;
                            bteAnalitica.Tag = lstCuentasContables[0].tablc_iid_tipo_analitica;
                            bteAnalitica.Text = String.Format("{0:00}", lstCuentasContables[0].tablc_iid_tipo_analitica);
                        }
                    }
                    bteCuenta.Enabled = false;

                }
            }
        }

        private void btnRugro_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarPresupuestoDet();
        }

        private void btnPresupuesto_EditValueChanged(object sender, EventArgs e)
        {
            //if (btnPresupuesto.Text == "")
            //{
            //    clear();
            //    btnRugro.Tag = 0;
            //    btnRugro.Text = "";
            //    bteCuenta.Enabled = true;
            //    bteCuenta.Tag = 0;
            //    bteCuenta.Text = "";
            //    txtCuentaDes.Text = "";

            //}
        }

        private void bteCuenta_EditValueChanged(object sender, EventArgs e)
        {
            //if (bteCuenta.Text == "")
            //{
            //    btnPresupuesto.Tag = 0;
            //    btnPresupuesto.Text = "";
            //    btnRugro.Text = "";
            //    btnRugro.Tag = 0;
            //}
        }

        private void bteCuenta_InvalidValue(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ErrorText = "formato incorrecto";
        }

        private void bteCuenta_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex(mask);
            if (bteCuenta.Text != mask)
            {
                string cadena = bteCuenta.Text;

                if (cadena.Length == 7)
                {
                    e.Cancel = false;
                }
                else
                if (cadena.Length == 9)
                {
                    e.Cancel = false;
                }
                else
                if (cadena.Length == 11)
                {
                    e.Cancel = false;
                }
                else
                if (cadena.Length == 13)
                {
                    e.Cancel = false;
                }
                else
                if (cadena.Length == 15)
                {
                    e.Cancel = false;
                }
                else
                if (cadena.Length > 15)
                {
                    e.Cancel = false;
                }
                else if (cadena.Length == 0)
                {
                    e.Cancel = false;
                }
                else
                    e.Cancel = true;
            }
            else
                e.Cancel = false;
        }
    }
}