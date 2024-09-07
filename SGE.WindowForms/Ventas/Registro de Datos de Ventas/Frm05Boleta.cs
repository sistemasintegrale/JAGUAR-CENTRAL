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
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class Frm05Boleta : DevExpress.XtraEditors.XtraForm
    {
        List<EBoletaCab> lstBoletas = new List<EBoletaCab>();
        bool flag_close = false;
        public string MotivoBaja;
        public Frm05Boleta()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            lstBoletas = new BVentas().listarBoletaCab(Parametros.intEjercicio);
            grdBoleta.DataSource = lstBoletas;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstBoletas.FindIndex(x => x.bovc_icod_boleta == intIcod);
            viewBoleta.FocusedRowHandle = index;
            viewBoleta.Focus();
        }


        private void nuevo()
        {
            FrmManteBoleta frm = new FrmManteBoleta();
            frm.MiEvento += new FrmManteBoleta.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.TDComercial = 683;
            frm.Show();
            frm.CargarControles();
            //frm.lkpMoneda.EditValue = 4;//DOLARES
            //frm.txtSerie.Text = "001";
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
            EBoletaCab obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                //List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == obe.doc_icod_documento && x.tipoDocumento == "03").ToList();
                //if (lstCab.Count > 0)
                //{
                var estBoleta = new BVentas().obtnerEstadoFacturacion(obe.doc_icod_documento, "03");
                if (estBoleta.EstadoFacturacion != 4)
                {
                    XtraMessageBox.Show("La Boleta de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //}
                if (obe.tablc_iid_situacion != 8)
                    throw new ArgumentException(String.Format("La boleta no puede ser modificada, su situación es : {0} ", obe.strSituacion));
                if (verificarDocVentaPlanilla(obe.bovc_icod_boleta))
                    throw new ArgumentException("La boleta ha sido generada desde una Planilla de Venta, no puede ser Modificada");
                FrmManteBoleta frm = new FrmManteBoleta();
                frm.MiEvento += new FrmManteBoleta.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.CargarControles();
                frm.SetModify();
                frm.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool verificarDocVentaPlanilla(int intIcodDoc)
        {
            bool flagExiste = false;
            var intFlag = new BVentas().verificarDocVentaPlanilla(Parametros.intTipoDocBoletaVenta, intIcodDoc);
            if (intFlag > 0)
                flagExiste = true;

            return flagExiste;

        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void anular()
        {
            EBoletaCab obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                //if (obe.tablc_iid_situacion != 8)
                //    throw new ArgumentException(String.Format("La boleta no puede ser anulada, su situación es {0}", obe.strSituacion));
                //if (verificarDocVentaPlanilla(obe.bovc_icod_boleta))
                //    throw new ArgumentException("La boleta ha sido generada desde una Planilla de Venta, no puede ser anulada");
                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == obe.doc_icod_documento && x.tipoDocumento == "03").ToList();
                if (obe.bovc_sfecha_boleta >= lstParametro[0].pm_sfecha_inicio)
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
                if (obe.bovc_sfecha_boleta > lstParametro[0].pm_sfecha_inicio)
                {
                    if (lstCab[0].EstadoFacturacion == 2)
                    {
                        if (XtraMessageBox.Show("¿Esta seguro que desea Anular el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            FrmComunicacionBajaBoleta frm = new FrmComunicacionBajaBoleta();
                            frm.obj = obe;
                            frm.SetInsert();
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                flag_close = frm.flag_close;
                            }
                            if (flag_close == true)
                            {

                                MotivoBaja = frm.obj.bovc_descripcion_motivo_baja;
                                AnularFacturaElectronica(MotivoBaja);
                                new BVentas().AnularBoletaVenta(obe);
                                reload(obe.bovc_icod_boleta);
                            }

                        }
                    }
                }
                else
                {
                    if (XtraMessageBox.Show("¿Esta seguro que desea ANULAR la factura?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        new BVentas().AnularBoletaVenta(obe);
                        reload(obe.bovc_icod_boleta);

                    }
                }



                //if (XtraMessageBox.Show("¿Esta seguro que desea ANULAR la boleta?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                //{
                //    new BVentas().AnularBoletaVenta(obe);
                //    reload(obe.bovc_icod_boleta);

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
            EBoletaCab Obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            if (Obe == null)
                return;
            List<EPuntoVenta> lstPuntoVenta = new BCentral().ListarPuntoVenta().Where(x => x.puvec_icod_punto_venta == 2).ToList();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == Obe.doc_icod_documento && x.tipoDocumento == "03").ToList();

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
            EBoletaCab obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            if (obe == null)
                return;
            int index = viewBoleta.FocusedRowHandle;
            try
            {
                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == obe.doc_icod_documento && x.tipoDocumento == "03").ToList();
                if (lstCab.Count > 0)
                {
                    if (lstCab[0].EstadoFacturacion != 4)
                    {
                        XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                if (obe.tablc_iid_situacion != Parametros.intSitDocCobrarGenerado)
                {
                    if (obe.tablc_iid_situacion != Parametros.intSitDocCobrarAnulado)
                    {
                        throw new ArgumentException(String.Format("La boleta no puede ser eliminada, su situación es {0}", obe.strSituacion));
                    }
                }
                if (verificarDocVentaPlanilla(obe.bovc_icod_boleta))
                    throw new ArgumentException("La boleta ha sido generada desde una Planilla de Venta, no puede ser eliminada");
                if (XtraMessageBox.Show("¿Esta seguro que desea ELIMINAR la boleta?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    new BVentas().eliminarBoletaVenta(obe);
                    reload(obe.bovc_icod_boleta);
                    eliminarSUNAT(obe);
                    /***********************************************/
                    if (lstBoletas.Count >= index + 1)
                        viewBoleta.FocusedRowHandle = index;
                    else
                        viewBoleta.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void eliminarSUNAT(EBoletaCab obe)
        {
            List<EFacturaVentaElectronica> lstCab = new List<EFacturaVentaElectronica>();
            List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
            lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == obe.bovc_icod_boleta && x.tipoDocumento == "03").ToList();
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
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnAnular_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            anular();
        }

        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdBoleta.DataSource = lstBoletas.Where(x => x.bovc_vnumero_boleta.Trim().Contains(txtNumero.Text.Trim()) &&
                x.cliec_vnombre_cliente.ToUpper().Trim().Contains(txtCliente.Text.Trim().ToUpper())).ToList();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void actualizarDescripcionDeDXCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BVentas().ActualizarDescripcionDXCBov(lstBoletas);
        }

        private void viewBoleta_DoubleClick(object sender, EventArgs e)
        {
            EBoletaCab obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {

                FrmManteBoleta frm = new FrmManteBoleta();
                frm.MiEvento += new FrmManteBoleta.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.SetModify();
                frm.Show();
                frm.txtMontoIGV.Properties.ReadOnly = true;
                frm.mnu.Enabled = false;
                frm.btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void imprimirBoletaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaCab obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
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


            string total = Convertir.ConvertNumeroEnLetras(obe.bovc_nmonto_total.ToString());

            var lstdet = new BVentas().listarBoletaDetalle(obe.bovc_icod_boleta);

            List<EBoletaDet> mlistDetalle = new List<EBoletaDet>();
            int cont = 1;
            foreach (var _BE in lstdet)
            {
                mlistDetalle.Add(_BE);

                string[] partes = partes = _BE.bolvd_vobservaciones.Split('@');
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
                            EBoletaDet __be = new EBoletaDet();
                            __be.strDesProducto = "    " + partes[i];
                            __be.OrdenItemImprimir = cont + 1;
                            cont++;
                            mlistDetalle.Add(__be);
                        }
                        else
                        {
                            if (partes[i] != "")
                            {
                                EBoletaDet __be = new EBoletaDet();
                                __be.strDesProducto = "    " + partes[i];
                                __be.OrdenItemImprimir = cont + 1;
                                cont++;
                                mlistDetalle.Add(__be);
                            }
                        }
                    }
                }

            }


            rptBoleta rpt = new rptBoleta();
            rpt.cargar(obe, mlistDetalle, total, Departamento, Provincia, Distrito);
        }

        private void imprimirBoletaChicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaCab obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
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


            string total = Convertir.ConvertNumeroEnLetras(obe.bovc_nmonto_total.ToString());

            var lstdet = new BVentas().listarBoletaDetalle(obe.bovc_icod_boleta);

            List<EBoletaDet> mlistDetalle = new List<EBoletaDet>();
            int cont = 1;
            foreach (var _BE in lstdet)
            {
                mlistDetalle.Add(_BE);

                string[] partes = partes = _BE.bolvd_vobservaciones.Split('@');
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
                            EBoletaDet __be = new EBoletaDet();
                            __be.strDesProducto = "    " + partes[i];
                            __be.OrdenItemImprimir = cont + 1;
                            cont++;
                            mlistDetalle.Add(__be);
                        }
                        else
                        {
                            if (partes[i] != "")
                            {
                                EBoletaDet __be = new EBoletaDet();
                                __be.strDesProducto = "    " + partes[i];
                                __be.OrdenItemImprimir = cont + 1;
                                cont++;
                                mlistDetalle.Add(__be);
                            }
                        }
                    }
                }

            }


            rptBoletaChica rpt = new rptBoletaChica();
            rpt.cargar(obe, mlistDetalle, total, Departamento, Provincia, Distrito);
        }

        private void afectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaCab Obea = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            BVentas afecto = new BVentas();
            Obea.bfavc_bafecto_igv = true;
            afecto.ActualizarAfecto(Obea.bovc_icod_boleta, Obea.bfavc_bafecto_igv);

            cargar();
            int index = lstBoletas.FindIndex(x => x.bovc_icod_boleta == Obea.bovc_icod_boleta);
            viewBoleta.FocusedRowHandle = index;

        }

        private void inafectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaCab Obea = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            BVentas afecto = new BVentas();
            Obea.bfavc_bafecto_igv = false;
            afecto.ActualizarAfecto(Obea.bovc_icod_boleta, Obea.bfavc_bafecto_igv);

            cargar();
            int index = lstBoletas.FindIndex(x => x.bovc_icod_boleta == Obea.bovc_icod_boleta);
            viewBoleta.FocusedRowHandle = index;
        }

        private void imprimirFacturaElectronicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaCab ObeFC = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            List<EFacturaVentaElectronica> lstFE = new List<EFacturaVentaElectronica>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            lstFE = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == ObeFC.bovc_icod_boleta).ToList();
            Obe = lstFE.FirstOrDefault();

            mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);

            rptBoletaElectronico rptBoleta = new rptBoletaElectronico();

            if (Obe.tipoDocumento == "01")
            {
                rptBoleta.cargar(Obe, mlisdet);
                rptBoleta.ShowPreview();
            }
        }
    }
}