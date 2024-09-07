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
    public partial class FrmManteAgenciaAduana : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteAgenciaAduana));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public List<EAgenciaAduana> oDetail;
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

        #endregion

        #region "Eventos"

        public FrmManteAgenciaAduana()
        {
            InitializeComponent();

        }

        private void FrmManteAgenciaAduana_Load(object sender, EventArgs e)
        {

        }

        private void txtcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            EventoKey(sender, e);
        }

        private void txtrazon_KeyPress(object sender, KeyPressEventArgs e)
        {
            EventoKey(sender, e);
        }

        private void txtdireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            EventoKey(sender, e);
        }

        private void txttelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtemail_KeyPress(object sender, KeyPressEventArgs e)
        {
            EventoKey(sender, e);
        }

        private void txtruc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtrazon.Enabled = !Enabled;
            txtdireccion.Enabled = !Enabled;
            txttelefono.Enabled = !Enabled;
            txtemail.Enabled = !Enabled;
            txtruc.Enabled = !Enabled;

        }

        private void clearControl()
        {
            txtcodigo.Text = String.Format("{0:000}", Convert.ToInt32(Correlative));
            txtrazon.Text = "";
            txtdireccion.Text = "";
            txttelefono.Text = "";
            txtemail.Text = "";
            txtruc.Text = "";
            txtrazon.Focus();
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
            EAgenciaAduana oBe = new EAgenciaAduana();

            try
            {
                if (string.IsNullOrEmpty(txtcodigo.Text))
                {
                    oBase = txtcodigo;
                    throw new ArgumentException("Ingresar Codigo");
                }

                if (string.IsNullOrEmpty(txtrazon.Text))
                {
                    oBase = txtrazon;
                    throw new ArgumentException("Ingresar Razon Social o Nombre");
                }
                if (txtruc.Text != "")
                {
                    if (txtruc.Text.Length < 11)
                    {
                        oBase = txtruc;
                        throw new ArgumentException("La Longitud del ruc debe ser igual a 11");
                    }
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarCosto = oDetail.Where(oB => oB.razon.ToUpper() == txtrazon.Text.ToUpper()).ToList();
                    if (BuscarCosto.Count > 0)
                    {
                        oBase = txtrazon;
                        throw new ArgumentException("La Descripcion Existe");
                    }

                    var CodigoRepetido = oDetail.Where(oB => oB.vidd_aduana.ToUpper() == txtcodigo.Text.ToUpper()).ToList();
                    if (CodigoRepetido.Count > 0)
                    {
                        oBase = txtcodigo;
                        throw new ArgumentException("El Codigo ya Existe");
                    }

                }

                oBe.idd_icod_aduana = 0;
                oBe.idd_aduana = Convert.ToInt32(txtcodigo.Text);
                oBe.razon = txtrazon.Text;
                oBe.Direccion = txtdireccion.Text;
                oBe.telefono = txttelefono.Text;
                oBe.email = txtemail.Text;
                oBe.ruc = txtruc.Text;
                oBe.estado = 1;
                oBe.usuario_crea = 0;
                oBe.add_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.usuario_modifica = 0;
                oBe.add_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BCompras().InsertarAgenciaAduana(oBe);
                }
                else
                {
                    oBe.idd_icod_aduana = Correlative;
                    new BCompras().ModificarAgenciaAduana(oBe);
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

        void EventoKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
            if (e.KeyChar == (char)Keys.Enter)
                this.btnGrabar_Click(sender, e);
        }


        #endregion


    }
}