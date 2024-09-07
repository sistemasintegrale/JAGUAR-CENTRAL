using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Producción;
using SGE.WindowForms.Ventas.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Produccion
{
    public partial class FrmFichaTecnica : DevExpress.XtraEditors.XtraForm
    {
        public List<EFichaTecnicaCab> mlist = new List<EFichaTecnicaCab>();
        private int xposition = 0;

        public FrmFichaTecnica()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            EFichaTecnicaCab obj = new EFichaTecnicaCab();
            mlist = new BCentral().ListarFichaTecnicaCab(Parametros.intEjercicio);

            dgrMotonave.DataSource = mlist;
            //viewMotonave.ActiveFilterString = "([fitec_icorrelativo_contramuestra] = 0)";


        }
        void reload(int intIcod)
        {
            Carga();
            int index = mlist.FindIndex(x => x.fitec_iid_ficha_tecnica == intIcod);
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
            var filtro = viewMotonave.RowFilter;
            FrmManteFichaTecnica frm = new FrmManteFichaTecnica();
            frm.MiEvento += new FrmManteFichaTecnica.DelegadoMensaje(reload);
            frm.oDetail = mlist;
            frm.SetInsert();
            frm.Show();


        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos();
        }
        private void Datos()
        {
            EFichaTecnicaCab Obe = (EFichaTecnicaCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmManteFichaTecnica frm = new FrmManteFichaTecnica();
            frm.MiEvento += new FrmManteFichaTecnica.DelegadoMensaje(reload);
            frm.oBe = Obe;
            frm.oDetail = mlist;
            frm.SetModify();
            frm.setValues();
            frm.Show();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void eliminar()
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                List<EFichaTecnicaCab> mlistproducto = new List<EFichaTecnicaCab>();
                EFichaTecnicaCab Obe = (EFichaTecnicaCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                BCentral oBl = new BCentral();
                Obe.intUsuario = Valores.intUsuario;
                Obe.vpc = WindowsIdentity.GetCurrent().Name;
                oBl.EliminarFichaTecnicaCab(Obe);
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
            EFichaTecnicaCab ObeNP = (EFichaTecnicaCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            List<EFichaTecnicaCab> lstNP = new List<EFichaTecnicaCab>();
            EFichaTecnicaCab Obe = new EFichaTecnicaCab();
            List<EFichaTecnicaDet> mlisdet = new List<EFichaTecnicaDet>();

            lstNP = new BCentral().ListarFichaTecnicaCab(Parametros.intEjercicio).Where(x => x.fitec_iid_ficha_tecnica == ObeNP.fitec_iid_ficha_tecnica).ToList();
            ObeNP = lstNP.FirstOrDefault();
            ObeNP.imagen = Image.FromStream(WebDavTest.Get($"{Rutas.RutaImagenes}{Rutas.RutaFichaTecnica}{ObeNP.fitec_iid_ficha_tecnica}/", $"{ObeNP.fitec_iid_ficha_tecnica}.png"));
            mlisdet = new BCentral().ListarFichaTecnicaDet(ObeNP.fitec_iid_ficha_tecnica);

            rptFichaTecnicaGRUPO rptFcatura = new rptFichaTecnicaGRUPO();

            rptFcatura.cargar(ObeNP, mlisdet);
            rptFcatura.ShowPreview();
        }
        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                EFichaTecnicaCab Obe = (EFichaTecnicaCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                if (Obe == null)
                    return;
                FrmManteFichaTecnica frm = new FrmManteFichaTecnica();
                frm.MiEvento += new FrmManteFichaTecnica.DelegadoMensaje(reload);
                frm.oBe = Obe;
                frm.oDetail = mlist;
                frm.SetModify();
                frm.setValues();
                frm.dobleclick();
                frm.Show();
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
                                                   //obj.resec_vdescripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.fitec_icod_ficha_tecnica.ToString().Contains(txtCodigo.Text.ToUpper())
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
            //FrmModelo Modelo = new FrmModelo();
            //EProdTablaRegistro Obe = (EProdTablaRegistro)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            //if (Obe != null)
            //{
            //    Modelo.CodeMarcas = Convert.ToInt32(Obe.icod_tabla);
            //    Modelo.Show();
            //}
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

        private void contramuestraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contramuestra();

        }

        private void contramuestra()
        {
            try
            {
                EFichaTecnicaCab Obe = (EFichaTecnicaCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                if (Obe == null)
                    return;
                if (Obe.fitec_icorrelativo_contramuestra != 0)
                {
                    throw new ArgumentException(String.Format("En Contramuestra no se puede generar"));
                }

                FrmManteFichaTecnicaContramuestra frm = new FrmManteFichaTecnicaContramuestra();
                frm.MiEvento += new FrmManteFichaTecnicaContramuestra.DelegadoMensaje(reload);
                frm.txtContramuestra.Text = String.Format("{0:0}", mlist.Where(x => x.fitec_icod_ficha_tecnica == Obe.fitec_icod_ficha_tecnica).Max(x => x.fitec_icorrelativo_contramuestra + 1)); ;
                frm.oBe = Obe;
                frm.oDetail = mlist;
                frm.SetInsert();
                frm.nuevo = true;
                frm.setValues();
                frm.Show();
                frm.simpleButton1.Enabled = true;
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgrMotonave_DoubleClick(object sender, EventArgs e)
        {
            EFichaTecnicaCab Obe = (EFichaTecnicaCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmManteFichaTecnica frm = new FrmManteFichaTecnica();
            frm.MiEvento += new FrmManteFichaTecnica.DelegadoMensaje(reload);
            frm.oBe = Obe;
            frm.oDetail = mlist;
            frm.SetModify();
            frm.setValues();
            frm.dobleclick();
            frm.Show();
        }

        private void contramuestraToolStripMenuItem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            contramuestra();
        }

        private void reporteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

}