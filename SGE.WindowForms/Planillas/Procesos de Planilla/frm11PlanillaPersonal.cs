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
    public partial class frm11PlanillaPersonal : DevExpress.XtraEditors.XtraForm
    {     
        private List<ENotaIngreso> lstNotaIngreso = new List<ENotaIngreso>();

        private List<EPlanillaPersonal> lstPlanillaPersonal = new List<EPlanillaPersonal>();
        public frm11PlanillaPersonal()
        {
            InitializeComponent();
        }

        private void FrmRegistroNotaIngreso_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstPlanillaPersonal = new BPlanillas().listarPlanillaPersonal();
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

     

            
    }
}