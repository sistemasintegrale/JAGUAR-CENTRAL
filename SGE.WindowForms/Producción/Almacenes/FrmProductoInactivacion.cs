using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Almacenes
{
    public partial class FrmProductoInactivacion : XtraForm
    {
        private List<EProdProducto> mlist = new List<EProdProducto>();

        public int cod_resp = 0;
        public FrmProductoInactivacion() => InitializeComponent();
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e) => BuscarCriterio();
        private void simpleButton1_Click(object sender, EventArgs e) => Carga();
        public void Carga()
        {
            mlist = new BCentral().ListarProdProductoByMarcaModeloStock(Convert.ToInt32(LkpMarca.EditValue), Convert.ToInt32(btnmodelo.Tag), Parametros.intEjercicio).ToList();
            mlist.ForEach(x => x.Select = true);
            grd.DataSource = mlist.OrderBy(x => x.pr_vcodigo_externo);
            gridView1.BestFitColumns();
            grd.RefreshDataSource();
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

        private void BuscarCriterio()
        {
            grd.DataSource = mlist.Where(obj =>
                                                   obj.pr_vdescripcion_producto.ToUpper().Contains(textEdit1.Text.ToUpper()) &&
                                                   obj.pr_vcodigo_externo.ToString().Contains(txtCodigo.Text.ToUpper())
                                             ).ToList();

        }


        private void btnmodelo_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            EProdTablaRegistro ob = new EProdTablaRegistro();
            EProdModelo obje = new EProdModelo();
            ob.iid_tipo_tabla = 11;
            var listRegistro = new BCentral().ListarProdTablaRegistro(ob).Where(obj => obj.tarec_viid_correlativo == LkpMarca.EditValue.ToString()).ToList();
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



        private void desactivarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lista = Services.ObtenerElementosFiltrados<EProdProducto>(gridView1);

            foreach (var item in lista.Where(x => x.Select).ToList())
            {
                int cantidad = new BCentral().VerificarProdProductoEnkardex(item.pr_icod_producto);
                if (cantidad == 0)
                {
                    new BCentral().EliminarProdProductos(item);

                }
                else
                {
                    decimal stock = new BAlmacen().StockProductoPorAnio(Parametros.intEjercicio, item.pr_icod_producto);
                    if (stock == 0)
                    {
                        new BCentral().EliminarProdProductos(item);

                    }
                    else
                    {
                        var oBe = new BCentral().ProductoPvtGetByID(item.pr_icod_producto);
                        oBe.pr_isituacion_producto = 0;
                        oBe.intUsuario = Valores.intUsuario;
                        new BCentral().ActualizarProdProductos(oBe);
                    }

                }
            }
            Carga();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            mlist.ForEach(x => x.Select = false);
            grd.RefreshDataSource();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            mlist.ForEach(x => x.Select = true);
            grd.RefreshDataSource();
        }
    }
}