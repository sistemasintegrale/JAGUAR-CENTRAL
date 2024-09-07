using System;

namespace SGE.Entity
{
    public class EProdAlmacen : EAuditoria
    {
        public int id_almacen { get; set; }

        public string idf_almacen { get; set; }//id con formato "00"

        public int puvec_icod_punto_venta { get; set; }

        public string descripcion_puntoVenta { get; set; }

        public string codigo_ubigeo { get; set; }

        public string descripcion { get; set; }

        public string direccion { get; set; }

        public string representante { get; set; }

        public int iactivo { get; set; }

        //para tranferncia de datos
        public int almac_icod_almacen { get; set; }
        public int almac_iid_almacen { get; set; }

        public string almac_vdescripcion { get; set; }
        public string almac_vdireccion { get; set; }
        public string almac_vrepresentante { get; set; }
        public int almac_iid_ubigeo { get; set; }
        public int? almac_iusuario_crea { get; set; }
        public DateTime? almac_sfecha_crea { get; set; }
        public int? almac_iusuario_modifica { get; set; }
        public DateTime? almac_sfecha_modifica { get; set; }
        public int almac_iactivo { get; set; }
    }
}
