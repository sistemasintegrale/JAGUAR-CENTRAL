using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;


namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class rptDistrito : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDistrito()
        {
            InitializeComponent();
        }
        public void cargar(List<EUbicacion> lista, string año, string ubicacion)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + año;
            xrlblTitulo1.Text = "RELACION DE " + ubicacion.ToUpper();
            this.DataSource = lista;

            //detalle
            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ubicc_ccod_ubicacion", "")});

            lblDescripcionn.DataBindings.AddRange(new XRBinding[] {
                new XRBinding("Text", lista, "ubicc_vnombre_ubicacion", "") });

            lblAbreviatura.DataBindings.AddRange(new XRBinding[]{
            new XRBinding ("Text", lista , "Ubicacion", "")});

            this.ShowPreview();

        }

        private void xrLine3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
