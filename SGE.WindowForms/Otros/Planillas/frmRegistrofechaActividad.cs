using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmRegistrofechaActividad : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistrofechaActividad));
        private BSMaintenanceStatus Status;
        public EPersonal obe = new EPersonal();
        public List<EPersonal> lstDatosActividad = new List<EPersonal>();
        public frmRegistrofechaActividad()
        {
            InitializeComponent();
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
        public void setValues()
        {

            dteFechaInicioAct.DateTime = Convert.ToDateTime(obe.pctd_sfecha_ini_actividad);
            dteFechaCeseAct.DateTime = Convert.ToDateTime(obe.pctd_sfecha_fin_actividad);
            lkMotivo.Text = obe.strMotivo;
            lkMotivo.EditValue = Convert.ToInt32(obe.tbpd_icod_motivo_cese);
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            save();
        }
        public void save()
        {
            BaseEdit OBase = null;
            Boolean Flag = true;
            try
            {
                if (dteFechaInicioAct.Text == "")
                {
                    OBase = dteFechaInicioAct;
                    throw new ArgumentException("ingrese la fecha de Inicio de la Actividad");
                }

                obe.pctd_sfecha_ini_actividad = Convert.ToDateTime(dteFechaInicioAct.EditValue);
                obe.pctd_sfecha_fin_actividad = Convert.ToDateTime(dteFechaCeseAct.EditValue);
                obe.tiporegistro = 1;
                obe.strMotivo = lkMotivo.Text;
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.intTpoOperacion = 1;
                    lstDatosActividad.Add(obe);
                }
                if (true)
                {
                    if (obe.intTpoOperacion != 1)
                    {
                        obe.pctd_icod_persona_contratacion = obe.pctd_icod_persona_contratacion;
                        obe.intTpoOperacion = 2;
                    }
                }
            }
            catch (Exception ex)
            {
                if (OBase != null)
                {
                    OBase.Focus();
                    OBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    OBase.ErrorText = ex.Message;
                    OBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

    }
}