using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmListarAnalitica : DevExpress.XtraEditors.XtraForm
    {
        List<ETablaRegistro> lstTipoAnaliticas = new List<ETablaRegistro>();
        public ETablaRegistro _Be { get; set; }

        public frmListarAnalitica()
        {
            InitializeComponent();
        }

        private void frm01CentroCosto_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstTipoAnaliticas = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoAnalitica).OrderBy(x => x.tarec_icorrelativo_registro).ToList();
            grdAnalitica.DataSource = lstTipoAnaliticas;
            viewAnalitica.Focus();
        }

        private void returnSeleccion()
        {
            if (lstTipoAnaliticas.Count > 0)
            {
                _Be = (ETablaRegistro)viewAnalitica.GetRow(viewAnalitica.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void viewCentroCosto_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }

        private void buscarCriterio()
        {
            grdAnalitica.DataSource = lstTipoAnaliticas.Where(x =>
                                                   x.tarec_icorrelativo_registro.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.tarec_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
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