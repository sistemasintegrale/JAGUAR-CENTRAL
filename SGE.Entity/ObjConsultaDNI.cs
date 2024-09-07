namespace SGE.Entity
{
    public class ObjConsultaDNI
    {
        public string token { get; set; }
        public string dni { get; set; }
    }

    public class objresultDNI
    {
        public string success { get; set; }
        public string dni { get; set; }
        public string nombre_completo { get; set; }

    }

    public class ObjConsulta
    {
        public string token { get; set; }
        public string ruc { get; set; }
    }

    public class Objesultado
    {
        public string success { get; set; }
        public string ruc { get; set; }
        public string nombre_o_razon_social { get; set; }
        public string estado_del_contribuyente { get; set; }
        public string condicion_de_domicilio { get; set; }
        public string ubigeo { get; set; }
        public string tipo_de_via { get; set; }
        public string nombre_de_via { get; set; }
        public string codigo_de_zona { get; set; }
        public string tipo_de_zona { get; set; }
        public string numero { get; set; }
        public string interior { get; set; }
        public string lote { get; set; }
        public string dpto { get; set; }
        public string manzana { get; set; }
        public string kilometro { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
        public string direccion { get; set; }
        public string direccion_completa { get; set; }
        public string ultima_actualizacion { get; set; }
    }
}
