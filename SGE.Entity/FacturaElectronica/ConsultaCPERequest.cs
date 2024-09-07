namespace SGE.Entity.FacturaElectronica
{
    public class ConsultaCPERequest
    {
        public string numRuc { get; set; }
        public string codComp { get; set; }
        public string numeroSerie { get; set; }
        public string numero { get; set; }
        public string fechaEmision { get; set; }
        public string monto { get; set; }
        public string rucConsulta { get; set; }
    }
}
