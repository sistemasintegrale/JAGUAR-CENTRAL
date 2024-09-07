using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmMantePXPImportacion : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMantePXPImportacion));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EDXPImportacion Obe = new EDXPImportacion();
        public List<EDXPImportacion> lstDXPImportacion = new List<EDXPImportacion>();
        public int impc_icod_importacion = 0;


        public frmMantePXPImportacion()
        {
            InitializeComponent();
        }
        private void frmManteBanco_Load(object sender, EventArgs e)
        {
            CargarControles();
        }
        public void CargarControles()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            }

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
            txtConcepto.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            //if (Status == BSMaintenanceStatus.ModifyCurrent)
            //    txtCodigo.Enabled = Enabled;
            //if (Status == BSMaintenanceStatus.CreateNew)
            //    txtCodigo.Enabled = Enabled;        
        }
        public void setValues()
        {
            //txtDescripcion.Text = Obe.hjcd1_vdescripcion;
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
                if (String.IsNullOrEmpty(txtConcepto.Text))
                {
                    oBase = txtConcepto;
                    throw new ArgumentException("Ingrese Descripcion");
                }
                Obe.impd_icod_importacion_detalle = Convert.ToInt32(bteRubro.Tag);
                Obe.impc_icod_importacion = Convert.ToInt32(bteNroImportacion.Tag);
                Obe.dxpd2_nmonto_importacion = Convert.ToDecimal(txtMonto.Text);
                Obe.Rubros = bteRubro.Text;
                Obe.Concepto = txtConcepto.Text;
                Obe.NumImpo = bteNroImportacion.Text;
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.dxpd2_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.intTipoOperacion = 1;
                    lstDXPImportacion.Add(Obe);
                }
                else
                {
                    if (Obe.intTipoOperacion != 1)
                    {
                        Obe.intTipoOperacion = 2;
                    }
                }

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
                    this.DialogResult = DialogResult.OK;
                    this.Close();
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

        private void bteNroImportacion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarImportaciones();
        }
        public void ListarImportaciones()
        {
            try
            {
                FrmListarDXPImportacion frm = new FrmListarDXPImportacion();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteNroImportacion.Tag = frm._Be.impc_icod_importacion;
                    bteNroImportacion.Text = frm._Be.impc_vnumero_importacion;
                    impc_icod_importacion = Convert.ToInt32(bteNroImportacion.Tag);
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bteRubro_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarRubros();
        }
        public void ListarRubros()
        {
            try
            {
                FrmListarRubros frm = new FrmListarRubros();
                frm.impc_icod_importacion = impc_icod_importacion;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteRubro.Tag = frm._Be.impd_icod_importacion_detalle;
                    bteRubro.Text = frm._Be.cpnd_vdescripcion;
                    //txtMoneda.Text = frm._Be.TipoMoneda;
                    txtConcepto.Text = frm._Be.cpn_vdescripcion_concepto_nacional;
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}