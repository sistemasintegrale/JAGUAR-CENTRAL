using System;

namespace SGE.Entity
{
    public class ETablaSunatCab : EAuditoria
    {
        public int suntc_icod { get; set; }
        public string suntc_codigo { get; set; }
        public string suntc_vdescripcion { get; set; }
        public int suntc_iusuario_crea { get; set; }
        public int suntc_iusuario_modifica { get; set; }
        public string suntc_vpc_crea { get; set; }
        public string suntc_vpc_modifica { get; set; }
        public int suntc_iactivo { get; set; }
        public String suntc_cestado { get; set; }
        public int suntc_iusuario_elimina { get; set; }
        public string suntc_vpc_elimina { get; set; }
    }
}
