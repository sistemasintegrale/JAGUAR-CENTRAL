using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Compras_Suministros
{
    public partial class frm03Percepcion : DevExpress.XtraEditors.XtraForm
    {
        List<EPercepcion> lstPercepcion = new List<EPercepcion>();
        public frm03Percepcion()
        {
            InitializeComponent();
        }

        private void frm08DocCompra_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstPercepcion = new BCompras().listarPercepcionCab(Parametros.intEjercicio);
            grdPercepcion.DataSource = lstPercepcion;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstPercepcion.FindIndex(x => x.percc_icod_percepcion == intIcod);
            viewPercepcion.FocusedRowHandle = index;
            viewPercepcion.Focus();
        }

        private void nuevo()
        {
            frmMantePercepcion frm = new frmMantePercepcion();
            frm.MiEvento += new frmMantePercepcion.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.Show();
        }

        private void modificar()
        {
            EPercepcion obe = (EPercepcion)viewPercepcion.GetRow(viewPercepcion.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                if (obe.intSituacionDXP != 1)
                    throw new ArgumentException(String.Format("El registro no puede modificado, su situación es {0}", obe.strSituacionDXP));

                frmMantePercepcion frm = new frmMantePercepcion();
                frm.MiEvento += new frmMantePercepcion.DelegadoMensaje(reload);
                frm.Obe = obe;
                frm.SetModify();
                frm.Show();
                frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminar()
        {
            EPercepcion obe = (EPercepcion)viewPercepcion.GetRow(viewPercepcion.FocusedRowHandle);
            if (obe == null)
                return;
            int index = viewPercepcion.FocusedRowHandle;
            try
            {
                if (obe.intSituacionDXP != 1)
                    throw new ArgumentException(String.Format("El registro no puede eliminado, su situación es {0}", obe.strSituacionDXP));
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new BCompras().eliminarPercepcionCab(obe);
                    cargar();
                    if (lstPercepcion.Count >= index + 1)
                        viewPercepcion.FocusedRowHandle = index;
                    else
                        viewPercepcion.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ver()
        {
            EPercepcion obe = (EPercepcion)viewPercepcion.GetRow(viewPercepcion.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                //frmManteFacturaCompra frm = new frmManteFacturaCompra();
                //frm.MiEvento += new frmManteFacturaCompra.DelegadoMensaje(reload);
                //frm.Obe = obe;
                //frm.SetCancel();
                //frm.Show();
                //frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void filtrar()
        {
            grdPercepcion.DataSource = lstPercepcion.Where(x => x.percc_vnro_percepcion.Contains(textEdit1.Text) &&
                x.strProveedor.Contains(textEdit2.Text.ToUpper())).ToList();
        }

        private void viewDocCompra_DoubleClick(object sender, EventArgs e)
        {
            ver();
        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

    }
}