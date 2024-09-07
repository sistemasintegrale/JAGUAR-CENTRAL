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

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmManteTipoCambio : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteModulo));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ETipoCambio Obe = new ETipoCambio();
        public List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();


        public frmManteTipoCambio()
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
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            dteFecha.Enabled = !Enabled;
            txtCompra.Enabled = !Enabled;
            txtVenta.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                dteFecha.Enabled = Enabled;
        }
        public void setValues()
        {
            dteFecha.EditValue = Obe.ticac_fecha_tipo_cambio;
            txtCompra.Text = Obe.ticac_tipo_cambio_compra.ToString();
            txtVenta.Text = Obe.ticac_tipo_cambio_venta.ToString();
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
                if (verificarFecha(Convert.ToDateTime(dteFecha.EditValue)))
                {
                    oBase = dteFecha;
                    throw new ArgumentException("Ya existe tipo de cambio registrado para esta fecha");
                }
                if (Convert.ToDecimal(txtCompra.Text) == 0)
                {
                    oBase = txtCompra;
                    throw new ArgumentException("Ingrese Tipo de Cambio - Compra");
                }

                if (Convert.ToDecimal(txtVenta.Text) == 0)
                {
                    oBase = txtVenta;
                    throw new ArgumentException("Ingrese Tipo de Cambio - Venta");
                }
                Obe.ticac_fecha_tipo_cambio = Convert.ToDateTime(dteFecha.EditValue);
                Obe.ticac_tipo_cambio_compra = Convert.ToDecimal(txtCompra.Text);
                Obe.ticac_tipo_cambio_venta = Convert.ToDecimal(txtVenta.Text);
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.ticac_flag_estado = true;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.ticac_icod_tipo_cambio = new BAdministracionSistema().insertarTipoCambio(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BAdministracionSistema().modificarTipoCambio(Obe);
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
                    this.MiEvento(Obe.ticac_icod_tipo_cambio);
                    this.Close();
                }
            }
        }

        private bool verificarFecha(DateTime dtFecha)
        {
            try
            {
                bool exists = false;
                if (lstTipoCambio.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTipoCambio.Where(x => x.ticac_fecha_tipo_cambio.ToShortDateString() == dtFecha.ToShortDateString()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTipoCambio.Where(x => x.ticac_fecha_tipo_cambio == dtFecha && x.ticac_icod_tipo_cambio != Obe.ticac_icod_tipo_cambio).ToList().Count > 0)
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

    }
}