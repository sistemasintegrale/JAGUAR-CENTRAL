using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ELibroBancos
    {
        [DataMember]
        public int Dia { get; set; }
        [DataMember]
        public string Mes { get; set; }
        [DataMember]
        public int iid_correlativo { get; set; }
        [DataMember]
        public int icod_entidad_financiera { get; set; }
        [DataMember]
        public int? iid_entidad_financiera { get; set; }
        [DataMember]
        public string Numero_cuenta { get; set; }
        [DataMember]
        public string Descripcion_Banco { get; set; }
        [DataMember]
        public int icod_correlativo { get; set; }
        [DataMember]
        public int iid_anio { get; set; }
        [DataMember]
        public int iid_mes { get; set; }
        [DataMember]
        public int icod_enti_financiera_cuenta { get; set; }
        [DataMember]
        public int? ii_tipo_doc { get; set; }
        [DataMember]
        public DateTime sfecha_cheque { get; set; }
        [DataMember]
        public string cflag_tipo_movimiento { get; set; }
        [DataMember]
        public int iid_motivo_mov_banco { get; set; }
        [DataMember]
        public string vdescripcion_beneficiario { get; set; }
        [DataMember]
        public int iid_tipo_moneda { get; set; }
        [DataMember]
        public string TipoMoneda { get; set; }
        [DataMember]
        public decimal nmonto_tipo_cambio { get; set; }
        [DataMember]
        public decimal nmonto_movimiento { get; set; }
        [DataMember]
        public decimal nmonto_movimiento_dolares { get; set; }
        [DataMember]
        public bool cflag_conciliacion { get; set; }
        [DataMember]
        public string sflag_conciliacion { get; set; }
        [DataMember]
        public decimal nmonto_saldo_banco { get; set; }
        [DataMember]
        public int iid_situacion_movimiento_banco { get; set; }
        [DataMember]
        public string inumero_orden { get; set; }
        [DataMember]
        public int iid_origen_mov_banco { get; set; }
        [DataMember]
        public string vnro_planilla_liquidacion { get; set; }
        [DataMember]
        public string vnro_retencion { get; set; }

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
        public DateTime? dfecha_movimiento { get; set; }
        [DataMember]
        public DateTime? dfecha_crea { get; set; }
        [DataMember]
        public string vnro_documento { get; set; }
        [DataMember]
        public string vglosa { get; set; }
        [DataMember]
        public string Situacion { get; set; }
        [DataMember]
        public decimal? Cargo { get; set; }
        [DataMember]
        public decimal? Abono { get; set; }
        [DataMember]
        public int idc_estado_Libro_Bancos { get; set; }
        [DataMember]
        public string descripcion_Libro_Bancos { get; set; }
        [DataMember]
        public int tarec_icorrelativo_registro { get; set; }
        [DataMember]
        public decimal SaldoAnterior { get; set; }
        [DataMember]
        public decimal SaldoLibro { get; set; }
        [DataMember]
        public decimal SaldoDisponible { get; set; }
        [DataMember]
        public int RowNumber { get; set; }
        [DataMember]
        public string TipoDocumento { get; set; }
        [DataMember]
        public string TipoDocAbreviado { get; set; }
        [DataMember]
        public string MotivoBanco { get; set; }
        [DataMember]
        public string documento { get; set; }
        [DataMember]
        public int anac_icod_analitica { get; set; }
        [DataMember]
        public int anad_iid_analitica { get; set; }
        [DataMember]
        public int? proc_icod_proveedor { get; set; }
        [DataMember]
        public string proc_vcod_proveedor { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
        [DataMember]
        public bool bProveedorNoExceptuadoRetencion { get; set; }
        [DataMember]
        public bool mobac_flag_estado { get; set; }
        [DataMember]
        public bool cflag_pase { get; set; }
        [DataMember]
        public int anac_icod_analitica_cliente { get; set; }
        [DataMember]
        public int? cliec_icod_cliente { get; set; }
        [DataMember]
        public string cliec_vnombre_cliente { get; set; }
        public string tdocc_vdescripcion { get; set; }

        public decimal? mobac_nmonto_saldo { get; set; }
        public decimal? mobac_nmonto_disponible { get; set; }
        public decimal? mobac_nmonto_abono_ant { get; set; }
        public decimal? mobac_nmonto_cargo_ant { get; set; }

        public decimal? mobac_nmonto_disponible_ant { get; set; }
        public decimal? mobac_nmonto_saldo_ant { get; set; }

        public decimal? mto_total { get; set; }
        public string mov { get; set; }
        public int? id_transferencia { get; set; }
        /**/
        public DateTime? mobac_sfecha_diferida { get; set; }
        public string mobac_origen_regitro { get; set; }
        public Image imagen { get; set; }
    }
}
