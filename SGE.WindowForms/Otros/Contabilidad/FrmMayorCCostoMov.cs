using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmMayorCCostoMov : DevExpress.XtraEditors.XtraForm
    {
        public List<EVoucherContableDet> mlist = new List<EVoucherContableDet>();
        public FrmMayorCCostoMov()
        {
            InitializeComponent();
        }

        private void FrmMayorCCostoMov_Load(object sender, EventArgs e)
        {
            grdMov.DataSource = mlist;
            viewMov.RefreshData();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            bool activo = viewMov.OptionsView.ShowAutoFilterRow;
            viewMov.ClearColumnsFilter();
            if (activo)
            {
                btnFiltrar.Text = "Mostrar filtro";
                viewMov.OptionsView.ShowAutoFilterRow = !activo;
            }
            else
            {
                btnFiltrar.Text = "Ocultar filtro";
                viewMov.OptionsView.ShowAutoFilterRow = !activo;
            }
        }
    }
}