using DevExpress.XtraEditors;
using SGE.BusinessLogic;
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
    public partial class FrmRegistroCCosto : DevExpress.XtraEditors.XtraForm
    {
        public EPersonalCCostos oBe = new EPersonalCCostos();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<EPersonalCCostos> lstcCostosPersonal = new List<EPersonalCCostos>();
        List<EPersonalCCostos> lstDeletecCostosPersonal = new List<EPersonalCCostos>();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public int IcodPersonal = 0;
        public int anio = 0;
        public int mes = 0;
        public int Indicador = 0;
        public EProvisionPlanillaPersonal oBeCC = new EProvisionPlanillaPersonal();
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }
        public void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

            }
        }

        public void setValues()
        {


        }

        public FrmRegistroCCosto()
        {
            InitializeComponent();
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        private void cargar()
        {
            if (Indicador == 1)
            {
                lstcCostosPersonal = new BPlanillas().listarPersonalCCostos(IcodPersonal).Where(x => x.pccd_iaño == anio && x.pccd_imes == mes).ToList();
                grdAlmacen.DataSource = lstcCostosPersonal;
            }
            else
            {
                lstcCostosPersonal = new BPlanillas().listarPersonalCCostos(IcodPersonal);
                grdAlmacen.DataSource = lstcCostosPersonal;
            }

        }

        //private void setFecha(DateEdit fecha)
        //{
        //    if (DateTime.Now.Year == Parametros.intEjercicio)
        //        fecha.EditValue = DateTime.Now;
        //    else
        //        fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        //}

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmRegistroPersonalCcosto frm = new frmRegistroPersonalCcosto())
                {
                    frm.perc_vnum_doc = oBe.perc_vnum_doc;
                    frm.lstPersonalCC = lstcCostosPersonal;
                    frm.SetInsert();
                    //if (lstPersonalCC.Count == 0)
                    //{
                    //    frm.txtCodigo.Text = "0001";
                    //}
                    //else {
                    //    frm.txtCodigo = String.Format("{0:000}", (lstPersonalCC.Max(x => x.pccd_iid_ccosto)) +1);
                    //}
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstcCostosPersonal = frm.lstPersonalCC;
                        grdAlmacen.DataSource = lstcCostosPersonal;
                        grdAlmacen.Refresh();
                        grdAlmacen.RefreshDataSource();
                        viewAlmacen.RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {

                oBe.perc_icod_personal = IcodPersonal;
                oBe.pccd_flag_estado = true;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    //oBe.rqmc_icod_requerimiento_materiales = new BVentas().insertarRequerimientoMateriales(oBe, lstRequerimientoMaterialesDetalle);
                    new BPlanillas().insertarCCostos(oBe, lstcCostosPersonal);
                    CalcularMontoCC();

                }
                else
                {
                    new BPlanillas().modificarCCostos(oBe, lstcCostosPersonal, lstDeletecCostosPersonal);
                    CalcularMontoCC();
                }
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
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    //MiEvento(oBe.rqmc_icod_requerimiento_materiales);
                    Close();
                }
            }
        }

        private void FrmManteFactura_Load(object sender, EventArgs e)
        {
            cargar();


        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EPersonalCCostos obe = (EPersonalCCostos)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (obe != null)
                {
                    int index = viewAlmacen.FocusedRowHandle;
                    using (frmRegistroPersonalCcosto frm = new frmRegistroPersonalCcosto())
                    {
                        frm.perc_vnum_doc = obe.perc_vnum_doc;
                        frm.Obe = obe;
                        frm.lstPersonalCC = lstcCostosPersonal;
                        frm.SetModify();
                        frm.setValues();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            lstcCostosPersonal = frm.lstPersonalCC;
                            viewAlmacen.RefreshData();
                            viewAlmacen.FocusedRowHandle = index;
                            viewAlmacen.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ERequerimientoMaterialesDetalle obe = (ERequerimientoMaterialesDetalle)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            //if (obe == null)
            //    return;
            //lstDelete.Add(obe);
            //lstRequerimientoMaterialesDetalle.Remove(obe);
            //viewAlmacen.RefreshData();
        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }


        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                EPersonalCCostos obe = (EPersonalCCostos)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (obe != null)
                {
                    int index = viewAlmacen.FocusedRowHandle;

                    lstDeletecCostosPersonal.Add(obe);
                    lstcCostosPersonal.Remove(obe);
                    viewAlmacen.RefreshData();
                    viewAlmacen.FocusedRowHandle = index;
                    viewAlmacen.Focus();
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CalcularMontoCC()
        {
            List<EProvisionPlanillaPersonalDetalle> lstProvisionDetalleCC = new List<EProvisionPlanillaPersonalDetalle>();
            lstProvisionDetalleCC = new BPlanillas().listarProvisionPlanillaPersonalDetalle(oBeCC.planc_icod_planilla_personal);
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
                lstcCostosPersonalCC = new BPlanillas().listarPersonalCCostos(x.pland_icod_personal).Where(xc => xc.pccd_iaño == Parametros.intEjercicio && xc.pccd_imes == oBeCC.mesec_iid_mes).ToList();
                lstcCostosPersonalCC.ForEach(cc =>
                {
                    count++;
                    Monto = Convert.ToDecimal(x.pland_nmonto_gratificacion);
                    MontoDividir = Math.Round((Convert.ToDecimal(x.pland_nmonto_gratificacion) / lstcCostosPersonalCC.Count), 2, MidpointRounding.AwayFromZero);


                    if (count == lstcCostosPersonalCC.Count)
                    {
                        DiferenciaMonto = Monto - SumaMontos;
                        if (oBeCC.planc_iid_tipo_planilla == 6436)
                        {
                            cc.pccd_nmonto_vacaciones = DiferenciaMonto;
                            new BPlanillas().modificarPersonalCCostos(cc);
                        }
                        if (oBeCC.planc_iid_tipo_planilla == 6437)
                        {
                            cc.pccd_nmonto_gratificaciones = DiferenciaMonto;
                            new BPlanillas().modificarPersonalCCostos(cc);
                        }
                        if (oBeCC.planc_iid_tipo_planilla == 6438)
                        {
                            cc.pccd_nmonto_cts = DiferenciaMonto;
                            new BPlanillas().modificarPersonalCCostos(cc);
                        }
                    }
                    SumaMontos = SumaMontos + MontoDividir;
                    if (count == 1 || count != lstcCostosPersonalCC.Count)
                    {


                        if (oBeCC.planc_iid_tipo_planilla == 6436)
                        {
                            cc.pccd_nmonto_vacaciones = MontoDividir;
                            new BPlanillas().modificarPersonalCCostos(cc);
                        }
                        if (oBeCC.planc_iid_tipo_planilla == 6437)
                        {
                            cc.pccd_nmonto_gratificaciones = MontoDividir;
                            new BPlanillas().modificarPersonalCCostos(cc);
                        }
                        if (oBeCC.planc_iid_tipo_planilla == 6438)
                        {
                            cc.pccd_nmonto_cts = MontoDividir;
                            new BPlanillas().modificarPersonalCCostos(cc);
                        }

                    }


                });

            });

        }

        private void grdAlmacen_Click(object sender, EventArgs e)
        {

        }

    }
}