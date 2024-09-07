using System;

namespace SGE.Entity
{
    public class ECuotasFactura : EAuditoria
    {
        public int fcc_icod { get; set; }
        public int? favc_icod_factura { get; set; }
        public int? doxcc_icod_correlativo { get; set; }
        public DateTime? fcc_sfecha_pago { get; set; }
        public decimal? fcc_nmonto { get; set; }
        public decimal? fcc_nsaldo { get; set; }
        public int? fcc_iestado { get; set; }
        public int? fcc_inro_cuota { get; set; }
        public decimal? fcc_nmonto_pagado { get; set; }
        public int operacion { get; set; }
    }
}
