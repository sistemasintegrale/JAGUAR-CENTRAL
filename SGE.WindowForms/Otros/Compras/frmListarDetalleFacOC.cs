using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmListarDetalleFacOC : DevExpress.XtraEditors.XtraForm
    {
        List<EAlmacen> lstAlmacenes = new List<EAlmacen>();
        List<EDetalleFacOC> lstDetalleFacOC = new List<EDetalleFacOC>();
        public EDetalleFacOC _Be { get; set; }
        public int ococ_icod_orden_compra = 0;
        public int prd_icod_producto = 0;
        public frmListarDetalleFacOC()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {

            lstDetalleFacOC = new BCompras().ListarDetalleFacOC(ococ_icod_orden_compra, prd_icod_producto);
            grdAlmacen.DataSource = lstDetalleFacOC;
            viewAlmacen.Focus();


        }

        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstDetalleFacOC.Count > 0)
            {
                _Be = (EDetalleFacOC)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buscarCriterio()
        {
            grdAlmacen.DataSource = lstAlmacenes.Where(x =>
                                                   x.almac_iid_almacen.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.almac_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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
        void reload(int intIcod)
        {
            cargar();
            int index = lstAlmacenes.FindIndex(x => x.almac_icod_almacen == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManteAlmacen frm = new frmManteAlmacen();
            frm.MiEvento += new frmManteAlmacen.DelegadoMensaje(reload);
            if (lstAlmacenes.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstAlmacenes.Max(x => x.almac_iid_almacen + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.lstAlmacenes = lstAlmacenes;
            frm.SetInsert();
            frm.Show();
            frm.txtNombre.Focus();

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EAlmacen Obe = (EAlmacen)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteAlmacen frm = new frmManteAlmacen();
            frm.MiEvento += new frmManteAlmacen.DelegadoMensaje(reload);
            frm.lstAlmacenes = lstAlmacenes;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtNombre.Focus();
        }
    }
}