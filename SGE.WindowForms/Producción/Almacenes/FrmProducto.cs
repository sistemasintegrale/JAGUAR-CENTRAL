using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Almacenes
{
    public partial class FrmProducto : DevExpress.XtraEditors.XtraForm
    {
        private List<EProdProducto> mlist = new List<EProdProducto>();
        private int xposition = 0;
        public int cod_resp = 0;
        public FrmProducto() => InitializeComponent();
        private void simpleButton1_Click(object sender, EventArgs e) => Carga();
        private void btnagregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => nuevo();
        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => Datos();
        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => eliminar();
        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => imprimir();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => eliminar();
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e) => imprimir();
        void form2_MiEvento() => Carga();
        public void Carga()
        {
            mlist = new BCentral().ListarProdProductoByMarcaModelo(Convert.ToInt32(LkpMarca.EditValue), Convert.ToInt32(btnmodelo.Tag)).ToList();

            gridControl1.DataSource = mlist.OrderBy(x => x.pr_vcodigo_externo);
            gridView1.BestFitColumns();
            gridControl1.RefreshDataSource();
        }

        void Modify()
        {
            Carga();
            gridView1.FocusedRowHandle = xposition;
        }
        private void FrmProducto_Load(object sender, EventArgs e)
        {
            cargarGrdControles();
            var lstMarca = new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro() { iid_tipo_tabla = 11 });
            lstMarca.Insert(0, new EProdTablaRegistro { descripcion = "TODOS", tarec_viid_correlativo = "0" });
            BSControls.LoaderLook(LkpMarca, lstMarca, "descripcion", "tarec_viid_correlativo", true);
        }

        private async void cargarGrdControles()
        {
            var data = await Task.WhenAll(cargarGrdLkp());
            BSControls.LoaderLookRepository(lkpGrdMarca, data[0].Item1, "descripcion", "icod_tabla", true);
            BSControls.LoaderLookRepository(lkpGrdColor, data[0].Item2, "descripcion", "tarec_iid_correlativo", true);
            BSControls.LoaderLookRepository(lkpGrdTalla, data[0].Item3, "descripcion", "tarec_iid_correlativo", true);
            BSControls.LoaderLookRepository(lkpgrdUm, data[0].Item4, "descripcion", "id_unidad_medida", true);
        }

        async Task<Tuple<object, object, object, object>> cargarGrdLkp()
        {
            var lst1 = await Task.Run(() => new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro { iid_tipo_tabla = Parametros.intMarca }));
            var lst2 = await Task.Run(() => new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro { iid_tipo_tabla = Parametros.intColor }));
            var lst3 = await Task.Run(() => new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro { iid_tipo_tabla = Parametros.intTalla }));
            var lst4 = await Task.Run(() => new BCentral().ListarProdUnidadMedida());

            return new Tuple<object, object, object, object>(lst1, lst2, lst3, lst4);
        }

        private void nuevo()
        {
            FrmManteProducto Motonave = new FrmManteProducto();
            Motonave.MiEvento += new FrmManteProducto.DelegadoMensaje(form2_MiEvento);
            Motonave.Correlative = new BCentral().ProductoPvtGetMaxID();
            Motonave.oDetail = mlist;
            Motonave.Show();
            Motonave.txtcodigo.Enabled = true;
            Motonave.chkIGV.Checked = true;
            Motonave.txtPorcentajeIGV.Text = Parametros.strPorcIGV;
            Motonave.SetInsert();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => nuevo();
        private void Datos()
        {

            EProdProducto oBe = (EProdProducto)gridView1.GetRow(gridView1.FocusedRowHandle);
            if (oBe is null) return;
            oBe = new BCentral().ProductoPvtGetByID(oBe.pr_icod_producto);
            FrmManteProducto Motonave = new FrmManteProducto();
            Motonave.MiEvento += new FrmManteProducto.DelegadoMensaje(Modify);
            Motonave.oDetail = mlist;
            Motonave.Show();
            Motonave.Correlative = oBe.pr_icod_producto;
            Motonave.LkpMarca.EditValue = string.Format("{0:000}", oBe.pr_iid_marca);
            Motonave.btnmodelo.Tag = string.Format("{0:0000}", oBe.pr_iid_modelo);
            Motonave.btnmodelo.Text = oBe.pr_viid_modelo;
            Motonave.LkpColor.EditValue = string.Format("{0:000}", oBe.pr_iid_color);
            Motonave.LkpTalla.EditValue = string.Format("{0:000}", oBe.pr_iid_talla);
            Motonave.txtcodigo.Text = oBe.pr_vcodigo_externo;
            Motonave.txtdescripcion.Text = oBe.pr_vdescripcion_producto;
            Motonave.LkpUnidad.EditValue = oBe.unidc_icod_unidad_medida;
            Motonave.txtabreviatura.Text = oBe.pr_vabreviatura_producto;
            Motonave.pr_tarec_icorrelativo = oBe.pr_tarec_icorrelativo;
            Motonave.chkIGV.Checked = Convert.ToBoolean(oBe.pr_afecto_igv);
            Motonave.txtPorcentajeIGV.Text = oBe.pr_nporcentaje_igv.ToString();
            Motonave.lkpSerie.EditValue = oBe.pr_icod_serie;
            Motonave.txtgrupo.Text = oBe.resec_vtalla_inicial;
            Motonave.txtgrupo2.Text = oBe.resec_vtalla_final;
            Motonave.SetModify();
            Motonave.LkpMarca.Enabled = false;
            Motonave.btnmodelo.Enabled = false;
            Motonave.LkpColor.Enabled = false;
            Motonave.LkpTalla.Enabled = false;
            Motonave.LkpUnidad.Enabled = false;
            Motonave.txtcodigo.Enabled = false;
            Motonave.lkpSerie.Enabled = false;
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => Datos();
        private void eliminar()
        {
            if (Services.MessageQuestion("Desea Eliminar el registro?") == DialogResult.No) return;
            EProdProducto Obe = (EProdProducto)gridView1.GetRow(gridView1.FocusedRowHandle);
            int cantidad = new BCentral().VerificarProdProductoEnkardex(Obe.pr_icod_producto);
            if (cantidad == 0)
            {
                BCentral oBl = new BCentral();
                oBl.EliminarProdProductos(Obe);
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
                gridView1.RefreshData();
            }
            else
            {
                XtraMessageBox.Show("No se Puede Eliminar: Existen Movimientos con Este Producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void imprimir()
        {

            rptProducto rptProducto = new rptProducto();
            rptProducto.cargar(mlist);

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                FrmManteProducto Motonave = new FrmManteProducto();
                Motonave.MiEvento += new FrmManteProducto.DelegadoMensaje(Modify);
                Motonave.oDetail = mlist;
                Motonave.Show();
                EProdProducto oBe = (EProdProducto)gridView1.GetRow(gridView1.FocusedRowHandle);
                Motonave.Correlative = oBe.pr_icod_producto;
                Motonave.LkpMarca.EditValue = string.Format("{0:000}", oBe.pr_iid_marca);
                Motonave.btnmodelo.Tag = string.Format("{0:0000}", oBe.pr_iid_modelo);
                Motonave.btnmodelo.Text = oBe.pr_viid_modelo;
                Motonave.LkpColor.EditValue = string.Format("{0:000}", oBe.pr_iid_color);
                Motonave.LkpTalla.EditValue = string.Format("{0:000}", oBe.pr_iid_talla);
                Motonave.txtcodigo.Text = oBe.pr_vcodigo_externo;
                Motonave.txtdescripcion.Text = oBe.pr_vdescripcion_producto;
                Motonave.LkpUnidad.EditValue = oBe.unidc_icod_unidad_medida;
                Motonave.txtabreviatura.Text = oBe.pr_vabreviatura_producto;
                Motonave.pr_tarec_icorrelativo = oBe.pr_tarec_icorrelativo;
                Motonave.SetCancel();
                Motonave.LkpMarca.Enabled = false;
                Motonave.comboBoxEdit1.Enabled = false;
                Motonave.btnmodelo.Enabled = false;
                Motonave.LkpColor.Enabled = false;
                Motonave.LkpTalla.Enabled = false;
                Motonave.LkpUnidad.Enabled = false;
                Motonave.txtcodigo.Enabled = false;
                Motonave.btnGuardar.Enabled = false;
            }
            xposition = gridView1.FocusedRowHandle;
        }


        private void BuscarCriterio()
        {
            gridControl1.DataSource = mlist.Where(obj =>
                                                   obj.pr_vdescripcion_producto.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.pr_vcodigo_externo.ToString().Contains(txtCodigo.Text.ToUpper())
                                             ).ToList();

        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
                nuevoToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F5)
                Datos();
            if (e.KeyCode == Keys.F9)
                eliminarToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F8) { imprimir(); }

        }



        private void btnmodelo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            List<EProdTablaRegistro> listRegistro = new List<EProdTablaRegistro>();
            EProdTablaRegistro ob = new EProdTablaRegistro();
            EProdModelo obje = new EProdModelo();
            ob.iid_tipo_tabla = 11;
            listRegistro = new BCentral().ListarProdTablaRegistro(ob).Where(obj => obj.tarec_viid_correlativo == LkpMarca.EditValue.ToString()).ToList();
            listRegistro.ForEach(obe =>
            {
                obje.tarec_icorrelativo = obe.icod_tabla;//ver

            });
            using (ListarModelo listmodelo = new ListarModelo())
            {
                listmodelo.CodeMarcas = Convert.ToInt32(obje.tarec_icorrelativo);
                if (listmodelo.ShowDialog() == DialogResult.OK)
                {

                    btnmodelo.Tag = string.Format("{0:0000}", listmodelo._Be.mo_iid_modelo);
                    btnmodelo.Text = listmodelo._Be.mo_vdescripcion;
                    cod_resp = listmodelo._Be.mo_icod_modelo;
                }
            }
        }







    }
}