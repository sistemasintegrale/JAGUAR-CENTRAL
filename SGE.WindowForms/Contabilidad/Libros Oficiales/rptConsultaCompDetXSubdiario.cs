using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Contabilidad.Libros_Oficiales
{
    public partial class rptConsultaCompDetXSubdiario : DevExpress.XtraReports.UI.XtraReport
    {
        List<StructSubdiario> lstSubdiario = new List<StructSubdiario>();
        int cont = 0;

        public rptConsultaCompDetXSubdiario()
        {
            InitializeComponent();
        }

        public void Cargar(List<EComprobanteDetalle> Lista, List<EComprobante> Lista2, string Mes)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblRuc.Text = "RUC: " + Modules.Valores.strRUC;
            lblTitulo1.Text = "LIBRO DIARIO - " + Mes.ToUpper() + " DE " + Parametros.intEjercicio.ToString();
            this.DataSource = Lista.OrderBy(obe => obe.id_subdiario).ThenBy(obe => obe.iid_subdiario_vnum_voucher).ToList();

            foreach (var item in Lista2.Select(obe => new { obe.idf_SubDiario, obe.subdiario_des }).Distinct().OrderBy(obe => obe.idf_SubDiario).ToList())
            {
                lstSubdiario.Add(new StructSubdiario() { numSubdiario = String.Format("{0:00}", item.idf_SubDiario), descSubdiario = item.subdiario_des });
            }

            lblTitulo2.Text = "SUBDIARIO : [" + lstSubdiario[cont].numSubdiario + "] " + lstSubdiario[cont].descSubdiario;

            GroupHeader2.GroupFields.AddRange(new GroupField[] { new GroupField("vid_subdiario") });
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("iid_voucher_contable") });

            #region GroupHeader

            lblSubdiario.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "iid_subdiario_vnum_voucher")});
            lblComprobante.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "iid_voucher_contable")});

            #endregion

            #region Detail

            lblCorrelativo.DataBindings.Add(new XRBinding("Text", null, "iid_subdiario_vnum_voucher"));
            lblFecha.DataBindings.Add(new XRBinding("Text", null, "fec_cab", "{0:dd/MM/yyyy}"));
            lblGlosa.DataBindings.Add(new XRBinding("Text", null, "vglosa_linea"));
            lblCuenta.DataBindings.Add(new XRBinding("Text", null, "viid_cuenta_contable"));
            lblDenominacion.DataBindings.Add(new XRBinding("Text", null, "ctacc_vnombre_descripcion_larga"));
            lblDebe.DataBindings.Add(new XRBinding("Text", null, "nmto_tot_debe_sol", "{0:n2}"));
            lblHaber.DataBindings.Add(new XRBinding("Text", null, "nmto_tot_haber_sol", "{0:n2}"));

            #endregion

            #region GroupFooter
            lblNumSubdiario.DataBindings.Add(new XRBinding("Text", null, "vid_subdiario"));
            lblDebeT.DataBindings.Add(new XRBinding("Text", null, "nmto_tot_debe_sol"));
            lblHaberT.DataBindings.Add(new XRBinding("Text", null, "nmto_tot_haber_sol"));
            #endregion

            #region ReportFooter
            lblDebeTT.DataBindings.Add(new XRBinding("Text", null, "nmto_tot_debe_sol"));
            lblHaberTT.DataBindings.Add(new XRBinding("Text", null, "nmto_tot_haber_sol"));
            #endregion

            this.ShowPreview();
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            if (cont < lstSubdiario.Count - 1)
            {
                GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
                lblTitulo2.Text = "SUBDIARIO : [" + lstSubdiario[cont + 1].numSubdiario + "] " + lstSubdiario[cont + 1].descSubdiario;
                cont++;
            }
            else
                GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.None;
        }

        struct StructSubdiario
        {
            public string numSubdiario { get; set; }
            public string descSubdiario { get; set; }
        }
    }
}
