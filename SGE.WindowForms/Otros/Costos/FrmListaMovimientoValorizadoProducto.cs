using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Costos
{
    public partial class FrmListaMovimientoValorizadoProducto : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        private List<EKardexValorizado> mLista = new List<EKardexValorizado>();
        bool bModif;
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public string Producto = "";
        public string UM = "";
        public DateTime FechaIni;
        public DateTime FechaFin;
        public int IdAlmacen = 0;
        public int IdProducto = 0;

        #endregion

        #region "Eventos"

        public FrmListaMovimientoValorizadoProducto()
        {
            InitializeComponent();
        }

        private void FrmListaMovimientoValorizadoProducto_Load(object sender, EventArgs e)
        {
            txtProducto.Text = Producto;
            txtUM.Text = UM;

            Carga();
        }

        #endregion

        #region "Metodos"

        public void Carga()
        {
            mLista = new BAlmacen().ListarKardexValorizadoInventario(IdAlmacen, IdProducto, FechaIni, FechaFin);
            txtMovimiento.Text = mLista.Count.ToString();
            grdKardex.DataSource = mLista;
        }


        #endregion

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Carga();
        }

        private void modificarFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModificarFecha(true);
            viewKardex.FocusedColumn = gridColumn1;
        }

        private void viewKardex_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ModificarFecha(false);
        }

        private void viewKardex_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (!(viewKardex.FocusedColumn == gridColumn1))
            {
                ModificarFecha(false);
            }
        }

        private void viewKardex_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (XtraMessageBox.Show("¿Desea modificar la fecha?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EKardexValorizado Obe = (EKardexValorizado)viewKardex.GetFocusedRow();
                Obe.kardv_sfecha_movimiento = Convert.ToDateTime(e.Value, null);
                new BAlmacen().ActualizarFechaKardexValorizado(Obe);
                Carga();
                bModif = true;
            }
            ModificarFecha(false);
        }

        private void ModificarFecha(bool estado)
        {
            gridColumn1.OptionsColumn.AllowEdit = estado;
            gridColumn1.OptionsColumn.AllowFocus = estado;
        }

        private void FrmListaMovimientoValorizadoProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bModif)
            {
                MiEvento();
            }
        }
    }
}