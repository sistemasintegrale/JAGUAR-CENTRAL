using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ECategoriaSubUno : ECategoriaSubDos
    {
        [DataMember]
        public int CatNUno_iid_tabla_registro { get; set; }
        [DataMember]
        public int CatNUno_iid_tipo_tabla { get; set; }
        [DataMember]
        public int CatNUno_icorrelativo_registro { get; set; }
        [DataMember]
        public string CatNUno_vdescripcion { get; set; }
        [DataMember]
        public decimal CatNUno_nvalor_numerico { get; set; }
        [DataMember]
        public string CatNUno_vvalor_texto { get; set; }
        [DataMember]
        public char CatNUno_cestado { get; set; }
        [DataMember]
        public Boolean Seleccion { get; set; }

        [DataMember]
        public string tarec_viid_correlativo { get; set; }

    }
}
