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
    public partial class frmManteEstadoGanPerCtaFuncion : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMantePosFinanCtaCont));
        private BSMaintenanceStatus mStatus;
        private List<ECuentaContable> aux = new List<ECuentaContable>();
        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        public List<int> ListaCtasUtilizadas = new List<int>();
        public int Cab_icod_correlativo;
        public delegate void DelegadoMensaje(int Cab_icod_correlativo);
        public event DelegadoMensaje MiEvento;
        public EEstadoGanPerFuncion obePosFinan = new EEstadoGanPerFuncion();
        public EEstadoGanPerCtasFuncion obePosFinanCta = new EEstadoGanPerCtasFuncion();
        private BContabilidad obl = new BContabilidad();

        #endregion

        public frmManteEstadoGanPerCtaFuncion()
        {
            InitializeComponent();
        }

        #region Status

        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        private void StatusControl()
        {
            bool enabled = (Status == BSMaintenanceStatus.View);
            txtCuentaDes.Properties.ReadOnly = enabled;
            btnAniadir.Enabled = !enabled;
        }

        #endregion

        private void frmMantePosFinanCtaCont_Load(object sender, EventArgs e)
        {
            CargarControles();
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
                CargarDatosControles();
            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            LoadMask();
        }

        #region Controles

        //private void LoadMask()
        //{
        //List<Parametro> mlista = (new BCuentaContable()).ListarParametro();
        //mlista.ForEach(obe =>
        //{
        //    this.bteCuenta.Properties.Mask.BeepOnError = true;
        //    this.bteCuenta.Properties.Mask.EditMask = obe.Mascara;
        //    this.bteCuenta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
        //    this.bteCuenta.Properties.Mask.ShowPlaceHolders = false;
        //});
        //}
        public void LoadMask()
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
        private void CargarControles()
        {
            //mlistCuenta = new BCuentaContable().ListarCuentaContable().Where(x => x.iid_tipo_cuenta == 2).ToList();
            //LoadMask();
        }

        private void CargarDatosControles()
        {
            //bteCuenta.Tag = obePosFinanCta.posd_iid_cuenta_contable;
            //bteCuenta.Text = obePosFinanCta.ctacc_vnumero_cuenta_contable;
            //txtCuentaDes.Text = obePosFinanCta.ctacc_vnombre_descripcion_larga;
        }

        #endregion        

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
                    //clear();
                    bteCuenta.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;
                    frm.txtCodigo.Focus();
                }
            }
        }

        private void btnAniadir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (bteCuenta.Tag == null)
                {
                    oBase = bteCuenta;
                    throw new ArgumentException("Ingrese una cuenta");
                }

                if (ListaCtasUtilizadas.Exists(obe => obe == Convert.ToInt32(bteCuenta.Tag)))
                {
                    oBase = bteCuenta;
                    throw new ArgumentException("La cuenta ya se encuentra registrada");
                }

                obePosFinanCta.egpfd_iid_cuenta_contable = Convert.ToInt32(bteCuenta.Tag);
                obePosFinanCta.egpfc_icod_estado_gan_per_funcion = obePosFinan.egpfc_icod_estado_gan_per_funcion;

                obePosFinanCta.strPc = WindowsIdentity.GetCurrent().Name;
                obePosFinanCta.intUsuario = Valores.intUsuario;
                Cab_icod_correlativo = obl.InsertarPEstadoGanPerCtasFuncion(obePosFinanCta);
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    //oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    //oBase.ErrorText = ex.Message;
                    //oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if (!String.IsNullOrEmpty(ex.Message))
                    XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Cab_icod_correlativo);
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #region Mantenimiento Cuenta

        private void bteCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarCuenta();
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
            }
            else
            {
                clearcta();
            }
        }

        private void clearcta()
        {
            txtCuentaDes.Text = string.Empty;
            bteCuenta.Tag = null;
        }

        #endregion

    }
}