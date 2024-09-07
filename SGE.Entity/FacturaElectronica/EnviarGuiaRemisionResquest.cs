namespace SGE.Entity.FacturaElectronica
{
    public class EnviarGuiaRemisionResquest
    {
        public string nombreDoc { get; set; }
        public string tramaZimp { get; set; }
        public string hash { get; set; }
        public string token { get; set; }
        public string endPoint { get; set; }
    }
}
