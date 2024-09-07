using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmListarOCIDetalle : DevExpress.XtraEditors.XtraForm
    {
        List<EOrdenCompraImportacion> lstHojaOCI = new List<EOrdenCompraImportacion>();
        public EOrdenCompraImportacion _Be { get; set; }
        public int CodOC = 0;
        public frmListarOCIDetalle()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {

            lstHojaOCI = new BCompras().ListarOrdenCompraImportacionDetalle(CodOC).Where(x => x.prdc_icod_producto > 0).ToList();
            grdRubros.DataSource = lstHojaOCI;
            viewRubros.Focus();

        }

        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstHojaOCI.Count > 0)
            {
                _Be = (EOrdenCompraImportacion)viewRubros.GetRow(viewRubros.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buscarCriterio()
        {
            //grdRubros.DataSource = lstHojaOCL.Where(x =>
            //                                       x.hjcd3_vcodigo_concepto_hc.ToString().Contains(txtCodigo.Text.ToUpper()) &&
            //                                       x.hjcd3_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
            //                                 ).ToList();
        }


        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}