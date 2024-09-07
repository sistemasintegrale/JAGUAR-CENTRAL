using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Listados
{
    public partial class frmListarTipo : DevExpress.XtraEditors.XtraForm
    {
        List<EProdTablaRegistro> lstAlmacenes = new List<EProdTablaRegistro>();
        public int intFamilia;
        public EProdTablaRegistro _Be { get; set; }
        public EProdTablaRegistro obe = new EProdTablaRegistro();
        public frmListarTipo()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            obe.iid_tipo_tabla = 13;
            lstAlmacenes = new BCentral().ListarProdTablaRegistro(obe);
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
                _Be = (EProdTablaRegistro)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buscarCriterio()
        {
            grdAlmacen.DataSource = lstAlmacenes.Where(x =>
                                                   x.tarec_iid_correlativo.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.tarec_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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