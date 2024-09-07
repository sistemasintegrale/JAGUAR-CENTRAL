using System;

namespace SGE.Entity
{
    public class EOrdenPedidoCab : EAuditoria
    {
        public int orpec_iid_orden_pedido { get; set; }
        public string orpec_icod_orden_pedido { get; set; }
        public DateTime orpec_sfecha_pedido { get; set; }
        public DateTime orpec_sfecha_entrega_inicial { get; set; }
        public DateTime orpec_sfecha_entrega_final { get; set; }
        public int orpec_icod_cliente { get; set; }
        public string orpec_vnumero_OC { get; set; }
        public string orpec_vforma_pago { get; set; }
        public string orpec_vtransporte { get; set; }
        public string cliec_vnombre_cliente { get; set; }
        public int intusuario { get; set; }
        public string vpc { get; set; }
        public int pmprc_inumero_orden_pedido { get; set; }
        public int orpec_itotal_unidades { get; set; }
        public decimal orpec_ntotal_docenas { get; set; }
        public int orpec_isituacion { get; set; }
        public string strsituacion { get; set; }
        public int tranc_icod_transportista { get; set; }
        public string tranc_vnombre_transportista { get; set; }
    }
}
