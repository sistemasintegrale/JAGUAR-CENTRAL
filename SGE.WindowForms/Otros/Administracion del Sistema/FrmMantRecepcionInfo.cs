using DevExpress.XtraEditors;
using System;
using System.IO;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class FrmMantRecepcionInfo : DevExpress.XtraEditors.XtraForm
    {
        string filePath = "";
        public FrmMantRecepcionInfo()
        {
            InitializeComponent();
        }

        private void btnBuscarDirec_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                string Extension = Path.GetExtension(ofd.FileName);
                if (Extension == ".zip")
                {
                    filePath = ofd.FileName;
                }
                else
                {
                    XtraMessageBox.Show("La Extensión del Archivo no es válido", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void btnprocesar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (filePath != "")
            {
                //(new BAdministracionSistema()).RecepcionInfo(filePath);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show("Elija el Archivo a Transferir", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


    }
}