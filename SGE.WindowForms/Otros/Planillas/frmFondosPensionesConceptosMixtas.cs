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
    public partial class frmFondosPensionesConceptosMixtas : DevExpress.XtraEditors.XtraForm
    {

        List<EFondosPensionesMixtas> lstFondosPensionesMixtas = new List<EFondosPensionesMixtas>();
        public int intIcodFondosPensiones = 0;
        public decimal SumaPorcentaje = 0;
        public frmFondosPensionesConceptosMixtas()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
            //-------
            //frmRegistroFondosPensiones frm2 = new frmRegistroFondosPensiones();
            //SumaPorcentaje = lstFondosPensionesConceptos.Sum(x => Convert.ToDecimal(x.fdpd_nporcentaje_concepto));
            //frm2.SumaPorcentaje = SumaPorcentaje;


        }

        private void cargar()
        {
            lstFondosPensionesMixtas = new BPlanillas().listarFondosPensionesMixtas(intIcodFondosPensiones);
            grdAlmacen.DataSource = lstFondosPensionesMixtas;
            viewAlmacen.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstFondosPensionesMixtas.FindIndex(x => x.fdpd2_icod_fp_concepto_mixto == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();
        }
        private void nuevo()
        {
            frmRegistroFondosPensionesConceptosMixtas frm = new frmRegistroFondosPensionesConceptosMixtas();
            frm.MiEvento += new frmRegistroFondosPensionesConceptosMixtas.DelegadoMensaje(reload);
            if (lstFondosPensionesMixtas.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstFondosPensionesMixtas.Max(x => Convert.ToInt32(x.fdpd2_iid_vcodigo_fp_concepto_mixto) + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.intIcodFondosPensiones = intIcodFondosPensiones;
            frm.lstFondosPensionesMixtas = lstFondosPensionesMixtas;
            //Exporta datos de suma de porcentaje
            //frmRegistroFondosPensiones frm2 = new frmRegistroFondosPensiones();
            //SumaPorcentaje = lstFondosPensionesConceptos.Sum(x =>Convert.ToDecimal(x.fdpd_nporcentaje_concepto));
            //frm2.SumaPorcentaje = SumaPorcentaje;
            //------------
            frm.SetInsert();
            frm.ShowDialog();
            frm.txtNombre.Focus();

        }
        private void modificar()
        {
            EFondosPensionesMixtas Obe = (EFondosPensionesMixtas)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroFondosPensionesConceptosMixtas frm = new frmRegistroFondosPensionesConceptosMixtas();
            frm.MiEvento += new frmRegistroFondosPensionesConceptosMixtas.DelegadoMensaje(reload);
            frm.lstFondosPensionesMixtas = lstFondosPensionesMixtas;
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
                EFondosPensionesMixtas Obe = (EFondosPensionesMixtas)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewAlmacen.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el Conceptos Comisión Mixta " + Obe.fdpd2_vdescripcion_concepto_mixto + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarFondosPensionesMixtas(Obe);
                    new BPlanillas().modificarPorcentajeFondoMixto(intIcodFondosPensiones);
                    cargar();
                    if (lstFondosPensionesMixtas.Count >= index + 1)
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
            grdAlmacen.DataSource = lstFondosPensionesMixtas.Where(x =>
                                                   x.fdpd2_iid_vcodigo_fp_concepto_mixto.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.fdpd2_vdescripcion_concepto_mixto.Contains(txtDescripcion.Text.ToUpper())
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




    }
}