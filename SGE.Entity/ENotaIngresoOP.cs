using System;

namespace SGE.Entity
{
    public class ENotaIngresoOP : EAuditoria
    {
        public int niop_icod_nota_ingreso { get; set; }
        public int niop_inumero { get; set; }
        public int niop_ialmacen { get; set; }
        public DateTime niop_sfecha { get; set; }
        public string niop_vobservacion { get; set; }
        public string niop_vreferencia { get; set; }
        public int orpec_iid_orden_pedido { get; set; }
        public string NumeroAlmacen { get; set; }
        public string NombreAlmacen { get; set; }
        public string NumeroPedido { get; set; }
    }
}
