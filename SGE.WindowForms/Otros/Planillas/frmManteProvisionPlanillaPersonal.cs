using DevExpress.XtraEditors;
using SGE.BusinessLogic;
//using SGE.WindowForms.Planillas.Registro_de_Datos;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmManteProvisionPlanillaPersonal : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteProvisionPlanillaPersonal));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /*----------------------------------------------------*/
        public EProvisionPlanillaPersonal oBe = new EProvisionPlanillaPersonal();
        /*----------------------------------------------------*/
        List<EProvisionPlanillaPersonalDetalle> lstProvisionDetalle = new List<EProvisionPlanillaPersonalDetalle>();
        List<EProvisionPlanillaPersonalDetalle> lstProvisionDetalleEliminados = new List<EProvisionPlanillaPersonalDetalle>();
        /*----------------------------------------------------*/
        public int Contrato = 0;
        public int FechInic, FechaFin, SumFecha = 0; //---Para fecha de contrato
        public int ctsmes = 0, ctsdias = 0;
        public DateTime? fechaCese, fechaCese2; public int EstFechaCese, SumaFechaCese = 0;
        public int i = 1;
        public int i2 = 0;
        public int ceseVoF = 0, ceseVoF2 = 0;
        public decimal? SumGratif, contGratif;
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
        public frmManteProvisionPlanillaPersonal()
        {
            InitializeComponent();
        }

        private void FrmManteNotaIngreso_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BPlanillas().listarTablaPlanillaDetalle(22), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpTipo, new BPlanillas().listarTablaPlanillaDetalle(23), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            Carga();
            lkpMes.EditValue = Convert.ToInt32(DateTime.Now.Month);
            lkpSituacion.EditValue = 6433;
            lkpTipo.EditValue = 6437;
            dteFecha.EditValue = DateTime.Now;


            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
            {

                lkpMes.EditValue = oBe.mesec_iid_mes;
                lkpSituacion.EditValue = oBe.tablc_iid_situacion_planilla;
                lkpTipo.EditValue = oBe.planc_iid_tipo_planilla;

            }

            //if (Convert.ToInt32(lkpMes.EditValue) == 1 && Parametros.intEjercicio == 2017)
            //{
            //    gridColumn35.OptionsColumn.AllowEdit = true;
            //    gridColumn35.OptionsColumn.AllowFocus = true;
            //}
            //else
            //{
            //    gridColumn35.OptionsColumn.AllowEdit = false;
            //    gridColumn35.OptionsColumn.AllowFocus = false;
            //}

        }

        private void Carga()
        {

            if (Status != BSMaintenanceStatus.CreateNew)
            {
                if (oBe.planc_iid_tipo_planilla == 6437)
                {
                    lstProvisionDetalle = new BPlanillas().listarProvisionPlanillaPersonalDetalle(oBe.planc_icod_planilla_personal);
                    grdDetalle.DataSource = lstProvisionDetalle;
                }
                else if (oBe.planc_iid_tipo_planilla == 6438)
                {
                    lstProvisionDetalle = new BPlanillas().listarProvisionPlanillaPersonalDetalle(oBe.planc_icod_planilla_personal);
                    grdCTS.DataSource = lstProvisionDetalle;
                }
                else if (oBe.planc_iid_tipo_planilla == 6436)
                {
                    lstProvisionDetalle = new BPlanillas().listarProvisionPlanillaPersonalDetalle(oBe.planc_icod_planilla_personal);
                    grdVacaciones.DataSource = lstProvisionDetalle;
                }

            }

        }

        private void StatusControl()
        {

            if (Status == BSMaintenanceStatus.View)
            {
                txtNumPlanilla.Enabled = false;
                txtObservaciones.Enabled = false;
                lkpSituacion.Enabled = false;
                lkpTipo.Enabled = false;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                lkpMes.Enabled = false;
                btnBuscar.Enabled = false;
                mnu.Enabled = false;
                dteFecha.Enabled = false;
            }


        }

        public void setValues()
        {
            txtNumPlanilla.Tag = oBe.planc_iid_planilla_personal;
            txtNumPlanilla.Text = oBe.NumPlanilla;
            txtObservaciones.Text = oBe.planc_vdescripcion;
            dteFecha.EditValue = oBe.planc_sfecha;
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


        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (lstProvisionDetalle.Count == 0)
                {
                    throw new ArgumentException("Ingresar Detalle");
                }
                if (Convert.ToDateTime(dteFecha.Text).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del ejercicio");
                }
                if (Convert.ToDateTime(dteFecha.Text).Month != Convert.ToInt32(lkpMes.EditValue))
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del Periodo");
                }

                /*---------------------------------------------------------*/
                oBe.planc_iid_planilla_personal = Convert.ToInt32(txtNumPlanilla.Text);
                oBe.planc_iid_anio = Parametros.intEjercicio;
                oBe.mesec_iid_mes = Convert.ToInt32(lkpMes.EditValue);
                oBe.tablc_iid_situacion_planilla = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.planc_iid_tipo_planilla = Convert.ToInt32(lkpTipo.EditValue);
                oBe.planc_vdescripcion = txtObservaciones.Text;
                oBe.planc_sfecha = Convert.ToDateTime(dteFecha.EditValue);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.planc_icod_planilla_personal = new BPlanillas().insertarProvisionPlanillaPersonal(oBe, lstProvisionDetalle);

                }
                else
                {

                    new BPlanillas().modificarProvisionPlanillaPersonal(oBe, lstProvisionDetalle, lstProvisionDetalleEliminados);
                }
                /*-------------------------------------------------*/
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
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(oBe.planc_icod_planilla_personal);
                    //imprimir();
                    this.Close();
                }
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

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                using (frmFacCompra2 frm = new frmFacCompra2())
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstProvisionDetalle.Clear();

                        viewDetalle.RefreshData();
                        viewDetalle.MoveLast();
                        viewDetalle.Focus();
                        btnBuscar.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            #region Gratifiacion
            if (Convert.ToInt32(lkpTipo.EditValue) == 6437)
            {
                int DiasporMes = 0;
                DiasporMes = System.DateTime.DaysInMonth(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
                List<EPersonal> lstPersonal = new List<EPersonal>();
                lstPersonal = new BPlanillas().listarPersonal().Where(z => z.perc_sfech_inicio <= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && (z.perc_icod_tip_contrato != 6392) && (z.perc_sfech_cese == null || z.perc_sfech_cese >= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))).OrderBy(s => s.ApellNomb).ToList();

                if (lstProvisionDetalle.Count > 0)
                {
                    lstProvisionDetalle.Clear();
                }
                foreach (var _bee in lstPersonal)
                {
                    EProvisionPlanillaPersonalDetalle PPD = new EProvisionPlanillaPersonalDetalle();
                    List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                    lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();
                    //******

                    Contrato = 0;
                    FechInic = 0;
                    FechaFin = 0;
                    fechaCese = null;
                    fechaCese2 = null;
                    EstFechaCese = 0;
                    SumaFechaCese = 0;
                    ceseVoF = 0;
                    ceseVoF2 = 0;
                    SumGratif = 0;
                    contGratif = 0;
                    List<EPersonal> lstContrato = new List<EPersonal>();
                    lstContrato = new BPlanillas().listarPersonal_contratacion(_bee.perc_icod_personal);
                    foreach (var item in lstContrato)
                    {
                        if (item.pctd_sfecha_ini_contrato <= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_fin_contrato >= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            Contrato = 1;
                        }
                        if (item.pctd_sfecha_ini_contrato <= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            FechInic = 1;
                        }
                        if (item.pctd_sfecha_fin_contrato >= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            FechaFin = 1;
                        }


                        if (item.pctd_sfecha_ini_contrato <= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_fin_contrato >= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            fechaCese = item.pctd_sfecha_cese;
                        }

                        if (item.pctd_sfecha_cese >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_cese <= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") || item.pctd_sfecha_cese == null)
                        {
                            fechaCese2 = item.pctd_sfecha_cese;

                            if (fechaCese2 >= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                            {
                                ceseVoF = 0;
                            }
                            else if (fechaCese2 == null)
                            {
                                ceseVoF = 0;
                            }
                            else
                            {
                                ceseVoF2 = 1;
                            }
                        }

                        //----

                        if (fechaCese >= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            EstFechaCese = 1;
                        }
                        else if (fechaCese == null)
                        {
                            EstFechaCese = 1;
                        }
                        else
                        {
                            SumaFechaCese = +1;
                        }

                    }

                    //******
                    if ((Contrato == 1 && EstFechaCese == 1 && SumaFechaCese == 0 && ceseVoF2 == 0) || ((FechInic + FechaFin) == 2 && EstFechaCese == 1 && SumaFechaCese == 0 && ceseVoF2 == 0) || (_bee.perc_icod_tip_contrato == 6391))
                    {
                        //-------
                        PPD.pland_iid_planilla_personal_det = i++;
                        PPD.pland_icod_personal = _bee.perc_icod_personal;
                        PPD.pland_ape_nom = _bee.ApellNomb;
                        PPD.pland_num_doc = _bee.perc_vnum_doc;
                        PPD.pland_cussp = _bee.perc_vcuspp;
                        PPD.pland_rem_basica = Convert.ToDecimal(_bee.perc_nmont_basico);
                        PPD.intUsuario = Valores.intUsuario;
                        PPD.strPc = WindowsIdentity.GetCurrent().Name;
                        PPD.pland_flag_estado = true;
                        PPD.pland_beps = _bee.perc_beps;
                        PPD.pland_sfecha_incio = _bee.perc_sfech_inicio;
                        PPD.pland_sfecha_cese = fechaCese;
                        PPD.pland_basignacion_familiar = _bee.perc_basig_familiar;
                        PPD.prpc_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                        PPD.prpc_ngratificacion_essalud = lstParametroPlanilla[0].prpc_ngratificacion_essalud;
                        PPD.prpc_ngratificacion_eps = lstParametroPlanilla[0].prpc_ngratificacion_eps;
                        PPD.strEPS = _bee.strEPS;

                        if (_bee.perc_basig_familiar == true)
                        {
                            PPD.pland_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                        }
                        else
                        {
                            PPD.pland_nasignacion_familiar = 0;
                        }
                        PPD.pland_nrem_computable = PPD.pland_rem_basica + PPD.pland_nasignacion_familiar;
                        List<EProvisionPlanillaPersonalDetalle> lstProv = new List<EProvisionPlanillaPersonalDetalle>();
                        lstProv = new BPlanillas().listarProvisionPlanillaPersonalDetalle_Gratif().Where(a => a.pland_icod_personal == PPD.pland_icod_personal).ToList();
                        foreach (var item in lstProv)
                        {
                            SumGratif += item.pland_nmonto_gratificacion;
                            contGratif = lstProv.Count();
                        }


                        PPD.pland_nmonto_gratificacion = Math.Round(Convert.ToDecimal(PPD.pland_nrem_computable / 6) + Convert.ToDecimal(((PPD.pland_nrem_computable / 6) * contGratif) - (SumGratif)), 2);




                        if (_bee.perc_beps == true)
                        {
                            PPD.pland_nbonificacion_mes = Math.Round((Convert.ToDecimal(PPD.pland_nmonto_gratificacion) * Convert.ToDecimal(lstParametroPlanilla[0].prpc_ngratificacion_eps) / 100), 2);
                        }
                        else
                        {
                            PPD.pland_nbonificacion_mes = Math.Round((Convert.ToDecimal(PPD.pland_nmonto_gratificacion) * Convert.ToDecimal(lstParametroPlanilla[0].prpc_ngratificacion_essalud) / 100), 2);
                        }

                        lstProvisionDetalle.Add(PPD);
                        //---------
                        grdDetalle.DataSource = lstProvisionDetalle;
                    }
                }

                grdDetalle.RefreshDataSource();
                btnBuscar.Enabled = false;
                lkpMes.Enabled = false;
                lkpTipo.Enabled = false;
            }
            #endregion
            #region CTS
            if (Convert.ToInt32(lkpTipo.EditValue) == 6438)
            {
                if (Convert.ToInt32(lkpMes.EditValue) == 1 && Parametros.intEjercicio == 2017)
                {
                    gridColumn35.OptionsColumn.AllowEdit = true;
                    gridColumn35.OptionsColumn.AllowFocus = true;
                    gridColumn35.AppearanceCell.BackColor = Color.FromArgb(255, 128, 0);
                }
                else
                {
                    gridColumn35.OptionsColumn.AllowEdit = false;
                    gridColumn35.OptionsColumn.AllowFocus = false;
                    gridColumn35.AppearanceCell.BackColor = Color.White;
                }

                int DiasporMes = 0;
                DiasporMes = System.DateTime.DaysInMonth(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
                List<EPersonal> lstPersonal = new List<EPersonal>();
                lstPersonal = new BPlanillas().listarPersonal().Where(z => z.perc_icod_tip_contrato != 6392).OrderBy(s => s.ApellNomb).ToList();

                if (lstProvisionDetalle.Count > 0)
                {
                    lstProvisionDetalle.Clear();
                }
                foreach (var _bee in lstPersonal)
                {
                    EProvisionPlanillaPersonalDetalle PPD = new EProvisionPlanillaPersonalDetalle();
                    List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                    lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();

                    List<EInicial_Prov_planilla_Det> lstInicialesProvPlanillaDet = new List<EInicial_Prov_planilla_Det>();
                    lstInicialesProvPlanillaDet = new BPlanillas().ListarInicial_Prov_Planilla_Detalle(1);
                    //******

                    Contrato = 0;
                    FechInic = 0;
                    FechaFin = 0;
                    fechaCese = null;
                    EstFechaCese = 0;
                    SumaFechaCese = 0;
                    ctsdias = 0;
                    List<EPersonal> lstContrato = new List<EPersonal>();
                    lstContrato = new BPlanillas().listarPersonal_contratacion(_bee.perc_icod_personal);
                    foreach (var item in lstContrato)
                    {
                        if (item.pctd_sfecha_ini_contrato <= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_fin_contrato >= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            Contrato = 1;
                        }
                        if (item.pctd_sfecha_ini_contrato >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_ini_contrato <= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            FechInic = 1;
                        }
                        if (item.pctd_sfecha_fin_contrato >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_fin_contrato <= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            FechaFin = 1;
                        }


                        if (item.pctd_sfecha_cese != null && item.pctd_sfecha_cese.Value.Year == Parametros.intEjercicio && item.pctd_sfecha_cese.Value.Month <= Convert.ToInt32(lkpMes.EditValue))
                        {

                            fechaCese = item.pctd_sfecha_cese;

                        }



                        if (item.pctd_sfecha_cese >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_cese <= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            ctsdias = item.pctd_sfecha_cese.Value.Day;

                        }
                        else
                        {
                            ctsdias = 0;
                        }






                    }

                    //******
                    if ((Contrato == 1 && (fechaCese >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") || fechaCese == null)) || ((FechInic == 1) || (FechaFin == 1) && (fechaCese >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") || fechaCese == null)) || (_bee.perc_icod_tip_contrato == 6391) && (fechaCese >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") || fechaCese == null))
                    {
                        //-------
                        PPD.pland_iid_planilla_personal_det = i++;
                        PPD.pland_icod_personal = _bee.perc_icod_personal;
                        PPD.pland_ape_nom = _bee.ApellNomb;
                        PPD.pland_num_doc = _bee.perc_vnum_doc;
                        PPD.pland_cussp = _bee.perc_vcuspp;
                        PPD.pland_rem_basica = Convert.ToDecimal(_bee.perc_nmont_basico);
                        PPD.intUsuario = Valores.intUsuario;
                        PPD.strPc = WindowsIdentity.GetCurrent().Name;
                        PPD.pland_flag_estado = true;
                        PPD.pland_beps = _bee.perc_beps;
                        PPD.pland_sfecha_incio = _bee.perc_sfech_inicio;
                        PPD.pland_sfecha_cese = fechaCese;
                        PPD.pland_basignacion_familiar = _bee.perc_basig_familiar;
                        PPD.prpc_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                        //PPD.prpc_ngratificacion_essalud = lstParametroPlanilla[0].prpc_ngratificacion_essalud;
                        //PPD.prpc_ngratificacion_eps = lstParametroPlanilla[0].prpc_ngratificacion_eps;
                        PPD.strEPS = _bee.strEPS;

                        if (_bee.perc_basig_familiar == true)
                        {
                            PPD.pland_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                        }
                        else
                        {
                            PPD.pland_nasignacion_familiar = 0;
                        }
                        ctsmes = 0;
                        if (Convert.ToInt32(lkpMes.EditValue) >= 5 && Convert.ToInt32(lkpMes.EditValue) <= 10)
                        {
                            switch (Convert.ToInt32(lkpMes.EditValue))
                            {
                                case 5:
                                    ctsmes = 1;
                                    break;
                                case 6:
                                    ctsmes = 2;
                                    break;
                                case 7:
                                    ctsmes = 3;
                                    break;
                                case 8:
                                    ctsmes = 4;
                                    break;
                                case 9:
                                    ctsmes = 5;
                                    break;
                                case 10:
                                    ctsmes = 6;
                                    break;

                            }
                        }
                        else
                        {

                            switch (Convert.ToInt32(lkpMes.EditValue))
                            {
                                case 11:
                                    ctsmes = 1;
                                    break;
                                case 12:
                                    ctsmes = 2;
                                    break;
                                case 1:
                                    ctsmes = 3;
                                    break;
                                case 2:
                                    ctsmes = 4;
                                    break;
                                case 3:
                                    ctsmes = 5;
                                    break;
                                case 4:
                                    ctsmes = 6;
                                    break;

                            }
                        }


                        PPD.pland_nrem_computable = PPD.pland_rem_basica + PPD.pland_nasignacion_familiar;
                        PPD.pland_ncts_horas_extras = 0;
                        PPD.pland_ncts_gratificacion = 0;
                        foreach (var item in lstInicialesProvPlanillaDet)
                        {
                            if (PPD.pland_icod_personal == item.perc_icod_personal)
                            {
                                if (Convert.ToInt32(lkpMes.EditValue) >= 1 && Convert.ToInt32(lkpMes.EditValue) <= 6 && Parametros.intEjercicio == 2017)
                                {
                                    PPD.pland_ncts_gratificacion = item.ippd_ninicial;
                                }

                            }
                        }
                        if (PPD.pland_ncts_gratificacion == 0)
                        {
                            PPD.pland_nctssexto_gratificacion = 0;
                        }
                        else
                        {
                            PPD.pland_nctssexto_gratificacion = PPD.pland_ncts_gratificacion / 6;
                        }

                        PPD.pland_ncts_comision = 0;
                        PPD.pland_nctssexto_comision = 0;
                        PPD.pland_ncts_total = (PPD.pland_nrem_computable) + (PPD.pland_nctssexto_comision) + (PPD.pland_nctssexto_gratificacion);




                        PPD.pland_nctsprovision_acumulada = 0;
                        PPD.pland_icts_meses = 0;
                        PPD.pland_icts_dias = 0;
                        List<EProvisionPlanillaPersonal> LSTPROV = new List<EProvisionPlanillaPersonal>();
                        LSTPROV = new BPlanillas().listarProvisionPlanillaPersonal().Where(z => Convert.ToInt32(z.planc_iid_tipo_planilla) == 6438).ToList();
                        foreach (var PROV in LSTPROV)
                        {
                            DateTime FechaPROV = Convert.ToDateTime(("01-" + lkpMes.EditValue + "-" + (Parametros.intEjercicio) + "").ToString());
                            FechaPROV = FechaPROV.AddMonths(-1);
                            if (PROV.mesec_iid_mes == FechaPROV.Date.Month && PROV.planc_iid_anio == FechaPROV.Date.Year)
                            {
                                List<EProvisionPlanillaPersonalDetalle> LSTPROVDET = new List<EProvisionPlanillaPersonalDetalle>();
                                LSTPROVDET = new BPlanillas().listarProvisionPlanillaPersonalDetalle(PROV.planc_icod_planilla_personal);
                                foreach (var PROVDET in LSTPROVDET)
                                {
                                    if (PROVDET.pland_icod_personal == PPD.pland_icod_personal)
                                    {
                                        //if ((Convert.ToInt32(lkpMes.EditValue) == 1 && Parametros.intEjercicio == 2017) || (PROVDET.pland_icts_meses == 0 || PROVDET.pland_icts_meses == null))
                                        //{
                                        //    if (ctsdias > 0 && ctsdias != DiasporMes)
                                        //    {
                                        //        PPD.pland_icts_meses = ctsmes - 1;
                                        //    }
                                        //    else
                                        //    {
                                        //        PPD.pland_icts_meses = ctsmes;
                                        //        if (ctsdias == DiasporMes)
                                        //        {
                                        //            ctsdias = 0;
                                        //        }
                                        //    }
                                        //    PPD.pland_icts_dias = ctsdias;
                                        //}
                                        //else
                                        //{
                                        PPD.pland_ncts_gratificacion = PROVDET.pland_ncts_gratificacion;
                                        PPD.pland_nctssexto_gratificacion = PPD.pland_ncts_gratificacion / 6;
                                        PPD.pland_ncts_total = (PPD.pland_nrem_computable) + (PPD.pland_nctssexto_comision) + (PPD.pland_nctssexto_gratificacion);
                                        PPD.pland_nctsprovision_acumulada = PROVDET.pland_nctsprovision_acumulada + PROVDET.pland_nctsprovision_mes;
                                        PPD.pland_icts_meses = PROVDET.pland_icts_meses + 1;
                                        PPD.pland_icts_dias = PROVDET.pland_icts_dias;
                                        //}

                                    }
                                }
                            }
                        }
                        if (Convert.ToInt32(lkpMes.EditValue) == 5 || Convert.ToInt32(lkpMes.EditValue) == 11)
                        {

                            PPD.pland_icts_meses = 1;
                            PPD.pland_icts_dias = 0;
                            PPD.pland_nctsprovision_acumulada = 0;
                        }
                        if (Convert.ToInt32(lkpMes.EditValue) == PPD.pland_sfecha_incio.Value.Month && PPD.pland_sfecha_incio.Value.Year == Parametros.intEjercicio)
                        {
                            if (Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") <= PPD.pland_sfecha_incio && PPD.pland_sfecha_incio <= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                            {
                                PPD.pland_icts_meses = 0;
                                PPD.pland_icts_dias = (DiasporMes - PPD.pland_sfecha_incio.Value.Day) + 1;
                            }
                        }
                        PPD.pland_ncts_meses_monto = ((PPD.pland_ncts_total / 12) * PPD.pland_icts_meses); ;
                        PPD.pland_ncts_dias_monto = ((PPD.pland_ncts_total / 360) * PPD.pland_icts_dias);
                        PPD.pland_ncts_por_mes = (PPD.pland_ncts_meses_monto + PPD.pland_ncts_dias_monto);
                        PPD.pland_nctsprovision_mes = Math.Round(Convert.ToDecimal(PPD.pland_ncts_por_mes - PPD.pland_nctsprovision_acumulada), 2);
                        lstProvisionDetalle.Add(PPD);
                        //---------

                    }
                }
                grdCTS.DataSource = lstProvisionDetalle;
                grdCTS.RefreshDataSource();
                btnBuscar.Enabled = false;
                lkpMes.Enabled = false;
                lkpTipo.Enabled = false;

            }
            #endregion
            #region Vacaciones
            if (Convert.ToInt32(lkpTipo.EditValue) == 6436)
            {
                int DiasporMes = 0;
                DiasporMes = System.DateTime.DaysInMonth(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
                List<EPersonal> lstPersonal = new List<EPersonal>();
                lstPersonal = new BPlanillas().listarPersonal().OrderBy(s => s.ApellNomb).ToList();

                if (lstProvisionDetalle.Count > 0)
                {
                    lstProvisionDetalle.Clear();
                }
                foreach (var _bee in lstPersonal)
                {
                    EProvisionPlanillaPersonalDetalle PPD = new EProvisionPlanillaPersonalDetalle();
                    List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                    lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();
                    //******

                    Contrato = 0;
                    FechInic = 0;
                    FechaFin = 0;
                    fechaCese = null;
                    EstFechaCese = 0;
                    SumaFechaCese = 0;
                    ctsdias = 0;
                    List<EPersonal> lstContrato = new List<EPersonal>();
                    lstContrato = new BPlanillas().listarPersonal_contratacion(_bee.perc_icod_personal);
                    foreach (var item in lstContrato)
                    {
                        if (item.pctd_sfecha_ini_contrato <= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_fin_contrato >= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            Contrato = 1;
                        }
                        if (item.pctd_sfecha_ini_contrato >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_ini_contrato <= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            FechInic = 1;
                        }
                        if (item.pctd_sfecha_fin_contrato >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_fin_contrato <= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            FechaFin = 1;
                        }

                        //if (item.pctd_sfecha_ini_contrato <= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_fin_contrato >= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        //{
                        fechaCese = item.pctd_sfecha_cese;

                        //}

                        if (item.pctd_sfecha_cese >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_cese <= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            ctsdias = item.pctd_sfecha_cese.Value.Day;

                        }
                        else
                        {
                            ctsdias = 0;
                        }






                    }

                    //******
                    if ((Contrato == 1 && (fechaCese >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") || fechaCese == null)) || ((FechInic == 1) || (FechaFin == 1) && (fechaCese >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") || fechaCese == null)) || (_bee.perc_icod_tip_contrato == 6391 && (fechaCese >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") || fechaCese == null)))
                    {
                        //-------
                        PPD.pland_iid_planilla_personal_det = i++;
                        PPD.pland_icod_personal = _bee.perc_icod_personal;
                        PPD.pland_ape_nom = _bee.ApellNomb;
                        PPD.pland_num_doc = _bee.perc_vnum_doc;
                        PPD.pland_cussp = _bee.perc_vcuspp;
                        PPD.strCargo = _bee.strCargo;
                        PPD.intUsuario = Valores.intUsuario;
                        PPD.strPc = WindowsIdentity.GetCurrent().Name;
                        PPD.pland_flag_estado = true;
                        PPD.pland_sfecha_incio = _bee.perc_sfech_inicio;
                        PPD.pland_sfecha_cese = fechaCese;
                        PPD.pland_rem_basica = Convert.ToDecimal(_bee.perc_nmont_basico);
                        if (_bee.perc_basig_familiar == true)
                        {
                            PPD.pland_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                        }
                        else
                        {
                            PPD.pland_nasignacion_familiar = 0;
                        }
                        PPD.pland_nrem_computable = PPD.pland_rem_basica + PPD.pland_nasignacion_familiar;
                        PPD.pland_vac_dias_Prov_mensual = Convert.ToDecimal(2.50);
                        PPD.pland_vac_dias_acumulado = 0;

                        List<EProvisionPlanillaPersonal> LSTPROV = new List<EProvisionPlanillaPersonal>();
                        LSTPROV = new BPlanillas().listarProvisionPlanillaPersonal().Where(z => Convert.ToInt32(z.planc_iid_tipo_planilla) == 6436).ToList();
                        foreach (var PROV in LSTPROV)
                        {
                            DateTime FechaPROV = Convert.ToDateTime(("01-" + lkpMes.EditValue + "-" + (Parametros.intEjercicio) + "").ToString());
                            FechaPROV = FechaPROV.AddMonths(-1);
                            if (PROV.mesec_iid_mes == FechaPROV.Date.Month && PROV.planc_iid_anio == FechaPROV.Date.Year)
                            {
                                List<EProvisionPlanillaPersonalDetalle> LSTPROVDET = new List<EProvisionPlanillaPersonalDetalle>();
                                LSTPROVDET = new BPlanillas().listarProvisionPlanillaPersonalDetalle(PROV.planc_icod_planilla_personal);
                                foreach (var PROVDET in LSTPROVDET)
                                {
                                    if (PROVDET.pland_icod_personal == PPD.pland_icod_personal)
                                    {
                                        if ((Convert.ToInt32(lkpMes.EditValue) == 1))
                                        {
                                            PPD.pland_vac_dias_acumulado = PPD.pland_vac_dias_Prov_mensual;
                                        }
                                        else
                                        {

                                            PPD.pland_vac_dias_acumulado = Math.Round(Convert.ToDecimal(PPD.pland_vac_dias_Prov_mensual + PROVDET.pland_vac_dias_acumulado), 2);
                                        }
                                    }
                                }
                            }
                        }

                        PPD.pland_vac_provision_mes = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable / lstParametroPlanilla[0].prpc_ndias_trabajo) * PPD.pland_vac_dias_Prov_mensual)), 2);
                        PPD.pland_vac_ajuste_mes = 0;
                        PPD.pland_vac_prov_tot_mensual = PPD.pland_vac_provision_mes + PPD.pland_vac_ajuste_mes;

                        lstProvisionDetalle.Add(PPD);
                        //---------

                    }
                }
                grdVacaciones.DataSource = lstProvisionDetalle;
                grdVacaciones.RefreshDataSource();
                btnBuscar.Enabled = false;
                lkpMes.Enabled = false;
                lkpTipo.Enabled = false;
            }
            #endregion
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipo.EditValue) == 6437)/*GRATIFICACION*/
            {
                using (frmManteAgregarPlanillaPersonal frm = new frmManteAgregarPlanillaPersonal())
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        EProvisionPlanillaPersonalDetalle PPD = new EProvisionPlanillaPersonalDetalle();
                        List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                        lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();
                        BPlanillas pers = new BPlanillas();
                        int Existe = 0;

                        foreach (var item in lstProvisionDetalle)
                        {
                            if (item.pland_icod_personal == frm.icod_personal)
                            {
                                XtraMessageBox.Show("EL Personal ya esta Registrado !!");
                                Existe = 1;
                            }
                        }


                        if (Existe == 0)
                        {

                            i2 = lstProvisionDetalle.Max(x => x.pland_iid_planilla_personal_det);
                            PPD.pland_iid_planilla_personal_det = i2 + 1;
                            PPD.pland_icod_personal = frm.icod_personal;
                            PPD.pland_ape_nom = frm.Nombre_Apellidos;
                            PPD.pland_num_doc = frm.num_documento;
                            PPD.pland_cussp = frm.cussp;
                            PPD.pland_rem_basica = frm.rem_basica;
                            PPD.intUsuario = Valores.intUsuario;
                            PPD.strPc = WindowsIdentity.GetCurrent().Name;
                            PPD.pland_flag_estado = true;
                            PPD.pland_beps = frm.beps;
                            PPD.pland_sfecha_incio = frm.fecha_incio;
                            List<EPersonal> lst_pers_Contrato = new List<EPersonal>();
                            lst_pers_Contrato = new BPlanillas().listarPersonal_contratacion(PPD.pland_icod_personal);
                            foreach (var item in lst_pers_Contrato)
                            {
                                if (item.pctd_sfecha_cese != null)
                                {
                                    PPD.pland_sfecha_cese = item.pctd_sfecha_cese;
                                }
                            }
                            PPD.pland_basignacion_familiar = frm.basignacion_familiar;
                            PPD.prpc_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                            PPD.prpc_ngratificacion_essalud = lstParametroPlanilla[0].prpc_ngratificacion_essalud;
                            PPD.prpc_ngratificacion_eps = lstParametroPlanilla[0].prpc_ngratificacion_eps;

                            if (frm.basignacion_familiar == true)
                            {
                                PPD.pland_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                            }
                            else
                            {
                                PPD.pland_nasignacion_familiar = 0;
                            }
                            PPD.pland_nrem_computable = PPD.pland_rem_basica + PPD.pland_nasignacion_familiar;
                            PPD.pland_nmonto_gratificacion = Math.Round(Convert.ToDecimal(PPD.pland_nrem_computable / 6), 2);
                            if (frm.beps == true)
                            {
                                PPD.pland_nbonificacion_mes = Math.Round((Convert.ToDecimal(PPD.pland_nmonto_gratificacion) * Convert.ToDecimal(lstParametroPlanilla[0].prpc_ngratificacion_eps) / 100), 2);
                                PPD.strEPS = "EPS";
                            }
                            else
                            {
                                PPD.pland_nbonificacion_mes = Math.Round((Convert.ToDecimal(PPD.pland_nmonto_gratificacion) * Convert.ToDecimal(lstParametroPlanilla[0].prpc_ngratificacion_essalud) / 100), 2);
                                PPD.strEPS = "ESSALUD";
                            }
                            PPD.operacion = 1;

                            lstProvisionDetalle.Add(PPD);
                            grdDetalle.RefreshDataSource();
                        }
                    }
                }
            }
            else if (Convert.ToInt32(lkpTipo.EditValue) == 6438)/*CTS*/
            {
                using (frmManteAgregarPlanillaPersonal frm = new frmManteAgregarPlanillaPersonal())
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        int DiasporMes = 0;
                        DiasporMes = System.DateTime.DaysInMonth(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
                        EProvisionPlanillaPersonalDetalle PPD = new EProvisionPlanillaPersonalDetalle();
                        List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                        lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();
                        BPlanillas pers = new BPlanillas();
                        int Existe = 0;

                        foreach (var item in lstProvisionDetalle)
                        {
                            if (item.pland_icod_personal == frm.icod_personal)
                            {
                                XtraMessageBox.Show("EL Personal ya esta Registrado !!");
                                Existe = 1;
                            }
                        }


                        if (Existe == 0)
                        {

                            i2 = lstProvisionDetalle.Max(x => x.pland_iid_planilla_personal_det);
                            PPD.pland_iid_planilla_personal_det = i2 + 1;
                            PPD.pland_icod_personal = frm.icod_personal;
                            PPD.pland_ape_nom = frm.Nombre_Apellidos;
                            PPD.pland_num_doc = frm.num_documento;
                            PPD.pland_cussp = frm.cussp;
                            PPD.pland_rem_basica = frm.rem_basica;
                            PPD.intUsuario = Valores.intUsuario;
                            PPD.strPc = WindowsIdentity.GetCurrent().Name;
                            PPD.pland_flag_estado = true;
                            PPD.pland_beps = frm.beps;
                            PPD.pland_sfecha_incio = frm.fecha_incio;
                            List<EPersonal> lst_pers_Contrato = new List<EPersonal>();
                            lst_pers_Contrato = new BPlanillas().listarPersonal_contratacion(PPD.pland_icod_personal);
                            foreach (var item in lst_pers_Contrato)
                            {
                                if (item.pctd_sfecha_cese != null)
                                {
                                    PPD.pland_sfecha_cese = item.pctd_sfecha_cese;
                                }
                            }

                            PPD.pland_basignacion_familiar = frm.basignacion_familiar;
                            PPD.prpc_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                            List<EPersonal> lstpersonal = new List<EPersonal>();
                            lstpersonal = new BPlanillas().listarPersonal().Where(a => a.perc_icod_personal == PPD.pland_icod_personal).ToList();
                            foreach (var _bee in lstpersonal)
                            {
                                PPD.strEPS = _bee.strEPS;

                                if (_bee.perc_basig_familiar == true)
                                {
                                    PPD.pland_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                                }
                                else
                                {
                                    PPD.pland_nasignacion_familiar = 0;
                                }
                                ctsmes = 0;
                                if (Convert.ToInt32(lkpMes.EditValue) >= 5 && Convert.ToInt32(lkpMes.EditValue) <= 10)
                                {
                                    switch (Convert.ToInt32(lkpMes.EditValue))
                                    {
                                        case 5:
                                            ctsmes = 1;
                                            break;
                                        case 6:
                                            ctsmes = 2;
                                            break;
                                        case 7:
                                            ctsmes = 3;
                                            break;
                                        case 8:
                                            ctsmes = 4;
                                            break;
                                        case 9:
                                            ctsmes = 5;
                                            break;
                                        case 10:
                                            ctsmes = 6;
                                            break;

                                    }
                                }
                                else
                                {

                                    switch (Convert.ToInt32(lkpMes.EditValue))
                                    {
                                        case 11:
                                            ctsmes = 1;
                                            break;
                                        case 12:
                                            ctsmes = 2;
                                            break;
                                        case 1:
                                            ctsmes = 3;
                                            break;
                                        case 2:
                                            ctsmes = 4;
                                            break;
                                        case 3:
                                            ctsmes = 5;
                                            break;
                                        case 4:
                                            ctsmes = 6;
                                            break;

                                    }
                                }
                                PPD.pland_nrem_computable = PPD.pland_rem_basica + PPD.pland_nasignacion_familiar;
                                PPD.pland_ncts_horas_extras = 0;
                                PPD.pland_ncts_gratificacion = 0;
                                PPD.pland_nctssexto_gratificacion = 0;
                                PPD.pland_ncts_comision = 0;
                                PPD.pland_nctssexto_comision = 0;
                                PPD.pland_ncts_total = (PPD.pland_nrem_computable) + (PPD.pland_nctssexto_comision) + (PPD.pland_nctssexto_gratificacion);




                                PPD.pland_nctsprovision_acumulada = 0;
                                PPD.pland_icts_meses = 0;
                                PPD.pland_icts_dias = 0;
                                List<EProvisionPlanillaPersonal> LSTPROV = new List<EProvisionPlanillaPersonal>();
                                LSTPROV = new BPlanillas().listarProvisionPlanillaPersonal().Where(z => Convert.ToInt32(z.planc_iid_tipo_planilla) == 6438).ToList();
                                foreach (var PROV in LSTPROV)
                                {
                                    DateTime FechaPROV = Convert.ToDateTime(("01-" + lkpMes.EditValue + "-" + (Parametros.intEjercicio) + "").ToString());
                                    FechaPROV = FechaPROV.AddMonths(-1);
                                    if (PROV.mesec_iid_mes == FechaPROV.Date.Month && PROV.planc_iid_anio == FechaPROV.Date.Year)
                                    {
                                        List<EProvisionPlanillaPersonalDetalle> LSTPROVDET = new List<EProvisionPlanillaPersonalDetalle>();
                                        LSTPROVDET = new BPlanillas().listarProvisionPlanillaPersonalDetalle(PROV.planc_icod_planilla_personal);
                                        foreach (var PROVDET in LSTPROVDET)
                                        {
                                            if (PROVDET.pland_icod_personal == PPD.pland_icod_personal)
                                            {
                                                if ((Convert.ToInt32(lkpMes.EditValue) == 1 && Parametros.intEjercicio == 2017) || (PROVDET.pland_icts_meses == 0 || PROVDET.pland_icts_meses == null))
                                                {
                                                    if (ctsdias > 0 && ctsdias != DiasporMes)
                                                    {
                                                        PPD.pland_icts_meses = ctsmes - 1;
                                                    }
                                                    else
                                                    {
                                                        PPD.pland_icts_meses = ctsmes;
                                                        if (ctsdias == DiasporMes)
                                                        {
                                                            ctsdias = 0;
                                                        }
                                                    }
                                                    PPD.pland_icts_dias = ctsdias;
                                                }
                                                else
                                                {
                                                    PPD.pland_nctsprovision_acumulada = PROVDET.pland_nctsprovision_acumulada + PROVDET.pland_nctsprovision_mes;
                                                    PPD.pland_icts_meses = PROVDET.pland_icts_meses + 1;
                                                    PPD.pland_icts_dias = PROVDET.pland_icts_dias;
                                                }
                                            }
                                        }
                                    }
                                }

                                PPD.pland_ncts_meses_monto = ((PPD.pland_ncts_total / 12) * PPD.pland_icts_meses); ;
                                PPD.pland_ncts_dias_monto = ((PPD.pland_ncts_total / 360) * PPD.pland_icts_dias);
                                PPD.pland_ncts_por_mes = (PPD.pland_ncts_meses_monto + PPD.pland_ncts_dias_monto);
                                PPD.pland_nctsprovision_mes = Math.Round(Convert.ToDecimal(PPD.pland_ncts_por_mes - PPD.pland_nctsprovision_acumulada), 2);
                            }

                            PPD.operacion = 1;

                            lstProvisionDetalle.Add(PPD);
                            grdCTS.RefreshDataSource();

                        }
                    }
                }
            }
            else if (Convert.ToInt32(lkpTipo.EditValue) == 6436)/*Vacaciones*/
            {

                using (frmManteAgregarPlanillaPersonal frm = new frmManteAgregarPlanillaPersonal())
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        int DiasporMes = 0;
                        DiasporMes = System.DateTime.DaysInMonth(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
                        EProvisionPlanillaPersonalDetalle PPD = new EProvisionPlanillaPersonalDetalle();
                        List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                        lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();
                        BPlanillas pers = new BPlanillas();
                        int Existe = 0;

                        foreach (var item in lstProvisionDetalle)
                        {
                            if (item.pland_icod_personal == frm.icod_personal)
                            {
                                XtraMessageBox.Show("EL Personal ya esta Registrado !!");
                                Existe = 1;
                            }
                        }


                        if (Existe == 0)
                        {
                            i2 = lstProvisionDetalle.Max(x => x.pland_iid_planilla_personal_det);
                            PPD.pland_iid_planilla_personal_det = i2 + 1;
                            PPD.pland_icod_personal = frm.icod_personal;
                            PPD.pland_ape_nom = frm.Nombre_Apellidos;
                            PPD.pland_num_doc = frm.num_documento;
                            PPD.strCargo = frm.strCargo;
                            PPD.intUsuario = Valores.intUsuario;
                            PPD.strPc = WindowsIdentity.GetCurrent().Name;
                            PPD.pland_flag_estado = true;
                            PPD.pland_sfecha_incio = frm.fecha_incio;
                            List<EPersonal> lst_pers_Contrato = new List<EPersonal>();
                            lst_pers_Contrato = new BPlanillas().listarPersonal_contratacion(PPD.pland_icod_personal);
                            foreach (var item in lst_pers_Contrato)
                            {
                                if (item.pctd_sfecha_cese != null)
                                {
                                    PPD.pland_sfecha_cese = item.pctd_sfecha_cese;
                                }
                            }
                            PPD.pland_rem_basica = Convert.ToDecimal(frm.rem_basica);
                            if (frm.basignacion_familiar == true)
                            {
                                PPD.pland_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                            }
                            else
                            {
                                PPD.pland_nasignacion_familiar = 0;
                            }
                            PPD.pland_nrem_computable = PPD.pland_rem_basica + PPD.pland_nasignacion_familiar;
                            PPD.pland_vac_dias_Prov_mensual = Convert.ToDecimal(2.50);
                            PPD.pland_vac_dias_acumulado = 0;

                            List<EProvisionPlanillaPersonal> LSTPROV = new List<EProvisionPlanillaPersonal>();
                            LSTPROV = new BPlanillas().listarProvisionPlanillaPersonal().Where(z => Convert.ToInt32(z.planc_iid_tipo_planilla) == 6436).ToList();
                            foreach (var PROV in LSTPROV)
                            {
                                DateTime FechaPROV = Convert.ToDateTime(("01-" + lkpMes.EditValue + "-" + (Parametros.intEjercicio) + "").ToString());
                                FechaPROV = FechaPROV.AddMonths(-1);
                                if (PROV.mesec_iid_mes == FechaPROV.Date.Month && PROV.planc_iid_anio == FechaPROV.Date.Year)
                                {
                                    List<EProvisionPlanillaPersonalDetalle> LSTPROVDET = new List<EProvisionPlanillaPersonalDetalle>();
                                    LSTPROVDET = new BPlanillas().listarProvisionPlanillaPersonalDetalle(PROV.planc_icod_planilla_personal);
                                    foreach (var PROVDET in LSTPROVDET)
                                    {
                                        if (PROVDET.pland_icod_personal == PPD.pland_icod_personal)
                                        {
                                            if ((Convert.ToInt32(lkpMes.EditValue) == 1))
                                            {
                                                PPD.pland_vac_dias_acumulado = Convert.ToDecimal(2.50);
                                            }
                                            else
                                            {
                                                //PPD.pland_nctsprovision_acumulada = PROVDET.pland_nctsprovision_acumulada + PROVDET.pland_nctsprovision_mes;
                                                PPD.pland_vac_dias_acumulado = Math.Round(Convert.ToDecimal(PPD.pland_vac_dias_Prov_mensual + PROVDET.pland_vac_dias_Prov_mensual), 2);
                                            }
                                        }
                                    }
                                }
                            }

                            PPD.pland_vac_provision_mes = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable / lstParametroPlanilla[0].prpc_ndias_trabajo) * PPD.pland_vac_dias_Prov_mensual)), 2);
                            PPD.pland_vac_ajuste_mes = 0;
                            PPD.pland_vac_prov_tot_mensual = PPD.pland_vac_provision_mes + PPD.pland_vac_ajuste_mes;




                        }

                        PPD.operacion = 1;

                        lstProvisionDetalle.Add(PPD);
                        grdVacaciones.RefreshDataSource();

                    }
                }
            }




        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipo.EditValue) == 6437)/*GRATIFICACION*/
            {
                EProvisionPlanillaPersonalDetalle obj = (EProvisionPlanillaPersonalDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                if (obj.operacion == 1)
                {
                    lstProvisionDetalle.Remove(obj);
                    viewDetalle.RefreshData();
                    viewDetalle.MovePrev();
                }
                else
                {

                    obj.operacion = 3;
                    lstProvisionDetalleEliminados.Add(obj);
                    lstProvisionDetalle.Remove(obj);
                    viewDetalle.RefreshData();
                    viewDetalle.MovePrev();

                }
            }
            else if (Convert.ToInt32(lkpTipo.EditValue) == 6438)/*CTS*/
            {
                EProvisionPlanillaPersonalDetalle obj = (EProvisionPlanillaPersonalDetalle)viewCTS.GetRow(viewCTS.FocusedRowHandle);
                if (obj.operacion == 1)
                {
                    lstProvisionDetalle.Remove(obj);
                    viewCTS.RefreshData();
                    viewCTS.MovePrev();
                }
                else
                {

                    obj.operacion = 3;
                    lstProvisionDetalleEliminados.Add(obj);
                    lstProvisionDetalle.Remove(obj);
                    viewCTS.RefreshData();
                    viewCTS.MovePrev();

                }
            }
            else if (Convert.ToInt32(lkpTipo.EditValue) == 6436)/*VACACIONES*/
            {
                EProvisionPlanillaPersonalDetalle obj = (EProvisionPlanillaPersonalDetalle)viewVacaciones.GetRow(viewVacaciones.FocusedRowHandle);
                if (obj.operacion == 1)
                {
                    lstProvisionDetalle.Remove(obj);
                    viewVacaciones.RefreshData();
                    viewVacaciones.MovePrev();
                }
                else
                {

                    obj.operacion = 3;
                    lstProvisionDetalleEliminados.Add(obj);
                    lstProvisionDetalle.Remove(obj);
                    viewVacaciones.RefreshData();
                    viewVacaciones.MovePrev();

                }
            }


        }

        private void cCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EProvisionPlanillaPersonalDetalle Obe = (EProvisionPlanillaPersonalDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmRegistroCCosto frm = new FrmRegistroCCosto();
            frm.IcodPersonal = Obe.pland_icod_personal;
            frm.anio = Parametros.intEjercicio;
            frm.mes = oBe.mesec_iid_mes;
            frm.Indicador = 1;
            frm.oBeCC = oBe;
            frm.Show();
        }

        private void lkpTipo_EditValueChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(lkpTipo.EditValue) == 6437)/*GRATIFICAION*/
            {
                grdDetalle.Visible = true;
                grdCTS.Visible = false;
                grdVacaciones.Visible = false;
            }
            else if (Convert.ToInt32(lkpTipo.EditValue) == 6438)/*CTS*/
            {
                grdDetalle.Visible = false;
                grdCTS.Visible = true;
                grdVacaciones.Visible = false;
            }
            else if (Convert.ToInt32(lkpTipo.EditValue) == 6436)/*VACACIONES*/
            {
                grdDetalle.Visible = false;
                grdCTS.Visible = false;
                grdVacaciones.Visible = true;
            }
        }

        private void viewCTS_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //**CTS**//
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.CreateNew)
            {

                EProvisionPlanillaPersonalDetalle Obe = (EProvisionPlanillaPersonalDetalle)viewCTS.GetRow(viewCTS.FocusedRowHandle);

                if (Obe != null)
                {
                    Obe.pland_nrem_computable = Obe.pland_rem_basica + Obe.pland_nasignacion_familiar;
                    Obe.pland_ncts_total = (Obe.pland_nrem_computable) + (Obe.pland_nctssexto_comision) + (Obe.pland_nctssexto_gratificacion);
                    Obe.pland_nctssexto_gratificacion = Convert.ToDecimal(Obe.pland_ncts_gratificacion) / 6;
                    Obe.pland_nctssexto_comision = Convert.ToDecimal(Obe.pland_ncts_comision) / 6;
                    Obe.pland_ncts_total = Convert.ToDecimal(Obe.pland_nrem_computable) + Obe.pland_nctssexto_comision + Obe.pland_nctssexto_gratificacion;
                    Obe.pland_ncts_meses_monto = ((Obe.pland_ncts_total / 12) * Obe.pland_icts_meses);
                    Obe.pland_ncts_dias_monto = ((Obe.pland_ncts_total / 360) * Obe.pland_icts_dias);
                    Obe.pland_ncts_por_mes = Obe.pland_ncts_meses_monto + Obe.pland_ncts_dias_monto;
                    //if (Convert.ToInt32(lkpMes.EditValue) == 1 && Parametros.intEjercicio == 2017)
                    //{

                    //}
                    //else
                    //{
                    //    Obe.pland_nctsprovision_acumulada = 0;
                    //    List<EProvisionPlanillaPersonal> LSTPROV = new List<EProvisionPlanillaPersonal>();
                    //    LSTPROV = new BPlanillas().listarProvisionPlanillaPersonal().Where(z => Convert.ToInt32(z.planc_iid_tipo_planilla) == 6438).ToList();
                    //    foreach (var PROV in LSTPROV)
                    //    {
                    //        DateTime FechaPROV = Convert.ToDateTime(("01-" + lkpMes.EditValue + "-" + (Parametros.intEjercicio) + "").ToString());
                    //        FechaPROV = FechaPROV.AddMonths(-1);
                    //        if (PROV.mesec_iid_mes == FechaPROV.Date.Month && PROV.planc_iid_anio == FechaPROV.Date.Year)
                    //        {
                    //            List<EProvisionPlanillaPersonalDetalle> LSTPROVDET = new List<EProvisionPlanillaPersonalDetalle>();
                    //            LSTPROVDET = new BPlanillas().listarProvisionPlanillaPersonalDetalle(PROV.planc_icod_planilla_personal);
                    //            foreach (var PROVDET in LSTPROVDET)
                    //            {
                    //                if (PROVDET.pland_icod_personal == Obe.pland_icod_personal)
                    //                {
                    //                    Obe.pland_nctsprovision_acumulada = PROVDET.pland_nctsprovision_acumulada + PROVDET.pland_nctsprovision_mes;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    Obe.pland_nctsprovision_mes = Math.Round(Convert.ToDecimal(Obe.pland_ncts_por_mes - Obe.pland_nctsprovision_acumulada), 2);
                }

                new BPlanillas().modificarProvisionPlanillaPersonalDetalle(Obe);
            }
        }

        private void calcularMontosCCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EProvisionPlanillaPersonalDetalle> lstProvisionDetalleCC = new List<EProvisionPlanillaPersonalDetalle>();
            lstProvisionDetalleCC = new BPlanillas().listarProvisionPlanillaPersonalDetalle(oBe.planc_icod_planilla_personal);
            /*Centro Costos*/
            List<EPersonalCCostos> lstcCostosPersonalCC = new List<EPersonalCCostos>();
            lstProvisionDetalleCC.ForEach(x =>
            {
                decimal Monto = 0;
                decimal MontoDividir = 0;
                decimal SumaMontos = 0;
                decimal DiferenciaMonto = 0;
                int count = 0;
                //lstcCostosPersonalCC = new BPlanillas().listarPersonalCCostos(x.pland_icod_personal, Parametros.intEjercicio, oBe.mesec_iid_mes);
                lstcCostosPersonalCC = new BPlanillas().listarPersonalCCostos(x.pland_icod_personal).Where(xc => xc.pccd_iaño == Parametros.intEjercicio && xc.pccd_imes == oBe.mesec_iid_mes).ToList();
                lstcCostosPersonalCC.ForEach(cc =>
                {
                    count++;
                    Monto = Convert.ToDecimal(x.pland_nmonto_gratificacion);
                    MontoDividir = Math.Round((Convert.ToDecimal(x.pland_nmonto_gratificacion) / lstcCostosPersonalCC.Count), 2, MidpointRounding.AwayFromZero);




                    if (count == lstcCostosPersonalCC.Count)
                    {
                        DiferenciaMonto = Monto - SumaMontos;
                        if (oBe.planc_iid_tipo_planilla == 6436)
                        {
                            cc.pccd_nmonto_vacaciones = DiferenciaMonto;
                            new BPlanillas().modificarPersonalCCostos(cc);
                        }
                        if (oBe.planc_iid_tipo_planilla == 6437)
                        {
                            cc.pccd_nmonto_gratificaciones = DiferenciaMonto;
                            new BPlanillas().modificarPersonalCCostos(cc);
                        }
                        if (oBe.planc_iid_tipo_planilla == 6438)
                        {
                            cc.pccd_nmonto_cts = DiferenciaMonto;
                            new BPlanillas().modificarPersonalCCostos(cc);
                        }
                    }
                    SumaMontos = SumaMontos + MontoDividir;
                    if (count == 1 || count != lstcCostosPersonalCC.Count)
                    {


                        if (oBe.planc_iid_tipo_planilla == 6436)
                        {
                            cc.pccd_nmonto_vacaciones = MontoDividir;
                            new BPlanillas().modificarPersonalCCostos(cc);
                        }
                        if (oBe.planc_iid_tipo_planilla == 6437)
                        {
                            cc.pccd_nmonto_gratificaciones = MontoDividir;
                            new BPlanillas().modificarPersonalCCostos(cc);
                        }
                        if (oBe.planc_iid_tipo_planilla == 6438)
                        {
                            cc.pccd_nmonto_cts = MontoDividir;
                            new BPlanillas().modificarPersonalCCostos(cc);
                        }

                    }


                });

            });
        }

        private void calcularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipo.EditValue) == 6437)//*GRATIFICACION*//
            {

            }
            else
            if (Convert.ToInt32(lkpTipo.EditValue) == 6438)//*CTS*//
            {
                foreach (var PPD in lstProvisionDetalle)
                {
                    PPD.pland_nrem_computable = PPD.pland_rem_basica + PPD.pland_nasignacion_familiar;
                    PPD.pland_ncts_total = (PPD.pland_nrem_computable) + (PPD.pland_nctssexto_comision) + (PPD.pland_nctssexto_gratificacion);
                    PPD.pland_nctssexto_gratificacion = Convert.ToDecimal(PPD.pland_ncts_gratificacion) / 6;
                    PPD.pland_nctssexto_comision = Convert.ToDecimal(PPD.pland_ncts_comision) / 6;
                    PPD.pland_ncts_total = Convert.ToDecimal(PPD.pland_nrem_computable) + PPD.pland_nctssexto_comision + PPD.pland_nctssexto_gratificacion;
                    PPD.pland_ncts_meses_monto = ((PPD.pland_ncts_total / 12) * PPD.pland_icts_meses);
                    PPD.pland_ncts_dias_monto = ((PPD.pland_ncts_total / 360) * PPD.pland_icts_dias);
                    PPD.pland_ncts_por_mes = PPD.pland_ncts_meses_monto + PPD.pland_ncts_dias_monto;
                    //if (Convert.ToInt32(lkpMes.EditValue) == 1 && Parametros.intEjercicio == 2017)
                    //{

                    //}
                    //else
                    //{
                    //    PPD.pland_nctsprovision_acumulada = 0;
                    //    List<EProvisionPlanillaPersonal> LSTPROV = new List<EProvisionPlanillaPersonal>();
                    //    LSTPROV = new BPlanillas().listarProvisionPlanillaPersonal().Where(z => Convert.ToInt32(z.planc_iid_tipo_planilla) == 6438).ToList();
                    //    foreach (var PROV in LSTPROV)
                    //    {
                    //        DateTime FechaPROV = Convert.ToDateTime(("01-" + lkpMes.EditValue + "-" + (Parametros.intEjercicio) + "").ToString());
                    //        FechaPROV = FechaPROV.AddMonths(-1);
                    //        if (PROV.mesec_iid_mes == FechaPROV.Date.Month && PROV.planc_iid_anio == FechaPROV.Date.Year)
                    //        {
                    //            List<EProvisionPlanillaPersonalDetalle> LSTPROVDET = new List<EProvisionPlanillaPersonalDetalle>();
                    //            LSTPROVDET = new BPlanillas().listarProvisionPlanillaPersonalDetalle(PROV.planc_icod_planilla_personal);
                    //            foreach (var PROVDET in LSTPROVDET)
                    //            {
                    //                if (PROVDET.pland_icod_personal == PPD.pland_icod_personal)
                    //                {
                    //                    PPD.pland_nctsprovision_acumulada = PROVDET.pland_nctsprovision_acumulada + PROVDET.pland_nctsprovision_mes;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    PPD.pland_nctsprovision_mes = Math.Round(Convert.ToDecimal(PPD.pland_ncts_por_mes - PPD.pland_nctsprovision_acumulada), 2);

                    viewCTS.RefreshData();
                    grdCTS.Refresh();
                    grdCTS.RefreshDataSource();

                    new BPlanillas().modificarProvisionPlanillaPersonalDetalle(PPD);
                }

            }
            else
            {

            }
        }

        private void viewDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //**GRATIFICACION**//
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.CreateNew)
            {

                EProvisionPlanillaPersonalDetalle Obe = (EProvisionPlanillaPersonalDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);

                if (Obe != null)
                {
                    List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                    lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();

                    Obe.pland_nrem_computable = Obe.pland_rem_basica + Obe.pland_nasignacion_familiar;
                    Obe.pland_nmonto_gratificacion = Math.Round(Convert.ToDecimal(Obe.pland_nrem_computable / 6) + Convert.ToDecimal(((Obe.pland_nrem_computable / 6) * contGratif) - (SumGratif)), 2);
                    if (Obe.pland_beps == true)
                    {
                        Obe.pland_nbonificacion_mes = Math.Round((Convert.ToDecimal(Obe.pland_nmonto_gratificacion) * Convert.ToDecimal(lstParametroPlanilla[0].prpc_ngratificacion_eps) / 100), 2);
                    }
                    else
                    {
                        Obe.pland_nbonificacion_mes = Math.Round((Convert.ToDecimal(Obe.pland_nmonto_gratificacion) * Convert.ToDecimal(lstParametroPlanilla[0].prpc_ngratificacion_essalud) / 100), 2);
                    }
                }

                new BPlanillas().modificarProvisionPlanillaPersonalDetalle(Obe);
            }
        }

        private void viewVacaciones_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            //**VACACIONES**//
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.CreateNew)
            {
                EProvisionPlanillaPersonalDetalle PPD = (EProvisionPlanillaPersonalDetalle)viewVacaciones.GetRow(viewVacaciones.FocusedRowHandle);
                if (PPD != null)
                {
                    List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                    lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();

                    PPD.pland_nrem_computable = PPD.pland_rem_basica + PPD.pland_nasignacion_familiar;
                    PPD.pland_vac_provision_mes = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable / lstParametroPlanilla[0].prpc_ndias_trabajo) * PPD.pland_vac_dias_Prov_mensual)), 2);
                    PPD.pland_vac_prov_tot_mensual = PPD.pland_vac_provision_mes + PPD.pland_vac_ajuste_mes;
                }
            }
        }

        private void duplicarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipo.EditValue) == 6438)/*CTS*/
            {
                EProvisionPlanillaPersonalDetalle frm = (EProvisionPlanillaPersonalDetalle)viewCTS.GetRow(viewCTS.FocusedRowHandle);



                int DiasporMes = 0;
                DiasporMes = System.DateTime.DaysInMonth(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
                EProvisionPlanillaPersonalDetalle PPD = new EProvisionPlanillaPersonalDetalle();
                List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();
                BPlanillas pers = new BPlanillas();
                int Existe = 0;

                foreach (var item in lstProvisionDetalle)
                {
                    if (item.pland_icod_personal == frm.pland_icod_personal)
                    {
                        XtraMessageBox.Show("EL Personal ya esta Registrado !!");
                        Existe = 1;
                    }
                }

                if (XtraMessageBox.Show("¿Esta seguro que desea Duplicar Planilla Personal  ?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    i2 = lstProvisionDetalle.Max(x => x.pland_iid_planilla_personal_det);
                    PPD.pland_iid_planilla_personal_det = i2 + 1;
                    PPD.pland_icod_personal = frm.pland_icod_personal;
                    PPD.pland_ape_nom = frm.pland_ape_nom;
                    PPD.pland_num_doc = frm.pland_num_doc;
                    PPD.pland_cussp = frm.pland_cussp;
                    PPD.pland_rem_basica = frm.pland_rem_basica;
                    PPD.intUsuario = Valores.intUsuario;
                    PPD.strPc = WindowsIdentity.GetCurrent().Name;
                    PPD.pland_flag_estado = true;
                    PPD.pland_beps = frm.pland_beps;
                    PPD.pland_sfecha_incio = (frm.pland_sfecha_cese).Value.AddDays(+1);
                    PPD.pland_sfecha_cese = null;
                    PPD.pland_basignacion_familiar = frm.pland_basignacion_familiar;
                    PPD.prpc_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                    List<EPersonal> lstpersonal = new List<EPersonal>();
                    lstpersonal = new BPlanillas().listarPersonal().Where(a => a.perc_icod_personal == PPD.pland_icod_personal).ToList();
                    foreach (var _bee in lstpersonal)
                    {
                        PPD.strEPS = _bee.strEPS;

                        if (_bee.perc_basig_familiar == true)
                        {
                            PPD.pland_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                        }
                        else
                        {
                            PPD.pland_nasignacion_familiar = 0;
                        }
                        ctsmes = 0;
                        if (Convert.ToInt32(lkpMes.EditValue) >= 5 && Convert.ToInt32(lkpMes.EditValue) <= 10)
                        {
                            switch (Convert.ToInt32(lkpMes.EditValue))
                            {
                                case 5:
                                    ctsmes = 1;
                                    break;
                                case 6:
                                    ctsmes = 2;
                                    break;
                                case 7:
                                    ctsmes = 3;
                                    break;
                                case 8:
                                    ctsmes = 4;
                                    break;
                                case 9:
                                    ctsmes = 5;
                                    break;
                                case 10:
                                    ctsmes = 6;
                                    break;

                            }
                        }
                        else
                        {

                            switch (Convert.ToInt32(lkpMes.EditValue))
                            {
                                case 11:
                                    ctsmes = 1;
                                    break;
                                case 12:
                                    ctsmes = 2;
                                    break;
                                case 1:
                                    ctsmes = 3;
                                    break;
                                case 2:
                                    ctsmes = 4;
                                    break;
                                case 3:
                                    ctsmes = 5;
                                    break;
                                case 4:
                                    ctsmes = 6;
                                    break;

                            }
                        }
                        PPD.pland_nrem_computable = PPD.pland_rem_basica + PPD.pland_nasignacion_familiar;
                        PPD.pland_ncts_horas_extras = 0;
                        PPD.pland_ncts_gratificacion = 0;
                        PPD.pland_nctssexto_gratificacion = 0;
                        PPD.pland_ncts_comision = 0;
                        PPD.pland_nctssexto_comision = 0;
                        PPD.pland_ncts_total = (PPD.pland_nrem_computable) + (PPD.pland_nctssexto_comision) + (PPD.pland_nctssexto_gratificacion);




                        PPD.pland_nctsprovision_acumulada = 0;
                        PPD.pland_icts_meses = 0;
                        PPD.pland_icts_dias = 0;
                        List<EProvisionPlanillaPersonal> LSTPROV = new List<EProvisionPlanillaPersonal>();
                        LSTPROV = new BPlanillas().listarProvisionPlanillaPersonal().Where(z => Convert.ToInt32(z.planc_iid_tipo_planilla) == 6438).ToList();
                        foreach (var PROV in LSTPROV)
                        {
                            DateTime FechaPROV = Convert.ToDateTime(("01-" + lkpMes.EditValue + "-" + (Parametros.intEjercicio) + "").ToString());
                            FechaPROV = FechaPROV.AddMonths(-1);
                            if (PROV.mesec_iid_mes == FechaPROV.Date.Month && PROV.planc_iid_anio == FechaPROV.Date.Year)
                            {
                                List<EProvisionPlanillaPersonalDetalle> LSTPROVDET = new List<EProvisionPlanillaPersonalDetalle>();
                                LSTPROVDET = new BPlanillas().listarProvisionPlanillaPersonalDetalle(PROV.planc_icod_planilla_personal);
                                foreach (var PROVDET in LSTPROVDET)
                                {
                                    if (PROVDET.pland_icod_personal == PPD.pland_icod_personal)
                                    {
                                        if ((Convert.ToInt32(lkpMes.EditValue) == 1 && Parametros.intEjercicio == 2017) || (PROVDET.pland_icts_meses == 0 || PROVDET.pland_icts_meses == null))
                                        {
                                            if (ctsdias > 0 && ctsdias != DiasporMes)
                                            {
                                                PPD.pland_icts_meses = ctsmes - 1;
                                            }
                                            else
                                            {
                                                PPD.pland_icts_meses = ctsmes;
                                                if (ctsdias == DiasporMes)
                                                {
                                                    ctsdias = 0;
                                                }
                                            }
                                            PPD.pland_icts_dias = ctsdias;
                                        }
                                        else
                                        {
                                            PPD.pland_ncts_gratificacion = 0;
                                            PPD.pland_nctssexto_gratificacion = 0;
                                            PPD.pland_ncts_total = (PPD.pland_nrem_computable) + (PPD.pland_nctssexto_comision) + (PPD.pland_nctssexto_gratificacion);
                                            PPD.pland_nctsprovision_acumulada = 0;
                                            PPD.pland_icts_meses = 0;
                                            PPD.pland_icts_dias = (DiasporMes - frm.pland_sfecha_cese.Value.Day) + 1;
                                        }
                                    }
                                }
                            }
                        }

                        PPD.pland_ncts_meses_monto = ((PPD.pland_ncts_total / 12) * PPD.pland_icts_meses); ;
                        PPD.pland_ncts_dias_monto = ((PPD.pland_ncts_total / 360) * PPD.pland_icts_dias);
                        PPD.pland_ncts_por_mes = (PPD.pland_ncts_meses_monto + PPD.pland_ncts_dias_monto);
                        PPD.pland_nctsprovision_mes = Math.Round(Convert.ToDecimal(PPD.pland_ncts_por_mes - PPD.pland_nctsprovision_acumulada), 2);
                    }

                    PPD.operacion = 1;

                    lstProvisionDetalle.Add(PPD);
                    grdCTS.RefreshDataSource();

                }


            }
        }

        private void viewCTS_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int DiasporMes = 0;
            DiasporMes = System.DateTime.DaysInMonth(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
            EProvisionPlanillaPersonalDetalle Obe = (EProvisionPlanillaPersonalDetalle)viewCTS.GetRow(viewCTS.FocusedRowHandle);

            if (Obe != null)
            {
                if (Obe.pland_sfecha_cese != null && (Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") <= Obe.pland_sfecha_cese && Obe.pland_sfecha_cese <= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "")))
                {
                    duplicarToolStripMenuItem.Visible = true;
                }
                else
                {
                    duplicarToolStripMenuItem.Visible = false;
                }
            }
        }








    }
}