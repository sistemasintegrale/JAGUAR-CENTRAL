using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmListarProductosDeStock : DevExpress.XtraEditors.XtraForm
    {
        private List<EProdProducto> mlist = new List<EProdProducto>();
        public EProdProducto _Be { get; set; }
        public int stocc_iid_almacen;
        public int puvec_icod_punto_venta;

        public FrmListarProductosDeStock()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            mlist = new BCentral().ListarProdProductosXAlmacen(stocc_iid_almacen, Parametros.intEjercicio).Where(stock => stock.stocc_nstock_prod != 0).ToList();
            dgrProducto.DataSource = mlist;

            EProdTablaRegistro ob = new EProdTablaRegistro { iid_tipo_tabla = 11 };
            BSControls.LoaderLook(LkpMarca, new BCentral().ListarProdTablaRegistroFiltros(ob), "descripcion", "tarec_viid_correlativo", true);
            EProdTablaRegistro obt = new EProdTablaRegistro { iid_tipo_tabla = 11 };
            BSControls.LoaderLook(LkpMarca, new BCentral().ListarProdTablaRegistroFiltros(ob), "descripcion", "tarec_viid_correlativo", true);
            EProdTablaRegistro obc = new EProdTablaRegistro { iid_tipo_tabla = 8 };
            BSControls.LoaderLook(LkpColor, new BCentral().ListarProdTablaRegistroFiltros(obc), "descripcion", "tarec_viid_correlativo", true);

        }

        private void FrmListarProductosDeStock_Load(object sender, EventArgs e)
        {
            Carga();
        }

        private void LkpMarca_EditValueChanged(object sender, EventArgs e)
        {
            CargarModelo();
            combos();
        }
        private void CargarModelo()
        {
            List<EProdTablaRegistro> listRegistro = new List<EProdTablaRegistro>();
            EProdTablaRegistro ob = new EProdTablaRegistro();
            EProdModelo obje = new EProdModelo();
            ob.iid_tipo_tabla = 11;
            listRegistro = new BCentral().ListarProdTablaRegistro(ob).Where(obj => obj.tarec_viid_correlativo == LkpMarca.EditValue.ToString()).ToList();
            listRegistro.ForEach(obe =>
            {
                obje.tarec_icorrelativo = obe.icod_tabla;
            });
            List<EProdModelo> lmodelo = new BCentral().ListarProdModelo(obje);
            EProdModelo EProdModelo = new EProdModelo { mo_viid_modelo = "0000", mo_vdescripcion = "Todos" };
            lmodelo.Insert(0, EProdModelo);
            BSControls.LoaderLook(LkpModelo, lmodelo, "mo_vdescripcion", "mo_viid_modelo", true);
        }

        private void LkpModelo_EditValueChanged(object sender, EventArgs e)
        {
            combos();
        }

        private void LkpColor_EditValueChanged(object sender, EventArgs e)
        {
            combos();
        }
        private void combos()
        {
            if (LkpMarca.Text == "Todos")
            {
                if (LkpColor.Text == "Todos")
                {
                    if (LkpModelo.Text == "Todos")
                    {
                        dgrProducto.DataSource = mlist;
                    }
                    else
                    {
                        dgrProducto.DataSource = mlist.Where(filtro => filtro.pr_iid_modelo == Convert.ToInt32(LkpModelo.EditValue)).ToList();
                    }
                }
                else
                {
                    if (LkpModelo.Text == "Todos")
                    {
                        dgrProducto.DataSource = mlist.Where(filtro => filtro.pr_iid_color == Convert.ToInt32(LkpColor.EditValue)).ToList();
                    }
                    else
                    {
                        dgrProducto.DataSource = mlist.Where(filtro => filtro.pr_iid_modelo == Convert.ToInt32(LkpModelo.EditValue) && filtro.pr_iid_color == Convert.ToInt32(LkpColor.EditValue)).ToList();
                    }
                }
            }
            else
            {
                if (LkpColor.Text == "Todos")
                {
                    if (LkpModelo.Text == "Todos")
                    {
                        dgrProducto.DataSource = mlist.Where(filtro => filtro.pr_iid_marca == Convert.ToInt32(LkpMarca.EditValue)).ToList();
                    }
                    else
                    {
                        dgrProducto.DataSource = mlist.Where(filtro => filtro.pr_iid_marca == Convert.ToInt32(LkpMarca.EditValue) && filtro.pr_iid_modelo == Convert.ToInt32(LkpModelo.EditValue)).ToList();
                    }
                }
                else
                {
                    if (LkpModelo.Text == "Todos")
                    {
                        dgrProducto.DataSource = mlist.Where(filtro => filtro.pr_iid_marca == Convert.ToInt32(LkpMarca.EditValue) && filtro.pr_iid_color == Convert.ToInt32(LkpColor.EditValue)).ToList();
                    }
                    else
                    {
                        dgrProducto.DataSource = mlist.Where(filtro => filtro.pr_iid_marca == Convert.ToInt32(LkpMarca.EditValue) && filtro.pr_iid_modelo == Convert.ToInt32(LkpModelo.EditValue) && filtro.pr_iid_color == Convert.ToInt32(LkpColor.EditValue)).ToList();
                    }
                }
            }
        }

        private void viewProducto_DoubleClick(object sender, EventArgs e)
        {
            _Be = (EProdProducto)viewProducto.GetRow(viewProducto.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}