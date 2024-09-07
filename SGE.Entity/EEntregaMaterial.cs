using System;

namespace SGE.Entity
{
    public class EEntregaMaterial : EAuditoria
    {
        public int icod_entrega_material { get; set; }
        public int rqmad_icod_item_requerimiento_material { get; set; }
        public DateTime em_sfecha_entrega { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal em_ncantidad { get; set; }
        public int perc_icod_personal { get; set; }


        //Datos Extra
        public string CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int IdUnidadMedida { get; set; }
        public string NombrePersonal { get; set; }
        public int IdKardex { get; set; }
    }
}
