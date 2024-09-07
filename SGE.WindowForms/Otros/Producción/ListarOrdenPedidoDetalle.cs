using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class ListarOrdenPedidoDetalle : DevExpress.XtraEditors.XtraForm
    {
        public EOrdenPedidoDet _Be;
        private List<EOrdenPedidoDet> mlist = new List<EOrdenPedidoDet>();
        private int xposition = 0;
        public int CodeMarcas;
        public int icod;
        public bool validarFichaTec = true;
        public ListarOrdenPedidoDetalle()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            EOrdenPedidoDet obj = new EOrdenPedidoDet();
            mlist = new BCentral().ListarOrdenPedidoDet(icod).Where(x => x.orped_btercerizado == false).ToList();
            dgrMotonave.DataSource = mlist;
            gridColumn6.Visible = validarFichaTec;
        }
        private void ListarModelo_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {

            _Be = (EOrdenPedidoDet)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);

            if (_Be.ConOPR == "Con OPR" && validarFichaTec)
            {
                Services.MessageError("El Item del Orden de Pedido ya Cuenta con Orden de Producción");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        private void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj =>
                                                   obj.orped_iitem_orden_pedido.ToString().Contains(txtCodigo.Text.ToUpper())
                                             //obj.f.ToString().Contains(txtdescripcion.Text.ToUpper())
                                             ).ToList();

        }
    }
}