using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmManteCuota : DevExpress.XtraEditors.XtraForm
    {
        public ECuotasFactura eCuotasFactura = new ECuotasFactura();
        public EFacturaCab EFacturaCab = new EFacturaCab();
        public BSMaintenanceStatus oState;
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
        public void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

            }
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            //setValues();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            SetCancel();
        }
        public FrmManteCuota()
        {
            InitializeComponent();
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eCuotasFactura.favc_icod_factura = EFacturaCab.favc_icod_factura;
            eCuotasFactura.doxcc_icod_correlativo = (int)EFacturaCab.doxcc_icod_correlativo;
            eCuotasFactura.fcc_sfecha_pago = dtefecha.DateTime;
            eCuotasFactura.fcc_nmonto = Convert.ToDecimal(txtMonto.Text);
            eCuotasFactura.fcc_iestado = Convert.ToInt32(lkpEstado.EditValue);
            eCuotasFactura.fcc_inro_cuota = Convert.ToInt32(spinNro.EditValue);
            eCuotasFactura.fcc_nmonto_pagado = Convert.ToDecimal(txtMontoPagado.Text);
            eCuotasFactura.fcc_nsaldo = eCuotasFactura.fcc_nmonto - eCuotasFactura.fcc_nmonto_pagado;
            eCuotasFactura.intUsuario = Valores.intUsuario;
            eCuotasFactura.strPc = WindowsIdentity.GetCurrent().Name;
            this.DialogResult = DialogResult.OK;
        }

        private void FrmManteCuota_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpEstado, new BGeneral().listarTablaRegistro(98), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            dtefecha.EditValue = DateTime.Now;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                SetValues();
            }
        }
        public void SetValues()
        {
            txtMonto.Text = eCuotasFactura.fcc_nmonto.ToString();
            dtefecha.EditValue = eCuotasFactura.fcc_sfecha_pago;
            lkpEstado.EditValue = eCuotasFactura.fcc_iestado;
            spinNro.EditValue = eCuotasFactura.fcc_inro_cuota;
            txtMontoPagado.Text = eCuotasFactura.fcc_nmonto_pagado.ToString();
            eCuotasFactura.operacion = 2;
        }

        private void txtMonto_EditValueChanged(object sender, EventArgs e)
        {
            calcularEstado();
        }

        private void txtMontoPagado_EditValueChanged(object sender, EventArgs e)
        {
            calcularEstado();
        }

        void calcularEstado()
        {
            var monto = Convert.ToDecimal(txtMonto.Text);
            var montoPagado = Convert.ToDecimal(txtMontoPagado.Text);
            if (monto != montoPagado && montoPagado != 0)
            {
                lkpEstado.EditValue = 3681; //PARCIAL
            }
            if (montoPagado == 0 || monto == 0)
            {
                lkpEstado.EditValue = 3680; //GENERADO
            }
            else
            if (monto == montoPagado)
            {
                lkpEstado.EditValue = 3682; //CANCELADO
            }
        }
    }
}