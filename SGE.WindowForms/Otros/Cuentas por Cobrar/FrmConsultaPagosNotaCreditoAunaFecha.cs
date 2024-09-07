using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmConsultaPagosNotaCreditoAunaFecha : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaPagosNotaCredito));

        private List<ENotaCreditoPago> Lista = new List<ENotaCreditoPago>();
        public DateTime sfecha;
        public EDocXCobrar eDocXCobrar;

        public FrmConsultaPagosNotaCreditoAunaFecha()
        {
            InitializeComponent();
        }
        private void Carga()
        {
            Lista = new BCuentasPorCobrar().ListarPagoNotaCreditoXIdDocXCobrarAUnaFecha(eDocXCobrar.doxcc_icod_correlativo, Parametros.intEjercicio, sfecha);
            grd.DataSource = Lista;
        }
        private void FrmConsultaPagosNotaCreditoAunaFecha_Load(object sender, EventArgs e)
        {
            this.Text = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
            grv.GroupPanelText = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
            Carga();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}