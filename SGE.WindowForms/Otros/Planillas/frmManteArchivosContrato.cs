using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmManteArchivosContrato : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteArchivosContrato));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EArchivos Obe = new EArchivos();
        public List<EArchivos> lstArchivos = new List<EArchivos>();

        public frmManteArchivosContrato()
        {
            InitializeComponent();
        }
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtCodigo.Enabled = false;
            txtDescripcion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            txtRuta.Enabled = !Enabled;


        }
        public void setValues()
        {
            txtCodigo.Text = String.Format("{0:0000}", Obe.arch_iid_correlativo);
            txtDescripcion.Text = Obe.arch_vdescripcion;
            txtRuta.Text = Obe.arch_vruta;


        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (Convert.ToInt32(txtCodigo.Text) == 0)
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código");
                }
                if (verificarCodigo(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los registros de personal");
                }
                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese Apellidos y Nombres");
                }
                /*----------------------*/

                if (verificarNombre(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("El nombre ingresado ya existe en los registros de personal");
                }

                /*----------------------*/
                /*----------------------*/

                Obe.arch_iid_correlativo = Convert.ToInt32(txtCodigo.Text);
                Obe.arch_vdescripcion = txtDescripcion.Text;
                Obe.arch_vruta = txtRuta.Text;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.arch_flag_estado = true;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.intTipoOperacion = 1;
                    lstArchivos.Add(Obe);
                }
                else
                {
                    if (Obe.intTipoOperacion != 1)
                        Obe.intTipoOperacion = 2;
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private bool verificarNombre(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstArchivos.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstArchivos.Where(x => x.arch_vdescripcion.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstArchivos.Where(x => x.arch_vdescripcion.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim() && x.arch_icod_archivos != Obe.arch_icod_archivos).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool verificarCodigo(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstArchivos.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstArchivos.Where(x => x.arch_iid_correlativo == Convert.ToInt32(strCodigo)).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstArchivos.Where(x => x.arch_iid_correlativo == Convert.ToInt32(strCodigo) && x.arch_icod_archivos != Obe.arch_icod_archivos).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmMantePersonal_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            Process.Start(openFileDialog1.FileName);
            txtRuta.Text = openFileDialog1.FileName;
        }
    }
}