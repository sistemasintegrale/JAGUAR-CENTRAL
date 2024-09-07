using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmMovimientoVoucher : DevExpress.XtraEditors.XtraForm
    {
        public List<EVoucherContableDet> lstDetalle = new List<EVoucherContableDet>();
        public frmMovimientoVoucher()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmMovimientoVoucher_Load(object sender, EventArgs e)
        {
            grdDetalle.DataSource = lstDetalle;
        }
    }
}