using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EAdelantoPago : EAuditoria
    {
        [DataMember]
        public long adpac_icod_correlativo { get; set; }
        [DataMember]
        public long doxcc_icod_correlativo_pago { get; set; }
        [DataMember]
        public long doxcc_icod_correlativo_adelanto { get; set; }
        [DataMember]
        public int? tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public int tdocc_iid_correlativo_pago { get; set; }
        [DataMember]
        public string AbreviaturaAdelanto { get; set; }
        [DataMember]
        public string AbreviaturaPago { get; set; }
        [DataMember]
        public string vnumero_doc_adelanto { get; set; }
        [DataMember]
        public string vnumero_doc_pago { get; set; }
        [DataMember]
        public decimal? SaldoDXCAdelanto { get; set; }
        [DataMember]
        public int cliec_icod_cliente { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public string Moneda { get; set; }
        [DataMember]
        public string SimboloMoneda { get; set; }
        [DataMember]
        public decimal adpac_nmonto_pago { get; set; }
        [DataMember]
        public decimal? adpac_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public string adpac_vdescripcion { get; set; }
        [DataMember]
        public DateTime? adpac_sfecha_pago { get; set; }
        [DataMember]
        public string adpac_iorigen { get; set; }
        [DataMember]
        public int efctc_icod_enti_financiera_cuenta { get; set; }
        [DataMember]
        public Int64? pdxcc_icod_correlativo { get; set; }
        [DataMember]
        public int? adpac_iusuario_crea { get; set; }
        [DataMember]
        public string adpac_vpc_crea { get; set; }
        [DataMember]
        public int? adpac_iusuario_modifica { get; set; }
        [DataMember]
        public string adpac_vpc_modifica { get; set; }
        [DataMember]
        public bool adpac_flag_estado { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_pagado { get; set; }
        [DataMember]
        public string doxcc_vnumero_doc { get; set; }
        [DataMember]
        public int? tdocc_iid_correlativo_adelanto { get; set; }
        public int? mobac_icod_correlativo { get; set; }
        public int iid_tipo_moneda_pago { get; set; }

    }
}
