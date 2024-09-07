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
using SGE.WindowForms.Reportes.Almacen.Registros;
using SGE.WindowForms.Otros.Contabilidad;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class frm02ClasificacionServicios : DevExpress.XtraEditors.XtraForm
    {
        List<EClasificacionProducto> lstClasificacionProducto = new List<EClasificacionProducto>();

        public frm02ClasificacionServicios()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            lstClasificacionProducto = new BContabilidad().listarClasificacionProducto();
            grdAlmacen.DataSource = lstClasificacionProducto;
            viewAlmacen.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstClasificacionProducto.FindIndex(x => x.clasc_icod_clasificacion == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();   
        } 
       
        private void nuevo()
        {
            frmManteClasificacion frm = new frmManteClasificacion();
            frm.MiEvento += new frmManteClasificacion.DelegadoMensaje(reload);
            if (lstClasificacionProducto.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstClasificacionProducto.Max(x => x.clasc_iid_clasificacion + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.lstClasificacionProducto = lstClasificacionProducto;
            frm.SetInsert();
            frm.Show();            
        }

        private void modificar()
        {
            EClasificacionProducto Obe = (EClasificacionProducto)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteClasificacion frm = new frmManteClasificacion();
            frm.MiEvento += new frmManteClasificacion.DelegadoMensaje(reload);
            frm.lstClasificacionProducto = lstClasificacionProducto;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();            
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            EClasificacionProducto Obe = (EClasificacionProducto)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteClasificacion frm = new frmManteClasificacion();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            try
            {
                EClasificacionProducto Obe = (EClasificacionProducto)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewAlmacen.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro " + Obe.clasc_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BContabilidad().eliminarClasificacionProducto(Obe);
                    cargar();
                    if (lstClasificacionProducto.Count >= index + 1)
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
        private void imprimir()
        {
            //if (lstAlmacenes.Count > 0)
            //{
            //    rptAlmacen rpt = new rptAlmacen();
            //    rpt.cargar("RELACIÓN DE ALMACENES", "", lstAlmacenes);
            //}
           
        }
        
        private void filtrar()
        {
            grdAlmacen.DataSource = lstClasificacionProducto.Where(x =>
                                                   x.clasc_iid_clasificacion.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.clasc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewAlmacen.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewAlmacen.ClearColumnsFilter();
        }     
    }
}