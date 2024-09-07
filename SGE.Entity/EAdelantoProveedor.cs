using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EAdelantoProveedor
    {
        [DataMember]
        public int icod_correlativo { get; set; }
        [DataMember]
        public int icod_correlativo_cabecera { get; set; }
        [DataMember]
        public string vnumero_adelanto { get; set; }
        [DataMember]
        public int icod_proveedor { get; set; }
        [DataMember]
        public int iid_tipo_doc { get; set; }
        [DataMember]
        public string vnumero_documento { get; set; }
        [DataMember]
        public DateTime sfecha_adelanto { get; set; }
        [DataMember]
        public int iid_tipo_moneda { get; set; }
        [DataMember]
        public string SimboloMoneda { get; set; }

        [DataMember]
        public decimal nmonto_tipo_cambio { get; set; }
        [DataMember]
        public decimal nmonto_adelanto { get; set; }
        [DataMember]
        public decimal nmonto_canjeado { get; set; }
        [DataMember]
        public decimal nmonto_saldo { get; set; }
        [DataMember]
        public string vobservacion { get; set; }
        [DataMember]
        public int nsituacion_adelanto_proveedor { get; set; }
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
        public int proc_icod_proveedor { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
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
        public bool flag_estado { get; set; }
        [DataMember]
        public string vglosa { get; set; }
        [DataMember]
        public int iid_situacion_movimiento_banco { get; set; }
        [DataMember]
        public Int64? doxpc_icod_correlativo { get; set; }
        [DataMember]
        public string Situacion { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public string moneda { get; set; }
        [DataMember]
        public string efctc_vnumero_cuenta { get; set; }
    }
}
