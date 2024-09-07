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
    public partial class frm02UsuarioAccesos : DevExpress.XtraEditors.XtraForm
    {
        List<EFormulario> lstFormularios = new List<EFormulario>();

        public frm02UsuarioAccesos()
        {
            InitializeComponent();
        }

        private void FrmUsuarioAccesos_Load(object sender, EventArgs e)
        {

        }

        private void bteUsuario_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarUsuario();
        }
        private void cargaAccesos()
        {
            lstFormularios = new BAdministracionSistema().listarAccesosPermitidos(Convert.ToInt32(bteUsuario.Tag));
            gridAccesos.DataSource = lstFormularios;
        }
        private void reloadCurrentAccess()
        {
            Valores.lstAccesosUsuario = new BAdministracionSistema().listarAccesosPermitidos(Valores.intUsuario);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }
        private void listarUsuario()
        {
            using (frmListarUsuario frm = new frmListarUsuario())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteUsuario.Tag = frm._Be.usua_icod_usuario;
                    bteUsuario.Text = frm._Be.usua_codigo_usuario;
                    txtDescripcion.Text = frm._Be.usua_nombre_usuario;
                    cargaAccesos();
                }
            }
        }
        private void nuevo()
        {
            try
            {
                if (bteUsuario.Tag == null)
                    throw new ArgumentException("Seleccione un usuario");

                if (Valores.strUsuario != "SISTEMA")
                {
                    if (Valores.strUsuario != "SUPERVISOR")
                        throw new ArgumentException("Acción no permitida, solo el usuario SISTEMA o SUPERVISOR pueden realizar ésta acción");
                }

                using (frmListaAccesosNoPermitidos frm = new frmListaAccesosNoPermitidos())
                {
                    frm.intUsuario_ = Convert.ToInt32(bteUsuario.Tag);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        cargaAccesos();
                        Valores.lstAccesosUsuario = new BAdministracionSistema().listarAccesosPermitidos(Valores.intUsuario);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void eliminar()
        {
            try
            {
                if (bteUsuario.Tag == null)
                    throw new ArgumentException("Seleccione un usuario");

                if (lstFormularios.Count <= 0)
                    throw new ArgumentException("No accesos para eliminar");
                if (bteUsuario.Text != Valores.strUsuario)
                {
                    if (Valores.strUsuario != "SISTEMA")
                        if (Valores.strUsuario != "SUPERVISOR")
                            throw new ArgumentException("Acción no permitida para este usuario");
                }
                if (lstFormularios.Count > 0)
                {
                    EFormulario Obe = (EFormulario)viewAccesos.GetRow(viewAccesos.FocusedRowHandle);
                    if (XtraMessageBox.Show("¿Está seguro que desea eliminar acceso?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            Obe.intUsuarioAcceso = Convert.ToInt32(bteUsuario.Tag);
                            new BAdministracionSistema().eliminarAccesoUsuario(Obe);
                            cargaAccesos();
                            Valores.lstAccesosUsuario = new BAdministracionSistema().listarAccesosPermitidos(Valores.intUsuario);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void imprimir()
        {
        }

        private void bteUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                listarUsuario();
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
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