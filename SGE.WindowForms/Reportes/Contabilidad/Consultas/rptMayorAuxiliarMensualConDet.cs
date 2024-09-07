
using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Contabilidad.Consultas
{
    public partial class rptMayorAuxiliarMensualConDet : DevExpress.XtraReports.UI.XtraReport
    {
        public rptMayorAuxiliarMensualConDet()
        {
            InitializeComponent();
        }
        public void Cargar(List<EVoucherContableDet> Lista, string Mes, string CuentaI,
            string CuentaF, string Titulo, string type, string codigo, decimal mtoTotalSaldoAnteriorSol,
            decimal mtoTotalSaldoAnteriorDol, decimal mtoTotalSaldoActualSol, decimal mtoTotalSaldoActualDol)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio;
            lblModulo.Text = "CONTABILIDAD";
            lblTitulo1.Text = String.Format("MAYOR AUXILIAR {0} - {1} DE {2} ", type, Mes.ToUpper(), Parametros.intEjercicio.ToString());
            lblTitulo2.Text = "CUENTA: DE " + CuentaI + " A " + CuentaF;
            lblRptCodigo.Text = codigo;
            this.DataSource = Lista;

            GroupHeader2.GroupFields.AddRange(new GroupField[] { new GroupField("strNroCuenta") });
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("anac_cecoc_code") });
            #region Header 01
            lblCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strNroCuenta", "")});
            lblCuentaDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesCuenta", "")});

            lblSaldoAntMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cuenta_iid_cuenta_contable_acumulado_sol", "{0:N2}")});
            lblDebeMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblHaberMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblSaldoActMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cuenta_iid_cuenta_contable_saldo_sol", "{0:N2}")});

            lblSaldoAntME_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cuenta_iid_cuenta_contable_acumulado_dol", "{0:N2}")});
            lblDebeME_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_dol", "{0:N2}")});
            lblHaberME_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_dol", "{0:N2}")});
            lblSaldoActME_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cuenta_iid_cuenta_contable_saldo_dol", "{0:N2}")});
            #endregion
            #region Header 02

            lblTipoAnaCos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "anac_cecoc_tipo", "")});
            lblCodAnaCos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "anac_cecoc_code", "")});
            lblDesAnaCos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "anac_cecoc_vdescripcion", "")});
            //           

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
            #region Detail

            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_numero_doc", "")});
            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "fec_cab", "{0:dd/MM/yyyy}")});
            lblGlosa.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_vglosa_linea", "")});
            lblMoneda.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strMonedaVContable", "")});
            lblTipoCambio.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_tipo_cambio", "")});


            lblDebeMN_D.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblHaberMN_D.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblDebeME_D.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_dol", "{0:N2}")});
            lblHaberME_D.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_dol", "{0:N2}")});

            lblComprobante.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "iid_subdiario_vnum_voucher", "")});
            lblSec.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nro_item_det", "{0:000}")});
            #endregion
            #region Footer
            lblSaldoAntMN_Total.Text = mtoTotalSaldoAnteriorSol.ToString("N2");
            lblSaldoActMN_Total.Text = mtoTotalSaldoActualSol.ToString("N2");
            lblSaldoAntME_Total.Text = mtoTotalSaldoAnteriorDol.ToString("N2");
            lblSaldoActME_Total.Text = mtoTotalSaldoActualDol.ToString("N2");
            //lblSaldoAntMN_Total.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "mtoTotalSaldoAnteriorSol", "{0:N2}")});
            lblDebeMN_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblHaberMN_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            //lblSaldoActMN_Total.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "mtoTotalSaldoActualSol", "{0:N2}")});

            //lblSaldoAntME_Total.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "mtoTotalSaldoAnteriorDol", "{0:N2}")});
            lblDebeME_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_dol", "{0:N2}")});
            lblHaberME_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_dol", "{0:N2}")});
            //lblSaldoActME_Total.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "mtoTotalSaldoActualDol", "{0:N2}")});
            #endregion
            this.ShowPreview();
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_debe_sol")) +
            //   Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_haber_sol")) +
            //   Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_debe_dol")) +
            //   Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_haber_dol")) == 0)
            //{
            //    e.Cancel = true;
            //}
            //else
            //    e.Cancel = false;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDecimal(GetCurrentColumnValue("vcocd_nmto_tot_debe_sol")) +
              Convert.ToDecimal(GetCurrentColumnValue("vcocd_nmto_tot_haber_sol")) +
              Convert.ToDecimal(GetCurrentColumnValue("vcocd_nmto_tot_debe_dol")) +
              Convert.ToDecimal(GetCurrentColumnValue("vcocd_nmto_tot_haber_dol")) == 0)
            {
                e.Cancel = true;
            }
            else
                e.Cancel = false;
        }
    }
}
