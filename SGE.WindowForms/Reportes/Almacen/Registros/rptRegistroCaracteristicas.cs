using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;


namespace SGE.WindowForms.Reportes.Almacen.Registros
{
    public partial class rptRegistroCaracteristicas : DevExpress.XtraReports.UI.XtraReport
    {
        public rptRegistroCaracteristicas()
        {
            InitializeComponent();
        }
        public void cargar(List<ECategoria> Listing, string NOMBRE_REGISTRO)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa;
            xrlblTitulo1.Text = "LISTA DE " + NOMBRE_REGISTRO;
            xrlblTitulo2.Text = "";
            xrLabel1.Text = "REGISTRO";

            this.DataSource = Listing;


            lblcodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "tarec_viid_correlativo", "")});

            lbldescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "tarec_vdescripcion", "")});

            lblabreviatura.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Listing, "tarec_vvalor_texto", "")});
            this.ShowPreview();
        }
    }
}
