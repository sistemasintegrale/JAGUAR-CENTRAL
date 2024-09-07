using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
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

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class FrmEntregaMaterial : DevExpress.XtraEditors.XtraForm
    {
        private List<ERequerimientoMaterial> mlist = new List<ERequerimientoMaterial>();
        private int xposition = 0;

        public FrmEntregaMaterial()
        {
            InitializeComponent();
        }
        public void Carga()
        {
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
            frmManteEntregaMaterial frm = new frmManteEntregaMaterial();
            frm.MiEvento += new frmManteEntregaMaterial.DelegadoMensaje(reload);
            frm.lstRequerimientoMaterial = mlist;
            frm.SetInsert();
            frm.Show();
            frm.txtNºRM.Text = (mlist.Count == 0) ? "0001" : String.Format("{0:0000}", mlist.Count + 1);

        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos();
        }
        private void Datos()
        {
            ERequerimientoMaterial Obe = (ERequerimientoMaterial)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteEntregaMaterial frm = new frmManteEntregaMaterial();
            frm.MiEvento += new frmManteEntregaMaterial.DelegadoMensaje(reload);
            frm.oBe = Obe;
            frm.lstRequerimientoMaterial = mlist;
            frm.SetModify();
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
                List<EOrdenProduccion> mlistproducto = new List<EOrdenProduccion>();
                EOrdenProduccion Obe = (EOrdenProduccion)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                BCentral oBl = new BCentral();
                Obe.intUsuario = Valores.intUsuario;
                Obe.vpc = WindowsIdentity.GetCurrent().Name;
                oBl.EliminarOrdenProduccion(Obe);
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
            EOrdenProduccion ObeOP = (EOrdenProduccion)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            List<EOrdenProduccion> lstOP = new List<EOrdenProduccion>();
            EOrdenProduccion Obe = new EOrdenProduccion();
            List<EOrdenProduccionDet> mlisdet = new List<EOrdenProduccionDet>();

            lstOP = new BCentral().ListarOrdenProduccion(Parametros.intEjercicio).Where(x => x.orprc_iid_orden_produccion == ObeOP.orprc_iid_orden_produccion).ToList();
            Obe = lstOP.FirstOrDefault();

            mlisdet = new BCentral().listarOrdenPrduccionDetalle(Obe.orprc_iid_orden_produccion);
            //mlisdet.OrderBy(x => x.orped_iitem_orden_pedido);
            //mlisdet.ForEach(x =>
            //{
            //    x.Codigo = "P" + "-" + Obe.orpec_icod_orden_pedido + "." + x.orped_iitem_orden_pedido;
            //    x.FichaTecnica = "FT" + "-" + x.strfichatecnica;
            //    x.Serie = x.resec_vdescripcion.Substring((x.resec_vdescripcion.Length - 2), 2);
            //    x.imagen = x.orped_vruta;
            //});

            rptOrdenProduccion rptFcatura = new rptOrdenProduccion();

            rptFcatura.cargar(Obe, mlisdet);
            rptFcatura.ShowPreview();
        }
        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                frmManteOrdenProdución Motonave = new frmManteOrdenProdución();
                Motonave.MiEvento += new frmManteOrdenProdución.DelegadoMensaje(reload);
                Motonave.Show();
                Motonave.SetCancel();
                EOrdenProduccion Obe = (EOrdenProduccion)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                //Motonave.txtcodigo.Text = Obe.orprc_icod_orden_produccion;
                //Motonave.txtdescripcion.Text = Obe.resec_vdescripcion;
                //Motonave.btnGuardar.Enabled = false;
                //Motonave.txtcodigo.Enabled = false;
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
                                                   obj.rqmac_icod_requerimiento_material.ToString().Contains(txtCodigo.Text.ToUpper())
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

        private void ingresoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mlist.Count > 0)
                {
                    int IdSituacion = int.Parse(viewMotonave.GetFocusedRowCellValue("orprc_iid_situacion").ToString());
                    if (IdSituacion == Parametros.intSitReporteProduccionGenerado)
                    {
                        FrmManteIngresoReporteProduccion Reporte = new FrmManteIngresoReporteProduccion();
                        Reporte.MiEvento += new FrmManteIngresoReporteProduccion.DelegadoMensaje(form2_MiEvento);
                        EOrdenProduccion Obe = (EOrdenProduccion)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
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
                        if (IdSituacion == Parametros.intSitReporteProduccionActualizado) { sSituacion = "Actualizado"; }
                        XtraMessageBox.Show("No se puede ingresar el producto al almacén \n" + "Situación : " + sSituacion, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarPTDelAlmacénToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EOrdenProduccion Obe = (EOrdenProduccion)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe.orprc_iid_kardex1 != 0)
            {
                if (XtraMessageBox.Show("Desea eliminar el producto Terminado del almacén", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.orprc_iid_situacion = Parametros.intSitReporteProduccionGenerado;
                    new BCentral().EliminarPTKardex(Obe);
                }

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Carga();
        }
    }

}