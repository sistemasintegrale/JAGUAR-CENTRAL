using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ETablaRegistro
    {
        [DataMember]
        public int tarec_icorrelativo_registro { get; set; }
        [DataMember]
        public string tarec_vdescripcion { get; set; }
        public string tarec_vtipo_operacion { get; set; }
        [DataMember]
        public int tarec_iid_tabla_registro { get; set; }
        [DataMember]
        public int tablc_iid_tipo_tabla { get; set; }
        [DataMember]
        public char tarec_cestado { get; set; }
        [DataMember]
        public string strEstado { get; set; }
        [DataMember]
        public int Anio_icod { get; set; }
        public int tarec_iid_tabla_registro_ingreso { get; set; }
        public int tarec_iid_tabla_registro_salida { get; set; }


    }
}
