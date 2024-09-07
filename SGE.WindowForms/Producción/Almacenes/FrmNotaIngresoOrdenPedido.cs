using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Producción;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Almacenes
{
    public partial class FrmNotaIngresoOrdenPedido : XtraForm
    {
        private List<ENotaIngresoOP> lista;
        public FrmNotaIngresoOrdenPedido() => InitializeComponent();
        private void simpleButton1_Click(object sender, EventArgs e) => Cargar();
        private void FrmNotaIngresoOrdenPedido_Load(object sender, EventArgs e) => Cargar();
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => Nuevo();
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => Modificar();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => Eliminar();
        private void textEdit1_EditValueChanged(object sender, EventArgs e) => Filtar();
        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e) => Services.ExportarExcel(grdLista);
        public void Reload(int icod)
        {
            Cargar();
            int index = lista.FindIndex(x => x.niop_icod_nota_ingreso == icod);
            viewLista.FocusedRowHandle = index;
        }
        private void Cargar()
        {
            lista = new BCentral().NotaIngresoOpListar();
            grdLista.DataSource = lista;
            grdLista.RefreshDataSource();
        }
        private void Modificar()
        {
            var obe = viewLista.GetFocusedRow() as ENotaIngresoOP;
            if (obe is null) return;
            var frm = new FrmManteNotaIngresoOP();
            frm.MiEvento += new FrmManteNotaIngresoOP.DelegadoMensaje(Reload);
            frm.Text = $"Modificando NI/OP N° {obe.niop_inumero}";
            frm.objCab = obe;
            frm.Show();
        }
        private void Eliminar()
        {
            var obe = viewLista.GetFocusedRow() as ENotaIngresoOP;
            if (obe is null) return;
            if (Services.MessageQuestion("Está Seguro de Eliminar el Registro?") != DialogResult.Yes) return;
            obe.intUsuario = Valores.intUsuario;
            new BCentral().NotaIngresoOpEliminar(obe);
            Cargar();
        }

        private void Nuevo()
        {
            var frm = new FrmManteNotaIngresoOP();
            frm.MiEvento += new FrmManteNotaIngresoOP.DelegadoMensaje(Reload);
            frm.Text = "Nueva NI/OP";
            frm.txtNumero.Text = (lista.Any() ? (lista.Max(x => x.niop_inumero) + 1) : 1).ToString("d4");
            frm.Show();
        }

        private void Filtar()
        {
            var filters = new Dictionary<string, string>();
            filters.Add("niop_inumero", txtNumero.Text);
            Services.FilterView(viewLista, filters);
        }

    }

}