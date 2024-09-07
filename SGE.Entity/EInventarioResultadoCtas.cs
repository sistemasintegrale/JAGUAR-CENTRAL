namespace SGE.Entity
{
    public class EInventarioResultadoCtas : EAuditoria
    {
        public int ird_icod_ctas_inventario_resultado { get; set; }
        public int irc_icod_inventario_resultado { get; set; }
        public int ird_iid_cuenta_contable { get; set; }
        public bool ird_flag_estado { get; set; }
        /*Cunetas*/
        public string ctacc_nombre_descripcion { get; set; }
        public string ctacc_numero_cuenta_contable { get; set; }

        public decimal MontosCC { get; set; }
    }
}
