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
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteOrdenCompraImportacion : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteOrdenCompra));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EOrdenCompraImportacion Obe = new EOrdenCompraImportacion();
        List<EOrdenCompraImportacion> lstPresupuestoplantilla = new List<EOrdenCompraImportacion>();
        List<EOrdenCompraImportacion> lstPresupuestoPlanillaEliminados = new List<EOrdenCompraImportacion>();
        List<EArchivos> lstArchivos = new List<EArchivos>();
        public int ococ_icod_orden_compra = 0;

        public frmManteOrdenCompraImportacion()
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
            txtFormaPago.Enabled = !Enabled;
            lkpMoneda.Enabled = !Enabled;


        }
        public void setValues()
        {
            txtSerie.Text = Obe.ocic_ianio.ToString();
            txtNumero.Text = Obe.ocic_vnumero_oci.Substring(5);
            dtmFechafactura.EditValue = Obe.ocic_sfecha_oci;
            lkpSituacion.EditValue = Convert.ToInt32(Obe.tablc_iid_situacion_oci);
            lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;

            btnProveedor.Tag = Obe.proc_icod_proveedor;
            btnProveedor.Text = Obe.prvc_vcod_proveedor;
            txtDesProveedor.Text = Obe.proc_vnombrecompleto;
            txtdireccion.Text = Obe.proc_vdireccion;
            txtRUC.Text = Obe.proc_vruc;

            txtSubTotal.Text = Obe.ocic_nmonto_sub_Total.ToString();
            txtporcDesc.Text = Obe.ocic_npor_descuento.ToString();
            txtContacto.Text = Obe.ocic_vcontacto;
            txtRecepcion.Text = Obe.ocic_vreferencia;
            txtGarantia.Text = Obe.ocic_vgarantia;
            txtLugarE.Text = Obe.ocic_vlugar_entrega;
            txtNota.Text = Obe.ocic_vnota_ocl;
            txttelefono.Text = Obe.ocic_vtelefono;

            bteProyecto.Tag = Obe.ocic_iid_proyecto;
            bteProyecto.Text = string.Format("{0:000000}", Obe.ocic_iid_proyecto);
            txtCCosto.Text = Obe.cecoc_vcodigo_centro_costo;
            lkpMotivo.EditValue = Obe.ocic_iid_motivo;

            txtFormaPago.Text = Obe.ocic_vforma_pago;

            txtIncoterm.Text = Obe.ocic_vincoterm;
            //dteFechEntrega.DateTime = Obe.ocic_sfecha_entrega;

            dteFechEntrega.DateTime = Convert.ToDateTime(Obe.ocic_sfecha_entrega);


            txtFormaPago.Text = Obe.ocic_vforma_pago;
            txtSolicitante.Text = Obe.ocic_vsolicitante;

        }
        public void setDobleClick()
        {
            txtSerie.Properties.ReadOnly = true;
            txtNumero.Properties.ReadOnly = true;
            dtmFechafactura.Enabled = false;
            btnProveedor.Enabled = false;

            txtFormaPago.Enabled = false;
            lkpMoneda.Enabled = false;
            btnGuardar.Enabled = false;
            mnuPresupuestoNacional.Enabled = false;
            Cargar();

        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            Cargar();
            GetOCL();
            dteFechEntrega.EditValue = DateTime.Now;

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
            lstPresupuestoplantilla = new BCompras().ListarOrdenCompraImportacionDetalle(ococ_icod_orden_compra);
            grcfactura.DataSource = lstPresupuestoplantilla;
            grcfactura.RefreshDataSource();
            grvfactura.ExpandAllGroups();

            this.Totalizar_sin_incluir_Igv();

        }
        private void frmManteCiaSeguros_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(70), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

            BSControls.LoaderLook(lkpMotivo, new BGeneral().listarTablaRegistro(71), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }
        private void GetOCL()
        {
            string correlativo = new BAdministracionSistema().ObtenerCorrelativoOCI(Convert.ToInt32(txtSerie.Text));
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
                    List<EOrdenCompraImportacion> Lstver = new BVentas().getOCICabVerificarNumero(String.Format("{0}{1}", txtSerie.Text, txtNumero.Text), Parametros.intEjercicio);
                    if (Lstver.Count > 0)
                    {
                        oBase = txtNumero;
                        XtraMessageBox.Show("El Número " + String.Format("{0}{1}", txtSerie.Text, txtNumero.Text) + " N° O/C: Ya Existia, Se Agrego Nuevo");
                        GetOCL();
                    }
                }
                Obe.ocic_icod_oci = ococ_icod_orden_compra;
                Obe.ocic_ianio = Convert.ToInt32(txtSerie.Text);
                Obe.ocic_vnumero_oci = txtSerie.Text + txtNumero.Text;
                Obe.ocic_sfecha_oci = Convert.ToDateTime(dtmFechafactura.EditValue);
                Obe.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
                Obe.prvc_vcod_proveedor = btnProveedor.Text;
                Obe.proc_vnombrecompleto = txtDesProveedor.Text;
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.tablc_iid_situacion_oci = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.ocic_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.ocic_nmonto_neto = Convert.ToDecimal(txtTotalNeto.Text);
                Obe.ocic_npor_imp_igv = Convert.ToDecimal(Parametros.strPorcIGV);
                Obe.ocic_nmonto_total = Convert.ToDecimal(txtTotalOC.Text);
                Obe.ocic_nmonto_sub_Total = Convert.ToDecimal(txtSubTotal.Text);
                Obe.ocic_npor_descuento = Convert.ToDecimal(txtporcDesc.Text);
                Obe.ocic_nmonto_descuento = Convert.ToDecimal(txtDescuento.Text);
                Obe.ocic_iid_proyecto = Convert.ToInt32(bteProyecto.Tag);
                Obe.ocic_vcontacto = txtContacto.Text;
                Obe.ocic_vreferencia = txtRecepcion.Text;
                Obe.ocic_vgarantia = txtGarantia.Text;
                Obe.ocic_vlugar_entrega = txtLugarE.Text;
                Obe.ocic_vnota_ocl = txtNota.Text;
                Obe.ocic_vtelefono = txttelefono.Text;
                Obe.ocic_iid_motivo = Convert.ToInt32(lkpMotivo.EditValue);
                Obe.ocic_vforma_pago = txtFormaPago.Text;

                Obe.ocic_vincoterm = txtIncoterm.Text;
                //Obe.ocic_sfecha_entrega = dteFechEntrega.DateTime;
                if (dteFechEntrega.DateTime == null || dteFechEntrega.Text == "" || dteFechEntrega.Text == "01/01/0001")
                {
                    Obe.ocic_sfecha_entrega = (DateTime?)null;
                }
                else
                {
                    Obe.ocic_sfecha_entrega = dteFechEntrega.DateTime;
                }
                Obe.ocic_vcotizacion = txtCotizacion.Text;
                Obe.ocic_vsolicitante = txtSolicitante.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.ocic_icod_oci = new BCompras().InsertarOrdenCompraImportacion(Obe, lstPresupuestoplantilla, lstArchivos);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCompras().ActualizarOrdenCompraImportacion(Obe, lstPresupuestoplantilla, lstPresupuestoPlanillaEliminados, lstArchivos);
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
                    this.MiEvento(Obe.ocic_icod_oci);
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
                frmManteOrdenCompraImportacionDetalle frmdetalle = new frmManteOrdenCompraImportacionDetalle();
                frmdetalle.Text = "Detalle de la Orden de Compra N°: " + txtSerie.Text + "-" + txtNumero.Text;
                frmdetalle.IntMoneda = Convert.ToInt32(lkpMoneda.EditValue);
                frmdetalle.txtItem.Text = (lstPresupuestoplantilla.Count + 1).ToString();
                frmdetalle.btnModificar.Enabled = false;
                frmdetalle.IntMoneda = Convert.ToInt32(lkpMoneda.EditValue);

                lstArchivos = frmdetalle.lstArchivos;
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    EOrdenCompraImportacion _be = new EOrdenCompraImportacion();
                    _be.ocid_icod_detalle_oci = frmdetalle.oBE.ocid_icod_detalle_oci;
                    _be.ocic_icod_oci = frmdetalle.oBE.ocic_icod_oci;
                    _be.ocid_iitem = frmdetalle.oBE.ocid_iitem;
                    _be.prdc_icod_producto = frmdetalle.oBE.prdc_icod_producto;
                    _be.prdc_vcodigo_fabricante = frmdetalle.oBE.prdc_vcodigo_fabricante;
                    _be.ocid_vdescripcion = frmdetalle.oBE.ocid_vdescripcion;
                    _be.ocid_vcaracteristicas = frmdetalle.oBE.ocid_vcaracteristicas;
                    _be.ocid_ncantidad = frmdetalle.oBE.ocid_ncantidad;
                    _be.ocid_ncunitario = frmdetalle.oBE.ocid_ncunitario;
                    _be.ocid_nmonto_total = frmdetalle.oBE.ocid_nmonto_total;
                    _be.ocid_ndescuento_item = frmdetalle.oBE.ocid_ndescuento_item;
                    _be.intUsuario = frmdetalle.oBE.intUsuario;
                    _be.strPc = frmdetalle.oBE.strPc;
                    _be.operacion = 1;
                    _be.strCodigoProducto = frmdetalle.oBE.strCodigoProducto;

                    _be.orpdi_nprecio_soles = frmdetalle.oBE.orpdi_nprecio_soles;
                    _be.orpdi_nprecio_dolares = frmdetalle.oBE.orpdi_nprecio_dolares;

                    _be.strDescProducto = frmdetalle.oBE.strDescProducto;
                    //_be.famic_vabreviatura = frmdetalle.oBE.famic_vabreviatura;
                    //_be.famid_vabreviatura = frmdetalle.oBE.famid_vabreviatura;
                    _be.strMedida = frmdetalle.oBE.strMedida;
                    _be.ocid_flag_estado = frmdetalle.oBE.ocid_flag_estado;
                    lstArchivos = frmdetalle.lstArchivos;
                    lstPresupuestoplantilla.Add(_be);
                    grcfactura.RefreshDataSource();


                    this.Totalizar_sin_incluir_Igv();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void mnuPresupuestoNacional_Opening(object sender, CancelEventArgs e)
        {

        }

        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EOrdenCompraImportacion _obeAU = (EOrdenCompraImportacion)grvfactura.GetRow(grvfactura.FocusedRowHandle);
            if (lstPresupuestoplantilla.Count > 0)
            {
                if (_obeAU.prdc_icod_producto != null)
                {
                    frmManteOrdenCompraImportacionDetalle frmdetalle = new frmManteOrdenCompraImportacionDetalle();
                    frmdetalle.Text = "Detalle de la Orden de Compra N°: " + txtSerie.Text + "-" + txtNumero.Text;
                    frmdetalle.ocod_icod_detalle_oc = _obeAU.ocid_icod_detalle_oci;
                    frmdetalle.oBE = _obeAU;
                    frmdetalle.setValues();
                    frmdetalle.SetModify();
                    frmdetalle.btnAgregar.Enabled = false;
                    frmdetalle.btnProducto.Enabled = false;
                    frmdetalle.IntMoneda = Convert.ToInt32(lkpMoneda.EditValue);
                    if (frmdetalle.ShowDialog() == DialogResult.OK)
                    {
                        _obeAU.ocid_iitem = frmdetalle.oBE.ocid_iitem;
                        _obeAU.ocid_ncantidad = frmdetalle.oBE.ocid_ncantidad;
                        _obeAU.ocid_ncunitario = frmdetalle.oBE.ocid_ncunitario;
                        _obeAU.ocid_nmonto_total = frmdetalle.oBE.ocid_nmonto_total;
                        _obeAU.ocid_vcaracteristicas = frmdetalle.oBE.ocid_vcaracteristicas;
                        if (_obeAU.ocid_icod_detalle_oci == 0)
                            _obeAU.operacion = 1;
                        else
                        {
                            if (_obeAU.operacion != 1)
                            {
                                _obeAU.operacion = 2;
                            }
                        }
                        grcfactura.RefreshDataSource();

                        this.Totalizar_sin_incluir_Igv();

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
            EOrdenCompraImportacion obj = (EOrdenCompraImportacion)grvfactura.GetRow(grvfactura.FocusedRowHandle);
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

            this.Totalizar_sin_incluir_Igv();


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
            txtSubTotal.Text = ((lstPresupuestoplantilla.Sum(ob => ob.ocid_nmonto_total))).ToString();
            txtDescuento.Text = Convertir.RedondearNumero((Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(txtporcDesc.Text)) / 100).ToString();
            txtTotalNeto.Text = Convertir.RedondearNumero((Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDescuento.Text))).ToString();

            //if (Convert.ToDecimal(txtporIGV.Text) == 0)
            //{
            //    txtIGV.Text = "0.00";
            //}
            //else
            //{

            //    txtIGV.Text = Math.Round(Convert.ToDecimal((Convert.ToDecimal(txtTotalNeto.Text) * Convert.ToDecimal(txtporIGV.Text)) / 100), 2, MidpointRounding.ToEven).ToString();
            //}

            txtTotalOC.Text = (Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDescuento.Text)).ToString();
        }
        public void Totalizar_Incluido_IGV()
        {
            //decimal dec_Total = 0;
            //decimal dec_Igv = 0;

            //txtSubTotal.Text = ((lstPresupuestoplantilla.Sum(ob => ob.ocid_nmonto_total))).ToString();
            ////dec_Total = ((lstPresupuestoplantilla.Sum(ob => ob.ocid_nmonto_total)));
            ////txtSubTotal.Text = Convertir.RedondearNumero((((Math.Round(dec_Total, 2, MidpointRounding.ToEven)) / (100 + Convert.ToDecimal(txtporIGV.Text))) * 100)).ToString();

            //txtDescuento.Text = Convertir.RedondearNumero((Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(txtporcDesc.Text)) / 100).ToString();
            //txtTotalNeto.Text = Convertir.RedondearNumero((Convert.ToDecimal(txtSubTotal.Text) - Convert.ToDecimal(txtDescuento.Text))).ToString();            
            //txtTotalOC.Text = (((Math.Round(Convert.ToDecimal(txtTotalNeto.Text), 2, MidpointRounding.ToEven)) * (100 + Convert.ToDecimal(txtporIGV.Text))) / 100).ToString();

            //if (Convert.ToDecimal(txtporIGV.Text) == 0)
            //{
            //    txtIGV.Text = "0.00";
            //}
            //else
            //{
            //    txtIGV.Text = (Convert.ToDecimal(txtTotalOC.Text) - Convert.ToDecimal(txtTotalNeto.Text)).ToString();
            //}

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

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl11_Click(object sender, EventArgs e)
        {

        }



        private void btnProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtNumero.Text = (new BAdministracionSistema()).ObtenerCorrelativoDocumento(txtSerie.Text, Parametros.intTipoDocumentoOC);
            }
        }



        private void ckeincluirIgv_CheckedChanged(object sender, EventArgs e)
        {


            this.Totalizar_sin_incluir_Igv();

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

            this.Totalizar_sin_incluir_Igv();

        }

        private void lkpMoneda_EditValueChanged(object sender, EventArgs e)
        {
            this.recalculoTotal();
        }
        private void recalculoTotal()
        {
            recalcularByMoneda(Convert.ToInt32(lkpMoneda.EditValue));

            this.Totalizar_sin_incluir_Igv();

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
                        OBE.ocid_ncunitario = orpdi_nprecio_soles;
                        OBE.ocid_nmonto_total = orpdi_nprecio_soles * OBE.ocid_ncantidad;
                    }
                    else
                    {
                        OBE.ocid_ncunitario = orpdi_nprecio_dolares;
                        OBE.ocid_nmonto_total = orpdi_nprecio_dolares * OBE.ocid_ncantidad;
                    }

                }
                grvfactura.RefreshData();
            }

        }



        private void txtporIGV_EditValueChanged(object sender, EventArgs e)
        {

            this.Totalizar_sin_incluir_Igv();

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
                    EOrdenCompraImportacion _e = new EOrdenCompraImportacion();
                    if (lstPresupuestoplantilla.Count > 0)
                        _e.ocid_iitem = (lstPresupuestoplantilla.Max(o => o.ocid_iitem) + 1);
                    else
                        _e.ocid_iitem = 1;
                    _e.ocic_icod_oci = 0;
                    _e.prdc_icod_producto = _BEE.prdc_icod_producto;
                    _e.strDescProducto = _BEE.prdc_vdescripcion_larga;
                    _e.strCodigoProducto = _BEE.prdc_vcode_producto;
                    _e.ocid_ncantidad = _BEE.lpedid_nCant_pedido;
                    _e.ocid_ncunitario = _BEE.lpedid_nprecio_neto;
                    _e.ocid_nmonto_total = _BEE.lpedid_nCant_pedido * _BEE.lpedid_nprecio_neto;
                    _e.ocid_flag_estado = true;
                    _e.operacion = 1;
                    //_e.famic_vabreviatura = _BEE.strCategoria;
                    //_e.famid_vabreviatura=_BEE.strSubCategoriaUno;
                    _e.strMedida = _BEE.StrUnidadMedida;
                    lstPresupuestoplantilla.Add(_e);
                }
                grcfactura.DataSource = lstPresupuestoplantilla;
                grcfactura.RefreshDataSource();

                this.Totalizar_sin_incluir_Igv();


            }
        }

        private void btnProveedor_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.ListarProveedor();
        }

        private void lkpProyecto_EditValueChanged(object sender, EventArgs e)
        {




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

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            //GetOCL();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void lkpMotivo_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpMotivo.EditValue) == 300)
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

            this.Totalizar_sin_incluir_Igv();

        }


    }
}
