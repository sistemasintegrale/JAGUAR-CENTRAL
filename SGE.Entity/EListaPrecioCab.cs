using System;

namespace SGE.Entity
{
    public class EListaPrecioCab : EAuditoria
    {
        public int lprec_icod_proveedor { get; set; }
        public int edit_icod_editorial { get; set; }
        public string lprec_Numerolista { get; set; }
        public DateTime lprec_sfecha_lista { get; set; }
        public string lprec_Observaciones { get; set; }
        public Boolean lprec_sflag_estado { get; set; }

        public string edit_vdescripcion { get; set; }
        public string proc_vcod_proveedor { get; set; }
    }
}
