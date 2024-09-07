using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmMantePercepcionDetalle : DevExpress.XtraEditors.XtraForm
    {
        public int id_tipo_moneda = 0;
        public int id_proveedor = 0;
        private BSMaintenanceStatus mStatus;
        public EPercepcionDet oBe = new EPercepcionDet();
        public List<EPercepcionDet> lstDetalle = new List<EPercepcionDet>();

        public frmMantePercepcionDetalle()
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                bteNroDocumento.Enabled = Enabled;
        }

        private void bteNroDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarDoc();
        }

        public void setValues()
        {
            txtDocumento.Text = oBe.strTipoDoc;
            txtDocumento.Tag = oBe.tdoc_icod_tipo_documento;
            bteNroDocumento.Text = oBe.percd_vnro_doc;
            bteNroDocumento.Tag = oBe.percd_icod_dxp;
            dtFecha.EditValue = oBe.percd_sfecha_doc;
            lkpTipoMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            txtMontoDoc.Text = oBe.percd_nmonto_doc.ToString();
            txtMontoPercepcion.Text = oBe.percd_nmonto_percibido_doc.ToString();
        }

        private void listarDoc()
        {
            FrmListarDocumentoPorPagarProveedor frm = new FrmListarDocumentoPorPagarProveedor();
            frm.flag_list_all = false;
            frm.bDocFacBol = true;
            frm.intMoneda = id_tipo_moneda;
            frm.intIcodProveedor = id_proveedor;
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bteNroDocumento.Tag = Convert.ToInt64(frm._oBe.doxpc_icod_correlativo);
                txtDocumento.Tag = Convert.ToInt32(frm._oBe.tdocc_icod_tipo_doc);
                txtDocumento.Text = frm._oBe.tdocc_vabreviatura_tipo_doc;
                bteNroDocumento.Text = frm._oBe.doxpc_vnumero_doc;
                dtFecha.EditValue = frm._oBe.doxpc_sfecha_doc;
                lkpTipoMoneda.EditValue = frm._oBe.tablc_iid_tipo_moneda;
                txtMontoDoc.Text = Convert.ToDecimal(frm._oBe.doxpc_nmonto_total_documento).ToString();
            }
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

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToDecimal(txtMontoPercepcion.Text) == 0)
                {
                    oBase = txtMontoPercepcion;
                    throw new ArgumentException("Ingresar el monto de percepción");
                }

                if (Convert.ToDecimal(txtMontoPercepcion.Text) > Convert.ToDecimal(txtMontoDoc.Text))
                {
                    oBase = txtMontoPercepcion;
                    throw new ArgumentException("El monto de percepción no debe ser mayor al monto del documento");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                    if (lstDetalle.Where(x => x.percd_icod_dxp == Convert.ToInt32(bteNroDocumento.Tag)).ToList().Count > 0)
                    {
                        oBase = bteNroDocumento;
                        throw new ArgumentException("El documento seleccionado ya fue añadido en el detalle");
                    }

                oBe.tdoc_icod_tipo_documento = Convert.ToInt32(txtDocumento.Tag);
                oBe.percd_icod_dxp = Convert.ToInt64(bteNroDocumento.Tag);
                oBe.percd_vnro_doc = bteNroDocumento.Text;
                oBe.percd_sfecha_doc = Convert.ToDateTime(dtFecha.EditValue);
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpTipoMoneda.EditValue);
                oBe.percd_nmonto_doc = Convert.ToDecimal(txtMontoDoc.Text);
                oBe.percd_nmonto_percibido_doc = Convert.ToDecimal(txtMontoPercepcion.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                /**/
                oBe.strMoneda = lkpTipoMoneda.Text;
                oBe.strTipoDoc = txtDocumento.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                    if (oBe.intTipoOperacion != 1)
                        oBe.intTipoOperacion = 2;

            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Flag = false;
            }
            finally
            {
                if (Flag)
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmMantePercepcionDetalle_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpTipoMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                setValues();
        }
    }
}