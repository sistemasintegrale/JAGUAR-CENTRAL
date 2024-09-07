namespace SGE.Entity
{
    public class ERequerimientoMaterialesDetalle : EAuditoria
    {
        public int rqmd_icod_req_materiales_detalle { get; set; }
        public int rqmc_icod_requerimiento_materiales { get; set; }
        public string rqmd_vcodigo_item_requerim { get; set; }
        public int hjcd3_icod_rubros_hc { get; set; }
        public decimal rqmd_cantidad_pedida { get; set; }
        public decimal rqmd_cantidad_aprobada { get; set; }
        public bool rqmd_flag_estado { get; set; }
        public int intTipoOperacion { get; set; }
        /*--------------------------------------------------------*/
        public string DescripcionRubro { get; set; }
        public string Medida { get; set; }
        public decimal Cantidad { get; set; }
        public string CodigoRubro { get; set; }
        public int prdc_icod_producto { get; set; }
        /*-------------------------------------------------------*/
        public decimal Stock { get; set; }
        public string StockDiferencia { get; set; }
    }
}
