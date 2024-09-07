using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmConsultaPagosAdelantosAunaFecha : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaPagosAdelantos));

        private List<EAdelantoPago> Lista = new List<EAdelantoPago>();

        public Boolean flag = true;
        public DateTime sfecha;
        public EDocXCobrar eDocXCobrar;
        public EAdelantoCliente _obeAdelanto;

        public FrmConsultaPagosAdelantosAunaFecha()
        {
            InitializeComponent();
        }
        private void Carga()
        {
            Lista = new BCuentasPorCobrar().ListarPagoAdelantoXIdDocXCobrarAUnaFecha(eDocXCobrar.doxcc_icod_correlativo, Parametros.intEjercicio, sfecha);
            grd.DataSource = Lista;
        }
        private void FrmConsultaPagosAdelantosAunaFecha_Load(object sender, EventArgs e)
        {
            if (flag == true)
            {
                this.Text = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
                grv.GroupPanelText = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
                Carga();
            }
            else
            {
                this.Text = "PAGOS EFECTUADOS CON EL ADELANTO Nº " + _obeAdelanto.vnumero_adelanto;
                grv.GroupPanelText = "PAGOS EFECTUADOS CON EL ADELANTO Nº " + _obeAdelanto.vnumero_adelanto;
                Carga();
            }
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}