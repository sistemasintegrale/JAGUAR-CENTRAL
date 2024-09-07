using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ECategoria : ECategoriaSubUno
    {
        [DataMember]
        public int Catc_iid_tipo_tabla { get; set; }
        [DataMember]
        public string Catc_vdescripcion { get; set; }
        [DataMember]
        public char Catc_cestado { get; set; }

        [DataMember]
        public string vidTipoTabla { get; set; }
        [DataMember]
        public string vestado { get; set; }

    }
}
