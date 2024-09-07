using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SGE.Common;
using SGE.Entity;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen
{
    public partial class FrmManteConductor : XtraForm
    {
        public EConductor obe = new EConductor();
        public FrmManteConductor() => InitializeComponent();
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e) => Dispose();
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e) => Acept();
        private void FrmManteConductor_Load(object sender, EventArgs e) => CargaInicial();
        private void Acept()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (string.IsNullOrEmpty(txtDNi.Text))
                {
                    oBase = txtDNi;
                    throw new ArgumentException("Ingrese DNI del conductor");
                }
                if (string.IsNullOrEmpty(txtNombres.Text))
                {
                    oBase = txtNombres;
                    throw new ArgumentException("Ingrese Nombre del conductor");
                }
                if (string.IsNullOrEmpty(txtApellidos.Text))
                {
                    oBase = txtApellidos;
                    throw new ArgumentException("Ingrese Apellidos del conductor");
                }
                if (string.IsNullOrEmpty(txtLicencia.Text))
                {
                    oBase = txtLicencia;
                    throw new ArgumentException("Ingrese Licencia del conductor");
                }

                obe.cod_vdni = txtDNi.Text;
                obe.cod_vnombres = txtNombres.Text;
                obe.cod_vapellidos = txtApellidos.Text;
                obe.cod_vlicencia = txtLicencia.Text;
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
                    DialogResult = DialogResult.OK;
                }
            }
        }


        private void CargaInicial()
        {
            SetValues();
        }

        private void SetValues()
        {
            txtDNi.Text = obe.cod_vdni;
            txtNombres.Text = obe.cod_vnombres;
            txtApellidos.Text = obe.cod_vapellidos;
            txtLicencia.Text = obe.cod_vlicencia;
        }

        private async void txtDNi_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDNi.Text.Length == 8)
            {
                picLoading.Visible = true;
                var data = await API_DNI.ConsultaDni(txtDNi.Text);
                txtNombres.Text = data.Nombres;
                txtApellidos.Text = data.ApellidoPaterno + " " + data.ApellidoMaterno;
                picLoading.Visible = false;
            }
            else
            {
                txtNombres.Text = "";
                txtApellidos.Text = "";
            }
        }
    }
}