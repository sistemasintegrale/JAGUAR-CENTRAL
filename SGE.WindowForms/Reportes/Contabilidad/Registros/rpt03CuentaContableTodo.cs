using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Reportes.Contabilidad.Registros
{
    public partial class rpt03CuentaContableTodo : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt03CuentaContableTodo()
        {
            InitializeComponent();
        }
        public void cargar(List<ECuentaContable> lista)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio;
            lblModulo.Text = "CONTABILIDAD";
            lblTitulo1.Text = "RELACIÓN DE PLAN DE CUENTAS";
            //string des = (desde.ToString() == "0") ? "00" : desde.ToString();
            //lblTitulo2.Text = "DESDE " + des + " HASTA " + hasta.ToString();
            lista.GroupBy(x => x.ctacc_);
            this.DataSource = lista;

            lblCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ctacc_numero_cuenta_contable", "")});

            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ctacc_nombre_descripcion", "")});

            lblAnal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "tablc_iid_tipo_analitica", "{0:00}")});

            lblCC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "", "")});

            lblDebe.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ctacc_icod_cuenta_debe_auto", "")});

            lblHaber.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ctacc_icod_cuenta_haber_auto", "")});

            lblMon.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "", "")});

            lblTit.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "strTipoCuenta", "")});

            lblNivel.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", lista, "ctacc_nivel_cuenta", "")});

            this.ShowPreview();
        }

        private void lblCC_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((Boolean)GetCurrentColumnValue("ctacc_iccosto"))
                ((XRControl)sender).Text = "SI";
            else
                ((XRControl)sender).Text = "NO";
        }

        private void xrLabel11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("tablc_iid_tipo_moneda").ToString() == "1")
                ((XRControl)sender).Text = "S/.";
            else if (GetCurrentColumnValue("tablc_iid_tipo_moneda").ToString() == "2")
                ((XRControl)sender).Text = "US$";
            else if (GetCurrentColumnValue("tablc_iid_tipo_moneda").ToString() == "3")
                ((XRControl)sender).Text = "HIS";
        }
    }
}
