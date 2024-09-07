namespace SGE.Entity
{
    public class EEstadoGanPer : EAuditoria
    {
        public int egpc_icod_estado_gan_per { get; set; }
        public string egpc_vlinea { get; set; }
        public int tablc_icod_linea_registro { get; set; }
        public string egpc_vconcepto { get; set; }
        public int tablc_icod_signo_monto { get; set; }
        public int tablc_icod_tipo_total { get; set; }
        public bool egpc_flag_estado { get; set; }
        public string DesLinea { get; set; }
        public string Signo { get; set; }
        public string Total { get; set; }
        /*resultado Estado Por Centro Costo*/
        public decimal Monto { get; set; }
    }
}
