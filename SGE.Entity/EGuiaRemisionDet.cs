using System;

namespace SGE.Entity
{
    public class EGuiaRemisionDet : EAuditoria
    {
        public int dremc_icod_detalle_remision { get; set; }
        public int remic_icod_remision { get; set; }
        public Int16 dremc_inro_item { get; set; }
        public int prdc_icod_producto { get; set; }
        public string dremc_vcodigo_extremo_prod { get; set; }
        public string dremc_vdescripcion_prod { get; set; }
        public decimal dremc_ncantidad_producto { get; set; }
        public decimal dremc_nMonto_Total { get; set; }
        public string dremc_vcantidad_producto { get; set; }
        public int? kardc_icod_correlativo { get; set; }

        public int dremc_imodelo { get; set; }
        public int dremc_icolor { get; set; }
        public int dremc_imarca { get; set; }
        public int dremc_itipo { get; set; }
        public int dremc_iserie { get; set; }
        public string dremc_rango_talla { get; set; }
        public int? dremc_talla1 { get; set; }
        public int? dremc_talla2 { get; set; }
        public int? dremc_talla3 { get; set; }
        public int? dremc_talla4 { get; set; }
        public int? dremc_talla5 { get; set; }
        public int? dremc_talla6 { get; set; }
        public int? dremc_talla7 { get; set; }
        public int? dremc_talla8 { get; set; }
        public int? dremc_talla9 { get; set; }
        public int? dremc_talla10 { get; set; }
        public int? dremc_cant_talla1 { get; set; }
        public int? dremc_cant_talla2 { get; set; }
        public int? dremc_cant_talla3 { get; set; }
        public int? dremc_cant_talla4 { get; set; }
        public int? dremc_cant_talla5 { get; set; }
        public int? dremc_cant_talla6 { get; set; }
        public int? dremc_cant_talla7 { get; set; }
        public int? dremc_cant_talla8 { get; set; }
        public int? dremc_cant_talla9 { get; set; }
        public int? dremc_cant_talla10 { get; set; }
        public long? dremc_iid_kardex1 { get; set; }
        public long? dremc_iid_kardex2 { get; set; }
        public long? dremc_iid_kardex3 { get; set; }
        public long? dremc_iid_kardex4 { get; set; }
        public long? dremc_iid_kardex5 { get; set; }
        public long? dremc_iid_kardex6 { get; set; }
        public long? dremc_iid_kardex7 { get; set; }
        public long? dremc_iid_kardex8 { get; set; }
        public long? dremc_iid_kardex9 { get; set; }
        public long? dremc_iid_kardex10 { get; set; }
        public int? dremc_icod_producto1 { get; set; }
        public int? dremc_icod_producto2 { get; set; }
        public int? dremc_icod_producto3 { get; set; }
        public int? dremc_icod_producto4 { get; set; }
        public int? dremc_icod_producto5 { get; set; }
        public int? dremc_icod_producto6 { get; set; }
        public int? dremc_icod_producto7 { get; set; }
        public int? dremc_icod_producto8 { get; set; }
        public int? dremc_icod_producto9 { get; set; }
        public int? dremc_icod_producto10 { get; set; }
        public string pr_viid_marca { get; set; }
        public string pr_viid_tipo { get; set; }
        public string pr_viid_color { get; set; }

        public int? tablc_iid_sit_item_guia_remision { get; set; }

        public string strCodProducto { get; set; }
        public string strDesProducto { get; set; }
        public int OrdenItemImprimir { get; set; }

        public string prdc_vpart_number { get; set; }
        public int unidc_icod_unidad_medida { get; set; }
        public String CodigoSUNAT { get; set; }
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

        public string resec_vdescripcion { get; set; }
        public string resec_vtalla_inicial { get; set; }
        public string resec_vtalla_final { get; set; }


        public string TallaAcumulada { get; set; }

        public EGuiaRemisionElectronicaDet electronicaDet { get; set; }
        public int? dremc_itipo_producto { get; set; }

        public EGuiaRemisionDet()
        {
            electronicaDet = new EGuiaRemisionElectronicaDet();

        }

        public EGuiaRemisionElectronicaDet generarGREdet()
        {
            electronicaDet.remic_icod_remision = remic_icod_remision;
            electronicaDet.Correlativo = dremc_inro_item;
            electronicaDet.CodigoItem = dremc_vcodigo_extremo_prod;
            electronicaDet.Descripcion = dremc_vdescripcion_prod;
            electronicaDet.UnidadMedida = CodigoSUNAT;
            electronicaDet.Cantidad = dremc_ncantidad_producto;
            electronicaDet.PesoItem = 0;
            electronicaDet.LineaReferencia = 0;
            electronicaDet.UM = strAbreUM;
            return electronicaDet;
        }
    }
}
