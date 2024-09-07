using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EUsuario : EAuditoria
    {
        [DataMember]
        public int usua_icod_usuario { get; set; }
        [DataMember]
        public string usua_codigo_usuario { get; set; }
        [DataMember]
        public string usua_nombre_usuario { get; set; }
        [DataMember]
        public string usua_password_usuario { get; set; }
        [DataMember]
        public bool usua_iactivo { get; set; }
        [DataMember]
        public string strEstado { get; set; }
        public Boolean usua_bflag_indicador { get; set; }
        public bool usua_bpunto_venta { get; set; }
        public int puvec_icod_punto_venta { get; set; }
    }
}
