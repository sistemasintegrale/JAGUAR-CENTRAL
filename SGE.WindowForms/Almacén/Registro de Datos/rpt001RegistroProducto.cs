using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class rpt001RegistroProducto : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt001RegistroProducto()
        {
            InitializeComponent();
        }

        public void cargar(List<EProducto> listing)
        {
            this.DataSource = listing;

            lblcodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", listing, "prdc_vcode_producto", "{0:0000}")});

            lblDesLarga.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", listing, "prdc_vdescripcion_larga", "")});

            lblCategoria.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", listing, "strCategoria", "")});

            lblSubCategoria.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", listing, "strSubCategoriaUno", "")});

            lblSubCategoria2.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", listing, "strSubCategoriaDos", "")});

            lblUm.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", listing, "StrUnidadMedida", "")});

            lblEstado.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", listing, "strEstado", "")});



            this.ShowPreview();
        }

    }
}
