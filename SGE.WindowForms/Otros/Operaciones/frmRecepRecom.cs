using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmRecepRecom : DevExpress.XtraEditors.XtraForm
    {
        public string strRecepRecom = "";
        public frmRecepRecom()
        {
            InitializeComponent();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            strRecepRecom = txtRecepRecom.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}