using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using SGE.WindowForms.Otros.Cuentas_por_Pagar;
using SGE.WindowForms.Tesorería.Bancos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Cuentas_Corrientes
{
    public partial class Frm04LXP : DevExpress.XtraEditors.XtraForm
    {
        List<ELetraPorPagar> lstLetrasPorCobrar = new List<ELetraPorPagar>();

        public Frm04LXP()
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
            lstLetrasPorCobrar = new BCuentasPorPagar().listarLetraPorPagar(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
            lstLetrasPorCobrar.ForEach(x =>
            {
                x.strNumero = x.lexpc_vnumero_letra.Insert(2, " - ");
            });
            grdLetraPorCobrar.DataSource = lstLetrasPorCobrar;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstLetrasPorCobrar.FindIndex(x => x.lexpc_icod_correlativo == intIcod);
            viewLetraPorCobrar.FocusedRowHandle = index;
            viewLetraPorCobrar.Focus();

        }

        private void nuevo()
        {
            FrmManteLetraPorPagar frm = new FrmManteLetraPorPagar();
            frm.MiEvento += new FrmManteLetraPorPagar.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.intPeriodo = Convert.ToInt32(lkpMes.EditValue);
            //frm.txtNroLetra.Text = String.Format("{0}{1:0000}", Parametros.intEjercicio.ToString().Substring(1, 3), lstLetrasPorCobrar.Count + 1);
            frm.txtNroLetra.Text = String.Format("{0} - {1:0000}", Parametros.intEjercicio.ToString().Substring(2, 2), frm.CorrelativoLetra);
            frm.Show();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }


        private void modificar()
        {
            ELetraPorPagar oBe = (ELetraPorPagar)viewLetraPorCobrar.GetRow(viewLetraPorCobrar.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                FrmManteLetraPorPagar frm = new FrmManteLetraPorPagar();
                frm.MiEvento += new FrmManteLetraPorPagar.DelegadoMensaje(reload);
                frm.intPeriodo = oBe.mesec_iid_mes;
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

        private void ubicacionCondicion()
        {
            ELetraPorPagar oBe = (ELetraPorPagar)viewLetraPorCobrar.GetRow(viewLetraPorCobrar.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                //if (obe.tablc_iid_situacion != 1)
                //    throw new ArgumentException(String.Format("La factura no puede ser modificada, su situación es ", obe.strSituacion));

                frmManteLetraUbicacionCondicion frm = new frmManteLetraUbicacionCondicion();
                frm.MiEvento += new frmManteLetraUbicacionCondicion.DelegadoMensaje(reload);
                frm.oBeLetraXP = oBe;
                frm.flagLXC = false;
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
            ELetraPorPagar oBe = (ELetraPorPagar)viewLetraPorCobrar.GetRow(viewLetraPorCobrar.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                var intSituacionDXC = new BCuentasPorPagar().getSituacionDocPorPagar(oBe.doxpc_icod_correlativo);
                if (intSituacionDXC != 8)
                    throw new ArgumentException("Ya se han realizado pagos a la letra por pagar, no puede ser eliminada");
                else
                {
                    if (XtraMessageBox.Show("¿Está seguro de eliminar la letra por pagar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        oBe.intUsuario = Valores.intUsuario;
                        oBe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        new BCuentasPorPagar().eliminarLetraPorPagar(oBe);
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
            grdLetraPorCobrar.DataSource = lstLetrasPorCobrar.Where(x => x.lexpc_vnumero_letra.Trim().Contains(txtNumero.Text.Trim()) &&
                x.strDesProveedor.ToUpper().Trim().Contains(txtCliente.Text.Trim().ToUpper())).ToList();
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            cargar();
        }

        private void rEgUbicaciónCondiciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ubicacionCondicion();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ELetraPorPagar oBe = (ELetraPorPagar)viewLetraPorCobrar.GetRow(viewLetraPorCobrar.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                var lstPlanCuentas = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
                #region cabecera
                EVoucherContableCab obj_CompCab = new EVoucherContableCab();
                obj_CompCab.anioc_iid_anio = Parametros.intEjercicio;
                obj_CompCab.mesec_iid_mes = oBe.lexpc_sfecha_letra.Month;
                //obj_CompCabLxp.vcocc_icod_vcontable = lstCabeceras.Count + 1;
                //obj_CompCabLxp.subdi_icod_subdiario = Convert.ToInt32(lstParamentros.parac_id_sd_docxpag);
                obj_CompCab.vcocc_fecha_vcontable = oBe.lexpc_sfecha_letra;
                obj_CompCab.vcocc_glosa = (oBe.lexpc_vobservaciones == null || oBe.lexpc_vobservaciones == "") ? "APLICACIÓN DE LETRA" : oBe.lexpc_vobservaciones.ToUpper();
                obj_CompCab.vcocc_observacion = obj_CompCab.vcocc_glosa;
                obj_CompCab.vcocc_numero_vcontable = "000000";//ESTO SE GENERARÁ AL MOMENTO DE INSERTAR (CORRELATIVO)               
                obj_CompCab.tarec_icorrelativo_origen_vcontable = 2;//ORIGEN : OTRO SISTEMA
                obj_CompCab.tablc_iid_moneda = oBe.tablc_iid_tipo_moneda;
                obj_CompCab.strTipoMoneda = (oBe.tablc_iid_tipo_moneda == 3) ? "S/." : "US$";
                //obj_CompCabLxp.intUsuario = intUsuario;
                //obj_CompCabLxp.strPc = strPc;
                obj_CompCab.vcocc_tipo_cambio = oBe.lexpc_nmonto_tipo_cambio;
                obj_CompCab.tbl_origen = "DXP-LXP";
                //obj_CompCabLxp.tbl_origen_icod = Convert.ToInt32(x.doxpc_icod_correlativo);
                #endregion
                List<EVoucherContableDet> lstCompDetalle = new List<EVoucherContableDet>();
                var lstTipoDoc = new BAdministracionSistema().listarTipoDocumento();
                #region detalle 01
                EVoucherContableDet obj_CompDet_01 = new EVoucherContableDet();
                //obj_CompDet_01.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                obj_CompDet_01.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                obj_CompDet_01.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraProveedor;
                obj_CompDet_01.vcocd_numero_doc = oBe.lexpc_vnumero_letra;
                obj_CompDet_01.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_01.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_01.vcocd_numero_doc);

                if (oBe.proc_icod_proveedor == 0)
                    throw new ArgumentException(String.Format("<<Error...>> No se encontró icod del proveedor de la LXP {0}", oBe.lexpc_vnumero_letra));

                var lstProveedor = new BCompras().ListarProveedor();

                if (lstProveedor.Where(a => a.iid_icod_proveedor == oBe.proc_icod_proveedor).ToList().Count() == 0)
                    throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_CompDet_01.strTipNroDocumento));

                EProveedor obj_Proveedor = lstProveedor.Where(x => x.iid_icod_proveedor == oBe.proc_icod_proveedor).ToList()[0];

                var lstDocumentoClase = new BAdministracionSistema().listarTipoDocumentoDetCta(Parametros.intTipoDocLetraProveedor);
                if (lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Parametros.intClaseTipoDocLetraProveedor).ToList().Count == 0)
                    throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_CompDet_01.vcocd_numero_doc));
                var obj_DocumentoClase = lstDocumentoClase.Where(z => z.tdocd_iid_correlativo == Parametros.intClaseTipoDocLetraProveedor).ToList()[0];

                if (oBe.tablc_iid_tipo_moneda == 3)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                if (oBe.tablc_iid_tipo_moneda == 4)
                    if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                        throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_01.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                obj_CompDet_01.ctacc_icod_cuenta_contable = (oBe.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                var Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_01.ctacc_icod_cuenta_contable).ToList();
                Lista.ForEach(Obe =>
                {
                    obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                    obj_CompDet_01.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                    obj_CompDet_01.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                    if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                    {
                        obj_CompDet_01.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                        obj_CompDet_01.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                        obj_CompDet_01.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                        obj_CompDet_01.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaProveedores, obj_Proveedor.anac_iid_analitica);
                    }
                });
                obj_CompDet_01.vcocd_vglosa_linea = obj_CompCab.vcocc_glosa;
                obj_CompDet_01.vcocd_nmto_tot_debe_sol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_sol = (oBe.tablc_iid_tipo_moneda == 3) ? oBe.lexpc_nmonto_total : Math.Round(oBe.lexpc_nmonto_total * Convert.ToDecimal(oBe.lexpc_nmonto_tipo_cambio), 2);
                obj_CompDet_01.vcocd_nmto_tot_debe_dol = 0;
                obj_CompDet_01.vcocd_nmto_tot_haber_dol = (oBe.tablc_iid_tipo_moneda == 4) ? oBe.lexpc_nmonto_total : Math.Round(oBe.lexpc_nmonto_total / Convert.ToDecimal(oBe.lexpc_nmonto_tipo_cambio), 2);
                obj_CompDet_01.intTipoOperacion = 1;
                obj_CompDet_01.vcocd_tipo_cambio = oBe.lexpc_nmonto_tipo_cambio;

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
                var lstDet = new BCuentasPorPagar().listarLetraPorPagarDet(oBe.lexpc_icod_correlativo);
                lstDet.ForEach(x =>
                {
                    EVoucherContableDet obj_CompDet_D = new EVoucherContableDet();
                    //obj_CompDet_D.vcocc_icod_vcontable = oBe.vcocc_icod_vcontable;
                    obj_CompDet_D.vcocd_nro_item_det = lstCompDetalle.Count + 1;
                    if (Convert.ToInt32(x.ctacc_iid_cuenta_contable) > 0)
                    {
                        obj_CompDet_D.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraProveedor;
                        obj_CompDet_D.vcocd_numero_doc = oBe.lexpc_vnumero_letra;
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
                        obj_CompDet_D.vcocd_numero_doc = x.pdxpc_vnumero_doc;
                        obj_CompDet_D.strTipNroDocumento = String.Format("{0} {1}", lstTipoDoc.Where(y => y.tdocc_icod_tipo_doc == obj_CompDet_D.tdocc_icod_tipo_doc).ToList()[0].tdocc_vabreviatura_tipo_doc, obj_CompDet_D.vcocd_numero_doc);

                        var lstDocumentoClase2 = new BAdministracionSistema().listarTipoDocumentoDetCta(Convert.ToInt32(x.tdocc_icod_tipo_doc));
                        /*****************************************/
                        if (lstDocumentoClase2.Where(a => a.tdocd_iid_correlativo == x.tdocc_iid_clase_doc).ToList().Count == 0)
                            throw new ArgumentException(String.Format("No se encontró CLASE DE DOC. para {0}-{1}", obj_CompDet_D.strTipNroDocumento.Substring(0, 3), obj_CompDet_D.vcocd_numero_doc));
                        obj_DocumentoClase = lstDocumentoClase2.Where(a => a.tdocd_iid_correlativo == x.tdocc_iid_clase_doc).ToList()[0];
                        /*****************************************/

                        if (lstProveedor.Where(a => a.iid_icod_proveedor == oBe.proc_icod_proveedor).ToList().Count() == 0)
                            throw new ArgumentException(String.Format("No se encontró datos del proveedor en doc. {0}", obj_CompDet_D.strTipNroDocumento));

                        if (oBe.proc_icod_proveedor == 0)
                            throw new ArgumentException(String.Format("No se encontró los datos de proveedor de la LXP {0}", oBe.lexpc_vnumero_letra));
                        obj_Proveedor = lstProveedor.Where(y => y.iid_icod_proveedor == oBe.proc_icod_proveedor).ToList()[0];

                        if (oBe.tablc_iid_tipo_moneda == 3)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable NACIONAL de la Clase Doc. {0} {1:00}", obj_CompDet_D.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                        if (oBe.tablc_iid_tipo_moneda == 4)
                            if (Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra) == 0)
                                throw new ArgumentException(String.Format("No se encontró cuenta contable EXTRANJERA de la Clase Doc. {0} {1:00}", obj_CompDet_D.strTipNroDocumento.Substring(0, 3), obj_DocumentoClase.tdocd_iid_codigo_doc_det));

                        obj_CompDet_D.ctacc_icod_cuenta_contable = (oBe.tablc_iid_tipo_moneda == 3) ? Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_nac) : Convert.ToInt32(obj_DocumentoClase.ctacc_icod_cuenta_contable_extra);

                        Lista = lstPlanCuentas.Where(Ob => Ob.ctacc_icod_cuenta_contable == obj_CompDet_D.ctacc_icod_cuenta_contable).ToList();
                        Lista.ForEach(Obe =>
                        {
                            obj_CompDet_D.strNroCuenta = Obe.ctacc_numero_cuenta_contable;
                            obj_CompDet_D.ctacc_icod_cuenta_debe_auto = Obe.ctacc_icod_cuenta_debe_auto;
                            obj_CompDet_D.ctacc_icod_cuenta_haber_auto = Obe.ctacc_icod_cuenta_haber_auto;
                            if (Convert.ToInt32(Obe.tablc_iid_tipo_analitica) > 0)
                            {
                                obj_CompDet_D.tablc_iid_tipo_analitica = Parametros.intTipoAnaliticaProveedores;
                                obj_CompDet_D.anad_icod_analitica = obj_Proveedor.anac_icod_analitica;
                                obj_CompDet_D.strAnalisis = String.Format("{0:00}.{1}", Parametros.intTipoAnaliticaProveedores, obj_Proveedor.anac_iid_analitica);
                            }
                        });
                    }

                    obj_CompDet_D.vcocd_vglosa_linea = x.lxppc_vconcepto;
                    obj_CompDet_D.vcocd_nmto_tot_debe_sol = (oBe.tablc_iid_tipo_moneda == 3) ? x.lxppc_nmonto_pago : Math.Round(Convert.ToDecimal(x.lxppc_nmonto_pago) * Convert.ToDecimal(oBe.lexpc_nmonto_tipo_cambio), 2);
                    obj_CompDet_D.vcocd_nmto_tot_haber_sol = 0;
                    obj_CompDet_D.vcocd_nmto_tot_debe_dol = (oBe.tablc_iid_tipo_moneda == 4) ? x.lxppc_nmonto_pago : Math.Round(Convert.ToDecimal(x.lxppc_nmonto_pago) / Convert.ToDecimal(oBe.lexpc_nmonto_tipo_cambio), 2);
                    obj_CompDet_D.vcocd_nmto_tot_haber_dol = 0;
                    obj_CompDet_D.intTipoOperacion = 1;
                    obj_CompDet_D.vcocd_tipo_cambio = oBe.lexpc_nmonto_tipo_cambio;

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
                Obxe.TipoDocumento = "LXP";
                Obxe.iid_anio = oBe.anioc_iid_anio;
                Obxe.proc_vnombrecompleto = oBe.strDesProveedor;
                Obxe.iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                Obxe.nmonto_movimiento = oBe.lexpc_nmonto_pagado;
                Obxe.vglosa = oBe.lexpc_vobservaciones;
                Obxe.TipoDocAbreviado = "LXP";
                Obxe.vnro_documento = oBe.lexpc_vnumero_letra;
                Obxe.dfecha_movimiento = oBe.lexpc_sfecha_letra;
                Obxe.nmonto_tipo_cambio = oBe.lexpc_nmonto_tipo_cambio;

                try { Obxe.imagen = Image.FromStream(WebDavTest.Get($"{Rutas.RutaImagenes}{Rutas.RutaLetras}{oBe.lexpc_icod_correlativo}/", $"{oBe.lexpc_icod_correlativo}.png")); }
                catch { }
                var lstRutas = new BAdministracionSistema().listarParametro();
                string strRutaLetra = lstRutas.Select(x => x.pmprc_vruta_letra).FirstOrDefault();

                reporte.cargar(Obxe, obj_CompCab, lstCompDetalle.Where(x =>
                    Convert.ToInt32(x.ctacc_iid_cuenta_contable_ref) == 0).ToList(), lkpMes.Text, "", "", strRutaLetra + oBe.fitec_vruta);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}