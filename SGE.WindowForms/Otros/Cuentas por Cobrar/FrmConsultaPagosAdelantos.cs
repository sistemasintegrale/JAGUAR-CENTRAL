using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmConsultaPagosAdelantos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaPagosAdelantos));

        private List<EAdelantoPago> Lista = new List<EAdelantoPago>();

        public Boolean flag = true;

        public EDocXCobrar eDocXCobrar;
        public EAdelantoCliente _obeAdelanto;


        //public long doxcc_icod_correlativo_adelanto;

        public FrmConsultaPagosAdelantos()
        {
            InitializeComponent();
        }

        private void Carga()
        {
            Lista = new BCuentasPorCobrar().ListarAdelantoPago(0, eDocXCobrar.doxcc_icod_correlativo);
            grd.DataSource = Lista;
        }

        private void FrmConsultaPagosAdelantos_Load(object sender, EventArgs e)
        {
            if (flag == true)
            {
                this.Text = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
                gv.GroupPanelText = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
                Carga();
            }
            else
            {
                this.Text = "PAGOS EFECTUADOS CON EL ADELANTO Nº " + _obeAdelanto.vnumero_adelanto;
                gv.GroupPanelText = "PAGOS EFECTUADOS CON EL ADELANTO Nº " + _obeAdelanto.vnumero_adelanto;
                Carga();
            }
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }





    }
}