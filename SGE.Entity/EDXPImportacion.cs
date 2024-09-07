using System;

namespace SGE.Entity
{
    public class EDXPImportacion : EAuditoria
    {
        public int dxpd2_icod_correlativo { get; set; }
        public long doxpc_icod_correlativo { get; set; }
        public int impc_icod_importacion { get; set; }
        public int impd_icod_importacion_detalle { get; set; }
        public decimal dxpd2_nmonto_importacion { get; set; }
        public bool dxpd2_flag_estado { get; set; }
        public int intTipoOperacion { get; set; }
        /*--------------------------------------------------*/
        public string Rubros { get; set; }
        public string Concepto { get; set; }
        public string NumImpo { get; set; }
        /*---------------------DXP-------------------------*/
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        public string doxpc_vnumero_doc { get; set; }
        public DateTime doxpc_sfecha_doc { get; set; }
        public string Moneda { get; set; }
        public decimal doxpc_nmonto_tipo_cambio { get; set; }
        public string proc_vnombrecompleto { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public int tdocc_icod_tipo_doc { get; set; }

    }
}
