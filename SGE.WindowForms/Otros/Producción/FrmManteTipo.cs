using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmManteTipo : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteTipo));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        MyKeyPress myKeyPressHandler = new MyKeyPress();
        public EProdTablaRegistro _Be { get; set; }
        public int TipoRegistro;
        public string NombreFormulario;

        public List<EProdTablaRegistro> mlistaRegistro = new List<EProdTablaRegistro>();

        public FrmManteTipo()
        {

            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
            txtcodigo.KeyPress += new KeyPressEventHandler(myKeyPressHandler.MyKeyCounter);

        }

        public List<EProdTablaRegistro> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BCentral Obl;
        public int Correlative = 0;

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
            //bool Enabled = (Status == BSMaintenanceStatus.View);
            //txtdescripcion.Enabled = !Enabled;
            //txtabreviatura.Enabled = !Enabled;
        }

        private void clearControl()
        {
            if (TipoRegistro == 1 || TipoRegistro == 4 ||
                TipoRegistro == 6 || TipoRegistro == 7)
                txtcodigo.Text = String.Format("{0}", Convert.ToInt32(Correlative));
            if (TipoRegistro == 11 || TipoRegistro == 2 ||
                TipoRegistro == 3 || TipoRegistro == 5 ||
                TipoRegistro == 8 || TipoRegistro == 9 ||
                TipoRegistro == 10)
                txtcodigo.Text = String.Format("{0:00}", Convert.ToInt32(Correlative));
            txtdescripcion.Text = "";
            txtabreviatura.Text = "";
            txtdescripcion.Focus();
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
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
            EProdTablaRegistro oBe = new EProdTablaRegistro();
            Obl = new BCentral();
            try
            {
                if (string.IsNullOrEmpty(txtcodigo.Text))
                {
                    oBase = txtcodigo;
                    throw new ArgumentException("Ingresar Codigo");
                }

                if (string.IsNullOrEmpty(txtdescripcion.Text))
                {
                    oBase = txtdescripcion;
                    throw new ArgumentException("Ingresar Descripcion");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarCosto = oDetail.Where(oB => oB.descripcion.ToUpper() == txtdescripcion.Text.ToUpper()).ToList();
                    if (BuscarCosto.Count > 0)
                    {
                        oBase = txtdescripcion;
                        throw new ArgumentException("La Descripcion Existe");
                    }
                }

                oBe.icod_tabla = 0;
                oBe.tablc_iid_tipo_tabla = TipoRegistro;
                oBe.tarec_icorrelativo_registro = Convert.ToInt32(txtcodigo.Text);
                oBe.tarec_vdescripcion = txtdescripcion.Text;
                oBe.tarec_vabreviatura = txtabreviatura.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.tarec_cestado = (Convert.ToInt32(lkpEstado.EditValue) == 1) ? 'A' : 'I';
                oBe.Estado = (Convert.ToInt32(lkpEstado.EditValue) == 1) ? 'A' : 'I'; ;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarProdTablaRegistro(oBe);
                }
                else
                {
                    oBe.icod_tabla = Correlative;
                    Obl.ModificarProdTablaRegistro(oBe);
                }
            }
            catch (Exception ex)
            {
                oBase.Focus();
                oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                oBase.ErrorText = ex.Message;
                oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        Status = BSMaintenanceStatus.View;

                    }
                    else
                    {
                        Status = BSMaintenanceStatus.View;

                    }
                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();

        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void FrmManteMotonave_Load(object sender, EventArgs e)
        {
            this.Text = "PVT Mantenimiento de " + NombreFormulario;
            BSControls.LoaderLook(lkpEstado, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);


        }

        private void FrmManteMotonave_FormClosing(object sender, FormClosingEventArgs e)
        {
            //   Close();
        }

        private void txtcodigo_Leave(object sender, EventArgs e)
        {

        }


    }
}