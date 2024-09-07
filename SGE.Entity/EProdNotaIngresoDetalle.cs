using System;

namespace SGE.Entity
{
    public class EProdNotaIngresoDetalle : EAuditoria
    {
        public string Documento;

        public int ningcd_icod_nota_ingreso { get; set; }
        public int ningc_icod_nota_ingreso { get; set; }
        public int? ningc_iid_almacen { get; set; }
        public int? ningcd_num_item { get; set; }
        public int? tablc_iid_motivo { get; set; }
        public int? pr_icod_producto { get; set; }
        public string pr_vcodigo_externo { get; set; }
        public string pr_vdescripcion_producto { get; set; }
        public decimal? pr_ncant_total_producto { get; set; }
        public string ningcd_rango_talla { get; set; }
        public int? ningcd_iid_moneda { get; set; }
        public decimal? ningcd_monto_unit { get; set; }
        public decimal? ningcd_monto_total { get; set; }
        public int? ningcd_talla1 { get; set; }
        public int? ningcd_talla2 { get; set; }
        public int? ningcd_talla3 { get; set; }
        public int? ningcd_talla4 { get; set; }
        public int? ningcd_talla5 { get; set; }
        public int? ningcd_talla6 { get; set; }
        public int? ningcd_talla7 { get; set; }
        public int? ningcd_talla8 { get; set; }
        public int? ningcd_talla9 { get; set; }
        public int? ningcd_talla10 { get; set; }
        public int? ningcd_cant_talla1 { get; set; }
        public int? ningcd_cant_talla2 { get; set; }
        public int? ningcd_cant_talla3 { get; set; }
        public int? ningcd_cant_talla4 { get; set; }
        public int? ningcd_cant_talla5 { get; set; }
        public int? ningcd_cant_talla6 { get; set; }
        public int? ningcd_cant_talla7 { get; set; }
        public int? ningcd_cant_talla8 { get; set; }
        public int? ningcd_cant_talla9 { get; set; }
        public int? ningcd_cant_talla10 { get; set; }
        public long? ningcd_iid_kardex1 { get; set; }
        public long? ningcd_iid_kardex2 { get; set; }
        public long? ningcd_iid_kardex3 { get; set; }
        public long? ningcd_iid_kardex4 { get; set; }
        public long? ningcd_iid_kardex5 { get; set; }
        public long? ningcd_iid_kardex6 { get; set; }
        public long? ningcd_iid_kardex7 { get; set; }
        public long? ningcd_iid_kardex8 { get; set; }
        public long? ningcd_iid_kardex9 { get; set; }
        public long? ningcd_iid_kardex10 { get; set; }

        public int? ningcd_iactivo { get; set; }
        public string TallaAcumulada { get; set; }
        public int operacion { get; set; }
        public int? ningc_inumero_nota_ingreso { get; set; }
        public string ningc_vobservaciones { get; set; }
        public string ningc_vreferencia { get; set; }
        public string ningc_inumero_doc { get; set; }
        public string ningc_vdescripcion_tipo_doc { get; set; }
        public string descripcionMotivo { get; set; }
        public DateTime? ningc_sfecha_nota_ingreso { get; set; }
        public string ningc_vnumero_nota_ingreso { get; set; }
    }
}
