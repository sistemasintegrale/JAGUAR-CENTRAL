using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EDocPorPagarNotaCredito : EAuditoria
    {
        [DataMember]
        public long ncpap_icod_correlativo { get; set; }
        [DataMember]
        public long doxpc_icod_correlativo_pago { get; set; }
        [DataMember]
        public long doxpc_icod_correlativo_nota_credito { get; set; }
        [DataMember]
        public int tipo_documento_nota_credito { get; set; }
        [DataMember]
        public int tipo_documento_pago { get; set; }

        [DataMember]
        public string AbreviaturaNotaCredito { get; set; }
        [DataMember]
        public string AbreviaturaPago { get; set; }

        [DataMember]
        public int? idd_correlativo_nota_credito { get; set; }
        [DataMember]
        public int? idd_correlativo_pago { get; set; }

        [DataMember]
        public string doc_vnumero_nota_credito { get; set; }
        [DataMember]
        public string doc_vnumero_pago { get; set; }


        [DataMember]
        public decimal? SaldoDXCNotaCredito { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_pagado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_documento { get; set; }
        [DataMember]
        public int cliec_icod_cliente { get; set; }
        [DataMember]
        public int iid_moneda_nota_credito { get; set; }
        [DataMember]
        public int iid_moneda_pago { get; set; }

        [DataMember]
        public string Moneda { get; set; }
        [DataMember]
        public string SimboloMoneda { get; set; }
        [DataMember]
        public decimal ncpap_nmonto_pago { get; set; }
        [DataMember]
        public decimal? ncpap_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public string ncpap_vdescripcion { get; set; }
        [DataMember]
        public DateTime? ncpap_sfecha_pago { get; set; }
        [DataMember]
        public string ncpap_iorigen { get; set; }
        [DataMember]
        public int efctc_icod_enti_financiera_cuenta { get; set; }
        [DataMember]
        public long? pdxpc_icod_correlativo { get; set; }
        [DataMember]
        public bool ncpap_flag_estado { get; set; }
        /**/
        public int intProveedor { get; set; }
        public int? mobac_icod_correlativo { get; set; }

        [DataMember]
        public int? ncpac_iusuario_crea { get; set; }
        [DataMember]
        public string ncpac_vpc_crea { get; set; }
        [DataMember]
        public string ncpac_vpc_modifica { get; set; }

        public int anio_icod_anio { get; set; }

        public int TDEntidadFinanciera { get; set; }
        public string AbreviaturaEF { get; set; }
        public string NumDOCEF { get; set; }
        public string GlosaEF { get; set; }

        public int CuentaBanco { get; set; }




    }
}
