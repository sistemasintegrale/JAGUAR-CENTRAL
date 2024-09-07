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

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmManteLetraPorCobrar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteLetraPorCobrar));

        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        /*************************************************************************/
        public ELetraPorCobrar oBe = new ELetraPorCobrar();
        public int intPeriodo = 0;
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        /*************************************************************************/

        List<ELetraPorCobrarDet> lstDetalle = new List<ELetraPorCobrarDet>();
        List<ELetraPorCobrarDet> lstDelete = new List<ELetraPorCobrarDet>();

        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        public FrmManteLetraPorCobrar()
        {
            InitializeComponent();
        }

        private void FrmManteCuentasPorCobrar_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void Carga()
        {
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                CheckFecha();
                dteFVencimiento.EditValue = dteFecha.EditValue;
            }
            else
                lstDetalle = new BCuentasPorCobrar().listarLetraPorCobrarDet(oBe.lexcc_icod_correlativo);
            grdDetalle.DataSource = lstDetalle;
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtConcepto.Enabled = !Enabled;
            txtAval.Enabled = !Enabled;
            txtMonto.Enabled = !Enabled;
            txtNroLetra.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;
            dteFVencimiento.Enabled = !Enabled;
            txtTipoCambio.Enabled = !Enabled;
            bteCliente.Enabled = !Enabled;
            lkpMoneda.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtNroLetra.Enabled = Enabled;
                bteCliente.Enabled = Enabled;
                lkpMoneda.Enabled = Enabled;
                txtMonto.Enabled = !Enabled;
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
            txtNroLetra.Text = oBe.lexcc_vnumero_letra;
            bteCliente.Tag = oBe.cliec_icod_cliente;
            bteCliente.Text = oBe.strDesCliente;
            txtAval.Text = oBe.lexcc_vaval;
            txtConcepto.Text = oBe.lexcc_vobservaciones;
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            txtTipoCambio.Text = oBe.lexcc_nmonto_tipo_cambio.ToString();
            txtMonto.Text = oBe.lexcc_nmonto_total.ToString();
            txtPagado.Text = oBe.lexcc_nmonto_pagado.ToString();
            dteFecha.EditValue = oBe.lexcc_sfecha_letra;
            dteFVencimiento.EditValue = oBe.lexcc_sfecha_vencimiento;
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
            using (FrmListarCliente frm = new FrmListarCliente())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                    bteCliente.Tag = frm._Be.cliec_icod_cliente;
                    oBe.cliec_icod_cliente = frm._Be.cliec_icod_cliente;
                }
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
                if (Convert.ToInt32(bteCliente.Tag) == 0)
                {
                    oBase = bteCliente;
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
                oBe.lexcc_vnumero_letra = txtNroLetra.Text;
                oBe.lexcc_icorrelativo = Convert.ToInt32(txtNroLetra.Text.Substring(3, 4));
                oBe.anioc_iid_anio = Parametros.intEjercicio;
                oBe.mesec_iid_mes = Convert.ToDateTime(dteFecha.EditValue).Month;
                oBe.cliec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.lexcc_vaval = txtAval.Text;
                oBe.lexcc_vobservaciones = txtConcepto.Text;
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.lexcc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                oBe.lexcc_nmonto_total = Convert.ToDecimal(txtMonto.Text);
                oBe.lexcc_nmonto_pagado = Convert.ToDecimal(txtPagado.Text);
                oBe.lexcc_sfecha_letra = Convert.ToDateTime(dteFecha.EditValue);
                oBe.lexcc_sfecha_vencimiento = Convert.ToDateTime(dteFVencimiento.EditValue);
                oBe.strDesCliente = bteCliente.Text;

                if (lstDetalle.Count > 0)
                {
                    if (oBe.lexcc_nmonto_total == oBe.lexcc_nmonto_pagado)
                        oBe.tablc_iid_situacion_letra = 3; //Cubierto
                    else
                        oBe.tablc_iid_situacion_letra = 2; //parcialmetne cubierto
                }
                else
                    oBe.tablc_iid_situacion_letra = 1;

                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.lexcc_icod_correlativo = new BCuentasPorCobrar().insertarLetraPorCobrar(oBe, lstDetalle);
                }
                else
                {
                    new BCuentasPorCobrar().modificarLetraPorCobrar(oBe, lstDetalle, lstDelete);
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
                    this.MiEvento(oBe.lexcc_icod_correlativo);
                    this.Close();
                }
            }

        }




        public void calcular()
        {
            if (lstDetalle.Count > 0)
                txtPagado.Text = lstDetalle.Sum(x => x.lxcpc_nmonto_pago).ToString();
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
                listar_cliente();
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

                FrmManteLetraPorCobrarDet frm = new FrmManteLetraPorCobrarDet();
                frm.lstDetalle = lstDetalle;
                frm.SetInsert();
                oBe.lexcc_sfecha_letra = Convert.ToDateTime(dteFecha.EditValue);
                oBe.lexcc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
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
            ELetraPorCobrarDet obe = (ELetraPorCobrarDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                FrmManteLetraPorCobrarDet frm = new FrmManteLetraPorCobrarDet();
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
            ELetraPorCobrarDet obe = (ELetraPorCobrarDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
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