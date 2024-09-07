using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class ListarFichaTecnica : DevExpress.XtraEditors.XtraForm
    {
        public EFichaTecnicaCab _Be;
        private List<EFichaTecnicaCab> mlist = new List<EFichaTecnicaCab>();
        private int xposition = 0;
        public int CodeMarcas;

        public ListarFichaTecnica()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            EFichaTecnicaCab obj = new EFichaTecnicaCab();
            mlist = new BCentral().ListarFichaTecnicaCab(Parametros.intEjercicio);
            mlist.ForEach(x =>
            {
                x.strFichaTec_ContraMuestra = x.fitec_icod_ficha_tecnica + "-" + x.fitec_icorrelativo_contramuestra;
            });
            dgrMotonave.DataSource = mlist;
        }
        private void ListarModelo_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            _Be = (EFichaTecnicaCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        public void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj =>
                                                 obj.strFichaTec_ContraMuestra.ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                 obj.strmarca.ToString().Contains(txtMarca.Text.ToUpper()) &&
                                                 obj.strmodelo.ToString().Contains(txtModelo.Text.ToUpper()) &&
                                                 obj.strcolor.ToString().Contains(txtColor.Text.ToUpper())
                                                 ).ToList();

        }
    }
}