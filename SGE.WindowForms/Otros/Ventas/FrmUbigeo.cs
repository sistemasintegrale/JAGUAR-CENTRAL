using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmUbigeo : XtraForm
    {
        public EUbigeo select;
        public FrmUbigeo() => InitializeComponent();

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e) => Dispose();

        private void FrmUbigeo_Load(object sender, EventArgs e) => Cargar();

        private void Cargar()
        {
            var lista = new BVentas().UbigeoListar();
            grdLista.DataSource = lista;

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e) => Aceptar();

        private void Aceptar()
        {
            select = viewLista.GetFocusedRow() as EUbigeo;
            DialogResult = DialogResult.OK;
        }

        private void viewLista_DoubleClick(object sender, EventArgs e) => Aceptar();

        private void txtDepartamento_EditValueChanged(object sender, EventArgs e) => Filtrar();

        private void Filtrar()
        {
            viewLista.Columns["departamento"].FilterInfo = new ColumnFilterInfo("[departamento] LIKE '%" + txtDepartamento.Text + "%'");
            viewLista.Columns["provincia"].FilterInfo = new ColumnFilterInfo("[provincia] LIKE '%" + txtProvincia.Text + "%'");
            viewLista.Columns["distrito"].FilterInfo = new ColumnFilterInfo("[distrito] LIKE '%" + txtDistrito.Text + "%'");
        }
    }
}