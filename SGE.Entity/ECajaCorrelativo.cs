using System;

namespace SGE.Entity
{
    public class ECajaCorrelativo : EAuditoria
    {
        public int cc_icod_caja_correlativo { get; set; }
        public int bcoc_icod_banco { get; set; }
        public int bcod_icod_banco_cuenta { get; set; }
        public int tdocc_icod_tipo_doc { get; set; }
        public int cc_inumero_correlativo { get; set; }
        public string DesBanco { get; set; }
        public String DesBancoCuenta { get; set; }
        public string TipoDoc { get; set; }
        public int cc_icod_tipo { get; set; }
        public string strTipo { get; set; }
    }
}
