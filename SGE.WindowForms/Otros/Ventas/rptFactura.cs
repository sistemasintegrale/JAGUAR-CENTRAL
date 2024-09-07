using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class rptFactura : DevExpress.XtraReports.UI.XtraReport
    {
        public rptFactura()
        {
            InitializeComponent();
        }
        public void cargar(EFacturaCab oBe, List<EFacturaDet> mlisdetalle, string total, string Departamento, string Provincia, string Distrito)
        {
            this.DataSource = mlisdetalle;
            #region Cabecera de la Factura
            string str_moneda = "";
            int dia, año;
            string mes;
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

            dia = oBe.favc_sfecha_factura.Day;
            mes = oBe.favc_sfecha_factura.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX")).ToUpper();
            año = oBe.favc_sfecha_factura.Year;
            lblDia.Text = dia.ToString();
            lblMes.Text = mes;
            lblAño.Text = año.ToString();
            //lblFecha.Text = oBe.favc_sfecha_factura.ToShortDateString();
            lblDesCliente.Text = oBe.cliec_vnombre_cliente;
            lblDireccion.Text = oBe.favc_vdireccion_cliente;
            lblVencimiento.Text = oBe.favc_sfecha_vencim_factura.ToShortDateString();
            //lblreferencia.Text = "G/R " + oBe.remic_vnumero_remision;

            //lblformaPago.Text = oBe.strFormaPago;
            //lblGuiaRemision.Text = oBe.remic_vnumero_remision;

            //if(Distrito!="")
            //{
            //    lblubigeo.Text = Departamento + "-" + Provincia + "-" + Distrito;
            //}
            lblRuc.Text = oBe.favc_vruc;

            //lblporcentajeIgv.Text = oBe.favc_npor_imp_igv.ToString("N2")+" %";
            lblNroFactura.Text = oBe.favc_vnumero_factura;
            //lblObservaciones1.Text = oBe.favc_vobservacion;
            #endregion

            #region Pie de la Factura
            lblSubTotal.Text = oBe.favc_nmonto_neto.ToString("N2");
            lblIGV.Text = oBe.favc_nmonto_imp.ToString("N2");
            lblTotalFactura.Text = oBe.favc_nmonto_total.ToString("N2");

            lblMontoletras.Text = total + str_moneda_letras;
            #endregion

            lblItem.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "strCodProducto", "")
            });

            lblDescripccion.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_vdescripcion", "")
            });
            lblCaracteristicas.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_vcaracteristicas", "")
            });

            lblCantidad.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_vncantidad", "{0:#}")
            });
            lblPrecioUnitario.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_vnprecio_unitario_item", "")
            });
            lblMontoBruto.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_vnprecio_total_item", "")
            });

            lblMoneda1.Text = str_moneda;
            lblMoneda2.Text = str_moneda;
            lblMoneda3.Text = str_moneda;
            lblMoneda4.Text = str_moneda;
            lblMoneda5.Text = str_moneda;
            lblMoneda6.Text = str_moneda;
            lblMoneda7.Text = str_moneda;
            lblMoneda8.Text = str_moneda;
            lblMoneda9.Text = str_moneda;
            lblMoneda10.Text = str_moneda;
            lblMoneda11.Text = str_moneda;

            if (oBe.favc_nanticipo > 0)
            {
                GroupFooter1.Visible = true;
                lblNombreAnticipo.Text = (oBe.favc_vnomb_anticipo + " " + Convert.ToInt32(oBe.favc_nanticipo) + "%").ToString();
                lblNombreGarantia.Text = oBe.favc_vnomb_garantia;
                decimal anticipo, subtotalAnticipo, igvAnticipo, totalFacturar, montogarantia, valorTotal = 0;
                double intAnticipo;
                intAnticipo = Convert.ToDouble(Convert.ToInt32(oBe.favc_nanticipo)) / 100;
                anticipo = (Convert.ToDecimal(intAnticipo * Convert.ToDouble(oBe.favc_nmonto_neto)));
                subtotalAnticipo = Convert.ToDecimal(Convert.ToDouble(oBe.favc_nmonto_neto) - Convert.ToDouble(anticipo));
                igvAnticipo = Convert.ToDecimal(Convert.ToDouble(subtotalAnticipo) * 0.18);
                totalFacturar = Convert.ToDecimal(Convert.ToDouble(subtotalAnticipo) + Convert.ToDouble(igvAnticipo));
                montogarantia = oBe.favc_nmonto_garantia;
                valorTotal = totalFacturar - montogarantia;

                lblAnticipo.Text = "-" + anticipo.ToString("N2");
                lblSubtotalAnticipo.Text = subtotalAnticipo.ToString("N2");
                lblIGVAnticipo.Text = igvAnticipo.ToString("N2");
                lblTotalFactAnticipo.Text = totalFacturar.ToString("N2");
                lblMontoGarantia.Text = "-" + montogarantia.ToString("N2");
                lblMontoValorTotal.Text = valorTotal.ToString("N2");
                //----
                lblSubTotal.Text = subtotalAnticipo.ToString("N2");
                lblIGV.Text = igvAnticipo.ToString("N2");
                lblTotalFactura.Text = totalFacturar.ToString("N2");


                total = Convertir.ConvertNumeroEnLetras(totalFacturar.ToString());
                lblMontoletras.Text = total + str_moneda_letras;
                if (oBe.favc_nmonto_garantia > 0)
                {
                    lblNombreGarantia.Visible = true;
                    lblNombreValorTotal.Visible = true;
                    lblMontoGarantia.Visible = true;
                    lblMontoValorTotal.Visible = true;
                    xrLine3.Visible = true;
                    lblMoneda10.Visible = true;
                    lblMoneda11.Visible = true;
                }
                else
                {
                    lblNombreGarantia.Visible = false;
                    lblNombreValorTotal.Visible = false;
                    lblMontoGarantia.Visible = false;
                    lblMontoValorTotal.Visible = false;
                    xrLine3.Visible = false;
                    lblMoneda10.Visible = false;
                    lblMoneda11.Visible = false;
                }
            }
            else
            {
                GroupFooter1.Visible = false;
            }


            //lblUME.DataBindings.AddRange(new XRBinding[]
            //{
            //new XRBinding("Text", mlisdetalle , "strDesUM", "")
            //});


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

            ////this.PrintDialog();
            this.ShowPreview();

        }

        private void subRptAvanceVentasGiroXFechasResumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void lblCaracteristicas_TextChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("favd_vcaracteristicas") == "" || GetCurrentColumnValue("favd_vcaracteristicas") == null)
            {
                lblCaracteristicas.Visible = false;
            }
            else
            {
                lblCaracteristicas.Visible = true;
            }
        }


    }
}
