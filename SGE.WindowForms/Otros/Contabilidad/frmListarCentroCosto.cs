using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmListarCentroCosto : DevExpress.XtraEditors.XtraForm
    {
        List<ECentroCosto> lstCentroCosto = new List<ECentroCosto>();
        public ECentroCosto _Be { get; set; }

        public frmListarCentroCosto()
        {
            InitializeComponent();
        }

        private void frm01CentroCosto_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstCentroCosto = new BContabilidad().listarCentroCosto();
            grdCentroCosto.DataSource = lstCentroCosto;
            viewCentroCosto.Focus();
        }
        private void returnSeleccion()
        {

            _Be = (ECentroCosto)viewCentroCosto.GetRow(viewCentroCosto.FocusedRowHandle);
            if (_Be == null)
                return;
            this.DialogResult = DialogResult.OK;

        }
        private void viewCentroCosto_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void buscarCriterio()
        {
            grdCentroCosto.DataSource = lstCentroCosto.Where(x =>
                                                   x.cecoc_vcodigo_centro_costo.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.cecoc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
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
            int index = lstCentroCosto.FindIndex(x => x.cecoc_icod_centro_costo == intIcod);
            viewCentroCosto.FocusedRowHandle = index;
            viewCentroCosto.Focus();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManteCentroCosto frm = new frmManteCentroCosto();
            frm.MiEvento += new frmManteCentroCosto.DelegadoMensaje(reload);
            frm.lstCentroCosto = lstCentroCosto;
            frm.SetInsert();
            frm.Show();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ECentroCosto Obe = (ECentroCosto)viewCentroCosto.GetRow(viewCentroCosto.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteCentroCosto frm = new frmManteCentroCosto();
            frm.MiEvento += new frmManteCentroCosto.DelegadoMensaje(reload);
            frm.lstCentroCosto = lstCentroCosto;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
        }
    }
}