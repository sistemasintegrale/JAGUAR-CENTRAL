using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmRegistroFondosPensionesConceptosMixtas : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroFondosPensionesConceptosMixtas));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        public EFondosPensionesMixtas Obe = new EFondosPensionesMixtas();
        public List<EFondosPensionesMixtas> lstFondosPensionesMixtas = new List<EFondosPensionesMixtas>();
        public int intIcodFondosPensiones = 0;

        public frmRegistroFondosPensionesConceptosMixtas()
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
            txtNombre.Enabled = !Enabled;
            txtPorcentaje.Enabled = !Enabled;
            txtMontoTope.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;


            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = Enabled;
            txtNombre.Enabled = !Enabled;
            txtPorcentaje.Enabled = !Enabled;
            txtMontoTope.Enabled = !Enabled;


        }
        public void setValues()
        {
            txtCodigo.Text = String.Format("{0:00}", Obe.fdpd2_iid_vcodigo_fp_concepto_mixto);
            txtNombre.Text = Obe.fdpd2_vdescripcion_concepto_mixto;
            txtPorcentaje.Text = Obe.fdpd2_nporcentaje_concepto_mixto.ToString();
            txtMontoTope.Text = Obe.fdpd2_ntope_concepto_mixto.ToString();
            intIcodFondosPensiones = Convert.ToInt32(Obe.fdpc_icod_fondo_pension);






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
                if (String.IsNullOrEmpty(txtNombre.Text))
                {
                    oBase = txtNombre;
                    throw new ArgumentException("Ingrese Descripción Conceptos Comisión Mixta ");
                }
                if (verificarNombreAlmacen(txtNombre.Text))
                {
                    oBase = txtNombre;
                    throw new ArgumentException("La Descripción ingresado ya existe en Conceptos Comisión Mixta");
                }
                if (Convert.ToDouble(txtPorcentaje.Text) == 0.00)
                {
                    oBase = txtPorcentaje;
                    throw new ArgumentException("Ingrese Porcentaje de Conceptos Comisión Mixta");
                }
                if (String.IsNullOrEmpty(txtMontoTope.Text))
                {
                    oBase = txtMontoTope;
                    throw new ArgumentException("Ingrese Monto Tope de Conceptos Comisión Mixta");
                }

                if (Convert.ToDecimal(txtPorcentaje.Text) >= 100)
                {
                    oBase = txtPorcentaje;
                    throw new ArgumentException("EL Porcentaje no debe ser MAYOR al 100%");
                }
                //if (Convert.ToDecimal(txtMontoTope.Text) >= 100)
                //{
                //    oBase = txtMontoTope;
                //    throw new ArgumentException("EL % Mixto no debe ser MAYOR al 100%");
                //}


                Obe.fdpd2_iid_vcodigo_fp_concepto_mixto = txtCodigo.Text;
                Obe.fdpd2_vdescripcion_concepto_mixto = txtNombre.Text;
                Obe.fdpd2_nporcentaje_concepto_mixto = Convert.ToDecimal(txtPorcentaje.Text);
                Obe.fdpd2_ntope_concepto_mixto = Convert.ToDecimal(txtMontoTope.Text);
                Obe.fdpd2_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.fdpc_icod_fondo_pension = intIcodFondosPensiones;




                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.fdpd2_icod_fp_concepto_mixto = new BPlanillas().insertarFondosPensionesMixtas(Obe);
                    new BPlanillas().modificarPorcentajeFondoMixto(intIcodFondosPensiones);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BPlanillas().modificarFondosPensionesMixtas(Obe);
                    new BPlanillas().modificarPorcentajeFondoMixto(intIcodFondosPensiones);
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
                    this.MiEvento(Convert.ToInt32(Obe.fdpd2_icod_fp_concepto_mixto));
                    this.Close();
                }
            }
        }
        private bool verificarNombreAlmacen(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstFondosPensionesMixtas.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstFondosPensionesMixtas.Where(x => x.fdpd2_vdescripcion_concepto_mixto.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstFondosPensionesMixtas.Where(x => x.fdpd2_vdescripcion_concepto_mixto.Trim() == strNombre.Trim() && x.fdpd2_iid_vcodigo_fp_concepto_mixto != Obe.fdpd2_iid_vcodigo_fp_concepto_mixto).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
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

        private void frmManteAlmacen_Load(object sender, EventArgs e)
        {



        }






        private void lkpEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }




    }
}