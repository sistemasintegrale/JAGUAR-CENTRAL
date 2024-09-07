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
    public partial class frm11NotaCreditoNoComercial : DevExpress.XtraEditors.XtraForm
    {
        List<ENotaCredito> lstNotaCredito = new List<ENotaCredito>();
        bool flag_close = false;
        public string MotivoBaja;
        public frm11NotaCreditoNoComercial()
        {
            InitializeComponent();
        }



        private void cargar()
        {

            lstNotaCredito = new BVentas().listarNotaCreditoClienteCab(Parametros.intEjercicio).Where(OB => OB.ncrec_tipo_nota_credito == 2).ToList();//2 NO COMERCIAL

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
                FrmManteNotaCreditoNoComercial frm = new FrmManteNotaCreditoNoComercial();
                frm.MiEvento += new FrmManteNotaCreditoNoComercial.DelegadoMensaje(reload);
                frm.lstCabeceras = lstNotaCredito;
                frm.TDComercial = 686;
                frm.SetInsert();
                frm.Show();
                frm.lkpMoneda.EditValue = 3;
                //frm.txtSerie.Text = "001";
            }
            else
            {
                XtraMessageBox.Show("No existe el Tipo de Cambio a la Fecha, Ingrese el Tipo Cambio para seguir con este proceso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar()
        {
            ENotaCredito oBe = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                //List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == oBe.doc_icod_documento && x.tipoDocumento == "07").ToList();
                var estadoFac = new BVentas().obtnerEstadoFacturacion(oBe.doc_icod_documento, "07");
                //if (lstCab.Count > 0)
                //{
                //if (lstCab[0].EstadoFacturacion != 4)
                if (estadoFac.EstadoFacturacion != 4)
                {
                    XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //}
                if (oBe.ncrec_iid_situacion_credito != Parametros.intSitDocCobrarGenerado)
                    throw new ArgumentException(String.Format("La N/C no puede ser modificada, su situación es {0}", oBe.strSituacion));

                FrmManteNotaCreditoNoComercial frm = new FrmManteNotaCreditoNoComercial();
                frm.MiEvento += new FrmManteNotaCreditoNoComercial.DelegadoMensaje(reload);
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
            ENotaCredito oBe = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (oBe == null)
                return;
            int index = viewNotaCredito.FocusedRowHandle;
            try
            {
                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == oBe.doc_icod_documento && x.tipoDocumento == "07").ToList();
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
                    new BVentas().eliminarNotaCreditoClienteCab(oBe);
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
            ENotaCredito oBe = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
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
            ENotaCredito ObeFC = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            List<EFacturaVentaElectronica> lstFE = new List<EFacturaVentaElectronica>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            lstFE = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == ObeFC.ncrec_icod_credito && x.tipoDocumento == "07").ToList();
            Obe = lstFE.FirstOrDefault();

            mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);
            mlisdet.ForEach(x =>
            {
                x.ValorUnitarioItem = x.MontoAfectoImpuestoIgv / x.cantidad;
            });

            rptNotaCreditoNoComercialElectronico rptFcatura = new rptNotaCreditoNoComercialElectronico();

            if (Obe.tipoDocumento == "07")
            {
                List<ENotaCredito> lstFV = new BVentas().listarNotaCreditoClienteCab(Parametros.intEjercicio).Where(x => x.ncrec_icod_credito == Obe.doc_icod_documento).ToList();
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
            ENotaCredito obe = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                //if (obe.tablc_iid_situacion != 8)//GENERADO
                //    throw new ArgumentException(String.Format("La factura no puede ser anulada, su situación es {0}", obe.strSituacion));
                //if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                //    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser anulada");
                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == obe.doc_icod_documento && x.tipoDocumento == "07").ToList();
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

                            FrmComunicacionBajaNC frm = new FrmComunicacionBajaNC();
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
                                new BVentas().anularNotaCreditoClienteCab(obe);
                                reload(obe.ncrec_icod_credito);
                            }

                        }
                    }
                }
                else
                {
                    if (XtraMessageBox.Show("¿Esta seguro que desea ANULAR la factura?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        new BVentas().anularNotaCreditoClienteCab(obe);
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
            ENotaCredito Obe = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (Obe == null)
                return;
            List<EPuntoVenta> lstPuntoVenta = new BCentral().ListarPuntoVenta().Where(x => x.puvec_icod_punto_venta == 2).ToList();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica().Where(x => x.doc_icod_documento == Obe.doc_icod_documento && x.tipoDocumento == "07").ToList();

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