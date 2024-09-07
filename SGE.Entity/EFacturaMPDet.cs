using System;

namespace SGE.Entity
{
    public class EFacturaMPDet : EAuditoria
    {
        public int favd_icod_item_factura { get; set; }
        public int favc_icod_factura { get; set; }
        public int favd_iitem_factura { get; set; }
        public int almac_icod_almacen { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal favd_ncantidad { get; set; }
        public string favd_vncantidad { get; set; }
        public decimal favd_monto_unit { get; set; }
        public decimal favd_monto_total { get; set; }
        public int favd_iid_moneda { get; set; }
        public int operacion { get; set; }
        ///----------------------
        public decimal PrecioVentaUnitarioItem { get; set; }
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
        public string strUM { get; set; }
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
    }
}
