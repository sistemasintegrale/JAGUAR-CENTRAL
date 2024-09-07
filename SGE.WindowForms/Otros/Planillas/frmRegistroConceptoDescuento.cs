using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmRegistroConceptoDescuento : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroConceptosIngresos));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EConceptoDescuento Obe = new EConceptoDescuento();
        public List<EConceptoDescuento> lstConceptoDescuento = new List<EConceptoDescuento>();
        public List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();

        public List<EParametroContable> mlista = (new BContabilidad()).listarParametroContable();

        public frmRegistroConceptoDescuento()
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
            txtCodigo.Enabled = false;
            lkpPlanilla.Enabled = !Enabled;
            lkpSituacion.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            txtPorcentaje.Enabled = !Enabled;
            lkpTipoCalculo.Enabled = !Enabled;
            lkpAccion.Enabled = !Enabled;
            txtMonto.Enabled = !Enabled;
            bteCuenta.Enabled = !Enabled;
            bteCuenta2.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;




            if (Status == BSMaintenanceStatus.CreateNew)
            {
                lkpSituacion.Enabled = false;

            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtCodigo.Enabled = false;


            }
        }
        public void setValues()
        {

            txtCodigo.Text = String.Format("{0:0000}", Obe.cdpc_iid_concepto_ing_plan);
            //lkpPlanilla.EditValue = Obe.tbpc_icod_tipo_planilla;//--
            //lkpSituacion.EditValue = Obe.tbpc_icod_situacion_concepto_plan;//--            
            txtDescripcion.Text = Obe.cdpc_vdescripcion;
            txtPorcentaje.Text = Obe.cdpc_nmonto_porcentaje_planilla.ToString();
            txtMonto.Text = Obe.cdpc_nmonto_calculo_planilla.ToString();
            //lkpTipoCalculo.EditValue = Obe.tbpc_icod_tipo_calculo_planilla;//--
            //lkpAccion.EditValue = Obe.tbpc_icod_tipo_accion_planilla;//--

            //lkpTipo.EditValue = Obe.tbpc_icod_tipo_concepto_planilla;//--
            bteCuenta.Tag = Obe.ctcc_icod_cuenta_contable_debe;
            bteCuenta.Text = Obe.ctacc_numero_cuenta_contable;
            bteCuenta2.Tag = Obe.ctcc_icod_cuenta_contable_haber;
            bteCuenta2.Text = Obe.ctacc_numero_cuenta_contable_haber;

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
                if (Convert.ToInt32(txtCodigo.Text) == 0)
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código");
                }
                if (verificarCodigo(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los Conceptos por Descuento");
                }
                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese la Descripción");
                }
                /*----------------------*/

                //if (verificarNombre(txtDescripcion.Text))
                //{
                //    oBase = txtDescripcion;
                //    throw new ArgumentException("La Descripción ya existe en los Conceptos por Descuento");
                //}

                /*----------------------*/

                //if ((bteCuenta.Text != "" && bteCuenta2.Text == "") || (bteCuenta.Text == "" && bteCuenta2.Text != ""))
                //{
                //     oBase = bteCuenta2; 
                //    throw new ArgumentException("Ingrese las cuentas DEBE/HABER o Ninguna");

                //}


                if (Convert.ToDecimal(txtPorcentaje.Text) >= 100)
                {
                    oBase = txtPorcentaje;
                    throw new ArgumentException("El Porcentaje no debe ser MAYOR al 100%");
                }



                /*----------------------*/

                Obe.cdpc_iid_concepto_ing_plan = txtCodigo.Text;
                Obe.tbpc_icod_tipo_planilla = Convert.ToInt32(lkpPlanilla.EditValue);
                Obe.tbpc_icod_situacion_concepto_plan = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.cdpc_vdescripcion = txtDescripcion.Text;
                Obe.tbpc_icod_tipo_accion_planilla = Convert.ToInt32(lkpAccion.EditValue);
                Obe.cdpc_nmonto_calculo_planilla = Convert.ToDecimal(txtMonto.Text);
                Obe.cdpc_nmonto_porcentaje_planilla = Convert.ToDecimal(txtPorcentaje.Text);
                Obe.tbpc_icod_tipo_calculo_planilla = Convert.ToInt32(lkpTipoCalculo.EditValue);
                if (bteCuenta.Text == null || bteCuenta.Text == "")
                { Obe.ctcc_icod_cuenta_contable_debe = 0; }
                else
                {
                    Obe.ctcc_icod_cuenta_contable_debe = Convert.ToInt32(bteCuenta.Tag);
                    //Obe.ctacc_numero_cuenta_contable = bteCuenta.Text;
                }

                if (bteCuenta2.Text == null || bteCuenta2.Text == "")
                { Obe.ctcc_icod_cuenta_contable_haber = 0; }
                else
                {
                    Obe.ctcc_icod_cuenta_contable_haber = Convert.ToInt32(bteCuenta2.Tag);
                    //Obe.ctacc_numero_cuenta_contable_haber = bteCuenta2.Text;
                }
                Obe.ctacc_numero_cuenta_contable = bteCuenta.Text;
                Obe.ctacc_numero_cuenta_contable_haber = bteCuenta2.Text;
                Obe.cipc_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.cdpc_icod_concepto_ingreso_plan = new BPlanillas().insertarConceptoDescuento(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BPlanillas().modificarConceptoDescuento(Obe);
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
                    this.MiEvento(Obe.cdpc_icod_concepto_ingreso_plan);
                    this.Close();
                }
            }
        }

        private bool verificarNombre(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstConceptoDescuento.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstConceptoDescuento.Where(x => x.cdpc_vdescripcion.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstConceptoDescuento.Where(x => x.cdpc_vdescripcion.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim() && x.cdpc_icod_concepto_ingreso_plan != Obe.cdpc_icod_concepto_ingreso_plan).ToList().Count > 0)
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
        private bool verificarCodigo(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstConceptoDescuento.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstConceptoDescuento.Where(x => x.cdpc_iid_concepto_ing_plan == strCodigo).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstConceptoDescuento.Where(x => x.cdpc_iid_concepto_ing_plan == strCodigo && x.cdpc_icod_concepto_ingreso_plan != Obe.cdpc_icod_concepto_ingreso_plan).ToList().Count > 0)
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

        private void frmMantePersonal_Load(object sender, EventArgs e)
        {
            txtMonto.Enabled = true;
            txtPorcentaje.Enabled = false;
            lkpTipoCalculo.Enabled = false;


            BSControls.LoaderLook(lkpTipoCalculo, new BPlanillas().listarComboTablaPlanillaDetalle(6), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpPlanilla, new BPlanillas().listarComboTablaPlanillaDetalle(1), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpSituacion, new BPlanillas().listarComboTablaPlanillaDetalle(2), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpAccion, new BPlanillas().listarComboTablaPlanillaDetalle(4), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpPlanilla.EditValue = Obe.tbpc_icod_tipo_planilla;//--
                lkpSituacion.EditValue = Obe.tbpc_icod_situacion_concepto_plan;//--   
                lkpTipoCalculo.EditValue = Obe.tbpc_icod_tipo_calculo_planilla;//--
                lkpAccion.EditValue = Obe.tbpc_icod_tipo_accion_planilla;//--

            }

            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();

            mlista.ForEach(obe =>
            {
                this.bteCuenta.Properties.Mask.BeepOnError = true;
                this.bteCuenta.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCuenta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCuenta.Properties.Mask.ShowPlaceHolders = false;

                this.bteCuenta2.Properties.Mask.BeepOnError = true;
                this.bteCuenta2.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCuenta2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCuenta2.Properties.Mask.ShowPlaceHolders = false;
            });

        }
        public void LimpiarListas()
        {


            lkpPlanilla.EditValue = null;//--
            lkpPlanilla.Text = null;
            lkpSituacion.EditValue = null;//--  
            lkpSituacion.Text = null;
            lkpTipoCalculo.EditValue = null;//--
            lkpTipoCalculo.Text = null;
            lkpAccion.EditValue = null;//--  
            lkpAccion.Text = null;

        }
        private void lkpArea_EditValueChanged(object sender, EventArgs e)
        {


            if (Status == BSMaintenanceStatus.CreateNew)
            {

                lkpSituacion.Enabled = false;

                if (lkpAccion.Text == "CALCULO")
                {
                    txtMonto.Enabled = false;
                    txtPorcentaje.Enabled = false;
                    lkpTipoCalculo.Enabled = true;
                    BSControls.LoaderLook(lkpTipoCalculo, new BPlanillas().listarComboTablaPlanillaDetalle(6), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
                }
                else if (lkpAccion.Text == "MONTO")
                {
                    lkpTipoCalculo.EditValue = null;
                    lkpTipoCalculo.Enabled = false;
                    txtMonto.Enabled = true;
                    txtPorcentaje.Enabled = false;
                }
                else if (lkpAccion.Text == "PORCENTAJE")
                {

                    lkpTipoCalculo.EditValue = null;
                    lkpTipoCalculo.Enabled = false;
                    txtMonto.Enabled = false;
                    txtPorcentaje.Enabled = true;
                }

            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {


                txtCodigo.Enabled = false;
                if (lkpAccion.Text == "CALCULO")
                {
                    txtMonto.Enabled = false;
                    txtPorcentaje.Enabled = false;
                    lkpTipoCalculo.Enabled = true;
                    BSControls.LoaderLook(lkpTipoCalculo, new BPlanillas().listarComboTablaPlanillaDetalle(6), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
                }
                else if (lkpAccion.Text == "MONTO")
                {
                    lkpTipoCalculo.EditValue = null;
                    lkpTipoCalculo.Enabled = false;
                    txtMonto.Enabled = true;
                    txtPorcentaje.Enabled = false;
                }
                else if (lkpAccion.Text == "PORCENTAJE")
                {

                    lkpTipoCalculo.EditValue = null;
                    lkpTipoCalculo.Enabled = false;
                    txtMonto.Enabled = false;
                    txtPorcentaje.Enabled = true;
                }

            }
        }


        private void labelControl13_Click(object sender, EventArgs e)
        {

        }

        private void bteCuenta_EditValueChanged(object sender, EventArgs e)
        {
            List<ECuentaContable> aux = new List<ECuentaContable>();
            if (bteCuenta.Text == "")
            {
                txtCuentaDes.Text = null;
                return;
            }

            aux = mlistCuenta.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuenta.Text.Replace(".", ""))).ToList();


            if (aux.Count == 1)
            {
                bteCuenta.Tag = aux[0].ctacc_icod_cuenta_contable;
                txtCuentaDes.Text = aux[0].ctacc_nombre_descripcion;
            }
        }

        private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCuenta();
        }
        private void ListarCuenta()
        {

            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCuenta.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;

                }
            }
        }

        private void bteCuenta2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCuenta2.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta2.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes2.Text = frm._Be.ctacc_nombre_descripcion;
                }
            }
        }

        private void bteCuenta2_EditValueChanged(object sender, EventArgs e)
        {
            if (bteCuenta2.Text == "")
            {
                txtCuentaDes2.Text = null;
                return;
            }
            List<ECuentaContable> aux = new List<ECuentaContable>();
            aux = mlistCuenta.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuenta2.Text.Replace(".", ""))).ToList();


            if (aux.Count == 1)
            {
                bteCuenta2.Tag = aux[0].ctacc_icod_cuenta_contable;
                txtCuentaDes2.Text = aux[0].ctacc_nombre_descripcion;
            }

        }
    }
}