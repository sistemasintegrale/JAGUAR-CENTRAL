using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Listados
{
    public partial class frmListarSubFamilia : DevExpress.XtraEditors.XtraForm
    {
        List<EFamiliaDet> lstAlmacenes = new List<EFamiliaDet>();
        public int intFamilia;
        public EFamiliaDet _Be { get; set; }
        public frmListarSubFamilia()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstAlmacenes = new BAlmacen().listarFamiliaDet(intFamilia);
            grdAlmacen.DataSource = lstAlmacenes;
            viewAlmacen.Focus();
        }

        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstAlmacenes.Count > 0)
            {
                _Be = (EFamiliaDet)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buscarCriterio()
        {
            grdAlmacen.DataSource = lstAlmacenes.Where(x =>
                                                   x.famid_vabreviatura.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.famid_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }


        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}