using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EParametroProduccion : EAuditoria
    {
        public int pmprc_icod_parametro_produccion { get; set; }
        public int pmprc_inumero_orden_pedido { get; set; }
        public int pmprc_inumero_ficha_tecnica { get; set; }
        public int pmprc_inumero_orden_produccion { get; set; }
        public int pmprc_inumero_nota_pedido { get; set; }
        public string pmprc_vruta { get; set; }
        public bool pmprc_flag_estado { get; set; }
        public int intusuario { get; set; }
        public string vpc { get; set; }

        public string pmprc_vruta_base { get; set; }
        public string pmprc_vficha_tecnica { get; set; }
        public string pmprc_vorden_pedido { get; set; }
        public string pmprc_vorden_produccion { get; set; }
        public string pmprc_vmodelo { get; set; }
    }
}
