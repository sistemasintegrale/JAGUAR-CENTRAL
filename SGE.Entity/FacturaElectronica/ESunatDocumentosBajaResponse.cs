namespace SGE.Entity.FacturaElectronica
{
    public class ESunatDocumentosBajaResponse
    {
        public string IdItems { get; set; }
        public int IdCabecera { get; set; }
        public string NroTicket { get; set; }
        public string NombreArchivo { get; set; }
        public bool Exito { get; set; }
        public string MensajeError { get; set; }
        public string Pila { get; set; }
        public string CodigoRespuesta { get; set; }
        public int ProcesoFirmar { get; set; }
        public int ProcesoEnviar { get; set; }
        public int ProcesoGenerar { get; set; }
        public string IdDocumento { get; set; }
    }
}
