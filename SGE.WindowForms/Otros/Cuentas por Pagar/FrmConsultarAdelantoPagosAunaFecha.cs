using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmConsultarAdelantoPagosAunaFecha : DevExpress.XtraEditors.XtraForm
    {

        List<EDocxPagarPagoAdelanto> mlistAdePagos = new List<EDocxPagarPagoAdelanto>();
        public long doxcc_icod_correlativo_adelanto;
        public EDocPorPagar eDocXPagar;
        public DateTime sfecha;


        public FrmConsultarAdelantoPagosAunaFecha()
        {
            InitializeComponent();
        }

        private void FrmConsultarAdelantoPagosAunaFecha_Load(object sender, EventArgs e)
        {
            this.cargar();
        }
        private void cargar()
        {
            this.Text = "PAGOS EFECTUADOS POR " + eDocXPagar.Abreviatura + " - " + eDocXPagar.doxpc_vnumero_doc;
            grv.GroupPanelText = "PAGOS EFECTUADOS POR " + eDocXPagar.Abreviatura + " - " + eDocXPagar.doxpc_vnumero_doc;

            mlistAdePagos = new BCuentasPorPagar().ListarPagoAdelantoXIdAdelantoAUnaFecha(doxcc_icod_correlativo_adelanto, Parametros.intEjercicio, sfecha);
            grd.DataSource = mlistAdePagos;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}