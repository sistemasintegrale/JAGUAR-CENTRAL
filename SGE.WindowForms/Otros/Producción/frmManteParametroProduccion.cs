using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class frmManteParametroProduccion : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteParametroProduccion));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public EParametroProduccion oBe = new EParametroProduccion();

        public frmManteParametroProduccion()
        {
            InitializeComponent();
        }

        private void FrmVariables_Load(object sender, EventArgs e)
        {

        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
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

                if (string.IsNullOrEmpty(txtNroOrdenPedido.Text))
                {
                    oBase = txtNroOrdenPedido;
                    throw new ArgumentException("Ingrese numero de orden de pedido");
                }
                if (string.IsNullOrEmpty(txtNroFichaTecnica.Text))
                {
                    oBase = txtNroFichaTecnica;
                    throw new ArgumentException("Ingrese numero de la ficha tecnica");
                }
                if (string.IsNullOrEmpty(txtOrdenProduccion.Text))
                {
                    oBase = txtOrdenProduccion;
                    throw new ArgumentException("Ingrese numero de orden de producción");
                }
                oBe.pmprc_inumero_nota_pedido = Convert.ToInt32(txtNotaPedido.Text);
                oBe.pmprc_inumero_orden_pedido = Convert.ToInt32(txtNroOrdenPedido.Text);
                oBe.pmprc_inumero_ficha_tecnica = Convert.ToInt32(txtNroFichaTecnica.Text);
                oBe.pmprc_inumero_orden_produccion = Convert.ToInt32(txtOrdenProduccion.Text);
                oBe.pmprc_vruta = txtRuta.Text;

                //RUTAS NUBE

                oBe.pmprc_vruta_base = txtRutaBase.Text;
                oBe.pmprc_vficha_tecnica = txtRutaFichaTecnica.Text;
                oBe.pmprc_vorden_pedido = txtRutaOrdenPedido.Text;
                oBe.pmprc_vorden_produccion = txtRutaOrdenProduccion.Text;
                oBe.pmprc_vmodelo = txtRutaModelo.Text;


                new BAdministracionSistema().modificarParametroProduccion(oBe);

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

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}