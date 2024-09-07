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
    public partial class FrmManteConceptoNacional : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteConceptoNacional));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public List<EConceptoPresupuestoNacional> oDetail;
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

        public FrmManteConceptoNacional()
        {
            InitializeComponent();
        }

        private void FrmManteConceptoNacional_Load(object sender, EventArgs e)
        {

        }

        private void txtcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            EventoKey(sender, e);
        }

        private void txtdescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            EventoKey(sender, e);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            this.SetSave();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region "Metodos"

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
            EConceptoPresupuestoNacional oBe = new EConceptoPresupuestoNacional();

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
                    var BuscarCosto = oDetail.Where(oB => oB.cpn_vdescripcion_concepto_nacional.ToUpper() == txtdescripcion.Text.ToUpper()).ToList();
                    if (BuscarCosto.Count > 0)
                    {
                        oBase = txtdescripcion;
                        throw new ArgumentException("La Descripcion Existe");
                    }

                    var CodigoRepetido = oDetail.Where(oB => oB.cpn_viid_concepto_nacional.ToUpper() == txtcodigo.Text.ToUpper()).ToList();
                    if (CodigoRepetido.Count > 0)
                    {
                        oBase = txtcodigo;
                        throw new ArgumentException("El Codigo ya Existe");
                    }
                }
                oBe.cpn_icod_concepto_nacional = 0;
                oBe.cpn_iid_concepto_nacional = Convert.ToInt32(txtcodigo.Text);
                oBe.cpn_vdescripcion_concepto_nacional = txtdescripcion.Text;
                oBe.cpn_iusuario_crea = null;
                oBe.cpn_iusuario_modifica = null;
                oBe.cpn_vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.cpn_vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BCompras().InsertarConceptoPresupuestoNacional(oBe);
                }
                else
                {
                    oBe.cpn_icod_concepto_nacional = Correlative;
                    new BCompras().ActualizarConceptoPresupuestoNacional(oBe);
                }
            }
            catch (Exception ex)
            {
                ////oBase.Focus();
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

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


    }
}