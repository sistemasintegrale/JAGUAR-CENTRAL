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
    public partial class FrmNotaSalida : DevExpress.XtraEditors.XtraForm
    {
        private List<EProdNotaSalida> mlist = new List<EProdNotaSalida>();

        public FrmNotaSalida()
        {
            InitializeComponent();
        }
        public void Carga()
        {
            mlist = new BCentral().ListarProdNotaSalida(Parametros.intEjercicio).Where(x => x.puvec_icod_punto_venta == 2).ToList();
            dgrMotonave.DataSource = mlist;
        }

        void form2_MiEvento()
        {
            Carga();
        }


        void Reload(int icod)
        {
            Carga();
            int xposition = mlist.FindIndex(x => x.salgc_icod_nota_salida == icod);
            viewMotonave.FocusedRowHandle = xposition;
        }


        private void FrmNotaSalida_Load(object sender, EventArgs e) => Carga();
        private void nuevo()
        {
            FrmManteNotaSalida Motonave = new FrmManteNotaSalida();
            Motonave.MiEvento += new FrmManteNotaSalida.DelegadoMensaje(Reload);
            EProdNotaSalida Obe = (EProdNotaSalida)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            Motonave.indicador = 0;
            int? i = 0;
            if (mlist.Count > 0)
                i = mlist.Max(ob => ob.salgc_inumero_nota_salida);
            Motonave.Correlative = Convert.ToInt32(i) + 1;
            Motonave.oDetail = mlist;
            Motonave.Show();
            //Motonave.lkpPuntoVenta.EditValue = ValPVTC.puvec_icod_punto_venta;
            Motonave.SetInsert();

        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }
        private void Datos()
        {
            var oBe = viewMotonave.GetFocusedRow() as EProdNotaSalida;
            if (oBe is null) return;
            FrmManteNotaSalida Motonave = new FrmManteNotaSalida();
            Motonave.MiEvento += new FrmManteNotaSalida.DelegadoMensaje(Reload);
            Motonave.oDetail = mlist;
            Motonave.indicador = 3;
            Motonave.salgc_icod_nota_salida = oBe.salgc_icod_nota_salida;
            Motonave.CargarDetalle(oBe.salgc_icod_nota_salida);
            Motonave.Show();
            Motonave.Correlative = oBe.salgc_icod_nota_salida;
            Motonave.txtnroNota.Text = oBe.salgc_vnumero_nota_salida;
            Motonave.btncodigoalmacen.Tag = oBe.salgc_iid_almacen;
            Motonave.btncodigoalmacen.Text = oBe.salgc_viid_almacen;
            Motonave.txtalmacen.Text = oBe.salgc_vdescripcion_almacen;
            Motonave.lkpmotivo.EditValue = oBe.tablc_iid_motivo;
            Motonave.bteTipoDoc.Tag = oBe.salgc_iid_tipo_doc;
            Motonave.txtnrodocumento.Text = oBe.salgc_inumero_doc;
            Motonave.dtmfecha.EditValue = oBe.salgc_sfecha_nota_salida;
            Motonave.txtobservacion.Text = oBe.salgc_vobservaciones;
            Motonave.txtReferencia.Text = oBe.salgc_vreferencia;
            Motonave.SetModify();
            Motonave.dtmfecha.Enabled = false;
            Motonave.btncodigoalmacen.Enabled = false;
            Motonave.txtnrodocumento.Enabled = false;
            Motonave.lkpmotivo.Enabled = false;
            Motonave.bteTipoDoc.Enabled = false;
            Motonave.CalcularCant();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => Datos();
        private void eliminar()
        {
            if (mlist.Count > 0)
            {
                if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    EProdNotaSalida Obe = (EProdNotaSalida)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                    BCentral BnotaSa = new BCentral();
                    BCentral oblKardex = new BCentral();
                    BCentral BnotaInDeta = new BCentral();
                    List<EProdNotaSalidaDetalle> LISTDETALLE = new List<EProdNotaSalidaDetalle>();
                    EProdKardex ekar = new EProdKardex();


                    BnotaSa.EliminarProdNotaSalida(Obe);
                    LISTDETALLE = new BCentral().ListarProdNotaSalidaDetalle(Obe.salgc_icod_nota_salida);

                    foreach (EProdNotaSalidaDetalle fo in LISTDETALLE)
                    {
                        int[] icod_karde = new int[10];
                        long[] talla = new long[10];
                        object[] idProducto = new object[10];
                        icod_karde[0] = Convert.ToInt32(fo.salgcd_iid_kardex1);
                        icod_karde[1] = Convert.ToInt32(fo.salgcd_iid_kardex2);
                        icod_karde[2] = Convert.ToInt32(fo.salgcd_iid_kardex3);
                        icod_karde[3] = Convert.ToInt32(fo.salgcd_iid_kardex4);
                        icod_karde[4] = Convert.ToInt32(fo.salgcd_iid_kardex5);
                        icod_karde[5] = Convert.ToInt32(fo.salgcd_iid_kardex6);
                        icod_karde[6] = Convert.ToInt32(fo.salgcd_iid_kardex7);
                        icod_karde[7] = Convert.ToInt32(fo.salgcd_iid_kardex8);
                        icod_karde[8] = Convert.ToInt32(fo.salgcd_iid_kardex9);
                        icod_karde[9] = Convert.ToInt32(fo.salgcd_iid_kardex10);
                        talla[0] = Convert.ToInt32(fo.salgcd_talla1);
                        talla[1] = Convert.ToInt32(fo.salgcd_talla2);
                        talla[2] = Convert.ToInt32(fo.salgcd_talla3);
                        talla[3] = Convert.ToInt32(fo.salgcd_talla4);
                        talla[4] = Convert.ToInt32(fo.salgcd_talla5);
                        talla[5] = Convert.ToInt32(fo.salgcd_talla6);
                        talla[6] = Convert.ToInt32(fo.salgcd_talla7);
                        talla[7] = Convert.ToInt32(fo.salgcd_talla8);
                        talla[8] = Convert.ToInt32(fo.salgcd_talla9);
                        talla[9] = Convert.ToInt32(fo.salgcd_talla10);
                        idProducto[0] = fo.salgcd_icod_producto1;
                        idProducto[1] = fo.salgcd_icod_producto2;
                        idProducto[2] = fo.salgcd_icod_producto3;
                        idProducto[3] = fo.salgcd_icod_producto4;
                        idProducto[4] = fo.salgcd_icod_producto5;
                        idProducto[5] = fo.salgcd_icod_producto6;
                        idProducto[6] = fo.salgcd_icod_producto7;
                        idProducto[7] = fo.salgcd_icod_producto8;
                        idProducto[8] = fo.salgcd_icod_producto9;
                        idProducto[9] = fo.salgcd_icod_producto10;
                        for (int j = 0; j <= 9; j++)
                        {
                            //KARDEX
                            ekar.kardc_ianio = Parametros.intEjercicio;
                            ekar.kardc_iid_correlativo = icod_karde[j];
                            ekar.intUsuario = Valores.intUsuario;
                            if (ekar.kardc_iid_correlativo > 0)
                            {
                                oblKardex.EliminarKardexPvt(ekar);
                                string codigoexterno = fo.pr_vcodigo_externo + talla[j];
                                var oProducto = new BCentral().VerificarProdProducto(codigoexterno);

                                EProdStockProducto objStock = new EProdStockProducto()
                                {
                                    stocc_ianio = Parametros.intEjercicio,
                                    stocc_iid_almacen = ekar.iid_almacen,
                                    stocc_iid_producto = oProducto[0].pr_icod_producto,
                                    intUsuario = Valores.intUsuario,
                                    stocc_nstock_prod = 0,

                                };
                                oblKardex.actualizarStockPvt(objStock);
                            }

                        }
                        BnotaInDeta.EliminarProdNotaSalidaDetalle(fo);
                    }
                    viewMotonave.DeleteRow(viewMotonave.FocusedRowHandle);
                    viewMotonave.RefreshData();
                }
            }
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EProdNotaSalidaDetalle> mlistadetalle = new List<EProdNotaSalidaDetalle>();
            if (mlist.Count > 0)
            {
                EProdNotaSalidaDetalle oBe = (EProdNotaSalidaDetalle)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
                mlistadetalle = new BCentral().ListarProdNotaSalidaReporte(oBe.salgc_icod_nota_salida);
                if (mlistadetalle.Count > 0)
                {
                    foreach (EProdNotaSalidaDetalle conve in mlistadetalle)
                    {
                        int[] tallas = new int[10];
                        int[] Cantidad = new int[10];
                        tallas[0] = Convert.ToInt32(conve.salgcd_talla1);
                        tallas[1] = Convert.ToInt32(conve.salgcd_talla2);
                        tallas[2] = Convert.ToInt32(conve.salgcd_talla3);
                        tallas[3] = Convert.ToInt32(conve.salgcd_talla4);
                        tallas[4] = Convert.ToInt32(conve.salgcd_talla5);
                        tallas[5] = Convert.ToInt32(conve.salgcd_talla6);
                        tallas[6] = Convert.ToInt32(conve.salgcd_talla7);
                        tallas[7] = Convert.ToInt32(conve.salgcd_talla8);
                        tallas[8] = Convert.ToInt32(conve.salgcd_talla9);
                        tallas[9] = Convert.ToInt32(conve.salgcd_talla10);
                        Cantidad[0] = Convert.ToInt32(conve.salgcd_cant_talla1);
                        Cantidad[1] = Convert.ToInt32(conve.salgcd_cant_talla2);
                        Cantidad[2] = Convert.ToInt32(conve.salgcd_cant_talla3);
                        Cantidad[3] = Convert.ToInt32(conve.salgcd_cant_talla4);
                        Cantidad[4] = Convert.ToInt32(conve.salgcd_cant_talla5);
                        Cantidad[5] = Convert.ToInt32(conve.salgcd_cant_talla6);
                        Cantidad[6] = Convert.ToInt32(conve.salgcd_cant_talla7);
                        Cantidad[7] = Convert.ToInt32(conve.salgcd_cant_talla8);
                        Cantidad[8] = Convert.ToInt32(conve.salgcd_cant_talla9);
                        Cantidad[9] = Convert.ToInt32(conve.salgcd_cant_talla10);
                        for (int i = 0; i < 10; i++)
                        {
                            if (tallas[i] != 0)
                            {
                                conve.TallaAcumulada = conve.TallaAcumulada + " - " + tallas[i].ToString() + "/" + Cantidad[i].ToString();
                            }
                        }
                    }
                    frmNotaSalidaReporte rpteDealle = new frmNotaSalidaReporte();
                    rpteDealle.cargar(mlistadetalle);
                }
            }
        }
        private void viewMotonave_DoubleClick(object sender, EventArgs e)
        {
            FrmManteNotaSalida Motonave = new FrmManteNotaSalida();
            Motonave.MiEvento += new FrmManteNotaSalida.DelegadoMensaje(Reload);
            Motonave.oDetail = mlist;
            EProdNotaSalida oBe = (EProdNotaSalida)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (oBe != null)
            {
                Motonave.indicador = 3;
                Motonave.CargarDetalle(oBe.salgc_icod_nota_salida);
                Motonave.Show();
                Motonave.Correlative = oBe.salgc_icod_nota_salida;
                Motonave.txtnroNota.Text = oBe.salgc_vnumero_nota_salida;
                Motonave.btncodigoalmacen.Tag = oBe.salgc_iid_almacen;
                Motonave.btncodigoalmacen.Text = oBe.salgc_viid_almacen;
                Motonave.txtalmacen.Text = oBe.salgc_vdescripcion_almacen;
                Motonave.lkpmotivo.EditValue = oBe.tablc_iid_motivo;
                Motonave.bteTipoDoc.Tag = oBe.salgc_iid_tipo_doc;
                Motonave.txtnrodocumento.Text = oBe.salgc_inumero_doc;
                Motonave.dtmfecha.EditValue = oBe.salgc_sfecha_nota_salida;
                Motonave.txtobservacion.Text = oBe.salgc_vobservaciones;
                Motonave.SetCancel();
                Motonave.dtmfecha.Enabled = false;
                Motonave.btncodigoalmacen.Enabled = false;
                Motonave.txtnrodocumento.Enabled = false;
                Motonave.lkpmotivo.Enabled = false;
                Motonave.bteTipoDoc.Enabled = false;
                Motonave.gridControl1.Enabled = false;
                //Motonave.barButtonItem1.Enabled = false;
                Motonave.groupControl1.Enabled = false;
            }
        }

        private void FrmNotaSalida_Activated(object sender, EventArgs e)
        {
            //((FrmMain)MdiParent).oInstance = this;
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
            if (e.KeyCode == Keys.F8) { }
            //PreViewPrint();
        }
        private void BuscarCriterio()
        {

        }
        private void dgrMotonave_DoubleClick(object sender, EventArgs e)
        {
            FrmManteNotaSalida Motonave = new FrmManteNotaSalida();
            Motonave.MiEvento += new FrmManteNotaSalida.DelegadoMensaje(Reload);
            Motonave.oDetail = mlist;
            EProdNotaSalida oBe = (EProdNotaSalida)viewMotonave.GetRow(viewMotonave.FocusedRowHandle);
            if (oBe != null)
            {
                Motonave.indicador = 3;
                Motonave.CargarDetalle(oBe.salgc_icod_nota_salida);
                Motonave.Show();
                Motonave.Correlative = oBe.salgc_icod_nota_salida;
                Motonave.txtnroNota.Text = oBe.salgc_vnumero_nota_salida;
                Motonave.btncodigoalmacen.Tag = oBe.salgc_iid_almacen;
                Motonave.btncodigoalmacen.Text = oBe.salgc_viid_almacen;
                Motonave.txtalmacen.Text = oBe.salgc_vdescripcion_almacen;
                Motonave.lkpmotivo.EditValue = oBe.tablc_iid_motivo;
                Motonave.bteTipoDoc.Tag = oBe.salgc_iid_tipo_doc;
                Motonave.txtnrodocumento.Text = oBe.salgc_inumero_doc;
                Motonave.dtmfecha.EditValue = oBe.salgc_sfecha_nota_salida;
                Motonave.txtobservacion.Text = oBe.salgc_vobservaciones;
                Motonave.SetModify();
                Motonave.dtmfecha.Enabled = false;
                Motonave.btncodigoalmacen.Enabled = false;
                Motonave.txtnrodocumento.Enabled = false;
                Motonave.lkpmotivo.Enabled = false;
                Motonave.bteTipoDoc.Enabled = false;
            }
        }

        private void btnmodificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Datos();
        }

        private void btneliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnagregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void dgrMotonave_Click(object sender, EventArgs e)
        {

        }
    }
}