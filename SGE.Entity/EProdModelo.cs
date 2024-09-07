using System.Data.Linq;
using System.Drawing;

namespace SGE.Entity
{
    public class EProdModelo : EAuditoria
    {
        public Image image;

        public int mo_icod_modelo { get; set; }
        public int? mo_iid_modelo { get; set; }
        public string mo_viid_modelo { get; set; }

        public int? pr_iid_tipo { get; set; }
        public string pr_vid_tipo { get; set; }
        public string pr_iid_tipo_descripcion { get; set; }
        public int? pr_iid_categoria { get; set; }
        public string pr_vid_categoria { get; set; }
        public string pr_iid_categoria_descripcion { get; set; }
        public int? pr_iid_linea { get; set; }
        public string pr_vid_linea { get; set; }
        public string pr_iid_linea_descripcion { get; set; }
        public int? pr_iid_capellada { get; set; }
        public string pr_vid_capellada { get; set; }
        public string pr_iid_capellada_descripcion { get; set; }
        public int? pr_iid_planta { get; set; }
        public string pr_vid_planta { get; set; }
        public string pr_iid_planta_descripcion { get; set; }
        public int? pr_iid_forro { get; set; }
        public string pr_vid_forro { get; set; }
        public string pr_iid_forro_descripcion { get; set; }
        public int? pr_iid_tipo_marca { get; set; }
        public string pr_vid_tipo_marca { get; set; }
        public string pr_iid_tipo_marca_descripcion { get; set; }
        public int? pr_iid_taco { get; set; }
        public string pr_vid_taco { get; set; }
        public string pr_iid_taco_descripcion { get; set; }
        public int? pr_iid_horma { get; set; }
        public string pr_vid_horma { get; set; }
        public string pr_iid_horma_descripcion { get; set; }

        public string mo_vdescripcion { get; set; }
        public int? tarec_icorrelativo { get; set; }
        public int? mo_estado { get; set; }
        public string mo_ruta { get; set; }
        public Binary mo_imagen { get; set; }
        public int tarec_iid_tabla_registro { get; set; }
        public string tarec_vdescripcion { get; set; }
        public string mo_cestado { get; set; }
        public int? mo_iserie { get; set; }
        public string estado { get { return mo_cestado == "A" ? "Activo" : "Inactivo"; } }
    }
}
