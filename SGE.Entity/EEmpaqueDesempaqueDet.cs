using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EEmpaqueDesempaqueDet : EAuditoria
    {
        [DataMember]
        public int empd_icod_desempaque { get; set; }
        [DataMember]
        public int empd_iitem_desempaque { get; set; }
        [DataMember]
        public string empd_Vitem_desempaque { get; set; }
        [DataMember]
        public int prdc_icod_producto { get; set; }
        [DataMember]
        public int kardc_icod_correlativo { get; set; }
        [DataMember]
        public decimal empd_dcantidad_desempaque { get; set; }
        [DataMember]
        public Boolean empd_flag_estado { get; set; }
        [DataMember]
        public int intTipoOperacion { get; set; }

    }
}
