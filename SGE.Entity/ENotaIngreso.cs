using System;

namespace SGE.Entity
{
    public class ENotaIngreso : EAuditoria
    {
        public int ningc_icod_nota_ingreso { get; set; }
        public int ningc_ianio { get; set; }
        public string ningc_numero_nota_ingreso { get; set; }
        public int almac_icod_almacen { get; set; }
        public int ningc_iid_motivo { get; set; }
        public DateTime ningc_fecha_nota_ingreso { get; set; }
        public int tdocc_icod_tipo_doc { get; set; }
        public string ningc_numero_doc { get; set; }
        public string ningc_referencia { get; set; }
        public string ningc_observaciones { get; set; }
        public int fcoc_icod_doc { get; set; }
        /*-------------------------------------------------------------*/
        public string strAlmacen { get; set; }
        public string strMotivo { get; set; }
        public string strTipoDoc { get; set; }
        public string strTipoNroDoc { get; set; }
    }
}
