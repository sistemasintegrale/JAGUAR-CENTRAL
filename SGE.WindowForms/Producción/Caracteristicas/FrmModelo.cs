using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Caracteristicas
{
    public partial class FrmModelo : DevExpress.XtraEditors.XtraForm
    {
        private List<EProdModelo> mlist = new List<EProdModelo>();
        private int xposition = 0;
        public int CodeMarcas;
        public FrmModelo()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            mlist = new BCentral().ModeloListarSimple(CodeMarcas);
            dgrMotonave.DataSource = mlist;
        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
            viewMotonave.FocusedRowHandle = xposition;
        }
        void AddEvent()
        {
            this.viewMotonave.DoubleClick += new System.EventHandler(this.viewMotonave_DoubleClick);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void Datos()
        {
            EProdModelo Obe = (EProdModelo)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe is null) return;
            Obe = new BCentral().ModeloGetById(Convert.ToInt32(Obe.mo_icod_modelo));
            FrmManteModelo Motonave = new FrmManteModelo();
            Motonave.MiEvento += new FrmManteModelo.DelegadoMensaje(Modify);
            Motonave.oDetail = mlist;
            Motonave.Correlative = Convert.ToInt32(Obe.mo_icod_modelo);
            Motonave.Show();
            Motonave.lkpCategoria.EditValue = Obe.pr_vid_tipo;
            Motonave.lkpTipo.EditValue = Obe.pr_vid_categoria;
            Motonave.LkpLinea.EditValue = Obe.pr_vid_linea;
            Motonave.txtcodigo.Text = Obe.mo_viid_modelo;
            Motonave.txtdescripcion.Text = Obe.mo_vdescripcion;
            Motonave.txtcodigo.Enabled = false;
            Motonave.lkpSituacion.EditValue = (Obe.mo_cestado == "A") ? 1 : 0;
            Motonave.lkpSerie.EditValue = Obe.mo_iserie;
            try { Motonave.pictureEdit1.Image = Image.FromStream(WebDavTest.Get($"{Rutas.RutaImagenes}{Rutas.RutaModelos}", $"{Obe.mo_icod_modelo}.png")); }
            catch { }
            Motonave.SetModify();
            Motonave.txtcodigo.ReadOnly = true;
            xposition = viewMotonave.FocusedRowHandle;
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                List<EProdProducto> mlistproducto = new List<EProdProducto>();

                EProdModelo Obe = (EProdModelo)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);

                mlistproducto = new BCentral().ListarProdProductos().Where(ou => ou.pr_iid_modelo == Obe.mo_iid_modelo && ou.pr_tarec_icorrelativo == Obe.tarec_icorrelativo).ToList();
                if (mlistproducto.Count == 0)
                {
                    BCentral oBl = new BCentral();
                    oBl.EliminarProdModelo(Obe);
                    viewMotonave.DeleteRow(viewMotonave.FocusedRowHandle);
                    viewMotonave.RefreshData();
                }
                else
                {
                    XtraMessageBox.Show("No se Puede Eliminar, Existen Productos Creados con el Modelo: " + Obe.mo_vdescripcion, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PreViewPrint();
        }

        private void FrmMotonave_Load(object sender, EventArgs e)
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

            if (mlist.Count > 0)
            {
                FrmManteModelo Motonave = new FrmManteModelo();
                Motonave.MiEvento += new FrmManteModelo.DelegadoMensaje(AddEvent);
                Motonave.Show();
                Motonave.SetCancel();
                EProdModelo Obe = (EProdModelo)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                Motonave.txtcodigo.Text = Obe.mo_viid_modelo;
                Motonave.txtdescripcion.Text = Obe.mo_vdescripcion;
                if (Obe.mo_ruta != null)
                    Motonave.pictureEdit1.Image = Image.FromFile(Obe.mo_ruta);
                Motonave.BtnGuardar.Enabled = false;
                Motonave.txtcodigo.Enabled = false;
            }
            this.viewMotonave.DoubleClick -= new System.EventHandler(this.viewMotonave_DoubleClick);

        }

        private void FrmMotonave_Activated(object sender, EventArgs e)
        {

        }
        private void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj =>
                                                   obj.mo_vdescripcion.ToUpper().Contains(textEdit1.Text.ToUpper())
                                             ).ToList();

        }
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void viewMotonave_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
                nuevoToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F5)
                Datos();
            if (e.KeyCode == Keys.F9)
                eliminarToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F8) { }
            //PreViewPrint();
        }

        private void materialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Materiales();
        }
        private void Materiales()
        {
            FrmMateriales Modelo = new FrmMateriales();
            EProdModelo Obe = (EProdModelo)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe != null)
            {
                Modelo.icodModelo = Convert.ToInt32(Obe.mo_icod_modelo);
                Modelo.Text = String.Format("Modelo :  {0}", Obe.mo_vdescripcion);
                Modelo.Show();
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}