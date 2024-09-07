using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmListarDXCGarantiaClientes : DevExpress.XtraEditors.XtraForm
    {
        public int? intIcodCliente = null;
        public bool bDocFacBol = true;
        public EDocXCobrar EDocPorCobrar { get; set; }
        public int intOpcionPlanillaVenta = 0;
        public bool flag_multiple = false;
        public int favc_icod_cliente = 0;
        public List<EDocXCobrar> lstDocPorCobrar = new List<EDocXCobrar>();
        public FrmListarDXCGarantiaClientes()
        {
            InitializeComponent();
        }
        private void FrmListarDocxCobrar_Load(object sender, EventArgs e)
        {
            cargar();
        }
        public void cargar()
        {

            lstDocPorCobrar = new BCuentasPorCobrar().ListarDocPorCobrarxCliente(favc_icod_cliente, Parametros.intEjercicio).Where(x => x.tdocc_vabreviatura_tipo_doc == "FAV").ToList();
            grdDocxCobrar.DataSource = lstDocPorCobrar;
        }
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void Acceptar()
        {
            EDocPorCobrar = (EDocXCobrar)viewDocxCobrar.GetRow(viewDocxCobrar.FocusedRowHandle);


            txtNumero.Focus();
            viewDocxCobrar.MoveLast();
            viewDocxCobrar.MoveFirst();


            if (EDocPorCobrar != null)
                this.DialogResult = DialogResult.OK;
        }
        private void viewDocxCobrar_DoubleClick(object sender, EventArgs e)
        {

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