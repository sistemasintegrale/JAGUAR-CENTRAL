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
    public partial class frmManteOrdenCompraServicio : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteOrdenCompra));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EOrdenCompraServicio Obe = new EOrdenCompraServicio();
        List<EOrdenCompraServicio> lstPresupuestoplantilla = new List<EOrdenCompraServicio>();
        List<EOrdenCompraServicio> lstPresupuestoPlanillaEliminados = new List<EOrdenCompraServicio>();
        List<EArchivos> lstArchivos = new List<EArchivos>();
        public int ococ_icod_orden_compra = 0;

        public frmManteOrdenCompraServicio()
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
            lkpMoneda.Enabled = !Enabled;


        }
        public void setValues()
        {
            txtSerie.Text = Obe.ocsc_ianio.ToString();
            txtNumero.Text = Obe.ocsc_vnumero_ocs.Substring(5);
            dtmFechafactura.EditValue = Obe.ocsc_sfecha_ocs;
            lkpSituacion.EditValue = Convert.ToInt32(Obe.tablc_iid_situacion_ocs);
            lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
            ckeincluirIgv.Checked = Obe.ocsc_bincluye_igv;
            txtporIGV.Text = Obe.ocsc_npor_imp_igv.ToString();

            btnProveedor.Tag = Obe.proc_icod_proveedor;
            btnProveedor.Text = Obe.prvc_vcod_proveedor;
            txtDesProveedor.Text = Obe.proc_vnombrecompleto;
            txtdireccion.Text = Obe.proc_vdireccion;
            txtRUC.Text = Obe.proc_vruc;

            //txtSubTotal.Text = Obe.ocsc_nmonto_sub_total.ToString();
            txtporcDesc.Text = Obe.ocsc_npor_descuento.ToString();
            txtIGV.Text = Obe.ocsc_nmonto_impuesto.ToString();
            txtContacto.Text = Obe.ocsc_vcontacto;
            txtReferencia.Text = Obe.ocsc_vreferncia;
            txtGarantia.Text = Obe.ocsc_vgarantia;
            txtLugarE.Text = Obe.ocsc_vlugar_entrega;
            txtNota.Text = Obe.ocsc_vnota_ocs;
            txttelefono.Text = Obe.ocsc_vtelefono;
            txtFormaPago.Text = Obe.ocsc_vforma_pago;

            lkpMotivo.EditValue = Obe.ocsc_iid_tipo;

            txtTotalOC.Text = Obe.ocsc_nmonto_total.ToString();
            txtCotizacion.Text = Obe.ocsc_vcotizacion;
            txtCiudad.Text = Obe.ocsc_vciudad;
        }
        public void setDobleClick()
        {
            txtSerie.Properties.ReadOnly = true;
            txtNumero.Properties.ReadOnly = true;
            dtmFechafactura.Enabled = false;
            btnProveedor.Enabled = false;

            lkpMoneda.Enabled = false;
            ckeincluirIgv.Enabled = false;
            btnGuardar.Enabled = false;
            mnuPresupuestoNacional.Enabled = false;
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
            lstPresupuestoplantilla = new BCompras().ListarOrdenCompraServicioDetalle(ococ_icod_orden_compra);
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
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(68), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

            BSControls.LoaderLook(lkpMotivo, new BGeneral().listarTablaRegistro(69), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

        }
        private void GetOCL()
        {
            string correlativo = new BAdministracionSistema().ObtenerCorrelativoOCS(Convert.ToInt32(txtSerie.Text));
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
                    List<EOrdenCompraServicio> Lstver = new BVentas().getOCSCabVerificarNumero(String.Format("{0}{1}", txtSerie.Text, txtNumero.Text), Parametros.intEjercicio);
                    if (Lstver.Count > 0)
                    {

                        oBase = txtNumero;
                        XtraMessageBox.Show("El Número " + String.Format("{0}{1}", txtNumero.Text, txtSerie.Text) + " N° O/C: Ya Existia, Se Agrego Nuevo");
                        GetOCL();
                    }
                }
                Obe.ocsc_icod_ocs = ococ_icod_orden_compra;
                Obe.ocsc_ianio = Convert.ToInt32(txtSerie.Text);
                Obe.ocsc_vnumero_ocs = txtSerie.Text + txtNumero.Text;
                Obe.ocsc_sfecha_ocs = Convert.ToDateTime(dtmFechafactura.EditValue);
                Obe.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
                Obe.prvc_vcod_proveedor = btnProveedor.Text;
                Obe.proc_vnombrecompleto = txtDesProveedor.Text;
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.tablc_iid_situacion_ocs = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.ocsc_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.ocsc_bincluye_igv = ckeincluirIgv.Checked;
                Obe.ocsc_nmonto_neto = Convert.ToDecimal(txtTotalNeto.Text);
                Obe.ocsc_npor_imp_igv = Convert.ToDecimal(Parametros.strPorcIGV);
                Obe.ocsc_nmonto_impuesto = Convert.ToDecimal(txtIGV.Text);
                Obe.ocsc_nmonto_total = Convert.ToDecimal(txtTotalOC.Text);
                Obe.ocsc_nmonto_sub_total = Convert.ToDecimal(txtSubTotal.Text);
                Obe.ocsc_npor_descuento = Convert.ToDecimal(txtporcDesc.Text);
                Obe.ocsc_nmonto_descuento = Convert.ToDecimal(txtDescuento.Text);
                Obe.ocsc_npor_imp_igv = Convert.ToDecimal(txtporIGV.Text);
                Obe.ocsc_vcontacto = txtContacto.Text;
                Obe.ocsc_vreferncia = txtReferencia.Text;
                Obe.ocsc_vgarantia = txtGarantia.Text;
                Obe.ocsc_vlugar_entrega = txtLugarE.Text;
                Obe.ocsc_vnota_ocs = txtNota.Text;
                Obe.ocsc_vtelefono = txttelefono.Text;
                Obe.ocsc_iid_tipo = Convert.ToInt32(lkpMotivo.EditValue);

                Obe.ocsc_vforma_pago = txtFormaPago.Text;

                Obe.ocsc_vcotizacion = txtCotizacion.Text;
                Obe.ocsc_vciudad = txtCiudad.Text;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.ocsc_icod_ocs = new BCompras().InsertarOrdenCompraServicio(Obe, lstPresupuestoplantilla, lstArchivos);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCompras().ActualizarOrdenCompraServicio(Obe, lstPresupuestoplantilla, lstPresupuestoPlanillaEliminados, lstArchivos);
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
                    this.MiEvento(Obe.ocsc_icod_ocs);
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
                frmManteOrdenCompraServicioDetalle frmdetalle = new frmManteOrdenCompraServicioDetalle();
                frmdetalle.Text = "Detalle de la Orden de Compra N°: " + txtSerie.Text + "-" + txtNumero.Text;
                frmdetalle.IntMoneda = Convert.ToInt32(lkpMoneda.EditValue);
                frmdetalle.txtItem.Text = (lstPresupuestoplantilla.Count + 1).ToString();
                frmdetalle.btnModificar.Enabled = false;
                frmdetalle.lstArchivos = lstArchivos;
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    EOrdenCompraServicio _be = new EOrdenCompraServicio();
                    _be.ocsd_icod_detalle_ocs = frmdetalle.oBE.ocsd_icod_detalle_ocs;
                    _be.ocsc_icod_ocs = frmdetalle.oBE.ocsc_icod_ocs;
                    _be.ocsd_iitem = frmdetalle.oBE.ocsd_iitem;
                    _be.ocsd_vcodigo_servicio_prov = frmdetalle.oBE.ocsd_vcodigo_servicio_prov;
                    _be.ocsd_sfecha_entrega = frmdetalle.oBE.ocsd_sfecha_entrega;
                    _be.ocsd_vdescripcion = frmdetalle.oBE.ocsd_vdescripcion;
                    _be.ocsd_vcaracteristicas = frmdetalle.oBE.ocsd_vcaracteristicas;
                    _be.ocsd_ncantidad = frmdetalle.oBE.ocsd_ncantidad;
                    _be.ocsd_ncunitaria = frmdetalle.oBE.ocsd_ncunitaria;
                    _be.ocsd_nvalor_total = frmdetalle.oBE.ocsd_nvalor_total;
                    _be.ocsd_ndescuento = frmdetalle.oBE.ocsd_ndescuento;
                    _be.intUsuario = frmdetalle.oBE.intUsuario;
                    _be.strPc = frmdetalle.oBE.strPc;
                    _be.operacion = 1;
                    _be.unidc_icod_unidad_medida = frmdetalle.oBE.unidc_icod_unidad_medida;

                    _be.orpdi_nprecio_soles = frmdetalle.oBE.orpdi_nprecio_soles;
                    _be.orpdi_nprecio_dolares = frmdetalle.oBE.orpdi_nprecio_dolares;

                    _be.strMedida = frmdetalle.oBE.strMedida;
                    _be.ocsd_flag_esatdo = frmdetalle.oBE.ocsd_flag_esatdo;
                    lstArchivos = frmdetalle.lstArchivos;
                    if (lstArchivos.Count > 0)
                    {
                        _be.ocsd_vdireccion_documento = lstArchivos[0].arch_vruta;
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

        private void mnuPresupuestoNacional_Opening(object sender, CancelEventArgs e)
        {

        }

        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EOrdenCompraServicio _obeAU = (EOrdenCompraServicio)grvfactura.GetRow(grvfactura.FocusedRowHandle);
            if (lstPresupuestoplantilla.Count > 0)
            {

                frmManteOrdenCompraServicioDetalle frmdetalle = new frmManteOrdenCompraServicioDetalle();
                frmdetalle.Text = "Detalle de la Orden de Compra N°: " + txtSerie.Text + "-" + txtNumero.Text;
                frmdetalle.ocod_icod_detalle_oc = _obeAU.ocsd_icod_detalle_ocs;
                frmdetalle.oBE = _obeAU;
                frmdetalle.setValues();
                frmdetalle.SetModify();
                frmdetalle.btnAgregar.Enabled = false;
                frmdetalle.lstArchivos = lstArchivos;
                if (lstArchivos.Count > 0)
                {
                    _obeAU.ocsd_vdireccion_documento = lstArchivos[0].arch_vruta;
                }
                if (frmdetalle.ShowDialog() == DialogResult.OK)
                {
                    _obeAU.ocsd_iitem = frmdetalle.oBE.ocsd_iitem;
                    _obeAU.ocsd_ncantidad = frmdetalle.oBE.ocsd_ncantidad;
                    _obeAU.ocsd_ncunitaria = frmdetalle.oBE.ocsd_ncunitaria;
                    _obeAU.ocsd_nvalor_total = frmdetalle.oBE.ocsd_nvalor_total;
                    _obeAU.ocsd_ndescuento = frmdetalle.oBE.ocsd_ndescuento;
                    _obeAU.unidc_icod_unidad_medida = frmdetalle.oBE.unidc_icod_unidad_medida;
                    _obeAU.ocsd_sfecha_entrega = frmdetalle.oBE.ocsd_sfecha_entrega;
                    lstArchivos = frmdetalle.lstArchivos;
                    if (_obeAU.ocsd_icod_detalle_ocs == 0)
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
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void Delete()
        {
            EOrdenCompraServicio obj = (EOrdenCompraServicio)grvfactura.GetRow(grvfactura.FocusedRowHandle);
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
            txtSubTotal.Text = ((lstPresupuestoplantilla.Sum(ob => ob.ocsd_nvalor_total))).ToString();
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

            txtTotalOC.Text = (Convert.ToDecimal(txtTotalNeto.Text) + Convert.ToDecimal(txtIGV.Text)).ToString();
        }
        public void Totalizar_Incluido_IGV()
        {
            decimal dec_Total = 0;
            decimal dec_Igv = 0;

            dec_Total = ((lstPresupuestoplantilla.Sum(ob => ob.ocsd_nvalor_total)));
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
                        OBE.ocsd_ncunitaria = orpdi_nprecio_soles;
                        OBE.ocsd_nvalor_total = orpdi_nprecio_soles * OBE.ocsd_ncantidad;
                    }
                    else
                    {
                        OBE.ocsd_ncunitaria = orpdi_nprecio_dolares;
                        OBE.ocsd_nvalor_total = orpdi_nprecio_dolares * OBE.ocsd_ncantidad;
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
                    EOrdenCompraServicio _e = new EOrdenCompraServicio();
                    if (lstPresupuestoplantilla.Count > 0)
                        _e.ocsd_iitem = (lstPresupuestoplantilla.Max(o => o.ocsd_iitem) + 1);
                    else
                        _e.ocsd_iitem = 1;
                    _e.ocsc_icod_ocs = 0;
                    _e.ocsd_ncantidad = _BEE.lpedid_nCant_pedido;
                    _e.ocsd_ncunitaria = _BEE.lpedid_nprecio_neto;
                    _e.ocsd_nvalor_total = _BEE.lpedid_nCant_pedido * _BEE.lpedid_nprecio_neto;
                    _e.ocsd_flag_esatdo = true;
                    _e.operacion = 1;
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

        private void lkpProyecto_EditValueChanged(object sender, EventArgs e)
        {




        }

        private void bteProyecto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarProyectos proyecto = new frmListarProyectos())
            {
                if (proyecto.ShowDialog() == DialogResult.OK)
                {

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

        private void txtporIGV_Leave(object sender, EventArgs e)
        {
            if (txtporIGV.Text == "0.00")
            {
                this.Totalizar_sin_incluir_Igv();
            }
            else
            {
                this.Totalizar_Incluido_IGV();
            }
        }

        private void txtIGV_EditValueChanged(object sender, EventArgs e)
        {

        }


    }
}
