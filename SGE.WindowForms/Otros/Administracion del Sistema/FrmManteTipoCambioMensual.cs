using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class FrmManteTipoCambioMensual : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteTipoCambioMensual));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public List<ETipoCambioMensual> oDetail;
        private BGeneral Obl;
        public FrmManteTipoCambioMensual()
        {
            InitializeComponent();
        }
        private void FrmManteTipoCambioMensual_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMeses, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);

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
            txtAnio.Enabled = false;
            lkpMeses.Enabled = false;
            txtValorCompra.Enabled = false;
            txtvalorventa.Enabled = false;
            btnGuardar.Enabled = false;
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                lkpMeses.Enabled = true;
                txtValorCompra.Enabled = true;
                txtvalorventa.Enabled = true;
                btnGuardar.Enabled = true;
            }

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtValorCompra.Enabled = true;
                txtvalorventa.Enabled = true;
                btnGuardar.Enabled = true;
            }

        }
        private void ClearControl()
        {
            txtValorCompra.Text = "0.0000";
            txtvalorventa.Text = "0.0000";
        }
        private void FillField(ETipoCambioMensual oBe)
        {
            txtAnio.Text = oBe.tcamm_iid_anio.ToString();
            lkpMeses.EditValue = oBe.mesec_iid_mes;
            txtValorCompra.Text = oBe.tcamm_ntipo_cambio_compra.ToString();
            txtvalorventa.Text = oBe.tcamm_ntipo_cambio_venta.ToString();
        }
        public void SetInsert()
        {
            txtAnio.Text = DateTime.Now.Year.ToString();
            lkpMeses.EditValue = DateTime.Now.Month;
            Status = BSMaintenanceStatus.CreateNew;
            ClearControl();
        }

        public void SetCancel(ETipoCambioMensual oBe)
        {
            Status = BSMaintenanceStatus.View;
            FillField(oBe);
        }

        public void SetModify(ETipoCambioMensual oBe)
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            FillField(oBe);
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            ETipoCambioMensual oBe = new ETipoCambioMensual();
            Obl = new BGeneral();
            try
            {
                if (lkpMeses.EditValue == null)
                {
                    oBase = lkpMeses;
                    throw new ArgumentException("Seleccione Mes");
                }
                if (string.IsNullOrEmpty(txtValorCompra.Text) || txtValorCompra.Text == "0.0000")
                {
                    oBase = txtValorCompra;
                    throw new ArgumentException("Ingresar el valor de la compra");
                }

                if (string.IsNullOrEmpty(txtvalorventa.Text) || txtvalorventa.Text == "0.0000")
                {
                    oBase = txtvalorventa;
                    throw new ArgumentException("Ingresar el valor de la venta");
                }

                oBe.tcamm_iid_anio = Convert.ToInt32(txtAnio.Text);
                oBe.mesec_iid_mes = Convert.ToInt32(lkpMeses.EditValue);
                oBe.tcamm_ntipo_cambio_compra = Convert.ToDecimal(txtValorCompra.Text);
                oBe.tcamm_ntipo_cambio_venta = Convert.ToDecimal(txtvalorventa.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarTipoCambioMensual(oBe);
                }
                else
                {
                    Obl.ActualizarTipoCambioMensual(oBe);
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

                Flag = false;
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}