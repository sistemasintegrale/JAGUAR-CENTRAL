using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Mantenimiento
{
    public partial class frm01ParametroContable : DevExpress.XtraEditors.XtraForm
    {
        public frm01ParametroContable()
        {
            InitializeComponent();
        }

        private void rptFrm01ParametroContable_Load(object sender, EventArgs e)
        {

        }

        private void rptFrm01ParametroContable_MouseMove(object sender, MouseEventArgs e)
        {
            this.Close();
            FrmManteParametrosContables frm = new FrmManteParametrosContables();
            frm.Show();
        }

        private void rptFrm01ParametroContable_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            FrmManteParametrosContables frm = new FrmManteParametrosContables();
            frm.Show();
        }
    }
}