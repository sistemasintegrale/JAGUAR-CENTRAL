using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmClaseDocumento : DevExpress.XtraEditors.XtraForm
    {
        List<ETipoDocumentoDetalleCta> lstClasesDocumento = new List<ETipoDocumentoDetalleCta>();
        public int intTipoDoc = 0;
        public frmClaseDocumento()
        {
            InitializeComponent();
        }

        private void frm01CentroCosto_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstClasesDocumento = new BAdministracionSistema().listarTipoDocumentoDetCta(intTipoDoc);
            grdAnalitica.DataSource = lstClasesDocumento;
            viewAnalitica.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstClasesDocumento.FindIndex(x => x.tdocd_iid_correlativo == intIcod);
            viewAnalitica.FocusedRowHandle = index;
            viewAnalitica.Focus();
        }
        private void nuevo()
        {
            FrmManteClaseDocumento frm = new FrmManteClaseDocumento();
            frm.MiEvento += new FrmManteClaseDocumento.DelegadoMensaje(reload);
            frm.intTipoDoc = intTipoDoc;
            frm.lstClasesDocumento = lstClasesDocumento;
            frm.SetInsert();
            frm.txtCodigo.Text = (lstClasesDocumento.Count + 1).ToString();
            frm.Show();
        }
        private void modificar()
        {
            ETipoDocumentoDetalleCta Obe = (ETipoDocumentoDetalleCta)viewAnalitica.GetRow(viewAnalitica.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmManteClaseDocumento frm = new FrmManteClaseDocumento();
            frm.MiEvento += new FrmManteClaseDocumento.DelegadoMensaje(reload);
            frm.intTipoDoc = intTipoDoc;
            frm.lstClasesDocumento = lstClasesDocumento;
            frm.oBe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
        }
        private void viewCentroCosto_DoubleClick(object sender, EventArgs e)
        {
            ETipoDocumentoDetalleCta Obe = (ETipoDocumentoDetalleCta)viewAnalitica.GetRow(viewAnalitica.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmManteClaseDocumento frm = new FrmManteClaseDocumento();
            frm.oBe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            ETipoDocumentoDetalleCta Obe = (ETipoDocumentoDetalleCta)viewAnalitica.GetRow(viewAnalitica.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new BAdministracionSistema().eliminarTipoDocumentoDetCta(Obe);
                cargar();
            }
        }
        private void imprimir()
        {
        }
        private void buscarCriterio()
        {
            grdAnalitica.DataSource = lstClasesDocumento.Where(x =>
                                                   x.tdocd_iid_codigo_doc_det.ToString().Contains(Convert.ToInt32(txtCodigo.Text).ToString().ToUpper()) &&
                                                   x.tdocd_descripcion.Contains(txtDescripcion.Text.ToUpper())
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

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}