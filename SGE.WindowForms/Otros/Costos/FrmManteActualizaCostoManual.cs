using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System;
using System.Windows.Forms;

namespace SGI.WindowsForm.Otros.Costos
{
    public partial class FrmManteActualizaCostoManual : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteActualizaCostoManual));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        private BAlmacen Obl;

        public int IdKardexValorizado = 0;
        public decimal MontoTotal = 0;
        public decimal MontoManual = 0;

        public EKardexValorizado Obe = new EKardexValorizado();

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

        public FrmManteActualizaCostoManual()
        {
            InitializeComponent();
        }

        private void FrmManteActualizaCostoManual_Load(object sender, EventArgs e)
        {
            txtPrecio.Text = MontoManual == 0 ? MontoTotal.ToString() : MontoManual.ToString();
        }


        private void btnGrabar_Click(object sender, EventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }


        #endregion

        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtPrecio.Enabled = !Enabled;

        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }


        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            Obl = new BAlmacen();

            try
            {
                if (Convert.ToDecimal(txtPrecio.Text) == 0)
                {
                    oBase = txtPrecio;
                    throw new ArgumentException("El precio no puede ser 0");
                }

                Obe.kardv_icod_correlativo = IdKardexValorizado;
                Obe.kardv_nmonto_ingreso_manual = Convert.ToDecimal(txtPrecio.Text);

                Obl.ActualizarKardexValorizadoMontoManual(Obe, Convert.ToDecimal(Obe.kardv_nmonto_ingreso_manual));


                //if (Status == BSMaintenanceStatus.CreateNew)
                //{
                //    Obl.InsertarKardexValorizado(oBe);
                //}
                //else
                //{
                //    Obl.ActualizarKardexValorizado(oBe);
                //}
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento();
                    this.Close();
                }
            }
        }


        #endregion

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelarr_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }


    }
}