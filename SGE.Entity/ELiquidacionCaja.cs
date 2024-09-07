using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class ELiquidacionCaja : EAuditoria
    {
        [DataMember]
        public int lqcc_icod_liquid_cja { get; set; }
        [DataMember]
        public int lqcc_iid_anio { get; set; }
        [DataMember]
        public int lqcc_iid_mes { get; set; }
        [DataMember]
        public int lqcc_icod_caja_liquida { get; set; }
        [DataMember]
        public int lqcc_inro_liquid_caja { get; set; }
        [DataMember]
        public DateTime lqcc_sfecha_liquid { get; set; }
        [DataMember]
        public string lqcc_vconcepto { get; set; }
        [DataMember]
        public int lqcc_iid_tipo_moneda { get; set; }
        [DataMember]
        public decimal lqcc_nmonto_total { get; set; }
        [DataMember]
        public decimal lqcc_nmonto_detalle { get; set; }
        [DataMember]
        public int lqcc_iid_situacion_liq { get; set; }
        [DataMember]
        public decimal lqcc_ntipo_cambio { get; set; }
        [DataMember]
        public int lqcc_flag_estado { get; set; }



        //***CAMPOS COMPLEMENTARIOS***//
        [DataMember]
        public string caja_nro { get; set; }
        [DataMember]
        public string caja_decripcion { get; set; }
        [DataMember]
        public string moneda { get; set; }
        [DataMember]
        public string situacion { get; set; }
        public int? vcocc_iid_voucher_contable { get; set; }
        public int caja_iid_cuenta_contable { get; set; }
        public int? caja_tipo_analitica { get; set; }
        public int? caja_icod_analitica { get; set; }
        public string strCodAnalitica { get; set; }
        //*****//   

    }
}
