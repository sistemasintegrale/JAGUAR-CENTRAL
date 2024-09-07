using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Almacén.Registro_de_Datos;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Almacén.Control_de_Inventarios_Físicos
{
    public partial class frm08GeneracionInventario : DevExpress.XtraEditors.XtraForm
    {
        private List<EInventarioCab> lstInventario = new List<EInventarioCab>();

        public frm08GeneracionInventario()
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
            grdNotaIngreso.DataSource = lstInventario;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstInventario.FindIndex(x => x.invc_icod_inventario == intIcod);
            viewNotaIngreso.FocusedRowHandle = index;
            viewNotaIngreso.Focus();
        }


        private void nuevo()
        {
            try
            {
                frmManteInventarioGen frm = new frmManteInventarioGen();
                frm.MiEvento += new frmManteInventarioGen.DelegadoMensaje(reload);
                frm.txtCorrelativo.Text = (lstInventario.Count == 0) ? "0001" : (lstInventario.Max(x => x.invc_iid_correlativo) + 1).ToString();
                frm.SetInsert();
                frm.Show();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar()
        {
            try
            {
                EInventarioCab Obe = (EInventarioCab)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
                if (Obe == null)
                    return;

                if (Obe.tablc_iid_situacion == Parametros.intInventarioActualizado)
                {
                    XtraMessageBox.Show("El Inventario ya ha sido actualizado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmManteInventarioGen frm = new frmManteInventarioGen();
                frm.MiEvento += new frmManteInventarioGen.DelegadoMensaje(reload);
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
        private void eliminar()
        {
            try
            {
                EInventarioCab Obe = (EInventarioCab)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewNotaIngreso.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BAlmacen().eliminarInventarioFisico(Obe);
                    cargar();
                    if (lstInventario.Count >= index + 1)
                        viewNotaIngreso.FocusedRowHandle = index;
                    else
                        viewNotaIngreso.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
            EInventarioCab obe = (EInventarioCab)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
            if (obe == null)
                return;
            var lstDetalle = new BAlmacen().listarInventarioFisicoDet(obe.invc_icod_inventario);

            rptInventario rpt = new rptInventario();
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
            viewNotaIngreso.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewNotaIngreso.ClearColumnsFilter();
        }



        private void modificarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            modificar();
        }

        private void crearPlantillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EInventarioCab Obe = (EInventarioCab)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
            if (Obe != null)
            {
                List<EInventarioDet> lstDetalle = new List<EInventarioDet>();
                lstDetalle = new BAlmacen().listarInventarioFisicoDet(Obe.invc_icod_inventario);
                grdDetalle.DataSource = lstDetalle;

                if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = sfdRuta.FileName;
                    if (!fileName.Contains(".xlsx"))
                    {
                        grdDetalle.ExportToXlsx(fileName + ".xlsx");
                        System.Diagnostics.Process.Start(fileName + ".xlsx");
                    }
                    else
                    {
                        grdDetalle.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                }
            }
        }
    }
}