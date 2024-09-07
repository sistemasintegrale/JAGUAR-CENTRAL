using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class FrmMantePagoAdelantoNotaCreditoCliente : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantePagoAdelantoNotaCreditoCliente));

        List<ELibroBancosDetalle> mListaDetalle = new List<ELibroBancosDetalle>();
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        public BAdministracionSistema objBtipoDoc = new BAdministracionSistema();
        public delegate void DelegadoMensaje(int cab_correl);
        public event DelegadoMensaje MiEvento;
        List<ELibroBancosDetalle> mlistDelete = new List<ELibroBancosDetalle>();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BTesoreria Obl;
        public int IdCodCorrelativo = 0;
        public int Correlative = 0;
        public int IdMotivoMovimientoBanco = 0;
        public int intIdAnaliticaCliente = 0;
        string iid_relacion;
        int? icod_tipo_relacion;
        public bool flag_conciliacion = false;
        public int vcocc_iid_voucher_contable;
        public int intIcodModulo = 0;
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        public enum Operacion
        {
            Nuevo = 1,
            Modificar = 2,
            Eliminar = 3,
            Consultar = 4
        }

        #endregion

        #region "Eventos"

        public FrmMantePagoAdelantoNotaCreditoCliente()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
        }

        private void FrmMantePagoAdelantoNotaCreditoCliente_Load(object sender, EventArgs e)
        {
            CargaLista();
            Carga();
        }

        private void btnCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                listar_cliente();
            }
        }
        private void listar_cliente()
        {
            using (FrmListarCliente frmCliente = new FrmListarCliente())
            {
                if (frmCliente.ShowDialog() == DialogResult.OK)
                {
                    intIdAnaliticaCliente = Convert.ToInt32(frmCliente._Be.anac_icod_analitica);
                    btnCliente.Text = frmCliente._Be.cliec_vnombre_cliente;
                    btnCliente.Tag = frmCliente._Be.cliec_icod_cliente;
                    iid_relacion = frmCliente._Be.anac_iid_analitica;
                    icod_tipo_relacion = 2;
                }
            }
        }
        private void CheckFecha()
        {
            if (Convert.ToInt32(lblMes.Text) == DateTime.Now.Month && Parametros.intEjercicio == DateTime.Now.Year)
                dtmFecha.EditValue = DateTime.Now;
            else
                dtmFecha.EditValue =
                    DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(Convert.ToInt32(lblMes.Text) - 1);
        }
        private void dtmFecha_EditValueChanged(object sender, EventArgs e)
        {
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
            BaseEdit oBase = null;
            try
            {
                if (btnCliente.Tag == null)
                {
                    throw new ArgumentException("Seleccione un cliente");
                }
                if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                {
                    throw new ArgumentException("No existe Tipo de Cambio para la fecha seleccionada");
                }
                if (Convert.ToDecimal(txtMonto.Text) == 0)
                {
                    throw new ArgumentException("Ingrese monto del movimiento");
                }
                using (FrmManteCtaxCobrar frm = new FrmManteCtaxCobrar())
                {
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        var ClieList = new BVentas().ListarCliente();
                        var obj = ClieList.Where(x => x.cliec_icod_cliente == Convert.ToInt32(btnCliente.Tag)).ToList()[0];
                        icod_tipo_relacion = 2;
                        iid_relacion = obj.anac_iid_analitica;
                        intIdAnaliticaCliente = Convert.ToInt32(obj.anac_icod_analitica);
                    }
                    frm.Cab_icod_correlativo = IdCodCorrelativo;
                    frm.intIdCliente = Convert.ToInt32(btnCliente.Tag);
                    frm.tip_mon = Convert.ToInt32(lkpMoneda.EditValue);
                    frm.TipoCambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                    frm.oDetailList = mListaDetalle;
                    frm.txtConcepto.Text = txtGlosa.Text;
                    frm.icod_tipo_relacion = icod_tipo_relacion;
                    frm.iid_relacion = iid_relacion;
                    frm.intIdAnaliticaCliente = intIdAnaliticaCliente;
                    frm.cta_cobrar = false;
                    frm.SetInsert();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        mListaDetalle = frm.oDetailList;
                        grdLibroBancosDetalle.DataSource = mListaDetalle;
                        viewLibroBancosDetalle.RefreshData();
                        calcular();
                    }
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
            }

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ELibroBancosDetalle Obe = (ELibroBancosDetalle)viewLibroBancosDetalle.GetRow(viewLibroBancosDetalle.FocusedRowHandle);
            if (Obe != null)
            {
                using (FrmManteCtaxCobrar frm = new FrmManteCtaxCobrar())
                {
                    frm.Obj = Obe;
                    frm.tip_mon = Convert.ToInt32(lkpMoneda.EditValue);
                    frm.intIdCliente = Convert.ToInt32(btnCliente.Tag);
                    frm.TipoCambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                    frm.oDetailList = mListaDetalle;
                    frm.SetModify();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        mListaDetalle = frm.oDetailList;
                        viewLibroBancosDetalle.RefreshData();
                        calcular();
                    }
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ELibroBancosDetalle Obe = (ELibroBancosDetalle)viewLibroBancosDetalle.GetRow(viewLibroBancosDetalle.FocusedRowHandle);
            if (Obe != null)
            {
                mlistDelete.Add(Obe);
                mListaDetalle.Remove(Obe);
                viewLibroBancosDetalle.RefreshData();
                calcular();
            }

        }
        #endregion

        #region "Metodos"

        private void StatusControl()
        {
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpTipoDocumento.Enabled = false;
                txtNumeroDeDocumento.Enabled = false;
                btnCliente.Enabled = false;
                //txtMonto.Enabled = false;
                dtmFecha.Enabled = false;
                txtTipoDeCambio.Enabled = false;
            }
        }

        private void clearControl()
        {
            txtTipoDeCambio.Text = "0.00";
            txtNumeroDeDocumento.Text = "";
            txtGlosa.Text = "";
            txtBeneficia.Text = "";
            lkpTipoDocumento.EditValue = "";
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
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
                    throw new ArgumentException("Ingrese el número de documento");
                }

                if (btnCliente.Tag == null)
                {
                    oBase = btnCliente;
                    throw new ArgumentException("Seleccione un cliente");
                }
                if (string.IsNullOrEmpty(txtGlosa.Text))
                {
                    oBase = txtGlosa;
                    throw new ArgumentException("Ingrese la glosa");
                }
                if (string.IsNullOrEmpty(txtBeneficia.Text))
                {
                    oBase = txtBeneficia;
                    throw new ArgumentException("Ingrese el benificiario");
                }

                if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                {
                    oBase = txtTipoDeCambio;
                    throw new ArgumentException("Seleccione Tipo de Cambio");
                }

                if (Convert.ToDecimal(txtMonto.Text) <= 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("El monto no puede ser 0 o menor que 0");
                }

                if (Convert.ToDecimal(txtMonto.Text) > 0)
                {
                    if (mListaDetalle.Count == 0)
                    {
                        oBase = txtMonto;
                        //throw new ArgumentException("Debe ingresar el detalle antes de continuar con la grabación");
                        if (XtraMessageBox.Show("No ha ingresado el detalle de las cuentas ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                            throw new ArgumentException(string.Empty);
                    }
                }

                if (mListaDetalle.Count > 0)
                {
                    decimal dmlSumMontoDetalle = 0;
                    foreach (var item in mListaDetalle)
                    {
                        if (Convert.ToInt32(lkpMoneda.EditValue) == 3)
                        {
                            dmlSumMontoDetalle = dmlSumMontoDetalle + item.mto_mov_soles;
                        }
                        else
                        {
                            dmlSumMontoDetalle = dmlSumMontoDetalle + item.mto_mov_dolar;
                        }
                    }

                    if (dmlSumMontoDetalle != Convert.ToDecimal(txtMonto.Text))
                    {
                        oBase = txtMonto;
                        //throw new ArgumentException("La sumatoria del monto del detalle no es igual al monto total");
                        if (XtraMessageBox.Show("La sumatoria del monto del detalle no es igual al monto total ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                            throw new ArgumentException(string.Empty);
                    }
                }

                decimal dmlMonto = 0;
                decimal.TryParse(txtMonto.Text, out dmlMonto);

                //Datos Entidad Financiera Movimiento
                oBe.icod_correlativo = IdCodCorrelativo;
                oBe.iid_correlativo = Correlative;
                oBe.iid_anio = Parametros.intEjercicio;
                oBe.iid_mes = Convert.ToInt32(lblMes.Text);
                oBe.icod_enti_financiera_cuenta = Convert.ToInt32(lblCuenta.Text);
                oBe.ii_tipo_doc = Convert.ToInt32(lkpTipoDocumento.EditValue);
                oBe.vdescripcion_beneficiario = txtBeneficia.Text;
                oBe.iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.nmonto_tipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                oBe.nmonto_movimiento = dmlMonto;
                oBe.nmonto_saldo_banco = 0;
                oBe.iid_situacion_movimiento_banco = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.dfecha_movimiento = Convert.ToDateTime(dtmFecha.Text);
                oBe.cflag_tipo_movimiento = Parametros.intTipoMovimientoCargo;
                oBe.vnro_documento = txtNumeroDeDocumento.Text;
                oBe.cflag_conciliacion = flag_conciliacion;
                oBe.iusuario_crea = Valores.intUsuario;
                oBe.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.vglosa = txtGlosa.Text;
                oBe.iid_motivo_mov_banco = IdMotivoMovimientoBanco;
                oBe.cliec_icod_cliente = Convert.ToInt32(btnCliente.Tag);
                oBe.mobac_flag_estado = true;
                oBe.TipoDocumento = lkpTipoDocumento.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    IdCodCorrelativo = Obl.insertarLibroBancos(oBe, mListaDetalle, null, null, null, null, null);
                    ActualizarCorrelativo();
                }
                else
                {
                    oBe.iusuario_modifica = Valores.intUsuario;
                    oBe.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                    Obl.actualizarLibroBancos(oBe, mListaDetalle, null, null, null, null, mlistDelete);
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
                    this.MiEvento(IdCodCorrelativo);
                    this.Close();
                }
            }

        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void CargaLista()
        {
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(36), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            /**/
            var lstTipoDocContabilidad = new BAdministracionSistema().listarTipoDocumentoPorModulo(intIcodModulo);
            BSControls.LoaderLook(lkpTipoDocumento, lstTipoDocContabilidad, "tdocc_vdescripcion", "tdocc_icod_tipo_doc", false);
            /**/
        }

        private void Carga()
        {
            mListaDetalle = new BTesoreria().ListarEntidadFinancieraDetalleADPNCP_Cliente(IdCodCorrelativo);
            mListaDetalle = mListaDetalle.OrderBy(x => x.icod_correlativo).ToList();
            grdLibroBancosDetalle.DataSource = mListaDetalle;
        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
        }

        public void calcular()
        {
            if (Convert.ToInt32(lkpMoneda.EditValue) == 3)
            {
                txtTotal.Text = mListaDetalle.Sum(x => x.mto_mov_soles).ToString();
                txtResta.Text = (Convert.ToDecimal(txtMonto.Text) - Convert.ToDecimal(txtTotal.Text)).ToString();
            }
            else
            {
                txtTotal.Text = mListaDetalle.Sum(x => x.mto_mov_dolar).ToString();
                txtResta.Text = (Convert.ToDecimal(txtMonto.Text) - Convert.ToDecimal(txtTotal.Text)).ToString();
            }
        }

        #endregion
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void txtMonto_EditValueChanged(object sender, EventArgs e)
        {
            calcular();
        }

        private void btnCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                listar_cliente();
            }
        }

        private void lkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            GetCorrelativo();
        }
        private void GetCorrelativo()
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
            }
        }
        private void ActualizarCorrelativo()
        {
            objBtipoDoc.ActualizarCorrelativoDocumentoBancos(Parametros.intEjercicio, Convert.ToInt32(lblMes.Text), Convert.ToInt32(lblCuenta.Text), Convert.ToInt32(lkpTipoDocumento.EditValue), txtNumeroDeDocumento.Text);
        }
    }
}