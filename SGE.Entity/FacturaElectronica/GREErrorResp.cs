namespace SGE.Entity.FacturaElectronica
{
    public class GREErrorResp
    {
        public string codRespuesta { get; set; }
        public Error error { get; set; }
        public string indCdrGenerado { get; set; }
    }

    public class Error
    {
        public string numError { get; set; }
        public string desError { get; set; }
    }
}
