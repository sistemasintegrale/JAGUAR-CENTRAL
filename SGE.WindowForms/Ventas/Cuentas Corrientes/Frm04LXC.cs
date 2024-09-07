using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using SGE.WindowForms.Tesorería.Bancos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Cuentas_Corrientes
{
    public partial class Frm04LXC : DevExpress.XtraEditors.XtraForm
    {
        List<ELetraPorCobrar> lstLetrasPorCobrar = new List<ELetraPorCobrar>();

        public Frm04LXC()
        {
            InitializeComponent();
        }

        private void Frm04Factura_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
            cargar();
        }

        private void cargar()
        {
            lstLetrasPorCobrar = new BCuentasPorCobrar().listarLetraPorCobrar(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
            grdLetraPorCobrar.DataSource = lstLetrasPorCobrar;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstLetrasPorCobrar.FindIndex(x => x.lexcc_icod_correlativo == intIcod);
            viewLetraPorCobrar.FocusedRowHandle = index;
            viewLetraPorCobrar.Focus();
        }

        private void nuevo()
        {
            FrmManteLetraPorCobrar frm = new FrmManteLetraPorCobrar();
            frm.MiEvento += new FrmManteLetraPorCobrar.DelegadoMensaje(reload);
            frm.intPeriodo = Convert.ToInt32(lkpMes.EditValue);
            frm.txtNroLetra.Text = String.Format("{0}{1:0000}", Parametros.intEjercicio.ToString().Substring(1, 3), lstLetrasPorCobrar.Count + 1);
            frm.SetInsert();
            frm.Show();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }


        private void modificar()
        {
            ELetraPorCobrar oBe = (ELetraPorCobrar)viewLetraPorCobrar.GetRow(viewLetraPorCobrar.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                //if (obe.tablc_iid_situacion != 1)
                //    throw new ArgumentException(String.Format("La factura no puede ser modificada, su situación es ", obe.strSituacion));

                FrmManteLetraPorCobrar frm = new FrmManteLetraPorCobrar();
                frm.MiEvento += new FrmManteLetraPorCobrar.DelegadoMensaje(reload);
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

        //

        private void ubicacionCondicion()
        {
            ELetraPorCobrar oBe = (ELetraPorCobrar)viewLetraPorCobrar.GetRow(viewLetraPorCobrar.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                //if (obe.tablc_iid_situacion != 1)
                //    throw new ArgumentException(String.Format("La factura no puede ser modificada, su situación es ", obe.strSituacion));

                frmManteLetraUbicacionCondicion frm = new frmManteLetraUbicacionCondicion();
                frm.MiEvento += new frmManteLetraUbicacionCondicion.DelegadoMensaje(reload);
                frm.oBeLetraXC = oBe;
                frm.flagLXC = true;
                frm.Show();
                frm.setValues();
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

        private void eliminar()
        {
            ELetraPorCobrar oBe = (ELetraPorCobrar)viewLetraPorCobrar.GetRow(viewLetraPorCobrar.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                var intSituacionDXC = new BCuentasPorCobrar().getSituacionDocPorCobrar(oBe.doxcc_icod_correlativo);
                if (intSituacionDXC != 1)
                    throw new ArgumentException("Ya se han realizado pagos a la letra por cobrar, no puede ser eliminada");
                else
                {
                    if (XtraMessageBox.Show("¿Está seguro de eliminar la letra por cobrar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        oBe.intUsuario = Valores.intUsuario;
                        oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        new BCuentasPorCobrar().eliminarLetraPorCobrar(oBe);
                        cargar();
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
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

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdLetraPorCobrar.DataSource = lstLetrasPorCobrar.Where(x => x.lexcc_vnumero_letra.Trim().Contains(txtNumero.Text.Trim()) &&
                x.strDesCliente.ToUpper().Trim().Contains(txtCliente.Text.Trim().ToUpper())).ToList();
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            cargar();
        }

        private void regUbicaciónCondiciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ubicacionCondicion();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ELetraPorCobrar oBe = (ELetraPorCobrar)viewLetraPorCobrar.GetRow(viewLetraPorCobrar.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                #region cabecera
                EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                obj_CompCab.anioc_iid_anio = Convert.ToInt32(oBe.anioc_iid_anio);
                obj_CompCab.mesec_iid_mes = Convert.ToInt32(oBe.mesec_iid_mes);
                obj_CompCab.vcocc_icod_vcontable = 0;
                obj_CompCab.subdi_icod_subdiario = 0;//Convert.ToInt32(lstParamentros[0].parac_id_sd_docxcob);
                obj_CompCab.vcocc_fecha_vcontable = Convert.ToDateTime(oBe.lexcc_sfecha_letra);
                obj_CompCab.vcocc_glosa = String.Format("DOCUMENTO DE VENTA {0} {1}", oBe.lexcc_vnumero_letra);
                obj_CompCab.vcocc_observacion = obj_CompCab.vcocc_glosa;
                obj_CompCab.strNroVco = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                obj_CompCab.tablc_iid_moneda = oBe.tablc_iid_tipo_moneda;
                obj_CompCab.strTipoMoneda = (oBe.tablc_iid_tipo_moneda == 1) ? "S/." : "US$";
                //obj_CompCab.intUsuario = intUsuario;
                //obj_CompCab.strPc = strPc;
                obj_CompCab.vcocc_tipo_cambio = Convert.ToDecimal(oBe.lexcc_nmonto_tipo_cambio);
                obj_CompCab.tbl_origen = "DXC";
                //obj_CompCab.tbl_origen_icod = Convert.ToInt32(x.doxcc_icod_correlativo);
                var lstPlanCuentas = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();

                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #endregion
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                //obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraCliente;
                obj_CompDet_01.vcocd_numero_doc = oBe.lexcc_vnumero_letra;
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);

                if (oBe.cliec_icod_cliente == 0)
                    throw new ArgumentException(String.Format("<<Error...>> No se encontró icod del cliente de la LXC {0}", oBe.lexcc_vnumero_letra));
                var lstCliente = new BVentas().ListarCliente();
                ECliente obj_Cliente = lstCliente.Where(x => x.cliec_icod_cliente == oBe.cliec_icod_cliente).ToList()[0];

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocLetraCliente);
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Parametros.intClaseTipoDocLetraPorCobrar).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_CompDet_01.vcocd_numero_doc));
                var obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Parametros.intClaseTipoDocLetraPorCobrar).ToList()[0];

                if (oBe.tablc_iid_tipo_moneda == 1)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBe.tablc_iid_tipo_moneda == 2)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_CompDet_01.ctacc_icod_cuenta_contable = (oBe.tablc_iid_tipo_moneda == 1) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                        obj_CompDet_01.anad_icod_analitica = obj_Cliente.anac_icod_analitica;
                        obj_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaClientes, obj_Cliente.anac_iid_analitica);
                    }
                });
                obj_CompDet_01.vcocd_vglosa_linea = obj_CompCab.vcocc_glosa;
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = (oBe.tablc_iid_tipo_moneda == 1) ? oBe.lexcc_nmonto_total : Math.Round(oBe.lexcc_nmonto_total * Convert.ToDecimal(oBe.lexcc_nmonto_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = (oBe.tablc_iid_tipo_moneda == 2) ? oBe.lexcc_nmonto_total : Math.Round(oBe.lexcc_nmonto_total / Convert.ToDecimal(oBe.lexcc_nmonto_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = 0;
                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBe.lexcc_nmonto_tipo_cambio;

                lstCompDetalle.Add(obj_CompDet_01);
                //lstDetGeneral.Add(obj_CompDet_01);/***********************************************************/
                //if (obj_CompDet_01.ctacc_icod_cuenta_debe_auto != null)
                //{
                //    var tuple = addCtaAutomatica(obj_CompDet_01, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                //    lstCompDetalle = tuple.Item1;
                //    lstDetGeneral = tuple.Item2;
                //}
                #endregion
                #region detalle 02
                var lstDet = new BCuentasPorCobrar().listarLetraPorCobrarDet(oBe.lexcc_icod_correlativo);
                lstDet.ForEach(x =>
                {
                    EVoucherContableDet obj_CompDet_D = new EVoucherContableDet();
                    //obj_CompDet_D.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_CompDet_D.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    if (Convert.ToInt32(x.ctacc_iid_cuenta_contable) > 0)
                    {
                        obj_CompDet_D.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraCliente;
                        obj_CompDet_D.vcocd_numero_doc = oBe.lexcc_vnumero_letra;
                        obj_CompDet_D.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_D.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_D.vcocd_numero_doc);
                        obj_CompDet_D.ctacc_icod_cuenta_contable = Convert.ToInt32(x.ctacc_iid_cuenta_contable);

                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_D.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_CompDet_D.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_CompDet_D.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_CompDet_D.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Obe.ctacc_iccosto)
                                obj_CompDet_D.cecoc_icod_centro_costo = x.cecoc_icod_centro_costo; ;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_CompDet_D.tablc_iid_tipo_analitica = Convert.ToInt32(x.strCodAnalitica);
                                obj_CompDet_D.anad_icod_analitica = x.anac_icod_analitica;
                                obj_CompDet_D.strAnalisis = String.Format("{0:00}.{1}", x.strCodAnalitica, x.strCodSubAnalitica);
                            }
                        });
                    }
                    else
                    {
                        obj_CompDet_D.tdocc_icod_tipo_doc = Convert.ToInt32(x.tdocc_icod_tipo_doc);
                        obj_CompDet_D.vcocd_numero_doc = x.pdxcc_vnumero_doc;
                        obj_CompDet_D.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_D.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_D.vcocd_numero_doc);

                        var lstDocumentoClase2 = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.tdocc_icod_tipo_doc));
                        /*****************************************/
                        if (lstDocumentoClase2.Where(a => a.tdocd_iid_correlativo == x.tdocc_iid_clase_doc).ToList().Count == 0)
                            throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_D.strTipNroDocumento.Substring(0, 3), obj_CompDet_D.vcocd_numero_doc));
                        obj_DocumentoClase = lstDocumentoClase2.Where(a => a.tdocd_iid_correlativo == x.tdocc_iid_clase_doc).ToList()[0];
                        /*****************************************/

                        obj_Cliente = lstCliente.Where(y => y.cliec_icod_cliente == oBe.cliec_icod_cliente).ToList()[0];

                        if (oBe.tablc_iid_tipo_moneda == 1)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_D.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                        if (oBe.tablc_iid_tipo_moneda == 2)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_D.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                        obj_CompDet_D.ctacc_icod_cuenta_contable = (oBe.tablc_iid_tipo_moneda == 1) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_D.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_CompDet_D.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_CompDet_D.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_CompDet_D.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_CompDet_D.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                                obj_CompDet_D.anad_icod_analitica = obj_Cliente.anac_icod_analitica;
                                obj_CompDet_D.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaClientes, obj_Cliente.anac_iid_analitica);
                            }
                        });
                    }

                    obj_CompDet_D.vcocd_vglosa_linea = x.lxcpc_vconcepto;
                    obj_CompDet_D.vcocd_nmto_tot_debe_sol = 0;
                    obj_CompDet_D.vcocd_nmto_tot_haber_sol = (oBe.tablc_iid_tipo_moneda == 1) ? x.lxcpc_nmonto_pago : Math.Round(Convert.ToDecimal(x.lxcpc_nmonto_pago) * Convert.ToDecimal(oBe.lexcc_nmonto_tipo_cambio), 2);
                    obj_CompDet_D.vcocd_nmto_tot_debe_dol = 0;
                    obj_CompDet_D.vcocd_nmto_tot_haber_dol = (oBe.tablc_iid_tipo_moneda == 2) ? x.lxcpc_nmonto_pago : Math.Round(Convert.ToDecimal(x.lxcpc_nmonto_pago) / Convert.ToDecimal(oBe.lexcc_nmonto_tipo_cambio), 2);
                    obj_CompDet_D.intTipoOperacion = 1;
                    obj_CompDet_D.vcocd_tipo_cambio = oBe.lexcc_nmonto_tipo_cambio;

                    lstCompDetalle.Add(obj_CompDet_D);
                    //lstDetGeneral.Add(obj_CompDet_D);/***********************************************************/
                    //if (obj_CompDet_D.ctacc_icod_cuenta_debe_auto != null)
                    //{
                    //    var tuple = addCtaAutomatica(obj_CompDet_D, lstCompDetalle, lstDetGeneral, lstPlanCuentas);
                    //    lstCompDetalle = tuple.Item1;
                    //    lstDetGeneral = tuple.Item2;
                    //}
                });
                #endregion
                #region totales y situación del voucher
                obj_CompCab.vcocc_nmto_tot_debe_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_sol));
                obj_CompCab.vcocc_nmto_tot_haber_sol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_sol));
                obj_CompCab.vcocc_nmto_tot_debe_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_debe_dol));
                obj_CompCab.vcocc_nmto_tot_haber_dol = Convert.ToDecimal(lstCompDetalle.Where(y => y.ctacc_iid_cuenta_contable_ref == null).ToList().Sum(x => x.vcocd_nmto_tot_haber_dol));
                obj_CompCab.intMovimientos = lstCompDetalle.Count;
                if (lstCompDetalle.Count > 0)
                {
                    if (obj_CompCab.vcocc_nmto_tot_debe_sol == obj_CompCab.vcocc_nmto_tot_haber_sol &&
                        obj_CompCab.vcocc_nmto_tot_debe_dol == obj_CompCab.vcocc_nmto_tot_haber_dol)
                    {
                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoCuadrado;
                        obj_CompCab.strVcoSituacion = "Cuadrado";
                    }
                    else
                    {
                        obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoNoCuadrado;
                        obj_CompCab.strVcoSituacion = "No Cuadrado";
                    }
                }
                else
                {
                    obj_CompCab.tarec_icorrelativo_situacion_vcontable = Parametros.intSitVcoSinDetalle;
                    obj_CompCab.strVcoSituacion = "Sin Detalle";
                }
                #endregion

                rtpComprobanteBanco1 reporte = new rtpComprobanteBanco1();
                ELibroBancos Obxe = new ELibroBancos();
                Obxe.TipoDocumento = "LXC";
                Obxe.iid_anio = oBe.anioc_iid_anio;
                Obxe.proc_vnombrecompleto = oBe.strDesCliente;
                Obxe.iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                Obxe.nmonto_movimiento = oBe.lexcc_nmonto_pagado;
                Obxe.vglosa = oBe.lexcc_vobservaciones;
                Obxe.TipoDocAbreviado = "LXC";
                Obxe.vnro_documento = oBe.lexcc_vnumero_letra;
                Obxe.dfecha_movimiento = oBe.lexcc_sfecha_letra;
                Obxe.nmonto_tipo_cambio = oBe.lexcc_nmonto_tipo_cambio;

                reporte.cargar(Obxe, obj_CompCab, lstCompDetalle.Where(x =>
                    Convert.ToInt32(x.ctacc_iid_cuenta_contable_ref) == 0).ToList(), lkpMes.Text, "", "", null);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}