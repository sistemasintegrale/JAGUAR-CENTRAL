using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarMotonaves : DevExpress.XtraEditors.XtraForm
    {
        public FrmListarMotonaves()
        {
            InitializeComponent();
        }

        private List<EMotonaves> mlist = new List<EMotonaves>();
        public EMotonaves _Be { get; set; }
        public void Carga()
        {
            mlist = new BCompras().ListarMotonaves();
            dgrMotonaves.DataSource = mlist;
        }

        private void BuscarCriterio()
        {
            dgrMotonaves.DataSource = mlist.Where(obj =>
                                                   obj.Descripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.vidd_motonaves.ToString().Contains(txtCodigo.Text.ToUpper())
                                             ).ToList();

        }

        private void viewMotonaves_DoubleClick(object sender, EventArgs e)
        {
            _Be = (EMotonaves)viewMotonaves.GetRow(viewMotonaves.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }

        private void FrmListarMotonaves_Load(object sender, EventArgs e)
        {
            Carga();
            txtCodigo.Focus();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Down)
            {
                dgrMotonaves.Focus();
                viewMotonaves.FocusedRowHandle = 0;
            }
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}