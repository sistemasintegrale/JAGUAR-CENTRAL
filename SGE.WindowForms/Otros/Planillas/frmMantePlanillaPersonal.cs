using DevExpress.XtraEditors;
using SGE.BusinessLogic;
//using SGE.WindowForms.Planillas.Registro_de_Datos;
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

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmMantePlanillaPersonal : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMantePlanillaPersonal));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /*----------------------------------------------------*/
        public EPlanillaPersonal oBe = new EPlanillaPersonal();
        /*----------------------------------------------------*/
        List<EPlanillaPersonalDetalle> lstDetalle = new List<EPlanillaPersonalDetalle>();
        List<EPlanillaPersonalDetalle> lstDetalleEliminados = new List<EPlanillaPersonalDetalle>();
        /*----------------------------------------------------*/
        public int cese = 0;
        public int Contrato = 0;
        public int FechInic, FechaFin, SumFecha = 0; //---Para fecha de contrato
        public int ctsmes = 0, ctsdias = 0;
        public DateTime? fechaCese; public int EstFechaCese, SumaFechaCese = 0;
        public int i = 1;
        public int i2 = 0;
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
        public frmMantePlanillaPersonal()
        {
            InitializeComponent();
        }

        private void FrmManteNotaIngreso_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BPlanillas().listarTablaPlanillaDetalle(22), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpTipo, new BPlanillas().listarTablaPlanillaDetalle(23).Where(z => z.tbpd_icod_tabla_planilla_detalle == 6435).ToList(), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            Carga();
            lkpMes.EditValue = Convert.ToInt32(DateTime.Now.Month);
            lkpSituacion.EditValue = 6433;
            lkpTipo.EditValue = 6435;
            dteFecha.EditValue = DateTime.Now;


            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
            {

                lkpMes.EditValue = oBe.mesec_iid_mes;
                lkpSituacion.EditValue = oBe.tablc_iid_situacion_planilla;
                lkpTipo.EditValue = oBe.planc_iid_tipo_planilla;
            }



        }

        private void Carga()
        {

            if (Status != BSMaintenanceStatus.CreateNew)
            {

                if (oBe.planc_iid_tipo_planilla == 6435)
                {
                    lstDetalle = new BPlanillas().listarPlanillaPersonalDetalle(oBe.planc_icod_planilla_personal).OrderBy(z => z.pland_ape_nom).ToList();
                    grdCTS.DataSource = lstDetalle;
                }
            }


        }

        private void StatusControl()
        {
            //bool Enabled = (Status == BSMaintenanceStatus.View);  
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
                if (lstDetalle.Count == 0)
                {
                    throw new ArgumentException("Ingresar al menos un producto");
                }

                if (verificarCCostos(txtNumPlanilla.Text))
                {
                    oBase = lkpMes;
                    throw new ArgumentException("No se puede Registrar una Planilla Personal en el mismo Mes");
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
                    oBe.planc_icod_planilla_personal = new BPlanillas().insertarPlanillaPersonal(oBe, lstDetalle);
                    //oBe.planc_icod_planilla_personal = new BPlanillas().insertarPlanillaPersonal(oBe);
                }
                else
                {
                    //new BPlanillas().modificarPlanillaPersonal(oBe, lstDetalle, lstDelete);
                    new BPlanillas().modificarPlanillaPersonal(oBe, lstDetalle, lstDetalleEliminados);
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
        //private void imprimir()
        //{
        //    if (XtraMessageBox.Show("¿Desea imprimir la nota de ingreso?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
        //        return;
        //    var lstDetalle = new BAlmacen().listarNotaIngresoDetalle(oBe.ningc_icod_nota_ingreso);
        //    rptNotaIngreso rpt = new rptNotaIngreso();
        //    rpt.cargar(String.Format("NOTA DE INGRESO N° {0}", oBe.ningc_numero_nota_ingreso), oBe.strAlmacen, lstDetalle, oBe);

        //}



        private bool verificarCCostos(string planilla_detalle)
        {
            try
            {
                bool exists = false;
                List<EPlanillaPersonal> lstplanilla = new List<EPlanillaPersonal>();
                lstplanilla = new BPlanillas().listarPlanillaPersonal();
                if (lstplanilla.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstplanilla.Where(x => x.mesec_iid_mes == Convert.ToInt32(lkpMes.EditValue) && x.planc_iid_anio == Convert.ToInt32(Parametros.intEjercicio)).ToList().Count > 0)
                            exists = true;
                    }
                    //if (Status == BSMaintenanceStatus.ModifyCurrent)
                    //{
                    //    if (lstplanilla.Where(x =>  x.mesec_iid_mes != Convert.ToInt32(lkpMes.EditValue) && x.planc_iid_anio == Convert.ToInt32(Parametros.intEjercicio)).ToList().Count > 0)
                    //        exists = true;
                    //}
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            #region Remuneraciones
            if (Convert.ToInt32(lkpTipo.EditValue) == 6435)
            {
                int DiasporMes = 0;
                DiasporMes = System.DateTime.DaysInMonth(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
                List<EPersonal> lstPersonal = new List<EPersonal>();
                lstPersonal = new BPlanillas().listarPersonal().OrderBy(s => s.ApellNomb).ToList();

                if (lstDetalle.Count > 0)
                {
                    lstDetalle.Clear();
                }
                foreach (var _bee in lstPersonal)
                {
                    EPlanillaPersonalDetalle PPD = new EPlanillaPersonalDetalle();
                    List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                    lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();
                    //******
                    int num = 1;
                    cese = 0;
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
                        num = num++;

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


                        fechaCese = item.pctd_sfecha_cese;
                        //if ( num==lstContrato.Count)
                        //{
                        //    if (fechaCese < Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        //{
                        //    cese = 1;
                        //} 
                        //}




                        if (item.pctd_sfecha_cese >= Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + "") && item.pctd_sfecha_cese <= Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""))
                        {
                            ctsdias = item.pctd_sfecha_cese.Value.Day;

                        }
                        else
                        {
                            ctsdias = 0;
                        }

                        //DateTime? Fecha1, Fecha2, FechaA, FechaB, FechaII, FechaFF;


                        //            Fecha1=item.pctd_sfecha_ini_contrato;
                        //            Fecha2=item.pctd_sfecha_fin_contrato;
                        //            FechaA=Convert.ToDateTime("01-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""); //--El primero de Enero 2013

                        //            FechaB=Convert.ToDateTime("" + DiasporMes + "-" + lkpMes.EditValue + "-" + Parametros.intEjercicio + ""); //--El ultimo de Enero 2013



                        //if (Fecha1>FechaA)
                        //    {
                        //    FechaII=Fecha1;
                        //    }else
                        //    {
                        //    FechaII=FechaA;
                        //    }

                        //if (Fecha2>FechaB)
                        //    {
                        //    FechaFF=FechaB;
                        //    }else
                        //    {
                        //    FechaFF=Fecha2;
                        //    }
                        //TimeSpan? RESULTdia = (FechaII - FechaFF);



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
                        PPD.pland_sueldo_basico = Convert.ToDecimal(_bee.perc_nmont_basico);
                        PPD.intUsuario = Valores.intUsuario;
                        PPD.strPc = WindowsIdentity.GetCurrent().Name;
                        PPD.pland_flag_estado = true;
                        PPD.pland_sfecha_incio = _bee.perc_sfech_inicio;
                        PPD.pland_sfecha_cese = fechaCese;

                        if (_bee.perc_icod_afp != 0)
                        {
                            List<EFondosPensiones> lstfondo_pensiones = new List<EFondosPensiones>();
                            lstfondo_pensiones = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == _bee.perc_icod_afp && x.fdpc_icod_fondo_pension != 0).ToList();
                            PPD.str_fondo_pension = "" + lstfondo_pensiones[0].fdpc_vdescripcion + "";
                        }
                        else
                        {
                            PPD.str_fondo_pension = "";
                        }


                        if (_bee.perc_basig_familiar == true)
                        {
                            PPD.pland_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                        }
                        else
                        {
                            PPD.pland_nasignacion_familiar = 0;
                        }
                        /**pland_reg_pension**/
                        if (_bee.perc_icod_tip_fdo_pension == 6384)
                        {
                            PPD.pland_reg_pension = 6384;
                            PPD.str_reg_pension = "AFP";
                        }
                        else
                        {
                            PPD.pland_reg_pension = 6385;
                            PPD.str_reg_pension = "ONP";
                        }

                        /**pland_comision**/
                        if (_bee.perc_icod_tip_comision == 6386)
                        {
                            PPD.pland_comision = 6386;
                            PPD.str_comision = "FIJA";
                        }
                        else if (_bee.perc_icod_tip_comision == 6387)
                        {
                            PPD.pland_comision = 6387;
                            PPD.str_comision = "MIXTA";
                        }
                        else
                        {
                            PPD.pland_comision = null;
                            PPD.str_comision = "";
                        }

                        PPD.pland_cargo = _bee.tablc_iid_tipo_cargo;
                        PPD.str_cargo = "" + _bee.strCargo + "";
                        PPD.pland_hijo = Convert.ToDecimal(_bee.perc_basig_familiar);
                        if (PPD.pland_hijo == 1)
                        {
                            PPD.str_hijo = "SI";
                        }
                        else
                        {
                            PPD.str_hijo = "NO";
                        }

                        PPD.pland_dias = 30;
                        PPD.pland_faltas = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_vacaciones = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_descanso_medico = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_dias_subsidios = 0;/**Datos k ingresa el usuario**/
                        //PPD.pland_dias_efectivos = PPD.pland_dias - (PPD.pland_faltas + PPD.pland_vacaciones + PPD.pland_descanso_medico + PPD.pland_dias_subsidios);
                        //PPD.pland_rem_basica =Math.Round( Convert.ToDecimal((PPD.pland_sueldo_basico / PPD.pland_dias) * PPD.pland_dias_efectivos),2);

                        //PPD.pland_nmonto_vacaciones = 0;

                        PPD.pland_nhoras_25 = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_nhoras_35 = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_nferiado_descanso = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_notros_ingresos = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_nsubsidios_essalud = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_ncomision_venta = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_ncomision_eventual = 0;/**Datos k ingresa el usuario**/
                        if (_bee.perc_nasig_transporte == 0 || _bee.perc_nasig_transporte == null)
                        {
                            PPD.pland_nasignacion_transporte = 0;
                        }
                        else
                        {
                            PPD.pland_nasignacion_transporte = _bee.perc_nasig_transporte;
                        }
                        /**Datos k ingresa el usuario**/
                        PPD.pland_nvales_alimentos = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_nadelanto_sueldo = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_ngratif_afecto = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_nbonif_afecto = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_nvacaciones_truncas = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_ngratif_no_afecto = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_nbonif_no_afecto = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_nCTS = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_nutilidades = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_ninasistencias = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_ntardanzas = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_npago_utilid = 0;/**Datos k ingresa el usuario**/



                        PPD.pland_desc_renta5 = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_desc_adelanto = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_desc_prestamo = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_desc_eps = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_desc_otros_desc_no_afect = 0;/**Datos k ingresa el usuario**/

                        PPD.pland_nutilidad_convencional = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_npago_utilidad_convencional = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_desc_aporte_c_prov = 0;/**Datos k ingresa el usuario**/
                        PPD.pland_desc_aporte_s_prov = 0;/**Datos k ingresa el usuario**/
                        PPD.rmcn_aporte_c_prov = 0;/**Datos k ingresa el usuario**/
                        PPD.rmcn_aporte_s_prov = 0;/**Datos k ingresa el usuario**/

                        PPD.pland_desc_otros_desc_afect = 0;
                        PPD.pland_desc_total_desc = 0;
                        PPD.pland_total_neto_pagar = 0;
                        PPD.rmcn_remun_computable = 0;

                        PPD.pland_dias_efectivos = PPD.pland_dias - (PPD.pland_faltas + PPD.pland_vacaciones + PPD.pland_descanso_medico + PPD.pland_dias_subsidios);
                        if (PPD.pland_faltas > 0)
                        {
                            PPD.pland_rem_basica = PPD.pland_sueldo_basico;
                        }
                        else
                        {
                            PPD.pland_rem_basica = Math.Round(Convert.ToDecimal((PPD.pland_sueldo_basico / lstParametroPlanilla[0].prpc_ndias_trabajo) * (PPD.pland_dias_efectivos + PPD.pland_descanso_medico)), 2);
                        }
                        PPD.pland_nmonto_vacaciones = Math.Round(Convert.ToDecimal((PPD.pland_sueldo_basico / PPD.pland_dias) * PPD.pland_vacaciones), 2);

                        PPD.pland_nremun_bruta = Convert.ToDecimal(PPD.pland_rem_basica + PPD.pland_nmonto_vacaciones + PPD.pland_nasignacion_familiar +
                            PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud + PPD.pland_ncomision_venta + PPD.pland_ncomision_eventual +
                            PPD.pland_nasignacion_transporte + PPD.pland_nvales_alimentos + PPD.pland_nadelanto_sueldo + PPD.pland_ngratif_afecto + PPD.pland_nbonif_afecto + PPD.pland_nvacaciones_truncas + PPD.pland_ngratif_no_afecto +
                            PPD.pland_nbonif_no_afecto + PPD.pland_nCTS + PPD.pland_nutilidades + PPD.pland_nutilidad_convencional);

                        PPD.pland_nremun_computable_neta = Math.Round(Convert.ToDecimal((PPD.pland_rem_basica + PPD.pland_nmonto_vacaciones + PPD.pland_nasignacion_familiar +
                            PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud +
                            PPD.pland_ncomision_venta + PPD.pland_ncomision_eventual) + PPD.pland_nvacaciones_truncas) - (Convert.ToDecimal(PPD.pland_ninasistencias + PPD.pland_ntardanzas + PPD.pland_npago_utilid)), 2);


                        PPD.pland_nrem_computable = Math.Round(Convert.ToDecimal((PPD.pland_rem_basica + PPD.pland_nmonto_vacaciones + PPD.pland_nasignacion_familiar +
                            PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud +
                            PPD.pland_ncomision_venta + PPD.pland_ncomision_eventual) + PPD.pland_nvacaciones_truncas), 2);



                        if (_bee.perc_icod_tip_fdo_pension == 6384)/**AFP*/
                        {

                            PPD.pland_desc_onp = 0;
                            if (_bee.perc_icod_tip_comision == 6386)/**FIJA*/
                            {

                                if (_bee.perc_icod_afp == 1)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(1).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {

                                            if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 2)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(2).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "04")
                                        {
                                            PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 3)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(3).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(4).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }


                                }
                            }
                            else /**MIXTA*/
                            {
                                if (_bee.perc_icod_afp == 1)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(1);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }


                                }
                                else if (_bee.perc_icod_afp == 2)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(2);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 3)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(3);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(4);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        else if (_bee.perc_icod_tip_fdo_pension == 6385)/**ONP*/
                        {
                            List<EFondosPensiones> lstfondoPension = new List<EFondosPensiones>();
                            lstfondoPension = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == 6).ToList();
                            PPD.pland_desc_onp = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * lstfondoPension[0].fdpc_nporcentaje_fijo) / 100), 2);
                            PPD.pland_desc_fondo = 0;
                            PPD.pland_desc_comision = 0;
                            PPD.pland_desc_seguro = 0;
                        }

                        PPD.pland_desc_tot_afp = PPD.pland_desc_fondo + PPD.pland_desc_comision + PPD.pland_desc_seguro + PPD.pland_desc_aporte_c_prov + PPD.pland_desc_aporte_s_prov;
                        PPD.pland_desc_retenc_judicial = Math.Round(Convert.ToDecimal(((PPD.pland_nutilidades + PPD.pland_nrem_computable - (PPD.pland_desc_tot_afp + PPD.pland_desc_renta5 + PPD.pland_desc_onp)) * _bee.perc_retenc_judicial) / 100), 2);

                        PPD.pland_desc_total_desc = PPD.pland_desc_tot_afp + PPD.pland_desc_renta5 + PPD.pland_desc_adelanto + PPD.pland_desc_prestamo + PPD.pland_desc_eps + PPD.pland_desc_otros_desc_no_afect + PPD.pland_desc_onp + PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas + PPD.pland_npago_utilid + PPD.pland_npago_utilidad_convencional;



                        if (PPD.pland_nsubsidios_essalud > 0)
                        {
                            PPD.pland_aport_essalud9 = Math.Round(Convert.ToDecimal((((PPD.pland_rem_basica + PPD.pland_vacaciones + PPD.pland_nasignacion_familiar + PPD.pland_nhoras_25 +
                                PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos) - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * lstParametroPlanilla[0].prpc_nporc_essalud) / 100), 2);
                        }
                        else if (PPD.pland_nsubsidios_essalud == 0 && Convert.ToDecimal((PPD.pland_nrem_computable - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas))) > lstParametroPlanilla[0].prpc_nsueldo_minimo)
                        {
                            PPD.pland_aport_essalud9 = Math.Round(Convert.ToDecimal((((PPD.pland_nrem_computable - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas))) * lstParametroPlanilla[0].prpc_nporc_essalud) / 100), 2);
                        }
                        else
                        {
                            PPD.pland_aport_essalud9 = Math.Round(Convert.ToDecimal((lstParametroPlanilla[0].prpc_nsueldo_minimo * lstParametroPlanilla[0].prpc_nporc_essalud) / 100), 2);
                        }
                        if (_bee.perc_beps == true)
                        {
                            PPD.pland_aport_eps_pacifico = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * lstParametroPlanilla[0].prpc_nporc_eps_pacifico) / 100), 2);
                            PPD.pland_aport_essalud = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * lstParametroPlanilla[0].prpc_nporc_eps_essalud) / 100), 2);
                        }
                        else
                        {
                            PPD.pland_aport_eps_pacifico = 0;
                            PPD.pland_aport_essalud = 0;
                        }

                        PPD.pland_total_neto_pagar = (PPD.pland_nremun_bruta - PPD.pland_desc_total_desc) - PPD.pland_desc_retenc_judicial;

                        #region Vacaciones Contable
                        PPD.vccn_nvacaciones = PPD.pland_nmonto_vacaciones;
                        if (_bee.perc_icod_tip_fdo_pension == 6384)/**AFP*/
                        {
                            PPD.vccn_nopn = 0;

                            if (_bee.perc_icod_tip_comision == 6386)/**FIJA*/
                            {

                                if (_bee.perc_icod_afp == 1)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(1).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {

                                            if (PPD.vccn_nvacaciones >= item.fdpd_ntope_concpeto)
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 2)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(2).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "04")
                                        {
                                            PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if (PPD.vccn_nvacaciones >= item.fdpd_ntope_concpeto)
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 3)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(3).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if (PPD.vccn_nvacaciones >= item.fdpd_ntope_concpeto)
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(4).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if (PPD.vccn_nvacaciones >= item.fdpd_ntope_concpeto)
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }


                                }
                            }
                            else /**MIXTA*/
                            {
                                if (_bee.perc_icod_afp == 1)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(1);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if (PPD.vccn_nvacaciones >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }


                                }
                                else if (_bee.perc_icod_afp == 2)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(2);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            if (PPD.vccn_nvacaciones >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 3)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(3);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if (PPD.vccn_nvacaciones >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(4);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if (PPD.vccn_nvacaciones >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        else if (_bee.perc_icod_tip_fdo_pension == 6385)/**ONP*/
                        {
                            List<EFondosPensiones> lstfondoPension = new List<EFondosPensiones>();
                            lstfondoPension = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == 6).ToList();

                            PPD.vccn_nfondo = 0;
                            PPD.vccn_ncomision = 0;
                            PPD.vccn_nseguro = 0;
                            PPD.vccn_nopn = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * lstfondoPension[0].fdpc_nporcentaje_fijo) / 100), 2);
                        }

                        PPD.vccn_ntotal_afp = PPD.vccn_nfondo + PPD.vccn_ncomision + PPD.vccn_nseguro;
                        if (PPD.pland_nmonto_vacaciones > ((PPD.pland_rem_basica + PPD.pland_nasignacion_familiar + PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud) - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)))
                        {
                            PPD.vccn_nrenta5 = PPD.pland_desc_renta5;
                        }
                        else
                        {
                            PPD.vccn_nrenta5 = 0;
                        }

                        if (PPD.pland_nmonto_vacaciones > ((PPD.pland_rem_basica + PPD.pland_nasignacion_familiar + PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud) - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)))
                        {
                            PPD.vccn_notros_desc = PPD.pland_desc_adelanto + PPD.pland_desc_prestamo + PPD.pland_desc_eps + PPD.pland_desc_otros_desc_no_afect;
                        }
                        else
                        {
                            PPD.vccn_notros_desc = 0;
                        }

                        PPD.vccn_nessalud = Math.Round(Convert.ToDecimal((PPD.pland_nmonto_vacaciones * lstParametroPlanilla[0].prpc_ngratificacion_essalud) / 100), 2);
                        PPD.vccn_nvacaciones_neto = Math.Round(Convert.ToDecimal(PPD.vccn_nvacaciones - (PPD.vccn_ntotal_afp + PPD.vccn_nopn + PPD.vccn_nrenta5 + PPD.vccn_notros_desc)), 2);
                        #endregion

                        #region Remuneraciones Contable

                        PPD.rmcn_remun_computable = (((PPD.pland_rem_basica + PPD.pland_nasignacion_familiar + PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud) - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)));

                        if (PPD.pland_dias_subsidios >= 30)
                        {
                            PPD.rmcn_onp = 0;
                            PPD.rmcn_fondo = 0;
                            PPD.rmcn_comision = 0;
                            PPD.rmcn_seguro = 0;
                            PPD.rmcn_total_afp = 0;
                            PPD.rmcn_rta_5ta = 0;
                            PPD.rmcn_otros_dstos = 0;
                            PPD.rmcn_reten_judicial = 0;
                            PPD.rmcn_essalud = 0;
                        }
                        else
                        {
                            if (_bee.perc_icod_tip_fdo_pension == 6384)/**AFP*/
                            {
                                PPD.rmcn_onp = 0;

                                if (_bee.perc_icod_tip_comision == 6386)/**FIJA*/
                                {

                                    if (_bee.perc_icod_afp == 1)
                                    {
                                        List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                        lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(1).ToList();
                                        foreach (var item in lstfondoPensionConceptos)
                                        {
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                            {
                                                PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                            {
                                                PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                            {

                                                if (PPD.rmcn_remun_computable >= item.fdpd_ntope_concpeto)
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }

                                            }
                                        }

                                    }
                                    else if (_bee.perc_icod_afp == 2)
                                    {
                                        List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                        lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(2).ToList();
                                        foreach (var item in lstfondoPensionConceptos)
                                        {
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "04")
                                            {
                                                PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                            {
                                                PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                            {
                                                if (PPD.rmcn_remun_computable >= item.fdpd_ntope_concpeto)
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                    else if (_bee.perc_icod_afp == 3)
                                    {
                                        List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                        lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(3).ToList();
                                        foreach (var item in lstfondoPensionConceptos)
                                        {
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                            {
                                                PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                            {
                                                PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                            {
                                                if (PPD.rmcn_remun_computable >= item.fdpd_ntope_concpeto)
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                        lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(4).ToList();
                                        foreach (var item in lstfondoPensionConceptos)
                                        {
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                            {
                                                PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                            {
                                                PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                            {
                                                if (PPD.rmcn_remun_computable >= item.fdpd_ntope_concpeto)
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                            }
                                        }


                                    }
                                }
                                else /**MIXTA*/
                                {
                                    if (_bee.perc_icod_afp == 1)
                                    {
                                        List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                        lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(1);
                                        foreach (var item in lstfondoPensionConceptosMixtas)
                                        {
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                            {
                                                PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                            {
                                                PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                            {
                                                if (PPD.rmcn_remun_computable >= item.fdpd2_ntope_concepto_mixto)
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                            }
                                        }


                                    }
                                    else if (_bee.perc_icod_afp == 2)
                                    {
                                        List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                        lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(2);
                                        foreach (var item in lstfondoPensionConceptosMixtas)
                                        {
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                            {
                                                PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                            {
                                                PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                            {
                                                if (PPD.rmcn_remun_computable >= item.fdpd2_ntope_concepto_mixto)
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                    else if (_bee.perc_icod_afp == 3)
                                    {
                                        List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                        lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(3);
                                        foreach (var item in lstfondoPensionConceptosMixtas)
                                        {
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                            {
                                                PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                            {
                                                PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                            {
                                                if (PPD.rmcn_remun_computable >= item.fdpd2_ntope_concepto_mixto)
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                        lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(4);
                                        foreach (var item in lstfondoPensionConceptosMixtas)
                                        {
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                            {
                                                PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                            {
                                                PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                            {
                                                if (PPD.rmcn_remun_computable >= item.fdpd2_ntope_concepto_mixto)
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                            else if (_bee.perc_icod_tip_fdo_pension == 6385)/**ONP*/
                            {
                                List<EFondosPensiones> lstfondoPension = new List<EFondosPensiones>();
                                lstfondoPension = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == 6).ToList();

                                PPD.rmcn_fondo = 0;
                                PPD.rmcn_comision = 0;
                                PPD.rmcn_seguro = 0;
                                PPD.rmcn_onp = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * lstfondoPension[0].fdpc_nporcentaje_fijo) / 100), 2);
                            }

                            PPD.rmcn_aporte_c_prov = PPD.pland_desc_aporte_c_prov;
                            PPD.rmcn_aporte_s_prov = PPD.pland_desc_aporte_s_prov;

                            PPD.rmcn_total_afp = PPD.rmcn_fondo + PPD.rmcn_comision + PPD.rmcn_seguro + PPD.rmcn_aporte_c_prov + PPD.rmcn_aporte_s_prov;

                            if (((PPD.pland_rem_basica + PPD.pland_nasignacion_familiar + PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos) - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= PPD.pland_nmonto_vacaciones)
                            {
                                PPD.rmcn_rta_5ta = PPD.pland_desc_renta5;
                            }
                            else
                            {
                                PPD.rmcn_rta_5ta = 0;
                            }

                            if (PPD.rmcn_remun_computable >= PPD.pland_nmonto_vacaciones)
                            {
                                PPD.rmcn_otros_dstos = PPD.pland_desc_adelanto + PPD.pland_desc_prestamo + PPD.pland_desc_eps + PPD.pland_desc_otros_desc_no_afect;
                            }
                            else
                            {
                                PPD.rmcn_otros_dstos = 0;
                            }

                            if (PPD.rmcn_remun_computable > PPD.vccn_nvacaciones)
                            {
                                PPD.rmcn_reten_judicial = PPD.pland_desc_retenc_judicial;
                            }
                            else
                            {
                                PPD.rmcn_reten_judicial = 0;
                            }

                            if (PPD.rmcn_remun_computable < lstParametroPlanilla[0].prpc_nsueldo_minimo)
                            {
                                PPD.rmcn_essalud = Math.Round(Convert.ToDecimal((lstParametroPlanilla[0].prpc_nsueldo_minimo * lstParametroPlanilla[0].prpc_ngratificacion_essalud) / 100), 2);
                            }
                            else
                            {
                                PPD.rmcn_essalud = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * lstParametroPlanilla[0].prpc_ngratificacion_essalud) / 100), 2);
                            }
                        }

                        PPD.rmcn_remun_neto = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable + PPD.pland_nvales_alimentos + PPD.pland_nasignacion_transporte) - (PPD.rmcn_total_afp + PPD.rmcn_onp + PPD.rmcn_rta_5ta + PPD.rmcn_otros_dstos + PPD.rmcn_reten_judicial)), 2);

                        #endregion

                        lstDetalle.Add(PPD);
                        //---------

                    }
                }
                grdCTS.DataSource = lstDetalle;
                grdCTS.RefreshDataSource();
                btnBuscar.Enabled = false;
                lkpMes.Enabled = false;
                lkpTipo.Enabled = false;
            }
            #endregion

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {


            using (frmManteAgregarPlanillaPersonal frm = new frmManteAgregarPlanillaPersonal())
            {
                //frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    EPlanillaPersonalDetalle PPD = new EPlanillaPersonalDetalle();
                    List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                    lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();
                    BPlanillas pers = new BPlanillas();
                    int Existe = 0;
                    //if (lstDetalle[0].pland_icod_personal == frm.icod_personal)                    
                    //    XtraMessageBox.Show("EL Personal ya esta Registrado !!");
                    foreach (var item in lstDetalle)
                    {
                        if (item.pland_icod_personal == frm.icod_personal)
                        {
                            XtraMessageBox.Show("EL Personal ya esta Registrado !!");
                            Existe = 1;
                        }
                    }


                    if (Existe == 0)
                    {
                        List<EPersonal> lstPersonal = new List<EPersonal>();
                        lstPersonal = new BPlanillas().listarPersonal().Where(a => a.perc_icod_personal == frm.icod_personal).ToList();
                        foreach (var _bee in lstPersonal)
                        {


                            i2 = lstDetalle.Max(x => x.pland_iid_planilla_personal_det);
                            PPD.pland_iid_planilla_personal_det = i++;
                            PPD.pland_icod_personal = _bee.perc_icod_personal;
                            PPD.pland_ape_nom = _bee.ApellNomb;
                            PPD.pland_num_doc = _bee.perc_vnum_doc;
                            PPD.pland_cussp = _bee.perc_vcuspp;
                            PPD.pland_sueldo_basico = Convert.ToDecimal(_bee.perc_nmont_basico);
                            PPD.intUsuario = Valores.intUsuario;
                            PPD.strPc = WindowsIdentity.GetCurrent().Name;
                            PPD.pland_flag_estado = true;
                            PPD.pland_sfecha_incio = _bee.perc_sfech_inicio;
                            PPD.pland_sfecha_cese = fechaCese;

                            if (_bee.perc_icod_afp != 0)
                            {
                                List<EFondosPensiones> lstfondo_pensiones = new List<EFondosPensiones>();
                                lstfondo_pensiones = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == _bee.perc_icod_afp && x.fdpc_icod_fondo_pension != 0).ToList();
                                PPD.str_fondo_pension = "" + lstfondo_pensiones[0].fdpc_vdescripcion + "";
                            }
                            else
                            {
                                PPD.str_fondo_pension = "";
                            }


                            if (_bee.perc_basig_familiar == true)
                            {
                                PPD.pland_nasignacion_familiar = lstParametroPlanilla[0].prpc_nasignacion_familiar;
                            }
                            else
                            {
                                PPD.pland_nasignacion_familiar = 0;
                            }
                            /**pland_reg_pension**/
                            if (_bee.perc_icod_tip_fdo_pension == 6384)
                            {
                                PPD.pland_reg_pension = 6384;
                                PPD.str_reg_pension = "AFP";
                            }
                            else
                            {
                                PPD.pland_reg_pension = 6385;
                                PPD.str_reg_pension = "ONP";
                            }

                            /**pland_comision**/
                            if (_bee.perc_icod_tip_comision == 6386)
                            {
                                PPD.pland_comision = 6386;
                                PPD.str_comision = "FIJA";
                            }
                            else if (_bee.perc_icod_tip_comision == 6387)
                            {
                                PPD.pland_comision = 6387;
                                PPD.str_comision = "MIXTA";
                            }
                            else
                            {
                                PPD.pland_comision = null;
                                PPD.str_comision = "";
                            }

                            PPD.pland_cargo = _bee.tablc_iid_tipo_cargo;
                            PPD.str_cargo = "" + _bee.strCargo + "";
                            PPD.pland_hijo = Convert.ToDecimal(_bee.perc_basig_familiar);
                            if (PPD.pland_hijo == 1)
                            {
                                PPD.str_hijo = "SI";
                            }
                            else
                            {
                                PPD.str_hijo = "NO";
                            }

                            PPD.pland_dias = 30;
                            PPD.pland_faltas = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_vacaciones = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_descanso_medico = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_dias_subsidios = 0;/**Datos k ingresa el usuario**/
                            //PPD.pland_dias_efectivos = PPD.pland_dias - (PPD.pland_faltas + PPD.pland_vacaciones + PPD.pland_descanso_medico + PPD.pland_dias_subsidios);
                            //PPD.pland_rem_basica =Math.Round( Convert.ToDecimal((PPD.pland_sueldo_basico / PPD.pland_dias) * PPD.pland_dias_efectivos),2);

                            //PPD.pland_nmonto_vacaciones = 0;

                            PPD.pland_nhoras_25 = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_nhoras_35 = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_nferiado_descanso = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_notros_ingresos = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_nsubsidios_essalud = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_ncomision_venta = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_ncomision_eventual = 0;/**Datos k ingresa el usuario**/
                            if (_bee.perc_nasig_transporte == 0 || _bee.perc_nasig_transporte == null)
                            {
                                PPD.pland_nasignacion_transporte = 0;
                            }
                            else
                            {
                                PPD.pland_nasignacion_transporte = _bee.perc_nasig_transporte;
                            }
                            /**Datos k ingresa el usuario**/
                            PPD.pland_nvales_alimentos = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_nadelanto_sueldo = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_ngratif_afecto = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_nbonif_afecto = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_nvacaciones_truncas = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_ngratif_no_afecto = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_nbonif_no_afecto = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_nCTS = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_nutilidades = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_ninasistencias = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_ntardanzas = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_npago_utilid = 0;/**Datos k ingresa el usuario**/

                            //PPD.pland_nremun_bruta  = 0 ;
                            //PPD.pland_nremun_computable_neta  = 0 ;
                            //PPD.pland_nrem_computable = 0;

                            PPD.pland_desc_renta5 = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_desc_adelanto = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_desc_prestamo = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_desc_eps = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_desc_otros_desc_no_afect = 0;/**Datos k ingresa el usuario**/
                            //PPD.pland_desc_retenc_judicial = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable-(PPD.pland_desc_tot_afp+PPD.pland_desc_renta5+PPD.pland_desc_onp))*_bee.perc_retenc_judicial)/100),2);/**Datos k ingresa el usuario**/
                            PPD.pland_desc_otros_desc_afect = 0;/**Datos k ingresa el usuario**/
                            PPD.pland_nutilidad_convencional = 0;
                            PPD.pland_npago_utilidad_convencional = 0;
                            PPD.pland_desc_aporte_c_prov = 0;
                            PPD.pland_desc_aporte_s_prov = 0;

                            PPD.pland_dias_efectivos = PPD.pland_dias - (PPD.pland_faltas + PPD.pland_vacaciones + PPD.pland_descanso_medico + PPD.pland_dias_subsidios);
                            if (PPD.pland_faltas > 0)
                            {
                                PPD.pland_rem_basica = PPD.pland_sueldo_basico;
                            }
                            else
                            {
                                PPD.pland_rem_basica = Math.Round(Convert.ToDecimal((PPD.pland_sueldo_basico / lstParametroPlanilla[0].prpc_ndias_trabajo) * (PPD.pland_dias_efectivos + PPD.pland_descanso_medico)), 2);
                            }

                            PPD.pland_nmonto_vacaciones = Math.Round(Convert.ToDecimal((PPD.pland_sueldo_basico / PPD.pland_dias) * PPD.pland_vacaciones), 2);

                            PPD.pland_nremun_bruta = Convert.ToDecimal(PPD.pland_rem_basica + PPD.pland_nmonto_vacaciones + PPD.pland_nasignacion_familiar +
                                PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud + PPD.pland_ncomision_venta + PPD.pland_ncomision_eventual +
                                PPD.pland_nasignacion_transporte + PPD.pland_nvales_alimentos + PPD.pland_nadelanto_sueldo + PPD.pland_ngratif_afecto + PPD.pland_nbonif_afecto + PPD.pland_nvacaciones_truncas + PPD.pland_ngratif_no_afecto +
                                PPD.pland_nbonif_no_afecto + PPD.pland_nCTS + PPD.pland_nutilidades + PPD.pland_nutilidad_convencional);

                            PPD.pland_nremun_computable_neta = Math.Round(Convert.ToDecimal((PPD.pland_rem_basica + PPD.pland_nmonto_vacaciones + PPD.pland_nasignacion_familiar +
                                PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud +
                                PPD.pland_ncomision_venta + PPD.pland_ncomision_eventual) + PPD.pland_nvacaciones_truncas) - (Convert.ToDecimal(PPD.pland_ninasistencias + PPD.pland_ntardanzas + PPD.pland_npago_utilid)), 2);


                            PPD.pland_nrem_computable = Math.Round(Convert.ToDecimal((PPD.pland_rem_basica + PPD.pland_nmonto_vacaciones + PPD.pland_nasignacion_familiar +
                                PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud +
                                PPD.pland_ncomision_venta + PPD.pland_ncomision_eventual) + PPD.pland_nvacaciones_truncas), 2);



                            if (_bee.perc_icod_tip_fdo_pension == 6384)/**AFP*/
                            {

                                PPD.pland_desc_onp = 0;
                                if (_bee.perc_icod_tip_comision == 6386)/**FIJA*/
                                {

                                    if (_bee.perc_icod_afp == 1)
                                    {
                                        List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                        lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(1).ToList();
                                        foreach (var item in lstfondoPensionConceptos)
                                        {
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                            {
                                                PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                            {
                                                PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                            {

                                                if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }

                                            }
                                        }

                                    }
                                    else if (_bee.perc_icod_afp == 2)
                                    {
                                        List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                        lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(2).ToList();
                                        foreach (var item in lstfondoPensionConceptos)
                                        {
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "04")
                                            {
                                                PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                            {
                                                PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                            {
                                                if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                    else if (_bee.perc_icod_afp == 3)
                                    {
                                        List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                        lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(3).ToList();
                                        foreach (var item in lstfondoPensionConceptos)
                                        {
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                            {
                                                PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                            {
                                                PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                            {
                                                if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                        lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(4).ToList();
                                        foreach (var item in lstfondoPensionConceptos)
                                        {
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                            {
                                                PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                            {
                                                PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                            {
                                                if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                            }
                                        }


                                    }
                                }
                                else /**MIXTA*/
                                {
                                    if (_bee.perc_icod_afp == 1)
                                    {
                                        List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                        lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(1);
                                        foreach (var item in lstfondoPensionConceptosMixtas)
                                        {
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                            {
                                                PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                            {
                                                PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                            {
                                                if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                            }
                                        }


                                    }
                                    else if (_bee.perc_icod_afp == 2)
                                    {
                                        List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                        lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(2);
                                        foreach (var item in lstfondoPensionConceptosMixtas)
                                        {
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                            {
                                                PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                            {
                                                PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                            {
                                                if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                    else if (_bee.perc_icod_afp == 3)
                                    {
                                        List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                        lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(3);
                                        foreach (var item in lstfondoPensionConceptosMixtas)
                                        {
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                            {
                                                PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                            {
                                                PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                            {
                                                if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                        lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(4);
                                        foreach (var item in lstfondoPensionConceptosMixtas)
                                        {
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                            {
                                                PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                            {
                                                PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                            {
                                                if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                                else
                                                {
                                                    PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                            else if (_bee.perc_icod_tip_fdo_pension == 6385)/**ONP*/
                            {
                                List<EFondosPensiones> lstfondoPension = new List<EFondosPensiones>();
                                lstfondoPension = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == 6).ToList();
                                PPD.pland_desc_onp = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * lstfondoPension[0].fdpc_nporcentaje_fijo) / 100), 2);
                                PPD.pland_desc_fondo = 0;
                                PPD.pland_desc_comision = 0;
                                PPD.pland_desc_seguro = 0;
                            }

                            PPD.pland_desc_tot_afp = PPD.pland_desc_fondo + PPD.pland_desc_comision + PPD.pland_desc_seguro + PPD.pland_desc_aporte_c_prov + PPD.pland_desc_aporte_s_prov;
                            PPD.pland_desc_retenc_judicial = Math.Round(Convert.ToDecimal(((PPD.pland_nutilidades + PPD.pland_nrem_computable - (PPD.pland_desc_tot_afp + PPD.pland_desc_renta5 + PPD.pland_desc_onp)) * _bee.perc_retenc_judicial) / 100), 2);

                            PPD.pland_desc_total_desc = PPD.pland_desc_tot_afp + PPD.pland_desc_renta5 + PPD.pland_desc_adelanto + PPD.pland_desc_prestamo + PPD.pland_desc_eps + PPD.pland_desc_otros_desc_no_afect + PPD.pland_desc_onp + PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas + PPD.pland_npago_utilid + PPD.pland_npago_utilidad_convencional;



                            if (PPD.pland_nsubsidios_essalud > 0)
                            {
                                PPD.pland_aport_essalud9 = Math.Round(Convert.ToDecimal((((PPD.pland_rem_basica + PPD.pland_vacaciones + PPD.pland_nasignacion_familiar + PPD.pland_nhoras_25 +
                                    PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos) - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * lstParametroPlanilla[0].prpc_nporc_essalud) / 100), 2);
                            }
                            else if (PPD.pland_nsubsidios_essalud == 0 && Convert.ToDecimal((PPD.pland_nrem_computable - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas))) > lstParametroPlanilla[0].prpc_nsueldo_minimo)
                            {
                                PPD.pland_aport_essalud9 = Math.Round(Convert.ToDecimal((((PPD.pland_nrem_computable - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas))) * lstParametroPlanilla[0].prpc_nporc_essalud) / 100), 2);
                            }
                            else
                            {
                                PPD.pland_aport_essalud9 = Math.Round(Convert.ToDecimal((lstParametroPlanilla[0].prpc_nsueldo_minimo * lstParametroPlanilla[0].prpc_nporc_essalud) / 100), 2);
                            }
                            if (_bee.perc_beps == true)
                            {
                                PPD.pland_aport_eps_pacifico = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * lstParametroPlanilla[0].prpc_nporc_eps_pacifico) / 100), 2);
                                PPD.pland_aport_essalud = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * lstParametroPlanilla[0].prpc_nporc_eps_essalud) / 100), 2);
                            }
                            else
                            {
                                PPD.pland_aport_eps_pacifico = 0;
                                PPD.pland_aport_essalud = 0;
                            }

                            PPD.pland_total_neto_pagar = (PPD.pland_nremun_bruta - PPD.pland_desc_total_desc) - PPD.pland_desc_retenc_judicial;
                            PPD.operacion = 1;
                        }
                        lstDetalle.Add(PPD);
                        grdCTS.RefreshDataSource();
                    }
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaPersonalDetalle obj = (EPlanillaPersonalDetalle)viewCTS.GetRow(viewCTS.FocusedRowHandle);
            if (obj.operacion == 1)
            {
                lstDetalle.Remove(obj);
                viewCTS.RefreshData();
                viewCTS.MovePrev();
            }
            else
            {

                obj.operacion = 3;
                lstDetalleEliminados.Add(obj);
                lstDetalle.Remove(obj);
                viewCTS.RefreshData();
                viewCTS.MovePrev();

            }

        }

        public void viewCTS_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.CreateNew)
            {


                EPlanillaPersonalDetalle Obe = (EPlanillaPersonalDetalle)viewCTS.GetRow(viewCTS.FocusedRowHandle);

                if (Obe != null)
                {
                    List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                    lstParametroPlanilla = new BPlanillas().listarParametroPlanilla().ToList();
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    Obe.intUsuario = Valores.intUsuario;
                    //Obe.pland_sueldo_basico = Obe.pland_sueldo_basico;
                    Obe.pland_dias_efectivos = Obe.pland_dias - (Obe.pland_faltas + Obe.pland_vacaciones + Obe.pland_descanso_medico + Obe.pland_dias_subsidios);
                    if (Obe.pland_faltas > 0)
                    {
                        Obe.pland_rem_basica = Obe.pland_sueldo_basico;
                    }
                    else
                    {
                        Obe.pland_rem_basica = Math.Round(Convert.ToDecimal((Obe.pland_sueldo_basico / lstParametroPlanilla[0].prpc_ndias_trabajo) * (Obe.pland_dias_efectivos + Obe.pland_descanso_medico)), 2);
                    }
                    if (Obe.pland_dias_subsidios == 30)
                    {
                        Obe.pland_nasignacion_familiar = 0;
                    }

                    if (Obe.pland_vacaciones > 0)
                    {
                        Obe.pland_nmonto_vacaciones = Math.Round(Convert.ToDecimal((Obe.pland_sueldo_basico / lstParametroPlanilla[0].prpc_ndias_trabajo) * Obe.pland_vacaciones), 2);
                    }
                    else
                    {
                        Obe.pland_nmonto_vacaciones = 0;
                    }

                    Obe.pland_nremun_bruta = Convert.ToDecimal(Obe.pland_rem_basica + Obe.pland_nmonto_vacaciones + Obe.pland_nasignacion_familiar +
                        Obe.pland_nhoras_25 + Obe.pland_nhoras_35 + Obe.pland_nferiado_descanso + Obe.pland_notros_ingresos + Obe.pland_nsubsidios_essalud + Obe.pland_ncomision_venta + Obe.pland_ncomision_eventual +
                        Obe.pland_nasignacion_transporte + Obe.pland_nvales_alimentos + Obe.pland_nadelanto_sueldo + Obe.pland_ngratif_afecto + Obe.pland_nbonif_afecto + Obe.pland_nvacaciones_truncas + Obe.pland_ngratif_no_afecto +
                        Obe.pland_nbonif_no_afecto + Obe.pland_nCTS + Obe.pland_nutilidades + Obe.pland_nutilidad_convencional);

                    Obe.pland_nremun_computable_neta = Math.Round(Convert.ToDecimal((Obe.pland_rem_basica + Obe.pland_nmonto_vacaciones + Obe.pland_nasignacion_familiar +
                        Obe.pland_nhoras_25 + Obe.pland_nhoras_35 + Obe.pland_nferiado_descanso + Obe.pland_notros_ingresos + Obe.pland_nsubsidios_essalud +
                        Obe.pland_ncomision_venta + Obe.pland_ncomision_eventual) + Obe.pland_nvacaciones_truncas) - (Convert.ToDecimal(Obe.pland_ninasistencias + Obe.pland_ntardanzas + Obe.pland_npago_utilid)), 2);


                    Obe.pland_nrem_computable = Math.Round(Convert.ToDecimal((Obe.pland_rem_basica + Obe.pland_nmonto_vacaciones + Obe.pland_nasignacion_familiar +
                        Obe.pland_nhoras_25 + Obe.pland_nhoras_35 + Obe.pland_nferiado_descanso + Obe.pland_notros_ingresos + Obe.pland_nsubsidios_essalud +
                        Obe.pland_ncomision_venta + Obe.pland_ncomision_eventual) + Obe.pland_nvacaciones_truncas), 2);


                    Obe.pland_ninasistencias = Math.Round(Convert.ToDecimal((Obe.pland_sueldo_basico / lstParametroPlanilla[0].prpc_ndias_trabajo) * (Obe.pland_faltas)), 2);

                    List<EPersonal> lstPersonal = new List<EPersonal>();
                    lstPersonal = new BPlanillas().listarPersonal().Where(a => a.perc_icod_personal == Obe.pland_icod_personal).ToList();
                    foreach (var _bee in lstPersonal)
                    {



                        if (_bee.perc_icod_tip_fdo_pension == 6384)/**AFP*/
                        {

                            Obe.pland_desc_onp = 0;
                            if (_bee.perc_icod_tip_comision == 6386)/**FIJA*/
                            {

                                if (_bee.perc_icod_afp == 1)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(1).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            Obe.pland_desc_fondo = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            Obe.pland_desc_comision = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if ((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 2)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(2).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "04")
                                        {
                                            Obe.pland_desc_fondo = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            Obe.pland_desc_comision = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if ((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 3)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(3).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            Obe.pland_desc_fondo = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            Obe.pland_desc_comision = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if ((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(4).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            Obe.pland_desc_fondo = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            Obe.pland_desc_comision = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if ((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }


                                }
                            }
                            else /**MIXTA*/
                            {
                                if (_bee.perc_icod_afp == 1)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(1);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            Obe.pland_desc_fondo = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            Obe.pland_desc_comision = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if ((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }


                                }
                                else if (_bee.perc_icod_afp == 2)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(2);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            Obe.pland_desc_fondo = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            Obe.pland_desc_comision = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            if ((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 3)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(3);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            Obe.pland_desc_fondo = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            Obe.pland_desc_comision = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if ((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(4);
                                    //if (lstfondoPensionConceptosMixtas[0].fdpd2_iid_vcodigo_fp_concepto_mixto == 01.ToString())
                                    //{
                                    //    Obe.pland_desc_fondo = (Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas) * lstfondoPensionConceptosMixtas[0].fdpd2_nporcentaje_concepto_mixto) / 100;
                                    //}
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            Obe.pland_desc_fondo = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            Obe.pland_desc_comision = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if ((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.pland_desc_seguro = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        else if (_bee.perc_icod_tip_fdo_pension == 6385)/**ONP*/
                        {
                            List<EFondosPensiones> lstfondoPension = new List<EFondosPensiones>();
                            lstfondoPension = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == 6).ToList();
                            Obe.pland_desc_onp = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - Obe.pland_nsubsidios_essalud - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * lstfondoPension[0].fdpc_nporcentaje_fijo) / 100), 2);
                            Obe.pland_desc_fondo = 0;
                            Obe.pland_desc_comision = 0;
                            Obe.pland_desc_seguro = 0;
                        }

                        Obe.pland_desc_tot_afp = Obe.pland_desc_fondo + Obe.pland_desc_comision + Obe.pland_desc_seguro + Obe.pland_desc_aporte_c_prov + Obe.pland_desc_aporte_s_prov;
                        Obe.pland_desc_retenc_judicial = Math.Round(Convert.ToDecimal(((Obe.pland_nutilidades + Obe.pland_nrem_computable - (Obe.pland_desc_tot_afp + Obe.pland_desc_renta5 + Obe.pland_desc_onp)) * _bee.perc_retenc_judicial) / 100), 2);/**Datos k ingresa el usuario**/

                        Obe.pland_desc_total_desc = Obe.pland_desc_tot_afp + Obe.pland_desc_renta5 + Obe.pland_desc_adelanto + Obe.pland_desc_prestamo + Obe.pland_desc_eps + Obe.pland_desc_otros_desc_no_afect + Obe.pland_desc_onp + Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas + Obe.pland_npago_utilid + Obe.pland_npago_utilidad_convencional;



                        if (Obe.pland_nsubsidios_essalud > 0)
                        {
                            Obe.pland_aport_essalud9 = Math.Round(Convert.ToDecimal((((Obe.pland_rem_basica + Obe.pland_vacaciones + Obe.pland_nasignacion_familiar + Obe.pland_nhoras_25 +
                                Obe.pland_nhoras_35 + Obe.pland_nferiado_descanso + Obe.pland_notros_ingresos) - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * lstParametroPlanilla[0].prpc_nporc_essalud) / 100), 2);
                        }
                        else if (Obe.pland_nsubsidios_essalud == 0 && Convert.ToDecimal((Obe.pland_nrem_computable - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas))) > lstParametroPlanilla[0].prpc_nsueldo_minimo)
                        {
                            Obe.pland_aport_essalud9 = Math.Round(Convert.ToDecimal((((Obe.pland_nrem_computable - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas))) * lstParametroPlanilla[0].prpc_nporc_essalud) / 100), 2);
                        }
                        else
                        {
                            Obe.pland_aport_essalud9 = Math.Round(Convert.ToDecimal((lstParametroPlanilla[0].prpc_nsueldo_minimo * lstParametroPlanilla[0].prpc_nporc_essalud) / 100), 2);
                        }

                        if (_bee.perc_beps == true)
                        {
                            Obe.pland_aport_eps_pacifico = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * lstParametroPlanilla[0].prpc_nporc_eps_pacifico) / 100), 2);
                            Obe.pland_aport_essalud = Math.Round(Convert.ToDecimal(((Obe.pland_nrem_computable - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) * lstParametroPlanilla[0].prpc_nporc_eps_essalud) / 100), 2);
                        }
                        else
                        {
                            Obe.pland_aport_eps_pacifico = 0;
                            Obe.pland_aport_essalud = 0;
                        }

                        Obe.pland_total_neto_pagar = (Obe.pland_nremun_bruta - Obe.pland_desc_total_desc) - Obe.pland_desc_retenc_judicial;

                        #region Vacaciones Contable
                        Obe.vccn_nvacaciones = Obe.pland_nmonto_vacaciones;
                        if (_bee.perc_icod_tip_fdo_pension == 6384)/**AFP*/
                        {
                            Obe.vccn_nopn = 0;

                            if (_bee.perc_icod_tip_comision == 6386)/**FIJA*/
                            {

                                if (_bee.perc_icod_afp == 1)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(1).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            Obe.vccn_nfondo = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            Obe.vccn_ncomision = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {

                                            if (Obe.vccn_nvacaciones >= item.fdpd_ntope_concpeto)
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 2)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(2).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "04")
                                        {
                                            Obe.vccn_nfondo = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            Obe.vccn_ncomision = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if (Obe.vccn_nvacaciones >= item.fdpd_ntope_concpeto)
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 3)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(3).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            Obe.vccn_nfondo = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            Obe.vccn_ncomision = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if (Obe.vccn_nvacaciones >= item.fdpd_ntope_concpeto)
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(4).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            Obe.vccn_nfondo = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            Obe.vccn_ncomision = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if (Obe.vccn_nvacaciones >= item.fdpd_ntope_concpeto)
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }


                                }
                            }
                            else /**MIXTA*/
                            {
                                if (_bee.perc_icod_afp == 1)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(1);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            Obe.vccn_nfondo = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            Obe.vccn_ncomision = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if (Obe.vccn_nvacaciones >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }


                                }
                                else if (_bee.perc_icod_afp == 2)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(2);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            Obe.vccn_nfondo = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            Obe.vccn_ncomision = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            if (Obe.vccn_nvacaciones >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 3)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(3);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            Obe.vccn_nfondo = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            Obe.vccn_ncomision = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if (Obe.vccn_nvacaciones >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(4);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            Obe.vccn_nfondo = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            Obe.vccn_ncomision = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if (Obe.vccn_nvacaciones >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                Obe.vccn_nseguro = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        else if (_bee.perc_icod_tip_fdo_pension == 6385)/**ONP*/
                        {
                            List<EFondosPensiones> lstfondoPension = new List<EFondosPensiones>();
                            lstfondoPension = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == 6).ToList();

                            Obe.vccn_nfondo = 0;
                            Obe.vccn_ncomision = 0;
                            Obe.vccn_nseguro = 0;
                            Obe.vccn_nopn = Math.Round(Convert.ToDecimal((Obe.vccn_nvacaciones * lstfondoPension[0].fdpc_nporcentaje_fijo) / 100), 2);
                        }

                        Obe.vccn_ntotal_afp = Obe.vccn_nfondo + Obe.vccn_ncomision + Obe.vccn_nseguro;
                        if (Obe.pland_nmonto_vacaciones > ((Obe.pland_rem_basica + Obe.pland_nasignacion_familiar + Obe.pland_nhoras_25 + Obe.pland_nhoras_35 + Obe.pland_nferiado_descanso + Obe.pland_notros_ingresos + Obe.pland_nsubsidios_essalud) - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)))
                        {
                            Obe.vccn_nrenta5 = Obe.pland_desc_renta5;
                        }
                        else
                        {
                            Obe.vccn_nrenta5 = 0;
                        }

                        if (Obe.pland_nmonto_vacaciones > ((Obe.pland_rem_basica + Obe.pland_nasignacion_familiar + Obe.pland_nhoras_25 + Obe.pland_nhoras_35 + Obe.pland_nferiado_descanso + Obe.pland_notros_ingresos + Obe.pland_nsubsidios_essalud) - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)))
                        {
                            Obe.vccn_notros_desc = Obe.pland_desc_adelanto + Obe.pland_desc_prestamo + Obe.pland_desc_eps + Obe.pland_desc_otros_desc_no_afect;
                        }
                        else
                        {
                            Obe.vccn_notros_desc = 0;
                        }

                        Obe.vccn_nessalud = Math.Round(Convert.ToDecimal((Obe.pland_nmonto_vacaciones * lstParametroPlanilla[0].prpc_ngratificacion_essalud) / 100), 2);
                        Obe.vccn_nvacaciones_neto = Math.Round(Convert.ToDecimal(Obe.vccn_nvacaciones - (Obe.vccn_ntotal_afp + Obe.vccn_nopn + Obe.vccn_nrenta5 + Obe.vccn_notros_desc)), 2);
                        #endregion

                        #region Remuneraciones Contable

                        Obe.rmcn_remun_computable = (((Obe.pland_rem_basica + Obe.pland_nasignacion_familiar + Obe.pland_nhoras_25 + Obe.pland_nhoras_35 + Obe.pland_nferiado_descanso + Obe.pland_notros_ingresos + Obe.pland_nsubsidios_essalud) - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)));

                        if (Obe.pland_dias_subsidios >= 30)
                        {
                            Obe.rmcn_onp = 0;
                            Obe.rmcn_fondo = 0;
                            Obe.rmcn_comision = 0;
                            Obe.rmcn_seguro = 0;
                            Obe.rmcn_total_afp = 0;
                            Obe.rmcn_rta_5ta = 0;
                            Obe.rmcn_otros_dstos = 0;
                            Obe.rmcn_reten_judicial = 0;
                            Obe.rmcn_essalud = 0;
                        }
                        else
                        {
                            if (_bee.perc_icod_tip_fdo_pension == 6384)/**AFP*/
                            {
                                Obe.rmcn_onp = 0;

                                if (_bee.perc_icod_tip_comision == 6386)/**FIJA*/
                                {

                                    if (_bee.perc_icod_afp == 1)
                                    {
                                        List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                        lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(1).ToList();
                                        foreach (var item in lstfondoPensionConceptos)
                                        {
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                            {
                                                Obe.rmcn_fondo = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                            {
                                                Obe.rmcn_comision = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                            {

                                                if (Obe.rmcn_remun_computable >= item.fdpd_ntope_concpeto)
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                                else
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }

                                            }
                                        }

                                    }
                                    else if (_bee.perc_icod_afp == 2)
                                    {
                                        List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                        lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(2).ToList();
                                        foreach (var item in lstfondoPensionConceptos)
                                        {
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "04")
                                            {
                                                Obe.rmcn_fondo = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                            {
                                                Obe.rmcn_comision = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                            {
                                                if (Obe.rmcn_remun_computable >= item.fdpd_ntope_concpeto)
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                                else
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                    else if (_bee.perc_icod_afp == 3)
                                    {
                                        List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                        lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(3).ToList();
                                        foreach (var item in lstfondoPensionConceptos)
                                        {
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                            {
                                                Obe.rmcn_fondo = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                            {
                                                Obe.rmcn_comision = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                            {
                                                if (Obe.rmcn_remun_computable >= item.fdpd_ntope_concpeto)
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                                else
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                        lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(4).ToList();
                                        foreach (var item in lstfondoPensionConceptos)
                                        {
                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                            {
                                                Obe.rmcn_fondo = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                            {
                                                Obe.rmcn_comision = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                            if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                            {
                                                if (Obe.rmcn_remun_computable >= item.fdpd_ntope_concpeto)
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                                else
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                                }
                                            }
                                        }


                                    }
                                }
                                else /**MIXTA*/
                                {
                                    if (_bee.perc_icod_afp == 1)
                                    {
                                        List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                        lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(1);
                                        foreach (var item in lstfondoPensionConceptosMixtas)
                                        {
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                            {
                                                Obe.rmcn_fondo = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                            {
                                                Obe.rmcn_comision = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                            {
                                                if (Obe.rmcn_remun_computable >= item.fdpd2_ntope_concepto_mixto)
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                                else
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                            }
                                        }


                                    }
                                    else if (_bee.perc_icod_afp == 2)
                                    {
                                        List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                        lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(2);
                                        foreach (var item in lstfondoPensionConceptosMixtas)
                                        {
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                            {
                                                Obe.rmcn_fondo = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                            {
                                                Obe.rmcn_comision = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                            {
                                                if (Obe.rmcn_remun_computable >= item.fdpd2_ntope_concepto_mixto)
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                                else
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                    else if (_bee.perc_icod_afp == 3)
                                    {
                                        List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                        lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(3);
                                        foreach (var item in lstfondoPensionConceptosMixtas)
                                        {
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                            {
                                                Obe.rmcn_fondo = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                            {
                                                Obe.rmcn_comision = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                            {
                                                if (Obe.rmcn_remun_computable >= item.fdpd2_ntope_concepto_mixto)
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                                else
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                        lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(4);
                                        foreach (var item in lstfondoPensionConceptosMixtas)
                                        {
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                            {
                                                Obe.rmcn_fondo = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                            {
                                                Obe.rmcn_comision = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }

                                            if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                            {
                                                if (Obe.rmcn_remun_computable >= item.fdpd2_ntope_concepto_mixto)
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                                else
                                                {
                                                    Obe.rmcn_seguro = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                            else if (_bee.perc_icod_tip_fdo_pension == 6385)/**ONP*/
                            {
                                List<EFondosPensiones> lstfondoPension = new List<EFondosPensiones>();
                                lstfondoPension = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == 6).ToList();

                                Obe.rmcn_fondo = 0;
                                Obe.rmcn_comision = 0;
                                Obe.rmcn_seguro = 0;
                                Obe.rmcn_onp = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * lstfondoPension[0].fdpc_nporcentaje_fijo) / 100), 2);
                            }

                            Obe.rmcn_aporte_c_prov = Obe.pland_desc_aporte_c_prov;
                            Obe.rmcn_aporte_s_prov = Obe.pland_desc_aporte_s_prov;

                            Obe.rmcn_total_afp = Obe.rmcn_fondo + Obe.rmcn_comision + Obe.rmcn_seguro + Obe.rmcn_aporte_c_prov + Obe.rmcn_aporte_s_prov;

                            if (((Obe.pland_rem_basica + Obe.pland_nasignacion_familiar + Obe.pland_nhoras_25 + Obe.pland_nhoras_35 + Obe.pland_nferiado_descanso + Obe.pland_notros_ingresos) - (Obe.pland_desc_otros_desc_afect + Obe.pland_ninasistencias + Obe.pland_ntardanzas)) >= Obe.pland_nmonto_vacaciones)
                            {
                                Obe.rmcn_rta_5ta = Obe.pland_desc_renta5;
                            }
                            else
                            {
                                Obe.rmcn_rta_5ta = 0;
                            }

                            if (Obe.rmcn_remun_computable >= Obe.pland_nmonto_vacaciones)
                            {
                                Obe.rmcn_otros_dstos = Obe.pland_desc_adelanto + Obe.pland_desc_prestamo + Obe.pland_desc_eps + Obe.pland_desc_otros_desc_no_afect;
                            }
                            else
                            {
                                Obe.rmcn_otros_dstos = 0;
                            }

                            if (Obe.rmcn_remun_computable > Obe.vccn_nvacaciones)
                            {
                                Obe.rmcn_reten_judicial = Obe.pland_desc_retenc_judicial;
                            }
                            else
                            {
                                Obe.rmcn_reten_judicial = 0;
                            }

                            if (Obe.rmcn_remun_computable < lstParametroPlanilla[0].prpc_nsueldo_minimo)
                            {
                                Obe.rmcn_essalud = Math.Round(Convert.ToDecimal((lstParametroPlanilla[0].prpc_nsueldo_minimo * lstParametroPlanilla[0].prpc_ngratificacion_essalud) / 100), 2);
                            }
                            else
                            {
                                Obe.rmcn_essalud = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable * lstParametroPlanilla[0].prpc_ngratificacion_essalud) / 100), 2);
                            }
                        }

                        Obe.rmcn_remun_neto = Math.Round(Convert.ToDecimal((Obe.rmcn_remun_computable + Obe.pland_nvales_alimentos + Obe.pland_nasignacion_transporte) - (Obe.rmcn_total_afp + Obe.rmcn_onp + Obe.rmcn_rta_5ta + Obe.rmcn_otros_dstos + Obe.rmcn_reten_judicial)), 2);

                        #endregion
                    }



                }

                new BPlanillas().modificarPlanillaPersonalDetalle(Obe);
            }

        }

        private void calcularToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (var PPD in lstDetalle)
            {
                PPD.operacion = 2;
                List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                lstParametroPlanilla = new BPlanillas().listarParametroPlanilla().ToList();
                List<EPersonal> lstPER = new List<EPersonal>();
                lstPER = new BPlanillas().listarPersonal().Where(n => n.perc_icod_personal == PPD.pland_icod_personal).ToList();
                if (PPD.pland_nasignacion_transporte == null)
                {
                    PPD.pland_nasignacion_transporte = 0;
                }

                if (PPD.pland_nutilidad_convencional == null)
                {
                    PPD.pland_nutilidad_convencional = 0;
                }
                if (PPD.pland_npago_utilidad_convencional == null)
                {
                    PPD.pland_npago_utilidad_convencional = 0;
                }
                if (PPD.pland_desc_aporte_c_prov == null)
                {
                    PPD.pland_desc_aporte_c_prov = 0;
                }
                if (PPD.pland_desc_aporte_s_prov == null)
                {
                    PPD.pland_desc_aporte_s_prov = 0;
                }

                if (PPD.rmcn_aporte_c_prov == null)
                {
                    PPD.rmcn_aporte_c_prov = 0;
                }
                if (PPD.rmcn_aporte_s_prov == null)
                {
                    PPD.rmcn_aporte_s_prov = 0;
                }


                if (PPD.pland_desc_otros_desc_afect == null)
                {
                    PPD.pland_desc_otros_desc_afect = 0;
                }
                if (PPD.pland_desc_total_desc == null)
                {
                    PPD.pland_desc_total_desc = 0;
                }
                if (PPD.pland_total_neto_pagar == null)
                {
                    PPD.pland_total_neto_pagar = 0;
                }
                if (PPD.rmcn_remun_computable == null)
                {
                    PPD.rmcn_remun_computable = 0;
                }


                //PPD.pland_dias_efectivos = PPD.pland_dias - (PPD.pland_faltas + PPD.pland_vacaciones + PPD.pland_descanso_medico + PPD.pland_dias_subsidios);
                //PPD.pland_rem_basica = Math.Round(Convert.ToDecimal((PPD.pland_sueldo_basico / PPD.pland_dias) * PPD.pland_dias_efectivos), 2);

                PPD.pland_dias_efectivos = PPD.pland_dias - (PPD.pland_faltas + PPD.pland_vacaciones + PPD.pland_descanso_medico + PPD.pland_dias_subsidios);
                if (PPD.pland_faltas > 0)
                {
                    PPD.pland_rem_basica = PPD.pland_sueldo_basico;
                }
                else
                {
                    PPD.pland_rem_basica = Math.Round(Convert.ToDecimal((PPD.pland_sueldo_basico / lstParametroPlanilla[0].prpc_ndias_trabajo) * (PPD.pland_dias_efectivos + PPD.pland_descanso_medico)), 2);
                }
                if (PPD.pland_dias_subsidios == 30)
                {
                    PPD.pland_nasignacion_familiar = 0;
                }
                if (PPD.pland_vacaciones > 0)
                {
                    PPD.pland_nmonto_vacaciones = Math.Round(Convert.ToDecimal((PPD.pland_sueldo_basico / lstParametroPlanilla[0].prpc_ndias_trabajo) * PPD.pland_vacaciones), 2);
                }
                else
                {
                    PPD.pland_nmonto_vacaciones = 0;
                }

                PPD.pland_nremun_bruta = Convert.ToDecimal(PPD.pland_rem_basica + PPD.pland_nmonto_vacaciones + PPD.pland_nasignacion_familiar +
                    PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud + PPD.pland_ncomision_venta + PPD.pland_ncomision_eventual +
                    PPD.pland_nasignacion_transporte + PPD.pland_nvales_alimentos + PPD.pland_nadelanto_sueldo + PPD.pland_ngratif_afecto + PPD.pland_nbonif_afecto + PPD.pland_nvacaciones_truncas + PPD.pland_ngratif_no_afecto +
                    PPD.pland_nbonif_no_afecto + PPD.pland_nCTS + PPD.pland_nutilidades + PPD.pland_nutilidad_convencional);

                PPD.pland_nremun_computable_neta = Math.Round(Convert.ToDecimal((PPD.pland_rem_basica + PPD.pland_nmonto_vacaciones + PPD.pland_nasignacion_familiar +
                    PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud +
                    PPD.pland_ncomision_venta + PPD.pland_ncomision_eventual) + PPD.pland_nvacaciones_truncas) - (Convert.ToDecimal(PPD.pland_ninasistencias + PPD.pland_ntardanzas + PPD.pland_npago_utilid)), 2);


                PPD.pland_nrem_computable = Math.Round(Convert.ToDecimal((PPD.pland_rem_basica + PPD.pland_nmonto_vacaciones + PPD.pland_nasignacion_familiar +
                    PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud +
                    PPD.pland_ncomision_venta + PPD.pland_ncomision_eventual) + PPD.pland_nvacaciones_truncas), 2);


                PPD.pland_ninasistencias = Math.Round(Convert.ToDecimal((PPD.pland_sueldo_basico / lstParametroPlanilla[0].prpc_ndias_trabajo) * (PPD.pland_faltas)), 2);

                List<EPersonal> lstPersonal = new List<EPersonal>();
                lstPersonal = new BPlanillas().listarPersonal().Where(a => a.perc_icod_personal == PPD.pland_icod_personal).ToList();
                foreach (var _bee in lstPersonal)
                {



                    if (_bee.perc_icod_tip_fdo_pension == 6384)/**AFP*/
                    {

                        PPD.pland_desc_onp = 0;
                        if (_bee.perc_icod_tip_comision == 6386)/**FIJA*/
                        {

                            if (_bee.perc_icod_afp == 1)
                            {
                                List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(1).ToList();
                                foreach (var item in lstfondoPensionConceptos)
                                {
                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                    {
                                        PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }
                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                    {
                                        PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }

                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                    {
                                        if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                    }
                                }

                            }
                            else if (_bee.perc_icod_afp == 2)
                            {
                                List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(2).ToList();
                                foreach (var item in lstfondoPensionConceptos)
                                {
                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "04")
                                    {
                                        PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }
                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                    {
                                        PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }

                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                    {
                                        if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                    }
                                }

                            }
                            else if (_bee.perc_icod_afp == 3)
                            {
                                List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(3).ToList();
                                foreach (var item in lstfondoPensionConceptos)
                                {
                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                    {
                                        PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }

                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                    {
                                        PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }

                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                    {
                                        if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(4).ToList();
                                foreach (var item in lstfondoPensionConceptos)
                                {
                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                    {
                                        PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }

                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                    {
                                        PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }

                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                    {
                                        if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd_ntope_concpeto)
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                    }
                                }


                            }
                        }
                        else /**MIXTA*/
                        {
                            if (_bee.perc_icod_afp == 1)
                            {
                                List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(1);
                                foreach (var item in lstfondoPensionConceptosMixtas)
                                {
                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                    {
                                        PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                    {
                                        PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                    {
                                        if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                    }
                                }


                            }
                            else if (_bee.perc_icod_afp == 2)
                            {
                                List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(2);
                                foreach (var item in lstfondoPensionConceptosMixtas)
                                {
                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                    {
                                        PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                    {
                                        PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                    {
                                        if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                    }
                                }

                            }
                            else if (_bee.perc_icod_afp == 3)
                            {
                                List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(3);
                                foreach (var item in lstfondoPensionConceptosMixtas)
                                {
                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                    {
                                        PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                    {
                                        PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                    {
                                        if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(4);

                                foreach (var item in lstfondoPensionConceptosMixtas)
                                {
                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                    {
                                        PPD.pland_desc_fondo = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                    {
                                        PPD.pland_desc_comision = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                    {
                                        if ((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= item.fdpd2_ntope_concepto_mixto)
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.pland_desc_seguro = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                    }
                                }

                            }
                        }
                    }
                    else if (_bee.perc_icod_tip_fdo_pension == 6385)/**ONP*/
                    {
                        List<EFondosPensiones> lstfondoPension = new List<EFondosPensiones>();
                        lstfondoPension = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == 6).ToList();
                        PPD.pland_desc_onp = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - PPD.pland_nsubsidios_essalud - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * lstfondoPension[0].fdpc_nporcentaje_fijo) / 100), 2);
                        PPD.pland_desc_fondo = 0;
                        PPD.pland_desc_comision = 0;
                        PPD.pland_desc_seguro = 0;
                    }

                    PPD.pland_desc_tot_afp = PPD.pland_desc_fondo + PPD.pland_desc_comision + PPD.pland_desc_seguro + PPD.pland_desc_aporte_c_prov + PPD.pland_desc_aporte_s_prov;
                    PPD.pland_desc_retenc_judicial = Math.Round(Convert.ToDecimal(((PPD.pland_nutilidades + PPD.pland_nrem_computable - (PPD.pland_desc_tot_afp + PPD.pland_desc_renta5 + PPD.pland_desc_onp)) * _bee.perc_retenc_judicial) / 100), 2);/**Datos k ingresa el usuario**/
                    PPD.pland_desc_total_desc = PPD.pland_desc_tot_afp + PPD.pland_desc_renta5 + PPD.pland_desc_adelanto + PPD.pland_desc_prestamo + PPD.pland_desc_eps + PPD.pland_desc_otros_desc_no_afect + PPD.pland_desc_onp + PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas + PPD.pland_npago_utilid + PPD.pland_npago_utilidad_convencional;

                    //List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                    //lstParametroPlanilla=new BPlanillas().listarParametroPlanilla().ToList();

                    if (PPD.pland_nsubsidios_essalud > 0)
                    {
                        PPD.pland_aport_essalud9 = Math.Round(Convert.ToDecimal((((PPD.pland_rem_basica + PPD.pland_vacaciones + PPD.pland_nasignacion_familiar + PPD.pland_nhoras_25 +
                            PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos) - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * lstParametroPlanilla[0].prpc_nporc_essalud) / 100), 2);
                    }
                    else if (PPD.pland_nsubsidios_essalud == 0 && Convert.ToDecimal((PPD.pland_nrem_computable - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas))) > lstParametroPlanilla[0].prpc_nsueldo_minimo)
                    {
                        PPD.pland_aport_essalud9 = Math.Round(Convert.ToDecimal((((PPD.pland_nrem_computable - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas))) * lstParametroPlanilla[0].prpc_nporc_essalud) / 100), 2);
                    }
                    else
                    {
                        PPD.pland_aport_essalud9 = Math.Round(Convert.ToDecimal((lstParametroPlanilla[0].prpc_nsueldo_minimo * lstParametroPlanilla[0].prpc_nporc_essalud) / 100), 2);
                    }

                    if (_bee.perc_beps == true)
                    {
                        PPD.pland_aport_eps_pacifico = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * lstParametroPlanilla[0].prpc_nporc_eps_pacifico) / 100), 2);
                        PPD.pland_aport_essalud = Math.Round(Convert.ToDecimal(((PPD.pland_nrem_computable - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) * lstParametroPlanilla[0].prpc_nporc_eps_essalud) / 100), 2);
                    }
                    else
                    {
                        PPD.pland_aport_eps_pacifico = 0;
                        PPD.pland_aport_essalud = 0;
                    }

                    PPD.pland_total_neto_pagar = (PPD.pland_nremun_bruta - PPD.pland_desc_total_desc) - PPD.pland_desc_retenc_judicial;

                    #region Vacaciones Contable
                    PPD.vccn_nvacaciones = PPD.pland_nmonto_vacaciones;
                    if (_bee.perc_icod_tip_fdo_pension == 6384)/**AFP*/
                    {
                        PPD.vccn_nopn = 0;

                        if (_bee.perc_icod_tip_comision == 6386)/**FIJA*/
                        {

                            if (_bee.perc_icod_afp == 1)
                            {
                                List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(1).ToList();
                                foreach (var item in lstfondoPensionConceptos)
                                {
                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                    {
                                        PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }
                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                    {
                                        PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }

                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                    {

                                        if (PPD.vccn_nvacaciones >= item.fdpd_ntope_concpeto)
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                    }
                                }

                            }
                            else if (_bee.perc_icod_afp == 2)
                            {
                                List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(2).ToList();
                                foreach (var item in lstfondoPensionConceptos)
                                {
                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "04")
                                    {
                                        PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }
                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                    {
                                        PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }

                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                    {
                                        if (PPD.vccn_nvacaciones >= item.fdpd_ntope_concpeto)
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                    }
                                }

                            }
                            else if (_bee.perc_icod_afp == 3)
                            {
                                List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(3).ToList();
                                foreach (var item in lstfondoPensionConceptos)
                                {
                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                    {
                                        PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }

                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                    {
                                        PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }

                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                    {
                                        if (PPD.vccn_nvacaciones >= item.fdpd_ntope_concpeto)
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(4).ToList();
                                foreach (var item in lstfondoPensionConceptos)
                                {
                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                    {
                                        PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }

                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                    {
                                        PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                    }

                                    if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                    {
                                        if (PPD.vccn_nvacaciones >= item.fdpd_ntope_concpeto)
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                    }
                                }


                            }
                        }
                        else /**MIXTA*/
                        {
                            if (_bee.perc_icod_afp == 1)
                            {
                                List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(1);
                                foreach (var item in lstfondoPensionConceptosMixtas)
                                {
                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                    {
                                        PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                    {
                                        PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                    {
                                        if (PPD.vccn_nvacaciones >= item.fdpd2_ntope_concepto_mixto)
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                    }
                                }


                            }
                            else if (_bee.perc_icod_afp == 2)
                            {
                                List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(2);
                                foreach (var item in lstfondoPensionConceptosMixtas)
                                {
                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                    {
                                        PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                    {
                                        PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                    {
                                        if (PPD.vccn_nvacaciones >= item.fdpd2_ntope_concepto_mixto)
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                    }
                                }

                            }
                            else if (_bee.perc_icod_afp == 3)
                            {
                                List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(3);
                                foreach (var item in lstfondoPensionConceptosMixtas)
                                {
                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                    {
                                        PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                    {
                                        PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                    {
                                        if (PPD.vccn_nvacaciones >= item.fdpd2_ntope_concepto_mixto)
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(4);
                                foreach (var item in lstfondoPensionConceptosMixtas)
                                {
                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                    {
                                        PPD.vccn_nfondo = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }
                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                    {
                                        PPD.vccn_ncomision = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                    }

                                    if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                    {
                                        if (PPD.vccn_nvacaciones >= item.fdpd2_ntope_concepto_mixto)
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                        else
                                        {
                                            PPD.vccn_nseguro = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                    }
                                }

                            }
                        }
                    }
                    else if (_bee.perc_icod_tip_fdo_pension == 6385)/**ONP*/
                    {
                        List<EFondosPensiones> lstfondoPension = new List<EFondosPensiones>();
                        lstfondoPension = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == 6).ToList();

                        PPD.vccn_nfondo = 0;
                        PPD.vccn_ncomision = 0;
                        PPD.vccn_nseguro = 0;
                        PPD.vccn_nopn = Math.Round(Convert.ToDecimal((PPD.vccn_nvacaciones * lstfondoPension[0].fdpc_nporcentaje_fijo) / 100), 2);
                    }

                    PPD.vccn_ntotal_afp = PPD.vccn_nfondo + PPD.vccn_ncomision + PPD.vccn_nseguro;
                    if (PPD.pland_nmonto_vacaciones > ((PPD.pland_rem_basica + PPD.pland_nasignacion_familiar + PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud) - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)))
                    {
                        PPD.vccn_nrenta5 = PPD.pland_desc_renta5;
                    }
                    else
                    {
                        PPD.vccn_nrenta5 = 0;
                    }

                    if (PPD.pland_nmonto_vacaciones > ((PPD.pland_rem_basica + PPD.pland_nasignacion_familiar + PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud) - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)))
                    {
                        PPD.vccn_notros_desc = PPD.pland_desc_adelanto + PPD.pland_desc_prestamo + PPD.pland_desc_eps + PPD.pland_desc_otros_desc_no_afect;
                    }
                    else
                    {
                        PPD.vccn_notros_desc = 0;
                    }

                    PPD.vccn_nessalud = Math.Round(Convert.ToDecimal((PPD.pland_nmonto_vacaciones * lstParametroPlanilla[0].prpc_ngratificacion_essalud) / 100), 2);
                    PPD.vccn_nvacaciones_neto = Math.Round(Convert.ToDecimal(PPD.vccn_nvacaciones - (PPD.vccn_ntotal_afp + PPD.vccn_nopn + PPD.vccn_nrenta5 + PPD.vccn_notros_desc)), 2);
                    #endregion

                    #region Remuneraciones Contable

                    PPD.rmcn_remun_computable = (((PPD.pland_rem_basica + PPD.pland_nasignacion_familiar + PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos + PPD.pland_nsubsidios_essalud) - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)));

                    if (PPD.pland_dias_subsidios >= 30)
                    {
                        PPD.rmcn_onp = 0;
                        PPD.rmcn_fondo = 0;
                        PPD.rmcn_comision = 0;
                        PPD.rmcn_seguro = 0;
                        PPD.rmcn_total_afp = 0;
                        PPD.rmcn_rta_5ta = 0;
                        PPD.rmcn_otros_dstos = 0;
                        PPD.rmcn_reten_judicial = 0;
                        PPD.rmcn_essalud = 0;
                    }
                    else
                    {
                        if (_bee.perc_icod_tip_fdo_pension == 6384)/**AFP*/
                        {
                            PPD.rmcn_onp = 0;

                            if (_bee.perc_icod_tip_comision == 6386)/**FIJA*/
                            {

                                if (_bee.perc_icod_afp == 1)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(1).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {

                                            if (PPD.rmcn_remun_computable >= item.fdpd_ntope_concpeto)
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }

                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 2)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(2).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "04")
                                        {
                                            PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if (PPD.rmcn_remun_computable >= item.fdpd_ntope_concpeto)
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 3)
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(3).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if (PPD.rmcn_remun_computable >= item.fdpd_ntope_concpeto)
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    List<EFondosPensionesConceptos> lstfondoPensionConceptos = new List<EFondosPensionesConceptos>();
                                    lstfondoPensionConceptos = new BPlanillas().listarFondosPensionesConceptos(4).ToList();
                                    foreach (var item in lstfondoPensionConceptos)
                                    {
                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "01")
                                        {
                                            PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "03")
                                        {
                                            PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                        }

                                        if (item.fdpd_iid_vcodigo_fondo_concepto == "02")
                                        {
                                            if (PPD.rmcn_remun_computable >= item.fdpd_ntope_concpeto)
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd_ntope_concpeto * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd_nporcentaje_concepto) / 100), 2);
                                            }
                                        }
                                    }


                                }
                            }
                            else /**MIXTA*/
                            {
                                if (_bee.perc_icod_afp == 1)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(1);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if (PPD.rmcn_remun_computable >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }


                                }
                                else if (_bee.perc_icod_afp == 2)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(2);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            if (PPD.rmcn_remun_computable >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else if (_bee.perc_icod_afp == 3)
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(3);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if (PPD.rmcn_remun_computable >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    List<EFondosPensionesMixtas> lstfondoPensionConceptosMixtas = new List<EFondosPensionesMixtas>();
                                    lstfondoPensionConceptosMixtas = new BPlanillas().listarFondosPensionesMixtas(4);
                                    foreach (var item in lstfondoPensionConceptosMixtas)
                                    {
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "01")
                                        {
                                            PPD.rmcn_fondo = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }
                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "03")
                                        {
                                            PPD.rmcn_comision = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                        }

                                        if (item.fdpd2_iid_vcodigo_fp_concepto_mixto == "02")
                                        {
                                            if (PPD.rmcn_remun_computable >= item.fdpd2_ntope_concepto_mixto)
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((item.fdpd2_ntope_concepto_mixto * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                            else
                                            {
                                                PPD.rmcn_seguro = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * item.fdpd2_nporcentaje_concepto_mixto) / 100), 2);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        else if (_bee.perc_icod_tip_fdo_pension == 6385)/**ONP*/
                        {
                            List<EFondosPensiones> lstfondoPension = new List<EFondosPensiones>();
                            lstfondoPension = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == 6).ToList();

                            PPD.rmcn_fondo = 0;
                            PPD.rmcn_comision = 0;
                            PPD.rmcn_seguro = 0;
                            PPD.rmcn_onp = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * lstfondoPension[0].fdpc_nporcentaje_fijo) / 100), 2);
                        }

                        PPD.rmcn_aporte_c_prov = PPD.pland_desc_aporte_c_prov;
                        PPD.rmcn_aporte_s_prov = PPD.pland_desc_aporte_s_prov;

                        PPD.rmcn_total_afp = PPD.rmcn_fondo + PPD.rmcn_comision + PPD.rmcn_seguro + PPD.rmcn_aporte_c_prov + PPD.rmcn_aporte_s_prov;

                        if (((PPD.pland_rem_basica + PPD.pland_nasignacion_familiar + PPD.pland_nhoras_25 + PPD.pland_nhoras_35 + PPD.pland_nferiado_descanso + PPD.pland_notros_ingresos) - (PPD.pland_desc_otros_desc_afect + PPD.pland_ninasistencias + PPD.pland_ntardanzas)) >= PPD.pland_nmonto_vacaciones)
                        {
                            PPD.rmcn_rta_5ta = PPD.pland_desc_renta5;
                        }
                        else
                        {
                            PPD.rmcn_rta_5ta = 0;
                        }

                        if (PPD.rmcn_remun_computable >= PPD.pland_nmonto_vacaciones)
                        {
                            PPD.rmcn_otros_dstos = PPD.pland_desc_adelanto + PPD.pland_desc_prestamo + PPD.pland_desc_eps + PPD.pland_desc_otros_desc_no_afect;
                        }
                        else
                        {
                            PPD.rmcn_otros_dstos = 0;
                        }

                        if (PPD.rmcn_remun_computable > PPD.vccn_nvacaciones)
                        {
                            PPD.rmcn_reten_judicial = PPD.pland_desc_retenc_judicial;
                        }
                        else
                        {
                            PPD.rmcn_reten_judicial = 0;
                        }

                        if (PPD.rmcn_remun_computable < lstParametroPlanilla[0].prpc_nsueldo_minimo)
                        {
                            PPD.rmcn_essalud = Math.Round(Convert.ToDecimal((lstParametroPlanilla[0].prpc_nsueldo_minimo * lstParametroPlanilla[0].prpc_ngratificacion_essalud) / 100), 2);
                        }
                        else
                        {
                            PPD.rmcn_essalud = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable * lstParametroPlanilla[0].prpc_ngratificacion_essalud) / 100), 2);
                        }
                    }

                    PPD.rmcn_remun_neto = Math.Round(Convert.ToDecimal((PPD.rmcn_remun_computable + PPD.pland_nvales_alimentos + PPD.pland_nasignacion_transporte) - (PPD.rmcn_total_afp + PPD.rmcn_onp + PPD.rmcn_rta_5ta + PPD.rmcn_otros_dstos + PPD.rmcn_reten_judicial)), 2);

                    #endregion

                }

                viewCTS.RefreshData();
                grdCTS.Refresh();
                grdCTS.RefreshDataSource();

                //new BPlanillas().modificarPlanillaPersonalDetalle(PPD);                


            }

        }


    }
}