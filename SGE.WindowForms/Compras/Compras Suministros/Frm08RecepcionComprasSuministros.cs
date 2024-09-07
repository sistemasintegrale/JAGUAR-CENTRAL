using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Compras.ComprasSuministros;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Compras_Suministros
{
    public partial class Frm08RecepcionComprasSuministros : DevExpress.XtraEditors.XtraForm
    {
        List<ERecepcionComprasSuministros> lstGuiaRemisionCompras = new List<ERecepcionComprasSuministros>();
        public Frm08RecepcionComprasSuministros()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            lstGuiaRemisionCompras = new BCompras().listarRecepcionComprasSuministros().Where(x => x.rcsc_sfecha_rcs.Year == Parametros.intEjercicio).ToList();
            grdGuiaRemision.DataSource = lstGuiaRemisionCompras;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstGuiaRemisionCompras.FindIndex(x => x.rcsc_icod_rcs == intIcod);
            viewGuiaRemision.FocusedRowHandle = index;
            viewGuiaRemision.Focus();
        }

        private void nuevo()
        {
            frmManteRecepcionComprasSuministros frm = new frmManteRecepcionComprasSuministros();
            frm.MiEvento += new frmManteRecepcionComprasSuministros.DelegadoMensaje(reload);
            frm.txtSerie.Text = Parametros.intEjercicio.ToString();
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
            ERecepcionComprasSuministros obe = (ERecepcionComprasSuministros)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {

                frmManteRecepcionComprasSuministros frm = new frmManteRecepcionComprasSuministros();
                frm.MiEvento += new frmManteRecepcionComprasSuministros.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.SetModify();
                frm.Show();
                frm.CalcularMontos();


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
            grdGuiaRemision.DataSource = lstGuiaRemisionCompras.Where(x => x.rcsc_vnumero_rcs.Trim().Contains(txtNumero.Text.Trim()) &&
                x.NomProveedor.ToUpper().Trim().Contains(txtProveedor.Text.Trim().ToUpper())).ToList();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ERecepcionComprasSuministros ObeRcs = (ERecepcionComprasSuministros)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            List<ERecepcionComprasSuministros> lstRcs = new List<ERecepcionComprasSuministros>();
            ERecepcionComprasSuministros Obe = new ERecepcionComprasSuministros();
            List<ERecepcionComprasSuministrosDetalle> mlisdet = new List<ERecepcionComprasSuministrosDetalle>();

            lstRcs = new BCompras().listarRecepcionComprasSuministros().Where(x => x.rcsc_icod_rcs == ObeRcs.rcsc_icod_rcs).ToList();
            Obe = lstRcs.FirstOrDefault();

            mlisdet = new BCompras().listarRecepcionComprasSuministrosDetalle(Obe.rcsc_icod_rcs);

            rptRecepcionComprasSuministros rptRcms = new rptRecepcionComprasSuministros();

            rptRcms.cargar(Obe, mlisdet);
            rptRcms.ShowPreview();

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
            ERecepcionComprasSuministros obe = (ERecepcionComprasSuministros)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                List<ERecepcionComprasSuministrosDetalle> _BEguiaConstatar = new BCompras().listarRecepcionComprasSuministrosDetalle(obe.rcsc_icod_rcs);
                //if (_BEguiaConstatar.tablc_iid_situacion_remision != 218)
                //{
                //    throw new ArgumentException(String.Format("La Guia de Remisión no puede ser eliminada, su situación es {0}", _BEguiaConstatar.StrSitucion));
                //    int xPosition = viewGuiaRemision.FocusedRowHandle;
                //    cargar();
                //    viewGuiaRemision.FocusedRowHandle = xPosition;
                //}
                //if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                //    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser eliminada");
                if (XtraMessageBox.Show("¿Esta seguro que desea ELIMINAR la Guía de Remisión?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    new BCompras().eliminarRecepcionComprasSuministros(obe);
                    reload(obe.rcsc_icod_rcs);

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }


        private void viewGuiaRemision_DoubleClick(object sender, EventArgs e)
        {
            ERecepcionComprasSuministros obe = (ERecepcionComprasSuministros)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {

                frmManteRecepcionComprasSuministros frm = new frmManteRecepcionComprasSuministros();
                frm.MiEvento += new frmManteRecepcionComprasSuministros.DelegadoMensaje(reload);
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


    }
}