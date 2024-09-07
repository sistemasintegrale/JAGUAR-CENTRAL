using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmConsultaPagosNotaCredito : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaPagosNotaCredito));

        private List<ENotaCreditoPago> Lista = new List<ENotaCreditoPago>();

        public EDocXCobrar eDocXCobrar;
        //public long doxcc_icod_correlativo_nota_credito;

        public FrmConsultaPagosNotaCredito()
        {
            InitializeComponent();
        }


        private void Carga()
        {
            Lista = new BCuentasPorCobrar().ListarPagoNotaCredito(0, eDocXCobrar.doxcc_icod_correlativo, 0, Parametros.intEjercicio);
            grd.DataSource = Lista;
        }


        private void FrmConsultaPagosNotaCredito_Load(object sender, EventArgs e)
        {
            this.Text = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
            grv.GroupPanelText = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
            Carga();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }




    }
}