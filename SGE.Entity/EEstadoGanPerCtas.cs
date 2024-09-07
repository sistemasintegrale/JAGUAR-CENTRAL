namespace SGE.Entity
{
    public class EEstadoGanPerCtas : EAuditoria
    {
        public int egpd_icod_ctas_estado_gan_per { get; set; }
        public int egpd_icod_estado_gan_per { get; set; }
        public int egpd_iid_cuenta_contable { get; set; }
        public bool egpd_flag_estado { get; set; }
        /*Cunetas*/
        public string ctacc_nombre_descripcion { get; set; }
        public string ctacc_numero_cuenta_contable { get; set; }

        public decimal MontosCC { get; set; }

    }
}
