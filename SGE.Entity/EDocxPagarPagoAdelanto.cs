using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EDocxPagarPagoAdelanto : EAuditoria
    {
        [DataMember]
        public long adpap_icod_correlativo { get; set; }
        [DataMember]
        public long doxpc_icod_correlativo_pago { get; set; }
        [DataMember]
        public long doxpc_icod_correlativo_adelanto { get; set; }
        [DataMember]
        public int tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public string AbreviaturaAdelanto { get; set; }
        [DataMember]
        public string AbreviaturaPago { get; set; }
        [DataMember]
        public string vnumero_adelanto { get; set; }
        [DataMember]
        public string vnumero_pago { get; set; }
        [DataMember]
        public decimal? SaldoDXCAdelanto { get; set; }
        [DataMember]
        public int cliec_icod_cliente { get; set; }
        [DataMember]
        public int? id_tipo_moneda_adelanto { get; set; }
        [DataMember]
        public int? id_tipo_moneda_pago { get; set; }
        [DataMember]
        public string Moneda { get; set; }
        [DataMember]
        public string SimboloMoneda { get; set; }
        [DataMember]
        public decimal adpap_nmonto_pago { get; set; }
        [DataMember]
        public decimal? adpap_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public string adpap_vdescripcion { get; set; }
        [DataMember]
        public DateTime? adpap_sfecha_pago { get; set; }
        [DataMember]
        public string adpap_iorigen { get; set; }
        [DataMember]
        public int efctc_icod_enti_financiera_cuenta { get; set; }
        [DataMember]
        public int? adpap_iusuario_crea { get; set; }
        [DataMember]
        public string adpap_vpc_crea { get; set; }
        [DataMember]
        public int? adpap_iusuario_modifica { get; set; }
        [DataMember]
        public string adpap_vpc_modifica { get; set; }
        [DataMember]
        public long? pdxpc_icod_correlativo { get; set; }
        [DataMember]
        public bool adpap_flag_estado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_pagado { get; set; }
        [DataMember]
        public int? Vcorrelativo_adelanto { get; set; }
        [DataMember]
        public int? Vcorrelativo_Pago { get; set; }
        [DataMember]
        public int? vcocc_iid_voucher_contable { get; set; }
        public int intProveedor { get; set; }
        public int? mobac_icod_correlativo { get; set; }
        public int intProveedorDXP { get; set; }

    }
}
