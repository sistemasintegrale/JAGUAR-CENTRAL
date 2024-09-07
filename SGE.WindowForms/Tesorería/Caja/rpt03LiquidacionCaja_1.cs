using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Tesorería.Caja
{
    public partial class rpt03LiquidacionCaja_1 : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt03LiquidacionCaja_1()
        {
            InitializeComponent();
        }
        public void carga(ELiquidacionCaja Obe, EVoucherContableCab ObeComp, List<EVoucherContableDet> Lista, string Mes)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "TESORERIA";
            lblTitulo1.Text = "LIQUIDACIÓN DE CAJA CHICA - " + Mes.ToUpper() + " " + Parametros.intEjercicio.ToString();
            string comprobante = string.Format("{0:00}", ObeComp.subdi_icod_subdiario) + "." + ObeComp.vcocc_numero_vcontable;
            lblTitulo2.Text = lblTitulo2.Text = "CORRELATIVO [" + String.Format("{0:000}", Obe.lqcc_inro_liquid_caja) + "] ";// + "COMPROBANTE N° " + "[" + comprobante + "]";
            this.DataSource = Lista;

            /*------------------------------------------------------------------------------------*/
            lblCaja.Text = Obe.caja_nro + " " + Obe.caja_decripcion;
            lblLiquidacion.Text = String.Format("{0:000}", Obe.lqcc_inro_liquid_caja);
            lblConcepto.Text = Obe.lqcc_vconcepto;
            /*------------------------------------------------------------------------------------*/
            string moneda, moneda2, asterisks;
            moneda = (Obe.lqcc_iid_tipo_moneda == 3) ? "S/." : "US$";
            moneda2 = (Obe.lqcc_iid_tipo_moneda == 3) ? "M.N." : "M.E.";
            asterisks = "************".Substring(0, 12 - Obe.lqcc_nmonto_total.ToString().Length);

            lblTotal.Text = moneda + " " + asterisks + Obe.lqcc_nmonto_total.ToString("N2");
            lblMoneda.Text = moneda2;
            lblFecha.Text = Obe.lqcc_sfecha_liquid.ToShortDateString();
            lblTipoCambio.Text = Obe.lqcc_ntipo_cambio.ToString();

            /*------------------------------------------------------------------------------------*/

            lblSecuencia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nro_item_det", "{0:000}")});
            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strTipNroDocumento", "")});//////---------//////
            lblCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strNroCuenta", "")});
            lblDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesCuenta", "")});

            lblAnalisis.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strAnalisis", "")});
            lblCCosto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strCodCCosto", "")});
            lblDebeMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblHaberMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblDebeME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_dol", "{0:N2}")});
            lblHaberME.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_dol", "{0:N2}")});

            lblDebeMNT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "")});
            lblHaberMNT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "")});
            lblDebeMET.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_dol", "")});
            lblHaberMET.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_dol", "")});

            this.ShowPreview();

        }

    }
}
