using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Planillas
{
    public partial class FrmMantGratificacion : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantGratificacion));


        private BSMaintenanceStatus mStatus;

        public EProvisionPlanillaPersonalDetalle obe = new EProvisionPlanillaPersonalDetalle();
        public List<EProvisionPlanillaPersonalDetalle> lstPlanillaPersonal = new List<EProvisionPlanillaPersonalDetalle>();
        BPlanillas objContabilidadData = new BPlanillas();

        public FrmMantGratificacion()
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
            if (lstPlanillaPersonal.Count > 0)
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
            //lstParametroCont = objContabilidadData.listarParametroPlanilla();
            //lstSubDiario = objContabilidadData.listarSubDiario();
            //lstCuentaContable = objContabilidadData.listarCuentaContable();
            //lstCCosto = objContabilidadData.listarCentroCosto();
            /*----------------------------------------------------------------------------*/

            SetModify();


        }

        private void FrmManteParametrosContables_Load(object sender, EventArgs e)
        {
            cargar();

            txtRemuneracion.Text = obe.pland_rem_basica.ToString();
            //txtGratifiacion_Essalud.Text = lstParametroCont[0].prpc_ngratificacion_essalud.ToString();
            //txtGratificacion_EPS.Text = lstParametroCont[0].prpc_ngratificacion_eps.ToString();
        }




        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            SetSave();
        }

        public void setValues()
        {
            //txtRemuneracion.Text = obe.pland_rem_basica.ToString();

        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            bool Flag = false;
            try
            {





                obe.pland_rem_basica = Convert.ToDecimal(txtRemuneracion.Text);
                obe.pland_nrem_computable = Convert.ToDecimal(txtRemuneracion.Text) + obe.pland_nasignacion_familiar;
                obe.pland_nmonto_gratificacion = Math.Round(Convert.ToDecimal(obe.pland_nrem_computable / 6), 2);
                if (obe.pland_beps == true)
                {
                    obe.pland_nbonificacion_mes = Math.Round((Convert.ToDecimal(obe.pland_nmonto_gratificacion) * Convert.ToDecimal(obe.prpc_ngratificacion_eps) / 100), 2);
                }
                else
                {
                    obe.pland_nbonificacion_mes = Math.Round((Convert.ToDecimal(obe.pland_nmonto_gratificacion) * Convert.ToDecimal(obe.prpc_ngratificacion_essalud) / 100), 2);
                }
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;




                if (XtraMessageBox.Show("\t\t\t\tLos datos serán actualizados\n ¿Está seguro que desea continuar con la grabación?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new BPlanillas().modificarProvisionPlanillaPersonalDetalle(obe);
                    Flag = true;
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

        //private void FrmManteParametrosContables_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Escape)
        //    {
        //        this.Close();
        //    }
        //}









    }
}
