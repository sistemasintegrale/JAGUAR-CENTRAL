using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;


namespace SGE.WindowForms.Compras.Consultas_de_Cuentas_Corrientes
{
    public partial class RptConsultaRecibosXHonorarios : DevExpress.XtraReports.UI.XtraReport
    {
        public RptConsultaRecibosXHonorarios()
        {
            InitializeComponent();
        }

        public void Cargar(List<EDocPorPagar> Lista, string mes)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblTitulo1.Text = "RECIBOS DE HONORARIOS - " + mes.ToUpper() + " DE " + Parametros.intEjercicio;

            this.DataSource = Lista;


            #region Detalle
            lblNumDoc.DataBindings.Add(new XRBinding("Text", null, "doxpc_vnumero_doc"));
            lblFecha.DataBindings.Add(new XRBinding("Text", null, "doxpc_sfecha_doc"));
            lblCodProv.DataBindings.Add(new XRBinding("Text", null, "proc_vcod_proveedor"));
            lblNomProv.DataBindings.Add(new XRBinding("Text", null, "proc_vnombrecompleto"));
            lblImponME.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_rho_dolar", "{0:n2}"));
            lblTC.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_tipo_cambio", "{0:n4}"));
            lblMImpon.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_destino_gravado", "{0:n2}"));
            lblMInafecto.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_destino_nogravado", "{0:n2}"));
            lblRetCuarta.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_retencion_rh", "{0:n2}"));
            lblPrecCompra.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_total_documento", "{0:n2}"));
            #endregion

            #region ReportFooter
            lblImponMETT.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_rho_dolar"));
            lblMImponTT.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_destino_gravado"));
            lblMInafectoTT.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_destino_nogravado"));
            lblRetCuartaTT.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_retencion_rh"));
            lblPrecCompraTT.DataBindings.Add(new XRBinding("Text", null, "doxpc_nmonto_total_documento"));
            #endregion

            this.ShowPreview();
        }
    }
}
