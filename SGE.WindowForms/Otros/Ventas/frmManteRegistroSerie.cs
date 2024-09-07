using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteRegistroSerie : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteRegistroSerie));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ESeries Obe = new ESeries();
        public List<ESeries> lstSeries = new List<ESeries>();

        public frmManteRegistroSerie()
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
            txtSerie.Enabled = !Enabled;
            txtCorrelativo.Enabled = !Enabled;
        }
        public void setValues()
        {
            txtSerie.Text = Obe.rs_vserie;
            txtCorrelativo.Text = String.Format("{0:00000000}", Obe.rs_icorrelativo);
            lkpPuntoVent.EditValue = Obe.rs_icod_pvt;
            lkpTipoDocumento.EditValue = Obe.rs_itipo_documento;
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
                if ((txtSerie.Text) == "")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese Serie");
                }
                if (Convert.ToInt32(lkpTipoDocumento.EditValue) == 0)
                {
                    oBase = txtCorrelativo;
                    throw new ArgumentException("Ingrese Tipo Documento");
                }
                /*----------------------*/

                Obe.rs_vserie = txtSerie.Text;
                Obe.rs_icorrelativo = Convert.ToInt32(txtCorrelativo.Text);
                Obe.rs_icod_pvt = Convert.ToInt32(lkpPuntoVent.EditValue);
                Obe.rs_itipo_documento = Convert.ToInt32(lkpTipoDocumento.EditValue);
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.rs_icod_registro_serie = new BVentas().insertarSeries(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarSeries(Obe);
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
                    this.MiEvento(Obe.rs_icod_registro_serie);
                    this.Close();
                }
            }
        }

        //private bool verificarNombre(string strNombre)
        //{
        //    try
        //    {
        //        bool exists = false;
        //        if (lstPersonal.Count > 0)
        //        {
        //            if (Status == BSMaintenanceStatus.CreateNew)
        //            {
        //                if (lstPersonal.Where(x => x.vdrsc_vnombre_vendedor.Replace(" ","").Trim() == strNombre.Replace(" ","").Trim()).ToList().Count > 0)
        //                    exists = true;
        //            }
        //            if (Status == BSMaintenanceStatus.ModifyCurrent)
        //            {
        //                if (lstPersonal.Where(x => x.vdrsc_vnombre_vendedor.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim() && x.vdrsc_icod_vendedor != Obe.vdrsc_icod_vendedor).ToList().Count > 0)
        //                    exists = true;
        //            }
        //        }
        //        return exists;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private bool verificarCodigo(string strCodigo) 
        //{
        //    try 
        //    {
        //        bool exists = false;
        //        if (lstPersonal.Count > 0)
        //        {
        //            if (Status == BSMaintenanceStatus.CreateNew)
        //            {
        //                if (lstPersonal.Where(x => x.vdrsc_vcod_vendedor == strCodigo).ToList().Count > 0)
        //                    exists = true;
        //            }
        //            if (Status == BSMaintenanceStatus.ModifyCurrent)
        //            {
        //                if (lstPersonal.Where(x => x.vdrsc_vcod_vendedor == strCodigo && x.vdrsc_icod_vendedor != Obe.vdrsc_icod_vendedor).ToList().Count > 0)
        //                    exists = true;
        //            }
        //        }
        //        return exists;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}        

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmMantePersonal_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpPuntoVent, new BCentral().ListarPuntoVenta(), "puvec_vdescripcion", "puvec_icod_punto_venta", true);
            BSControls.LoaderLook(lkpTipoDocumento, new BGeneral().listarTablaRegistro(96), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

        }

        private void lkpArea_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}