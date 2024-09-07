using System;

namespace SGE.Entity
{
    public class EPedidoProvDet : EAuditoria
    {
        public int lpedid_icod_proveedor { get; set; }
        public int lpedi_icod_proveedor { get; set; }
        public int prdc_icod_producto { get; set; }
        public int lpedid_icod_moneda { get; set; }
        public string lpedid_vDesc_moneda { get; set; }
        public decimal lpedid_nprecio_lista { get; set; }
        public decimal lpedid_nperso_desc { get; set; }
        public decimal lpedid_nprecio_neto { get; set; }
        public decimal lpedid_nstock_producto { get; set; }
        public decimal lpedid_ncompras_sem1 { get; set; }
        public decimal lpedid_ncompras_sem2 { get; set; }
        public decimal lpedid_ncompras_sem3 { get; set; }
        public decimal lpedid_ncompras_sem4 { get; set; }
        public int lpedid_nCant_pedido { get; set; }
        public decimal lpedid_nCosto_pedido { get; set; }
        public Boolean lpedid_sflag_estado { get; set; }

        public string prdc_vdescripcion_larga { get; set; }
        public string prdc_vcode_producto { get; set; }
        public string prdc_vAutor { get; set; }
        public string strEditorial { get; set; }
        public int intTipoOperacion { get; set; }


        public string strCategoria { get; set; }
        public string strSubCategoriaUno { get; set; }
        public string strSubCategoriaDos { get; set; }
        public string StrUnidadMedida { get; set; }

        public int lpedid_item { get; set; }
    }
}
