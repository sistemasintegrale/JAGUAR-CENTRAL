using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmManteSaldosIniciales : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteSaldosIniciales));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<EVoucherContableDet> lstDetalle = new List<EVoucherContableDet>();
        List<EVoucherContableDet> lstDelete = new List<EVoucherContableDet>();
        List<ESubDiario> lstSubdiarios = new List<ESubDiario>();
        public List<EVoucherContableCab> lstComprobantes = new List<EVoucherContableCab>();
        public int intOrigen = 0;
        public int intMes = -1;
        /*-------------------------------------------------------------------------------*/
        public EVoucherContableCab objCab = new EVoucherContableCab();
        public EParametroContable objParContables = new EParametroContable();
        BContabilidad objContabilidadData = new BContabilidad();

        public FrmManteSaldosIniciales()
        {
            InitializeComponent();
        }


        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
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
            bteSubDiario.Enabled = false;
            lkpMoneda.Enabled = false;
            if (Status == BSMaintenanceStatus.View)
            {
                txtNroComprobante.Enabled = false;
                mnuComprobantes.Enabled = false;
                txtGlosa.Enabled = false;
                txtTipoCambio.Enabled = false;
                BtnGuardar.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtNroComprobante.Enabled = false;
        }
        private void clearControl()
        {
            txtGlosa.Text = "";
            txtMovimientos.Text = "";
        }

        private void loadlista()
        {
            grdDetalle.DataSource = lstDetalle.Where(oBe => oBe.ctacc_iid_cuenta_contable_ref == null).ToList();
            txtMovimientos.Text = lstDetalle.Count.ToString();
        }

        private void cargar()
        {
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpEstado, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionVoucher), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);

        }

        public void setValues()
        {
            if (objCab != null)
            {
                dteFecha.EditValue = Convert.ToDateTime(objCab.vcocc_fecha_vcontable);
                bteSubDiario.Text = String.Format("{0:00}", objCab.subdi_icod_subdiario);
                bteSubDiario.Tag = objCab.subdi_icod_subdiario;
                txtNroComprobante.Text = objCab.vcocc_numero_vcontable;
                txtGlosa.Text = objCab.vcocc_glosa;
                lkpMoneda.EditValue = objCab.tablc_iid_moneda;
                lkpEstado.EditValue = objCab.tarec_icorrelativo_situacion_vcontable;
                txtTipoCambio.Text = objCab.vcocc_tipo_cambio.ToString();
            }
        }

        private void FrmManteSaldosIniciales_Load(object sender, EventArgs e)
        {
            lstDetalle = objContabilidadData.listarVoucherContableDet(objCab.vcocc_icod_vcontable);
            objParContables = objContabilidadData.listarParametroContable()[0];

            /*-----------------------------------------------------------------*/
            bteSubDiario.Tag = objParContables.parac_id_sd_apert;
            bteSubDiario.Text = String.Format("{0:00}", objParContables.parac_id_sd_apert);
            cargar();
            loadlista();
            viewDetalle.RefreshData();
            updateSituacion();

        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
            lkpMoneda.ItemIndex = 0;
            dteFecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1);
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            txtMovimientos.Text = lstDetalle.Count.ToString();
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (string.IsNullOrEmpty(bteSubDiario.Text))
                {
                    oBase = bteSubDiario;
                    throw new ArgumentException("Sub - Diario de Apertura no reconocido\nNota: Consultar Parámetros Contables");
                }
                if (string.IsNullOrEmpty(txtNroComprobante.Text))
                {
                    oBase = txtNroComprobante;
                    throw new ArgumentException("Ingrese Número de Comprobante");
                }

                if (string.IsNullOrEmpty(txtGlosa.Text))
                {
                    oBase = txtGlosa;
                    throw new ArgumentException("Ingrese glosa o descricpción del comprobante");
                }

                objCab.anioc_iid_anio = Parametros.intEjercicio;
                objCab.mesec_iid_mes = intMes;
                objCab.subdi_icod_subdiario = Convert.ToInt32(bteSubDiario.Tag);
                objCab.vcocc_fecha_vcontable = Convert.ToDateTime(dteFecha.EditValue);
                objCab.vcocc_glosa = txtGlosa.Text;
                objCab.vcocc_observacion = txtGlosa.Text;
                objCab.vcocc_numero_vcontable = txtNroComprobante.Text.Trim();
                objCab.tarec_icorrelativo_situacion_vcontable = Convert.ToInt32(lkpEstado.EditValue);
                objCab.tarec_icorrelativo_origen_vcontable = intOrigen;
                objCab.tablc_iid_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                objCab.vcocc_nmto_tot_debe_sol = lstDetalle.Sum(x => Convert.ToDecimal(x.vcocd_nmto_tot_debe_sol));
                objCab.vcocc_nmto_tot_haber_sol = lstDetalle.Sum(x => Convert.ToDecimal(x.vcocd_nmto_tot_haber_sol));
                objCab.vcocc_nmto_tot_debe_dol = lstDetalle.Sum(x => Convert.ToDecimal(x.vcocd_nmto_tot_debe_dol));
                objCab.vcocc_nmto_tot_haber_dol = lstDetalle.Sum(x => Convert.ToDecimal(x.vcocd_nmto_tot_haber_dol));
                objCab.vcocc_flag_estado = true;
                objCab.vcocc_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                objCab.intUsuario = Modules.Valores.intUsuario;
                objCab.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    objCab.vcocc_icod_vcontable = objContabilidadData.insertarVoucherContableCab(objCab, lstDetalle);
                }
                else
                {
                    objContabilidadData.modificarVoucherContableCab(objCab, lstDetalle, lstDelete);
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
                    this.MiEvento(objCab.vcocc_icod_vcontable);
                    this.Close();
                }
            }
        }
        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private bool VerifyFields()
        {
            BaseEdit oBase = null;
            bool flag = true;
            try
            {
                if (string.IsNullOrEmpty(bteSubDiario.Text))
                {
                    oBase = bteSubDiario;
                    throw new ArgumentException("Ingrese Sub - Diario");
                }
                if (bteSubDiario.Tag == null)
                {
                    oBase = bteSubDiario;
                    throw new ArgumentException("Ingrese un Sub - Diario válido");
                }
                if (string.IsNullOrEmpty(txtGlosa.Text))
                {
                    oBase = txtGlosa;
                    throw new ArgumentException("Ingrese la descripción");
                }
                if (Convert.ToDecimal(txtTipoCambio.Text) == 0)
                {
                    oBase = txtTipoCambio;
                    throw new ArgumentException("Ingrese un tipo de cambio referencial");
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
                flag = false;
            }
            return flag;

        }

        private void loadGrid()
        {
            grdDetalle.DataSource = lstDetalle.Where(oBe => oBe.ctacc_iid_cuenta_contable_ref == null).ToList();
            viewDetalle.RefreshData();
            txtMovimientos.Text = lstDetalle.Count.ToString();
            updateSituacion();
            setTotalCab();
        }
        private void updateSituacion()
        {
            if (lstDetalle.Count > 0)
            {
                if (lstDetalle.Sum(x => x.vcocd_nmto_tot_debe_sol) == lstDetalle.Sum(x => x.vcocd_nmto_tot_haber_sol) &&
                    lstDetalle.Sum(x => x.vcocd_nmto_tot_debe_dol) == lstDetalle.Sum(x => x.vcocd_nmto_tot_haber_dol))
                    lkpEstado.EditValue = Parametros.intSitVcoCuadrado;
                else
                    lkpEstado.EditValue = Parametros.intSitVcoNoCuadrado;
            }
            else
                lkpEstado.EditValue = Parametros.intSitVcoSinDetalle;
            txtMovimientos.Text = lstDetalle.Count.ToString();

        }
        private void setTotalCab()
        {
            txtMtoDebeSol.Text = gridColumn4.SummaryText;
            txtMtoDebeDol.Text = gridColumn6.SummaryText;
            txtMtoHaberSol.Text = gridColumn5.SummaryText;
            txtMtoHaberDol.Text = gridColumn7.SummaryText;
        }
        private void nuevo()
        {
            if (VerifyFields())
            {
                using (frmManteComprobanteDetalle frm = new frmManteComprobanteDetalle())
                {
                    frm.flagSaldoInicial = true;
                    frm.btnModificar.Enabled = false;
                    frm.lstDetalle = lstDetalle;
                    frm.intTipoMoneda = Convert.ToInt32(lkpMoneda.EditValue);
                    frm.txtglosa.Text = txtGlosa.Text;
                    frm.txtTC.Text = txtTipoCambio.Text;
                    frm.SetInsert();

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        loadGrid();
                        viewDetalle.MoveLast();
                        viewDetalle.Focus();
                    }
                }
            }
        }
        private void modificar()
        {
            try
            {
                EVoucherContableDet Obe = (EVoucherContableDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                if (Obe == null)
                    return;
                if (Obe.ctacc_iid_cuenta_contable_ref != null)
                    throw new ArgumentException("No de puede modificar manualmente la Cuenta Automática, debe modifcar la cuenta de origen");
                else
                {
                    int index = viewDetalle.FocusedRowHandle;
                    using (frmManteComprobanteDetalle frm = new frmManteComprobanteDetalle())
                    {
                        frm.flagSaldoInicial = true;
                        frm.Obe = Obe;
                        frm.SetModify();
                        frm.btnAgregar.Enabled = false;
                        frm.lstDetalle = lstDetalle;
                        frm.intTipoMoneda = Convert.ToInt32(lkpMoneda.EditValue);
                        frm.dblTipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            loadGrid();
                            viewDetalle.FocusedRowHandle = index;
                            viewDetalle.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void renumerar()
        {
            for (int i = 0; i < lstDetalle.Count; i++)
            {
                lstDetalle[i].vcocd_nro_item_det = i + 1;
                if (lstDetalle[i].intTipoOperacion == 0)
                    lstDetalle[i].intTipoOperacion = Parametros.intOperacionModificar;
            }
        }
        private void eliminar()
        {
            try
            {
                EVoucherContableDet Obe = (EVoucherContableDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                int index2 = viewDetalle.FocusedRowHandle;
                lstDelete.Add(Obe);
                lstDetalle.Remove(Obe);
                loadGrid();
                setTotalCab();
                updateSituacion();
                renumerar();
                if (lstDetalle.Count >= index2 + 1)
                    viewDetalle.FocusedRowHandle = index2;
                else
                    viewDetalle.FocusedRowHandle = index2 - 1;
                viewDetalle.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstDetalle.Count > 0)
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    eliminar();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtNroComprobante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= (char)48 && e.KeyChar <= (char)57 || e.KeyChar <= (char)8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void lkpEstado_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpEstado.EditValue) == Parametros.intSitVcoNoCuadrado)
                lkpEstado.Properties.AppearanceDisabled.ForeColor = Color.Red;
            else if (Convert.ToInt32(lkpEstado.EditValue) == Parametros.intSitVcoSinDetalle)
                lkpEstado.Properties.AppearanceDisabled.ForeColor = Color.Red;
            else if (Convert.ToInt32(lkpEstado.EditValue) == Parametros.intSitVcoCuadrado)
                lkpEstado.Properties.AppearanceDisabled.ForeColor = Color.Black;
        }
    }
}