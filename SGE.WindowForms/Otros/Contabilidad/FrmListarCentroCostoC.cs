using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmListarCentroCostoC : DevExpress.XtraEditors.XtraForm
    {
        private List<ECentroCosto> mlist = new List<ECentroCosto>();
        private int xposition = 0;
        public int intIdModulo = 0;
        public ECentroCosto _Be { get; set; }
        public FrmListarCentroCostoC()
        {
            InitializeComponent();
        }

        public void Carga()
        {
            mlist = new BContabilidad().listarCentroCosto();
            if (intIdModulo == Parametros.intModuloCtasPorPagar || intIdModulo == Parametros.intModuloCtasPorCobrar) //documentos por cobrar o pagar
                gridControl1.DataSource = mlist.ToList();//los que están activos
            else
                gridControl1.DataSource = mlist;//sin filtro
        }

        private void FrmListarCentroCosto_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void BuscarCriterio()
        {
            gridControl1.DataSource = mlist.Where(obj =>
                                                   obj.cecoc_vdescripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.cecoc_vcodigo_centro_costo.ToUpper().Contains(txtCodigo.Text.ToUpper())
                                             ).ToList();

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            _Be = (ECentroCosto)gridView1.GetRow(gridView1.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _Be = (ECentroCosto)gridView1.GetRow(gridView1.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}