using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EDocXCobrarDocxPagarCanje : EAuditoria
    {
        //DATOS DXC
        [DataMember]
        public long? doxcc_icod_correlativo { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_saldo { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_pagado { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_total { get; set; }
        [DataMember]
        public string doxcc_vnumero_doc { get; set; }
        [DataMember]
        public int? tipo_doc_dxc { get; set; }
        [DataMember]
        public string tipo_doc_abr_dxc { get; set; }
        [DataMember]
        public int? cliec_icod_cliente { get; set; }
        [DataMember]
        public string cliec_vnombre_cliente { get; set; }
        [DataMember]
        public int? tipo_moneda_dxc { get; set; }
        [DataMember]
        public int? clase_documento_dxc { get; set; }
        [DataMember]
        public DateTime? doxcc_sfecha_doc { get; set; }
        [DataMember]
        public string doxpc_vdescrip_transaccion { get; set; }
        [DataMember]
        public string simboloMoneda_dxc { get; set; }

        //DATOS DXP
        [DataMember]
        public long? doxpc_icod_correlativo { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_saldo { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_pagado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_documento { get; set; }
        [DataMember]
        public string doxpc_vnumero_doc { get; set; }
        [DataMember]
        public int? tipo_doc_dxp { get; set; }
        [DataMember]
        public string tipo_doc_abr_dxp { get; set; }
        [DataMember]
        public int? proc_icod_proveedor { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
        [DataMember]
        public int? tipo_moneda_dxp { get; set; }
        [DataMember]
        public int? clase_documento_dxp { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_doc { get; set; }
        [DataMember]
        public string simboloMoneda_dxp { get; set; }

        //DATOS DXC PAGO
        [DataMember]
        public long? pdxcc_icod_correlativo { get; set; }
        [DataMember]
        public decimal? pdxcc_nmonto_cobro { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_cobro { get; set; }
        [DataMember]
        public int pdxcc_isituacion { get; set; }

        //DATOS DXP PAGO
        [DataMember]
        public long pdxpc_icod_correlativo { get; set; }
        [DataMember]
        public decimal? pdxpc_nmonto_pago { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_pago { get; set; }
        [DataMember]
        public int pdxpc_isituacion { get; set; }
        [DataMember]
        public int pdxpc_mes { get; set; }
        [DataMember]
        public int? voucher_cont_dxp { get; set; }

        //DATOS DEL CANJE
        [DataMember]
        public long? canjec_icod_correlativo { get; set; }
        [DataMember]
        public int? tipo_moneda_canje { get; set; }
        [DataMember]
        public decimal? canjec_nmonto_pago { get; set; }
        [DataMember]
        public string canjec_vobservacion { get; set; }
        [DataMember]
        public decimal? canjec_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public bool flag_estado { get; set; }
        [DataMember]
        public string canjec_iorigen { get; set; }
        [DataMember]
        public int canjec_isituacion { get; set; }
        [DataMember]
        public DateTime fecha_pago { get; set; }

        public int? tipo_moneda_canjeDXC { get; set; }
    }
}
