using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Operaciones;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmManteFactura : DevExpress.XtraEditors.XtraForm
    {
        public EFacturaCab oBe = new EFacturaCab();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public List<EFacturaCab> oDetail;
        List<EFacturaDet> lstFacturaDetalle = new List<EFacturaDet>();
        List<EFacturaDet> lstDelete = new List<EFacturaDet>();
        public List<ECuotasFactura> listaCuotas = new List<ECuotasFactura>();
        public List<ECuotasFactura> listaCuotasEliminar = new List<ECuotasFactura>();


        List<EFacturaMPDet> lstFacturaMPDetalle = new List<EFacturaMPDet>();
        List<EFacturaMPDet> lstDeleteMP = new List<EFacturaMPDet>();
        List<EPuntoVenta> lstPuntoVenta = new List<EPuntoVenta>();

        string strCodCliente = "";
        int Numero_dias_vencimiento_cliente = 0;
        public int TipodeFactura = 0;
        public int TDComercial = 0;
        public string PorIGV;
        public string CodigoSUNAT { get; set; }
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
        public void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.Enabled = Enabled;
                txtNumero.Enabled = Enabled;
                lkpMoneda.Enabled = Enabled;
                BtnGuiaRemision.Enabled = false;
            }
        }

        public void setValues()
        {
            cbIncluyeIGV.Checked = oBe.favc_bincluye_igv;
            txtSerie.Text = oBe.favc_vnumero_factura.Substring(0, 4);
            txtNumero.Text = oBe.favc_vnumero_factura.Substring(4, 8);
            dteFecha.EditValue = oBe.favc_sfecha_factura;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion;
            bteCliente.Tag = oBe.favc_icod_cliente;
            bteCliente.Text = oBe.cliec_vnombre_cliente;
            txtRUC.Text = oBe.favc_vruc;
            txtDireccion.Text = oBe.favc_vdireccion_cliente;
            txtTelefono.Text = oBe.strTelefonoCliente;
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            txtPorcentIGV.Text = oBe.favc_npor_imp_igv.ToString();
            lkpFormaPago.EditValue = oBe.tablc_iid_forma_pago;
            txtMontoNeto.Text = oBe.favc_nmonto_neto.ToString();
            txtMontoIGV.Text = oBe.favc_nmonto_imp.ToString();
            txtMontoTotal.Text = oBe.favc_nmonto_total.ToString();
            dteVencimiento.EditValue = oBe.favc_sfecha_vencim_factura;
            TipodeFactura = Convert.ToInt32(oBe.favc_tipo_factura);
            BtnGuiaRemision.Tag = oBe.remic_icod_remision;
            BtnGuiaRemision.Text = oBe.favc_vnumero_guia;
            bteVendedor.Tag = oBe.vendc_icod_vendedor;
            bteVendedor.Text = oBe.NomVendedor;
            btncodigoalmacen.Tag = oBe.favc_iid_almacen;
            //btncodigoalmacen.Text = oBe.favc_viid_almacen;
            btncodigoalmacen.Text = oBe.DesAlmacen;
            txtNumOC.Text = oBe.favc_vnumero_orden;
            txtGuia.Text = oBe.favc_vnumero_guia;
            lkpPuntoVenta.EditValue = oBe.puvec_icod_punto_venta;
            lkpPuntoVenta.Text = oBe.puvec_icod_punto_venta.ToString();
            txtNumOP.Text = oBe.favc_vnumero_orden_pedido;
            txtMontoCredito.Text = oBe.facv_nmonto_credito.ToString();
            txtMonto1raCuota.Text = oBe.facv_nmonto_1era_cuota.ToString();
            dteFechaPago1raCuota.EditValue = oBe.facv_sfecha_pago_1era_cuota;
            txtPorcRetencion.Text = oBe.facv_nporc_retencion.ToString();
            txtMontoRetencion.Text = oBe.facv_nmonto_retencion.ToString();

            setCliente(oBe.favc_icod_cliente);


            if (oBe.favc_tipo_factura == 1)
            {

                lstFacturaDetalle = new BVentas().listarFacturaDetalle(oBe.favc_icod_factura);
                viewFacturaMercaderia.RefreshData();
                txtcantidadTotal.Text = lstFacturaDetalle.Sum(x => x.favd_ncantidad_total_producto).ToString();
            }
            else if (oBe.favc_tipo_factura == 2)
            {

                lstFacturaMPDetalle = new BVentas().listarFacturaMPDetalle(oBe.favc_icod_factura);
                grdFacturaSuministros.DataSource = lstFacturaMPDetalle;
                viewFacturaSuministros.RefreshData();
                txtcantidadTotal.Text = lstFacturaMPDetalle.Sum(x => x.favd_ncantidad).ToString();
            }
            else
            {
                lstFacturaMPDetalle = new BVentas().listarFacturaMPDetalle(oBe.favc_icod_factura);
                grdAlquileres.DataSource = lstFacturaMPDetalle;
                viewAlquileres.RefreshData();
                txtcantidadTotal.Text = lstFacturaMPDetalle.Sum(x => x.favd_ncantidad).ToString();
            }

            //lstFacturaDetalle = new BVentas().listarFacturaDetalle(oBe.favc_icod_factura);
            Numero_dias_vencimiento_cliente = oBe.cliec_nnumero_dias;
            //txtcantidadTotal.Text = lstFacturaDetalle.Sum(x => x.favd_ncantidad_total_producto).ToString();
            if (oBe.remic_icod_remision != 0)
            {
                nuevoToolStripMenuItem.Enabled = false;
                eliminarToolStripMenuItem1.Enabled = false;
                //nuevoServicioToolStripMenuItem.Enabled = false;
            }
            listaCuotas = new BVentas().CuotasFacturaListar(oBe.doxcc_icod_correlativo);
        }

        public FrmManteFactura()
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


            if (oBe.favc_tipo_factura == 1 || TipodeFactura == 1)
            {

                xtraTabControl1.SelectedTabPage = xtraMercaderia;
                xtraSuministros.PageEnabled = false;
                xtraAlquileres.PageEnabled = false;
            }
            else if (oBe.favc_tipo_factura == 2 || TipodeFactura == 2)
            {
                xtraTabControl1.SelectedTabPage = xtraSuministros;
                xtraMercaderia.PageEnabled = false;
                xtraAlquileres.PageEnabled = false;
                //btncodigoalmacen.Enabled = false;

            }
            else
            {
                xtraTabControl1.SelectedTabPage = xtraAlquileres;
                xtraSuministros.PageEnabled = false;
                xtraMercaderia.PageEnabled = false;
                btncodigoalmacen.Enabled = false;
            }
            grdFacturaMercaderia.DataSource = lstFacturaDetalle;


        }
        public void CargarControles()
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionDocumento), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda).Where(x => x.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpFormaPago, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaFormaPago).Where(x => x.tarec_iid_tabla_registro == 116 || x.tarec_iid_tabla_registro == 117
                ).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpPuntoVenta, new BCentral().ListarPuntoVenta(), "puvec_vdescripcion", "puvec_icod_punto_venta", true);
        }

        string serieOriginal;

        private void getNroDoc()
        {
            try
            {


                if (TDComercial == 680)
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
                else if (TDComercial == 681)
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
                else if (TDComercial == 682)
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





                serieOriginal = txtSerie.Text;

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
            if (TipodeFactura == 1 || oBe.favc_tipo_factura == 1)
            {
                BaseEdit oBase = null;
                Boolean Flag = true;
                try
                {
                    FrmFacturaDetalle frmdetalle = new FrmFacturaDetalle();
                    frmdetalle.indicador = 1;
                    frmdetalle.icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                    frmdetalle.icod_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                    frmdetalle.txtitem.Text = (lstFacturaDetalle.Count + 1).ToString();
                    frmdetalle.flag_afecto_igv = Convert.ToBoolean(cbIncluyeIGV.Checked);
                    frmdetalle.btnModificar.Enabled = false;
                    frmdetalle.btnAgregar.Enabled = true;
                    frmdetalle.SetInsert();
                    if (frmdetalle.ShowDialog() == DialogResult.OK)
                    {
                        EFacturaDet _obe = new EFacturaDet();
                        _obe.almac_icod_almacen = frmdetalle._BE.almac_icod_almacen;
                        _obe.favd_iitem_factura = frmdetalle._BE.favd_iitem_factura;
                        _obe.tablc_iid_motivo = frmdetalle._BE.tablc_iid_motivo;
                        _obe.favd_vcodigo_extremo_prod = frmdetalle._BE.favd_vcodigo_extremo_prod;
                        _obe.codigo_producto = frmdetalle._BE.codigo_producto;
                        _obe.favd_vdescripcion_prod = frmdetalle._BE.favd_vdescripcion_prod;
                        _obe.favd_icod_serie = frmdetalle._BE.favd_icod_serie;
                        _obe.favd_rango_talla = frmdetalle._BE.favd_rango_talla;
                        _obe.resec_vdescripcion = frmdetalle._BE.resec_vdescripcion;
                        _obe.favd_iid_moneda = frmdetalle._BE.favd_iid_moneda;
                        _obe.favd_ncantidad_total_producto = frmdetalle._BE.favd_ncantidad_total_producto;
                        _obe.favd_monto_unit = frmdetalle._BE.favd_monto_unit;
                        _obe.favd_monto_total = frmdetalle._BE.favd_monto_total;
                        _obe.favd_nprecio_unitario_item = frmdetalle._BE.favd_nprecio_unitario_item;
                        _obe.favd_nmonto_impuesto_item = frmdetalle._BE.favd_nmonto_impuesto_item;
                        _obe.favd_nneto_igv = frmdetalle._BE.favd_nneto_igv;
                        _obe.favd_nneto_exo = frmdetalle._BE.favd_nneto_exo;
                        _obe.favd_nprecio_total_item = frmdetalle._BE.favd_nprecio_total_item;
                        _obe.favd_talla1 = frmdetalle._BE.favd_talla1;
                        _obe.favd_talla2 = frmdetalle._BE.favd_talla2;
                        _obe.favd_talla3 = frmdetalle._BE.favd_talla3;
                        _obe.favd_talla4 = frmdetalle._BE.favd_talla4;
                        _obe.favd_talla5 = frmdetalle._BE.favd_talla5;
                        _obe.favd_talla6 = frmdetalle._BE.favd_talla6;
                        _obe.favd_talla7 = frmdetalle._BE.favd_talla7;
                        _obe.favd_talla8 = frmdetalle._BE.favd_talla8;
                        _obe.favd_talla9 = frmdetalle._BE.favd_talla9;
                        _obe.favd_talla10 = frmdetalle._BE.favd_talla10;
                        _obe.favd_cant_talla1 = frmdetalle._BE.favd_cant_talla1;
                        _obe.favd_cant_talla2 = frmdetalle._BE.favd_cant_talla2;
                        _obe.favd_cant_talla3 = frmdetalle._BE.favd_cant_talla3;
                        _obe.favd_cant_talla4 = frmdetalle._BE.favd_cant_talla4;
                        _obe.favd_cant_talla5 = frmdetalle._BE.favd_cant_talla5;
                        _obe.favd_cant_talla6 = frmdetalle._BE.favd_cant_talla6;
                        _obe.favd_cant_talla7 = frmdetalle._BE.favd_cant_talla7;
                        _obe.favd_cant_talla8 = frmdetalle._BE.favd_cant_talla8;
                        _obe.favd_cant_talla9 = frmdetalle._BE.favd_cant_talla9;
                        _obe.favd_cant_talla10 = frmdetalle._BE.favd_cant_talla10;
                        _obe.favd_icod_producto1 = frmdetalle._BE.favd_icod_producto1;
                        _obe.favd_icod_producto2 = frmdetalle._BE.favd_icod_producto2;
                        _obe.favd_icod_producto3 = frmdetalle._BE.favd_icod_producto3;
                        _obe.favd_icod_producto4 = frmdetalle._BE.favd_icod_producto4;
                        _obe.favd_icod_producto5 = frmdetalle._BE.favd_icod_producto5;
                        _obe.favd_icod_producto6 = frmdetalle._BE.favd_icod_producto6;
                        _obe.favd_icod_producto7 = frmdetalle._BE.favd_icod_producto7;
                        _obe.favd_icod_producto8 = frmdetalle._BE.favd_icod_producto8;
                        _obe.favd_icod_producto9 = frmdetalle._BE.favd_icod_producto9;
                        _obe.favd_icod_producto10 = frmdetalle._BE.favd_icod_producto10;
                        _obe.unidc_icod_unidad_medida = frmdetalle._BE.unidc_icod_unidad_medida;
                        _obe.flag_afecto_igv = frmdetalle._BE.flag_afecto_igv;
                        _obe.operacion = 1;
                        #region Factura Electronica Detalle
                        _obe.NumeroOrdenItem = frmdetalle._BE.NumeroOrdenItem;
                        _obe.cantidad = frmdetalle._BE.cantidad;
                        _obe.unidadMedida = frmdetalle._BE.unidadMedida;
                        _obe.ValorVentaItem = frmdetalle._BE.ValorVentaItem;
                        _obe.CodMotivoDescuentoItem = frmdetalle._BE.CodMotivoDescuentoItem;
                        _obe.FactorDescuentoItem = frmdetalle._BE.FactorDescuentoItem;
                        _obe.DescuentoItem = frmdetalle._BE.DescuentoItem;
                        _obe.BaseDescuentotem = frmdetalle._BE.BaseDescuentotem;
                        _obe.CodMotivoCargoItem = frmdetalle._BE.CodMotivoCargoItem;
                        _obe.FactorCargoItem = frmdetalle._BE.FactorCargoItem;
                        _obe.MontoCargoItem = frmdetalle._BE.MontoCargoItem;
                        _obe.BaseCargoItem = frmdetalle._BE.BaseCargoItem;
                        _obe.MontoTotalImpuestosItem = frmdetalle._BE.MontoTotalImpuestosItem;
                        _obe.MontoImpuestoIgvItem = frmdetalle._BE.MontoImpuestoIgvItem;
                        _obe.MontoAfectoImpuestoIgv = Math.Round(frmdetalle._BE.MontoAfectoImpuestoIgv, 2);
                        _obe.MontoInafectoItem = frmdetalle._BE.MontoInafectoItem;
                        _obe.PorcentajeIGVItem = frmdetalle._BE.PorcentajeIGVItem;
                        _obe.MontoImpuestoISCItem = frmdetalle._BE.MontoImpuestoISCItem;
                        _obe.MontoAfectoImpuestoIsc = frmdetalle._BE.MontoAfectoImpuestoIsc;
                        _obe.PorcentajeISCtem = frmdetalle._BE.PorcentajeISCtem;
                        _obe.MontoImpuestoIVAPtem = frmdetalle._BE.MontoImpuestoIVAPtem;
                        _obe.MontoAfectoImpuestoIVAPItem = frmdetalle._BE.MontoAfectoImpuestoIVAPItem;
                        _obe.PorcentajeIVAPItem = frmdetalle._BE.PorcentajeIVAPItem;
                        _obe.descripcion = frmdetalle._BE.descripcion;
                        _obe.codigoItem = frmdetalle._BE.codigoItem;
                        _obe.ObservacionesItem = frmdetalle._BE.ObservacionesItem;
                        _obe.ValorUnitarioItem = frmdetalle._BE.ValorUnitarioItem;
                        _obe.tipoImpuesto = frmdetalle._BE.tipoImpuesto;
                        _obe.UMedida = frmdetalle._BE.UMedida;
                        _obe.PrecioVentaUnitarioItem = frmdetalle._BE.PrecioVentaUnitarioItem;
                        #endregion
                        lstFacturaDetalle.Add(_obe);

                        if (Convert.ToDecimal(frmdetalle.PorIGV) != 0)
                        {
                            PorIGV = frmdetalle.PorIGV;
                        }
                        CalcularCant();
                        setTotal();
                        grdFacturaMercaderia.DataSource = lstFacturaDetalle;
                        grdFacturaMercaderia.RefreshDataSource();

                    }
                }
                catch (Exception ex)
                {
                    if (oBase != null)
                    {
                        oBase.Focus();
                        //oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
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

                    }
                }

            }
            else if (TipodeFactura == 2 || oBe.favc_tipo_factura == 2)
            {
                using (frmManteFacturaDetalle frm = new frmManteFacturaDetalle())
                {
                    frm.SetInsert();

                    frm.lstFacturaDetalle = lstFacturaMPDetalle;
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.TipodeFactura = TipodeFactura;
                    frm.flag_afecto_igv = Convert.ToBoolean(cbIncluyeIGV.Checked);
                    frm.intAlmacen = Convert.ToInt32(btncodigoalmacen.Tag);
                    frm.txtItem.Text = (lstFacturaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstFacturaDetalle.Count + 1);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (Convert.ToDecimal(frm.PorIGV) != 0)
                        {
                            PorIGV = frm.PorIGV;
                        }
                        lstFacturaMPDetalle = frm.lstFacturaDetalle;
                        grdFacturaSuministros.DataSource = lstFacturaMPDetalle;
                        viewFacturaSuministros.RefreshData();
                        viewFacturaSuministros.MoveLast();
                        setTotal();
                    }
                }
            }
            else
            {
                using (frmManteFacturaDetalle frm = new frmManteFacturaDetalle())
                {
                    frm.SetInsert();

                    frm.lstFacturaDetalle = lstFacturaMPDetalle;
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.TipodeFactura = TipodeFactura;
                    frm.txtItem.Text = (lstFacturaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstFacturaDetalle.Count + 1);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (Convert.ToDecimal(frm.PorIGV) != 0)
                        {
                            PorIGV = frm.PorIGV;
                        }
                        lstFacturaMPDetalle = frm.lstFacturaDetalle;
                        grdAlquileres.DataSource = lstFacturaMPDetalle;
                        viewAlquileres.RefreshData();
                        viewAlquileres.MoveLast();
                        setTotal();
                    }
                }
            }
        }

        public void CalcularCant()
        {
            if (oBe.favc_tipo_factura == 1 || TipodeFactura == 1)
            {
                txtcantidadTotal.Text = (lstFacturaDetalle.Sum(ob => ob.favd_ncantidad_total_producto)).ToString();
            }
            else if (oBe.favc_tipo_factura == 2 || TipodeFactura == 2)
            {
                txtcantidadTotal.Text = (lstFacturaMPDetalle.Sum(ob => ob.favd_ncantidad)).ToString();
            }
            else
            {
                txtcantidadTotal.Text = (lstFacturaMPDetalle.Sum(ob => ob.favd_ncantidad)).ToString();
            }
        }

        private void nuevoServicio()
        {
            using (frmManteFacturaServicioDetalle frm = new frmManteFacturaServicioDetalle())
            {
                frm.SetInsert();
                frm.lstFacturaDetalle = lstFacturaDetalle;
                frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
                frm.txtItem.Text = (lstFacturaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstFacturaDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstFacturaDetalle = frm.lstFacturaDetalle;
                    viewFacturaMercaderia.RefreshData();
                    viewFacturaMercaderia.MoveLast();
                    setTotal();

                }
            }
        }

        private void modificarServicio()
        {
            EFacturaDet obe = (EFacturaDet)viewFacturaMercaderia.GetRow(viewFacturaMercaderia.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteFacturaServicioDetalle frm = new frmManteFacturaServicioDetalle())
            {
                frm.obe = obe;
                frm.lstFacturaDetalle = lstFacturaDetalle;
                frm.SetModify();
                frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
                frm.txtItem.Text = String.Format("{0:000}", obe.favd_iitem_factura);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstFacturaDetalle = frm.lstFacturaDetalle;
                    viewFacturaMercaderia.RefreshData();
                    viewFacturaMercaderia.MoveLast();
                    setTotal();
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
                        txtRUC.Text = frm._Be.cliec_cruc;
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

                        txtPorcRetencion.ReadOnly = !frm._Be.cliec_bagente_retenedor;
                        txtMontoRetencion.ReadOnly = !frm._Be.cliec_bagente_retenedor;

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
            var _Be = new BVentas().ListarCliente().Where(x => x.cliec_icod_cliente == intCliente).ToList()[0];

            txtPorcRetencion.ReadOnly = !_Be.cliec_bagente_retenedor;
            txtMontoRetencion.ReadOnly = !_Be.cliec_bagente_retenedor;


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
                    throw new ArgumentException("Ingrese Nro. de Serie de Factura");
                }

                if (txtSerie.Text == "0000")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("N° de Serie no registrado, registrar N° serie en REGISTRO DE TIPOS DE DOCUMENTOS");
                }

                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Factura");
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

                if (String.IsNullOrWhiteSpace(txtRUC.Text))
                {
                    oBase = txtRUC;
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
                if (Convert.ToInt32(lkpFormaPago.EditValue) == 117)
                {
                    if (listaCuotas.Count == 0)
                    {
                        oBase = lkpFormaPago;
                        throw new ArgumentException("Ingrese las Cuotas");
                    }
                    else
                    {
                        var cuotas = listaCuotas.First();
                        txtMonto1raCuota.Text = cuotas.fcc_nmonto.ToString();
                        dteFechaPago1raCuota.EditValue = cuotas.fcc_sfecha_pago;
                    }
                }
                PorIGV = txtPorcentIGV.Text;
                oBe.actualizarSerie = serieOriginal == txtSerie.Text;
                oBe.favc_vnumero_factura = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.favc_sfecha_factura = Convert.ToDateTime(dteFecha.Text);
                oBe.favc_sfecha_vencim_factura = Convert.ToDateTime(dteVencimiento.Text);
                oBe.favc_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.cliec_vnombre_cliente = bteCliente.Text;
                oBe.favc_vdireccion_cliente = txtDireccion.Text;
                oBe.favc_vruc = txtRUC.Text;
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.tablc_iid_forma_pago = Convert.ToInt32(lkpFormaPago.EditValue);
                oBe.tablc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.vendc_icod_vendedor = Convert.ToInt32(bteVendedor.Tag);
                oBe.puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                oBe.favc_iid_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                oBe.favc_tipo_factura = TipodeFactura;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                oBe.remic_icod_remision = Convert.ToInt32(BtnGuiaRemision.Tag);
                oBe.favc_vnumero_orden = txtNumOC.Text;
                oBe.favc_vnumero_guia = BtnGuiaRemision.Text;

                oBe.favc_vnumero_orden_pedido = txtNumOP.Text;


                oBe.favc_bincluye_igv = cbIncluyeIGV.Checked;

                if (Convert.ToDecimal(PorIGV) > 0)
                {
                    oBe.favc_noperacion_gravadas = Convert.ToDecimal(txtMontoNeto.Text);
                    oBe.favc_npor_imp_igv = Convert.ToDecimal(PorIGV);
                    oBe.favc_noperacion_inafectas = 0;
                }
                else
                {
                    oBe.favc_noperacion_inafectas = Convert.ToDecimal(txtMontoTotal.Text);
                }
                oBe.favc_nmonto_imp = Convert.ToDecimal(txtMontoIGV.Text);
                oBe.favc_nmonto_neto = Convert.ToDecimal(txtMontoNeto.Text);

                oBe.favc_nmonto_total = Convert.ToDecimal(txtMontoTotal.Text);



                oBe.favc_tipo_factura = TipodeFactura;

                oBe.favc_icod_pvt = 2;//Central

                oBe.facv_nmonto_credito = Convert.ToDecimal(txtMontoCredito.Text);
                oBe.facv_nmonto_1era_cuota = Convert.ToDecimal(txtMonto1raCuota.Text);

                if (dteFechaPago1raCuota.DateTime == null || dteFechaPago1raCuota.Text == "" || dteFechaPago1raCuota.Text == "01/01/0001")
                {
                    oBe.facv_sfecha_pago_1era_cuota = (DateTime?)null;
                }
                else
                {
                    oBe.facv_sfecha_pago_1era_cuota = Convert.ToDateTime(dteFechaPago1raCuota.DateTime);
                }

                oBe.facv_nporc_retencion = Convert.ToDecimal(txtPorcRetencion.Text);
                oBe.facv_nmonto_retencion = Convert.ToDecimal(txtMontoRetencion.Text);

                #region Facturacion Electronica
                oBe.idDocumento = oBe.favc_vnumero_factura.Remove(4, 8) + '-' + oBe.favc_vnumero_factura.Remove(0, 4);
                oBe.fechaEmision = oBe.favc_sfecha_factura.ToString("dd/MM/yyyy hh:mm tt");
                oBe.fechaVencimiento = oBe.favc_sfecha_vencim_factura.ToString("dd/MM/yyyy hh:mm tt");
                oBe.tipoDocumento = "01";
                if (Convert.ToInt32(lkpMoneda.EditValue) == 3)
                {
                    oBe.moneda = "PEN";
                }
                else
                {
                    oBe.moneda = "USD";
                }
                
                oBe.cantidadItems = lstFacturaDetalle.Count;
                oBe.nombreComercialEmisor = Valores.strNombreEmpresa;
                oBe.nombreLegalEmisor = "CALZADOS JAGUAR SAC";
                oBe.tipoDocumentoEmisor = "6";
                oBe.nroDocumentoEmisior = Valores.strRUC;
                oBe.CodLocalEmisor = "0000";
                oBe.nroDocumentoReceptor = txtRUC.Text;
                oBe.tipoDocumentoReceptor = "6";
                oBe.nombreLegalReceptor = bteCliente.Text;
                oBe.direccionReceptor = txtDireccion.Text;
                oBe.CodMotivoDescuento = 0;
                oBe.PorcDescuento = 0;
                oBe.MontoDescuentoGlobal = 0;
                oBe.BaseMontoDescuento = 0;
                oBe.MontoTotalImpuesto = oBe.favc_nmonto_imp;
                oBe.MontoGravadasIGV = oBe.favc_nmonto_neto;
                oBe.CodigoTributo = 1000;
                oBe.MontoExonerado = 0;
                oBe.MontoInafecto = oBe.favc_noperacion_inafectas;
                oBe.MontoGratuitoImpuesto = 0;
                oBe.MontoBaseGratuito = 0;
                oBe.totalIgv = oBe.favc_nmonto_imp;
                oBe.MontoGravadosISC = 0;
                oBe.totalIsc = 0;
                oBe.MontoGravadosOtros = 0;
                oBe.totalOtrosTributos = 0;
                oBe.TotalValorVenta = oBe.favc_noperacion_gravadas + oBe.favc_noperacion_inafectas;
                oBe.TotalPrecioVenta = oBe.favc_nmonto_total;
                oBe.MontoDescuento = 0;
                oBe.MontoTotalCargo = 0;
                oBe.MontoTotalAnticipo = 0;
                oBe.ImporteTotalVenta = oBe.favc_nmonto_total;
                oBe.EstadoFacturacion = 4;
                if (lstPuntoVenta.Count > 0)
                {
                    oBe.CodigoSunat = lstPuntoVenta[0].puvec_vcodigo_sunat;
                    oBe.UsuarioOSE = lstPuntoVenta[0].puvec_vusuario_ose;
                }
                if (Convert.ToInt32(lkpFormaPago.EditValue) != 117)
                {
                    oBe.FormaPagoS = "Contado";
                }
                else  
                {
                    oBe.FormaPagoS = "Credito";
                }

                oBe.MontoTotalPago = oBe.facv_nmonto_credito;

                oBe.MontoCuota = oBe.facv_nmonto_1era_cuota;
                oBe.FechaPago = oBe.facv_sfecha_pago_1era_cuota.ToString();


                #endregion

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (TipodeFactura == 1 || oBe.favc_tipo_factura == 1)
                    {

                        oBe.favc_icod_factura = new BVentas().insertarFactura(oBe, lstFacturaDetalle, listaCuotas);
                    }
                    else
                    {
                        oBe.favc_icod_factura = new BVentas().insertarFacturaServio(oBe, lstFacturaMPDetalle, listaCuotas);
                    }

                }
                else
                {
                    if (TipodeFactura == 1 || oBe.favc_tipo_factura == 1)
                    {

                        new BVentas().modificarFactura(oBe, lstFacturaDetalle, lstDelete, listaCuotas, listaCuotasEliminar);
                    }
                    else
                    {
                        new BVentas().modificarFacturaServicios(oBe, lstFacturaMPDetalle, lstDeleteMP, listaCuotas, listaCuotasEliminar);
                    }

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
                    MiEvento(oBe.favc_icod_factura);
                    Close();
                }
            }
        }
        private void modificarItem()
        {

            if (TipodeFactura == 1 || oBe.favc_tipo_factura == 1)
            {
                EFacturaDet obe = (EFacturaDet)viewFacturaMercaderia.GetRow(viewFacturaMercaderia.FocusedRowHandle);
                if (obe == null)
                    return;

                using (FrmFacturaDetalle frmdetalle = new FrmFacturaDetalle())
                {

                    frmdetalle.txtitem.Text = obe.favd_iitem_factura.ToString();
                    frmdetalle.favd_icod_item_factura = obe.favd_icod_item_factura;
                    frmdetalle.icod_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                    frmdetalle.icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                    frmdetalle.btncodigoProducto.Tag = obe.prdc_icod_producto;
                    frmdetalle.indicador = 0;
                    frmdetalle.btncodigoProducto.Text = obe.favd_vcodigo_extremo_prod;
                    frmdetalle.txtdescripcion.Text = obe.favd_vdescripcion_prod;
                    frmdetalle.descripcion = obe.favd_vdescripcion_prod;
                    frmdetalle.txtcantidaddetalle.Text = obe.favd_ncantidad_total_producto.ToString();
                    frmdetalle.txtprecio.Text = obe.favd_nprecio_unitario_item.ToString();
                    frmdetalle.txtPrecioVenta.Text = obe.favd_nprecio_total_item.ToString();
                    frmdetalle.txtDescuento.Text = obe.favd_nporcentaje_descuento_item.ToString();
                    frmdetalle.lkpSerie.EditValue = obe.favd_icod_serie;
                    frmdetalle.txtrangotallas.Text = obe.favd_rango_talla;
                    frmdetalle.txttalla1.Text = string.Format("{0:00}", obe.favd_talla1);
                    frmdetalle.txttalla2.Text = string.Format("{0:00}", obe.favd_talla2);
                    frmdetalle.txttalla3.Text = string.Format("{0:00}", obe.favd_talla3);
                    frmdetalle.txttalla4.Text = string.Format("{0:00}", obe.favd_talla4);
                    frmdetalle.txttalla5.Text = string.Format("{0:00}", obe.favd_talla5);
                    frmdetalle.txttalla6.Text = string.Format("{0:00}", obe.favd_talla6);
                    frmdetalle.txttalla7.Text = string.Format("{0:00}", obe.favd_talla7);
                    frmdetalle.txttalla8.Text = string.Format("{0:00}", obe.favd_talla8);
                    frmdetalle.txttalla9.Text = string.Format("{0:00}", obe.favd_talla9);
                    frmdetalle.txttalla10.Text = string.Format("{0:00}", obe.favd_talla10);
                    frmdetalle.txtcantidad1.Text = obe.favd_cant_talla1.ToString();
                    frmdetalle.txtcantidad2.Text = obe.favd_cant_talla2.ToString();
                    frmdetalle.txtcantidad3.Text = obe.favd_cant_talla3.ToString();
                    frmdetalle.txtcantidad4.Text = obe.favd_cant_talla4.ToString();
                    frmdetalle.txtcantidad5.Text = obe.favd_cant_talla5.ToString();
                    frmdetalle.txtcantidad6.Text = obe.favd_cant_talla6.ToString();
                    frmdetalle.txtcantidad7.Text = obe.favd_cant_talla7.ToString();
                    frmdetalle.txtcantidad8.Text = obe.favd_cant_talla8.ToString();
                    frmdetalle.txtcantidad9.Text = obe.favd_cant_talla9.ToString();
                    frmdetalle.txtcantidad10.Text = obe.favd_cant_talla10.ToString();
                    frmdetalle.icodProducto[0] = Convert.ToInt32(obe.favd_icod_producto1);
                    frmdetalle.icodProducto[1] = Convert.ToInt32(obe.favd_icod_producto2);
                    frmdetalle.icodProducto[2] = Convert.ToInt32(obe.favd_icod_producto3);
                    frmdetalle.icodProducto[3] = Convert.ToInt32(obe.favd_icod_producto4);
                    frmdetalle.icodProducto[4] = Convert.ToInt32(obe.favd_icod_producto5);
                    frmdetalle.icodProducto[5] = Convert.ToInt32(obe.favd_icod_producto6);
                    frmdetalle.icodProducto[6] = Convert.ToInt32(obe.favd_icod_producto7);
                    frmdetalle.icodProducto[7] = Convert.ToInt32(obe.favd_icod_producto8);
                    frmdetalle.icodProducto[8] = Convert.ToInt32(obe.favd_icod_producto9);
                    frmdetalle.icodProducto[9] = Convert.ToInt32(obe.favd_icod_producto10);

                    #region Factura Electronica Detalle
                    obe.NumeroOrdenItem = obe.favd_iitem_factura;
                    obe.cantidad = obe.favd_ncantidad_total_producto;
                    obe.unidadMedida = obe.CodigoSUNAT;
                    obe.ValorVentaItem = obe.ValorVentaItem;
                    obe.CodMotivoDescuentoItem = 0;
                    obe.FactorDescuentoItem = 0;
                    obe.DescuentoItem = 0;
                    obe.BaseDescuentotem = 0;
                    obe.CodMotivoCargoItem = 0;
                    obe.FactorCargoItem = 0;
                    obe.MontoCargoItem = 0;
                    obe.BaseCargoItem = 0;
                    obe.MontoTotalImpuestosItem = obe.favd_nmonto_impuesto_item;
                    obe.MontoImpuestoIgvItem = obe.favd_nmonto_impuesto_item;
                    obe.MontoAfectoImpuestoIgv = obe.favd_nprecio_neto_item;
                    obe.PorcentajeIGVItem = obe.favd_nporcentaje_descuento_item;
                    obe.MontoImpuestoISCItem = 0;
                    obe.MontoAfectoImpuestoIsc = 0;
                    obe.PorcentajeISCtem = 0;
                    obe.MontoImpuestoIVAPtem = 0;
                    obe.MontoAfectoImpuestoIVAPItem = 0;
                    obe.PorcentajeIVAPItem = 0;
                    obe.descripcion = obe.favd_vdescripcion_prod;
                    obe.codigoItem = obe.favd_vcodigo_extremo_prod;
                    obe.ObservacionesItem = obe.ObservacionesItem;
                    obe.ValorUnitarioItem = obe.ValorUnitarioItem;
                    obe.UMedida = obe.strDesUM;

                    #endregion
                    frmdetalle.btncodigoProducto.Enabled = false;
                    frmdetalle.lkpSerie.Enabled = false;
                    frmdetalle.btnGenerar.Enabled = false;
                    frmdetalle.btnAgregar.Enabled = false;
                    //frmdetalle.Redimencionarstock();
                    frmdetalle.cargarcantidadesOriginales();
                    frmdetalle.flag_afecto_igv = Convert.ToBoolean(cbIncluyeIGV.Checked);
                    frmdetalle.icodUM = obe.unidc_icod_unidad_medida;
                    frmdetalle.CodigoSUNAT = obe.CodigoSUNAT;
                    frmdetalle.strUM = obe.strDesUM;
                    frmdetalle.SetModify();
                    frmdetalle._BE = obe;
                    if (frmdetalle.ShowDialog() == DialogResult.OK)
                    {

                        obe.favd_ncantidad_total_producto = frmdetalle._BE.favd_ncantidad_total_producto;
                        obe.favd_nprecio_unitario_item = frmdetalle._BE.favd_nprecio_unitario_item;
                        obe.favd_nprecio_total_item = frmdetalle._BE.favd_nprecio_total_item;
                        obe.favd_talla1 = frmdetalle._BE.favd_talla1;
                        obe.favd_talla2 = frmdetalle._BE.favd_talla2;
                        obe.favd_talla3 = frmdetalle._BE.favd_talla3;
                        obe.favd_talla4 = frmdetalle._BE.favd_talla4;
                        obe.favd_talla5 = frmdetalle._BE.favd_talla5;
                        obe.favd_talla6 = frmdetalle._BE.favd_talla6;
                        obe.favd_talla7 = frmdetalle._BE.favd_talla7;
                        obe.favd_talla8 = frmdetalle._BE.favd_talla8;
                        obe.favd_talla9 = frmdetalle._BE.favd_talla9;
                        obe.favd_talla10 = frmdetalle._BE.favd_talla10;
                        obe.favd_cant_talla1 = frmdetalle._BE.favd_cant_talla1;
                        obe.favd_cant_talla2 = frmdetalle._BE.favd_cant_talla2;
                        obe.favd_cant_talla3 = frmdetalle._BE.favd_cant_talla3;
                        obe.favd_cant_talla4 = frmdetalle._BE.favd_cant_talla4;
                        obe.favd_cant_talla5 = frmdetalle._BE.favd_cant_talla5;
                        obe.favd_cant_talla6 = frmdetalle._BE.favd_cant_talla6;
                        obe.favd_cant_talla7 = frmdetalle._BE.favd_cant_talla7;
                        obe.favd_cant_talla8 = frmdetalle._BE.favd_cant_talla8;
                        obe.favd_cant_talla9 = frmdetalle._BE.favd_cant_talla9;
                        obe.favd_cant_talla10 = frmdetalle._BE.favd_cant_talla10;
                        if (obe.favd_icod_item_factura == 0)
                            obe.operacion = 1;
                        else
                        {
                            if (obe.operacion != 1)
                            {
                                obe.operacion = 2;
                            }
                        }
                        #region Factura Electronica Detalle
                        obe.NumeroOrdenItem = obe.favd_iitem_factura;
                        obe.cantidad = obe.favd_ncantidad_total_producto;
                        obe.unidadMedida = obe.unidadMedida;
                        obe.ValorVentaItem = obe.ValorVentaItem;
                        obe.CodMotivoDescuentoItem = 0;
                        obe.FactorDescuentoItem = 0;
                        obe.DescuentoItem = 0;
                        obe.BaseDescuentotem = 0;
                        obe.CodMotivoCargoItem = 0;
                        obe.FactorCargoItem = 0;
                        obe.MontoCargoItem = 0;
                        obe.BaseCargoItem = 0;
                        obe.MontoTotalImpuestosItem = obe.favd_nmonto_impuesto_item;
                        obe.MontoImpuestoIgvItem = obe.favd_nmonto_impuesto_item;
                        if (obe.favd_nmonto_impuesto_item == 0)
                        {
                            obe.MontoInafectoItem = obe.favd_nprecio_total_item;
                            obe.MontoAfectoImpuestoIgv = 0;
                        }
                        else
                        {
                            obe.MontoInafectoItem = 0;
                            obe.MontoAfectoImpuestoIgv = Math.Round(obe.favd_nneto_igv, 2);
                        }
                        obe.PorcentajeIGVItem = obe.PorcentajeIGVItem;
                        obe.MontoImpuestoISCItem = 0;
                        obe.MontoAfectoImpuestoIsc = 0;
                        obe.PorcentajeISCtem = 0;
                        obe.MontoImpuestoIVAPtem = 0;
                        obe.MontoAfectoImpuestoIVAPItem = 0;
                        obe.PorcentajeIVAPItem = 0;
                        obe.descripcion = obe.favd_vdescripcion_prod;
                        obe.codigoItem = obe.favd_vcodigo_extremo_prod;
                        obe.ObservacionesItem = obe.ObservacionesItem;
                        obe.ValorUnitarioItem = obe.MontoAfectoImpuestoIgv / obe.favd_ncantidad_total_producto;
                        obe.PrecioVentaUnitarioItem = (obe.favd_nprecio_total_item / obe.favd_ncantidad_total_producto);
                        if (obe.favd_nmonto_impuesto_item > 0)
                        {
                            obe.tipoImpuesto = "10";
                        }
                        else
                        {
                            obe.tipoImpuesto = "30";
                        }
                        obe.UMedida = obe.UMedida;
                        #endregion
                        CalcularCant();
                        setTotal();
                        grdFacturaMercaderia.RefreshDataSource();
                    }
                }



            }
            else if (TipodeFactura == 2 || oBe.favc_tipo_factura == 2)
            {
                EFacturaMPDet obe = (EFacturaMPDet)viewFacturaSuministros.GetRow(viewFacturaSuministros.FocusedRowHandle);
                if (obe == null)
                    return;
                using (frmManteFacturaDetalle frm = new frmManteFacturaDetalle())
                {
                    frm.obe = obe;
                    frm.remic_icod_remision = Convert.ToInt32(BtnGuiaRemision.Tag);
                    frm.dblStockDisponible = Convert.ToDecimal(obe.dblStockDisponible);
                    frm.flag_afecto_igv = Convert.ToBoolean(cbIncluyeIGV.Checked);
                    frm.lstFacturaDetalle = lstFacturaMPDetalle;
                    frm.SetModify();
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.txtItem.Text = String.Format("{0:000}", obe.favd_iitem_factura);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstFacturaMPDetalle = frm.lstFacturaDetalle;
                        viewFacturaMercaderia.RefreshData();
                        viewFacturaMercaderia.MoveLast();
                        setTotal();
                    }
                }

            }

            else
            {

                using (frmManteFacturaDetalle frm = new frmManteFacturaDetalle())
                {
                    frm.obe = viewAlquileres.GetFocusedRow() as EFacturaMPDet;
                    frm.lstFacturaDetalle = lstFacturaMPDetalle;
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.TipodeFactura = TipodeFactura;
                    frm.txtItem.Text = (lstFacturaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstFacturaDetalle.Count + 1);
                    frm.SetModify();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (Convert.ToDecimal(frm.PorIGV) != 0)
                        {
                            PorIGV = frm.PorIGV;
                        }
                        lstFacturaMPDetalle = frm.lstFacturaDetalle;
                        grdAlquileres.DataSource = lstFacturaMPDetalle;
                        viewAlquileres.RefreshData();
                        viewAlquileres.MoveLast();
                        setTotal();
                    }
                }

            }
        }


        private void setTotal()
        {
            if (TipodeFactura == 1)
            {
                txtMontoTotal.Text = lstFacturaDetalle.Sum(x => x.favd_nprecio_total_item).ToString();
                if (lstFacturaDetalle.Count > 0)
                {
                    txtMontoIGV.Text = lstFacturaDetalle.Sum(x => x.favd_nmonto_impuesto_item).ToString();
                    txtMontoNeto.Text = lstFacturaDetalle.Sum(x => x.favd_nneto_igv).ToString();
                    txtMontoNetoExo.Text = lstFacturaDetalle.Sum(x => x.favd_nneto_exo).ToString();
                    PorIGV = Parametros.strPorcIGV;


                }
                else
                {
                    txtMontoNeto.Text = "0.00";
                    txtMontoIGV.Text = "0.00";
                }
            }
            else
            {
                txtMontoTotal.Text = lstFacturaMPDetalle.Sum(x => x.favd_nprecio_total_item).ToString();
                if (lstFacturaMPDetalle.Count > 0)
                {
                    txtMontoIGV.Text = lstFacturaMPDetalle.Sum(x => x.favd_nmonto_impuesto_item).ToString();
                    txtMontoNeto.Text = lstFacturaMPDetalle.Sum(x => x.favd_nneto_igv).ToString();
                    txtMontoNetoExo.Text = lstFacturaMPDetalle.Sum(x => x.favd_nneto_exo).ToString();
                }
                else
                {
                    txtMontoNeto.Text = "0.00";
                    txtMontoIGV.Text = "0.00";
                }
            }

            if (Convert.ToInt32(lkpFormaPago.EditValue) == Constantes.PagoCredito && Convert.ToDecimal(txtPorcRetencion.Text) > 0)
            {
                if (Convert.ToDecimal(txtMontoTotal.Text) > 700)
                {
                    txtMontoRetencion.Text = Math.Round(Convert.ToDecimal(txtMontoTotal.Text) * Porcentretencion, 2).ToString();
                    txtMontoCredito.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtMontoRetencion.Text)).ToString();
                    txtMonto1raCuota.Text = txtMontoCredito.Text;
                }
                else
                {
                    txtMontoRetencion.Text = "0.00";
                    txtMontoCredito.Text = "0.00";
                    txtMonto1raCuota.Text = "0.00";

                }

            }

        }
        public decimal Porcentretencion = Convert.ToDecimal("0.0" + Constantes.PorcentajeRetencion.ToString().Replace(".", ""));
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

        private void bteVendedor_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarVendedor();
        }

        private void bteCliente_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void btncodigoalmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (TipodeFactura == 1 || oBe.favc_tipo_factura == 1)
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
                        }
                    }
                }
            }
            else
            {
                using (frmListarAlmacen frm = new frmListarAlmacen())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btncodigoalmacen.Tag = frm._Be.almac_icod_almacen;
                        btncodigoalmacen.Text = frm._Be.almac_vdescripcion;
                    }
                }
            }

        }

        private void btnGuardar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void FrmManteFactura_Load_1(object sender, EventArgs e)
        {
            cargar();
            lstPuntoVenta = new BCentral().ListarPuntoVenta().Where(x => x.puvec_icod_punto_venta == 2).ToList();
        }

        private void modificarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            modificarItem();
        }

        private void eliminarToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            EFacturaDet obe = (EFacturaDet)viewFacturaMercaderia.GetRow(viewFacturaMercaderia.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstFacturaDetalle.Remove(obe);
            viewFacturaMercaderia.RefreshData();
            setTotal();
        }

        private void lkpFormaPago_EditValueChanged(object sender, EventArgs e)
        {
            if (Status != BSMaintenanceStatus.View)
            {
                if (Convert.ToInt32(lkpFormaPago.EditValue) != 1)
                    spinDias.Enabled = true;
                else
                {
                    spinDias.Enabled = false;
                    spinDias.EditValue = "0";
                    dteVencimiento.EditValue = dteFecha.EditValue;
                }
                if (Convert.ToInt32(lkpFormaPago.EditValue) == 117)
                {
                    txtMontoCredito.Enabled = true;
                    txtMonto1raCuota.Enabled = true;
                    dteFechaPago1raCuota.Enabled = true;
                    txtMontoCredito.Text = "0.00";
                    txtMonto1raCuota.Text = "0.00";
                    simpleButton1.Enabled = true;

                }
                else if (Convert.ToInt32(lkpFormaPago.EditValue) != 117)
                {
                    txtMontoCredito.Enabled = false;
                    txtMonto1raCuota.Enabled = false;
                    dteFechaPago1raCuota.Enabled = false;

                    txtMontoCredito.Text = "";
                    txtMonto1raCuota.Text = "";
                    dteFechaPago1raCuota.EditValue = "";
                    simpleButton1.Enabled = false;

                }
                setTotal();
            }

        }

        private void spinDias_EditValueChanged(object sender, EventArgs e)
        {
            dteVencimiento.EditValue = Convert.ToDateTime(dteFecha.EditValue).AddDays(Convert.ToInt32(spinDias.EditValue));
        }

        private void lkpPuntoVenta_EditValueChanged(object sender, EventArgs e)
        {

        }
        private List<EProdProducto> mlist = new List<EProdProducto>();
        private void importarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EIntervalo> mlistadeSeries = new List<EIntervalo>();
            mlist = (new BCentral()).ListarProdProductos();

            List<EProdInventarioFisicoDetalle> listaInventarioDetalle = new List<EProdInventarioFisicoDetalle>();

            using (frmImportarProductosNotaIngreso frmCliente = new frmImportarProductosNotaIngreso())
            {
                if (frmCliente.ShowDialog() == DialogResult.OK)
                {
                    listaInventarioDetalle = frmCliente.ListaImportacion;

                    foreach (EProdInventarioFisicoDetalle obj in listaInventarioDetalle)
                    {
                        ArrayList tallasAculadas = new ArrayList();
                        foreach (EProdInventarioFisicoDetalle objAux in listaInventarioDetalle)
                        {
                            if (obj.pr_vcodigo_externo.Substring(0, 13).Trim() == objAux.pr_vcodigo_externo.Substring(0, 13).Trim() && objAux.flag == false)
                            {
                                tallasAculadas.Add(Convert.ToInt32(objAux.pr_vcodigo_externo.Substring(13, 2)));
                                objAux.flag = true;
                            }
                        }
                        object[] tallasAcumuladasArray = tallasAculadas.ToArray();
                        if (tallasAcumuladasArray.LongCount() > 0)
                        {
                            int TallaMayor = Convert.ToInt32(tallasAcumuladasArray[0]);
                            int TallaMenor = Convert.ToInt32(tallasAcumuladasArray[0]);
                            for (int i = 0; i < tallasAcumuladasArray.Length; i++)
                            {
                                if (Convert.ToInt32(tallasAcumuladasArray[i]) > TallaMayor)
                                {
                                    TallaMayor = Convert.ToInt32(tallasAcumuladasArray[i]);
                                }
                                if (Convert.ToInt32(tallasAcumuladasArray[i]) < TallaMenor)
                                {
                                    TallaMenor = Convert.ToInt32(tallasAcumuladasArray[i]);
                                }
                            }
                            EIntervalo objEintervalo = new EIntervalo();
                            objEintervalo.serie1 = TallaMenor;
                            objEintervalo.serie2 = TallaMayor;
                            objEintervalo.codigoExterno = obj.pr_vcodigo_externo.Substring(0, 13);
                            objEintervalo.Descripcion = obj.pr_vdescripcion_producto.Substring(0, obj.pr_vdescripcion_producto.Length - 2);
                            mlistadeSeries.Add(objEintervalo);
                        }
                    }
                    GuardarDetalleConCodigosImportados(listaInventarioDetalle, mlistadeSeries);
                }
            }
        }
        private void GuardarDetalleConCodigosImportados(List<EProdInventarioFisicoDetalle> mlitaProductos, List<EIntervalo> mlistaIntervalos)
        {
            int i = 1;
            foreach (EIntervalo objIntervalo in mlistaIntervalos)
            {
                ArrayList AcumuladoKardex = new ArrayList();
                ArrayList Tallas = new ArrayList();
                ArrayList cantidades = new ArrayList();
                ArrayList icodProducto = new ArrayList();

                foreach (EProdInventarioFisicoDetalle objProduc in mlitaProductos)
                {
                    if (objProduc.pr_vcodigo_externo.Substring(0, 13).Trim() == objIntervalo.codigoExterno.Trim())
                    {
                        string codigoexterno = objProduc.pr_vcodigo_externo;

                        Tallas.Add(objProduc.pr_vcodigo_externo.Substring(13, 2));
                        cantidades.Add(objProduc.infid_ncant_fisica);
                        icodProducto.Add(objProduc.pr_icod_producto);
                        //}
                    }
                }

                object[] TallasArray = Tallas.ToArray();
                object[] TallasOri = new object[10];
                for (int u = 0; u < 10; u++)
                {
                    if (TallasArray.Length > u)
                    {
                        TallasOri[u] = TallasArray[u];
                    }
                    else
                    {
                        TallasOri[u] = 0;
                    }
                }
                object[] CantidadesArray = cantidades.ToArray();
                object[] CantidadesOri = new object[10];
                for (int t = 0; t < 10; t++)
                {
                    if (CantidadesArray.Length > t)
                    {
                        CantidadesOri[t] = CantidadesArray[t];
                    }
                    else
                    {
                        CantidadesOri[t] = 0;
                    }
                }
                object[] icodProductoArray = cantidades.ToArray();
                object[] icodProductoOri = new object[10];
                for (int t = 0; t < 10; t++)
                {
                    if (icodProductoArray.Length > t)
                    {
                        icodProductoOri[t] = icodProductoArray[t];
                    }
                    else
                    {
                        icodProductoOri[t] = 0;
                    }
                }
                EFacturaDet objd = new EFacturaDet();
                objd.almac_icod_almacen = Convert.ToInt32(btncodigoalmacen.Tag);
                objd.favd_iitem_factura = i;
                objd.favd_vcodigo_extremo_prod = objIntervalo.codigoExterno;
                objd.favd_vdescripcion_prod = objIntervalo.Descripcion;
                objd.favd_rango_talla = objIntervalo.serie1 + "/" + objIntervalo.serie2;
                objd.favd_iid_moneda = 1;
                objd.favd_ncantidad_total_producto = mlitaProductos.Where(o => o.pr_vcodigo_externo.Substring(0, 13).Trim() == objIntervalo.codigoExterno.Trim()).Sum(ob => ob.infid_ncant_fisica);
            }
        }
        public class EIntervalo
        {
            public int serie1 { get; set; }
            public int serie2 { get; set; }
            public string codigoExterno { get; set; }
            public string Descripcion { get; set; }

        }

        private void BtnGuiaRemision_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (TipodeFactura == 1 || oBe.favc_tipo_factura == 1)
            {



                try
                {
                    using (FrmListarGuiaRemision frm = new FrmListarGuiaRemision())
                    {
                        frm.TipoGuia = 1;//Mercaderia
                        if (frm.ShowDialog() == DialogResult.OK)
                        {


                            lstFacturaDetalle.Clear();


                            BtnGuiaRemision.Tag = frm._Be.remic_icod_remision;
                            BtnGuiaRemision.Text = frm._Be.remic_vnumero_remision;

                            bteCliente.Tag = frm._Be.cliec_icod_cliente;
                            bteCliente.Text = frm._Be.NomClie;
                            txtDireccion.Text = frm._Be.remic_vdireccion_destinatario;
                            txtRUC.Text = frm._Be.remic_vruc_destinatario;

                            List<EGuiaRemisionDet> mListGuiaDet = new List<EGuiaRemisionDet>();
                            mListGuiaDet = new BVentas().listarGuiaRemisionDet(frm._Be.remic_icod_remision, Parametros.intEjercicio);
                            foreach (var _BEguia in mListGuiaDet)
                            {
                                EFacturaDet _BEe = new EFacturaDet();
                                _BEe.prdc_icod_producto = _BEguia.prdc_icod_producto;
                                _BEe.favd_iitem_factura = _BEguia.dremc_inro_item;
                                _BEe.favd_vcodigo_extremo_prod = _BEguia.dremc_vcodigo_extremo_prod;
                                _BEe.favd_vdescripcion = _BEguia.dremc_vdescripcion_prod;
                                _BEe.favd_nprecio_unitario_item = 0;
                                _BEe.favd_ncantidad = (_BEguia.dremc_ncantidad_producto);
                                _BEe.favd_nmonto_impuesto_item = 0;
                                _BEe.favd_nporcentaje_descuento_item = 0;
                                _BEe.favd_nprecio_total_item = 0;
                                _BEe.favd_nobservaciones = _BEguia.dremc_vobservaciones;
                                _BEe.intUsuario = Valores.intUsuario;
                                _BEe.almac_icod_almacen = frm._Be.almac_icod_almacen;
                                _BEe.strAlmacen = frm._Be.strDesAlmacen;


                                _BEe.unidc_icod_unidad_medida = _BEguia.unidc_icod_unidad_medida;
                                _BEe.CodigoSUNAT = _BEguia.CodigoSUNAT;

                                _BEe.favd_vdescripcion_prod = _BEguia.dremc_vdescripcion_prod;
                                _BEe.prdc_vpart_number = _BEguia.prdc_vpart_number;
                                _BEe.strDesUM = _BEguia.strDesUM;
                                _BEe.strCategoria = _BEguia.strCategoria;
                                _BEe.strSubCategoriaUno = _BEguia.strSubCategoriaUno;
                                _BEe.intTipoOperacion = 1;


                                _BEe.favd_icod_serie = Convert.ToInt32(_BEguia.dremc_iserie);
                                _BEe.resec_vdescripcion = _BEguia.resec_vdescripcion;
                                _BEe.favd_rango_talla = _BEguia.dremc_rango_talla;
                                _BEe.favd_talla1 = _BEguia.dremc_talla1;
                                _BEe.favd_talla2 = _BEguia.dremc_talla2;
                                _BEe.favd_talla3 = _BEguia.dremc_talla3;
                                _BEe.favd_talla4 = _BEguia.dremc_talla4;
                                _BEe.favd_talla5 = _BEguia.dremc_talla5;
                                _BEe.favd_talla6 = _BEguia.dremc_talla6;
                                _BEe.favd_talla7 = _BEguia.dremc_talla7;
                                _BEe.favd_talla8 = _BEguia.dremc_talla8;
                                _BEe.favd_talla9 = _BEguia.dremc_talla9;
                                _BEe.favd_talla10 = _BEguia.dremc_talla10;
                                _BEe.favd_cant_talla1 = _BEguia.dremc_cant_talla1;
                                _BEe.favd_cant_talla2 = _BEguia.dremc_cant_talla2;
                                _BEe.favd_cant_talla3 = _BEguia.dremc_cant_talla3;
                                _BEe.favd_cant_talla4 = _BEguia.dremc_cant_talla4;
                                _BEe.favd_cant_talla5 = _BEguia.dremc_cant_talla5;
                                _BEe.favd_cant_talla6 = _BEguia.dremc_cant_talla6;
                                _BEe.favd_cant_talla7 = _BEguia.dremc_cant_talla7;
                                _BEe.favd_cant_talla8 = _BEguia.dremc_cant_talla8;
                                _BEe.favd_cant_talla9 = _BEguia.dremc_cant_talla9;
                                _BEe.favd_cant_talla10 = _BEguia.dremc_cant_talla10;
                                _BEe.favd_icod_producto1 = _BEguia.dremc_icod_producto1;
                                _BEe.favd_icod_producto2 = _BEguia.dremc_icod_producto2;
                                _BEe.favd_icod_producto3 = _BEguia.dremc_icod_producto3;
                                _BEe.favd_icod_producto4 = _BEguia.dremc_icod_producto4;
                                _BEe.favd_icod_producto5 = _BEguia.dremc_icod_producto5;
                                _BEe.favd_icod_producto6 = _BEguia.dremc_icod_producto6;
                                _BEe.favd_icod_producto7 = _BEguia.dremc_icod_producto7;
                                _BEe.favd_icod_producto8 = _BEguia.dremc_icod_producto8;
                                _BEe.favd_icod_producto9 = _BEguia.dremc_icod_producto9;
                                _BEe.favd_icod_producto10 = _BEguia.dremc_icod_producto10;

                                _BEe.favd_iid_kardex1 = _BEguia.dremc_iid_kardex1;
                                _BEe.favd_iid_kardex2 = _BEguia.dremc_iid_kardex2;
                                _BEe.favd_iid_kardex3 = _BEguia.dremc_iid_kardex3;
                                _BEe.favd_iid_kardex4 = _BEguia.dremc_iid_kardex4;
                                _BEe.favd_iid_kardex5 = _BEguia.dremc_iid_kardex5;
                                _BEe.favd_iid_kardex6 = _BEguia.dremc_iid_kardex6;
                                _BEe.favd_iid_kardex7 = _BEguia.dremc_iid_kardex7;
                                _BEe.favd_iid_kardex8 = _BEguia.dremc_iid_kardex8;
                                _BEe.favd_iid_kardex9 = _BEguia.dremc_iid_kardex9;
                                _BEe.favd_iid_kardex10 = _BEguia.dremc_iid_kardex10;


                                lstFacturaDetalle.Add(_BEe);

                            }
                            nuevoToolStripMenuItem.Enabled = false;
                            eliminarToolStripMenuItem1.Enabled = false;
                            //nuevoServicioToolStripMenuItem.Enabled = false;
                            viewFacturaMercaderia.RefreshData();
                            setTotal();
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                try
                {
                    using (FrmListarGuiaRemision frm = new FrmListarGuiaRemision())
                    {
                        frm.TipoGuia = 2;//Suministros
                        if (frm.ShowDialog() == DialogResult.OK)
                        {


                            lstFacturaDetalle.Clear();


                            BtnGuiaRemision.Tag = frm._Be.remic_icod_remision;
                            BtnGuiaRemision.Text = frm._Be.remic_vnumero_remision;

                            bteCliente.Tag = frm._Be.cliec_icod_cliente;
                            bteCliente.Text = frm._Be.NomClie;
                            txtDireccion.Text = frm._Be.remic_vdireccion_destinatario;
                            txtRUC.Text = frm._Be.remic_vruc_destinatario;

                            List<EGuiaRemisionMPDet> mListGuiaDet = new List<EGuiaRemisionMPDet>();
                            mListGuiaDet = new BVentas().listarGuiaRemisionMPDet(frm._Be.remic_icod_remision, Parametros.intEjercicio);
                            foreach (var _BEguia in mListGuiaDet)
                            {
                                EFacturaMPDet _BEe = new EFacturaMPDet();
                                _BEe.prdc_icod_producto = _BEguia.prdc_icod_producto;
                                _BEe.favd_iitem_factura = _BEguia.dremc_inro_item;
                                _BEe.favd_nprecio_unitario_item = 0;
                                _BEe.favd_ncantidad = (_BEguia.dremc_ncantidad_producto);
                                _BEe.favd_nmonto_impuesto_item = 0;
                                _BEe.favd_nporcentaje_descuento_item = 0;
                                _BEe.favd_nprecio_total_item = 0;
                                _BEe.favd_nobservaciones = _BEguia.dremc_vobservaciones;
                                _BEe.intUsuario = Valores.intUsuario;
                                _BEe.almac_icod_almacen = frm._Be.almac_icod_almacen;
                                _BEe.strAlmacen = frm._Be.strDesAlmacen;


                                List<EProducto> lstUM = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_icod_producto == _BEguia.prdc_icod_producto).ToList();
                                if (lstUM.Count > 0)
                                {
                                    lstUM.ForEach(x =>
                                    {
                                        _BEe.unidc_icod_unidad_medida = Convert.ToInt32(x.unidc_icod_unidad_medida);
                                        _BEe.CodigoSUNAT = x.CodigoSUNAT;
                                    });
                                }

                                _BEe.strCodProducto = _BEguia.strCodProducto;
                                _BEe.strDesProducto = _BEguia.strDesProducto;
                                _BEe.prdc_vpart_number = _BEguia.prdc_vpart_number;
                                _BEe.strDesUM = _BEguia.strDesUM;
                                _BEe.strCategoria = _BEguia.strCategoria;
                                _BEe.strSubCategoriaUno = _BEguia.strSubCategoriaUno;
                                _BEe.intTipoOperacion = 1;


                                lstFacturaMPDetalle.Add(_BEe);

                            }
                            nuevoToolStripMenuItem.Enabled = false;
                            eliminarToolStripMenuItem1.Enabled = false;
                            grdFacturaSuministros.DataSource = lstFacturaMPDetalle;
                            viewFacturaMercaderia.RefreshData();
                            xtraTabControl1.SelectedTabPage = xtraSuministros;
                            xtraMercaderia.PageEnabled = false;
                            xtraAlquileres.PageEnabled = false;
                            setTotal();
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }



        }

        private void btnDireccion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (FrmListarDirecionCliente frm = new FrmListarDirecionCliente())
                {
                    frm.cliec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        txtDireccion.Text = frm._Be.dc_vdireccion;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmListarDirecionCliente frm = new FrmListarDirecionCliente())
                {
                    frm.cliec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        txtDireccion.Text = frm._Be.dc_vdireccion;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbIncluyeIGV_CheckedChanged_1(object sender, EventArgs e)
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

        private void txtSerie_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        private void grdFacturaMercaderia_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            if (listaCuotas.Count() == 0)
            {
                listaCuotas.Add(new ECuotasFactura
                {
                    fcc_inro_cuota = 1,
                    fcc_nmonto = Convert.ToDecimal(txtMontoCredito.Text),
                    fcc_sfecha_pago = (DateTime?)null,
                    intUsuario = Valores.intUsuario,
                    strPc = WindowsIdentity.GetCurrent().Name,
                    favc_icod_factura = oBe.favc_icod_factura,
                    doxcc_icod_correlativo = (int)oBe.doxcc_icod_correlativo,
                    fcc_iestado = Constantes.CuotaGenerado

                });

            }

            FrmManteCuotasFactura frm = new FrmManteCuotasFactura();
            frm.Text = $"Cuotas de la FAV {txtSerie.Text}-{txtNumero.Text}";
            frm.eFacturaCab = oBe;
            frm.txtMontoCredito.Text = txtMontoCredito.Text;
            frm.listaCuotas = listaCuotas;
            frm.listaCuotasEliminar = listaCuotasEliminar;
            frm.Status = Status;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                listaCuotas = frm.listaCuotas;
                listaCuotasEliminar = frm.listaCuotasEliminar;
                txtMontoCredito.Text = listaCuotas.Sum(x => x.fcc_nmonto).ToString();
            }
        }

        private void txtPorcRetencion_EditValueChanged(object sender, EventArgs e)
        {
            if (!txtPorcRetencion.ContainsFocus) return;
            decimal porcentaje = Convert.ToDecimal(txtPorcRetencion.Text) / 100;
            txtMontoRetencion.Text = (Convert.ToDecimal(txtMontoTotal.Text) * porcentaje).ToString();

        }

        private void txtMontoRetencion_EditValueChanged(object sender, EventArgs e)
        {
            if (!txtMontoRetencion.ContainsFocus) return;
            if (Convert.ToDecimal(txtMontoRetencion.Text) == 0)
            {
                txtPorcRetencion.Text = "0";
            }
            else
            {
                txtPorcRetencion.Text = ((Convert.ToDecimal(txtMontoRetencion.Text) / Convert.ToDecimal(txtMontoTotal.Text)) * 100).ToString();
            }

        }
    }

}