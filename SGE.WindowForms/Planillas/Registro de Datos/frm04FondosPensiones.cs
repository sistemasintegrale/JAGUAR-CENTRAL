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
    public partial class frm04FondosPensiones : DevExpress.XtraEditors.XtraForm
    {

        public List<EFondosPensionesMixtas> lstFondosPensionesMixtas = new List<EFondosPensionesMixtas>();
        public List<EFondosPensionesConceptos> lstFondosPensionesConceptos = new List<EFondosPensionesConceptos>();
        public List<EFondosPensiones> lstFondosPensiones = new List<EFondosPensiones>();
        //public decimal sumsPorcentaje = 0;
        public frm04FondosPensiones()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
            grdAlmacen.Refresh();
            viewAlmacen.RefreshData();
        }       
       
        public void cargar()
        {
            lstFondosPensiones = new BPlanillas().listarFondosPensiones();
            grdAlmacen.DataSource = lstFondosPensiones;
            viewAlmacen.Focus();



        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstFondosPensiones.FindIndex(x => x.fdpc_icod_fondo_pension == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();   
        }
        private void nuevo()
        {
            frmRegistroFondosPensiones frm = new frmRegistroFondosPensiones();
            frm.MiEvento += new frmRegistroFondosPensiones.DelegadoMensaje(reload);
            if (lstFondosPensiones.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstFondosPensiones.Max(x =>Convert.ToInt32(x.fdpc_iid_vcodigo_fondo) + 1));
            else
                frm.txtCodigo.Text = "01";            
            frm.lstFondosPensiones = lstFondosPensiones;
            frm.SetInsert();
            frm.ShowDialog();
            frm.txtNombre.Focus();
            frm.ckeAFP.Checked = false;
        }
        private void modificar()
        {
            EFondosPensiones Obe = (EFondosPensiones)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroFondosPensiones frm = new frmRegistroFondosPensiones();
            frm.MiEvento += new frmRegistroFondosPensiones.DelegadoMensaje(reload);
            frm.lstFondosPensiones = lstFondosPensiones;
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
            int usuario = Valores.intUsuario;
            string pc = WindowsIdentity.GetCurrent().Name;
            try
            {
                EFondosPensiones Obe = (EFondosPensiones)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewAlmacen.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el Fondo de Pensión " + Obe.fdpc_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarFondosPensiones(Obe);                    
                    new BPlanillas().eliminarPorcentajeFijo(Obe.fdpc_icod_fondo_pension,usuario,pc);
                    new BPlanillas().eliminarPorcentajeMixto(Obe.fdpc_icod_fondo_pension, usuario, pc);
                    cargar();
                    if (lstFondosPensiones.Count >= index + 1)
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
            grdAlmacen.DataSource = lstFondosPensiones.Where(x =>
                                                   x.fdpc_iid_vcodigo_fondo.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.fdpc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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
            EFondosPensiones Obe = (EFondosPensiones)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroFondosPensiones frm = new frmRegistroFondosPensiones();
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
            EFondosPensiones Obe = (EFondosPensiones)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            int index = viewAlmacen.FocusedRowHandle;
            frmFondosPensionesConceptos frm = new frmFondosPensionesConceptos();
                         
            frm.intIcodFondosPensiones = Obe.fdpc_icod_fondo_pension;
            frm.ShowDialog();
            if (frm.intIcodFondosPensiones>0)
            {
                cargar();
            }
        
        }

        private void PorcentajesMixtos()
        {
            EFondosPensiones Obe = (EFondosPensiones)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            int index = viewAlmacen.FocusedRowHandle;
            frmFondosPensionesConceptosMixtas frm = new frmFondosPensionesConceptosMixtas();

            frm.intIcodFondosPensiones = Obe.fdpc_icod_fondo_pension;
            frm.ShowDialog();
            if (frm.intIcodFondosPensiones > 0)
            {
                cargar();
            }
            
        }

        private void viewAlmacen_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EFondosPensiones Obe = (EFondosPensiones)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe != null)
            {
                if (Obe.tablc_iid_tipo_fondo_pensiones == false)
                {
                    PorcentajesFIjosToolStripMenuItem.Visible = false;
                    PorcentajesMixtosToolStripMenuItem1.Visible = false;
                }
                else
                {
                    PorcentajesFIjosToolStripMenuItem.Visible = true;
                    PorcentajesMixtosToolStripMenuItem1.Visible = true;
                }
            }
        }

        private void PorcentajesMixtosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PorcentajesMixtos();
        }

      
           
    }
}