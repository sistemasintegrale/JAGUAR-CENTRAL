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

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmManteLiquidacionGastos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteLiquidacionGastos));

        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        /*************************************************************************/
        public ELiquidacionGastos oBe = new ELiquidacionGastos();
        public int intPeriodo = 0;
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        /*************************************************************************/

        List<ELiquidacionGastosDet> lstDetalle = new List<ELiquidacionGastosDet>();
        List<ELiquidacionGastosDet> lstDelete = new List<ELiquidacionGastosDet>();

        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        public FrmManteLiquidacionGastos()
        {
            InitializeComponent();
        }

        private void FrmManteCuentasPorCobrar_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void Carga()
        {
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5).Where(x => x.tarec_icorrelativo_registro != 3).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                CheckFecha();
                dteFVencimiento.EditValue = dteFecha.EditValue;
            }
            else
                lstDetalle = new BCuentasPorPagar().listarLiquidacionGastosDet(oBe.lqgc_icod_correlativo);
            grdDetalle.DataSource = lstDetalle;
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtConcepto.Enabled = !Enabled;
            txtMonto.Enabled = !Enabled;
            txtNroLG.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;
            dteFVencimiento.Enabled = !Enabled;
            txtTipoCambio.Enabled = !Enabled;
            bteProveedor.Enabled = !Enabled;
            lkpMoneda.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtNroLG.Enabled = Enabled;
                bteProveedor.Enabled = Enabled;
                lkpMoneda.Enabled = Enabled;
                txtMonto.Enabled = Enabled;
                dteFecha.Enabled = Enabled;
                dteFVencimiento.Enabled = Enabled;
                txtTipoCambio.Enabled = Enabled;
            }
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
            txtNroLG.Text = oBe.lqgc_vnumero_liq_gasto;
            bteProveedor.Tag = oBe.proc_icod_proveedor;
            bteProveedor.Text = oBe.strDesProveedor;
            txtConcepto.Text = oBe.lqgc_vconcepto;
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            txtTipoCambio.Text = oBe.lqgc_nmonto_tipo_cambio.ToString();
            txtMonto.Text = oBe.lqgc_nmonto_total.ToString();
            txtPagado.Text = oBe.lqgc_nmonto_pagado.ToString();
            dteFecha.EditValue = oBe.lqgc_sfecha_liq_gasto;
            dteFVencimiento.EditValue = oBe.lqgc_sfecha_vencimiento;
        }

        private void btnCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                ListarProveedor();
            }
        }

        private void ListarProveedor()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                bteProveedor.Tag = Proveedor._Be.iid_icod_proveedor;
                bteProveedor.Text = Proveedor._Be.vnombrecompleto;
                oBe.proc_icod_proveedor = Proveedor._Be.iid_icod_proveedor;
            }
        }


        private void dtmFecha_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                if (Convert.ToDateTime(dteFecha.EditValue).Month == intPeriodo)
                {
                    var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dteFecha.EditValue).ToShortDateString()).ToList();
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
                    CheckFecha();
                    dteFecha.Focus();
                }
            }
        }
        private void CheckFecha()
        {
            if (intPeriodo == DateTime.Now.Month && Parametros.intEjercicio == DateTime.Now.Year)
                dteFecha.EditValue = DateTime.Now;
            else
                dteFecha.EditValue =
                    DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(intPeriodo - 1);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            this.SetSave();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetSave()
        {

            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (Convert.ToInt32(bteProveedor.Tag) == 0)
                {
                    oBase = bteProveedor;
                    throw new ArgumentException("Seleccione cliente");
                }

                if (Convert.ToDecimal(txtTipoCambio.Text) == 0)
                {
                    oBase = txtTipoCambio;
                    throw new ArgumentException("Ingrese tipo de cambio");
                }

                if (Convert.ToDecimal(txtMonto.Text) < Convert.ToDecimal(txtPagado.Text))
                {
                    oBase = txtMonto;
                    throw new ArgumentException("El monto pagado no pude se mayor al monto de la letra");
                }

                TimeSpan ts = Convert.ToDateTime(dteFVencimiento.EditValue) - Convert.ToDateTime(dteFecha.EditValue);
                if (ts.Days < 0)
                {
                    oBase = dteFVencimiento;
                    throw new ArgumentException("la fecha de vencimiento no puede ser menor a la fecha de la letra");
                }
                oBe.lqgc_vnumero_liq_gasto = txtNroLG.Text;
                oBe.lqgc_icorrelativo = Convert.ToInt32(txtNroLG.Text.Substring(2, 4));
                oBe.anioc_iid_anio = Parametros.intEjercicio;
                oBe.mesec_iid_mes = Convert.ToDateTime(dteFecha.EditValue).Month;
                oBe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                oBe.lqgc_vconcepto = txtConcepto.Text;
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.lqgc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                oBe.lqgc_nmonto_total = Convert.ToDecimal(txtMonto.Text);
                oBe.lqgc_nmonto_pagado = Convert.ToDecimal(txtPagado.Text);
                oBe.lqgc_sfecha_liq_gasto = Convert.ToDateTime(dteFecha.EditValue);
                oBe.lqgc_sfecha_vencimiento = Convert.ToDateTime(dteFVencimiento.EditValue);
                oBe.strDesProveedor = bteProveedor.Text;

                if (lstDetalle.Count > 0)
                {
                    if (oBe.lqgc_nmonto_total == oBe.lqgc_nmonto_pagado)
                        oBe.tablc_iid_sit_liq_gasto = 3; //Cubierto
                    else
                        oBe.tablc_iid_sit_liq_gasto = 2; //parcialmetne cubierto
                }
                else
                    oBe.tablc_iid_sit_liq_gasto = 1;

                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.lqgc_icod_correlativo = new BCuentasPorPagar().insertarLiquidacionGastos(oBe, lstDetalle);
                }
                else
                {
                    new BCuentasPorPagar().modificarLiquidacionGastos(oBe, lstDetalle, lstDelete);
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
                    this.MiEvento(oBe.lqgc_icod_correlativo);
                    this.Close();
                }
            }

        }




        public void calcular()
        {
            if (lstDetalle.Count > 0)
                txtPagado.Text = lstDetalle.Sum(x => x.lqgd_nmonto_pago).ToString();
            else
                txtPagado.Text = "0.00";
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
                ListarProveedor();
            }
        }
        public void generarVoucher2()
        {
            SetSave();
        }

        private void nuevo()
        {
            try
            {
                if (Convert.ToDecimal(txtTipoCambio.Text) == 0)
                {
                    throw new ArgumentException("Registrar tipo de cambio");
                }

                FrmManteLiquidacionGastosDet frm = new FrmManteLiquidacionGastosDet();
                frm.lstDetalle = lstDetalle;
                frm.SetInsert();
                oBe.lqgc_sfecha_liq_gasto = Convert.ToDateTime(dteFecha.EditValue);
                oBe.lqgc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                frm.txtConcepto.Text = txtConcepto.Text;
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                frm.oBe = oBe;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();
                    calcular();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void modificar()
        {
            ELiquidacionGastosDet obe = (ELiquidacionGastosDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                FrmManteLiquidacionGastosDet frm = new FrmManteLiquidacionGastosDet();
                frm.lstDetalle = lstDetalle;
                frm.SetModify();
                frm.oBe = oBe;
                frm.obe = obe;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();
                    calcular();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminar()
        {
            ELiquidacionGastosDet obe = (ELiquidacionGastosDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                lstDelete.Add(obe);
                lstDetalle.Remove(obe);
                viewDetalle.RefreshData();
                calcular();
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
            eliminar();
        }
    }
}