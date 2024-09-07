using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Operaciones;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmManteBoleta : DevExpress.XtraEditors.XtraForm
    {
        public EBoletaCab oBe = new EBoletaCab();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<EBoletaDet> lstBoletaDetalle = new List<EBoletaDet>();
        List<EBoletaDet> lstDelete = new List<EBoletaDet>();
        string strCodCliente = "";
        public string PorIGV;
        public int TDComercial = 0;
        public string CodigoSUNAT { get; set; }
        int Numero_dias_vencimiento_cliente = 0;
        List<EPuntoVenta> lstPuntoVenta = new List<EPuntoVenta>();
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
                txtSerie.Enabled = Enabled;
                txtNumero.Enabled = Enabled;
                lkpMoneda.Enabled = Enabled;

            }
        }

        private void setValues()
        {
            txtSerie.Text = oBe.bovc_vnumero_boleta.Substring(0, 4);
            txtNumero.Text = oBe.bovc_vnumero_boleta.Substring(4, 8);
            dteFecha.EditValue = oBe.bovc_sfecha_boleta;

            lkpSituacion.EditValue = oBe.tablc_iid_situacion;
            bteCliente.Tag = oBe.cliec_icod_cliente;
            bteCliente.Text = oBe.cliec_vnombre_cliente;
            txtDNI.Text = oBe.cliec_cruc;
            txtDireccion.Text = oBe.cliec_vdireccion_cliente;
            txtTelefono.Text = oBe.strTelefonoCliente;
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            txtPorcentIGV.Text = oBe.bovc_npor_imp_igv.ToString();
            lkpFormaPago.EditValue = oBe.tablc_iid_forma_pago;
            txtMontoNeto.Text = oBe.bovc_nmonto_neto.ToString();
            txtMontoIGV.Text = oBe.bovc_nmonto_imp.ToString();
            txtMontoTotal.Text = oBe.bovc_nmonto_total.ToString();
            dteVencimiento.EditValue = oBe.bovc_sfecha_vencim_boleta;



            Numero_dias_vencimiento_cliente = Convert.ToInt32(oBe.cliec_nnumero_dias);
            lstBoletaDetalle = new BVentas().listarBoletaDetalle(oBe.bovc_icod_boleta);
            viewFactura.RefreshData();
            txtcantidadTotal.Text = lstBoletaDetalle.Sum(x => x.bovd_ncantidad_total_producto).ToString();
            BtnGuiaRemision.Tag = oBe.remic_icod_remision;
            BtnGuiaRemision.Text = oBe.remic_vnumero_remision;
            bteVendedor.Tag = oBe.vendc_icod_vendedor;
            bteVendedor.Text = oBe.NomVendedor;
            btncodigoalmacen.Tag = oBe.bovc_iid_almacen;
            btncodigoalmacen.Text = oBe.bovc_vdescripcion_almacen;
            lkpPuntoVenta.EditValue = oBe.puvec_icod_punto_venta;
            lkpPuntoVenta.Text = oBe.puvec_icod_punto_venta.ToString();
            if (oBe.remic_icod_remision != 0)
            {
                nuevoToolStripMenuItem.Enabled = false;
                eliminarToolStripMenuItem.Enabled = false;
                nuevoServicioToolStripMenuItem.Enabled = false;
            }
        }

        public FrmManteBoleta()
        {
            InitializeComponent();
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            SetCancel();
        }

        private void cargar()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                setFecha(dteFecha);
                setFecha(dteVencimiento);
                getNroDoc();
                txtPorcentIGV.Text = Parametros.strPorcIGV;
            }

            grdFactura.DataSource = lstBoletaDetalle;

        }
        public void CargarControles()
        {
            var lstTipoPago = new BGeneral().listarTablaRegistro(20).Where(x => x.tarec_iid_tabla_registro == 116 || x.tarec_iid_tabla_registro == 117
                  ).ToList();
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(21), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5).Where(x => x.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpFormaPago, lstTipoPago, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpPuntoVenta, new BCentral().ListarPuntoVenta(), "puvec_vdescripcion", "puvec_icod_punto_venta", true);
        }
        private void getNroDoc()
        {
            try
            {
                //if (txtSerie.Text.Length == 4)
                //{
                var lst = new BVentas().listarSeries().Where(x => x.rs_icod_pvt == 2 && x.rs_itipo_documento == TDComercial).ToList();
                if (Convert.ToInt32(lst.Count()) > 0)
                {
                    txtSerie.Text = lst[0].rs_vserie;
                    txtNumero.Text = (Convert.ToInt32(lst[0].rs_icorrelativo) + 1).ToString();
                }
                else
                {
                    throw new ArgumentException("Numero de Serie Incorrecto");
                }
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmBoletaDetalle frm = new FrmBoletaDetalle())
            {
                frm.SetInsert();
                frm.lstBoletaDetalle = lstBoletaDetalle;
                frm.txtitem.Text = (lstBoletaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstBoletaDetalle.Count + 1);
                frm.indicador = 1;
                frm.icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                frm.icod_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                frm.flag_afecto_igv = Convert.ToBoolean(cbIncluyeIGV.Checked);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    EBoletaDet _obe = new EBoletaDet();
                    _obe.almac_icod_almacen = frm.obe.almac_icod_almacen;
                    _obe.bovd_iitem_boleta = frm.obe.bovd_iitem_boleta;
                    _obe.tablc_iid_motivo = frm.obe.tablc_iid_motivo;
                    _obe.bovd_vcodigo_extremo_prod = frm.obe.bovd_vcodigo_extremo_prod;
                    _obe.prdc_icod_producto = frm.obe.prdc_icod_producto;
                    _obe.bovd_vdescripcion_prod = frm.obe.bovd_vdescripcion_prod;
                    _obe.bovd_icod_serie = frm.obe.bovd_icod_serie;
                    _obe.bovd_rango_talla = frm.obe.bovd_rango_talla;
                    _obe.resec_vdescripcion = frm.obe.resec_vdescripcion;
                    //_obe.bovd_iid_moneda = frm.obe.bovd_iid_moneda;
                    _obe.bovd_ncantidad_total_producto = frm.obe.bovd_ncantidad_total_producto;
                    _obe.bovd_monto_unit = frm.obe.bovd_monto_unit;
                    _obe.bovd_monto_total = frm.obe.bovd_monto_total;
                    _obe.bovd_nprecio_unitario_item = frm.obe.bovd_nprecio_unitario_item;
                    _obe.bovd_nprecio_total_item = frm.obe.bovd_nprecio_total_item;
                    _obe.bovd_nmonto_impuesto_item = frm.obe.bovd_nmonto_impuesto_item;
                    _obe.bovd_nneto_igv = frm.obe.bovd_nneto_igv;
                    _obe.bovd_nneto_exo = frm.obe.bovd_nneto_exo;
                    _obe.bovd_talla1 = frm.obe.bovd_talla1;
                    _obe.bovd_talla2 = frm.obe.bovd_talla2;
                    _obe.bovd_talla3 = frm.obe.bovd_talla3;
                    _obe.bovd_talla4 = frm.obe.bovd_talla4;
                    _obe.bovd_talla5 = frm.obe.bovd_talla5;
                    _obe.bovd_talla6 = frm.obe.bovd_talla6;
                    _obe.bovd_talla7 = frm.obe.bovd_talla7;
                    _obe.bovd_talla8 = frm.obe.bovd_talla8;
                    _obe.bovd_talla9 = frm.obe.bovd_talla9;
                    _obe.bovd_talla10 = frm.obe.bovd_talla10;
                    _obe.bovd_cant_talla1 = frm.obe.bovd_cant_talla1;
                    _obe.bovd_cant_talla2 = frm.obe.bovd_cant_talla2;
                    _obe.bovd_cant_talla3 = frm.obe.bovd_cant_talla3;
                    _obe.bovd_cant_talla4 = frm.obe.bovd_cant_talla4;
                    _obe.bovd_cant_talla5 = frm.obe.bovd_cant_talla5;
                    _obe.bovd_cant_talla6 = frm.obe.bovd_cant_talla6;
                    _obe.bovd_cant_talla7 = frm.obe.bovd_cant_talla7;
                    _obe.bovd_cant_talla8 = frm.obe.bovd_cant_talla8;
                    _obe.bovd_cant_talla9 = frm.obe.bovd_cant_talla9;
                    _obe.bovd_cant_talla10 = frm.obe.bovd_cant_talla10;
                    _obe.operacion = 1;
                    _obe.flag_afecto_igv = frm.obe.flag_afecto_igv;
                    _obe.strDesUM = frm.strUM;
                    #region Factura Electronica Detalle
                    _obe.NumeroOrdenItem = frm.obe.NumeroOrdenItem;
                    _obe.cantidad = frm.obe.cantidad;
                    _obe.unidadMedida = frm.obe.unidadMedida;
                    _obe.ValorVentaItem = frm.obe.ValorVentaItem;
                    _obe.CodMotivoDescuentoItem = frm.obe.CodMotivoDescuentoItem;
                    _obe.FactorDescuentoItem = frm.obe.FactorDescuentoItem;
                    _obe.DescuentoItem = frm.obe.DescuentoItem;
                    _obe.BaseDescuentotem = frm.obe.BaseDescuentotem;
                    _obe.CodMotivoCargoItem = frm.obe.CodMotivoCargoItem;
                    _obe.FactorCargoItem = frm.obe.FactorCargoItem;
                    _obe.MontoCargoItem = frm.obe.MontoCargoItem;
                    _obe.BaseCargoItem = frm.obe.BaseCargoItem;
                    _obe.MontoTotalImpuestosItem = frm.obe.MontoTotalImpuestosItem;
                    _obe.MontoImpuestoIgvItem = frm.obe.MontoImpuestoIgvItem;
                    _obe.MontoAfectoImpuestoIgv = frm.obe.MontoAfectoImpuestoIgv;
                    _obe.MontoInafectoItem = frm.obe.MontoInafectoItem;
                    _obe.PorcentajeIGVItem = frm.obe.PorcentajeIGVItem;
                    _obe.MontoImpuestoISCItem = frm.obe.MontoImpuestoISCItem;
                    _obe.MontoAfectoImpuestoIsc = frm.obe.MontoAfectoImpuestoIsc;
                    _obe.PorcentajeISCtem = frm.obe.PorcentajeISCtem;
                    _obe.MontoImpuestoIVAPtem = frm.obe.MontoImpuestoIVAPtem;
                    _obe.MontoAfectoImpuestoIVAPItem = frm.obe.MontoAfectoImpuestoIVAPItem;
                    _obe.PorcentajeIVAPItem = frm.obe.PorcentajeIVAPItem;
                    _obe.descripcion = frm.obe.descripcion;
                    _obe.codigoItem = frm.obe.codigoItem;
                    _obe.ObservacionesItem = frm.obe.ObservacionesItem;
                    _obe.ValorUnitarioItem = frm.obe.ValorUnitarioItem;
                    _obe.tipoImpuesto = frm.obe.tipoImpuesto;
                    _obe.UMedida = frm.obe.UMedida;
                    _obe.PrecioVentaUnitarioItem = frm.obe.PrecioVentaUnitarioItem;
                    #endregion
                    lstBoletaDetalle.Add(_obe);
                    if (Convert.ToDecimal(frm.PorIGV) != 0)
                    {
                        PorIGV = frm.PorIGV;
                    }
                    CalcularCant();
                    setTotales();
                    grdFactura.DataSource = lstBoletaDetalle;
                    grdFactura.RefreshDataSource();
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
                        txtDireccion.Text = frm._Be.cliec_vdireccion_cliente;
                        txtDNI.Text = frm._Be.cliec_vnumero_doc_cli;
                        txtTelefono.Text = frm._Be.cliec_vnro_telefono;
                        strCodCliente = frm._Be.cliec_vcod_cliente;
                        Numero_dias_vencimiento_cliente = Convert.ToInt32(frm._Be.cliec_nnumero_dias);
                        if (frm._Be.cliec_bcredito == true)
                        {
                            lkpFormaPago.EditValue = 117;//forma de pago CREDITO
                            dteVencimiento.EditValue = Convert.ToDateTime(dteFecha.EditValue).AddDays(Convert.ToInt32(Numero_dias_vencimiento_cliente));
                        }
                        else
                        {
                            Numero_dias_vencimiento_cliente = 0;
                            dteVencimiento.EditValue = Convert.ToDateTime(dteFecha.EditValue).AddDays(Convert.ToInt32(Numero_dias_vencimiento_cliente));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setCliente(int intCliente)
        {
            try
            {
                var _Be = new BVentas().ListarCliente().Where(x => x.cliec_icod_cliente == intCliente).ToList()[0];
                bteCliente.Tag = _Be.cliec_icod_cliente;
                bteCliente.Text = _Be.cliec_vnombre_cliente;
                txtDireccion.Text = _Be.cliec_vdireccion_cliente;
                txtDNI.Text = _Be.cliec_cruc;
                txtTelefono.Text = _Be.cliec_vnro_telefono;
                strCodCliente = _Be.cliec_vcod_cliente;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (txtSerie.Text == "")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese Nro. de Serie de Boleta");
                }

                if (txtSerie.Text == "0000")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("N° de Serie no registrado, registrar N° serie en REGISTRO DE TIPOS DE DOCUMENTOS");
                }

                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Boleta");
                }

                if (Convert.ToDateTime(dteFecha.Text).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del ejercicio");
                }

                if (Convert.ToInt32(bteCliente.Tag) == 0)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccione cliente");
                }

                if (String.IsNullOrWhiteSpace(txtDNI.Text))
                {
                    oBase = txtDNI;
                    throw new ArgumentException("Cliente no cuenta con RUC registrado, favor de registrar RUC del Cliente");
                }

                if (Convert.ToDateTime(dteFecha.Text) > Convert.ToDateTime(dteVencimiento.Text))
                {
                    oBase = dteVencimiento;
                    throw new ArgumentException("la fecha de vencimiento no debe ser menor a la fecha de la factura");
                }

                if (Convert.ToDecimal(txtMontoTotal.Text) == 0)
                {
                    oBase = txtMontoTotal;
                    throw new ArgumentException("El monto Total no puede ser 0");
                }

                oBe.bovc_vnumero_boleta = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.bovc_sfecha_boleta = Convert.ToDateTime(dteFecha.Text);
                oBe.bovc_sfecha_vencim_boleta = Convert.ToDateTime(dteVencimiento.Text);
                oBe.cliec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.cliec_vcod_cliente = strCodCliente;
                oBe.cliec_vnombre_cliente = txtDireccion.Text;
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.tablc_iid_forma_pago = Convert.ToInt32(lkpFormaPago.EditValue);
                oBe.tablc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.bovc_vdni = txtDNI.Text;
                oBe.vendc_icod_vendedor = Convert.ToInt32(bteVendedor.Tag);
                oBe.puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                oBe.bovc_iid_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.anio = Parametros.intEjercicio;
                oBe.bovc_flag_estado = true;

                oBe.remic_icod_remision = Convert.ToInt32(BtnGuiaRemision.Tag);
                oBe.remic_vnumero_remision = BtnGuiaRemision.Text;


                oBe.bfavc_bafecto_igv = cbIncluyeIGV.Checked;

                if (Convert.ToDecimal(PorIGV) > 0)
                {
                    oBe.bovc_noperacion_gravadas = Convert.ToDecimal(txtMontoNeto.Text);
                    oBe.bovc_npor_imp_igv = Convert.ToDecimal(PorIGV);
                }
                else
                {
                    oBe.bovc_noperacion_inafectas = Convert.ToDecimal(txtMontoTotal.Text);
                }
                oBe.bovc_nmonto_imp = Convert.ToDecimal(txtMontoIGV.Text);
                oBe.bovc_nmonto_neto = Convert.ToDecimal(txtMontoNeto.Text);

                oBe.bovc_nmonto_total = Convert.ToDecimal(txtMontoTotal.Text);

                oBe.bovc_icod_pvt = 2;//Central

                #region Facturacion Electronica
                oBe.idDocumento = oBe.bovc_vnumero_boleta.Remove(4, 8) + '-' + oBe.bovc_vnumero_boleta.Remove(0, 4);
                oBe.fechaEmision = oBe.bovc_sfecha_boleta.ToString();
                oBe.fechaVencimiento = oBe.bovc_sfecha_vencim_boleta.ToString();
                oBe.tipoDocumento = "03";
                if (Convert.ToInt32(lkpMoneda.EditValue) == 3)
                {
                    oBe.moneda = "PEN";
                }
                else
                {
                    oBe.moneda = "USD";
                }
                oBe.cantidadItems = lstBoletaDetalle.Count;
                oBe.nombreComercialEmisor = Valores.strNombreEmpresa;
                oBe.nombreLegalEmisor = "CALZADOS JAGUAR SAC";
                oBe.tipoDocumentoEmisor = "6";
                oBe.nroDocumentoEmisior = Valores.strRUC;
                oBe.CodLocalEmisor = "0000";
                oBe.nroDocumentoReceptor = txtDNI.Text;
                oBe.tipoDocumentoReceptor = "1";
                oBe.nombreLegalReceptor = bteCliente.Text;
                oBe.direccionReceptor = txtDireccion.Text;
                oBe.CodMotivoDescuento = 0;
                oBe.PorcDescuento = 0;
                oBe.MontoDescuentoGlobal = 0;
                oBe.BaseMontoDescuento = 0;
                oBe.MontoTotalImpuesto = oBe.bovc_nmonto_imp + oBe.bovc_nmonto_ivap;
                oBe.MontoGravadasIGV = oBe.bovc_nmonto_neto;
                oBe.CodigoTributo = 1000;
                oBe.TotalIvap = oBe.bovc_nmonto_ivap;
                oBe.MontoGravadasIVAP = oBe.bovc_nmonto_neto_ivap;
                oBe.MontoExonerado = 0;
                oBe.MontoInafecto = oBe.bovc_nmonto_neto_exo;
                oBe.MontoGratuitoImpuesto = 0;
                oBe.MontoBaseGratuito = 0;
                oBe.totalIgv = oBe.bovc_nmonto_imp;
                oBe.MontoGravadosISC = 0;
                oBe.totalIsc = 0;
                oBe.MontoGravadosOtros = 0;
                oBe.totalOtrosTributos = 0;
                oBe.TotalValorVenta = oBe.bovc_nmonto_neto + oBe.bovc_nmonto_neto_ivap + oBe.bovc_nmonto_neto_exo;
                oBe.TotalPrecioVenta = oBe.bovc_nmonto_total;
                oBe.MontoDescuento = 0;
                oBe.MontoTotalCargo = 0;
                oBe.MontoTotalAnticipo = 0;
                oBe.ImporteTotalVenta = oBe.bovc_nmonto_total;
                oBe.EstadoFacturacion = 4;

                #endregion
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.bovc_icod_boleta = new BVentas().insertarBoleta(oBe, lstBoletaDetalle);
                }
                else
                {
                    new BVentas().modificarBoleta(oBe, lstBoletaDetalle, lstDelete);
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
                    MiEvento(oBe.bovc_icod_boleta);
                    Close();
                }
            }
        }

        private void FrmManteFactura_Load(object sender, EventArgs e)
        {
            cargar();
            lstPuntoVenta = new BCentral().ListarPuntoVenta().Where(x => x.puvec_icod_punto_venta == 2).ToList();
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

        public void CalcularCant()
        {
            txtcantidadTotal.Text = (lstBoletaDetalle.Sum(ob => ob.bovd_ncantidad_total_producto)).ToString();
        }

        private void setTotales()
        {
            txtMontoTotal.Text = lstBoletaDetalle.Sum(x => x.bovd_nprecio_total_item).ToString();
            if (lstBoletaDetalle.Count > 0)
            {

                txtMontoIGV.Text = lstBoletaDetalle.Sum(x => x.bovd_nmonto_impuesto_item).ToString();
                txtMontoNeto.Text = lstBoletaDetalle.Sum(x => x.bovd_nneto_igv).ToString();
                txtMontoNetoExo.Text = lstBoletaDetalle.Sum(x => x.bovd_nneto_exo).ToString();
            }
            else
            {
                txtMontoNeto.Text = "0.00";
                txtMontoIGV.Text = "0.00";
                txtMontoTotal.Text = "0.00";
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaDet obe = (EBoletaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            //if (obe.intClasificacionProducto != Parametros.intTipoPrdServicio)
            modificarItem();
            //else
            //    modificarServicio();           
        }

        private void modificarItem()
        {
            EBoletaDet obe = (EBoletaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            using (FrmBoletaDetalle frm = new FrmBoletaDetalle())
            {
                frm.txtitem.Text = obe.bovd_iitem_boleta.ToString();
                frm.favd_icod_item_factura = obe.bovd_icod_item_boleta;
                frm.icod_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                frm.icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                frm.btncodigoProducto.Tag = obe.prdc_icod_producto;
                frm.indicador = 0;
                frm.btncodigoProducto.Text = obe.bovd_vcodigo_extremo_prod;
                frm.txtdescripcion.Text = obe.bovd_vdescripcion_prod;
                frm.txtcantidaddetalle.Text = obe.bovd_ncantidad_total_producto.ToString();
                frm.txtprecio.Text = obe.bovd_nprecio_unitario_item.ToString();
                frm.txtPrecioVenta.Text = obe.bovd_nprecio_total_item.ToString();
                frm.txtDescuento.Text = obe.bovd_nporcentaje_descuento_item.ToString();
                frm.lkpSerie.EditValue = obe.bovd_icod_serie;
                frm.txttalla1.Text = string.Format("{0:00}", obe.bovd_talla1);
                frm.txttalla2.Text = string.Format("{0:00}", obe.bovd_talla2);
                frm.txttalla3.Text = string.Format("{0:00}", obe.bovd_talla3);
                frm.txttalla4.Text = string.Format("{0:00}", obe.bovd_talla4);
                frm.txttalla5.Text = string.Format("{0:00}", obe.bovd_talla5);
                frm.txttalla6.Text = string.Format("{0:00}", obe.bovd_talla6);
                frm.txttalla7.Text = string.Format("{0:00}", obe.bovd_talla7);
                frm.txttalla8.Text = string.Format("{0:00}", obe.bovd_talla8);
                frm.txttalla9.Text = string.Format("{0:00}", obe.bovd_talla9);
                frm.txttalla10.Text = string.Format("{0:00}", obe.bovd_talla10);
                frm.txtcantidad1.Text = obe.bovd_cant_talla1.ToString();
                frm.txtcantidad2.Text = obe.bovd_cant_talla2.ToString();
                frm.txtcantidad3.Text = obe.bovd_cant_talla3.ToString();
                frm.txtcantidad4.Text = obe.bovd_cant_talla4.ToString();
                frm.txtcantidad5.Text = obe.bovd_cant_talla5.ToString();
                frm.txtcantidad6.Text = obe.bovd_cant_talla6.ToString();
                frm.txtcantidad7.Text = obe.bovd_cant_talla7.ToString();
                frm.txtcantidad8.Text = obe.bovd_cant_talla8.ToString();
                frm.txtcantidad9.Text = obe.bovd_cant_talla9.ToString();
                frm.txtcantidad10.Text = obe.bovd_cant_talla10.ToString();
                #region Factura Electronica Detalle
                obe.NumeroOrdenItem = obe.bovd_iitem_boleta;
                obe.cantidad = obe.bovd_ncantidad_total_producto;
                obe.unidadMedida = CodigoSUNAT;
                obe.ValorVentaItem = obe.bovd_nprecio_total_item;
                obe.CodMotivoDescuentoItem = 0;
                obe.FactorDescuentoItem = 0;
                obe.DescuentoItem = 0;
                obe.BaseDescuentotem = 0;
                obe.CodMotivoCargoItem = 0;
                obe.FactorCargoItem = 0;
                obe.MontoCargoItem = 0;
                obe.BaseCargoItem = 0;
                obe.MontoTotalImpuestosItem = obe.bovd_nmonto_impuesto_item;
                obe.MontoImpuestoIgvItem = obe.bovd_nmonto_impuesto_item;
                obe.MontoAfectoImpuestoIgv = obe.bovd_nprecio_neto_item;
                obe.PorcentajeIGVItem = obe.bovd_nporcentaje_descuento_item;
                obe.MontoImpuestoISCItem = 0;
                obe.MontoAfectoImpuestoIsc = 0;
                obe.PorcentajeISCtem = 0;
                obe.MontoImpuestoIVAPtem = 0;
                obe.MontoAfectoImpuestoIVAPItem = 0;
                obe.PorcentajeIVAPItem = 0;
                obe.descripcion = obe.bovd_vdescripcion_prod;
                obe.codigoItem = obe.bovd_vcodigo_extremo_prod;
                obe.ObservacionesItem = obe.ObservacionesItem;
                obe.ValorUnitarioItem = obe.bovd_nprecio_unitario_item;
                obe.UMedida = obe.strDesUM;
                #endregion
                frm.btncodigoProducto.Enabled = false;
                frm.lkpSerie.Enabled = false;
                frm.btnGenerar.Enabled = false;
                frm.btnAgregar.Enabled = false;

                frm.cargarcantidadesOriginales();
                frm.flag_afecto_igv = Convert.ToBoolean(cbIncluyeIGV.Checked);
                frm.icodUM = obe.unidc_icod_unidad_medida;
                frm.CodigoSUNAT = obe.CodigoSUNAT;
                frm.strUM = obe.strDesUM;
                frm.SetModify();
                frm.obe = obe;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    obe.bovd_ncantidad_total_producto = frm.obe.bovd_ncantidad_total_producto;
                    obe.bovd_nprecio_unitario_item = frm.obe.bovd_nprecio_unitario_item;
                    obe.bovd_nprecio_total_item = frm.obe.bovd_nprecio_total_item;
                    obe.bovd_talla1 = frm.obe.bovd_talla1;
                    obe.bovd_talla2 = frm.obe.bovd_talla2;
                    obe.bovd_talla3 = frm.obe.bovd_talla3;
                    obe.bovd_talla4 = frm.obe.bovd_talla4;
                    obe.bovd_talla5 = frm.obe.bovd_talla5;
                    obe.bovd_talla6 = frm.obe.bovd_talla6;
                    obe.bovd_talla7 = frm.obe.bovd_talla7;
                    obe.bovd_talla8 = frm.obe.bovd_talla8;
                    obe.bovd_talla9 = frm.obe.bovd_talla9;
                    obe.bovd_talla10 = frm.obe.bovd_talla10;
                    obe.bovd_cant_talla1 = frm.obe.bovd_cant_talla1;
                    obe.bovd_cant_talla2 = frm.obe.bovd_cant_talla2;
                    obe.bovd_cant_talla3 = frm.obe.bovd_cant_talla3;
                    obe.bovd_cant_talla4 = frm.obe.bovd_cant_talla4;
                    obe.bovd_cant_talla5 = frm.obe.bovd_cant_talla5;
                    obe.bovd_cant_talla6 = frm.obe.bovd_cant_talla6;
                    obe.bovd_cant_talla7 = frm.obe.bovd_cant_talla7;
                    obe.bovd_cant_talla8 = frm.obe.bovd_cant_talla8;
                    obe.bovd_cant_talla9 = frm.obe.bovd_cant_talla9;
                    obe.bovd_cant_talla10 = frm.obe.bovd_cant_talla10;
                    if (obe.bovd_icod_item_boleta == 0)
                        obe.operacion = 1;
                    else
                    {
                        if (obe.operacion != 1)
                        {
                            obe.operacion = 2;
                        }
                    }

                    obe.strDesUM = frm.strUM;
                    #region Factura Electronica Detalle
                    obe.NumeroOrdenItem = frm.obe.NumeroOrdenItem;
                    obe.cantidad = frm.obe.cantidad;
                    obe.unidadMedida = frm.obe.unidadMedida;
                    obe.ValorVentaItem = frm.obe.ValorVentaItem;
                    obe.CodMotivoDescuentoItem = frm.obe.CodMotivoDescuentoItem;
                    obe.FactorDescuentoItem = frm.obe.FactorDescuentoItem;
                    obe.DescuentoItem = frm.obe.DescuentoItem;
                    obe.BaseDescuentotem = frm.obe.BaseDescuentotem;
                    obe.CodMotivoCargoItem = frm.obe.CodMotivoCargoItem;
                    obe.FactorCargoItem = frm.obe.FactorCargoItem;
                    obe.MontoCargoItem = frm.obe.MontoCargoItem;
                    obe.BaseCargoItem = frm.obe.BaseCargoItem;
                    obe.MontoTotalImpuestosItem = frm.obe.MontoTotalImpuestosItem;
                    obe.MontoImpuestoIgvItem = frm.obe.MontoImpuestoIgvItem;
                    obe.MontoAfectoImpuestoIgv = frm.obe.MontoAfectoImpuestoIgv;
                    obe.MontoInafectoItem = frm.obe.MontoInafectoItem;
                    obe.PorcentajeIGVItem = frm.obe.PorcentajeIGVItem;
                    obe.MontoImpuestoISCItem = frm.obe.MontoImpuestoISCItem;
                    obe.MontoAfectoImpuestoIsc = frm.obe.MontoAfectoImpuestoIsc;
                    obe.PorcentajeISCtem = frm.obe.PorcentajeISCtem;
                    obe.MontoImpuestoIVAPtem = frm.obe.MontoImpuestoIVAPtem;
                    obe.MontoAfectoImpuestoIVAPItem = frm.obe.MontoAfectoImpuestoIVAPItem;
                    obe.PorcentajeIVAPItem = frm.obe.PorcentajeIVAPItem;
                    obe.descripcion = frm.obe.descripcion;
                    obe.codigoItem = frm.obe.codigoItem;
                    obe.ObservacionesItem = frm.obe.ObservacionesItem;
                    obe.ValorUnitarioItem = frm.obe.ValorUnitarioItem;
                    obe.tipoImpuesto = frm.obe.tipoImpuesto;
                    obe.UMedida = frm.obe.UMedida;
                    obe.PrecioVentaUnitarioItem = frm.obe.PrecioVentaUnitarioItem;
                    #endregion
                    CalcularCant();
                    setTotales();
                    grdFactura.RefreshDataSource();
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaDet obe = (EBoletaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstBoletaDetalle.Remove(obe);
            viewFactura.RefreshData();
            setTotales();
        }

        private void nuevoServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoServicio();
        }

        private void nuevoServicio()
        {
            using (frmManteBoletaServicioDetalle frm = new frmManteBoletaServicioDetalle())
            {
                frm.SetInsert();
                frm.lstBoletaDetalle = lstBoletaDetalle;
                frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
                frm.txtItem.Text = (lstBoletaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstBoletaDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstBoletaDetalle = frm.lstBoletaDetalle;
                    viewFactura.RefreshData();
                    viewFactura.MoveLast();
                    setTotales();

                }
            }
        }

        private void modificarServicio()
        {
            EBoletaDet obe = (EBoletaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteBoletaServicioDetalle frm = new frmManteBoletaServicioDetalle())
            {
                frm.obe = obe;
                frm.lstBoletaDetalle = lstBoletaDetalle;
                frm.SetModify();
                frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
                frm.txtItem.Text = String.Format("{0:000}", obe.bovd_iitem_boleta);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstBoletaDetalle = frm.lstBoletaDetalle;
                    viewFactura.RefreshData();
                    viewFactura.MoveLast();
                    setTotales();
                }
            }
        }

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void grdFactura_MouseMove(object sender, MouseEventArgs e)
        {
            //this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }

        private void dteFecha_EditValueChanged(object sender, EventArgs e)
        {
            dteVencimiento.EditValue = Convert.ToDateTime(dteFecha.EditValue).AddDays(Convert.ToInt32(Numero_dias_vencimiento_cliente));
        }

        private void txtSerie_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void BtnGuiaRemision_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarGuiaRemision();
        }
        private void listarGuiaRemision()
        {
            try
            {
                using (FrmListarGuiaRemision frm = new FrmListarGuiaRemision())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {


                        lstBoletaDetalle.Clear();


                        BtnGuiaRemision.Tag = frm._Be.remic_icod_remision;
                        BtnGuiaRemision.Text = frm._Be.remic_vnumero_remision;

                        List<EGuiaRemisionDet> mListGuiaDet = new List<EGuiaRemisionDet>();
                        mListGuiaDet = new BVentas().listarGuiaRemisionDet(frm._Be.remic_icod_remision, Parametros.intEjercicio);
                        foreach (var _BEguia in mListGuiaDet)
                        {
                            EBoletaDet _BEe = new EBoletaDet();
                            _BEe.prdc_icod_producto = _BEguia.prdc_icod_producto;
                            _BEe.bovd_iitem_boleta = _BEguia.dremc_inro_item;
                            _BEe.strCodProducto = _BEguia.strCodProducto;
                            _BEe.bovd_vdescripcion = _BEguia.strDesProducto;
                            _BEe.bovd_nprecio_unitario_item = 0;
                            _BEe.bovd_ncantidad = (_BEguia.dremc_ncantidad_producto);
                            _BEe.bovd_nmonto_impuesto_item = 0;
                            _BEe.bovd_nporcentaje_descuento_item = 0;
                            _BEe.bovd_nprecio_total_item = 0;
                            _BEe.bolvd_vobservaciones = _BEguia.dremc_vobservaciones;
                            _BEe.intUsuario = Valores.intUsuario;
                            _BEe.almac_icod_almacen = frm._Be.almac_icod_almacen;
                            _BEe.strAlmacen = frm._Be.strDesAlmacen;

                            _BEe.strDesProducto = _BEguia.strDesProducto;
                            _BEe.prdc_vpart_number = _BEguia.prdc_vpart_number;
                            _BEe.strDesUM = _BEguia.strDesUM;
                            _BEe.strCategoria = _BEguia.strCategoria;
                            _BEe.strSubCategoriaUno = _BEguia.strSubCategoriaUno;
                            _BEe.intTipoOperacion = 1;
                            lstBoletaDetalle.Add(_BEe);

                        }

                        nuevoToolStripMenuItem.Enabled = false;
                        eliminarToolStripMenuItem.Enabled = false;
                        nuevoServicioToolStripMenuItem.Enabled = false;

                        viewFactura.RefreshData();
                        setTotales();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
            }
        }

        private void bteVendedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarVendedor();
        }
        public void listarVendedor()
        {
            try
            {
                using (frmListarVendedor frm = new frmListarVendedor())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteVendedor.Tag = frm._Be.vdrsc_icod_vendedor;
                        bteVendedor.Text = frm._Be.vdrsc_vnombre_vendedor;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncodigoalmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit texto = (ButtonEdit)sender;
            if (texto.Name == "btncodigoalmacen")
            {
                using (FrmListarAlmacen Almacen = new FrmListarAlmacen())
                {
                    Almacen.puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                    if (Almacen.ShowDialog() == DialogResult.OK)
                    {
                        btncodigoalmacen.Tag = Almacen._Be.id_almacen;
                        btncodigoalmacen.Text = Almacen._Be.descripcion;
                        //GenerarCodigo();
                    }
                }
            }
        }

        private void cbIncluyeIGV_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbIncluyeIGV.Checked)
            {
                txtPorcentIGV.Text = "0.0";
            }
            else
            {
                txtPorcentIGV.Text = Parametros.strPorcIGV;
            }
        }
    }
}