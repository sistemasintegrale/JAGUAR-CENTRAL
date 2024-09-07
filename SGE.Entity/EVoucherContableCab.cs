using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EVoucherContableCab : EAuditoria
    {
        [DataMember]
        public int anioc_iid_anio { get; set; }
        [DataMember]
        public int mesec_iid_mes { get; set; }
        [DataMember]
        public int vcocc_icod_vcontable { get; set; }
        [DataMember]
        public int subdi_icod_subdiario { get; set; }
        [DataMember]
        public string vcocc_numero_vcontable { get; set; }
        [DataMember]
        public DateTime vcocc_fecha_vcontable { get; set; }
        [DataMember]
        public string vcocc_glosa { get; set; }
        [DataMember]
        public string vNumero_documento { get; set; }
        [DataMember]
        public string vcocc_observacion { get; set; }
        [DataMember]
        public int tarec_icorrelativo_situacion_vcontable { get; set; }
        [DataMember]
        public int tarec_icorrelativo_origen_vcontable { get; set; }
        [DataMember]
        public int tablc_iid_moneda { get; set; }
        [DataMember]
        public decimal vcocc_tipo_cambio { get; set; }
        [DataMember]
        public decimal vcocc_nmto_tot_debe_sol { get; set; }
        [DataMember]
        public decimal vcocc_nmto_tot_haber_sol { get; set; }
        [DataMember]
        public decimal vcocc_nmto_tot_debe_dol { get; set; }
        [DataMember]
        public decimal vcocc_nmto_tot_haber_dol { get; set; }
        [DataMember]
        public string tbl_origen { get; set; }
        [DataMember]
        public int? tbl_origen_icod { get; set; }
        [DataMember]
        public bool vcocc_flag_estado { get; set; }
        /*---------------------------------------------------------------------*/
        [DataMember]
        public string strTipoMoneda { get; set; }
        [DataMember]
        public string strVcoSituacion { get; set; }
        [DataMember]
        public string strNroVco { get; set; }
        [DataMember]
        public string strSubdiario { get; set; }
        [DataMember]
        public int intMovimientos { get; set; }

        [DataMember]
        public string doxpc_viid_correlativo { get; set; }

        public string strTipoMoneda2 { get; set; }

    }
}
