namespace SGE.Entity
{
    public class ECostoVenta
    {
        public decimal dfacc_ncantidad_producto { get; set; }
        public decimal pcp { get; set; }
        public int? iid_cuenta_costos { get; set; }
        public int? iid_cuenta_producto { get; set; }
        public int? iid_centro_costos { get; set; }
        public string codigo_centro_costos { get; set; }
        public string descripcion { get; set; }
        public int ctacc_icod_cuenta_contable { get; set; }
    }
}
