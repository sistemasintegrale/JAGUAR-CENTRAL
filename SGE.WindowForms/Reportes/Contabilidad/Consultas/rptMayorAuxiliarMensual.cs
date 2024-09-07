using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Contabilidad.Consultas
{
    public partial class rptMayorAuxiliarMensual : DevExpress.XtraReports.UI.XtraReport
    {
        public rptMayorAuxiliarMensual()
        {
            InitializeComponent();
        }
        public void cargar(List<EVoucherContableDet> Lista, string Mes, string CuentaI, string CuentaF, string codigo, string type)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio;
            lblModulo.Text = "CONTABILIDAD";
            lblTitulo1.Text = String.Format("MAYOR AUXILIAR {0} - {1} DE {2}", type, Mes.ToUpper(), Parametros.intEjercicio.ToString());
            lblTitulo2.Text = "CUENTAS: DE " + CuentaI + " A " + CuentaF;
            lblRptCodigo.Text = codigo;
            this.DataSource = Lista;
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("strNroCuenta") });
            #region Header
            lblCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strNroCuenta", "")});
            lblDesCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesCuenta", "")});

            lblSaldoAntMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_sol", "{0:N2}")});
            lblDebeMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblHaberMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblSaldoActMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_sol", "{0:N2}")});

            lblSaldoAntME_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_dol", "{0:N2}")});
            lblDebeME_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_dol", "{0:N2}")});
            lblHaberME_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_dol", "{0:N2}")});
            lblSaldoActME_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_dol", "{0:N2}")});
            #endregion
            #region Detail

            lblTipoAnaCos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "anac_cecoc_tipo", "")});
            lblCodAnaCos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "anac_cecoc_code", "")});
            lblDesAnaCos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "anac_cecoc_vdescripcion", "")});


            lblSaldoAntMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_sol", "{0:N2}")});
            lblDebeMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblHaberMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblSaldoActMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_sol", "{0:N2}")});

            lblSaldoAntME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_dol", "{0:N2}")});
            lblDebeME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_dol", "{0:N2}")});
            lblHaberME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_dol", "{0:N2}")});
            lblSaldoActME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_dol", "{0:N2}")});
            #endregion
            #region Footer

            lblSaldoAntMN_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_sol", "{0:N2}")});
            lblDebeMN_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblHaberMN_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblSaldoActMN_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_sol", "{0:N2}")});

            lblSaldoAntME_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_acumulado_dol", "{0:N2}")});
            lblDebeME_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_dol", "{0:N2}")});
            lblHaberME_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_dol", "{0:N2}")});
            lblSaldoActME_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_dol", "{0:N2}")});
            #endregion

            this.ShowPreview();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_debe_sol")) +
            //    Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_haber_sol")) +
            //    Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_debe_dol")) +
            //    Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_haber_dol")) == 0)
            //{
            //    e.Cancel = true;
            //}
            //else
            //    e.Cancel = false;
        }

    }
}
