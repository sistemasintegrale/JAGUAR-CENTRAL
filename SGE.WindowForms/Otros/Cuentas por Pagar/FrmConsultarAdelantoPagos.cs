using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmConsultarAdelantoPagos : DevExpress.XtraEditors.XtraForm
    {
        List<EDocxPagarPagoAdelanto> mlistAdePagos = new List<EDocxPagarPagoAdelanto>();
        public long doxcc_icod_correlativo_adelanto;
        public EDocPorPagar eDocXPagar;


        public FrmConsultarAdelantoPagos()
        {
            InitializeComponent();
        }

        private void FrmConsultarAdelantoPagos_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            this.Text = "PAGOS EFECTUADOS POR " + eDocXPagar.Abreviatura + " - " + eDocXPagar.doxpc_vnumero_doc;
            grv.GroupPanelText = "PAGOS EFECTUADOS POR " + eDocXPagar.Abreviatura + " - " + eDocXPagar.doxpc_vnumero_doc;

            mlistAdePagos = new BCuentasPorPagar().ListarPagoAdelantoXIdAdelanto(0, doxcc_icod_correlativo_adelanto, Parametros.intEjercicio);
            grd.DataSource = mlistAdePagos;
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}