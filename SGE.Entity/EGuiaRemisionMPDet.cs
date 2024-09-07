using System;

namespace SGE.Entity
{
    public class EGuiaRemisionMPDet : EAuditoria
    {
        public int dremc_icod_detalle_remision { get; set; }
        public int remic_icod_remision { get; set; }
        public Int16 dremc_inro_item { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal dremc_ncantidad_producto { get; set; }
        public decimal dremc_nMonto_Total { get; set; }
        public string dremc_vcantidad_producto { get; set; }
        public int? kardc_icod_correlativo { get; set; }

        public int? tablc_iid_sit_item_guia_remision { get; set; }

        public string strCodProducto { get; set; }
        public string strDesProducto { get; set; }
        public int OrdenItemImprimir { get; set; }

        public string prdc_vpart_number { get; set; }
        public string strDesUM { get; set; }
        public string strAbreUM { get; set; }
        public string strCategoria { get; set; }
        public string strSubCategoriaUno { get; set; }
        public int intTipoOperacion { get; set; }
        public decimal dblStockDisponible { get; set; }
        public string dremc_vobservaciones { get; set; }

        public Boolean dremc_PastBibli { get; set; }
        public decimal dremc_nDescuento { get; set; }
        public decimal dremc_nprecio_lista { get; set; }
        public decimal dremc_nPrecio_Unitario { get; set; }

        public int? kardc_icod_correlativo_ingreso { get; set; }

        public int gtablc_iid_tipo_venta { get; set; }
        public string StrTipoVenta { get; set; }

        public string dremc_icodigo { get; set; }
        public int unidc_icod_unidad_medida { get; set; }
        public string CodigoSUNAT { get; set; }

        public string TallaAcumulada { get; set; }

        public EGuiaRemisionElectronicaDet electronicaDet { get; set; }
        public int? dremc_itipo_producto { get; set; }

        public EGuiaRemisionMPDet()
        {
            electronicaDet = new EGuiaRemisionElectronicaDet();
        }
        public EGuiaRemisionElectronicaDet generarGREdet()
        {
            electronicaDet.remic_icod_remision = remic_icod_remision;
            electronicaDet.Correlativo = dremc_inro_item;
            electronicaDet.CodigoItem = strCodProducto;
            electronicaDet.Descripcion = strDesProducto;
            electronicaDet.UnidadMedida = CodigoSUNAT;
            electronicaDet.Cantidad = dremc_ncantidad_producto;
            electronicaDet.PesoItem = 0;
            electronicaDet.LineaReferencia = 0;
            electronicaDet.UM = strAbreUM;
            return electronicaDet;
        }
    }
}
