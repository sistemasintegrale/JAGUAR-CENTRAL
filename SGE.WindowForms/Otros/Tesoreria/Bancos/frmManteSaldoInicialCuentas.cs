using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class frmManteSaldoInicialCuentas : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteSaldoInicialCuentas));
        public EBancoCuenta Obe = new EBancoCuenta();
        public frmManteSaldoInicialCuentas()
        {
            InitializeComponent();
        }
        private void cargar()
        {
            txtBanco.Text = Obe.strBanco;
            txtCuenta.Text = Obe.bcod_vnumero_cuenta;
            txtMoneda.Text = Obe.strMoneda;
            txtMonto.Text = (Obe.bcod_monto_apertura != null) ? Obe.bcod_monto_apertura.ToString() : "0.00";
            DtmFecha.EditValue = (Obe.bcod_fecha_apertura != null) ? Obe.bcod_fecha_apertura : DateTime.Now;
            if (Obe.intMotivo == Parametros.intMotivoSaldoInicial) { rbInicial.Checked = true; }
            if (Obe.intMotivo == Parametros.intMotivoApertura) { rbApertura.Checked = true; }
        }
        private void FrmManteSaldoInicialCuentas_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private BSMaintenanceStatus mStatus;
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
            if (Status == BSMaintenanceStatus.View)
            {
                txtMonto.Enabled = false;
                btnGuardar.Enabled = false;
                DtmFecha.Enabled = false;
                rbApertura.Enabled = false;
                rbInicial.Enabled = false;
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

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Convert.ToDecimal(txtMonto.Text) == 0)
            {
                if (XtraMessageBox.Show("El monto será registrado como 0.00\n\t\t¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    SetSave();
            }
            else
                SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (DtmFecha.EditValue == null)
                {
                    oBase = DtmFecha;
                    throw new ArgumentException("Seleccione fecha");
                }
                Obe.bcod_monto_apertura = Convert.ToDecimal(txtMonto.Text);
                Obe.bcod_fecha_apertura = Convert.ToDateTime(DtmFecha.EditValue);
                Obe.intAnio = Parametros.intEjercicio;
                Obe.intMes = Convert.ToDateTime(DtmFecha.EditValue).Month;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.intMotivo = Parametros.intMotivoApertura;
                Obe.intTipoDocumento = Parametros.intTipoDocAperturaCtaBanco;
                if (rbInicial.Checked == true)
                {
                    Obe.intMes = 0;
                    Obe.intMotivo = Parametros.intMotivoSaldoInicial;
                    Obe.intTipoDocumento = Parametros.intTipoDocSaldoInicialCtaBanco;
                }
                new BTesoreria().ActualizarSaldoInicialBancoCuenta(Obe);

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
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void rbInicial_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInicial.Checked == true)
            {
                DtmFecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddDays(-1);
                DtmFecha.Enabled = false;
            }
        }

        private void rbApertura_CheckedChanged(object sender, EventArgs e)
        {
            if (rbApertura.Checked == true)
            {
                DtmFecha.EditValue = DateTime.Now;
                DtmFecha.Enabled = true;
            }
        }


    }
}