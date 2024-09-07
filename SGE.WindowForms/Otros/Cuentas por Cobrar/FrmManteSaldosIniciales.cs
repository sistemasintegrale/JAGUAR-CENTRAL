using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Administracion_del_Sistema.Listados;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmManteSaldosIniciales : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteSaldosIniciales));

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
        List<EDocXCobrarCuentaContable> ListaEliminados = new List<EDocXCobrarCuentaContable>();
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();

        private BCuentasPorCobrar Obl;
        public long icod;
        public int? situacion;

        public decimal? afecto = 0;
        public decimal? inafecto = 0;
        public decimal? servicio = 0;
        public decimal? impuesto = 0;
        public decimal? subtotal = 0;
        public decimal? total = 0;

        public FrmManteSaldosIniciales()
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
            bool Enabled = (Status == BSMaintenanceStatus.CreateNew);

            bteTipoDocumento.Enabled = Enabled;
            bteClaseDocumento.Enabled = Enabled;
            txtSerie.Properties.ReadOnly = !Enabled;
            txtNumeroDocumento.Properties.ReadOnly = !Enabled;
            bteCliente.Enabled = Enabled;
            txtTipoCambio.Properties.ReadOnly = true;

            Enabled = (Status == BSMaintenanceStatus.View);

            deFechaDocumento.Properties.ReadOnly = Enabled;
            LkpTipoMoneda.Properties.ReadOnly = Enabled;
            deFechaVencimiento.Properties.ReadOnly = Enabled;
            txtConcepto.Properties.ReadOnly = Enabled;
            txtOperacionGrabada.Properties.ReadOnly = Enabled;
            txtInafecto.Properties.ReadOnly = Enabled;

            txtSerie.Properties.ReadOnly = false;

        }

        private void FrmSaldosIniciales_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        private void FrmManteSaldosIniciales_Load(object sender, EventArgs e)
        {
            cargar();
            if (this.Status == BSMaintenanceStatus.CreateNew)
            {
                deFechaDocumento.EditValue = Convert.ToDateTime("31/12/" + (Parametros.intEjercicio - 1).ToString());
            }
        }

        public void cargar()
        {
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            var lstMoneda = new BGeneral().listarTablaRegistro(5);
            BSControls.LoaderLook(LkpTipoMoneda, lstMoneda, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
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

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EDocXCobrar oBe = new EDocXCobrar();
            Obl = new BCuentasPorCobrar();
            try
            {
                if (bteTipoDocumento.Tag == null)
                {
                    oBase = bteTipoDocumento;
                    throw new ArgumentException("Seleccionar un Tipo de Documento");
                }
                if (bteClaseDocumento.Tag == null)
                {
                    oBase = bteClaseDocumento;
                    throw new ArgumentException("Seleccionar una Clase de Documento");
                }
                if (bteCliente.Tag == null)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccionar una Cliente");
                }
                if (deFechaDocumento.EditValue == null)
                {
                    oBase = deFechaDocumento;
                    throw new ArgumentException("Seleccionar la fecha del documento");
                }
                if (Convert.ToDateTime(deFechaDocumento.Text).Year >= Parametros.intEjercicio)
                {
                    oBase = deFechaDocumento;
                    throw new ArgumentException("La fecha del documento debe ser menor al " + Parametros.intEjercicio.ToString());
                }
                if (LkpTipoMoneda.EditValue == null)
                {
                    oBase = deFechaDocumento;
                    throw new ArgumentException("Seleccionar una moneda");
                }
                if (txtTipoCambio.Text == "")
                {
                    oBase = txtTipoCambio;
                    throw new ArgumentException("Se debe registrar el tipo de cambio para la fecha seleccionada");
                }
                if (txtConcepto.Text == "")
                {
                    oBase = txtConcepto;
                    throw new ArgumentException("Ingresar el Concepto");
                }
                if (deFechaVencimiento.EditValue == null)
                {
                    oBase = deFechaVencimiento;
                    throw new ArgumentException("Seleccionar la fecha de vencimiento");
                }
                if (deFechaDocumento.DateTime > deFechaVencimiento.DateTime)
                {
                    oBase = deFechaVencimiento;
                    throw new ArgumentException("La fecha de vencimiento no debe ser menor a la fecha del documento");
                }
                if (Convert.ToDecimal(txtOperacionGrabada.Text) == 0 && Convert.ToDecimal(txtInafecto.Text) == 0)
                {
                    oBase = txtOperacionGrabada;
                    throw new ArgumentException("Ingresar a menos un monto");
                }
                if (Convert.ToDateTime(deFechaVencimiento.Text).Year > Parametros.intEjercicio)
                {
                    if (XtraMessageBox.Show("La fecha de vencimiento no corresponde al año en ejercicio, ¿Desea seguir grabando?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        oBase = null;
                        throw new ArgumentException("");
                    }
                }
                oBe.mesec_iid_mes = 0;
                oBe.tdocc_icod_tipo_doc = Convert.ToInt32(bteTipoDocumento.Tag);
                oBe.tdodc_iid_correlativo = Convert.ToInt32(bteClaseDocumento.Tag); ;
                oBe.doxcc_vnumero_doc = txtSerie.Text + txtNumeroDocumento.Text;
                oBe.cliec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.cliec_vnombre_cliente = bteCliente.Text;
                oBe.doxcc_sfecha_doc = Convert.ToDateTime(deFechaDocumento.EditValue);
                oBe.doxcc_sfecha_vencimiento_doc = Convert.ToDateTime(deFechaVencimiento.EditValue); ;
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(LkpTipoMoneda.EditValue);
                oBe.doxcc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                oBe.tablc_iid_tipo_pago = 1;//por defecto va a ser al contado
                oBe.doxcc_vdescrip_transaccion = txtConcepto.Text;
                if (Convert.ToDecimal(txtOperacionGrabada.Text) == 0)
                    oBe.doxcc_nmonto_afecto = 0;
                else
                    oBe.doxcc_nmonto_afecto = afecto;
                if (Convert.ToDecimal(txtInafecto.Text) == 0)
                    oBe.doxcc_nmonto_inafecto = 0;
                else
                    oBe.doxcc_nmonto_inafecto = inafecto;

                if (impuesto == 0)
                    oBe.doxcc_nmonto_impuesto = 0;
                else
                    oBe.doxcc_nmonto_impuesto = impuesto;
                oBe.doxcc_nmonto_total = total;
                oBe.doxcc_nmonto_saldo = total;
                oBe.doxcc_nmonto_pagado = 0;
                oBe.tablc_iid_situacion_documento = Parametros.intSitDocCobrarGenerado;
                oBe.doxcc_vobservaciones = txtConcepto.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString(); ;

                oBe.doxcc_tipo_comprobante_referencia = 0;
                oBe.doxcc_num_serie_referencia = "";
                oBe.doxcc_num_comprobante_referencia = "";
                oBe.doxcc_sfecha_emision_referencia = null;
                oBe.doxcc_nporcentaje_igv = 0;


                oBe.anio = Parametros.intEjercicio;
                oBe.doxcc_flag_estado = true;
                oBe.doxcc_origen = "2";

                //oBe.doxpc_nporcentaje_imp_renta = 0;
                //oBe.doxpc_nmonto_retencion_rh = 0;
                //oBe.doxpc_nmonto_retenido = 0;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obl.InsertarDocumentoXCobrar(oBe, Lista);
                }
                else
                {
                    oBe.doxcc_icod_correlativo = icod;
                    Obl.ActualizarDocumentoXCobrar(oBe, Lista, ListaEliminados);
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
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (ex.Message != "")
                        XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        private void bteTipoDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarTipoDocumento();
        }

        private void ListarTipoDocumento()
        {
            frmListarTipoDocumento Documentos = new frmListarTipoDocumento();
            Documentos.bModuloall = true;
            Documentos.intIdModulo = Parametros.intModuloCtasPorCobrar;
            if (Documentos.ShowDialog() == DialogResult.OK)
            {
                bteTipoDocumento.Tag = Documentos._Be.tdocc_icod_tipo_doc;
                bteTipoDocumento.Text = Documentos._Be.tdocc_vabreviatura_tipo_doc;

                bteClaseDocumento.Tag = null;
                bteClaseDocumento.Text = string.Empty;
                lblDescripcionClaseDocumento.Text = string.Empty;
            }
            bteClaseDocumento.Focus();
        }

        private void bteClaseDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarClaseDocumentos();
        }

        private void ListarClaseDocumentos()
        {
            frmListarClaseDocumento Clase = new frmListarClaseDocumento();
            Clase.intTipoDoc = Convert.ToInt32(bteTipoDocumento.Tag);
            if (Clase.ShowDialog() == DialogResult.OK)
            {
                bteClaseDocumento.Tag = Clase._Be.tdocd_iid_correlativo;
                bteClaseDocumento.Text = Clase._Be.tdocd_iid_codigo_doc_det.ToString();
                lblDescripcionClaseDocumento.Text = Clase._Be.tdocd_descripcion;
            }
            txtSerie.Focus();
        }

        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                using (FrmListarCliente frmCliente = new FrmListarCliente())
                {
                    if (frmCliente.ShowDialog() == DialogResult.OK)
                    {
                        bteCliente.Text = frmCliente._Be.cliec_vnombre_cliente;
                        bteCliente.Tag = frmCliente._Be.cliec_icod_cliente;
                    }
                }
            }
        }

        private void deFechaDocumento_EditValueChanged(object sender, EventArgs e)
        {
            txtTipoCambio.Text = "0.0000";
            txtTipoCambio.Enabled = true;
            var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(deFechaDocumento.EditValue).ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                txtTipoCambio.Enabled = false;
            });
        }

        private void txtOperacionGrabada_KeyUp(object sender, KeyEventArgs e)
        {

            Totalizar();
        }

        private void txtInafecto_KeyUp(object sender, KeyEventArgs e)
        {

            Totalizar();
        }

        private void txtServicio_KeyUp(object sender, KeyEventArgs e)
        {
            txtOperacionGrabada.Text = "0.00";
            txtInafecto.Text = "0.00";
            Totalizar();
        }

        private void txtIGV_KeyUp(object sender, KeyEventArgs e)
        {
            Totalizar();
        }

        private void Totalizar()
        {
            afecto = Convert.ToDecimal(txtOperacionGrabada.Text);
            inafecto = Convert.ToDecimal(txtInafecto.Text);

            subtotal = afecto + inafecto + servicio;
            total = afecto + inafecto + servicio + impuesto;

            lblSubTotalValor.Text = subtotal.ToString();
            lblPrecioVentaValor.Text = total.ToString();
            lblSaldoValor.Text = "0.00";
        }

        private void FrmManteSaldosIniciales_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void txtOperacionGrabada_Enter(object sender, EventArgs e)
        {
            txtOperacionGrabada.SelectAll();
        }

        private void txtInafecto_Enter(object sender, EventArgs e)
        {
            txtInafecto.SelectAll();
        }

        private void bteCliente_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCliente();
        }

        private void ListarCliente()
        {
            using (FrmListarCliente frmCliente = new FrmListarCliente())
            {
                if (frmCliente.ShowDialog() == DialogResult.OK)
                {
                    bteCliente.Text = frmCliente._Be.cliec_vnombre_cliente;
                    bteCliente.Tag = frmCliente._Be.cliec_icod_cliente;
                }
                deFechaDocumento.Focus();
            }
        }

        private void bteCliente_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarCliente();
        }

        private void bteTipoDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarTipoDocumento();
        }

        private void bteClaseDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            ListarClaseDocumentos();
        }


    }
}