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

namespace SGE.WindowForms.Planillas.Procesos_de_Planilla
{
    public partial class frm10ProvisionPlanillaPersonal : DevExpress.XtraEditors.XtraForm
    {     
        private List<ENotaIngreso> lstNotaIngreso = new List<ENotaIngreso>();

        private List<EProvisionPlanillaPersonal> lstProvisionPlanillaPersonal = new List<EProvisionPlanillaPersonal>();
        public frm10ProvisionPlanillaPersonal()
        {
            InitializeComponent();
        }

        private void FrmRegistroNotaIngreso_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstProvisionPlanillaPersonal = new BPlanillas().listarProvisionPlanillaPersonal();
            grdPlanillaPersonal.DataSource = lstProvisionPlanillaPersonal;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstProvisionPlanillaPersonal.FindIndex(x => x.planc_icod_planilla_personal == intIcod);
            viewPlanillaPersonal.FocusedRowHandle = index;
            viewPlanillaPersonal.Focus();
        }      
      
        private void viewNotaIngreso_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                EProvisionPlanillaPersonal Obe = (EProvisionPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmManteProvisionPlanillaPersonal frm = new frmManteProvisionPlanillaPersonal();
                frm.MiEvento += new frmManteProvisionPlanillaPersonal.DelegadoMensaje(reload);
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
                frmManteProvisionPlanillaPersonal frm = new frmManteProvisionPlanillaPersonal();
                frm.MiEvento += new frmManteProvisionPlanillaPersonal.DelegadoMensaje(reload);
                frm.nuevoToolStripMenuItem.Visible = false;
                frm.eliminarToolStripMenuItem.Visible = false;
                frm.cCostosToolStripMenuItem.Visible = false;
                frm.calcularToolStripMenuItem.Visible = false;
                frm.duplicarToolStripMenuItem.Visible = false;
                frm.SetInsert();
                frm.Show();
                
                if (lstProvisionPlanillaPersonal.Count == 0)
                {
                    frm.txtNumPlanilla.Text = "00001";
                }
                else
                {
                    frm.txtNumPlanilla.Text = String.Format("{0:00000}", (lstProvisionPlanillaPersonal.Max(ob => Convert.ToInt32(ob.planc_iid_planilla_personal)) + 1));
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

                EProvisionPlanillaPersonal Obe = (EProvisionPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmManteProvisionPlanillaPersonal frm = new frmManteProvisionPlanillaPersonal();
                frm.MiEvento += new frmManteProvisionPlanillaPersonal.DelegadoMensaje(reload);
                frm.nuevoToolStripMenuItem.Visible = true;
                frm.eliminarToolStripMenuItem.Visible = true;
                frm.cCostosToolStripMenuItem.Visible = true;
                frm.calcularToolStripMenuItem.Visible = true;
                frm.duplicarToolStripMenuItem.Visible = false;
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
                EProvisionPlanillaPersonal Obe = (EProvisionPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewPlanillaPersonal.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar Planilla Personal " + Obe.NumPlanilla + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarProvisionPlanillaPersonal(Obe);
                    cargar();
                    if (lstProvisionPlanillaPersonal.Count >= index + 1)
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
            EProvisionPlanillaPersonal obe = (EProvisionPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
            if (obe == null)
                return;

            if (obe.tablc_iid_situacion_planilla == 6433)
            {
                obe.tablc_iid_situacion_planilla = 6434;
                if (XtraMessageBox.Show("¿Esta seguro que desea CERRAR Provision Planilla Personal " + obe.NumPlanilla + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new BPlanillas().modificarProvisionPlanillaPersonalSituacion(obe);
                }
            }
            else
            {
                obe.tablc_iid_situacion_planilla = 6433;
                if (XtraMessageBox.Show("¿Esta seguro que desea APERTURAR Provision Planilla Personal " + obe.NumPlanilla + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new BPlanillas().modificarProvisionPlanillaPersonalSituacion(obe);
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
            EProvisionPlanillaPersonal obe = (EProvisionPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.tablc_iid_situacion_planilla == 6434)
            {
                XtraMessageBox.Show(" No se puede Modificar si esta CERRADO la Provisión Planilla Personal " + obe.NumPlanilla + "?", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Question);
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
            EProvisionPlanillaPersonal obe = (EProvisionPlanillaPersonal)viewPlanillaPersonal.GetRow(viewPlanillaPersonal.FocusedRowHandle);
            try
            {
                if (obe.planc_iid_tipo_planilla==6437)/*GRATIFICACIONES*/
                {
                    List<EProvisionPlanillaPersonalDetalle> lstProvisionPlanillaDetalle = new List<EProvisionPlanillaPersonalDetalle>();
                    lstProvisionPlanillaDetalle = new BPlanillas().listarProvisionPlanillaPersonalDetalle(obe.planc_icod_planilla_personal);
                    grdGratificacion.DataSource = lstProvisionPlanillaDetalle;

                    if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                    {
                        grdGratificacion.DataSource = lstProvisionPlanillaDetalle;//.OrderBy(obe => obe.strNroCuenta).ThenBy(obe => obe.anac_cecoc_tipo).ThenBy(obe => obe.anac_cecoc_code).ThenBy(obe => obe.fec_cab).ToList();
                        string fileName = sfdRuta.FileName;
                        if (!fileName.Contains(".xlsx"))
                        {
                            grdGratificacion.ExportToXlsx(fileName + ".xlsx");
                            System.Diagnostics.Process.Start(fileName + ".xlsx");
                        }
                        else
                        {
                            grdGratificacion.ExportToXlsx(fileName);
                            System.Diagnostics.Process.Start(fileName);
                        }
                        grdGratificacion.DataSource = null;
                        sfdRuta.FileName = string.Empty;
                    }
                }
                else if (obe.planc_iid_tipo_planilla == 6438)/*CTS*/
                {
                    List<EProvisionPlanillaPersonalDetalle> lstProvisionPlanillaDetalle = new List<EProvisionPlanillaPersonalDetalle>();
                    lstProvisionPlanillaDetalle = new BPlanillas().listarProvisionPlanillaPersonalDetalle(obe.planc_icod_planilla_personal);
                    grdCTS.DataSource = lstProvisionPlanillaDetalle;

                    if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                    {
                        grdCTS.DataSource = lstProvisionPlanillaDetalle;//.OrderBy(obe => obe.strNroCuenta).ThenBy(obe => obe.anac_cecoc_tipo).ThenBy(obe => obe.anac_cecoc_code).ThenBy(obe => obe.fec_cab).ToList();
                        string fileName = sfdRuta.FileName;
                        if (!fileName.Contains(".xlsx"))
                        {
                            grdCTS.ExportToXlsx(fileName + ".xlsx");
                            System.Diagnostics.Process.Start(fileName + ".xlsx");
                        }
                        else
                        {
                            grdCTS.ExportToXlsx(fileName);
                            System.Diagnostics.Process.Start(fileName);
                        }
                        grdCTS.DataSource = null;
                        sfdRuta.FileName = string.Empty;
                    }
                }
                else if (obe.planc_iid_tipo_planilla == 6436)/*VACACIONES*/
                {
                    List<EProvisionPlanillaPersonalDetalle> lstProvisionPlanillaDetalle = new List<EProvisionPlanillaPersonalDetalle>();
                    lstProvisionPlanillaDetalle = new BPlanillas().listarProvisionPlanillaPersonalDetalle(obe.planc_icod_planilla_personal);
                    grdVacaciones.DataSource = lstProvisionPlanillaDetalle;

                    if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                    {
                        grdVacaciones.DataSource = lstProvisionPlanillaDetalle;//.OrderBy(obe => obe.strNroCuenta).ThenBy(obe => obe.anac_cecoc_tipo).ThenBy(obe => obe.anac_cecoc_code).ThenBy(obe => obe.fec_cab).ToList();
                        string fileName = sfdRuta.FileName;
                        if (!fileName.Contains(".xlsx"))
                        {
                            grdVacaciones.ExportToXlsx(fileName + ".xlsx");
                            System.Diagnostics.Process.Start(fileName + ".xlsx");
                        }
                        else
                        {
                            grdVacaciones.ExportToXlsx(fileName);
                            System.Diagnostics.Process.Start(fileName);
                        }
                        grdVacaciones.DataSource = null;
                        sfdRuta.FileName = string.Empty;
                    }
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


           
    }
}