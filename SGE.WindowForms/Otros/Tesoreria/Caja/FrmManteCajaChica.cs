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

namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    public partial class FrmManteCajaChica : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteCajaChica));

        private List<ECajaChica> mlist = new List<ECajaChica>();
        private List<ECajaChica> mlistDetalle = new List<ECajaChica>();
        private List<ECuentaContable> aux = new List<ECuentaContable>();
        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        private List<ETablaRegistro> ListaAnalitica = new List<ETablaRegistro>();
        private List<ETablaRegistro> auxA = new List<ETablaRegistro>();
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;


        public FrmManteCajaChica()
        {
            InitializeComponent();
        }

        public List<ECajaChica> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BTesoreria Obl;
        public int Correlative = 0;


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
            txtNumeroCaja.Enabled = false;
            txtDescripcion.Enabled = false;
            txtResponsable.Enabled = false;
            lkpMoneda.Enabled = false;

            BtnGuardar.Enabled = false;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtDescripcion.Enabled = true;
                txtResponsable.Enabled = true;
                BtnGuardar.Enabled = true;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtNumeroCaja.Enabled = true;
                txtDescripcion.Enabled = true;
                txtResponsable.Enabled = true;
                lkpMoneda.Enabled = true;
                bteCuenta.Enabled = true;
                BtnGuardar.Enabled = true;
            }

        }
        private void btnAnalitica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                ListarAnalitica();
            }
        }

        private void FrmCajaChica_Load(object sender, EventArgs e)
        {
            cargar();
            LoadMask();
        }
        private void cargar()
        {
            var lstMoneda = new BGeneral().listarTablaRegistro(5);
            BSControls.LoaderLook(lkpMoneda, lstMoneda, "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            ListaAnalitica = new BGeneral().listarTablaRegistro(24);
        }
        private void LoadMask()
        {
            List<EParametroContable> mlista = new BContabilidad().listarParametroContable();
            mlista.ForEach(obe =>
            {
                this.bteCuenta.Properties.Mask.BeepOnError = true;
                this.bteCuenta.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCuenta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCuenta.Properties.Mask.ShowPlaceHolders = false;
                this.bteCuenta.Properties.Mask.UseMaskAsDisplayFormat = true;
            });
        }

        void form2_MiEvento()
        {
            cargar();
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            lkpMoneda.ItemIndex = 0;
        }


        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }


        private void SetSave()
        {
            {
                BaseEdit oBase = null;
                Boolean Flag = true;
                ECajaChica oBe = new ECajaChica();
                Obl = new BTesoreria();
                try
                {
                    if (string.IsNullOrEmpty(txtNumeroCaja.Text))
                    {
                        oBase = txtNumeroCaja;
                        throw new ArgumentException("Ingrese número de caja");
                    }
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        var BuscarCodigo = oDetail.Where(oB => oB.vnro_caja_liquida.ToUpper() == txtNumeroCaja.Text.ToUpper()).ToList();
                        if (BuscarCodigo.Count > 0)
                        {
                            oBase = txtDescripcion;
                            throw new ArgumentException("El número de caja ingresado, ya existe");
                        }
                    }

                    if (string.IsNullOrEmpty(txtDescripcion.Text))
                    {
                        oBase = txtDescripcion;
                        throw new ArgumentException("Ingrese descripción");
                    }

                    if (bteCuenta.Tag == null)
                    {
                        oBase = bteCuenta;
                        throw new ArgumentException("Ingrese o seleccione cuenta contable");
                    }
                    if (bteAnalitica.Enabled == true)
                    {
                        if (bteAnalitica.Tag == null)
                        {
                            oBase = bteAnalitica;
                            throw new ArgumentException("Seleccione analítica");
                        }
                    }
                    if (lkpMoneda.EditValue == null)
                    {
                        oBase = lkpMoneda;
                        throw new ArgumentException("Seleccione tipo de moneda");
                    }
                    int? ValNull;
                    ValNull = null;
                    oBe.icod_caja_liquida = 0;
                    oBe.vnro_caja_liquida = txtNumeroCaja.Text;
                    oBe.vdescrip_caja_liquida = txtDescripcion.Text;
                    oBe.vnom_responsable = txtResponsable.Text;
                    oBe.iid_cuenta_contable = Convert.ToInt32(bteCuenta.Tag);
                    oBe.icod_analitica = (bteSubAnalitica.Tag == null) ? ValNull : Convert.ToInt32(bteSubAnalitica.Tag);
                    oBe.iid_situacion_cuenta = 1;
                    oBe.iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                    oBe.iusuario_crea = Valores.intUsuario;
                    oBe.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();

                    //oBe.id_correlative_caja_chica = Convert.ToInt32(txtcodigo.Text);

                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        Obl.InsertarCajaChica(oBe);
                    }
                    else
                    {
                        oBe.iusuario_modifica = Valores.intUsuario;
                        oBe.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                        oBe.icod_caja_liquida = Correlative;
                        Obl.ActualizarCajaChica(oBe);
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
                    {
                        this.MiEvento();
                        this.Close();
                    }
                }
            }
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void BtnCancelar1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void btnAnalitica_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

        private void btnCtaContable_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCuenta();
        }
        private void clear()
        {
            bteAnalitica.Text = string.Empty;
            bteAnalitica.Tag = null;
            bteSubAnalitica.Text = string.Empty;
            bteSubAnalitica.Tag = null;
            bteAnalitica.Enabled = false;
            bteSubAnalitica.Enabled = false;
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

                    if (frm._Be.tablc_iid_tipo_analitica != 0)
                    {
                        bteAnalitica.Enabled = true;
                        bteSubAnalitica.Enabled = true;
                        bteAnalitica.Tag = frm._Be.tablc_iid_tipo_analitica;
                        bteAnalitica.Text = String.Format("{0:00}", frm._Be.tablc_iid_tipo_analitica);
                    }
                }
            }
        }

        private void bteCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarCuenta();
        }

        private void bteAnalitica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarAnalitica();
        }

        private void clearcta()
        {
            txtCuentaDes.Text = string.Empty;
            bteCuenta.Tag = null;
            //         
            bteAnalitica.Tag = null;
            bteAnalitica.Text = string.Empty;
            //
            bteSubAnalitica.Tag = null;
            bteSubAnalitica.Text = string.Empty;
            //            
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

                bteAnalitica.Enabled = (aux[0].tablc_iid_tipo_analitica != 0) ? true : false;
                bteSubAnalitica.Enabled = (aux[0].tablc_iid_tipo_analitica != 0) ? true : false;

                auxA = ListaAnalitica.Where(x => Convert.ToInt32(x.tarec_icorrelativo_registro) == aux[0].tablc_iid_tipo_analitica).ToList();
                if (auxA.Count == 1)
                {
                    bteAnalitica.Tag = auxA[0].tarec_icorrelativo_registro;
                    bteAnalitica.Text = String.Format("{0:00}", auxA[0].tarec_icorrelativo_registro);
                }
            }
            else
            {
                clearcta();
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
                }
            }
        }

        private void bteSubAnalitica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarSubAnalitica();
        }
    }
}