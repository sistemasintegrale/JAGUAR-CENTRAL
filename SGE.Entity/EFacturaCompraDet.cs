using System;

namespace SGE.Entity
{
    public class EFacturaCompraDet : EAuditoria
    {
        public int fcod_icod_doc { get; set; }
        public int fcoc_icod_doc { get; set; }
        public int fcod_iitem { get; set; }
        public int prd_icod_producto { get; set; }

        public decimal fcod_ncantidad { get; set; }
        public decimal fcod_nmonto_unit { get; set; }
        public decimal fcod_nmonto_total { get; set; }
        public decimal fcod_ndescuento { get; set; }
        public Int64 fcod_icod_kardex { get; set; }
        public string fcod_vdescripcion_item { get; set; }
        public int ocod_icod_detalle_oc { get; set; }
        public int grcd_icod_detalle { get; set; }
        /**/
        public string strCodProducto { get; set; }
        public string strDesUM { get; set; }
        public string strMoneda { get; set; }
        public string strLinea { get; set; }
        public string strCodLinea { get; set; }
        public string strSubLinea { get; set; }
        public int intTipoOperacion { get; set; }
        public int intCtaContable { get; set; }
        public int intTipoAnalitica { get; set; }
        public int intAnaliticaProveedor { get; set; }
        public bool flagCCosto { get; set; }


        public string prov_vcodigo_prov { get; set; }
        public decimal fcod_nmonto_unit_costo { get; set; }
        public decimal fcod_nmonto_total_costo { get; set; }

        public decimal fcoc_nmonto_tipo_cambio { get; set; }
        public decimal PunidS { get; set; }
        public decimal PunidCFactor { get; set; }
        public decimal PunidCfactorTotal { get; set; }
        public string proc_vnombrecompleto { get; set; }
        public string favc_vnumero_factura { get; set; }
        /*Importacion*/
        public string strDesProducto { get; set; }
        public decimal PUnitario { get; set; }
        public decimal PCUnit { get; set; }
        public decimal PCTot { get; set; }
        public decimal fcod_factor { get; set; }
        /*Mercaderia*/
        public string fcod_rango_talla { get; set; }
        public int? fcod_talla1 { get; set; }
        public int? fcod_talla2 { get; set; }
        public int? fcod_talla3 { get; set; }
        public int? fcod_talla4 { get; set; }
        public int? fcod_talla5 { get; set; }
        public int? fcod_talla6 { get; set; }
        public int? fcod_talla7 { get; set; }
        public int? fcod_talla8 { get; set; }
        public int? fcod_talla9 { get; set; }
        public int? fcod_talla10 { get; set; }
        public int? fcod_cant_talla1 { get; set; }
        public int? fcod_cant_talla2 { get; set; }
        public int? fcod_cant_talla3 { get; set; }
        public int? fcod_cant_talla4 { get; set; }
        public int? fcod_cant_talla5 { get; set; }
        public int? fcod_cant_talla6 { get; set; }
        public int? fcod_cant_talla7 { get; set; }
        public int? fcod_cant_talla8 { get; set; }
        public int? fcod_cant_talla9 { get; set; }
        public int? fcod_cant_talla10 { get; set; }
        public long? fcod_iid_kardex1 { get; set; }
        public long? fcod_iid_kardex2 { get; set; }
        public long? fcod_iid_kardex3 { get; set; }
        public long? fcod_iid_kardex4 { get; set; }
        public long? fcod_iid_kardex5 { get; set; }
        public long? fcod_iid_kardex6 { get; set; }
        public long? fcod_iid_kardex7 { get; set; }
        public long? fcod_iid_kardex8 { get; set; }
        public long? fcod_iid_kardex9 { get; set; }
        public long? fcod_iid_kardex10 { get; set; }
    }
}
