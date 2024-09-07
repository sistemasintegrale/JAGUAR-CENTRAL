using System;

namespace SGE.Entity
{
    public class ENotaPedidoCab : EAuditoria
    {
        public int nopec_iid_nota_pedido { get; set; }
        public string nopec_icod_nota_pedido { get; set; }
        public DateTime nopec_sfecha_pedido { get; set; }
        public DateTime nopec_sfecha_entrega_inicial { get; set; }
        public DateTime nopec_sfecha_entrega_final { get; set; }
        public int nopec_icod_cliente { get; set; }
        public string nopec_vnumero_OC { get; set; }
        public string nopec_vforma_pago { get; set; }
        public string nopec_vtransporte { get; set; }
        public string cliec_vnombre_cliente { get; set; }
        public int intusuario { get; set; }
        public string vpc { get; set; }
        public int pmprc_inumero_orden_pedido { get; set; }
        public int nopec_itotal_unidades { get; set; }
        public decimal nopec_ntotal_docenas { get; set; }
        public int nopec_isituacion { get; set; }
        public string strsituacion { get; set; }
        public int tranc_icod_transportista { get; set; }
        public string tranc_vnombre_transportista { get; set; }
    }
}
