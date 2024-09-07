using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarRecepcionCompraSuministros : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        List<ERecepcionComprasSuministros> lstpresupuesto = new List<ERecepcionComprasSuministros>();
        public ERecepcionComprasSuministros _Be { get; set; }
        public bool bNoExceptuadosRetencion = false;
        public int proc_icod_proveedor = 0;

        #endregion

        #region "Eventos"

        public FrmListarRecepcionCompraSuministros()
        {
            InitializeComponent();
        }

        private void FrmListarProveedor_Load(object sender, EventArgs e)
        {


            Carga();

            txtnumeroOC.Focus();
        }

        #endregion

        #region "Metodos"

        public void Carga()
        {
            lstpresupuesto = new BCompras().listarRecepcionComprasSuministros().Where(_be => _be.proc_icod_proveedor == proc_icod_proveedor).ToList();
            //lstpresupuesto = new BCompras().ListarOrdenCompra().ToList();
            grdManoObra.DataSource = lstpresupuesto;
        }



        private void BuscarCriterio()
        {
            string cod = txtnumeroOC.Text, desc = txtProveedor.Text;
            grdManoObra.DataSource = lstpresupuesto.Where(obe => ((cod != string.Empty) ? obe.rcsc_vnumero_rcs.Contains(cod) : obe.NomProveedor.Contains(desc))).ToList();

        }

        private void viewProveedor_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (ERecepcionComprasSuministros)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DgAcept();
        }


        private void txtRUC_EditValueChanged(object sender, EventArgs e)
        {
            if (txtnumeroOC.ContainsFocus)
            {
                txtProveedor.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void txtRazonSocial_EditValueChanged(object sender, EventArgs e)
        {
            if (txtProveedor.ContainsFocus)
            {
                txtnumeroOC.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void viewManoObra_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }
    }
}


