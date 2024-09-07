using System;

namespace SGE.Entity
{
    public class EListaPrecioDetalle : EAuditoria
    {
        public int lpred_icod_proveedor { get; set; }
        public int lprec_icod_proveedor { get; set; }
        public int prdc_icod_producto { get; set; }
        public int lpred_icod_moneda { get; set; }
        public decimal lpred_nprecio_lista { get; set; }
        public decimal lpred_nperso_desc { get; set; }
        public decimal lpred_nprecio_neto { get; set; }
        public Boolean lpred_sflag_estado { get; set; }

        public string prdc_vcode_producto { get; set; }
        public string prdc_vdescripcion_larga { get; set; }
        public string tarec_vdescripcion { get; set; }
        public int intTipoOperacion { get; set; }

        public string prdc_vAutor { get; set; }
        public string strEditorial { get; set; }
        public string lpred_vdescripcion_moneda { get; set; }
        public string edit_vdescripcion { get; set; }

        public string DescripcionMoneda { get; set; }

        public decimal lpedid_ncompras_sem1 { get; set; }
        public decimal lpedid_nstock_producto { get; set; }
        public decimal lpedid_ncompras_sem2 { get; set; }
        public decimal lpedid_ncompras_sem3 { get; set; }
        public decimal lpedid_ncompras_sem4 { get; set; }

        public Boolean lpedid_bExisteCatalogo { get; set; }

        public string prov_vcodigo_prov { get; set; }
    }
}
