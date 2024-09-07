using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ECategoriaSubDos : EAuditoria
    {
        [DataMember]
        public int CatNDos_iid_tabla_registro { get; set; }
        [DataMember]
        public int CatNUno_iid_tabla_registro { get; set; }
        [DataMember]
        public int CatNDos_icorrelativo_registro { get; set; }
        [DataMember]
        public string CatNDos_vdescripcion { get; set; }
        [DataMember]
        public decimal CatNDos_nvalor_numerico { get; set; }
        [DataMember]
        public string CatNDos_vvalor_texto { get; set; }
        [DataMember]
        public char CatNDos_cestado { get; set; }
        [DataMember]
        public Boolean Seleccion { get; set; }

        [DataMember]
        public string tarec_viid_correlativo { get; set; }

    }
}
