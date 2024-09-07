using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmListarProducto : DevExpress.XtraEditors.XtraForm
    {
        private List<EProdProducto> mlist = new List<EProdProducto>();
        public EProdProducto _Be { get; set; }
        public int condicion;
        public int icod_marca;
        public int indicador_OP = 0;

        public FrmListarProducto()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            //if (condicion==1)
            //{
            //    mlist = new BCentral().ListarProdProductos().Where(x=>x.pr_tarec_icorrelativo==Convert.ToInt32(icod_marca)).ToList();
            //}
            //else if (indicador_OP == 1)
            //{

            //}
            //else
            //{
            //    mlist = new BCentral().ListarProdProductos();
            //}

            //dgrMotonave.DataSource = mlist;
        }

        private void DgAcept()
        {
            _Be = (EProdProducto)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }
        private void FrmListarProducto_Load(object sender, EventArgs e)
        {
            txtCodigo.Focus();
            Carga();
        }

        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Down)
            {
                dgrMotonave.Focus();
                viewMotonave.FocusedRowHandle = 0;
            }
        }

        private void viewMotonave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                DgAcept();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            mlist.Clear();
            dgrMotonave.DataSource = mlist;
            mlist = new BCentral().ListarProdProductosIndexes(txtCodigo.Text, txtDescripcion.Text, txtModelo.Text);
            dgrMotonave.DataSource = mlist;
        }
    }
}