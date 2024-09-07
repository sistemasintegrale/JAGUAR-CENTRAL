using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Sistema
{
    public partial class FrmEnviarInformacion : DevExpress.XtraEditors.XtraForm
    {
        public FrmEnviarInformacion()
        {
            InitializeComponent();
        }
        private void FrmEnviarInformacion_Load(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Esta opción es un proceso, para generar\nArchivo con los datos Maestros para los\nPuntos de Ventas.\n\n          ¿Desea Continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                FrmMantEnviarInformacion frm = new FrmMantEnviarInformacion();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        private void FrmEnviarInformacion_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void FrmEnviarInformacion_MouseMove(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void marqueeProgressBarControl1_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}