namespace SGE.Entity
{
    public class EGuiaRemisionElectronicaDet
    {

        public int remic_icod_remision { get; set; }
        public int Correlativo { get; set; }
        public string CodigoItem { get; set; }
        public string Descripcion { get; set; }
        public string UnidadMedida { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PesoItem { get; set; }
        public int LineaReferencia { get; set; }
        public string UM { get; set; }
    }
}
