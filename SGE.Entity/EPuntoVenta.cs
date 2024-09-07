namespace SGE.Entity
{
    public class EPuntoVenta
    {

        public int puvec_icod_punto_venta { get; set; }

        public int puvec_vcod_punto_venta { get; set; }
        //[DataMember]
        public string puvec_vdescripcion { get; set; }
        //[DataMember]
        public int puvec_iactivo { get; set; }
        //[DataMember]
        public string puvec_iactivo_descrpcion { get; set; }
        //[DataMember]
        public bool indicador { get; set; }
        public string puvec_vserie_factura { get; set; }
        public int puvec_icorrelativo_factura { get; set; }
        public string puvec_vserie_factura_suministros { get; set; }
        public int puvec_icorrelativo_factura_suministros { get; set; }
        public string puvec_vserie_factura_alquileres { get; set; }
        public int puvec_icorrelativo_factura_alquileres { get; set; }
        public string puvec_vserie_boleta { get; set; }
        public int puvec_icorrelativo_boleta { get; set; }
        public string puvec_vserie_notaF_credito { get; set; }
        public string puvec_vserie_notaB_credito { get; set; }
        public int puvec_icorrelativo_nota_credito { get; set; }
        public string puvec_vserie_notaF_debito { get; set; }
        public string puvec_vserie_notaB_debito { get; set; }
        public int puvec_icorrelativo_nota_debito { get; set; }
        public string puvec_vcodigo_sunat { get; set; }
        public string puvec_vdireccion { get; set; }
        public string puvec_vusuario_ose { get; set; }
        public string strSituacion { get; set; }
        public int intUsuario { get; set; }
        public string strPc { get; set; }
    }

}
