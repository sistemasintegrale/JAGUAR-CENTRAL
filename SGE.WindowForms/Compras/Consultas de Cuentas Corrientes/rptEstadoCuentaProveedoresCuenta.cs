using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;


namespace SGE.WindowForms.Compras.Consultas_de_Cuentas_Corrientes
{
    public partial class rptEstadoCuentaProveedoresCuenta : DevExpress.XtraReports.UI.XtraReport
    {
        public rptEstadoCuentaProveedoresCuenta()
        {
            InitializeComponent();
        }
        public void cargar(List<EDocPorPagar> lista, string anio, string MES)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + anio;

            xrlblTitulo1.Text = "ESTADO DE CUENTAS PROVEEDORES POR CUENTA CONTABLE ";
            xrlblTitulo2.Text = "AL MES DE " + MES;


            this.DataSource = lista;

            GroupHeader2.GroupFields.AddRange(new GroupField[] {
            new GroupField("icod_cuenta_contable")});


            GroupHeader1.GroupFields.AddRange(new GroupField[] {
            new GroupField("proc_icod_proveedor")});


            //Detalles
            lblcuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "cuenta_contable", "")});

            lbldescripcionCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "vdescripcion_cuenta_contable", "")});

            lblcodigoProveedor.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "proc_vcod_proveedor", "")});

            lblnombreproveedor.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "proc_vnombrecompleto", "")});

            lblDocumentoCAB.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "Abreviatura", "")});

            lblFechacab.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "doxpc_sfecha_doc", "{0:dd/MM/yyyy}")});

            lblMontoTotalNac.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "TOTAL_SOLES", "{0:n}")});

            lblMontopagadoNac.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "PAGADO_SOLES", "{0:n}")});

            lblMontoActualNac.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "SALDO_SOLES", "{0:n}")});

            //enlaces al grupo 1
            lblMontoTotalExt.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "SALDO_DOLARES", "{0:n}")});

            lblMontoPagadoExt.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "PAGADO_DOLARES", "{0:n}")});

            lblSaldoActualExt.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "TOTAL_DOLARES", "{0:n}")});

            lblMONTOSALDOTOTALsoles.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "SALDO_SOLES", "{0:n}")});

            lblMONTOSALDOTOTALdolares.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "SALDO_DOLARES", "{0:n}")});




            this.ShowPreview();
        }
    }
}
