using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class rptCliente : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCliente()
        {
            InitializeComponent();
        }

        public void cargar(List<ECliente> lista, string anio, string dni, string situacion, string giro, string vendedor)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa + "- AÑO " + anio;
            xrlblTitulo1.Text = "RELACIÓN ALFABÉTICA DE CLIENTES";

            System.Text.StringBuilder lineas = new System.Text.StringBuilder();
            lineas.Append("[");
            if (situacion == "Todos")
                lineas.Append("ACTIVOS E INACTIVOS");
            else
                lineas.Append(situacion);
            lineas.Append("/GIRO: ");
            lineas.Append(giro.ToUpper());
            lineas.Append("/VENDEDOR: ");
            lineas.Append(vendedor.ToUpper());
            lineas.Append("]");
            xrlblTitulo2.Text = lineas.ToString();

            this.DataSource = lista;

            //Detalles
            lblCliente.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cliec_vnombre_cliente", "")});

            lblDireccion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cliec_vdireccion_cliente", "")});

            lblDNI.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cliec_vnumero_doc_cli", "")});

            lblTelefono.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cliec_vnro_telefono", "")});

            lblVendedor.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vendc_iid_vendedor", "")});

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cliec_vcod_cliente", "")});

            this.ShowPreview();
        }

    }
}
