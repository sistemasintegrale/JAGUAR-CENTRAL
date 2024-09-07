using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class ListarModelo : DevExpress.XtraEditors.XtraForm
    {
        public EProdModelo _Be;
        private List<EProdModelo> mlist = new List<EProdModelo>();
        public int CodeMarcas = 0;

        public ListarModelo()
        {
            InitializeComponent();
        }
        public void Carga()
        {


            mlist = new BCentral().ModeloListarSimple(CodeMarcas);
            dgrMotonave.DataSource = mlist;
        }
        private void ListarModelo_Load(object sender, EventArgs e)
        {
            CargarCombos();
            Carga();
        }

        private async void CargarCombos()
        {

            var data = await Task.WhenAll(CargarCombo());

            BSControls.LoaderLookRepository(lkpGrdCategoria, data[0].Item1, "descripcion", "tarec_iid_correlativo", true);
            BSControls.LoaderLookRepository(lkpGrdTipo, data[0].Item2, "descripcion", "tarec_iid_correlativo", true);
            BSControls.LoaderLookRepository(lkpGrdLinea, data[0].Item3, "descripcion", "tarec_iid_correlativo", true);
            BSControls.LoaderLookRepository(lkpgrdMarca, data[0].Item4, nameof(EProdTablaRegistro.descripcion), nameof(EProdTablaRegistro.icod_tabla), true);

        }

        private async Task<Tuple<object, object, object, object>> CargarCombo()
        {
            var lst1 = await Task.Run(() => new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro { iid_tipo_tabla = Parametros.intTipo }));
            var lst2 = await Task.Run(() => new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro { iid_tipo_tabla = Parametros.intCategoria }));
            var lst3 = await Task.Run(() => new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro { iid_tipo_tabla = Parametros.intLinea }));
            var lst4 = await Task.Run(() => new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro { iid_tipo_tabla = Parametros.intMarca }));
            return new Tuple<object, object, object, object>(lst1, lst2, lst3, lst4);
        }


        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            _Be = (EProdModelo)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            _Be = new BCentral().ModeloGetById(_Be.mo_icod_modelo);
            this.DialogResult = DialogResult.OK;
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        private void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj =>

                                                   obj.mo_vdescripcion.ToString().Contains(txtdescripcion.Text.ToUpper())
                                             ).ToList();

        }

        private void dgrMotonave_Click(object sender, EventArgs e)
        {

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteModelo Motonave = new FrmManteModelo();
            Motonave.MiEvento += new FrmManteModelo.DelegadoMensaje(form2_MiEvento);
            viewMotonave.MoveLast();
            EProdModelo Obe = (EProdModelo)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            int? i = 0;
            if (mlist.Count > 0)
                i = mlist.Max(ob => ob.mo_iid_modelo);
            Motonave.Correlative = Convert.ToInt32(i) + 1;
            Motonave.oDetail = mlist;
            Motonave.CodeMarca = CodeMarcas;
            Motonave.Show();
            Motonave.txtcodigo.Enabled = true;
            Motonave.SetInsert();
        }

        public void form2_MiEvento()
        {
            Carga();
        }
    }
}