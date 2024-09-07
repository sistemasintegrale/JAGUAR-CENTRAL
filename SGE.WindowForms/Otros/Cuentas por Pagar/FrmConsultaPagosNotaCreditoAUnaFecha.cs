using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmConsultaPagosNotaCreditoAUnaFecha : DevExpress.XtraEditors.XtraForm
    {
        private List<EDocPorPagarPago> Lista = new List<EDocPorPagarPago>();

        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public int mes;
        public DateTime sfecha;

        public EDocPorPagar eDocXPagar = new EDocPorPagar();


        public FrmConsultaPagosNotaCreditoAUnaFecha()
        {
            InitializeComponent();
        }

        private void FrmConsultaPagosNotaCreditoAUnaFecha_Load(object sender, EventArgs e)
        {
            Carga();
            this.Text = "Pagos efectuados por Notas de Crédito de " + eDocXPagar.tdocc_vabreviatura_tipo_doc + "-" + eDocXPagar.doxpc_vnumero_doc;
            grv.GroupPanelText = "Pagos efectuados por Notas de Crédito de " + eDocXPagar.tdocc_vabreviatura_tipo_doc + "-" + eDocXPagar.doxpc_vnumero_doc;
        }
        private void Carga()
        {
            Lista = new BCuentasPorPagar().ListarPagoDocumentoXPagarXIdDocXPagarAunaFecha(eDocXPagar.doxpc_icod_correlativo, Parametros.intEjercicio, sfecha).Where(ob => ob.pdxpc_vorigen == "N").ToList();//A identifica que es una nota de crédito
            grd.DataSource = Lista;
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}