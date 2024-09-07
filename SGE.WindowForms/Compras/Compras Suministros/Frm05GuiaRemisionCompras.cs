using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Compras;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Compras_Suministros
{
    public partial class Frm05GuiaRemisionCompras : DevExpress.XtraEditors.XtraForm
    {
        List<EGuiaRemisionCompras> lstGuiaRemisionCompras = new List<EGuiaRemisionCompras>();
        public Frm05GuiaRemisionCompras()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            lstGuiaRemisionCompras = new BCompras().listarGuiaRemisionCompras(Parametros.intEjercicio);


            var lstFacturas = new BCompras().listarFacCompra(Parametros.intEjercicio);

            #region NumFac
            lstGuiaRemisionCompras.ForEach(x =>
            {
                x.NumFac = "";

                /*List<EFacturaCompra> lstFC = new BCompras().listarFacCompra(Parametros.intEjercicio).Where(fc => fc.fcoc_icod_documento == x.grcc_icod_grc && fc.fcoc_tipo_documento == 103).ToList()*//*;*/

                var lstFC = lstFacturas.Where(fc => fc.fcoc_icod_documento == x.grcc_icod_grc && fc.fcoc_tipo_documento == 103).ToList();

                if (lstFC.Count > 0)
                {
                    int count = 0;
                    lstFC.ForEach(f =>
                    {
                        count++;
                        if (count == 1)
                        {
                            x.NumFac = f.fcoc_num_doc;
                        }
                        else
                        {
                            x.NumFac += '/' + f.fcoc_num_doc + '/';
                        }
                    });

                }

            });
            #endregion
            grdGuiaRemision.DataSource = lstGuiaRemisionCompras;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstGuiaRemisionCompras.FindIndex(x => x.grcc_icod_grc == intIcod);
            viewGuiaRemision.FocusedRowHandle = index;
            viewGuiaRemision.Focus();
        }

        private void nuevo()
        {
            frmManteGuiaRemisionCompras frm = new frmManteGuiaRemisionCompras();
            frm.MiEvento += new frmManteGuiaRemisionCompras.DelegadoMensaje(reload);
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
            EGuiaRemisionCompras obe = (EGuiaRemisionCompras)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;

            try
            {
                if (obe.tablc_iid_situacion_grc == 219)
                {
                    sit = 219;
                    descripcion_situacion = "FACTURADA";
                }
                if (obe.tablc_iid_situacion_grc == 220)
                {
                    sit = 220;
                    descripcion_situacion = "ANULADA";
                }
                if (sit == 0)
                {
                    frmManteGuiaRemisionCompras frm = new frmManteGuiaRemisionCompras();
                    frm.MiEvento += new frmManteGuiaRemisionCompras.DelegadoMensaje(reload);
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
            grdGuiaRemision.DataSource = lstGuiaRemisionCompras.Where(x => x.grcc_vnumero_grc.Trim().Contains(txtNumero.Text.Trim()) &&
                x.NomProveedor.ToUpper().Trim().Contains(txtProveedor.Text.Trim().ToUpper())).ToList();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;



            List<EUbicacion> lUbicacion = new BVentas().ListarUbicacion();
            int? tipo = null;
            int? Dios = null;
            int? padre = null;
            int? abuelo = null;
            int? bisabuelo = null;

            string pais;
            string Departamento = "";
            string Provincia = "";
            string Distrito = "";
            lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion).ToList().ForEach(oB =>
            {
                tipo = oB.tablc_iid_tipo_ubicacion;
                padre = oB.ubicc_icod_ubicacion_padre;
            });
            switch (tipo)
            {
                case 4:
                    Dios = obe.ubicc_icod_ubicacion;
                    break;
                case 3:
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Departamento = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }

                    break;
                case 2:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                        Departamento = oB.ubicc_vnombre_ubicacion;
                    });
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Provincia = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }
                    break;
                case 1:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                        Provincia = oB.ubicc_vnombre_ubicacion;
                    });

                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == abuelo).ToList().ForEach(oB =>
                    {
                        bisabuelo = oB.ubicc_icod_ubicacion_padre;
                        Departamento = oB.ubicc_vnombre_ubicacion;
                    });
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Distrito = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }
                    break;

            }
            var lstdet = new BVentas().listarGuiaRemisionDet(obe.remic_icod_remision, Parametros.intEjercicio);


            List<EGuiaRemisionDet> mlistDetalle = new List<EGuiaRemisionDet>();
            int cont = 1;
            foreach (var _BE in lstdet)
            {
                mlistDetalle.Add(_BE);

                string[] partes = partes = _BE.dremc_vobservaciones.Split('@');
                int cont_estapacios = 0;
                for (int i = 0; i < partes.Length; i++)
                {
                    if (partes[i] == "")
                    {
                        cont_estapacios = cont_estapacios + 1;
                    }
                }
                if (cont_estapacios != partes.Length)
                {
                    for (int i = 0; i < partes.Length; i++)
                    {
                        if (i == 0)
                        {
                            EGuiaRemisionDet __be = new EGuiaRemisionDet();
                            __be.strDesProducto = "" + partes[i];
                            __be.OrdenItemImprimir = cont + 1;
                            cont++;
                            mlistDetalle.Add(__be);
                        }
                        else
                        {
                            if (partes[i] != "")
                            {
                                EGuiaRemisionDet __be = new EGuiaRemisionDet();
                                __be.strDesProducto = "" + partes[i];
                                __be.OrdenItemImprimir = cont + 1;
                                cont++;
                                mlistDetalle.Add(__be);
                            }
                        }
                    }
                }
            }
            rptGuiaRemision rpt = new rptGuiaRemision();
            //rpt.cargar(obe, mlistDetalle,"","","");
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void eliminar()
        {
            EGuiaRemisionCompras obe = (EGuiaRemisionCompras)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;


            if (obe.tablc_iid_situacion_grc == 219)
            {
                XtraMessageBox.Show("La Guá Remision No se puede Eliminar, su Situación es FACTURADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }

            if (XtraMessageBox.Show("¿Esta seguro que desea ELIMINAR la Guía de Remisión?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            obe.intUsuario = Valores.intUsuario;
            obe.strPc = WindowsIdentity.GetCurrent().Name;
            new BCompras().eliminarGuiaRemisionCompras(obe);
            reload(obe.grcc_icod_grc);



        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }


        private void viewGuiaRemision_DoubleClick(object sender, EventArgs e)
        {
            EGuiaRemisionCompras obe = (EGuiaRemisionCompras)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {

                frmManteGuiaRemisionCompras frm = new frmManteGuiaRemisionCompras();
                frm.MiEvento += new frmManteGuiaRemisionCompras.DelegadoMensaje(reload);
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
            List<EGuiaRemisionComprasDetalle> lstDetalle = new List<EGuiaRemisionComprasDetalle>();
            lstGuiaRemisionCompras.ForEach(x =>
            {
                lstDetalle = new BCompras().listarGuiaRemisionComprasDetalle(x.grcc_icod_grc);
                lstDetalle.ForEach(d =>
                {
                    new BCompras().ActualizarCantidadRecibidaOC(d.ocod_icod_detalle_oc);
                });
                new BCompras().CambiarSituacionOC(x.ococ_icod_orden_compra);
            });
        }


    }
}