using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ECajaChica
    {
        [DataMember]
        public int icod_caja_liquida { get; set; }
        [DataMember]
        public string vnro_caja_liquida { get; set; }
        [DataMember]
        public string vdescrip_caja_liquida { get; set; }
        [DataMember]
        public int iid_tipo_moneda { get; set; }
        [DataMember]
        public int iid_cuenta_contable { get; set; }
        [DataMember]
        public int? icod_analitica { get; set; }
        [DataMember]
        public string vnom_responsable { get; set; }
        [DataMember]
        public int iid_situacion_cuenta { get; set; }
        [DataMember]
        public int? iusuario_crea { get; set; }
        [DataMember]
        public DateTime? sfecha_crea { get; set; }
        [DataMember]
        public string vpc_crea { get; set; }
        [DataMember]
        public int? iusuario_modifica { get; set; }
        [DataMember]
        public DateTime? sfecha_modifica { get; set; }
        [DataMember]
        public string vpc_modifica { get; set; }
        [DataMember]
        public string viid_caja_chica { get; set; }
        [DataMember]
        public string Moneda { get; set; }
        [DataMember]
        public string Situacion { get; set; }
        [DataMember]
        public int id_correlative_caja_chica { get; set; }

        //****//
        [DataMember]
        public string vnumero_cuenta_contable { get; set; }
        [DataMember]
        public string vdescripcion_cuenta_contable { get; set; }
        [DataMember]
        public int? tblc_tipo_analitica { get; set; }
        [DataMember]
        public string anac_iid_analitica { get; set; }
        [DataMember]
        public string anac_vdescripcion { get; set; }
        [DataMember]
        public string analisis { get; set; }
        //****//
    }

}
