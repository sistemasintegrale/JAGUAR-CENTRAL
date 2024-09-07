using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmManteMotonave : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteMotonave));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public EMotonaves _Be { get; set; }
        public FrmManteMotonave()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();

        }

        public List<EMotonaves> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
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
            txtcodigo.Text = String.Format("{0:0000}", Convert.ToInt32(Correlative));
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
            EMotonaves oBe = new EMotonaves();

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
                    var BuscarCosto = oDetail.Where(oB => oB.Descripcion.ToUpper() == txtdescripcion.Text.ToUpper()).ToList();
                    if (BuscarCosto.Count > 0)
                    {
                        oBase = txtdescripcion;
                        throw new ArgumentException("La Descripcion Existe");
                    }

                    var CodigoRepetido = oDetail.Where(oB => oB.vidd_motonaves.ToUpper() == txtcodigo.Text.ToUpper()).ToList();
                    if (CodigoRepetido.Count > 0)
                    {
                        oBase = txtcodigo;
                        throw new ArgumentException("El Codigo ya Existe");
                    }

                }

                oBe.idd_icod_motonaves = 0;
                oBe.idd_motonaves = Convert.ToInt32(txtcodigo.Text);
                oBe.Descripcion = txtdescripcion.Text;
                oBe.estado = 1;
                oBe.usuario_crea = null;
                oBe.fecha_crea = null;
                oBe.usuario_modifica = null;
                oBe.fecha_modifica = null;
                oBe.usuario_pc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.usuario_pc_modifica = WindowsIdentity.GetCurrent().Name.ToString();

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BCompras().InsertarMotonaves(oBe);
                }
                else
                {
                    oBe.idd_icod_motonaves = Correlative;
                    new BCompras().ModificarMotonaves(oBe);
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
                        Status = BSMaintenanceStatus.View;
                    else
                        Status = BSMaintenanceStatus.View;

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
            this.Close();
        }

        private void FrmManteMotonave_Load(object sender, EventArgs e)
        {

        }

        private void FrmManteMotonave_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }
    }
}