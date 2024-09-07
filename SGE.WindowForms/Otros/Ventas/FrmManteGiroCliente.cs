using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Ventas
{
    public partial class FrmManteGiroCliente : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public FrmManteGiroCliente()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
        }

        public List<EGiro> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BVentas Obl;

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
            txtIdGiro.Enabled = !Enabled;
            txtGiro.Enabled = !Enabled;
            LkpSituacion.Enabled = !Enabled;
            chkIndicador.Enabled = !Enabled;
            txtIdGiro.Focus();
        }

        private void clearControl()
        {
            txtIdGiro.Text = "";
            txtGiro.Text = "";
            LkpSituacion.EditValue = null;
            chkIndicador.Checked = false;
        }


        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
            LkpSituacion.ItemIndex = 0;
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
            EGiro oBe = new EGiro();
            Obl = new BVentas();
            try
            {

                if (string.IsNullOrEmpty(txtIdGiro.Text))
                {
                    oBase = txtIdGiro;
                    throw new ArgumentException("Ingresar Código");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarCodigo = oDetail.Where(oB => Convert.ToString(oB.giroc_iid_giro).ToUpper() == txtIdGiro.Text.ToUpper()).ToList();
                    if (BuscarCodigo.Count > 0)
                    {
                        oBase = txtIdGiro;
                        throw new ArgumentException("El Código Existe");
                    }
                }
                oBe.giroc_iid_giro = Convert.ToChar(txtIdGiro.Text);
                oBe.giroc_vnombre_giro = string.IsNullOrEmpty(txtGiro.Text) ? null : txtGiro.Text;
                oBe.tablc_iid_situacion_giro = Convert.ToInt32(LkpSituacion.EditValue);
                oBe.giroc_bindicador_expo_nextel = chkIndicador.Checked;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarGiro(oBe);
                }
                else
                {
                    oBe.giroc_icod_giro = Correlative;
                    Obl.ActualizarGiro(oBe);
                }
            }
            catch (Exception ex)
            {
                oBase.Focus();
                oBase.ErrorText = ex.Message;
                oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        private void txtdescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Escape)
            {
                cerrar_form(sender, e);
            }
        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void FrmManteDetalleAnalitica_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        private void FrmManteGiroCliente_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            var lstEstado = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado);
            BSControls.LoaderLook(LkpSituacion, lstEstado, "tarec_vdescripcion", "tarec_icorrelativo_registro", false);

        }






    }
}