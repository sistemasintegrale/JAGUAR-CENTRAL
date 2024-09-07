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
using SGE.WindowForms.Otros.Planillas;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Linq;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.WindowForms.Planillas;
using SGE.WindowForms.Modules;


namespace SGE.WindowForms.Planillas.Registro_de_Datos
{
    public partial class frm02CargoPuesto : DevExpress.XtraEditors.XtraForm
    {
        List<ECargo> lstCargo = new List<ECargo>();
        public frm02CargoPuesto()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            lstCargo = new BPlanillas().listarCargo();
            grdAlmacen.DataSource =lstCargo ;
            viewAlmacen.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstCargo.FindIndex(x => x.carg_icod_cargo == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();   
        }
        private void nuevo()
        {
            frmRegistroCargo frm = new frmRegistroCargo();
            frm.MiEvento += new frmRegistroCargo.DelegadoMensaje(reload);
            frm.lstCargo = lstCargo;
            frm.SetInsert();
            frm.ShowDialog();
            frm.txtDescripcion.Focus();
           
        }

        private void modificar()
        {
            ECargo Obe = (ECargo)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroCargo frm = new frmRegistroCargo();
            frm.MiEvento += new frmRegistroCargo.DelegadoMensaje(reload);
            frm.lstCargo = lstCargo;
            frm.Obe = Obe;
            frm.SetModify();
            frm.setValues();
            frm.ShowDialog();            
            frm.txtDescripcion.Focus();
        }

        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
         
        }

        private void eliminar()
        {
            try
            {
                ECargo Obe = (ECargo)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewAlmacen.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el Cargo " + Obe.carg_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarCargo(Obe);
                    cargar();
                    if (lstCargo.Count >= index + 1)
                        viewAlmacen.FocusedRowHandle = index;
                    else
                        viewAlmacen.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        
        private void buscarCriterio()
        {
            grdAlmacen.DataSource= lstCargo.Where(x =>
                                                   x.carg_vabreviado.Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.carg_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
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

        

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void viewAlmacen_DoubleClick(object sender, EventArgs e)
        {
            ECargo Obe = (ECargo)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroCargo frm = new frmRegistroCargo();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.setValues();
            frm.ShowDialog();
            
        }

      

      
           
    }
}