using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Tesoreria.Caja;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Tesorería.Caja
{
    public partial class Frm03LiquidacionCaja : DevExpress.XtraEditors.XtraForm
    {
        private List<ELiquidacionCaja> mlist = new List<ELiquidacionCaja>();

        public Frm03LiquidacionCaja()
        {
            InitializeComponent();
        }

        private void Frm03LiquidacionCaja_Load(object sender, EventArgs e)
        {
            var lstMeses = new BGeneral().listarTablaRegistro(4);
            BSControls.LoaderLook(lkpMeses, lstMeses, "tarec_vdescripcion", "tarec_icorrelativo_registro", true);

            lkpMeses.EditValue = DateTime.Now.Month;
            Cargar();
        }
        private void Cargar()
        {
            mlist = new BTesoreria().listarLiquidacionCaja(Parametros.intEjercicio, Convert.ToInt32(lkpMeses.EditValue));
            grdLiquidaCaja.DataSource = mlist;
        }

        void Modify()
        {
            Cargar();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            datos("Modificar");
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anular();
        }

        private void verDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verdetalle();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void viewLiquidaCaja_DoubleClick(object sender, EventArgs e)
        {
            datos("");
        }
        private void nuevo()
        {
            FrmManteLiquidacionCaja frm = new FrmManteLiquidacionCaja();
            frm.MiEvento += new FrmManteLiquidacionCaja.DelegadoMensaje(Modify);
            frm.Selected_Mes = Convert.ToInt32(lkpMeses.EditValue);
            frm.lstLiquidacionCaja = mlist;
            frm.Show();
            frm.SetInsert();
        }
        private void datos(string TypE)
        {
            ELiquidacionCaja Obe = (ELiquidacionCaja)viewLiquidaCaja.GetRow(viewLiquidaCaja.FocusedRowHandle);
            if (Obe != null)
            {
                if (Obe.situacion == "ANULADO")
                {
                    XtraMessageBox.Show("la liquidación esta anulada no puede ser modificada", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                FrmManteLiquidacionCaja frm = new FrmManteLiquidacionCaja();
                frm.MiEvento += new FrmManteLiquidacionCaja.DelegadoMensaje(Modify);
                frm.Selected_Mes = Convert.ToInt32(lkpMeses.EditValue);
                frm.oBe = Obe;
                frm.lstLiquidacionCaja = mlist;
                if (TypE == "Modificar")
                    frm.SetModify();
                else
                    frm.SetCancel();
                frm.Show();
                frm.CargarModify();
            }
        }
        private void anular()
        {
            ELiquidacionCaja Obe = (ELiquidacionCaja)viewLiquidaCaja.GetRow(viewLiquidaCaja.FocusedRowHandle);
            if (Obe != null)
            {
                if (Obe.lqcc_iid_situacion_liq == 1)
                {
                    if (XtraMessageBox.Show("¿Está seguro que desea anular la liquidación " + string.Format("{0:000}", Obe.lqcc_inro_liquid_caja) + " ?", "Informacíon del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (Convert.ToInt32(Obe.vcocc_iid_voucher_contable) > 0)
                        {
                            if (XtraMessageBox.Show("El voucher contable correspondiente a la liquidación será eliminado\n\t\t\t¿Está seguro que desea continuar con la anulación?", "Información del Sistema : Contabilidad", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                                return;
                        }
                        BTesoreria oBE = new BTesoreria();
                        oBE.anularLiquidacionCaja(Obe);
                        Cargar();
                        //if (Convert.ToInt32(Obe.vcocc_iid_voucher_contable) > 0)
                        //{
                        //    EComprobante obj = new EComprobante();
                        //    obj.iid_voucher_contable = Convert.ToInt32(Obe.vcocc_iid_voucher_contable);
                        //    BComprobantes oBl = new BComprobantes();
                        //    oBl.EliminarComprobante(obj);
                        //}
                    }
                }
                else
                {
                    XtraMessageBox.Show("La liquidación no puede ser anulada, porque su situación actual es: " + Obe.situacion, "Informacíon del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void imprimir()
        {
            ELiquidacionCaja Obe = (ELiquidacionCaja)viewLiquidaCaja.GetRow(viewLiquidaCaja.FocusedRowHandle);
            if (Obe == null)
                return;
            try
            {
                //if (Convert.ToInt32(Obe.vcocc_iid_voucher_contable) > 0)
                //{
                rpt03LiquidacionCaja_1 rpt = new rpt03LiquidacionCaja_1();
                var lstDet = new BTesoreria().ListarLiquidacionCajaDetalle(Obe.lqcc_icod_liquid_cja);
                var oBeTuple = CrearVoucherContableLiquidacionCaja(Obe, lstDet);
                rpt.carga(Obe, oBeTuple.Item1, oBeTuple.Item2, lkpMeses.Text);
                //}

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        private void verdetalle()
        {
        }
        private void lkpMeses_EditValueChanged(object sender, EventArgs e)
        {
            Cargar();
        }

        public Tuple<EVoucherContableCab, List<EVoucherContableDet>> CrearVoucherContableLiquidacionCaja(ELiquidacionCaja obj_LCJ, List<ELiquidacionCajaDet> lstLqCajaDet)
        {
            try
            {

                var lstParamentros = new BContabilidad().listarParametroContable();
                var mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();

                #region cabecera...
                EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                obj_CompCab.anioc_iid_anio = obj_LCJ.lqcc_iid_anio;
                obj_CompCab.mesec_iid_mes = obj_LCJ.lqcc_iid_mes;
                obj_CompCab.vcocc_icod_vcontable = Convert.ToInt32(obj_LCJ.vcocc_iid_voucher_contable);
                obj_CompCab.subdi_icod_subdiario = Convert.ToInt32(lstParamentros[0].parac_id_sd_cjachic);
                obj_CompCab.vcocc_fecha_vcontable = obj_LCJ.lqcc_sfecha_liquid;
                obj_CompCab.vcocc_glosa = obj_LCJ.lqcc_vconcepto;
                obj_CompCab.vcocc_observacion = obj_LCJ.lqcc_vconcepto;
                obj_CompCab.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                obj_CompCab.tablc_iid_moneda = obj_LCJ.lqcc_iid_tipo_moneda;
                obj_CompCab.intUsuario = obj_LCJ.intUsuario;
                obj_CompCab.strPc = obj_LCJ.strPc;
                obj_CompCab.vcocc_tipo_cambio = obj_LCJ.lqcc_ntipo_cambio;
                obj_CompCab.tbl_origen = "TSR-LCJ";
                obj_CompCab.tbl_origen_icod = obj_LCJ.lqcc_icod_liquid_cja;
                if (Convert.ToDecimal(obj_LCJ.lqcc_ntipo_cambio) <= 0)
                {
                    throw new ArgumentException("Tipo de cambio no válido para la generación del voucher contable");
                }
                #endregion
                //****************** CREANDO VOUCHER DETALLE *********************//                   
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstDocumento = new BAdministracionSistema().listarTipoDocumento();
                ETipoDocumento obj_Documento = new ETipoDocumento();
                #region detalle 01 (Haber)...
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                obj_CompDet_01.vcocc_icod_vcontable = Convert.ToInt32(obj_LCJ.vcocc_iid_voucher_contable);
                obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_CompDet_01.tdocc_icod_tipo_doc = 77;//ID DOC LIQUIDACION DE CAJA
                obj_Documento = lstDocumento.Where(d => d.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0];
                obj_CompDet_01.vcocd_numero_doc = obj_LCJ.caja_nro + String.Format("{0:000}", obj_LCJ.lqcc_inro_liquid_caja);
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", obj_Documento.tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);
                obj_CompDet_01.ctacc_icod_cuenta_contable = obj_LCJ.caja_iid_cuenta_contable;
                var Lista = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.strDesCuenta = Obe.ctacc_nombre_descripcion;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                });
                obj_CompDet_01.tablc_iid_tipo_analitica = obj_LCJ.caja_tipo_analitica;
                obj_CompDet_01.anad_icod_analitica = obj_LCJ.caja_icod_analitica;
                //obj_CompDet_01.strAnalisis = 
                obj_CompDet_01.vcocd_vglosa_linea = obj_LCJ.lqcc_vconcepto;
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = (obj_LCJ.lqcc_iid_tipo_moneda == 3) ? obj_LCJ.lqcc_nmonto_total : Math.Round(obj_LCJ.lqcc_nmonto_total * obj_LCJ.lqcc_ntipo_cambio, 2);
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = (obj_LCJ.lqcc_iid_tipo_moneda == 4) ? obj_LCJ.lqcc_nmonto_total : Math.Round(obj_LCJ.lqcc_nmonto_total / obj_LCJ.lqcc_ntipo_cambio, 2);
                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = obj_LCJ.lqcc_ntipo_cambio;
                lstCompDetalle.Add(obj_CompDet_01);
                //if (obj_CompDet_01.iid_cautomatica_debe != null)
                //    lstCompDetalle = AddCuentaAutomatica(obj_CompDet_01, lstCompDetalle, Convert.ToDecimal(obj_CompDet_01.nmto_tot_haber_sol),
                //                     Convert.ToDecimal(obj_CompDet_01.nmto_tot_haber_dol), mlistCuenta);

                #endregion
                #region detalle 02 (Debe)...
                lstLqCajaDet.ForEach(x =>
                {
                    EVoucherContableDet obj_item_CompDet_Det = new EVoucherContableDet();
                    obj_item_CompDet_Det.vcocc_icod_vcontable = Convert.ToInt32(obj_LCJ.vcocc_iid_voucher_contable);
                    if (x.lqcd_iid_cuenta_contable != null)
                    {
                        obj_item_CompDet_Det.vcocd_nro_item_det = (lstCompDetalle.Count == 0) ? 1 : lstCompDetalle.Count + 1;
                        obj_item_CompDet_Det.tdocc_icod_tipo_doc = 77;//ID DOC LIQUIDACION DE CAJA
                        obj_Documento = lstDocumento.Where(d => d.tdocc_icod_tipo_doc == obj_item_CompDet_Det.tdocc_icod_tipo_doc).ToList()[0];
                        obj_item_CompDet_Det.vcocd_numero_doc = obj_LCJ.caja_nro + String.Format("{0:000}", obj_LCJ.lqcc_inro_liquid_caja);
                        obj_item_CompDet_Det.strTipNroDocumento = String.Format("{0} {1}", obj_Documento.tdocc_vabreviatura_tipo_doc, obj_item_CompDet_Det.vcocd_numero_doc);
                        obj_item_CompDet_Det.ctacc_icod_cuenta_contable = Convert.ToInt32(x.lqcd_iid_cuenta_contable);
                        Lista = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_Det.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_item_CompDet_Det.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_item_CompDet_Det.strDesCuenta = Obe.ctacc_nombre_descripcion;
                            obj_item_CompDet_Det.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_item_CompDet_Det.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        });
                        obj_item_CompDet_Det.cecoc_icod_centro_costo = x.lqcd_iid_centro_costo;
                        obj_item_CompDet_Det.strCodCCosto = x.codigo_ccosto;
                        obj_item_CompDet_Det.tablc_iid_tipo_analitica = x.lqcd_iid_tipo_analitica;
                        obj_item_CompDet_Det.anad_icod_analitica = x.lqcd_iid_analitica;
                        obj_item_CompDet_Det.strAnalisis = x.analisis;
                        obj_item_CompDet_Det.vcocd_vglosa_linea = x.lqcd_vdescripcion_movim;
                        obj_item_CompDet_Det.vcocd_nmto_tot_debe_sol = (obj_LCJ.lqcc_iid_tipo_moneda == 3) ? x.lqcd_nmonto_pago : Math.Round(x.lqcd_nmonto_pago * obj_LCJ.lqcc_ntipo_cambio, 2);
                        obj_item_CompDet_Det.vcocd_nmto_tot_haber_sol = 0;
                        obj_item_CompDet_Det.vcocd_nmto_tot_debe_dol = (obj_LCJ.lqcc_iid_tipo_moneda == 4) ? x.lqcd_nmonto_pago : Math.Round(x.lqcd_nmonto_pago / obj_LCJ.lqcc_ntipo_cambio, 2);
                        obj_item_CompDet_Det.vcocd_nmto_tot_haber_dol = 0;
                        obj_item_CompDet_Det.intTipoOperacion = 1;
                        obj_item_CompDet_Det.vcocd_tipo_cambio = obj_LCJ.lqcc_ntipo_cambio;
                    }
                    else
                    {
                        obj_item_CompDet_Det.vcocd_nro_item_det = (lstCompDetalle.Count == 0) ? 1 : lstCompDetalle.Count + 1;
                        obj_item_CompDet_Det.tdocc_icod_tipo_doc = Convert.ToInt32(x.lqcd_iid_tipo_doc);
                        obj_Documento = lstDocumento.Where(d => d.tdocc_icod_tipo_doc == obj_item_CompDet_Det.tdocc_icod_tipo_doc).ToList()[0];

                        var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.lqcd_iid_tipo_doc));
                        if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList().Count == 0)
                            throw new ArgumentException("Clase de Documento no válida");
                        ETipoDocumentoDetalleCta obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == x.lqcd_iid_clase_tipo_doc).ToList()[0];

                        obj_item_CompDet_Det.vcocd_numero_doc = x.lqcd_vnumero_doc;
                        obj_item_CompDet_Det.strTipNroDocumento = String.Format("{0} {1}", obj_Documento.tdocc_vabreviatura_tipo_doc, obj_item_CompDet_Det.vcocd_numero_doc);
                        //**//
                        if (obj_LCJ.lqcc_iid_tipo_moneda == 3)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException("No se encontró cuenta contable NACIONAL para la generación del voucher contable");

                        if (obj_LCJ.lqcc_iid_tipo_moneda == 4)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException("No se encontró cuenta contable EXTRANJERA para la generación del voucher contable");
                        obj_item_CompDet_Det.ctacc_icod_cuenta_contable = (obj_LCJ.lqcc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                        Lista = mlistCuenta.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_item_CompDet_Det.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_item_CompDet_Det.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_item_CompDet_Det.strDesCuenta = Obe.ctacc_nombre_descripcion;
                            obj_item_CompDet_Det.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_item_CompDet_Det.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                        });
                        //**//
                        obj_item_CompDet_Det.cecoc_icod_centro_costo = null;
                        obj_item_CompDet_Det.tablc_iid_tipo_analitica = 5; //5 : Proveedores
                        obj_item_CompDet_Det.anad_icod_analitica = x.lqcd_iid_analitica;
                        obj_item_CompDet_Det.strAnalisis = x.analisis;
                        obj_item_CompDet_Det.vcocd_vglosa_linea = x.lqcd_vdescripcion_movim;
                        obj_item_CompDet_Det.vcocd_nmto_tot_debe_sol = (obj_LCJ.lqcc_iid_tipo_moneda == 3) ? x.lqcd_nmonto_pago : Math.Round(x.lqcd_nmonto_pago * obj_LCJ.lqcc_ntipo_cambio, 2);
                        obj_item_CompDet_Det.vcocd_nmto_tot_haber_sol = 0;
                        obj_item_CompDet_Det.vcocd_nmto_tot_debe_dol = (obj_LCJ.lqcc_iid_tipo_moneda == 4) ? x.lqcd_nmonto_pago : Math.Round(x.lqcd_nmonto_pago / obj_LCJ.lqcc_ntipo_cambio, 2);
                        obj_item_CompDet_Det.vcocd_nmto_tot_haber_dol = 0;
                        obj_item_CompDet_Det.intTipoOperacion = 1;
                        obj_item_CompDet_Det.vcocd_tipo_cambio = obj_LCJ.lqcc_ntipo_cambio;
                    }
                    lstCompDetalle.Add(obj_item_CompDet_Det);
                    //if (obj_item_CompDet_Det.iid_cautomatica_debe != null)
                    //    lstCompDetalle = AddCuentaAutomatica(obj_item_CompDet_Det, lstCompDetalle, Convert.ToDecimal(obj_item_CompDet_Det.nmto_tot_debe_sol),
                    //                     Convert.ToDecimal(obj_item_CompDet_Det.nmto_tot_debe_dol), mlistCuenta);
                });
                #endregion
                #region Totales del voucher...
                //TOTALES DE LA CABECERA DEL VOUCHER                
                obj_CompCab.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                obj_CompCab.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                obj_CompCab.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                obj_CompCab.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));

                if (lstCompDetalle.Count > 0)
                {
                    if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_haber_sol &&
                        obj_CompCab.vcocc_nmto_tot_debe_dol == obj_CompCab.vcocc_nmto_tot_haber_dol)
                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = 1;
                    else
                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = 2;
                }
                else
                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = 4;
                #endregion

                //if (Convert.ToInt32(obj_LCJ.vcocc_iid_voucher_contable) == 0)//SI ES CERO ES PORQUE NO HA SIDO REGISTRADO
                //{
                //    int NroVoucher;
                //    NroVoucher = (new ContabilidadData()).insertarVoucherContableCab(obj_CompCab, lstCompDetalle, 0);
                //    //ACTUALIZAMOS EL REGISTRO DE LA LIQUIDACION (AÑADIMOS EL NRO DE VOUCHER)
                //    (new VentasData()).ActualizaNroCompDoc(77, obj_LCJ.lqcc_icod_liquid_cja, NroVoucher);
                //}
                //else
                //{
                //    var lstDeleteDet = (new ContabilidadData()).ListarComprobanteDetalle(Convert.ToInt32(obj_LCJ.vcocc_iid_voucher_contable));
                //    lstDeleteDet.ForEach(x => { x.operacion = 3; });
                //    (new ContabilidadData()).ModificarComprobante(obj_CompCab, lstCompDetalle, lstDeleteDet);
                //}
                return new Tuple<EVoucherContableCab, List<EVoucherContableDet>>(obj_CompCab, lstCompDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}