using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class rptFAV : DevExpress.XtraReports.UI.XtraReport
    {
        public rptFAV()
        {
            InitializeComponent();
        }

        public void cargar(EFacturaCab oBe, List<EFacturaDet> mlisdetalle, string total, string Departamento, string Provincia, string Distrito, EFacturaVentaElectronica Obe)
        {
            string str_moneda = string.Empty;
            string str_moneda_letras = string.Empty;
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
            lblNroFactura.Text = oBe.favc_vnumero_factura;
            lblDesCliente.Text = oBe.cliec_vnombre_cliente;
            lblDireccion.Text = oBe.favc_vdireccion_cliente;
            lblVencimiento.Text = oBe.favc_sfecha_vencim_factura.ToShortDateString();
            lblFechaEmision.Text = oBe.favc_sfecha_factura.ToString("dd/MM/yyyy");
            lblMoneda.Text = str_moneda_letras;
            PictureBoxQR.Image = Convertir.GenerarQR(Convertir.GenerarCodigoQR(Obe));
            lblMontoLetras.Text = Convertir.ConvertNumeroEnLetras(oBe.favc_nmonto_total.ToString());




            lblDescripccion.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "favd_vdescripcion", "")
            });
            lblUnid.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "UM", "")
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


            lblOpGravada.Text = oBe.favc_nmonto_neto.ToString("N2");
            lblImporteTotal.Text = oBe.favc_nmonto_total.ToString("N2");
            txtIgv.Text = oBe.favc_nmonto_imp.ToString("N2");


            xrLabel39.Text = str_moneda;
            xrLabel40.Text = str_moneda;
            xrLabel41.Text = str_moneda;
            xrLabel42.Text = str_moneda;
            xrLabel43.Text = str_moneda;
            xrLabel44.Text = str_moneda;
            xrLabel45.Text = str_moneda;
            xrLabel46.Text = str_moneda;
            xrLabel47.Text = str_moneda;
            xrLabel48.Text = str_moneda;
        }
    }
}
