using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes
{
    public partial class rpt01PlanillaDetalle : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt01PlanillaDetalle()
        {
            InitializeComponent();
        }


        public void cargar(List<EPlanillaCobranzaDet> lista, string strNroPlanilla, string strFecha)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa + "- AÑO " + Parametros.intEjercicio;
            xrlblTitulo1.Text = String.Format("PLANILLA DE VENTA DIARIA N° {0}", strNroPlanilla);
            xrlblTitulo2.Text = strFecha;

            this.DataSource = lista;

            GroupHeader2.GroupFields.AddRange(new GroupField[] { new GroupField("tablc_iid_tipo_mov") });
            lblTipoMov.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strTipoMov", "")});


            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("tablc_iid_tipo_moneda") });
            lblMonedaGroup.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strMonedaGroup", "")});


            #region Detalle del Reporte


            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strTipoDoc", "")});

            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "plnd_sfecha_doc", "{0:dd/MM/yyyy}")});

            lblCliente.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strCliente", "")});

            lblMoneda.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strTipoMoneda", "")});

            lblTotalDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "plnd_nmonto", "{0:N2}")});

            lblEfectivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoEfectivo", "{0:N2}")});

            lblTarjeta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoTarjetaCredito", "{0:N2}")});

            lblCheque.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoCheque", "{0:N2}")});

            lblTransferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoTransferencia", "{0:N2}")});

            lblNotaCredito.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoNotaCredito", "{0:N2}")});

            lblAdelanto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoAnticipo", "{0:N2}")});

            lblTotalPago.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "plnd_nmonto_pagado", "{0:N2}")});

            lblCredito.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoCredito", "{0:N2}")});

            #endregion
            #region Pie del Grupo

            lblTotalMonto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "plnd_nmonto", "{0:N2}")});

            lblTotalEfectivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoEfectivo", "{0:N2}")});

            lblTotalTarjeta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoTarjetaCredito", "{0:N2}")});

            lblTotalCheque.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoCheque", "{0:N2}")});

            lblTotalTransf.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoTransferencia", "{0:N2}")});

            lblTotalNC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoNotaCredito", "{0:N2}")});

            lblTotalAdelanto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoAnticipo", "{0:N2}")});

            lblTotalPagos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "plnd_nmonto_pagado", "{0:N2}")});

            lblTotalCreditos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoCredito", "{0:N2}")});

            #endregion

            lblTotalGEfectivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoEfectivo", "{0:N2}")});

            lblTotalGTarjeta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoTarjetaCredito", "{0:N2}")});

            lblTotalGCheque.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoCheque", "{0:N2}")});

            lblTotalGTransferencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoTransferencia", "{0:N2}")});

            lblTotalGNC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoNotaCredito", "{0:N2}")});

            lblTotalGAdelanto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblPagoAnticipo", "{0:N2}")});

            lblTotalGPagado.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "plnd_nmonto_pagado", "{0:N2}")});
            //this.Print();
            this.ShowPreview();
        }
    }
}
