using DevExpress.XtraEditors;
using SGE.BusinessLogic;
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

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmManteDxCDet : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteDxCDet));
        private BSMaintenanceStatus mStatus;
        public ELibroBancosDetalle Obj = new ELibroBancosDetalle();
        public int tip_mon = 0;
        public int Cab_icod_correlativo = 0;
        public List<ELibroBancosDetalle> oDetailList = new List<ELibroBancosDetalle>();
        string ana_cod = "";

        public EDocXCobrarCuentaContable objEDxC = new EDocXCobrarCuentaContable(); //*+
        public decimal saldoDetalle; //*+


        public FrmManteDxCDet()
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
            bteCuenta.Tag = objEDxC.ctacc_iid_cuenta_contable;
            bteCuenta.Text = objEDxC.CuentaContable;
            txtCuentaDes.Text = objEDxC.DescripcionCuentaContable;

            Enabled = (objEDxC.cecoc_icod_centro_costo != null && objEDxC.cecoc_icod_centro_costo != 0);
            bteCCosto.Enabled = Enabled;
            bteCCosto.Tag = objEDxC.cecoc_icod_centro_costo;
            bteCCosto.Text = (Enabled) ? objEDxC.CentroCosto : string.Empty;
            txtcentrocosto.Text = objEDxC.DescripcionCentroCosto;

            Enabled = (objEDxC.anac_icod_analitica != null && objEDxC.anac_icod_analitica != 0);
            bteAnalitica.Enabled = Enabled;
            bteSubAnalitica.Enabled = Enabled;
            bteAnalitica.Tag = objEDxC.TipoAnalitica;
            bteAnalitica.Text = (Enabled) ? string.Format("{0:00}", objEDxC.TipoAnalitica) : string.Empty;
            bteSubAnalitica.Tag = objEDxC.anac_icod_analitica;
            bteSubAnalitica.Text = (Enabled) ? objEDxC.Analitica : string.Empty;

            txtMonto.Text = objEDxC.ccdcc_nmonto.ToString();
            txtConcepto.Text = objEDxC.ccdcc_vglosa;
        }

        private void StatusControl()
        {
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                btnAgregar.Enabled = false;
                btnModificar.Enabled = true;
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
                if (string.IsNullOrEmpty(txtConcepto.Text))
                {
                    oBase = txtConcepto;
                    throw new ArgumentException("Ingrese concepto");
                }

                objEDxC.ctacc_iid_cuenta_contable = Convert.ToInt32(bteCuenta.Tag); //id de la cuenta contable(GRABAR EN BD)
                objEDxC.CuentaContable = bteCuenta.Text; //se mostrará en la grilla***
                objEDxC.DescripcionCuentaContable = txtCuentaDes.Text; //descripción explícita***
                if (bteCCosto.Tag == null)
                    objEDxC.cecoc_icod_centro_costo = null; //NINGUNO
                else
                    objEDxC.cecoc_icod_centro_costo = Convert.ToInt32(bteCCosto.Tag); //correlativo del centro de costo(GRABAR EN BD)
                objEDxC.CentroCosto = (string.IsNullOrEmpty(bteCCosto.Text)) ? "  -  " : bteCCosto.Text; //se mostrará en la grilla***
                objEDxC.DescripcionCentroCosto = txtcentrocosto.Text;
                if (bteSubAnalitica.Tag == null)
                    objEDxC.anac_icod_analitica = null; //NINGUNO
                else
                    objEDxC.anac_icod_analitica = Convert.ToInt32(bteSubAnalitica.Tag); //correlativo de la analítica(GRABAR EN BD)

                objEDxC.TipoAnalitica = (bteAnalitica.Tag == null) ? "  -  " : bteAnalitica.Text; //se mostrará en la grilla***
                objEDxC.Analitica = (string.IsNullOrEmpty(bteSubAnalitica.Text)) ? "  -  " : bteSubAnalitica.Text; //se mostrará en la grilla***
                objEDxC.ccdcc_nmonto = Convert.ToDecimal(txtMonto.Text); //monto a ingresar(GRABAR BD)
                objEDxC.ccdcc_vglosa = txtConcepto.Text; //descripción (GRABAR BD)
                objEDxC.ccdcc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                objEDxC.intUsuario = Valores.intUsuario;
                objEDxC.strPc = WindowsIdentity.GetCurrent().Name.ToString();


                if (Status == BSMaintenanceStatus.CreateNew)
                    objEDxC.operacion = 1;
                else //modificación
                {
                    if (objEDxC.operacion != 1)// indica si se ha creado recién(null ó 1) o se está modificando una que ha sido cargado de la base de datos(4)
                    {
                        objEDxC.operacion = 2;
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
                frm.intTipoAnalitica = Convert.ToInt32(bteAnalitica.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteSubAnalitica.Tag = frm._Be.anad_icod_analitica;//anac_icod_analitica(correlativo) - analitica_detalle
                    bteSubAnalitica.Text = frm._Be.anad_iid_analitica;//anac_iid_analitica(código/varchar) - analitica_detalle
                    ana_cod = frm._Be.anad_iid_analitica;
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

                bteAnalitica.Enabled = (Convert.ToInt32(aux[0].tablc_iid_tipo_analitica) != 0) ? true : false;
                bteSubAnalitica.Enabled = (Convert.ToInt32(aux[0].tablc_iid_tipo_analitica) != 0) ? true : false;

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
    }
}