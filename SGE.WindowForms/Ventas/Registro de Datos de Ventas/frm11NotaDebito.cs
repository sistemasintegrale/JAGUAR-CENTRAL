using DevExpress.XtraEditors;
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
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class frm11NotaDebito : DevExpress.XtraEditors.XtraForm
    {
        List<ENotaDebito> lstNotaCredito = new List<ENotaDebito>();
        bool flag_close = false;
        public string MotivoBaja;
        public frm11NotaDebito()
        {
            InitializeComponent();
        }



        private void cargar()
        {

            lstNotaCredito = new BVentas().listarNotaDebitoClienteCab(Parametros.intEjercicio);
            grdNotaCredito.DataSource = lstNotaCredito;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstNotaCredito.FindIndex(x => x.ncrec_icod_credito == intIcod);
            viewNotaCredito.FocusedRowHandle = index;
            viewNotaCredito.Focus();
        }

        private void nuevo()
        {
            decimal nmonto_tipoCambio = new BContabilidad().getTipoCambioPorFecha(DateTime.Now);
            if (nmonto_tipoCambio != 0)
            {
                FrmManteNotaDebito frm = new FrmManteNotaDebito();
                frm.MiEvento += new FrmManteNotaDebito.DelegadoMensaje(reload);
                frm.lstCabeceras = lstNotaCredito;
                frm.SetInsert();
                frm.Show();
                //frm.lkpMoneda.EditValue = 4;//DOLARES
                //frm.txtSerie.Text = "001";
            }
            else
            {
                XtraMessageBox.Show("No existe el Tipo de Cambio a la Fecha, Ingrese el Tipo Cambio para seguir con este proceso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar()
        {
            ENotaDebito oBe = (ENotaDebito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == oBe.doc_icod_documento && x.tipoDocumento == "08").ToList();
                if (lstCab.Count > 0)
                {
                    if (lstCab[0].EstadoFacturacion != 4)
                    {
                        XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                if (oBe.ncrec_iid_situacion_credito != Parametros.intSitDocCobrarGenerado)
                    throw new ArgumentException(String.Format("La N/C no puede ser modificada, su situación es {0}", oBe.strSituacion));

                FrmManteNotaDebito frm = new FrmManteNotaDebito();
                frm.MiEvento += new FrmManteNotaDebito.DelegadoMensaje(reload);
                frm.oBe = oBe;
                frm.SetModify();
                frm.Show();
                frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminar()
        {
            ENotaDebito oBe = (ENotaDebito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (oBe == null)
                return;
            int index = viewNotaCredito.FocusedRowHandle;
            try
            {
                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == oBe.doc_icod_documento && x.tipoDocumento == "08").ToList();
                if (lstCab.Count > 0)
                {
                    if (lstCab[0].EstadoFacturacion != 4)
                    {
                        XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                if (oBe.ncrec_iid_situacion_credito != Parametros.intSitDocCobrarGenerado)
                    throw new ArgumentException(String.Format("La N/C no puede ser eliminada, su situación es {0}", oBe.strSituacion));

                if (XtraMessageBox.Show(String.Format("¿Está seguro que desea eliminar la NC {0}?", oBe.ncrec_vnumero_credito), "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    oBe.intUsuario = Valores.intUsuario;
                    oBe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarNotaDebitoClienteCab(oBe);
                    /**/
                    reload(oBe.ncrec_icod_credito);
                    /***********************************************/
                    if (lstNotaCredito.Count >= index + 1)
                        viewNotaCredito.FocusedRowHandle = index;
                    else
                        viewNotaCredito.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ver()
        {
            ENotaDebito oBe = (ENotaDebito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                //frmManteFacturaCompra frm = new frmManteFacturaCompra();
                //frm.MiEvento += new frmManteFacturaCompra.DelegadoMensaje(reload);
                //frm.Obe = obe;
                //frm.SetCancel();
                //frm.Show();
                //frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void filtrar()
        {
            grdNotaCredito.DataSource = lstNotaCredito.Where(x => x.ncrec_vnumero_credito.Contains(textEdit1.Text) &&
                x.strDesCliente.Contains(textEdit2.Text.ToUpper())).ToList();
        }

        private void viewDocCompra_DoubleClick(object sender, EventArgs e)
        {
            ver();
        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void frm11NotaCredito_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<ENotaCreditoDet> lstDetalle = new List<ENotaCreditoDet>();
            //ENotaCredito oBe = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            //if (oBe == null)
            //    return;

            //lstDetalle = new BVentas().listarNotaCreditoClienteDet(oBe.ncrec_icod_credito);

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
            //lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == oBe.ubicc_icod_ubicacion).ToList().ForEach(oB =>
            //{
            //    tipo = oB.tablc_iid_tipo_ubicacion;
            //    padre = oB.ubicc_icod_ubicacion_padre;
            //});
            //switch (tipo)
            //{
            //    case 4:
            //        Dios = oBe.ubicc_icod_ubicacion;
            //        break;
            //    case 3:
            //        foreach (var _BBE in lUbicacion)
            //        {
            //            if (_BBE.ubicc_icod_ubicacion == oBe.ubicc_icod_ubicacion)
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
            //            if (_BBE.ubicc_icod_ubicacion == oBe.ubicc_icod_ubicacion)
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
            //            if (_BBE.ubicc_icod_ubicacion == oBe.ubicc_icod_ubicacion)
            //            {
            //                Distrito = _BBE.ubicc_vnombre_ubicacion;
            //            }
            //        }
            //        break;

            //}


            //string total = Convertir.ConvertNumeroEnLetras(oBe.ncrec_nmonto_total.ToString());

            //rptNotaCredito rpt = new rptNotaCredito();
            //rpt.cargar(oBe, lstDetalle, total, Departamento, Provincia, Distrito);
        }

        private void imprimirFacturaElectronicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ENotaDebito ObeFC = (ENotaDebito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            List<EFacturaVentaElectronica> lstFE = new List<EFacturaVentaElectronica>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            lstFE = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == ObeFC.ncrec_icod_credito && x.tipoDocumento == "08").ToList();
            Obe = lstFE.FirstOrDefault();

            mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);

            rptNotaDebitoElectronico rptFcatura = new rptNotaDebitoElectronico();

            if (Obe.tipoDocumento == "08")
            {
                List<ENotaDebito> lstFV = new BVentas().listarNotaDebitoClienteCab(Parametros.intEjercicio).Where(x => x.ncrec_icod_credito == Obe.doc_icod_documento).ToList();
                if (lstFV.Count > 0)
                {
                    Obe.FechaReferencia = ObeFC.ncrec_sfecha_documento.ToString("dd/MM/yyyy");
                    Obe.DetalleDes = lstFV[0].ncrec_vdetalle;
                }

                rptFcatura.cargar(Obe, mlisdet);
                rptFcatura.ShowPreview();
            }
        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anular();
        }
        public void anular()
        {
            ENotaDebito obe = (ENotaDebito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                //if (obe.tablc_iid_situacion != 8)//GENERADO
                //    throw new ArgumentException(String.Format("La factura no puede ser anulada, su situación es {0}", obe.strSituacion));
                //if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                //    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser anulada");
                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == obe.doc_icod_documento && x.tipoDocumento == "08").ToList();
                if (obe.ncrec_sfecha_credito >= lstParametro[0].pm_sfecha_inicio)
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
                if (obe.ncrec_sfecha_credito > lstParametro[0].pm_sfecha_inicio)
                {
                    if (lstCab[0].EstadoFacturacion == 2)
                    {
                        if (XtraMessageBox.Show("¿Esta seguro que desea Anular el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            FrmComunicacionBajaND frm = new FrmComunicacionBajaND();
                            frm.obj = obe;
                            frm.SetInsert();
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                flag_close = frm.flag_close;
                            }
                            if (flag_close == true)
                            {

                                MotivoBaja = frm.obj.ncrec_descripcion_motivo_baja;
                                AnularFacturaElectronica(MotivoBaja);
                                new BVentas().anularNotaDebitoClienteCab(obe);
                                reload(obe.ncrec_icod_credito);
                            }

                        }
                    }
                }
                else
                {
                    if (XtraMessageBox.Show("¿Esta seguro que desea ANULAR la factura?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        new BVentas().anularNotaDebitoClienteCab(obe);
                        reload(obe.ncrec_icod_credito);

                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void AnularFacturaElectronica(string MotivoBaja)
        {
            int intIcodE = 0;
            ENotaDebito Obe = (ENotaDebito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (Obe == null)
                return;
            List<EPuntoVenta> lstPuntoVenta = new BCentral().ListarPuntoVenta().Where(x => x.puvec_icod_punto_venta == 2).ToList();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == Obe.doc_icod_documento && x.tipoDocumento == "08").ToList();

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

    }
}