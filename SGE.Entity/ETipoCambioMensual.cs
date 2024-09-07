namespace SGE.Entity
{
    public class ETipoCambioMensual : EAuditoria
    {
        public int tcamm_icod_tcam_mensual { get; set; }
        public int tcamm_iid_anio { get; set; }
        public int mesec_iid_mes { get; set; }
        public string vdes_mesec_iid_mes { get; set; }
        public decimal tcamm_ntipo_cambio_compra { get; set; }
        public decimal tcamm_ntipo_cambio_venta { get; set; }
    }
}
