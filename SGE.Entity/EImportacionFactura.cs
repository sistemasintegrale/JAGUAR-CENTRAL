using System;

namespace SGE.Entity
{
    public class EImportacionFactura : EAuditoria
    {

        public int? impd1_icod_import_factura { set; get; }
        public int? impc_icod_importacion { set; get; }
        public int? fcoc_icod_doc { set; get; }
        public bool? impd1_flag_estado { set; get; }
        public int proc_icod_proveedor { set; get; }
        //-----------------------------------------------
        public string proc_vnombrecompleto { set; get; }
        public string fcoc_num_doc { set; get; }
        public DateTime? fcoc_sfecha_doc { set; get; }
        public int? tablc_iid_tipo_moneda { set; get; }
        public string tarec_vdescripcion { set; get; }
        public decimal? fcoc_nmonto_total_detalle { set; get; }
        public decimal? fcoc_nmonto_tipo_cambio { set; get; }
    }
}
