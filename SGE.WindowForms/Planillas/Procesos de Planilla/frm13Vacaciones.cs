using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Linq;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.WindowForms.Otros.Planillas;
using SGE.WindowForms.Reportes.Almacen.Registros;

namespace SGE.WindowForms.Planillas.Procesos_de_Planilla
{
    public partial class frm13Vacaciones : DevExpress.XtraEditors.XtraForm
    {
        List<EPersonal> lstFamiliaCab = new List<EPersonal>();
        List<EVacaciones> lstFamiliaDet = new List<EVacaciones>();

        public frm13Vacaciones()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
            cargarGridSize();
        }

        private void cargarGridSize()
        {
            grdFamilia.Height = (this.Height) / 2;
            //grdSubFamilia.Height = (this.Height) / 2 + 10;
        }
       
        private void cargar()
        {
            lstFamiliaCab = new BPlanillas().listarPersonal();
            if (lstFamiliaCab.Count == 1)
                lstFamiliaDet = new BPlanillas().listarVacaciones().Where(a=>a.vacd_icod_personal==lstFamiliaCab[0].perc_icod_personal ).ToList();
            
            grdFamilia.DataSource = lstFamiliaCab;
            viewFamilia.Focus();
        }
        
        #region Familia Det
        void reloadSubFamilia(int intIcod)
        {
            EPersonal Obe = (EPersonal)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            lstFamiliaDet = new BPlanillas().listarVacaciones().Where(x=>x.vacd_icod_personal==Obe.perc_icod_personal).ToList();
            grdSubFamilia.DataSource = lstFamiliaDet;
            int index = lstFamiliaDet.FindIndex(x => x.vacd_icod_vacaciones == intIcod);
            viewSubFamilia.FocusedRowHandle = index;
            viewSubFamilia.GroupPanelText = String.Format("VACACIONES DE : {0} - {1}", Obe.perc_iid_personal, Obe.ApellNomb);
            viewSubFamilia.Focus();   
        }
        private void nuevoSubFamilia()
        {
            EPersonal Obe = (EPersonal)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe == null)
            {
                XtraMessageBox.Show("Para poder registrar Sub Líneas de Productos, debe registrar Vacaciones", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            frmManteVacaciones frm = new frmManteVacaciones();
            frm.MiEvento += new frmManteVacaciones.DelegadoMensaje(reloadSubFamilia);
            if (lstFamiliaDet.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstFamiliaDet.Max(x => Convert.ToInt32(x.vacd_iid_vacaciones) + 1));
            else
                frm.txtCodigo.Text = "01";    
            frm.intIcodFamiliaCab = Obe.perc_icod_personal;
            frm.lstFamiliaDet = lstFamiliaDet;
            frm.SetInsert();
            frm.Text = String.Format("Mantenimiento - Registro de Vacaciones de {0}", Obe.ApellNomb);
            frm.Show();
            
            
        }
        private void modificarSubFamilia()
        {
            EPersonal ObeF = (EPersonal)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            EVacaciones Obe = (EVacaciones)viewSubFamilia.GetRow(viewSubFamilia.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteVacaciones frm = new frmManteVacaciones();
            frm.MiEvento += new frmManteVacaciones.DelegadoMensaje(reloadSubFamilia);
            frm.lstFamiliaDet = lstFamiliaDet;
            frm.intIcodFamiliaCab = ObeF.perc_icod_personal;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Text = String.Format("Mantenimiento - Registro de Vacaciones de {0}", ObeF.ApellNomb);
            frm.Show();
            frm.setValues();
            
            
        }
        private void eliminarSubFamilia()
        {
            try
            {
                EVacaciones Obe = (EVacaciones)viewSubFamilia.GetRow(viewSubFamilia.FocusedRowHandle);
                if (Obe == null)
                    return;
                //var lstProducto = new BAlmacen().listarProducto(Parametros.intEjercicio);
                int index = viewSubFamilia.FocusedRowHandle;
                //if (lstProducto.Where(x => x.famid_icod_familia == Obe.vacd_icod_vacaciones).ToList().Count > 0)
                //{
                //    XtraMessageBox.Show("La Sub-Familia no puede ser eliminada porque ha sido usada en los registros de productos", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar Vacación : " + Obe.vacd_iid_vacaciones + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarVacaciones(Obe);
                    reloadSubFamilia(0);
                    if (lstFamiliaDet.Count >= index + 1)
                        viewSubFamilia.FocusedRowHandle = index;
                    else
                        viewSubFamilia.FocusedRowHandle = index - 1;

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nuevoSubFamilia();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            modificarSubFamilia();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            eliminarSubFamilia();
        }      
        
        #endregion

        private void viewFamilia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EPersonal Obe = (EPersonal)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe != null)
            {
                lstFamiliaDet = new BPlanillas().listarVacaciones().Where(a=>a.vacd_icod_personal==Obe.perc_icod_personal).ToList();
                grdSubFamilia.DataSource = lstFamiliaDet;
                viewSubFamilia.GroupPanelText = String.Format("Vacaciones de : {0} - {1}", Obe.perc_iid_personal, Obe.ApellNomb);
            }
        }

        private void impFamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void impSubfamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //rptFamilia rpt = new rptFamilia();
            //rpt.cargar("RELACIÓN DE LINEAS DE PRODUCTOS", "", lstFamiliaCab);
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewFamilia.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewFamilia.ClearColumnsFilter();

            viewSubFamilia.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewSubFamilia.ClearColumnsFilter();
        }

        private void viewFamilia_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            EPersonal Obe = (EPersonal)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe != null)
            {
                lstFamiliaDet = new BPlanillas().listarVacaciones().Where(a => a.vacd_icod_personal == Obe.perc_icod_personal).ToList();
                grdSubFamilia.DataSource = lstFamiliaDet;
                viewSubFamilia.GroupPanelText = String.Format("Vacaciones de : {0} - {1}", Obe.perc_iid_personal, Obe.ApellNomb);
            }
        }
    }
}