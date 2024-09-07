using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Producción.Caracteristicas
{
    public partial class FrmMarca : DevExpress.XtraEditors.XtraForm
    {
        private List<EProdTablaRegistro> mlist = new List<EProdTablaRegistro>();
        private int xposition = 0;

        public FrmMarca()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            EProdTablaRegistro obj = new EProdTablaRegistro() { iid_tipo_tabla = 11 };
            mlist = new BCentral().ListarProdTablaRegistro(obj);
            dgrMotonave.DataSource = mlist;
        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
            viewMotonave.FocusedRowHandle = xposition;
        }
        void AddEvent()
        {
            this.viewMotonave.DoubleClick += new System.EventHandler(this.viewMotonave_DoubleClick);
        }

        private void FrmMarca_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }
        private void nuevo()
        {
            FrmManteTipo Motonave = new FrmManteTipo();
            Motonave.MiEvento += new FrmManteTipo.DelegadoMensaje(form2_MiEvento);
            viewMotonave.MoveLast();
            EProdTablaRegistro Obe = (EProdTablaRegistro)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            int i = 0;
            if (mlist.Count > 0)
                i = mlist.Max(ob => ob.tarec_iid_correlativo);
            Motonave.Correlative = Convert.ToInt32(i) + 1;
            Motonave.oDetail = mlist;
            Motonave.TipoRegistro = 11;
            Motonave.NombreFormulario = "Marca";
            Motonave.Show();
            Motonave.txtcodigo.Properties.ReadOnly = true;
            Motonave.SetInsert();
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos();
        }
        private void Datos()
        {
            FrmManteTipo Motonave = new FrmManteTipo();
            Motonave.MiEvento += new FrmManteTipo.DelegadoMensaje(Modify);
            Motonave.oDetail = mlist;
            Motonave.TipoRegistro = 11;
            Motonave.NombreFormulario = "Marca";
            Motonave.Show();
            EProdTablaRegistro Obe = (EProdTablaRegistro)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            Motonave.Correlative = Obe.icod_tabla;
            Motonave.txtcodigo.Text = Obe.tarec_viid_correlativo;
            Motonave.txtdescripcion.Text = Obe.descripcion;
            Motonave.txtabreviatura.Text = Obe.tarec_vabreviatura;
            Motonave.txtcodigo.Properties.ReadOnly = true;
            Motonave.lkpEstado.EditValue = (Obe.Estado == 'A') ? 1 : 0;
            Motonave.SetModify();
            xposition = viewMotonave.FocusedRowHandle;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void eliminar()
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                List<EProducto> mlistproducto = new List<EProducto>();
                EProdTablaRegistro Obe = (EProdTablaRegistro)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                //mlistproducto = new BProducto().ListarProductos().Where(ot => ot.pr_tarec_icorrelativo == Obe.icod_tabla).ToList();
                //if (mlistproducto.Count == 0)
                //{
                BCentral oBl = new BCentral();
                oBl.EliminarProdTablaRegistro(Obe);
                viewMotonave.DeleteRow(viewMotonave.FocusedRowHandle);
                viewMotonave.RefreshData();
                //}
                //else
                //{
                //    XtraMessageBox.Show("No se Puede Eliminar: Existen Productos Creados con Esta Marca", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //}
            }
        }
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }
        private void imprimir()
        {
            if (mlist.Count > 0)
            {
                rptRegistroCaracteristicas rpt = new rptRegistroCaracteristicas();
                rpt.cargar(mlist, "MARCA");
            }
        }
        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                FrmManteTipo Motonave = new FrmManteTipo();
                Motonave.MiEvento += new FrmManteTipo.DelegadoMensaje(AddEvent);
                Motonave.NombreFormulario = "Marca";
                Motonave.Show();
                Motonave.SetCancel();
                EProdTablaRegistro Obe = (EProdTablaRegistro)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                Motonave.txtcodigo.Text = Obe.tarec_viid_correlativo;
                Motonave.txtdescripcion.Text = Obe.descripcion;
                Motonave.BtnGuardar.Enabled = false;
                Motonave.txtcodigo.Enabled = false;
            }
            this.viewMotonave.DoubleClick -= new System.EventHandler(this.viewMotonave_DoubleClick);
        }

        private void FrmMarca_Activated(object sender, EventArgs e)
        {
            //((FrmMain)MdiParent).oInstance = this;
        }

        private void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj =>
                                                   obj.descripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.tarec_viid_correlativo.ToString().Contains(txtCodigo.Text.ToUpper())
                                             ).ToList();

        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();

        }

        private void viewMotonave_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
                nuevoToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F5)
                Datos();
            if (e.KeyCode == Keys.F9)
                eliminarToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F8) { imprimir(); }

        }

        private void modeloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modelo();
        }
        private void modelo()
        {
            FrmModelo Modelo = new FrmModelo();
            EProdTablaRegistro Obe = (EProdTablaRegistro)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe != null)
            {
                Modelo.CodeMarcas = Convert.ToInt32(Obe.icod_tabla);
                Modelo.Show();
            }
        }

        private void btnagregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnmodificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Datos();
        }

        private void btneliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnimprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void btnmodelo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modelo();
        }
    }

}