using System;

namespace SGE.Entity
{
    public class ENotaCreditoProvedor : ENotaCreditoProveedorDet
    {
        public int ncpc_icod_nota_cred { get; set; }
        public int tdodc_iid_clase_nota_cred { get; set; }
        public string ncpc_nro_nota_cred { get; set; }
        public DateTime ncpc_fecha_nota_cred { get; set; }
        public int proc_icod_proveedor { get; set; }
        public int? ncpc_tipo_doc_ref_doc { get; set; }
        public string ncpc_nro_doc_ref_doc { get; set; }
        public DateTime? ncpc_sfecha_referencia { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal ncpc_tipo_cambio { get; set; }
        public int almac_icod_almacen { get; set; }
        public int ncpc_mes_proceso { get; set; }
        public int tablc_iid_situacion { get; set; }
        public decimal ncpc_nmonto_neto_doc { get; set; }
        public decimal ncpc_nporcent_imp_doc { get; set; }
        public decimal ncpc_nmonto_total_doc { get; set; }
        public decimal ncpc_nmonto_destino_gravado { get; set; }
        public decimal ncpc_nmonto_destino_mixto { get; set; }
        public decimal ncpc_nmonto_destino_nogravado { get; set; }
        public decimal ncpc_nmonto_adq_nogravado { get; set; }
        public decimal ncpc_nmonto_imp_destino_gravado { get; set; }
        public decimal ncpc_nmonto_imp_destino_mixto { get; set; }
        public decimal ncpc_nmonto_imp_destino_nogravado { get; set; }
        public Int64 doxpc_icod_correlativo { get; set; }
        public int? ncpc_anio { get; set; }
        /**/
        public string strProveedor { get; set; }
        public string strAlmacen { get; set; }
        public string strSituacion { get; set; }
        public string strMoneda { get; set; }
        public Int64 intDXP { get; set; }
        public int intClaseNCP { get; set; }
        public string strClaseNCP { get; set; }
        public bool ncpc_flag_importacion { get; set; }

    }
}
