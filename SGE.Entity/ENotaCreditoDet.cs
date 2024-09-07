using System;
using static SGE.Common.Enums;

namespace SGE.Entity
{
    public class ENotaCreditoDet : EAuditoria
    {
        public int dcrec_icod_detalle_credito { get; set; }
        public int ncrec_icod_credito { get; set; }
        public Int16 dcrec_inro_item { get; set; }
        public decimal dcrec_ncantidad_producto { get; set; }
        public decimal dcrec_nmonto_unitario { get; set; }
        public string dcrec_vcodigo_extremo_prod { get; set; }
        public string dcrec_vdescripcion { get; set; }
        public decimal PrecioVentaUnitarioItem { get; set; }
        public int dcrec_icod_serie { get; set; }
        public string Serie { get; set; }
        public string dcrec_rango_talla { get; set; }
        public int? dcrec_talla1 { get; set; }
        public int? dcrec_talla2 { get; set; }
        public int? dcrec_talla3 { get; set; }
        public int? dcrec_talla4 { get; set; }
        public int? dcrec_talla5 { get; set; }
        public int? dcrec_talla6 { get; set; }
        public int? dcrec_talla7 { get; set; }
        public int? dcrec_talla8 { get; set; }
        public int? dcrec_talla9 { get; set; }
        public int? dcrec_talla10 { get; set; }
        public int? dcrec_cant_talla1 { get; set; }
        public int? dcrec_cant_talla2 { get; set; }
        public int? dcrec_cant_talla3 { get; set; }
        public int? dcrec_cant_talla4 { get; set; }
        public int? dcrec_cant_talla5 { get; set; }
        public int? dcrec_cant_talla6 { get; set; }
        public int? dcrec_cant_talla7 { get; set; }
        public int? dcrec_cant_talla8 { get; set; }
        public int? dcrec_cant_talla9 { get; set; }
        public int? dcrec_cant_talla10 { get; set; }
        public int? dcrec_icod_producto2 { get; set; }
        public int? dcrec_icod_producto3 { get; set; }
        public int? dcrec_icod_producto4 { get; set; }
        public int? dcrec_icod_producto5 { get; set; }
        public int? dcrec_icod_producto6 { get; set; }
        public int? dcrec_icod_producto7 { get; set; }
        public int? dcrec_icod_producto8 { get; set; }
        public int? dcrec_icod_producto9 { get; set; }
        public int? dcrec_icod_producto10 { get; set; }
        public int? dcrec_icod_producto1 { get; set; }
        public long? dcrec_iid_kardex1 { get; set; }
        public long? dcrec_iid_kardex2 { get; set; }
        public long? dcrec_iid_kardex3 { get; set; }
        public long? dcrec_iid_kardex4 { get; set; }
        public long? dcrec_iid_kardex5 { get; set; }
        public long? dcrec_iid_kardex6 { get; set; }
        public long? dcrec_iid_kardex7 { get; set; }
        public long? dcrec_iid_kardex8 { get; set; }
        public long? dcrec_iid_kardex9 { get; set; }
        public long? dcrec_iid_kardex10 { get; set; }

        public decimal dcrec_nmonto_item { get; set; }
        public string dcrec_vmonto_item { get; set; }
        public decimal dcrec_nmonto_impuesto { get; set; }
        public int? prdc_icod_producto { get; set; }
        public int? almac_icod_almacen { get; set; }
        public int? tablc_iid_sit_item_nota_credito { get; set; }
        public Int64? kardc_iid_correlativo { get; set; }
        /**/
        public int intClasificacion { get; set; }
        public int intTipoOperacion { get; set; }
        public string strAlmacen { get; set; }
        public string strCodProducto { get; set; }
        public string strDesProducto { get; set; }
        public string strDesProductoPresentar { get; set; }
        public string strDesUM { get; set; }
        public string strMoneda { get; set; }
        public string strLinea { get; set; }
        public string strSubLinea { get; set; }

        public int unidc_icod_unidad_medida { get; set; }

        public decimal dcrec_nneto_igv { get; set; }
        public decimal dcrec_nneto_exo { get; set; }
        public decimal dcrec_nporcentaje_impuesto { get; set; }
        public Operacion operacion { get; set; }

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
        public string tipoImpuesto { get; set; }
        public string UMedida { get; set; }
        public string CodConceptoAsignarItem { get; set; }
        public string DescripcionAdicionalItem { get; set; }
        public string CodigoSUNAT { get; set; }

    }
}
