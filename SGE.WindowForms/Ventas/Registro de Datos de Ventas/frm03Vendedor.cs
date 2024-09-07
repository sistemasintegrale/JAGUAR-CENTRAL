using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class frm03Vendedor : DevExpress.XtraEditors.XtraForm
    {
        List<EVendedor> lstPersonal = new List<EVendedor>();


        public frm03Vendedor()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {


            lstPersonal = new BOperaciones().listarVendedor();
            grdManoObra.DataSource = lstPersonal;
            viewManoObra.Focus();
        }

        #region Marca
        void reload(int intIcod)
        {
            cargar();
            int index = lstPersonal.FindIndex(x => x.vdrsc_icod_vendedor == intIcod);
            viewManoObra.FocusedRowHandle = index;
            viewManoObra.Focus();
        }

        private void nuevo()
        {
            frmManteVendedor frm = new frmManteVendedor();
            frm.MiEvento += new frmManteVendedor.DelegadoMensaje(reload);
            if (lstPersonal.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:0000}", lstPersonal.Max(x => x.vdrsc_vcod_vendedor + 1));
            else
                frm.txtCodigo.Text = "0001";
            frm.lstPersonal = lstPersonal;
            frm.SetInsert();
            frm.Show();
            frm.txtNombres.Focus();
        }
        private void modificar()
        {
            EVendedor Obe = (EVendedor)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteVendedor frm = new frmManteVendedor();
            frm.MiEvento += new frmManteVendedor.DelegadoMensaje(reload);
            frm.lstPersonal = lstPersonal;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtNombres.Focus();
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
                EVendedor Obe = (EVendedor)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewManoObra.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BOperaciones().eliminarVendedor(Obe);
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

    }
}