using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmManteReciboHonorario : DevExpress.XtraEditors.XtraForm
    {

        List<EDocPorPagarDetalleCuentaContable> Lista = new List<EDocPorPagarDetalleCuentaContable>();
        List<EDocPorPagarDetalleCuentaContable> ListaEliminados = new List<EDocPorPagarDetalleCuentaContable>();

        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteReciboHonorario));
        public List<CuentaContableDetalleBE> mListaCuentaContableDetalleOrigen = new List<CuentaContableDetalleBE>();
        List<EDocPorPagarDetalleCuentaContable> mListaDetalle = new List<EDocPorPagarDetalleCuentaContable>();
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        List<ECuentaContable> mListaCuenta = new List<ECuentaContable>();
        public delegate void DelegadoMensaje(long Cab_icod_correlativo);
        public event DelegadoMensaje MiEvento;
        public int anio = 0;
        public int mes = 0;
        public List<EDocPorPagar> oDetail;

        public BSMaintenanceStatus oState;
        private BCuentasPorPagar Obl;
        public long Cab_icod_correlativo = 0;
        public long? Correlative = 0;
        public int id_moneda;
        public int? vcocc_iid_voucher_contable;

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

        public enum Operacion
        {
            Nuevo = 1,
            Modificar = 2,
            Eliminar = 3,
            Consultar = 4
        }

        #endregion

        #region "Eventos"

        public FrmManteReciboHonorario()
        {
            InitializeComponent();
        }

        private void FrmManteReciboHonorario_Load(object sender, EventArgs e)
        {
            CargaControles();
            Carga();
        }

        private void viewCuentaContableDetalle_DoubleClick(object sender, EventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal detalle, subtotal;
            detalle = (Lista.Count > 0) ? Lista.Sum(cuentas => cuentas.cdxpc_nmonto_cuenta) : 0;
            subtotal = Convert.ToDecimal(txtDesGrav.Text) + Convert.ToDecimal(txtDesNoGrav.Text);
            detalle = ((string.IsNullOrEmpty(lblNetoPagar.Text)) ? 0 : subtotal - detalle);
            using (FrmManteDxPDet frm = new FrmManteDxPDet())
            {
                frm.saldoDetalle = (detalle < 0) ? 0 : detalle;
                frm.SetInsert();
                frm.txtConcepto.Text = txtConcepto.Text;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Lista.Add(frm.objDet);
                    viewCuentaContableDetalle.RefreshData();
                    viewCuentaContableDetalle.MoveLast();
                }
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocPorPagarDetalleCuentaContable entidad = (EDocPorPagarDetalleCuentaContable)viewCuentaContableDetalle.GetRow(viewCuentaContableDetalle.FocusedRowHandle);
                using (FrmManteDxPDet frm = new FrmManteDxPDet())
                {
                    frm.objDet = entidad;
                    frm.SetModify();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        viewCuentaContableDetalle.RefreshData();
                        viewCuentaContableDetalle.MoveLast();
                    }
                }
            }
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                if (XtraMessageBox.Show("Esta seguro de eliminar", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    EliminarDetalle();
                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void EliminarDetalle()
        {
            EDocPorPagarDetalleCuentaContable obj = (EDocPorPagarDetalleCuentaContable)viewCuentaContableDetalle.GetRow(viewCuentaContableDetalle.FocusedRowHandle);
            if (obj.TipOper == 1)
            {
                Lista.Remove(obj);
                viewCuentaContableDetalle.RefreshData();
                viewCuentaContableDetalle.MovePrev();
            }
            else
            {
                obj.TipOper = 3;
                obj.intUsuario = Valores.intUsuario;
                obj.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                ListaEliminados.Add(obj);
                Lista.Remove(obj);
                viewCuentaContableDetalle.RefreshData();
                viewCuentaContableDetalle.MovePrev();
            }
        }
        #endregion

        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.CreateNew);
            int dxc_situ = 0;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                foreach (EDocPorPagar item in oDetail.Where(obe => obe.doxpc_icod_correlativo == Cab_icod_correlativo).ToList())
                {
                    dxc_situ = Convert.ToInt32(item.tablc_iid_situacion_documento);
                    break;
                }

            btnProveedor.Enabled = Enabled;
            txtTipoDoc.Properties.ReadOnly = true;
            btnClaseDocumento.Enabled = Enabled;

            txtSerie.Properties.ReadOnly = !Enabled;
            txtNumeroDocumento.Properties.ReadOnly = !Enabled;

            dtmFechaDocumento.Properties.ReadOnly = !Enabled;
            dtmFechaVencimiento.Properties.ReadOnly = !Enabled;

            lkpMoneda.Properties.ReadOnly = !Enabled;
            txtTipoDeCambio.Properties.ReadOnly = !Enabled;
            txtConcepto.Properties.ReadOnly = !Enabled;
            //txtCorrelativo.Properties.ReadOnly = !(Status == BSMaintenanceStatus.ModifyCurrent);

            Enabled = (Status == BSMaintenanceStatus.View);

            bool bmonto = (Enabled) ? Enabled : ((dxc_situ == Parametros.intSitDocPagadoParcial || dxc_situ == Parametros.intSitDocCancelado) ? true : false);
            txtDesGrav.Properties.ReadOnly = bmonto;
            txtDesNoGrav.Properties.ReadOnly = bmonto;
            txtRetencion.Properties.ReadOnly = true;
            txtPorcCuarta.Properties.ReadOnly = bmonto;

            mnuCuentaContable.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                lkpMoneda.EditValue = id_moneda;
        }

        private void clearControl()
        {
            List<EParametro> parametros = new BAdministracionSistema().listarParametro();
            List<EParametroContable> parametrosContable = new BContabilidad().listarParametroContable();
            txtConcepto.Text = "";
            txtCorrelativo.Text = String.Format("{0:0000000}", Convert.ToInt32(Correlative));
            txtDesGrav.Text = "0.00";
            txtDesNoGrav.Text = "0.00";
            btnClaseDocumento.Text = "";
            lblProveedor.Text = "";
            lblSaldo.Text = "0.00";
            txtPorcCuarta.Text = parametrosContable[0].Porcentaje_de_Retencion.ToString();
        }

        public void CargaControles()
        {
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            var lstMoneda = new BGeneral().listarTablaRegistro(5);
            BSControls.LoaderLook(lkpMoneda, lstMoneda, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

            if (Status == BSMaintenanceStatus.CreateNew)
            {
                dtmFechaDocumento.EditValue = DateTime.Now.ToShortDateString();
                dtmFechaVencimiento.EditValue = DateTime.Now;
            }
            txtTipoDoc.Text = "RHO";
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
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
            EDocPorPagar oBe = new EDocPorPagar();
            Obl = new BCuentasPorPagar();
            try
            {
                if (string.IsNullOrEmpty(btnProveedor.Text))
                {
                    oBase = btnProveedor;
                    throw new ArgumentException("Ingresar el Proveedor");
                }

                if (string.IsNullOrEmpty(btnClaseDocumento.Text))
                {
                    oBase = btnClaseDocumento;
                    throw new ArgumentException("Ingrese la clase del documento");
                }

                if (Convert.ToInt32(txtNumeroDocumento.Text) == 0 || (txtSerie.Text) == "0")
                {
                    oBase = txtNumeroDocumento;
                    throw new ArgumentException("Ingrese el N° de Documento");
                }

                if (dtmFechaVencimiento.DateTime.Date < dtmFechaDocumento.DateTime.Date)
                {
                    oBase = dtmFechaVencimiento;
                    throw new ArgumentException("La fecha de vencimiento no puede ser menor a la fecha del documento.");
                }

                if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    if (Correlative != Convert.ToInt64(txtCorrelativo.Text))
                    {
                        if (oDetail.Exists(obe => obe.anio == Parametros.intEjercicio && obe.mesec_iid_mes == mes && obe.doxpc_iid_correlativo == Convert.ToInt64(txtCorrelativo.Text)))
                        {
                            oBase = txtCorrelativo;
                            throw new ArgumentException("El número correlativo " + txtCorrelativo.Text + " ya fue utilizado");
                        }
                    }
                }

                if (Convert.ToDecimal(txtTipoDeCambio.Text) == 0)
                {
                    oBase = txtTipoDeCambio;
                    throw new ArgumentException("Ingrese el Tipo de Cambio");
                }

                if (string.IsNullOrEmpty(txtConcepto.Text))
                {
                    oBase = txtConcepto;
                    throw new ArgumentException("Ingresar el concepto");
                }

                if (Convert.ToDecimal((lblNetoPagar.Text == "") ? "0" : lblNetoPagar.Text) == 0)
                {
                    oBase = txtDesGrav;
                    throw new ArgumentException("Ingrese el monto");
                }

                if (Lista.Sum(cuentas => cuentas.cdxpc_nmonto_cuenta) != (Convert.ToDecimal(txtDesGrav.Text) + (Convert.ToDecimal(txtDesNoGrav.Text))))
                {
                    //throw new ArgumentException("La suma de los montos de las cuentas no es igual al valor de compra");
                    if (XtraMessageBox.Show("La suma de los montos de las cuentas no es igual al valor compra ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        throw new ArgumentException(string.Empty);
                }
                oBe.vcocc_iid_voucher_contable = vcocc_iid_voucher_contable;
                //oBe.doxpc_icod_correlativo = IdDocumentoPorPagar; //este campo es autonumérico en la bd
                oBe.anio = Parametros.intEjercicio;
                oBe.mesec_iid_mes = mes; // el mes es jalado del formulario anterior
                oBe.tdocc_icod_tipo_doc = Convert.ToInt32(51); //tipo documento
                oBe.tdodc_iid_correlativo = Convert.ToInt32(btnClaseDocumento.Tag); //clase de documento
                oBe.doxpc_iid_correlativo = Convert.ToInt64(txtCorrelativo.Text); //número correlativo con respecto al año mes, sólo se graba si se va a modificar, si es nuevo se va a autogenerar en el proc. alamcenado
                oBe.doxpc_vnumero_doc = txtSerie.Text + txtNumeroDocumento.Text;
                oBe.doxpc_numdoc_tipo = 1;

                oBe.doxpc_sfecha_doc = Convert.ToDateTime(dtmFechaDocumento.EditValue);
                oBe.doxpc_sfecha_vencimiento_doc = Convert.ToDateTime(dtmFechaVencimiento.EditValue);
                oBe.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag); //correlativo del proveedor
                oBe.proc_vnombrecompleto = lblProveedor.Text; //nombre completo del proveedor
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.doxpc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                oBe.doxpc_vdescrip_transaccion = txtConcepto.Text;
                oBe.doxpc_nmonto_destino_gravado = Convert.ToDecimal(txtDesGrav.Text); //adq. destino gravado
                oBe.doxpc_nmonto_destino_mixto = 0; //adq. destino mixto
                oBe.doxpc_nmonto_destino_nogravado = Convert.ToDecimal(txtDesNoGrav.Text); //adq. destino no gravada
                oBe.doxpc_nmonto_referencial_cif = 0; //monto referencia dependiendo el tipo de documento
                oBe.doxpc_nmonto_servicio_no_domic = 0; //monto servicio
                oBe.doxpc_nmonto_imp_destino_gravado = 0; //impuesto adq.destino gravado
                oBe.doxpc_nmonto_imp_destino_mixto = 0; //impuesto mixto
                oBe.doxpc_nmonto_imp_destino_nogravado = 0; //impuesto adq. destino no gravado
                oBe.doxpc_nporcentaje_imp_renta = Convert.ToDecimal(txtPorcCuarta.Text);
                oBe.doxpc_nmonto_total_documento = Convert.ToDecimal(lblNetoPagar.Text); //neto a pagar
                oBe.doxpc_nmonto_total_pagado = 0; //monto pagado cero, porque recién se está creando
                oBe.doxpc_nmonto_total_saldo = Convert.ToDecimal(lblSaldo.Text); //saldo
                oBe.doxpc_nporcentaje_igv = 0; //IGV
                oBe.tablc_iid_situacion_documento = Parametros.intSitDocGenerado; //292 indica la situación del documento GENERADO

                oBe.doxpc_tipo_comprobante_referencia = 0;
                oBe.doxpc_num_serie_referencia = "";
                oBe.doxpc_num_comprobante_referencia = "";
                oBe.doxpc_sfecha_emision_referencia = null;

                oBe.doxpc_nporcentaje_isc = 0; //porcentaje del ISC
                oBe.doxpc_nmonto_isc = 0; //monto correspondiente al ISC
                oBe.doxpc_vnro_deposito_detraccion = null; //número de depósito de detracción
                oBe.doxpc_sfec_deposito_detraccion = null; //fech de depósito de detracción
                oBe.intUsuario = Valores.intUsuario; //código de usuario que está creando el documento
                oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.doxpc_origen = "2"; //origen del documento, en este caso 2 representa que ha sido creado directamente
                oBe.doxpc_flag_estado = true;
                oBe.doxpc_nmonto_nogravado = 0; //monto no gravado
                oBe.doxpc_nmonto_retencion_rh = Convert.ToDecimal(txtRetencion.Text);
                oBe.doxpc_nmonto_retenido = 0;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Cab_icod_correlativo = Obl.InsertarEDocPorPagar(oBe, Lista, null, null);
                }
                else
                {
                    oBe.doxpc_icod_correlativo = Cab_icod_correlativo;
                    Obl.ActualizarEDocPorPagar(oBe, Lista, null, ListaEliminados, null, null, null);
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
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                        Status = BSMaintenanceStatus.View;
                    else
                        Status = BSMaintenanceStatus.View;

                    Status = BSMaintenanceStatus.View;
                    this.MiEvento(Cab_icod_correlativo);
                    this.Close();
                }
            }
        }

        private void ListarProveedor()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                btnProveedor.Tag = Proveedor._Be.iid_icod_proveedor;
                btnProveedor.Text = Proveedor._Be.vcod_proveedor;
                lblProveedor.Text = Proveedor._Be.vnombrecompleto;
            }
        }

        private void ListarClaseDocumento()
        {
            frmListarClaseDocumento Clase = new frmListarClaseDocumento();
            Clase.intTipoDoc = Convert.ToInt32(51);

            if (Clase.ShowDialog() == DialogResult.OK)
            {
                btnClaseDocumento.Tag = Clase._Be.tdocd_iid_correlativo;
                btnClaseDocumento.Text = Clase._Be.tdocd_iid_codigo_doc_det.ToString();
                lblClaseDocumento.Text = Clase._Be.tdocd_descripcion;
            }
            txtSerie.Focus();
        }

        void form2_MiEvento()
        {
            Carga();
        }

        private void Carga()
        {
            Lista = new BCuentasPorPagar().listarDXPDetCtaContable(Cab_icod_correlativo);
            grdCuentaContableDetalle.DataSource = Lista;
        }

        #endregion

        public class CuentaContableDetalleBE
        {
            public long cdxpc_icod_correlativo { get; set; }
            public long doxpc_icod_correlativo { get; set; }
            public int ctacc_iid_cuenta_contable { get; set; }
            public string NumeroCuentaContable { get; set; }
            public string DescripcionCuentaContable { get; set; }
            public int cecoc_icod_centro_costo { get; set; }
            public string CodigoCentroCosto { get; set; }
            public string DescripcionCentroCosto { get; set; }
            public int anac_icod_analitica { get; set; }
            public int IdTipoAnalitica { get; set; }
            public string TipoAnalitica { get; set; }
            public string NumeroAnalitica { get; set; }
            public decimal cdxpc_nmonto_cuenta { get; set; }
            public string cdxpc_vglosa { get; set; }
            public bool cdxpc_flag_estado { get; set; }
            public int TipOper { get; set; }

            public CuentaContableDetalleBE()
            {

            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProveedor();
        }

        private void btnClaseDocumento_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarClaseDocumento();
        }

        private void btnProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarProveedor();
        }

        private void btnClaseDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            ListarClaseDocumento();
        }

        private void txtDesNoGrav_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDesNoGrav.ContainsFocus)
            {
                txtDesGrav.Text = "0.00";
                txtRetencion.Text = "0.00";
                totalizar();
            }
        }

        private void totalizar()
        {
            decimal total;
            total = Convert.ToDecimal(txtDesGrav.Text) + Convert.ToDecimal(txtDesNoGrav.Text) - Convert.ToDecimal(txtRetencion.Text);
            lblNetoPagar.Text = total.ToString();
            lblSaldo.Text = total.ToString();
        }

        private void calcularCuarta()
        {
            if (Convert.ToDecimal(txtPorcCuarta.Text) != 0)
            {
                txtRetencion.Text = (Convert.ToDecimal(txtDesGrav.Text) * (Convert.ToDecimal(txtPorcCuarta.Text) / 100)).ToString();
            }
            else
            {
                txtRetencion.Text = "0.00";
            }
        }

        private void txtDesGrav_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDesGrav.ContainsFocus)
            {
                txtDesNoGrav.Text = "0.00";
                calcularCuarta();
                totalizar();
            }
        }

        private void txtPorcCuarta_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPorcCuarta.ContainsFocus)
            {
                calcularCuarta();
                totalizar();
            }
        }

        private void dtmFechaDocumento_EditValueChanged(object sender, EventArgs e)
        {
            txtTipoDeCambio.Text = "1.0000";
            //txtTipoDeCambio.Properties.ReadOnly = false;
            //var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtmFechaDocumento.EditValue).ToShortDateString()).ToList();
            //Lista.ForEach(obe =>
            //{
            //    txtTipoDeCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            //    txtTipoDeCambio.Properties.ReadOnly = true;
            //});
        }

    }
}