using System;

namespace SGE.Entity
{
    public class EDocCompra : EAuditoria
    {
        public int facc_icod_doc { get; set; }
        public int proc_icod_proveedor { get; set; }
        public int tdocc_icod_tipo_doc { get; set; }
        public string facc_num_doc { get; set; }
        public DateTime facc_sfecha_doc { get; set; }
        public int almac_icod_almacen { get; set; }
        //public int? facc_iid_orden_reparacion { get; set; }
        public long doxpc_icod_correlativo { get; set; }
        public DateTime facc_svencimiento { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public int facc_iforma_pago { get; set; }
        public string facc_vobservaciones { get; set; }
        public decimal facc_nporcent_imp_doc { get; set; }
        public decimal facc_nmonto_neto_doc { get; set; }
        public decimal facc_nmonto_imp { get; set; }
        public decimal facc_nmonto_total_doc { get; set; }
        public int facc_isituacion { get; set; }
        public bool facc_flag_incluye_igv { get; set; }
        public Int16 facc_anio { get; set; }
        public Int16 facc_mes { get; set; }
        public int facc_icod_comprador { get; set; }
        /**/
        public string strMesProceso { get; set; }
        public string strTipoDoc { get; set; }
        public string strProveedor { get; set; }
        public string strAlmacen { get; set; }
        public string strMoneda { get; set; }
        public string strFormaPago { get; set; }
        public string strSituacion { get; set; }
        public string strComprador { get; set; }
        public Int64 intDXP { get; set; }
        public Int64 intCorrelativoDXP { get; set; }
        public bool flagCorrelativo { get; set; }
    }
}
