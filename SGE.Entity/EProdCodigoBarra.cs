using System;

namespace SGE.Entity
{
    public class EProdCodigoBarra
    {
        public int picbc_icod_plan_cod_barr { get; set; }
        public int picbc_inumero_plant { get; set; }
        public DateTime picbc_sfecha_plant { get; set; }
        public string picbc_vdescrip { get; set; }
        public int picbc_iicod_marca { get; set; }
        public int picbc_iicod_modelo { get; set; }
        public string DescripModelo { get; set; }
        public int picbc_iactivo { get; set; }
        public int iusuario { get; set; }
        public int tarec_icorrelativo_modelo { get; set; }
    }
}
