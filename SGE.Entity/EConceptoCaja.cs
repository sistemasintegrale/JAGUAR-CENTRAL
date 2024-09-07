using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EConceptoCaja : EAuditoria
    {
        [DataMember]
        public int icod_concepto_caja { get; set; }
        [DataMember]
        public string ccod_concep_mov { get; set; }
        [DataMember]
        public string vdescripcion { get; set; }
        [DataMember]
        public int? iid_correlativo { get; set; }
        [DataMember]
        public long iid_cuenta_contable { get; set; }
        [DataMember]
        public int iid_situacion_cuenta { get; set; }
        [DataMember]
        public int? iusuario_crea { get; set; }
        [DataMember]
        public DateTime? sfecha_crea { get; set; }
        [DataMember]
        public string vpc_crea { get; set; }
        [DataMember]
        public int? iusuario_modifica { get; set; }
        [DataMember]
        public DateTime? sfecha_modifica { get; set; }
        [DataMember]
        public string vpc_modifica { get; set; }
        [DataMember]
        public string viid_concepto { get; set; }
        [DataMember]
        public string Estado { get; set; }

        //-----//
        [DataMember]
        public int? tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public string tdodc_iid_codigo_doc_det { get; set; }
        //-----//
        [DataMember]
        public string cuenta_vdes { get; set; }
        [DataMember]
        public string tipo_analitica { get; set; }
        [DataMember]
        public bool ccosto_flag { get; set; }
        [DataMember]
        public string doc_vdes { get; set; }
        [DataMember]
        public string cuenta_ambos { get; set; }
        [DataMember]
        public int? cuenta { get; set; }
        public int cmvcc_icod_tipo { get; set; }
        public string strTipo { get; set; }
    }
}
