using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class rptFacturaChica : DevExpress.XtraReports.UI.XtraReport
    {
        public rptFacturaChica()
        {
            InitializeComponent();
        }
        public void cargar(EFacturaCab oBe, List<EFacturaDet> mlisdetalle, string total, string Departamento, string Provincia, string Distrito)
        {
            this.DataSource = mlisdetalle;
            #region Cabecera de la Factura
            string str_moneda = "";
            string str_moneda_letras = "";
            if (oBe.tablc_iid_tipo_moneda == 3)
            {
                str_moneda = "S/. ";
                str_moneda_letras = " NUEVOS SOLES.";
            }
            else
            {
                str_moneda = "$. ";
                str_moneda_letras = " DOLARES AMERICANOS.";
            }


            lblFecha.Text = oBe.favc_sfecha_factura.ToShortDateString();
            lblDesCliente.Text = oBe.cliec_vnombre_cliente;
            lblDireccion.Text = oBe.favc_vdireccion_cliente;
            //lblreferencia.Text = "G/R " + oBe.remic_vnumero_remision;

            lblformaPago.Text = oBe.strFormaPago;
            lblGuiaRemision.Text = oBe.remic_vnumero_remision;

            //if(Distrito!="")
            //{
            //    lblubigeo.Text = Departamento + "-" + Provincia + "-" + Distrito;
            //}
            lblRuc.Text = oBe.favc_vruc;

            lblporcentajeIgv.Text = oBe.favc_npor_imp_igv.ToString("N2") + " %";
            lblNroFactura.Text = oBe.favc_vnumero_factura;
            //lblObservaciones1.Text = oBe.favc_vobservacion;
            #endregion

            #region Pie de la Factura
            lblSubTotal.Text = str_moneda + oBe.favc_nmonto_neto.ToString("N2");
            lblIGV.Text = str_moneda + oBe.favc_nmonto_imp.ToString("N2");
            lblTotalFactura.Text = str_moneda + oBe.favc_nmonto_total.ToString("N2");

            lblMontoletras.Text = "SON " + total + str_moneda_letras;
            #endregion

            //lblCodigo.DataBindings.AddRange(new XRBinding[]
            //{
            //new XRBinding("Text", mlisdetalle , "strCodProducto", "")
            //});

            lblDescripccion.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "strDesProducto", "")
            });
            lblcodigo.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "strCodProducto", "")
            });
            lblCantidad.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_vncantidad", "")
            });
            lblPrecioUnitario.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_vnprecio_unitario_item", "")
            });
            lblMontoBruto.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_vnprecio_total_item", "")
            });

            lblUME.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "strDesUM", "")
            });


            //string[] partes = oBe.favc_vobservacion.Split('@');
            //if (partes.Length > 0)
            //{
            //    lblObservaciones1.Text = partes[0];
            //    if (partes.Length >= 2)
            //    {
            //        lblObservaciones2.Text = partes[1];
            //    }
            //    if (partes.Length >= 3)
            //    {
            //        lblObservaciones3.Text = partes[2];
            //    }
            //    if (partes.Length >= 4)
            //    {
            //        lblObservaciones4.Text = partes[3];
            //    }
            //    if (partes.Length >= 5)
            //    {
            //        lblObservaciones5.Text = partes[4];
            //    }
            //}


            this.ShowPreview();

        }

        private void subRptAvanceVentasGiroXFechasResumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }


    }
}
