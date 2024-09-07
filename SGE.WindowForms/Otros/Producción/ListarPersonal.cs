using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Producción;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class ListarPersonal : XtraForm
    {
        public EPVTPersonal _Be;
        private List<EPVTPersonal> mlist = new List<EPVTPersonal>();
        public int icodAreaEspecifica = 0;


        public ListarPersonal()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            EPVTPersonal obj = new EPVTPersonal();
            mlist = new BCentral().ListarRegistroPersonal().Where(x => x.perc_isituacion == (int?)Codigos.PersonalSituacion.Activo).ToList();

            dgrMotonave.DataSource = icodAreaEspecifica == 0 ? mlist : mlist.Where(x => x.perc_iarea_proceso == icodAreaEspecifica).ToList();
            dgrMotonave.RefreshDataSource();
        }
        private void ListarModelo_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            _Be = (EPVTPersonal)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }
        public void reload(int icod)
        {
            Carga();
            int index = mlist.FindIndex(x => x.perc_iid_personal == icod);
            viewMotonave.FocusedRowHandle = index;
            viewMotonave.Focus();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        private void BuscarCriterio()
        {
            viewMotonave.Columns["perc_vcod_personal"].FilterInfo = new ColumnFilterInfo("[perc_vcod_personal] LIKE '%" + txtCodigo.Text + "%'");
            viewMotonave.Columns["perc_vnombres_apellidos"].FilterInfo = new ColumnFilterInfo("[perc_vnombres_apellidos] LIKE '%" + txtdescripcion.Text + "%'");
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManteRegistroPersonal frm = new frmManteRegistroPersonal();
            frm.MiEvento += new frmManteRegistroPersonal.DelegadoMensaje(reload);
            frm.lstRegistroPersonal = mlist;
            frm.SetInsert();
            frm.Show();

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPVTPersonal Obe = (EPVTPersonal)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteRegistroPersonal frm = new frmManteRegistroPersonal();
            frm.MiEvento += new frmManteRegistroPersonal.DelegadoMensaje(reload);
            frm.oBe = Obe;
            frm.txtCodigo.Enabled = false;
            frm.lstRegistroPersonal = mlist;
            frm.SetModify();
            frm.Show();
            frm.anac_icod_analitica = Obe.perc_icod_analitica;
        }

        private void txtdescripcion_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}