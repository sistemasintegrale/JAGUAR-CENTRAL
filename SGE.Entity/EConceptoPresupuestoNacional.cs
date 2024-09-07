using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EConceptoPresupuestoNacional
    {
        [DataMember]
        public int cpn_icod_concepto_nacional { get; set; }
        [DataMember]
        public int? cpn_iid_concepto_nacional { get; set; }
        [DataMember]
        public string cpn_viid_concepto_nacional { get; set; }
        [DataMember]
        public string cpn_vdescripcion_concepto_nacional { get; set; }
        [DataMember]
        public int? cpn_iid_situacion_concepto_nacional { get; set; }
        [DataMember]
        public int? cpn_iusuario_crea { get; set; }
        [DataMember]
        public DateTime? cpn_sfecha_crea { get; set; }
        [DataMember]
        public string cpn_vpc_crea { get; set; }
        [DataMember]
        public int? cpn_iusuario_modifica { get; set; }
        [DataMember]
        public DateTime? cpn_sfecha_modifica { get; set; }
        [DataMember]
        public string cpn_vpc_modifica { get; set; }
    }
}
