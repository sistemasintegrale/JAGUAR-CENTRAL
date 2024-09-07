namespace SGE.Entity
{
    public class EProductividad
    {
        public int icod_personal { get; set; }
        public int idd_personal { get; set; }
        public string nombre_personal { get; set; }
        public string cargo_personal { get; set; }
        public decimal cantidad_servicios { get; set; }
        public decimal total_comision { get; set; }
    }
}
