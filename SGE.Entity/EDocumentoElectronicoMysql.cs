namespace SGE.Entity
{
    public class EDocumentoElectronicoMysql
    {
        public string ruc { get; set; }
        public string numero_documento { get; set; }
        public string tipo_documento { get; set; }
        public string fecha_emision { get; set; }
        public decimal monto_precio { get; set; }
        public string xml_bytes { get; set; }
        public string pdf_bytes { get; set; }
    }
}
