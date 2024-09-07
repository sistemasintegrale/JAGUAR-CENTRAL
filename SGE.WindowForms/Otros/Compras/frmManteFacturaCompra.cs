using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteFacturaCompra : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        /*--------------*/
        public EFacturaCompra Obe = new EFacturaCompra();
        public List<EFacturaCompraDet> lstDetalle = new List<EFacturaCompraDet>();
        public List<EFacturaCompraDet> lstDelete = new List<EFacturaCompraDet>();
        private List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        List<EGuiaRemisionComprasMercaderia> lstGRSelecionados = new List<EGuiaRemisionComprasMercaderia>();
        List<ERecepcionComprasMercaderia> lstRCMSelecionados = new List<ERecepcionComprasMercaderia>();
        List<EDocumentosVarios> lstDocumentosVarios = new List<EDocumentosVarios>();
        //public int OCL = 0;
        public int tipo_Producto;
        /*--------------*/
        //public int CodOC = 0;
        /*Guia Remision*/
        public int CodGR = 0;
        public int GR = 0;
        public frmManteFacturaCompra()
        {
            InitializeComponent();
        }

        private void frmManteFacturaCompra_Load(object sender, EventArgs e)
        {
            cargar();
            if (tipo_Producto == 1)
            {
                tdcDocumentos.SelectedTabPage = xtpgCuentasContables;
                xtraTabPage1.PageEnabled = false;
            }
            else if (tipo_Producto == 2)
            {
                tdcDocumentos.SelectedTabPage = xtraTabPage1;
                xtpgCuentasContables.PageEnabled = false;
            }

        }
        public void setGuardar()
        {
            SetSave();

        }

        private void cargar()
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(21), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpFormaPago, new BGeneral().listarTablaRegistro(20), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_iid_tabla_registro != 43 && x.tarec_iid_tabla_registro != 56).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();

            if (tipo_Producto == 1)
            {
                #region Suministros
                List<TipoDoc> lst1 = new List<TipoDoc>();
                lst1.Add(new TipoDoc { intCodigo = 0, strTipoDoc = "NINGUNO" });
                lst1.Add(new TipoDoc { intCodigo = 103, strTipoDoc = "GRC" });
                lst1.Add(new TipoDoc { intCodigo = 120, strTipoDoc = "RCS" });
                BSControls.LoaderLook(lkpTipoDoc, lst1, "strTipoDoc", "intCodigo", true);
                #endregion
            }
            else if (tipo_Producto == 2)
            {
                #region Mercaderia
                List<TipoDoc> lst2 = new List<TipoDoc>();
                lst2.Add(new TipoDoc { intCodigo = 0, strTipoDoc = "NINGUNO" });
                lst2.Add(new TipoDoc { intCodigo = 122, strTipoDoc = "GRM" });
                lst2.Add(new TipoDoc { intCodigo = 123, strTipoDoc = "RCM" });
                BSControls.LoaderLook(lkpTipoDoc, lst2, "strTipoDoc", "intCodigo", true);
                #endregion
            }



            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtIGV.Text = Parametros.strPorcIGV;
                setFecha(dtFecha);
                setFecha(dtFechaVencimiento);

                getTipoCambio();
                setAlmacen();

            }
            else
            {
                if (tipo_Producto == 1)
                {
                    #region Suministros
                    lstDetalle = new BCompras().listarFacCompraDet(Obe.fcoc_icod_doc);
                    grdDetalle.DataSource = lstDetalle;
                    #endregion
                }
                else if (tipo_Producto == 2)
                {
                    #region Mercaderia
                    lstDetalle = new BCompras().listarFacCompraDet(Obe.fcoc_icod_doc);
                    grdMercaderia.DataSource = lstDetalle;
                    #endregion
                }
            }

        }
        public class TipoDoc
        {
            public int intCodigo { get; set; }
            public string strTipoDoc { get; set; }
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
            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();
            var Lista = lstTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtFecha.EditValue).ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            });
        }

        private void setAlmacen()
        {
            if (tipo_Producto == 1)
            {
                #region Suministros
                var lstAlmacen = new BAlmacen().listarAlmacenes();
                if (lstAlmacen.Count > 0)
                {
                    bteAlmacen.Text = lstAlmacen[0].almac_vdescripcion;
                    bteAlmacen.Tag = lstAlmacen[0].almac_icod_almacen;
                }
                #endregion
            }
            if (tipo_Producto == 2)
            {
                #region Mercaderia
                var lstAlmacenM = new BCentral().ListarProdAlmacen();
                if (lstAlmacenM.Count > 0)
                {
                    bteAlmacen.Text = lstAlmacenM[0].descripcion;
                    bteAlmacen.Tag = lstAlmacenM[0].id_almacen;
                }
                #endregion
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
            btnGuardar.Enabled = !Enabled;
            mnu.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
                enableControls(false);

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteRefreshTipoCambio.Enabled = false;
                lkpTipoDoc.Enabled = false;
                btnDocumentos.Enabled = false;
                //btnConsultar.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.View)
            {
                txtObservacion.Properties.ReadOnly = true;

                txtMtoDesGrav.Properties.ReadOnly = true;
                txtImpDesGrav.Properties.ReadOnly = true;
                txtMtoDesMixto.Properties.ReadOnly = true;
                txtImpDesMixto.Properties.ReadOnly = true;
                txtMtoDesNoGrav.Properties.ReadOnly = true;
                txtImpDesNoGrav.Properties.ReadOnly = true;
                txtMtoNoGravada.Properties.ReadOnly = true;
                txtNroDetraccion.Properties.ReadOnly = true;
                dtFechaDetracc.Enabled = false;
                bteRefreshTipoCambio.Enabled = false;
                lkpMes.Enabled = false;
                dtFechaVencimiento.Enabled = false;
                lkpFormaPago.Enabled = false;
            }

        }

        private void enableControls(bool Enabled)
        {
            txtSerie.Enabled = Enabled;
            txtCorrelativo.Enabled = Enabled;
            dtFecha.Enabled = Enabled;
            bteProveedor.Enabled = Enabled;
            //dtFechaVencimiento.Enabled = Enabled;
            //lkpFormaPago.Enabled = Enabled;
            lkpMoneda.Enabled = Enabled;
            bteAlmacen.Enabled = Enabled;
            txtIGV.Enabled = Enabled;
            cbIncluyeIGV.Enabled = Enabled;
            txtTipoCambio.Enabled = Enabled;
            lkpMes.Enabled = Enabled;

        }

        public void setValues()
        {
            bteProveedor.Tag = Obe.proc_icod_proveedor;
            bteProveedor.Text = Obe.strProveedor;
            txtSerie.Text = Obe.fcoc_num_doc.Substring(0, 4);
            txtCorrelativo.Text = Obe.fcoc_num_doc.Substring(4, 8);
            dtFecha.EditValue = Obe.fcoc_sfecha_doc;
            lkpFormaPago.EditValue = Obe.fcoc_iforma_pago;
            dtFechaVencimiento.EditValue = Obe.fcoc_svencimiento;
            txtObservacion.Text = Obe.fcoc_vreferencia;
            lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
            bteAlmacen.Text = Obe.strAlmacen;
            bteAlmacen.Tag = Obe.almac_icod_almacen;
            txtIGV.Text = Obe.fcoc_nporcent_imp_doc.ToString();
            lkpSituacion.EditValue = Obe.fcoc_isituacion;
            //
            txtMtoDesGrav.Text = Obe.fcoc_nmonto_destino_gravado.ToString();
            txtMtoDesMixto.Text = Obe.fcoc_nmonto_destino_mixto.ToString();
            txtMtoDesNoGrav.Text = Obe.fcoc_nmonto_destino_nogravado.ToString();
            txtMtoNoGravada.Text = Obe.fcoc_nmonto_nogravado.ToString();
            txtImpDesGrav.Text = Obe.fcoc_nmonto_imp_destino_gravado.ToString();
            txtImpDesMixto.Text = Obe.fcoc_nmonto_imp_destino_mixto.ToString();
            txtImpDesNoGrav.Text = Obe.fcoc_nmonto_imp_destino_nogravado.ToString();
            txtTipoCambio.Text = Obe.fcoc_nmonto_tipo_cambio.ToString();
            txtAdelanto.Text = Obe.fcoc_nmonto_adelanto.ToString();
            lkpMes.EditValue = Convert.ToInt32(Obe.fcoc_imes_iid_proceso);
            cbIncluyeIGV.Checked = Obe.fcoc_bincluye_igv;
            txtNroDetraccion.Text = Obe.fcoc_vnro_depo_detraccion;
            dtFechaDetracc.Text = (Obe.fcoc_sfecha_depo_detraccion == null) ? "" : Convert.ToDateTime(Obe.fcoc_sfecha_depo_detraccion).ToShortDateString();
            txtMtoNeto.Text = Obe.fcoc_nmonto_neto_detalle.ToString();
            txtMtoTotal.Text = Obe.fcoc_nmonto_total_detalle.ToString();
            //btnOC.Tag = Obe.ococ_icod_orden_compra;
            //btnOC.Text = Obe.ococ_numero_orden_compra;
            tipo_Producto = Obe.fcoc_tipo_producto;
            lkpTipoDoc.EditValue = Obe.fcoc_tipo_documento;
            btnDocumentos.Tag = Obe.fcoc_icod_documento;
            btnDocumentos.Text = Obe.NumTD;
            txtMontoTotalCabecera.Text = (Obe.fcoc_nmonto_total_detalle - Obe.fcoc_nmonto_adelanto).ToString();
            //bteProveedor.Tag = Obe.proc_icod_proveedor;
            //bteProveedor.Text = Obe.strProveedor;



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
                if (Convert.ToInt32(lkpMoneda.EditValue) == 4)
                {
                    if (txtTipoCambio.Text == "")
                    {
                        oBase = txtTipoCambio;
                        throw new ArgumentException("Ingrese Tipo de Cambio");
                    }
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (txtSerie.Enabled)
                    {
                        if (txtSerie.Text == "000")
                        {
                            txtSerie.Focus();
                            throw new ArgumentException("Ingrese nro. de Serie de la factura");
                        }

                        if (txtCorrelativo.Text == "000000")
                        {
                            txtCorrelativo.Focus();
                            throw new ArgumentException("Ingrese nro. de la factura");
                        }

                    }

                }
                if (Convert.ToInt32(lkpMes.EditValue) == 0)
                {
                    oBase = lkpMes;
                    throw new ArgumentException("Seleccione el Mes del Proceso");
                }
                if (lstDetalle.Count == 0)
                {
                    XtraMessageBox.Show("La Factura de Compra, debe tener al menos un registro de un producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Flag = false;
                    return;

                }

                if (Convert.ToDecimal(txtMontoTotalCabecera.Text) != Convert.ToDecimal(txtMtoTotal.Text))
                {
                    if (XtraMessageBox.Show("Los Montos de la Cabecera no coinciden con los Item de la Factura ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                    }
                    else
                    {
                        Flag = false;
                        return;
                    }

                }
                /**/
                DateTime? dtNullVal = null;
                int? intNullVal = null;
                Int16? intNullVal16 = null;

                Obe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);

                Obe.fcoc_num_doc = txtSerie.Text + txtCorrelativo.Text;

                Obe.fcoc_sfecha_doc = Convert.ToDateTime(dtFecha.EditValue);
                Obe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                Obe.fcoc_svencimiento = Convert.ToDateTime(dtFechaVencimiento.EditValue);
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.fcoc_iforma_pago = Convert.ToInt32(lkpFormaPago.EditValue);
                Obe.fcoc_vreferencia = txtObservacion.Text;
                Obe.fcoc_nporcent_imp_doc = Convert.ToDecimal(txtIGV.Text);
                Obe.fcoc_nmonto_destino_gravado = Convert.ToDecimal(txtMtoDesGrav.Text);
                Obe.fcoc_nmonto_destino_mixto = Convert.ToDecimal(txtMtoDesMixto.Text);
                Obe.fcoc_nmonto_destino_nogravado = Convert.ToDecimal(txtMtoDesNoGrav.Text);
                Obe.fcoc_nmonto_nogravado = Convert.ToDecimal(txtMtoNoGravada.Text);
                Obe.fcoc_nmonto_imp_destino_gravado = Convert.ToDecimal(txtImpDesGrav.Text);
                Obe.fcoc_nmonto_imp_destino_mixto = Convert.ToDecimal(txtImpDesMixto.Text);
                Obe.fcoc_nmonto_imp_destino_nogravado = Convert.ToDecimal(txtImpDesNoGrav.Text);
                Obe.fcoc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                Obe.fcoc_nmonto_adelanto = Convert.ToDecimal(txtAdelanto.Text);
                Obe.fcoc_imes_iid_proceso = Convert.ToInt16(lkpMes.EditValue);
                Obe.fcoc_bincluye_igv = cbIncluyeIGV.Checked;
                Obe.fcoc_vnro_depo_detraccion = txtNroDetraccion.Text;
                Obe.fcoc_sfecha_depo_detraccion = (String.IsNullOrWhiteSpace(dtFechaDetracc.Text)) ? dtNullVal : Convert.ToDateTime(dtFechaDetracc.Text);
                Obe.fcoc_nmonto_neto_detalle = Convert.ToDecimal(txtMtoNeto.Text);
                Obe.fcoc_nmonto_total_detalle = Convert.ToDecimal(txtMtoTotal.Text);
                Obe.fcoc_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                //Obe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                //Obe.ococ_icod_orden_compra = Convert.ToInt32(btnOC.Tag);
                Obe.fcoc_anio = Convert.ToInt16(Parametros.intEjercicio);
                Obe.fcoc_tipo_documento = Convert.ToInt32(lkpTipoDoc.EditValue);
                Obe.fcoc_icod_documento = Convert.ToInt32(btnDocumentos.Tag);
                /**/
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.fcoc_flag_importacion = false;///////////////////
                Obe.fcoc_iestado_recep = 273;//FACTURADO
                Obe.fcoc_tipo_producto = tipo_Producto;
                Obe.MontoTotalCabecera = Convert.ToDecimal(txtMontoTotalCabecera.Text);
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (tipo_Producto == 1)//Suministros
                    {
                        Obe.fcoc_icod_doc = new BCompras().insertarFacCompraNacional(Obe, lstDetalle);
                    }
                    else if (tipo_Producto == 2)//Mercaderia
                    {
                        Obe.fcoc_icod_doc = new BCompras().insertarFacCompraMercaderia(Obe, lstDetalle, lstDocumentosVarios);
                    }

                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCompras().modificarFacCompraNacional(Obe, lstDetalle, lstDelete);
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
                    this.MiEvento(Obe.fcoc_icod_doc);
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
            txtSerie.Focus();
        }

        private void bteProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (tipo_Producto == 1)//Suministros
            {
                if (e.KeyCode == Keys.F10)
                    listarProveedor();
            }
            else if (tipo_Producto == 2)//Mercaderia
            {

            }
        }
        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (tipo_Producto == 1)//Suministros
            {
                listarAlmacen();
                txtMtoDesGrav.Focus();
            }
            else if (tipo_Producto == 2)//Mercaderia
            {
                ListarAlmacenPT();
            }
        }
        private void ListarAlmacenPT()
        {
            using (FrmListarAlmacen Almacen = new FrmListarAlmacen())
            {
                Almacen.puvec_icod_punto_venta = 2;
                if (Almacen.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = Almacen._Be.id_almacen;
                    bteAlmacen.Text = Almacen._Be.descripcion;
                    //txtalmacen.Text = Almacen._Be.descripcion;
                }
            }
        }
        private void bteAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (tipo_Producto == 1)//Suministros
            {
                if (e.KeyCode == Keys.F10)
                    listarAlmacen();
            }
            else if (tipo_Producto == 2)//Mercaderia
            {
                ListarAlmacenPT();
            }
        }
        private void nuevo()
        {
            BaseEdit oBase = null;
            try
            {
                if (tipo_Producto == 1)
                {
                    #region Suministros
                    if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                    {
                        oBase = bteAlmacen;
                        throw new ArgumentException("Seleccione Almacén");
                    }
                    using (frmManteFacCompraDetalle frm = new frmManteFacCompraDetalle())
                    {
                        frm.SetInsert();
                        frm.lstDetalle = lstDetalle;
                        frm.txtMoneda.Text = lkpMoneda.Text;
                        frm.tipo_Producto = tipo_Producto;
                        frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                        frm.CodGR = CodGR;
                        frm.GR = GR;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            lstDetalle = frm.lstDetalle;
                            grdDetalle.DataSource = lstDetalle;
                            viewDetalle.RefreshData();
                            viewDetalle.MoveLast();
                            setTotal();
                        }
                    }
                    #endregion
                }
                else if (tipo_Producto == 2)
                {
                    #region Mercaderia
                    using (frmManteFacCompraMercaderiaDetalle frm = new frmManteFacCompraMercaderiaDetalle())
                    {
                        frm.SetInsert();
                        frm.lstDetalle = lstDetalle;
                        frm.txtitem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                        //frm.CodOC = CodOC;
                        //frm.OCL = OCL;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            lstDetalle = frm.lstDetalle;
                            grdMercaderia.DataSource = lstDetalle;
                            viewMercaderia.RefreshData();
                            viewMercaderia.MoveLast();
                            setTotal();
                        }
                    }
                    #endregion
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

            if (tipo_Producto == 1)
            {
                #region Suministros
                EFacturaCompraDet obe = (EFacturaCompraDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                if (obe == null)
                    return;
                int index = viewDetalle.FocusedRowHandle;
                using (frmManteFacCompraDetalle frm = new frmManteFacCompraDetalle())
                {
                    frm.obe = obe;
                    frm.lstDetalle = lstDetalle;
                    frm.SetModify();
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.tipo_Producto = tipo_Producto;
                    frm.txtItem.Text = String.Format("{0:000}", obe.fcod_iitem);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewDetalle.RefreshData();
                        setTotal();
                        viewDetalle.FocusedRowHandle = index;
                    }
                }
                #endregion
            }
            else if (tipo_Producto == 2)
            {
                #region Mercaderia
                EFacturaCompraDet obe = (EFacturaCompraDet)viewMercaderia.GetRow(viewMercaderia.FocusedRowHandle);
                if (obe == null)
                    return;
                int index = viewMercaderia.FocusedRowHandle;
                using (frmManteFacCompraMercaderiaDetalle frm = new frmManteFacCompraMercaderiaDetalle())
                {
                    frm._BE = obe;
                    frm.lstDetalle = lstDetalle;
                    frm.SetModify();
                    frm.txtitem.Text = String.Format("{0:000}", obe.fcod_iitem);
                    frm.btncodigoProducto.Enabled = false;
                    frm.txtgrupo.Enabled = false;
                    frm.txtgrupo2.Enabled = false;
                    frm.btngenerar.Enabled = false;
                    //frm.btnGuardar.Enabled = false;
                    frm.modificarDeta();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewDetalle.RefreshData();
                        setTotal();
                        viewDetalle.FocusedRowHandle = index;
                    }
                }
                #endregion
            }
        }
        private void eliminar()
        {
            EFacturaCompraDet obe = (EFacturaCompraDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
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

        private void dtFecha_EditValueChanged(object sender, EventArgs e)
        {
            //getTipoCambio();
            dtFechaVencimiento.EditValue = dtFecha.EditValue;
            lkpMes.EditValue = Convert.ToDateTime(dtFecha.EditValue).Month;
        }

        private void setTotal()
        {
            if (lstDetalle.Count > 0)
            {
                if (cbIncluyeIGV.Checked)
                {
                    decimal total = lstDetalle.Sum(x => x.fcod_nmonto_total);
                    decimal neto = Math.Round(total / Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                    decimal adelanto = Convert.ToDecimal(txtAdelanto.Text);
                    txtMtoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                    txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                    txtMontoTotalCabecera.Text = Convertir.RedondearNumero(total - adelanto).ToString();
                    txtMtoDesGrav.Text = (Convert.ToDecimal(txtMtoTotal.Text) / Convert.ToDecimal("1." + txtIGV.Text.Replace(".", ""))).ToString();
                    txtImpDesGrav.Text = (Convert.ToDecimal(txtMtoTotal.Text) - Convert.ToDecimal(txtMtoDesGrav.Text)).ToString();

                }
                else
                {
                    decimal neto = lstDetalle.Sum(x => x.fcod_nmonto_total);
                    decimal total = Math.Round(neto * Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                    decimal adelanto = Convert.ToDecimal(txtAdelanto.Text);
                    txtMtoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                    txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                    txtMontoTotalCabecera.Text = Convertir.RedondearNumero(total - adelanto).ToString();
                    txtMtoDesGrav.Text = neto.ToString();
                    txtImpDesGrav.Text = (Math.Round(total - neto, 2)).ToString();

                }

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
            txtImpDesGrav.Text = Convertir.RedondearNumero(impuesto).ToString();
        }
        private void txtMontocabecera()
        {
            decimal monto_total = Convert.ToDecimal(txtMtoDesGrav.Text) + Convert.ToDecimal(txtMtoDesMixto.Text) + Convert.ToDecimal(txtMtoDesNoGrav.Text) + Convert.ToDecimal(txtMtoNoGravada.Text) +
               Convert.ToDecimal(txtImpDesGrav.Text) + Convert.ToDecimal(txtImpDesMixto.Text) + Convert.ToDecimal(txtImpDesNoGrav.Text);
            txtMontoTotalCabecera.Text = Convertir.RedondearNumero(monto_total).ToString();
        }
        private void txtMtoDesMixto_()
        {
            decimal neto = Convert.ToDecimal(txtMtoDesMixto.Text);
            decimal impuesto = neto * Convert.ToDecimal("0." + txtIGV.Text.Replace(".", ""));
            txtImpDesMixto.Text = Convertir.RedondearNumero(impuesto).ToString();
        }

        private void txtMtoDesNoGrav_()
        {
            decimal neto = Convert.ToDecimal(txtMtoDesNoGrav.Text);
            decimal impuesto = neto * Convert.ToDecimal("0." + txtIGV.Text.Replace(".", ""));
            txtImpDesNoGrav.Text = Convertir.RedondearNumero(impuesto).ToString();
        }
        private void txtMtoDesGrav_EditValueChanged(object sender, EventArgs e)
        {
            txtMtoDesGrav_();
            txtMontocabecera();
        }

        private void txtMtoDesMixto_EditValueChanged(object sender, EventArgs e)
        {
            txtMtoDesMixto_();
            txtMontocabecera();
        }

        private void txtMtoDesNoGrav_EditValueChanged(object sender, EventArgs e)
        {
            txtMtoDesNoGrav_();
            txtMontocabecera();
        }

        private void txtIGV_EditValueChanged(object sender, EventArgs e)
        {
            setTotal();
            txtMtoDesGrav_(); txtMtoDesMixto_(); txtMtoDesNoGrav_();
        }

        private void bteRefreshTipoCambio_Click(object sender, EventArgs e)
        {
            getTipoCambio();
        }

        private void txtImpDesGrav_EditValueChanged(object sender, EventArgs e)
        {
            txtMontocabecera();
        }

        private void txtImpDesMixto_EditValueChanged(object sender, EventArgs e)
        {
            txtMontocabecera();
        }

        private void txtMtoNoGravada_EditValueChanged(object sender, EventArgs e)
        {
            txtMontocabecera();
        }

        private void txtImpDesNoGrav_EditValueChanged(object sender, EventArgs e)
        {
            txtMontocabecera();
        }
        // importar datos de un excel
        string filePath = "";
        private void importarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            if (DialogResult.OK == ofd.ShowDialog(this))//busca el archivo excel
            {
                string Extension = Path.GetExtension(ofd.FileName);

                if (Extension == ".xls")// tipo de extension
                {
                    filePath = ofd.FileName;
                    ImportarDatosExcel();
                }
                else
                {
                    ClearLista();
                    //XtraMessageBox.Show("El archivo no coincide con el tipo de archivo seleccionado " + lkpTipoArchivo.Text, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }
        private void ClearLista()
        {
            lstDetalle.Clear();
            viewDetalle.RefreshData();
        }
        private void ImportarDatosExcel()
        {
            ClearLista();

            DataTable dt;
            OleDbDataAdapter MyDataAdapter;

            string connString = "provider=Microsoft.Jet.OLEDB.4.0;" + @"data source=" + filePath + ";" + "Extended Properties=Excel 8.0;";//version del 97-2003
            OleDbConnection oledbConn = new OleDbConnection(connString);
            try
            {

                MyDataAdapter = new OleDbDataAdapter("SELECT * FROM [FactCompra$]", connString);//nombre de la hoja del excel
                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                FillList(dt);


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                oledbConn.Close();
            }
        }
        private void FillList(DataTable dt)//todos los datos del excel
        {
            int contF = 0;
            string nomC = String.Empty;
            try
            {
                if (dt.Rows.Count > 0)
                {
                    int b = dt.Rows.Count;
                    foreach (DataRow row in dt.Rows)
                    {
                        contF++;
                        EFacturaCompraDet obe = new EFacturaCompraDet();

                        foreach (DataColumn column in dt.Columns)
                        {

                            switch (column.ToString().ToUpper().Trim())//todo en mayuscula 
                            {
                                //planilla

                                case "CODPROVEEDOR":
                                    nomC = "CODPROVEEDOR";
                                    obe.prov_vcodigo_prov = row[column].ToString();
                                    break;
                                case "CODIGOPROD":
                                    nomC = "CODIGOPROD";
                                    obe.strCodProducto = row[column].ToString();
                                    List<EProducto> MlistProd = new BAlmacen().listarProductoXCodigp(Parametros.intEjercicio, obe.strCodProducto);
                                    if (MlistProd.Count > 0)
                                    {
                                        obe.prd_icod_producto = Convert.ToInt32(MlistProd[0].prdc_icod_producto);
                                        obe.fcod_vdescripcion_item = (MlistProd[0].prdc_vdescripcion_larga);
                                        obe.strLinea = (MlistProd[0].strCategoria);
                                        obe.strSubLinea = (MlistProd[0].strSubCategoriaUno);
                                        obe.strDesUM = (MlistProd[0].StrUnidadMedida);

                                        obe.strMoneda = lkpMoneda.Text;
                                    }
                                    else
                                    {
                                        obe.prd_icod_producto = 0;
                                    }
                                    break;

                                case "CANTIDAD":
                                    nomC = "CANTIDAD";
                                    obe.fcod_ncantidad = Convert.ToInt32(row[column]);
                                    break;
                                case "PUNITARIO":
                                    nomC = "PUNITARIO";
                                    obe.fcod_nmonto_unit = Convert.ToDecimal(row[column]);
                                    obe.fcod_nmonto_total = obe.fcod_nmonto_unit * obe.fcod_ncantidad;
                                    break;
                            }

                            if (lstDetalle.Count > 0)
                            {
                                if (cbIncluyeIGV.Checked)
                                {
                                    decimal total = lstDetalle.Sum(x => x.fcod_nmonto_total);
                                    decimal neto = Math.Round(total / Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                                    txtMtoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                                    txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                                }
                                else
                                {
                                    decimal neto = lstDetalle.Sum(x => x.fcod_nmonto_total);
                                    decimal total = Math.Round(neto * Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                                    txtMtoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                                    txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                                }
                            }
                        }

                        obe.intTipoOperacion = 1;
                        obe.intUsuario = Valores.intUsuario;
                        obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        lstDetalle.Add(obe);

                    }
                }
                //List<EFacturaCompraDet> mlistNuevos=new List<EFacturaCompraDet> ();
                //foreach (var BE in lstDetalle)
                //{
                //    if (BE.prd_icod_producto == 0)
                //    {
                //        mlistNuevos.Add(BE);
                //    }
                //}
                lstDetalle = lstDetalle.Where(ob => ob.prd_icod_producto != 0).ToList();
                int i = 1;
                foreach (var _bee in lstDetalle)
                {
                    _bee.fcod_iitem = i;
                    i = i + 1;
                }
                grdDetalle.DataSource = lstDetalle;
                viewDetalle.RefreshData();

                //if (mlistNuevos.Count > 0)
                //{
                //    if (XtraMessageBox.Show("¿Desea Exportar los que no existen en el catálogo de productos?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //    {
                //        ExportarDatos(mlistNuevos);
                //    }
                //}
                //XtraMessageBox.Show("Se importaron " + (lstDetalle.Where(e => e.prov_vcodigo_prov == vcod_proveedor).ToList().GroupBy(i => i.prdc_vcode_producto).Select(group => group.First()).ToList().Count()) + " registros", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //txtTotalRegistro.Text = lstDetalle.Count().ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bteLimpiar_Click(object sender, EventArgs e)
        {

            //btnOC.Tag = 0;
            //btnOC.Text = "";
            //OCL = 0;
            GR = 0;
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

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

        private void txtCorrelativo_Leave(object sender, EventArgs e)
        {
            //List<EFacturaCompra> Lstver = new BCompras().getVerificarNumero(String.Format("{0}{1}", txtSerie.Text, txtCorrelativo.Text));
            //if (Lstver.Count > 0)
            //{             
            //    XtraMessageBox.Show("El Número " + String.Format("{0}{1}", txtSerie.Text, txtCorrelativo.Text) + " N° Factura Ya Existia");             
            //}
        }

        private void btnGR_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarGuiaRemision();
        }
        private void listarGuiaRemision()
        {
            if (tipo_Producto == 1)
            {
                #region Suministros
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 103)//Guia de Remision
                {
                    FrmListarGuiaRemision frm = new FrmListarGuiaRemision();
                    frm.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                    frm.Carga();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnDocumentos.Tag = frm._Be.grcc_icod_grc;
                        btnDocumentos.Text = frm._Be.grcc_vnumero_grc;
                        CodGR = Convert.ToInt32(btnDocumentos.Tag);
                        ListarDetalleGR();
                        nuevoToolStripMenuItem.Enabled = false;
                        eliminarToolStripMenuItem.Enabled = false;
                        GR = 1;
                    }
                }
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 120)//Recepcion de Compra Suministros
                {
                    FrmListarRecepcionCompraSuministros frm = new FrmListarRecepcionCompraSuministros();
                    frm.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                    frm.Carga();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnDocumentos.Tag = frm._Be.rcsc_icod_rcs;
                        btnDocumentos.Text = frm._Be.rcsc_vnumero_rcs;
                        CodGR = Convert.ToInt32(btnDocumentos.Tag);
                        ListarDetalleGR();
                        nuevoToolStripMenuItem.Enabled = false;
                        eliminarToolStripMenuItem.Enabled = false;
                        GR = 1;
                    }
                }
                #endregion
            }
            else if (tipo_Producto == 2)
            {
                #region Mercaderia
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 122)//Guia de Remision Mercaderia
                {
                    FrmListarGuiaRemisionMercaderia frm = new FrmListarGuiaRemisionMercaderia();
                    frm.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                    frm.flag_multiple = true;
                    frm.Carga();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnDocumentos.Tag = frm._Be.grcc_icod_grc;
                        btnDocumentos.Text = frm._Be.grcc_vnumero_grc;
                        CodGR = Convert.ToInt32(btnDocumentos.Tag);
                        ListarDetalleGR();
                        nuevoToolStripMenuItem.Enabled = false;
                        eliminarToolStripMenuItem.Enabled = false;
                        GR = 1;
                    }
                }
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 123)//Recepcion Compra Mercaderia
                {
                    FrmListarRecepcionCompraMercaderia frm = new FrmListarRecepcionCompraMercaderia();
                    frm.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                    frm.flag_multiple = true;
                    frm.Carga();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnDocumentos.Tag = frm._Be.rcmc_icod_rcm;
                        btnDocumentos.Text = frm._Be.rcmc_vnumero_rcm;
                        CodGR = Convert.ToInt32(btnDocumentos.Tag);
                        ListarDetalleGR();
                        nuevoToolStripMenuItem.Enabled = false;
                        eliminarToolStripMenuItem.Enabled = false;
                        GR = 1;
                    }
                }
                #endregion
            }
        }
        public void ListarDetalleGR()
        {
            if (tipo_Producto == 1)
            {
                #region Suministros
                List<EFacturaCompraDet> lstFD = new List<EFacturaCompraDet>();
                lstDetalle = lstFD;
                grdDetalle.DataSource = 0;
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 103)//Guia de Remision
                {
                    List<EGuiaRemisionComprasDetalle> lstGRDetalle = new List<EGuiaRemisionComprasDetalle>();
                    lstGRDetalle = new BCompras().listarGuiaRemisionComprasDetalle(Convert.ToInt32(btnDocumentos.Tag));
                    foreach (var _bee in lstGRDetalle)
                    {
                        EFacturaCompraDet BGRCOMPRAS = new EFacturaCompraDet();
                        BGRCOMPRAS.fcod_iitem = _bee.grcd_iid_detalle;
                        BGRCOMPRAS.prd_icod_producto = _bee.prdc_icod_producto;
                        BGRCOMPRAS.fcod_ncantidad = _bee.grcd_ncantidad;
                        BGRCOMPRAS.fcod_vdescripcion_item = _bee.DesProducto;
                        BGRCOMPRAS.strCodProducto = _bee.strCodProd;
                        BGRCOMPRAS.strDesUM = _bee.Unidad;
                        BGRCOMPRAS.fcod_icod_kardex = _bee.kardc_icod_correlativo;
                        BGRCOMPRAS.grcd_icod_detalle = _bee.grcd_icod_detalle;
                        lstDetalle.Add(BGRCOMPRAS);
                        lstDetalle.OrderBy(E => E.fcod_iitem).ToList();
                        grdDetalle.DataSource = lstDetalle;
                    }
                    grdDetalle.RefreshDataSource();
                }
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 120)//Recepcion de Compra Suministros
                {
                    List<ERecepcionComprasSuministrosDetalle> lstRCSDetalle = new List<ERecepcionComprasSuministrosDetalle>();
                    lstRCSDetalle = new BCompras().listarRecepcionComprasSuministrosDetalle(Convert.ToInt32(btnDocumentos.Tag));
                    foreach (var _bee in lstRCSDetalle)
                    {
                        EFacturaCompraDet BGRCOMPRAS = new EFacturaCompraDet();
                        BGRCOMPRAS.fcod_iitem = _bee.rcsd_iid_detalle;
                        BGRCOMPRAS.prd_icod_producto = _bee.prdc_icod_producto;
                        BGRCOMPRAS.fcod_ncantidad = _bee.rcsd_ncantidad;
                        BGRCOMPRAS.fcod_vdescripcion_item = _bee.DesProducto;
                        BGRCOMPRAS.strCodProducto = _bee.strCodProd;
                        BGRCOMPRAS.strDesUM = _bee.Unidad;
                        BGRCOMPRAS.fcod_icod_kardex = _bee.kardc_icod_correlativo;
                        BGRCOMPRAS.grcd_icod_detalle = _bee.rcsd_icod_detalle;
                        lstDetalle.Add(BGRCOMPRAS);
                        lstDetalle.OrderBy(E => E.fcod_iitem).ToList();
                        grdDetalle.DataSource = lstDetalle;
                    }
                    grdDetalle.RefreshDataSource();
                }
                #endregion
            }
            else if (tipo_Producto == 2)
            {
                #region Mercaderia
                List<EFacturaCompraDet> lstFD = new List<EFacturaCompraDet>();
                lstDetalle = lstFD;
                grdMercaderia.DataSource = 0;
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 122)//Guia de Remision Mercaderia
                {
                    List<EGuiaRemisionComprasMercaderiaDetalle> lstGRDetalle = new List<EGuiaRemisionComprasMercaderiaDetalle>();
                    lstGRDetalle = new BCompras().listarGuiaRemisionComprasMercaderiaDetalle(Convert.ToInt32(btnDocumentos.Tag));

                    foreach (var _bee in lstGRDetalle)
                    {
                        foreach (var _DET in lstDetalle)
                        {
                            if (_DET.prd_icod_producto == _bee.prdc_icod_producto)
                            {
                                _DET.fcod_cant_talla1 = Convert.ToInt32(_DET.fcod_cant_talla1 + _bee.grcd_cant_talla1);
                                _DET.fcod_cant_talla2 = Convert.ToInt32(_DET.fcod_cant_talla2 + _bee.grcd_cant_talla2);
                                _DET.fcod_cant_talla3 = Convert.ToInt32(_DET.fcod_cant_talla3 + _bee.grcd_cant_talla3);
                                _DET.fcod_cant_talla4 = Convert.ToInt32(_DET.fcod_cant_talla4 + _bee.grcd_cant_talla4);
                                _DET.fcod_cant_talla5 = Convert.ToInt32(_DET.fcod_cant_talla5 + _bee.grcd_cant_talla5);
                                _DET.fcod_cant_talla6 = Convert.ToInt32(_DET.fcod_cant_talla6 + _bee.grcd_cant_talla6);
                                _DET.fcod_cant_talla7 = Convert.ToInt32(_DET.fcod_cant_talla7 + _bee.grcd_cant_talla7);
                                _DET.fcod_cant_talla8 = Convert.ToInt32(_DET.fcod_cant_talla8 + _bee.grcd_cant_talla8);
                                _DET.fcod_cant_talla9 = Convert.ToInt32(_DET.fcod_cant_talla9 + _bee.grcd_cant_talla9);
                                _DET.fcod_cant_talla10 = Convert.ToInt32(_DET.fcod_cant_talla10 + _bee.grcd_cant_talla10);

                                _DET.fcod_ncantidad = Convert.ToInt32(_DET.fcod_ncantidad + _bee.grcd_ncantidad);
                            }
                        }
                        if (lstDetalle.Count(ob => ob.prd_icod_producto == _bee.prdc_icod_producto) == 0)
                        {
                            EFacturaCompraDet BFCCOMPRAS = new EFacturaCompraDet();
                            BFCCOMPRAS.fcod_iitem = _bee.grcd_iid_detalle;
                            BFCCOMPRAS.prd_icod_producto = _bee.prdc_icod_producto;
                            BFCCOMPRAS.fcod_ncantidad = _bee.grcd_ncantidad;
                            BFCCOMPRAS.fcod_vdescripcion_item = _bee.DesProducto;
                            BFCCOMPRAS.strCodProducto = _bee.strCodProd;
                            BFCCOMPRAS.strDesUM = _bee.Unidad;
                            BFCCOMPRAS.fcod_icod_kardex = _bee.kardc_icod_correlativo;
                            BFCCOMPRAS.grcd_icod_detalle = _bee.grcd_icod_detalle;

                            BFCCOMPRAS.fcod_rango_talla = _bee.grcd_rango_talla;
                            BFCCOMPRAS.fcod_talla1 = _bee.grcd_talla1;
                            BFCCOMPRAS.fcod_talla2 = _bee.grcd_talla2;
                            BFCCOMPRAS.fcod_talla3 = _bee.grcd_talla3;
                            BFCCOMPRAS.fcod_talla4 = _bee.grcd_talla4;
                            BFCCOMPRAS.fcod_talla5 = _bee.grcd_talla5;
                            BFCCOMPRAS.fcod_talla6 = _bee.grcd_talla6;
                            BFCCOMPRAS.fcod_talla7 = _bee.grcd_talla7;
                            BFCCOMPRAS.fcod_talla8 = _bee.grcd_talla8;
                            BFCCOMPRAS.fcod_talla9 = _bee.grcd_talla9;
                            BFCCOMPRAS.fcod_talla10 = _bee.grcd_talla10;

                            BFCCOMPRAS.fcod_cant_talla1 = _bee.grcd_cant_talla1;
                            BFCCOMPRAS.fcod_cant_talla2 = _bee.grcd_cant_talla2;
                            BFCCOMPRAS.fcod_cant_talla3 = _bee.grcd_cant_talla3;
                            BFCCOMPRAS.fcod_cant_talla4 = _bee.grcd_cant_talla4;
                            BFCCOMPRAS.fcod_cant_talla5 = _bee.grcd_cant_talla5;
                            BFCCOMPRAS.fcod_cant_talla6 = _bee.grcd_cant_talla6;
                            BFCCOMPRAS.fcod_cant_talla7 = _bee.grcd_cant_talla7;
                            BFCCOMPRAS.fcod_cant_talla8 = _bee.grcd_cant_talla8;
                            BFCCOMPRAS.fcod_cant_talla9 = _bee.grcd_cant_talla9;
                            BFCCOMPRAS.fcod_cant_talla10 = _bee.grcd_cant_talla10;

                            BFCCOMPRAS.fcod_iid_kardex1 = _bee.grcd_iid_kardex1;
                            BFCCOMPRAS.fcod_iid_kardex2 = _bee.grcd_iid_kardex2;
                            BFCCOMPRAS.fcod_iid_kardex3 = _bee.grcd_iid_kardex3;
                            BFCCOMPRAS.fcod_iid_kardex4 = _bee.grcd_iid_kardex4;
                            BFCCOMPRAS.fcod_iid_kardex5 = _bee.grcd_iid_kardex5;
                            BFCCOMPRAS.fcod_iid_kardex6 = _bee.grcd_iid_kardex6;
                            BFCCOMPRAS.fcod_iid_kardex7 = _bee.grcd_iid_kardex7;
                            BFCCOMPRAS.fcod_iid_kardex8 = _bee.grcd_iid_kardex8;
                            BFCCOMPRAS.fcod_iid_kardex9 = _bee.grcd_iid_kardex9;
                            BFCCOMPRAS.fcod_iid_kardex10 = _bee.grcd_iid_kardex10;

                            lstDetalle.Add(BFCCOMPRAS);
                            lstDetalle.OrderBy(E => E.fcod_iitem).ToList();

                        }
                    }


                }

                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 123)//Recepcion Compra Mercaderia
                {
                    List<ERecepcionComprasMercaderiaDetalle> lstRCMDetalle = new List<ERecepcionComprasMercaderiaDetalle>();
                    lstRCMDetalle = new BCompras().listarRecepcionComprasMercaderiaDetalle(Convert.ToInt32(btnDocumentos.Tag));

                    foreach (var _bee in lstRCMDetalle)
                    {
                        foreach (var _DET in lstDetalle)
                        {
                            if (_DET.prd_icod_producto == _bee.prdc_icod_producto)
                            {
                                _DET.fcod_cant_talla1 = Convert.ToInt32(_DET.fcod_cant_talla1 + _bee.rcmd_cant_talla1);
                                _DET.fcod_cant_talla2 = Convert.ToInt32(_DET.fcod_cant_talla2 + _bee.rcmd_cant_talla2);
                                _DET.fcod_cant_talla3 = Convert.ToInt32(_DET.fcod_cant_talla3 + _bee.rcmd_cant_talla3);
                                _DET.fcod_cant_talla4 = Convert.ToInt32(_DET.fcod_cant_talla4 + _bee.rcmd_cant_talla4);
                                _DET.fcod_cant_talla5 = Convert.ToInt32(_DET.fcod_cant_talla5 + _bee.rcmd_cant_talla5);
                                _DET.fcod_cant_talla6 = Convert.ToInt32(_DET.fcod_cant_talla6 + _bee.rcmd_cant_talla6);
                                _DET.fcod_cant_talla7 = Convert.ToInt32(_DET.fcod_cant_talla7 + _bee.rcmd_cant_talla7);
                                _DET.fcod_cant_talla8 = Convert.ToInt32(_DET.fcod_cant_talla8 + _bee.rcmd_cant_talla8);
                                _DET.fcod_cant_talla9 = Convert.ToInt32(_DET.fcod_cant_talla9 + _bee.rcmd_cant_talla9);
                                _DET.fcod_cant_talla10 = Convert.ToInt32(_DET.fcod_cant_talla10 + _bee.rcmd_cant_talla10);

                                _DET.fcod_ncantidad = Convert.ToInt32(_DET.fcod_ncantidad + _bee.rcmd_ncantidad);
                            }
                        }
                        if (lstDetalle.Count(ob => ob.prd_icod_producto == _bee.prdc_icod_producto) == 0)
                        {
                            EFacturaCompraDet BFCCOMPRAS = new EFacturaCompraDet();
                            BFCCOMPRAS.fcod_iitem = _bee.rcmd_iid_detalle;
                            BFCCOMPRAS.prd_icod_producto = _bee.prdc_icod_producto;
                            BFCCOMPRAS.fcod_ncantidad = _bee.rcmd_ncantidad;
                            BFCCOMPRAS.fcod_vdescripcion_item = _bee.DesProducto;
                            BFCCOMPRAS.strCodProducto = _bee.strCodProd;
                            BFCCOMPRAS.strDesUM = _bee.Unidad;
                            BFCCOMPRAS.fcod_icod_kardex = _bee.kardc_icod_correlativo;
                            BFCCOMPRAS.grcd_icod_detalle = _bee.rcmd_icod_detalle;

                            BFCCOMPRAS.fcod_rango_talla = _bee.rcmd_rango_talla;
                            BFCCOMPRAS.fcod_talla1 = _bee.rcmd_talla1;
                            BFCCOMPRAS.fcod_talla2 = _bee.rcmd_talla2;
                            BFCCOMPRAS.fcod_talla3 = _bee.rcmd_talla3;
                            BFCCOMPRAS.fcod_talla4 = _bee.rcmd_talla4;
                            BFCCOMPRAS.fcod_talla5 = _bee.rcmd_talla5;
                            BFCCOMPRAS.fcod_talla6 = _bee.rcmd_talla6;
                            BFCCOMPRAS.fcod_talla7 = _bee.rcmd_talla7;
                            BFCCOMPRAS.fcod_talla8 = _bee.rcmd_talla8;
                            BFCCOMPRAS.fcod_talla9 = _bee.rcmd_talla9;
                            BFCCOMPRAS.fcod_talla10 = _bee.rcmd_talla10;

                            BFCCOMPRAS.fcod_cant_talla1 = _bee.rcmd_cant_talla1;
                            BFCCOMPRAS.fcod_cant_talla2 = _bee.rcmd_cant_talla2;
                            BFCCOMPRAS.fcod_cant_talla3 = _bee.rcmd_cant_talla3;
                            BFCCOMPRAS.fcod_cant_talla4 = _bee.rcmd_cant_talla4;
                            BFCCOMPRAS.fcod_cant_talla5 = _bee.rcmd_cant_talla5;
                            BFCCOMPRAS.fcod_cant_talla6 = _bee.rcmd_cant_talla6;
                            BFCCOMPRAS.fcod_cant_talla7 = _bee.rcmd_cant_talla7;
                            BFCCOMPRAS.fcod_cant_talla8 = _bee.rcmd_cant_talla8;
                            BFCCOMPRAS.fcod_cant_talla9 = _bee.rcmd_cant_talla9;
                            BFCCOMPRAS.fcod_cant_talla10 = _bee.rcmd_cant_talla10;

                            BFCCOMPRAS.fcod_iid_kardex1 = _bee.rcmd_iid_kardex1;
                            BFCCOMPRAS.fcod_iid_kardex2 = _bee.rcmd_iid_kardex2;
                            BFCCOMPRAS.fcod_iid_kardex3 = _bee.rcmd_iid_kardex3;
                            BFCCOMPRAS.fcod_iid_kardex4 = _bee.rcmd_iid_kardex4;
                            BFCCOMPRAS.fcod_iid_kardex5 = _bee.rcmd_iid_kardex5;
                            BFCCOMPRAS.fcod_iid_kardex6 = _bee.rcmd_iid_kardex6;
                            BFCCOMPRAS.fcod_iid_kardex7 = _bee.rcmd_iid_kardex7;
                            BFCCOMPRAS.fcod_iid_kardex8 = _bee.rcmd_iid_kardex8;
                            BFCCOMPRAS.fcod_iid_kardex9 = _bee.rcmd_iid_kardex9;
                            BFCCOMPRAS.fcod_iid_kardex10 = _bee.rcmd_iid_kardex10;

                            lstDetalle.Add(BFCCOMPRAS);
                            lstDetalle.OrderBy(E => E.fcod_iitem).ToList();

                        }
                    }
                }


                grdMercaderia.DataSource = lstDetalle;
                grdMercaderia.RefreshDataSource();
                #endregion
            }

        }
        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipoDoc.EditValue) == 0)
            {
                btnDocumentos.Enabled = false;
                btnVariosDoc.Enabled = false;
                nuevoToolStripMenuItem.Enabled = true;
                eliminarToolStripMenuItem.Enabled = true;

            }
            else
            {
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    btnDocumentos.Enabled = true;
                    btnVariosDoc.Enabled = true;
                    nuevoToolStripMenuItem.Enabled = false;
                    eliminarToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void btnVariosDoc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            if (tipo_Producto == 1)
            {
                #region Suministros
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 103)//Guia de Remision Suministros
                {
                    List<EGuiaRemisionCompras> lstGRMSelec = new List<EGuiaRemisionCompras>();
                    FrmListarGuiaRemisionSuministrosVarios frm = new FrmListarGuiaRemisionSuministrosVarios();
                    frm.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                    frm.flag_multiple = true;
                    frm.Carga();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnDocumentos.Tag = frm._Be.grcc_icod_grc;
                        //btnDocumentos.Text = frm._Be.grcc_vnumero_grc;
                        CodGR = Convert.ToInt32(btnDocumentos.Tag);
                        lstGRMSelec = frm.lstGRM;
                        ListarDetalleGRSVarios(lstGRMSelec);
                        nuevoToolStripMenuItem.Enabled = false;
                        eliminarToolStripMenuItem.Enabled = false;
                        GR = 1;
                    }
                }
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 120)//Recepcion Compra Suministros
                {
                    List<ERecepcionComprasSuministros> lstRCMSelec = new List<ERecepcionComprasSuministros>();
                    FrmListarRecepcionCompraSuministrosVarios frm = new FrmListarRecepcionCompraSuministrosVarios();
                    frm.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                    frm.flag_multiple = true;
                    frm.Carga();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnDocumentos.Tag = frm._Be.rcsc_icod_rcs;
                        //btnDocumentos.Text = frm._Be.rcmc_vnumero_rcm;
                        CodGR = Convert.ToInt32(btnDocumentos.Tag);
                        lstRCMSelec = frm.lstRCM;
                        ListarDetalleRCSVarios(lstRCMSelec);
                        nuevoToolStripMenuItem.Enabled = false;
                        eliminarToolStripMenuItem.Enabled = false;
                        GR = 1;
                    }
                }
                #endregion
            }
            else if (tipo_Producto == 2)
            {
                #region Mercaderia
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 122)//Guia de Remision Mercaderia
                {
                    List<EGuiaRemisionComprasMercaderia> lstGRMSelec = new List<EGuiaRemisionComprasMercaderia>();
                    FrmListarGuiaRemisionMercaderiaVarios frm = new FrmListarGuiaRemisionMercaderiaVarios();
                    frm.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                    frm.flag_multiple = true;
                    frm.Carga();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnDocumentos.Tag = frm._Be.grcc_icod_grc;
                        //btnDocumentos.Text = frm._Be.grcc_vnumero_grc;
                        CodGR = Convert.ToInt32(btnDocumentos.Tag);
                        lstGRMSelec = frm.lstGRM;
                        ListarDetalleGRVarios(lstGRMSelec);
                        nuevoToolStripMenuItem.Enabled = false;
                        eliminarToolStripMenuItem.Enabled = false;
                        GR = 1;
                    }
                }
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 123)//Recepcion Compra Mercaderia
                {
                    List<ERecepcionComprasMercaderia> lstRCMSelec = new List<ERecepcionComprasMercaderia>();
                    FrmListarRecepcionCompraMercaderiaVarios frm = new FrmListarRecepcionCompraMercaderiaVarios();
                    frm.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                    frm.flag_multiple = true;
                    frm.Carga();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnDocumentos.Tag = frm._Be.rcmc_icod_rcm;
                        //btnDocumentos.Text = frm._Be.rcmc_vnumero_rcm;
                        CodGR = Convert.ToInt32(btnDocumentos.Tag);
                        lstRCMSelec = frm.lstRCM;
                        ListarDetalleRCMVarios(lstRCMSelec);
                        nuevoToolStripMenuItem.Enabled = false;
                        eliminarToolStripMenuItem.Enabled = false;
                        GR = 1;
                    }
                }
                #endregion
            }
        }
        public void ListarDetalleGRSVarios(List<EGuiaRemisionCompras> lstGRSelec)
        {
            #region Mercaderia

            lstGRSelec.Where(r => r.flag_multiple).ToList().ForEach(x =>
            {
                List<EFacturaCompraDet> lstAcumulado = new List<EFacturaCompraDet>();
                List<EFacturaCompraDet> lstFD = new List<EFacturaCompraDet>();
                //lstDetalle = lstFD;
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 103)//Guia de Remision Mercaderia
                {
                    List<EGuiaRemisionComprasDetalle> lstGRDetalle = new List<EGuiaRemisionComprasDetalle>();
                    lstGRDetalle = new BCompras().listarGuiaRemisionComprasDetalle(x.grcc_icod_grc);

                    foreach (var _bee in lstGRDetalle)
                    {
                        EFacturaCompraDet BGRCOMPRAS = new EFacturaCompraDet();
                        BGRCOMPRAS.fcod_iitem = _bee.grcd_iid_detalle;
                        BGRCOMPRAS.prd_icod_producto = _bee.prdc_icod_producto;
                        BGRCOMPRAS.fcod_ncantidad = _bee.grcd_ncantidad;
                        BGRCOMPRAS.fcod_vdescripcion_item = _bee.DesProducto;
                        BGRCOMPRAS.strCodProducto = _bee.strCodProd;
                        BGRCOMPRAS.strDesUM = _bee.Unidad;
                        BGRCOMPRAS.fcod_icod_kardex = _bee.kardc_icod_correlativo;
                        BGRCOMPRAS.grcd_icod_detalle = _bee.grcd_icod_detalle;
                        lstDetalle.Add(BGRCOMPRAS);
                        lstDetalle.OrderBy(E => E.fcod_iitem).ToList();
                        //grdDetalle.DataSource = lstDetalle;
                    }

                }
                EDocumentosVarios DV = new EDocumentosVarios();
                DV.fcoc_icod_documento = x.grcc_icod_grc;
                lstDocumentosVarios.Add(DV);
            });
            grdDetalle.DataSource = lstDetalle;
            ////grdMercaderia.RefreshDataSource();
            #endregion
        }
        public void ListarDetalleRCSVarios(List<ERecepcionComprasSuministros> lstRCMSelec)
        {
            #region Mercaderia

            lstRCMSelec.Where(r => r.flag_multiple).ToList().ForEach(x =>
            {

                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 120)//Recepcion Compra Mercaderia
                {
                    List<ERecepcionComprasSuministrosDetalle> lstRCMDetalle = new List<ERecepcionComprasSuministrosDetalle>();
                    lstRCMDetalle = new BCompras().listarRecepcionComprasSuministrosDetalle(x.rcsc_icod_rcs);

                    foreach (var _bee in lstRCMDetalle)
                    {
                        EFacturaCompraDet BGRCOMPRAS = new EFacturaCompraDet();
                        BGRCOMPRAS.fcod_iitem = _bee.rcsd_iid_detalle;
                        BGRCOMPRAS.prd_icod_producto = _bee.prdc_icod_producto;
                        BGRCOMPRAS.fcod_ncantidad = _bee.rcsd_ncantidad;
                        BGRCOMPRAS.fcod_vdescripcion_item = _bee.DesProducto;
                        BGRCOMPRAS.strCodProducto = _bee.strCodProd;
                        BGRCOMPRAS.strDesUM = _bee.Unidad;
                        BGRCOMPRAS.fcod_icod_kardex = _bee.kardc_icod_correlativo;
                        BGRCOMPRAS.grcd_icod_detalle = _bee.rcsd_icod_detalle;
                        lstDetalle.Add(BGRCOMPRAS);
                        lstDetalle.OrderBy(E => E.fcod_iitem).ToList();
                        //grdDetalle.DataSource = lstDetalle;
                    }
                }
                EDocumentosVarios DV = new EDocumentosVarios();
                DV.fcoc_icod_documento = x.rcsc_icod_rcs;
                lstDocumentosVarios.Add(DV);
            });

            grdDetalle.DataSource = lstDetalle;
            #endregion
        }


        public void ListarDetalleGRVarios(List<EGuiaRemisionComprasMercaderia> lstGRSelec)
        {
            #region Mercaderia

            lstGRSelec.Where(r => r.flag_multiple).ToList().ForEach(x =>
            {
                List<EFacturaCompraDet> lstAcumulado = new List<EFacturaCompraDet>();
                List<EFacturaCompraDet> lstFD = new List<EFacturaCompraDet>();
                //lstDetalle = lstFD;
                if (Convert.ToInt32(lkpTipoDoc.EditValue) == 122)//Guia de Remision Mercaderia
                {
                    List<EGuiaRemisionComprasMercaderiaDetalle> lstGRDetalle = new List<EGuiaRemisionComprasMercaderiaDetalle>();
                    lstGRDetalle = new BCompras().listarGuiaRemisionComprasMercaderiaDetalle(x.grcc_icod_grc);

                    foreach (var _bee in lstGRDetalle)
                    {
                        foreach (var _DET in lstDetalle)
                        {
                            if (_DET.prd_icod_producto == _bee.prdc_icod_producto)
                            {
                                _DET.fcod_cant_talla1 = Convert.ToInt32(_DET.fcod_cant_talla1) + Convert.ToInt32(_bee.grcd_cant_talla1);
                                _DET.fcod_cant_talla2 = Convert.ToInt32(_DET.fcod_cant_talla2) + Convert.ToInt32(_bee.grcd_cant_talla2);
                                _DET.fcod_cant_talla3 = Convert.ToInt32(_DET.fcod_cant_talla3) + Convert.ToInt32(_bee.grcd_cant_talla3);
                                _DET.fcod_cant_talla4 = Convert.ToInt32(_DET.fcod_cant_talla4) + Convert.ToInt32(_bee.grcd_cant_talla4);
                                _DET.fcod_cant_talla5 = Convert.ToInt32(_DET.fcod_cant_talla5) + Convert.ToInt32(_bee.grcd_cant_talla5);
                                _DET.fcod_cant_talla6 = Convert.ToInt32(_DET.fcod_cant_talla6) + Convert.ToInt32(_bee.grcd_cant_talla6);
                                _DET.fcod_cant_talla7 = Convert.ToInt32(_DET.fcod_cant_talla7) + Convert.ToInt32(_bee.grcd_cant_talla7);
                                _DET.fcod_cant_talla8 = Convert.ToInt32(_DET.fcod_cant_talla8) + Convert.ToInt32(_bee.grcd_cant_talla8);
                                _DET.fcod_cant_talla9 = Convert.ToInt32(_DET.fcod_cant_talla9) + Convert.ToInt32(_bee.grcd_cant_talla9);
                                _DET.fcod_cant_talla10 = Convert.ToInt32(_DET.fcod_cant_talla10) + Convert.ToInt32(_bee.grcd_cant_talla10);

                                //_DET.fcod_ncantidad = Convert.ToInt32(_DET.fcod_ncantidad + _bee.grcd_ncantidad);
                                _DET.fcod_ncantidad = Convert.ToDecimal((_DET.fcod_cant_talla1 + _DET.fcod_cant_talla2 + _DET.fcod_cant_talla3 + _DET.fcod_cant_talla4 + _DET.fcod_cant_talla5 + _DET.fcod_cant_talla6 + _DET.fcod_cant_talla7 + _DET.fcod_cant_talla8 + _DET.fcod_cant_talla9 + _DET.fcod_cant_talla10));


                            }
                        }
                        if (lstDetalle.Count(ob => ob.prd_icod_producto == _bee.prdc_icod_producto) == 0)
                        {
                            EFacturaCompraDet BFCCOMPRAS = new EFacturaCompraDet();
                            BFCCOMPRAS.fcod_iitem = _bee.grcd_iid_detalle;
                            BFCCOMPRAS.prd_icod_producto = _bee.prdc_icod_producto;
                            //BFCCOMPRAS.fcod_ncantidad = _bee.grcd_ncantidad;
                            BFCCOMPRAS.fcod_vdescripcion_item = _bee.DesProducto;
                            BFCCOMPRAS.strCodProducto = _bee.strCodProd;
                            BFCCOMPRAS.strDesUM = _bee.Unidad;
                            BFCCOMPRAS.fcod_icod_kardex = _bee.kardc_icod_correlativo;
                            BFCCOMPRAS.grcd_icod_detalle = _bee.grcd_icod_detalle;

                            BFCCOMPRAS.fcod_rango_talla = _bee.grcd_rango_talla;
                            BFCCOMPRAS.fcod_talla1 = _bee.grcd_talla1;
                            BFCCOMPRAS.fcod_talla2 = _bee.grcd_talla2;
                            BFCCOMPRAS.fcod_talla3 = _bee.grcd_talla3;
                            BFCCOMPRAS.fcod_talla4 = _bee.grcd_talla4;
                            BFCCOMPRAS.fcod_talla5 = _bee.grcd_talla5;
                            BFCCOMPRAS.fcod_talla6 = _bee.grcd_talla6;
                            BFCCOMPRAS.fcod_talla7 = _bee.grcd_talla7;
                            BFCCOMPRAS.fcod_talla8 = _bee.grcd_talla8;
                            BFCCOMPRAS.fcod_talla9 = _bee.grcd_talla9;
                            BFCCOMPRAS.fcod_talla10 = _bee.grcd_talla10;

                            BFCCOMPRAS.fcod_cant_talla1 = _bee.grcd_cant_talla1;
                            BFCCOMPRAS.fcod_cant_talla2 = _bee.grcd_cant_talla2;
                            BFCCOMPRAS.fcod_cant_talla3 = _bee.grcd_cant_talla3;
                            BFCCOMPRAS.fcod_cant_talla4 = _bee.grcd_cant_talla4;
                            BFCCOMPRAS.fcod_cant_talla5 = _bee.grcd_cant_talla5;
                            BFCCOMPRAS.fcod_cant_talla6 = _bee.grcd_cant_talla6;
                            BFCCOMPRAS.fcod_cant_talla7 = _bee.grcd_cant_talla7;
                            BFCCOMPRAS.fcod_cant_talla8 = _bee.grcd_cant_talla8;
                            BFCCOMPRAS.fcod_cant_talla9 = _bee.grcd_cant_talla9;
                            BFCCOMPRAS.fcod_cant_talla10 = _bee.grcd_cant_talla10;

                            BFCCOMPRAS.fcod_iid_kardex1 = _bee.grcd_iid_kardex1;
                            BFCCOMPRAS.fcod_iid_kardex2 = _bee.grcd_iid_kardex2;
                            BFCCOMPRAS.fcod_iid_kardex3 = _bee.grcd_iid_kardex3;
                            BFCCOMPRAS.fcod_iid_kardex4 = _bee.grcd_iid_kardex4;
                            BFCCOMPRAS.fcod_iid_kardex5 = _bee.grcd_iid_kardex5;
                            BFCCOMPRAS.fcod_iid_kardex6 = _bee.grcd_iid_kardex6;
                            BFCCOMPRAS.fcod_iid_kardex7 = _bee.grcd_iid_kardex7;
                            BFCCOMPRAS.fcod_iid_kardex8 = _bee.grcd_iid_kardex8;
                            BFCCOMPRAS.fcod_iid_kardex9 = _bee.grcd_iid_kardex9;
                            BFCCOMPRAS.fcod_iid_kardex10 = _bee.grcd_iid_kardex10;

                            lstDetalle.Add(BFCCOMPRAS);
                            lstDetalle.OrderBy(E => E.fcod_iitem).ToList();

                            BFCCOMPRAS.fcod_ncantidad = Convert.ToDecimal((BFCCOMPRAS.fcod_cant_talla1 + BFCCOMPRAS.fcod_cant_talla2 + BFCCOMPRAS.fcod_cant_talla3 + BFCCOMPRAS.fcod_cant_talla4 + BFCCOMPRAS.fcod_cant_talla5 + BFCCOMPRAS.fcod_cant_talla6 + BFCCOMPRAS.fcod_cant_talla7 + BFCCOMPRAS.fcod_cant_talla8 + BFCCOMPRAS.fcod_cant_talla9 + BFCCOMPRAS.fcod_cant_talla10));

                            //grdMercaderia.DataSource = lstDetalle;
                        }
                    }

                }
                EDocumentosVarios DV = new EDocumentosVarios();
                DV.fcoc_icod_documento = x.grcc_icod_grc;
                lstDocumentosVarios.Add(DV);
            });
            grdMercaderia.DataSource = lstDetalle;
            ////grdMercaderia.RefreshDataSource();
            #endregion
        }
        public void ListarDetalleRCMVarios(List<ERecepcionComprasMercaderia> lstRCMSelec)
        {
            #region Mercaderia

            lstRCMSelec.Where(r => r.flag_multiple).ToList().ForEach(x =>
             {

                 if (Convert.ToInt32(lkpTipoDoc.EditValue) == 123)//Recepcion Compra Mercaderia
                 {
                     List<ERecepcionComprasMercaderiaDetalle> lstRCMDetalle = new List<ERecepcionComprasMercaderiaDetalle>();
                     lstRCMDetalle = new BCompras().listarRecepcionComprasMercaderiaDetalle(x.rcmc_icod_rcm);

                     foreach (var _bee in lstRCMDetalle)
                     {
                         foreach (var _DET in lstDetalle)
                         {
                             if (_DET.prd_icod_producto == _bee.prdc_icod_producto)
                             {
                                 _DET.fcod_cant_talla1 = Convert.ToInt32(_DET.fcod_cant_talla1 + _bee.rcmd_cant_talla1);
                                 _DET.fcod_cant_talla2 = Convert.ToInt32(_DET.fcod_cant_talla2 + _bee.rcmd_cant_talla2);
                                 _DET.fcod_cant_talla3 = Convert.ToInt32(_DET.fcod_cant_talla3 + _bee.rcmd_cant_talla3);
                                 _DET.fcod_cant_talla4 = Convert.ToInt32(_DET.fcod_cant_talla4 + _bee.rcmd_cant_talla4);
                                 _DET.fcod_cant_talla5 = Convert.ToInt32(_DET.fcod_cant_talla5 + _bee.rcmd_cant_talla5);
                                 _DET.fcod_cant_talla6 = Convert.ToInt32(_DET.fcod_cant_talla6 + _bee.rcmd_cant_talla6);
                                 _DET.fcod_cant_talla7 = Convert.ToInt32(_DET.fcod_cant_talla7 + _bee.rcmd_cant_talla7);
                                 _DET.fcod_cant_talla8 = Convert.ToInt32(_DET.fcod_cant_talla8 + _bee.rcmd_cant_talla8);
                                 _DET.fcod_cant_talla9 = Convert.ToInt32(_DET.fcod_cant_talla9 + _bee.rcmd_cant_talla9);
                                 _DET.fcod_cant_talla10 = Convert.ToInt32(_DET.fcod_cant_talla10 + _bee.rcmd_cant_talla10);

                                 _DET.fcod_ncantidad = Convert.ToInt32(_DET.fcod_ncantidad + _bee.rcmd_ncantidad);
                             }
                         }
                         if (lstDetalle.Count(ob => ob.prd_icod_producto == _bee.prdc_icod_producto) == 0)
                         {
                             EFacturaCompraDet BFCCOMPRAS = new EFacturaCompraDet();
                             BFCCOMPRAS.fcod_iitem = _bee.rcmd_iid_detalle;
                             BFCCOMPRAS.prd_icod_producto = _bee.prdc_icod_producto;
                             BFCCOMPRAS.fcod_ncantidad = _bee.rcmd_ncantidad;
                             BFCCOMPRAS.fcod_vdescripcion_item = _bee.DesProducto;
                             BFCCOMPRAS.strCodProducto = _bee.strCodProd;
                             BFCCOMPRAS.strDesUM = _bee.Unidad;
                             BFCCOMPRAS.fcod_icod_kardex = _bee.kardc_icod_correlativo;
                             BFCCOMPRAS.grcd_icod_detalle = _bee.rcmd_icod_detalle;

                             BFCCOMPRAS.fcod_rango_talla = _bee.rcmd_rango_talla;
                             BFCCOMPRAS.fcod_talla1 = _bee.rcmd_talla1;
                             BFCCOMPRAS.fcod_talla2 = _bee.rcmd_talla2;
                             BFCCOMPRAS.fcod_talla3 = _bee.rcmd_talla3;
                             BFCCOMPRAS.fcod_talla4 = _bee.rcmd_talla4;
                             BFCCOMPRAS.fcod_talla5 = _bee.rcmd_talla5;
                             BFCCOMPRAS.fcod_talla6 = _bee.rcmd_talla6;
                             BFCCOMPRAS.fcod_talla7 = _bee.rcmd_talla7;
                             BFCCOMPRAS.fcod_talla8 = _bee.rcmd_talla8;
                             BFCCOMPRAS.fcod_talla9 = _bee.rcmd_talla9;
                             BFCCOMPRAS.fcod_talla10 = _bee.rcmd_talla10;

                             BFCCOMPRAS.fcod_cant_talla1 = _bee.rcmd_cant_talla1;
                             BFCCOMPRAS.fcod_cant_talla2 = _bee.rcmd_cant_talla2;
                             BFCCOMPRAS.fcod_cant_talla3 = _bee.rcmd_cant_talla3;
                             BFCCOMPRAS.fcod_cant_talla4 = _bee.rcmd_cant_talla4;
                             BFCCOMPRAS.fcod_cant_talla5 = _bee.rcmd_cant_talla5;
                             BFCCOMPRAS.fcod_cant_talla6 = _bee.rcmd_cant_talla6;
                             BFCCOMPRAS.fcod_cant_talla7 = _bee.rcmd_cant_talla7;
                             BFCCOMPRAS.fcod_cant_talla8 = _bee.rcmd_cant_talla8;
                             BFCCOMPRAS.fcod_cant_talla9 = _bee.rcmd_cant_talla9;
                             BFCCOMPRAS.fcod_cant_talla10 = _bee.rcmd_cant_talla10;

                             BFCCOMPRAS.fcod_iid_kardex1 = _bee.rcmd_iid_kardex1;
                             BFCCOMPRAS.fcod_iid_kardex2 = _bee.rcmd_iid_kardex2;
                             BFCCOMPRAS.fcod_iid_kardex3 = _bee.rcmd_iid_kardex3;
                             BFCCOMPRAS.fcod_iid_kardex4 = _bee.rcmd_iid_kardex4;
                             BFCCOMPRAS.fcod_iid_kardex5 = _bee.rcmd_iid_kardex5;
                             BFCCOMPRAS.fcod_iid_kardex6 = _bee.rcmd_iid_kardex6;
                             BFCCOMPRAS.fcod_iid_kardex7 = _bee.rcmd_iid_kardex7;
                             BFCCOMPRAS.fcod_iid_kardex8 = _bee.rcmd_iid_kardex8;
                             BFCCOMPRAS.fcod_iid_kardex9 = _bee.rcmd_iid_kardex9;
                             BFCCOMPRAS.fcod_iid_kardex10 = _bee.rcmd_iid_kardex10;

                             lstDetalle.Add(BFCCOMPRAS);
                             lstDetalle.OrderBy(E => E.fcod_iitem).ToList();

                         }

                     }
                 }
                 EDocumentosVarios DV = new EDocumentosVarios();
                 DV.fcoc_icod_documento = x.rcmc_icod_rcm;
                 lstDocumentosVarios.Add(DV);
             });

            grdMercaderia.DataSource = lstDetalle;
            #endregion
        }

        private void txtAdelanto_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        private void txtAdelanto_EditValueChanging(object sender, EventArgs e)
        {
            setTotal();
        }
    }
}