using GE.WindowForms.Ventas.Registro_de_Datos_de_Ventas;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class FrmRegistroListaPrecios : DevExpress.XtraEditors.XtraForm
    {
        public FrmRegistroListaPrecios()
        {
            InitializeComponent();
        }
        private List<EProducto> mlist = new List<EProducto>();

        List<EProdListaPrecios> mlistaproducto = new List<EProdListaPrecios>();
        List<EProdListaPrecios> mlistaprecio = new List<EProdListaPrecios>();

        public EProducto _Be { get; set; }
        public int stocc_iid_almacen;
        //BListaPrecios obl = new BListaPrecios();

        public void Carga()
        {
            EProdTablaRegistro ob = new EProdTablaRegistro { iid_tipo_tabla = 11, descripcion = "<Seleccionar>", tarec_viid_correlativo = "00" };
            List<EProdTablaRegistro> lmodelo = new BCentral().ListarProdTablaRegistro(ob).Where(j => j.descripcion != "Todos").ToList();
            lmodelo.Insert(0, ob);
            BSControls.LoaderLook(LkpMarca, lmodelo, "descripcion", "tarec_viid_correlativo", true);

            EProdTablaRegistro obf = new EProdTablaRegistro { iid_tipo_tabla = 8, descripcion = "<Seleccionar>", tarec_viid_correlativo = "00" };
            List<EProdTablaRegistro> lcolor = new BCentral().ListarProdTablaRegistro(obf).Where(j => j.descripcion != "Todos").ToList();
            lcolor.Insert(0, obf);
            BSControls.LoaderLook(LkpColor, lcolor, "descripcion", "tarec_viid_correlativo", true);

        }
        private void FrmRegistroListaPrecios_Load(object sender, EventArgs e)
        {
            Carga();
        }
        private void CargarModelo()
        {
            //List<EProdTablaRegistro> listRegistro = new List<EProdTablaRegistro>();
            //EProdTablaRegistro ob = new EProdTablaRegistro();
            //EProdTablaRegistro obje = new EProdTablaRegistro();
            //ob.iid_tipo_tabla = 11;
            //listRegistro = new BCentral().ListarProdTablaRegistro(ob).Where(obj => obj.tarec_viid_correlativo == LkpMarca.EditValue.ToString()).ToList();
            //listRegistro.ForEach(obe =>
            //{
            //    obje.tarec_viid_correlativo = obe.iid_tipo_tabla;
            //});
            //List<EModelo> lmodelo = new BModelo().ListarModelo(obje);
            //EModelo emodelo = new EModelo { mo_viid_modelo = "0000", mo_vdescripcion = "<Seleccionar>" };
            //lmodelo.Insert(0, emodelo);
            //BSControls.LoaderLook(LkpModelo, lmodelo, "mo_vdescripcion", "mo_viid_modelo", true);
        }

        private void LkpMarca_EditValueChanged(object sender, EventArgs e)
        {
            CargarModelo();
        }

        private void cargarDatos()
        {
            mlistaprecio = new List<EProdListaPrecios>();
            mlistaproducto = null;
            mlistaproducto = new BCentral().ListaProdProductoSinTalla(Convert.ToInt32(LkpMarca.EditValue), Convert.ToInt32(btnmodelo.Tag), 0);
            if (Convert.ToInt32(LkpColor.EditValue) != 0)
            {
                mlistaproducto = mlistaproducto.Where(obus => obus.pr_iid_color == Convert.ToInt32(LkpColor.EditValue)).ToList();
            }
            if (mlistaproducto.Count > 0)
            {
                foreach (var objPro in mlistaproducto)
                {
                    EProdListaPrecios objLista = new EProdListaPrecios();
                    objLista = new BCentral().ListarListaPreciosXCodigo(objPro.pr_vcodigo_externo);
                    if (objLista.lprec_icod_precio == 0)
                    {
                        EProdListaPrecios objAux = new EProdListaPrecios();
                        objAux.lprec_icod_precio = 0;
                        objAux.pr_vcodigo_externo = objPro.pr_vcodigo_externo.Substring(0, 13);
                        objAux.pr_vdescripcion_producto = objPro.pr_vdescripcion_producto;
                        objAux.lprec_nprecio_costo = 0;
                        objAux.lprec_nporc_utilidad = 0;
                        objAux.lprec_nprecio_vtamin = 0;
                        objAux.lprec_nprecio_vtamay = 0;
                        objAux.lprec_nprecio_vtaotros = 0;
                        objAux.lprec_bdetalle = false;
                        objAux.lprec_iactivo = 1;
                        mlistaprecio.Add(objAux);
                    }
                    else
                    {
                        mlistaprecio.Add(objLista);
                    }
                }
            }
            else
            {
                mlistaprecio = null;
            }
            dgrProducto.DataSource = mlistaprecio;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.cargarDatos();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datos();
        }
        private void Datos()
        {
            EProdListaPrecios oBe = (EProdListaPrecios)viewProducto.GetRow(viewProducto.FocusedRowHandle);
            if (oBe != null)
            {
                FrmMantListaPrecios Motonave = new FrmMantListaPrecios();
                Motonave.MiEvento += new FrmMantListaPrecios.DelegadoMensaje(form2_MiEvento);
                Motonave.txtCodigo.Text = oBe.pr_vcodigo_externo.Substring(0, 13);
                Motonave.lprec_icod_precio = oBe.lprec_icod_precio;
                Motonave.txtdescripcion.Text = oBe.pr_vdescripcion_producto;
                Motonave.txtcosto.Text = oBe.lprec_nprecio_costo.ToString();
                Motonave.txtmargen.Text = oBe.lprec_nporc_utilidad.ToString();
                Motonave.txtPrEcioMayo.Text = oBe.lprec_nprecio_vtamay.ToString();
                Motonave.txtMinorista.Text = oBe.lprec_nprecio_vtamin.ToString();
                Motonave.txtprecioRem.Text = oBe.lprec_nprecio_vtaotros.ToString();
                Motonave.lprec_icod_precio = oBe.lprec_icod_precio;
                Motonave.Show();

                if (oBe.lprec_icod_precio != 0)
                {
                    Motonave.SetModify();
                }
                else
                    Motonave.SetInsert();
                Motonave.Control();
            }
        }
        void form2_MiEvento()
        {
            this.cargarDatos();
        }

        private void viewProducto_DoubleClick(object sender, EventArgs e)
        {
            EProdListaPrecios oBe = (EProdListaPrecios)viewProducto.GetRow(viewProducto.FocusedRowHandle);
            if (oBe != null)
            {
                if (oBe.lprec_icod_precio != 0)
                {
                    FrmMantListaPrecios Motonave = new FrmMantListaPrecios();
                    Motonave.txtCodigo.Text = oBe.pr_vcodigo_externo;
                    Motonave.txtdescripcion.Text = oBe.pr_vdescripcion_producto;
                    Motonave.txtcosto.Text = oBe.lprec_nprecio_costo.ToString();
                    Motonave.txtmargen.Text = oBe.lprec_nporc_utilidad.ToString();
                    Motonave.txtPrEcioMayo.Text = oBe.lprec_nprecio_vtamin.ToString();
                    Motonave.txtMinorista.Text = oBe.lprec_nprecio_vtamay.ToString();
                    Motonave.txtprecioRem.Text = oBe.lprec_nprecio_vtaotros.ToString();
                    Motonave.lprec_icod_precio = oBe.lprec_icod_precio;
                    Motonave.Show();
                    Motonave.btnGuardar.Enabled = false;
                    Motonave.SetCancel();
                }

            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            List<EProdListaPrecios> mlistadet = new List<EProdListaPrecios>();
            if (mlistaprecio != null)
            {
                foreach (var obj in mlistaprecio)
                {
                    List<EProdListaPreciosDetalle> mlitAux = new List<EProdListaPreciosDetalle>();
                    mlitAux = (new BCentral()).ListarProdListaPrecioDetalle(obj.lprec_icod_precio);
                    if (mlitAux.Count > 0)
                    {
                        foreach (EProdListaPreciosDetalle objDet in mlitAux)
                        {
                            EProdListaPrecios objlistaPre = new EProdListaPrecios();
                            objlistaPre.lprec_icod_precio = obj.lprec_icod_precio;
                            objlistaPre.pr_vdescripcion_producto = obj.pr_vdescripcion_producto;
                            objlistaPre.lprec_nprecio_costo = obj.lprec_nprecio_costo;
                            objlistaPre.lprec_nporc_utilidad = obj.lprec_nporc_utilidad;
                            objlistaPre.lprec_nprecio_vtamin = obj.lprec_nprecio_vtamin;
                            objlistaPre.lprec_nprecio_vtamay = obj.lprec_nprecio_vtamay;
                            objlistaPre.lprec_nprecio_vtaotros = obj.lprec_nprecio_vtaotros;
                            objlistaPre.lpred_vrango_tallas = objDet.lpred_vrango_tallas;
                            objlistaPre.lpred_nprecio_vtamin = objDet.lpred_nprecio_vtamin;
                            objlistaPre.lpred_nprecio_vtamay = objDet.lpred_nprecio_vtamay;
                            objlistaPre.lpred_nprecio_vtaotros = objDet.lpred_nprecio_vtaotros;
                            mlistadet.Add(objlistaPre);
                        }
                    }
                    else
                    {
                        if (obj.lprec_icod_precio != 0)
                        {
                            mlistadet.Add(obj);
                        }
                    }
                }
                rptRegistroListaPrecios rpt = new rptRegistroListaPrecios();
                rpt.cargar(mlistadet);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EProdListaPrecios oBe = (EProdListaPrecios)viewProducto.GetRow(viewProducto.FocusedRowHandle);
            if (oBe != null)
            {
                if (oBe.lprec_icod_precio != 0)
                {
                    new BCentral().EliminarProdListaPrecio(oBe);
                    cargarDatos();
                }
            }
            LkpColor.Focus();
            viewProducto.MoveFirst();
        }

        private void viewProducto_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
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

                    //oblvariables.pr_iid_categoria_descripcion = listmodelo._Be.pr_iid_categoria_descripcion;
                    //oblvariables.pr_iid_linea_descripcion = listmodelo._Be.pr_iid_linea_descripcion;
                    //oblvariables.pr_iid_tipo_marca_descripcion = listmodelo._Be.pr_iid_tipo_marca_descripcion;
                    //oblvariables.pr_tarec_icorrelativo = Convert.ToInt32(obje.tarec_icorrelativo);
                    //oblvariables.icod_modelo = listmodelo._Be.mo_icod_modelo;
                }
            }
        }

        private void btnmodelo_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}