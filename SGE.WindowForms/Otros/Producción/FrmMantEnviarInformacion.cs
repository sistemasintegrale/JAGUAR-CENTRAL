using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Ionic.Utils.Zip;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmMantEnviarInformacion : DevExpress.XtraEditors.XtraForm
    {

        string rutaArchivo;
        List<ESendInfo> mlistaInfo = new List<ESendInfo>();

        public FrmMantEnviarInformacion()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog dialogoRuta = new FolderBrowserDialog();
            if (dialogoRuta.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = dialogoRuta.SelectedPath;
            }
        }
        private void cargar()
        {
            mlistaInfo = (new BCentral()).ListarSendInfo();
            dgrSendInfo.DataSource = mlistaInfo;
        }
        private void FrmMantEnviarInformacion_Load(object sender, EventArgs e)
        {

            cargar();
        }
        private void SetSave()
        {
            List<ETablaRegistro> mlistaRegistro = new List<ETablaRegistro>();
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (rutaArchivo == null || rutaArchivo == "")
                {
                    oBase = textEdit1;
                    throw new ArgumentException("Establesca un Direcctorio /n para guardar la Informacion");
                }
                textEdit1.Focus();
                int k = 0;

                bool[] marcados = new bool[viewSendInfo.RowCount];

                for (int i = 0; i < viewSendInfo.RowCount; i++)
                {
                    marcados[i] = Convert.ToBoolean(viewSendInfo.GetRowCellValue(i, "estado"));
                }

                foreach (ESendInfo obj in mlistaInfo)
                {
                    obj.estado = marcados[k];
                    k++;
                }

                progressBarControl1.Properties.Step = 1;
                progressBarControl1.Properties.PercentView = true;
                progressBarControl1.Properties.Maximum = 10;
                progressBarControl1.Properties.Minimum = 0;
                new BCentral().crearArchivoTransferencia(rutaArchivo, mlistaInfo, progressBarControl1);

                this.DialogResult = DialogResult.OK;
                progressBarControl1.PerformStep();
                progressBarControl1.Update();
            }
            catch (Exception ex)
            {
                //oBase.Focus();
                oBase.ErrorText = ex.Message;
                oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {

                }
            }
        }

        private void btnTransferir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            progressBarControl1.Enabled = true;
            this.SetSave();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBarControl1.Properties.Step = 10000;
            progressBarControl1.Properties.PercentView = true;
            progressBarControl1.Properties.Maximum = 10;
            progressBarControl1.Properties.Minimum = 2;
            progressBarControl1.PerformStep();
            progressBarControl1.Update();
        }

        private void progressBarControl1_EditValueChanged(object sender, EventArgs e)
        {

        }

    }

}