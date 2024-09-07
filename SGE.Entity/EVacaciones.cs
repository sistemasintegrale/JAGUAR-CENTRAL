using System;

namespace SGE.Entity
{
    public class EVacaciones : EAuditoria
    {
        public int? vacd_icod_vacaciones { get; set; }
        public string vacd_iid_vacaciones { get; set; }
        public int? vacd_icod_personal { get; set; }
        public int? vacd_ndias { get; set; }
        public DateTime? vacd_sfecha_ini { get; set; }
        public DateTime? vacd_sfecha_fin { get; set; }
        public int? vacd_mes { get; set; }
        public int? vacd_año { get; set; }
        public string strMes { get; set; }
    }
}
