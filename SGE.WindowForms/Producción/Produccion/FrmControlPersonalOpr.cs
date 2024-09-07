using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Planillas;
using SGE.WindowForms.Otros.Producción;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Producción.Produccion
{
    public partial class FrmControlPersonalOpr : XtraForm
    {
        private List<ControlPersonalOPR> lista = new List<ControlPersonalOPR>();
        public FrmControlPersonalOpr() => InitializeComponent();
        private void FrmControlPersonalOpr_Load(object sender, EventArgs e) => CargaInicial();
        private void CriterioBusquedaChanged(object sender, EventArgs e) => Filtrar();
        private void btnRefresh_Click(object sender, EventArgs e) => Cargar();
        private void consultaOPRToolStripMenuItem_Click(object sender, EventArgs e) => ConsultaOpr();
        private void actualizaPersonalToolStripMenuItem_Click(object sender, EventArgs e) => AsignarPersonal();
        private void CargaInicial() => Cargar();
        private void Cargar()
        {
            lista = new BCentral().ControlPersonalOprListar();
            grdLista.DataSource = lista;
            grdLista.RefreshDataSource();
            viewLista.BestFitColumns();
        }



        private void Filtrar()
        {
            viewLista.Columns["NumeroOrdenProduccion"].FilterInfo = new ColumnFilterInfo("[NumeroOrdenProduccion] LIKE '%" + txtNumeroOPR.Text + "%'");
            viewLista.Columns["NombreCliente"].FilterInfo = new ColumnFilterInfo("[NombreCliente] LIKE '%" + txtCliente.Text + "%'");
        }



        private void ConsultaOpr()
        {
            var Obe = viewLista.GetFocusedRow() as ControlPersonalOPR;
            if (Obe == null) return;
            var frm = new frmManteOrdenProdución();
            frm.MiEvento += new frmManteOrdenProdución.DelegadoMensaje(reload);
            frm.oBe = new BCentral().OrdenProduccionGetById(Obe.IdOrdenProduccion);
            frm.tabControl.SelectedTabPageIndex = 1;
            frm.tabControl.TabPages[0].PageVisible = false;
            frm.FromAsignacion = true;
            frm.SetModify();
            frm.Show();

        }

        private void AsignarPersonal()
        {
            var Obe = viewLista.GetFocusedRow() as ControlPersonalOPR;
            if (Obe == null) return;
            var frm = new frmManteOrdenProduciónPersonal();
            frm.Text = "Asignacion Personal de la OPR : " + Obe.NumeroOrdenProduccion;
            frm.MiEvento += new frmManteOrdenProduciónPersonal.DelegadoMensaje(reload);
            frm.oBe = new BCentral().OrdenProduccionGetById(Obe.IdOrdenProduccion);
            frm.SetModify();
            frm.Show();

        }

        private void reload(int icod)
        {
            Cargar();
        }


    }
}