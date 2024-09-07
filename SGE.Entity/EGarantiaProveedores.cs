using System;

namespace SGE.Entity
{
    public class EGarantiaProveedores : EAuditoria
    {
        public int garp_icod_garantia { get; set; }
        public string garap_vnumero_garantia { get; set; }
        public DateTime garp_sfecha_garantia { get; set; }
        public int tablc_iid_situacion { get; set; }
        public int proc_icod_proveedor { get; set; }
        public int pryc_icod_proyecto { get; set; }
        public int ocsc_icod_ocs { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal garp_nmonto { get; set; }
        public int fcoc_icod_doc { get; set; }
        public long doxpc_icod_correlativo { get; set; }
        public long pdxpc_icod_correlativo { get; set; }
        public bool garp_flag_estado { get; set; }
        /*-------------------------------------------------*/
        public string Situacion { get; set; }
        public string CentroCostos { get; set; }
        public string DesProyecto { get; set; }
        public string Moneda { get; set; }
        public string NumDoc { get; set; }
        public string NomProv { get; set; }
        public string NumOCS { get; set; }
        /*-------------------------------------------------*/
        public Int64 intDXP { get; set; }

        public string NumDXP { get; set; }
        public int ClaseDXP { get; set; }

        public string CentroCosto { get; set; }
        public string DesCC { get; set; }

    }
}
