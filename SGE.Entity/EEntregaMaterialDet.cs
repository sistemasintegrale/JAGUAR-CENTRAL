using System;

namespace SGE.Entity
{
    public class EEntregaMaterialDet : EAuditoria
    {
        public int rqmed_icod_item_requerimiento_material_entrega { get; set; }
        public int rqmad_icod_item_requerimiento_material { get; set; }
        public int rqmac_iid_requerimiento_material { get; set; }
        public DateTime rqmed_sfecha_entrega { get; set; }
        public decimal rqmed_ncantidad_entregada { get; set; }
        public bool rqmed_flag_estado { get; set; }
        public int intTipoOperacion { get; set; }
        public int rqmed_kardc_icod_correlativo { get; set; }
    }
}
