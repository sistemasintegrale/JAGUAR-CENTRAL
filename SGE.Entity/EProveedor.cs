using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EProveedor
    {
        [DataMember]
        public int iid_icod_proveedor { get; set; }
        [DataMember]
        public int iid_proveedor { get; set; }
        [DataMember]
        public string vcod_proveedor { get; set; }
        [DataMember]
        public string vnombre { get; set; }
        [DataMember]
        public string vpaterno { get; set; }
        [DataMember]
        public string vmaterno { get; set; }
        [DataMember]
        public string vnombrecompleto { get; set; }
        [DataMember]
        public int iid_tipo_persona { get; set; }
        [DataMember]
        public string vcomercial { get; set; }
        [DataMember]
        public string vdireccion { get; set; }
        [DataMember]
        public int? iid_distrito { get; set; }
        [DataMember]
        public int? iid_provincia { get; set; }
        [DataMember]
        public int? iid_departamento { get; set; }
        [DataMember]
        public int? idd_pais { get; set; }
        [DataMember]
        public string vtelefono { get; set; }
        [DataMember]
        public string vfax { get; set; }
        [DataMember]
        public int? isituacion { get; set; }
        [DataMember]
        public string Situacion { get; set; }
        [DataMember]
        public string vrepresentante { get; set; }
        [DataMember]
        public string vruc { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_relacion { get; set; }
        [DataMember]
        public int? iid_usuario_crea { get; set; }
        [DataMember]
        public DateTime? fecha_crea { get; set; }
        [DataMember]
        public string vpc_crea { get; set; }
        [DataMember]
        public int? iid_usuario_modifica { get; set; }
        [DataMember]
        public DateTime fecha_modifica { get; set; }
        [DataMember]
        public string vpc_modifica { get; set; }
        [DataMember]
        public int? iactivo { get; set; }
        [DataMember]
        public string vdni { get; set; }
        [DataMember]
        public string proc_vcta_bco_nacion { get; set; }
        public int proc_igiro_proveedor { get; set; }

        public string proc_vcorreo { get; set; }
        public DateTime? proc_sfecha { get; set; }
        public int? proc_tipo_doc { get; set; }
        public int? ubicc_icod_ubicacion { get; set; }
        public string strgiro { get; set; }

        [DataMember]
        public int anac_icod_analitica { get; set; }
        [DataMember]
        public string anac_iid_analitica { get; set; }
        [DataMember]
        public int? tarec_icorrelativo { get; set; }
        [DataMember]
        public int? flag_exceptuado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_saldo_soles { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_saldo_dolares { get; set; }

        public string proc_vmodalidad_pago { get; set; }
        public string proc_vbanco_pago { get; set; }
        public string proc_vcuenta_corriente_banco { get; set; }
        public string proc_vcodigo_interbancario { get; set; }
        public int? proc_pais_nodomic { get; set; }
        public string proc_responsable_produccion { get; set; }
    }
}
