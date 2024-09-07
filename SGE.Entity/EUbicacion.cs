using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EUbicacion
    {
        [DataMember]
        public int ubicc_icod_ubicacion { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_ubicacion { get; set; }

        [DataMember]
        public string Ubicacion { get; set; }

        [DataMember]
        public string ubicc_ccod_ubicacion { get; set; }

        [DataMember]
        public string ubicc_vnombre_ubicacion { get; set; }

        [DataMember]
        public int? ubicc_icod_ubicacion_padre { get; set; }

        [DataMember]
        public int? ubicc_iid_situacion_ubicacion { get; set; }

    }
}
