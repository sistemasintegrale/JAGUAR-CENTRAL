using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class FrmManteCuentasPorPagar : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteCuentasPorPagar));
        List<ELibroBancosDetalle> mlist = new List<ELibroBancosDetalle>();
        List<ELibroBancosDetalle> mlistDelete = new List<ELibroBancosDetalle>();
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        public BAdministracionSistema objBtipoDoc = new BAdministracionSistema();
        private int xposition = 0;
        public delegate void DelegadoMensaje(int cab_correl);
        public event DelegadoMensaje MiEvento;
        private List<ECuentaContable> mListaCuenta = new List<ECuentaContable>();
        public List<ELibroBancosDetalle> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private BTesoreria Obl;
        public int IdCodCorrelativo = 0;
        public int Correlative = 0;
        public int IdMotivoMovimientoBanco = 0;
        public int intIdAnaliticaProveedor = 0;
        private string sRUC = "";
        string iid_relacion;
        int? icod_tipo_relacion;
        int? flag_exceptuado;
        decimal mnto;
        public int vcocc_iid_voucher_contable;
        public bool flag_conciliacion = false;
        public int intIcodModulo = 0;
        public int icodBancos, icodBancoDet;

        public List<EImagenCheque> lstImagenCheque = new List<EImagenCheque>();

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

        public FrmManteCuentasPorPagar()
        {
            InitializeComponent();
        }

        private void FrmManteCuentasPorPagar_Load(object sender, EventArgs e)
        {
            CargaLista();
            Carga();
        }

        private void btnProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listar_proveedor();
        }
        private void listar_proveedor()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                intIdAnaliticaProveedor = Proveedor._Be.anac_icod_analitica;
                sRUC = Proveedor._Be.vcod_proveedor;
                btnProveedor.Tag = Proveedor._Be.iid_icod_proveedor;
                btnProveedor.Text = Proveedor._Be.vnombrecompleto;
                iid_relacion = Proveedor._Be.anac_iid_analitica;
                //icod_tipo_relacion = Proveedor._Be.tarec_icorrelativo;
                icod_tipo_relacion = 5;//proveedores
                flag_exceptuado = Proveedor._Be.flag_exceptuado;
            }
        }

        private void dtmFecha_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtmFecha.EditValue).Month == Convert.ToInt32(lblMes.Text))
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
                if (btnProveedor.Tag == null)
                {
                    oBase = btnProveedor;
                    throw new ArgumentException("Seleccione un proveedor");
                }
                if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                {
                    oBase = dtmFecha;
                    throw new ArgumentException("No existe Tipo de Cambio para la fecha seleccionada");
                }
                if (Convert.ToDecimal(txtMonto_.Text) == 0)
                {
                    oBase = txtMonto_;
                    throw new ArgumentException("Ingrese el monto del movimiento");
                }


                if (Convert.ToDecimal(lkpMoneda.EditValue) == 3) mnto = Convert.ToDecimal(txtMonto_.Text);
                if (Convert.ToDecimal(lkpMoneda.EditValue) == 4)
                    mnto = Convert.ToDecimal(txtMonto_.Text) / Convert.ToDecimal(txtTipoDeCambio.Text);


                using (FrmManteCtaxPagar frm = new FrmManteCtaxPagar())
                {
                    frm.TipoCambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                    frm.flag_exceptuado = flag_exceptuado;
                    frm.mnto_total = mnto;
                    frm.tip_mon = Convert.ToInt32(lkpMoneda.EditValue);
                    frm.lstDetalle = mlist;
                    frm.txtConcepto.Text = txtGlosa.Text;
                    frm.SetInsert();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        mlist = frm.lstDetalle;
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
        public void calcular()
        {
            if (Convert.ToInt32(lkpMoneda.EditValue) == 3)
            {
                txtTotal.Text = mlist.Sum(x => x.mto_mov_soles).ToString();
                txtResta.Text = (Convert.ToDecimal(txtMonto_.Text) - Convert.ToDecimal(txtTotal.Text)).ToString();
            }
            else
            {
                txtTotal.Text = mlist.Sum(x => x.mto_mov_dolar).ToString();
                txtResta.Text = (Convert.ToDecimal(txtMonto_.Text) - Convert.ToDecimal(txtTotal.Text)).ToString();
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ELibroBancosDetalle Obe = (ELibroBancosDetalle)viewLibroBancosDetalle.GetRow(viewLibroBancosDetalle.FocusedRowHandle);
            if (Obe != null)
            {
                if (Convert.ToDecimal(lkpMoneda.EditValue) == 3) mnto = Convert.ToDecimal(txtMonto_.Text);
                if (Convert.ToDecimal(lkpMoneda.EditValue) == 4)
                    mnto = Convert.ToDecimal(txtMonto_.Text) / Convert.ToDecimal(txtTipoDeCambio.Text);
                using (FrmManteCtaxPagar frm = new FrmManteCtaxPagar())
                {
                    frm.Obj = Obe;
                    frm.tip_mon = Convert.ToInt32(lkpMoneda.EditValue);
                    frm.flag_exceptuado = flag_exceptuado;
                    frm.mnto_total = mnto;
                    frm.TipoCambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                    frm.lstDetalle = mlist;
                    frm.SetModify();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        mlist = frm.lstDetalle;
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
                Obe.TipOper = 3;
                mlistDelete.Add(Obe);
                mlist.Remove(Obe);
                viewLibroBancosDetalle.RefreshData();
                calcular();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            SetSave();
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
                txtMonto_.Enabled = false;
                txtNumeroDeDocumento.Enabled = false;
                txtTipoDeCambio.Enabled = false;
                lkpTipoDocumento.Enabled = false;
                dtmFecha.Enabled = false;
                txtMonto_.Enabled = false;
                txtTipoDeCambio.Enabled = false;
                lkpTipoDocumento.Enabled = false;
                mnuLibroBancosDetalle.Enabled = false;
                btnGuardar.Enabled = false;
                btnProveedor.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpTipoDocumento.Enabled = false;
                txtNumeroDeDocumento.Enabled = false;
                btnProveedor.Enabled = false;
                dtmFecha.Enabled = false;
                txtMonto_.Enabled = true;
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
            //txtMonto_.EditValue = 0;
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

                    if (btnProveedor.Tag == null)
                    {
                        oBase = btnProveedor;
                        throw new ArgumentException("Seleccione un proveedor");
                    }
                    if (string.IsNullOrEmpty(txtGlosa.Text))
                    {
                        oBase = txtGlosa;
                        throw new ArgumentException("Ingrese glosa");
                    }
                    if (string.IsNullOrEmpty(txtBeneficia.Text))
                    {
                        oBase = txtBeneficia;
                        throw new ArgumentException("Ingrese el benificiario");
                    }

                    if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                    {
                        oBase = dtmFecha;
                        throw new ArgumentException("No existe Tipo de Cambio para la fecha seleccionada");
                    }


                    if (Convert.ToDecimal(txtMonto_.Text) <= 0)
                    {
                        oBase = txtMonto_;
                        throw new ArgumentException("Ingrese el monto del movimiento");
                    }

                    if (mlist.Count == 0)
                    {
                        //throw new ArgumentException("Debe ingresar el detalle antes de continuar con la grabación");
                        if (XtraMessageBox.Show("No ha ingresado el detalle de las cuentas ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                            throw new ArgumentException(string.Empty);
                    }

                    if (Convert.ToInt32(lkpMoneda.EditValue) == 3)
                    {
                        if (Convert.ToDecimal(txtMonto_.Text) != mlist.Sum(x => x.mto_mov_soles))
                        {
                            oBase = txtMonto_;
                            //throw new ArgumentException("La sumatoria del monto del detalle no es igual al monto total");
                            if (XtraMessageBox.Show("La sumatoria del monto del detalle no es igual al monto total ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                                throw new ArgumentException(string.Empty);
                        }
                    }
                    if (Convert.ToInt32(lkpMoneda.EditValue) == 4)
                    {
                        if (Convert.ToDecimal(txtMonto_.Text) != mlist.Sum(x => x.mto_mov_dolar))
                        {
                            oBase = txtMonto_;
                            //throw new ArgumentException("La sumatoria del monto del detalle no es igual al monto total");
                            if (XtraMessageBox.Show("La sumatoria del monto del detalle no es igual al monto total ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                                throw new ArgumentException(string.Empty);
                        }
                    }



                    //Datos Entidad Financiera Movimiento
                    oBe.icod_correlativo = IdCodCorrelativo;
                    oBe.iid_anio = Parametros.intEjercicio;
                    oBe.iid_mes = Convert.ToInt32(lblMes.Text);
                    //obed.mobdc_iid_anio = Parametros.intEjercicio;
                    //obed.mobdc_iid_mes_periodo = Convert.ToInt32(lblMes.Text);
                    oBe.icod_enti_financiera_cuenta = Convert.ToInt32(lblCuenta.Text);
                    oBe.ii_tipo_doc = Convert.ToInt32(lkpTipoDocumento.EditValue);
                    oBe.vdescripcion_beneficiario = txtBeneficia.Text;
                    oBe.iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                    oBe.nmonto_tipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                    oBe.nmonto_movimiento = Convert.ToDecimal(txtMonto_.Text);
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
                    oBe.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
                    oBe.mobac_flag_estado = true;
                    oBe.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
                    oBe.TipoDocumento = lkpTipoDocumento.Text;









                    DateTime? dtNullVal = null;


                    if (Status == BSMaintenanceStatus.CreateNew)
                    {

                        IdCodCorrelativo = Obl.insertarLibroBancos(oBe, mlist, null, null, null, null, lstImagenCheque);
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
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
        }

        private void Carga()
        {

            mlist = new BTesoreria().ListarEntidadFinancieraDetalle(IdCodCorrelativo);
            grdLibroBancosDetalle.DataSource = mlist;
        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
            viewLibroBancosDetalle.FocusedRowHandle = xposition;
        }
        #endregion
        private void CheckFecha()
        {
            if (Convert.ToInt32(lblMes.Text) == DateTime.Now.Month && Parametros.intEjercicio == DateTime.Now.Year)
                dtmFecha.EditValue = DateTime.Now;
            else
                dtmFecha.EditValue =
                    DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(Convert.ToInt32(lblMes.Text) - 1);
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void btnProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                listar_proveedor();
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

                    if (Convert.ToInt32(lkpTipoDocumento.EditValue) == 14)
                    {
                        btnAgregarFoto.Enabled = true;

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
                if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                    throw new ArgumentException("Registrar tipo de cambio");

                FrmListarDocumentoPorPagarProveedor frm = new FrmListarDocumentoPorPagarProveedor();
                frm.flag_list_all = true;
                frm.bDocFacBol = false;
                frm.flag_multiple = true;
                frm.Carga();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var lst = frm.lstDocPorPagar;
                    lst.Where(r => r.flag_multiple).ToList().ForEach(x =>
                    {
                        ELibroBancosDetalle Obj = new ELibroBancosDetalle();
                        Obj.iid_correlativo = IdCodCorrelativo;
                        Obj.doxpc_icod_correlativo = x.doxpc_icod_correlativo;
                        Obj.mobdc_iid_anio = Parametros.intEjercicio;
                        Obj.mobdc_iid_mes_periodo = Convert.ToInt32(lblMes.Text);
                        Obj.tdocc_icod_tipo_doc = x.tdocc_icod_tipo_doc;
                        Obj.tdodc_iid_correlativo = x.tdodc_iid_correlativo;
                        Obj.mto_mov_soles = (x.tablc_iid_tipo_moneda == 3) ? Convert.ToDecimal(x.doxpc_nmonto_total_saldo) : Math.Round(Convert.ToDecimal(x.doxpc_nmonto_total_saldo) * Convert.ToDecimal(txtTipoDeCambio.Text), 2);
                        Obj.mto_mov_dolar = (x.tablc_iid_tipo_moneda == 4) ? Convert.ToDecimal(x.doxpc_nmonto_total_saldo) : Math.Round(Convert.ToDecimal(x.doxpc_nmonto_total_saldo) / Convert.ToDecimal(txtTipoDeCambio.Text), 2);
                        //Obj.mto_retenido_soles = Convert.ToDecimal(txtRetencionSoles.Text);
                        //Obj.mto_retenido_dolar = Convert.ToDecimal(txtRetencionDolares.Text);
                        Obj.mto_detalle_soles = Obj.mto_mov_soles;
                        Obj.mto_detalle_dolar = Obj.mto_mov_dolar;
                        Obj.docxp_nmonto_total_documento = Convert.ToDecimal(x.doxpc_nmonto_total_documento);
                        Obj.vglosa = txtGlosa.Text;
                        Obj.iusuario_crea = Valores.intUsuario;
                        Obj.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                        Obj.mobdc_flag_estado = true;
                        //
                        //Obj.doxpc_icod_documento = x.doxpc_icod_documento;
                        Obj.mobdc_icod_proveedor = x.proc_icod_proveedor;
                        Obj.tablc_icod_tipo_analitica = x.tipo_analitica;
                        Obj.icod_analitica = x.anac_icod_analitica;
                        Obj.anac_iid_analitica = String.Format("{0:00}", x.tipo_analitica);
                        //if (deFechaPago.EditValue != null) { Obj.doxpc_sfecha_doc = Convert.ToDateTime(deFechaPago.EditValue); }
                        //else { Obj.doxpc_sfecha_doc = null; }
                        Obj.tdocc_vabreviatura_tipo_doc = x.tdocc_vabreviatura_tipo_doc;
                        Obj.vnumero_doc = x.doxpc_vnumero_doc;
                        Obj.doxpc_sfecha_doc = x.doxpc_sfecha_doc;
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

        private void btnAgregarFoto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Listarfoto();
        }
        private void Listarfoto()
        {

            frmManteDiseñoMuestra Proveedor = new frmManteDiseñoMuestra();
            //if (Obe.prsc_vruta_foto == null)
            //{
            //    Obe.prsc_vruta_foto = "";
            //}
            //Proveedor._be = lstImagenCheque.FirstOrDefault(); 
            Proveedor.lstImagenCheque = lstImagenCheque;


            //Proveedor.ChosenFile = Obe.prsc_vruta_foto;
            //Proveedor._be = Obe;
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                //Obe.prsc_vruta_foto = Proveedor._be.prsc_vruta_foto;
                lstImagenCheque = Proveedor.lstImagenCheque;
                //if (Obe.prsc_vruta_foto != "")
                //{
                //    btnAgregarFoto.Text = "Contiene Foto";
                //}
            }

        }
    }
}