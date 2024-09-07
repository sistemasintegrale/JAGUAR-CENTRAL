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
    public partial class FrmManteCuentasPorCobrar : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteCuentasPorCobrar));

        List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();

        public delegate void DelegadoMensaje(int cab_correl);
        public event DelegadoMensaje MiEvento;
        private List<ECuentaContable> lstCuentaContable = new List<ECuentaContable>();
        public BAdministracionSistema objBtipoDoc = new BAdministracionSistema();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BTesoreria Obl;
        public int IdCodCorrelativo = 0;
        public int Correlative = 0;
        public int IdMotivoMovimientoBanco = 0;
        public int intIdAnaliticaCliente;
        string iid_relacion;
        int? icod_tipo_relacion;
        public int vcocc_iid_voucher_contable;
        public bool flag_conciliacion = false;

        List<ELibroBancosDetalle> mlist = new List<ELibroBancosDetalle>();
        List<ELibroBancosDetalle> mlistDelete = new List<ELibroBancosDetalle>();
        public int intIcodModulo = 6;
        public int icodBancos, icodBancoDet;
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

        public FrmManteCuentasPorCobrar()
        {
            InitializeComponent();
        }

        private void FrmManteCuentasPorCobrar_Load(object sender, EventArgs e)
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
                    bteCliente.Text = frmCliente._Be.cliec_vnombre_cliente;
                    bteCliente.Tag = frmCliente._Be.cliec_icod_cliente;
                    iid_relacion = frmCliente._Be.anac_iid_analitica;
                    //icod_tipo_relacion = frmCliente._Be.tarec_icorrelativo;
                    icod_tipo_relacion = 2;//clientes
                }
            }
        }


        private void dtmFecha_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtmFecha.EditValue).Month == Convert.ToInt32(lblMes.Text))
            {
                var Lista = lstTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtmFecha.EditValue).ToShortDateString()).ToList();
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
        private void CheckFecha()
        {
            if (Convert.ToInt32(lblMes.Text) == DateTime.Now.Month && Parametros.intEjercicio == DateTime.Now.Year)
                dtmFecha.EditValue = DateTime.Now;
            else
                dtmFecha.EditValue =
                    DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(Convert.ToInt32(lblMes.Text) - 1);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            try
            {
                if (bteCliente.Tag == null)
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
                        var obj = ClieList.Where(x => x.cliec_icod_cliente == Convert.ToInt32(bteCliente.Tag)).ToList()[0];
                        icod_tipo_relacion = 2;
                        iid_relacion = obj.anac_iid_analitica;
                        intIdAnaliticaCliente = Convert.ToInt32(obj.anac_icod_analitica);
                    }
                    frm.intIdCliente = Convert.ToInt32(bteCliente.Tag);
                    frm.tip_mon = Convert.ToInt32(lkpMoneda.EditValue);
                    frm.TipoCambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                    frm.oDetailList = mlist;
                    frm.txtConcepto.Text = txtGlosa.Text;
                    frm.icod_tipo_relacion = icod_tipo_relacion;
                    frm.iid_relacion = iid_relacion;
                    frm.intIdAnaliticaCliente = intIdAnaliticaCliente;
                    frm.cta_cobrar = true;
                    frm.SetInsert();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        mlist = frm.oDetailList;
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
                    frm.intIdCliente = Convert.ToInt32(bteCliente.Tag);
                    frm.TipoCambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                    frm.oDetailList = mlist;
                    frm.SetModify();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        mlist = frm.oDetailList;
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
                mlist.Remove(Obe);
                viewLibroBancosDetalle.RefreshData();
                calcular();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            this.SetSave();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        #endregion

        #region "Metodos"

        private void StatusControl()
        {
            if (Status == BSMaintenanceStatus.View)
            {
                txtBeneficia.Enabled = false;
                txtGlosa.Enabled = false;
                txtMonto.Enabled = false;
                txtNumeroDeDocumento.Enabled = false;
                txtTipoDeCambio.Enabled = false;
                lkpTipoDocumento.Enabled = false;
                dtmFecha.Enabled = false;
                txtMonto.Enabled = false;
                txtTipoDeCambio.Enabled = false;
                lkpTipoDocumento.Enabled = false;
                mnuLibroBancosDetalle.Enabled = false;
                btnGuardar.Enabled = false;
                bteCliente.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

                txtMonto.Enabled = false;
                txtNumeroDeDocumento.Enabled = false;
                bteCliente.Enabled = false;
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

                    if (bteCliente.Tag == null)
                    {
                        oBase = bteCliente;
                        throw new ArgumentException("Seleccione cliente");
                    }

                    if (string.IsNullOrEmpty(txtBeneficia.Text))
                    {
                        oBase = txtBeneficia;
                        throw new ArgumentException("Ingrese el benificiario");
                    }

                    if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                    {
                        oBase = txtTipoDeCambio;
                        throw new ArgumentException("Seleccione o ingrese Tipo de Cambio");
                    }

                    if (Convert.ToDecimal(txtMonto.Text) <= 0)
                    {
                        oBase = txtMonto;
                        throw new ArgumentException("El monto no puede ser 0 o menor que 0.");
                    }

                    if (mlist.Count == 0)
                    {
                        //throw new ArgumentException("Debe ingresar el detalle antes continuar con la grabación");
                        if (XtraMessageBox.Show("No ha ingresado el detalle de las cuentas ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                            throw new ArgumentException(string.Empty);
                    }

                    if (Convert.ToInt32(lkpMoneda.EditValue) == 3)
                    {
                        if (Convert.ToDecimal(txtMonto.Text) != mlist.Sum(x => x.mto_mov_soles))
                        {
                            oBase = txtMonto;
                            //throw new ArgumentException("La sumatoria del monto del detalle no es igual al monto total");
                            if (XtraMessageBox.Show("La sumatoria del monto del detalle no es igual al monto total ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                                throw new ArgumentException(string.Empty);
                        }
                    }
                    if (Convert.ToInt32(lkpMoneda.EditValue) == 2)
                    {
                        if (Convert.ToDecimal(txtMonto.Text) != mlist.Sum(x => x.mto_mov_dolar))
                        {
                            oBase = txtMonto;
                            //throw new ArgumentException("La sumatoria del monto del detalle no es igual al monto total");
                            if (XtraMessageBox.Show("La sumatoria del monto del detalle no es igual al monto total ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                                throw new ArgumentException(string.Empty);
                        }
                    }

                    //Datos Entidad Financiera Movimiento
                    oBe.icod_correlativo = IdCodCorrelativo;
                    oBe.iid_anio = Parametros.intEjercicio;
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
                    oBe.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                    oBe.vnro_documento = txtNumeroDeDocumento.Text;
                    oBe.cflag_conciliacion = flag_conciliacion;
                    oBe.iusuario_crea = Valores.intUsuario;
                    oBe.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                    oBe.vglosa = txtGlosa.Text;
                    oBe.iid_motivo_mov_banco = IdMotivoMovimientoBanco;
                    oBe.cliec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                    oBe.mobac_flag_estado = true;
                    oBe.TipoDocumento = lkpTipoDocumento.Text;


                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        IdCodCorrelativo = Obl.insertarLibroBancos(oBe, mlist, null, null, null, null, null);
                        ActualizarCorrelativo();
                    }
                    else
                    {
                        oBe.iusuario_modifica = Valores.intUsuario;
                        oBe.vpc_modifica = WindowsIdentity.GetCurrent().Name.ToString();
                        Obl.actualizarLibroBancos(oBe, mlist, null, null, null, null, mlistDelete);
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
        }

        private void CargaLista()
        {
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(36), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            /**/
            var lstTipoDocContabilidad = new BAdministracionSistema().listarTipoDocumentoPorModulo(intIcodModulo);
            BSControls.LoaderLook(lkpTipoDocumento, lstTipoDocContabilidad, "tdocc_vdescripcion", "tdocc_icod_tipo_doc", false);
            /**/
            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();
        }

        private void Carga()
        {
            mlist = new BTesoreria().ListarEntidadFinancieraDetalle(IdCodCorrelativo);
            grdLibroBancosDetalle.DataSource = mlist;
        }

        #endregion
        public void calcular()
        {
            if (Convert.ToInt32(lkpMoneda.EditValue) == 3)
            {
                txtTotal.Text = mlist.Sum(x => x.mto_mov_soles).ToString();
                txtResta.Text = (Convert.ToDecimal(txtMonto.Text) - Convert.ToDecimal(txtTotal.Text)).ToString();
            }
            else
            {
                txtTotal.Text = mlist.Sum(x => x.mto_mov_dolar).ToString();
                txtResta.Text = (Convert.ToDecimal(txtMonto.Text) - Convert.ToDecimal(txtTotal.Text)).ToString();
            }
        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
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
        public List<ECajaCorrelativo> lst = new List<ECajaCorrelativo>();
        private void GetCorrelativo()
        {
            if (intIcodModulo == 14)
            {
                lst = new BTesoreria().listarCajasCorrelativo().Where(x => x.bcoc_icod_banco == icodBancos && x.bcod_icod_banco_cuenta == icodBancoDet && x.tdocc_icod_tipo_doc == Convert.ToInt32(lkpTipoDocumento.EditValue)).ToList();
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

        private bool flag_exist(int? intTipoDoc, string strNroDoc)
        {
            bool rpta = false;
            if (mlist.Where(x => x.tdocc_icod_tipo_doc == intTipoDoc && x.vnumero_doc == strNroDoc).ToList().Count > 0)
                rpta = true;
            return rpta;
        }

        private void nuevoSeleccMultipleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(bteCliente.Tag) == 0)
                    throw new ArgumentException("Seleccione el cliente");

                if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                    throw new ArgumentException("Registrar tipo de cambio");

                FrmListarDocxCobrar frm = new FrmListarDocxCobrar();

                frm.bDocFacBol = true;
                frm.flag_multiple = true;
                frm.intIcodCliente = Convert.ToInt32(bteCliente.Tag);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var lst = frm.lstDocPorCobrar;

                    lst.Where(r => r.flag_multiple).ToList().ForEach(x =>
                    {
                        ELibroBancosDetalle Obj = new ELibroBancosDetalle();
                        Obj.iid_correlativo = IdCodCorrelativo;
                        Obj.doxcc_icod_correlativo = x.doxcc_icod_correlativo;
                        Obj.tdocc_icod_tipo_doc = x.tdocc_icod_tipo_doc;
                        Obj.tdodc_iid_correlativo = x.tdodc_iid_correlativo;
                        Obj.mto_mov_soles = (x.tablc_iid_tipo_moneda == 3) ? Convert.ToDecimal(x.doxcc_nmonto_saldo) : Math.Round(Convert.ToDecimal(x.doxcc_nmonto_saldo) * Convert.ToDecimal(txtTipoDeCambio.Text), 2);
                        Obj.mto_mov_dolar = (x.tablc_iid_tipo_moneda == 4) ? Convert.ToDecimal(x.doxcc_nmonto_saldo) : Math.Round(Convert.ToDecimal(x.doxcc_nmonto_saldo) / Convert.ToDecimal(txtTipoDeCambio.Text), 2);

                        Obj.mto_detalle_soles = Obj.mto_mov_soles;
                        Obj.mto_detalle_dolar = Obj.mto_mov_dolar;
                        Obj.docxp_nmonto_total_documento = Convert.ToDecimal(x.doxcc_nmonto_total);
                        Obj.vglosa = txtGlosa.Text;
                        Obj.iusuario_crea = Valores.intUsuario;
                        Obj.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                        Obj.mobdc_flag_estado = true;
                        //

                        Obj.mobdc_icod_cliente = x.cliec_icod_cliente;
                        Obj.tablc_icod_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        Obj.icod_analitica = x.intAnaliticaCliente;
                        Obj.anac_iid_analitica = String.Format("{0:00}", Parametros.intTipoAnaliticaClientes);
                        //if (deFechaPago.EditValue != null) { Obj.doxpc_sfecha_doc = Convert.ToDateTime(deFechaPago.EditValue); }
                        //else { Obj.doxpc_sfecha_doc = null; }
                        Obj.tdocc_vabreviatura_tipo_doc = x.tdocc_vabreviatura_tipo_doc;
                        Obj.vnumero_doc = x.doxcc_vnumero_doc;
                        Obj.doxcc_sfecha_doc = x.doxcc_sfecha_doc;
                        Obj.TipOper = 1;
                        if (!flag_exist(Obj.tdocc_icod_tipo_doc, Obj.vnumero_doc))
                            mlist.Add(Obj);
                    });
                }
                viewLibroBancosDetalle.RefreshData();
                calcular();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}