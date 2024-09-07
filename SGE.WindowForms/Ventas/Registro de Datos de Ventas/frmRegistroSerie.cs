using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Operaciones;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class frmRegistroSerie : DevExpress.XtraEditors.XtraForm
    {
        List<ESeries> lstSeries = new List<ESeries>();


        public frmRegistroSerie()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {


            lstSeries = new BVentas().listarSeries();
            grdManoObra.DataSource = lstSeries;
            viewManoObra.Focus();
        }

        #region Marca
        void reload(int intIcod)
        {
            cargar();
            int index = lstSeries.FindIndex(x => x.rs_icod_registro_serie == intIcod);
            viewManoObra.FocusedRowHandle = index;
            viewManoObra.Focus();
        }

        private void nuevo()
        {
            frmManteRegistroSerie frm = new frmManteRegistroSerie();
            frm.MiEvento += new frmManteRegistroSerie.DelegadoMensaje(reload);
            frm.lstSeries = lstSeries;
            frm.SetInsert();
            frm.Show();
        }
        private void modificar()
        {
            ESeries Obe = (ESeries)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteRegistroSerie frm = new frmManteRegistroSerie();
            frm.MiEvento += new frmManteRegistroSerie.DelegadoMensaje(reload);
            frm.lstSeries = lstSeries;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();

        }
        private void viewManoObra_DoubleClick(object sender, EventArgs e)
        {
            EVendedor Obe = (EVendedor)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteVendedor frm = new frmManteVendedor();
            frm.MiEvento += new frmManteVendedor.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            try
            {
                ESeries Obe = (ESeries)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewManoObra.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarSeries(Obe);
                    cargar();
                    if (lstSeries.Count >= index + 1)
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

    }
}