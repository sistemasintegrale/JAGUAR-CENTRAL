using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class ENotaCreditoCliente
    {
        [DataMember]
        public long? doxcc_icod_correlativo { get; set; }
        [DataMember]
        public string doxcc_vnumero_doc { get; set; }
        [DataMember]
        public DateTime? doxcc_sfecha_doc { get; set; }
        [DataMember]
        public string tarec_vdescripcion { get; set; }
        [DataMember]
        public string cliec_vnombre_cliente { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public string simboloMoneda { get; set; }
        [DataMember]
        public long? cliec_icod_cliente { get; set; }
        [DataMember]
        public string tdodc_descripcion { get; set; }

        //montos
        [DataMember]
        public decimal? doxcc_nmonto_total { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_pagado { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_saldo { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_tipo_cambio { get; set; }

        [DataMember]
        public int? tablc_iid_situacion_documento { get; set; }

        //datos reporte
        [DataMember]
        public string str_doxcc_sfecha_doc { get; set; }
        [DataMember]
        public string str_doxcc_nmonto_total { get; set; }
        [DataMember]
        public string str_doxcc_nmonto_pagado { get; set; }
        [DataMember]
        public string str_doxcc_nmonto_saldo { get; set; }

    }
}
