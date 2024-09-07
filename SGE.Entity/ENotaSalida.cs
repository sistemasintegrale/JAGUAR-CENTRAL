using System;

namespace SGE.Entity
{
    public class ENotaSalida : EAuditoria
    {
        public int nsalc_icod_nota_salida { get; set; }
        public int nsalc_ianio { get; set; }
        public string nsalc_numero_nota_salida { get; set; }
        public int almac_icod_almacen { get; set; }
        public int nsalc_iid_motivo { get; set; }
        public DateTime nsalc_fecha_nota_salida { get; set; }
        public int tdocc_icod_tipo_doc { get; set; }
        public string nsalc_numero_doc { get; set; }
        public string nsalc_referencia { get; set; }
        public string nsalc_observaciones { get; set; }
        /*-------------------------------------------------------------*/
        public string strAlmacen { get; set; }
        public string strMotivo { get; set; }
        public string strTipoDoc { get; set; }
        public string strTipoNroDoc { get; set; }
    }
}
