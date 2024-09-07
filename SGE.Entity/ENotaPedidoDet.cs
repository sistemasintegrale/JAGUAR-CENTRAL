using System.Drawing;

namespace SGE.Entity
{
    public class ENotaPedidoDet : EAuditoria
    {
        public int noped_icod_item_nota_pedido { get; set; }
        public int nopec_iid_nota_pedido { get; set; }
        public int noped_iitem_nota_pedido { get; set; }
        public int noped_iresponsable { get; set; }
        public int noped_ificha_tecnica { get; set; }
        public int noped_imodelo { get; set; }
        public int noped_icolor { get; set; }
        public int noped_imarca { get; set; }
        public int noped_itipo { get; set; }
        public int noped_serie { get; set; }
        public int noped_talla1 { get; set; }
        public int noped_talla2 { get; set; }
        public int noped_talla3 { get; set; }
        public int noped_talla4 { get; set; }
        public int noped_talla5 { get; set; }
        public int noped_talla6 { get; set; }
        public int noped_talla7 { get; set; }
        public int noped_talla8 { get; set; }
        public int noped_talla9 { get; set; }
        public int noped_talla10 { get; set; }
        public int noped_cant_talla1 { get; set; }
        public int noped_cant_talla2 { get; set; }
        public int noped_cant_talla3 { get; set; }
        public int noped_cant_talla4 { get; set; }
        public int noped_cant_talla5 { get; set; }
        public int noped_cant_talla6 { get; set; }
        public int noped_cant_talla7 { get; set; }
        public int noped_cant_talla8 { get; set; }
        public int noped_cant_talla9 { get; set; }
        public int noped_cant_talla10 { get; set; }
        public string noped_vruta { get; set; }
        public string noped_vruta_nombre { get; set; }
        public int noped_itotal_item { get; set; }
        public decimal noped_ndocenas { get; set; }
        public int intusuario { get; set; }
        public string vpc { get; set; }
        public bool noped_flag_estado { get; set; }
        public string pr_viid_marca { get; set; }
        public string pr_viid_tipo { get; set; }
        public string pr_viid_color { get; set; }
        public string NomVendedor { get; set; }
        public string strfichatecnica { get; set; }
        public string strmodelo { get; set; }
        public string resec_vdescripcion { get; set; }
        public int intTipoOperacion { get; set; }
        public int noped_isituacion { get; set; }
        public string strsituacion { get; set; }
        public int fitec_ilinea { get; set; }
        public string strlinea { get; set; }
        public string Codigo { get; set; }
        public string FichaTecnica { get; set; }
        public string Serie { get; set; }
        public string imagen { get; set; }
        public Image imagen2 { get; set; }

        public string noped_vcodigo_cliente { get; set; }
        public string noped_vcolor_cliente { get; set; }
        public decimal noped_nprecio_fabrica { get; set; }
        public decimal noped_nprecio_cliente { get; set; }
        public string strfichatecnica_Contramuestra { get; set; }
        public string fitec_icorrelativo_contramuestra { get; set; }
    }
}
