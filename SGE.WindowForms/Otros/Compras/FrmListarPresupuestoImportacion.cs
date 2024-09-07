using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarPresupuestoImportacion : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        List<EPresupuestoNacional> mLista = new List<EPresupuestoNacional>();
        public EPresupuestoNacional _Be { get; set; }
        public bool bNoExceptuadosRetencion = false;
        public int proc_icod_proveedor = 0;

        #endregion

        #region "Eventos"

        public FrmListarPresupuestoImportacion()
        {
            InitializeComponent();
        }

        private void FrmListarProveedor_Load(object sender, EventArgs e)
        {

            BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaRegistro(Parametros.intTablaTipoPresupuestoNacional), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            Carga();


        }
        public void Carga()
        {
            mLista = new BCompras().ListarPresupuestoNacional(Parametros.intEjercicio, Convert.ToInt32(lkpTipo.EditValue));
            dgrPresupuestoNacional.DataSource = mLista;
        }
        #endregion

        #region "Metodos"






        private void BuscarCriterio()
        {
            dgrPresupuestoNacional.DataSource = mLista.Where(obj =>
                                                   obj.prep_cod_presupuesto.ToString().ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.prep_vconcepto.ToUpper().Contains(txtConcepto.Text.ToUpper())
                                             ).ToList();

        }





        private void DgAcept()
        {
            _Be = (EPresupuestoNacional)viewPresupuestoNacional.GetRow(viewPresupuestoNacional.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void viewPresupuestoNacional_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }
    }
}


