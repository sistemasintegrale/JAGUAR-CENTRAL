using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmListarPersonal : DevExpress.XtraEditors.XtraForm
    {
        List<EPersonal> lstPersonal = new List<EPersonal>();
        public EPersonal _Be { get; set; }

        public frmListarPersonal()
        {
            InitializeComponent();
        }

        private void frm01CentroCosto_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstPersonal = new BPlanillas().listarPersonal();
            grdCentroCosto.DataSource = lstPersonal;
            viewCentroCosto.Focus();
        }
        private void returnSeleccion()
        {

            _Be = (EPersonal)viewCentroCosto.GetRow(viewCentroCosto.FocusedRowHandle);
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
            grdCentroCosto.DataSource = lstPersonal.Where(x =>
                                                   x.perc_iid_personal.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.ApellNomb.Contains(txtDescripcion.Text.ToUpper())
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
            int index = lstPersonal.FindIndex(x => Convert.ToInt32(x.perc_iid_personal) == intIcod);
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