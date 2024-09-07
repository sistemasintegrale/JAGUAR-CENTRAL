using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EConceptoPresupuestoNacionalDetalle
    {
        [DataMember]
        public int cpnd_icod_detalle_nacional { get; set; }
        [DataMember]
        public int cpn_icod_concepto_nacional { get; set; }
        [DataMember]
        public int cpnd_iid_detalle_nacional { get; set; }
        [DataMember]
        public string cpnd_viid_detalle_nacional { get; set; }
        [DataMember]
        public int? ctacc_iid_cuenta_contable { get; set; }
        [DataMember]
        public string ctacc_vnombre_descripcion_larga { get; set; }
        [DataMember]
        public string ctacc_vnumero_cuenta_contable { get; set; }
        [DataMember]
        public string cpnd_vdescripcion { get; set; }
        [DataMember]
        public int cpnd_iid_situacion_detalle { get; set; }
        [DataMember]
        public int cpnd_iusuario_crea { get; set; }
        [DataMember]
        public DateTime? cpnd_sfecha_crea { get; set; }
        [DataMember]
        public string cpnd_vpc_crea { get; set; }
        [DataMember]
        public int cpnd_iusuario_modifica { get; set; }
        [DataMember]
        public DateTime? cpnd_sfecha_modifica { get; set; }
        [DataMember]
        public string cpnd_vpc_modifica { get; set; }
        [DataMember]
        public string cimcc_viid_concepto_nacional { get; set; }
        [DataMember]
        public string cimdc_viid_detalle_concepto { get; set; }
        [DataMember]
        public bool cpnd_flag_estado { get; set; }
    }
}
