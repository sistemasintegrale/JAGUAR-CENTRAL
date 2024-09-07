using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Compras.Consultas_de_Compras
{
    public partial class rptOrdenCompraConsultar : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrdenCompraConsultar()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, Boolean Generado, Boolean FacturadoParcial, Boolean FacturadoTotal, List<EOrdenCompra> lista)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblModulo.Text = "COMPRAS";
            lblTitulo1.Text = strTitulo1;
            lblTitulo2.Text = strTitulo2;
            chkGenerado.Checked = Generado;
            chkFacturadoParcial.Checked = FacturadoParcial;
            chkFacturadoTotal.Checked = FacturadoTotal;
            this.DataSource = lista;

            lblNumero.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ococ_numero_orden_compra", "")});

            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ococ_sfecha_orden_compra", "{0:dd/MM/yyyy}")});

            lblProveedor.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "proc_vnombrecompleto", "")});

            lblMoneda.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strMoneda", "")});

            lblSituacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "str_situacion", "")});

            lblNeto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ococ_nmonto_neto", "{0:N2}")});

            lblIGV.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ococ_nmonto_imp", "{0:N2}")});

            lblTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ococ_nmonto_total", "{0:N2}")});

            this.ShowPreview();

        }
    }
}