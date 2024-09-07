using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmBalanceComprobacionMov : DevExpress.XtraEditors.XtraForm
    {
        public List<EVoucherContableDet> mlist = new List<EVoucherContableDet>();
        public FrmBalanceComprobacionMov()
        {
            InitializeComponent();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void FrmBalanceComprobacionMov_Load(object sender, EventArgs e)
        {
            grdMov.DataSource = mlist;
            viewMov.RefreshData();
        }
    }
}