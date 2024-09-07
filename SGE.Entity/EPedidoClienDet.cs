using System;

namespace SGE.Entity
{
    public class EPedidoClienDet : EAuditoria
    {
        public int lpedid_icod_cliente { get; set; }
        public int lpedi_icod_cliente { get; set; }
        public int lpedid_iitem { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal lpedid_nstock_producto { get; set; }
        public int lpedid_nCant_pedido { get; set; }
        public int lpedid_icod_moneda { get; set; }
        public string lpedid_vDesc_moneda { get; set; }
        public decimal lpedid_nprecio_uni { get; set; }
        public decimal lpedid_nTotal_precio { get; set; }
        public Boolean lpedid_sflag_estado { get; set; }
        public string prdc_vdescripcion_larga { get; set; }
        public string prdc_vcode_producto { get; set; }
        public string prdc_vAutor { get; set; }
        public string strEditorial { get; set; }
        public int intTipoOperacion { get; set; }

        public string strCategoria { get; set; }
        public string strSubCategoriaUno { get; set; }
        public string StrUnidadMedida { get; set; }
    }
}
