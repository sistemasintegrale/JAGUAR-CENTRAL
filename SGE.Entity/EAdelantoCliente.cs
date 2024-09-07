using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EAdelantoCliente
    {
        [DataMember]
        public int icod_correlativo { get; set; }
        [DataMember]
        public int icod_correlativo_cabecera { get; set; }
        [DataMember]
        public int? mobac_icod_correlativo { get; set; }
        [DataMember]
        public string vnumero_adelanto { get; set; }
        [DataMember]
        public int icod_cliente { get; set; }
        [DataMember]
        public int iid_tipo_doc { get; set; }
        [DataMember]
        public DateTime adci_sfecha_adelanto { get; set; }
        [DataMember]
        public string vnumero_documento { get; set; }
        [DataMember]
        public DateTime sfecha_adelanto { get; set; }
        [DataMember]
        public int iid_tipo_moneda { get; set; }
        [DataMember]
        public decimal nmonto_tipo_cambio { get; set; }
        [DataMember]
        public decimal nmonto_adelanto { get; set; }
        [DataMember]
        public decimal nmonto_pagado { get; set; }
        [DataMember]
        public decimal nmonto_saldo { get; set; }
        [DataMember]
        public string vobservacion { get; set; }
        [DataMember]
        public int nsituacion_adelanto_cliente { get; set; }
        [DataMember]
        public int iusuario_crea { get; set; }
        [DataMember]
        public DateTime sfecha_crea { get; set; }
        [DataMember]
        public string vpc_crea { get; set; }
        [DataMember]
        public int iusuario_modifica { get; set; }
        [DataMember]
        public DateTime sfecha_modifica { get; set; }
        [DataMember]
        public string vpc_modifica { get; set; }
        [DataMember]
        public string vnombrecliente { get; set; }
        [DataMember]
        public int iid_anio { get; set; }
        [DataMember]
        public int iid_mes { get; set; }
        [DataMember]
        public int ii_tipo_doc { get; set; }
        [DataMember]
        public string vdescripcion_beneficiario { get; set; }
        [DataMember]
        public bool cflag_conciliacion { get; set; }
        [DataMember]
        public decimal? nmonto_movimiento { get; set; }
        [DataMember]
        public string vnro_documento { get; set; }
        [DataMember]
        public string vglosa { get; set; }
        [DataMember]
        public bool flag_estado { get; set; }
        [DataMember]
        public int iid_situacion_movimiento_banco { get; set; }
        [DataMember]
        public Int64 doxcc_icod_correlativo { get; set; }
        [DataMember]
        public string Situacion { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public string SimboloMoneda { get; set; }
        [DataMember]
        public string moneda { get; set; }
        [DataMember]
        public string efctc_vnumero_cuenta { get; set; }
    }
}
