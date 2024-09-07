using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Planillas
{
    public partial class FrmParametrosPlanilla2 : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteParametrosContables));
        private List<ESubDiario> lstSubDiario = new List<ESubDiario>();
        private List<ECuentaContable> lstCuentaContable = new List<ECuentaContable>();
        private List<ECentroCosto> lstCCosto = new List<ECentroCosto>();
        private BSMaintenanceStatus mStatus;
        private List<EParametroPlanilla> lstParametroCont = new List<EParametroPlanilla>();
        BPlanillas objContabilidadData = new BPlanillas();
        public delegate void DelegadoMensaje(int Cab_icod_correlativo);
        public event DelegadoMensaje MiEvento;
        public FrmParametrosPlanilla2()
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
            if (lstCuentaContable.Count > 0)
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
            StatusControl();
        }
        private void cargar()
        {
            /*----------------------------------------------------------------------------*/
            lstParametroCont = objContabilidadData.listarParametroPlanilla();
            //lstSubDiario = objContabilidadData.listarSubDiario();
            //lstCuentaContable = objContabilidadData.listarCuentaContable();
            //lstCCosto = objContabilidadData.listarCentroCosto();
            /*----------------------------------------------------------------------------*/
            if (lstParametroCont.Count > 0)
                SetModify();
            else
                SetInsert();
            /*----------------------------------------------------------------------------*/

        }

        private void FrmManteParametrosContables_Load(object sender, EventArgs e)
        {
            cargar();
            txtAsignacionFamiliar.Text = lstParametroCont[0].prpc_nasignacion_familiar.ToString();
            txtGratifiacion_Essalud.Text = lstParametroCont[0].prpc_ngratificacion_essalud.ToString();
            txtGratificacion_EPS.Text = lstParametroCont[0].prpc_ngratificacion_eps.ToString();
            bteRemuneracion.Tag = lstParametroCont[0].prpc_id_cta_remuneracion;
            bteRemuneracion.Text = lstParametroCont[0].CuentaRemuneracion;
            bteVacaciones.Tag = lstParametroCont[0].prpc_id_cta_vacaciones;
            bteVacaciones.Text = lstParametroCont[0].CuentaVacaciones;
            bteGratificaciones.Tag = lstParametroCont[0].prpc_id_cta_gratificaciones;
            bteGratificaciones.Text = lstParametroCont[0].CuentaGratificaciones;
            bteCTS.Tag = lstParametroCont[0].prpc_id_cta_cts;
            bteCTS.Text = lstParametroCont[0].CuentaCTS;
            txtPorc_Essalud.Text = lstParametroCont[0].prpc_nporc_essalud.ToString();
            txtPorc_EPS_pacifico.Text = lstParametroCont[0].prpc_nporc_eps_pacifico.ToString();
            txtPorc_EPS_Essalud.Text = lstParametroCont[0].prpc_nporc_eps_essalud.ToString();
            txtSueldo_Minimo.Text = lstParametroCont[0].prpc_nsueldo_minimo.ToString();
            /*Cuenta Destino*/
            bteDestinoVacaciones.Tag = lstParametroCont[0].prpc_id_cta_destino_vacaciones;
            bteDestinoVacaciones.Text = lstParametroCont[0].CuentaDestinoVacaciones;
            bteDestinoGratificaciones.Tag = lstParametroCont[0].prpc_id_cta_destino_gratificaciones;
            bteDestinoGratificaciones.Text = lstParametroCont[0].CuentaDestinoGratificaciones;
            bteDestinoCTS.Tag = lstParametroCont[0].prpc_id_cta_destino_cts;
            bteDestinoCTS.Text = lstParametroCont[0].CuentaDestinoCTS;
            txtDias_trabajo.Text = lstParametroCont[0].prpc_ndias_trabajo.ToString();
        }




        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            bool Flag = false;
            string mask = "";
            try
            {

                string mask2 = "";
                string mask3 = "";

                for (int i = mask.Length - 1; i > -1; i--)
                {
                    int k;
                    k = i;
                    if (i == 0)
                    {
                        mask2 = mask2 + "\\d{" + mask.Substring(0, 1) + "}?";
                        mask3 = mask3 + mask.Substring(i, 1);
                    }
                    else
                    {
                        mask2 = mask2 + "\\d{" + mask.Substring(i, 1) + "}?\\.";
                        mask3 = mask3 + mask.Substring(i, 1) + "-";
                    }

                }


                EParametroPlanilla Obe = new EParametroPlanilla();
                Obe.prpc_nasignacion_familiar = Convert.ToDecimal(txtAsignacionFamiliar.Text);
                Obe.prpc_ngratificacion_essalud = Convert.ToDecimal(txtGratifiacion_Essalud.Text);
                Obe.prpc_ngratificacion_eps = Convert.ToDecimal(txtGratificacion_EPS.Text);
                Obe.prpc_id_cta_remuneracion = Convert.ToInt32(bteRemuneracion.Tag);
                Obe.prpc_id_cta_vacaciones = Convert.ToInt32(bteVacaciones.Tag);
                Obe.prpc_id_cta_gratificaciones = Convert.ToInt32(bteGratificaciones.Tag);
                Obe.prpc_id_cta_cts = Convert.ToInt32(bteCTS.Tag);
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.prpc_nporc_essalud = Convert.ToDecimal(txtPorc_Essalud.Text);
                Obe.prpc_nporc_eps_pacifico = Convert.ToDecimal(txtPorc_EPS_pacifico.Text);
                Obe.prpc_nporc_eps_essalud = Convert.ToDecimal(txtPorc_EPS_Essalud.Text);
                Obe.prpc_nsueldo_minimo = Convert.ToDecimal(txtSueldo_Minimo.Text);
                /*Cuenta Destino*/
                Obe.prpc_id_cta_destino_vacaciones = Convert.ToInt32(bteDestinoVacaciones.Tag);
                Obe.prpc_id_cta_destino_gratificaciones = Convert.ToInt32(bteDestinoGratificaciones.Tag);
                Obe.prpc_id_cta_destino_cts = Convert.ToInt32(bteDestinoCTS.Tag);
                Obe.prpc_ndias_trabajo = Convert.ToDecimal(txtDias_trabajo.Text);

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    objContabilidadData.insertarParametroPlanilla(Obe);
                    Flag = true;
                }
                else
                {
                    if (XtraMessageBox.Show("\t\t\t\tLos datos serán actualizados\n ¿Está seguro que desea continuar con la grabación?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        objContabilidadData.modificarParametroPlanilla(Obe);
                        Flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
            }
            finally
            {
                if (Flag)
                    this.Close();
            }
        }

        private void FrmManteParametrosContables_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }



        private void bteRemuneracion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();
                }
            }
        }

        private void bteVacaciones_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();
                }
            }
        }

        private void bteCTS_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();
                }
            }
        }

        private void bteGratificaciones_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();
                }
            }
        }

        private void bteDestinoVacaciones_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();
                }
            }
        }

        private void bteDestinoGratificaciones_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();
                }
            }
        }

        private void bteDestinoCTS_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();
                }
            }
        }

    }
}
