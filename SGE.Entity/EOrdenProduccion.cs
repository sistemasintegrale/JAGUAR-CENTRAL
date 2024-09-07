using System;

namespace SGE.Entity
{
    public class EOrdenProduccion : EAuditoria
    {
        public int orprc_iid_orden_produccion { get; set; }
        public string orprc_icod_orden_produccion { get; set; }
        public DateTime orprc_sfecha_orden_produccion { get; set; }
        public int orprc_ipedido { get; set; }
        public string orprc_vpedido { get; set; }
        public int orprc_iitem_pedido { get; set; }
        public string orprc_vitem_pedido { get; set; }
        public int orprc_ificha_tecnica { get; set; }
        public string orprc_vficha_tecnica { get; set; }
        public string orprc_vresponsable { get; set; }
        public int orprc_imodelo { get; set; }
        public string DesModelo { get; set; }
        public int orprc_icolor { get; set; }
        public int orprc_imarca { get; set; }
        public string DesMarca { get; set; }
        public int orprc_itipo { get; set; }
        public int orprc_iserie { get; set; }
        public int orprc_ilinea { get; set; }
        public string orprc_vlinea { get; set; }
        public int orprc_talla1 { get; set; }
        public int orprc_talla2 { get; set; }
        public int orprc_talla3 { get; set; }
        public int orprc_talla4 { get; set; }
        public int orprc_talla5 { get; set; }
        public int orprc_talla6 { get; set; }
        public int orprc_talla7 { get; set; }
        public int orprc_talla8 { get; set; }
        public int orprc_talla9 { get; set; }
        public int orprc_talla10 { get; set; }
        public int orprc_cant_talla1 { get; set; }
        public int orprc_cant_talla2 { get; set; }
        public int orprc_cant_talla3 { get; set; }
        public int orprc_cant_talla4 { get; set; }
        public int orprc_cant_talla5 { get; set; }
        public int orprc_cant_talla6 { get; set; }
        public int orprc_cant_talla7 { get; set; }
        public int orprc_cant_talla8 { get; set; }
        public int orprc_cant_talla9 { get; set; }
        public int orprc_cant_talla10 { get; set; }
        public long? orprc_iid_kardex1 { get; set; }
        public long? orprc_iid_kardex2 { get; set; }
        public long? orprc_iid_kardex3 { get; set; }
        public long? orprc_iid_kardex4 { get; set; }
        public long? orprc_iid_kardex5 { get; set; }
        public long? orprc_iid_kardex6 { get; set; }
        public long? orprc_iid_kardex7 { get; set; }
        public long? orprc_iid_kardex8 { get; set; }
        public long? orprc_iid_kardex9 { get; set; }
        public long? orprc_iid_kardex10 { get; set; }
        public int? orprc_icod_producto1 { get; set; }
        public int? orprc_icod_producto2 { get; set; }
        public int? orprc_icod_producto3 { get; set; }
        public int? orprc_icod_producto4 { get; set; }
        public int? orprc_icod_producto5 { get; set; }
        public int? orprc_icod_producto6 { get; set; }
        public int? orprc_icod_producto7 { get; set; }
        public int? orprc_icod_producto8 { get; set; }
        public int? orprc_icod_producto9 { get; set; }
        public int? orprc_icod_producto10 { get; set; }
        public int orprc_iid_almacen { get; set; }
        public DateTime? orprc_sfecha_ing_kardex { get; set; }
        public int orprc_iid_situacion { get; set; }
        public int orprc_ntotal { get; set; }
        public decimal orprc_ndocenas { get; set; }
        public string orprc_vcodigo_producto { get; set; }
        public string orprc_vdescripcion_producto { get; set; }

        public string vpc { get; set; }
        public bool orprc_flag_estado { get; set; }
        public int orprc_tipo { get; set; }
        public int orprc_isituacion { get; set; }
        public int orprc_iresponsable { get; set; }
        public string strsituacion { get; set; }
        public string strtipo { get; set; }
        public string strcolor { get; set; }
        public string resec_vdescripcion { get; set; }
        public string strpedido { get; set; }
        public string stritempedido { get; set; }
        public string strfichatecnica { get; set; }
        public int orprc_cant_aprocesar1 { get; set; }
        public int orprc_cant_aprocesar2 { get; set; }
        public int orprc_cant_aprocesar3 { get; set; }
        public int orprc_cant_aprocesar4 { get; set; }
        public int orprc_cant_aprocesar5 { get; set; }
        public int orprc_cant_aprocesar6 { get; set; }
        public int orprc_cant_aprocesar7 { get; set; }
        public int orprc_cant_aprocesar8 { get; set; }
        public int orprc_cant_aprocesar9 { get; set; }
        public int orprc_cant_aprocesar10 { get; set; }
        public string pedido { get; set; }
        public bool imprimir { get; set; } = false;
        public bool eliminar { get; set; } = false;

        public string strOrdenPedido
        {
            get
            {
                if (int.TryParse(orprc_vitem_pedido, out int itemPedido))
                {
                    return orprc_vpedido + "-" + itemPedido.ToString("D4");
                }
                else
                {
                    // Manejo de error en caso de que orprc_vitem_pedido no sea un número válido.
                    return "Valor no válido";
                }
            }
        }
    }
}
