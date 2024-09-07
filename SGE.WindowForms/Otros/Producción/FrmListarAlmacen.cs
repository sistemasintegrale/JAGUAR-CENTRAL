using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmListarAlmacen : DevExpress.XtraEditors.XtraForm
    {
        public int puvec_icod_punto_venta;
        public FrmListarAlmacen()
        {
            InitializeComponent();
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private List<EProdAlmacen> mlist = new List<EProdAlmacen>();
        public EProdAlmacen _Be { get; set; }
        public void Carga()
        {

            mlist = new BCentral().ListarProdAlmacen().Where(x => x.puvec_icod_punto_venta == puvec_icod_punto_venta).ToList();
            dgrAlmacen.DataSource = mlist;
        }

        private void BuscarCriterio()
        {
            dgrAlmacen.DataSource = mlist.Where(obj =>
                                                   obj.descripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.idf_almacen.ToString().Contains(txtCodigo.Text.ToUpper())
                                             ).ToList();

        }

        private void DgAcept()
        {
            _Be = (EProdAlmacen)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }
        private void FrmListarAlmacen_Load(object sender, EventArgs e)
        {
            txtCodigo.Focus();
            Carga();
        }

        private void viewAlmacen_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Down)
            {
                dgrAlmacen.Focus();
                viewAlmacen.FocusedRowHandle = 0;
            }
        }

        private void viewAlmacen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                DgAcept();
        }


    }
}