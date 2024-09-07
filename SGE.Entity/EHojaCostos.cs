using System;

namespace SGE.Entity
{
    public class EHojaCostos : EAuditoria
    {
        public int hjcc_icod_hoja_costo { get; set; }
        public string hjcc_vnumero_hoja_costo { get; set; }
        public DateTime hjcc_sfecha_hoja_costo { get; set; }
        public decimal hjcc_ntipo_cambio { get; set; }
        public int tablc_iid_situacion_hc { get; set; }
        public int pryc_icod_proyecto { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public string hjcc_vdescripcion { get; set; }
        public decimal hjcc_nmonto_presup { get; set; }
        public decimal hjcc_nmonto_real { get; set; }
        public bool hjcc_flag_estado { get; set; }
        /*------------------------------------------*/
        public string Situacion { get; set; }
        public string CentroCostos { get; set; }
        public string Moneda { get; set; }
        public string pryc_vdescripcion { get; set; }
        public int pryc_icod_proyecto_2 { get; set; }
        public decimal hjcc_ntotal_soles { get; set; }
        public decimal hjcc_ntotal_dolares { get; set; }
    }
}
