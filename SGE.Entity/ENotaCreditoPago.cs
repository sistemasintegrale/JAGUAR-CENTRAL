using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ENotaCreditoPago : EAuditoria
    {
        [DataMember]
        public long ncpac_icod_correlativo { get; set; }
        [DataMember]
        public long doxcc_icod_correlativo_pago { get; set; }
        [DataMember]
        public long doxcc_icod_correlativo_nota_credito { get; set; }
        [DataMember]
        public int tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public string AbreviaturaNotaCredito { get; set; }
        [DataMember]
        public string AbreviaturaPago { get; set; }
        [DataMember]
        public int? iid_correlativo_nota_credito { get; set; }
        [DataMember]
        public int? iid_correlativo_pago { get; set; }
        [DataMember]
        public string vnumero_documento_NC { get; set; }
        [DataMember]
        public string vnumero_documento_pago { get; set; }
        [DataMember]
        public decimal? SaldoDXCNotaCredito { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_pagado { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_total { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public string SimboloMoneda { get; set; }
        [DataMember]
        public decimal ncpac_nmonto_pago { get; set; }
        [DataMember]
        public decimal? ncpac_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public string ncpac_vdescripcion { get; set; }
        [DataMember]
        public DateTime? ncpac_sfecha_pago { get; set; }
        [DataMember]
        public string ncpac_iorigen { get; set; }
        [DataMember]
        public int efctc_icod_enti_financiera_cuenta { get; set; }
        [DataMember]
        public int? ncpac_iusuario_crea { get; set; }
        [DataMember]
        public string ncpac_vpc_crea { get; set; }
        [DataMember]
        public int? ncpac_iusuario_modifica { get; set; }
        [DataMember]
        public string ncpac_vpc_modifica { get; set; }
        [DataMember]
        public long? pdxcc_icod_correlativo { get; set; }
        [DataMember]
        public bool ncpac_flag_estado { get; set; }
        public int? mobac_icod_correlativo { get; set; }
        public int iid_tipo_moneda_pago { get; set; }
        /**/
        public int intCliente { get; set; }

        public int TDEntidadFinanciera { get; set; }
        public string AbreviaturaEF { get; set; }
        public string NumDOCEF { get; set; }
        public string GlosaEF { get; set; }

        public int CuentaBanco { get; set; }

    }
}
