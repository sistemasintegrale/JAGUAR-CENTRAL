using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmTipoImpresion : DevExpress.XtraEditors.XtraForm
    {
        public int id_situacion;
        public FrmTipoImpresion()
        {
            InitializeComponent();
        }

        private void FrmTipoImpresion_Load(object sender, EventArgs e)
        {

        }



        private void cbosituacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (cbosituacion.Text == "Todos")
            {
                id_situacion = 2;
            }
            if (cbosituacion.Text == "Activo")
            {
                id_situacion = 1;
            }
            if (cbosituacion.Text == "Inactivo")
            {
                id_situacion = 0;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}