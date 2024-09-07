
using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Contabilidad.Libros_Oficiales
{
    public partial class rptConsultaLibroMayor : DevExpress.XtraReports.UI.XtraReport
    {
        public rptConsultaLibroMayor()
        {
            InitializeComponent();
        }
        public void Cargar(List<EVoucherContableDet> Lista, string Mes, string CuentaI, string CuentaF, decimal mtoTotalSaldoAnteriorSol,
            decimal mtoTotalSaldoAnteriorDol, decimal mtoTotalSaldoActualSol, decimal mtoTotalSaldoActualDol)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio;
            lblModulo.Text = "RUC: " + Modules.Valores.strRUC; ;
            lblTitulo1.Text = "LIBRO MAYOR - " + Mes.ToUpper() + " DE " + Parametros.intEjercicio.ToString();
            lblTitulo2.Text = "CUENTA: DE " + CuentaI + " A " + CuentaF;
            this.DataSource = Lista;

            GroupHeader2.GroupFields.AddRange(new GroupField[] { new GroupField("viid_cuenta_contable") });
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("anac_cecoc_code") });
            #region Header 01
            lblCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "viid_cuenta_contable", "")});
            lblCuentaDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vdescripcion_cuenta_contable", "")});

            lblSaldoAntMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cuenta_iid_cuenta_contable_acumulado_sol", "{0:N2}")});
            lblDebeMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nmto_tot_debe_sol", "{0:N2}")});
            lblHaberMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nmto_tot_haber_sol", "{0:N2}")});
            lblSaldoActMN_H.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cuenta_iid_cuenta_contable_saldo_sol", "{0:N2}")});

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
            new XRBinding("Text", Lista, "nmto_tot_debe_sol", "{0:N2}")});
            lblHaberMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nmto_tot_haber_sol", "{0:N2}")});
            lblSaldoActMN.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ctacc_iid_cuenta_contable_saldo_sol", "{0:N2}")});

            #endregion
            #region Detail

            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vnumero_documento", "")});
            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "fec_cab", "{0:dd/MM/yyyy}")});
            lblGlosa.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vglosa_linea", "")});
            lblMoneda.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mon_cab", "")});
            lblTipoCambio.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tipocambio", "")});


            lblDebeMN_D.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nmto_tot_debe_sol", "{0:N2}")});
            lblHaberMN_D.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nmto_tot_haber_sol", "{0:N2}")});

            lblNumCorre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "iid_subdiario_vnum_voucher", "")});
            lblNumCorre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_num_corre", "")});
            #endregion
            #region Footer
            lblSaldoAntMN_Total.Text = mtoTotalSaldoAnteriorSol.ToString("N2");
            lblSaldoActMN_Total.Text = mtoTotalSaldoActualSol.ToString("N2");
            //lblSaldoAntMN_Total.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "mtoTotalSaldoAnteriorSol", "{0:N2}")});
            lblDebeMN_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nmto_tot_debe_sol", "{0:N2}")});
            lblHaberMN_Total.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nmto_tot_haber_sol", "{0:N2}")});
            //lblSaldoActMN_Total.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "mtoTotalSaldoActualSol", "{0:N2}")});

            //lblSaldoAntME_Total.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "mtoTotalSaldoAnteriorDol", "{0:N2}")});
            //lblSaldoActME_Total.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "mtoTotalSaldoActualDol", "{0:N2}")});
            #endregion
            this.ShowPreview();
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            String tipoAnacCecoc;
            try
            {
                tipoAnacCecoc = GetCurrentColumnValue("anac_cecoc_tipo").ToString();
            }
            catch (Exception ex)
            {
                tipoAnacCecoc = null;
            }
            if ((Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_debe_sol")) +
               Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_haber_sol")) +
               Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_debe_dol")) +
               Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_haber_dol")) == 0)
                || String.IsNullOrWhiteSpace(tipoAnacCecoc))
            {
                e.Cancel = true;
            }
            else
                e.Cancel = false;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_debe_sol")) +
              Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_haber_sol")) +
              Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_debe_dol")) +
              Convert.ToDecimal(GetCurrentColumnValue("nmto_tot_haber_dol")) == 0)
            {
                e.Cancel = true;
            }
            else
                e.Cancel = false;
        }
    }
}
