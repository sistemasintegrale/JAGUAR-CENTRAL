namespace SGE.Entity.FacturaElectronica
{
    public class GenerarTokenrequest
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string ruc { get; set; }
        public string endPoint { get; set; }

    }
}
