using System;

namespace SGE.Entity
{
    public class EPercepcion : EAuditoria
    {
        public int percc_icod_percepcion { get; set; }
        public string percc_vnro_percepcion { get; set; }
        public DateTime percc_sfecha_percepcion { get; set; }
        public int proc_icod_proveedor { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal percc_nmonto_cobrado { get; set; }
        public decimal percc_nmonto_percibido { get; set; }
        public decimal percc_tipo_cambio { get; set; }
        public int tablc_iid_situacion { get; set; }
        /**/
        public string strProveedor { get; set; }
        public string strMoneda { get; set; }
        public string strSituacion { get; set; }
        public Int64 percc_icod_dxp { get; set; }

        public int? intSituacionDXP { get; set; }
        public string strSituacionDXP { get; set; }
    }
}
