using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using SGE.WindowForms.Otros.Producción;
using SGE.WindowForms.Producción.Caracteristicas;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Produccion
{
    public partial class FrmRegistroPersonal : DevExpress.XtraEditors.XtraForm
    {
        private List<EPVTPersonal> mlist = new List<EPVTPersonal>();
        public EPVTPersonal _Be = new EPVTPersonal();


        public FrmRegistroPersonal()
        {
            InitializeComponent();
        }
        public void Carga()
        {

            mlist = new BCentral().ListarRegistroPersonal();
            dgrMotonave.DataSource = mlist;
        }
        void reload(int intIcod)
        {
            Carga();
            int index = mlist.FindIndex(x => x.perc_iid_personal == intIcod);
            viewMotonave.FocusedRowHandle = index;
            viewMotonave.Focus();
        }
        void form2_MiEvento()
        {
            Carga();
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
            frmManteRegistroPersonal frm = new frmManteRegistroPersonal();
            frm.MiEvento += new frmManteRegistroPersonal.DelegadoMensaje(reload);
            frm.lstRegistroPersonal = mlist;
            frm.SetInsert();
            frm.Show();
            //frm.txtNrOrden.Focus();

        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos();
        }
        public int iid_analitica;
        private void Datos()
        {
            EPVTPersonal Obe = (EPVTPersonal)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteRegistroPersonal frm = new frmManteRegistroPersonal();
            frm.MiEvento += new frmManteRegistroPersonal.DelegadoMensaje(reload);
            frm.oBe = Obe;
            frm.txtCodigo.Enabled = false;
            frm.lstRegistroPersonal = mlist;
            frm.SetModify();
            frm.Show();
            frm.anac_icod_analitica = iid_analitica;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void eliminar()
        {
            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                //List<EPVTPersonal> mlistproducto = new List<EPVTPersonal>();
                EPVTPersonal Obe = (EPVTPersonal)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                BCentral oBl = new BCentral();
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.perc_icod_analitica = Obe.perc_icod_analitica;
                oBl.EliminarRegistroPersonal(Obe);
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
            viewMotonave.Columns["perc_vcod_personal"].FilterInfo = new ColumnFilterInfo("[perc_vcod_personal] LIKE '%" + txtCodigo.Text + "%'");
            viewMotonave.Columns["perc_vnombres_apellidos"].FilterInfo = new ColumnFilterInfo("[perc_vnombres_apellidos] LIKE '%" + txtDescripcion.Text + "%'");

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


    }

}