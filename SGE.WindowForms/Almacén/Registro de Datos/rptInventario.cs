using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class rptInventario : DevExpress.XtraReports.UI.XtraReport
    {
        public rptInventario()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<EInventarioDet> lstDetalle, EInventarioCab Obe)
        {
            /*------------------------------------------------------*/
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "ALMACÉN";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;
            /*------------------------------------------------------*/
            lblTipo.Text = Obe.strTipoInventario;
            lblObservacion.Text = Obe.invc_vobservaciones;
            /*------------------------------------------------------*/
            this.DataSource = lstDetalle;

            GroupHeader2.GroupFields.AddRange(new GroupField[] { new GroupField("strFamilia") });
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("strSubFamilia") });
            /*------------------------------------------------------*/

            lblFamilia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "strFamilia", "")});

            /*------------------------------------------------------*/

            lblSubFamilia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "strSubFamilia", "")});
            /*------------------------------------------------------*/

            lblItem.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "invd_inro_item", "{0:000}")});

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "strCodProducto", "")});

            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "strDesProducto", "")});

            lblUnidadMedida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lstDetalle, "strUnidadMedida", "")});

            //lblCantidad.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", lstDetalle, "invd_icantidad", "{0:N4}")});

            this.ShowPreview();
        }

    }
}
