namespace SGE.Entity
{
    public class ComparacionKardexStock
    {
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int IdAlmacen { get; set; }
        public string DescripcionAlmacen { get; set; }
        public decimal CantidadStock { get; set; }
        public decimal CantidadKardex { get; set; }
    }
}
