using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteNCP : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        /*--------------*/
        public ENotaCreditoProvedor Obe = new ENotaCreditoProvedor();
        public List<ENotaCreditoProveedorDet> lstDetalle = new List<ENotaCreditoProveedorDet>();
        public List<ENotaCreditoProveedorDet> lstDelete = new List<ENotaCreditoProveedorDet>();
        private List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        /*--------------*/
        public bool flag_almacen = false;

        public frmManteNCP()
        {
            InitializeComponent();
        }

        private void frmManteFacturaCompra_Load(object sender, EventArgs e)
        {
            cargar();
            if (flag_almacen)
                enablingAlmacen(false);
        }

        private void cargar()
        {
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_iid_tabla_registro != 43 && x.tarec_iid_tabla_registro != 56).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();
            List<TipoDoc> lst = new List<TipoDoc>();
            lst.Add(new TipoDoc { intCodigo = 24, strTipoDoc = "FAC" });
            //lst.Add(new TipoDoc { intCodigo = 84, strTipoDoc = "BOC" });
            BSControls.LoaderLook(lkpTipoDocRef, lst, "strTipoDoc", "intCodigo", true);


            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtIGV.Text = Parametros.strPorcIGV;
                setFecha(dtFecha);
                //lkpMes.EditValue = DateTime.Now.Month;
                getTipoCambio();
                setAlmacen();
            }
            else
            {
                //CARGAR EL DETALLE SI ES MODIFICACION O CONSULTA                
                lstDetalle = new BCompras().listarNCProveedorDet(Obe.ncpc_icod_nota_cred);
            }
            grdDetalle.DataSource = lstDetalle;
        }

        public class TipoDoc
        {
            public int intCodigo { get; set; }
            public string strTipoDoc { get; set; }
        }

        private void enablingAlmacen(bool Enabled)
        {
            bteClaseNC.Enabled = Enabled;
            bteClaseNC.Tag = 40;
            bteClaseNC.Text = "02";
            txtClaseNotaCredito.Text = "NOTA CREDITO - DEVOLUCIONES";
            //
            txtMtoDesGrav.Enabled = Enabled;
            txtImpDesGrav.Enabled = Enabled;
            txtMtoDesMixto.Enabled = Enabled;
            txtImpDesMixto.Enabled = Enabled;
            txtMtoDesNoGrav.Enabled = Enabled;
            txtImpDesNoGrav.Enabled = Enabled;
            txtMtoNoGravada.Enabled = Enabled;
        }
        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }
        public void getTipoCambio()
        {
            var Lista = lstTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtFecha.EditValue).ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            });
        }
        public void getTipoCambioReferencia()
        {
            var Lista = lstTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtFechaReferencia.EditValue).ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            });
        }
        private void setAlmacen()
        {
            var lstAlmacen = new BAlmacen().listarAlmacenes();
            if (lstAlmacen.Count > 0)
            {
                bteAlmacen.Text = lstAlmacen[0].almac_vdescripcion;
                bteAlmacen.Tag = lstAlmacen[0].almac_icod_almacen;
            }
        }
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            //txtNroDoc2.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            mnu.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
                enableControls(false);
        }

        private void enableControls(bool Enabled)
        {
            txtSerie.Enabled = Enabled;
            txtCorrelativo.Enabled = Enabled;
            //txtNroDoc2.Enabled = Enabled;
            dtFecha.Enabled = Enabled;
            bteProveedor.Enabled = Enabled;
            lkpMoneda.Enabled = Enabled;
            bteAlmacen.Enabled = Enabled;
            txtIGV.Enabled = Enabled;
            txtTipoCambio.Enabled = Enabled;
            lkpMes.Enabled = Enabled;
        }

        public void setValues()
        {
            bteProveedor.Text = Obe.strProveedor;
            bteProveedor.Tag = Obe.proc_icod_proveedor;
            bteNroDocRef.Text = Obe.ncpc_nro_doc_ref_doc;
            lkpTipoDocRef.EditValue = Obe.ncpc_tipo_doc_ref_doc;
            bteClaseNC.Tag = Obe.tdodc_iid_clase_nota_cred;
            bteClaseNC.Text = String.Format("{0:00}", Obe.intClaseNCP);
            txtClaseNotaCredito.Text = Obe.strClaseNCP;

            //if (Obe.ncpc_nro_nota_cred.Length == 12)
            //{
            txtSerie.Text = Obe.ncpc_nro_nota_cred.Substring(0, 4);
            txtCorrelativo.Text = Obe.ncpc_nro_nota_cred.Substring(4, 8);
            //}
            //else
            //    txtNroDoc2.Text = Obe.ncpc_nro_nota_cred;
            dtFecha.EditValue = Obe.ncpc_fecha_nota_cred;
            lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
            bteAlmacen.Text = Obe.strAlmacen;
            bteAlmacen.Tag = Obe.almac_icod_almacen;
            txtIGV.Text = Obe.ncpc_nporcent_imp_doc.ToString();
            //
            txtMtoDesGrav.Text = Obe.ncpc_nmonto_destino_gravado.ToString();
            txtMtoDesMixto.Text = Obe.ncpc_nmonto_destino_mixto.ToString();
            txtMtoDesNoGrav.Text = Obe.ncpc_nmonto_destino_nogravado.ToString();
            txtMtoNoGravada.Text = Obe.ncpc_nmonto_adq_nogravado.ToString();
            txtImpDesGrav.Text = Obe.ncpc_nmonto_imp_destino_gravado.ToString();
            txtImpDesMixto.Text = Obe.ncpc_nmonto_imp_destino_mixto.ToString();
            txtImpDesNoGrav.Text = Obe.ncpc_nmonto_imp_destino_nogravado.ToString();
            txtTipoCambio.Text = Obe.ncpc_tipo_cambio.ToString();
            lkpMes.EditValue = Convert.ToInt32(Obe.ncpc_mes_proceso);
            //txtMtoNeto.Text = Obe.ncpc_nmonto_neto_doc.ToString();
            //txtMtoTotal.Text = Obe.ncpc_nmonto_total_doc.ToString();
            /**/
            decimal total = lstDetalle.Sum(x => x.ncpd_nmonto_total);
            decimal neto = Math.Round(total / Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
            txtMtoNeto.Text = neto.ToString();
            txtMtoTotal.Text = total.ToString();
            dtFechaReferencia.EditValue = Obe.ncpc_sfecha_referencia;
            /**/
            dtFechaReferencia.EditValue = Obe.ncpc_sfecha_referencia;
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (bteProveedor.Tag == null)
                {
                    oBase = bteProveedor;
                    throw new ArgumentException("Seleccione proveedor");
                }

                if (Convert.ToInt32(bteClaseNC.Tag) == 0)
                {
                    oBase = bteClaseNC;
                    throw new ArgumentException("Selecione la clase de nota de crédito");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (txtSerie.Enabled)
                    {
                        if (txtSerie.Text == "000")
                        {
                            txtSerie.Focus();
                            throw new ArgumentException("Ingrese nro. de Serie de la nota de crédito");
                        }

                        if (txtCorrelativo.Text == "000000")
                        {
                            txtCorrelativo.Focus();
                            throw new ArgumentException("Ingrese nro. de la nota de crédito");
                        }

                    }
                    //else
                    //{
                    //    if (String.IsNullOrWhiteSpace(txtNroDoc2.Text))
                    //    {
                    //        oBase = txtNroDoc2;
                    //        throw new ArgumentException("Ingrese nro. de la ntoa de crédito");
                    //    }
                    //}
                }
                int? intNull = null;
                /**/
                Obe.tdodc_iid_clase_nota_cred = Convert.ToInt32(bteClaseNC.Tag);
                //if (String.IsNullOrWhiteSpace(txtNroDoc2.Text))
                Obe.ncpc_nro_nota_cred = txtSerie.Text + txtCorrelativo.Text;
                //else
                //    Obe.ncpc_nro_nota_cred = txtNroDoc2.Text;
                Obe.ncpc_fecha_nota_cred = Convert.ToDateTime(dtFecha.EditValue);
                Obe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);

                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.ncpc_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                Obe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                Obe.ncpc_mes_proceso = Convert.ToInt16(lkpMes.EditValue);
                Obe.ncpc_nmonto_neto_doc = Convert.ToDecimal(txtMtoNeto.Text);
                Obe.ncpc_nporcent_imp_doc = Convert.ToDecimal(txtIGV.Text);
                Obe.ncpc_nmonto_total_doc = Convert.ToDecimal(txtMtoTotal.Text);
                /**/
                Obe.ncpc_nmonto_destino_gravado = Convert.ToDecimal(txtMtoDesGrav.Text);
                Obe.ncpc_nmonto_destino_mixto = Convert.ToDecimal(txtMtoDesMixto.Text);
                Obe.ncpc_nmonto_destino_nogravado = Convert.ToDecimal(txtMtoDesNoGrav.Text);
                Obe.ncpc_nmonto_adq_nogravado = Convert.ToDecimal(txtMtoNoGravada.Text);
                Obe.ncpc_nmonto_imp_destino_gravado = Convert.ToDecimal(txtImpDesGrav.Text);
                Obe.ncpc_nmonto_imp_destino_mixto = Convert.ToDecimal(txtImpDesMixto.Text);
                Obe.ncpc_nmonto_imp_destino_nogravado = Convert.ToDecimal(txtImpDesNoGrav.Text);
                /**/
                Obe.ncpc_nmonto_neto_doc = Convert.ToDecimal(txtMontoNeto.Text);
                Obe.ncpc_nmonto_total_doc = Convert.ToDecimal(txtMontoTotalCabecera.Text);
                /**/
                Obe.strProveedor = bteProveedor.Text;
                Obe.strAlmacen = bteAlmacen.Text;
                /**/
                Obe.ncpc_anio = Parametros.intEjercicio;
                Obe.tablc_iid_situacion = 1;
                if (dtFechaReferencia.EditValue != null)
                {
                    Obe.ncpc_sfecha_referencia = Convert.ToDateTime(dtFechaReferencia.EditValue);
                }
                else
                {
                    Obe.ncpc_sfecha_referencia = null;
                }
                if (Convert.ToInt32(lkpTipoDocRef.EditValue) != 0)
                {
                    Obe.ncpc_tipo_doc_ref_doc = Convert.ToInt32(lkpTipoDocRef.EditValue);
                }
                else
                {
                    Obe.ncpc_tipo_doc_ref_doc = null;
                }
                Obe.ncpc_nro_doc_ref_doc = bteNroDocRef.Text;
                /**/
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.ncpc_flag_importacion = false;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.ncpc_icod_nota_cred = new BCompras().insertarNCProveedor(Obe, lstDetalle);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCompras().modificarNCProveedor(Obe, lstDetalle, lstDelete);
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Obe.ncpc_icod_nota_cred);
                    this.Close();
                }
            }
        }

        private void listarProveedor()
        {
            FrmListarProveedor frm = new FrmListarProveedor();
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bteProveedor.Tag = frm._Be.iid_icod_proveedor;
                bteProveedor.Text = frm._Be.vnombrecompleto;
            }
        }

        private void listarAlmacen()
        {
            using (frmListarAlmacen frm = new frmListarAlmacen())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = frm._Be.almac_icod_almacen;
                    bteAlmacen.Text = frm._Be.almac_vdescripcion;
                }
            }
        }

        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProveedor();
        }

        private void bteProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarProveedor();
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void bteAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarAlmacen();
        }

        private void nuevo()
        {
            BaseEdit oBase = null;
            try
            {
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione Almacén");
                }
                using (frmManteNCPDetalle frm = new frmManteNCPDetalle())
                {
                    frm.SetInsert();
                    frm.lstDetalle = lstDetalle;
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewDetalle.RefreshData();
                        viewDetalle.MoveLast();
                        setTotal();
                    }
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar()
        {
            ENotaCreditoProveedorDet obe = (ENotaCreditoProveedorDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            int index = viewDetalle.FocusedRowHandle;
            using (frmManteNCPDetalle frm = new frmManteNCPDetalle())
            {
                frm.obe = obe;
                frm.lstDetalle = lstDetalle;
                frm.SetModify();
                frm.txtMoneda.Text = lkpMoneda.Text;
                frm.txtItem.Text = String.Format("{0:000}", obe.ncpd_iitem);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();
                    setTotal();
                    viewDetalle.FocusedRowHandle = index;
                }
            }
        }

        private void eliminar()
        {
            ENotaCreditoProveedorDet obe = (ENotaCreditoProveedorDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            viewDetalle.RefreshData();
            setTotal();
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

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void txtSerie_KeyUp(object sender, KeyEventArgs e)
        {
            //if ((txtSerie.Text) != "0")
            //{
            //    txtNroDoc2.Enabled = false;
            //    txtNroDoc2.Text = String.Empty;
            //}
            //else 
            //{
            //    if (Convert.ToInt32(txtCorrelativo.Text) == 0)
            //        txtNroDoc2.Enabled = true;
            //}
        }

        private void txtCorrelativo_KeyUp(object sender, KeyEventArgs e)
        {
            //if (Convert.ToInt32(txtCorrelativo.Text) != 0)
            //{
            //       txtNroDoc2.Enabled = false;
            //    txtNroDoc2.Text = String.Empty;
            //}
            //else
            //{
            //    if (Convert.ToInt32(txtSerie.Text) == 0)
            //        txtNroDoc2.Enabled = true;
            //}
        }

        private void txtNroDoc2_KeyUp(object sender, KeyEventArgs e)
        {
            //if (String.IsNullOrEmpty(txtNroDoc2.Text))
            //{
            //    txtSerie.Enabled = true;
            //    txtCorrelativo.Enabled = true;
            //}
            //else
            //{
            //    txtSerie.Enabled = false;
            //    txtCorrelativo.Enabled = false;
            //}
        }

        private void dtFecha_EditValueChanged(object sender, EventArgs e)
        {
            getTipoCambio();
            switch (Convert.ToDateTime(dtFecha.EditValue).Month)
            {
                case 0:
                    lkpMes.EditValue = 43;
                    break;
                case 1:
                    lkpMes.EditValue = 44;
                    break;
                case 2:
                    lkpMes.EditValue = 45;
                    break;
                case 3:
                    lkpMes.EditValue = 46;
                    break;
                case 4:
                    lkpMes.EditValue = 47;
                    break;
                case 5:
                    lkpMes.EditValue = 48;
                    break;
                case 6:
                    lkpMes.EditValue = 49;
                    break;
                case 7:
                    lkpMes.EditValue = 50;
                    break;
                case 8:
                    lkpMes.EditValue = 51;
                    break;
                case 9:
                    lkpMes.EditValue = 43;
                    break;
                case 10:
                    lkpMes.EditValue = 52;
                    break;
                case 11:
                    lkpMes.EditValue = 54;
                    break;
                case 12:
                    lkpMes.EditValue = 55;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

        private void setTotal()
        {
            if (lstDetalle.Count > 0)
            {
                decimal total = lstDetalle.Sum(x => x.ncpd_nmonto_total);
                decimal neto = Math.Round(total / Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                txtMtoNeto.Text = neto.ToString();
                txtMtoTotal.Text = total.ToString();
                if (flag_almacen)
                    txtMtoDesGrav.Text = neto.ToString();
            }
            else
            {
                decimal neto = 0;
                txtMtoDesGrav.Text = neto.ToString();
            }
        }

        private void cbIncluyeIGV_CheckedChanged(object sender, EventArgs e)
        {
            setTotal();
        }

        private void txtMtoDesGrav_()
        {
            decimal neto = Convert.ToDecimal(txtMtoDesGrav.Text);
            decimal impuesto = neto * Convert.ToDecimal("0." + txtIGV.Text.Replace(".", ""));
            txtImpDesGrav.Text = impuesto.ToString();
        }

        private void txtMtoDesMixto_()
        {
            decimal neto = Convert.ToDecimal(txtMtoDesMixto.Text);
            decimal impuesto = neto * Convert.ToDecimal("0." + txtIGV.Text.Replace(".", ""));
            txtImpDesMixto.Text = impuesto.ToString();
        }

        private void txtMtoDesNoGrav_()
        {
            decimal neto = Convert.ToDecimal(txtMtoDesNoGrav.Text);
            decimal impuesto = neto * Convert.ToDecimal("0." + txtIGV.Text.Replace(".", ""));
            txtImpDesNoGrav.Text = impuesto.ToString();
        }
        private void txtMtoDesGrav_EditValueChanged(object sender, EventArgs e)
        {
            txtMtoDesGrav_();
            txtMontocabecera();
            txtMontoNetoCabecera();
        }

        private void txtMtoDesMixto_EditValueChanged(object sender, EventArgs e)
        {
            txtMtoDesMixto_();
            txtMontocabecera();
            txtMontoNetoCabecera();
        }

        private void txtMtoDesNoGrav_EditValueChanged(object sender, EventArgs e)
        {
            txtMtoDesNoGrav_();
            txtMontocabecera();
            txtMontoNetoCabecera();
        }

        private void txtIGV_EditValueChanged(object sender, EventArgs e)
        {
            setTotal();
            txtMtoDesGrav_(); txtMtoDesMixto_(); txtMtoDesNoGrav_();
        }

        private void listarDocReferencia()
        {
            try
            {
                if (Convert.ToInt32(bteProveedor.Tag) == 0)
                    throw new ArgumentException("Seleccione el proveedor");
                FrmListarDocXPagar frm = new FrmListarDocXPagar();
                frm.intProveedor = Convert.ToInt32(bteProveedor.Tag);
                frm.flag_no_pendiente = false;
                frm.flag_docs_para_ncp = true;
                frm.NCPI = 1;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lkpTipoDocRef.EditValue = frm._Be.tdocc_icod_tipo_doc;
                    bteNroDocRef.Text = frm._Be.doxpc_vnumero_doc;
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bteNroDocRef_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarDocReferencia();
        }


        private void listarNCPClase()
        {
            frmListarClaseDocumento Clase = new frmListarClaseDocumento();
            Clase.intTipoDoc = 86;//86 es el id de la NCP
            if (Clase.ShowDialog() == DialogResult.OK)
            {
                bteClaseNC.Tag = Clase._Be.tdocd_iid_correlativo;
                bteClaseNC.Text = String.Format("{0:00}", Clase._Be.tdocd_iid_codigo_doc_det);
                txtClaseNotaCredito.Text = Clase._Be.tdocd_descripcion;
            }
        }

        private void bteClaseNC_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarNCPClase();
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtSerie_Leave(object sender, EventArgs e)
        {

            if (txtSerie.Text.Length == 4)
            {

            }
            else
            {
                XtraMessageBox.Show("N° Serie debe ser 4");
            }
            if (txtSerie.Text == "")
            {
                XtraMessageBox.Show("N° Serie no puede ser vacio");
            }
        }
        private void txtMontocabecera()
        {
            decimal monto_total = Convert.ToDecimal(txtMtoDesGrav.Text) + Convert.ToDecimal(txtMtoDesMixto.Text) + Convert.ToDecimal(txtMtoDesNoGrav.Text) + Convert.ToDecimal(txtMtoNoGravada.Text) +
               Convert.ToDecimal(txtImpDesGrav.Text) + Convert.ToDecimal(txtImpDesMixto.Text) + Convert.ToDecimal(txtImpDesNoGrav.Text);
            txtMontoTotalCabecera.Text = Convertir.RedondearNumero(monto_total).ToString();
        }

        private void txtMontoNetoCabecera()
        {
            decimal monto_neto = Convert.ToDecimal(txtMtoDesGrav.Text) + Convert.ToDecimal(txtMtoDesMixto.Text) + Convert.ToDecimal(txtMtoDesNoGrav.Text) + Convert.ToDecimal(txtMtoNoGravada.Text);
            //Convert.ToDecimal(txtImpDesGrav.Text) + Convert.ToDecimal(txtImpDesMixto.Text) + Convert.ToDecimal(txtImpDesNoGrav.Text);
            txtMontoNeto.Text = Convertir.RedondearNumero(monto_neto).ToString();
        }

        private void txtMtoNoGravada_EditValueChanged(object sender, EventArgs e)
        {
            txtMontocabecera();
            txtMontoNetoCabecera();
        }

        private void txtImpDesGrav_EditValueChanged(object sender, EventArgs e)
        {
            txtMontocabecera();
        }

        private void txtImpDesMixto_EditValueChanged(object sender, EventArgs e)
        {
            txtMontocabecera();
        }

        private void txtImpDesNoGrav_EditValueChanged(object sender, EventArgs e)
        {
            txtMontocabecera();
        }

        private void dtFechaReferencia_EditValueChanged(object sender, EventArgs e)
        {
            getTipoCambioReferencia();
        }

    }
}