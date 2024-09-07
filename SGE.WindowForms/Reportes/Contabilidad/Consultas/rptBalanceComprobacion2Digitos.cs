using DevExpress.XtraReports.UI;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Contabilidad.Consultas
{
    public partial class rptBalanceComprobacion2Digitos : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBalanceComprobacion2Digitos()
        {
            InitializeComponent();
        }
        public void cargar(List<EVoucherContableDet> Lista, string strEjercicio, string strMes, string strDigitos, string strMoneda, string codigo)
        {

            xrlblTitulo.Text = (codigo == "<RFRM13>") ? String.Format("BALANCE DE COMPROBACIÓN EN {0} - AL DIA {1}", strMoneda, strMes.ToUpper()) :
                String.Format("BALANCE DE COMPROBACIÓN EN {0} - AL MES DE {1} DE {2}", strMoneda, strMes.ToUpper(), strEjercicio);
            xrlblTitulo2.Text = String.Format("RESUMEN A {0} DÍGITOS", strDigitos);
            xrlblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + strEjercicio;
            lblRptCodigo.Text = codigo;

            this.DataSource = Lista;
            #region Detail
            lblCtaPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strNroCuenta", "")});
            lblDescPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesCuenta", "")});

            lblSaldoAntPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_sol", "{0:N2}")});
            lblDebitosPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblCreditosPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblSaldoActPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_sol", "{0:N2}")});
            #endregion
            #region Footer
            lblSaldoAntTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_sol", "")});
            lblDebitosTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "")});
            lblCreditosTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "")});
            lblSaldoActTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_sol", "")});
            #endregion
            this.ShowPreview();
        }
    }
}
