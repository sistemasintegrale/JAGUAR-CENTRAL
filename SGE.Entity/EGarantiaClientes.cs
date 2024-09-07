using System;

namespace SGE.Entity
{
    public class EGarantiaClientes : EAuditoria
    {
        public int garc_icod_garantia { get; set; }
        public string garc_vnumero_garantia { get; set; }
        public DateTime garc_sfecha_garantia { get; set; }
        public int tablc_iid_situacion { get; set; }
        public int cliec_icod_cliente { get; set; }
        public int pryc_icod_proyecto { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal garc_nmonto { get; set; }
        public int favc_icod_factura { get; set; }
        public long doxpc_icod_correlativo { get; set; }
        public long pdxpc_icod_correlativo { get; set; }
        public bool garc_flag_estado { get; set; }
        /*-------------------------------------------------*/
        public string Situacion { get; set; }
        public string CentroCostos { get; set; }
        public string DesProyecto { get; set; }
        public string Moneda { get; set; }
        public string NumDoc { get; set; }
        public string NomClie { get; set; }
        /*-------------------------------------------------*/
        public Int64 intDXP { get; set; }
        /*Contabilizacion*/
        public int ClaseFac { get; set; }
        public string NumDXC { get; set; }

        public int cecoc_icod_centro_costo { get; set; }
    }
}
