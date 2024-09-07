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
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using SGE.WindowForms.Reportes.Almacen.Registros;
using SGE.WindowForms.Planillas.Registro_de_Datos;

namespace SGE.WindowForms.Planillas.Registro_de_Datos
{
    public partial class frm01Empresa : DevExpress.XtraEditors.XtraForm
    {
        List<EEmpresa> lstEmpresa = new List<EEmpresa>();
        public frm01Empresa()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
            grdAlmacen.Refresh();
            viewAlmacen.RefreshData();
        }       
       
        private void cargar()
        {
            lstEmpresa = new BPlanillas().listarEmpresa();
            grdAlmacen.DataSource = lstEmpresa;
            viewAlmacen.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstEmpresa.FindIndex(x => x.cia_icod_empresa == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();   
        }        
        //private void nuevo()
        //{
            //frmRegistroEmpresa frm = new frmRegistroEmpresa();
            //frm.MiEvento += new frmManteAlmacen.DelegadoMensaje(reload);
            //if (lstAlmacenes.Count > 0)
            //    frm.txtCodigo.Text = String.Format("{0:00}", lstAlmacenes.Max(x => x.almac_iid_almacen + 1));
            //else
            //    frm.txtCodigo.Text = "01";
            //frm.lstAlmacenes = lstAlmacenes;
            //frm.SetInsert();
            //frm.Show();
            //frm.txtNombre.Focus();
            //frm.rbnActivo.Checked = true;
        //}
        private void modificar()
        {

            EEmpresa Obe = (EEmpresa)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroEmpresa frm = new frmRegistroEmpresa();
            frm.MiEvento += new frmRegistroEmpresa.DelegadoMensaje(reload);
            frm.LstEmpresa = lstEmpresa;
            frm.Obe = Obe;
            frm.SetModify();
            frm.setValues();
            frm.ShowDialog();
            
            frm.txtRazonSocial.Enabled = true;
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            EEmpresa Obe = (EEmpresa)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroEmpresa frm = new frmRegistroEmpresa();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.setValues();
            frm.ShowDialog();
            
        }
        //private void eliminar()
        //{
        //    try
        //    {
        //        EAlmacen Obe = (EAlmacen)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
        //        if (Obe == null)
        //            return;
        //        int index = viewAlmacen.FocusedRowHandle;
        //        if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el almacén " + Obe.almac_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            Obe.intUsuario = Valores.intUsuario;
        //            Obe.strPc = WindowsIdentity.GetCurrent().Name;
        //            new BAlmacen().eliminarAlmacen(Obe);
        //            cargar();
        //            if (lstAlmacenes.Count >= index + 1)
        //                viewAlmacen.FocusedRowHandle = index;
        //            else
        //                viewAlmacen.FocusedRowHandle = index - 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //private void imprimir()
        //{
        //    if (lstAlmacenes.Count > 0)
        //    {
        //        rptAlmacen rpt = new rptAlmacen();
        //        rpt.cargar("RELACIÓN DE ALMACENES", "", lstAlmacenes);
        //    }
           
        //}
        
        private void buscarCriterio()
        {
                                 
        }
        //private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    nuevo();
        //}

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        //private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    eliminar();
        //}

        //private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    imprimir();
        //}

        //private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    nuevo();
        //}

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        //private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    eliminar();
        //}

        //private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    imprimir();
        //}

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewAlmacen.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewAlmacen.ClearColumnsFilter();
        }


      
           
    }
}