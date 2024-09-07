using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EEventoVenta : EAuditoria
    {
        [DataMember]
        public int evev_icod_evento_venta { get; set; }
        [DataMember]
        public string evev_vnumero_evento_venta { get; set; }
        [DataMember]
        public int evev_isituacion_even_venta { get; set; }
        [DataMember]
        public string evev_vlugar_evento_venta { get; set; }
        [DataMember]
        public string evev_vDirecc_evento_venta { get; set; }
        [DataMember]
        public string evev_vcorreo_evento_venta { get; set; }
        [DataMember]
        public string evev_vcontac_evento_venta { get; set; }

        [DataMember]
        public string evev_vTelefo_evento_venta { get; set; }
        [DataMember]
        public DateTime? evev_sfecha_evento_inicio { get; set; }
        [DataMember]
        public DateTime? evev_sfecha_evento_final { get; set; }
        [DataMember]
        public int almac_icod_almacen { get; set; }
        [DataMember]
        public string almac_vresponsa_even_venta { get; set; }
        public Boolean evev_flag_estado { get; set; }
        public string almac_vdescripcion { get; set; }
        public string desSituacion { get; set; }
        public string evev_vnombre_evento_venta { get; set; }
    }
}
