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
    public partial class FrmManteCajasChicas : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteCajaChica));

        private List<ECajasChicas> mlist = new List<ECajasChicas>();
        private List<ECajasChicas> mlistDetalle = new List<ECajasChicas>();
        private List<ECuentaContable> aux = new List<ECuentaContable>();
        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        private List<ETablaRegistro> ListaAnalitica = new List<ETablaRegistro>();
        private List<ETablaRegistro> auxA = new List<ETablaRegistro>();
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;


        public FrmManteCajasChicas()
        {
            InitializeComponent();
        }

        public List<ECajasChicas> oDetail;
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
            txtDescripcion.Enabled = false;

            BtnGuardar.Enabled = false;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtDescripcion.Enabled = true;
                BtnGuardar.Enabled = true;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtDescripcion.Enabled = true;
                bteCuenta.Enabled = true;
                BtnGuardar.Enabled = true;
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
                ECajasChicas oBe = new ECajasChicas();
                Obl = new BTesoreria();
                try
                {

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

                    int? ValNull;
                    ValNull = null;
                    oBe.cc_vdescricpcion = txtDescripcion.Text;
                    oBe.ctacc_icod_cuenta_contable = Convert.ToInt32(bteCuenta.Tag);
                    oBe.intUsuario = Valores.intUsuario;
                    oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();

                    //oBe.id_correlative_caja_chica = Convert.ToInt32(txtcodigo.Text);

                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        Obl.insertarCajasChicas(oBe);
                    }
                    else
                    {
                        oBe.cc_icod_caja_chica = Correlative;
                        Obl.modificarCajasChicas(oBe);
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

        private void btnCtaContable_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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
                    bteCuenta.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;

                }
            }
        }

        private void bteCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarCuenta();
        }


        private void clearcta()
        {
            txtCuentaDes.Text = string.Empty;
            bteCuenta.Tag = null;
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


                auxA = ListaAnalitica.Where(x => Convert.ToInt32(x.tarec_icorrelativo_registro) == aux[0].tablc_iid_tipo_analitica).ToList();
                if (auxA.Count == 1)
                {
                }
            }
            else
            {
                clearcta();
            }
        }

    }
}