using System;

namespace SGE.Entity
{
    public class EPercepcionDet : EAuditoria
    {
        public int percd_icod_detalle { get; set; }
        public int percc_icod_percepcion { get; set; }
        public int tdoc_icod_tipo_documento { get; set; }
        public Int64 percd_icod_dxp { get; set; }
        public string percd_vnro_doc { get; set; }
        public DateTime percd_sfecha_doc { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal percd_nmonto_doc { get; set; }
        public decimal percd_nmonto_percibido_doc { get; set; }
        /**/
        public int intTipoOperacion { get; set; }
        public string strMoneda { get; set; }
        public string strTipoDoc { get; set; }
        public int tdodc_iid_correlativo { get; set; }
        public int intAnalitica { get; set; }
        public string strCodAnalitica { get; set; }
    }
}
