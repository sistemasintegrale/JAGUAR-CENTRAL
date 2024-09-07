using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class rptComprasDaotCD : DevExpress.XtraReports.UI.XtraReport
    {
        public rptComprasDaotCD()
        {
            InitializeComponent();
        }

        List<EComprasDaotDetalle> ListaCD;
        List<EComprasDaot> ListaSD;

        public void Cargar(List<EComprasDaotDetalle> ListaCD, List<EComprasDaot> ListaSD, decimal monto)
        {
            this.ListaCD = ListaCD;
            this.ListaSD = ListaSD;
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblTitulo1.Text = "COMPRAS A PROVEEDORES MAYOR A " + monto.ToString("n2");
            lblTitulo2.Text = "DEL AÑO " + Parametros.intEjercicio.ToString();
            this.DataSource = CrearData();
            DetailReport1.DataMember = "EComprasDaotDetalles";


            #region Detalles Compras Proveedor
            lblCodProv.DataBindings.Add(new XRBinding("Text", null, "proc_vcod_proveedor"));
            lblNomProv.DataBindings.Add(new XRBinding("Text", null, "proc_vnombrecompleto"));
            lblMontoProvS.DataBindings.Add(new XRBinding("Text", null, "valor_compra_soles", "{0:n2}"));
            lblMontoProvD.DataBindings.Add(new XRBinding("Text", null, "valor_compra_dolares", "{0:n2}"));
            #endregion

            #region Detail
            lblTipoDoc.DataBindings.Add(new XRBinding("Text", null, "EComprasDaotDetalles.tdocc_vabreviatura_tipo_doc"));
            lblNumDoc.DataBindings.Add(new XRBinding("Text", null, "EComprasDaotDetalles.doxpc_vnumero_doc"));
            lblFecha.DataBindings.Add(new XRBinding("Text", null, "EComprasDaotDetalles.doxpc_sfecha_doc", "{0:dd/MM/yyyy}"));
            lblMoneda.DataBindings.Add(new XRBinding("Text", null, "EComprasDaotDetalles.simboloMoneda"));
            lblMontoS.DataBindings.Add(new XRBinding("Text", null, "EComprasDaotDetalles.valor_compra_soles", "{0:n2}"));
            lblMontoD.DataBindings.Add(new XRBinding("Text", null, "EComprasDaotDetalles.valor_compra_dolares", "{0:n2}"));
            lblTC.DataBindings.Add(new XRBinding("Text", null, "EComprasDaotDetalles.doxpc_nmonto_tipo_cambio", "{0:n4}"));
            lblConcepto.DataBindings.Add(new XRBinding("Text", null, "EComprasDaotDetalles.doxpc_vdescrip_transaccion"));
            #endregion

            #region ReportFooter
            lblMontoST.DataBindings.Add(new XRBinding("Text", null, "EComprasDaotDetalles.valor_compra_soles"));
            lblMontoDT.DataBindings.Add(new XRBinding("Text", null, "EComprasDaotDetalles.valor_compra_dolares"));
            #endregion

            this.ShowPreview();
        }

        private EComprasDaotCollection CrearData()
        {
            EComprasDaotCollection cdaots = new EComprasDaotCollection();

            foreach (EComprasDaot item in ListaSD)
            {
                foreach (EComprasDaotDetalle item2 in ListaCD.Where(obe => obe.proc_icod_proveedor == item.proc_icod_proveedor).ToList())
                {
                    item.eComprasDaotDetalles.Add(item2);
                }
                cdaots.Add(item);
            }
            return cdaots;
        }

    }
}
