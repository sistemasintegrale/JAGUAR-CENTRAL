using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class rptVentasDaotSD : DevExpress.XtraReports.UI.XtraReport
    {
        public rptVentasDaotSD()
        {
            InitializeComponent();
        }

        public void Cargar(List<EVentasDaot> Lista, decimal monto)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblTitulo1.Text = "VENTAS A CLIENTES MAYOR A " + monto.ToString("n2");
            lblTitulo2.Text = "DEL AÑO " + Parametros.intEjercicio.ToString();

            this.DataSource = Lista;


            #region Detalle
            lblCodClie.DataBindings.Add(new XRBinding("Text", null, "cliec_vcod_cliente"));
            lblNomClie.DataBindings.Add(new XRBinding("Text", null, "cliec_vnombre_cliente"));
            lblMontoS.DataBindings.Add(new XRBinding("Text", null, "valor_venta_soles", "{0:n2}"));
            lblMontoD.DataBindings.Add(new XRBinding("Text", null, "valor_venta_dolares", "{0:n2}"));
            #endregion

            #region ReportFooter
            lblMontoST.DataBindings.Add(new XRBinding("Text", null, "valor_venta_soles"));
            lblMontoDT.DataBindings.Add(new XRBinding("Text", null, "valor_venta_dolares"));
            #endregion

            this.ShowPreview();
        }

    }
}
