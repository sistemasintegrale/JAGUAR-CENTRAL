using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Ventas.Reporte
{
    public partial class rptBoletaElectronico : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBoletaElectronico()
        {
            InitializeComponent();
        }
        public void cargar(EFacturaVentaElectronica Obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        {
            /*-------------------------------------*/

            /*
             * Datos de cliente 
            */

            lblCliente.Text = Obe.nombreLegalReceptor.ToUpper();
            lblDireccionCliente.Text = Obe.direccionReceptor;

            lblRucCliente.Text = Obe.nroDocumentoReceptor;
            lblFechaEmision.Text = Obe.FEmisionPresentacion;
            lblFechaVencimiento.Text = Obe.fechaVencimiento;
            lblMoneda.Text = Obe.moneda;
            //lblTipoOperacion.Text = Obe.tipoOperacion;

            //lbcMarca.Text = Obe.strMarca;
            //lbcPlaca.Text = Obe.strPlaca;
            //lbcKm.Text = Obe.bovc_vkilometraje;
            //lbcAnio.Text = Convert.ToString(Obe.intAnioVehiculo);
            //string ruta = AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug\", "");

            //string RutaArchivoLogo = ruta + @"Resources\NOVAGLASS.png";

            /*
             * Datos de emisor
             */
            lblRuEmisor.Text = Obe.nroDocumentoEmisior;
            lblNombreEmisor.Text = Obe.nombreComercialEmisor;
            lblNroDocumento.Text = Obe.idDocumento;
            //lblDireccionEmisor1.Text = Obe.direccionEmisor;
            //lblDireccionEmisor2.Text = Obe.departamentoEmisor;
            //lblDireccionEmisor3.Text = Obe.provinciaEmisor + " - " + Obe.distritoEmisor;
            //PictureBoxLogo.ImageUrl = RutaArchivoLogo;
            //lblGuiaRemision.Text = nomLineaServicio;

            PictureBoxQR.Image = Convertir.GenerarQR(Convertir.GenerarCodigoQR(Obe));

            string Descripcion_moneda = "";
            if (Obe.moneda == "PEN")
            {
                Descripcion_moneda = " SOLES";
                mon1.Text = "S/";
                mon2.Text = "S/";
                mon3.Text = "S/";
                mon4.Text = "S/";
                mon5.Text = "S/";
                mon6.Text = "S/";
                mon7.Text = "S/";
                mon8.Text = "S/";
                lblMoneda.Text = "SOLES";
            }
            else
            {
                Descripcion_moneda = " DOLARES AMERICANOS";
                mon1.Text = "US$";
                mon2.Text = "US$";
                mon3.Text = "US$";
                mon4.Text = "US$";
                mon5.Text = "US$";
                mon6.Text = "US$";
                mon7.Text = "US$";
                mon8.Text = "US$";
                lblMoneda.Text = "DOLARES";
            }

            lblMontoLentra.Text = "SON: " + Convertir.ConvertNumeroEnLetras(Obe.TotalPrecioVenta.ToString()) + Descripcion_moneda;


            lblSubTotal.Text = Obe.TotalValorVenta.ToString();
            lblOpGravadas.Text = Obe.MontoGravadasIGV.ToString();
            lblOpExoneradas.Text = Obe.MontoExonerado.ToString();
            lblOpGratuito.Text = Obe.MontoGratuitoImpuesto.ToString();
            lblOpInafectadas.Text = Obe.MontoInafecto.ToString();
            lblDescuento.Text = Obe.MontoDescuentoGlobal.ToString();
            lblIgv.Text = Obe.totalIgv.ToString();
            lblTotal.Text = Obe.TotalPrecioVenta.ToString();

            this.DataSource = mlisdet;


            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "cantidad", "")});

            lblNombre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "descripcion", "")});


            lblUnidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "UMedida", "")});

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "codigoItem", "")});

            lblPrecioUnitario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "ValorUnitarioItem", "{0:n2}")});


            lblValorTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "ValorVentaItem", "{0:n2}")});

            //lblImporteDesc.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", mlisdet, "descuento", "")});

            //lblImporteNeto.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", mlisdet, "impuesto", "{0:n2}")});



            this.ShowPreview();
        }

        //internal void cargar(EPlanillaCobranzaDet obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
