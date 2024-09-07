using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class FrmManteMovimientoVarios : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteMovimientoVarios));


        public delegate void DelegadoMensaje(int cab_corrrel);
        public event DelegadoMensaje MiEvento;
        public int icod_movimiento = 0;
        public int vcocc_iid_voucher_contable;
        public bool flag_conciliacion = false;

        #region Properties

        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        List<ELibroBancosDetalle> oDetailList = new List<ELibroBancosDetalle>();
        List<ELibroBancosDetalle> oDetailListDelete = new List<ELibroBancosDetalle>();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public int Cab_icod_correlativo = 0;
        public BAdministracionSistema objBtipoDoc = new BAdministracionSistema();
        public int intIcodModulo = 0;
        #endregion


        private BTesoreria Obl;

        public int IdMotivoMovimientoBanco = 0;
        public int icodBancos, icodBancoDet, icodTipo;

        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        public FrmManteMovimientoVarios()
        {
            InitializeComponent();
        }

        private void FrmManteMovimientoVarios_Load(object sender, EventArgs e)
        {
            CargaLista();
            Carga();
        }

        private void dtmFecha_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lblMes.Text) == 0)
                return;
            if (Convert.ToDateTime(dtmFecha.EditValue).Month == Convert.ToInt32(lblMes.Text) && Convert.ToDateTime(dtmFecha.EditValue).Year == Parametros.intEjercicio)
            {
                var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtmFecha.EditValue).ToShortDateString()).ToList();
                if (Lista.Count > 0)
                {
                    Lista.ForEach(obe =>
                    {
                        txtTipoDeCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                    });
                }
                else
                    txtTipoDeCambio.Text = "0.0000";
            }
            else
            {
                XtraMessageBox.Show("La fecha seleccionada no está dentro del mes o año de ejercicio", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CheckFecha();
                dtmFecha.Focus();
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (intIcodModulo == 14)
            {
                if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                {
                    XtraMessageBox.Show("Ingrese tipo de cambio", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtmFecha.Focus();
                    return;
                }
                using (FrmManteMovVariosDetLiquidacion frm = new FrmManteMovVariosDetLiquidacion())
                {
                    frm.TipoCambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                    frm.saldo = Convert.ToDecimal(txtResta.Text);
                    frm.SetInsert();
                    frm.tip_mon = Convert.ToInt32(lkpMoneda.EditValue);
                    frm.Cab_icod_correlativo = Cab_icod_correlativo;
                    frm.dtmFecha.EditValue = dtmFecha.EditValue;
                    frm.icodTipo = icodTipo;
                    frm.oDetailList = oDetailList;
                    frm.txtConcepto.Text = txtGlosa.Text;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        oDetailList = frm.oDetailList;
                        viewDetalle.RefreshData();
                        calcular();
                        viewDetalle.MoveLast();
                    }
                }
            }
            else
            {
                if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                {
                    XtraMessageBox.Show("Ingrese tipo de cambio", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtmFecha.Focus();
                    return;
                }
                using (FrmManteMovVariosDet frm = new FrmManteMovVariosDet())
                {
                    frm.TipoCambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                    frm.saldo = Convert.ToDecimal(txtResta.Text);
                    frm.SetInsert();
                    frm.tip_mon = Convert.ToInt32(lkpMoneda.EditValue);
                    frm.Cab_icod_correlativo = Cab_icod_correlativo;
                    frm.oDetailList = oDetailList;
                    frm.txtConcepto.Text = txtGlosa.Text;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        oDetailList = frm.oDetailList;
                        viewDetalle.RefreshData();
                        calcular();
                        viewDetalle.MoveLast();
                    }
                }
            }

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (intIcodModulo == 14)
            {
                ELibroBancosDetalle Obe = (ELibroBancosDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                if (Obe != null)
                {
                    using (FrmManteMovVariosDetLiquidacion frm = new FrmManteMovVariosDetLiquidacion())
                    {
                        frm.TipoCambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                        frm.Obj = Obe;
                        frm.tip_mon = Convert.ToInt32(lkpMoneda.EditValue);
                        frm.Cab_icod_correlativo = Cab_icod_correlativo;
                        frm.oDetailList = oDetailList;
                        frm.SetModify();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            oDetailList = frm.oDetailList;
                            viewDetalle.RefreshData();
                            calcular();
                        }
                    }
                }
            }
            else
            {
                ELibroBancosDetalle Obe = (ELibroBancosDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                if (Obe != null)
                {
                    using (FrmManteMovVariosDet frm = new FrmManteMovVariosDet())
                    {
                        frm.TipoCambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                        frm.Obj = Obe;
                        frm.tip_mon = Convert.ToInt32(lkpMoneda.EditValue);
                        frm.Cab_icod_correlativo = Cab_icod_correlativo;
                        frm.oDetailList = oDetailList;
                        frm.SetModify();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            oDetailList = frm.oDetailList;
                            viewDetalle.RefreshData();
                            calcular();
                        }
                    }
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ELibroBancosDetalle Obe = (ELibroBancosDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (Obe != null)
            {
                oDetailListDelete.Add(Obe);
                oDetailList.Remove(Obe);
                viewDetalle.RefreshData();
                calcular();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            this.SetSave();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StatusControl()
        {
            if (Status == BSMaintenanceStatus.View)
            {
                txtBeneficia.Enabled = false;
                txtGlosa.Enabled = false;
                txtMonto.Enabled = false;
                txtNumeroDeDocumento.Enabled = false;
                //txtTipoDeCambio.Enabled = false;
                lkpTipoDocumento.Enabled = false;
                dtmFecha.Enabled = false;
                rdTipoMovimiento.Enabled = false;
                mnuLibroBancosDetalle.Enabled = false;
                btnGuardar.Enabled = false;
            }
            txtNumeroDeDocumento.Focus();
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

                //txtMonto.Enabled = false;
                txtNumeroDeDocumento.Enabled = false;
                //txtTipoDeCambio.Enabled = false;
                lkpTipoDocumento.Enabled = false;
                //dtmFecha.Enabled = false;
                rdTipoMovimiento.Enabled = false;
                txtTipoDeCambio.Enabled = false;
            }
            //txtTipoDeCambio.Enabled = (Convert.ToInt32(lblMes.Text) == 0 && Status != BSMaintenanceStatus.View ) ? true : false;
        }

        private void clearControl()
        {
            txtTipoDeCambio.Text = "0.0000";
            txtNumeroDeDocumento.Text = "";
            txtGlosa.Text = string.Empty;
            txtBeneficia.Text = string.Empty;
            //lkpTipoDocumento.EditValue = "";
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            lkpTipoDocumento.ItemIndex = 0;
            CheckFecha();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }
        private void CheckFecha()
        {
            if (Convert.ToInt32(lblMes.Text) == 0)
            {
                dtmFecha.EditValue = DateTime.Now;
                return;
            }
            if (Convert.ToInt32(lblMes.Text) == DateTime.Now.Month && Parametros.intEjercicio == DateTime.Now.Year)
                dtmFecha.EditValue = DateTime.Now;
            else
                dtmFecha.EditValue =
                    DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(Convert.ToInt32(lblMes.Text) - 1);
        }
        public void calcular()
        {
            txtTotal.Text = oDetailList.Sum(x => x.mnto).ToString();
            txtResta.Text = (Convert.ToDecimal(txtMonto.Text) - Convert.ToDecimal(txtTotal.Text)).ToString();
        }
        private void SetSave()
        {

            BaseEdit oBase = null;
            Boolean Flag = true;
            ELibroBancos oBe = new ELibroBancos();
            Obl = new BTesoreria();

            try
            {
                if (string.IsNullOrEmpty(txtNumeroDeDocumento.Text))
                {
                    oBase = txtNumeroDeDocumento;
                    throw new ArgumentException("Ingrese número de documento");
                }

                if (string.IsNullOrEmpty(txtBeneficia.Text))
                {
                    oBase = txtBeneficia;
                    throw new ArgumentException("Ingrese el benificiario");
                }

                //if (dtFechaDiferida.Enabled)
                //{
                //    if (dtFechaDiferida.EditValue == null)
                //    {
                //        oBase = dtFechaDiferida;
                //        throw new ArgumentException("Ingrese la fecha diferida del documento");
                //    }
                //}


                if (Convert.ToDecimal(txtMonto.EditValue) <= 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("El monto no puede ser 0 o menor que 0");
                }
                if (Convert.ToInt32(lblMes.Text) > 0)
                {
                    if (oDetailList.Count == 0)
                        //throw new ArgumentException("Debe ingresar el detalle antes continuar con la grabación");
                        if (XtraMessageBox.Show("No ha ingresado el detalle de las cuentas ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                            throw new ArgumentException(string.Empty);

                    if (Convert.ToDecimal(txtMonto.Text) != oDetailList.Sum(x => x.mnto))
                    {
                        oBase = txtMonto;
                        //throw new ArgumentException("La sumatoria del monto del detalle no es igual al monto total");
                        if (XtraMessageBox.Show("La sumatoria del monto del detalle no es igual al monto total ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                            throw new ArgumentException(string.Empty);
                    }
                }


                oBe.icod_correlativo = Cab_icod_correlativo;
                oBe.iid_anio = Parametros.intEjercicio;
                oBe.iid_mes = Convert.ToInt32(lblMes.Text);
                oBe.icod_enti_financiera_cuenta = Convert.ToInt32(lblCuenta.Text);
                oBe.ii_tipo_doc = Convert.ToInt32(lkpTipoDocumento.EditValue);
                oBe.vdescripcion_beneficiario = txtBeneficia.Text;
                oBe.iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.nmonto_tipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                oBe.nmonto_movimiento = Convert.ToDecimal(txtMonto.Text);
                oBe.nmonto_saldo_banco = 0;
                oBe.iid_situacion_movimiento_banco = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.dfecha_movimiento = Convert.ToDateTime(dtmFecha.Text);
                oBe.cflag_tipo_movimiento = rdTipoMovimiento.SelectedIndex.ToString();
                oBe.vnro_documento = txtNumeroDeDocumento.Text;
                oBe.cflag_conciliacion = flag_conciliacion;
                oBe.iusuario_crea = Valores.intUsuario;
                oBe.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.vglosa = txtGlosa.Text;
                oBe.iid_motivo_mov_banco = IdMotivoMovimientoBanco;
                oBe.mobac_flag_estado = true;
                oBe.TipoDocumento = lkpTipoDocumento.Text;

                /**/
                DateTime? dtNullVal = null;
                //oBe.mobac_sfecha_diferida = (dtFechaDiferida.Enabled) ? Convert.ToDateTime(dtFechaDiferida.EditValue) : dtNullVal;


                Enumerar();
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Cab_icod_correlativo = Obl.insertarLibroBancos(oBe, oDetailList, null, null, null, null, null);
                    ActualizarCorrelativo();
                }
                else
                {

                    oBe.iusuario_modifica = Valores.intUsuario;
                    oBe.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                    Obl.actualizarLibroBancos(oBe, oDetailList, null, null, null, null, oDetailListDelete);
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
                    this.MiEvento(Cab_icod_correlativo);
                    this.Close();
                }
            }

        }

        private void CargaLista()
        {
            var lstMoneda = new BGeneral().listarTablaRegistro(5);
            BSControls.LoaderLook(lkpMoneda, lstMoneda, "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(36), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            /**/

            /**/

            //if (Status == BSMaintenanceStatus.CreateNew)
            //{
            var lstTipoDocContabilidad = new BAdministracionSistema().listarTipoDocumentoPorModulo(intIcodModulo);
            BSControls.LoaderLook(lkpTipoDocumento, lstTipoDocContabilidad, "tdocc_vdescripcion", "tdocc_icod_tipo_doc", true);
            //}

            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();


        }
        public int id_tipo_moneda;
        private void Carga()
        {
            oDetailList = new BTesoreria().ListarEntidadFinancieraDetalle(Cab_icod_correlativo);
            //lkpMoneda.EditValue = id_tipo_moneda;
            if (id_tipo_moneda == 4)
            {
                oDetailList.ForEach(x =>
                {
                    x.mnto = x.mto_mov_dolar;
                });
            }
            grdDetalle.DataSource = oDetailList;
        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
        }
        private void Enumerar()
        {
            int cont = 0;
            oDetailList.ForEach(x =>
            {
                x.iid_correlativo = cont + 1;
                cont += 1;
                if (x.TipOper != 1)
                    x.TipOper = 2;
            });
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtMonto_EditValueChanged(object sender, EventArgs e)
        {
            calcular();
        }

        private void lkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            GetCorrelativo();
        }
        public List<ECajaCorrelativo> lst = new List<ECajaCorrelativo>();
        private void GetCorrelativo()
        {

            if (intIcodModulo == 14)
            {
                lst = new BTesoreria().listarCajasCorrelativo().Where(x => x.bcoc_icod_banco == icodBancos && x.bcod_icod_banco_cuenta == icodBancoDet && x.tdocc_icod_tipo_doc == Convert.ToInt32(lkpTipoDocumento.EditValue) && x.cc_icod_tipo == icodTipo).ToList();
                if (lst.Count > 0)
                {
                    txtNumeroDeDocumento.Text = String.Format("{0:00000000}", Convert.ToInt32(lst[0].cc_inumero_correlativo) + 1);
                }
            }
            else
            {
                string nro_correlativo = "";
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    txtNumeroDeDocumento.Text = string.Empty;
                    var lst = objBtipoDoc.GetCorrelativoDocumentoBancos(Parametros.intEjercicio, Convert.ToInt32(lblMes.Text), Convert.ToInt32(lblCuenta.Text), Convert.ToInt32(lkpTipoDocumento.EditValue));

                    if (lst != null)
                    {
                        nro_correlativo = lst;
                        int nro;
                        bool bNum = int.TryParse(nro_correlativo, out nro);
                        if (bNum)
                            txtNumeroDeDocumento.Text = (nro_correlativo == null) ? "" : String.Format("{0:000000}", Convert.ToInt32(nro_correlativo) + 1);
                    }

                    //if (lkpTipoDocumento.Text == "CHEQUE")
                    //    dtFechaDiferida.Enabled = true;
                    //else
                    //{
                    //    dtFechaDiferida.Enabled = false;
                    //    dtFechaDiferida.Text = String.Empty;
                    //    dtFechaDiferida.EditValue = null;
                    //}
                }
            }


        }
        private void ActualizarCorrelativo()
        {

            if (intIcodModulo == 14)
            {
                if (lst.Count > 0)
                {
                    lst.ForEach(x =>
                    {
                        x.cc_inumero_correlativo = Convert.ToInt32(txtNumeroDeDocumento.Text);
                        new BTesoreria().modificarCajasCorrelativo(x);
                    });
                }
            }
            else
            {
                objBtipoDoc.ActualizarCorrelativoDocumentoBancos(Parametros.intEjercicio, Convert.ToInt32(lblMes.Text), Convert.ToInt32(lblCuenta.Text), Convert.ToInt32(lkpTipoDocumento.EditValue), txtNumeroDeDocumento.Text);
            }



        }
        public void generarVoucher2()
        {
            SetSave();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}