using System;
using System.Drawing;

namespace SGE.Entity
{
    public class EOrdenPedidoDet : EAuditoria, IDisposable
    {
        public int orped_icod_item_orden_pedido { get; set; }
        public int orpec_iid_orden_pedido { get; set; }
        public int orped_iitem_orden_pedido { get; set; }
        public int orped_iresponsable { get; set; }
        public int orped_ificha_tecnica { get; set; }
        public int orped_imodelo { get; set; }
        public int orped_icolor { get; set; }
        public int orped_imarca { get; set; }
        public int orped_itipo { get; set; }
        public int orped_serie { get; set; }
        public int orped_talla1 { get; set; }
        public int orped_talla2 { get; set; }
        public int orped_talla3 { get; set; }
        public int orped_talla4 { get; set; }
        public int orped_talla5 { get; set; }
        public int orped_talla6 { get; set; }
        public int orped_talla7 { get; set; }
        public int orped_talla8 { get; set; }
        public int orped_talla9 { get; set; }
        public int orped_talla10 { get; set; }
        public int orped_cant_talla1 { get; set; }
        public int orped_cant_talla2 { get; set; }
        public int orped_cant_talla3 { get; set; }
        public int orped_cant_talla4 { get; set; }
        public int orped_cant_talla5 { get; set; }
        public int orped_cant_talla6 { get; set; }
        public int orped_cant_talla7 { get; set; }
        public int orped_cant_talla8 { get; set; }
        public int orped_cant_talla9 { get; set; }
        public int orped_cant_talla10 { get; set; }
        public string orped_vruta { get; set; }
        public int orped_itotal_item { get; set; }
        public decimal orped_ndocenas { get; set; }
        public int intusuario { get; set; }
        public string vpc { get; set; }
        public bool orped_flag_estado { get; set; }
        public string pr_viid_marca { get; set; }
        public string pr_viid_tipo { get; set; }
        public string pr_viid_color { get; set; }
        public string NomVendedor { get; set; }
        public string strfichatecnica { get; set; }
        public string strmodelo { get; set; }
        public string resec_vdescripcion { get; set; }
        public int intTipoOperacion { get; set; }
        public int orped_isituacion { get; set; }
        public string strsituacion { get; set; }
        public int fitec_ilinea { get; set; }
        public string strlinea { get; set; }
        public string Codigo { get; set; }
        public string FichaTecnica { get; set; }
        public string Serie { get; set; }

        public Image imagen2 { get; set; }
        public string orped_vcodigo_cliente { get; set; }
        public string orped_vcolor_cliente { get; set; }
        public decimal orped_dprecio_cliente { get; set; }
        public decimal? orped_dprecio_total { get; set; }
        public string orped_vhorma { get; set; }
        public bool orped_btercerizado { get; set; }
        public string orped_vnum_ficha_tecnica { get; set; }
        public string ConOPR { get; set; }
        public int orped_isituacion_entrega { get; set; }
        public string SituacionEntrega { get; set; }

        public void Dispose()
        {
            imagen2 = null;
            GC.SuppressFinalize(this);
        }
    }
}
