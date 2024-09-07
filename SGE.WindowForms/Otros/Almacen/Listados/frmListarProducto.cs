using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Listados
{
    public partial class frmListarProducto : DevExpress.XtraEditors.XtraForm
    {
        List<EProducto> lstProductos = new List<EProducto>();
        public EProducto _Be { get; set; }
        public bool flag_solo_prods = false;
        public bool stock = false;
        public frmListarProducto()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {

            lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion).ToList();
            if (stock)
                lstProductos = lstProductos.Where(x => x.prdc_nMonto_stock > 0).ToList();
            grdProducto.DataSource = lstProductos;
            viewProducto.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstProductos.FindIndex(x => x.prdc_icod_producto == intIcod);
            viewProducto.FocusedRowHandle = index;
            viewProducto.Focus();
        }

        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstProductos.Count > 0)
            {
                _Be = (EProducto)viewProducto.GetRow(viewProducto.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buscarCriterio()
        {
            if (txtCodigo.Text == "" && txtDescripcion.Text == "")
            {
                lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion).ToList();
            }
            else
            {
                lstProductos = new BAlmacen().listarProductoCodigoDescrip(Parametros.intEjercicio, txtCodigo.Text, txtDescripcion.Text);
            }
            grdProducto.DataSource = lstProductos;
            grdProducto.RefreshDataSource();
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
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void nuevo()
        {
            frmManteProductoAntiguo frm = new frmManteProductoAntiguo();
            frm.MiEvento += new frmManteProductoAntiguo.DelegadoMensaje(reload);
            frm.lstProductos = lstProductos;
            frm.SetInsert();
            frm.Show();

        }
        private void modificar()
        {
            EProducto Obe = (EProducto)viewProducto.GetRow(viewProducto.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteProductoAntiguo frm = new frmManteProductoAntiguo();
            frm.MiEvento += new frmManteProductoAntiguo.DelegadoMensaje(reload);
            frm.lstProductos = lstProductos;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewProducto.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewProducto.ClearColumnsFilter();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_EditValueChanged(object sender, EventArgs e)
        {
            buscarCriterio();
        }
    }
}