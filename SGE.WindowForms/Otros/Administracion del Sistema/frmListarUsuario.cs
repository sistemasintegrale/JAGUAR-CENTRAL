using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmListarUsuario : DevExpress.XtraEditors.XtraForm
    {
        private List<EUsuario> lstUsuarios = new List<EUsuario>();
        public EUsuario _Be { get; set; }
        public frmListarUsuario()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstUsuarios = new BAdministracionSistema().listarUsuarios();
            grdUsuario.DataSource = lstUsuarios;
        }

        private void returnSeleccion()
        {
            if (lstUsuarios.Count > 0)
            {
                _Be = (EUsuario)viewUsuario.GetRow(viewUsuario.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
        private void viewUsuario_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }
    }
}