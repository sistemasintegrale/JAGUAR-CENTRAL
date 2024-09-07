using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EAnioEjercicio
    {
        public int anioc_icod_anio_ejercicio { get; set; }
        public int anioc_iid_anio_ejercicio { get; set; }
        public bool anioc_iactivo { get; set; }
        public bool anioc_flag_estado { get; set; }
        /*-------------------------------------------------*/
        public string strEstado { get; set; }
    }
}
