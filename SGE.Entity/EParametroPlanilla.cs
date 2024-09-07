namespace SGE.Entity
{
    public class EParametroPlanilla : EAuditoria
    {
        public decimal? prpc_nasignacion_familiar { set; get; }
        public decimal? prpc_ngratificacion_essalud { set; get; }
        public decimal? prpc_ngratificacion_eps { set; get; }
        public int prpc_id_cta_remuneracion { get; set; }
        public int prpc_id_cta_vacaciones { get; set; }
        public int prpc_id_cta_gratificaciones { get; set; }
        public int prpc_id_cta_cts { get; set; }
        public decimal? prpc_nporc_essalud { set; get; }
        public decimal? prpc_nporc_eps_pacifico { set; get; }
        public decimal? prpc_nporc_eps_essalud { set; get; }
        public decimal? prpc_nsueldo_minimo { set; get; }
        /*Cuenta Contable*/
        public string CuentaRemuneracion { get; set; }
        public string CuentaVacaciones { get; set; }
        public string CuentaGratificaciones { get; set; }
        public string CuentaCTS { get; set; }
        /*Cuentas Destino*/
        public int prpc_id_cta_destino_vacaciones { get; set; }
        public int prpc_id_cta_destino_gratificaciones { get; set; }
        public int prpc_id_cta_destino_cts { get; set; }
        public string CuentaDestinoVacaciones { get; set; }
        public string CuentaDestinoGratificaciones { get; set; }
        public string CuentaDestinoCTS { get; set; }
        public decimal? prpc_ndias_trabajo { set; get; }
    }
}
