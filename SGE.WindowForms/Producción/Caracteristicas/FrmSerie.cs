using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Producción;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Caracteristicas
{
    public partial class FrmSerie : DevExpress.XtraEditors.XtraForm
    {
        private List<ERegistroSerie> mlist = new List<ERegistroSerie>();
        private int xposition = 0;

        public FrmSerie()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            ERegistroSerie obj = new ERegistroSerie();
            mlist = new BCentral().ListarRegistroSerie();
            dgrMotonave.DataSource = mlist;
        }
        void reload(int intIcod)
        {
            Carga();
            int index = mlist.FindIndex(x => x.resec_iid_registro_serie == intIcod);
            viewMotonave.FocusedRowHandle = index;
            viewMotonave.Focus();
        }
        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();

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
            frmManteSeries frm = new frmManteSeries();
            frm.MiEvento += new frmManteSeries.DelegadoMensaje(reload);
            frm.lstregistroserie = mlist;
            frm.SetInsert();
            frm.Show();
            frm.txtcodigo.Focus();

        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos();
        }
        private void Datos()
        {
            ERegistroSerie Obe = (ERegistroSerie)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteSeries frm = new frmManteSeries();
            frm.MiEvento += new frmManteSeries.DelegadoMensaje(reload);
            frm.oBe = Obe;
            frm.lstregistroserie = mlist;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtdescripcion.Focus();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void eliminar()
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                List<ERegistroSerie> mlistproducto = new List<ERegistroSerie>();
                ERegistroSerie Obe = (ERegistroSerie)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                BCentral oBl = new BCentral();
                Obe.intUsuario = Valores.intUsuario;
                Obe.vpc = WindowsIdentity.GetCurrent().Name;
                oBl.EliminarRegistroSerie(Obe);
                viewMotonave.DeleteRow(viewMotonave.FocusedRowHandle);
                viewMotonave.RefreshData();
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
                //rptRegistroCaracteristicas rpt = new rptRegistroCaracteristicas();
                //rpt.cargar(mlist, "Serie");
            }
        }
        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                frmManteSeries Motonave = new frmManteSeries();
                Motonave.MiEvento += new frmManteSeries.DelegadoMensaje(reload);
                Motonave.Show();
                Motonave.SetCancel();
                ERegistroSerie Obe = (ERegistroSerie)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                Motonave.txtcodigo.Text = Obe.resec_icod_registro_serie;
                Motonave.txtdescripcion.Text = Obe.resec_vdescripcion;
                //Motonave.btnGuardar.Enabled = false;
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
                                                   obj.resec_vdescripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.resec_icod_registro_serie.ToString().Contains(txtCodigo.Text.ToUpper())
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