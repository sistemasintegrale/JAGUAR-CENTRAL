using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Almacén.Registro_de_Datos;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Almacén.Control_de_Inventarios_Físicos
{
    public partial class frm10ActualizacionInventario : DevExpress.XtraEditors.XtraForm
    {
        private List<EInventarioCab> lstInventario = new List<EInventarioCab>();

        public frm10ActualizacionInventario()
        {
            InitializeComponent();
        }

        private void FrmRegistroNotaIngreso_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstInventario = new BAlmacen().listarInventarioFisico(Parametros.intEjercicio);
            grdInventario.DataSource = lstInventario;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstInventario.FindIndex(x => x.invc_icod_inventario == intIcod);
            viewInventario.FocusedRowHandle = index;
            viewInventario.Focus();
        }


        private void nuevo()
        {
            try
            {
                EInventarioCab Obe = (EInventarioCab)viewInventario.GetRow(viewInventario.FocusedRowHandle);
                if (Obe == null)
                    return;

                if (Obe.tablc_iid_situacion == Parametros.intInventarioActualizado)
                {
                    XtraMessageBox.Show("El Inventario ya ha sido actualizado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmManteInventarioAct frm = new frmManteInventarioAct();
                frm.MiEvento += new frmManteInventarioAct.DelegadoMensaje(reload);
                frm.oBe = Obe;
                //frm.txtCorrelativo.Text = (lstInventario.Count == 0) ? "0001" : (lstInventario.Max(x => x.invc_iid_correlativo) + 1).ToString();
                frm.Show();
                frm.SetModify();
                frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar()
        {
            //try
            //{

            //    ENotaIngreso Obe = (ENotaIngreso)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
            //    if (Obe == null)
            //        return;
            //    frmManteNotaIngreso frm = new frmManteNotaIngreso();
            //    frm.MiEvento += new frmManteNotaIngreso.DelegadoMensaje(reload);

            //    frm.oBe = Obe;
            //    frm.SetModify();
            //    frm.Show();
            //    frm.setValues();
            //    frm.txtReferencia.Focus();
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void eliminar()
        {
            try
            {
                EInventarioCab Obe = (EInventarioCab)viewInventario.GetRow(viewInventario.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewInventario.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BAlmacen().eliminarInventarioFisico(Obe);
                    cargar();
                    if (lstInventario.Count >= index + 1)
                        viewInventario.FocusedRowHandle = index;
                    else
                        viewInventario.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
            EInventarioCab obe = (EInventarioCab)viewInventario.GetRow(viewInventario.FocusedRowHandle);
            if (obe == null)
                return;
            var lstDetalle = new BAlmacen().listarInventarioFisicoDet(obe.invc_icod_inventario);

            rptInventarioReg rpt = new rptInventarioReg();
            rpt.cargar(String.Format("INVENTARIO FÍSICO N° {0:0000}", obe.invc_iid_correlativo), String.Format("Fecha: {0:dd/MM/yyyy}", obe.invc_sfecha_inventario), lstDetalle, obe);

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

        private void lkpAlmacen_EditValueChanged(object sender, EventArgs e)
        {
            cargar();
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

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewInventario.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewInventario.ClearColumnsFilter();
        }

        private void revertirActualizaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            revertir();
        }

        private void revertir()
        {
            try
            {
                EInventarioCab Obe = (EInventarioCab)viewInventario.GetRow(viewInventario.FocusedRowHandle);
                if (Obe == null)
                    return;

                int index = viewInventario.FocusedRowHandle;
                if (XtraMessageBox.Show(String.Format("¿Esta seguro que desea revertir la actualización el invetario N° {0:0000}?", Obe.invc_iid_correlativo), "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //ELIMINAR LA NOTA DE INGRESO SI EXISTE
                    if (Convert.ToInt32(Obe.ningc_icod_nota_ingreso) > 0)
                    {
                        ENotaIngreso ObeNI = new ENotaIngreso();
                        ObeNI.ningc_icod_nota_ingreso = Convert.ToInt32(Obe.ningc_icod_nota_ingreso);
                        ObeNI.intUsuario = Valores.intUsuario;
                        ObeNI.strPc = WindowsIdentity.GetCurrent().Name;
                        ObeNI.almac_icod_almacen = Convert.ToInt32(Obe.almac_icod_almacen);
                        new BAlmacen().eliminarNotaIngreso(ObeNI);
                    }
                    //ELIMINAR LA NOTA DE SALIDA SI EXISTE
                    if (Convert.ToInt32(Obe.nsalc_icod_nota_salida) > 0)
                    {
                        ENotaSalida ObeNS = new ENotaSalida();
                        ObeNS.nsalc_icod_nota_salida = Convert.ToInt32(Obe.nsalc_icod_nota_salida);
                        ObeNS.intUsuario = Valores.intUsuario;
                        ObeNS.strPc = WindowsIdentity.GetCurrent().Name;
                        ObeNS.almac_icod_almacen = Convert.ToInt32(Obe.almac_icod_almacen);
                        new BAlmacen().eliminarNotaSalida(ObeNS);
                    }
                    //
                    Obe.tablc_iid_situacion = Parametros.intInventarioRegistrado;
                    new BAlmacen().modificarInventarioFisicoCab(Obe);
                    cargar();


                    viewInventario.FocusedRowHandle = index;

                    //if (lstInventario.Count >= index + 1)
                    //    viewInventario.FocusedRowHandle = index;
                    //else
                    //    viewInventario.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}