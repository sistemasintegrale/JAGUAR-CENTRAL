using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Reportes.Almacen.Registros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class FrmRegistroCaracteristicasSubCategoria : DevExpress.XtraEditors.XtraForm
    {
        private List<ECategoria> mlist = new List<ECategoria>();
        private int xposition = 0;
        public int tablc_iid_tipo_tabla = 0;
        public string vRegistroCaracteristicas;

        public FrmRegistroCaracteristicasSubCategoria()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            ECategoria obj = new ECategoria() { CatNUno_iid_tabla_registro = tablc_iid_tipo_tabla };
            mlist = new BAlmacen().ListarCategoriaSubDos(obj);
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

        private void FrmColor_Load(object sender, EventArgs e)
        {
            Carga();
        }
        private void nuevo()
        {

            FrmManteTipoSubCate Motonave = new FrmManteTipoSubCate();
            Motonave.MiEvento += new FrmManteTipoSubCate.DelegadoMensaje(form2_MiEvento);
            viewMotonave.MoveLast();
            int i = 0;
            if (mlist.Count > 0)
                i = mlist.Max(ob => ob.CatNUno_icorrelativo_registro);
            Motonave.Correlative = Convert.ToInt32(i) + 1;
            Motonave.oDetail = mlist;
            Motonave.TipoRegistro = tablc_iid_tipo_tabla;
            Motonave.NombreFormulario = vRegistroCaracteristicas;
            Motonave.Show();
            Motonave.txtcodigo.Enabled = true;
            Motonave.SetInsert();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos();
        }
        private void Datos()
        {
            ECategoria Obe = (ECategoria)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe != null)
            {
                FrmManteTipoSubCate Motonave = new FrmManteTipoSubCate();
                Motonave.MiEvento += new FrmManteTipoSubCate.DelegadoMensaje(Modify);
                Motonave.oDetail = mlist;
                Motonave.TipoRegistro = tablc_iid_tipo_tabla;
                Motonave.NombreFormulario = vRegistroCaracteristicas;
                Motonave.Show();
                Motonave.Correlative = Obe.CatNDos_iid_tabla_registro;
                Motonave.txtcodigo.Text = Obe.tarec_viid_correlativo;
                Motonave.txtabreviatura.Text = Obe.CatNDos_vvalor_texto;
                Motonave.txtdescripcion.Text = Obe.CatNDos_vdescripcion;
                Motonave.txtcodigo.Enabled = false;
                Motonave.SetModify();
                xposition = viewMotonave.FocusedRowHandle;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void eliminar()
        {
            ECategoria Obe = (ECategoria)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe != null)
            {
                if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    new BAlmacen().EliminarCategoriaSubDos(Obe);
                    mlist.Remove(Obe);

                    viewMotonave.RefreshData();
                }
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
                rpt.cargar(mlist, vRegistroCaracteristicas);
            }
        }

        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                FrmManteTipoSubCate Motonave = new FrmManteTipoSubCate();
                Motonave.MiEvento += new FrmManteTipoSubCate.DelegadoMensaje(AddEvent);
                Motonave.NombreFormulario = vRegistroCaracteristicas;
                Motonave.Show();
                Motonave.SetCancel();
                ECategoria Obe = (ECategoria)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                Motonave.txtcodigo.Text = Obe.tarec_viid_correlativo;
                Motonave.txtdescripcion.Text = Obe.CatNDos_vdescripcion;
                Motonave.BtnGuardar.Enabled = false;
                Motonave.txtcodigo.Enabled = false;
            }
            this.viewMotonave.DoubleClick -= new System.EventHandler(this.viewMotonave_DoubleClick);
        }



        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        private void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj =>
                                                   obj.CatNDos_vdescripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.tarec_viid_correlativo.ToString().Contains(txtCodigo.Text.ToUpper())
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

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Datos();
        }

        private void btnEliminar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnImprimir_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}