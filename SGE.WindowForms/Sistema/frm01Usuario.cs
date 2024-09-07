using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Sistema
{
    public partial class frm01Usuario : DevExpress.XtraEditors.XtraForm
    {
        private List<EUsuario> lstUsuarios = new List<EUsuario>();
        public frm01Usuario()
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
        void reload(int intIcod)
        {
            cargar();
            int index = lstUsuarios.FindIndex(x => x.usua_icod_usuario == intIcod);
            viewUsuario.FocusedRowHandle = index;
            viewUsuario.Focus();
        }
        private void nuevo()
        {
            if (Valores.strUsuario != "SISTEMA")
            {
                XtraMessageBox.Show("Ud. no cuenta con los permisos para poder crear cuentas de usuario", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmManteUsuario frm = new frmManteUsuario();
            frm.MiEvento += new frmManteUsuario.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.Show();

        }
        private void modificar()
        {
            EUsuario Obe = (EUsuario)viewUsuario.GetRow(viewUsuario.FocusedRowHandle);
            if (Obe.usua_icod_usuario == Valores.intUsuario)
            {
                DataToModify(Obe, 1);
            }
            else
            {
                if (Valores.strUsuario == "SISTEMA" || Valores.strUsuario == "SUPERVISOR")
                {
                    if (Obe.usua_nombre_usuario == "SISTEMA")
                        XtraMessageBox.Show("Ud. no cuenta con la capacidad de poder modificar la cuenta de este usuario", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        DataToModify(Obe, 1);
                }
                else
                    XtraMessageBox.Show("Ud. no cuenta con la capacidad de poder modificar la cuenta otros usuarios", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void DataToModify(EUsuario Obe, int opc)
        {
            frmManteUsuario frm = new frmManteUsuario();
            frm.MiEvento += new frmManteUsuario.DelegadoMensaje(reload);
            frm.mlist = lstUsuarios;
            frm.Obe = Obe;
            frm.Show();
            frm.setValues();
            if (opc == 1)
                frm.SetModify();
            else
                frm.SetCancel();
        }
        private void eliminar()
        {
            if (lstUsuarios.Count > 0)
            {
                if (Valores.strUsuario == "SISTEMA")
                {
                    EUsuario Obe = (EUsuario)viewUsuario.GetRow(viewUsuario.FocusedRowHandle);
                    Obe.intUsuario = Modules.Valores.intUsuario;
                    if (Obe.usua_codigo_usuario == "SISTEMA" || Obe.usua_codigo_usuario == "SUPERVISOR")
                    {
                        XtraMessageBox.Show("Usuario no puede ser eliminado", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (XtraMessageBox.Show("¿Está seguro de eliminar cuenta de usuario: " + Obe.usua_codigo_usuario + "?\r\n Nota: Los accesos permitidos del usuario serán eliminados", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                new BAdministracionSistema().eliminarUsuario(Obe);
                                viewUsuario.DeleteRow(viewUsuario.FocusedRowHandle);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                    XtraMessageBox.Show("Ud. no puede eliminar registro", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void imprimir()
        {
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }
    }
}