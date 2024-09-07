using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
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
    public partial class FrmRequerimientoMaterial : XtraForm
    {
        private List<ERequerimientoMaterial> mlist = new List<ERequerimientoMaterial>();
        public FrmRequerimientoMaterial() => InitializeComponent();
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e) => BuscarCriterio();
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e) => imprimir();
        private void btnagregar_ItemClick(object sender, ItemClickEventArgs e) => nuevo();
        private void btnmodificar_ItemClick(object sender, ItemClickEventArgs e) => Datos();
        private void btneliminar_ItemClick(object sender, ItemClickEventArgs e) => eliminar();
        private void btnimprimir_ItemClick(object sender, ItemClickEventArgs e) => imprimir();
        private void btnmodelo_ItemClick(object sender, ItemClickEventArgs e) => modelo();
        private void FrmMarca_Load(object sender, EventArgs e) => Carga();
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => nuevo();
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => Datos();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => eliminar();
        private void FrmMarca_Activated(object sender, EventArgs e) { }
        public void Carga()
        {
            BSControls.LoaderLookRepository(lkpGrdProceso, new BCentral().listarAreaProduccion(), "arprc_vdescripcion", "arprc_iid_codigo", false);
            ERequerimientoMaterial obj = new ERequerimientoMaterial();
            mlist = new BCentral().ListarRequerimientoMaterial(Parametros.intEjercicio);
            mlist.ForEach(x => x.pedido = "P-" + x.strpedido + "." + x.stritempedido);
            dgrMotonave.DataSource = mlist;
        }
        void reload(int intIcod)
        {
            Carga();
            int index = mlist.FindIndex(x => x.rqmac_iid_requerimiento_material == intIcod);
            viewMotonave.FocusedRowHandle = index;
            viewMotonave.Focus();
        }
        private void nuevo()
        {
            frmManteRequerimientoMaterial frm = new frmManteRequerimientoMaterial();
            frm.MiEvento += new frmManteRequerimientoMaterial.DelegadoMensaje(reload);
            frm.lstRequerimientoMaterial = mlist;
            frm.SetInsert();
            frm.Show();
            frm.txtNºRM.Text = (mlist.Count == 0) ? "0001" : String.Format("{0:0000}", mlist.Max(x => Convert.ToInt32(x.rqmac_icod_requerimiento_material)) + 1);
        }

        private void Datos()
        {
            var Obe = (ERequerimientoMaterial)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe == null) return;
            frmManteRequerimientoMaterial frm = new frmManteRequerimientoMaterial();
            frm.MiEvento += new frmManteRequerimientoMaterial.DelegadoMensaje(reload);
            frm.oBe = Obe;
            frm.lstRequerimientoMaterial = mlist;
            frm.SetModify();
            frm.Show();
        }


        private void eliminar()
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                ERequerimientoMaterial Obe = (ERequerimientoMaterial)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                BCentral oBl = new BCentral();
                Obe.intUsuario = Valores.intUsuario;
                Obe.vpc = WindowsIdentity.GetCurrent().Name;
                oBl.EliminarRequerimientoMaterial(Obe);
                viewMotonave.DeleteRow(viewMotonave.FocusedRowHandle);
                viewMotonave.RefreshData();
            }
        }

        private void imprimir()
        {
            var ObeOP = (ERequerimientoMaterial)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            var Obe = new BCentral().ListarOrdenProduccion(Parametros.intEjercicio).Where(x => x.orprc_iid_orden_produccion == ObeOP.rqmac_iorden_produccion).ToList().First();
            var mlisdet = new BCentral().listarOrdenPrduccionDetalle(Obe.orprc_iid_orden_produccion);
            mlisdet.ForEach(x => x.strProducto = string.IsNullOrEmpty(x.strProducto) ? x.orprd_vmaterial : x.strProducto);
            rptRequerimientoMateriales rptFcatura = new rptRequerimientoMateriales();
            rptFcatura.cargar(Obe, mlisdet);
            rptFcatura.ShowPreview();
        }
        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            ERequerimientoMaterial obe = (ERequerimientoMaterial)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (obe == null) return;
            frmManteRequerimientoMaterial frm = new frmManteRequerimientoMaterial();
            frm.oBe = obe;
            frm.setValues();
            frm.SetCancel();
            frm.Show();
        }

        private void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj =>
                                                   //obj.resec_vdescripcion.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.rqmac_icod_requerimiento_material.ToString().Contains(txtCodigo.Text.ToUpper())
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
            EProdTablaRegistro Obe = (EProdTablaRegistro)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe != null) return;
            FrmModelo Modelo = new FrmModelo();
            Modelo.CodeMarcas = Convert.ToInt32(Obe.icod_tabla);
            Modelo.Show();

        }

        private void ingresoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EOrdenProduccion Obe = (EOrdenProduccion)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe == null) return;
            if (Obe.orprc_iid_situacion == Parametros.intSitReporteProduccionGenerado)
            {
                FrmManteIngresoReporteProduccion Reporte = new FrmManteIngresoReporteProduccion();
                Reporte.MiEvento += new FrmManteIngresoReporteProduccion.DelegadoMensaje(Carga);
                Reporte.IdReporteProduccion = Obe.orprc_iid_orden_produccion;
                Reporte.oBe = Obe;
                Reporte.Show();
                Reporte.txtCodigo.Text = Obe.orprc_icod_orden_produccion.ToString();
                Reporte.txtCantidad.EditValue = Obe.orprc_ntotal;
                Reporte.dtmFecha.DateTime = Obe.orprc_sfecha_orden_produccion;
            }
            else
            {
                string sSituacion = "";
                if (Obe.orprc_iid_situacion == Parametros.intSitReporteProduccionActualizado) { sSituacion = "Actualizado"; }
                XtraMessageBox.Show("No se puede ingresar el producto al almacén \n" + "Situación : " + sSituacion, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void eliminarPTDelAlmacénToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EOrdenProduccion Obe = (EOrdenProduccion)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe.orprc_iid_kardex1 == 0) return;
            if (XtraMessageBox.Show("Desea eliminar el producto Terminado del almacén", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Obe.orprc_iid_situacion = Parametros.intSitReporteProduccionGenerado;
                new BCentral().EliminarPTKardex(Obe);
            }


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            reload(1);
        }
    }

}