using System.Collections.Generic;

namespace SGE.Entity
{
    public class EGuiaRemisionElectronica
    {
        public string IdDocumento { get; set; }
        public int remic_icod_remision { get; set; }
        public string FechaEmision { get; set; }
        public string HoraEmision { get; set; }
        public string TipoDocumento { get; set; }
        public string Glosa { get; set; }
        public string numDocRemitente { get; set; }
        public string tipDocRemitente { get; set; }
        public string rznSocialRemitente { get; set; }
        public string numDocDestinatario { get; set; }
        public string tipDocDestinatario { get; set; }
        public string rznSocialDestinatario { get; set; }
        public string CodigoMotivoTraslado { get; set; }
        public string DescripcionMotivo { get; set; }
        public bool Transbordo { get; set; }
        public decimal PesoBrutoTotal { get; set; }
        public int NroPallets { get; set; }
        public string ModalidadTraslado { get; set; }
        public string FechaInicioTraslado { get; set; }
        public string RucTransportista { get; set; }
        public string RazonSocialTransportista { get; set; }
        public string NroPlacaVehiculo { get; set; }
        public string NroDocumentoConductor { get; set; }
        public string ubiLlegada { get; set; }
        public string dirLlegada { get; set; }
        public string ubiPartida { get; set; }
        public string dirPartida { get; set; }
        public string obsGuia { get; set; }
        public string NumeroContenedor { get; set; }
        public string CodigoPuerto { get; set; }
        public string ShipmentId { get; set; }
        public string ObsServaciones { get; set; }
        public string UmPesoBruto { get; set; }
        public decimal NumBultos { get; set; }
        public string TipoDocumentoTransportista { get; set; }

        public List<EGuiaRemisionElectronicaDet> detalleelectronico { get; set; }
        public int EstadoEnvio { get; set; }

        public EGuiaRemisionElectronica()
        {
            detalleelectronico = new List<EGuiaRemisionElectronicaDet>();
        }
    }
}
