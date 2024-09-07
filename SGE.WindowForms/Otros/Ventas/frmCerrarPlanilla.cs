using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmCerrarPlanilla : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        public EPlanillaCobranzaCab oBePlnCab = new EPlanillaCobranzaCab();
        public EBanco banco = new EBanco();


        public frmCerrarPlanilla()
        {
            InitializeComponent();
        }

        private void frmManteAnticipo_Load(object sender, EventArgs e)
        {
            cargar();
            setValues();
        }

        private void cargar()
        {
            // BSControls.LoaderLook(lkpBancoSol, new BTesoreria().listarBancos(), "bcoc_vnombre_banco", "bcoc_icod_banco", false);
            BSControls.LoaderLook(lkpBancoDol, new BTesoreria().listarBancos(), "bcoc_vnombre_banco", "bcoc_icod_banco", true);

            int index = new BTesoreria().listarBancos().FindIndex(x => x.bcoc_icod_banco == 27);
            BSControls.LoaderLook(lkpBancoSol, new BTesoreria().listarBancos().ToList(), "bcoc_vnombre_banco", "bcoc_icod_banco", true);
            lkpBancoSol.ItemIndex = index;



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
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
            }
        }

        public void setValues()
        {
            txtNroPlanilla.Text = oBePlnCab.plnc_vnumero_planilla;
            dteFecha.EditValue = oBePlnCab.plnc_sfecha_planilla;
            dteFecha2.EditValue = dteFecha.EditValue;
            getMontos();
            enableControl();
        }

        private void getMontos()
        {
            txtMontoSol.Text = new BVentas().getTotalEfectivoPlanilla(oBePlnCab.plnc_icod_planilla).Item1.ToString();
            txtMontoDol.Text = new BVentas().getTotalEfectivoPlanilla(oBePlnCab.plnc_icod_planilla).Item2.ToString();

            txtSoles.Text = new BVentas().getTotalEfectivoPlanilla(oBePlnCab.plnc_icod_planilla).Item1.ToString();
            txtDolares.Text = new BVentas().getTotalEfectivoPlanilla(oBePlnCab.plnc_icod_planilla).Item2.ToString();
        }

        private void enableControl()
        {
            if (Convert.ToDecimal(txtSoles.Text) > 0)
            {
                lkpBancoSol.Enabled = true;
                lkpCuentaSol.Enabled = true;
                txtDolares.Enabled = false;
            }
            else
            {
                lkpBancoSol.Enabled = false;
                lkpCuentaSol.Enabled = false;
            }

            if (Convert.ToDecimal(txtDolares.Text) > 0)
            {
                lkpBancoDol.Enabled = true;
                lkpCuentaDol.Enabled = true;
                txtSoles.Enabled = false;
            }
            else
            {
                lkpBancoDol.Enabled = false;
                lkpCuentaDol.Enabled = false;
            }
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }
        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            ELibroBancos oBeBcoMovCabSol = null;
            ELibroBancos oBeBcoMovCabDol = null;

            try
            {
                if (Convert.ToDecimal(txtSoles.Text) + Convert.ToDecimal(txtMontoDol.Text) == 0)
                    throw new ArgumentException("La planilla no puede ser cerrada, no existe montos en efectivo");

                if (lkpBancoSol.Enabled)
                {
                    if (Convert.ToInt32(lkpBancoSol.EditValue) == 0)
                    {
                        oBase = lkpBancoSol;
                        throw new ArgumentException("Seleccione el banco y cuenta bancaria de destino");
                    }

                    if (Convert.ToInt32(lkpCuentaSol.EditValue) == 0)
                    {
                        oBase = lkpCuentaSol;
                        throw new ArgumentException("Seleccione la cuenta bancaria de destino");
                    }
                }

                if (lkpBancoDol.Enabled)
                {
                    if (Convert.ToInt32(lkpBancoDol.EditValue) == 0)
                    {
                        oBase = lkpBancoDol;
                        throw new ArgumentException("Seleccione el banco y cuenta bancaria de destino");
                    }

                    if (Convert.ToInt32(lkpCuentaDol.EditValue) == 0)
                    {
                        oBase = lkpCuentaDol;
                        throw new ArgumentException("Seleccione la cuenta bancaria de destino");
                    }
                }

                if (XtraMessageBox.Show("¿Esta seguro de seguir con el cierre de planilla?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                #region depósito en soles
                if (Convert.ToDecimal(txtSoles.Text) > 0)
                {
                    oBeBcoMovCabSol = new ELibroBancos();
                    oBeBcoMovCabSol.iid_anio = Parametros.intEjercicio;
                    oBeBcoMovCabSol.iid_mes = oBePlnCab.plnc_sfecha_planilla.Month;
                    oBeBcoMovCabSol.dfecha_movimiento = oBePlnCab.plnc_sfecha_planilla;
                    oBeBcoMovCabSol.icod_enti_financiera_cuenta = Convert.ToInt32(lkpCuentaSol.EditValue);
                    oBeBcoMovCabSol.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                    oBeBcoMovCabSol.vglosa = oBePlnCab.plnc_vobservaciones;
                    //oBeBcoMovCabSol.vdescripcion_beneficiario =banco.bcoc_vnombre_banco;
                    oBeBcoMovCabSol.vdescripcion_beneficiario = "Caja Ventas";
                    oBeBcoMovCabSol.iid_tipo_moneda = Parametros.intTipoMonedaSoles;
                    //oBeBcoMovCab.cliec_icod_cliente = oBeAntc.antc_icod_cliente;
                    oBeBcoMovCabSol.nmonto_tipo_cambio = new BContabilidad().getTipoCambioPorFecha(oBePlnCab.plnc_sfecha_planilla);
                    //oBeBcoMovCabSol.nmonto_movimiento = Convert.ToDecimal(txtMontoSol.Text);
                    oBeBcoMovCabSol.nmonto_movimiento = Convert.ToDecimal(txtSoles.Text);
                    oBeBcoMovCabSol.nmonto_movimiento_dolares = 0;
                    oBeBcoMovCabSol.dfecha_movimiento = Convert.ToDateTime(dteFecha2.EditValue);
                    oBeBcoMovCabSol.nmonto_saldo_banco = 0;
                    oBeBcoMovCabSol.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                    oBeBcoMovCabSol.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                    oBeBcoMovCabSol.vnro_documento = oBePlnCab.plnc_vnumero_planilla;
                    oBeBcoMovCabSol.cflag_conciliacion = false;
                    oBeBcoMovCabSol.iusuario_crea = Valores.intUsuario;
                    oBeBcoMovCabSol.vpc_crea = WindowsIdentity.GetCurrent().Name;
                    oBeBcoMovCabSol.iid_motivo_mov_banco = Parametros.intMotivoVarios;
                    oBeBcoMovCabSol.mobac_flag_estado = true;
                    oBeBcoMovCabSol.TipoDocumento = "PVD";
                    oBeBcoMovCabSol.mobac_origen_regitro = "PLN";
                    //oBeAntc.antc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab); 


                }
                #endregion
                #region depósito en dólares
                if (Convert.ToDecimal(txtDolares.Text) > 0)
                {
                    oBeBcoMovCabDol = new ELibroBancos();
                    oBeBcoMovCabDol.iid_anio = Parametros.intEjercicio;
                    oBeBcoMovCabDol.iid_mes = oBePlnCab.plnc_sfecha_planilla.Month;
                    oBeBcoMovCabDol.dfecha_movimiento = oBePlnCab.plnc_sfecha_planilla;
                    oBeBcoMovCabDol.icod_enti_financiera_cuenta = Convert.ToInt32(lkpCuentaDol.EditValue);
                    oBeBcoMovCabDol.ii_tipo_doc = Parametros.intTipoDocPlanillaVenta;
                    oBeBcoMovCabDol.vglosa = oBePlnCab.plnc_vobservaciones;
                    //oBeBcoMovCabDol.vdescripcion_beneficiario = banco.bcoc_vnombre_banco;
                    oBeBcoMovCabDol.vdescripcion_beneficiario = "Caja Venta";
                    oBeBcoMovCabDol.iid_tipo_moneda = Parametros.intTipoMonedaDolares;
                    //oBeBcoMovCab.cliec_icod_cliente = oBeAntc.antc_icod_cliente;
                    oBeBcoMovCabDol.nmonto_tipo_cambio = new BContabilidad().getTipoCambioPorFecha(oBePlnCab.plnc_sfecha_planilla);
                    //oBeBcoMovCabDol.nmonto_movimiento = Convert.ToDecimal(txtMontoDol.Text);
                    oBeBcoMovCabDol.nmonto_movimiento_dolares = Convert.ToDecimal(txtDolares.Text);
                    oBeBcoMovCabDol.nmonto_movimiento = 0;
                    oBeBcoMovCabDol.dfecha_movimiento = Convert.ToDateTime(dteFecha2.EditValue);
                    oBeBcoMovCabDol.nmonto_saldo_banco = 0;
                    oBeBcoMovCabDol.iid_situacion_movimiento_banco = Parametros.intSitLibroBancosRegistrado;
                    oBeBcoMovCabDol.cflag_tipo_movimiento = Parametros.intTipoMovimientoAbono;
                    oBeBcoMovCabDol.vnro_documento = oBePlnCab.plnc_vnumero_planilla;
                    oBeBcoMovCabDol.cflag_conciliacion = false;
                    oBeBcoMovCabDol.iusuario_crea = Valores.intUsuario;
                    oBeBcoMovCabDol.vpc_crea = WindowsIdentity.GetCurrent().Name;
                    oBeBcoMovCabDol.iid_motivo_mov_banco = Parametros.intMotivoVarios;
                    oBeBcoMovCabDol.mobac_flag_estado = true;
                    oBeBcoMovCabDol.TipoDocumento = "PVD";
                    oBeBcoMovCabDol.mobac_origen_regitro = "PLN";
                    //oBeAntc.antc_icod_entidad_finan_mov = objTesoreriaData.InsertarMovimientoBancos(oBeBcoMovCab);   

                }
                #endregion
                new BVentas().cerrarPlanilla(oBeBcoMovCabSol, oBeBcoMovCabDol, oBePlnCab.plnc_icod_planilla);
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    MiEvento(oBePlnCab.plnc_icod_planilla);
                    Close();
                }
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void lkpBancoSol_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpCuentaSol, new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBancoSol.EditValue)).Where(x => x.tablc_iid_tipo_moneda == 3).ToList(), "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);
        }

        private void lkpBancoDol_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpCuentaDol, new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBancoDol.EditValue)).Where(x => x.tablc_iid_tipo_moneda == 4).ToList(), "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);
        }


    }
}