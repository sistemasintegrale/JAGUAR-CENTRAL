using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;


namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class rptEstadoCuentaDetalleProveedor : DevExpress.XtraReports.UI.XtraReport
    {
        public rptEstadoCuentaDetalleProveedor()
        {
            InitializeComponent();
        }
        public void cargar(List<EDocPorPagar> lista, string anio, string titulo1, string titulo2)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + anio;
            xrlblTitulo1.Text = titulo1;
            xrLabel11.Text = titulo2;
            //  xrlblTitulo2.Text = "DEL CLIENTE: " + cliente;

            this.DataSource = lista;

            //Agrupación por (N/C, Adelantos = 1 --- Otros documentos = 0)
            GrupoTipoDocumento.GroupFields.AddRange(new GroupField[] {
            new GroupField("tdocc_icod_tipo_doc")});

            //Detalles
            lblNumeroDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_vnumero_doc", "")});

            lblFechaDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_sfecha_doc", "{0:dd/MM/yyyy}")});

            lblMoneda.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "SimboloMoneda", "")});

            lblPrecioVenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "Valorcompra", "{0:n}")});

            lblMontoPagado.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}")});

            lblSaldoActual.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}")});

            lblFechaVencimiento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_sfecha_vencimiento_doc", "{0:dd/MM/yyyy}")});

            lblFechaEntrega.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_sfecha_doc", "{0:dd/MM/yyyy}")});

            lblObservaciones.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_vdescrip_transaccion", "")});

            //enlaces al grupo 1
            lblGTDPrecioVentaMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "Valorcompra", "{0:n}")});

            lblGTDMontoPagadoMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}")});

            lblGTDSaldoActualMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}")});


            lblGTDPrecioVentaME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "Valorcompra", "{0:n}")});

            lblGTDMontoPagadoME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}")});

            lblGTDSaldoActualME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}")});


            //enlaces al grupo de totales
            lblGGPrecioVentaMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "Valorcompra", "{0:n}")});

            lblGGMontoPagadoMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}")});

            lblGGSaldoActualMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}")});

            lblGGPrecioVentaME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "Valorcompra", "{0:n}")});

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

        private void lblGTDSaldoActualMN_SummaryReset(object sender, EventArgs e)
        {
            SaldoMN = 0;
        }

        private void lblGTDSaldoActualMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 1)
                SaldoMN = SaldoMN + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_saldo"));
        }

        private void lblGTDSaldoActualMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SaldoMN;
            e.Handled = true;
        }

        private void lblGTDSaldoActualME_SummaryReset(object sender, EventArgs e)
        {
            SaldoME = 0;
        }

        private void lblGTDSaldoActualME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 2)
                SaldoME = SaldoME + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_saldo"));
        }

        private void lblGTDSaldoActualME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SaldoME;
            e.Handled = true;
        }

        private void lblGTDMontoPagadoMN_SummaryReset(object sender, EventArgs e)
        {
            PagadoMN = 0;

        }

        private void lblGTDMontoPagadoMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PagadoMN;
            e.Handled = true;


        }

        private void lblGTDMontoPagadoMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 1)
                PagadoMN = PagadoMN + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_pagado"));
        }

        private void lblGTDMontoPagadoME_SummaryReset(object sender, EventArgs e)
        {
            PagadoME = 0;
        }

        private void lblGTDMontoPagadoME_SummaryRowChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 2)
                PagadoME = PagadoME + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_pagado"));
        }

        private void lblGTDMontoPagadoME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PagadoME;
            e.Handled = true;
        }

        private void lblGTDPrecioVentaMN_SummaryReset(object sender, EventArgs e)
        {
            PrecioMN = 0;
        }

        private void lblGTDPrecioVentaMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 1)
                PrecioMN = PrecioMN + Convert.ToDecimal(GetCurrentColumnValue("Valorcompra"));
        }

        private void lblGTDPrecioVentaMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PrecioMN;
            e.Handled = true;
        }

        private void lblGTDPrecioVentaME_SummaryReset(object sender, EventArgs e)
        {
            PrecioME = 0;
        }

        private void lblGTDPrecioVentaME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 2)
                PrecioME = PrecioME + Convert.ToDecimal(GetCurrentColumnValue("Valorcompra"));
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

        private void lblGGSaldoActualMN_SummaryReset(object sender, EventArgs e)
        {
            SaldoGMN = 0;
        }

        private void lblGGSaldoActualMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 1)
                SaldoGMN = SaldoGMN + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_saldo"));
        }

        private void lblGGSaldoActualMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SaldoGMN;
            e.Handled = true;
        }

        private void lblGGSaldoActualME_SummaryReset(object sender, EventArgs e)
        {
            SaldoGME = 0;
        }

        private void lblGGSaldoActualME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 2)
                SaldoGME = SaldoGME + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_saldo"));
        }

        private void lblGGSaldoActualME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SaldoGME;
            e.Handled = true;
        }

        private void lblGGMontoPagadoMN_SummaryReset(object sender, EventArgs e)
        {
            PagadoGMN = 0;
        }

        private void lblGGMontoPagadoMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 1)
                PagadoGMN = PagadoGMN + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_pagado"));
        }

        private void lblGGMontoPagadoMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PagadoGMN;
            e.Handled = true;
        }

        private void lblGGMontoPagadoME_SummaryReset(object sender, EventArgs e)
        {
            PagadoGME = 0;
        }

        private void lblGGMontoPagadoME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 2)
                PagadoGME = PagadoGME + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_pagado"));
        }

        private void lblGGMontoPagadoME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PagadoGME;
            e.Handled = true;
        }

        private void lblGGPrecioVentaMN_SummaryReset(object sender, EventArgs e)
        {
            PrecioGMN = 0;
        }

        private void lblGGPrecioVentaMN_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 1)
                PrecioGMN = PrecioGMN + Convert.ToDecimal(GetCurrentColumnValue("Valorcompra"));
        }

        private void lblGGPrecioVentaMN_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PrecioGMN;
            e.Handled = true;
        }

        private void lblGGPrecioVentaME_SummaryReset(object sender, EventArgs e)
        {
            PrecioGME = 0;
        }

        private void lblGGPrecioVentaME_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 2)
                PrecioGME = PrecioGME + Convert.ToDecimal(GetCurrentColumnValue("Valorcompra"));
        }

        private void lblGGPrecioVentaME_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PrecioGME;
            e.Handled = true;
        }





    }
}
