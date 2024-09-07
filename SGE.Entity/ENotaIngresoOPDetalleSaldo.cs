namespace SGE.Entity
{
    public class ENotaIngresoOPDetalleSaldo : EAuditoria
    {
        public int niopds_icod_nota_ingreso_detalle_saldo { get; set; }
        public int orped_icod_item_orden_pedido { get; set; }
        public int? niopds_saldos_1 { get; set; }
        public int? niopds_saldos_2 { get; set; }
        public int? niopds_saldos_3 { get; set; }
        public int? niopds_saldos_4 { get; set; }
        public int? niopds_saldos_5 { get; set; }
        public int? niopds_saldos_6 { get; set; }
        public int? niopds_saldos_7 { get; set; }
        public int? niopds_saldos_8 { get; set; }
        public int? niopds_saldos_9 { get; set; }
        public int? niopds_saldos_10 { get; set; }
        public bool? niopds_flag_estado { get; set; }
    }
}
