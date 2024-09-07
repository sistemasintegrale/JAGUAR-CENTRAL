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

namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    public partial class FrmManteCajasCorrelativos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteCajaChica));

        private List<ECajaCorrelativo> mlist = new List<ECajaCorrelativo>();
        private List<ECajaCorrelativo> mlistDetalle = new List<ECajaCorrelativo>();
        private List<ECuentaContable> aux = new List<ECuentaContable>();
        private List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        private List<ETablaRegistro> ListaAnalitica = new List<ETablaRegistro>();
        private List<ETablaRegistro> auxA = new List<ETablaRegistro>();
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;


        public FrmManteCajasCorrelativos()
        {
            InitializeComponent();
        }

        public List<ECajaCorrelativo> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BTesoreria Obl;
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
            lkpCaja.Enabled = false;
            lkpTipoDoc.Enabled = false;

            BtnGuardar.Enabled = false;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpCaja.Enabled = true;
                lkpTipoDoc.Enabled = true;
                BtnGuardar.Enabled = true;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                lkpCaja.Enabled = true;
                lkpTipoDoc.Enabled = true;
                BtnGuardar.Enabled = true;
            }

        }

        private void FrmCajaChica_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpCaja, new BTesoreria().listarBancos().Where(x => x.bcoc_flag_indicador == true), "bcoc_vnombre_banco", "bcoc_icod_banco", true);
            var lstTipoDocContabilidad = new BAdministracionSistema().listarTipoDocumentoPorModulo(14);
            BSControls.LoaderLook(lkpTipoDoc, lstTipoDocContabilidad, "tdocc_vdescripcion", "tdocc_icod_tipo_doc", true);
            BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaRegistro(97), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            cargar();

        }
        private void cargar()
        {
            var lstMoneda = new BGeneral().listarTablaRegistro(5);
            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            ListaAnalitica = new BGeneral().listarTablaRegistro(24);
        }

        void form2_MiEvento()
        {
            cargar();
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
            {
                BaseEdit oBase = null;
                Boolean Flag = true;
                ECajaCorrelativo oBe = new ECajaCorrelativo();
                Obl = new BTesoreria();
                try
                {

                    //if (string.IsNullOrEmpty(txtDescripcion.Text))
                    //{
                    //    oBase = txtDescripcion;
                    //    throw new ArgumentException("Ingrese descripción");
                    //}

                    //if (bteCuenta.Tag == null)
                    //{
                    //    oBase = bteCuenta;
                    //    throw new ArgumentException("Ingrese o seleccione cuenta contable");
                    //}

                    int? ValNull;
                    ValNull = null;
                    oBe.bcoc_icod_banco = Convert.ToInt32(lkpCaja.EditValue);
                    oBe.bcod_icod_banco_cuenta = Convert.ToInt32(lkpCuenta.EditValue);
                    oBe.tdocc_icod_tipo_doc = Convert.ToInt32(lkpTipoDoc.EditValue);
                    oBe.cc_inumero_correlativo = Convert.ToInt32(txtCorrelativo.Text);
                    oBe.intUsuario = Valores.intUsuario;
                    oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                    oBe.cc_icod_tipo = Convert.ToInt32(lkpTipo.EditValue);

                    //oBe.id_correlative_caja_chica = Convert.ToInt32(txtcodigo.Text);

                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        Obl.insertarCajasCorrelativo(oBe);
                    }
                    else
                    {
                        oBe.cc_icod_caja_correlativo = Correlative;
                        Obl.modificarCajasCorrelativo(oBe);
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
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Flag = false;
                }
                finally
                {
                    if (Flag)
                    {
                        this.MiEvento();
                        this.Close();
                    }
                }
            }
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void BtnCancelar1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void lkpCaja_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpCaja.EditValue != null)
            {
                var lstCuentas = new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpCaja.EditValue));
                BSControls.LoaderLook(lkpCuenta, lstCuentas, "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);

                //if (Convert.ToInt32(lkpCaja.EditValue) == 36)//compras
                //{
                //    var lstTipoDocContabilidad = new BAdministracionSistema().listarTipoDocumentoPorModulo(6);
                //    BSControls.LoaderLook(lkpTipoDoc, lstTipoDocContabilidad, "tdocc_vdescripcion", "tdocc_icod_tipo_doc", true);
                //}
                //else if (Convert.ToInt32(lkpCaja.EditValue) == 37)//produccion
                //{
                //    var lstTipoDocContabilidad = new BAdministracionSistema().listarTipoDocumentoPorModulo(14);
                //    BSControls.LoaderLook(lkpTipoDoc, lstTipoDocContabilidad, "tdocc_vdescripcion", "tdocc_icod_tipo_doc", true);
                //}

            }
        }
    }
}