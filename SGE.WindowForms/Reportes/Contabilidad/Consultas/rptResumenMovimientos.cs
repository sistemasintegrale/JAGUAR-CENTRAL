using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Contabilidad.Consultas
{
    public partial class rptResumenMovimientos : DevExpress.XtraReports.UI.XtraReport
    {
        public rptResumenMovimientos()
        {
            InitializeComponent();
        }
        public void Cargar(List<EVoucherContableDet> Lista, string Mes, string CuentaI, string CuentaF, string codigo)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio;
            lblModulo.Text = "CONTABILIDAD";
            lblTitulo1.Text = "MAYOR AUXILIAR ACUMULADO - A " + Mes.ToUpper() + " DE " + Parametros.intEjercicio;
            lblTitulo2.Text = "CUENTAS : DE " + CuentaI + " A " + CuentaF;
            lblRptCodigo.Text = codigo;
            this.DataSource = Lista;
            #region Detail
            lblCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strNroCuenta", "")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesCuenta", "")});
            lblMes.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "id_mes", "{0:00}")});

            lblVdebe.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_vent_nmto_tot_debe_sol", "{0:N2}")});
            lblVhaber.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_vent_nmto_tot_haber_sol", "{0:N2}")});
            lblVsaldo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_vent_nmto_saldo", "{0:N2}")});


            lblCdebe.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_comp_nmto_tot_debe_sol", "{0:N2}")});
            lblChaber.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_comp_nmto_tot_haber_sol", "{0:N2}")});
            lblCsaldo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_comp_nmto_saldo", "{0:N2}")});

            lblBdebe.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_banc_nmto_tot_debe_sol", "{0:N2}")});
            lblBhaber.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_banc_nmto_tot_haber_sol", "{0:N2}")});
            lblBsaldo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_banc_nmto_saldo", "{0:N2}")});
            #endregion
            #region Footer
            lblVdebeTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_vent_nmto_tot_debe_sol", "{0:N2}")});
            lblVhaberTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_vent_nmto_tot_haber_sol", "{0:N2}")});
            lblVsaldoTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_vent_nmto_saldo", "{0:N2}")});


            lblCdebeTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_comp_nmto_tot_debe_sol", "{0:N2}")});
            lblChaberTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_comp_nmto_tot_haber_sol", "{0:N2}")});
            lblCsaldoTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_comp_nmto_saldo", "{0:N2}")});

            lblBdebeTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_banc_nmto_tot_debe_sol", "{0:N2}")});
            lblBhaberTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_banc_nmto_tot_haber_sol", "{0:N2}")});
            lblBsaldoTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "subdi_banc_nmto_saldo", "{0:N2}")});
            #endregion
            this.ShowPreview();
        }
    }
}
