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
    public partial class FrmManteRegistroFirmas : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteRegistroFirmas));
        private BSMaintenanceStatus mStatus;
        private List<ERegistroFirmas> lstRegistroFirma = new List<ERegistroFirmas>();
        BCompras objComprasData = new BCompras();

        public FrmManteRegistroFirmas()
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
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            StatusControl();
        }
        private void cargar()
        {
            /*----------------------------------------------------------------------------*/
            lstRegistroFirma = objComprasData.listarRegistroFirmas();
            /*----------------------------------------------------------------------------*/
            if (lstRegistroFirma.Count > 0)
                SetModify();
            else
                SetInsert();
        }

        private void FrmManteParametrosContables_Load(object sender, EventArgs e)
        {
            cargar();
            txtElavoradoOCL.Text = lstRegistroFirma[0].regf_ocl_elavorado;
            txtAutorizadoOCL.Text = lstRegistroFirma[0].regf_ocl_autorizado;
            txtRevisadoOCL.Text = lstRegistroFirma[0].regf_ocl_revisado;
            txtAprobado1.Text = lstRegistroFirma[0].regf_oci_aprobado1;
            txtAprobado2.Text = lstRegistroFirma[0].regf_oci_aprobado2;
            txtAprobado3.Text = lstRegistroFirma[0].regf_oci_aprobado3;
            txtAprobado4.Text = lstRegistroFirma[0].regf_oci_aprobado4;
            txtElavoradoOCS.Text = lstRegistroFirma[0].regf_ocs_elavorado;
            txtRevisadoOCS.Text = lstRegistroFirma[0].regf_ocs_revisado;
        }
        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            bool Flag = false;
            string mask = "";
            try
            {
                string mask2 = "";
                string mask3 = "";

                //if (lkpIngBancos.EditValue == null)
                //{
                //    oBase = lkpIngBancos;
                //    throw new ArgumentException("Seleccione Sub-Diario de Ing. de Bancos");
                //}
                //if (lkpEgrBancos.EditValue == null)
                //{
                //    oBase = lkpEgrBancos;
                //    throw new ArgumentException("Seleccione Sub-Diario de Egr. de Bancos");
                //}
                //if (lkpCajaChica.EditValue == null)
                //{
                //    oBase = lkpCajaChica;
                //    throw new ArgumentException("Seleccione Sub-Diario de Caja Chica");
                //}
                //if (lkpCostoVenta.EditValue == null)
                //{
                //    oBase = lkpCostoVenta;
                //    throw new ArgumentException("Seleccione Sub-Diario de Costo de Venta");
                //}
                //if (lkpDocXPagar.EditValue == null)
                //{
                //    oBase = lkpDocXPagar;
                //    throw new ArgumentException("Seleccione Sub-Diario de Docs. por Pagar");
                //}
                //if (lkpDocXCobrar.EditValue == null)
                //{
                //    oBase = lkpDocXCobrar;
                //    throw new ArgumentException("Seleccione Sub-Diario de Docs. por Cobrar");
                //}
                //if (lkpApertura.EditValue == null)
                //{
                //    oBase = lkpApertura;
                //    throw new ArgumentException("Seleccione Sub-Diario de Apertura");
                //}
                //if (LkpCierreAnual.EditValue == null)
                //{
                //    oBase = LkpCierreAnual;
                //    throw new ArgumentException("Seleccione Sub-Diario de Cierre Anual");
                //}
                ERegistroFirmas Obe = new ERegistroFirmas();
                if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    Obe.regf_icod_registro_firmas = lstRegistroFirma[0].regf_icod_registro_firmas;
                }
                Obe.regf_ocl_elavorado = txtElavoradoOCL.Text;
                Obe.regf_ocl_autorizado = txtAutorizadoOCL.Text;
                Obe.regf_ocl_revisado = txtRevisadoOCL.Text;
                Obe.regf_oci_aprobado1 = txtAprobado1.Text;
                Obe.regf_oci_aprobado2 = txtAprobado2.Text;
                Obe.regf_oci_aprobado3 = txtAprobado3.Text;
                Obe.regf_oci_aprobado4 = txtAprobado4.Text;
                Obe.regf_ocs_elavorado = txtElavoradoOCS.Text;
                Obe.regf_ocs_revisado = txtRevisadoOCS.Text;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    objComprasData.insertarRegistroFirmas(Obe);
                    Flag = true;
                }
                else
                {
                    if (XtraMessageBox.Show("\t\t\t\tLos datos serán actualizados\n ¿Está seguro que desea continuar con la grabación?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        objComprasData.modificarRegistroFirmas(Obe);
                        Flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
            }
            finally
            {
                if (Flag)
                    this.Close();
            }
        }

        private void FrmManteParametrosContables_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }


    }
}
