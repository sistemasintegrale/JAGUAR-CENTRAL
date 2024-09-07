using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Listados
{
    public partial class FrmListarDoRef : DevExpress.XtraEditors.XtraForm
    {
        public int? intIcodCliente = null;
        public bool bDocFacBol = true;
        public EDocXCobrar EDocPorCobrar { get; set; }
        public int intOpcionPlanillaVenta = 0;
        public bool flag_multiple = false;
        public int TipoDoc = 0;
        public List<EDocXCobrar> lstDocPorCobrar = new List<EDocXCobrar>();
        public FrmListarDoRef()
        {
            InitializeComponent();
        }
        private void FrmListarDocxCobrar_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstDocPorCobrar = new BCuentasPorCobrar().ListarDocPorCobrarxCliente(intIcodCliente, Parametros.intEjercicio).Where(x => x.tdocc_icod_tipo_doc == TipoDoc).ToList();
            grdDocxCobrar.DataSource = lstDocPorCobrar;

        }
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void Acceptar()
        {
            EDocPorCobrar = (EDocXCobrar)viewDocxCobrar.GetRow(viewDocxCobrar.FocusedRowHandle);

            if (flag_multiple)
            {
                txtNumero.Focus();
                viewDocxCobrar.MoveLast();
                viewDocxCobrar.MoveFirst();
            }

            if (EDocPorCobrar != null)
                this.DialogResult = DialogResult.OK;
        }
        private void viewDocxCobrar_DoubleClick(object sender, EventArgs e)
        {
            if (!flag_multiple)
                Acceptar();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Acceptar();
        }

        private void txtNumero_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdDocxCobrar.DataSource = lstDocPorCobrar.Where(x => x.doxcc_vnumero_doc.Contains(txtNumero.Text)).ToList();
        }
    }
}