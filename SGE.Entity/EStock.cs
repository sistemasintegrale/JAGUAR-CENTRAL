namespace SGE.Entity
{
    public class EStock
    {
        public int stocc_icod_stock { get; set; }
        public int stocc_ianio { get; set; }
        public int almac_icod_almacen { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal stocc_stock_producto { get; set; }
        public int intTipoMovimiento { get; set; }
        /**/
        public string strAlmacen { get; set; }
        public string strDesProducto { get; set; }
        public string strCodProducto { get; set; }
        public string strCodUM { get; set; }
        public string strDesUM { get; set; }
        public string CodigoSUNAT { get; set; }
        public int unidc_icod_unidad_medida { get; set; }
        public decimal dblPrecioCosto { get; set; }
        public decimal dblPrecioVenta { get; set; }

        public string strCategoria { get; set; }
        public string strSubCategoriaUno { get; set; }
        public string strSubCategoriaDos { get; set; }
        public string strEditorial { get; set; }
        public decimal prdc_nPrecio_soles { get; set; }
        public decimal prdc_nPrecio_dolares { get; set; }
        public decimal prdc_nPor_Descuento { get; set; }
        public decimal prdc_nPrecio_venta { get; set; }
        public decimal prdc_nPrecio_venta_Dol { get; set; }

        public int stocc_icod_stock_producto { get; set; }
        public int stocc_iid_almacen { get; set; }
        public int stocc_iid_producto { get; set; }
        public decimal stocc_nstock_prod { get; set; }
        public int puvec_icod_punto_venta { get; set; }
        public string stocc_codigo_producto { get; set; }
        public decimal stocc_nprecio_costo_prom_prod { get; set; }
        public decimal stocc_ntotal_costo_prod { get; set; }
    }
}
