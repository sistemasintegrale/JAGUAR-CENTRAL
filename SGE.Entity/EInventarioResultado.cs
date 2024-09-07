namespace SGE.Entity
{
    public class EInventarioResultado : EAuditoria
    {

        public int irc_icod_inventario_resultado { get; set; }
        public string irc_vlinea { get; set; }
        public int tablc_icod_linea_registro { get; set; }
        public string irc_vconcepto { get; set; }
        public int tablc_icod_signo_monto { get; set; }
        public int tablc_icod_tipo_total { get; set; }
        public bool irc_flag_estado { get; set; }
        public string DesLinea { get; set; }
        public string Signo { get; set; }
        public string Total { get; set; }
        /*resultado Estado Por Centro Costo*/
        public decimal Monto { get; set; }
    }
}
