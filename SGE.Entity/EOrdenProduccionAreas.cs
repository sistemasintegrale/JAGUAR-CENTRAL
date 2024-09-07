using System;

namespace SGE.Entity
{
    public class EOrdenProduccionAreas : EAuditoria
    {
        public int orprac_iid_codigo { get; set; }
        public int orprc_iid_orden_produccion { get; set; }
        public int orprac_icod_proceso { get; set; }
        public int orprac_ipersonal { get; set; }
        public string orprac_sfecha_asignacion { get; set; }
        public string orprac_sfecha_terminado { get; set; }
        public Boolean orprac_bterminado { get; set; }
        public Boolean orprac_bvisto_bueno { get; set; }
        public int intTipoOperacion { get; set; }
        public string strpersonal { get; set; }
        public string strproceso { get; set; }
        public int? orprc_isituacion { get; set; }
        public string strEstado { get; set; }
    }
}
