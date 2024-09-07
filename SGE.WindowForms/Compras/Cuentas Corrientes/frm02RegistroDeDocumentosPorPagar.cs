using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Cuentas_por_Pagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Cuentas_Corrientes
{
    public partial class frm02RegistroDeDocumentosPorPagar : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm02RegistroDeDocumentosPorPagar));
        private List<EDocPorPagar> Lista = new List<EDocPorPagar>();
        private int xposition = 0;
        #endregion

        #region "Eventos"

        public frm02RegistroDeDocumentosPorPagar()
        {
            InitializeComponent();
        }

        private void RegistroDeDocumentosPorPagar_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
            cargaLkpTipoDoc();
            Carga();
            viewDXP.Focus();
        }

        private void Carga()
        {
            EDocPorPagar objE_DocPorPagar = new EDocPorPagar();
            objE_DocPorPagar.anio = Parametros.intEjercicio;
            objE_DocPorPagar.mesec_iid_mes = Convert.ToInt32(lkpMes.EditValue);

            //Lista = new BCuentasPorPagar().ListarEDocPorPagar(objE_DocPorPagar).OrderBy(ord => ord.doxpc_viid_correlativo).Where(obe => obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).ToList(); //ordenarlo por su correlativo y no mostrar los adelantos;
            Lista = new BCuentasPorPagar().ListarEDocPorPagar(objE_DocPorPagar).OrderBy(ord => ord.doxpc_viid_correlativo).ToList(); //ordenarlo por su correlativo y no mostrar los adelantos;
            grdDXP.DataSource = Lista;
        }

        private void cargaLkpTipoDoc()
        {
            List<string> listaTD = new List<string>();
            listaTD = Lista.OrderBy(ord => ord.tdocc_vabreviatura_tipo_doc).Select(sel => sel.tdocc_vabreviatura_tipo_doc).Distinct().ToList();
            listaTD.Add("TODOS");
            lkpTipoDoc.Properties.DataSource = listaTD;
            lkpTipoDoc.ItemIndex = listaTD.Count - 1;
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpMes.EditValue != null)
                Carga();
            cargaLkpTipoDoc();
            txtNumDoc.Text = "";
            txtCliente.Text = "";
        }
        private void nuevo()
        {
            using (FrmMantedocumentosXPagar frm = new FrmMantedocumentosXPagar())
            {
                frm.MiEvento += new FrmMantedocumentosXPagar.DelegadoMensaje(Modify);
                frm.mes = Convert.ToInt32(lkpMes.EditValue);
                frm.SetInsert();
                if (frm.ShowDialog() == DialogResult.OK)
                    cargaLkpTipoDoc();
            }
        }

        private void modificar()
        {
            if (Lista.Count > 0)
            {

                EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                if (Obe.tablc_iid_situacion_documento == 9)
                {

                    if (XtraMessageBox.Show("El documento esta parcialmente pagado, ¿desea modificar de todas maneras?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                    }
                    else
                    {
                        return;
                    }

                }
                if (Obe.tablc_iid_situacion_documento == 10)
                {
                    if (XtraMessageBox.Show("El documento esta Cancelado, ¿desea modificar de todas maneras?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                    }
                    else
                    {
                        return;
                    }


                }

                ////if (Obe.tablc_iid_situacion_documento == 8)
                ////{
                if (Obe.tdocc_icod_tipo_doc == Parametros.intTipoDocReciboPorHonorarios)
                {
                    using (FrmManteReciboHonorario frm = new FrmManteReciboHonorario())
                    {
                        frm.MiEvento += new FrmManteReciboHonorario.DelegadoMensaje(Modify);

                        frm.oDetail = Lista;
                        frm.Cab_icod_correlativo = Convert.ToInt64(Obe.doxpc_icod_correlativo);
                        frm.CargaControles();
                        //CARGAR CONTROLES
                        frm.vcocc_iid_voucher_contable = Obe.vcocc_iid_voucher_contable;
                        frm.Correlative = Obe.doxpc_iid_correlativo;
                        frm.txtCorrelativo.Text = Obe.doxpc_viid_correlativo;
                        frm.mes = Convert.ToInt32(Obe.mesec_iid_mes);
                        frm.btnProveedor.Tag = Obe.proc_icod_proveedor; //correlativo de proveedor
                        frm.btnProveedor.Text = Obe.proc_vcod_proveedor; //código de proveedor
                        frm.lblProveedor.Text = Obe.proc_vnombrecompleto; //nombre completo proveedor
                        frm.btnClaseDocumento.Tag = Obe.tdodc_iid_correlativo; //correlativo de la clase del documento
                        frm.btnClaseDocumento.Text = Obe.tdodc_descripcion; //descripcion de la clase del documento
                        frm.dtmFechaDocumento.EditValue = Obe.doxpc_sfecha_doc; //fecha documento
                        frm.dtmFechaVencimiento.EditValue = Obe.doxpc_sfecha_vencimiento_doc; //fecha vencimiento documento
                        frm.lkpMoneda.ItemIndex = Obe.tablc_iid_tipo_moneda - 1;
                        frm.id_moneda = Obe.tablc_iid_tipo_moneda;
                        frm.txtSerie.Text = Obe.doxpc_vnumero_doc.Substring(0, 4);
                        frm.txtNumeroDocumento.Text = Obe.doxpc_vnumero_doc.Substring(4);

                        frm.txtPorcCuarta.Text = Obe.doxpc_nporcentaje_imp_renta.ToString();
                        frm.txtTipoDeCambio.Text = Obe.doxpc_nmonto_tipo_cambio.ToString();
                        frm.txtConcepto.Text = Obe.doxpc_vdescrip_transaccion;
                        frm.txtDesGrav.EditValue = Obe.doxpc_nmonto_destino_gravado;
                        frm.txtDesNoGrav.EditValue = Obe.doxpc_nmonto_destino_nogravado;
                        frm.lblSaldo.Text = Obe.doxpc_nmonto_total_saldo.ToString();
                        frm.lblNetoPagar.Text = Obe.doxpc_nmonto_total_documento.ToString();
                        frm.txtRetencion.Text = Obe.doxpc_nmonto_retencion_rh.ToString();

                        if (Obe.tablc_iid_situacion_documento == 2 || Obe.tablc_iid_situacion_documento == 3)
                        {

                            frm.SetCancel();
                        }
                        else
                        {
                            frm.SetModify();
                        }


                        frm.ShowDialog();
                    }
                }
                else
                {
                    if (Obe.doxpc_origen == "2")
                    {
                        using (FrmMantedocumentosXPagar frm = new FrmMantedocumentosXPagar())
                        {
                            frm.MiEvento += new FrmMantedocumentosXPagar.DelegadoMensaje(Modify);

                            frm.bteTipoDoc.Enabled = false;
                            frm.bteClaseDoc.Enabled = false;
                            //frm.dtmFechaDocumento.Enabled = false;
                            frm.txtTipoDeCambio.Enabled = false;
                            frm.btnProveedor.Enabled = false;
                            frm.txtSerie.Properties.ReadOnly = Enabled;
                            frm.txtNumeroDocumento.Properties.ReadOnly = Enabled;
                            frm.txtNroDoc2.Properties.ReadOnly = Enabled;
                            frm.txtCorrelativo.Properties.ReadOnly = Enabled;

                            frm.oDetail = Lista;
                            frm.obeDocXPagar = Obe;
                            frm.SetModify();
                            frm.SetValues();
                            frm.ShowDialog();

                        }
                    }
                    else
                    {
                        //XtraMessageBox.Show("El Registro no puede ser Modificado, el Documento proviene de otro Origen", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        using (FrmMantedocumentosXPagar frm = new FrmMantedocumentosXPagar())
                        {
                            frm.MiEvento += new FrmMantedocumentosXPagar.DelegadoMensaje(Modify);
                            frm.oDetail = Lista;
                            frm.obeDocXPagar = Obe;
                            frm.StatusControl2();
                            //frm.SetModify();
                            frm.SetValues();
                            frm.ShowDialog();
                        }
                    }
                }

            }
            //else
            //{
            //    XtraMessageBox.Show("El Registro no puede ser Modificado, el documento se encuentra "+Obe.vSituacion , "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void eliminar()
        {
            try
            {
                if (Lista.Count > 0)
                {
                    EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);

                    if (Obe.tablc_iid_situacion_documento == 9)
                    {
                        XtraMessageBox.Show("El documento no puede ser Eliminado por que está Parcialmente pagado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (Obe.tablc_iid_situacion_documento == 10)
                    {
                        XtraMessageBox.Show("El documento no puede ser Eliminado por que está Cancelado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (Obe.doxpc_origen == "2")
                    {
                        if (!(new BCuentasPorPagar().VerificarExistenPagos(Obe.doxpc_icod_correlativo, Convert.ToInt32(Obe.tdocc_icod_tipo_doc))))
                        {
                            if (XtraMessageBox.Show("¿Está seguro de eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                /**/
                                BCuentasPorPagar Obl = new BCuentasPorPagar();
                                Obe.intUsuario = Valores.intUsuario;
                                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                                /**/
                                List<EDocPorPagarDetalleCuentaContable> ListaEliminadosCtaCont = new BCuentasPorPagar().listarDXPDetCtaContable(Obe.doxpc_icod_correlativo);
                                ListaEliminadosCtaCont.ForEach(x =>
                                {
                                    x.intUsuario = Valores.intUsuario;
                                    x.strPc = WindowsIdentity.GetCurrent().Name;
                                    if (x.pdxpc_icod_correlativo != null && x.pdxpc_icod_correlativo != 0)
                                        ActualizarPago(x, Obe.tablc_iid_tipo_moneda);
                                });
                                /**/
                                List<EDXPImportacion> DeletelstDXPImportacion = new BCompras().ListarDXPImportacion(Obe.doxpc_icod_correlativo);
                                DeletelstDXPImportacion.ForEach(x =>
                                {
                                    x.intUsuario = Valores.intUsuario;
                                    x.strPc = WindowsIdentity.GetCurrent().Name;
                                });
                                Obl.EliminarEDocPorPagar(Obe, ListaEliminadosCtaCont, DeletelstDXPImportacion);
                                viewDXP.DeleteRow(viewDXP.FocusedRowHandle);
                                cargaLkpTipoDoc();
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("No se puede eliminar porque existen pagos asociados", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                        XtraMessageBox.Show("El documento procede de un área distinta, no puede eliminarlo", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    XtraMessageBox.Show("No hay registros por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void ActualizarPago(EDocPorPagarDetalleCuentaContable obj, int tipo_moneda)
        {
            if (obj.tablc_iid_tipo_moneda == tipo_moneda)
            {
                obj.doxpc_nmonto_total_saldo = obj.doxpc_nmonto_total_saldo + obj.cdxpc_nmonto_cuenta;
                obj.doxpc_nmonto_total_pagado = obj.doxpc_nmonto_total_pagado - obj.cdxpc_nmonto_cuenta;
            }
            else
            {
                if (obj.tablc_iid_tipo_moneda == Parametros.intSoles)
                {
                    obj.doxpc_nmonto_total_saldo = obj.doxpc_nmonto_total_saldo + (obj.cdxpc_nmonto_cuenta * tipo_moneda);
                    obj.doxpc_nmonto_total_pagado = obj.doxpc_nmonto_total_pagado - (obj.cdxpc_nmonto_cuenta * tipo_moneda);
                }
                else
                {
                    obj.doxpc_nmonto_total_saldo = obj.doxpc_nmonto_total_saldo + (obj.cdxpc_nmonto_cuenta / tipo_moneda);
                    obj.doxpc_nmonto_total_pagado = obj.doxpc_nmonto_total_pagado - (obj.cdxpc_nmonto_cuenta / tipo_moneda);
                }
            }

            if (obj.doxpc_nmonto_total_saldo > obj.doxpc_nmonto_total_documento)
            {
                obj.doxpc_nmonto_total_saldo = Convert.ToDecimal(obj.doxpc_nmonto_total_documento);
                obj.doxpc_nmonto_total_pagado = 0;
            }
        }

        private void pagosDirectosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista.Count > 0)
                {
                    EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                    if (Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocNotaCreditoProveedor && Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor)
                    {
                        FrmPagoDirectoDocumentosXPagar frm = new FrmPagoDirectoDocumentosXPagar();
                        frm.MiEvento += new FrmPagoDirectoDocumentosXPagar.DelegadoMensaje(Modify);
                        frm.mes = Convert.ToInt32(lkpMes.EditValue);
                        frm.eDocXPagar = Obe;
                        frm.Cab_icod_correlativo = Obe.doxpc_icod_correlativo;
                        frm.ShowDialog();
                    }
                    else
                        XtraMessageBox.Show("No se debe registrar el Pago de un Adelanto o Nota de Crédito", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pagosAdelantoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista.Count > 0)
                {
                    EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                    if (Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor && Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocNotaCreditoProveedor)
                    {
                        using (FrmConsultaPagosAdelantosCanje frm = new FrmConsultaPagosAdelantosCanje())
                        {
                            frm.MiEvento += new FrmConsultaPagosAdelantosCanje.DelegadoMensaje(Modify);
                            frm.mes = Convert.ToInt32(lkpMes.EditValue);
                            frm.eDocXPagar = Obe;
                            frm.Cab_icod_correlativo = Obe.doxpc_icod_correlativo;
                            frm.ShowDialog();
                        }
                    }
                    else
                        XtraMessageBox.Show("No se debe registrar el Pago de un Adelanto o Nota de Crédito", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pagoNCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista.Count > 0)
                {
                    EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                    if (Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor && Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocNotaCreditoProveedor)
                    {
                        FrmConsultaPagosNotaCredito frm = new FrmConsultaPagosNotaCredito();
                        frm.MiEvento += new FrmConsultaPagosNotaCredito.DelegadoMensaje(Modify);
                        frm.mes = Convert.ToInt32(lkpMes.EditValue);
                        frm.eDocXPagar = Obe;
                        frm.Cab_icod_correlativo = Obe.doxpc_icod_correlativo;
                        frm.Show();
                    }
                    else
                        XtraMessageBox.Show("No se debe registrar el Pago de un Adelanto o Nota de Crédito", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void canjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista.Count > 0)
                {
                    EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                    if (Obe.tdodc_iid_correlativo != 1 && Obe.tdodc_iid_correlativo != 36)
                    {
                        FrmCanjeDocumentoXCobrarDocumentosXPagar frm = new FrmCanjeDocumentoXCobrarDocumentosXPagar();
                        frm.MiEvento += new FrmCanjeDocumentoXCobrarDocumentosXPagar.DelegadoMensaje(Modify);
                        frm.mes = Convert.ToInt32(lkpMes.EditValue);
                        frm.eDocXPagar = Obe;
                        frm.Cab_icod_correlativo = Obe.doxpc_icod_correlativo;
                        frm.ShowDialog();
                    }
                    else
                        XtraMessageBox.Show("No se debe registrar el Pago de un Adelanto o Nota de Crédito", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void consultaDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista.Count > 0)
                {
                    EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);

                    if (Obe.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoProveedor) //adelanto
                    {
                        FrmConsultaPagosAdelantos a = new FrmConsultaPagosAdelantos();
                        a.IcodDXP = Convert.ToInt32(Obe.doxpc_icod_correlativo);
                        a.Moneda = Convert.ToInt32(Obe.tablc_iid_tipo_moneda);
                        a.eDocXPagar = Obe;
                        a.Show();
                        //a.ShowDialog();
                    }
                    else if (Obe.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoProveedor || Obe.tdocc_icod_tipo_doc == 119) //nota de crédito
                    {
                        FrmConsultaPagosConNCredito nc = new FrmConsultaPagosConNCredito();
                        nc.codENotaCreditoCliente = Obe.doxpc_icod_correlativo;
                        nc.Show();
                    }
                    else
                    {
                        FrmConsultaPagosDocumentosXPagar p = new FrmConsultaPagosDocumentosXPagar();
                        p.eDocXPagar = Obe;
                        p.ShowDialog();
                    }
                }
                else
                    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void datosAdicionalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocPorPagar obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                if (obe.tdodc_iestado_registro == 0)
                {

                    using (FrmManteDocxPagarAdicional frm = new FrmManteDocxPagarAdicional())
                    {
                        EDxPDatosAdicionales obeDatAdic = new BCuentasPorPagar().ListaDxPDatosAdicionalesXIcod(obe.doxpc_icod_correlativo)[0];
                        frm.obeDatAdic = obeDatAdic;
                        if (obeDatAdic.condicion == 1)
                        {
                            frm.SetModify();
                        }
                        else
                        {
                            frm.SetInsert();
                            frm.bteTipoComprobante.Tag = obeDatAdic.doxpc_tipo_comprobante;
                            frm.bteTipoComprobante.Text = obeDatAdic.tipo_comprobante_descripcion;
                            if (obeDatAdic.tipo_comprobante_codigo == "10" || obeDatAdic.tipo_comprobante_codigo == "14")
                            {
                                frm.txtNumCorrelDoc.Text = obeDatAdic.doxpc_num_doc_domiciliado;
                            }
                            else if (obeDatAdic.tipo_comprobante_codigo == "12")
                                frm.txtNumTicket.Text = obeDatAdic.doxpc_num_doc_domiciliado;
                            else
                            {
                                frm.txtNumSerieDoc.Text = obeDatAdic.doxpc_num_serie;
                                frm.txtNumCorrelDoc.Text = obeDatAdic.doxpc_num_doc_domiciliado;
                            }
                            if (obeDatAdic.proc_vnombre != null && obeDatAdic.proc_vnombre != string.Empty)
                            {
                                frm.txtNombre.Text = obeDatAdic.proc_vnombre;
                                frm.txtApPaterno.Text = obeDatAdic.proc_vpaterno;
                                frm.txtApMaterno.Text = obeDatAdic.proc_vmaterno;
                            }
                            else
                            {
                                frm.txtNombre.Text = obeDatAdic.proc_vnombrecompleto;
                                frm.txtApPaterno.Properties.ReadOnly = true;
                                frm.txtApMaterno.Properties.ReadOnly = true;
                            }
                            frm.bteTipoComprobanteRef.Tag = obeDatAdic.doxpc_tipo_comprobante_referencia;
                            frm.bteTipoComprobanteRef.Text = obeDatAdic.tipo_comprobante_descripcion_ref;
                        }
                        frm.txtDesGrav.Text = obeDatAdic.doxpc_nmonto_destino_gravado.ToString();
                        frm.txtDesNoGrav.Text = obeDatAdic.doxpc_nmonto_destino_nogravado.ToString();
                        frm.txtDestMixto.Text = obeDatAdic.doxpc_nmonto_destino_mixto.ToString();
                        frm.txtAdqNoGrav.Text = obeDatAdic.doxpc_nmonto_nogravado.ToString();

                        frm.txtSerieComprob.Text = obeDatAdic.doxpc_num_serie_referencia;
                        frm.txtNumComprob.Text = obeDatAdic.doxpc_num_comprobante_referencia;
                        frm.dtmFechaDocumento.DateTime = Convert.ToDateTime(obeDatAdic.doxpc_sfecha_emision_referencia);

                        if (obeDatAdic.doxpc_iid_tipo_doc_referencia != null)
                        {
                            frm.bteTipoComprobanteRef.Tag = obeDatAdic.doxpc_tipo_comprobante_referencia;
                            frm.bteTipoComprobanteRef.Text = obeDatAdic.tipo_comprobante_descripcion_ref;
                            frm.txtSerieComprob.Text = obeDatAdic.doxpc_num_serie_referencia;
                            frm.txtNumComprob.Text = obeDatAdic.doxpc_num_comprobante_referencia;
                            frm.dtmFechaDocumento.DateTime = Convert.ToDateTime(obeDatAdic.doxpc_sfecha_emision_referencia);
                            frm.txtBaseImp.Text = obeDatAdic.doxpc_nmonto_referencial_cif.ToString();
                            frm.txtIGV.Text = (obeDatAdic.doxpc_nmonto_referencial_cif * (obeDatAdic.doxpc_nporcentaje_igv / 100)).ToString();

                            frm.bteTipoComprobante.Enabled = true;
                            frm.bteTipoComprobante.Properties.ReadOnly = true;
                            frm.txtSerieComprob.Enabled = true;
                            frm.txtNumComprob.Enabled = true;
                            frm.dtmFechaDocumento.Enabled = true;
                            frm.txtBaseImp.Enabled = true;
                            frm.txtIGV.Enabled = true;
                        }

                        frm.ShowDialog();
                    }
                }
                else
                    XtraMessageBox.Show("El tipo de documento no está considerado como de registro de compra", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                XtraMessageBox.Show("No hay registro para adicionar datos", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion

        #region "Metodos"

        void Modify(long Cab_icod_correlativo)
        {
            Carga();
            int index = Lista.FindIndex(obe => obe.doxpc_icod_correlativo == Cab_icod_correlativo);
            viewDXP.FocusedRowHandle = index;
        }

        private void Datos(BSMaintenanceStatus status)
        {
            EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
            if (Obe.tdocc_icod_tipo_doc == Parametros.intTipoDocReciboPorHonorarios)
            {
                using (FrmManteReciboHonorario frm = new FrmManteReciboHonorario())
                {
                    frm.MiEvento += new FrmManteReciboHonorario.DelegadoMensaje(Modify);

                    frm.oDetail = Lista;
                    frm.Cab_icod_correlativo = Convert.ToInt64(Obe.doxpc_icod_correlativo);
                    frm.CargaControles();
                    //CARGAR CONTROLES
                    frm.vcocc_iid_voucher_contable = Obe.vcocc_iid_voucher_contable;
                    frm.Correlative = Obe.doxpc_iid_correlativo;
                    frm.txtCorrelativo.Text = Obe.doxpc_viid_correlativo;
                    frm.mes = Convert.ToInt32(Obe.mesec_iid_mes);
                    frm.btnProveedor.Tag = Obe.proc_icod_proveedor; //correlativo de proveedor
                    frm.btnProveedor.Text = Obe.proc_vcod_proveedor; //código de proveedor
                    frm.lblProveedor.Text = Obe.proc_vnombrecompleto; //nombre completo proveedor
                    frm.btnClaseDocumento.Tag = Obe.tdodc_iid_correlativo; //correlativo de la clase del documento
                    frm.btnClaseDocumento.Text = Obe.tdodc_descripcion; //descripcion de la clase del documento
                    frm.dtmFechaDocumento.EditValue = Obe.doxpc_sfecha_doc; //fecha documento
                    frm.dtmFechaVencimiento.EditValue = Obe.doxpc_sfecha_vencimiento_doc; //fecha vencimiento documento
                    frm.lkpMoneda.ItemIndex = Obe.tablc_iid_tipo_moneda - 1;
                    frm.id_moneda = Obe.tablc_iid_tipo_moneda;
                    frm.txtSerie.Text = Obe.doxpc_vnumero_doc.Substring(0, 4);
                    frm.txtNumeroDocumento.Text = Obe.doxpc_vnumero_doc.Substring(4);

                    frm.txtPorcCuarta.Text = Obe.doxpc_nporcentaje_imp_renta.ToString();
                    frm.txtTipoDeCambio.Text = Obe.doxpc_nmonto_tipo_cambio.ToString();
                    frm.txtConcepto.Text = Obe.doxpc_vdescrip_transaccion;
                    frm.txtDesGrav.EditValue = Obe.doxpc_nmonto_destino_gravado;
                    frm.txtDesNoGrav.EditValue = Obe.doxpc_nmonto_destino_nogravado;
                    frm.lblSaldo.Text = Obe.doxpc_nmonto_total_saldo.ToString();
                    frm.lblNetoPagar.Text = Obe.doxpc_nmonto_total_documento.ToString();
                    frm.txtRetencion.Text = Obe.doxpc_nmonto_retencion_rh.ToString();

                    if (Obe.tablc_iid_situacion_documento == 2 || Obe.tablc_iid_situacion_documento == 3)
                    {

                        frm.SetCancel();
                    }
                    else
                    {
                        frm.SetModify();
                    }


                    frm.ShowDialog();
                }
            }
            else
            {
                using (FrmMantedocumentosXPagar frm = new FrmMantedocumentosXPagar())
                {
                    frm.MiEvento += new FrmMantedocumentosXPagar.DelegadoMensaje(Modify);
                    frm.oDetail = Lista;
                    frm.obeDocXPagar = Obe;
                    if (Obe.tdocc_icod_tipo_doc != 106)
                    {
                        if (Obe.doxpc_vnumero_doc.Length == 12)
                        {
                            frm.txtSerie.Text = Obe.doxpc_vnumero_doc.Substring(0, 4);
                            frm.txtNumeroDocumento.Text = Obe.doxpc_vnumero_doc.Substring(4, 8);
                        }
                        else
                            frm.txtNroDoc2.Text = Obe.doxpc_vnumero_doc;
                    }

                    frm.Cab_icod_correlativo = Obe.doxpc_icod_correlativo;
                    //CARGAR CONTROLES
                    frm.vcocc_iid_voucher_contable = Obe.vcocc_iid_voucher_contable;
                    frm.Correlativo = Obe.doxpc_iid_correlativo;
                    frm.txtCorrelativo.Text = Obe.doxpc_viid_correlativo;
                    frm.CargaControles();
                    //frm.anio = Convert.ToInt32(Obe.anioc_iid_anio);
                    frm.mes = Convert.ToInt32(Obe.mesec_iid_mes);
                    frm.btnProveedor.Tag = Obe.proc_icod_proveedor; //correlativo de proveedor
                    frm.btnProveedor.Text = Obe.proc_vcod_proveedor; //código de proveedor
                    frm.lblProveedor.Text = Obe.proc_vnombrecompleto; //nombre completo proveedor
                    frm.bteTipoDoc.Tag = Obe.tdocc_icod_tipo_doc; //código del tipo de documento
                    frm.bteTipoDoc.Text = Obe.tdocc_vabreviatura_tipo_doc; //abreviatura del tipo de documento
                    frm.bteClaseDoc.Tag = Obe.tdodc_iid_correlativo; //correlativo de la clase del documento
                    frm.bteClaseDoc.Text = Obe.clase_viid_correlativo; //descripcion de la clase del documento
                    frm.lblClaseDocumento.Text = Obe.tdodc_descripcion;
                    frm.dtmFechaDocumento.EditValue = Obe.doxpc_sfecha_doc; //fecha documento
                    frm.dtmFechaVencimiento.EditValue = Obe.doxpc_sfecha_vencimiento_doc; //fecha vencimiento documento

                    //var Array = Obe.doxpc_vnumero_doc.Split('-');
                    //if (Array.Length == 1)
                    //{
                    //    frm.txtSerie.Text = Obe.doxpc_vnumero_doc.Substring(0, 3);
                    //    frm.txtNumeroDocumento.Text = Obe.doxpc_vnumero_doc.Substring(3);
                    //}




                    frm.txtIgv.EditValue = Obe.doxpc_nporcentaje_igv;
                    frm.txtTipoDeCambio.Text = Obe.doxpc_nmonto_tipo_cambio.ToString();
                    frm.txtConcepto.Text = Obe.doxpc_vdescrip_transaccion;
                    frm.txtDesGrav.EditValue = Obe.doxpc_nmonto_destino_gravado;
                    frm.txtDestMixto.EditValue = Obe.doxpc_nmonto_destino_mixto;
                    frm.txtDesNoGrav.EditValue = Obe.doxpc_nmonto_destino_nogravado;
                    frm.txtNoGravada.EditValue = Obe.doxpc_nmonto_nogravado;
                    frm.txtReferencia.EditValue = Obe.doxpc_nmonto_referencial_cif;
                    frm.txtServicios.EditValue = Obe.doxpc_nmonto_servicio_no_domic;
                    frm.txtIgvAdqDesGrav.EditValue = Obe.doxpc_nmonto_imp_destino_gravado;
                    frm.txtIgvDestMixto.EditValue = Obe.doxpc_nmonto_imp_destino_mixto;
                    frm.txtIgvDesNoGrav.EditValue = Obe.doxpc_nmonto_imp_destino_nogravado;
                    frm.txtSelcCons.EditValue = Obe.doxpc_nmonto_isc;
                    frm.txtSubtotal.Text = Obe.Valorcompra.ToString();
                    frm.txtPrecioCompra.Text = Obe.doxpc_nmonto_total_documento.ToString();
                    frm.txtSaldo.Text = Obe.doxpc_nmonto_total_saldo.ToString();
                    frm.txtDetraccion.Text = Obe.doxpc_vnro_deposito_detraccion;
                    frm.dtmFechaDetraccion.EditValue = Obe.doxpc_sfec_deposito_detraccion;

                    frm.lkpTipoDocRef.EditValue = Obe.doxpc_tipo_comprobante_referencia; //tipo documento referencia sólo si es N/C el documento creado
                    frm.txtseriereferencia.Text = Obe.doxpc_num_serie_referencia == null ? "000" : Obe.doxpc_num_serie_referencia;//clase del documento referencia
                    frm.txtCorrelativoreferencia.Text = Obe.doxpc_num_comprobante_referencia == null ? "0000000" : Obe.doxpc_num_comprobante_referencia;
                    frm.dtFechaReferencia.EditValue = Obe.doxpc_sfecha_emision_referencia;
                    frm.lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
                    if (Obe.tdocc_icod_tipo_doc == 49 && Obe.tdodc_iid_correlativo == 47)
                    {
                        frm.txtDesGrav.Text = Obe.doxpc_nmonto_nogravado.ToString();
                        frm.txtNoGravada.Text = Obe.doxpc_nmonto_destino_gravado.ToString();
                    }
                    /*DUA*/
                    if (Obe.tdocc_icod_tipo_doc == 106)
                    {
                        frm.txtCodAduana.Text = Obe.doxpc_codigo_aduana;
                        frm.txtAnio.Text = Obe.doxpc_anio;
                        frm.txtNumDeclaracion.Text = Obe.doxpc_numero_declaracion;
                    }
                    if (Obe.tdocc_icod_tipo_doc == 32)
                    {
                        frm.txtCodAduana.Text = Obe.doxpc_codigo_aduana;
                    }

                    if (status == BSMaintenanceStatus.View)
                    {
                        frm.obeDocXPagar = null;
                        frm.Cab_icod_correlativo = Obe.doxpc_icod_correlativo;
                        frm.SetCancel();
                    }
                    else if (status == BSMaintenanceStatus.ModifyCurrent && Obe.doxpc_origen == "2")
                        frm.SetModify();

                    else
                        frm.SetCancel();

                    if (Obe.tablc_iid_situacion_documento == 1)
                    {
                        frm.txtTipoDeCambio.Properties.ReadOnly = false;
                    }
                    else
                    {
                        int soles = 0;
                        int dolares = 0;
                        List<EDocPorPagarPago> lIS = new BCuentasPorPagar().listarDxpPagos(Convert.ToInt32(Obe.doxpc_icod_correlativo), Parametros.intEjercicio);
                        foreach (var item in lIS)
                        {
                            if (item.tablc_iid_tipo_moneda == 3)
                            {
                                soles = soles + 1;
                            }
                            else
                            {
                                dolares = dolares + 1;
                            }
                        }
                        if ((Obe.tablc_iid_tipo_moneda) == 3)
                        {
                            if (dolares != 0)
                            {
                                frm.txtTipoDeCambio.Properties.ReadOnly = true;
                            }
                            else
                            {
                                frm.txtTipoDeCambio.Properties.ReadOnly = false;
                            }
                        }
                        else
                        {
                            if (soles != 0)
                            {
                                frm.txtTipoDeCambio.Properties.ReadOnly = true;
                            }
                            else
                            {
                                frm.txtTipoDeCambio.Properties.ReadOnly = false;
                            }
                        }
                    }

                    frm.ShowDialog();


                }
            }
        }
        public class TipoDoc
        {
            public int intCodigo { get; set; }
            public string strTipoDoc { get; set; }
        }
        private void Datos_Otro_Origen(BSMaintenanceStatus status)
        {
            EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
            if (Obe.tdocc_icod_tipo_doc == Parametros.intTipoDocReciboPorHonorarios)
            {
                using (FrmManteReciboHonorario frm = new FrmManteReciboHonorario())
                {
                    frm.MiEvento += new FrmManteReciboHonorario.DelegadoMensaje(Modify);

                    frm.oDetail = Lista;
                    frm.Cab_icod_correlativo = Convert.ToInt64(Obe.doxpc_icod_correlativo);

                    //CARGAR CONTROLES
                    frm.vcocc_iid_voucher_contable = Obe.vcocc_iid_voucher_contable;
                    frm.Correlative = Obe.doxpc_iid_correlativo;
                    frm.txtCorrelativo.Text = Obe.doxpc_viid_correlativo;
                    //frm.anio = Convert.ToInt32(Obe.anioc_iid_anio);
                    frm.mes = Convert.ToInt32(Obe.mesec_iid_mes);
                    frm.btnProveedor.Tag = Obe.proc_icod_proveedor; //correlativo de proveedor
                    frm.btnProveedor.Text = Obe.proc_iid_proveedor; //código de proveedor
                    frm.lblProveedor.Text = Obe.proc_vnombrecompleto; //nombre completo proveedor
                    frm.btnClaseDocumento.Tag = Obe.tdodc_iid_correlativo; //correlativo de la clase del documento
                    frm.btnClaseDocumento.Text = Obe.tdodc_descripcion; //descripcion de la clase del documento
                    frm.dtmFechaDocumento.EditValue = Obe.doxpc_sfecha_doc; //fecha documento
                    frm.dtmFechaVencimiento.EditValue = Obe.doxpc_sfecha_vencimiento_doc; //fecha vencimiento documento
                    //frm.lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda; //código tipo de moneda
                    //frm.lkpMoneda.Text = Obe.vMoneda; //descripción a mostrar del tipo de moneda
                    frm.lkpMoneda.ItemIndex = Obe.tablc_iid_tipo_moneda - 1;
                    frm.id_moneda = Obe.tablc_iid_tipo_moneda;
                    frm.txtSerie.Text = Obe.doxpc_vnumero_doc.Substring(0, 3);
                    frm.txtNumeroDocumento.Text = Obe.doxpc_vnumero_doc.Substring(3);

                    frm.txtPorcCuarta.Text = Obe.doxpc_nporcentaje_imp_renta.ToString();
                    frm.txtTipoDeCambio.Text = Obe.doxpc_nmonto_tipo_cambio.ToString();
                    frm.txtConcepto.Text = Obe.doxpc_vdescrip_transaccion;
                    frm.txtDesGrav.EditValue = Obe.doxpc_nmonto_destino_gravado;
                    frm.txtDesNoGrav.EditValue = Obe.doxpc_nmonto_destino_nogravado;
                    frm.lblSaldo.Text = Obe.doxpc_nmonto_total_saldo.ToString();
                    frm.lblNetoPagar.Text = Obe.doxpc_nmonto_total_documento.ToString();
                    frm.txtRetencion.Text = (Obe.doxpc_nmonto_destino_gravado * (Obe.doxpc_nporcentaje_imp_renta / 100)).ToString();

                    if (status == BSMaintenanceStatus.View)
                        frm.SetCancel();
                    else if (status == BSMaintenanceStatus.ModifyCurrent)
                        frm.SetModify();

                    frm.btnProveedor.Enabled = false;
                    frm.txtTipoDoc.Properties.ReadOnly = true;
                    frm.btnClaseDocumento.Enabled = false;
                    frm.txtSerie.Properties.ReadOnly = true;
                    frm.txtNumeroDocumento.Properties.ReadOnly = true;
                    frm.dtmFechaDocumento.Enabled = false;
                    frm.dtmFechaVencimiento.Enabled = false;
                    frm.lkpMoneda.Enabled = false;
                    frm.txtTipoDeCambio.Enabled = false;
                    frm.txtConcepto.Properties.ReadOnly = true;
                    frm.txtCorrelativo.Properties.ReadOnly = true;
                    frm.txtDesGrav.Properties.ReadOnly = true;
                    frm.txtDesNoGrav.Properties.ReadOnly = true;
                    frm.txtRetencion.Properties.ReadOnly = true;
                    frm.txtPorcCuarta.Properties.ReadOnly = true;

                    frm.ShowDialog();
                }
            }
            else
            {
                using (FrmMantedocumentosXPagar frm = new FrmMantedocumentosXPagar())
                {
                    frm.MiEvento += new FrmMantedocumentosXPagar.DelegadoMensaje(Modify);

                    frm.oDetail = Lista;
                    frm.obeDocXPagar = Obe;

                    //CARGAR CONTROLES
                    frm.vcocc_iid_voucher_contable = Obe.vcocc_iid_voucher_contable;
                    frm.Correlativo = Obe.doxpc_iid_correlativo;
                    frm.txtCorrelativo.Text = Obe.doxpc_viid_correlativo;
                    //frm.anio = Convert.ToInt32(Obe.anioc_iid_anio);
                    frm.mes = Convert.ToInt32(Obe.mesec_iid_mes);
                    frm.btnProveedor.Tag = Obe.proc_icod_proveedor; //correlativo de proveedor
                    frm.btnProveedor.Text = Obe.proc_iid_proveedor; //código de proveedor
                    frm.lblProveedor.Text = Obe.proc_vnombrecompleto; //nombre completo proveedor
                    frm.bteTipoDoc.Tag = Obe.tdocc_icod_tipo_doc; //código del tipo de documento
                    frm.bteTipoDoc.Text = Obe.tdocc_vabreviatura_tipo_doc; //abreviatura del tipo de documento
                    frm.bteClaseDoc.Tag = Obe.tdodc_iid_correlativo; //correlativo de la clase del documento
                    frm.bteClaseDoc.Text = Obe.tdodc_descripcion; //descripcion de la clase del documento
                    frm.dtmFechaDocumento.EditValue = Obe.doxpc_sfecha_doc; //fecha documento
                    frm.dtmFechaVencimiento.EditValue = Obe.doxpc_sfecha_vencimiento_doc; //fecha vencimiento documento
                    //frm.lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda; //código tipo de moneda
                    //frm.lkpMoneda.Text = Obe.vMoneda; //descripción a mostrar del tipo de moneda
                    frm.lkpMoneda.ItemIndex = Obe.tablc_iid_tipo_moneda - 1;



                    frm.txtSerie.Text = Obe.doxpc_vnumero_doc.Substring(0, 3);
                    frm.txtNumeroDocumento.Text = Obe.doxpc_vnumero_doc.Substring(3);


                    frm.txtIgv.EditValue = Obe.doxpc_nporcentaje_igv;
                    frm.txtTipoDeCambio.Text = Obe.doxpc_nmonto_tipo_cambio.ToString();
                    frm.txtConcepto.Text = Obe.doxpc_vdescrip_transaccion;
                    frm.txtDesGrav.EditValue = Obe.doxpc_nmonto_destino_gravado;
                    frm.txtDestMixto.EditValue = Obe.doxpc_nmonto_destino_mixto;
                    frm.txtDesNoGrav.EditValue = Obe.doxpc_nmonto_destino_nogravado;
                    frm.txtNoGravada.EditValue = Obe.doxpc_nmonto_nogravado;
                    frm.txtReferencia.EditValue = Obe.doxpc_nmonto_referencial_cif;
                    frm.txtServicios.EditValue = Obe.doxpc_nmonto_servicio_no_domic;
                    frm.txtIgvAdqDesGrav.EditValue = Obe.doxpc_nmonto_imp_destino_gravado;
                    frm.txtIgvDestMixto.EditValue = Obe.doxpc_nmonto_imp_destino_mixto;
                    frm.txtIgvDesNoGrav.EditValue = Obe.doxpc_nmonto_imp_destino_nogravado;
                    frm.txtSelcCons.EditValue = Obe.doxpc_nmonto_isc;
                    frm.txtSubtotal.Text = (Obe.doxpc_nmonto_destino_gravado + Obe.doxpc_nmonto_destino_mixto + Obe.doxpc_nmonto_destino_nogravado + Obe.doxpc_nmonto_nogravado + Obe.doxpc_nmonto_referencial_cif + Obe.doxpc_nmonto_servicio_no_domic).ToString();
                    frm.txtPrecioCompra.Text = Obe.doxpc_nmonto_total_documento.ToString();
                    frm.txtSaldo.Text = Obe.doxpc_nmonto_total_saldo.ToString();
                    frm.txtDetraccion.Text = Obe.doxpc_vnro_deposito_detraccion.ToString();
                    frm.dtmFechaDetraccion.EditValue = Obe.doxpc_sfec_deposito_detraccion;


                    //si el tipo de documento es REC(correlativo 49 iid 49) y clase rec. guía pagos varios(correlativo 47 iid 5)
                    //el monto adq. dest. gravado se cargará en txtNoGravada y adq. no gravada se cargará en txtDesGrav

                    if (Obe.tdocc_icod_tipo_doc == 49 && Obe.tdodc_iid_correlativo == 47)
                    {
                        frm.txtDesGrav.Text = Obe.doxpc_nmonto_nogravado.ToString();
                        frm.txtNoGravada.Text = Obe.doxpc_nmonto_destino_gravado.ToString();
                    }

                    if (status == BSMaintenanceStatus.View)
                        frm.SetCancel();
                    else if (status == BSMaintenanceStatus.ModifyCurrent)
                        frm.SetModify();



                    frm.btnProveedor.Enabled = false;
                    frm.bteTipoDoc.Enabled = false;
                    frm.bteClaseDoc.Enabled = false;

                    frm.txtSerie.Properties.ReadOnly = true;
                    frm.txtNumeroDocumento.Properties.ReadOnly = true;
                    frm.txtTipoDeCambio.Properties.ReadOnly = true;
                    frm.txtCorrelativo.Properties.ReadOnly = true;
                    frm.dtmFechaDocumento.Properties.ReadOnly = true;
                    frm.dtmFechaVencimiento.Properties.ReadOnly = true;
                    frm.lkpMoneda.Properties.ReadOnly = true;
                    frm.txtConcepto.Properties.ReadOnly = true;
                    frm.txtIgv.Properties.ReadOnly = true;
                    frm.txtDesGrav.Properties.ReadOnly = true;
                    frm.txtDestMixto.Properties.ReadOnly = true;
                    frm.txtDesNoGrav.Properties.ReadOnly = true;
                    frm.txtNoGravada.Properties.ReadOnly = true;

                    //su habilitación depende del tipo de documento

                    frm.txtReferencia.Properties.ReadOnly = true;
                    frm.txtServicios.Properties.ReadOnly = true;
                    frm.txtIgvAdqDesGrav.Properties.ReadOnly = true;
                    frm.txtIgvDestMixto.Properties.ReadOnly = true;
                    frm.txtIgvDesNoGrav.Properties.ReadOnly = true;

                    frm.txtDetraccion.Properties.ReadOnly = true;
                    frm.dtmFechaDetraccion.Properties.ReadOnly = true;

                    frm.txtSelcCons.Properties.ReadOnly = true;
                    frm.ShowDialog();
                }
            }
        }

        #endregion

        private void filtroBuscar()
        {
            if (lkpTipoDoc.Text == "TODOS" && txtNumDoc.Text.Trim() == "" && txtCliente.Text.TrimStart().TrimEnd() == "")
            {
                grdDXP.DataSource = Lista;
            }
            else
            {
                List<EDocPorPagar> listaTempDxC = new List<EDocPorPagar>();
                if (lkpTipoDoc.Text != "TODOS")
                    listaTempDxC = Lista.Where(ob => ob.tdocc_vabreviatura_tipo_doc == lkpTipoDoc.Text).ToList();
                else
                    listaTempDxC = Lista;
                listaTempDxC = listaTempDxC.Where(ob => ob.doxpc_vnumero_doc.Contains(txtNumDoc.Text.TrimStart().TrimEnd())).ToList();
                listaTempDxC = listaTempDxC.Where(ob => ob.proc_vnombrecompleto.Contains(txtCliente.Text.TrimStart().TrimEnd())).ToList();
                grdDXP.DataSource = listaTempDxC;
            }
        }

        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoDoc.ContainsFocus)
                filtroBuscar();
        }

        private void txtNumDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNumDoc.ContainsFocus)
                filtroBuscar();
        }

        private void txtCliente_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCliente.ContainsFocus)
                filtroBuscar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void imprimirComprobanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
            if (Obe == null)
                return;
            try
            {
                if (Convert.ToInt32(Obe.vcocc_iid_voucher_contable) > 0)
                    imprimirCaso1(Obe, Convert.ToInt32(Obe.vcocc_iid_voucher_contable));
                else
                    imprimirCaso2(Obe);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void imprimirCaso1(EDocPorPagar Obe, int idVoucher)
        {
            //var lstComprobante = new BComprobantes().ListarComprobantesUnitario(idVoucher);

            //List<EComprobanteDetalle> lista = new BComprobantes().ListarComprobanteDetalle(idVoucher);
            //int cont = 0;
            //lista.Where(x => Convert.ToInt32(x.ctacc_iid_cuenta_contable_ref) == 0).ToList().ForEach(x =>
            //{
            //    cont += 1;
            //    x.nro_item_det = cont;               
            //    if (x.iid_tipo_relacion != null)
            //    {
            //        x.vdes_Analisis = string.Format("{0:00}", x.iid_tipo_relacion) + "." + x.viid_relacion;
            //        x.vglosa_linea = (x.vglosa_linea.Length > 32) ? x.vglosa_linea.Substring(0, 32) : x.vglosa_linea;
            //    }
            //});
            //rptCtasPorPagarComprobante reporte = new rptCtasPorPagarComprobante();
            //reporte.cargar(Obe, lstComprobante[0], lista.Where(x=> 
            //    Convert.ToInt32(x.ctacc_iid_cuenta_contable_ref) == 0).ToList(), lkpMes.Text);
        }
        private void imprimirCaso2(EDocPorPagar Obe)
        {
            //int idVoucher = 0;
            //switch (Obe.tdocc_icod_tipo_doc)
            //{
            //    case 1:
            //        idVoucher = new BComprobantes().getIdVoucher(1, Convert.ToInt32(Obe.doxpc_icod_correlativo));
            //        if (idVoucher > 0)
            //            imprimirCaso1(Obe, idVoucher);
            //        else
            //            throw new ArgumentException("No existe Voucher Contable asociado al registro seleccionado");
            //        break;
            //    case 24:
            //        idVoucher = new BComprobantes().getIdVoucher(24, Convert.ToInt32(Obe.doxpc_icod_correlativo));
            //        if (idVoucher > 0)
            //            imprimirCaso1(Obe, idVoucher);
            //        else
            //            throw new ArgumentException("No existe Voucher Contable asociado al regsitro seleccionado");
            //        break;
            //    case 54:
            //        idVoucher = new BComprobantes().getIdVoucher(24/*no cambiar el 24*/, Convert.ToInt32(Obe.doxpc_icod_correlativo));
            //        if (idVoucher > 0)
            //            imprimirCaso1(Obe, idVoucher);
            //        else
            //            throw new ArgumentException("No existe Voucher Contable asociado al regsitro seleccionado");
            //        break;
            //    case 86:
            //        break;
            //}
        }

        private void generarArchivoDeTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GenerarArchivoTexto();
        }

        private void GenerarArchivoTexto()
        {
            //try
            //{
            //    List<EDxPDatosAdicionales> lista = new BCuentasPorPagar().ListaDxPDatosAdicionalesGenerarTexto(Convert.ToInt32(lkpMes.EditValue), Parametros.intPeriodo);
            //    StreamWriter sw = new StreamWriter("D:\\prueba\\prueba.txt");
            //    foreach (EDxPDatosAdicionales item in lista)
            //    {
            //        sw.Write(item.tipo_comprobante_codigo + "|");
            //        sw.Write(item.tipo_comprobante_codigo + "|");
            //        sw.Write(Convert.ToDateTime(item.doxpc_sfecha_doc).ToString("dd/MM/yyyy") + "|");
            //        sw.Write(item.doxpc_num_serie + "|");
            //        sw.Write(item.doxpc_num_doc_domiciliado + "|");
            //        sw.Write(item.tipo_persona_codigo + "|");
            //        sw.Write(item.tipo_documento_codigo + "|");
            //        sw.Write((item.tipo_persona_codigo == "01") ? string.Empty : item.proc_vnombre + "|");
            //        sw.Write((item.tipo_persona_codigo != "01") ? string.Empty : item.proc_vpaterno + "|");
            //        sw.Write((item.tipo_persona_codigo != "01") ? string.Empty : item.proc_vmaterno + "|");
            //        sw.Write((item.tipo_persona_codigo != "01") ? string.Empty : item.proc_vnombre + "|");
            //        sw.Write("|"); //NOMBRE 2
            //        sw.Write(item.tablc_iid_tipo_moneda + "|");
            //        sw.Write(item.doxpc_tipo_destino + "|");
            //        sw.Write(item.doxpc_numero_destino + "|");
            //        sw.Write(Convert.ToDecimal(item.doxpc_nmonto_base_impon_ivap).ToString("n2") + "|");
            //        sw.Write(Convert.ToDecimal(item.doxpc_nmonto_isc).ToString("n2") + "|");
            //        sw.Write(Convert.ToDecimal(item.monto_igv).ToString("n2") + "|");
            //        sw.Write(Convert.ToDecimal(item.monto_otros).ToString("n2") + "|");
            //        sw.Write(item.doxpc_ind_detraccion + "|");
            //        sw.Write(item.doxpc_cod_tasa_detrac + "|");
            //        sw.Write(item.doxpc_vnro_deposito_detraccion + "|");
            //        sw.Write(item.doxpc_ind_retencion + "|");
            //        if (item.tipo_comprobante_codigo_ref == null)
            //        {
            //            sw.Write("|");
            //            sw.Write("|");
            //            sw.Write("|");
            //        }
            //        else
            //        {
            //            sw.Write(item.tipo_comprobante_codigo_ref + "|");
            //            sw.Write(item.doxpc_num_serie_referencia + "|");
            //            sw.Write(item.doxpc_vnro_doc_referencia + "|");
            //        }
            //        sw.WriteLine();
            //    }
            //    sw.Close();
            //    Process.Start("D:\\prueba\\prueba.txt");
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //Lista.ForEach(x => 
            //{
            //    //var lst = (new BDocPorPagarPago().ListarPagoDocumentoXPagarXIdDocXPagar(x.doxpc_icod_correlativo)).Where(ob => ob.pdxpc_vorigen == "D").ToList();
            //    //if (lst.Count > 0)
            //    //    x.flag = true;   

            //    //var lst = new BAdelantoPago().ListarPagoAdelantoXIdDocXPagar(x.doxpc_icod_correlativo);
            //    //if (lst.Count > 0)
            //    //    x.flag = true;                    

            //    var lst = new BDocPorPagarPago().ListarPagoDocumentoXPagarXIdDocXPagar(x.doxpc_icod_correlativo).Where(ob => ob.pdxpc_vorigen == "X").ToList(); //X identifica que es canje
            //    if (lst.Count > 0)
            //        x.flag = true;       
            //});

            //grd.DataSource = Lista.Where(x => x.flag == true).ToList();

        }

        private void recibohonorariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmManteReciboHonorario frm = new FrmManteReciboHonorario())
            {
                frm.MiEvento += new FrmManteReciboHonorario.DelegadoMensaje(Modify);
                viewDXP.MoveLast();
                frm.mes = Convert.ToInt32(lkpMes.EditValue);
                frm.SetInsert();
                if (frm.ShowDialog() == DialogResult.OK)
                    cargaLkpTipoDoc();
            }
        }

        private void gv_DoubleClick(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                Datos(BSMaintenanceStatus.View);
            }
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void gv_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //GridView View = sender as GridView;
            //if (e.RowHandle >= 0)
            //{
            //    bool cuadra = Convert.ToBoolean(View.GetRowCellValue(e.RowHandle,View.Columns["dxp_dcta_cuadra"]));
            //    if (!cuadra)
            //    {
            //        e.Appearance.BackColor = Color.LightSalmon;
            //        //e.Appearance.BackColor2 = Color.SeaShell;
            //    }
            //}
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewDXP.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewDXP.ClearColumnsFilter();
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

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = viewDXP.FocusedRowHandle;
            Carga();
            viewDXP.FocusedRowHandle = index;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            int index = viewDXP.FocusedRowHandle;
            Carga();
            viewDXP.FocusedRowHandle = index;
        }

        private void actualizarTipoDeCambioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (XtraMessageBox.Show(String.Format("¿Esta seguro que desea ejecutar el proceso de Actualización de Tipo Cambio para todos los documentos?", lkpMes.Text.ToUpper()), "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    return;
            //new BCuentasPorCobrar().ActualizarTipoCambio("DXP");
            //XtraMessageBox.Show("Actualización realizada", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void pagosConDocPorCobrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista.Count > 0)
                {
                    EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                    if (Obe.tdodc_iid_correlativo != 1 && Obe.tdodc_iid_correlativo != 36)
                    {
                        FrmCanjeDocumentoXCobrarDocumentosXPagar p = new FrmCanjeDocumentoXCobrarDocumentosXPagar();
                        p.MiEvento += new FrmCanjeDocumentoXCobrarDocumentosXPagar.DelegadoMensaje(Modify);
                        p.mes = Convert.ToInt32(lkpMes.EditValue);
                        p.eDocXPagar = Obe;
                        p.Mon = Obe.vMoneda;
                        p.IcodMon = Obe.tablc_iid_tipo_moneda;
                        p.ShowDialog();
                        xposition = viewDXP.FocusedRowHandle;
                    }
                    else
                        XtraMessageBox.Show("No se debe registrar el Pago de un Adelanto o Nota de Crédito", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            lkpMes_EditValueChanged(sender, e);
        }
    }
}