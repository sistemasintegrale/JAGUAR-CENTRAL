namespace SGE.Entity
{
    public class EFacturaElectronicaResponse
    {
        public int IdCabezera { get; set; }
        public string NombreArchivo { get; set; }
        public string CodigoRespuesta { get; set; }
        public string MensajeRespuesta { get; set; }
        public string NroTicketCdr { get; set; }
        public string TramaZipCdr { get; set; }
        public bool Exito { get; set; }
        public int ProcesoGenerar { get; set; }
        public int ProcesoFirmar { get; set; }
        public int ProcesoEnviar { get; set; }
        public string MensajeError { get; set; }
        public string Urldescarga { get; set; }
    }
}