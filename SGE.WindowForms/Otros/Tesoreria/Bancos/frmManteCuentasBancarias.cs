using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
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

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class frmManteCuentasBancarias : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteCuentasBancarias));
        public List<EBancoCuenta> lstBancoCuentas = new List<EBancoCuenta>();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EBancoCuenta Obe = new EBancoCuenta();
        public EBanco objBanco = new EBanco();
        public frmManteCuentasBancarias()
        {
            InitializeComponent();
        }
        private void frmManteCuentasBancarias_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpTipoCuenta, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoCuentaBanco), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda).Where(x => x.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
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
            txtCodigo.Enabled = !Enabled;
            lkpSituacion.Enabled = !Enabled;
            txtNroCuenta.Enabled = !Enabled;
            lkpTipoCuenta.Enabled = !Enabled;
            lkpMoneda.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            txtAnalitica.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtCodigo.Enabled = Enabled;
                txtNroCuenta.Enabled = !Enabled;
                lkpTipoCuenta.Enabled = Enabled;
                var flagTieneMovimientos = new BTesoreria().verificarBancoCuentaMovimientos(Obe.bcod_icod_banco_cuenta);
                if (flagTieneMovimientos)
                    lkpMoneda.Enabled = Enabled;
                else
                    lkpMoneda.Enabled = !Enabled;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = Enabled;
        }

        public void setValues()
        {
            txtCodigo.Text = String.Format("{0:00}", Obe.bcod_iicod_banco_cuenta);
            lkpSituacion.EditValue = Obe.bcod_iid_situacion_cuenta;
            txtNroCuenta.Text = Obe.bcod_vnumero_cuenta;
            lkpTipoCuenta.EditValue = Obe.tablc_iid_tipo_cuenta_ef;
            lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
            txtAnalitica.Text = Obe.strCodAnalitica;
            bteAnalitica.Tag = Obe.tablc_iid_tipo_analitica;
            bteAnalitica.Text = Obe.strTipoAnalitica;
            bteCCosto.Tag = Obe.cecoc_icod_centro_costo;
            bteCCosto.Text = Obe.strCodCCosto;
            txtCCosto.Text = Obe.strDesCCosto;
            bteCtaContable.Tag = Obe.ctacc_icod_cuenta_contable;
            bteCtaContable.Text = Obe.strCodCtaContable;
            txtCtaDes.Text = Obe.strDesCtaContable;
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
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (String.IsNullOrEmpty(txtNroCuenta.Text))
                {
                    oBase = txtNroCuenta;
                    throw new ArgumentException("Ingrese Nro. de Cuenta");
                }
                if (verificarCuentaBanco(txtNroCuenta.Text))
                {
                    oBase = txtNroCuenta;
                    throw new ArgumentException("El Nro. de Cuenta ya existe en los registros de las cuentas");
                }
                if (Convert.ToInt32(bteCtaContable.Tag) == 0)
                {
                    oBase = bteCtaContable;
                    throw new ArgumentException("Seleccione cuenta contable");
                }
                int? intNullVal = null;
                Obe.bcoc_icod_banco = objBanco.bcoc_icod_banco;
                Obe.bcod_vnumero_cuenta = txtNroCuenta.Text;
                Obe.tablc_iid_tipo_cuenta_ef = Convert.ToInt32(lkpTipoCuenta.EditValue);
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.bcod_iid_situacion_cuenta = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.bcod_iicod_banco_cuenta = Convert.ToInt32(txtCodigo.Text);
                Obe.tarec_iid_tabla_registro = null;
                Obe.bcod_flag_estado = true;
                Obe.strBanco = objBanco.bcoc_vnombre_banco;
                Obe.strCodAnalitica = txtAnalitica.Text;
                Obe.ctacc_icod_cuenta_contable = Convert.ToInt32(bteCtaContable.Tag);
                Obe.cecoc_icod_centro_costo = (Convert.ToInt32(bteCCosto.Tag) == 0) ? intNullVal : Convert.ToInt32(bteCCosto.Tag);
                Obe.tablc_iid_tipo_analitica = Convert.ToInt32(bteAnalitica.Tag);


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.bcod_icod_banco_cuenta = new BTesoreria().insertarBancoCuenta(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BTesoreria().modificarBancoCuenta(Obe);
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Obe.bcod_icod_banco_cuenta);
                    this.Close();
                }
            }
        }
        private bool verificarCuentaBanco(string strCuenta)
        {
            try
            {
                bool exists = false;
                if (lstBancoCuentas.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstBancoCuentas.Where(x => x.bcod_vnumero_cuenta.Trim() == strCuenta.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstBancoCuentas.Where(x => x.bcod_vnumero_cuenta.Trim() == strCuenta.Trim() && x.bcod_icod_banco_cuenta != Obe.bcod_icod_banco_cuenta).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void bteCtaContable_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {

                    bteCtaContable.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCtaContable.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCtaDes.Text = frm._Be.ctacc_nombre_descripcion;
                    bteCCosto.Enabled = frm._Be.ctacc_iccosto;
                    if (!frm._Be.ctacc_iccosto)
                    {
                        bteCCosto.Text = String.Empty;
                        bteCCosto.Tag = null;
                        txtCCosto.Text = String.Empty;
                    }

                    //if (frm._Be.tablc_iid_tipo_analitica != 0)
                    //{
                    //    bteAnalitica.Enabled = true;
                    //    bteSubAnalitica.Enabled = true;
                    //    bteAnalitica.Tag = frm._Be.tablc_iid_tipo_analitica;
                    //    bteAnalitica.Text = string.Format("{0:00}", frm._Be.tablc_iid_tipo_analitica);
                    //}
                }
            }
        }

        private void bteAnalitica_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarAnalitica frm = new frmListarAnalitica())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAnalitica.Tag = frm._Be.tarec_icorrelativo_registro;
                    bteAnalitica.Text = frm._Be.tarec_vdescripcion;

                    //bteSubAnalitica.Tag = null;
                    //bteSubAnalitica.Text = string.Empty;
                }
            }
        }

        private void bteCCosto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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





    }
}