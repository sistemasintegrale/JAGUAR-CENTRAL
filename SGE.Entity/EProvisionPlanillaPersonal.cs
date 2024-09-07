using System;

namespace SGE.Entity
{
    public class EProvisionPlanillaPersonal : EAuditoria
    {
        public int planc_icod_planilla_personal { get; set; }
        public int planc_iid_planilla_personal { get; set; }
        public string NumPlanilla { get; set; }
        public int mesec_iid_mes { get; set; }
        public string Mes { get; set; }
        public int planc_iid_anio { get; set; }
        public string planc_vdescripcion { get; set; }
        public int tablc_iid_situacion_planilla { get; set; }
        public int planc_iid_tipo_planilla { get; set; }
        public string Situacion { get; set; }
        public string Tipo { get; set; }
        public DateTime planc_sfecha { get; set; }
    }
}
