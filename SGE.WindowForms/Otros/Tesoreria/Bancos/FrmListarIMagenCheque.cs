using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class FrmListarIMagenCheque : DevExpress.XtraEditors.XtraForm
    {
        private List<ECajaChica> mlist = new List<ECajaChica>();
        public ECajaChica _Be { get; set; }
        public List<EImagenCheque> lstImagenCheque = new List<EImagenCheque>();
        public FrmListarIMagenCheque()
        {
            InitializeComponent();
        }
        private void Carga()
        {
            mlist = new BTesoreria().ListarCajaChica();

            grdCajaChica.DataSource = mlist;
        }
        private void FrmListarCajaTesoreria_Load(object sender, EventArgs e)
        {
            Carga();
        }
        private void BuscarCriterio()
        {
            grdCajaChica.DataSource = mlist.Where(obj =>
                                                   obj.vdescrip_caja_liquida.ToUpper().Contains(txtDescripcion.Text.ToUpper()) &&
                                                   obj.vnro_caja_liquida.ToUpper().Contains(txtNroCaja.Text.ToUpper())
                                             ).ToList();

        }

        private void txtNroCaja_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        private void DgAcept()
        {
            _Be = (ECajaChica)viewCajaChica.GetRow(viewCajaChica.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }
        private void viewCajaChica_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void viewCajaChica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DgAcept();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManteDiseñoMuestra Proveedor = new frmManteDiseñoMuestra();
            //Proveedor.lstImagenCheque = lstImagenCheque;
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                lstImagenCheque = Proveedor.lstImagenCheque;

            }
        }
    }
}