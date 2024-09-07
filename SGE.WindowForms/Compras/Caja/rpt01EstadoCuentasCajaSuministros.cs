using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Compras.Caja
{
    public partial class rpt01EstadoCuentasCajaSuministros : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt01EstadoCuentasCajaSuministros()
        {
            InitializeComponent();
        }
        public void carga(List<ELibroBancos> Lista, string mes, string banco, string cuenta)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "CAJA - SUMINISTROS";
            lblTitulo1.Text = "ESTADO DE CUENTAS - " + mes.ToUpper() + " DE " + Parametros.intEjercicio.ToString();
            lblTitulo2.Text = banco.ToUpper() + " - " + cuenta;

            this.DataSource = Lista;


            #region Detalle
            lblDia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Dia", "{0:00}")});
            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tdocc_vdescripcion", "")});

            lblNroDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vnro_documento", "")});

            lblBeneficia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vdescripcion_beneficiario", "")});
            lblGlosa.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vglosa", "")});
            lblAbonoDet.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Abono", "{0:N2}")});
            lblCargoDet.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Cargo", "{0:N2}")});
            lblSaldoDet.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "SaldoLibro", "{0:N2}")});
            #endregion
            #region Cabecera

            lblAbonoCab.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mobac_nmonto_abono_ant", "{0:N2}")});
            lblCargoCab.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mobac_nmonto_cargo_ant", "{0:N2}")});
            lblSaldoCab.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "SaldoAnterior", "{0:N2}")});
            #endregion
            #region Footer                       
            lblMovimientos.Text = Lista.Count.ToString();
            getTotal(Convert.ToDecimal(Lista.Sum(x => x.Abono)), Convert.ToDecimal(Lista.Sum(z => z.Cargo)),
                Convert.ToDecimal(Lista[0].mobac_nmonto_abono_ant), Convert.ToDecimal(Lista[0].mobac_nmonto_cargo_ant));
            #endregion
            this.ShowPreview();

        }
        private void getTotal(decimal totalAbono, decimal totalCargo, decimal abonoAnt, decimal cargoAnt)
        {
            lblAbonoFoot.Text = (abonoAnt + totalAbono).ToString("N2");
            lblCargoFoot.Text = (cargoAnt + totalCargo).ToString("N2");
        }

    }
}
