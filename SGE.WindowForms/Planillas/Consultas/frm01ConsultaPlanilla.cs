using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Security.Principal;
using SGE.Entity;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using SGE.WindowForms.Reportes.Almacen.Registros;
using SGE.WindowForms.Otros.Planillas;

namespace SGE.WindowForms.Planillas.Consultas
{
    public partial class frm01ConsultaPlanilla : DevExpress.XtraEditors.XtraForm
    {     
        private List<ENotaIngreso> lstNotaIngreso = new List<ENotaIngreso>();

        private List<EPlanillaPersonal> lstPlanillaPersonal = new List<EPlanillaPersonal>();
        public frm01ConsultaPlanilla()
        {
            InitializeComponent();
        }

        private void FrmRegistroNotaIngreso_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstPlanillaPersonal = new BPlanillas().listarPlanillaPersonal().Where(m => m.tablc_iid_situacion_planilla == 6434).ToList();
            grdPlanillaPersonal.DataSource = lstPlanillaPersonal;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstPlanillaPersonal.FindIndex(x => x.planc_icod_planilla_personal == intIcod);
            viewPlanillaPersonal.FocusedRowHandle = index;
            viewPlanillaPersonal.Focus();
        }      
      
        private void viewNotaIngreso_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                EPlanillaPersonal Obe = (EPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmMantePlanillaPersonal frm = new frmMantePlanillaPersonal();
                frm.MiEvento += new frmMantePlanillaPersonal.DelegadoMensaje(reload);
                frm.oBe = Obe;
                frm.SetCancel();
                frm.Show();
                frm.setValues();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void nuevo()
        {
            try
            {
                frmMantePlanillaPersonal frm = new frmMantePlanillaPersonal();
                frm.MiEvento += new frmMantePlanillaPersonal.DelegadoMensaje(reload);
                frm.nuevoToolStripMenuItem.Visible = false;
                frm.eliminarToolStripMenuItem.Visible = false;
                frm.cCostosToolStripMenuItem.Visible = false;
                frm.datosToolStripMenuItem.Visible = false;
                frm.calcularToolStripMenuItem.Visible = false;
                frm.SetInsert();
                frm.Show();                
                if (lstPlanillaPersonal.Count == 0)
                {
                    frm.txtNumPlanilla.Text = "00001";
                }
                else
                {
                    frm.txtNumPlanilla.Text = String.Format("{0:00000}", (lstPlanillaPersonal.Max(ob => Convert.ToInt32(ob.planc_iid_planilla_personal)) + 1));
                }
                frm.lkpMes.Enabled = true;
                frm.lkpTipo.Enabled = true;
                frm.lkpSituacion.Enabled = false;
           
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar()
        {
            try
            {

                EPlanillaPersonal Obe = (EPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmMantePlanillaPersonal frm = new frmMantePlanillaPersonal();
                frm.MiEvento += new frmMantePlanillaPersonal.DelegadoMensaje(reload);
                frm.nuevoToolStripMenuItem.Visible = true;
                frm.eliminarToolStripMenuItem.Visible = true;
                frm.cCostosToolStripMenuItem.Visible = false;
                frm.datosToolStripMenuItem.Visible = false;
                frm.calcularToolStripMenuItem.Visible = true;
                frm.oBe = Obe;
                frm.SetModify();
                frm.Show();
                frm.setValues();
                frm.btnBuscar.Enabled = false;
                frm.lkpMes.Enabled = false;
                frm.lkpTipo.Enabled = false;
                frm.lkpSituacion.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void eliminar()
        {
            try
            {
                EPlanillaPersonal Obe = (EPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewPlanillaPersonal.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar Planilla Personal " + Obe.NumPlanilla + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarPlanillaPersonal(Obe);
                    cargar();
                    if (lstPlanillaPersonal.Count >= index + 1)
                        viewPlanillaPersonal.FocusedRowHandle = index;
                    else
                        viewPlanillaPersonal.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Cierre()
        {
            EPlanillaPersonal obe = (EPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
            if (obe == null)
                return;

            if (obe.tablc_iid_situacion_planilla==6433)
            {
                obe.tablc_iid_situacion_planilla = 6434;
                if (XtraMessageBox.Show("¿Esta seguro que desea CERRAR Planilla Personal " + obe.NumPlanilla + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new BPlanillas().modificarPlanillaPersonalSituacion(obe);
                }
            }
            else
            {
                obe.tablc_iid_situacion_planilla = 6433;
                if (XtraMessageBox.Show("¿Esta seguro que desea APERTURAR Planilla Personal " + obe.NumPlanilla + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new BPlanillas().modificarPlanillaPersonalSituacion(obe);
                }
            }
            
            reload(obe.planc_icod_planilla_personal);            
            viewPlanillaPersonal.Focus();

           
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaPersonal obe = (EPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.tablc_iid_situacion_planilla==6434)
            {
                XtraMessageBox.Show(" No se puede Modificar si esta CERRADO la  Planilla Personal " + obe.NumPlanilla + "?", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                modificar();
            }
           
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void CierreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cierre();
        }  

        private void lkpAlmacen_EditValueChanged(object sender, EventArgs e)
        {
            cargar();
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

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewPlanillaPersonal.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewPlanillaPersonal.ClearColumnsFilter();
        }

        private void importarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaPersonal obe = (EPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
            try
            {
            
                List<EPlanillaPersonalDetalle> lstPlanillaDetalle = new List<EPlanillaPersonalDetalle>();
                lstPlanillaDetalle = new BPlanillas().listarPlanillaPersonalDetalle(obe.planc_icod_planilla_personal);
                grdRemuneracion.DataSource = lstPlanillaDetalle;               

                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {
                    grdRemuneracion.DataSource = lstPlanillaDetalle;//.OrderBy(obe => obe.strNroCuenta).ThenBy(obe => obe.anac_cecoc_tipo).ThenBy(obe => obe.anac_cecoc_code).ThenBy(obe => obe.fec_cab).ToList();
                    string fileName = sfdRuta.FileName;
                    if (!fileName.Contains(".xlsx"))
                    {
                        grdRemuneracion.ExportToXlsx(fileName + ".xlsx");
                        System.Diagnostics.Process.Start(fileName + ".xlsx");
                    }
                    else
                    {
                        grdRemuneracion.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                    grdRemuneracion.DataSource = null;
                    sfdRuta.FileName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void grdDetalle_Click(object sender, EventArgs e)
        {

        }

        private void consultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EPlanillaPersonal Obe = (EPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmMantePlanillaPersonal frm = new frmMantePlanillaPersonal();
                frm.MiEvento += new frmMantePlanillaPersonal.DelegadoMensaje(reload);
                frm.oBe = Obe;
                frm.SetCancel();
                frm.Show();
                frm.setValues();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportarAFPNETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaPersonal obe = (EPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
            try
            {
                List<EPlanillaPersonalDetalle> lstPlanillaDetalleNew = new List<EPlanillaPersonalDetalle>();
                List<EPlanillaPersonalDetalle> lstPlanillaDetalle = new List<EPlanillaPersonalDetalle>();
                lstPlanillaDetalle = new BPlanillas().listarPlanillaPersonalDetalle(obe.planc_icod_planilla_personal);
                foreach (var item in lstPlanillaDetalle)
                {
                    List<EPersonal> lstPersonal = new List<EPersonal>();
                    lstPersonal = new BPlanillas().listarPersonal().Where(p => p.perc_icod_personal == item.pland_icod_personal).ToList();
                    if (lstPersonal[0].perc_icod_tip_fdo_pension==6384)
                    {
                        item.afp = 1;
                        if (lstPersonal[0].tbpd_icod_tip_doc==21)//*DNI*//
                        {
                            item.TipoDoc = 0;
                        }
                        else if (lstPersonal[0].tbpd_icod_tip_doc == 24)//*PASAPORTE*//
                        {
                            item.TipoDoc = 4;
                        }
                        else if (lstPersonal[0].tbpd_icod_tip_doc == 22)//*C.E.(22)*//
                        {
                            item.TipoDoc = 1;
                        }
                        item.ApellPat = lstPersonal[0].perc_vapellido_pat;
                        item.ApellMat = lstPersonal[0].perc_vapellido_mat;
                        item.Nombres = lstPersonal[0].perc_vnombres;
                        item.RelacionLaboral = "S";
                        int DiasporMes = 0;
                        DiasporMes = System.DateTime.DaysInMonth(Parametros.intEjercicio, Convert.ToInt32(obe.mesec_iid_mes));
                        List<EPersonal> lstContrato = new List<EPersonal>();
                        lstContrato=new BPlanillas().listarPersonal_contratacion(item.pland_icod_personal);
                        foreach (var PER in lstContrato)
                        {

                            //if (PER.pctd_sfecha_ini_contrato >= Convert.ToDateTime("01-" + obe.mesec_iid_mes + "-" + Parametros.intEjercicio + "") && PER.pctd_sfecha_fin_contrato <= Convert.ToDateTime("" + DiasporMes + "-" + obe.mesec_iid_mes + "-" + Parametros.intEjercicio + ""))
                            if (Convert.ToDateTime("01-" + obe.mesec_iid_mes + "-" + Parametros.intEjercicio + "") < PER.pctd_sfecha_ini_contrato && PER.pctd_sfecha_ini_contrato <= Convert.ToDateTime("" + DiasporMes + "-" + obe.mesec_iid_mes + "-" + Parametros.intEjercicio + ""))
                            {
                                if (PER.pctd_sfecha_ini_contrato >= Convert.ToDateTime("01-" + obe.mesec_iid_mes + "-" + Parametros.intEjercicio + "") && PER.pctd_sfecha_ini_contrato <= Convert.ToDateTime("" + DiasporMes + "-" + obe.mesec_iid_mes + "-" + Parametros.intEjercicio + ""))
                                {
                                    item.InicioRL = "S";
                                }
                                
                            }
                            else
                            {
                                item.InicioRL = "N";
                            }


                            if (Convert.ToDateTime("01-" + obe.mesec_iid_mes + "-" + Parametros.intEjercicio + "") < PER.pctd_sfecha_cese && PER.pctd_sfecha_cese <= Convert.ToDateTime("" + DiasporMes + "-" + obe.mesec_iid_mes + "-" + Parametros.intEjercicio + ""))
                            {
                             item.CeseRL = "S";                            
                            }
                            else
                            {
                             item.CeseRL = "N";
                            }
                           
                        }
                        item.RemuneracionAsegurable = item.pland_nrem_computable;
                        item.AporteVoluntarioCon = 0;
                        item.AporteVoluntarioSin = 0;
                        item.AporteVoluntarioEmpleador = 0;
                        item.TipoTrabajador = "N";

                        lstPlanillaDetalleNew.Add(item);
                    }
                    
                }

                grdRemuneracion.DataSource = lstPlanillaDetalleNew;

                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {
                    grdRemuneracion.DataSource = lstPlanillaDetalleNew;//.OrderBy(obe => obe.strNroCuenta).ThenBy(obe => obe.anac_cecoc_tipo).ThenBy(obe => obe.anac_cecoc_code).ThenBy(obe => obe.fec_cab).ToList();
                    string fileName = sfdRuta.FileName;
                    if (!fileName.Contains(".xlsx"))
                    {
                        grdRemuneracion.ExportToXlsx(fileName + ".xlsx");
                        System.Diagnostics.Process.Start(fileName + ".xlsx");
                    }
                    else
                    {
                        grdRemuneracion.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                    grdRemuneracion.DataSource = null;
                    sfdRuta.FileName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void exportarTeleCréditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaPersonal obe = (EPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
            try
            {
                List<EPlanillaPersonalDetalle> lstPlanillaDetalleNew = new List<EPlanillaPersonalDetalle>();
                List<EPlanillaPersonalDetalle> lstPlanillaDetalle = new List<EPlanillaPersonalDetalle>();
                lstPlanillaDetalle = new BPlanillas().listarPlanillaPersonalDetalle(obe.planc_icod_planilla_personal);
                foreach (var item in lstPlanillaDetalle)
                {
                    List<EPersonal> lstPersonal = new List<EPersonal>();
                    lstPersonal = new BPlanillas().listarPersonal().Where(p => p.perc_icod_personal == item.pland_icod_personal).ToList();
                    //if (lstPersonal[0].perc_icod_tip_fdo_pension == 6384)
                    //{
                        item.TipoRegistro = "A";
                        if (lstPersonal[0].perc_icod_banc_haber==6407)
                        {
                           item.TipoCuenta = "A";
                        }
                        else
                        {
                            item.TipoCuenta = "B";
                        }
                        
                        item.CuentaAbono = lstPersonal[0].perc_vbanc_haber;
                        if (lstPersonal[0].tbpd_icod_tip_doc == 21)//*DNI*//
                        {
                            item.TipoDoc = 1;
                        }
                        else if (lstPersonal[0].tbpd_icod_tip_doc == 24)//*PASAPORTE*//
                        {
                            item.TipoDoc = 4;
                        }
                        else//*C.E.(22)*//
                        {
                            item.TipoDoc = 3;
                        }
                        item.TipoMoneda = "S";
                        item.ValidacionIDC = "S";
                        lstPlanillaDetalleNew.Add(item);
                    //}

                }

                grdTelecredito.DataSource = lstPlanillaDetalleNew;

                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {
                    grdTelecredito.DataSource = lstPlanillaDetalleNew;//.OrderBy(obe => obe.strNroCuenta).ThenBy(obe => obe.anac_cecoc_tipo).ThenBy(obe => obe.anac_cecoc_code).ThenBy(obe => obe.fec_cab).ToList();
                    string fileName = sfdRuta.FileName;
                    if (!fileName.Contains(".xlsx"))
                    {
                        grdTelecredito.ExportToXlsx(fileName + ".xlsx");
                        System.Diagnostics.Process.Start(fileName + ".xlsx");
                    }
                    else
                    {
                        grdTelecredito.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                    grdTelecredito.DataSource = null;
                    sfdRuta.FileName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     

            
    }
}