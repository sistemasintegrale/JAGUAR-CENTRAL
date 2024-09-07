using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Sistema
{
    public partial class FrmRecepcionInfo : DevExpress.XtraEditors.XtraForm
    {
        public FrmRecepcionInfo()
        {
            InitializeComponent();
        }

        private void FrmRecepcionInfo_Load(object sender, EventArgs e)
        {
            //FrmMantRecepcionInfo info = new FrmMantRecepcionInfo();
            //if (info.ShowDialog() == DialogResult.OK)
            //{
            //}
        }

        private void FrmRecepcionInfo_MouseMove(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}