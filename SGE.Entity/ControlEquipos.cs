using System;

namespace SGE.Entity
{
    public class ControlEquipos : ControlVersiones
    {
        public int ceq_icod_equipo { get; set; }
        public string ceq_vnombre_equipo { get; set; }
        public int? cvr_icod_version { get; set; }
        public DateTime? ceq_sfecha_actualizacion { get; set; }
        public string cep_vid_cpu { get; set; }
        public bool cep_bflag_acceso { get; set; }
        public string cep_vubicacion_actualizador { get; set; }
        public int cep_id_pvt { get; set; }
    }
}
