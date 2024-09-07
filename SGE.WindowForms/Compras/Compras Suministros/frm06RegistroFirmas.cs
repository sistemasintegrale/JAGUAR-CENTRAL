using SGE.WindowForms.Otros.Compras;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Compras_Suministros
{
    public partial class frm06RegistroFirmas : DevExpress.XtraEditors.XtraForm
    {
        public frm06RegistroFirmas()
        {
            InitializeComponent();
        }

        private void rptFrm01ParametroContable_Load(object sender, EventArgs e)
        {

        }

        private void rptFrm01ParametroContable_MouseMove(object sender, MouseEventArgs e)
        {
            this.Close();
            FrmManteRegistroFirmas frm = new FrmManteRegistroFirmas();
            frm.Show();
        }

        private void rptFrm01ParametroContable_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            FrmManteRegistroFirmas frm = new FrmManteRegistroFirmas();
            frm.Show();
        }
    }
}