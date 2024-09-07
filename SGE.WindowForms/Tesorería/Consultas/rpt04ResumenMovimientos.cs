using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Tesorería.Consultas
{
    public partial class rpt04ResumenMovimientos : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt04ResumenMovimientos()
        {
            InitializeComponent();
        }
        public void carga(List<EEntidadFinancieraCuenta> Lista, string FechaI, string FechaF)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "TESORERIA";
            lblTitulo1.Text = "RESUMEN DE MOVIMIENTOS DE BANCOS";
            lblTitulo2.Text = "DEL " + FechaI + " AL " + FechaF;

            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("iid_tipo_moneda") });
            lblCuentaMoneda.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "CuentasMoneda", "")});

            this.DataSource = Lista;
            #region Detalle Grupo
            lblBanco.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vdes_entidad_financiera", "")});
            lblTipo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dfecha_movimiento", "")});
            lblCuenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Descripcion", "")});
            lblSaldoAnterior.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mto_saldo_anterior", "{0:N2}")});
            lblAbonos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mto_abono_acumulado", "{0:N2}")});
            lblCargos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mto_cargo_acumulado", "{0:N2}")});
            lblSaldoLibros.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mto_saldo_libro", "{0:N2}")});
            lblDisponible.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mto_saldo_disponible", "{0:N2}")});
            #endregion
            #region Footer Grupo

            lblTotalSaldoAnt.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mto_saldo_anterior", "")});
            lblTotalAbonos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mto_abono_acumulado", "")});
            lblTotalCargos.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mto_cargo_acumulado", "")});
            lblTotalSaldoLibro.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mto_saldo_libro", "")});
            lblTotalDisponible.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mto_saldo_disponible", "")});
            #endregion
            this.ShowPreview();
        }

    }
}
