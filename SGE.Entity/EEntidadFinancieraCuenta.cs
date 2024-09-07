using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EEntidadFinancieraCuenta

    {
        [DataMember]
        public int id_entidad_Financiera_cuenta { get; set; }
        [DataMember]
        public int id_entidad_financiera { get; set; }
        [DataMember]
        public int iid_entidad_financiera { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int iid_tipo_cuenta_ef { get; set; }
        [DataMember]
        public int iid_tipo_moneda { get; set; }
        [DataMember]
        public decimal? nmto_apertura { get; set; }
        [DataMember]
        public decimal? nmto_saldo_inicial { get; set; }
        [DataMember]
        public int iid_cuenta_contable { get; set; }
        [DataMember]
        public int icod_analitica { get; set; }
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
        public string viid_Entidad_Financiera_Cuenta { get; set; }
        [DataMember]
        public string Moneda { get; set; }
        [DataMember]
        public string CuentasMoneda { get; set; }
        [DataMember]
        public int id_correlative_Financiera_Cuenta { get; set; }
        [DataMember]
        public string Situacion { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public int id_Analitica { get; set; }
        [DataMember]
        public string id_correlative_Analitica { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int anac_icod_analitica { get; set; }
        [DataMember]
        public string anac_iid_analitica { get; set; }
        [DataMember]
        public string descripcionDetalle { get; set; }
        [DataMember]
        public int icorrelativoDetalle { get; set; }
        [DataMember]
        public int tarec_iid_tabla_registro { get; set; }
        [DataMember]
        public string TipoAnalitica { get; set; }
        [DataMember]
        public string vdes_entidad_financiera { get; set; }
        [DataMember]
        public int iid_annio { get; set; }
        [DataMember]
        public int iid_mes { get; set; }
        [DataMember]
        public DateTime? efctc_sfecha_apertura { get; set; }
        [DataMember]
        public int? mobc_motivo { get; set; }
        [DataMember]
        public decimal mto_abono_acumulado { get; set; }
        [DataMember]
        public decimal mto_cargo_acumulado { get; set; }
        [DataMember]
        public decimal mto_saldo_anterior { get; set; }
        [DataMember]
        public decimal mto_saldo_libro { get; set; }
        [DataMember]
        public decimal mto_saldo_disponible { get; set; }
        [DataMember]
        public int? icod_tipo_doc { get; set; }
    }
}

