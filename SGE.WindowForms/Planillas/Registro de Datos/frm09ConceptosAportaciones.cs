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
    public partial class frm09ConceptosAportaciones : DevExpress.XtraEditors.XtraForm
    {
        List<EConceptoAportacion> lstConceptoAportacion = new List<EConceptoAportacion>();

        public frm09ConceptosAportaciones()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            lstConceptoAportacion = new BPlanillas().listarConceptoAportacion();
            grdAlmacen.DataSource = lstConceptoAportacion;
            viewAlmacen.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstConceptoAportacion.FindIndex(x => x.capc_icod_concepto_aportaciones_plan == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();   
        }
        private void nuevo()
        {
            frmRegistroConceptoAportaciones frm = new frmRegistroConceptoAportaciones();
            frm.MiEvento += new frmRegistroConceptoAportaciones.DelegadoMensaje(reload);
            if (lstConceptoAportacion.Count > 0)
                frm.txtCodigo.Text = String.Format("{000:0000}", lstConceptoAportacion.Max(x => Convert.ToInt32(x.capc_iid_concepto_apor_plan) + 1));
            else
                frm.txtCodigo.Text = "0001";

            frm.lstConceptoAportacion = lstConceptoAportacion;
            frm.SetInsert();
            frm.ShowDialog();
            //frm.txtNombre.Focus();
            
        }
        private void modificar()
        {
            EConceptoAportacion Obe = (EConceptoAportacion)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroConceptoAportaciones frm = new frmRegistroConceptoAportaciones();
            frm.MiEvento += new frmRegistroConceptoAportaciones.DelegadoMensaje(reload);
            frm.lstConceptoAportacion = lstConceptoAportacion;            
            frm.Obe = Obe; 
            frm.SetModify(); 
            frm.setValues();
            frm.ShowDialog();           
             
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
          
        }

        private void eliminar()
        {
           
            try
            {
                EConceptoAportacion Obe = (EConceptoAportacion)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewAlmacen.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el Concepto Aportación " + Obe.capc_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarConceptoAportacion(Obe);                    
                    cargar();
                    if (lstConceptoAportacion.Count >= index + 1)
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
            grdAlmacen.DataSource = lstConceptoAportacion.Where(x =>
                                                   x.capc_iid_concepto_apor_plan.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.capc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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
            EConceptoAportacion Obe = (EConceptoAportacion)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroConceptoAportaciones frm = new frmRegistroConceptoAportaciones();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }

        private void PorcentajesFIjosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PorcentajesFijos();
        }


        private void PorcentajesFijos() 
        {
            //ETablaPlanilla Obe = (ETablaPlanilla)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            //if (Obe == null)
            //    return;
            //int index = viewAlmacen.FocusedRowHandle;
            //frmRegistroConceptosIngresos frm = new frmRegistroConceptosIngresos();

            //frm.intIcodFondosPensiones = Obe.tbpc_icod_tabla_planilla;
            //frm.Text = String.Format("Relacion de Tabla: {0} - {1}", Obe.tbpc_iid_vcodigo_tabla_planilla, Obe.tbpc_vdescripcion);
            //frm.Show();
           
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