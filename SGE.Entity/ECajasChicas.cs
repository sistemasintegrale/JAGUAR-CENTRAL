namespace SGE.Entity
{
    public class ECajasChicas : EAuditoria
    {
        public int cc_icod_caja_chica { get; set; }
        public string cc_vdescricpcion { get; set; }
        public long ctacc_icod_cuenta_contable { get; set; }
        public string ctacc_numero_cuenta_contable { get; set; }
    }
}
