using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Reportes.Contabilidad.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Registros
{
    public partial class frm05Comprobantes : DevExpress.XtraEditors.XtraForm
    {
        int intMes = -1;
        string strMes;
        List<EVoucherContableCab> lstComprobantes = new List<EVoucherContableCab>();
        List<EVoucherContableDet> lstDiferencia = new List<EVoucherContableDet>();

        List<EVoucherContableDet> lst01 = new List<EVoucherContableDet>();
        List<EVoucherContableDet> lst02 = new List<EVoucherContableDet>();
        BContabilidad objContabilidadData = new BContabilidad();
        List<ECuentaContable> lstCuentaContable = new List<ECuentaContable>();

        public frm05Comprobantes()
        {
            InitializeComponent();
        }

        private string opcionProceso = "";

        private void FrmComprobantes_Load(object sender, EventArgs e)
        {
            lstCuentaContable = objContabilidadData.listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            BSControls.LoaderLook(LkpMeses, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            LkpMeses.EditValue = DateTime.Now.Month;
        }

        public void cargar()
        {
            lstComprobantes = objContabilidadData.listarVoucherContableCab(Parametros.intEjercicio, intMes);
            //dgrComprobante.DataSource = lstComprobantes;
            //viewComprobante.RefreshData();
        }

        void reload(int intIcod)
        {
            cargar();
            dgrComprobante.DataSource = lstComprobantes;
            int index = lstComprobantes.FindIndex(x => x.vcocc_icod_vcontable == intIcod);
            viewComprobante.FocusedRowHandle = index;
            viewComprobante.Focus();
        }

        private void nuevo()
        {
            frmManteComprobante frm = new frmManteComprobante();
            frm.MiEvento += new frmManteComprobante.DelegadoMensaje(reload);
            frm.intOrigen = Parametros.intOriVcoManual;
            frm.intMes = intMes;
            frm.SetInsert();
            frm.lstComprobantes = lstComprobantes;
            frm.Show();
        }

        private void modificar()
        {

            EVoucherContableCab oBe = (EVoucherContableCab)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            if (oBe != null)
            {
                if (oBe.tarec_icorrelativo_origen_vcontable == Parametros.intOriVcoOtroSistema)
                {
                    XtraMessageBox.Show("El comprobante no puede ser modificado porque ha sido generado desde otro sistema", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    frmManteComprobante frm = new frmManteComprobante();
                    frm.MiEvento += new frmManteComprobante.DelegadoMensaje(reload);
                    frm.intMes = intMes;
                    frm.objCab = oBe;
                    frm.lstComprobantes = lstComprobantes;
                    frm.SetModify();
                    frm.Show();
                    frm.setValues();
                }
            }
        }
        private void eliminar()
        {
            try
            {
                EVoucherContableCab Obe = (EVoucherContableCab)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
                if (Obe != null)
                {
                    int index = viewComprobante.FocusedRowHandle;
                    if (Obe.tarec_icorrelativo_origen_vcontable == Parametros.intOriVcoOtroSistema)
                    {
                        XtraMessageBox.Show("El comprobante no puede ser eliminado porque ha sido generado desde otro sistema", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (XtraMessageBox.Show("¿Está seguro que desea eliminar el comprobante " + Obe.strNroVco + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        objContabilidadData.eliminarVoucherContableCab(Obe);
                        cargar();
                        if (lstComprobantes.Count >= index + 1)
                            viewComprobante.FocusedRowHandle = index;
                        else
                            viewComprobante.FocusedRowHandle = index - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewAnalitica_DoubleClick(object sender, EventArgs e)
        {
            EVoucherContableCab oBe = (EVoucherContableCab)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            if (oBe != null)
            {
                frmManteComprobante frm = new frmManteComprobante();
                frm.SetCancel();
                frm.intMes = intMes;
                frm.objCab = oBe;
                frm.Show();
                frm.setValues();
            }
        }


        private void buscarCriterio()
        {
            dgrComprobante.DataSource = lstComprobantes.Where(x => x.vcocc_glosa.ToUpper().Contains(txtDescripcion.Text.ToUpper()) &&
                                                   x.strNroVco.ToString().ToUpper().StartsWith(txtCodigo.Text.ToUpper())).ToList();

        }
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void imprimirComprobanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimirComprobante();
        }

        private void imprimirComprobante()
        {
            EVoucherContableCab Obe = (EVoucherContableCab)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            if (Obe != null)
            {
                var lstDetalle = new BContabilidad().listarVoucherContableDet(Obe.vcocc_icod_vcontable);
                rpt05Comprobantes rpt = new rpt05Comprobantes();
                rpt.cargar(Obe, lstDetalle, strMes);
            }
        }

        private void imprimirRelaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstComprobantes.Count > 0)
            {
                rpt05ComprobanteRelacion rpt = new rpt05ComprobanteRelacion();
                rpt.cargar(lstComprobantes, strMes);
            }
        }
        private void RedondeoComprobante()
        {

            //List<ECuentaContable> lstPlanCuentas = new List<ECuentaContable>();
            //int? ctacc_icod_cuenta_debe_auto = 0;
            //int? ctacc_icod_cuenta_haber_auto = 0;
            lstComprobantes.ForEach(x =>
            {

                //var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == x.vcocc_icod_vcontable).ToList();

                //Lista.ForEach(Obe =>
                //{                
                //     ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                //     ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;                
                //});

                if (x.vcocc_nmto_tot_debe_sol == x.vcocc_nmto_tot_haber_sol && x.vcocc_nmto_tot_debe_dol != x.vcocc_nmto_tot_haber_dol)
                {
                    if (x.vcocc_nmto_tot_debe_dol > x.vcocc_nmto_tot_haber_dol)
                        redondearVoucher(x.vcocc_icod_vcontable, 1);
                    //if (Convert.ToInt32(ctacc_icod_cuenta_debe_auto) > 0)
                    //    agregarCtaAutomatica(Obe, mto_sol, mto_dol);  
                    else if (x.vcocc_nmto_tot_debe_dol < x.vcocc_nmto_tot_haber_dol)
                        redondearVoucher(x.vcocc_icod_vcontable, 2);
                }
                else if (x.vcocc_nmto_tot_debe_sol != x.vcocc_nmto_tot_haber_sol && x.vcocc_nmto_tot_debe_dol == x.vcocc_nmto_tot_haber_dol)
                {
                    if (x.vcocc_nmto_tot_debe_sol > x.vcocc_nmto_tot_haber_sol)
                        redondearVoucher(x.vcocc_icod_vcontable, 3);
                    else if (x.vcocc_nmto_tot_debe_sol < x.vcocc_nmto_tot_haber_sol)
                        redondearVoucher(x.vcocc_icod_vcontable, 4);
                }
            });
            cargar();
        }

        private void redondearVoucher(int vcocc_icod_vcontable, int intCaso)
        {
            objContabilidadData.redondearVoucher(vcocc_icod_vcontable, intCaso, Valores.intUsuario, WindowsIdentity.GetCurrent().Name);
        }

        private void redondeoDeComprobantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (intMes == -1)
                return;
            if (lstComprobantes.Count > 0)
            {
                if (XtraMessageBox.Show("¿Está seguro que desea ejecutar el Proceso de Rendodeo de Comprobates para el mes de " + strMes + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    opcionProceso = "REDONDEO";
                    controlEnable(true);
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            else
                XtraMessageBox.Show("No existen comprobantes para ejecutar este Proceso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void diferenciaDeCambioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (intMes == -1)
                return;
            if (lstComprobantes.Count > 0)
            {
                if (XtraMessageBox.Show("¿Está seguro que desea ejecutar el Proceso de Diferencia de Cambio para el mes de " + LkpMeses.Text + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    opcionProceso = "DIFERENCIA";
                    controlEnable(true);
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }


        string strMensaje = "";

        private void calcularDiferenciaDeCambio()
        {
            try
            {
                decimal monto;
                var obj_Parametro = objContabilidadData.listarParametroContable()[0];
                #region Eliminar Comprobante Previo
                foreach (var _bee in lstComprobantes.Where(x => x.mesec_iid_mes == Convert.ToInt32(LkpMeses.EditValue) && x.subdi_icod_subdiario == Parametros.intSubDiarioDiferenciaCambio).ToList())
                {
                    EVoucherContableCab c = new EVoucherContableCab();
                    c.vcocc_icod_vcontable = _bee.vcocc_icod_vcontable;
                    objContabilidadData.eliminarVoucherContableCab(_bee);

                }

                #endregion
                #region Tipo de Cambio
                List<ETipoCambioMensual> lstTipoCambio = new BGeneral().ListarTipoCambioMensual().Where(x =>
                      x.tcamm_iid_anio == Parametros.intEjercicio).ToList();
                ETipoCambioMensual obj_TipoCambio = new ETipoCambioMensual();
                if (lstTipoCambio.Where(x => x.mesec_iid_mes == intMes).ToList().Count > 0)
                    obj_TipoCambio = lstTipoCambio.Where(x => x.mesec_iid_mes == intMes).ToList()[0];
                else
                    throw new ArgumentException("Tipo de Cambio Mensual no registrado\nNota: El Proceso de Diferencia de Cambio no se puede completar sin el Tipo de Cambio");

                #endregion
                #region Listas de las Cuentas para la Diferencia de Cambio
                //if (intMes==1)
                //{
                lst01 = new BContabilidad().listarDiferenciaCambio_3(Parametros.intEjercicio, intMes, 0, 59);
                //}
                //else if (intMes!=1)
                //{
                //    lst01 = new BContabilidad().listarDiferenciaCambio(Parametros.intEjercicio, intMes, 0, 59); 
                //}
                lst02 = new BContabilidad().listarDiferenciaCambio_2(Parametros.intEjercicio, intMes, 0, 59);
                #endregion
                #region Calcular Acumulado de las Cuentas
                lst01.ForEach(x =>
                {
                    /********************ACUMULADO CUENTAS DEBE / HABER**********************/
                    //x.vcocd_nmto_tot_debe_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable
                    //    && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica
                    //    && a.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList().Sum(y => y.vcocd_nmto_tot_debe_sol);

                    x.vcocd_nmto_tot_debe_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable
                      && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica
                     ).ToList().Sum(y => y.vcocd_nmto_tot_debe_sol);
                    /*------------------------------------------------------------*/
                    //x.vcocd_nmto_tot_haber_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable
                    //    && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica
                    //    && a.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList().Sum(y => y.vcocd_nmto_tot_haber_sol);

                    x.vcocd_nmto_tot_haber_sol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable
                    && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica
                    ).ToList().Sum(y => y.vcocd_nmto_tot_haber_sol);
                    /*-----------------------*/

                    //x.vcocd_nmto_tot_debe_dol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable
                    //    && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica
                    //    && a.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList().Sum(y => y.vcocd_nmto_tot_debe_dol);

                    x.vcocd_nmto_tot_debe_dol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable
                      && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica
                      ).ToList().Sum(y => y.vcocd_nmto_tot_debe_dol);

                    /*---------------------------------*/

                    //x.vcocd_nmto_tot_haber_dol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable
                    //    && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica
                    //    && a.cecoc_icod_centro_costo == x.cecoc_icod_centro_costo).ToList().Sum(y => y.vcocd_nmto_tot_haber_dol);

                    x.vcocd_nmto_tot_haber_dol = lst02.Where(a => a.ctacc_icod_cuenta_contable == x.ctacc_icod_cuenta_contable
                      && a.tablc_iid_tipo_analitica == x.tablc_iid_tipo_analitica && a.anad_icod_analitica == x.anad_icod_analitica
                      ).ToList().Sum(y => y.vcocd_nmto_tot_haber_dol);

                    /********************SALDO ACTUAL**********************/
                    x.dblCuentaSaldoActSol = x.dblCuentaSaldoAntSol + (Convert.ToDecimal(x.vcocd_nmto_tot_debe_sol) - Convert.ToDecimal(x.vcocd_nmto_tot_haber_sol));
                    x.dblCuentaSaldoActDol = x.dblCuentaSaldoAntDol + (Convert.ToDecimal(x.vcocd_nmto_tot_debe_dol) - Convert.ToDecimal(x.vcocd_nmto_tot_haber_dol));
                });
                #endregion
                #region Calculando Diferencia de Cambio Parte 01
                lst01.ForEach(x =>
                {
                    x.vcocd_nmto_tot_debe_sol = 0;
                    x.vcocd_nmto_tot_haber_sol = 0;
                    x.vcocd_nmto_tot_debe_dol = 0;
                    x.vcocd_nmto_tot_haber_dol = 0;
                    /*--------------------------*/
                    x.vcocd_nmto_tot_debe_dol = 0;
                    x.vcocd_nmto_tot_debe_sol = 0;
                    x.vcocd_nmto_tot_haber_dol = 0;
                    x.vcocd_nmto_tot_haber_sol = 0;
                    if (x.ctacc_icod_cuenta_contable == 0)
                    {

                    }
                    /*--------------------------*/

                    //primer caso
                    if (x.ctacc_icod_cuenta_contable == 0 && x.ctacc_icod_cuenta_contable < 0)
                    {
                        x.vcocd_nmto_tot_debe_sol = x.dblCuentaSaldoActSol * -1;
                        x.vcocd_nmto_tot_haber_sol = 0;
                    }
                    else if (x.dblCuentaSaldoActDol == 0 && x.dblCuentaSaldoActSol > 0)
                    {
                        x.vcocd_nmto_tot_debe_sol = 0;
                        x.vcocd_nmto_tot_haber_sol = x.dblCuentaSaldoActSol;
                    }
                    /*Agreado Nuevo*/
                    else if (x.dblCuentaSaldoActDol == 0 && x.dblCuentaSaldoActSol < 0)
                    {
                        x.vcocd_nmto_tot_debe_sol = 0;
                        x.vcocd_nmto_tot_haber_sol = x.dblCuentaSaldoActSol;
                    }

                    //segundo caso
                    if (x.dblCuentaSaldoActSol > 0)
                    {
                        monto = (Convert.ToDecimal(x.dblCuentaSaldoActDol) * obj_TipoCambio.tcamm_ntipo_cambio_compra) -
                            Convert.ToDecimal(x.dblCuentaSaldoActSol);

                        if (monto > 0)
                        {
                            x.vcocd_nmto_tot_debe_sol = monto;
                            x.vcocd_nmto_tot_haber_sol = 0;
                        }
                        if (monto < 0)
                        {
                            x.vcocd_nmto_tot_debe_sol = 0;
                            x.vcocd_nmto_tot_haber_sol = monto * -1;
                        }
                    }
                    if (x.dblCuentaSaldoActDol < 0)
                    {
                        monto = (Convert.ToDecimal(x.dblCuentaSaldoActDol) * obj_TipoCambio.tcamm_ntipo_cambio_venta) -
                            Convert.ToDecimal(x.dblCuentaSaldoActSol);
                        if (monto > 0)
                        {
                            x.vcocd_nmto_tot_debe_sol = monto;
                            x.vcocd_nmto_tot_haber_sol = 0;
                        }
                        if (monto < 0)
                        {
                            x.vcocd_nmto_tot_debe_sol = 0;
                            x.vcocd_nmto_tot_haber_sol = monto * -1;
                        }
                    }
                });

                #endregion
                #region Armando Detalle del Comprobante de la Diferencia de Cambio
                EVoucherContableCab obj_DiferenciaCab = new EVoucherContableCab();
                ECuentaContable objCta = new ECuentaContable();

                decimal monto2 = 0;

                lst01.ForEach(x =>
                {
                    monto2 = Convert.ToDecimal(x.vcocd_nmto_tot_debe_sol) - Convert.ToDecimal(x.vcocd_nmto_tot_haber_sol);
                    if (monto2 != 0)
                    {
                        EVoucherContableDet obj = new EVoucherContableDet();
                        x.tdocc_icod_tipo_doc = 56;
                        x.vcocd_numero_doc = "AJUSTE";
                        x.vcocd_vglosa_linea = "D/C " + x.ctacc_icod_cuenta_contable + " " + String.Format("{0:00}", x.tablc_iid_tipo_analitica) + "." + x.anad_icod_analitica;
                        x.vcocd_flag_estado = true;
                        x.intTipoOperacion = Parametros.intOperacionNuevo;
                        x.vcocd_nmto_tot_debe_dol = 0;
                        x.vcocd_nmto_tot_haber_dol = 0;
                        x.ctacc_iid_cuenta_contable_ref = null;
                        x.vcocd_tipo_cambio = 0;
                        /***************************************************************/
                        obj.tdocc_icod_tipo_doc = 56;
                        obj.vcocd_numero_doc = "AJUSTE";
                        obj.vcocd_vglosa_linea = "D/C " + x.ctacc_icod_cuenta_contable + " " + string.Format("{0:00}", x.tablc_iid_tipo_analitica) + "." + x.anad_icod_analitica;
                        obj.vcocd_flag_estado = true;
                        obj.intTipoOperacion = Parametros.intOperacionNuevo;
                        obj.vcocd_nmto_tot_debe_dol = 0;
                        obj.vcocd_nmto_tot_haber_dol = 0;
                        obj.ctacc_iid_cuenta_contable_ref = null;
                        obj.vcocd_tipo_cambio = 0;

                        if (monto2 > 0)
                        {
                            x.vcocd_nmto_tot_debe_sol = monto2;
                            x.vcocd_nmto_tot_haber_sol = 0;

                            obj.vcocd_nmto_tot_debe_sol = 0;
                            obj.vcocd_nmto_tot_haber_sol = monto2;

                            if (Convert.ToInt32(obj_Parametro.parac_id_cta_gdifc_mn) == 0)
                                throw new ArgumentException("No de encotró cuenta contable de ganancia por diferencia de cambio, favor de registrar cuenta en Parámetros Contables");
                            obj.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_Parametro.parac_id_cta_gdifc_mn);

                            objCta = lstCuentaContable.Where(y => y.ctacc_icod_cuenta_contable == obj_Parametro.parac_id_cta_gdifc_mn).ToList()[0];
                            //objCta = lstCuentaContable.ToList()[0];
                            if (objCta.ctacc_iccosto)
                            {
                                if (Convert.ToInt32(obj_Parametro.parac_id_ccosto_difc) == 0)
                                    throw new ArgumentException("No de encotró centro de costo por diferencia de cambio, favor de registrar centro de costo en Parámetros Contables");

                                obj.cecoc_icod_centro_costo = obj_Parametro.parac_id_ccosto_difc;
                            }
                            if (Convert.ToInt32(objCta.tablc_iid_tipo_analitica) > 0)
                            {
                                obj.tablc_iid_tipo_analitica = x.tablc_iid_tipo_analitica;
                                obj.anad_icod_analitica = x.anad_icod_analitica;
                            }
                            lstDiferencia.Add(x);
                            lstDiferencia.Add(obj);

                            if (objCta.ctacc_icod_cuenta_debe_auto != null)
                            {
                                obj.ctacc_icod_cuenta_debe_auto = objCta.ctacc_icod_cuenta_debe_auto;
                                obj.ctacc_icod_cuenta_haber_auto = objCta.ctacc_icod_cuenta_haber_auto;
                                lstDiferencia = AddAutomatic(lstDiferencia, obj, monto2);
                            }
                        }
                        if (monto2 < 0)
                        {
                            x.vcocd_nmto_tot_debe_sol = 0;
                            x.vcocd_nmto_tot_haber_sol = monto2 * -1;

                            obj.vcocd_nmto_tot_debe_sol = monto2 * -1;
                            obj.vcocd_nmto_tot_haber_sol = 0;

                            if (Convert.ToInt32(obj_Parametro.parac_id_cta_pdifc_mn) == 0)
                                throw new ArgumentException("No de encotró cuenta contable de perdida por diferencia de cambio, favor de registrar cuenta en Parámetros Contables");
                            obj.ctacc_icod_cuenta_contable = Convert.ToInt32(obj_Parametro.parac_id_cta_pdifc_mn);
                            objCta = lstCuentaContable.Where(y => y.ctacc_icod_cuenta_contable == obj_Parametro.parac_id_cta_pdifc_mn).ToList()[0];
                            //objCta = lstCuentaContable.ToList()[0];
                            if (objCta.ctacc_iccosto)
                            {
                                if (Convert.ToInt32(obj_Parametro.parac_id_ccosto_difc) == 0)
                                    throw new ArgumentException("No de encotró centro de costo por diferencia de cambio, favor de registrar centro de costo en Parámetros Contables");

                                obj.cecoc_icod_centro_costo = obj_Parametro.parac_id_ccosto_difc;
                            }
                            if (Convert.ToInt32(objCta.tablc_iid_tipo_analitica) > 0)
                            {
                                obj.tablc_iid_tipo_analitica = x.tablc_iid_tipo_analitica;
                                obj.anad_icod_analitica = x.anad_icod_analitica;
                            }
                            lstDiferencia.Add(x);
                            lstDiferencia.Add(obj);

                            if (objCta.ctacc_icod_cuenta_debe_auto != null)
                            {
                                obj.ctacc_icod_cuenta_debe_auto = objCta.ctacc_icod_cuenta_debe_auto;
                                obj.ctacc_icod_cuenta_haber_auto = objCta.ctacc_icod_cuenta_haber_auto;
                                lstDiferencia = AddAutomatic(lstDiferencia, obj, monto2 * -1);
                            }
                        }
                    }

                });
                #endregion
                #region Armando Cabecera del Comprobante de la Diferencia de Cambio
                if (lstDiferencia.Count > 0)
                {
                    int nro_item = 0;
                    lstDiferencia.ForEach(x =>
                    {
                        nro_item += 1;
                        x.vcocd_nro_item_det = nro_item;
                    });
                    strMensaje = "El proceso de DIFERENCIA DE CAMBIO ha sido ejecutado satisfactoriamente";
                    obj_DiferenciaCab.anioc_iid_anio = Parametros.intEjercicio;
                    obj_DiferenciaCab.mesec_iid_mes = intMes;
                    if (Convert.ToInt32(obj_Parametro.parac_id_sd_difc_mn) == 0)
                        throw new ArgumentException("No de encotró subdiario por diferencia de cambio, favor de registrar subdiario en Parámetros Contables");
                    obj_DiferenciaCab.subdi_icod_subdiario = Convert.ToInt32(obj_Parametro.parac_id_sd_difc_mn);
                    if (obj_Parametro.parac_nro_comp_difc_mn == "")
                        throw new ArgumentException("No de encotró Nro. de Voucher por diferencia de cambio, favor de registrar Nro. de Voucher en Parámetros Contables");
                    obj_DiferenciaCab.vcocc_numero_vcontable = obj_Parametro.parac_nro_comp_difc_mn;
                    obj_DiferenciaCab.tablc_iid_moneda = Parametros.intTipoMonedaHistorico;
                    obj_DiferenciaCab.tarec_icorrelativo_origen_vcontable = Parametros.intOriVcoPorDiferenciaCambio;
                    obj_DiferenciaCab.vcocc_glosa = "ASIENTO POR DIFERENCIA DE CAMBIO";
                    if (intMes == 12)
                        obj_DiferenciaCab.vcocc_fecha_vcontable = new DateTime(Parametros.intEjercicio + 1, 1, 1).AddDays(-1);
                    else
                        obj_DiferenciaCab.vcocc_fecha_vcontable = new DateTime(Parametros.intEjercicio, intMes + 1, 1).AddDays(-1);
                    obj_DiferenciaCab.intUsuario = Modules.Valores.intUsuario;
                    obj_DiferenciaCab.strPc = WindowsIdentity.GetCurrent().Name;
                    obj_DiferenciaCab.vcocc_nmto_tot_debe_sol = lstDiferencia.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => Convert.ToDecimal(x.vcocd_nmto_tot_debe_sol));
                    obj_DiferenciaCab.vcocc_nmto_tot_haber_sol = lstDiferencia.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => Convert.ToDecimal(x.vcocd_nmto_tot_haber_sol));
                    obj_DiferenciaCab.vcocc_nmto_tot_debe_dol = lstDiferencia.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => Convert.ToDecimal(x.vcocd_nmto_tot_debe_dol));
                    obj_DiferenciaCab.vcocc_nmto_tot_haber_dol = lstDiferencia.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => Convert.ToDecimal(x.vcocd_nmto_tot_haber_dol));

                    if (lstDiferencia.Sum(x => x.vcocd_nmto_tot_debe_sol) == lstDiferencia.Sum(x => x.vcocd_nmto_tot_haber_sol) &&
                            lstDiferencia.Sum(x => x.vcocd_nmto_tot_debe_dol) == lstDiferencia.Sum(x => x.vcocd_nmto_tot_haber_dol))
                        obj_DiferenciaCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                    else
                        obj_DiferenciaCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                    if (lstDiferencia.Count == 0)
                        obj_DiferenciaCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;

                    objContabilidadData.insertarVoucherContableCab(obj_DiferenciaCab, lstDiferencia);
                    lstDiferencia = new List<EVoucherContableDet>();
                }
                else
                {
                    strMensaje = "El proceso no ha encontrado DIFERENCIA DE CAMBIO para el mes de " + strMes;
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<EVoucherContableDet> AddAutomatic(List<EVoucherContableDet> Lista, EVoucherContableDet ob, decimal monto)
        {
            ECuentaContable CtaAux = new ECuentaContable();
            for (int x = 0; x < 2; x++)
            {
                EVoucherContableDet obj = new EVoucherContableDet();
                obj.tdocc_icod_tipo_doc = ob.tdocc_icod_tipo_doc;
                obj.vcocd_numero_doc = ob.vcocd_numero_doc;
                obj.vcocd_nmto_tot_debe_dol = 0;
                obj.vcocd_nmto_tot_haber_dol = 0;

                if (x == 0)
                {
                    obj.ctacc_icod_cuenta_contable = Convert.ToInt32(ob.ctacc_icod_cuenta_debe_auto);
                    obj.vcocd_nmto_tot_debe_sol = monto;
                    /*-------------------------------------------------------------------*/
                    if (lstCuentaContable.Where(y => y.ctacc_icod_cuenta_contable == obj.ctacc_icod_cuenta_contable).ToList().Count == 0)
                        throw new ArgumentException("Cuenta Contable Automática, no figura en la lista de SUBCUENTAS");
                    CtaAux = lstCuentaContable.Where(y => y.ctacc_icod_cuenta_contable == obj.ctacc_icod_cuenta_contable).ToList()[0];
                    /*-------------------------------------------------------------------*/
                    if (Convert.ToInt32(CtaAux.tablc_iid_tipo_analitica) > 0)
                    {
                        obj.tablc_iid_tipo_analitica = ob.tablc_iid_tipo_analitica;
                        obj.anad_icod_analitica = ob.anad_icod_analitica;
                    }
                    if (CtaAux.ctacc_iccosto)
                    {
                        obj.cecoc_icod_centro_costo = ob.cecoc_icod_centro_costo;
                    }
                    /*-------------------------------------------------------------------*/
                }
                if (x == 1)
                {
                    obj.ctacc_icod_cuenta_contable = Convert.ToInt32(ob.ctacc_icod_cuenta_haber_auto);
                    obj.vcocd_nmto_tot_haber_sol = monto;
                    /*-------------------------------------------------------------------*/
                    if (lstCuentaContable.Where(y => y.ctacc_icod_cuenta_contable == obj.ctacc_icod_cuenta_contable).ToList().Count == 0)
                        throw new ArgumentException("Cuenta Contable Automática, no figura en la lista de SUBCUENTAS");
                    CtaAux = lstCuentaContable.Where(y => y.ctacc_icod_cuenta_contable == obj.ctacc_icod_cuenta_contable).ToList()[0];
                    /*-------------------------------------------------------------------*/
                    if (Convert.ToInt32(CtaAux.tablc_iid_tipo_analitica) > 0)
                    {
                        obj.tablc_iid_tipo_analitica = obj.tablc_iid_tipo_analitica;
                        obj.anad_icod_analitica = obj.anad_icod_analitica;
                    }
                    if (CtaAux.ctacc_iccosto)
                    {
                        obj.cecoc_icod_centro_costo = ob.cecoc_icod_centro_costo;
                    }
                    /*-------------------------------------------------------------------*/
                }
                obj.vcocd_vglosa_linea = ob.vcocd_vglosa_linea;
                obj.vcocd_flag_estado = true;
                obj.intTipoOperacion = Parametros.intOperacionNuevo;
                obj.ctacc_iid_cuenta_contable_ref = ob.ctacc_icod_cuenta_contable;
                Lista.Add(obj);
            }
            return Lista;
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (intMes == -1)
                return;
            limpiarLista();
            controlEnable(true);
            backgroundWorker2.RunWorkerAsync();
        }

        private void eliminarDiferenciaDeCambioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstComprobantes.Where(x => x.tarec_icorrelativo_origen_vcontable == Parametros.intOriVcoPorDiferenciaCambio).ToList().Count == 1)
            {
                if (XtraMessageBox.Show("¿Está seguro que desea eliminar el comprobante por Diferencia de Cambio del mes " + LkpMeses.Text + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    EVoucherContableCab Obe = lstComprobantes.Where(x => x.mesec_iid_mes == Convert.ToInt32(LkpMeses.EditValue) && x.tarec_icorrelativo_origen_vcontable == Parametros.intOriVcoPorDiferenciaCambio).ToList()[0];
                    objContabilidadData.eliminarVoucherContableCab(Obe);
                    viewComprobante.RefreshData();
                    //controlEnable(true);
                    //backgroundWorker2.RunWorkerAsync();
                }
            }
            else
                XtraMessageBox.Show("No existe comprobante por Diferencia de Cambio registrado para este mes", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void mnuComprobantes_Opened(object sender, EventArgs e)
        {
            EVoucherContableCab Obe = (EVoucherContableCab)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            if (Obe != null)
            {
                if (Obe.tbl_origen_icod == null)
                    verDocumentoOriginalToolStripMenuItem.Enabled = false;
                else
                    verDocumentoOriginalToolStripMenuItem.Enabled = true;
            }
        }
        private void verDocumentoOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void controlEnable(bool Enable)
        {
            panel1.Visible = Enable;
            LkpMeses.Enabled = !Enable;
            txtCodigo.Enabled = !Enable;
            txtDescripcion.Enabled = !Enable;
            mnuComprobantes.Enabled = !Enable;
            btnBuscar.Enabled = !Enable;
            dgrComprobante.Enabled = !Enable;
        }
        private void limpiarLista()
        {
            lstComprobantes.Clear();
            viewComprobante.RefreshData();
            viewComprobante.GroupPanelText = "Resultado de la Búsqueda";
        }
        #region BackgroundWorkers
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (opcionProceso == "REDONDEO")
                {
                    eliminarActualizarRedondeos();
                    RedondeoComprobante();

                }
                else if (opcionProceso == "DIFERENCIA")
                {
                    calcularDiferenciaDeCambio();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundWorker2.RunWorkerAsync();
            controlEnable(false);
            dgrComprobante.DataSource = lstComprobantes;
            viewComprobante.GroupPanelText = String.Format("COMPROBANTES MES : {0}", strMes.ToUpper());
            viewComprobante.Focus();

            if (opcionProceso == "REDONDEO")
                XtraMessageBox.Show("El proceso de REDONDEO ha sido ejecutado satisfactoriamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (opcionProceso == "DIFERENCIA")
                XtraMessageBox.Show(strMensaje, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        /*****************************************************************************************************************/
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (LkpMeses.EditValue != null)
            {
                intMes = Convert.ToInt32(LkpMeses.EditValue);
                strMes = LkpMeses.Text;
                limpiarLista();
                cargar();
                //controlEnable(true);
                //backgroundWorker2.RunWorkerAsync();
            }
        }
        /*****************************************************************************************************************/
        private void eliminarActualizarRedondeos()
        {
            new BContabilidad().eliminarRedondeoVoucher(Parametros.intEjercicio, intMes);
            cargar();
        }

        private void viewComprobante_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["strVcoSituacion"]);
                if (strSituacion == "No Cuadrado")
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;

                }
            }
        }

        private void LkpMeses_EditValueChanged(object sender, EventArgs e)
        {
            if (LkpMeses.EditValue != null)
            {
                intMes = Convert.ToInt32(LkpMeses.EditValue);
                strMes = LkpMeses.Text;
                limpiarLista();
                cargar();
                dgrComprobante.DataSource = lstComprobantes;
                viewComprobante.Focus();
                viewComprobante.GroupPanelText = String.Format("COMPROBANTES MES : {0}", strMes.ToUpper());
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnCalculadora_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewComprobante.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewComprobante.ClearColumnsFilter();
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EVoucherContableCab Obe = (EVoucherContableCab)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            reload(Obe.vcocc_icod_vcontable);
        }
    }
}



