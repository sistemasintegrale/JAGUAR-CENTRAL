namespace SGE.Entity
{
    public class ETipoTarjeta : EAuditoria
    {
        public int tcrc_icod_tipo_tarjeta_cred { get; set; }
        public int tcrc_iid_tipo_tarjeta_cred { get; set; }
        public string tcrc_vdescripcion_tipo_tarjeta_cred { get; set; }
        public decimal tcrc_nporcentaje_comision { get; set; }
        public int bcoc_icod_banco { get; set; }
        public int bcod_icod_banco_cuenta { get; set; }

        public string strDesBanco { get; set; }
        public string strNroCuenta { get; set; }
    }
}
