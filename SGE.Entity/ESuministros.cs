namespace SGE.Entity
{
    public class ESuministros : EAuditoria
    {
        public int sumd_icod_suministros_hc { get; set; }
        public int hjcd1_icod_detalle_hc { get; set; }
        public string sumd_vpartida { get; set; }
        public string sumd_vdescripcion { get; set; }
        public string sumd_vunidad_medida { get; set; }
        public int? sumd_icantidad { get; set; }
        public decimal? sumd_precio_unitario { get; set; }
        public decimal? sumd_precio_total { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
