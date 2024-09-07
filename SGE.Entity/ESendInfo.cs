using System;

namespace SGE.Entity
{
    public class ESendInfo
    {
        public int trans_icod_trans { get; set; }
        public string trans_descripcion { get; set; }
        public string trans_nombre_tabla { get; set; }
        public string trans_nombre_archivo { get; set; }
        public DateTime sfecha_crea { get; set; }
        public bool estado { get; set; }
    }
}
