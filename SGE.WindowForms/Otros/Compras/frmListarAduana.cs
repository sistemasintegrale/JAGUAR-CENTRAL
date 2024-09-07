using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmListarAduana : DevExpress.XtraEditors.XtraForm
    {
        List<EAgenciaAduana> lstCentroCosto = new List<EAgenciaAduana>();
        public EAgenciaAduana _Be { get; set; }

        public frmListarAduana()
        {
            InitializeComponent();
        }

        private void frm01CentroCosto_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstCentroCosto = new BCompras().ListarAgenciaAduana();
            grdCentroCosto.DataSource = lstCentroCosto;
            viewCentroCosto.Focus();
        }
        private void returnSeleccion()
        {

            _Be = (EAgenciaAduana)viewCentroCosto.GetRow(viewCentroCosto.FocusedRowHandle);
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
                                                   x.idd_aduana.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.razon.Contains(txtDescripcion.Text.ToUpper())
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
            int index = lstCentroCosto.FindIndex(x => x.idd_icod_aduana == intIcod);
            viewCentroCosto.FocusedRowHandle = index;
            viewCentroCosto.Focus();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmManteCentroCosto frm = new frmManteCentroCosto();
            //frm.MiEvento += new frmManteCentroCosto.DelegadoMensaje(reload);
            //frm.lstCentroCosto = lstCentroCosto;
            //frm.SetInsert();
            //frm.Show();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ECentroCosto Obe = (ECentroCosto)viewCentroCosto.GetRow(viewCentroCosto.FocusedRowHandle);
            //if (Obe == null)
            //    return;
            //frmManteCentroCosto frm = new frmManteCentroCosto();
            //frm.MiEvento += new frmManteCentroCosto.DelegadoMensaje(reload);
            //frm.lstCentroCosto = lstCentroCosto;
            //frm.Obe = Obe;
            //frm.SetModify();
            //frm.Show();
            //frm.setValues(); 
        }
    }
}