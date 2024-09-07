using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmMantePresupuestoNacionalDetalle : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantePresupuestoNacionalDetalle));
        public EPresupuestoNacionalDetalle oBE;
        public decimal Cantidad = 0;

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

        public FrmMantePresupuestoNacionalDetalle()
        {
            InitializeComponent();
        }

        private void FrmMantePresupuestoNacionalDetalle_Load(object sender, EventArgs e)
        {
            //CargaControles();
        }

        private void txtMontoTotal_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtMontoTotal.Text) > 0)
            {
                CalcularMontoUnitario(Convert.ToDecimal(txtMontoTotal.Text));
            }
            else
            {
                txtMontoUnitario.Text = "0.00";
                txtMontoUnitario.EditValue = 0;
            }
        }

        private void txtMontoOrigen_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtMontoOrigen.EditValue) > 0)
            {
                if (Convert.ToInt32(lkpMoneda.EditValue) == Parametros.intDolares)
                {
                    ConvertirSoles(Convert.ToDecimal(txtMontoOrigen.EditValue));
                }
            }
        }

        #endregion





        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            lkpMoneda.Enabled = !Enabled;
            txtMontoTotal.Enabled = !Enabled;
            txtMontoUnitario.Enabled = !Enabled;
            txtMontoOrigen.Enabled = !Enabled;
            txtMontoTotal.Focus();
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void CargaControles()
        {
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5).Where(ob => ob.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

        }

        private void SetSave()
        {
            {
                {
                    BaseEdit oBase = null;
                    Boolean Flag = true;
                    oBE = new EPresupuestoNacionalDetalle();
                    try
                    {
                        if (lkpMoneda.EditValue == null)
                        {
                            oBase = lkpMoneda;
                            throw new ArgumentException("Seleccione moneda");
                        }
                        oBE.prepd_nmont_tot_concepto = Convert.ToDecimal(txtMontoTotal.Text);
                        oBE.prepd_nmont_unit_concepto = Convert.ToDecimal(txtMontoUnitario.Text);
                        oBE.tablc_iid_tipo_moneda_origen = Convert.ToInt32(lkpMoneda.EditValue);
                        oBE.TipoMoneda = Convert.ToInt32(lkpMoneda.EditValue) == Parametros.intSoles ? "S/." : "US$";
                        oBE.prepd_nmont_tot_concepto_origen = Convert.ToDecimal(txtMontoOrigen.Text);

                        this.DialogResult = DialogResult.OK;
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
                            this.Close();
                        }
                    }
                }
            }
        }

        private void CalcularMontoUnitario(decimal MontoTotal)
        {
            //txtMontoUnitario.Text = (MontoTotal / Cantidad).ToString();
        }

        private void ConvertirSoles(decimal MontoOrigen)
        {
            txtMontoTotal.EditValue = MontoOrigen * Convert.ToDecimal(txtTipoDeCambio.EditValue);
        }

        #endregion

        private void btnGuardar_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void btnSalir_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }


    }
}