namespace SGE.Entity
{
    public class EGastosAdministrativos : EAuditoria
    {
        public int gadm_icod_gastos_administrativos { get; set; }
        public int hjcd1_icod_detalle_hc { get; set; }
        public string gadm_vpartida { get; set; }
        public string gadm_vdescripcion { get; set; }
        public string gadm_vunidad_medida { get; set; }
        public int gadm_icantidad { get; set; }
        public decimal gadm_precio_unitario { get; set; }
        public decimal gadm_precio_total { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
