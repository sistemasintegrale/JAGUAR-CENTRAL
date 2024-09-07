namespace SGE.Entity
{
    public class EStockValorizado
    {
        public int stocv_icod_stock { get; set; }
        public int stocv_ianio { get; set; }
        public int almcc_icod_almacen { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal stocv_nstock_prod { get; set; }
        public decimal stocv_nprecio_costo_prom_prod { get; set; }
        public decimal stocv_ntotal_costo_prod { get; set; }

        public string strDesAlmacenCtbl { get; set; }
        public string strCodProducto { get; set; }
        public string strDesProducto { get; set; }
        public string strCodUnidadMedida { get; set; }
    }
}
