using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Compras.ComprasMercaderia;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Compras_Mercaderia
{
    public partial class Frm06RecepcionComprasMercaderia : DevExpress.XtraEditors.XtraForm
    {
        List<ERecepcionComprasMercaderia> lstRecepcionCompras = new List<ERecepcionComprasMercaderia>();
        public Frm06RecepcionComprasMercaderia()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            lstRecepcionCompras = new BCompras().listarRecepcionComprasMercaderia().Where(x => x.rcmc_sfecha_rcm.Year == Parametros.intEjercicio).ToList();
            grdGuiaRemision.DataSource = lstRecepcionCompras;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstRecepcionCompras.FindIndex(x => x.rcmc_icod_rcm == intIcod);
            viewGuiaRemision.FocusedRowHandle = index;
            viewGuiaRemision.Focus();
        }

        private void nuevo()
        {
            frmManteRecepcionComprasMercaderia frm = new frmManteRecepcionComprasMercaderia();
            frm.MiEvento += new frmManteRecepcionComprasMercaderia.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.Show();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void Frm04Factura_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void modificar()
        {
            string descripcion_situacion = "";
            int sit = 0;
            ERecepcionComprasMercaderia obe = (ERecepcionComprasMercaderia)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;

            try
            {
                if (obe.tablc_iid_situacion_rcm == 219)
                {
                    sit = 219;
                    descripcion_situacion = "FACTURADA";
                }
                if (obe.tablc_iid_situacion_rcm == 220)
                {
                    sit = 220;
                    descripcion_situacion = "ANULADA";
                }
                if (sit == 0)
                {
                    frmManteRecepcionComprasMercaderia frm = new frmManteRecepcionComprasMercaderia();
                    frm.MiEvento += new frmManteRecepcionComprasMercaderia.DelegadoMensaje(reload);
                    frm.oBe = obe;
                    frm.SetModify();
                    frm.Show();
                    frm.CalcularMontos();
                }
                else
                {
                    XtraMessageBox.Show("No puede ser Modificado, la Guia de Remision de Compra esta " + descripcion_situacion, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private bool verificarDocVentaPlanilla(int intIcodDoc)
        {
            bool flagExiste = false;
            var intFlag = new BVentas().verificarDocVentaPlanilla(Parametros.intTipoDocFacturaVenta, intIcodDoc);
            if (intFlag > 0)
                flagExiste = true;

            return flagExiste;

        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }


        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdGuiaRemision.DataSource = lstRecepcionCompras.Where(x => x.rcmc_vnumero_rcm.Trim().Contains(txtNumero.Text.Trim()) &&
                x.NomProveedor.ToUpper().Trim().Contains(txtProveedor.Text.Trim().ToUpper())).ToList();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ERecepcionComprasMercaderia ObeRcm = (ERecepcionComprasMercaderia)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            List<ERecepcionComprasMercaderia> lstRcm = new List<ERecepcionComprasMercaderia>();
            ERecepcionComprasMercaderia Obe = new ERecepcionComprasMercaderia();
            List<ERecepcionComprasMercaderiaDetalle> mlisdet = new List<ERecepcionComprasMercaderiaDetalle>();

            lstRcm = new BCompras().listarRecepcionComprasMercaderia().Where(x => x.rcmc_icod_rcm == ObeRcm.rcmc_icod_rcm).ToList();
            Obe = lstRcm.FirstOrDefault();

            mlisdet = new BCompras().listarRecepcionComprasMercaderiaDetalle(Obe.rcmc_icod_rcm);

            rptRecepcionComprasMercaderia rptRcmc = new rptRecepcionComprasMercaderia();

            rptRcmc.cargar(Obe, mlisdet);
            rptRcmc.ShowPreview();


            //EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            //if (obe == null)
            //    return;



            //List<EUbicacion> lUbicacion = new BVentas().ListarUbicacion();
            //int? tipo = null;
            //int? Dios = null;
            //int? padre = null;
            //int? abuelo = null;
            //int? bisabuelo = null;

            //string pais;
            //string Departamento = "";
            //string Provincia = "";
            //string Distrito = "";
            //lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion).ToList().ForEach(oB =>
            //{
            //    tipo = oB.tablc_iid_tipo_ubicacion;
            //    padre = oB.ubicc_icod_ubicacion_padre;
            //});
            //switch (tipo)
            //{
            //    case 4:
            //        Dios = obe.ubicc_icod_ubicacion;
            //        break;
            //    case 3:
            //        foreach (var _BBE in lUbicacion)
            //        {
            //            if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
            //            {
            //                Departamento = _BBE.ubicc_vnombre_ubicacion;
            //            }
            //        }

            //        break;
            //    case 2:
            //        lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
            //        {
            //            abuelo = oB.ubicc_icod_ubicacion_padre;
            //            Departamento = oB.ubicc_vnombre_ubicacion;
            //        });
            //        foreach (var _BBE in lUbicacion)
            //        {
            //            if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
            //            {
            //                Provincia = _BBE.ubicc_vnombre_ubicacion;
            //            }
            //        }
            //        break;
            //    case 1:
            //        lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
            //        {
            //            abuelo = oB.ubicc_icod_ubicacion_padre;
            //            Provincia = oB.ubicc_vnombre_ubicacion;
            //        });

            //        lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == abuelo).ToList().ForEach(oB =>
            //        {
            //            bisabuelo = oB.ubicc_icod_ubicacion_padre;
            //            Departamento = oB.ubicc_vnombre_ubicacion;
            //        });
            //        foreach (var _BBE in lUbicacion)
            //        {
            //            if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
            //            {
            //                Distrito = _BBE.ubicc_vnombre_ubicacion;
            //            }
            //        }
            //        break;

            //}
            //var lstdet = new BVentas().listarGuiaRemisionDet(obe.remic_icod_remision,Parametros.intEjercicio);


            //List<EGuiaRemisionDet> mlistDetalle = new List<EGuiaRemisionDet>();
            //int cont = 1;
            //foreach (var _BE in lstdet)
            //{
            //    mlistDetalle.Add(_BE);

            //    string[] partes = partes = _BE.dremc_vobservaciones.Split('@');
            //    int cont_estapacios = 0;
            //    for (int i = 0; i < partes.Length; i++)
            //    {
            //        if (partes[i] == "")
            //        {
            //            cont_estapacios = cont_estapacios + 1;
            //        }
            //    }
            //    if (cont_estapacios != partes.Length)
            //    {
            //        for (int i = 0; i < partes.Length; i++)
            //        {
            //            if (i == 0)
            //            {
            //                EGuiaRemisionDet __be = new EGuiaRemisionDet();
            //                __be.strDesProducto = "" + partes[i];
            //                __be.OrdenItemImprimir = cont + 1;
            //                cont++;
            //                mlistDetalle.Add(__be);
            //            }
            //            else
            //            {
            //                if (partes[i] != "")
            //                {
            //                    EGuiaRemisionDet __be = new EGuiaRemisionDet();
            //                    __be.strDesProducto = "" + partes[i];
            //                    __be.OrdenItemImprimir = cont + 1;
            //                    cont++;
            //                    mlistDetalle.Add(__be);
            //                }
            //            }
            //        }
            //    }
            //}
            //rptGuiaRemision rpt = new rptGuiaRemision();
            ////rpt.cargar(obe, mlistDetalle,"","","");

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void eliminar()
        {
            //EGuiaRemisionComprasMercaderia obe = (EGuiaRemisionComprasMercaderia)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            //if (obe == null)
            //    return;
            //try
            //{
            //List<EGuiaRemisionComprasDetalle> _BEguiaConstatar = new BCompras().listarGuiaRemisionComprasDetalle(obe.grcc_icod_grc);
            ////if (_BEguiaConstatar.tablc_iid_situacion_remision != 218)
            ////{
            ////    throw new ArgumentException(String.Format("La Guia de Remisión no puede ser eliminada, su situación es {0}", _BEguiaConstatar.StrSitucion));
            ////    int xPosition = viewGuiaRemision.FocusedRowHandle;
            ////    cargar();
            ////    viewGuiaRemision.FocusedRowHandle = xPosition;
            ////}
            ////if (verificarDocVentaPlanilla(obe.favc_icod_factura))
            ////    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser eliminada");
            //if (XtraMessageBox.Show("¿Esta seguro que desea ELIMINAR la Guía de Remisión?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            //{
            //    obe.intUsuario = Valores.intUsuario;
            //    obe.strPc = WindowsIdentity.GetCurrent().Name;
            //    new BCompras().eliminarGuiaRemisionComprasMercaderia(obe);
            //    reload(obe.grcc_icod_grc);

            //}

            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            if (lstRecepcionCompras.Count > 0)
            {
                if (XtraMessageBox.Show("Desea Eliminar el registro", "Informacion del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    ERecepcionComprasMercaderia Obe = (ERecepcionComprasMercaderia)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
                    //BCentral BnotaIn = new BCentral();
                    //BCentral BnotaInDeta = new BCentral();
                    List<ERecepcionComprasMercaderiaDetalle> LISTDETALLE = new List<ERecepcionComprasMercaderiaDetalle>();
                    EProdKardex ekar = new EProdKardex();
                    List<EProdProducto> oProducto = new List<EProdProducto>();
                    new BCompras().eliminarRecepcionComprasMercaderia(Obe);
                    LISTDETALLE = new BCompras().listarRecepcionComprasMercaderiaDetalle(Obe.rcmc_icod_rcm);
                    foreach (ERecepcionComprasMercaderiaDetalle fo in LISTDETALLE)
                    {
                        int[] icod_karde = new int[10];
                        long[] talla = new long[10];
                        //KARDEX
                        ekar.kardc_ianio = Parametros.intEjercicio;
                        ekar.kardx_sfecha = DateTime.Today;
                        //ekar.iid_almacen = Convert.ToInt32(fo.ningc_iid_almacen);
                        ekar.iid_producto = Convert.ToInt32(fo.prdc_icod_producto);
                        ekar.movimiento = 0;
                        ekar.operacion = 3;
                        icod_karde[0] = Convert.ToInt32(fo.rcmd_iid_kardex1);
                        icod_karde[1] = Convert.ToInt32(fo.rcmd_iid_kardex2);
                        icod_karde[2] = Convert.ToInt32(fo.rcmd_iid_kardex3);
                        icod_karde[3] = Convert.ToInt32(fo.rcmd_iid_kardex4);
                        icod_karde[4] = Convert.ToInt32(fo.rcmd_iid_kardex5);
                        icod_karde[5] = Convert.ToInt32(fo.rcmd_iid_kardex6);
                        icod_karde[6] = Convert.ToInt32(fo.rcmd_iid_kardex7);
                        icod_karde[7] = Convert.ToInt32(fo.rcmd_iid_kardex8);
                        icod_karde[8] = Convert.ToInt32(fo.rcmd_iid_kardex9);
                        icod_karde[9] = Convert.ToInt32(fo.rcmd_iid_kardex10);
                        talla[0] = Convert.ToInt32(fo.rcmd_talla1);
                        talla[1] = Convert.ToInt32(fo.rcmd_talla2);
                        talla[2] = Convert.ToInt32(fo.rcmd_talla3);
                        talla[3] = Convert.ToInt32(fo.rcmd_talla4);
                        talla[4] = Convert.ToInt32(fo.rcmd_talla5);
                        talla[5] = Convert.ToInt32(fo.rcmd_talla6);
                        talla[6] = Convert.ToInt32(fo.rcmd_talla7);
                        talla[7] = Convert.ToInt32(fo.rcmd_talla8);
                        talla[8] = Convert.ToInt32(fo.rcmd_talla9);
                        talla[9] = Convert.ToInt32(fo.rcmd_talla10);
                        for (int j = 0; j <= 9; j++)
                        {
                            string codigoexterno = fo.strCodProd + talla[j].ToString();
                            oProducto = new BCentral().VerificarProdProducto(codigoexterno);
                            ekar.kardc_iid_correlativo = icod_karde[j];
                            if (oProducto.Count > 0)
                            {
                                //ekar.puvec_icod_punto_venta = Obe.puvec_icod_punto_venta;
                                ekar.iid_producto = oProducto[0].pr_icod_producto;
                                new BCentral().InsertarKardexpvt(ekar);
                            }
                        }
                        new BCompras().eliminarRecepcionComprasMercaderiaDetalle(fo);

                    }
                    viewGuiaRemision.DeleteRow(viewGuiaRemision.FocusedRowHandle);
                    viewGuiaRemision.RefreshData();
                }
            }

        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }


        private void viewGuiaRemision_DoubleClick(object sender, EventArgs e)
        {
            ERecepcionComprasMercaderia obe = (ERecepcionComprasMercaderia)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {

                frmManteRecepcionComprasMercaderia frm = new frmManteRecepcionComprasMercaderia();
                frm.MiEvento += new frmManteRecepcionComprasMercaderia.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.SetCancel();
                frm.Show();
                frm.btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reprocesarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<ERecepcionComprasDetalle> lstDetalle = new List<EGuiaRemisionComprasDetalle>();
            //lstRecepcionCompras.ForEach(x=>
            //{
            //    lstDetalle = new BCompras().listarGuiaRemisionComprasDetalle(x.grcc_icod_grc);
            //    lstDetalle.ForEach(d=>
            //    {
            //        new BCompras().ActualizarCantidadRecibidaOC(d.ocod_icod_detalle_oc);
            //    });
            //    new BCompras().CambiarSituacionOC(x.ococ_icod_orden_compra);
            //});
        }


    }
}