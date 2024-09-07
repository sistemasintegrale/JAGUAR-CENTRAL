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

namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    public partial class FrmManteLiquidacionCaja : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteLiquidacionCaja));
        public int Selected_Mes;
        List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        public ELiquidacionCaja oBe = new ELiquidacionCaja();
        public List<ELiquidacionCaja> lstLiquidacionCaja = new List<ELiquidacionCaja>();
        private List<ELiquidacionCajaDet> lstDet = new List<ELiquidacionCajaDet>();
        private List<ELiquidacionCajaDet> lstDelete = new List<ELiquidacionCajaDet>();
        private List<ECajaChica> lstCajaChica = new List<ECajaChica>();

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public int caja_iid_cuenta = 0;
        public int? caja_tipo_analitica = 0;
        public int? caja_icod_analitica = 0;
        public FrmManteLiquidacionCaja()
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
            if (Status == BSMaintenanceStatus.View)
            {
                bteNroCaja.Enabled = false;
                txtNroLiquidacion.Enabled = false;
                dtmFecha.Enabled = false;
                txtConcepto.Enabled = false;
                txtMonto.Enabled = false;
                mnuLiquidaCaja.Enabled = false;
                btnGuardar.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteNroCaja.Enabled = false;
                txtNroLiquidacion.Enabled = false;
                dtmFecha.Enabled = false;
            }
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            CheckFecha();
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }
        private void FrmManteLiquidacionCaja_Load(object sender, EventArgs e)
        {
            Cargar();
        }
        private void bteNroCaja_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCaja();
        }

        private void bteNroCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                ListarCaja();
            }
        }

        private void dtmFecha_EditValueChanged(object sender, EventArgs e)
        {
            GetTipoCambio();
        }
        private void Cargar()
        {
            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();
            lstCajaChica = new BTesoreria().ListarCajaChica();
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            lstDet = new BTesoreria().ListarLiquidacionCajaDetalle(oBe.lqcc_icod_liquid_cja);
            grdDetalle.DataSource = lstDet;
        }
        public void CargarModify()
        {
            bteNroCaja.Tag = oBe.lqcc_icod_caja_liquida;
            bteNroCaja.Text = oBe.caja_nro;
            txtNroLiquidacion.Text = oBe.lqcc_inro_liquid_caja.ToString();
            dtmFecha.EditValue = oBe.lqcc_sfecha_liquid;
            txtConcepto.Text = oBe.lqcc_vconcepto;
            lkpMoneda.EditValue = oBe.lqcc_iid_tipo_moneda;
            txtMonto.Text = oBe.lqcc_nmonto_total.ToString();
            txtTipoDeCambio.Text = oBe.lqcc_ntipo_cambio.ToString();
        }
        private void ListarCaja()
        {
            using (FrmListarCajaTesoreria frm = new FrmListarCajaTesoreria())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteNroCaja.Tag = frm._Be.icod_caja_liquida;
                    bteNroCaja.Text = frm._Be.vnro_caja_liquida;
                    lkpMoneda.EditValue = frm._Be.iid_tipo_moneda;
                    lblMonto.Text = lblMonto.Text + " " + frm._Be.Moneda;
                    txtConcepto.Text = frm._Be.vdescrip_caja_liquida;
                    caja_iid_cuenta = frm._Be.iid_cuenta_contable;
                    caja_tipo_analitica = frm._Be.tblc_tipo_analitica;
                    caja_icod_analitica = frm._Be.icod_analitica;
                    GetLiquidacionCorrelativo();
                }
            }
        }
        private void GetLiquidacionCorrelativo()
        {
            int correlativo = new BTesoreria().GetCorrelativoLiquidacion(Parametros.intEjercicio, Convert.ToInt32(bteNroCaja.Tag));
            txtNroLiquidacion.Text = string.Format("{0:0000}", correlativo);
        }
        private void VerifyLiquidacionCorrelativo()
        {
        }
        private void GetTipoCambio()
        {
            if (Convert.ToDateTime(dtmFecha.EditValue).Month == Selected_Mes && Convert.ToDateTime(dtmFecha.EditValue).Year == Parametros.intEjercicio)
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
                XtraMessageBox.Show("La fecha seleccionada no está dentro del periodo y/o ejercicio de trabajo", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CheckFecha();
                dtmFecha.Focus();
            }
        }
        private void CheckFecha()
        {
            if (Selected_Mes == DateTime.Now.Month && Parametros.intEjercicio == DateTime.Now.Year)
                dtmFecha.EditValue = DateTime.Now;
            else
                dtmFecha.EditValue =
                    DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(Selected_Mes - 1);
        }

        private void txtMonto_EditValueChanged(object sender, EventArgs e)
        {

            getTotalDetalle();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prenuevo(1);
        }

        private void pagoDeProvisiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prenuevo(2);
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }


        private void nuevo()
        {
            using (FrmManteLiquidacionCajaDet frm = new FrmManteLiquidacionCajaDet())
            {
                frm.dtmFecha.EditValue = dtmFecha.EditValue;
                frm.oDetail = lstDet;
                frm.mto_cab_restante = Convert.ToDecimal(txtResta.Text);
                frm.tipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                frm.SetInsert();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDet = frm.oDetail;
                    viewDetalle.RefreshData();
                    getTotalDetalle();
                }
            }
        }
        private void prenuevo(int opc)
        {
            BaseEdit oBase = null;
            bool Flag = true;
            try
            {
                if (bteNroCaja.Tag == null)
                {
                    oBase = bteNroCaja;
                    throw new ArgumentException("Seleccione una caja válida");
                }
                if (string.IsNullOrEmpty(txtConcepto.Text))
                {
                    oBase = txtConcepto;
                    throw new ArgumentException("Ingrese concepto");
                }
                if (dtmFecha.EditValue == null)
                {
                    oBase = dtmFecha;
                    throw new ArgumentException("Seleccione fecha");
                }
                if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                {
                    oBase = dtmFecha;
                    throw new ArgumentException("No existe tipo de cambio ,para la fecha seleccionada");
                }
                if (Convert.ToDecimal(txtMonto.Text) <= 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("Ingrese monto mayor a 0.00");
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
                Flag = false;
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (Flag)
                {
                    switch (opc)
                    {
                        case 1:
                            nuevo();
                            break;
                        case 2:
                            pagoprovision();
                            break;
                    }
                }
            }

        }
        private void pagoprovision()
        {
            using (FrmManteLiquidacionCajaDetDoc frm = new FrmManteLiquidacionCajaDetDoc())
            {
                frm.dtmFecha.EditValue = dtmFecha.EditValue;
                frm.nro_caja_cab = bteNroCaja.Text;
                frm.nro_liquidacion = txtNroLiquidacion.Text;
                frm.mto_cab_restante = Convert.ToDecimal(txtResta.Text);
                frm.TipoMoneda1 = Convert.ToInt32(lkpMoneda.EditValue);
                frm.oDetail = lstDet;
                frm.tipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                frm.SetInsert();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDet = frm.oDetail;
                    viewDetalle.RefreshData();
                    getTotalDetalle();
                }
            }
        }
        private void modificar()
        {
            ELiquidacionCajaDet Obe = (ELiquidacionCajaDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (Obe == null)
                return;

            if (Obe.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaCContable)
                modificarCuenta(Obe);
            else if (Obe.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaPagoProvision)
                modificarDocumento(Obe);
            else if (Obe.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaGenProvision)
                modificarGenProvision(Obe);

        }
        private void modificarCuenta(ELiquidacionCajaDet Obe)
        {
            using (FrmManteLiquidacionCajaDet frm = new FrmManteLiquidacionCajaDet())
            {
                frm.oBe = Obe;
                frm.mto_cab_restante = Convert.ToDecimal(txtResta.Text);
                frm.dtmFecha.EditValue = dtmFecha.EditValue;
                frm.oDetail = lstDet;
                frm.SetModify();
                frm.CargarModify();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDet = frm.oDetail;
                    viewDetalle.RefreshData();
                    getTotalDetalle();
                }
            }
        }
        private void modificarDocumento(ELiquidacionCajaDet Obe)
        {
            using (FrmManteLiquidacionCajaDetDoc frm = new FrmManteLiquidacionCajaDetDoc())
            {
                frm.oBe = Obe;
                frm.nro_caja_cab = bteNroCaja.Text;
                frm.nro_liquidacion = txtNroLiquidacion.Text;
                frm.mto_cab_restante = Convert.ToDecimal(txtResta.Text);
                frm.TipoMoneda1 = Convert.ToInt32(lkpMoneda.EditValue);
                frm.dtmFecha.EditValue = dtmFecha.EditValue;
                frm.oDetail = lstDet;
                frm.SetModify();
                frm.setValues();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDet = frm.oDetail;
                    viewDetalle.RefreshData();
                    getTotalDetalle();
                }
            }
        }

        private void modificarGenProvision(ELiquidacionCajaDet Obe)
        {
            using (FrmManteLiquidacionCajaDetGeneDoc frm = new FrmManteLiquidacionCajaDetGeneDoc())
            {
                frm.oBe = Obe;
                frm.nro_caja_cab = bteNroCaja.Text;
                frm.nro_liquidacion = txtNroLiquidacion.Text;
                frm.mto_cab_restante = Convert.ToDecimal(txtResta.Text);

                frm.dtmFecha.EditValue = dtmFecha.EditValue;
                frm.oDetail = lstDet;
                frm.setValues();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDet = frm.oDetail;
                    viewDetalle.RefreshData();
                    getTotalDetalle();
                }
            }
        }
        private void eliminar()
        {
            ELiquidacionCajaDet Obe = (ELiquidacionCajaDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (Obe != null)
            {
                Obe.operacion = 3;
                lstDelete.Add(Obe);
                renumerar(lstDet.FindIndex(x => x == Obe));
                lstDet.Remove(Obe);
                viewDetalle.RefreshData();
                getTotalDetalle();
            }
        }
        private void renumerar(int posicion)
        {
            if (lstDet.Count > posicion + 1)
            {
                for (int i = posicion + 1; i < lstDet.Count; i++)
                {
                    lstDet[i].lqcd_inro_item = lstDet[i].lqcd_inro_item - 1;
                    if (lstDet[i].operacion == null)
                    {
                        lstDet[i].operacion = 2;
                    }
                }
            }
        }
        private void getTotalDetalle()
        {
            txtTotal.Text = lstDet.Sum(x => x.lqcd_nmonto_pago).ToString();
            txtResta.Text = (Convert.ToDecimal(txtMonto.Text) - lstDet.Sum(x => x.lqcd_nmonto_pago)).ToString();
        }

        private int getSituacion()
        {
            int situacion = 0;
            if (lstDet.Count == 0)
                situacion = 1;
            else
            {
                decimal SumDet = lstDet.Sum(x => x.lqcd_nmonto_pago);
                if (SumDet == Convert.ToDecimal(txtMonto.Text))
                    situacion = 3;
                else if (SumDet < Convert.ToDecimal(txtMonto.Text))
                    situacion = 2;
            }
            return situacion;
        }
        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            BTesoreria obE = new BTesoreria();
            try
            {
                if (bteNroCaja.Tag == null)
                {
                    oBase = bteNroCaja;
                    throw new ArgumentException("Seleccione caja");
                }
                if (string.IsNullOrEmpty(txtNroLiquidacion.Text))
                {
                    oBase = txtNroLiquidacion;
                    throw new ArgumentException("Ingrese N° de liquidación");
                }
                if (string.IsNullOrEmpty(txtConcepto.Text))
                {
                    oBase = txtConcepto;
                    throw new ArgumentException("Ingrese concepto");
                }
                if (dtmFecha.EditValue == null)
                {
                    oBase = dtmFecha;
                    throw new ArgumentException("Seleccione fecha");
                }
                if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                {
                    oBase = dtmFecha;
                    throw new ArgumentException("No existe tipo de cambio ,para la fecha seleccionada");
                }
                if (Convert.ToDecimal(txtMonto.Text) <= 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("Ingrese monto mayor a 0.00");
                }

                oBe.lqcc_iid_anio = Parametros.intEjercicio;
                oBe.lqcc_iid_mes = Selected_Mes;
                oBe.lqcc_icod_caja_liquida = Convert.ToInt32(bteNroCaja.Tag);
                oBe.caja_nro = bteNroCaja.Text;
                oBe.lqcc_inro_liquid_caja = Convert.ToInt32(txtNroLiquidacion.Text);
                oBe.lqcc_sfecha_liquid = Convert.ToDateTime(dtmFecha.EditValue);
                oBe.lqcc_vconcepto = txtConcepto.Text;
                oBe.lqcc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.lqcc_nmonto_total = Convert.ToDecimal(txtMonto.Text);
                oBe.lqcc_nmonto_detalle = (lstDet.Count > 0) ? lstDet.Sum(x => x.lqcd_nmonto_pago) : 0;
                oBe.lqcc_iid_situacion_liq = getSituacion();
                oBe.lqcc_ntipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                oBe.lqcc_flag_estado = 1;
                if (Status == BSMaintenanceStatus.CreateNew)
                {

                    oBe.caja_iid_cuenta_contable = caja_iid_cuenta;
                    oBe.caja_tipo_analitica = caja_tipo_analitica;
                    oBe.caja_icod_analitica = caja_icod_analitica;
                    oBe.intUsuario = Valores.intUsuario;
                    oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                    obE.insertarLiquidacionCaja(oBe, lstDet);
                }
                else
                {
                    oBe.intUsuario = Valores.intUsuario;
                    oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                    obE.modificarLiquidacionCaja(oBe, lstDet, lstDelete);
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

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void bteNroCaja_KeyUp(object sender, KeyEventArgs e)
        {
            var GetCaja = lstCajaChica.Where(x => Convert.ToInt32(x.vnro_caja_liquida) == Convert.ToInt32(bteNroCaja.Text)).ToList();

            if (GetCaja.Count == 1)
            {

                bteNroCaja.Tag = GetCaja[0].icod_caja_liquida;
                bteNroCaja.Text = GetCaja[0].vnro_caja_liquida;
                lkpMoneda.EditValue = GetCaja[0].iid_tipo_moneda;
                lblMonto.Text = lblMonto.Text + " " + GetCaja[0].Moneda;
                txtConcepto.Text = GetCaja[0].vdescrip_caja_liquida;
                caja_iid_cuenta = GetCaja[0].iid_cuenta_contable;
                caja_tipo_analitica = GetCaja[0].tblc_tipo_analitica;
                caja_icod_analitica = GetCaja[0].icod_analitica;
                GetLiquidacionCorrelativo();
            }
            else
            {
                bteNroCaja.Tag = null;
                bteNroCaja.Text = string.Empty;
                lkpMoneda.EditValue = null;
                lblMonto.Text = "Monto";
                txtConcepto.Text = string.Empty;
            }
        }

        private void genProvisiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmManteLiquidacionCajaDetGeneDoc frm = new FrmManteLiquidacionCajaDetGeneDoc())
            {
                frm.dtmFecha.EditValue = dtmFecha.EditValue;
                frm.nro_caja_cab = bteNroCaja.Text;
                frm.nro_liquidacion = txtNroLiquidacion.Text;
                frm.mto_cab_restante = Convert.ToDecimal(txtResta.Text);
                frm.oDetail = lstDet;
                frm.tipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                frm.SetInsert();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDet = frm.oDetail;
                    viewDetalle.RefreshData();
                    getTotalDetalle();
                }
            }
        }

        private void grdDetalle_Click(object sender, EventArgs e)
        {

        }
    }
}