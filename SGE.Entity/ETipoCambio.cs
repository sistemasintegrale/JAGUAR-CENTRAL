using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ETipoCambio : EAuditoria
    {
        [DataMember]
        public int ticac_icod_tipo_cambio { get; set; }
        [DataMember]
        public DateTime ticac_fecha_tipo_cambio { get; set; }
        [DataMember]
        public decimal ticac_tipo_cambio_compra { get; set; }
        [DataMember]
        public decimal ticac_tipo_cambio_venta { get; set; }
        public bool ticac_flag_estado { get; set; }
    }
}
