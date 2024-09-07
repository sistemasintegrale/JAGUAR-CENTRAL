using System;

namespace SGE.Entity
{
    public class EDocRecepcionCompra : EAuditoria
    {
        public int drcc_icod_doc_recepcion_compra { get; set; }
        public string drcc_vnumero_doc_recepcion_compra { get; set; }
        public DateTime drcc_sfecha { get; set; }
        public int proc_icod_proveedor { get; set; }
        public int almac_icod_almacen { get; set; }
        public int tdocc_icod_tipo_doc { get; set; }
        public int tablc_iid_motivo { get; set; }
        public int tablc_iid_situacion { get; set; }
        public string drcc_vobservaciones { get; set; }
        /*--------------------------------------------------------*/
        public string NomProveedor { get; set; }
        public string DesAlmacen { get; set; }
        public string TipoAbreviatura { get; set; }
        public string Motivo { get; set; }
        public string Situacion { get; set; }
    }
}
