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
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using SGE.WindowForms.Otros.Planillas;


namespace SGE.WindowForms.Planillas.Procesos_de_Planilla
{
    public partial class frm12InicialesProvPlanilla : DevExpress.XtraEditors.XtraForm
    {
        
        List<EInicial_Prov_planilla> lstInicialProvPlanilla= new List<EInicial_Prov_planilla>();

        EInicial_Prov_planilla obe = new EInicial_Prov_planilla();
        

        public frm12InicialesProvPlanilla()
        {
            InitializeComponent();
        }        
        
        private string opcionProceso = "";

        private void FrmComprobantes_Load(object sender, EventArgs e)
        {
            cargar();
                  
        }

        public void cargar()
        {
            lstInicialProvPlanilla = new BPlanillas().ListarInicial_Prov_Planilla();
            dgrComprobante.DataSource = lstInicialProvPlanilla;
            viewComprobante.Focus();
        }

        void reload(int intIcod)
        {
            cargar();           
            dgrComprobante.DataSource = lstInicialProvPlanilla;
            int index = lstInicialProvPlanilla.FindIndex(x => x.ippc_icod_inicial_provision_planilla == intIcod);
            viewComprobante.FocusedRowHandle = index;
            viewComprobante.Focus();
        }       
       
        private void nuevo()
        {
            frmRegistroInicialesProvPlanilla frm = new frmRegistroInicialesProvPlanilla();
            frm.MiEvento += new frmRegistroInicialesProvPlanilla.DelegadoMensaje(reload);
            if (lstInicialProvPlanilla.Count > 0)
                frm.txtCodigo.Text = String.Format("{00:000}", lstInicialProvPlanilla.Max(x => Convert.ToInt32(x.ippc_iid_inicial_provision_planilla) + 1));
            else
                frm.txtCodigo.Text = "001";

            frm.lstInicialesProvPlanilla = lstInicialProvPlanilla;
            frm.SetInsert();
            frm.ShowDialog();
            frm.txtNombre.Focus();
        }

        private void modificar()
        {

            EInicial_Prov_planilla Obe = (EInicial_Prov_planilla)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroInicialesProvPlanilla frm = new frmRegistroInicialesProvPlanilla();
            frm.MiEvento += new frmRegistroInicialesProvPlanilla.DelegadoMensaje(reload);
            frm.lstInicialesProvPlanilla = lstInicialProvPlanilla;
            frm.Obe = Obe;
            frm.SetModify();
            frm.setValues();
            frm.ShowDialog();
            frm.txtNombre.Focus();
        }
        private void eliminar()
        {
            int usuario = Valores.intUsuario;
            string pc = WindowsIdentity.GetCurrent().Name;
            try
            {
                EInicial_Prov_planilla Obe = (EInicial_Prov_planilla)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewComprobante.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar la Incial Provision Planilla  " + Obe.ippc_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarInicial_Prov_Planilla(Obe);
                    
                    cargar();
                    if (lstInicialProvPlanilla.Count >= index + 1)
                        viewComprobante.FocusedRowHandle = index;
                    else
                        viewComprobante.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }     

        private void viewAnalitica_DoubleClick(object sender, EventArgs e)
        {
            EInicial_Prov_planilla oBe = (EInicial_Prov_planilla)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            if (oBe != null)
            {
                frmRegistroInicialesProvPlanilla frm = new frmRegistroInicialesProvPlanilla();                
                frm.SetCancel();                
                frm.Obe = oBe;                
                frm.Show();
                frm.setValues();                
            }
        }             
        

        private void buscarCriterio()
        {
            dgrComprobante.DataSource = lstInicialProvPlanilla.Where(x => x.ippc_iid_inicial_provision_planilla.ToUpper().Contains(txtDescripcion.Text.ToUpper()) &&
                                                   x.ippc_vdescripcion.ToString().ToUpper().StartsWith(txtCodigo.Text.ToUpper())).ToList();

        }
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
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

        private void NuevotoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void ModificartoolStripMenuItem2_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void EliminartoolStripMenuItem3_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void ImprimirtoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            
        }

        private void detalleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EInicial_Prov_planilla obe = (EInicial_Prov_planilla)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            if (obe != null)
            {
                frmIncialesProvPlanillaDet frm = new frmIncialesProvPlanillaDet();
                frm.intIcodFondosPensiones = obe.ippc_icod_inicial_provision_planilla;
                frm.ShowDialog();
            }
        }

       

     
    }
}



