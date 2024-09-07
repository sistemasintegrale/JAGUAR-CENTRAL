using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarDXPImportacion : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        List<EImportacion> lstImportacion = new List<EImportacion>();
        public EImportacion _Be { get; set; }
        public bool bNoExceptuadosRetencion = false;
        public int proc_icod_proveedor = 0;

        #endregion

        #region "Eventos"

        public FrmListarDXPImportacion()
        {
            InitializeComponent();
        }

        private void FrmListarProveedor_Load(object sender, EventArgs e)
        {
            Carga();
        }

        #endregion

        #region "Metodos"

        public void Carga()
        {
            lstImportacion = new BCompras().ListarImportacion().Where(x => x.tablc_iid_sit_import == 333).ToList();
            grdManoObra.DataSource = lstImportacion;
        }



        private void BuscarCriterio()
        {
            string Num = lstImportacion[0].impc_vnumero_importacion;
            grdManoObra.DataSource = lstImportacion.Where(obe => obe.impc_vnumero_importacion == Num).ToList();

        }

        private void viewProveedor_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EImportacion)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
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
                //txtProveedor.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void txtRazonSocial_EditValueChanged(object sender, EventArgs e)
        {
            //if (txtProveedor.ContainsFocus)
            //{
            //    txtnumeroOC.Text = string.Empty;
            //    BuscarCriterio();
            //}
        }

        private void viewManoObra_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }
    }
}


