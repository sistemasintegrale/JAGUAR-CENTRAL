using DevExpress.XtraReports.UI;
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

namespace SGE.WindowForms.Producción.Consulta
{
    public partial class FrmListarCatalogoProducto : DevExpress.XtraEditors.XtraForm
    {
        private List<EProdProducto> mlist = new List<EProdProducto>();
        public FrmListarCatalogoProducto() => InitializeComponent();
        private void FrmListarCatalogoProducto_Load(object sender, EventArgs e) => cargarGrdControles();
        private void simpleButton1_Click(object sender, EventArgs e) => CargarProducto();
        void AddEvent() => this.viewMotonave.DoubleClick += new System.EventHandler(this.viewMotonave_DoubleClick);


        private async void cargarGrdControles()
        {
            simpleButton1.Enabled = false;
            var data = await Task.WhenAll(cargarGrdLkp());
            data[0].Item1.Insert(0, new EProdTablaRegistro { tarec_viid_correlativo = "0", descripcion = "Todos" });
            BSControls.LoaderLook(LkpMarca, data[0].Item1, "descripcion", "tarec_viid_correlativo", true);
            data[0].Item2.Insert(0, new EProdTablaRegistro { tarec_iid_correlativo = 0, descripcion = "Todos" });
            BSControls.LoaderLook(LkpColor, data[0].Item2, "descripcion", "tarec_iid_correlativo", true);
            data[0].Item3.Insert(0, new EProdModelo { mo_iid_modelo = 0, mo_vdescripcion = "Todos" });
            BSControls.LoaderLook(LkpModelo, data[0].Item3, "mo_vdescripcion", "mo_iid_modelo", true);
            BSControls.LoaderLookRepository(lkpgrdUM, data[0].Item4, "descripcion", "id_unidad_medida", true);
            simpleButton1.Enabled = true;

        }



        async Task<(List<EProdTablaRegistro>, List<EProdTablaRegistro>, List<EProdModelo>, object)> cargarGrdLkp()
        {
            (List<EProdTablaRegistro>, List<EProdTablaRegistro>, List<EProdModelo>, object) data = default;
            data.Item1 = await Task.Run(() => new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro { iid_tipo_tabla = Parametros.intMarca }));
            data.Item2 = await Task.Run(() => new BCentral().ListarProdTablaRegistro(new EProdTablaRegistro { iid_tipo_tabla = Parametros.intColor }));
            data.Item3 = await Task.Run(() => new BCentral().ModeloListarSimple(0));
            data.Item4 = await Task.Run(() => new BCentral().ListarProdUnidadMedida());
            return data;
        }

        private void CargarProducto()
        {

            mlist = new BCentral().ListarProdProductoByMarcaModelo(Convert.ToInt32(LkpMarca.EditValue), Convert.ToInt32(LkpModelo.EditValue)).ToList();
            if (Convert.ToInt32(LkpColor.EditValue) != 0)
                mlist = mlist.Where(x => x.pr_iid_color == Convert.ToInt32(LkpColor.EditValue)).ToList();
            dgrMotonave.DataSource = mlist.OrderBy(x => x.pr_vcodigo_externo);
            dgrMotonave.RefreshDataSource();
        }

        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {

            FrmManteProducto Motonave = new FrmManteProducto();
            Motonave.MiEvento += new FrmManteProducto.DelegadoMensaje(AddEvent);
            Motonave.oDetail = mlist;
            Motonave.Show();
            EProdProducto oBe = (EProdProducto)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
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
            this.viewMotonave.DoubleClick -= new System.EventHandler(this.viewMotonave_DoubleClick);
        }




        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable DT = new DataTable();
            FrmTipoImpresion frmImpresion = new FrmTipoImpresion();
            if (mlist.Count > 0)
            {
                if (frmImpresion.ShowDialog() == DialogResult.OK)
                {
                    if (frmImpresion.id_situacion == 2)
                    {
                        DT = BSConverter<EProdProducto>.Convert(mlist);
                        rptRelaciondeCatalogo reporte = new rptRelaciondeCatalogo();
                        reporte.cargar(DT, DateTime.Today.ToString(), "hola", Modules.Valores.strNombreEmpresa);
                        reporte.ShowPreview();
                    }
                    else
                    {
                        mlist = mlist.Where(ob => ob.pr_isituacion_producto == frmImpresion.id_situacion).ToList();
                        DT = BSConverter<EProdProducto>.Convert(mlist);
                        rptRelaciondeCatalogo reporte = new rptRelaciondeCatalogo();
                        reporte.cargar(DT, DateTime.Today.ToString(), "hola", "hola");
                        reporte.ShowPreview();
                    }
                }
            }
        }

        private void LkpMarca_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}