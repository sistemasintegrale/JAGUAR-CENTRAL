namespace SGE.Common
{
    public static class Codigos
    {
        public enum PersonalSituacion
        {
            Id = 28,
            Activo = 45146,
            Inactivo = 45147
        }

        public enum MotivoSalidaAlmacen
        {
            Id = 35,
            Ventas = 101,
            TransferenciaEntreAlmacenes = 102,
            Devolucion = 103,
            SaldoInicial = 152,
            Otros = 194,
            Transformacion = 645,
        }

        public enum MotivoGuiaRemision
        {
            Id = 51,
            Venta = 221,
            Compra = 222,
            ParaTransformacion = 223,
            Consignacion = 224,
            Devolucion = 225,
            TransfEntreEstablecimientosMismaEmpr = 226,
            Exportacion = 227,
            Otros = 228,
            VentaSujetaConfirmar = 649,
            CentaConEntregaTerceros = 650,
            RecojoDeBienesTransformados = 651,
            EmisorItinerante = 652,
            ZonaPrimaria = 653,
            Importacion = 654
        }

        public enum TipoProducto
        {
            Mercadería = 1,
            Servicio = 2
        }

        public enum UnidadMedida
        {
            Servicio = 35
        }

        public enum TipoGuiaRemision
        {
            ProductoTerminado = 1,
            Suministros = 2
        }
        public enum TipoDocumentoPersonal
        {
            Id = 23,
            Dni = 1,
            CarnetDeExtrangería = 4,
            Ruc = 6,
            Pasaporte = 7,
            PartidaDeNacimiento = 11,
            Otros = 0
        }

        public enum SituacionEntregaOrdenPedidoItem
        {
            id = 100,
            Pendiente = 5685,
            EntregaParcial = 5686,
            EntregaTotal = 5687,
        }
    }

}
