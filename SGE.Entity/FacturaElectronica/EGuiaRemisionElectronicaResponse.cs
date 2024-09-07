using System;

namespace SGE.Entity.FacturaElectronica
{
    public class EGuiaRemisionElectronicaResponse
    {
        public int remic_icod_remision { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool Exito { get; set; }
        public string MensajeError { get; set; }
        public string NumeroTicket { get; set; }
        public int CodigoRespuesta { get; set; }
        public string MensajeRespuesta { get; set; }
        public string LinkDescarga { get; set; }
    }
}
