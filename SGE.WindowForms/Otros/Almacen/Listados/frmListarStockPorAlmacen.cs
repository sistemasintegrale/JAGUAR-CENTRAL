using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Listados
{
    public partial class frmListarStockPorAlmacen : DevExpress.XtraEditors.XtraForm
    {
        List<EStock> lstStockProductos = new List<EStock>();
        public EStock _Be { get; set; }
        public int intAlmacen = 0;
        public Boolean afecto;
        public frmListarStockPorAlmacen()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstStockProductos = new BAlmacen().listarStockPorAlmacenOptimizado(Parametros.intEjercicio, intAlmacen, "", "");
            grdStock.DataSource = lstStockProductos;
            viewStock.Focus();
        }

        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstStockProductos.Count > 0)
            {
                _Be = (EStock)viewStock.GetRow(viewStock.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buscarCriterio()
        {
            grdStock.DataSource = new BAlmacen().listarStockPorAlmacenOptimizado(Parametros.intEjercicio, intAlmacen, txtCodigo.Text.Trim(), txtDescripcion.Text.Trim());
            grdStock.DataSource = lstStockProductos.Where(x =>
                                                   x.strCodProducto.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.strDesProducto.Contains(txtDescripcion.Text.ToUpper())
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

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewStock.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewStock.ClearColumnsFilter();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}