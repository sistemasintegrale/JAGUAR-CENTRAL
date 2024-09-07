using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmDetalleStockValorizadoInicial : DevExpress.XtraEditors.XtraForm
    {
        public List<EKardexValorizado> lstKardexVal = new List<EKardexValorizado>();
        public delegate void DelegadoMensaje(Int64 intIcod);
        public event DelegadoMensaje MiEvento;
        public FrmDetalleStockValorizadoInicial()
        {
            InitializeComponent();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void Carga()
        {
            grdStock.DataSource = lstKardexVal;
        }
        void reload(Int64 intIcod)
        {
            viewStock.RefreshData();

        }
        private void modificar()
        {
            EKardexValorizado oBe = (EKardexValorizado)viewStock.GetRow(viewStock.FocusedRowHandle);
            if (oBe == null)
                return;

            FrmManteIngresoStockInicialValorizado frm = new FrmManteIngresoStockInicialValorizado();
            frm.MiEvento += new FrmManteIngresoStockInicialValorizado.DelegadoMensaje(reload);
            frm.oBe = oBe;
            frm.Show();
            frm.SetModify();
            frm.setValues();
            reload(0);

        }
        private void eliminar()
        {
            EKardexValorizado oBe = (EKardexValorizado)viewStock.GetRow(viewStock.FocusedRowHandle);
            if (oBe == null)
                return;

            if (XtraMessageBox.Show("¿Está seguro que desea eliminar registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EKardexValorizado Obe = (EKardexValorizado)viewStock.GetRow(viewStock.FocusedRowHandle);
                new BAlmacen().eliminarKardexValorizado(oBe);
                viewStock.DeleteRow(viewStock.FocusedRowHandle);
            }

        }

        private void FrmDetalleStockValorizadoInicial_Load(object sender, EventArgs e)
        {
            grdStock.DataSource = lstKardexVal;
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FrmDetalleStockValorizadoInicial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lstKardexVal.Count > 0)
                this.MiEvento(lstKardexVal[0].kardv_icod_correlativo);
        }
    }
}