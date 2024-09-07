using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmConsultaPagosDocumentosXCobrar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaPagosDocumentosXCobrar));


        private List<EDocXCobrarPago> Lista = new List<EDocXCobrarPago>();

        //public long doxcc_icod_correlativo;
        public EDocXCobrar eDocXCobrar;

        public FrmConsultaPagosDocumentosXCobrar()
        {
            InitializeComponent();
        }

        private void DocumentosXCobrar_Load(object sender, EventArgs e)
        {
            this.Text = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
            grv.GroupPanelText = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
            Carga();
        }

        private void Carga()
        {
            Lista = new BCuentasPorCobrar().ListarPagoDirectoDocumentoXCobrar(eDocXCobrar.doxcc_icod_correlativo);
            grd.DataSource = Lista;
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }


    }
}