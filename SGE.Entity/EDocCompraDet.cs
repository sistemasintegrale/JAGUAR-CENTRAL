using System;

namespace SGE.Entity
{
    public class EDocCompraDet : EAuditoria
    {
        public int facd_icod_doc { get; set; }
        public int facc_icod_doc { get; set; }
        public int facd_iitem { get; set; }
        public int? prd_icod_producto { get; set; }
        public decimal? facd_ncantidad { get; set; }
        public decimal? facd_nmonto_unit { get; set; }
        public decimal facd_nmonto_total { get; set; }
        public Int64? facd_icod_kardex { get; set; }
        public string facd_vdescripcion_item { get; set; }
        /**/
        public string strCodProducto { get; set; }
        public string strDesUM { get; set; }
        public string strMoneda { get; set; }
        public string strCodLinea { get; set; }
        public string strLinea { get; set; }
        public string strSubLinea { get; set; }
        public int intTipoOperacion { get; set; }
        public int intClasificacion { get; set; }
        /**/
        public DateTime dtFecha { get; set; }
        public string strTipoDoc { get; set; }
        public string strNroDoc { get; set; }
        public int intCorrelativo { get; set; }
        public string strProveedor { get; set; }
        public string strComprador { get; set; }
        /*Campos para la generación de detalle del DXP (Cuentas Contables)*/
        public int intCtaContable { get; set; }
        public bool flagCCosto { get; set; }
        public int intTipoAnalitica { get; set; }
        public int intAnaliticaProveedor { get; set; }

        public string marc_vabreviatura { get; set; }
        public string marc_vdescripcion { get; set; }
        public string modc_vabreviatura { get; set; }
        public string modc_vdescripcion { get; set; }

    }
}
