using System;

namespace SGE.Entity
{
    public class EFacturaDet : EAuditoria
    {
        public int favd_icod_item_factura { get; set; }
        public int favc_icod_factura { get; set; }
        public int favd_iitem_factura { get; set; }
        public int almac_icod_almacen { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal favd_ncantidad { get; set; }
        public int? tablc_iid_motivo { get; set; }
        public string favd_vcodigo_extremo_prod { get; set; }
        public string codigo_producto { get; set; }
        public string favd_vdescripcion_prod { get; set; }
        public decimal favd_ncantidad_total_producto { get; set; }
        public int favd_icod_serie { get; set; }
        public string favd_rango_talla { get; set; }
        public string resec_vdescripcion { get; set; }
        public int? favd_talla1 { get; set; }
        public int? favd_talla2 { get; set; }
        public int? favd_talla3 { get; set; }
        public int? favd_talla4 { get; set; }
        public int? favd_talla5 { get; set; }
        public int? favd_talla6 { get; set; }
        public int? favd_talla7 { get; set; }
        public int? favd_talla8 { get; set; }
        public int? favd_talla9 { get; set; }
        public int? favd_talla10 { get; set; }
        public int? favd_cant_talla1 { get; set; }
        public int? favd_cant_talla2 { get; set; }
        public int? favd_cant_talla3 { get; set; }
        public int? favd_cant_talla4 { get; set; }
        public int? favd_cant_talla5 { get; set; }
        public int? favd_cant_talla6 { get; set; }
        public int? favd_cant_talla7 { get; set; }
        public int? favd_cant_talla8 { get; set; }
        public int? favd_cant_talla9 { get; set; }
        public int? favd_cant_talla10 { get; set; }
        public int? favd_icod_producto1 { get; set; }
        public int? favd_icod_producto2 { get; set; }
        public int? favd_icod_producto3 { get; set; }
        public int? favd_icod_producto4 { get; set; }
        public int? favd_icod_producto5 { get; set; }
        public int? favd_icod_producto6 { get; set; }
        public int? favd_icod_producto7 { get; set; }
        public int? favd_icod_producto8 { get; set; }
        public int? favd_icod_producto9 { get; set; }
        public int? favd_icod_producto10 { get; set; }
        public long? favd_iid_kardex1 { get; set; }
        public long? favd_iid_kardex2 { get; set; }
        public long? favd_iid_kardex3 { get; set; }
        public long? favd_iid_kardex4 { get; set; }
        public long? favd_iid_kardex5 { get; set; }
        public long? favd_iid_kardex6 { get; set; }
        public long? favd_iid_kardex7 { get; set; }
        public long? favd_iid_kardex8 { get; set; }
        public long? favd_iid_kardex9 { get; set; }
        public long? favd_iid_kardex10 { get; set; }
        public string favd_vncantidad { get; set; }
        public decimal favd_monto_unit { get; set; }
        public decimal favd_monto_total { get; set; }
        public int favd_iid_moneda { get; set; }
        public int operacion { get; set; }
        ///----------------------
        public string favd_vnprecio_unitario_item { get; set; }
        public string favd_vnprecio_total_item { get; set; }

        public string favd_vdescripcion { get; set; }
        public decimal favd_nprecio_unitario_item { get; set; }
        public decimal favd_nmonto_impuesto_item { get; set; }
        public decimal favd_nporcentaje_descuento_item { get; set; }
        public decimal favd_nprecio_total_item { get; set; }
        public decimal favd_nneto_exo { get; set; }
        public decimal favd_nneto_igv { get; set; }
        public int? kardc_icod_correlativo { get; set; }
        /**/
        public string strCodProducto { get; set; }
        public string strCategoria { get; set; }
        public string strSubCategoriaUno { get; set; }
        public string strDesUM { get; set; }
        public string CodigoSUNAT { get; set; }
        public string strDesProducto { get; set; }
        public string strMoneda { get; set; }
        public string strAlmacen { get; set; }
        public decimal dblMontoDescuento { get; set; }
        public decimal? dblStockDisponible { get; set; }
        public int intTipoOperacion { get; set; }
        public bool flagPlanilla { get; set; }
        public string prdc_vpart_number { get; set; }
        public string favd_nobservaciones { get; set; }
        public int OrdenItemImprimir { get; set; }
        public decimal favd_nprecio_neto_item { get; set; }

        public string vnumero_factura { get; set; }

        public int tablc_iid_tipo_venta { get; set; }
        public string strTipoVenta { get; set; }
        public string favd_vcaracteristicas { get; set; }
        public string resec_vtalla_inicial { get; set; }
        public string resec_vtalla_final { get; set; }
        public Boolean flag_afecto_igv { get; set; }
        public int unidc_icod_unidad_medida { get; set; }
        /*Factura Electronica Detalle*/
        public int IdItems { get; set; }
        public int IdCabecera { get; set; }
        public int NumeroOrdenItem { get; set; }
        public decimal cantidad { get; set; }
        public string unidadMedida { get; set; }
        public decimal ValorVentaItem { get; set; }
        public int CodMotivoDescuentoItem { get; set; }
        public decimal FactorDescuentoItem { get; set; }
        public decimal DescuentoItem { get; set; }
        public decimal BaseDescuentotem { get; set; }
        public int CodMotivoCargoItem { get; set; }
        public decimal FactorCargoItem { get; set; }
        public decimal MontoCargoItem { get; set; }
        public decimal BaseCargoItem { get; set; }
        public decimal MontoTotalImpuestosItem { get; set; }
        public decimal MontoImpuestoIgvItem { get; set; }
        public decimal MontoAfectoImpuestoIgv { get; set; }
        public decimal PorcentajeIGVItem { get; set; }
        public decimal MontoInafectoItem { get; set; }
        public decimal MontoImpuestoISCItem { get; set; }
        public decimal MontoAfectoImpuestoIsc { get; set; }
        public decimal PorcentajeISCtem { get; set; }
        public decimal MontoImpuestoIVAPtem { get; set; }
        public decimal MontoAfectoImpuestoIVAPItem { get; set; }
        public decimal PorcentajeIVAPItem { get; set; }
        public string descripcion { get; set; }
        public string codigoItem { get; set; }
        public string ObservacionesItem { get; set; }
        public decimal ValorUnitarioItem { get; set; }
        public string UMedida { get; set; }
        public string tipoImpuesto { get; set; }

        public string CodConceptoAsignarItem { get; set; }
        public string DescripcionAdicionalItem { get; set; }
        public decimal? PrecioVentaUnitarioItem { get; set; }
        public decimal favd_nmonto_imp_arroz { get; set; }
        public decimal favd_nneto_ivap { get; set; }
        public decimal favd_npor_imp_arroz { get; set; }


    }
}
