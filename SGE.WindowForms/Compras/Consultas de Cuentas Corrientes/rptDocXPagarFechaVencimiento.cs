using DevExpress.XtraReports.UI;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Compras.Consultas_de_Cuentas_Corrientes
{
    public partial class rptDocXPagarFechaVencimiento : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDocXPagarFechaVencimiento()
        {
            InitializeComponent();
        }
        public void cargar(List<EDocPorPagar> lista, string anio, string SfechaInicio, string sfechaFinal)
        {

            xrlblTitulo1.Text = "DOCUMENTOS PENDIENTES DE PAGO " + anio;
            xrlblTitulo2.Text = "CON FECHA DE VENCIMIENTO DEL " + SfechaInicio + " AL " + sfechaFinal;


            this.DataSource = lista;



            //Enlace al grupo Cliente
            lblsfechaVen.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_sfecha_vencimiento_doc", "{0:dd/MM/yyyy}")});

            //Detalles
            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "Abreviatura", "")});

            lblnumero.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_vnumero_doc", "")});


            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_sfecha_doc", "{0:dd/MM/yyyy}")});

            lblproveedor.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "proc_vnombrecompleto", "")});

            lblMon.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "SimboloMoneda", "")});

            lblPrecioCompra.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_documento", "{0:n}")});

            lblMontoPagado.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}")});

            lblSaldoActual.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}")});


            lblPreCompraSOLES.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_documento", "{0:n}")});

            lblMontoPagadoSOLES.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}")});

            lblSaldoActualSOLES.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}")});

            lblPreCompraDolares.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_documento", "{0:n}")});

            lblMontoPagadodolares.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}")});

            lblSaldoActualdolares.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}")});

            this.ShowPreview();
        }
        decimal SaldoMN = 0;
        decimal SaldoME = 0;
        decimal PagadoMN = 0;
        decimal PagadoME = 0;
        decimal PrecioMN = 0;
        decimal PrecioME = 0;

        private void lblPreCompraSOLES_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PrecioMN;
            e.Handled = true;
        }

        private void lblPreCompraSOLES_SummaryReset(object sender, EventArgs e)
        {
            PrecioMN = 0;

        }

        private void lblPreCompraSOLES_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 1)
                PrecioMN = PrecioMN + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_documento"));

        }




        private void lblMontoPagadoSOLES_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PagadoMN;
            e.Handled = true;
        }

        private void lblMontoPagadoSOLES_SummaryReset(object sender, EventArgs e)
        {
            PagadoMN = 0;
        }

        private void lblMontoPagadoSOLES_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 1)
                PagadoMN = PagadoMN + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_pagado"));
        }

        private void lblSaldoActualSOLES_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SaldoMN;
            e.Handled = true;
        }

        private void lblSaldoActualSOLES_SummaryReset(object sender, EventArgs e)
        {
            SaldoMN = 0;
        }

        private void lblSaldoActualSOLES_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 1)
                SaldoMN = SaldoMN + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_saldo"));
        }

        private void lblPreCompraDolares_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PrecioME;
            e.Handled = true;
        }

        private void lblPreCompraDolares_SummaryReset(object sender, EventArgs e)
        {
            PrecioME = 0;
        }

        private void lblPreCompraDolares_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 2)
                PrecioME = PrecioME + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_documento"));
        }

        private void lblMontoPagadodolares_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = PagadoME;
            e.Handled = true;
        }

        private void lblMontoPagadodolares_SummaryReset(object sender, EventArgs e)
        {
            PagadoME = 0;
        }

        private void lblMontoPagadodolares_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 2)
                PagadoME = PagadoME + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_pagado"));
        }

        private void lblSaldoActualdolares_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = SaldoME;
            e.Handled = true;
        }

        private void lblSaldoActualdolares_SummaryReset(object sender, EventArgs e)
        {
            SaldoME = 0;
        }

        private void lblSaldoActualdolares_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_iid_tipo_moneda")) == 2)
                SaldoME = SaldoME + Convert.ToDecimal(GetCurrentColumnValue("doxpc_nmonto_total_saldo"));
        }


    }
}
