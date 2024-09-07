using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Contabilidad.Libros_Oficiales
{
    public partial class rptListaInventarioPermanenteConDetalle : DevExpress.XtraReports.UI.XtraReport
    {
        public rptListaInventarioPermanenteConDetalle()
        {
            InitializeComponent();
        }
        public void cargar(string strTitulo1, string strTitulo2, List<EKardex> lista, DateTime periodo)
        {
            string establecimiento;
            List<EPuntoVenta> lstpunto = new BCentral().ListarPuntoVenta();
            establecimiento = "ESTABLECIMIENTO " + " (" + lstpunto[0].puvec_vcodigo_sunat + ") " + lstpunto[0].puvec_vdireccion;
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblruc.Text = Valores.strRUC;
            lblPeriodo.Text = periodo.ToString("MMMM").ToUpper() + " " + periodo.Year;
            lblEstablecimiento.Text = establecimiento;
            lista.ForEach(x =>
            {
                x.strTabla5 = "(" + x.famic_vtipo_existencia + ") " + x.clasc_vdescripcion;
                x.tipoMov = x.kardc_tipo_movimiento == 1 ? "01" : "00";
                x.kardc_numero_doc = x.kardc_numero_doc.Replace("-", "");
                x.kard_numero_doc = x.kardc_numero_doc.Substring(4);
                x.kard_serie_doc = x.kardc_numero_doc.Substring(0, 4);
                x.strProducto = x.strCodProducto + " " + x.strProducto + " (" + x.kardc_codUnidMEd + ") " + x.unidc_vdescripcion;
                if (x.kard_numero_doc.Length != 12)
                {
                    x.kard_numero_doc = x.kard_numero_doc.PadLeft(8, '0');
                }
            });


            this.DataSource = lista;


            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("prdc_icod_producto") });

            lblExistencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista,"strProducto", "")});

            lblTipoMaterial.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strTabla5", "")});

            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "kardc_fecha_movimiento", "{0:dd/MM/yyyy}")});

            lblTipoDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "tdocc_coa", "")});

            lblSerie.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "kard_serie_doc", "")});

            lblNumero.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "kard_numero_doc", "")});

            lblTipoOperacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "tarec_vtipo_operacion", "")});

            lblIngreso.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblIngreso", "")});

            lblSalida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblSalida", "")});

            lblSaldoActual.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "dblSaldo", "")});

            this.ShowPreview();
        }
    }
}
