namespace SGE.Entity
{
    public class EArchivos : EAuditoria
    {
        public int arch_icod_archivos { get; set; }
        public int arch_iid_correlativo { get; set; }
        public int arch_iid_orden_compra_local { get; set; }
        public string arch_vdescripcion { get; set; }
        public string arch_vruta { get; set; }
        public bool arch_flag_estado { get; set; }
        public int intTipoOperacion { get; set; }
        public int arch_iid_personal { get; set; }
    }
}
