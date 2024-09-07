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
    public partial class frmManteAnticipo : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod, EPlanillaCobranzaCab oBePlnCabReload);
        public event DelegadoMensaje MiEvento;
        public EAnticipo oBe = new EAnticipo();
        public EPlanillaCobranzaCab oBePlnCab = new EPlanillaCobranzaCab();
        public EPlanillaCobranzaDet oBePlnDet = new EPlanillaCobranzaDet();

        public frmManteAnticipo()
        {
            InitializeComponent();
        }

        private void frmManteAnticipo_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            BSControls.LoaderLook(lkpTipoPago, new BGeneral().listarTablaRegistro(41).Where(x => x.tarec_icorrelativo_registro == 1 || x.tarec_icorrelativo_registro == 2).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpTipoMoneda, new BGeneral().listarTablaRegistro(5).Where(x => x.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpTipoTarjeta, new BGeneral().listarTablaRegistro(40), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
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

            txtNroAnticipo.Enabled = !Enabled;

            bteCliente.Enabled = !Enabled;
            txtObservaciones.Enabled = !Enabled;
            lkpTipoMoneda.Enabled = !Enabled;
            txtMonto.Enabled = !Enabled;
            lkpTipoPago.Enabled = !Enabled;
            lkpTipoTarjeta.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtNroAnticipo.Enabled = Enabled;
                //dteFecha.Enabled = Enabled;
                bteCliente.Enabled = Enabled;
                lkpTipoMoneda.Enabled = Enabled;
            }
            //if(Status == BSMaintenanceStatus.View && DateTime.Now.ToShortDateString() == "27-03-2014")            
            //    btnGuardar.Enabled = !Enabled;

        }

        public void setValues()
        {
            txtNroAnticipo.Text = oBePlnDet.strAnticipo;
            dteFecha.EditValue = oBePlnDet.plnd_sfecha_doc;
            bteCliente.Tag = oBePlnDet.intCliente;
            bteCliente.Text = oBePlnDet.strCliente;
            txtObservaciones.Text = oBePlnDet.strPagoDescripcion;
            lkpTipoMoneda.EditValue = oBePlnDet.tablc_iid_tipo_moneda;
            lkpTipoPago.EditValue = oBePlnDet.intTipoPago;
            lkpTipoTarjeta.EditValue = oBePlnDet.intTipoTarjeta;
            txtMonto.Text = oBePlnDet.plnd_nmonto_pagado.ToString();
            oBe.antc_icod_adelanto_cliente = Convert.ToInt32(oBePlnDet.plnd_icod_documento);
            oBe.antc_icod_anticipo = Convert.ToInt32(oBePlnDet.antc_icod_anticipo);
            oBe.antc_icod_entidad_finan_mov = oBePlnDet.intIcodEntidadFinanMov;
            oBe.antc_icod_dxc_adelanto = Convert.ToInt64(oBePlnDet.intIcodDxc);

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
            try
            {
                if (Convert.ToInt32(bteCliente.Tag) == 0)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccione un cliente");
                }

                if (Convert.ToDecimal(txtMonto.Text) <= 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("El monto del anticipo debe se mayor a 0.00");
                }

                if (Convert.ToDateTime(dteFecha.EditValue).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha ingresada esta fuera del año de ejercicio");
                }

                #region Planilla Det
                int? intNull = null;
                oBePlnDet.plnc_icod_planilla = oBePlnCab.plnc_icod_planilla;
                oBePlnDet.tablc_iid_tipo_mov = 3;//ANTICIPO
                oBePlnDet.plnd_sfecha_doc = Convert.ToDateTime(dteFecha.EditValue);
                oBePlnDet.plnd_icod_tipo_doc = 83;//83 es id de ADELANTO CLIENTE
                //oBePlnDet.plnd_icod_documento = 0;//pendiente
                //oBePlnDet.plnd_vnumero_doc = "";//pendiente
                //oBePlnDet.plnd_nmonto = 0;//Convert.ToDecimal(txtMonto.Text);
                oBePlnDet.plnd_nmonto_pagado = Convert.ToDecimal(txtMonto.Text);
                oBePlnDet.tablc_iid_tipo_moneda = Convert.ToInt32(lkpTipoMoneda.EditValue);
                oBePlnDet.strCliente = bteCliente.Text;
                oBePlnDet.intUsuario = Valores.intUsuario;
                oBePlnDet.strPc = WindowsIdentity.GetCurrent().Name;
                #endregion
                #region Anticipo
                oBe.antc_vnumero_anticipo = txtNroAnticipo.Text;
                oBe.antc_sfecha_anticipo = Convert.ToDateTime(dteFecha.EditValue);
                oBe.antc_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.antc_observaciones = txtObservaciones.Text;
                oBe.antc_nmonto_anticipo = Convert.ToDecimal(txtMonto.Text);
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpTipoMoneda.EditValue);
                oBe.tablc_iid_situacion_anticipo = 1;//pendiente
                oBe.tablc_iid_tipo_pago = Convert.ToInt32(lkpTipoPago.EditValue);
                oBe.tablc_iid_tipo_tarjeta = (Convert.ToInt32(lkpTipoTarjeta.EditValue) == 0) ? intNull : Convert.ToInt32(lkpTipoTarjeta.EditValue);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                #endregion

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBePlnDet.plnd_icod_detalle = new BVentas().insertarAnticipoPln(oBePlnCab, oBePlnDet, oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarAnticipoPln(oBePlnCab, oBePlnDet, oBe);
                }
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
                    MiEvento(oBePlnDet.plnd_icod_detalle, oBePlnCab);
                    Close();
                }
            }
        }
        private void listarCliente()
        {
            try
            {
                using (FrmListarCliente frm = new FrmListarCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteCliente.Tag = frm._Be.cliec_icod_cliente;
                        bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void lkpTipoPago_EditValueChanged(object sender, EventArgs e)
        {
            if (Status != BSMaintenanceStatus.View)
            {
                if (Convert.ToInt32(lkpTipoPago.EditValue) == 1)
                {
                    lkpTipoTarjeta.Enabled = false;
                    lkpTipoTarjeta.EditValue = null;
                }
                else if (Convert.ToInt32(lkpTipoPago.EditValue) == 2)
                {
                    lkpTipoTarjeta.Enabled = true;
                    lkpTipoTarjeta.EditValue = 1;
                }
            }

        }
    }
}