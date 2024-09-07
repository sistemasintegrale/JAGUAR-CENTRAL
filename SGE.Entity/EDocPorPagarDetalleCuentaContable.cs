using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EDocPorPagarDetalleCuentaContable : EAuditoria
    {
        [DataMember]
        public long cdxpc_icod_correlativo { get; set; }
        [DataMember]
        public long doxpc_icod_correlativo { get; set; }
        [DataMember]
        public long ctacc_iid_cuenta_contable { get; set; }
        [DataMember]
        public string ctacc_vnumero_cuenta_contable { get; set; }
        [DataMember]
        public int? cecoc_icod_centro_costo { get; set; } //correlativo del centro de costo
        [DataMember]
        public int? anac_icod_analitica { get; set; } //correlativo de la analítica
        [DataMember]
        public decimal cdxpc_nmonto_cuenta { get; set; }
        [DataMember]
        public string cdxpc_vglosa { get; set; }
        [DataMember]
        public string IdTipoAnalitica { get; set; }
        [DataMember]
        public string TipoAnalitica { get; set; }
        [DataMember]
        public string NumeroAnalitica { get; set; }
        [DataMember]
        public string NumeroCuentaContable { get; set; }
        [DataMember]
        public string DescripcionCuentaContable { get; set; }
        [DataMember]
        public string CodigoCentroCosto { get; set; }
        [DataMember]
        public string DescripcionCentroCosto { get; set; }
        [DataMember]
        public int TipOper { get; set; }
        [DataMember]
        public bool cdxpc_flag_estado { get; set; }
        [DataMember]
        public int cdxpc_isituacion { get; set; }
        [DataMember]
        public long? pdxpc_icod_correlativo { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public string doxpc_vnumero_doc { get; set; }
        [DataMember]
        public long? doxpc_icod_correlativo_dxp { get; set; } //para el pago si fuera tipo doc LCO
        [DataMember]
        public decimal? doxpc_nmonto_total_saldo { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_pagado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_documento { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public decimal pdxpc_nmonto_pago_dxp { get; set; }

        [DataMember]
        public int? prep_icod_presupuesto { get; set; }
        [DataMember]
        public int? prepd_icod_detalle { get; set; }
        [DataMember]
        public string prep_cod_presupuesto { get; set; }
        [DataMember]
        public string cpnd_vdescripcion { get; set; }
    }
}
