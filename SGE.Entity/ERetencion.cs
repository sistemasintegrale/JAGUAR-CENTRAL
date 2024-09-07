using System;

namespace SGE.Entity
{
    public class ERetencion : EAuditoria
    {
        public int retc_icod_comprobante_retencion { get; set; }
        public int anioc_iid_anio { get; set; }
        public int mesec_iid_mes { get; set; }
        public string retc_vnumero_comprob_reten { get; set; }
        public DateTime retc_sfec_comprob_reten { get; set; }
        public int proc_icod_cliente { get; set; }
        public int tablc_iid_moneda { get; set; }
        public decimal retc_nmto_tipo_cambio { get; set; }
        public decimal retc_nmto_total_pago { get; set; }
        public decimal retc_nmto_total_retencion { get; set; }
        public int tablc_iid_situacion { get; set; }
        public DateTime retc_sfecha_pago { get; set; }
        /**/
        public string strCliente { get; set; }
        public string strSituacion { get; set; }
        public string strMoneda { get; set; }
        public string strCodAnalitica { get; set; }
        public int intAnalitica { get; set; }

    }
}
