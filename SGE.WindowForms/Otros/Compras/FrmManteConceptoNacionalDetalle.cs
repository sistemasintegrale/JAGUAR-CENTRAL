using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmManteConceptoNacionalDetalle : DevExpress.XtraEditors.XtraForm
    {
        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        private List<ECuentaContable> aux = new List<ECuentaContable>();

        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteConceptoNacional));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public int CodeConcepto = 0;
        public List<EConceptoPresupuestoNacionalDetalle> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
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

        #endregion

        #region "Eventos"

        public FrmManteConceptoNacionalDetalle()
        {
            InitializeComponent();
        }

        private void FrmManteConceptoPresupuestoNacional_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            this.SetSave();
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            EventoKey(sender, e);
        }

        private void btnGrabar_KeyPress(object sender, KeyPressEventArgs e)
        {
            EventoKey(sender, e);
        }


        #endregion

        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtDescripcion.Enabled = !Enabled;
        }

        private void clearControl()
        {
            txtcodigo.Text = String.Format("{0:00}", Convert.ToInt32(Correlative));
            txtDescripcion.Text = "";
            txtDescripcion.Focus();
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
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
            BaseEdit oBase = null;
            Boolean Flag = true;
            EConceptoPresupuestoNacionalDetalle oBe = new EConceptoPresupuestoNacionalDetalle();

            try
            {
                if (string.IsNullOrEmpty(txtcodigo.Text))
                {
                    oBase = txtcodigo;
                    throw new ArgumentException("Ingresar Codigo");
                }

                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingresar Descripcion");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarCosto = oDetail.Where(oB => oB.cpnd_vdescripcion.ToUpper() == txtDescripcion.Text.ToUpper()).ToList();
                    if (BuscarCosto.Count > 0)
                    {
                        oBase = txtDescripcion;
                        throw new ArgumentException("La Descripcion Existe");
                    }

                    var CodigoRepetido = oDetail.Where(oB => oB.cpnd_viid_detalle_nacional.ToUpper() == txtcodigo.Text.ToUpper()).ToList();
                    if (CodigoRepetido.Count > 0)
                    {
                        oBase = txtcodigo;
                        throw new ArgumentException("El Codigo ya Existe");
                    }

                }

                oBe.cpnd_icod_detalle_nacional = 0;
                oBe.cpn_icod_concepto_nacional = CodeConcepto;
                oBe.cpnd_iid_detalle_nacional = Convert.ToInt32(txtcodigo.Text);
                oBe.cpnd_vdescripcion = txtDescripcion.Text;
                oBe.cpnd_iid_situacion_detalle = 1;
                oBe.cpnd_iusuario_crea = 0;
                oBe.cpnd_iusuario_modifica = 0;
                oBe.cpnd_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.cpnd_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.cpnd_flag_estado = true;

                if (btnCtaContable.Tag == null)
                {
                    oBe.ctacc_iid_cuenta_contable = null;
                }
                else
                    oBe.ctacc_iid_cuenta_contable = Convert.ToInt32(btnCtaContable.Tag);

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BCompras().InsertarConceptoPresupuestoNacionalDetalle(oBe);
                }
                else
                {
                    oBe.cpnd_icod_detalle_nacional = Correlative;
                    new BCompras().ActualizarConceptoPresupuestoNacionalDetalle(oBe);
                }
            }
            catch (Exception ex)
            {
                oBase.Focus();
                oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                oBase.ErrorText = ex.Message;
                oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                        Status = BSMaintenanceStatus.View;
                    else
                        Status = BSMaintenanceStatus.View;

                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        void EventoKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
            if (e.KeyChar == (char)Keys.Enter)
                this.btnGrabar_Click(sender, e);
        }

        #endregion

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FrmManteConceptoNacionalDetalle_Load(object sender, EventArgs e)
        {
            LoadMask();
            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
        }
        private void LoadMask()
        {
            List<EParametroContable> mlista = new BContabilidad().listarParametroContable();
            mlista.ForEach(obe =>
            {
                this.btnCtaContable.Properties.Mask.BeepOnError = true;
                this.btnCtaContable.Properties.Mask.EditMask = obe.parac_vmascara;
                this.btnCtaContable.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.btnCtaContable.Properties.Mask.ShowPlaceHolders = false;
            });
        }

        private void btnCtaContable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= (char)48 && e.KeyChar <= (char)57 || e.KeyChar <= (char)8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnCtaContable_KeyUp(object sender, KeyEventArgs e)
        {
            if (btnCtaContable.Text == "")
            {
                clearcta();
                return;
            }

            aux = mlistCuenta.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(btnCtaContable.Text.Replace(".", ""))).ToList();


            if (aux.Count == 1)
            {
                btnCtaContable.Tag = aux[0].ctacc_icod_cuenta_contable;
                txtdescripcioncuenta.Text = aux[0].ctacc_nombre_descripcion;
            }
            else
            {
                clearcta();
            }
        }
        private void clearcta()
        {
            txtdescripcioncuenta.Text = string.Empty;
            btnCtaContable.Tag = null;
        }

        private void btnCtaContable_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                FrmListaCuentaContableC Cuenta = new FrmListaCuentaContableC();
                Cuenta.tipocuenta = true;
                Cuenta.saldo_inicial = 0;
                if (Cuenta.ShowDialog() == DialogResult.OK)
                {
                    btnCtaContable.Tag = Cuenta._Be.ctacc_icod_cuenta_contable;
                    btnCtaContable.Text = Cuenta._Be.ctacc_numero_cuenta_contable.Replace(".", "");
                    txtdescripcioncuenta.Text = Cuenta._Be.ctacc_nombre_descripcion;
                }
            }
        }

        private void btnCtaContable_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}