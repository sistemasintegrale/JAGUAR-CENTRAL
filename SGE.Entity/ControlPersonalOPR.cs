using System;

namespace SGE.Entity
{
    public class ControlPersonalOPR
    {
        public int IdOrdenProduccion { get; set; }
        public string NumeroOrdenProduccion { get; set; }
        public DateTime? FechaOrdenProduccion { get; set; }
        public string NombreCliente { get; set; }
        public string Tipo { get; set; }
        public string Serie { get; set; }
        public string Pedido { get; set; }
        public int? PedidoItem { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public decimal? Docenas { get; set; }
        public int? PorCortar { get; set; }
        public string Situacion { get; set; }
        public string PedidoPedidoItem { get { return $"{Pedido}-{PedidoItem.Value.ToString("d2")}"; } }
    }
}
