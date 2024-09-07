using System;

namespace SGE.Entity
{
    public class EProdReporte
    {
        //[DataMember]
        public DateTime sfechaInicio { get; set; }
        //[DataMember]
        public DateTime sfechaFinal { get; set; }
        //[DataMember]
        public int icod_almacen { get; set; }
        //[DataMember]
        public int puvec_icod_punto_venta { get; set; }

        public int tipo_movimiento { get; set; }
        //[DataMember]
        public int id_motivop { get; set; }
        //[DataMember]
        public string CodigoExterno { get; set; }
    }
}
