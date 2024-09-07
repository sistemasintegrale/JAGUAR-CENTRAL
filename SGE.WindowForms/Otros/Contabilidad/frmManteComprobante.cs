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
    public partial class frmManteComprobante : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteComprobante));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /*----------------------------------------------------------------------------------------------*/
        BContabilidad objContabilidadData = new BContabilidad();
        public List<EVoucherContableCab> lstComprobantes = new List<EVoucherContableCab>();
        private List<EVoucherContableDet> lstDetalle = new List<EVoucherContableDet>();
        private List<EVoucherContableDet> lstDelete = new List<EVoucherContableDet>();
        private List<ESubDiario> lstSubdiario = new List<ESubDiario>();
        private List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        private List<ECuentaContable> lstCuentaContable = new List<ECuentaContable>();
        /*----------------------------------------------------------------------------------------------*/
        public EVoucherContableCab objCab = new EVoucherContableCab();
        public int intMes = 0;
        int intTipoMoneda = 0;
        public int intOrigen = 0;


        public frmManteComprobante()
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
            txtDescripcion.Enabled = false;
            lkpMoneda.Enabled = false;
            dteFecha.Enabled = false;
            mnuDetalle.Enabled = false;
            btnGuardar.Enabled = false;
            txtTipoCambio.Enabled = false;
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                bteSubDiario.Enabled = true;
                txtDescripcion.Enabled = true;
                lkpMoneda.Enabled = true;
                dteFecha.Enabled = true;
                mnuDetalle.Enabled = true;
                btnGuardar.Enabled = true;
                txtTipoCambio.Enabled = true;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtDescripcion.Enabled = true;
                mnuDetalle.Enabled = true;
                btnGuardar.Enabled = true;
                txtTipoCambio.Enabled = false;
            }
            bteSubDiario.Focus();
        }



        private void cargar()
        {
            lstDetalle = objContabilidadData.listarVoucherContableDet(objCab.vcocc_icod_vcontable);
            lstCuentaContable = objContabilidadData.listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            lstSubdiario = objContabilidadData.listarSubDiario();
            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpEstado, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionVoucher), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            updateSituacion();
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

        public void setValues()
        {
            if (objCab != null)
            {
                dteFecha.Text = Convert.ToDateTime(objCab.vcocc_fecha_vcontable).ToShortDateString();
                bteSubDiario.Text = String.Format("{0:00}", objCab.subdi_icod_subdiario);
                bteSubDiario.Tag = objCab.subdi_icod_subdiario;
                txtNroVoucher.Text = objCab.vcocc_numero_vcontable;
                txtDescripcion.Text = objCab.vcocc_glosa;
                intTipoMoneda = Convert.ToInt32(objCab.tablc_iid_moneda);
                lkpMoneda.EditValue = objCab.tablc_iid_moneda;
                lkpEstado.EditValue = objCab.tarec_icorrelativo_situacion_vcontable;
                txtTipoCambio.Text = objCab.vcocc_tipo_cambio.ToString();
            }
        }

        public void getTipoCambio()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                var Lista = lstTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dteFecha.EditValue).ToShortDateString()).ToList();
                Lista.ForEach(obe =>
                {
                    txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                });
                if (lstDetalle.Count > 0)
                    recalcular();
            }
        }

        private void setFecha()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                if (Parametros.intEjercicio == DateTime.Now.Year && intMes == DateTime.Now.Month)
                    dteFecha.EditValue = DateTime.Now;
                else
                    dteFecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(intMes - 1);
            }
        }


        private void FrmManteDetalleComprobante_Load(object sender, EventArgs e)
        {
            cargar();
            setFecha();
            getTipoCambio();
            loadGrid();
            updateSituacion();
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

                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese la descripción");
                }
                //if (Convert.ToDateTime(dteFecha.Text).Month != intMes && Convert.ToDateTime(dteFecha.Text).Year > Parametros.intEjercicio)
                //{
                //    throw new ArgumentException("El Mes o el año de la Fecha es Diferente a Ejercicio o Periodo");
                //}
                if (Convert.ToDateTime(dteFecha.Text).Year > Parametros.intEjercicio)
                {
                    throw new ArgumentException("El año de la Fecha es Diferente");
                }
                if (Convert.ToDateTime(dteFecha.Text).Month != intMes)
                {
                    throw new ArgumentException("El Mes de la Fecha es Diferente");
                }
                if (lstDetalle.Count == 0)
                {
                    if (XtraMessageBox.Show("¿Está seguro que desea grabar el Comprobante SIN DETALLE?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Flag = true;
                    else
                        Flag = false;
                }
                if (Flag)
                {
                    objCab.anioc_iid_anio = Parametros.intEjercicio;
                    objCab.mesec_iid_mes = intMes;
                    objCab.subdi_icod_subdiario = Convert.ToInt32(bteSubDiario.Tag);
                    objCab.vcocc_fecha_vcontable = Convert.ToDateTime(dteFecha.Text);
                    objCab.vcocc_glosa = txtDescripcion.Text;
                    objCab.vcocc_observacion = txtDescripcion.Text;
                    objCab.vcocc_numero_vcontable = txtNroVoucher.Text.Trim();
                    objCab.tarec_icorrelativo_situacion_vcontable = Convert.ToInt32(lkpEstado.EditValue);
                    objCab.tarec_icorrelativo_origen_vcontable = intOrigen;
                    objCab.tablc_iid_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                    objCab.vcocc_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                    objCab.vcocc_nmto_tot_debe_sol = lstDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => Convert.ToDecimal(x.vcocd_nmto_tot_debe_sol));
                    objCab.vcocc_nmto_tot_haber_sol = lstDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => Convert.ToDecimal(x.vcocd_nmto_tot_haber_sol));
                    objCab.vcocc_nmto_tot_debe_dol = lstDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => Convert.ToDecimal(x.vcocd_nmto_tot_debe_dol));
                    objCab.vcocc_nmto_tot_haber_dol = lstDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => Convert.ToDecimal(x.vcocd_nmto_tot_haber_dol));
                    objCab.vcocc_flag_estado = true;
                    objCab.intUsuario = Valores.intUsuario;
                    objCab.strPc = WindowsIdentity.GetCurrent().Name;

                    if (Status == BSMaintenanceStatus.CreateNew)
                        objCab.vcocc_icod_vcontable = objContabilidadData.insertarVoucherContableCab(objCab, lstDetalle);
                    else
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

        private void DtmFecha_EditValueChanged(object sender, EventArgs e)
        {
            getTipoCambio();
        }
        private void recalcular()
        {
            loadGrid();
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


        private void setTotalCab()
        {
            txtMtoDebeSol.Text = gridColumn4.SummaryText;
            txtMtoDebeDol.Text = gridColumn6.SummaryText;
            txtMtoHaberSol.Text = gridColumn5.SummaryText;
            txtMtoHaberDol.Text = gridColumn7.SummaryText;
        }


        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
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
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese la descripción");
                }
                if (Convert.ToDecimal(txtTipoCambio.Text) == 0)
                {
                    oBase = dteFecha;
                    throw new ArgumentException(String.Format("Registre el Tipo de Cambio para la fecha {0} o ingrese el Tipo de Cambio", dteFecha.Text));
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
        private void nuevo()
        {
            if (VerifyFields())
            {
                using (frmManteComprobanteDetalle frm = new frmManteComprobanteDetalle())
                {
                    frm.SetInsert();
                    frm.btnModificar.Enabled = false;
                    frm.lstDetalle = lstDetalle;
                    frm.intTipoMoneda = Convert.ToInt32(lkpMoneda.EditValue);
                    frm.dblTipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
                    frm.txtglosa.Text = txtDescripcion.Text;
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
        private void eliminar()
        {
            try
            {
                /*--------------------------------------------------------------------------------------------*/
                bool flagCta = false;
                int index = 0;
                /*--------------------------------------------------------------------------------------------*/
                EVoucherContableDet Obe = (EVoucherContableDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                int index2 = viewDetalle.FocusedRowHandle;
                /*--------------------------------------------------------------------------------------------*/
                if (Obe.ctacc_iid_cuenta_contable_ref != null)
                    throw new Exception("No se puede eliminar Cuenta Automática\nNota : Debe eliminar la cuenta principal");
                else
                {

                    var Lista = lstCuentaContable.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(Obe.ctacc_icod_cuenta_contable)).ToList();
                    if (Convert.ToInt32(Lista[0].ctacc_icod_cuenta_debe_auto) > 0)
                        flagCta = true;
                    if (flagCta)
                    {
                        index = lstDetalle.FindIndex(x => x.vcocd_nro_item_det == Obe.vcocd_nro_item_det);
                        /*--------------------------------------------------------------------------------------------*/
                        lstDelete.Add(lstDetalle[index]);
                        lstDelete.Add(lstDetalle[index + 1]);
                        lstDelete.Add(lstDetalle[index + 2]);
                        /*--------------------------------------------------------------------------------------------*/
                        lstDetalle.Remove(lstDetalle[index]);
                        lstDetalle.Remove(lstDetalle[index + 1]);
                        lstDetalle.Remove(lstDetalle[index + 2]);
                    }
                    else
                    {
                        lstDelete.Add(Obe);
                        lstDetalle.Remove(Obe);
                    }
                }
                loadGrid();
                setTotalCab();
                updateSituacion();
                //renumerar();
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
        private void bteSubDiario_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarSubDiarios();
        }
        private void ListarSubDiarios()
        {
            using (frmListarSubDiario frm = new frmListarSubDiario())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteSubDiario.Tag = frm._Be.subdi_icod_subdiario;
                    bteSubDiario.Text = frm._Be.subdi_icod_subdiario.ToString();
                    GetVoucherNumber();
                }
            }
        }
        private void bteSubDiario_KeyUp(object sender, KeyEventArgs e)
        {
            List<ESubDiario> lstSDaux = new List<ESubDiario>();

            lstSDaux = lstSubdiario.Where(x => x.subdi_icod_subdiario.ToString() == Convert.ToInt32(bteSubDiario.Text).ToString()).ToList();

            if (lstSDaux.Count == 1)
            {
                bteSubDiario.Tag = lstSDaux[0].subdi_icod_subdiario;
                GetVoucherNumber();

            }
            else
            {
                txtNroVoucher.Text = string.Empty;
                bteSubDiario.Tag = null;
            }
        }

        private void GetVoucherNumber()
        {
            string numcomp = "0";
            if (lstComprobantes.Count > 0)
            {
                var lista = lstComprobantes.Where(x => x.anioc_iid_anio == Parametros.intEjercicio
                 && x.mesec_iid_mes == intMes && x.subdi_icod_subdiario == Convert.ToInt32(bteSubDiario.Tag)).ToList();

                int auxcomp;
                auxcomp = Convert.ToInt32(lista.Max(x => (x.vcocc_numero_vcontable)));

                numcomp = String.Format("{0:00000}", (auxcomp + 1));
                txtNroVoucher.Text = numcomp;
            }
            else
            {
                numcomp = String.Format("{0:00000}", 1);
                txtNroVoucher.Text = numcomp;
            }
        }

        private void bteSubDiario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= (char)48 && e.KeyChar <= (char)57 || e.KeyChar <= (char)8))
                e.Handled = false;
            else
                e.Handled = true;
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
            eliminar();
        }

        private void bteSubDiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                ListarSubDiarios();
            }
        }

        private void lkpMoneda_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                if (lstDetalle.Count > 0)
                {
                    lkpMoneda.EditValue = lstDetalle[0].tablc_iid_moneda;
                    XtraMessageBox.Show("El tipo de moneda no puede ser cambiado, porque ya existen registros en el detalle del comprobante", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
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

