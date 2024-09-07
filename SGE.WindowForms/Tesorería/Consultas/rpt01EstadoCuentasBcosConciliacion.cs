using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Tesorería.Consultas
{
    public partial class rpt01EstadoCuentasBcosConciliacion : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt01EstadoCuentasBcosConciliacion()
        {
            InitializeComponent();
        }
        public void carga(List<ELibroBancos> Lista, string mes, string banco, string cuenta, decimal saldo, decimal saldoLibro)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + Parametros.intEjercicio.ToString();
            lblModulo.Text = "TESORERIA";
            lblTitulo1.Text = "CONCILIACIÓN BANCARIA - " + mes.ToUpper() + " DE " + Parametros.intEjercicio.ToString();
            lblTitulo2.Text = banco.ToUpper() + " - " + cuenta;
            //
            this.DataSource = Lista;
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("cflag_tipo_movimiento") });

            lblgrupo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "descripcion_Libro_Bancos", "")});

            lblMontoGrupo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "mto_total", "{0:N2}")});
            #region Cab           
            lblMontoMesAnterior.Text = saldoLibro.ToString("N2");
            #endregion
            #region Det
            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "dfecha_movimiento", "{0:dd/MM/yyyy}")});
            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tdocc_vdescripcion", "")});
            lblBeneficiario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vdescripcion_beneficiario", "")});
            lblMonto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nmonto_movimiento", "{0:N2}")});
            #endregion
            lblSaldoBancos.Text = saldo.ToString("N2");
            this.ShowPreview();
        }
    }
}
