using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class frmManteOrdenProduccionAreas : DevExpress.XtraEditors.XtraForm
    {
        public List<EOrdenProduccionAreas> lstOrdenProduccionAreas = new List<EOrdenProduccionAreas>();
        public EOrdenProduccionAreas obe = new EOrdenProduccionAreas();
        public string Categoria, SubCategoria;
        public decimal dblStockDisponible = 0;
        public int tipo_moneda = 0;
        public int remic_icod_remision = 0;
        public Boolean afecto;
        public string CodigoSUNAT, strDesUM;
        public int unidc_icod_unidad_medida;
        public bool FromAsignacion = false;
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



        private void StatusControl()
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

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();
        }
        public frmManteOrdenProduccionAreas()
        {
            InitializeComponent();
        }

        private void setValues()
        {
            lkpProceso.EditValue = obe.orprac_icod_proceso;

            btePersonal.Tag = obe.orprac_ipersonal;
            btePersonal.Text = obe.strpersonal;
            if (obe.orprac_sfecha_asignacion == null || obe.orprac_sfecha_asignacion == "")
            {
                dteFechaAsg.Text = obe.orprac_sfecha_asignacion;
            }
            else
            {
                dteFechaAsg.EditValue = Convert.ToDateTime(obe.orprac_sfecha_asignacion);
            }
            if (obe.orprac_sfecha_terminado == null || obe.orprac_sfecha_terminado == "")
            {
                dteFechaTerm.Text = obe.orprac_sfecha_terminado;
            }
            else
            {
                dteFechaTerm.EditValue = Convert.ToDateTime(obe.orprac_sfecha_terminado);
            }
            cbTermino.Checked = obe.orprac_bterminado;
            cbVªBª.Checked = obe.orprac_bvisto_bueno;
        }
        public void CargarCombos()
        {


            BSControls.LoaderLook(lkpProceso, new BCentral().listarAreaProduccion(), "arprc_vdescripcion", "arprc_iid_codigo", true);

        }
        private void frmManteFacturaDetalle_Load(object sender, EventArgs e)
        {
            this.Height = FromAsignacion ? 170 : this.Height;
            CargarCombos();
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                btePersonal.EditValue = obe.orprac_ipersonal;
                btePersonal.Text = obe.strpersonal;
                lkpProceso.EditValue = obe.orprac_icod_proceso;
                if (Convert.ToInt32(btePersonal.Tag) > 0)
                {
                    lkpProceso.Enabled = false;
                    dteFechaAsg.Enabled = true;
                    if (cbTermino.Checked == true)
                    {
                        dteFechaTerm.Enabled = true;
                    }
                    else
                    {
                        dteFechaTerm.Enabled = false;
                    }
                    cbTermino.Enabled = true;
                    cbVªBª.Enabled = true;
                }
                else
                {
                    lkpProceso.Enabled = false;
                    dteFechaAsg.Enabled = false;
                    if (cbTermino.Checked == true)
                    {
                        dteFechaTerm.Enabled = true;
                    }
                    else
                    {
                        dteFechaTerm.Enabled = false;
                    }
                    cbTermino.Enabled = false;
                    cbVªBª.Enabled = false;
                }

            }


        }


        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {
                if (Convert.ToInt32(btePersonal.Tag) != 0)
                {
                    if (string.IsNullOrEmpty(dteFechaAsg.Text))
                    {
                        oBase = dteFechaAsg;
                        throw new ArgumentException("Ingrese la fecha de asignación");
                    }
                }

                if (cbTermino.Checked)
                {
                    if (string.IsNullOrEmpty(dteFechaTerm.Text))
                    {
                        oBase = dteFechaTerm;
                        throw new ArgumentException("Ingrese la fecha de termino");
                    }
                }
                obe.orprac_icod_proceso = Convert.ToInt32(lkpProceso.EditValue);

                obe.orprac_ipersonal = Convert.ToInt32(btePersonal.Tag);
                obe.strpersonal = btePersonal.Text;
                obe.orprac_sfecha_asignacion = Convert.ToString(dteFechaAsg.Text);
                obe.orprac_sfecha_terminado = Convert.ToString(dteFechaTerm.Text);
                obe.orprac_bterminado = cbTermino.Checked;
                obe.orprac_bvisto_bueno = cbVªBª.Checked;
                obe.orprc_isituacion =
                    cbVªBª.Checked ? Constantes.EstadoTareaPersonalTerminado :
                                    (cbTermino.Checked ? Constantes.EstadoTareaPersonalTerminado : (Convert.ToInt32(btePersonal.Tag) > 0 ? Constantes.EstadoTareaPersonalAsignado : Constantes.EstadoTareaPersonalPendiente));
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;
                switch (obe.orprc_isituacion)
                {
                    case Constantes.EstadoTareaPersonalTerminado:
                        obe.strEstado = "TERMINADO";
                        break;
                    case Constantes.EstadoTareaPersonalAsignado:
                        obe.strEstado = "ASIGNADO";
                        break;
                    case Constantes.EstadoTareaPersonalPendiente:
                        obe.strEstado = "PENDIENTE";
                        break;

                }

                if (obe.orprac_iid_codigo == 0)
                {
                    obe.intTipoOperacion = 1;

                }
                else
                {
                    if (obe.intTipoOperacion != 1)
                        obe.intTipoOperacion = 2;
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }


        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void groupControl1_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }



        private void cbTermino_Click(object sender, EventArgs e)
        {
            dteFechaTerm.Enabled = cbTermino.Checked;
            if (!dteFechaTerm.Enabled)
                dteFechaTerm.EditValue = (DateTime?)null;
        }
        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }
        private void btePersonal_Click(object sender, EventArgs e)
        {
            try
            {
                using (ListarPersonal frm = new ListarPersonal())
                {
                    frm.icodAreaEspecifica = obe.orprac_icod_proceso;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btePersonal.Tag = frm._Be.perc_iid_personal;
                        btePersonal.Text = frm._Be.perc_vnombres_apellidos;
                        setFecha(dteFechaAsg);
                        dteFechaAsg.Enabled = true;

                    }
                }

                cbTermino.Enabled = true;
                cbVªBª.Enabled = true;
                if (cbTermino.Checked == true)
                {
                    dteFechaTerm.Enabled = true;
                }
                else
                {
                    dteFechaTerm.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarPersonal_Click(object sender, EventArgs e)
        {
            btePersonal.Tag = 0;
            btePersonal.Text = "";
            dteFechaAsg.EditValue = (DateTime?)null;
            dteFechaAsg.Enabled = false;
            cbTermino.Checked = false;
            cbTermino.Enabled = false;
            dteFechaTerm.EditValue = (DateTime?)null;
            dteFechaTerm.Enabled = false;
        }

        private void cbTermino_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bteAlmacen_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lkpTipoVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }


    }
}