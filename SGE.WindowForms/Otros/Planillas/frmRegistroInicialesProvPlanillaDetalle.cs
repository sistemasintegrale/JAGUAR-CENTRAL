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
    public partial class frmRegistroInicialesProvPlanillaDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroInicialesProvPlanillaDetalle));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        public EInicial_Prov_planilla_Det Obe = new EInicial_Prov_planilla_Det();
        public List<EInicial_Prov_planilla_Det> lstInicialesProvPlanillaDetalle = new List<EInicial_Prov_planilla_Det>();
        public int intIcodFondosPensiones = 0;

        public frmRegistroInicialesProvPlanillaDetalle()
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
            txtMontBasico.Enabled = !Enabled;
            bteCCosto.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;


            if (Status == BSMaintenanceStatus.ModifyCurrent)
                bteCCosto.Enabled = false;
            if (Status == BSMaintenanceStatus.CreateNew)

                txtMontBasico.Enabled = !Enabled;
            bteCCosto.Enabled = !Enabled;


        }
        public void setValues()
        {
            txtCodigo.Text = Obe.ippd_iid_inicial_provision_planilla_detalle;
            bteCCosto.Tag = Obe.perc_icod_personal;
            bteCCosto.Text = Obe.strNomyApell.ToString();
            txtMontBasico.Text = Obe.ippd_ninicial.ToString();







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


                if (String.IsNullOrEmpty(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese Codigo Tabla Planilla Detalle");
                }
                if (verificarCodigoTablaPlanillaDetalle(Convert.ToInt32(bteCCosto.Tag)))
                {
                    oBase = bteCCosto;
                    throw new ArgumentException("El Personal ingresado ya existe ");
                }

                if (String.IsNullOrEmpty(txtMontBasico.Text))
                {
                    oBase = txtMontBasico;
                    throw new ArgumentException("Ingrese Monto Inicial");
                }

                Obe.ippc_icod_inicial_provision_planilla = intIcodFondosPensiones;
                Obe.ippd_iid_inicial_provision_planilla_detalle = txtCodigo.Text;
                Obe.perc_icod_personal = Convert.ToInt32(bteCCosto.Tag);
                Obe.ippd_ninicial = Convert.ToDecimal(txtMontBasico.Text);

                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;





                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.ippd_icod_inicial_provision_planilla_detalle = new BPlanillas().InsertarInicial_Prov_Planilla_Detalle(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BPlanillas().modificarInicial_Prov_Planilla_Detalle(Obe);
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
                    this.MiEvento(Convert.ToInt32(Obe.ippd_icod_inicial_provision_planilla_detalle));
                    this.Close();
                }
            }
        }
        private bool verificarCodigoTablaPlanillaDetalle(int strcodigo)
        {
            try
            {
                bool exists = false;
                if (lstInicialesProvPlanillaDetalle.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstInicialesProvPlanillaDetalle.Where(x => x.perc_icod_personal == strcodigo).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstInicialesProvPlanillaDetalle.Where(x => x.perc_icod_personal == strcodigo && x.ippd_icod_inicial_provision_planilla_detalle != Obe.ippd_icod_inicial_provision_planilla_detalle).ToList().Count > 0)
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

        private void bteCCosto_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bteCCosto_Click(object sender, EventArgs e)
        {
            using (frmListarPersonal frm = new frmListarPersonal())
            {
                //frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCCosto.Text = frm._Be.ApellNomb;
                    bteCCosto.Tag = frm._Be.perc_icod_personal;

                }
            }
        }






    }
}