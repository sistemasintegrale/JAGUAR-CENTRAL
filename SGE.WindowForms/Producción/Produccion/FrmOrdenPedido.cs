using DevExpress.XtraBars;
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
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Producción.Produccion
{
    public partial class FrmOrdenPedido : XtraForm
    {
        private List<EOrdenPedidoCab> mlist = new List<EOrdenPedidoCab>();

        public FrmOrdenPedido() => InitializeComponent();
        private void FrmMarca_Load(object sender, EventArgs e) => Carga();
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => nuevo();
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => Datos();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => eliminar();
        private void btnagregar_ItemClick(object sender, ItemClickEventArgs e) => nuevo();
        private void btnmodificar_ItemClick(object sender, ItemClickEventArgs e) => Datos();
        private void btneliminar_ItemClick(object sender, ItemClickEventArgs e) => eliminar();
        private void btnimprimir_ItemClick(object sender, ItemClickEventArgs e) => imprimir();
        private void btnmodelo_ItemClick(object sender, ItemClickEventArgs e) => modelo();
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e) => imprimir();
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e) => BuscarCriterio();
        private void FrmMarca_Activated(object sender, EventArgs e) { }
        public void Carga()
        {
            mlist = new BCentral().ListarOrdenPedidoCab(Parametros.intEjercicio);
            dgrMotonave.DataSource = mlist;
        }
        void reload(int intIcod)
        {
            Carga();
            int index = mlist.FindIndex(x => x.orpec_iid_orden_pedido == intIcod);
            viewMotonave.FocusedRowHandle = index;
            viewMotonave.Focus();
        }
        private void nuevo()
        {
            FrmManteOrdenPedido frm = new FrmManteOrdenPedido();
            frm.MiEvento += new FrmManteOrdenPedido.DelegadoMensaje(reload);
            frm.oDetail = mlist;
            frm.SetInsert();
            frm.Show();
        }

        private void Datos()
        {
            var Obe = viewMotonave.GetFocusedRow() as EOrdenPedidoCab;
            if (Obe == null) return;
            FrmManteOrdenPedido frm = new FrmManteOrdenPedido();
            frm.MiEvento += new FrmManteOrdenPedido.DelegadoMensaje(reload);
            frm.oBe = Obe;
            frm.oDetail = mlist;
            frm.SetModify();
            frm.setValues();
            frm.Show();
        }
        private void eliminar()
        {
            EOrdenPedidoCab Obe = (EOrdenPedidoCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe is null) return;
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
            
            
            Obe.intUsuario = Valores.intUsuario;
            Obe.vpc = WindowsIdentity.GetCurrent().Name;
            var Mlist = new BCentral().ListarOrdenPedidoDet(Obe.orpec_iid_orden_pedido);
            if (Mlist.Exists(x => x.orped_isituacion_entrega != (int)SituacionEntregaOrdenPedidoItem.Pendiente))
            {
                Services.MessageError($"No se puede eliminar OP, ya tiene entregas en el Item");
                return;
            }
            new BCentral().EliminarOrdenPedidoCab(Obe);
            viewMotonave.DeleteRow(viewMotonave.FocusedRowHandle);
            viewMotonave.RefreshData();

        }

        private void imprimir()
        {
            EOrdenPedidoCab Obe = (EOrdenPedidoCab)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            List<EOrdenPedidoCab> lstNP = new List<EOrdenPedidoCab>();
            List<EOrdenPedidoDet> mlisdet = new List<EOrdenPedidoDet>();
            Obe = new BCentral().ListarOrdenPedidoCab(Parametros.intEjercicio).Where(x => x.orpec_iid_orden_pedido == Obe.orpec_iid_orden_pedido).FirstOrDefault();
            mlisdet = new BCentral().ListarOrdenPedidoDet(Obe.orpec_iid_orden_pedido);
            mlisdet.OrderBy(x => x.orped_iitem_orden_pedido);
            mlisdet.ForEach(x =>
            {
                x.Codigo = "P" + "-" + Obe.orpec_icod_orden_pedido + "." + x.orped_iitem_orden_pedido;
                if (x.strfichatecnica == "" || x.strfichatecnica == null)
                {
                    x.FichaTecnica = x.strfichatecnica;
                }
                else
                {
                    x.FichaTecnica = "FT" + "-" + x.strfichatecnica;
                }
                x.Serie = x.resec_vdescripcion.Substring((x.resec_vdescripcion.Length - 2), 2);
            });

            rptOrdenPedido rptFcatura = new rptOrdenPedido();


            rptFcatura.cargar(Obe, mlisdet);
            rptFcatura.ShowPreview();


        }
        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count == 0) return;
            frmManteOrdenProdución Motonave = new frmManteOrdenProdución();
            Motonave.MiEvento += new frmManteOrdenProdución.DelegadoMensaje(reload);
            Motonave.Show();
            Motonave.SetCancel();
            EOrdenProduccion Obe = viewMotonave.GetFocusedRow() as EOrdenProduccion;
        }

        private void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj =>
                                                   //obj.resec_vdescripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.orpec_icod_orden_pedido.ToString().Contains(txtCodigo.Text.ToUpper())
                                             ).ToList();

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

        private void simpleButton1_Click(object sender, EventArgs e) => Carga();
    }

}