using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Ventas.Reporte
{
    public partial class rptFacturaElectronico : DevExpress.XtraReports.UI.XtraReport
    {
        public rptFacturaElectronico()
        {
            InitializeComponent();
        }
        public void cargar(EFacturaVentaElectronica Obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        {
            /*-------------------------------- -----*/

            /*
             * Datos de cliente 
            */

            lblCliente.Text = Obe.nombreLegalReceptor.ToUpper();
            lblDireccionCliente.Text = Obe.direccionReceptor;

            lblRucCliente.Text = Obe.nroDocumentoReceptor;
            lblFechaEmision.Text = Obe.FEmisionPresentacion;
            lblGuiaRemision.Text = Obe.favc_vnumero_guia;
            lblMoneda.Text = Obe.moneda;
            lblFechaVencimiento.Text = Obe.fechaVencimiento;
            //lblTipoOperacion.Text = Obe.tipoOperacion;
            lblOrdenCompra.Text = Obe.favc_vnumero_orden;

            /*
             * Datos de emisor
             */
            lblRuEmisor.Text = Obe.nroDocumentoEmisior;

            lblNroDocumento.Text = Obe.idDocumento;

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

            lblPorcRetencion.Text = $"Retención ( {Obe.PorcRetension.ToString("n2")}% )";
            lblMontoRetencion.Text = Obe.MontoRetension.ToString("n2");

            if (Obe.MontoRetension == 0)
            {
                lblPorcRetencion.Visible = false;
                lblMontoRetencion.Visible = false;
                xrLabel27.Visible = false;
            }


            int intDias = ((TimeSpan)(Convert.ToDateTime(Obe.fechaVencimiento) - Convert.ToDateTime(Obe.fechaEmision))).Days;

            if (intDias == 0)
            {
                lblCondicionPago.Text = "";
            }
            else
            {
                lblCondicionPago.Text = "A LOS " + intDias + " DIAS";
            }



            this.DataSource = mlisdet;


            lblCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "cantidad", "")});

            lblNombre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "descripcion", "")});


            lblUnidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "unidadMedida", "")});

            lblCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "codigoItem", "")});

            lblPrecioUnitario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "ValorUnitarioItem", "{0:n2}")});


            lblValorTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "MontoAfectoImpuestoIgv", "{0:n2}")});

            //lblImporteDesc.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", mlisdet, "descuento", "")});

            //lblImporteNeto.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", mlisdet, "impuesto", "{0:n2}")});
            if (Obe.cuotas.Count != 0)
            {
                rptCuotasFAV rpt = new rptCuotasFAV();
                rpt.cargar(Obe.cuotas);
                xrSubreport1.ReportSource = rpt;
            }
            else
            {
                xrSubreport1.Visible = false;
            }



            this.ShowPreview();
        }

        //internal void cargar(EPlanillaCobranzaDet obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
