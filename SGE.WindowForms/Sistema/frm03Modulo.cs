using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Sistema
{
    public partial class frm03Modulo : DevExpress.XtraEditors.XtraForm
    {
        List<EModulo> lstModulo = new List<EModulo>();
        public frm03Modulo()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstModulo = new BAdministracionSistema().listarModulo();
            grdModulo.DataSource = lstModulo;
            viewModulo.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstModulo.FindIndex(x => x.moduc_icod_modulo == intIcod);
            viewModulo.FocusedRowHandle = index;
            viewModulo.Focus();
        }
        private void nuevo()
        {
            frmManteModulo frm = new frmManteModulo();
            frm.MiEvento += new frmManteModulo.DelegadoMensaje(reload);
            frm.lstModulo = lstModulo;
            frm.SetInsert();
            frm.Show();
            frm.txtCodigo.Focus();
        }
        private void modificar()
        {
            EModulo Obe = (EModulo)viewModulo.GetRow(viewModulo.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteModulo frm = new frmManteModulo();
            frm.MiEvento += new frmManteModulo.DelegadoMensaje(reload);
            frm.lstModulo = lstModulo;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtDescripcion.Focus();
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            EModulo Obe = (EModulo)viewModulo.GetRow(viewModulo.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteModulo frm = new frmManteModulo();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            EModulo Obe = (EModulo)viewModulo.GetRow(viewModulo.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                new BAdministracionSistema().eliminarModulo(Obe);
                cargar();
            }
        }
        private void imprimir()
        {

        }

        private void buscarCriterio()
        {
            grdModulo.DataSource = lstModulo.Where(x =>
                                                   String.Format("{0:00}", x.intCorrelativo).ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.moduc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }
    }
}