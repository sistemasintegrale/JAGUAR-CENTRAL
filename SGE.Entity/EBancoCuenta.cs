using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EBancoCuenta : EAuditoria
    {
        [DataMember]
        public int bcod_icod_banco_cuenta { get; set; }
        [DataMember]
        public int bcoc_icod_banco { get; set; }
        [DataMember]
        public string bcod_vnumero_cuenta { get; set; }
        [DataMember]
        public int tablc_iid_tipo_cuenta_ef { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public int bcod_iid_situacion_cuenta { get; set; }
        [DataMember]
        public int bcod_iicod_banco_cuenta { get; set; }
        [DataMember]
        public int? tarec_iid_tabla_registro { get; set; }
        [DataMember]
        public bool bcod_flag_estado { get; set; }
        [DataMember]
        public int anad_icod_analitica { get; set; }
        [DataMember]
        public decimal? bcod_monto_apertura { get; set; }
        [DataMember]
        public DateTime? bcod_fecha_apertura { get; set; }
        public int? ctacc_icod_cuenta_contable { get; set; }
        public int? cecoc_icod_centro_costo { get; set; }
        public int? tablc_iid_tipo_analitica { get; set; }

        /*--------------------------------------------------------------------------*/
        public string strTipoCuenta { get; set; }
        public string strMoneda { get; set; }
        public string strSituacion { get; set; }
        public string strCodAnalitica { get; set; }
        public string strBanco { get; set; }
        public string strMotivo { get; set; }
        public string strCodCtaContable { get; set; }
        public string strDesCtaContable { get; set; }
        public string strCodCCosto { get; set; }
        public string strDesCCosto { get; set; }
        public string strTipoAnalitica { get; set; }
        public int intMotivo { get; set; }
        public int intAnio { get; set; }
        public int intMes { get; set; }
        public int? intTipoDocumento { get; set; }
    }
}
