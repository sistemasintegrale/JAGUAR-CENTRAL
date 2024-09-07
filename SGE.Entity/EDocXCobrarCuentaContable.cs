using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EDocXCobrarCuentaContable : EAuditoria
    {
        [DataMember]
        public long ccdcc_icod_correlativo { get; set; }
        [DataMember]
        public long doxcc_icod_correlativo { get; set; }
        [DataMember]
        public long ctacc_iid_cuenta_contable { get; set; }
        [DataMember]
        public string CuentaContable { get; set; }
        [DataMember]
        public string DescripcionCuentaContable { get; set; }
        [DataMember]
        public int? cecoc_icod_centro_costo { get; set; }
        [DataMember]
        public string CentroCosto { get; set; }
        [DataMember]
        public string DescripcionCentroCosto { get; set; }
        [DataMember]
        public int? IndicadorCentroCosto { get; set; }
        [DataMember]
        public int? anac_icod_analitica { get; set; }
        [DataMember]
        public string TipoAnalitica { get; set; }
        [DataMember]
        public string Analitica { get; set; }
        [DataMember]
        public decimal ccdcc_nmonto { get; set; }
        [DataMember]
        public string ccdcc_vglosa { get; set; }
        [DataMember]
        public int ccdcc_isituacion { get; set; }
        [DataMember]
        public int operacion { get; set; }
        [DataMember]
        public int? usuario { get; set; }
        [DataMember]
        public string pc { get; set; }
        [DataMember]
        public bool ccdcc_flag_estado { get; set; }
    }
}
