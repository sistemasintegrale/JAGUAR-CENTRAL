using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class rptComprasDaotSD : DevExpress.XtraReports.UI.XtraReport
    {
        public rptComprasDaotSD()
        {
            InitializeComponent();
        }

        public void Cargar(List<EComprasDaot> Lista, decimal monto)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblTitulo1.Text = "COMPRAS A PROVEEDORES MAYOR A " + monto.ToString("n2");
            lblTitulo2.Text = "DEL AÑO " + Parametros.intEjercicio.ToString();

            this.DataSource = Lista;


            #region Detalle
            lblCodProv.DataBindings.Add(new XRBinding("Text", null, "proc_vcod_proveedor"));
            lblNomProv.DataBindings.Add(new XRBinding("Text", null, "proc_vnombrecompleto"));
            lblMontoS.DataBindings.Add(new XRBinding("Text", null, "valor_compra_soles", "{0:n2}"));
            lblMontoD.DataBindings.Add(new XRBinding("Text", null, "valor_compra_dolares", "{0:n2}"));
            #endregion

            #region ReportFooter
            lblMontoST.DataBindings.Add(new XRBinding("Text", null, "valor_compra_dolares"));
            lblMontoDT.DataBindings.Add(new XRBinding("Text", null, "valor_compra_soles"));
            #endregion

            this.ShowPreview();
        }

    }
}
