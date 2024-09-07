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
    public partial class ListarFichaTecnicaRef : DevExpress.XtraEditors.XtraForm
    {
        public EFichaTecnicaCab _Be;
        private List<EFichaTecnicaCab> mlist = new List<EFichaTecnicaCab>();
        private int xposition = 0;
        public int CodeMarcas;

        public ListarFichaTecnicaRef()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            //EFichaTecnicaCab obj = new EFichaTecnicaCab() { tarec_icorrelativo = CodeMarcas };
            //mlist = new BCentral().ListarProdModelo(obj);
            mlist = new BCentral().ListarFichaTecnicaCab(Parametros.intEjercicio);
            mlist.ForEach(x =>
            {
                x.fitec_icod_ficha_tecnica = x.fitec_icod_ficha_tecnica + "-" + x.fitec_icorrelativo_contramuestra.ToString();
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
        private void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj =>
                                                   obj.fitec_icod_ficha_tecnica.ToUpper().Contains(txtCodigo.Text.ToUpper())
                                             //&& obj.fitec_sfecha_ficha_tecnica.ToString().Contains(txtdescripcion.Text.ToUpper())
                                             ).ToList();

        }
    }
}