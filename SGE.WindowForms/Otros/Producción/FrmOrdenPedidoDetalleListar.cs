using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class FrmOrdenPedidoDetalleListar : DevExpress.XtraEditors.XtraForm
    {
        public int orpec_iid_orden_pedido;
        private List<EOrdenPedidoDet> lstOrdenPedidoDet = new List<EOrdenPedidoDet>();
        public EOrdenPedidoDet selectDet = new EOrdenPedidoDet();

        public FrmOrdenPedidoDetalleListar() => InitializeComponent();
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => this.Dispose();
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => this.Aceptar();
        private void FrmOrdenPedidoDetalleListar_Load(object sender, EventArgs e) => Cargar();
        private void viewOrdenPedido_DoubleClick(object sender, EventArgs e) => Aceptar();

        private void Aceptar()
        {
            selectDet = (EOrdenPedidoDet)viewOrdenPedido.GetRow(viewOrdenPedido.FocusedRowHandle);
            if (selectDet is null) return;
            this.DialogResult = DialogResult.OK;
        }

        private void Cargar()
        {

            lstOrdenPedidoDet = new BCentral().ListarOrdenPedidoDet(orpec_iid_orden_pedido);
            grdOrdenPedido.DataSource = lstOrdenPedidoDet;
            grdOrdenPedido.RefreshDataSource();

        }


    }
}