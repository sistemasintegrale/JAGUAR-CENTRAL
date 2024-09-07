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
    public partial class ListarOrdenProduccion : DevExpress.XtraEditors.XtraForm
    {
        public EOrdenProduccion _Be;
        private List<EOrdenProduccion> mlist = new List<EOrdenProduccion>();
        private int xposition = 0;
        public int CodeMarcas;

        public ListarOrdenProduccion()
        {
            InitializeComponent();
        }
        public void Carga()
        {

            mlist = new BCentral().ListarOrdenProduccion(Parametros.intEjercicio);
            mlist.ForEach(x => x.pedido = "P-" + x.orprc_vpedido + "." + x.orprc_vitem_pedido);
            dgrMotonave.DataSource = mlist;
        }
        private void ListarModelo_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            _Be = (EOrdenProduccion)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        private void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj =>
                                                   obj.orprc_icod_orden_produccion.ToUpper().Contains(txtCodigo.Text.ToUpper())
                                             //obj.f.ToString().Contains(txtdescripcion.Text.ToUpper())
                                             ).ToList();

        }
    }
}