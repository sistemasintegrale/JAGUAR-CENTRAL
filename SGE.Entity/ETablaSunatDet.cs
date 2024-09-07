namespace SGE.Entity
{
    public class ETablaSunatDet : EAuditoria
    {
        public int suntc_icod { get; set; }
        public int suntd_icod { get; set; }
        public string suntd_codigo { get; set; }
        public string suntd_vdescripcion { get; set; }
        public string suntd_parametro { get; set; }
        public int suntd_iusuario_crea { get; set; }
        public string suntd_vpc_crea { get; set; }
        public int suntd_iusuario_modifica { get; set; }
        public string suntd_vpc_modifica { get; set; }
        public int suntd_iactivo { get; set; }
        public int suntd_iusuario_eliminar { get; set; }
        public string suntd_vpc_eliminar { get; set; }
    }
}
