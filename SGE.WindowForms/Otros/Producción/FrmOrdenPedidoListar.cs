using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class FrmOrdenPedidoListar : DevExpress.XtraEditors.XtraForm
    {
        private List<EOrdenPedidoCab> lista = new List<EOrdenPedidoCab>();
        public EOrdenPedidoCab OrdenSelect = new EOrdenPedidoCab();
        public FrmOrdenPedidoListar() => InitializeComponent();
        private void FrmOrdenPedidoListar_Load(object sender, EventArgs e) => Carga();
        private void viewLista_DoubleClick(object sender, EventArgs e) => Aceptar();
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => this.Dispose();
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => Aceptar();

        private void Carga()
        {
            lista = new BCentral().ListarOrdenPedidoCab(Parametros.intEjercicio);
            grdLista.DataSource = lista;
        }

        private void Aceptar()
        {
            OrdenSelect = (EOrdenPedidoCab)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (OrdenSelect is null) return;
            this.DialogResult = DialogResult.OK;
        }


    }
}