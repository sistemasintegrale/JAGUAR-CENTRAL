using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ECuentaContable : EAuditoria
    {
        [DataMember]
        public int anioc_iid_anio { get; set; }
        [DataMember]
        public long ctacc_icod_cuenta_contable { get; set; }
        [DataMember]
        public string ctacc_numero_cuenta_contable { get; set; }
        [DataMember]
        public string ctacc_nombre_descripcion { get; set; }
        [DataMember]
        public long ctacc_iid_cuenta_padre { get; set; }
        [DataMember]
        public int ctacc_ { get; set; }
        [DataMember]
        public int ctacc_nivel_cuenta { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public char? ctacc_cflag_relacion { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_analitica { get; set; }
        [DataMember]
        public int? tablc_iid_clasificacion { get; set; }
        [DataMember]
        public int? tablc_iid_elemento { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_cuenta { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_saldo { get; set; }
        [DataMember]
        public int? ctacc_icod_cuenta_debe_auto { get; set; }
        [DataMember]
        public int? ctacc_icod_cuenta_haber_auto { get; set; }
        [DataMember]
        public bool ctacc_iccosto { get; set; }
        [DataMember]
        public bool ctacc_flag_estado { get; set; }
        /*-------------------------------------------------------*/
        [DataMember]
        public string strEstado { get; set; }
        [DataMember]
        public string strTipoMoneda { get; set; }
        [DataMember]
        public string strTipoCuenta { get; set; }
        [DataMember]
        public string strTipoAnalitica { get; set; }
        [DataMember]
        public string strFlagCCosto { get; set; }

        public string strNumeroCuentaDebeAuto { get; set; }
        public string strDesCuentaDebeAuto { get; set; }
        public string strNumeroCuentaHaberAuto { get; set; }
        public string strDesCuentaHaberAuto { get; set; }
    }
}
