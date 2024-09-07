using DevExpress.XtraEditors;
using SGE.BusinessLogic;
//using SGE.WindowForms.Ventas.Operaciones;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Operaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteOrdenCompraConsultar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteOrdenCompraConsultar));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EOrdenCompra Obe = new EOrdenCompra();
        List<EOrdenCompra> lstPresupuestoplantilla = new List<EOrdenCompra>();
        List<EOrdenCompra> lstPresupuestoPlanillaEliminados = new List<EOrdenCompra>();
        List<EArchivos> lstArchivos = new List<EArchivos>();
        public int ococ_icod_orden_compra = 0;

        public frmManteOrdenCompraConsultar()
        {
            InitializeComponent();
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
            txtSerie.Properties.ReadOnly = !Enabled;
            txtNumero.Properties.ReadOnly = !Enabled;
            btnProveedor.Enabled = !Enabled;
            dtmFechafactura.Enabled = !Enabled;
            lkpformaDePago.Enabled = !Enabled;
            lkpMoneda.Enabled = !Enabled;


        }
        public void setValues()
        {
            txtSerie.Text = Obe.ococ_ianio.ToString();
            txtNumero.Text = Obe.ococ_numero_orden_compra.Substring(5);
            dtmFechafactura.EditValue = Obe.ococ_sfecha_orden_compra;
            lkpSituacion.EditValue = Convert.ToInt32(Obe.tablc_iid_situacion_oc);
            lkpformaDePago.EditValue = Obe.tablc_iforma_pago;
            lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
            ckeincluirIgv.Checked = Obe.facc_bIncluido_igv;
            txtporIGV.Text = Obe.ococ_npor_imp_igv.ToString();

            btnProveedor.Tag = Obe.proc_icod_proveedor;
            btnProveedor.Text = Obe.prvc_vcod_proveedor;
            txtDesProveedor.Text = Obe.proc_vnombrecompleto;
            txtdireccion.Text = Obe.proc_vdireccion;
            txtRUC.Text = Obe.proc_vruc;

            txtSubTotal.Text = Obe.orpc_nmonto_sub_Total.ToString();
            txtporcDesc.Text = Obe.orpc_npor_descuento.ToString();
            txtIGV.Text = Obe.ococ_nmonto_imp.ToString();
            txtContacto.Text = Obe.ococ_vcontacto;
            txtRecepcion.Text = Obe.ococ_vreferencia;
            txtGarantia.Text = Obe.ococ_vciudad;
            txtLugarE.Text = Obe.ococ_vlugar_entrega;
            txttelefono.Text = Obe.ococ_vciudad;
            txtTotalOC.Text = Obe.ococ_nmonto_total.ToString();
            txtForm_Pago.Text = Obe.ococ_vforma_pago;
            txtCotizacion.Text = Obe.ococ_vcotizacion;

        }
        public void setDobleClick()
        {
            txtSerie.Properties.ReadOnly = true;
            txtNumero.Properties.ReadOnly = true;
            dtmFechafactura.Enabled = false;
            btnProveedor.Enabled = false;
            lkpformaDePago.Enabled = false;
            lkpMoneda.Enabled = false;
            ckeincluirIgv.Enabled = false;
            btnGuardar.Enabled = false;
            mnuPresupuestoNacional.Enabled = false;
            txtReferencia.Enabled = false;
            txtRecepcion.Enabled = false;
            txtCotizacion.Enabled = false;
            txtGarantia.Enabled = false;
            txtForm_Pago.Enabled = false;
            txtGarantia.Enabled = false;
            lkpMotivo.Enabled = false;
            bteProyecto.Enabled = false;
            txtLugarE.Enabled = false;
            txtNota.Enabled = false;
            txtContacto.Enabled = false;
            txttelefono.Enabled = false;
            txtporcDesc.Enabled = false;
            Cargar();


        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            Cargar();
            GetOCL();

        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;

        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            Cargar();
        }
        public void Cargar()
        {
            lstPresupuestoplantilla = new BCompras().ListarOrdenCompraDetalle(ococ_icod_orden_compra);
            grcfactura.DataSource = lstPresupuestoplantilla;
            grcfactura.RefreshDataSource();
            grvfactura.ExpandAllGroups();
            if (ckeincluirIgv.Checked == false)
            {
                this.Totalizar_sin_incluir_Igv();
            }
            else
            {
                this.Totalizar_Incluido_IGV();
            }
        }
        private void frmManteCiaSeguros_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(66), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpformaDePago, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaFormaPagoA), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMotivo, new BGeneral().listarTablaRegistro(67), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

        }
        private void GetOCL()
        {
            string correlativo = new BAdministracionSistema().ObtenerCorrelativoOCL(Convert.ToInt32(txtSerie.Text));
            txtNumero.Text = correlativo;

        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {

                if (Convert.ToInt32(btnProveedor.Tag) == 0)
                {
                    oBase = btnProveedor;
                    throw new ArgumentException("Ingresar Proveedor");
                }
                if (Convert.ToInt32(txtSerie.Text) == 0)
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingresar Serie de la Orden de Compra");
                }
                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingresar correlativo de la Orden de Compra");
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    List<EOrdenCompra> Lstver = new BVentas().getOCLCabVerificarNumero(String.Format("{0}{1}", txtSerie.Text, txtNumero.Text), Parametros.intEjercicio);
                    if (Lstver.Count > 0)
                    {

                        oBase = txtNumero;
                        XtraMessageBox.Show("El Número " + String.Format("{0}{1}", txtSerie.Text, txtNumero.Text) + " N° O/C: Ya Existia, Se Agrego Nuevo");
                        GetOCL();
                    }
                }
                Obe.ococ_icod_orden_compra = ococ_icod_orden_compra;
                Obe.ococ_ianio = Convert.ToInt32(txtSerie.Text);
                Obe.ococ_icod_orden_compra = ococ_icod_orden_compra;
                Obe.ococ_numero_orden_compra = txtSerie.Text + txtNumero.Text;
                Obe.ococ_sfecha_orden_compra = Convert.ToDateTime(dtmFechafactura.EditValue);
                Obe.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
                Obe.prvc_vcod_proveedor = btnProveedor.Text;
                Obe.proc_vnombrecompleto = txtDesProveedor.Text;
                Obe.tablc_iforma_pago = Convert.ToInt32(lkpformaDePago.EditValue);
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.tablc_iid_situacion_oc = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.ococ_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.facc_bIncluido_igv = ckeincluirIgv.Checked;
                Obe.ococ_nmonto_neto = Convert.ToDecimal(txtTotalNeto.Text);
                Obe.ococ_npor_imp_igv = Convert.ToDecimal(Parametros.strPorcIGV);
                Obe.ococ_nmonto_imp = Convert.ToDecimal(txtIGV.Text);
                Obe.ococ_nmonto_total = Convert.ToDecimal(txtTotalOC.Text);
                Obe.orpc_nmonto_sub_Total = Convert.ToDecimal(txtSubTotal.Text);
                Obe.orpc_npor_descuento = Convert.ToDecimal(txtporcDesc.Text);
                Obe.orpc_nmonto_descuento = Convert.ToDecimal(txtDescuento.Text);
                Obe.ococ_npor_imp_igv = Convert.ToDecimal(txtporIGV.Text);
                Obe.ococ_vcontacto = txtContacto.Text;
                Obe.ococ_vreferencia = txtRecepcion.Text;
                Obe.ococ_vcomprobante = txtGarantia.Text;
                Obe.ococ_vlugar_entrega = txtLugarE.Text;
                Obe.ococ_vciudad = txttelefono.Text;


                Obe.ococ_vforma_pago = txtForm_Pago.Text;
                Obe.ococ_vcotizacion = txtCotizacion.Text;




                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.ococ_icod_orden_compra = new BCompras().InsertarOrdenCompra(Obe, lstPresupuestoplantilla, lstArchivos);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCompras().ActualizarOrdenCompra(Obe, lstPresupuestoplantilla, lstPresupuestoPlanillaEliminados, lstArchivos);
                }
            }

            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSerie.Focus();
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Obe.ococ_icod_orden_compra);
                    this.Close();
                }
            }
        }
        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void ListarProveedor()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                btnProveedor.Tag = Proveedor._Be.iid_icod_proveedor;
                txtDesProveedor.Text = Proveedor._Be.vnombrecompleto;
                btnProveedor.Text = Proveedor._Be.vcod_proveedor;
                txtdireccion.Text = Proveedor._Be.vdireccion;
                txtRUC.Text = Proveedor._Be.vruc;
                txttelefono.Text = Proveedor._Be.vtelefono;
                txtContacto.Text = Proveedor._Be.vdni;
            }

        }
        private void NuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmManteOrdenCompraDetalleDetalle frmdetalle = new frmManteOrdenCompraDetalleDetalle();
                frmdetalle.Text = "Detalle de la Orden de Compra N°: " + txtSerie.Text + "-" + txtNumero.Text;
                frmdetalle.IntMoneda = Convert.ToInt32(lkpMoneda.EditValue);
                frmdetalle.txtItem.Text = (lstPresupuestoplantilla.Count + 1).ToString();
                frmdetalle.btnModificar.Enabled = false;
                frmdetalle.IntMoneda = Convert.ToInt32(lkpMoneda.EditValue);
                frmdetalle.lstArchivos = lstArchivos;
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    EOrdenCompra _be = new EOrdenCompra();
                    _be.ocod_icod_detalle_oc = frmdetalle.oBE.ocod_icod_detalle_oc;
                    _be.ococ_icod_orden_compra = frmdetalle.oBE.ococ_icod_orden_compra;
                    _be.ocod_iitem = frmdetalle.oBE.ocod_iitem;
                    _be.prdc_icod_producto = frmdetalle.oBE.prdc_icod_producto;
                    _be.prdc_vcodigo_fabricante = frmdetalle.oBE.prdc_vcodigo_fabricante;
                    _be.ocod_dfecha_entrega = frmdetalle.oBE.ocod_dfecha_entrega;
                    _be.ocod_vdescripcion = frmdetalle.oBE.ocod_vdescripcion;
                    _be.ocod_vcaracteristicas = frmdetalle.oBE.ocod_vcaracteristicas;
                    _be.ocod_ncantidad = frmdetalle.oBE.ocod_ncantidad;
                    _be.ocod_ncantidad_saldo = frmdetalle.oBE.ocod_ncantidad - frmdetalle.oBE.ocod_ncantidad_recibida;
                    _be.ocod_ncunitario = frmdetalle.oBE.ocod_ncunitario;
                    _be.ocod_nmonto_total = frmdetalle.oBE.ocod_nmonto_total;
                    _be.ocod_ndescuento_item = frmdetalle.oBE.ocod_ndescuento_item;
                    _be.kardc_iid_correlativo = frmdetalle.oBE.kardc_iid_correlativo;
                    _be.intUsuario = frmdetalle.oBE.intUsuario;
                    _be.strPc = frmdetalle.oBE.strPc;
                    _be.operacion = 1;
                    _be.strCodigoProducto = frmdetalle.oBE.strCodigoProducto;

                    _be.orpdi_nprecio_soles = frmdetalle.oBE.orpdi_nprecio_soles;
                    _be.orpdi_nprecio_dolares = frmdetalle.oBE.orpdi_nprecio_dolares;

                    _be.strDescProducto = frmdetalle.oBE.strDescProducto;
                    _be.famic_vabreviatura = frmdetalle.oBE.famic_vabreviatura;
                    _be.famid_vabreviatura = frmdetalle.oBE.famid_vabreviatura;
                    _be.strMedida = frmdetalle.oBE.strMedida;
                    _be.ocod_flag_estado = frmdetalle.oBE.ocod_flag_estado;
                    lstArchivos = frmdetalle.lstArchivos;
                    if (lstArchivos.Count > 0)
                    {
                        _be.ocod_vdireccion_documento = lstArchivos[0].arch_vruta;
                    }
                    lstPresupuestoplantilla.Add(_be);
                    grcfactura.RefreshDataSource();

                    if (ckeincluirIgv.Checked == false)
                    {
                        this.Totalizar_sin_incluir_Igv();
                    }
                    else
                    {
                        this.Totalizar_Incluido_IGV();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EOrdenCompra _obeAU = (EOrdenCompra)grvfactura.GetRow(grvfactura.FocusedRowHandle);
            if (lstPresupuestoplantilla.Count > 0)
            {
                if (_obeAU.prdc_icod_producto != null)
                {
                    frmManteOrdenCompraDetalleDetalle frmdetalle = new frmManteOrdenCompraDetalleDetalle();
                    frmdetalle.Text = "Detalle de la Orden de Compra N°: " + txtSerie.Text + "-" + txtNumero.Text;
                    frmdetalle.ocod_icod_detalle_oc = _obeAU.ocod_icod_detalle_oc;
                    frmdetalle.oBE = _obeAU;
                    frmdetalle.setValues();
                    frmdetalle.SetModify();
                    frmdetalle.btnAgregar.Enabled = false;
                    frmdetalle.btnProducto.Enabled = false;
                    frmdetalle.IntMoneda = Convert.ToInt32(lkpMoneda.EditValue);
                    frmdetalle.lstArchivos = lstArchivos;
                    if (frmdetalle.ShowDialog() == DialogResult.OK)
                    {
                        _obeAU.ocod_iitem = frmdetalle.oBE.ocod_iitem;
                        _obeAU.ocod_ncantidad = frmdetalle.oBE.ocod_ncantidad;
                        _obeAU.ocod_ncantidad_saldo = frmdetalle.oBE.ocod_ncantidad - frmdetalle.oBE.ocod_ncantidad_recibida;
                        _obeAU.ocod_ncunitario = frmdetalle.oBE.ocod_ncunitario;
                        _obeAU.ocod_nmonto_total = frmdetalle.oBE.ocod_nmonto_total;
                        _obeAU.ocod_ndescuento_item = frmdetalle.oBE.ocod_ndescuento_item;
                        _obeAU.ocod_vcaracteristicas = frmdetalle.oBE.ocod_vcaracteristicas;
                        _obeAU.ocod_dfecha_entrega = frmdetalle.oBE.ocod_dfecha_entrega;
                        lstArchivos = frmdetalle.lstArchivos;
                        if (lstArchivos.Count > 0)
                        {
                            _obeAU.ocod_vdireccion_documento = lstArchivos[0].arch_vruta;
                        }
                        if (_obeAU.ocod_icod_detalle_oc == 0)
                            _obeAU.operacion = 1;
                        else
                        {
                            if (_obeAU.operacion != 1)
                            {
                                _obeAU.operacion = 2;
                            }
                        }
                        grcfactura.RefreshDataSource();
                        if (ckeincluirIgv.Checked == false)
                        {
                            this.Totalizar_sin_incluir_Igv();
                        }
                        else
                        {
                            this.Totalizar_Incluido_IGV();
                        }
                    }
                }
            }
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void Delete()
        {
            EOrdenCompra obj = (EOrdenCompra)grvfactura.GetRow(grvfactura.FocusedRowHandle);
            if (obj.operacion == 1)
            {
                lstPresupuestoplantilla.Remove(obj);
                grvfactura.RefreshData();
                grvfactura.MovePrev();
            }
            else
            {

                if (lstPresupuestoplantilla.Count != 1)
                {
                    obj.operacion = 3;
                    lstPresupuestoPlanillaEliminados.Add(obj);
                    lstPresupuestoplantilla.Remove(obj);
                    grvfactura.RefreshData();
                    grvfactura.MovePrev();
                }
                else
                {
                    XtraMessageBox.Show("La factura debe tener al menos un Item", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (ckeincluirIgv.Checked == false)
            {
                this.Totalizar_sin_incluir_Igv();
            }
            else
            {
                this.Totalizar_Incluido_IGV();
            }

        }

        private void txtcantidad_EditValueChanged(object sender, EventArgs e)
        {

        }
        //private void Totalizar()
        //{
        //    decimal dec_subTotal = 0;
        //    decimal dec_Descuento = 0;
        //    dec_subTotal = Convertir.RedondearNumero((lstPresupuestoplantilla.Sum(ob => ob.ocod_nmonto_total)));
        //    dec_Descuento = Convertir.RedondearNumero((dec_subTotal * Convert.ToDecimal(txtporcDesc.Text)) / 100);
        //    txtSubTotal.Text = Convertir.RedondearNumero((lstPresupuestoplantilla.Sum(ob => ob.ocod_nmonto_total))).ToString();
        //    txtDescuento.Text = dec_Descuento.ToString();
        //    txtTotalNeto.Text = Convertir.RedondearNumero((dec_subTotal - dec_Descuento)).ToString();
        //    txtIGV.Text = Convertir.RedondearNumero(((dec_subTotal - dec_Descuento) * (Parametros.decimalIGV)) / 100).ToString();
        //    txtTotalOC.Text = Convertir.RedondearNumero(((((dec_subTotal - dec_Descuento) * (Parametros.decimalIGV)) / 100) + (dec_subTotal - dec_Descuento))).ToString();

        //}
        private void Totalizar_sin_incluir_Igv()
        {
            decimal dec_Descuento = 0;
            txtSubTotal.Text = ((lstPresupuestoplantilla.Sum(ob => ob.ocod_nmonto_total))).ToString();
            txtDescuento.Text = Convertir.RedondearNumero((Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(txtporcDesc.Text)) / 100).ToString();
            txtTotalNeto.Text = Convertir.RedondearNumero((Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDescuento.Text))).ToString();

            if (Convert.ToDecimal(txtporIGV.Text) == 0)
            {
                txtIGV.Text = "0.00";
            }
            else
            {

                txtIGV.Text = Math.Round(Convert.ToDecimal((Convert.ToDecimal(txtTotalNeto.Text) * Convert.ToDecimal(txtporIGV.Text)) / 100), 2, MidpointRounding.ToEven).ToString();
            }

            txtTotalOC.Text = (Convert.ToDecimal(txtSubTotal.Text) + Convert.ToDecimal(txtIGV.Text)).ToString();
        }
        public void Totalizar_Incluido_IGV()
        {
            decimal dec_Total = 0;
            decimal dec_Igv = 0;

            dec_Total = ((lstPresupuestoplantilla.Sum(ob => ob.ocod_nmonto_total)));
            txtSubTotal.Text = Convertir.RedondearNumero((((Math.Round(dec_Total, 2, MidpointRounding.ToEven)) / (100 + Convert.ToDecimal(txtporIGV.Text))) * 100)).ToString();

            txtDescuento.Text = Convertir.RedondearNumero((Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(txtporcDesc.Text)) / 100).ToString();
            txtTotalNeto.Text = Convertir.RedondearNumero((Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDescuento.Text))).ToString();


            txtTotalOC.Text = (((Math.Round(Convert.ToDecimal(txtTotalNeto.Text), 2, MidpointRounding.ToEven)) * (100 + Convert.ToDecimal(txtporIGV.Text))) / 100).ToString();

            if (Convert.ToDecimal(txtporIGV.Text) == 0)
            {
                txtIGV.Text = "0.00";
            }
            else
            {
                txtIGV.Text = (Convert.ToDecimal(txtTotalOC.Text) - Convert.ToDecimal(txtTotalNeto.Text)).ToString();
            }

        }

        //private void Totalizar_sin_incluir_Igv()
        //{
        //    txtSubTotal.Text = ((lstPresupuestoplantilla.Sum(ob => ob.ocod_nmonto_total))).ToString();
        //    txtIGV.Text = Math.Round(Convert.ToDecimal((lstPresupuestoplantilla.Sum(ob => ob.ocod_nmonto_total) * Parametros.decimalIGV) / 100), 2, MidpointRounding.ToEven).ToString();
        //    txtTotalOC.Text = (Convert.ToDecimal(txtSubTotal.Text) + Convert.ToDecimal(txtIGV.Text)).ToString();
        //}
        //public void Totalizar_Incluido_IGV()
        //{
        //    txtTotalOC.Text = ((lstPresupuestoplantilla.Sum(ob => ob.ocod_nmonto_total))).ToString();
        //    txtIGV.Text = ((Math.Round(Convert.ToDecimal(lstPresupuestoplantilla.Sum(ob => ob.ocod_nmonto_total)), 2, MidpointRounding.ToEven)) / (100 + Convert.ToDecimal(Parametros.decimalIGV))).ToString();
        //    txtSubTotal.Text = (Convert.ToDecimal(txtTotalOC.Text) - Convert.ToDecimal(txtIGV.Text)).ToString();
        //}   
        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtNumero.Text = (new BAdministracionSistema()).ObtenerCorrelativoDocumento(txtSerie.Text, Parametros.intTipoDocumentoOC);
            }
        }
        private void ckeincluirIgv_CheckedChanged(object sender, EventArgs e)
        {

            if (ckeincluirIgv.Checked == false)
            {
                this.Totalizar_sin_incluir_Igv();
            }
            else
            {
                this.Totalizar_Incluido_IGV();
            }
        }
        private void btnProveedor_EditValueChanged(object sender, EventArgs e)
        {
            List<EProveedor> lstProveedor = new List<EProveedor>();
            lstProveedor = new BCompras().ListarProveedor();

            var lstAux = lstProveedor.Where(x => x.vcod_proveedor == btnProveedor.Text.Trim()).ToList();
            if (lstAux.Count > 0)
            {
                btnProveedor.Tag = lstAux[0].iid_icod_proveedor;
                txtDesProveedor.Text = lstAux[0].vnombrecompleto;
                btnProveedor.Text = lstAux[0].vcod_proveedor;
                txtdireccion.Text = lstAux[0].vdireccion;
                txtRUC.Text = lstAux[0].vruc;
                txttelefono.Text = lstAux[0].vtelefono;
                txtContacto.Text = lstAux[0].vdni;

            }
            else
            {
                btnProveedor.Tag = 0;
                txtDesProveedor.Text = "";
                btnProveedor.Text = "";
                txtdireccion.Text = "";
                txtRUC.Text = "";
                txttelefono.Text = "";
                txtContacto.Text = "";

            }
        }

        private void txtporcDesc_EditValueChanged(object sender, EventArgs e)
        {
            if (ckeincluirIgv.Checked == false)
            {
                this.Totalizar_sin_incluir_Igv();
            }
            else
            {
                this.Totalizar_Incluido_IGV();
            }
        }

        private void lkpMoneda_EditValueChanged(object sender, EventArgs e)
        {
            this.recalculoTotal();
        }
        private void recalculoTotal()
        {
            recalcularByMoneda(Convert.ToInt32(lkpMoneda.EditValue));
            if (ckeincluirIgv.Checked == false)
            {
                this.Totalizar_sin_incluir_Igv();
            }
            else
            {
                this.Totalizar_Incluido_IGV();
            }
        }
        private void recalcularByMoneda(int intTipoMoneda)
        {

            if (Status == BSMaintenanceStatus.CreateNew)
            {
                foreach (var OBE in lstPresupuestoplantilla)
                {
                    decimal orpdi_nprecio_soles = 0;
                    decimal orpdi_nprecio_dolares = 0;

                    orpdi_nprecio_soles = OBE.orpdi_nprecio_soles;
                    orpdi_nprecio_dolares = OBE.orpdi_nprecio_dolares;

                    if (intTipoMoneda == Parametros.intSoles)
                    {
                        OBE.ocod_ncunitario = orpdi_nprecio_soles;
                        OBE.ocod_nmonto_total = orpdi_nprecio_soles * OBE.ocod_ncantidad;
                    }
                    else
                    {
                        OBE.ocod_ncunitario = orpdi_nprecio_dolares;
                        OBE.ocod_nmonto_total = orpdi_nprecio_dolares * OBE.ocod_ncantidad;
                    }

                }
                grvfactura.RefreshData();
            }

        }
        private void txtporIGV_EditValueChanged(object sender, EventArgs e)
        {
            if (ckeincluirIgv.Checked == false)
            {
                this.Totalizar_sin_incluir_Igv();
            }
            else
            {
                this.Totalizar_Incluido_IGV();
            }
        }

        private void btnListaPedido_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarListaPrecio();
        }
        private void listarListaPrecio()
        {
            List<EPedidoProvDet> LstPedido = new List<EPedidoProvDet>();
            FrmListarPedidoProveedor frm = new FrmListarPedidoProveedor();
            frm.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnProveedor.Tag = frm._Be.proc_icod_proveedor;
                btnProveedor.Text = frm._Be.proc_vcod_proveedor;
                txtDesProveedor.Text = frm._Be.proc_vnombrecompleto;
                txtContacto.Text = frm._Be.proc_vruc;
                txtdireccion.Text = frm._Be.proc_vdireccion;
                txttelefono.Text = frm._Be.proc_vtelefono;
                txtContacto.Text = frm._Be.proc_vdni;
                LstPedido = new BCompras().listarPedidoCompraDet(Convert.ToInt32(frm._Be.lpedi_icod_proveedor), Parametros.intEjercicio);
                foreach (var _BEE in LstPedido)
                {
                    EOrdenCompra _e = new EOrdenCompra();
                    if (lstPresupuestoplantilla.Count > 0)
                        _e.ocod_iitem = (lstPresupuestoplantilla.Max(o => o.ocod_iitem) + 1);
                    else
                        _e.ocod_iitem = 1;
                    _e.ococ_icod_orden_compra = 0;
                    _e.prdc_icod_producto = _BEE.prdc_icod_producto;
                    _e.strDescProducto = _BEE.prdc_vdescripcion_larga;
                    _e.strCodigoProducto = _BEE.prdc_vcode_producto;
                    _e.ocod_ncantidad = _BEE.lpedid_nCant_pedido;
                    _e.ocod_ncunitario = _BEE.lpedid_nprecio_neto;
                    _e.ocod_nmonto_total = _BEE.lpedid_nCant_pedido * _BEE.lpedid_nprecio_neto;
                    _e.ocod_flag_estado = true;
                    _e.operacion = 1;
                    _e.famic_vabreviatura = _BEE.strCategoria;
                    _e.famid_vabreviatura = _BEE.strSubCategoriaUno;
                    _e.strMedida = _BEE.StrUnidadMedida;
                    lstPresupuestoplantilla.Add(_e);
                }
                grcfactura.DataSource = lstPresupuestoplantilla;
                grcfactura.RefreshDataSource();
                if (ckeincluirIgv.Checked == false)
                {
                    this.Totalizar_sin_incluir_Igv();
                }
                else
                {
                    this.Totalizar_Incluido_IGV();
                }
            }
        }

        private void btnProveedor_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.ListarProveedor();
        }
        private void bteProyecto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarProyectos proyecto = new frmListarProyectos())
            {
                if (proyecto.ShowDialog() == DialogResult.OK)
                {
                    bteProyecto.Text = proyecto._Be.pryc_vcorrelativo;
                    bteProyecto.Tag = proyecto._Be.pryc_icod_proyecto;
                    txtCCosto.Text = proyecto._Be.CentroCosto;
                }

            }

        }

        private void txtSerie_EditValueChanged_1(object sender, EventArgs e)
        {
            GetOCL();
        }

        private void lkpMotivo_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpMotivo.EditValue) == 286)
            {
                bteProyecto.Enabled = true;
            }
            else
            {
                bteProyecto.Enabled = false;
            }
        }

        private void ckeincluirIgv_CheckedChanged_1(object sender, EventArgs e)
        {
            if (ckeincluirIgv.Checked == false)
            {
                this.Totalizar_sin_incluir_Igv();
            }
            else
            {
                this.Totalizar_Incluido_IGV();
            }
        }

        private void detalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listarDetalleFacOC();
        }

        private void listarDetalleFacOC()
        {
            EOrdenCompra obe = (EOrdenCompra)grvfactura.GetRow(grvfactura.FocusedRowHandle);
            try
            {
                frmListarDetalleFacOC frm = new frmListarDetalleFacOC();

                frm.ococ_icod_orden_compra = ococ_icod_orden_compra;
                frm.prd_icod_producto = Convert.ToInt32(obe.prdc_icod_producto);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    new BCompras().ActualizarCantidadRecibidaOC(ococ_icod_orden_compra, Convert.ToInt32(obe.prdc_icod_producto), Convert.ToInt32(obe.ocod_icod_detalle_oc));
                    Cargar();
                }
                grvfactura.RefreshData();
                Cargar();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
