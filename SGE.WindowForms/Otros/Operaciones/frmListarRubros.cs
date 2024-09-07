using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmListarRubros : DevExpress.XtraEditors.XtraForm
    {
        List<EHojaCostosRubros> lstHojaCostosRubros = new List<EHojaCostosRubros>();
        public EHojaCostosRubros _Be { get; set; }
        public int IcodRubros = 0;
        public frmListarRubros()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {

            lstHojaCostosRubros = new BVentas().listarHojaCostosRubrosRQM(IcodRubros).Where(x => x.tablc_icod_tipo_rubro == 313 || x.prdc_icod_producto > 0 || x.hjcd3_ncantidad_saldo > 0).ToList();
            grdRubros.DataSource = lstHojaCostosRubros;
            viewRubros.Focus();

        }

        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstHojaCostosRubros.Count > 0)
            {
                _Be = (EHojaCostosRubros)viewRubros.GetRow(viewRubros.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buscarCriterio()
        {
            grdRubros.DataSource = lstHojaCostosRubros.Where(x =>
                                                   x.hjcd3_vcodigo_concepto_hc.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.hjcd3_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
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