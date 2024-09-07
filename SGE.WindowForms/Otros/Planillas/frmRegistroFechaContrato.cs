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
    public partial class frmRegistroFechaContrato : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroFechaContrato));
        private BSMaintenanceStatus Status;
        public EPersonal obe = new EPersonal();
        public List<EPersonal> lstDatosContrato = new List<EPersonal>();
        public int perc_icod_personal = 0;
        public List<EArchivos> lstArchivos = new List<EArchivos>();
        public EArchivos arc = new EArchivos();



        public frmRegistroFechaContrato()
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
            dtefechaInicioContr.DateTime = Convert.ToDateTime(obe.pctd_sfecha_ini_contrato);
            dtefechafinalCont.DateTime = Convert.ToDateTime(obe.pctd_sfecha_fin_contrato);
            if (obe.pctd_sfecha_cese == null || obe.pctd_sfecha_cese == Convert.ToDateTime("01/01/0001"))
            {
                dteFechaCese.DateTime = default(DateTime);
            }
            else
            {
                dteFechaCese.DateTime = Convert.ToDateTime(obe.pctd_sfecha_cese);
            }


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
                if (dtefechaInicioContr.Text == "")
                {
                    OBase = dtefechaInicioContr;
                    throw new ArgumentException("ingrese la fecha de Inicio del Contrato");
                }
                if (dtefechafinalCont.Text == "")
                {
                    OBase = dtefechafinalCont;
                    throw new ArgumentException("ingrese la fecha final del Contrato");
                }

                if (dtefechafinalCont.DateTime < dtefechaInicioContr.DateTime)
                {
                    OBase = dtefechafinalCont;
                    throw new ArgumentException("Error en el rango de fecha Inicial y Final");
                }
                obe.pctd_sfecha_ini_contrato = Convert.ToDateTime(dtefechaInicioContr.EditValue);
                obe.pctd_sfecha_fin_contrato = Convert.ToDateTime(dtefechafinalCont.EditValue);
                if (dteFechaCese.Text == "")
                {
                    obe.pctd_sfecha_cese = null;
                }
                else
                {
                    obe.pctd_sfecha_cese = dteFechaCese.DateTime;
                }
                if (lkpMotivoCese.Text == "")
                {
                    obe.tbpd_icod_motivo_cese = null;
                }
                else
                {
                    obe.tbpd_icod_motivo_cese = Convert.ToInt32(lkpMotivoCese.EditValue);
                }


                obe.tiporegistro = 2;
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.intTpoOperacion = 1;
                    lstDatosContrato.Add(obe);
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frm01ArchivosContrato frmdetalle = new frm01ArchivosContrato();
            frmdetalle.perc_icod_persona = perc_icod_personal;
            frmdetalle.lstArchivos = lstArchivos;
            frmdetalle.returnSeleccion();
            if (frmdetalle.ShowDialog() == DialogResult.OK)
            {
                lstArchivos = frmdetalle.lstArchivos;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void frmRegistroFechaContrato_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMotivoCese, new BPlanillas().listarComboTablaPlanillaDetalle(21), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            lkpMotivoCese.EditValue = null;
            if (Status != BSMaintenanceStatus.CreateNew)
            {
                lkpMotivoCese.EditValue = obe.tbpd_icod_motivo_cese;
            }
        }

        private void dteFechaCese_EditValueChanged(object sender, EventArgs e)
        {
            if (dteFechaCese.Text == "01/01/0001")
            {
                dteFechaCese.Text = "";
            }
        }





    }
}