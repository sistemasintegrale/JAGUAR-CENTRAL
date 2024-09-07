using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Linq;
using System.Security.Principal;



namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmManteSerie : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteSerie));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        MyKeyPress myKeyPressHandler = new MyKeyPress();
        public ERegistroSerie _Be { get; set; }
        public int TipoRegistro;
        public string NombreFormulario;

        public List<ERegistroSerie> mlistaRegistro = new List<ERegistroSerie>();

        public FrmManteSerie()
        {
            
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
            txtcodigo.KeyPress += new KeyPressEventHandler(myKeyPressHandler.MyKeyCounter);
            
        }

        public List<ERegistroSerie> oDetail;
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
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtdescripcion.Enabled = !Enabled;
            
        }

        private void clearControl()
        {
            
            txtcodigo.Text = String.Format("{0:00}", Convert.ToInt32(Correlative));
            txtdescripcion.Text = "";
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
            ERegistroSerie oBe = new ERegistroSerie();
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
                if (string.IsNullOrEmpty(txtTallaI.Text))
                {
                    oBase = txtTallaI;
                    throw new ArgumentException("Ingresar Talla Inicial");
                }
                if (string.IsNullOrEmpty(txtTallaF.Text))
                {
                    oBase = txtTallaF;
                    throw new ArgumentException("Ingresar Talla Final");
                }
                if (Convert.ToInt32(txtTallaI.Text) > Convert.ToInt32(txtTallaF.Text))
                {
                    oBase = txtTallaF;
                    throw new ArgumentException("la Talla final debe ser mayor a la talla inicial");
                }
                
                oBe.resec_icod_registro_serie = txtcodigo.Text;
                oBe.resec_vdescripcion = txtdescripcion.Text;
                oBe.resec_vtalla_inicial = txtTallaI.Text;
                oBe.resec_vtalla_final = txtTallaF.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.vpc = WindowsIdentity.GetCurrent().Name.ToString();
               
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarRegistroSerie(oBe);
                }
                else
                {
                    oBe.resec_iid_registro_serie = Correlative;
                    Obl.ModificarRegistroSerie(oBe);
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
        }

        private void FrmManteMotonave_FormClosing(object sender, FormClosingEventArgs e)
        {
            Close();
        }

        private void txtcodigo_Leave(object sender, EventArgs e)
        {

        }

     
    }
}