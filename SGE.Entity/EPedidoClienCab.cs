using System;

namespace SGE.Entity
{
    public class EPedidoClienCab : EAuditoria
    {
        public int lpedi_icod_cliente { get; set; }
        public int perc_icod_personal { get; set; }
        public string lpedi_Numerolista { get; set; }
        public int cliec_icod_cliente { get; set; }
        public DateTime lpedi_sfecha_cliente { get; set; }
        public string lpedi_Observaciones { get; set; }
        public Boolean lpedi_sflag_estado { get; set; }
        public int lpedi_isituacion_prov { get; set; }
        public string StrSituacion { get; set; }

        public string cliec_vnombre_cliente { get; set; }
        public string cliec_vcod_cliente { get; set; }
        public string perc_vapellidos_nombres { get; set; }

        public int tablc_iid_tipo_ped { get; set; }
        public string StrTipoPedido { get; set; }
    }
}
