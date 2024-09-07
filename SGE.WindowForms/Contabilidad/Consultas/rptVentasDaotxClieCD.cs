using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class rptVentasDaotxClieCD : DevExpress.XtraReports.UI.XtraReport
    {
        public rptVentasDaotxClieCD()
        {
            InitializeComponent();
        }

        public void Cargar(List<EVentasDaotDetalle> ListaCD, string nom_prov)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblTitulo1.Text = "DOCUMENTOS DE VENTA DEL AÑO " + Parametros.intEjercicio.ToString("n2");
            lblTitulo2.Text = "DEL CLIENTE " + nom_prov;

            this.DataSource = ListaCD;


            #region Detalle
            lblTipoDoc.DataBindings.Add(new XRBinding("Text", null, "tdocc_vabreviatura_tipo_doc"));
            lblNumDoc.DataBindings.Add(new XRBinding("Text", null, "doxcc_vnumero_doc"));
            lblFecha.DataBindings.Add(new XRBinding("Text", null, "doxcc_sfecha_doc", "{0:dd/MM/yyyy}"));
            lblMoneda.DataBindings.Add(new XRBinding("Text", null, "simboloMoneda"));
            lblMontoTotal.DataBindings.Add(new XRBinding("Text", null, "doxcc_nmonto_total", "{0:n2}"));
            lblMontoS.DataBindings.Add(new XRBinding("Text", null, "valor_venta_soles", "{0:n2}"));
            lblMontoD.DataBindings.Add(new XRBinding("Text", null, "valor_venta_dolares_str", "{0:n2}"));
            lblTC.DataBindings.Add(new XRBinding("Text", null, "doxcc_nmonto_tipo_cambio", "{0:n4}"));
            #endregion

            #region ReportFooter
            lblMontoST.DataBindings.Add(new XRBinding("Text", null, "valor_venta_soles"));
            #endregion

            this.ShowPreview();
        }
    }
}
