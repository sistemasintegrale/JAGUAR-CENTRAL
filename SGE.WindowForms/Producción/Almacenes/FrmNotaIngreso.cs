using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Almacenes
{
    public partial class FrmNotaIngreso : DevExpress.XtraEditors.XtraForm
    {
        private BCentral oblKardex = new BCentral();
        private List<EProdNotaIngreso> mlist = new List<EProdNotaIngreso>();


        public void Carga()
        {
            mlist = new BCentral().ListarProdNotaIngreso(Parametros.intEjercicio).Where(x => x.puvec_icod_punto_venta == 2).ToList();
            dgrMotonave.DataSource = mlist;
            viewMotonave.BestFitColumns();
        }


        void Reload(int icod)
        {
            Carga();
            int xposition = mlist.FindIndex(x => x.ningc_icod_nota_ingreso == icod);
            viewMotonave.FocusedRowHandle = xposition;
        }

        public FrmNotaIngreso()
        {
            InitializeComponent();
        }

        private void FrmNotaIngreso_Load(object sender, EventArgs e)
        {
            Carga();
        }
        private void nuevo()
        {
            FrmNotaIngresoRegistro Motonave = new FrmNotaIngresoRegistro();
            Motonave.MiEvento += new FrmNotaIngresoRegistro.DelegadoMensaje(Reload);

            EProdNotaIngreso Obe = (EProdNotaIngreso)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            int? i = 0;
            if (mlist.Count > 0)
                i = mlist.Max(ob => ob.ningc_inumero_nota_ingreso);
            Motonave.Correlativo = Convert.ToInt32(i) + 1;
            Motonave.oDetail = mlist;
            Motonave.Show();
            Motonave.SetInsert();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => nuevo();
        private void Modificar()
        {
            EProdNotaIngreso oBe = (EProdNotaIngreso)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (oBe is null) return;

            FrmNotaIngresoRegistro frm = new FrmNotaIngresoRegistro();
            frm.MiEvento += new FrmNotaIngresoRegistro.DelegadoMensaje(Reload);
            frm.oDetail = mlist;
            frm.oDetail = mlist;
            frm.ningc_icod_nota_ingreso = oBe.ningc_icod_nota_ingreso;
            frm.Show();
            frm.ningc_icod_nota_ingreso = oBe.ningc_icod_nota_ingreso;
            frm.txtnroNota.Text = oBe.ningc_vnumero_nota_ingreso;
            frm.btncodigoalmacen.Tag = oBe.ningc_iid_almacen;
            frm.btncodigoalmacen.Text = oBe.ningc_viid_almacen;
            frm.txtalmacen.Text = oBe.ningc_vdescripcion_almacen;
            frm.lkpmotivo.EditValue = oBe.tablc_iid_motivo;
            frm.lkpPuntoVenta.EditValue = oBe.puvec_icod_punto_venta;
            frm.lkptipodocumento.EditValue = oBe.ningc_iid_tipo_doc;
            frm.txtnrodocumento.Text = oBe.ningc_inumero_doc;
            frm.dtmfechadocumento.EditValue = oBe.ningc_sfecha_compra;
            frm.dtmfecha.EditValue = oBe.ningc_sfecha_nota_ingreso;
            frm.txtobservacion.Text = oBe.ningc_vobservaciones;
            frm.txtreferencia.Text = oBe.ningc_vreferencia;
            frm.btnPedido.Tag = oBe.orpec_iid_orden_pedido;
            frm.btnPedido.Text = oBe.numero_pedido;
            frm.btnItemPedido.Tag = oBe.orped_icod_item_orden_pedido;
            frm.btnItemPedido.Text = oBe.numero_item_pedido.ToString();
            frm.SetModify();
            frm.dtmfecha.Enabled = false;
            frm.btncodigoalmacen.Enabled = false;
            frm.txtnrodocumento.Enabled = false;
            frm.dtmfechadocumento.Enabled = false;
            frm.lkpmotivo.Enabled = false;
            frm.lkptipodocumento.Enabled = false;
            frm.lkpPuntoVenta.Enabled = false;
            frm.CalcularCant();

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => Modificar();
        private void eliminar()
        {

            if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                EProdNotaIngreso Obe = (EProdNotaIngreso)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                BCentral BnotaIn = new BCentral();
                BCentral BnotaInDeta = new BCentral();
                List<EProdNotaIngresoDetalle> LISTDETALLE = new List<EProdNotaIngresoDetalle>();
                EProdKardex ekar = new EProdKardex();
                List<EProdProducto> oProducto = new List<EProdProducto>();
                BnotaIn.EliminarProdNotaIngreso(Obe);
                LISTDETALLE = new BCentral().ListarProdNotaIngresoDetalle(Obe.ningc_icod_nota_ingreso);
                foreach (EProdNotaIngresoDetalle fo in LISTDETALLE)
                {
                    int[] icod_karde = new int[10];
                    long[] talla = new long[10];
                    //KARDEX
                    ekar.kardc_ianio = Parametros.intEjercicio;
                    ekar.kardx_sfecha = DateTime.Today;
                    ekar.iid_almacen = Convert.ToInt32(fo.ningc_iid_almacen);
                    ekar.iid_producto = Convert.ToInt32(fo.pr_icod_producto);
                    ekar.movimiento = 0;
                    ekar.operacion = 3;
                    icod_karde[0] = Convert.ToInt32(fo.ningcd_iid_kardex1);
                    icod_karde[1] = Convert.ToInt32(fo.ningcd_iid_kardex2);
                    icod_karde[2] = Convert.ToInt32(fo.ningcd_iid_kardex3);
                    icod_karde[3] = Convert.ToInt32(fo.ningcd_iid_kardex4);
                    icod_karde[4] = Convert.ToInt32(fo.ningcd_iid_kardex5);
                    icod_karde[5] = Convert.ToInt32(fo.ningcd_iid_kardex6);
                    icod_karde[6] = Convert.ToInt32(fo.ningcd_iid_kardex7);
                    icod_karde[7] = Convert.ToInt32(fo.ningcd_iid_kardex8);
                    icod_karde[8] = Convert.ToInt32(fo.ningcd_iid_kardex9);
                    icod_karde[9] = Convert.ToInt32(fo.ningcd_iid_kardex10);
                    talla[0] = Convert.ToInt32(fo.ningcd_talla1);
                    talla[1] = Convert.ToInt32(fo.ningcd_talla2);
                    talla[2] = Convert.ToInt32(fo.ningcd_talla3);
                    talla[3] = Convert.ToInt32(fo.ningcd_talla4);
                    talla[4] = Convert.ToInt32(fo.ningcd_talla5);
                    talla[5] = Convert.ToInt32(fo.ningcd_talla6);
                    talla[6] = Convert.ToInt32(fo.ningcd_talla7);
                    talla[7] = Convert.ToInt32(fo.ningcd_talla8);
                    talla[8] = Convert.ToInt32(fo.ningcd_talla9);
                    talla[9] = Convert.ToInt32(fo.ningcd_talla10);
                    for (int j = 0; j <= 9; j++)
                    {


                        ekar.kardc_iid_correlativo = icod_karde[j];
                        ekar.intUsuario = Valores.intUsuario;
                        new BCentral().EliminarKardexPvt(ekar);
                        EProdStockProducto objStock = new EProdStockProducto()
                        {
                            stocc_ianio = Parametros.intEjercicio,
                            stocc_iid_almacen = ekar.iid_almacen,
                            stocc_iid_producto = ekar.iid_producto,
                            stocc_nstock_prod = 0,

                        };
                        new BCentral().actualizarStockPvt(objStock);


                    }
                    new BCentral().EliminarProdNotaIngresoDetalle(fo);

                }
                viewMotonave.DeleteRow(viewMotonave.FocusedRowHandle);
                viewMotonave.RefreshData();
            }

        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            List<EProdNotaIngresoDetalle> mlistadetalle = new List<EProdNotaIngresoDetalle>();
            if (mlistadetalle.Count > 0)
            {
                foreach (EProdNotaIngresoDetalle conve in mlistadetalle)
                {
                    int[] tallas = new int[10];
                    int[] Cantidad = new int[10];
                    tallas[0] = Convert.ToInt32(conve.ningcd_talla1);
                    tallas[1] = Convert.ToInt32(conve.ningcd_talla2);
                    tallas[2] = Convert.ToInt32(conve.ningcd_talla3);
                    tallas[3] = Convert.ToInt32(conve.ningcd_talla4);
                    tallas[4] = Convert.ToInt32(conve.ningcd_talla5);
                    tallas[5] = Convert.ToInt32(conve.ningcd_talla6);
                    tallas[6] = Convert.ToInt32(conve.ningcd_talla7);
                    tallas[7] = Convert.ToInt32(conve.ningcd_talla8);
                    tallas[8] = Convert.ToInt32(conve.ningcd_talla9);
                    tallas[9] = Convert.ToInt32(conve.ningcd_talla10);
                    Cantidad[0] = Convert.ToInt32(conve.ningcd_cant_talla1);
                    Cantidad[1] = Convert.ToInt32(conve.ningcd_cant_talla2);
                    Cantidad[2] = Convert.ToInt32(conve.ningcd_cant_talla3);
                    Cantidad[3] = Convert.ToInt32(conve.ningcd_cant_talla4);
                    Cantidad[4] = Convert.ToInt32(conve.ningcd_cant_talla5);
                    Cantidad[5] = Convert.ToInt32(conve.ningcd_cant_talla6);
                    Cantidad[6] = Convert.ToInt32(conve.ningcd_cant_talla7);
                    Cantidad[7] = Convert.ToInt32(conve.ningcd_cant_talla8);
                    Cantidad[8] = Convert.ToInt32(conve.ningcd_cant_talla9);
                    Cantidad[9] = Convert.ToInt32(conve.ningcd_cant_talla10);
                    for (int i = 0; i < 10; i++)
                    {
                        if (tallas[i] != 0)
                        {
                            conve.TallaAcumulada = conve.TallaAcumulada + " - " + tallas[i].ToString() + "/" + Cantidad[i].ToString();
                        }
                    }
                }
                rptNotaIngresoReporte rpteDealle = new rptNotaIngresoReporte();
                rpteDealle.cargar(mlistadetalle);

            }

        }

        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                FrmNotaIngresoRegistro Motonave = new FrmNotaIngresoRegistro();
                Motonave.MiEvento += new FrmNotaIngresoRegistro.DelegadoMensaje(Reload);
                Motonave.Show();
                Motonave.SetCancel();
                EProdNotaIngreso oBe = (EProdNotaIngreso)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                Motonave.ningc_icod_nota_ingreso = oBe.ningc_icod_nota_ingreso;
                Motonave.txtnroNota.Text = oBe.ningc_vnumero_nota_ingreso;
                Motonave.btncodigoalmacen.Tag = oBe.ningc_iid_almacen;
                Motonave.btncodigoalmacen.Text = oBe.ningc_viid_almacen;
                Motonave.txtalmacen.Text = oBe.ningc_vdescripcion_almacen;
                Motonave.lkpmotivo.EditValue = oBe.tablc_iid_motivo;
                Motonave.lkptipodocumento.EditValue = oBe.ningc_iid_tipo_doc;
                Motonave.txtnrodocumento.Text = oBe.ningc_inumero_doc;
                Motonave.dtmfechadocumento.EditValue = oBe.ningc_sfecha_compra;
                Motonave.dtmfecha.EditValue = oBe.ningc_sfecha_nota_ingreso;
                Motonave.txtobservacion.Text = oBe.ningc_vobservaciones;
                Motonave.txtreferencia.Text = oBe.ningc_vreferencia;
                Motonave.dtmfecha.Enabled = false;
                Motonave.btncodigoalmacen.Enabled = false;
                Motonave.txtnrodocumento.Enabled = false;
                Motonave.dtmfechadocumento.Enabled = false;
                Motonave.lkpmotivo.Enabled = false;
                Motonave.lkptipodocumento.Enabled = false;
                Motonave.grcNotaDetalle.Enabled = false;

            }

        }


        private void BuscarCriterio()
        {
            dgrMotonave.DataSource = mlist.Where(obj =>
                                                   obj.ningc_inumero_nota_ingreso.ToString().ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.proc_vnombrecompleto.ToString().Contains(textEdit1.Text.ToUpper())
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
                Modificar();
            if (e.KeyCode == Keys.F9)
                eliminarToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F8) { }
            //       PreViewPrint();
        }

        private void btnagregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnmodificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Modificar();
        }

        private void btneliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Carga();
        }
    }
}