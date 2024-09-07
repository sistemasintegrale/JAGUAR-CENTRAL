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
    public partial class frmRegistroFondosPensionesConceptos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroFondosPensionesConceptos));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        public EFondosPensionesConceptos Obe = new EFondosPensionesConceptos();
        public List<EFondosPensionesConceptos> lstFondosPensionesConceptos = new List<EFondosPensionesConceptos>();
        public int intIcodFondosPensiones = 0;

        public frmRegistroFondosPensionesConceptos()
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
            txtCodigo.Text = String.Format("{0:00}", Obe.fdpd_iid_vcodigo_fondo_concepto);
            txtNombre.Text = Obe.fdpd_vdescripcion_concepto;
            txtPorcentaje.Text = Obe.fdpd_nporcentaje_concepto.ToString();
            txtMontoTope.Text = Obe.fdpd_ntope_concpeto.ToString();

            intIcodFondosPensiones = Convert.ToInt16(Obe.fdpc_icod_fondo_pension);





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
                    throw new ArgumentException("Ingrese Descripción Conceptos Comisión Fija");
                }
                if (verificarNombreAlmacen(txtNombre.Text))
                {
                    oBase = txtNombre;
                    throw new ArgumentException("La Descripción ingresado ya existe en Conceptos Comisión Fija");
                }
                if (Convert.ToDouble(txtPorcentaje.Text) == 0.00)
                {
                    oBase = txtPorcentaje;
                    throw new ArgumentException("Ingrese Porcentaje de Conceptos Comisión Fija");
                }
                if (String.IsNullOrEmpty(txtMontoTope.Text))
                {
                    oBase = txtMontoTope;
                    throw new ArgumentException("Ingrese Monto Tope de Conceptos Comisión Fija");
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


                Obe.fdpd_iid_vcodigo_fondo_concepto = txtCodigo.Text;
                Obe.fdpd_vdescripcion_concepto = txtNombre.Text;
                Obe.fdpd_nporcentaje_concepto = Convert.ToDecimal(txtPorcentaje.Text);
                Obe.fdpd_ntope_concpeto = Convert.ToDecimal(txtMontoTope.Text);
                Obe.fdpd_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.fdpc_icod_fondo_pension = intIcodFondosPensiones;




                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.fdpd_icod_fondo_pension_concepto = new BPlanillas().insertarFondosPensionesConceptos(Obe);
                    new BPlanillas().modificarPorcentajeFondoFijo(intIcodFondosPensiones);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BPlanillas().modificarFondosPensionesConceptos(Obe);
                    new BPlanillas().modificarPorcentajeFondoFijo(intIcodFondosPensiones);
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
                    this.MiEvento(Convert.ToInt32(Obe.fdpd_icod_fondo_pension_concepto));
                    this.Close();
                }
            }
        }
        private bool verificarNombreAlmacen(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstFondosPensionesConceptos.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstFondosPensionesConceptos.Where(x => x.fdpd_vdescripcion_concepto.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstFondosPensionesConceptos.Where(x => x.fdpd_vdescripcion_concepto.Trim() == strNombre.Trim() && x.fdpd_iid_vcodigo_fondo_concepto != Obe.fdpd_iid_vcodigo_fondo_concepto).ToList().Count > 0)
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