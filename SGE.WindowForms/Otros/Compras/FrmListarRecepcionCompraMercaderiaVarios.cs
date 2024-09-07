using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarRecepcionCompraMercaderiaVarios : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        public List<ERecepcionComprasMercaderia> lstRCM = new List<ERecepcionComprasMercaderia>();
        public List<ERecepcionComprasMercaderia> lstRCMSelecionados = new List<ERecepcionComprasMercaderia>();
        public ERecepcionComprasMercaderia _Be { get; set; }
        public bool bNoExceptuadosRetencion = false;
        public int proc_icod_proveedor = 0;
        public bool flag_multiple = false;

        #endregion

        #region "Eventos"

        public FrmListarRecepcionCompraMercaderiaVarios()
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
            lstRCM = new BCompras().listarRecepcionComprasMercaderia().Where(_be => _be.proc_icod_proveedor == proc_icod_proveedor).ToList();
            lstRCM.ForEach(x =>
            {
                x.flag_multiple = false;
            });
            grdManoObra.DataSource = lstRCM;
            grdManoObra.DataSource = lstRCM;
        }



        private void BuscarCriterio()
        {
            string cod = txtnumeroOC.Text, desc = txtProveedor.Text;
            grdManoObra.DataSource = lstRCM.Where(obe => ((cod != string.Empty) ? obe.rcmc_vnumero_rcm.Contains(cod) : obe.NomProveedor.Contains(desc))).ToList();

        }


        private void DgAcept()
        {

            _Be = (ERecepcionComprasMercaderia)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (flag_multiple)
            {
                viewManoObra.MoveLast();
                viewManoObra.MoveFirst();
            }
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }

        #endregion

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
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

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DgAcept();
        }
    }
}


