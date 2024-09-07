using System;

namespace SGE.Entity
{
    public class ERegistroSerie : EAuditoria
    {
        public int resec_iid_registro_serie { get; set; }
        public string resec_icod_registro_serie { get; set; }
        public string resec_vdescripcion { get; set; }
        public string resec_vtalla_inicial { get; set; }
        public string resec_vtalla_final { get; set; }
        public int intusuario { get; set; }
        public DateTime sfecha { get; set; }
        public string vpc { get; set; }
        public bool resec_flag_estado { get; set; }
    }
}
