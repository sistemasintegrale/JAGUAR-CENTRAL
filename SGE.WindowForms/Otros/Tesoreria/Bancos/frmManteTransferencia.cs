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
    public partial class frmManteTransferencia : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        public int intMes = 0;
        public int intCuenta = 0;
        public int intBanco = 0;
        public ELibroBancos oBe = new ELibroBancos();
        ELibroBancos oBe2 = new ELibroBancos();

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
            if (Status == BSMaintenanceStatus.View)
            { }
        }

        public frmManteTransferencia()
        {
            InitializeComponent();
        }

        private void frmManteTransferencia_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            BSControls.LoaderLook(lkpTipoDocumento, new BAdministracionSistema().listarTipoDocumentoPorModulo(Parametros.intModuloTesoreria), "tdocc_vdescripcion", "tdocc_icod_tipo_doc", true);
            BSControls.LoaderLook(lkpBanco, new BTesoreria().listarBancos(), "bcoc_vnombre_banco", "bcoc_icod_banco", true);
            BSControls.LoaderLook(lkpBancoOri, new BTesoreria().listarBancos().Where(x => x.bcoc_icod_banco == intBanco).ToList(), "bcoc_vnombre_banco", "bcoc_icod_banco", true);
            /**/
            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();
            if (Status == BSMaintenanceStatus.CreateNew)
                setFecha();
        }

        private void setFecha()
        {
            if (intMes == DateTime.Now.Month && Parametros.intEjercicio == DateTime.Now.Year)
                dtFecha.EditValue = DateTime.Now;
            else
                dtFecha.EditValue =
                    DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(intMes - 1);
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

        public void setValues()
        {
            lkpTipoDocumento.EditValue = oBe.ii_tipo_doc;
            txtNroDoc.Text = oBe.vnro_documento;
            txtGlosa.Text = oBe.vglosa;
            txtBeneficia.Text = oBe.vdescripcion_beneficiario;
            txtMonto.Text = oBe.nmonto_movimiento.ToString();
            txtTipoCambio.Text = oBe.nmonto_tipo_cambio.ToString();
            dtFecha.EditValue = oBe.dfecha_movimiento;
            var oBeTrans = new BTesoreria().listarTransferencia(Convert.ToInt32(oBe.id_transferencia));
            lkpBanco.EditValue = oBeTrans[0].bcoc_icod_banco;
            lkpCuenta.EditValue = oBeTrans[0].bcod_icod_banco_cuenta;
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToDecimal(txtMonto.Text) == 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("Ingrese el monto de la transferencia");
                }

                if (Convert.ToDecimal(txtTipoCambio.Text) == 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("No existe tipo de cambio registrado, ingresar tipo de cambio");
                }

                #region Salida
                oBe.iid_anio = Parametros.intEjercicio;
                oBe.iid_mes = intMes;
                oBe.icod_enti_financiera_cuenta = Convert.ToInt32(lkpCuentaOri.EditValue);
                oBe.ii_tipo_doc = Convert.ToInt32(lkpTipoDocumento.EditValue);
                oBe.vdescripcion_beneficiario = txtBeneficia.Text;
                oBe.iid_tipo_moneda = Convert.ToInt32(lkpCuentaOri.GetColumnValue("tablc_iid_tipo_moneda"));
                oBe.nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                oBe.nmonto_movimiento = Convert.ToDecimal(txtMonto.Text);
                oBe.nmonto_saldo_banco = 0;
                oBe.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                oBe.dfecha_movimiento = Convert.ToDateTime(dtFecha.Text);
                oBe.cflag_tipo_movimiento = "0";//0 ES SALIDA
                oBe.vnro_documento = txtNroDoc.Text;
                oBe.cflag_conciliacion = false;
                oBe.iusuario_crea = Valores.intUsuario;
                oBe.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.vglosa = txtGlosa.Text;
                oBe.iid_motivo_mov_banco = Parametros.intMotivoTransferenciaCuentas;
                oBe.mobac_flag_estado = true;
                oBe.TipoDocumento = lkpTipoDocumento.Text;
                #endregion
                #region Ingreso
                oBe2.icod_correlativo = Convert.ToInt32(oBe.id_transferencia);
                oBe2.iid_anio = Parametros.intEjercicio;
                oBe2.iid_mes = intMes;
                oBe2.icod_enti_financiera_cuenta = Convert.ToInt32(lkpCuenta.EditValue);
                oBe2.ii_tipo_doc = Convert.ToInt32(lkpTipoDocumento.EditValue);
                oBe2.vdescripcion_beneficiario = txtBeneficia.Text;
                oBe2.iid_tipo_moneda = Convert.ToInt32(lkpCuenta.GetColumnValue("tablc_iid_tipo_moneda"));
                oBe2.nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                if (oBe.iid_tipo_moneda != oBe2.iid_tipo_moneda)
                {
                    if (oBe.iid_tipo_moneda == 3)
                        oBe2.nmonto_movimiento = Math.Round(Convert.ToDecimal(txtMonto.Text) / Convert.ToDecimal(txtTipoCambio.Text), 2);
                    else
                        oBe2.nmonto_movimiento = Math.Round(Convert.ToDecimal(txtMonto.Text) * Convert.ToDecimal(txtTipoCambio.Text), 2);
                }
                else
                    oBe2.nmonto_movimiento = Convert.ToDecimal(txtMonto.Text);

                oBe2.nmonto_saldo_banco = 0;
                oBe2.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                oBe2.dfecha_movimiento = Convert.ToDateTime(dtFecha.Text);
                oBe2.cflag_tipo_movimiento = "1";//1 ES INGRESO
                oBe2.vnro_documento = txtNroDoc.Text;
                oBe2.cflag_conciliacion = false;
                oBe2.iusuario_crea = Valores.intUsuario;
                oBe2.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                oBe2.vglosa = txtGlosa.Text;
                oBe2.iid_motivo_mov_banco = Parametros.intMotivoTransferenciaCuentas;
                oBe2.mobac_flag_estado = true;
                oBe2.TipoDocumento = lkpTipoDocumento.Text;
                #endregion
                if (Status == BSMaintenanceStatus.CreateNew)
                    oBe.iid_correlativo = new BTesoreria().insertarTransferencia(oBe, oBe2);
                else
                    new BTesoreria().modificarTransferencia(oBe, oBe2);


            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
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
                    MiEvento(oBe.iid_correlativo);
                    Close();
                }
            }
        }


        private void lkpBanco_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpBanco.EditValue != null)
            {
                if (Convert.ToInt32(lkpBanco.EditValue) == intBanco)
                {
                    var lstCuentas = new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBanco.EditValue)).Where(x => x.bcod_icod_banco_cuenta != intCuenta).ToList();
                    BSControls.LoaderLook(lkpCuenta, lstCuentas, "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);
                }
                else
                {
                    var lstCuentas = new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBanco.EditValue));
                    BSControls.LoaderLook(lkpCuenta, lstCuentas, "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);
                }
            }
        }

        private void dtFecha_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                if (Convert.ToDateTime(dtFecha.EditValue).Month == intMes)
                {
                    var Lista = lstTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtFecha.EditValue).ToShortDateString()).ToList();
                    if (Lista.Count > 0)
                    {
                        Lista.ForEach(obe =>
                        {
                            txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                        });
                    }
                    else
                        txtTipoCambio.Text = "0.0000";
                }
                else
                {
                    XtraMessageBox.Show("La fecha seleccionada no está dentro del mes o año de ejercicio", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setFecha();
                    dtFecha.Focus();
                }
            }
        }

        private void lkpBancoOri_EditValueChanged(object sender, EventArgs e)
        {
            var lstCuentas = new BTesoreria().listarBancoCuentas(intBanco).Where(x => x.bcod_icod_banco_cuenta == intCuenta).ToList();
            BSControls.LoaderLook(lkpCuentaOri, lstCuentas, "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}