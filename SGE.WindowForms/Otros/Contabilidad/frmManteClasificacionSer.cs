using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmManteClasificacionSer : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteClasificacion));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        //public SGE.WindowForms.Contabilidad.Costos.frm03ClasificacionServicios.EClasificacionServicio oBe = new SGE.WindowForms.Contabilidad.Costos.frm03ClasificacionServicios.EClasificacionServicio();
        public EParametroContable oBeParCtbl = new EParametroContable();


        public frmManteClasificacionSer()
        {
            InitializeComponent();
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
            txtCodigo.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            bteCuenta.Enabled = !Enabled;
            txtCuentaDes.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtDescripcion.Enabled = Enabled;
                txtCodigo.Enabled = Enabled;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = Enabled;
        }

        public void setValues()
        {
            //txtCodigo.Text = String.Format("{0:00}", oBe.icod_clasificacion_serv);
            //txtDescripcion.Text = oBe.vdescripcion_clasificacion_serv;
            //bteCuenta.Tag = oBe.icod_cta_contable;    
            //bteCuenta.Text = oBe.strCodCuentaContable;           
            //txtCuentaDes.Text = oBe.strDesCuentaContable;            
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
                int? intNullVal = null;

                //oBe.icod_cta_contable = (Convert.ToInt32(bteCuenta.Tag) == 0) ? intNullVal : Convert.ToInt32(bteCuenta.Tag);
                //oBe.strCodCuentaContable = bteCuenta.Text;
                //oBe.strDesCuentaContable = txtCuentaDes.Text;

                //if (oBe.icod_clasificacion_serv == 1)
                //    oBeParCtbl.ctacc_icod_cta_ctbl_serv_propio = (Convert.ToInt32(bteCuenta.Tag) == 0) ? intNullVal : Convert.ToInt32(bteCuenta.Tag);
                //else if(oBe.icod_clasificacion_serv == 2)
                //    oBeParCtbl.ctacc_icod_cta_ctbl_serv_externo = (Convert.ToInt32(bteCuenta.Tag) == 0) ? intNullVal : Convert.ToInt32(bteCuenta.Tag);

                new BContabilidad().modificarParamentroContable(oBeParCtbl);
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    //this.MiEvento(oBe.icod_clasificacion_serv);
                    //this.Close();
                }
            }
        }


        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCuenta(sender);
        }

        private void bteCtaCompras_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCuenta(sender);
        }

        private void bteCtaCostos_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCuenta(sender);
        }

        private void listarCuenta(object sender)
        {
            ButtonEdit opcion = (ButtonEdit)sender;
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    opcion.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    opcion.Text = frm._Be.ctacc_numero_cuenta_contable.ToString();
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;
                }
            }
        }

        private void frmManteClasificacion_Load(object sender, EventArgs e)
        {

        }
    }
}