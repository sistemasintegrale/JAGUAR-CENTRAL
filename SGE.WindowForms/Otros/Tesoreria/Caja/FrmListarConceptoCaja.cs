using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    public partial class FrmListarConceptoCaja : DevExpress.XtraEditors.XtraForm
    {
        private List<EConceptoMovCaja> mlist = new List<EConceptoMovCaja>();
        public EConceptoMovCaja _Be { get; set; }
        public bool flagConDoc = false;

        public FrmListarConceptoCaja()
        {
            InitializeComponent();
        }

        private void FrmListarConceptoCaja_Load(object sender, EventArgs e)
        {
            Carga();
        }
        private void Carga()
        {
            if (flagConDoc)
                mlist = new BTesoreria().ListarConceptoCaja().Where(x => Convert.ToInt32(x.tdocc_icod_tipo_doc) != 0).ToList();
            else
                mlist = new BTesoreria().ListarConceptoCaja();
            grdCajaChica.DataSource = mlist;
        }
        private void DgAcept()
        {
            _Be = (EConceptoMovCaja)viewCajaChica.GetRow(viewCajaChica.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }
        private void BuscarCriterio()
        {
            grdCajaChica.DataSource = mlist.Where(obj =>
                                                   obj.vdescripcion.ToUpper().Contains(txtDescripcion.Text.ToUpper()) &&
                                                   obj.ccod_concep_mov.ToUpper().Contains(txtNroCaja.Text.ToUpper())
                                             ).ToList();

        }

        private void txtNroCaja_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void viewCajaChica_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void viewCajaChica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DgAcept();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

    }
}