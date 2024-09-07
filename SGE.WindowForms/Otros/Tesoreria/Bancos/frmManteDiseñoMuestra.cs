using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class frmManteDiseñoMuestra : DevExpress.XtraEditors.XtraForm
    {
        public string ChosenFile = "";
        public EImagenCheque _be = new EImagenCheque();
        public List<EImagenCheque> lstImagenCheque = new List<EImagenCheque>();

        public frmManteDiseñoMuestra()
        {
            InitializeComponent();
        }

        private void frmManteFacturaCompra_Load(object sender, EventArgs e)
        {
            //getRuta();
            if (lstImagenCheque.Count > 0)
            {
                _be = lstImagenCheque.FirstOrDefault();
                var ruta = _be.ic_vruta;

                //MemoryStream ms = new MemoryStream(ruta);

            }







        }


        public void setValues()
        {

        }



        private void SetSave()
        {
            string cadena = "";
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {



            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {


                }
            }
        }

        private void listarProveedor()
        {
            //FrmListarProveedor frm = new FrmListarProveedor();
            //frm.Carga();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    bteProveedor.Tag = frm._Be.iid_icod_proveedor;
            //    bteProveedor.Text = frm._Be.vcod_proveedor;
            //    txtNombreProveedor.Text = frm._Be.vnombrecompleto;
            //    txtdireccion.Text = frm._Be.vdireccion;
            //    txtruc.Text = frm._Be.vruc;
            //    txtfax.Text = frm._Be.vfax;
            //    txttelefono.Text = frm._Be.vtelefono;
            //    txtmail.Text = frm._Be.proc_vcorreo;
            //}
        }
        byte[] File = null;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            getRuta();
            FileInfo Files;
            OpenFd.InitialDirectory = ruta;
            OpenFd.Title = "Insertar Imagen";
            OpenFd.FileName = "";
            OpenFd.Filter = "JPG Imagenes(*.jpg)|*.jpg|JPEG Imagenes(*.jpeg)|*.jpeg|PNG Imagenes(*.png)|*.png";

            if (OpenFd.ShowDialog() == DialogResult.OK)
            {

                ChosenFile = OpenFd.FileName;
                Files = new FileInfo(OpenFd.FileName);

                pictureEdit1.Image = Image.FromFile(ChosenFile);

                //MemoryStream ms = new MemoryStream();
                //pictureEdit1.Image.Save(ChosenFile);

                Stream mySTream = OpenFd.OpenFile();
                using (MemoryStream ms = new MemoryStream())
                {
                    mySTream.CopyTo(ms);
                    File = ms.ToArray();
                }


            }
        }
        public string ruta;
        public int CountRuta;
        private void getRuta()
        {
            try
            {
                var lst = new BAdministracionSistema().listarParametro();

                ruta = lst[0].pmprc_vruta_cheque;
                CountRuta = lst[0].pmprc_vruta_cheque.Trim().Length;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string Rutamage = ChosenFile.Substring(CountRuta);
            //_be.ic_vruta = File;
            _be.ic_vruta = Rutamage;
            lstImagenCheque.Add(_be);

            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ChosenFile = null;
            pictureEdit1.Image = null;
        }
    }
}