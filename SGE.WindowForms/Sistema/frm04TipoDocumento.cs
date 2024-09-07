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
    public partial class frm04TipoDocumento : DevExpress.XtraEditors.XtraForm
    {
        List<ETipoDocumento> lstTipoDocumento = new List<ETipoDocumento>();
        public frm04TipoDocumento()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstTipoDocumento = new BAdministracionSistema().listarTipoDocumento();
            grdTipoDocumento.DataSource = lstTipoDocumento;
            viewTipoDocumento.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTipoDocumento.FindIndex(x => x.tdocc_icod_tipo_doc == intIcod);
            viewTipoDocumento.FocusedRowHandle = index;
            viewTipoDocumento.Focus();
        }
        private void nuevo()
        {
            frmManteTipoDocumento frm = new frmManteTipoDocumento();
            frm.MiEvento += new frmManteTipoDocumento.DelegadoMensaje(reload);
            frm.lstTipoDocumento = lstTipoDocumento;
            frm.SetInsert();
            frm.Show();
            frm.txtCodigo.Focus();
        }
        private void modificar()
        {
            ETipoDocumento Obe = (ETipoDocumento)viewTipoDocumento.GetRow(viewTipoDocumento.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteTipoDocumento frm = new frmManteTipoDocumento();
            frm.MiEvento += new frmManteTipoDocumento.DelegadoMensaje(reload);
            frm.lstTipoDocumento = lstTipoDocumento;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtDescripcion.Focus();
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            ETipoDocumento Obe = (ETipoDocumento)viewTipoDocumento.GetRow(viewTipoDocumento.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteTipoDocumento frm = new frmManteTipoDocumento();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            ETipoDocumento Obe = (ETipoDocumento)viewTipoDocumento.GetRow(viewTipoDocumento.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                new BAdministracionSistema().eliminarTipoDocumento(Obe);
                cargar();
            }
        }
        private void imprimir()
        {

        }

        private void buscarCriterio()
        {
            grdTipoDocumento.DataSource = lstTipoDocumento.Where(x =>
                                                   x.tdocc_vabreviatura_tipo_doc.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.tdocc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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
        private void listarClaseDocumento()
        {
            ETipoDocumento Obe = (ETipoDocumento)viewTipoDocumento.GetRow(viewTipoDocumento.FocusedRowHandle);
            if (Obe == null)
                return;
            frmClaseDocumento frm = new frmClaseDocumento();
            frm.intTipoDoc = Obe.tdocc_icod_tipo_doc;
            frm.Show();
        }
        private void claseDelDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listarClaseDocumento();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listarClaseDocumento();
        }
    }
}