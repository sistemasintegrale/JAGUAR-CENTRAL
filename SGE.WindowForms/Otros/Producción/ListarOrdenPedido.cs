using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class ListarOrdenPedido : DevExpress.XtraEditors.XtraForm
    {
        public EOrdenPedidoCab _Be;
        private List<EOrdenPedidoCab> mlist = new List<EOrdenPedidoCab>();
        private int xposition = 0;
        public int CodeMarcas;

        public ListarOrdenPedido()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            EOrdenPedidoCab obj = new EOrdenPedidoCab();
            mlist = new BCentral().ListarOrdenPedidoCab(Parametros.intEjercicio);
            dgrMotonave.DataSource = mlist;
        }
        private void ListarModelo_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            _Be = (EOrdenPedidoCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        private void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj =>
                                                   obj.orpec_icod_orden_pedido.ToUpper().Contains(txtCodigo.Text.ToUpper())
                                             //obj.f.ToString().Contains(txtdescripcion.Text.ToUpper())
                                             ).ToList();

        }
    }
}