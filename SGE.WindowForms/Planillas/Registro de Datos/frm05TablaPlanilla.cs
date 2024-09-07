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


namespace SGE.WindowForms.Planillas.Registro_de_Datos
{
    public partial class frm05TablaPlanilla : DevExpress.XtraEditors.XtraForm
    {
        List<ETablaPlanilla> lstTablaPlanilla = new List<ETablaPlanilla>();
        
        public frm05TablaPlanilla()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            lstTablaPlanilla = new BPlanillas().listarTablaPlanilla();
            grdAlmacen.DataSource = lstTablaPlanilla;
            viewAlmacen.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTablaPlanilla.FindIndex(x => x.tbpc_icod_tabla_planilla == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();   
        }
        private void nuevo()
        {
            frmRegistroTablaPlanilla frm = new frmRegistroTablaPlanilla();
            frm.MiEvento += new frmRegistroTablaPlanilla.DelegadoMensaje(reload);
            if (lstTablaPlanilla.Count > 0)
                frm.txtCodigo.Text = String.Format("{00:000}", lstTablaPlanilla.Max(x =>Convert.ToInt32(x.tbpc_iid_vcodigo_tabla_planilla) + 1));
            else
                frm.txtCodigo.Text = "001";
            
            frm.lstTablaPlanilla = lstTablaPlanilla;
            frm.SetInsert();
            frm.ShowDialog();
            frm.txtNombre.Focus();
            
        }
        private void modificar()
        {
            ETablaPlanilla Obe = (ETablaPlanilla)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroTablaPlanilla frm = new frmRegistroTablaPlanilla();
            frm.MiEvento += new frmRegistroTablaPlanilla.DelegadoMensaje(reload);
            frm.lstTablaPlanilla = lstTablaPlanilla;
            frm.Obe = Obe;
            frm.SetModify();
            frm.setValues();
            frm.ShowDialog();            
            frm.txtNombre.Focus();
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
          
        }

        private void eliminar()
        {
            int usuario =Valores.intUsuario;
            string pc=WindowsIdentity.GetCurrent().Name;
            try
            {
                ETablaPlanilla Obe = (ETablaPlanilla)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewAlmacen.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar la tabla Planilla " + Obe.tbpc_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarTablaPlanilla(Obe);
                    new BPlanillas().eliminarDetalleTablaPlanilla(Obe.tbpc_icod_tabla_planilla,usuario,pc);
                    cargar();
                    if (lstTablaPlanilla.Count >= index + 1)
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
            grdAlmacen.DataSource = lstTablaPlanilla.Where(x =>
                                                   x.tbpc_iid_vcodigo_tabla_planilla.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.tbpc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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

        private void grdAlmacen_DoubleClick(object sender, EventArgs e)
        {
            ETablaPlanilla Obe = (ETablaPlanilla)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroTablaPlanilla frm = new frmRegistroTablaPlanilla();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.setValues();
            frm.ShowDialog();
            
        }

        private void PorcentajesFIjosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PorcentajesFijos();
        }


        private void PorcentajesFijos() 
        {
            ETablaPlanilla Obe = (ETablaPlanilla)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            int index = viewAlmacen.FocusedRowHandle;
            frmTablaPlanillaDetalle frm = new frmTablaPlanillaDetalle();

            frm.intIcodFondosPensiones = Obe.tbpc_icod_tabla_planilla;
            frm.Text = String.Format("Relacion de Tabla: {0} - {1}", Obe.tbpc_iid_vcodigo_tabla_planilla, Obe.tbpc_vdescripcion);
            frm.Show();
           
        }

        private void PorcentajesMixtos()
        {
           
        }

        private void viewAlmacen_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //EFondosPensiones Obe = (EFondosPensiones)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            //if (Obe != null)
            //{
            //    if (Obe.tablc_iid_tipo_fondo_pensiones == false)
            //    {
            //        PorcentajesFIjosToolStripMenuItem.Visible = false;
            //        PorcentajesMixtosToolStripMenuItem1.Visible = false;
            //    }
            //    else
            //    {
            //        PorcentajesFIjosToolStripMenuItem.Visible = true;
            //        PorcentajesMixtosToolStripMenuItem1.Visible = true;
            //    }
            //}
        }
        
        private void PorcentajesMixtosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }


           
    }
}