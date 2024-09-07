using System;
using static SGE.Common.Enums;

namespace SGE.Entity
{
    public class ENotaIngresoOPDetalle : EAuditoria
    {
        public int niopd_icod_nota_ingreso_detalle { get; set; }
        public int niopd_iitem_nota_ingreso_detalle { get; set; }
        public int niop_icod_nota_ingreso { get; set; }
        public int orped_icod_item_orden_pedido { get; set; }
        public int? niopd_cant_1 { get; set; }
        public int? niopd_cant_2 { get; set; }
        public int? niopd_cant_3 { get; set; }
        public int? niopd_cant_4 { get; set; }
        public int? niopd_cant_5 { get; set; }
        public int? niopd_cant_6 { get; set; }  
        public int? niopd_cant_7 { get; set; }
        public int? niopd_cant_8 { get; set; }
        public int? niopd_cant_9 { get; set; }
        public int? niopd_cant_10 { get; set; }
        public int? niopd_iprod_1 { get; set; }
        public int? niopd_iprod_2 { get; set; }
        public int? niopd_iprod_3 { get; set; }
        public int? niopd_iprod_4 { get; set; }
        public int? niopd_iprod_5 { get; set; }
        public int? niopd_iprod_6 { get; set; }
        public int? niopd_iprod_7 { get; set; }
        public int? niopd_iprod_8 { get; set; }
        public int? niopd_iprod_9 { get; set; }
        public int? niopd_iprod_10 { get; set; }
        public int? niopd_kardex_1 { get; set; }
        public int? niopd_kardex_2 { get; set; }
        public int? niopd_kardex_3 { get; set; }
        public int? niopd_kardex_4 { get; set; }
        public int? niopd_kardex_5 { get; set; }
        public int? niopd_kardex_6 { get; set; }
        public int? niopd_kardex_7 { get; set; }
        public int? niopd_kardex_8 { get; set; }
        public int? niopd_kardex_9 { get; set; }
        public int? niopd_kardex_10 { get; set; }
        public Operacion Operacion { get; set; }
        public string NumeroItemOP { get; set; }
        public int orped_imarca { get; set; }
        public int orped_imodelo { get; set; }
        public int orped_icolor { get; set; }
        public int Total { get {
                int sum = Convert.ToInt32(niopd_cant_1) +
                    Convert.ToInt32(niopd_cant_2) +
                    Convert.ToInt32(niopd_cant_3) +
                    Convert.ToInt32(niopd_cant_4) +
                    Convert.ToInt32(niopd_cant_5) +
                    Convert.ToInt32(niopd_cant_6) +
                    Convert.ToInt32(niopd_cant_7) +
                    Convert.ToInt32(niopd_cant_8) +
                    Convert.ToInt32(niopd_cant_9) +
                    Convert.ToInt32(niopd_cant_10); 
                return sum; 
            } }
    }
}
