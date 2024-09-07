using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmConsultaPagosDocumentosXPagar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaPagosDocumentosXPagar));

        private List<EDocPorPagarPago> Lista = new List<EDocPorPagarPago>();

        //public long doxcc_icod_correlativo;
        public EDocPorPagar eDocXPagar;

        public FrmConsultaPagosDocumentosXPagar()
        {
            InitializeComponent();
        }

        private void DocumentosXCobrar_Load(object sender, EventArgs e)
        {
            this.Text = "PAGOS EFECTUADOS POR " + eDocXPagar.tdocc_vabreviatura_tipo_doc + " - " + eDocXPagar.doxpc_vnumero_doc;
            gv.GroupPanelText = "PAGOS EFECTUADOS POR " + eDocXPagar.tdocc_vabreviatura_tipo_doc + " - " + eDocXPagar.doxpc_vnumero_doc;
            Carga();
        }

        private void Carga()
        {
            Lista = new BCuentasPorPagar().listarDxpPagos(eDocXPagar.doxpc_icod_correlativo, Parametros.intEjercicio);
            grd.DataSource = Lista;
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


    }
}