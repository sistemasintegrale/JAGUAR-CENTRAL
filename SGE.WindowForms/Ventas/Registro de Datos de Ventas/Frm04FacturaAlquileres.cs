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
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class Frm04FacturaAlquileres : DevExpress.XtraEditors.XtraForm
    {
        List<EFacturaCab> lstFacturas = new List<EFacturaCab>();
        bool flag_close = false;
        public string MotivoBaja;
        public Frm04FacturaAlquileres()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            lstFacturas = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_tipo_factura == 3).ToList();
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
                var fact = new BVentas().obtnerEstadoFacturacion(obe.doc_icod_documento, "01");
                if (fact.EstadoFacturacion != 4)
                {
                    XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
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
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == obe.doc_icod_documento && x.tipoDocumento == "01").ToList();
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
            List<EFacturaVentaElectronica> lstCab = new List<EFacturaVentaElectronica>();
            List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
            lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == obe.favc_icod_factura && x.tipoDocumento == "01").ToList();
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
            frm.SetInsert();
            frm.Show();
            frm.CargarControles();

            //frm.lkpMoneda.EditValue = 4;//DOLARES
            //frm.txtSerie.Text = "001";
        }

        private void imprimirFacturaElectronicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaCab ObeFC = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            List<EFacturaVentaElectronica> lstFE = new List<EFacturaVentaElectronica>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            lstFE = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == ObeFC.favc_icod_factura).ToList();
            Obe = lstFE.FirstOrDefault();

            mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);

            rptFacturaElectronico rptFcatura = new rptFacturaElectronico();

            if (Obe.tipoDocumento == "01")
            {
                List<EFacturaCab> lstFV = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_icod_factura == Obe.doc_icod_documento).ToList();
                if (lstFV.Count > 0)
                {
                    Obe.favc_vnumero_orden = lstFV[0].favc_vnumero_orden;
                    Obe.favc_vnumero_guia = lstFV[0].favc_vnumero_guia;
                }

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
            frm.TDComercial = 682;
            frm.SetInsert();
            frm.Show();
            frm.CargarControles();
        }

        private void actualizarDetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EFacturaCab> lstCab = new List<EFacturaCab>();
            lstCab = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_tipo_factura == 3).ToList();
            lstCab.ForEach(x =>
            {
                List<EFacturaMPDet> lstDet = new List<EFacturaMPDet>();
                lstDet = new BVentas().listarFacturaMPDetalle(x.favc_icod_factura);
                lstDet.ForEach(d =>
                {
                    d.favd_nporcentaje_descuento_item = Convert.ToDecimal(Parametros.strPorcIGV);
                    d.favd_nmonto_impuesto_item = Math.Round((Convert.ToDecimal((d.favd_nprecio_total_item * Convert.ToDecimal(Parametros.strPorcIGV)) / (100 + Convert.ToDecimal(Parametros.strPorcIGV)))), 2, MidpointRounding.ToEven);
                    d.favd_nneto_igv = Convert.ToDecimal(d.favd_nprecio_total_item) - d.favd_nmonto_impuesto_item;
                    d.favd_nneto_exo = 0;

                    new BVentas().modificarFacturaMPDetalle(d);

                });
            });
        }

        private void actualizarCabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EFacturaCab> lstCab = new List<EFacturaCab>();
            lstCab = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_tipo_factura == 3).ToList();
            lstCab.ForEach(x =>
            {
                List<EFacturaMPDet> lstDet = new List<EFacturaMPDet>();
                lstDet = new BVentas().listarFacturaMPDetalle(x.favc_icod_factura);

                x.favc_noperacion_gravadas = lstDet.Sum(n => n.favd_nneto_igv);
                x.favc_npor_imp_igv = Convert.ToDecimal(Parametros.strPorcIGV);

                x.favc_nmonto_imp = lstDet.Sum(i => i.favd_nmonto_impuesto_item);
                x.favc_nmonto_neto = lstDet.Sum(n => n.favd_nneto_igv);

                x.favc_noperacion_inafectas = 0;

                new BVentas().modificarFactura(x);

            });
        }
    }
}