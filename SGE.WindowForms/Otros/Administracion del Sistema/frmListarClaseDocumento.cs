using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmListarClaseDocumento : DevExpress.XtraEditors.XtraForm
    {
        List<ETipoDocumentoDetalleCta> lstClasesDocumento = new List<ETipoDocumentoDetalleCta>();
        public ETipoDocumentoDetalleCta _Be { get; set; }
        public int intTipoDoc = 0;
        public frmListarClaseDocumento()
        {
            InitializeComponent();
        }
        private void returnSeleccion()
        {
            if (lstClasesDocumento.Count > 0)
            {
                _Be = (ETipoDocumentoDetalleCta)viewAnalitica.GetRow(viewAnalitica.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
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

        private void viewCentroCosto_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }

        private void buscarCriterio()
        {
            grdAnalitica.DataSource = lstClasesDocumento.Where(x =>
                                                   x.tdocd_iid_codigo_doc_det.ToString().Contains(Convert.ToInt32(txtCodigo.Text).ToString().ToUpper()) &&
                                                   x.tdocd_descripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }
    }
}