using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EOrdenCompraCab : EOrdenCompraDet
    {
        [DataMember]
        public int occc_icod_orden_compra { get; set; }
        [DataMember]
        public int proc_icod_proveedor { get; set; }
        [DataMember]
        public int occc_ianio { get; set; }
        [DataMember]
        public string occc_vnumero_orden { get; set; }
        [DataMember]
        public int intcorrelativo { get; set; }
        [DataMember]
        public DateTime occc_sfecha_orden { get; set; }
        [DataMember]
        public string occc_vreferencia { get; set; }
        [DataMember]
        public string occc_vatencion { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public string occc_vlugar_entrega { get; set; }
        [DataMember]
        public string occc_vforma_pago { get; set; }
        [DataMember]
        public DateTime occc_sfecha_entrega { get; set; }
        [DataMember]
        public string occc_vobs_fecha_entrega { get; set; }
        [DataMember]
        public string occc_vvalidez_oferta { get; set; }
        [DataMember]
        public Boolean occc_bincluye_igv { get; set; }
        [DataMember]
        public decimal occc_nporcentaje_igv { get; set; }
        [DataMember]
        public decimal occc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public decimal occc_nmonto_total { get; set; }
        [DataMember]
        public decimal occc_nmonto_neto { get; set; }
        [DataMember]
        public decimal occc_nmonto_igv { get; set; }
        [DataMember]
        public int occc_isituacion { get; set; }
        [DataMember]
        public Boolean occc_flag_estado { get; set; }

        [DataMember]
        public string NumeroDocumento { get; set; }
        [DataMember]
        public string SitDescripcion { get; set; }
        [DataMember]
        public string proc_vcod_proveedor { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
        [DataMember]
        public string DesMoneda { get; set; }
        [DataMember]
        public string AbrevMoneda { get; set; }


        [DataMember]
        public string proc_vdireccion { get; set; }
        [DataMember]
        public string proc_vdni { get; set; }
        [DataMember]
        public string proc_vruc { get; set; }
        [DataMember]
        public string proc_vfax { get; set; }
        [DataMember]
        public string proc_vcorreo { get; set; }
        [DataMember]
        public string proc_vtelefono { get; set; }

    }
}
