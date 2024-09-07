using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
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
    public struct viewDocCompra
    {
        static viewDocCompra()
        {

        }
    }
    public partial class frmManteFacturaCompraImportaciones : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        /*--------------*/
        public EFacturaCompra Obe = new EFacturaCompra();
        public List<EFacturaCompraDet> lstDetalle = new List<EFacturaCompraDet>();
        public List<EFacturaCompraDet> lstDelete = new List<EFacturaCompraDet>();
        private List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        private List<EImportacionFactura> lstImpFactura = new List<EImportacionFactura>();
        /*--------------*/
        public EFacturaCompraDet obed = new EFacturaCompraDet();
        public int CodOC = 0;
        public int OCI = 0;
        public frmManteFacturaCompraImportaciones()
        {
            InitializeComponent();
        }

        private void frmManteFacturaCompra_Load(object sender, EventArgs e)
        {
            cargar();
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
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);

            if (Status == BSMaintenanceStatus.CreateNew)
            {




            }





            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();

            if (Status == BSMaintenanceStatus.CreateNew)
            {
                //txtIGV.Text = Parametros.strPorcIGV;
                setFecha(dtFecha);
                setFecha(dtFechaVencimiento);

                getTipoCambio();
                setAlmacen();

            }
            else
                //CARGAR EL DETALLE SI ES MODIFICACION O CONSULTA
                lstDetalle = new BCompras().listarFacCompraDet(Obe.fcoc_icod_doc);
            grdDetalle.DataSource = lstDetalle;
            txtTotalRegistro.Text = lstDetalle.Count().ToString();
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

            if (Lista.Count == 0)
            {
                txtTipoCambio.Text = "0.0000";
            }

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
                //bteAlmacen.Text = lstAlmacen[0].almac_vdescripcion;
                //bteAlmacen.Tag = lstAlmacen[0].almac_icod_almacen;
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
            txtNroDoc2.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            mnu.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
                enableControls(false);

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteRefreshTipoCambio.Enabled = false;
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
                //dtFechaVencimiento.Enabled = false;
                lkpFormaPago.Enabled = false;
            }

        }

        private void enableControls(bool Enabled)
        {
            txtSerie.Enabled = Enabled;
            txtCorrelativo.Enabled = Enabled;
            txtNroDoc2.Enabled = Enabled;
            //dtFecha.Enabled = Enabled;
            bteProveedor.Enabled = Enabled;
            //dtFechaVencimiento.Enabled = Enabled;
            //lkpFormaPago.Enabled = Enabled;
            lkpMoneda.Enabled = Enabled;
            //bteAlmacen.Enabled = Enabled;
            //txtIGV.Enabled = Enabled;
            //cbIncluyeIGV.Enabled = Enabled;
            txtTipoCambio.Enabled = Enabled;
            lkpMes.Enabled = Enabled;

        }

        public void setValues()
        {
            bteProveedor.Text = Obe.strProveedor;
            bteProveedor.Tag = Obe.proc_icod_proveedor;
            if (Obe.fcoc_num_doc.Length == 9)
            {
                txtSerie.Text = Obe.fcoc_num_doc.Substring(0, 3);
                txtCorrelativo.Text = Obe.fcoc_num_doc.Substring(3, 6);
            }
            else
                txtNroDoc2.Text = Obe.fcoc_num_doc;
            dtFecha.EditValue = Obe.fcoc_sfecha_doc;
            lkpFormaPago.EditValue = Obe.fcoc_iforma_pago;
            dtFechaVencimiento.EditValue = Obe.fcoc_svencimiento;
            txtObservacion.Text = Obe.fcoc_vreferencia;
            lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
            //bteAlmacen.Text = Obe.strAlmacen;
            //bteAlmacen.Tag = Obe.almac_icod_almacen;
            //txtIGV.Text = Obe.fcoc_nporcent_imp_doc.ToString();
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
            lkpMes.EditValue = Convert.ToInt32(Obe.fcoc_imes_iid_proceso);
            //cbIncluyeIGV.Checked = Obe.fcoc_bincluye_igv;
            txtNroDetraccion.Text = Obe.fcoc_vnro_depo_detraccion;
            dtFechaDetracc.Text = (Obe.fcoc_sfecha_depo_detraccion == null) ? "" : Convert.ToDateTime(Obe.fcoc_sfecha_depo_detraccion).ToShortDateString();
            //txtMtoNeto.Text = Obe.fcoc_nmonto_neto_detalle.ToString();
            txtMtoTotal.Text = Obe.fcoc_nmonto_total_detalle.ToString();
            btnOC.Tag = Obe.ococ_icod_orden_compra;
            btnOC.Text = Obe.ococ_numero_orden_compra;

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
                    else
                    {
                        if (String.IsNullOrWhiteSpace(txtNroDoc2.Text))
                        {
                            oBase = txtNroDoc2;
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
                if (String.IsNullOrWhiteSpace(txtNroDoc2.Text))
                    Obe.fcoc_num_doc = txtSerie.Text + txtCorrelativo.Text;
                else
                    Obe.fcoc_num_doc = txtNroDoc2.Text;
                Obe.fcoc_sfecha_doc = Convert.ToDateTime(dtFecha.EditValue);
                Obe.fcoc_svencimiento = Convert.ToDateTime(dtFechaVencimiento.EditValue);
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.fcoc_iforma_pago = Convert.ToInt32(lkpFormaPago.EditValue);
                Obe.fcoc_vreferencia = txtObservacion.Text;
                Obe.fcoc_nporcent_imp_doc = Convert.ToDecimal(null);
                Obe.fcoc_nmonto_destino_gravado = Convert.ToDecimal(txtMtoDesGrav.Text);
                Obe.fcoc_nmonto_destino_mixto = Convert.ToDecimal(txtMtoDesMixto.Text);
                Obe.fcoc_nmonto_destino_nogravado = Convert.ToDecimal(txtMtoDesNoGrav.Text);
                Obe.fcoc_nmonto_nogravado = Convert.ToDecimal(txtMtoNoGravada.Text);
                Obe.fcoc_nmonto_imp_destino_gravado = Convert.ToDecimal(txtImpDesGrav.Text);
                Obe.fcoc_nmonto_imp_destino_mixto = Convert.ToDecimal(txtImpDesMixto.Text);
                Obe.fcoc_nmonto_imp_destino_nogravado = Convert.ToDecimal(txtImpDesNoGrav.Text);
                Obe.fcoc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                Obe.fcoc_imes_iid_proceso = Convert.ToInt16(lkpMes.EditValue);
                Obe.fcoc_bincluye_igv = Convert.ToBoolean(null);
                Obe.fcoc_vnro_depo_detraccion = txtNroDetraccion.Text;
                Obe.fcoc_sfecha_depo_detraccion = (String.IsNullOrWhiteSpace(dtFechaDetracc.Text)) ? dtNullVal : Convert.ToDateTime(dtFechaDetracc.Text);
                Obe.fcoc_nmonto_neto_detalle = Convert.ToDecimal(null);
                Obe.fcoc_nmonto_total_detalle = Convert.ToDecimal(txtMtoTotal.Text);
                Obe.fcoc_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.fcoc_iestado_recep = 273;//facturado
                Obe.MontoTotalCabecera = Convert.ToDecimal(txtMontoTotalCabecera.Text);
                if (obed.prd_icod_producto == 0)

                    Obe.ococ_icod_orden_compra = Convert.ToInt32(btnOC.Tag);

                Obe.fcoc_anio = Convert.ToInt16(Parametros.intEjercicio);
                /**/
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                Obe.fcoc_flag_importacion = true;///////////////////

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.fcoc_icod_doc = new BCompras().insertarFacCompraImportacion(Obe, lstDetalle);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCompras().modificarFacCompraImportacion(Obe, lstDetalle, lstDelete);
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
        public void ActualizarMontoFactor()
        {

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
            //using (frmListarAlmacen frm = new frmListarAlmacen())
            //{
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        bteAlmacen.Tag = frm._Be.almac_icod_almacen;
            //        bteAlmacen.Text = frm._Be.almac_vdescripcion;
            //    }
            //}
        }
        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProveedor();
            txtSerie.Focus();
        }

        private void bteProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarProveedor();
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
            txtMtoDesGrav.Focus();
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
                //if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                //{
                //    oBase = bteAlmacen;
                //    throw new ArgumentException("Seleccione Almacén");
                //}
                using (frmManteFacCompraImportacionDetalle frm = new frmManteFacCompraImportacionDetalle())
                {
                    frm.SetInsert();
                    frm.lstDetalle = lstDetalle;
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                    frm.CodOC = CodOC;
                    frm.OCI = OCI;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewDetalle.RefreshData();
                        viewDetalle.MoveLast();
                        setTotal();
                        txtTotalRegistro.Text = lstDetalle.Count().ToString();
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
                frm.txtItem.Text = String.Format("{0:000}", obe.fcod_iitem);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();
                    setTotal();
                    viewDetalle.FocusedRowHandle = index;
                    txtTotalRegistro.Text = lstDetalle.Count().ToString();
                    txtTotalPedido.Text = lstDetalle.Sum(r => r.fcod_ncantidad).ToString();
                }
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
            txtTotalRegistro.Text = lstDetalle.Count().ToString();
            txtTotalPedido.Text = lstDetalle.Sum(r => r.fcod_ncantidad).ToString();
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
            if (txtSerie.Text != "0000")
            {
                txtNroDoc2.Enabled = false;
                txtNroDoc2.Text = String.Empty;
            }
            else
            {
                if (txtCorrelativo.Text == "00000000")
                    txtNroDoc2.Enabled = true;
            }
        }

        private void txtCorrelativo_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(txtCorrelativo.Text) != 0)
            {
                txtNroDoc2.Enabled = false;
                txtNroDoc2.Text = String.Empty;
            }
            else
            {
                if (txtSerie.Text == "0000")
                    txtNroDoc2.Enabled = true;
            }
        }

        private void txtNroDoc2_KeyUp(object sender, KeyEventArgs e)
        {
            if (String.IsNullOrEmpty(txtNroDoc2.Text))
            {
                txtSerie.Enabled = true;
                txtCorrelativo.Enabled = true;
            }
            else
            {
                txtSerie.Enabled = false;
                txtCorrelativo.Enabled = false;
            }
        }

        private void dtFecha_EditValueChanged(object sender, EventArgs e)
        {
            getTipoCambio();
            dtFechaVencimiento.EditValue = dtFecha.EditValue;
            //lkpMes.EditValue = Convert.ToDateTime(dtFecha.EditValue).Month;

            int mes = dtFecha.DateTime.Month;

            switch (mes)
            {
                case 1:
                    lkpMes.EditValue = 1;
                    break;
                case 2:
                    lkpMes.EditValue = 2;
                    break;
                case 3:
                    lkpMes.EditValue = 3;
                    break;
                case 4:
                    lkpMes.EditValue = 4;
                    break;
                case 5:
                    lkpMes.EditValue = 5;
                    break;
                case 6:
                    lkpMes.EditValue = 6;
                    break;
                case 7:
                    lkpMes.EditValue = 7;
                    break;
                case 8:
                    lkpMes.EditValue = 8;
                    break;
                case 9:
                    lkpMes.EditValue = 9;
                    break;
                case 10:
                    lkpMes.EditValue = 10;
                    break;
                case 11:
                    lkpMes.EditValue = 11;
                    break;
                case 12:
                    lkpMes.EditValue = 12;
                    break;
            }
        }

        private void setTotal()
        {
            if (lstDetalle.Count > 0)
            {
                //if (cbIncluyeIGV.Checked)
                //{
                decimal total = lstDetalle.Sum(x => Math.Round(x.fcod_nmonto_total, 2));
                //decimal neto = Math.Round(total / Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                //txtMtoNeto.Text =Convertir.RedondearNumero( neto).ToString();
                txtMtoTotal.Text = (total).ToString();
                //}
                //else
                //{
                //    decimal neto = lstDetalle.Sum(x => x.fcod_nmonto_total);
                //    decimal total = Math.Round(neto * Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                //    txtMtoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                //    txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                //}
            }
        }

        private void cbIncluyeIGV_CheckedChanged(object sender, EventArgs e)
        {
            setTotal();
        }

        private void txtMtoDesGrav_()
        {
            //decimal neto = Convert.ToDecimal(txtMtoDesGrav.Text);
            //decimal impuesto = neto * Convert.ToDecimal("0." + txtIGV.Text.Replace(".", ""));
            //txtImpDesGrav.Text =Convertir.RedondearNumero(impuesto).ToString();
        }
        private void txtMontocabecera()
        {
            decimal monto_total = Convert.ToDecimal(txtMtoDesGrav.Text) + Convert.ToDecimal(txtMtoDesMixto.Text) + Convert.ToDecimal(txtMtoDesNoGrav.Text) + Convert.ToDecimal(txtMtoNoGravada.Text) +
               Convert.ToDecimal(txtImpDesGrav.Text) + Convert.ToDecimal(txtImpDesMixto.Text) + Convert.ToDecimal(txtImpDesNoGrav.Text);
            txtMontoTotalCabecera.Text = Convertir.RedondearNumero(monto_total).ToString();
        }
        private void txtMtoDesMixto_()
        {
            //decimal neto = Convert.ToDecimal(txtMtoDesMixto.Text);
            //decimal impuesto = neto * Convert.ToDecimal("0." + txtIGV.Text.Replace(".", ""));
            //txtImpDesMixto.Text = Convertir.RedondearNumero(impuesto).ToString();
        }

        private void txtMtoDesNoGrav_()
        {
            //decimal neto = Convert.ToDecimal(txtMtoDesNoGrav.Text);
            //decimal impuesto = neto * Convert.ToDecimal("0." + txtIGV.Text.Replace(".", ""));
            //txtImpDesNoGrav.Text = Convertir.RedondearNumero(impuesto).ToString();
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

        private void btnOC_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarOrdenCompra();
        }
        private void listarOrdenCompra()
        {
            FrmListarOrdenCompraImportaciones frm = new FrmListarOrdenCompraImportaciones();
            frm.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                lstDetalle.Clear();
                btnOC.Tag = frm._Be.ocic_icod_oci;
                btnOC.Text = frm._Be.ocic_vnumero_oci;
                CodOC = Convert.ToInt32(btnOC.Tag);
                List<EOrdenCompraImportacion> mlistDetalle = new List<EOrdenCompraImportacion>();
                mlistDetalle = new BCompras().ListarOrdenCompraImportacionDetalle(Convert.ToInt32(frm._Be.ocic_vnumero_oci));
                foreach (var _be in mlistDetalle)
                {
                    EFacturaCompraDet _FD = new EFacturaCompraDet();
                    _FD.fcod_iitem = _be.ocid_iitem;
                    _FD.prd_icod_producto = Convert.ToInt32(_be.prdc_icod_producto);
                    _FD.fcod_ncantidad = Convert.ToInt32(_be.ocid_ncantidad);
                    _FD.fcod_nmonto_unit = _be.ocid_ncunitario;
                    _FD.fcod_nmonto_total = _be.ocid_ncantidad * _be.ocid_ncunitario;
                    _FD.fcod_vdescripcion_item = _be.strDescProducto;
                    _FD.strCodProducto = _be.strCodigoProducto;
                    _FD.strLinea = _be.famic_vabreviatura;
                    _FD.strSubLinea = _be.famid_vabreviatura;
                    _FD.strDesUM = _be.strAbrevUniMed;
                    _FD.strMoneda = _be.strMoneda;
                    _FD.intTipoOperacion = 1;
                    lstDetalle.Add(_FD);
                }
                grdDetalle.DataSource = lstDetalle;
                grdDetalle.RefreshDataSource();
                txtTotalRegistro.Text = lstDetalle.Count().ToString();
                OCI = 1;
            }
        }

        private void viewDetalle_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            TotalGrilla();
        }
        private void TotalGrilla()
        {
            txtTotalPedido.Text = lstDetalle.Sum(ot => ot.fcod_ncantidad).ToString();

        }

        private void viewDetalle_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TotalGrilla();
        }
        string filePath = "";
        private void importarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                string Extension = Path.GetExtension(ofd.FileName);

                if (Extension == ".xls")
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
        private void ImportarDatosExcel()
        {
            ClearLista();

            DataTable dt;
            OleDbDataAdapter MyDataAdapter;

            string connString = "provider=Microsoft.Jet.OLEDB.4.0;" + @"data source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection oledbConn = new OleDbConnection(connString);
            try
            {

                MyDataAdapter = new OleDbDataAdapter("SELECT * FROM [FactCompra$]", connString);
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
        private void ClearLista()
        {
            lstDetalle.Clear();
            viewDetalle.RefreshData();
        }

        private void FillList(DataTable dt)
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

                            switch (column.ToString().ToUpper().Trim())
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
                                    obe.fcod_nmonto_total = Convert.ToDecimal(obe.fcod_nmonto_unit * obe.fcod_ncantidad);
                                    break;
                                case "DESCRIPCION":
                                    nomC = "DESCRIPCION";
                                    obe.fcod_vdescripcion_item = row[column].ToString();
                                    break;
                            }
                            if (lstDetalle.Count > 0)
                            {
                                //if (cbIncluyeIGV.Checked)
                                //{
                                decimal total = lstDetalle.Sum(x => x.fcod_nmonto_total);
                                //decimal neto = Math.Round(total / Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                                //txtMtoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                                txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                                //}
                                //else
                                //{
                                //    decimal neto = lstDetalle.Sum(x => x.fcod_nmonto_total);
                                //    decimal total = Math.Round(neto * Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                                //    txtMtoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                                //    txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                                //}
                            }
                        }

                        obe.intTipoOperacion = 1;
                        obe.intUsuario = Valores.intUsuario;
                        obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        lstDetalle.Add(obe);

                    }
                }

                List<EFacturaCompraDet> mlistNuevos = new List<EFacturaCompraDet>();
                foreach (var BE in lstDetalle)
                {
                    if (BE.prd_icod_producto == 0)
                    {
                        mlistNuevos.Add(BE);
                    }
                }
                lstDetalle = lstDetalle.Where(ob => ob.prd_icod_producto != 0).ToList();
                int i = 1;
                foreach (var _bee in lstDetalle)
                {
                    _bee.fcod_iitem = i;
                    i = i + 1;
                }
                grdDetalle.DataSource = lstDetalle;
                viewDetalle.RefreshData();

                if (mlistNuevos.Count > 0)
                {
                    if (XtraMessageBox.Show("¿Desea Exportar los que no existen en el catálogo de productos?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ExportarDatos(mlistNuevos);
                    }
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportarDatos(List<EFacturaCompraDet> Mlist)
        {
            try
            {
                if (sfdTXT.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = sfdTXT.FileName;
                    if (!fileName.Contains(".txt"))
                    {
                        ExportarATXT(fileName + ".txt", Mlist);
                        System.Diagnostics.Process.Start(fileName + ".txt");
                    }
                    else
                    {
                        ExportarATXT(fileName, Mlist);
                        System.Diagnostics.Process.Start(fileName);
                    }
                }
                sfdTXT.FileName = string.Empty;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportarATXT(String ruta, List<EFacturaCompraDet> Mlist)
        {
            StreamWriter sw = new StreamWriter(ruta);
            string error = string.Empty;
            int cont = 0;
            string columna = string.Empty;
            try
            {
                int totFilas = Mlist.Count;
                foreach (EFacturaCompraDet item in Mlist)
                {
                    cont++;
                    error = item.prov_vcodigo_prov;
                    columna = "1";
                    sw.Write(item.prov_vcodigo_prov + "|"); // 1
                    columna = "2";
                    sw.Write(item.strCodProducto + "|"); // 2
                    columna = "3";
                    sw.Write(item.fcod_ncantidad + "|"); // A: Aptertura M: Mes C:Cierre // 3
                    columna = "4";
                    sw.Write(item.fcod_nmonto_unit + "|"); // 4
                    columna = "5";
                    sw.Write(item.fcod_vdescripcion_item + "|"); // 5


                    if (totFilas != cont)
                    {
                        sw.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message + "\nFila " + cont + "\nDocumento " + error + "\nColumna Nº " + columna);
            }
            finally
            {
                sw.Close();
            }
        }







    }
}
