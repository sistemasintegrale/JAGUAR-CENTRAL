using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Administracion_del_Sistema.Listados;
using SGE.WindowForms.Otros.Operaciones;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmManteDocumentosXCobrar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteDocumentosXCobrar));

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
        List<EDocXCobrarCuentaContable> ListaEliminados = new List<EDocXCobrarCuentaContable>();

        private BCuentasPorCobrar Obl;
        public EDocXCobrar _BE = new EDocXCobrar();
        public int? situacion;
        public long? ctacc_icod_cuenta_gastos_nac = 0;
        public decimal? afecto = 0;
        public decimal? inafecto = 0;
        public decimal? servicio = 0;
        public decimal? impuesto = 0;
        public decimal? impuesto2 = 0;
        public decimal? subtotal = 0;
        public decimal? total = 0;

        public FrmManteDocumentosXCobrar()
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
        public void Values()
        {
            bteTipoDocumento.Tag = _BE.tdocc_icod_tipo_doc;
            bteTipoDocumento.Text = _BE.Abreviatura;
            bteClaseDocumento.Tag = _BE.tdodc_iid_correlativo;
            bteClaseDocumento.Text = _BE.ClaseDocumento;
            if (_BE.doxcc_vnumero_doc.Length == 12)
            {
                txtSerie.Text = _BE.doxcc_vnumero_doc.Remove(4);
                txtNumeroDocumento.Text = _BE.doxcc_vnumero_doc.Remove(0, 4);
            }
            else
            {
                txtSerie.Text = _BE.doxcc_vnumero_doc.Remove(3);
                txtNumeroDocumento.Text = _BE.doxcc_vnumero_doc.Remove(0, 3);
            }

            lblDescripcionClaseDocumento.Text = _BE.DescripcionClaseDocumento;
            bteCliente.Tag = _BE.cliec_icod_cliente;
            bteCliente.Text = _BE.cliec_vnombre_cliente;
            deFechaDocumento.EditValue = _BE.doxcc_sfecha_doc;
            LkpTipoMoneda.EditValue = _BE.tablc_iid_tipo_moneda;
            txtTipoCambio.Text = _BE.doxcc_nmonto_tipo_cambio.ToString();
            txtConcepto.Text = _BE.doxcc_vobservaciones;
            deFechaVencimiento.EditValue = _BE.doxcc_sfecha_vencimiento_doc;
            txtOperacionGrabada.Text = _BE.doxcc_nmonto_afecto.ToString();
            txtInafecto.Text = _BE.doxcc_nmonto_inafecto.ToString();
            txtIGV.Text = _BE.doxcc_nporcentaje_igv.ToString();
            lblIGVValor.Text = _BE.doxcc_nmonto_impuesto.ToString();
            lblPrecioVentaValor.Text = _BE.doxcc_nmonto_total.ToString();
            lblSaldoValor.Text = _BE.doxcc_nmonto_saldo.ToString();
            situacion = _BE.tablc_iid_situacion_documento;
            afecto = _BE.doxcc_nmonto_afecto;
            inafecto = _BE.doxcc_nmonto_inafecto;
            impuesto = _BE.doxcc_nmonto_impuesto;
            subtotal = (_BE.doxcc_nmonto_afecto + _BE.doxcc_nmonto_inafecto);
            total = _BE.doxcc_nmonto_total;

            lkpTipoDocRef.EditValue = _BE.doxcc_tipo_comprobante_referencia; //tipo documento referencia sólo si es N/C el documento creado
            bteNroDocRef.Text = _BE.doxcc_num_serie_referencia + _BE.doxcc_num_comprobante_referencia;//clase del documento referencia
            dtFechaReferencia.EditValue = _BE.doxcc_sfecha_emision_referencia;
            bteProyecto.Tag = _BE.pryc_icod_proyecto;
            bteProyecto.Text = _BE.NomProyecto;
            txtCC.Text = _BE.CentroCossto;
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
            txtIGV.Properties.ReadOnly = Enabled;
            mnu.Enabled = !Enabled;
        }
        public void StatusControl2()
        {
            bool Enabled = (Status == BSMaintenanceStatus.CreateNew);

            bteTipoDocumento.Enabled = Enabled;
            bteClaseDocumento.Enabled = Enabled;
            txtSerie.Properties.ReadOnly = !Enabled;
            txtNumeroDocumento.Properties.ReadOnly = !Enabled;
            bteCliente.Enabled = Enabled;
            txtTipoCambio.Properties.ReadOnly = true;
            txtTipoCambio.Enabled = Enabled;
            dtFechaReferencia.Enabled = Enabled;
            deFechaDocumento.Enabled = Enabled;
            deFechaVencimiento.Enabled = Enabled;
            txtConcepto.Enabled = Enabled;
            txtOperacionGrabada.Enabled = Enabled;
            txtInafecto.Enabled = Enabled;
            txtIGV.Enabled = Enabled;
            LkpTipoMoneda.Enabled = Enabled;
            Enabled = (Status == BSMaintenanceStatus.View);
            deFechaDocumento.Properties.ReadOnly = Enabled;
            LkpTipoMoneda.Properties.ReadOnly = Enabled;
            deFechaVencimiento.Properties.ReadOnly = Enabled;
            txtConcepto.Properties.ReadOnly = Enabled;
            txtOperacionGrabada.Properties.ReadOnly = Enabled;
            txtInafecto.Properties.ReadOnly = Enabled;
            txtIGV.Properties.ReadOnly = Enabled;
            mnu.Enabled = Enabled;
        }
        private void FrmManteDocumentosXCobrar_Load(object sender, EventArgs e)
        {
            Lista = new BCuentasPorCobrar().BuscarDocumentoXCobrarCuentaContable(_BE.doxcc_icod_correlativo);
            cargar();
            grc.DataSource = Lista;
            if (this.Status == BSMaintenanceStatus.CreateNew)
            {
                deFechaDocumento.EditValue = DateTime.Now.ToShortDateString();
            }
            else
            {
                List<TipoDoc> lst = new List<TipoDoc>();
                lst.Add(new TipoDoc { intCodigo = 0, strTipoDoc = ".......Eligir un documento....." });
                lst.Add(new TipoDoc { intCodigo = 26, strTipoDoc = "FAV" });
                lst.Add(new TipoDoc { intCodigo = 9, strTipoDoc = "BOV" });
                BSControls.LoaderLook(lkpTipoDocRef, lst, "strTipoDoc", "intCodigo", true);
                lkpTipoDocRef.EditValue = _BE.doxcc_tipo_comprobante_referencia;
                LkpTipoMoneda.EditValue = _BE.tablc_iid_tipo_moneda;
                lkpTipoDXC.EditValue = _BE.tablc_iid_tipo_docxpagar;
            }
        }
        public class TipoDoc
        {
            public int intCodigo { get; set; }
            public string strTipoDoc { get; set; }
        }
        public void cargar()
        {
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();
            var lstMoneda = new BGeneral().listarTablaRegistro(5);
            BSControls.LoaderLook(LkpTipoMoneda, lstMoneda, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

            List<TipoDoc> lst = new List<TipoDoc>();
            lst.Add(new TipoDoc { intCodigo = 0, strTipoDoc = ".......Eligir un documento....." });
            lst.Add(new TipoDoc { intCodigo = 26, strTipoDoc = "FAV" });
            lst.Add(new TipoDoc { intCodigo = 9, strTipoDoc = "BOV" });
            BSControls.LoaderLook(lkpTipoDocRef, lst, "strTipoDoc", "intCodigo", true);
            BSControls.LoaderLook(lkpTipoDXC, new BGeneral().listarTablaRegistro(80), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            deFechaVencimiento.EditValue = DateTime.Now;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            Values();
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            Values();
            Totalizar();
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EDocXCobrar oBe = new EDocXCobrar();
            Obl = new BCuentasPorCobrar();
            try
            {
                if ((txtSerie.Text) == "")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese la Serie del Documento");
                }
                if (Convert.ToInt32(txtNumeroDocumento.Text) == 0)
                {
                    oBase = txtNumeroDocumento;
                    throw new ArgumentException("Ingrese el Correlativo del Documento");
                }
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
                if (Convert.ToDecimal(txtIGV.Text) != 0 && (Convert.ToDecimal(txtOperacionGrabada.Text) + Convert.ToDecimal(txtInafecto.Text)) == 0)
                {
                    oBase = txtOperacionGrabada;
                    throw new ArgumentException("Ingresar monto");
                }
                if (Convert.ToDecimal(txtTipoCambio.Text) == 0)
                {
                    oBase = txtTipoCambio;
                    throw new ArgumentException("Se requiere el Tipo de Cambio para Guardar el Registro");
                }
                if (Lista.Count == 0)
                {
                    if (XtraMessageBox.Show("No ha ingresado el detalle de las cuentas ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        throw new ArgumentException(string.Empty);
                }
                else if ((total - impuesto) != Lista.Sum(cuentas => cuentas.ccdcc_nmonto))
                {
                    if (XtraMessageBox.Show("La suma de los montos de las cuentas no es igual al valor de venta ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        throw new ArgumentException(string.Empty);
                }


                oBe.mesec_iid_mes = Convert.ToInt16(deFechaDocumento.DateTime.Month);
                oBe.tdocc_icod_tipo_doc = Convert.ToInt32(bteTipoDocumento.Tag);
                oBe.tdodc_iid_correlativo = Convert.ToInt32(bteClaseDocumento.Tag); ;
                oBe.doxcc_vnumero_doc = txtSerie.Text + txtNumeroDocumento.Text;
                oBe.cliec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.cliec_vnombre_cliente = bteCliente.Text;
                oBe.doxcc_sfecha_doc = Convert.ToDateTime(deFechaDocumento.EditValue);
                oBe.doxcc_sfecha_vencimiento_doc = Convert.ToDateTime(deFechaVencimiento.EditValue); ;
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(LkpTipoMoneda.EditValue);
                oBe.doxcc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                oBe.tablc_iid_tipo_pago = 174;//CONTADO SGE_TABLA=41
                oBe.doxcc_vdescrip_transaccion = txtConcepto.Text;
                oBe.doxcc_nmonto_afecto = afecto;
                oBe.doxcc_nmonto_inafecto = inafecto;
                oBe.doxcc_nporcentaje_igv = Convert.ToDecimal(txtIGV.Text);
                oBe.doxcc_nmonto_impuesto = Convert.ToDecimal(lblIGVValor.Text);
                oBe.doxcc_nmonto_total = Math.Round(Convert.ToDecimal(total), 2);
                oBe.doxcc_nmonto_saldo = Math.Round(Convert.ToDecimal(total), 2);
                oBe.doxcc_nmonto_pagado = 0;
                oBe.tablc_iid_situacion_documento = Parametros.intSitDocCobrarGenerado;
                oBe.doxcc_vobservaciones = txtConcepto.Text;
                oBe.pryc_icod_proyecto = Convert.ToInt32(bteProyecto.Tag);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                oBe.tablc_iid_tipo_docxpagar = Convert.ToInt32(lkpTipoDXC.EditValue);
                oBe.doxcc_tipo_comprobante_referencia = Convert.ToInt32(lkpTipoDocRef.EditValue);
                if (bteNroDocRef.Text != "")
                {
                    oBe.doxcc_num_serie_referencia = bteNroDocRef.Text.Substring(0, 4);//clase del documento referencia
                    oBe.doxcc_num_comprobante_referencia = bteNroDocRef.Text.Substring(4);
                }
                else
                {
                    oBe.doxcc_num_serie_referencia = "";
                    oBe.doxcc_num_comprobante_referencia = "";
                }
                if (dtFechaReferencia.EditValue != null)
                    oBe.doxcc_sfecha_emision_referencia = Convert.ToDateTime(dtFechaReferencia.EditValue);
                else
                {
                    oBe.doxcc_sfecha_emision_referencia = null;
                }
                oBe.anio = Parametros.intEjercicio;
                oBe.doxcc_origen = "2"; //el origen cuando se crea directamente es 2 para verificar lo que podemos modificar, no podemos modificar lo que no creemos(origen diferente de 2)
                oBe.ctacc_icod_cuenta_gastos_nac = Convert.ToInt64(ctacc_icod_cuenta_gastos_nac);

                oBe.puvec_icod_punto_venta = 2;

                if (Status == BSMaintenanceStatus.CreateNew)
                {

                    Obl.InsertarDocumentoXCobrar(oBe, Lista);
                }
                else
                {
                    oBe.doxcc_icod_correlativo = _BE.doxcc_icod_correlativo;
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
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.DialogResult = DialogResult.OK;
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
                ctacc_icod_cuenta_gastos_nac = Convert.ToInt64(Clase._Be.ctacc_icod_cuenta_gastos_nac);
            }
            txtSerie.Focus();
        }

        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

        private void deFechaDocumento_EditValueChanged(object sender, EventArgs e)
        {
            txtTipoCambio.Text = "0.0000";
            txtTipoCambio.Properties.ReadOnly = false;
            var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(deFechaDocumento.EditValue).ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                txtTipoCambio.Properties.ReadOnly = true;
            });
        }

        public void Totalizar()
        {
            afecto = Convert.ToDecimal(txtOperacionGrabada.Text);
            inafecto = Convert.ToDecimal(txtInafecto.Text);

            impuesto = Convert.ToDecimal(txtIGV.Text) / 100 * afecto;
            impuesto2 = Math.Round(Convert.ToDecimal(279.585), 2);
            subtotal = afecto + inafecto + servicio;
            total = afecto + inafecto + servicio + impuesto;

            lblSubTotalValor.Text = subtotal.ToString();

            decimal IGVValor = Math.Round(Convert.ToDecimal(impuesto), 2, MidpointRounding.AwayFromZero);

            lblIGVValor.Text = Math.Round(Convert.ToDecimal(impuesto), 2, MidpointRounding.AwayFromZero).ToString();
            txtIGVValor.Text = Math.Round((Convert.ToDecimal(impuesto)), 2).ToString();
            lblPrecioVentaValor.Text = Math.Round(Convert.ToDecimal(total), 2).ToString();
            lblSaldoValor.Text = "0.00";
        }


        private void Nuevo_Click(object sender, EventArgs e)
        {
            decimal detalle;
            detalle = (Lista.Count > 0) ? Lista.Sum(cuentas => cuentas.ccdcc_nmonto) : 0;
            detalle = ((string.IsNullOrEmpty(lblSubTotalValor.Text)) ? 0 : Convert.ToDecimal(lblSubTotalValor.Text)) - detalle;
            using (FrmManteDxCDet frm = new FrmManteDxCDet())
            {
                frm.saldoDetalle = (detalle < 0) ? 0 : detalle;
                frm.SetInsert();
                frm.txtConcepto.Text = txtConcepto.Text;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Lista.Add(frm.objEDxC);
                    grd.RefreshData();
                    grd.MoveLast();
                    BtnGuardar.Enabled = true;
                }
            }
        }

        private void Modificar_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrarCuentaContable entidad = (EDocXCobrarCuentaContable)grd.GetRow(grd.FocusedRowHandle);
                using (FrmManteDxCDet frm = new FrmManteDxCDet())
                {
                    frm.objEDxC = entidad;
                    frm.SetModify();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        grd.RefreshData();
                        grd.MoveLast();
                        BtnGuardar.Enabled = true;
                    }
                }
            }
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void Eliminar_Click(object sender, EventArgs e)
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
            EDocXCobrarCuentaContable obj = (EDocXCobrarCuentaContable)grd.GetRow(grd.FocusedRowHandle);
            if (obj.operacion == 1)
            {
                Lista.Remove(obj);
                grd.RefreshData();
                grd.MovePrev();
            }
            else
            {
                obj.operacion = 3;
                obj.pc = WindowsIdentity.GetCurrent().Name.ToString();
                obj.usuario = Valores.intUsuario;
                ListaEliminados.Add(obj);
                Lista.Remove(obj);
                grd.RefreshData();
                grd.MovePrev();
            }
        }

        private void bteTipoDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarTipoDocumento();
        }

        private void bteClaseDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarClaseDocumentos();
        }

        private void bteCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                ListarCliente();
        }

        private void txtOperacionGrabada_Enter(object sender, EventArgs e)
        {
            txtOperacionGrabada.SelectAll();
        }

        private void txtOperacionGrabada_EditValueChanged(object sender, EventArgs e)
        {
            if (txtOperacionGrabada.ContainsFocus)
            {

                Totalizar();
            }
        }

        private void txtInafecto_EditValueChanged(object sender, EventArgs e)
        {
            if (txtInafecto.ContainsFocus)
            {

                Totalizar();
            }
        }



        private void txtIGV_EditValueChanged(object sender, EventArgs e)
        {
            if (txtIGV.ContainsFocus)
            {
                Totalizar();
            }
        }

        private void bteNroDocRef_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmListarDocxCobrar frm = new FrmListarDocxCobrar();
            if (Convert.ToInt32(bteCliente.Tag) != 0)
                frm.intIcodCliente = Convert.ToInt32(bteCliente.Tag);
            else
            {
                XtraMessageBox.Show("Debe elegir un Cliente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            frm.TipoDoc = Convert.ToInt32(lkpTipoDocRef.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                lkpTipoDocRef.EditValue = frm.EDocPorCobrar.tdocc_icod_tipo_doc;
                lkpTipoDocRef.Text = frm.EDocPorCobrar.tdocc_vabreviatura_tipo_doc;
                bteNroDocRef.Text = frm.EDocPorCobrar.doxcc_vnumero_doc;
                dtFechaReferencia.EditValue = frm.EDocPorCobrar.doxcc_sfecha_doc;
            }
        }

        private void bteTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(bteTipoDocumento.Tag) == Parametros.intTipoDocNotaCreditoCliente || Convert.ToInt32(bteTipoDocumento.Tag) == Parametros.intTipoDocNotaDebito || Convert.ToInt32(bteTipoDocumento.Tag) == 0)
            {
                lkpTipoDocRef.Enabled = true;
                bteNroDocRef.Enabled = true;
                dtFechaReferencia.Enabled = true;
            }
            else
            {
                lkpTipoDocRef.Enabled = false;
                bteNroDocRef.Enabled = false;
                dtFechaReferencia.Enabled = false;
            }
        }

        private void gcImporte_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bteProyecto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProyecto();
        }
        private void ListarProyecto()
        {
            using (frmListarProyectos frm = new frmListarProyectos())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteProyecto.Tag = frm._Be.pryc_icod_proyecto;
                    bteProyecto.Text = frm._Be.pryc_vdescripcion;
                    txtCC.Text = frm._Be.CentroCosto;
                }
            }
        }

        private void lkpTipoDXC_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipoDXC.EditValue) == 337)
            {
                bteProyecto.Enabled = false;
            }
            else
            {
                bteProyecto.Enabled = true;
            }
        }

    }
}