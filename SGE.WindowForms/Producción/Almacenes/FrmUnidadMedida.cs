using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Almacenes
{
    public partial class FrmUnidadMedida : DevExpress.XtraEditors.XtraForm
    {
        private List<EProdUnidadMedida> mlist = new List<EProdUnidadMedida>();
        private int xposition = 0;

        public FrmUnidadMedida()
        {
            InitializeComponent();
        }
        private void FrmListarUnidadMedida_Load(object sender, EventArgs e)
        {
            Carga();
        }

        void form2_MiEvento()
        {
            Carga();
        }

        private void Carga()
        {
            mlist = new BCentral().ListarProdUnidadMedida();
            dgrUnidadMedida.DataSource = mlist;
        }
        void Modify()
        {
            Carga();
            viewUnidadMedida.FocusedRowHandle = xposition;
        }

        private void FrmUnidadMedida_Load(object sender, EventArgs e)
        {
            Carga();
        }
        private void nuevo()
        {
            FrmMantUnidadMedida Unidad = new FrmMantUnidadMedida();
            Unidad.MiEvento += new FrmMantUnidadMedida.DelegadoMensaje(form2_MiEvento);
            viewUnidadMedida.MoveLast();
            EProdUnidadMedida Obe = (EProdUnidadMedida)viewUnidadMedida.GetRow(viewUnidadMedida.FocusedRowHandle);
            int i = 0;
            if (mlist.Count > 0)
                i = mlist.Max(ob => Convert.ToInt32(Obe.idf_unidad_medida));

            Unidad.Correlative = Convert.ToInt32(i) + 1;
            Unidad.oDetail = mlist;
            Unidad.Show();
            Unidad.txtCodigo.Enabled = true;
            Unidad.SetInsert();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void viewUnidadMedida_DoubleClick(object sender, EventArgs e)
        {
            FrmMantUnidadMedida Unidad = new FrmMantUnidadMedida();
            Unidad.MiEvento += new FrmMantUnidadMedida.DelegadoMensaje(form2_MiEvento);

            Unidad.SetCancel();
            EProdUnidadMedida Obe = (EProdUnidadMedida)viewUnidadMedida.GetRow(viewUnidadMedida.FocusedRowHandle);
            if (Obe == null)
            {
                Unidad.Dispose();
                return;
            }
            Unidad.Show();
            Unidad.Correlative = Obe.id_unidad_medida;
            Unidad.txtCodigo.Text = Obe.idf_unidad_medida.ToString();
            Unidad.txtDescripcion.Text = Obe.descripcion;
            Unidad.txtAbreviatura.Text = Obe.abreviatura_unidad_medida;
            Unidad.btnGuardar.Enabled = false;
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos();
        }
        private void Datos()
        {
            FrmMantUnidadMedida Unidad = new FrmMantUnidadMedida();
            Unidad.MiEvento += new FrmMantUnidadMedida.DelegadoMensaje(Modify);
            Unidad.oDetail = mlist;
            Unidad.Show();
            EProdUnidadMedida Obe = (EProdUnidadMedida)viewUnidadMedida.GetRow(viewUnidadMedida.FocusedRowHandle);
            Unidad.Correlative = Obe.id_unidad_medida;
            Unidad.txtCodigo.Text = Obe.idf_unidad_medida.ToString();
            Unidad.txtDescripcion.Text = Obe.descripcion;
            Unidad.txtAbreviatura.Text = Obe.abreviatura_unidad_medida;
            Unidad.txtCodigoSUNAT.Text = Obe.unidc_vcodigo_sunat;
            Unidad.txtCodigo.Enabled = false;
            Unidad.SetModify();
            Unidad.txtAbreviatura.Enabled = false;
            xposition = viewUnidadMedida.FocusedRowHandle;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();

        }
        private void eliminar()
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                EProdUnidadMedida Obe = (EProdUnidadMedida)viewUnidadMedida.GetRow(viewUnidadMedida.FocusedRowHandle);
                BCentral oBl = new BCentral();
                oBl.EliminarProdUnidadMedida(Obe.id_unidad_medida);
                viewUnidadMedida.DeleteRow(viewUnidadMedida.FocusedRowHandle);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }
        private void imprimir()
        {
            //if (mlist.Count > 0)
            //{
            //    rptRegistroCaracteristicas rpt = new rptRegistroCaracteristicas();
            //    rpt.cargar(mlist, "UNIDAD DE MEDIDA");
            //}
        }

        private void FrmUnidadMedida_Activated(object sender, EventArgs e)
        {
            //((FrmMain)MdiParent).oInstance = this;
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        private void BuscarCriterio()
        {
            dgrUnidadMedida.DataSource = mlist.Where(obj =>
                                                   obj.descripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.idf_unidad_medida.ToUpper().Contains(txtCodigo.Text.ToUpper())
                                             ).ToList();

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

    }
}