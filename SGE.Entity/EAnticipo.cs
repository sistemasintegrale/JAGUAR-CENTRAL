using System;

namespace SGE.Entity
{
    public class EAnticipo : EAuditoria
    {
        public int antc_icod_anticipo { get; set; }
        public string antc_vnumero_anticipo { get; set; }
        public DateTime antc_sfecha_anticipo { get; set; }
        public int antc_icod_cliente { get; set; }
        public string antc_observaciones { get; set; }
        public decimal antc_nmonto_anticipo { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public int tablc_iid_situacion_anticipo { get; set; }
        public int antc_icod_adelanto_cliente { get; set; }
        public Int64 antc_icod_dxc_adelanto { get; set; }
        public int tablc_iid_tipo_pago { get; set; }
        public int? tablc_iid_tipo_tarjeta { get; set; }
        public int? antc_icod_entidad_finan_mov { get; set; }
    }
}
