using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class rptVentasDaotCD : DevExpress.XtraReports.UI.XtraReport
    {
        public rptVentasDaotCD()
        {
            InitializeComponent();
        }

        List<EVentasDaotDetalle> ListaCD;
        List<EVentasDaot> ListaSD;

        public void Cargar(List<EVentasDaotDetalle> ListaCD, List<EVentasDaot> ListaSD, decimal monto)
        {
            this.ListaCD = ListaCD;
            this.ListaSD = ListaSD;
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblTitulo1.Text = "VENTAS A CLIENTES MAYOR A " + monto.ToString("n2");
            lblTitulo2.Text = "DEL AÑO " + Parametros.intEjercicio.ToString();
            this.DataSource = CrearData();
            DetailReport1.DataMember = "EVentasDaotDetalles";


            #region Detalles Compras Proveedor
            lblCodClie.DataBindings.Add(new XRBinding("Text", null, "cliec_vcod_cliente"));
            lblNomClie.DataBindings.Add(new XRBinding("Text", null, "cliec_vnombre_cliente"));
            lblMontoProvS.DataBindings.Add(new XRBinding("Text", null, "valor_venta_soles", "{0:n2}"));
            lblMontoProvD.DataBindings.Add(new XRBinding("Text", null, "valor_venta_dolares", "{0:n2}"));
            #endregion

            #region Detail
            lblTipoDoc.DataBindings.Add(new XRBinding("Text", null, "EVentasDaotDetalles.tdocc_vabreviatura_tipo_doc"));
            lblNumDoc.DataBindings.Add(new XRBinding("Text", null, "EVentasDaotDetalles.doxcc_vnumero_doc"));
            lblFecha.DataBindings.Add(new XRBinding("Text", null, "EVentasDaotDetalles.doxcc_sfecha_doc", "{0:dd/MM/yyyy}"));
            lblMoneda.DataBindings.Add(new XRBinding("Text", null, "EVentasDaotDetalles.simboloMoneda"));
            lblMontoS.DataBindings.Add(new XRBinding("Text", null, "EVentasDaotDetalles.valor_venta_soles", "{0:n2}"));
            lblMontoD.DataBindings.Add(new XRBinding("Text", null, "EVentasDaotDetalles.valor_venta_dolares", "{0:n2}"));
            lblTC.DataBindings.Add(new XRBinding("Text", null, "EVentasDaotDetalles.doxcc_nmonto_tipo_cambio", "{0:n4}"));
            lblConcepto.DataBindings.Add(new XRBinding("Text", null, "EVentasDaotDetalles.doxcc_vdescrip_transaccion"));
            #endregion

            #region ReportFooter
            lblMontoST.DataBindings.Add(new XRBinding("Text", null, "EVentasDaotDetalles.valor_venta_soles"));
            lblMontoDT.DataBindings.Add(new XRBinding("Text", null, "EVentasDaotDetalles.valor_venta_dolares"));
            #endregion

            this.ShowPreview();
        }

        private EVentasDaotCollection CrearData()
        {
            EVentasDaotCollection vdaots = new EVentasDaotCollection();

            foreach (EVentasDaot item in ListaSD)
            {
                foreach (EVentasDaotDetalle item2 in ListaCD.Where(obe => obe.cliec_icod_cliente == item.cliec_icod_cliente).ToList())
                {
                    item.eVentasDaotDetalles.Add(item2);
                }
                vdaots.Add(item);
            }
            return vdaots;
        }

    }
}
