using System;

namespace SGE.Entity
{
    public class EBoletaCompraDet : EAuditoria
    {
        public int bcod_icod_doc { get; set; }
        public int bcoc_icod_doc { get; set; }
        public int bcod_iitem { get; set; }
        public int prd_icod_producto { get; set; }

        public decimal bcod_ncantidad { get; set; }
        public decimal bcod_nmonto_unit { get; set; }
        public decimal bcod_nmonto_total { get; set; }
        public decimal bcod_ndescuento { get; set; }
        public Int64 bcod_icod_kardex { get; set; }
        public string bcod_vdescripcion_item { get; set; }
        public int ocod_icod_detalle_oc { get; set; }
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
        public decimal bcod_nmonto_unit_costo { get; set; }
        public decimal bcod_nmonto_total_costo { get; set; }

        public decimal bcoc_nmonto_tipo_cambio { get; set; }
        public decimal PunidS { get; set; }
        public decimal PunidCFactor { get; set; }
        public decimal PunidCfactorTotal { get; set; }
        public string proc_vnombrecompleto { get; set; }
        public string favc_vnumero_factura { get; set; }
        public int grcd_icod_detalle { get; set; }

        /*Importacion*/
        public string strDesProducto { get; set; }
        public decimal PUnitario { get; set; }
        public decimal PCUnit { get; set; }
        public decimal PCTot { get; set; }
        public decimal bcod_factor { get; set; }
    }
}
