using DevExpress.XtraReports.UI;
using SGE.WindowForms.Modules;
using System;
using System.Data;


namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class rptEstadoCuentaDetallePagoProveedor : DevExpress.XtraReports.UI.XtraReport
    {
        public rptEstadoCuentaDetallePagoProveedor()
        {
            InitializeComponent();
        }
        public void cargar(DataTable lista, string anio, string titulo1, string titulo2)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + anio;
            xrlblTitulo1.Text = titulo1;
            xrlblTitulo2.Text = titulo2;

            this.DataSource = lista;

            //Agrupación por (N/C, Adelantos = 1 --- Otros documentos = 0)
            GrupoTipoDocumento.GroupFields.AddRange(new GroupField[] { new GroupField("tdocc_icod_tipo_doc") });

            //Agrupación por Documento
            GrupoDocumento.GroupFields.AddRange(new GroupField[] { new GroupField("doxpc_vnumero_doc") });

            //Enlace al GrupoDocumento
            lblNumeroDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_vnumero_doc", "")});

            lblFechaDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_sfecha_doc", "{0:dd/MM/yyyy}")});

            lblMoneda.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "SimboloMoneda", "")});

            lblPrecioVenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ValorVenta", "{0:n}")});

            lblMontoPagado.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}")});

            lblSaldoActual.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}")});

            lblFechaVencimiento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_sfecha_vencimiento_doc", "{0:dd/MM/yyyy}")});

            lblFechaEntrega.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_sfecha_emision_referencia", "{0:dd/MM/yyyy}")});

            lblObservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_vdescrip_transaccion", "")});


            //Detalle de los pagos
            lblNumeroDocumentoPago.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "DocPago", "")});

            lblFechaDocumentoPago.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "FechaPago", "{0:dd/MM/yyyy}")});

            lblMonedaPago.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "MonedaPago", "")});

            lblMontoPagadoPago.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "MontoPago", "{0:n}")});



            //enlaces al grupo 1
            lblGTDPrecioVentaMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ValorVenta", "{0:n}")});

            lblGTDMontoPagadoMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}")});

            lblGTDSaldoActualMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}")});


            lblGTDPrecioVentaME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ValorVenta", "{0:n}")});

            lblGTDMontoPagadoME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}")});

            lblGTDSaldoActualME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}")});


            //enlaces al grupo de totales
            lblGGPrecioVentaMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ValorVenta", "{0:n}")});

            lblGGMontoPagadoMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}")});

            lblGGSaldoActualMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}")});

            lblGGPrecioVentaME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ValorVenta", "{0:n}")});

            lblGGMontoPagadoME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}")});

            lblGGSaldoActualME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}")});


            this.ShowPreview();
        }
        decimal SaldoMN = 0;
        decimal SaldoME = 0;
        decimal PagadoMN = 0;
        decimal PagadoME = 0;
        decimal PrecioMN = 0;
        decimal PrecioME = 0;

        string DocSaldoMN = "";
        string DocSaldoME = "";
        string DocPagadoMN = "";
        string DocPagadoME = "";
        string DocPrecioMN = "";
        string DocPrecioME = "";

        private void lblGTDSaldoActualMN_SummaryReset(object sender, EventArgs e)
        {
            SaldoMN = 0;
            DocSaldoMN = "";
        }

        private void lblGTDSaldoActualMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 3 && DocSaldoMN != GetCurrentColumnValue("doxpc_vnumero_doc").ToString())
                SaldoMN = SaldoMN + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_saldo"));
            DocSaldoMN = GetCurrentColumnValue("doxpc_vnumero_doc").ToString();
        }

        private void lblGTDSaldoActualMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SaldoMN;
            e.Handled = true;
        }

        private void lblGTDSaldoActualME_SummaryReset(object sender, EventArgs e)
        {
            SaldoME = 0;
            DocSaldoME = "";
        }

        private void lblGTDSaldoActualME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 4 && DocSaldoME != GetCurrentColumnValue("doxpc_vnumero_doc").ToString())
                SaldoME = SaldoME + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_saldo"));
            DocSaldoME = GetCurrentColumnValue("doxpc_vnumero_doc").ToString();
        }

        private void lblGTDSaldoActualME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SaldoME;
            e.Handled = true;
        }

        private void lblGTDMontoPagadoMN_SummaryReset(object sender, EventArgs e)
        {
            PagadoMN = 0;
            DocPagadoMN = "";
        }

        private void lblGTDMontoPagadoMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 3 && DocPagadoMN != GetCurrentColumnValue("doxpc_vnumero_doc").ToString())
                PagadoMN = PagadoMN + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_pagado"));
            DocPagadoMN = GetCurrentColumnValue("doxpc_vnumero_doc").ToString();
        }

        private void lblGTDMontoPagadoMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PagadoMN;
            e.Handled = true;
        }

        private void lblGTDMontoPagadoME_SummaryReset(object sender, EventArgs e)
        {
            PagadoME = 0;
            DocPagadoME = "";
        }

        private void lblGTDMontoPagadoME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PagadoME;
            e.Handled = true;
        }

        private void lblGTDMontoPagadoME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 4 && DocPagadoME != GetCurrentColumnValue("doxpc_vnumero_doc").ToString())
                PagadoME = PagadoME + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_pagado"));
            DocPagadoME = GetCurrentColumnValue("doxpc_vnumero_doc").ToString();
        }

        private void lblGTDPrecioVentaMN_SummaryReset(object sender, EventArgs e)
        {
            PrecioMN = 0;
            DocPrecioMN = "";
        }

        private void lblGTDPrecioVentaMN_SummaryRowChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 3 && DocPrecioMN != GetCurrentColumnValue("doxpc_vnumero_doc").ToString())
                PrecioMN = PrecioMN + Convert.ToDecimal(GetCurrentColumnValue("ValorVenta"));
            DocPrecioMN = GetCurrentColumnValue("doxpc_vnumero_doc").ToString();
        }

        private void lblGTDPrecioVentaMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PrecioMN;
            e.Handled = true;
        }

        private void lblGTDPrecioVentaME_SummaryReset(object sender, EventArgs e)
        {
            PrecioME = 0;
            DocPrecioME = "";
        }

        private void lblGTDPrecioVentaME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 4 && DocPrecioME != GetCurrentColumnValue("doxpc_vnumero_doc").ToString())
                PrecioME = PrecioME + Convert.ToDecimal(GetCurrentColumnValue("ValorVenta"));
            DocPrecioME = GetCurrentColumnValue("doxpc_vnumero_doc").ToString();
        }

        private void lblGTDPrecioVentaME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PrecioME;
            e.Handled = true;
        }
        decimal SaldoGMN = 0;
        decimal SaldoGME = 0;
        decimal PagadoGMN = 0;
        decimal PagadoGME = 0;
        decimal PrecioGMN = 0;
        decimal PrecioGME = 0;

        string DocSaldoGMN = "";
        string DocSaldoGME = "";
        string DocPagadoGMN = "";
        string DocPagadoGME = "";
        string DocPrecioGMN = "";
        string DocPrecioGME = "";

        private void lblGGSaldoActualMN_SummaryReset(object sender, EventArgs e)
        {
            SaldoGMN = 0;
            DocSaldoGMN = "";
        }

        private void lblGGSaldoActualMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 3 && DocSaldoGMN != GetCurrentColumnValue("doxpc_vnumero_doc").ToString())
                SaldoGMN = SaldoGMN + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_saldo"));
            DocSaldoGMN = GetCurrentColumnValue("doxpc_vnumero_doc").ToString();
        }

        private void lblGGSaldoActualMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SaldoGMN;
            e.Handled = true;
        }

        private void lblGGSaldoActualME_SummaryReset(object sender, EventArgs e)
        {
            SaldoGME = 0;
            DocSaldoGME = "";
        }

        private void lblGGSaldoActualME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 4 && DocSaldoGME != GetCurrentColumnValue("doxpc_vnumero_doc").ToString())
                SaldoGME = SaldoGME + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_saldo"));
            DocSaldoGME = GetCurrentColumnValue("doxpc_vnumero_doc").ToString();
        }

        private void lblGGSaldoActualME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SaldoGME;
            e.Handled = true;
        }

        private void lblGGMontoPagadoMN_SummaryReset(object sender, EventArgs e)
        {
            PagadoGMN = 0;
            DocPagadoGMN = "";
        }

        private void lblGGMontoPagadoMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 3 && DocPagadoGMN != GetCurrentColumnValue("doxpc_vnumero_doc").ToString())
                PagadoGMN = PagadoGMN + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_pagado"));
            DocPagadoGMN = GetCurrentColumnValue("doxpc_vnumero_doc").ToString();
        }

        private void lblGGMontoPagadoMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PagadoGMN;
            e.Handled = true;
        }

        private void lblGGMontoPagadoME_SummaryReset(object sender, EventArgs e)
        {
            PagadoGME = 0;
            DocPagadoGME = "";
        }

        private void lblGGMontoPagadoME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 4 && DocPagadoGME != GetCurrentColumnValue("doxpc_vnumero_doc").ToString())
                PagadoGME = PagadoGME + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_pagado"));
            DocPagadoGME = GetCurrentColumnValue("doxpc_vnumero_doc").ToString();
        }

        private void lblGGMontoPagadoME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PagadoGME;
            e.Handled = true;
        }

        private void lblGGPrecioVentaMN_SummaryReset(object sender, EventArgs e)
        {
            PrecioGMN = 0;
            DocPrecioGMN = "";
        }

        private void lblGGPrecioVentaMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 3 && DocPrecioGMN != GetCurrentColumnValue("doxpc_vnumero_doc").ToString())
                PrecioGMN = PrecioGMN + Convert.ToDecimal(GetCurrentColumnValue("ValorVenta"));
            DocPrecioGMN = GetCurrentColumnValue("doxpc_vnumero_doc").ToString();
        }

        private void lblGGPrecioVentaMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PrecioGMN;
            e.Handled = true;
        }

        private void lblGGPrecioVentaME_SummaryReset(object sender, EventArgs e)
        {
            PrecioGME = 0;
            DocPrecioGME = "";
        }

        private void lblGGPrecioVentaME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 4 && DocPrecioGME != GetCurrentColumnValue("doxpc_vnumero_doc").ToString())
                PrecioGME = PrecioGME + Convert.ToDecimal(GetCurrentColumnValue("ValorVenta"));
            DocPrecioGME = GetCurrentColumnValue("doxpc_vnumero_doc").ToString();
        }

        private void lblGGPrecioVentaME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PrecioGME;
            e.Handled = true;
        }





    }
}
