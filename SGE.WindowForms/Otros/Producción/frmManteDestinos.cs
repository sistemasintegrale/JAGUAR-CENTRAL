using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class frmManteDestinos : XtraForm
    {
        public EAreaUbicacion Obe = new EAreaUbicacion();
        internal int icodAreaRef;

        public frmManteDestinos() => InitializeComponent();
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => Dispose();
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => Guardar();
        private void frmManteDestinos_Load(object sender, EventArgs e) => Cargar();

        private void Guardar()
        {
            bool flag = true;
            BaseEdit oBase = null;
            try
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese Descripción");
                }
                Obe.arubd_codigo = Convert.ToInt32(txtCodigo.Text);
                Obe.arprc_iid_codigo = Convert.ToInt32(lkpArea.EditValue);
                Obe.descripcion = txtDescripcion.Text;
                Obe.Estado = 'A';

                if (Obe.arubd_iid_area_ubicacion == 0)

                    new BCentral().InsertarAreaUbicacion(Obe);

                else

                    new BCentral().ModificarAreaUbicacion(Obe);

            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flag = false;
                }
            }
            finally
            {
                if (flag)
                    DialogResult = DialogResult.OK;
            }
        }
        private void Cargar()
        {
            BSControls.LoaderLook(lkpArea, new BCentral().listarAreaProduccion(), "arprc_vdescripcion", "arprc_iid_codigo", true);
            lkpArea.EditValue = icodAreaRef;
            if (Obe.arubd_iid_area_ubicacion != 0)
                SetValues();
        }

        private void SetValues()
        {
            txtCodigo.Text = Obe.arubd_codigo.ToString();
            lkpArea.EditValue = Obe.arprc_iid_codigo;
            txtDescripcion.Text = Obe.descripcion;
        }


    }
}