using System;

namespace SGE.Entity
{
    public class EPedidoProvCab : EAuditoria
    {
        public int lpedi_icod_proveedor { get; set; }
        public int proc_icod_proveedor { get; set; }
        public string lpedi_Numerolista { get; set; }
        public int lprec_icod_proveedor { get; set; }
        public DateTime lpedi_sfecha_proveedor { get; set; }
        public string lpedi_Observaciones { get; set; }
        public Boolean lpedi_sflag_estado { get; set; }
        public int lpedi_isituacion_prov { get; set; }
        public string StrSituacion { get; set; }

        public string proc_vnombrecompleto { get; set; }
        public string proc_vcod_proveedor { get; set; }
        public string lprec_Numerolista { get; set; }


        public string proc_vruc { get; set; }
        public string proc_vdireccion { get; set; }
        public string proc_vtelefono { get; set; }
        public string proc_vcorreo { get; set; }
        public string proc_vdni { get; set; }

        public int CantidadPedidoTotal { get; set; }
    }
}
