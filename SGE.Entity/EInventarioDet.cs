namespace SGE.Entity
{
    public class EInventarioDet : EAuditoria
    {
        public int invd_icod_detalle { get; set; }
        public int invc_icod_inventario { get; set; }
        public int invd_inro_item { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal invd_sis_stock { get; set; }
        public decimal invd_icantidad { get; set; }
        /**/
        public decimal dblDiferencia { get; set; }
        public string strCategoria { get; set; }
        public string strSubCategoriaUno { get; set; }
        public string strSubCategoriaDos { get; set; }
        public string strEditorial { get; set; }
        public string strCodProducto { get; set; }
        public string strDesProducto { get; set; }
        public string strUnidadMedida { get; set; }
        public int intOperacion { get; set; }
        public decimal kardv_precio_costo_promedio { get; set; }
        public decimal kardv_costo_total { get; set; }
        public decimal dcPrecioProducto { get; set; }
        public decimal dcPrecioTotal { get; set; }
        public string prdc_vAutor { get; set; }



    }
}
