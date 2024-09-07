using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmManteIngresoPresupuestoNacional : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteIngresoPresupuestoNacional));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        public int IdPresupuestoNacional = 0;
        public int IdAlmacen = 0;
        public int IdProductoEspecifico = 0;
        public string NumeroPresupuesto = "";
        public long id_kardex;

        private BSMaintenanceStatus mStatus;

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

        public FrmManteIngresoPresupuestoNacional()
        {
            InitializeComponent();
        }

        private void FrmManteIngresoPresupuestoNacional_Load(object sender, EventArgs e)
        {

            dtmFecha.Focus();
        }

        private void dtmFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            EventoKey(sender, e);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

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
            dtmFecha.Enabled = !Enabled;
            dtmFecha.Focus();
        }

        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EPresupuestoNacional oBe = new EPresupuestoNacional();
            try
            {

                //Datos del Presupuesto Nacional
                oBe.prep_icod_presupuesto = IdPresupuestoNacional;
                oBe.krdx_icod_kardex = 0;
                oBe.krdx_sfecha_kardex = Convert.ToDateTime(dtmFecha.EditValue);
                oBe.prep_isituacion = 304;//recibido

                //Datos del Kardex
                EKardex objE_Kardex = new EKardex();
                //objE_Kardex.kardc_iid_correlativo = Convert.ToInt32(id_kardex);
                //objE_Kardex.kardc_ianio = Parametros.intPeriodo;
                //objE_Kardex.kardc_sfecha_movimiento = Convert.ToDateTime(dtmFecha.EditValue);
                //objE_Kardex.kardc_icod_almacen = IdAlmacen;
                //objE_Kardex.kardc_iid_producto_especifico = IdProductoEspecifico;
                //objE_Kardex.kardc_icantidad_prod = Convert.ToDecimal(txtCantidad.EditValue);
                //objE_Kardex.kardc_iid_tipo_doc = Parametros.intTipoDocPresupuestoNacional;
                //objE_Kardex.kardc_inumero_doc = NumeroPresupuesto;
                //objE_Kardex.kardc_itipo_movimiento = Parametros.intTipoMovIngreso;
                //objE_Kardex.kardc_iid_motivo = Parametros.intMovIngCompra;
                //objE_Kardex.kardc_vbeneficiario = txtAlmacen.Text;
                //objE_Kardex.kardc_vobservaciones = "COMPRA DE MERCADERIA";
                //objE_Kardex.kardc_iusuario_crea = Valores.CodeUser;
                //objE_Kardex.kardc_flag_estado = true;

                //Obl.ActualizarPresupuestoNacionalKardex(oBe, objE_Kardex);

            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Flag = false;
                }
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

        private void btnGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            txtCantidad.Focus();
            this.SetSave();
        }

        private void btnSalir_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}