using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class rptCuotasFAV : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCuotasFAV()
        {
            InitializeComponent();
        }

        public void cargar(List<ECuotasFactura> lista)
        {
            this.DataSource = lista;
            lblFechaPago.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "fcc_sfecha_pago", "{0:dd/MM/yyyy}") });
            lblIdCuota.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "fcc_inro_cuota", "") });
            lblMonto.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "fcc_nmonto", "{0:n2}") });
        }

    }
}
