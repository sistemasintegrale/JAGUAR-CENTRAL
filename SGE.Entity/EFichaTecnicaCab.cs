using System;
using System.Drawing;

namespace SGE.Entity
{
    public class EFichaTecnicaCab : EAuditoria
    {
        public int? fitec_iid_ficha_tecnica_ref { get; set; }
        public string strFicharef { get; set; }

        public int fitec_iid_ficha_tecnica { get; set; }
        public string fitec_icod_ficha_tecnica { get; set; }
        public int fitec_icorrelativo_contramuestra { get; set; }
        public DateTime fitec_sfecha_ficha_tecnica { get; set; }
        public int fitec_imarca { get; set; }
        public int fitec_imodelo { get; set; }
        public int fitec_itipo { get; set; }
        public int fitec_icolor { get; set; }
        public int fitec_ilinea { get; set; }
        public int fitec_itipo_trabajo { get; set; }
        public int fitec_iserie { get; set; }
        public string fitec_vruta { get; set; }
        public string fitec_vobservaciones { get; set; }
        public int intusuario { get; set; }
        public string vpc { get; set; }
        public string strmarca { get; set; }
        public string strmodelo { get; set; }
        public string strtipo { get; set; }
        public string strcolor { get; set; }
        public string strlinea { get; set; }
        public string strtipo_trabajo { get; set; }
        public string strserie { get; set; }
        public bool fitec_flag_estado { get; set; }
        public string strFichaTec_ContraMuestra { get; set; }

        public int? fitec_icod_orden_pedido { get; set; }
        public string StrOrdenPedido { get; set; }
        public int? fitec_icod_orden_pedido_det { get; set; }
        public string StrOrdenPedidoDet { get; set; }
        public Image imagen { get; set; }
    }
}
