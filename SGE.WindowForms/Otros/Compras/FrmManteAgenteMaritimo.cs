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
    public partial class FrmManteAgenteMaritimo : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteAgenteMaritimo));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public List<EAgenteMaritimo> oDetail;
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

        public FrmManteAgenteMaritimo()
        {
            InitializeComponent();
        }

        private void FrmManteAgenteMaritimo_Load(object sender, EventArgs e)
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
            EventoKey(sender, e);
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
            EAgenteMaritimo oBe = new EAgenteMaritimo();

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
                    var BuscarCosto = oDetail.Where(oB => oB.agm_vrazon.ToUpper() == txtrazon.Text.ToUpper()).ToList();
                    if (BuscarCosto.Count > 0)
                    {
                        oBase = txtrazon;
                        throw new ArgumentException("La Descripcion Existe");
                    }

                    var CodigoRepetido = oDetail.Where(oB => oB.agm_iid_maritimo.ToString().ToUpper() == txtcodigo.Text.ToUpper()).ToList();
                    if (CodigoRepetido.Count > 0)
                    {
                        oBase = txtcodigo;
                        throw new ArgumentException("El Codigo ya Existe");
                    }

                }

                oBe.agm_icod_maritimo = 0;
                oBe.agm_iid_maritimo = Convert.ToInt32(txtcodigo.Text);
                oBe.agm_vrazon = txtrazon.Text;
                oBe.agm_vDireccion = txtdireccion.Text;
                oBe.agm_vtelefono = txttelefono.Text;
                oBe.agm_vemail = txtemail.Text;
                oBe.agm_vruc = txtruc.Text;
                oBe.agm_iid_usuario_crea = 0;
                oBe.agm_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.agm_iid_usuario_modifica = 0;
                oBe.agm_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.agm_flag_estado = true;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BCompras().InsertarAgenteMaritimo(oBe);
                }
                else
                {
                    oBe.agm_icod_maritimo = Correlative;
                    new BCompras().ModificarAgenteMaritimo(oBe);
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