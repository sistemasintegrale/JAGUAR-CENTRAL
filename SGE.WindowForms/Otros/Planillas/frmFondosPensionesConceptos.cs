using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
//using SGE.WindowForms.Planillas.Registro_de_Datos;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmFondosPensionesConceptos : DevExpress.XtraEditors.XtraForm
    {

        List<EFondosPensionesConceptos> lstFondosPensionesConceptos = new List<EFondosPensionesConceptos>();
        public int intIcodFondosPensiones = 0;
        public decimal SumaPorcentaje = 0;
        public frmFondosPensionesConceptos()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
            //-------
            frmRegistroFondosPensiones frm2 = new frmRegistroFondosPensiones();
            SumaPorcentaje = lstFondosPensionesConceptos.Sum(x => Convert.ToDecimal(x.fdpd_nporcentaje_concepto));
            //frm2.SumaPorcentaje = SumaPorcentaje;



        }

        //void Modify(int Cab_icod_correlativo)
        //{
        //    cargar();
        //    int index = lstFondosPensionesConceptos.FindIndex(obe => obe.fdpc_icod_fondo_pension == Cab_icod_correlativo);
        //    viewAlmacen.FocusedRowHandle = index;
        //}

        private void cargar()
        {
            lstFondosPensionesConceptos = new BPlanillas().listarFondosPensionesConceptos(intIcodFondosPensiones);
            grdAlmacen.DataSource = lstFondosPensionesConceptos;
            viewAlmacen.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstFondosPensionesConceptos.FindIndex(x => x.fdpd_icod_fondo_pension_concepto == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();
        }
        private void nuevo()
        {
            frmRegistroFondosPensionesConceptos frm = new frmRegistroFondosPensionesConceptos();
            frm.MiEvento += new frmRegistroFondosPensionesConceptos.DelegadoMensaje(reload);
            if (lstFondosPensionesConceptos.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstFondosPensionesConceptos.Max(x => Convert.ToInt32(x.fdpd_iid_vcodigo_fondo_concepto) + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.intIcodFondosPensiones = intIcodFondosPensiones;
            frm.lstFondosPensionesConceptos = lstFondosPensionesConceptos;
            frm.SetInsert();
            frm.ShowDialog();
            frm.txtNombre.Focus();
            grdAlmacen.Refresh();


        }
        private void modificar()
        {
            EFondosPensionesConceptos Obe = (EFondosPensionesConceptos)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroFondosPensionesConceptos frm = new frmRegistroFondosPensionesConceptos();
            frm.MiEvento += new frmRegistroFondosPensionesConceptos.DelegadoMensaje(reload);
            frm.lstFondosPensionesConceptos = lstFondosPensionesConceptos;
            frm.Obe = Obe;
            frm.SetModify();
            frm.setValues();
            frm.ShowDialog();
            frm.txtNombre.Focus();


        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {

        }

        private void eliminar()
        {
            try
            {
                EFondosPensionesConceptos Obe = (EFondosPensionesConceptos)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewAlmacen.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el Conceptos Comisión Fija " + Obe.fdpd_vdescripcion_concepto + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarFondosPensionesConceptos(Obe);
                    new BPlanillas().modificarPorcentajeFondoFijo(intIcodFondosPensiones);
                    cargar();
                    if (lstFondosPensionesConceptos.Count >= index + 1)
                        viewAlmacen.FocusedRowHandle = index;
                    else
                        viewAlmacen.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buscarCriterio()
        {
            grdAlmacen.DataSource = lstFondosPensionesConceptos.Where(x =>
                                                   x.fdpd_iid_vcodigo_fondo_concepto.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.fdpd_vdescripcion_concepto.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
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



        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void grdAlmacen_DoubleClick(object sender, EventArgs e)
        {
            //EFondosPensiones Obe = (EFondosPensiones)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            //if (Obe == null)
            //    return;
            //frmRegistroFondosPensiones frm = new frmRegistroFondosPensiones();
            //frm.Obe = Obe;
            //frm.SetCancel();
            //frm.Show();
            //frm.setValues();
        }

        private void frmFondosPensionesConceptos_FormClosed(object sender, FormClosedEventArgs e)
        {

            //frm04FondosPensiones frm2 = new frm04FondosPensiones();
            //frm2.lstFondosPensiones.Clear();
            //   frm2.cargar();
            //   frm2.grdAlmacen.BeginUpdate();
            //   frm2.grdAlmacen.DataSource = frm2.lstFondosPensiones;
            //   frm2.grdAlmacen.RefreshDataSource();
            //   frm2.viewAlmacen.RefreshData();
            //   frm2.grdAlmacen.EndUpdate();





        }

        private void frmFondosPensionesConceptos_FormClosing(object sender, FormClosingEventArgs e)
        {

            //frm04FondosPensiones frm2 = new frm04FondosPensiones();

            //    frm2.lstFondosPensiones.Clear();
            //    frm2.cargar();
            //   frm2.grdAlmacen.BeginUpdate();                                         
            //    frm2.grdAlmacen.DataSource = frm2.lstFondosPensiones;
            //   frm2.grdAlmacen.RefreshDataSource();   
            //    frm2.viewAlmacen.RefreshData();
            //    frm2.grdAlmacen.EndUpdate();
        }




    }
}