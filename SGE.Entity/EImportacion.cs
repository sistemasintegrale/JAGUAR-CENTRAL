using System;

namespace SGE.Entity
{
    public class EImportacion : EAuditoria
    {
        public int impc_icod_importacion { set; get; }
        public string impc_vnumero_importacion { set; get; }
        public DateTime? impc_sfecha_importacion { set; get; }
        public int? tablc_iid_sit_import { set; get; }
        public int? add_icod_aduana { set; get; }
        public DateTime? impc_sfecha_embarque { set; get; }
        public string impc_vconoc_embarque { set; get; }
        public string impc_vprocedencia { set; get; }
        public string impc_vemp_transporte { set; get; }
        public string impc_vnave { set; get; }
        public string impc_vdua { set; get; }
        public DateTime? impc_sfecha_arribo { set; get; }
        public DateTime? impc_sfecha_ingreso { set; get; }
        public int? tablc_iid_tipo_moneda { set; get; }
        public decimal? impc_nfactor_dolar { set; get; }
        public decimal? impc_nfactor_sol { set; get; }
        public bool? impc_flag_estado { set; get; }

        public string add_vrazon { set; get; }
        public string impc_vguia_ingreso { set; get; }
        public int? pryc_icod_proyecto { set; get; }
        public string strSituacion { set; get; }
        public string strAduana { set; get; }
        public string strMondeda { set; get; }
        public string strProyecto { set; get; }
        public string strFactor { set; get; }


        public decimal? strSumSoles { set; get; }
        public decimal? strSumDolares { set; get; }
        public decimal? strFactSoles { set; get; }
        public decimal? strFactDolares { set; get; }

        /*Ingreso Kardex*/
        public int almac_icod_almacen { get; set; }
        public DateTime? fcoc_sfecha_doc { get; set; }

        public decimal impc_nmonto_total_soles { get; set; }
        public decimal impc_nmonto_total_dolares { get; set; }
    }
}
