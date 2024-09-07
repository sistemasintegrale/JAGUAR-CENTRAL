using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
//using SGE.BusinessLogic.Contabilidad;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmManteEstadoGanPerFuncion : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteEstadoGanPer));
        private BSMaintenanceStatus mStatus;
        public int Cab_icod_correlativo;
        public delegate void DelegadoMensaje(int Cab_icod_correlativo);
        public event DelegadoMensaje MiEvento;
        public EEstadoGanPerFuncion obeEstadoGanPer = new EEstadoGanPerFuncion();
        private BContabilidad obl = new BContabilidad();
        public List<string> ListaPosFinanIcod = new List<string>();

        #endregion

        public frmManteEstadoGanPerFuncion()
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

            txtLinea.Properties.ReadOnly = enabled;
            lkpTipoLinea.Properties.ReadOnly = enabled;
            txtConcepto.Properties.ReadOnly = enabled;
            btnGuardar.Enabled = !enabled;
        }

        #endregion

        private void frmMantePosFinanciera_Load(object sender, EventArgs e)
        {
            CargarControles();
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
                CargarDatosControles();
        }

        #region Controles

        public void CargarControles()
        {
            BSControls.LoaderLook(lkpTipoLinea, new BGeneral().listarTablaRegistro(84), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpSigno, new BGeneral().listarTablaRegistro(85), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpTotal, new BGeneral().listarTablaRegistro(86), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

        }
        private void CargarDatosControles()
        {
            txtLinea.Text = obeEstadoGanPer.egpfc_vlinea;
            lkpTipoLinea.EditValue = obeEstadoGanPer.tablc_icod_linea_registro;
            lkpTotal.EditValue = obeEstadoGanPer.tablc_icod_tipo_total;
            txtConcepto.Text = obeEstadoGanPer.egpfc_vconcepto;
            lkpSigno.EditValue = obeEstadoGanPer.tablc_icod_signo_monto;
        }


        #endregion

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToInt32(txtLinea.Text) <= 0)
                {
                    oBase = txtLinea;
                    throw new ArgumentException("Ingrese un número de línea correcto");
                }

                if (ListaPosFinanIcod.Exists(obe => obe == txtLinea.Text))
                {
                    oBase = txtLinea;
                    throw new ArgumentException("El número de línea ya ha sido asignado");
                }

                if (lkpTotal.Enabled == true)
                    obeEstadoGanPer.tablc_icod_tipo_total = Convert.ToInt32(lkpTotal.EditValue);
                else
                //obeEstadoGanPer.tablc_icod_tipo_total = null;

                if (string.IsNullOrEmpty(txtConcepto.Text))
                {
                    oBase = txtConcepto;
                    throw new ArgumentException("Ingrese un concepto");
                }

                obeEstadoGanPer.egpfc_vlinea = txtLinea.Text;
                obeEstadoGanPer.tablc_icod_linea_registro = Convert.ToInt32(lkpTipoLinea.EditValue);
                obeEstadoGanPer.egpfc_vconcepto = txtConcepto.Text;
                obeEstadoGanPer.tablc_icod_signo_monto = Convert.ToInt32(lkpSigno.EditValue);
                obeEstadoGanPer.tablc_icod_tipo_total = Convert.ToInt32(lkpTotal.EditValue);

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obeEstadoGanPer.strPc = WindowsIdentity.GetCurrent().Name;
                    obeEstadoGanPer.intUsuario = Valores.intUsuario;
                    Cab_icod_correlativo = obl.InsertarEstadoGanPerFuncion(obeEstadoGanPer);
                }
                else
                {
                    obeEstadoGanPer.strPc = WindowsIdentity.GetCurrent().Name;
                    obeEstadoGanPer.intUsuario = Valores.intUsuario;
                    obl.ModificarEstadoGanPerFuncion(obeEstadoGanPer);
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

        private void lkpTipoLinea_EditValueChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(lkpTipoLinea.EditValue) != 356)
            {
                lkpSigno.EditValue = 0;
                lkpSigno.Text = "";
                lkpTipoLinea.Focus();
            }
            else
            {
                BSControls.LoaderLook(lkpSigno, new BGeneral().listarTablaRegistro(85), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
                lkpTipoLinea.Focus();
            }
            if (Convert.ToInt32(lkpTipoLinea.EditValue) != 358 && Convert.ToInt32(lkpTipoLinea.EditValue) != 359)
            {
                lkpTotal.EditValue = 0;
                lkpTotal.Text = "";
                lkpTipoLinea.Focus();
            }
            else
            {
                BSControls.LoaderLook(lkpTotal, new BGeneral().listarTablaRegistro(86), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
                lkpTipoLinea.Focus();
            }

        }




    }
}