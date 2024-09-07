using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    public partial class FrmListarConceptoCajaEfectivo : DevExpress.XtraEditors.XtraForm
    {
        private List<EConceptoCaja> mlist = new List<EConceptoCaja>();
        public EConceptoCaja _Be { get; set; }
        public bool flagConDoc = false;
        public int icodTipo;

        public FrmListarConceptoCajaEfectivo()
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
                mlist = new BTesoreria().ListarConceptoCajaEfectivo().Where(x => Convert.ToInt32(x.tdocc_icod_tipo_doc) != 0 && x.cmvcc_icod_tipo == icodTipo).ToList();
            else
                mlist = new BTesoreria().ListarConceptoCajaEfectivo().Where(x => x.cmvcc_icod_tipo == icodTipo).ToList();
            grdCajaChica.DataSource = mlist;
        }
        private void DgAcept()
        {
            _Be = (EConceptoCaja)viewCajaChica.GetRow(viewCajaChica.FocusedRowHandle);
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