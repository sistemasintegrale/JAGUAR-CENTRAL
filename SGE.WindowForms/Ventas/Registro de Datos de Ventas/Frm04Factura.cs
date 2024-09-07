using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.Entity.FacturaElectronica;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;
using SGE.WindowForms.Ventas.Reporte;
using SGI.WindowsForm.Otros.Ventas;
using SIDE.COMUN.DTO.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class Frm04Factura : DevExpress.XtraEditors.XtraForm
    {
        private List<string> mensajeRespuesta = new List<string>();
        List<EFacturaCab> lstFacturas = new List<EFacturaCab>();
        bool flag_close = false;
        public string MotivoBaja;
        public DateTime FechaActual;
        public Frm04Factura()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            lstFacturas = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_tipo_factura == 1).ToList();
            foreach (var item in lstFacturas)
            {
                FechaActual = DateTime.Now;
                TimeSpan ts = FechaActual - Convert.ToDateTime(item.FEmisionPresentacion);
                int Dias = ts.Days;
                item.Dias = Dias;
                if (item.EstadoFacturacion == (int)EstadoDocumento.aprobado)
                {
                    item.Dias = 0;
                }
                if (item.EstadoFacturacion == (int)EstadoDocumento.enviadoSunat)
                {
                    item.Dias = 0;
                }
            }
            grdFactura.DataSource = lstFacturas;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstFacturas.FindIndex(x => x.favc_icod_factura == intIcod);
            viewFactura.FocusedRowHandle = index;
            viewFactura.Focus();
        }





        private void Frm04Factura_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void modificar()
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {

                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                //List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == obe.doc_icod_documento && x.tipoDocumento == "01").ToList();
                var fact = new BVentas().obtnerEstadoFacturacion(obe.doc_icod_documento, "01");
                //if (lstCab.Count > 0)
                //{
                if (fact.EstadoFacturacion != 4)
                {
                    XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //}

                if (obe.tablc_iid_situacion != 8)//GENERADO
                    throw new ArgumentException(String.Format("La factura no puede ser modificada, su situación es ", obe.strSituacion));
                if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser modificada");

                FrmManteFactura frm = new FrmManteFactura();
                frm.MiEvento += new FrmManteFactura.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.CargarControles();
                frm.SetModify();
                frm.Show();
                frm.BtnGuiaRemision.Enabled = false;

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
        private void anular()
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                if (obe.tablc_iid_situacion != 8)//GENERADO
                    throw new ArgumentException(String.Format("La factura no puede ser anulada, su situación es {0}", obe.strSituacion));
                if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser anulada");
                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronicaXid(obe.doc_icod_documento, "01").ToList();
                if (obe.favc_sfecha_factura >= lstParametro[0].pm_sfecha_inicio)
                {
                    if (lstCab.Count > 0)
                    {
                        if (lstCab[0].EstadoFacturacion == 4)
                        {
                            XtraMessageBox.Show("La Factura de Venta aun no fue enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                if (obe.favc_sfecha_factura > lstParametro[0].pm_sfecha_inicio)
                {
                    if (lstCab[0].EstadoFacturacion == 2)
                    {
                        if (XtraMessageBox.Show("¿Esta seguro que desea Anular el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            FrmComunicacionBaja frm = new FrmComunicacionBaja();
                            frm.obj = obe;
                            frm.SetInsert();
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                flag_close = frm.flag_close;
                            }
                            if (flag_close == true)
                            {

                                MotivoBaja = frm.obj.favc_descripcion_motivo_baja;
                                AnularFacturaElectronica(MotivoBaja);
                                new BVentas().AnularFacturaVenta(obe);
                                reload(obe.favc_icod_factura);
                            }

                        }
                    }
                }
                else
                {
                    if (XtraMessageBox.Show("¿Esta seguro que desea ANULAR la factura?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        new BVentas().AnularFacturaVenta(obe);
                        reload(obe.favc_icod_factura);

                    }
                }

                //if (XtraMessageBox.Show("¿Esta seguro que desea ANULAR la factura?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                //{
                //    new BVentas().AnularFacturaVenta(obe);
                //    reload(obe.favc_icod_factura);

                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void AnularFacturaElectronica(string MotivoBaja)
        {
            int intIcodE = 0;
            EFacturaCab Obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (Obe == null)
                return;
            List<EPuntoVenta> lstPuntoVenta = new BCentral().ListarPuntoVenta().Where(x => x.puvec_icod_punto_venta == 2).ToList();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == Obe.doc_icod_documento && x.tipoDocumento == "01").ToList();

            ESunatDocumentosBaja _Baja = new ESunatDocumentosBaja();

            _Baja.Id = 1;
            _Baja.FechaEmision = lstCab[0].fechaEmision;
            _Baja.TipoDocumento = lstCab[0].tipoDocumento;
            _Baja.Serie = lstCab[0].idDocumento.Remove(4);
            _Baja.Correlativo = lstCab[0].idDocumento.Remove(0, 5);
            _Baja.MotivoBaja = MotivoBaja;
            _Baja.EstadoFacturacion = 4;
            if (lstPuntoVenta.Count > 0)
            {
                _Baja.CodigoSunat = lstPuntoVenta[0].puvec_vcodigo_sunat;
                _Baja.UsuarioOSE = lstPuntoVenta[0].puvec_vusuario_ose;
            }

            new BVentas().insertarSunatDocumentosBajaCab(_Baja);

        }
        private void eliminar()
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            int index = viewFactura.FocusedRowHandle;
            try
            {
                var fact = new BVentas().obtnerEstadoFacturacion(obe.doc_icod_documento, "01");

                if (fact.EstadoFacturacion != 4)
                {
                    XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (obe.tablc_iid_situacion != Parametros.intSitDocCobrarGenerado)
                {
                    if (obe.tablc_iid_situacion != Parametros.intSitDocCobrarAnulado)
                    {
                        throw new ArgumentException(String.Format("La factura no puede ser eliminada, su situación es {0}", obe.strSituacion));
                    }
                }
                if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser eliminada");

                if (XtraMessageBox.Show("¿Esta seguro que desea ELIMINAR la factura?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    obe.intUsuario = Valores.intUsuario;
                    obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().EliminarFacturaVenta(obe);
                    reload(obe.favc_icod_factura);
                    eliminarSUNAT(obe);
                    /***********************************************/
                    if (lstFacturas.Count >= index + 1)
                        viewFactura.FocusedRowHandle = index;
                    else
                        viewFactura.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void eliminarSUNAT(EFacturaCab obe)
        {

            var lstCab = new BVentas().listarfacturaVentaElectronicaXid(obe.favc_icod_factura, "01");
            int IdCabecera = lstCab[0].IdCabecera;
            new BVentas().eliminarFacturaVentaElectronica(IdCabecera);
            if (lstCab.Count > 0)
            {
                new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabecera);
            }
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anular();
        }

        private void viewFactura_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["strSituacion"]);
                if (strSituacion == "ANULADO")
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;

                }
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            anular();
        }

        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdFactura.DataSource = lstFacturas.Where(x => x.favc_vnumero_factura.Trim().Contains(txtNumero.Text.Trim()) &&
                x.cliec_vnombre_cliente.ToUpper().Trim().Contains(txtCliente.Text.Trim().ToUpper())).ToList();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void viewFactura_DoubleClick(object sender, EventArgs e)
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {

                //if (obe.tablc_iid_situacion != Parametros.intSitDocCobrarGenerado)
                //    throw new ArgumentException(String.Format("La factura no puede ser modificada, su situación es ", obe.strSituacion));
                //if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                //    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser modificada");

                FrmManteFactura frm = new FrmManteFactura();
                //frm.MiEvento += new FrmManteFactura.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.setValues();
                //frm.SetCancel();
                frm.Show();
                frm.mnu.Enabled = false;
                frm.btnGuardar.Enabled = false;
                frm.bteCliente.Enabled = false;
                frm.txtSerie.Properties.ReadOnly = true;
                frm.txtNumero.Properties.ReadOnly = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void actualizarDescripcionDeDXCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BVentas().ActualizarDescripcionDXCFAC(lstFacturas);
        }

        private void actualizarCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void facturaGrandeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
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



            string total = Convertir.ConvertNumeroEnLetras(obe.favc_nmonto_total.ToString());
            var lstdet = new BVentas().listarFacturaDetalle(obe.favc_icod_factura);

            List<EFacturaDet> mlistDetalle = new List<EFacturaDet>();
            int cont = 1;
            foreach (var _BE in lstdet)
            {
                mlistDetalle.Add(_BE);

                //string[] partes = partes = _BE.favd_nobservaciones.Split('@');
                //int cont_estapacios = 0;
                //for (int i = 0; i < partes.Length; i++)
                //{
                //    if (partes[i] == "")
                //    {
                //        cont_estapacios = cont_estapacios + 1;
                //    }
                //}
                //if (cont_estapacios != partes.Length)
                //{
                //    for (int i = 0; i < partes.Length; i++)
                //    {
                //        if (i == 0)
                //        {
                //            EFacturaDet __be = new EFacturaDet();
                //            __be.strDesProducto = "    " + partes[i];
                //            __be.OrdenItemImprimir = cont + 1;
                //            cont++;
                //            mlistDetalle.Add(__be);
                //        }
                //        else
                //        {
                //            if (partes[i] != "")
                //            {
                //                EFacturaDet __be = new EFacturaDet();
                //                __be.strDesProducto = "    " + partes[i];
                //                __be.OrdenItemImprimir = cont + 1;
                //                cont++;
                //                mlistDetalle.Add(__be);
                //            }
                //        }
                //    }
                //}

            }

            using (FrmElegirImpresora frmImpresora = new FrmElegirImpresora())
            {
                frmImpresora.cargar();
                frmImpresora.ckImpresora.Checked = true;
                if (frmImpresora.ShowDialog() == DialogResult.OK)
                {
                    rptFactura rpt = new rptFactura();
                    rpt.cargar(obe, mlistDetalle, total, Departamento, Provincia, Distrito);
                    //rpt.Print();
                    rpt.Print(frmImpresora.url_impresora);
                }
            }

        }

        private void facturaChicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
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



            string total = Convertir.ConvertNumeroEnLetras(obe.favc_nmonto_total.ToString());
            var lstdet = new BVentas().listarFacturaDetalle(obe.favc_icod_factura);

            List<EFacturaDet> mlistDetalle = new List<EFacturaDet>();
            int cont = 1;
            foreach (var _BE in lstdet)
            {
                mlistDetalle.Add(_BE);

                string[] partes = partes = _BE.favd_nobservaciones.Split('@');
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
                            EFacturaDet __be = new EFacturaDet();
                            __be.strDesProducto = "    " + partes[i];
                            __be.OrdenItemImprimir = cont + 1;
                            cont++;
                            mlistDetalle.Add(__be);
                        }
                        else
                        {
                            if (partes[i] != "")
                            {
                                EFacturaDet __be = new EFacturaDet();
                                __be.strDesProducto = "    " + partes[i];
                                __be.OrdenItemImprimir = cont + 1;
                                cont++;
                                mlistDetalle.Add(__be);
                            }
                        }
                    }
                }

            }
            rptFacturaChica rpt = new rptFacturaChica();
            rpt.cargar(obe, mlistDetalle, total, Departamento, Provincia, Distrito);
        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteFactura frm = new FrmManteFactura();
            frm.MiEvento += new FrmManteFactura.DelegadoMensaje(reload);
            frm.TipodeFactura = 2;//Suministros
            frm.SetInsert();
            frm.Show();
            frm.CargarControles();

        }

        private void mercaderiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteFactura frm = new FrmManteFactura();
            frm.MiEvento += new FrmManteFactura.DelegadoMensaje(reload);
            frm.TipodeFactura = 1;//Mercaderia
            frm.TDComercial = 680;
            frm.SetInsert();
            frm.Show();
            frm.CargarControles();
        }

        private void imprimirFacturaElectronicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaCab ObeFC = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            List<EFacturaVentaElectronica> lstFE = new List<EFacturaVentaElectronica>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            lstFE = new BVentas().listarfacturaVentaElectronicaXid(ObeFC.favc_icod_factura, "01");
            Obe = lstFE.FirstOrDefault();

            mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);
            mlisdet.ForEach(x =>
            {
                x.ValorUnitarioItem = Math.Round((x.MontoAfectoImpuestoIgv / x.cantidad), 4);
            });


            rptFacturaElectronico rptFcatura = new rptFacturaElectronico();


            if (Obe.tipoDocumento == "01")
            {
                List<EFacturaCab> lstFV = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_icod_factura == Obe.doc_icod_documento).ToList();
                if (lstFV.Count > 0)
                {
                    Obe.favc_vnumero_orden = lstFV[0].favc_vnumero_orden;
                    Obe.favc_vnumero_guia = lstFV[0].favc_vnumero_guia;
                    Obe.PorcRetension = Convert.ToDecimal(lstFV[0].facv_nporc_retencion);
                    Obe.MontoRetension = Convert.ToDecimal(lstFV[0].facv_nmonto_retencion);
                }
                Obe.cuotas = new List<ECuotasFactura>();
                Obe.cuotas = new BVentas().CuotasFacturaListar(ObeFC.doxcc_icod_correlativo);
                rptFcatura.cargar(Obe, mlisdet);
                rptFcatura.ShowPreview();
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void alquileresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteFactura frm = new FrmManteFactura();
            frm.MiEvento += new FrmManteFactura.DelegadoMensaje(reload);
            frm.TipodeFactura = 3;//Alquileres
            frm.SetInsert();
            frm.Show();
            frm.CargarControles();
        }
        public int[] icodTalla = new int[10];
        private void actualizarTallasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EFacturaCab> lstCab = new List<EFacturaCab>();
            lstCab = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_tipo_factura == 1).ToList();
            lstCab.ForEach(x =>
            {
                List<EFacturaDet> lstDet = new List<EFacturaDet>();
                lstDet = new BVentas().listarFacturaDetalle(x.favc_icod_factura);
                lstDet.ForEach(d =>
                {
                    int i = -1;
                    List<ERegistroSerie> lstSerie = new BCentral().ListarRegistroSerie().Where(t => t.resec_iid_registro_serie == d.favd_icod_serie).ToList();
                    for (int tt = Convert.ToInt32(lstSerie[0].resec_vtalla_inicial); tt <= Convert.ToInt32(lstSerie[0].resec_vtalla_final); tt++)
                    {
                        i++;
                        icodTalla[i] = tt;

                    }
                    d.favd_talla1 = icodTalla[0];
                    d.favd_talla2 = icodTalla[1];
                    d.favd_talla3 = icodTalla[2];
                    d.favd_talla4 = icodTalla[3];
                    d.favd_talla5 = icodTalla[4];
                    d.favd_talla6 = icodTalla[5];
                    d.favd_talla7 = icodTalla[6];
                    d.favd_talla8 = icodTalla[7];
                    d.favd_talla9 = icodTalla[8];
                    d.favd_talla10 = icodTalla[9];

                    new BVentas().modificarFacturaDetalle(d);

                });
            });
        }

        private void actualizarPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EFacturaCab> lstCab = new List<EFacturaCab>();
            lstCab = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_tipo_factura == 1).ToList();
            lstCab.ForEach(x =>
            {
                List<EFacturaDet> lstDet = new List<EFacturaDet>();
                lstDet = new BVentas().listarFacturaDetalle(x.favc_icod_factura);
                lstDet.ForEach(d =>
                {
                    d.favd_nporcentaje_descuento_item = Convert.ToDecimal(Parametros.strPorcIGV);
                    d.favd_nmonto_impuesto_item = Math.Round((Convert.ToDecimal((d.favd_nprecio_total_item * Convert.ToDecimal(Parametros.strPorcIGV)) / (100 + Convert.ToDecimal(Parametros.strPorcIGV)))), 2, MidpointRounding.ToEven);
                    d.favd_nneto_igv = Convert.ToDecimal(d.favd_nprecio_total_item) - d.favd_nmonto_impuesto_item;
                    d.favd_nneto_exo = 0;

                    new BVentas().modificarFacturaDetalle(d);

                });
            });
        }

        private void actualizarCabToolStripMenuItem_Click(object sender, EventArgs e)
        {

            List<EFacturaCab> lstCab = new List<EFacturaCab>();
            lstCab = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_tipo_factura == 1).ToList();
            lstCab.ForEach(x =>
            {
                List<EFacturaDet> lstDet = new List<EFacturaDet>();
                lstDet = new BVentas().listarFacturaDetalle(x.favc_icod_factura);

                x.favc_noperacion_gravadas = lstDet.Sum(n => n.favd_nneto_igv);
                x.favc_npor_imp_igv = Convert.ToDecimal(Parametros.strPorcIGV);

                x.favc_nmonto_imp = lstDet.Sum(i => i.favd_nmonto_impuesto_item);
                x.favc_nmonto_neto = lstDet.Sum(n => n.favd_nneto_igv);

                x.favc_noperacion_inafectas = 0;

                new BVentas().modificarFactura(x);

            });
        }

        private async void enviarSUNATToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaCab ObeFC = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (ObeFC is null) return;
            if (ObeFC.EstadoSunat == "APROBADO")
            {
                Services.MessageError("La FAV ya ha sido enviada a SUNAT");
                return;
            }
            DocumentoElectronicoResponse model = new DocumentoElectronicoResponse();
           
            var Obe = new BVentas().listarfacturaVentaElectronicaXid(ObeFC.favc_icod_factura, "01").FirstOrDefault();
            var mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera).Where(x => x.ValorVentaItem != Convert.ToDecimal(0.01)).ToList();
             
            Obe.cuotas = new BVentas().CuotasFacturaListar(ObeFC.doxcc_icod_correlativo);
            DocumentoElectronico objdocumento = new DocumentoElectronico();
            objdocumento.DatosGuiaTransportista = null;
            objdocumento.DatoAdicionales = new List<DatoAdicional>();
            objdocumento.Relacionados = new List<DocumentoRelacionado>();
            objdocumento.OtrosDocumentosRelacionados = new List<DocumentoRelacionado>();
            objdocumento.Discrepancias = new List<Discrepancia>();

            objdocumento = FacturaElectronicaDto.ModelDto(Obe, mlisdet);

            EFacturaElectronicaResponse response = await model.FacturaElectronica(objdocumento);
            if (response.Exito)
            {
                mensajeRespuesta.Add($"{Obe.idDocumento};Se envio correctamente.");
            }
            else
            {
                mensajeRespuesta.Add($"{Obe.idDocumento};Ocurrio un errror, para mas detalle filtar por la opcion rechazados.");
            }

            response.IdCabezera = Obe.IdCabecera;
            int idResponse = new BVentas().insertarFacturaElectronicaResponse(response);
            if (idResponse == 0)
            {
                new BVentas().modificarFacturaElectronicaResponse(Obe.IdCabecera);
                new BVentas().insertarFacturaElectronicaResponse(response);
            }
            if (response.Exito)
            {
                new BVentas().actualizarFacturaElectronicaResponse(Obe.IdCabecera, (int)EstadoDocumento.enviadoSunat);
                int codigoRespuesta = Convert.ToInt32(response.CodigoRespuesta);
                if (codigoRespuesta <= 0)
                {
                    new BVentas().actualizarFacturaElectronicaResponse(Obe.IdCabecera, (int)EstadoDocumento.aprobado);
                }
            }
            else
            {
                int estadoSunat = 0;

                int codigoRespuesta = Convert.ToInt32(response.CodigoRespuesta);
                if (codigoRespuesta >= (int)EstadoDocumento.rangoExcepcionMin &&
                    codigoRespuesta <= (int)EstadoDocumento.rangoExcepcionMax)
                {
                    /*Vuelve al estado de pendientes por enviar para ser modificado*/
                    estadoSunat = (int)EstadoDocumento.rechazado;
                }
                else if (codigoRespuesta >= (int)EstadoDocumento.rangoRechazadoMin &&
                    codigoRespuesta <= (int)EstadoDocumento.rangoRechazadoMax)
                {
                    estadoSunat = (int)EstadoDocumento.rechazado;
                }
                else if (codigoRespuesta >= (int)EstadoDocumento.rangoObservadoMax)
                {
                    /*Vuelve al estado de pendientes por enviar para ser modificado*/
                    estadoSunat = (int)EstadoDocumento.rechazado;
                }

                new BVentas().actualizarFacturaElectronicaResponse(Obe.IdCabecera, estadoSunat);
            }
            reload(ObeFC.favc_icod_factura);
        }
        
        private void grdFactura_Click(object sender, EventArgs e)
        {

        }

        private void cuotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            var listaCuotas = new BVentas().CuotasFacturaListar(obe.doxcc_icod_correlativo);
            var listaCuotasEliminar = new List<ECuotasFactura>();
            FrmManteCuotasFactura frm = new FrmManteCuotasFactura();
            frm.Text = $"Cuotas de la FAV {obe.favc_vnumero_factura}";
            frm.eFacturaCab = obe;
            frm.txtMontoCredito.Text = obe.facv_nmonto_credito.ToString();
            frm.listaCuotas = listaCuotas;
            frm.listaCuotasEliminar = listaCuotasEliminar;
            frm.Status = Maintenance.BSMaintenanceStatus.ModifyCurrent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                listaCuotas = frm.listaCuotas;
                listaCuotasEliminar = frm.listaCuotasEliminar;

                listaCuotas.ForEach(x =>
                {
                    x.favc_icod_factura = obe.favc_icod_factura;
                    x.doxcc_icod_correlativo = (int)obe.doxcc_icod_correlativo;
                    if (x.operacion == 2)
                    {
                        new SGE.DataAccess.VentasData().CuotasFacturaModificar(x);
                    }
                    if (x.fcc_icod == 0)
                    {
                        x.fcc_icod = new SGE.DataAccess.VentasData().CuotasFacturaIngresar(x);
                    }
                });

                listaCuotasEliminar.ForEach(x =>
                {
                    new SGE.DataAccess.VentasData().CuotasFacturaEliminar(x);
                });
            }
        }
    }
}