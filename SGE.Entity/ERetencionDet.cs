using System;

namespace SGE.Entity
{
    public class ERetencionDet : EAuditoria
    {
        public int retd_icod_detalle { get; set; }
        public int retc_icod_comprobante_retencion { get; set; }
        public int tdoc_icod_tipo_documento { get; set; }
        public string retd_vnumero_documento { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public DateTime retd_sfec_documento { get; set; }
        public decimal retd_nmto_tipo_cambio_doc { get; set; }
        public decimal retd_nmto_pago_doc { get; set; }
        public decimal retd_nmto_retencion { get; set; }
        public decimal retd_nmto_total_doc { get; set; }
        public Int64 pdxpc_icod_correlativo { get; set; }
        public Int64 intIcodDXC { get; set; }
        /**/
        public string strTipoDoc { get; set; }
        public string strMoneda { get; set; }
        public int intTipoOperacion { get; set; }
        public int tdodc_iid_correlativo { get; set; }
        public int intAnalitica { get; set; }
        public string strCodAnalitica { get; set; }
        public int Moneda_DXC { get; set; }
    }
}
