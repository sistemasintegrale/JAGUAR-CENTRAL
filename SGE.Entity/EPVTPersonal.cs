using System;

namespace SGE.Entity
{
    public class EPVTPersonal : EAuditoria
    {

        public int perc_iid_personal { get; set; }
        public string perc_vcod_personal { get; set; }
        public string perc_vnombres_apellidos { get; set; }
        public string perc_vDNI { get; set; }
        public string perc_vdomicillo { get; set; }
        public string perc_vtelefono { get; set; }
        public int perc_iarea_empresa { get; set; }
        public int perc_isub_area_empresa { get; set; }
        public int perc_iarea_proceso { get; set; }
        public int perc_icargo { get; set; }
        public DateTime perc_sfecha_nacimiento { get; set; }
        public int perc_isexo { get; set; }
        public int perc_irelacion_laboral { get; set; }
        public bool perc_flag_estado { get; set; }
        public string strarea_empresa { get; set; }
        public string strarea_proceso { get; set; }
        public string strcargo { get; set; }
        public string strrelacion_laboral { get; set; }
        public string strsexo { get; set; }
        public string strsubareaempresa { get; set; }
        public int perc_icod_analitica { get; set; }
        public string anac_iid_analitica { get; set; }
        public int? perc_isituacion { get; set; }
        public string strSituacion { get; set; }
    }
}
