namespace SGE.Entity
{
    public class EFacturaVentaDetalleElectronico
    {
        public int IdItems { get; set; }
        public int IdCabecera { get; set; }
        public int NumeroOrdenItem { get; set; }
        public decimal cantidad { get; set; }

        public string unidadMedida { get; set; }
        public decimal ValorVentaItem { get; set; }
        public int CodMotivoDescuentoItem { get; set; }
        public decimal FactorDescuentoItem { get; set; }
        public decimal DescuentoItem { get; set; }
        public decimal BaseDescuentotem { get; set; }
        public int CodMotivoCargoItem { get; set; }
        public decimal FactorCargoItem { get; set; }
        public decimal MontoCargoItem { get; set; }
        public decimal BaseCargoItem { get; set; }
        public decimal MontoTotalImpuestosItem { get; set; }
        public decimal MontoImpuestoIgvItem { get; set; }
        public decimal MontoAfectoImpuestoIgv { get; set; }
        public decimal PorcentajeIGVItem { get; set; }
        public decimal MontoImpuestoISCItem { get; set; }
        public decimal MontoAfectoImpuestoIsc { get; set; }
        public decimal PorcentajeISCtem { get; set; }
        public decimal MontoImpuestoIVAPtem { get; set; }
        public decimal MontoAfectoImpuestoIVAPItem { get; set; }
        public decimal PorcentajeIVAPItem { get; set; }
        public string descripcion { get; set; }
        public string codigoItem { get; set; }
        public string ObservacionesItem { get; set; }
        public decimal ValorUnitarioItem { get; set; }
        public string tipoImpuesto { get; set; }
        public string descripcionO { get; set; }
        public string placaVehiculo { get; set; }
        public string UMedida { get; set; }
        public string CodConceptoAsignarItem { get; set; }
        public string DescripcionAdicionalItem { get; set; }
        public decimal MontoInafectoItem { get; set; }
        public decimal PrecioVentaUnitarioItem { get; set; }
    }
}