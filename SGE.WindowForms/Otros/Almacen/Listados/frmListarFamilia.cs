using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Listados
{
    public partial class frmListarFamilia : DevExpress.XtraEditors.XtraForm
    {
        List<EFamiliaCab> lstAlmacenes = new List<EFamiliaCab>();
        public EFamiliaCab _Be { get; set; }
        public frmListarFamilia()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstAlmacenes = new BAlmacen().listarFamiliaCab();
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
                _Be = (EFamiliaCab)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buscarCriterio()
        {
            grdAlmacen.DataSource = lstAlmacenes.Where(x =>
                                                   x.famic_vabreviatura.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.famic_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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