using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Producción;
using SGE.WindowForms.Producción.Caracteristicas;
using SGE.WindowForms.Ventas.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;


namespace SGE.WindowForms.Producción.Produccion
{
    public partial class FrmNotaPedido : XtraForm
    {
        private List<ENotaPedidoCab> mlist = new List<ENotaPedidoCab>();
        public FrmNotaPedido() => InitializeComponent();
        private void FrmMarca_Load(object sender, EventArgs e) => Carga();
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => nuevo();
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => Datos();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => eliminar();
        private void btnagregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => nuevo();
        private void btnmodificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => Datos();
        private void btneliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => eliminar();
        private void btnimprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => imprimir();
        private void btnmodelo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => modelo();
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e) => imprimir();
        public void Carga()
        {
            mlist = new BCentral().ListarNotaPedidoCab(Parametros.intEjercicio);
            dgrMotonave.DataSource = mlist;
        }
        void reload(int intIcod)
        {
            Carga();
            int index = mlist.FindIndex(x => x.nopec_iid_nota_pedido == intIcod);
            viewMotonave.FocusedRowHandle = index;
            viewMotonave.Focus();
        }

        private void nuevo()
        {
            FrmManteNotaPedido frm = new FrmManteNotaPedido();
            frm.MiEvento += new FrmManteNotaPedido.DelegadoMensaje(reload);
            frm.getNroDoc();
            frm.oDetail = mlist;
            frm.SetInsert();
            frm.Show();


        }

        private void Datos()
        {
            ENotaPedidoCab Obe = (ENotaPedidoCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmManteNotaPedido frm = new FrmManteNotaPedido();
            frm.MiEvento += new FrmManteNotaPedido.DelegadoMensaje(reload);
            frm.oBe = Obe;
            frm.oDetail = mlist;
            frm.SetModify();
            frm.setValues();
            frm.Show();
        }


        private void eliminar()
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                ENotaPedidoCab Obe = (ENotaPedidoCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                BCentral oBl = new BCentral();
                Obe.intUsuario = Valores.intUsuario;
                Obe.vpc = WindowsIdentity.GetCurrent().Name;
                oBl.EliminarNotaPedidoCab(Obe);
                viewMotonave.DeleteRow(viewMotonave.FocusedRowHandle);
                viewMotonave.RefreshData();
            }
        }

        private void imprimir()
        {
            var ObeNP = viewMotonave.GetFocusedRow() as ENotaPedidoCab;
            if (ObeNP is null) return;
            var lstNP = new BCentral().ListarNotaPedidoCab(Parametros.intEjercicio).Where(x => x.nopec_iid_nota_pedido == ObeNP.nopec_iid_nota_pedido).ToList();
            var Obe = lstNP.FirstOrDefault();
            var mlisdet = new BCentral().ListarNotaPedidoDet(Obe.nopec_iid_nota_pedido).OrderBy(x => x.noped_iitem_nota_pedido).ToList();
            mlisdet.ForEach(x =>
            {
                x.Codigo = "P" + "-" + Obe.nopec_icod_nota_pedido + "." + x.noped_iitem_nota_pedido;
                x.FichaTecnica = "FT" + "-" + x.strfichatecnica;
                x.Serie = x.resec_vdescripcion.Substring((x.resec_vdescripcion.Length - 2), 2);
            });

            rptNotaPedido rptFcatura = new rptNotaPedido();

            rptFcatura.cargar(Obe, mlisdet);
            rptFcatura.ShowPreview();

        }
        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                FrmManteNotaPedido Motonave = new FrmManteNotaPedido();
                Motonave.MiEvento += new FrmManteNotaPedido.DelegadoMensaje(reload);
                Motonave.Show();
                Motonave.SetCancel();
                ENotaPedidoCab Obe = (ENotaPedidoCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
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
                                                   obj.nopec_icod_nota_pedido.ToString().Contains(txtCodigo.Text.ToUpper())
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


    }

}