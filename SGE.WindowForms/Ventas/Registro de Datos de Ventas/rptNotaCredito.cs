using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class rptNotaCredito : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNotaCredito()
        {
            InitializeComponent();
        }
        public void cargar(ENotaCredito oBe, List<ENotaCreditoDet> mlisdetalle, string total, string Departamento, string Provincia, string Distrito)
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
            string tipo_doc = "";
            if (oBe.tdocc_icod_tipo_doc == 9)
            {
                tipo_doc = "BOV ";
            }
            else
            {
                tipo_doc = "FAV ";
            }

            lblFecha.Text = oBe.ncrec_sfecha_credito.ToShortDateString();
            lblDesCliente.Text = oBe.strDesCliente;
            //lblDireccion.Text = oBe.favc_vdireccion_cliente;
            lblreferencia.Text = tipo_doc + oBe.ncrec_vnumero_documento;
            //if (Distrito != "")
            //{
            //    lblubigeo.Text = Departamento + "-" + Provincia + "-" + Distrito;
            //}
            lblRuc.Text = oBe.strRuc;

            lblporcentajeIgv.Text = oBe.ncrec_npor_imp_igv.ToString("N2") + " %";
            lblNroFactura.Text = oBe.ncrec_vnumero_credito;
            //lblObservaciones1.Text = oBe.favc_vobservacion;
            #endregion

            #region Pie de la Factura
            lblSubTotal.Text = str_moneda + oBe.ncrec_nmonto_neto.ToString("N2");
            lblIGV.Text = str_moneda + oBe.ncrec_npor_imp_igv.ToString("N2");
            lblTotalFactura.Text = str_moneda + oBe.ncrec_nmonto_total.ToString("N2");

            //lblMontoletras.Text = total + str_moneda_letras;
            #endregion

            lblcodigoProducto.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "strCodProducto", "")
            });

            lblDescripccion.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "strDesProducto", "")
            });

            lblCantidad.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "dcrec_ncantidad_producto", "")
            });
            lblPrecioUnitario.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "dcrec_nmonto_unitario", "")
            });
            lblMontoBruto.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "dcrec_nmonto_item", "")
            });


            lblMontoletras.Text = "SON: " + total;


            this.ShowPreview();

        }

        private void subRptAvanceVentasGiroXFechasResumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }


    }
}
