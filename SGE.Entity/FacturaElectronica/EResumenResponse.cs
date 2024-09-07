namespace SGE.Entity
{
    public class EResumenResponse
    {
        public string IdItems { get; set; }
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
