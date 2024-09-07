using System;

namespace SGE.Entity
{
    public class ETipoCambioEuro : EAuditoria
    {
        public int tceu_icod_tipo_cambio_euro { get; set; }
        public DateTime tceu_sfecha_tipo_cambio_euro { get; set; }
        public decimal tceu_tipo_cambio_euro_compra { get; set; }
        public decimal tceu_tipo_cambio_euro_venta { get; set; }
        public bool tceu_flag_estado { get; set; }

    }
}
