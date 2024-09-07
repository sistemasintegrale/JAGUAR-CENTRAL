using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmConsultaPagosConNCredito : DevExpress.XtraEditors.XtraForm
    {
        List<ENotaCreditoPago> Lista = new List<ENotaCreditoPago>();
        public long codENotaCreditoCliente;

        public FrmConsultaPagosConNCredito()
        {
            InitializeComponent();
        }

        private void FrmConsultaPagosConNCredito_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            Lista = (new BCuentasPorCobrar()).ListarPagoNotaCredito(0, codENotaCreditoCliente, 0, 0);
            grd.DataSource = Lista;
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}