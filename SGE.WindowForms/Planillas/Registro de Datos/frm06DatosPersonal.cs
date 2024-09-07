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
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using SGE.WindowForms.Otros.Operaciones;
using SGE.WindowForms.Otros.Planillas;

namespace SGE.WindowForms.Planillas.Registro_de_Datos
{
    public partial class frm06DatosPersonal : DevExpress.XtraEditors.XtraForm
    {
        List<EPersonal> lstPersonal = new List<EPersonal>();


        public frm06DatosPersonal()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {


            lstPersonal = new BPlanillas().listarPersonal();
            grdManoObra.DataSource = lstPersonal;
            viewManoObra.Focus();
        }
    
        #region Marca
        void reload(int intIcod)
        {
            cargar();
            int index = lstPersonal.FindIndex(x => x.perc_icod_personal == intIcod);
            viewManoObra.FocusedRowHandle = index;
            viewManoObra.Focus();   
        }
      
        private void nuevo()
        {
            frmRegistroPersonal frm = new frmRegistroPersonal();
            frm.MiEvento += new frmRegistroPersonal.DelegadoMensaje(reload);
            frm.lstPersonal = lstPersonal;
            frm.SetInsert();
            frm.ShowDialog();
            frm.txtCodigo.Focus();
            frm.lkpSituacion.Enabled = !Enabled;
        }
        private void modificar()
        {
            EPersonal Obe = (EPersonal)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroPersonal frm = new frmRegistroPersonal();
            frm.MiEvento += new frmRegistroPersonal.DelegadoMensaje(reload);
            frm.lstPersonal = lstPersonal;
            frm.Obe = Obe;
            frm.SetModify();
            frm.setValues();
            frm.ShowDialog();            
            frm.txtApellidoPat.Focus();
            frm.txtCodigo.Enabled = !Enabled;
        }
        private void viewManoObra_DoubleClick(object sender, EventArgs e)
        {
            EPersonal Obe = (EPersonal)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroPersonal frm = new frmRegistroPersonal();
            frm.MiEvento += new frmRegistroPersonal.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.SetCancel();
            frm.setValues();
            frm.Show();
            frm.lkpSituacion.Enabled = false;
                        
        }
        private void eliminar()
        {
            try
            {
                EPersonal Obe = (EPersonal)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewManoObra.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar los Datos Personal?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarPersonal(Obe);
                    cargar();
                    if (lstPersonal.Count >= index + 1)
                        viewManoObra.FocusedRowHandle = index;
                    else
                        viewManoObra.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
           
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

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
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
            imprimir();
        }
        #endregion

        private void btnNuevo_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void buscarCriterio()
        {
            grdManoObra.DataSource = lstPersonal.Where(x =>
                                                   x.perc_iid_personal.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.ApellNomb.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void cCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPersonal Obe = (EPersonal)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmRegistroCCosto frm = new FrmRegistroCCosto();
            frm.IcodPersonal = Obe.perc_icod_personal;
            frm.Show();
        }
        
    }
}