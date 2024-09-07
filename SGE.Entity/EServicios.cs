namespace SGE.Entity
{
    public class EServicios : EAuditoria
    {
        public int servd_icod_servicios { get; set; }
        public int hjcd1_icod_detalle_hc { get; set; }
        public string servd_vdescripcion { get; set; }
        public string servd_vunidad_medida { get; set; }
        public int servd_icantidad { get; set; }
        public decimal servd_precio_unitario { get; set; }
        public decimal servd_precio_total { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
