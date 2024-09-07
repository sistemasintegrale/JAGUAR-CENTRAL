using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmManteVentaDirecta : DevExpress.XtraEditors.XtraForm
    {
        public EVentaDirecta oBe = new EVentaDirecta();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<EVentaDirectaDet> lstDetalle = new List<EVentaDirectaDet>();
        List<EVentaDirectaDet> lstDelete = new List<EVentaDirectaDet>();
        string strCodCliente = "";

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

                txtNumero.Enabled = Enabled;
                dteFecha.Enabled = Enabled;
                bteCliente.Enabled = Enabled;
                lkpMoneda.Enabled = Enabled;
                btePlaca.Enabled = Enabled;
            }
        }

        private void setValues()
        {
            txtNumero.Text = oBe.dvdc_vnumero_doc_venta_directa;
            dteFecha.EditValue = oBe.dvdc_sfecha_doc_venta_directa;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion;
            bteCliente.Tag = oBe.dvdc_icod_cliente;
            bteCliente.Text = oBe.strDesCliente;
            txtDNI.Text = oBe.dvdc_vdni;
            txtDireccion.Text = oBe.dvdc_vdireccion_cliente;
            txtRUC.Text = oBe.dvdc_vruc;
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            txtMontoTotal.Text = oBe.dvdc_nmonto_total.ToString();
            btePlaca.Tag = oBe.dvdc_iid_vehiculo;
            btePlaca.Text = oBe.strPlaca;
            txtMarca.Text = oBe.strMarca;
            txtModelo.Text = oBe.strModelo;
            txtColor.Text = oBe.dvdc_vcolor;
            txtAnio.Text = oBe.intAnioVehiculo.ToString();
            lstDetalle = new BVentas().listarVentaDirectaDetalle(oBe.dvdc_icod_doc_venta_directa);
            viewFactura.RefreshData();
        }

        public FrmManteVentaDirecta()
        {
            InitializeComponent();
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            SetCancel();
        }

        private void cargar()
        {
            grdFactura.DataSource = lstDetalle;
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(39), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            if (Status == BSMaintenanceStatus.CreateNew)
                setFecha(dteFecha);
        }

        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }

        private void setTotal()
        {
            txtMontoTotal.Text = lstDetalle.Sum(x => x.dvdd_nprecio_total_item).ToString();
        }

        private void nuevo(int intOpcion)
        {
            if (intOpcion == 1)// opcion 1 es para ingresar un nuevo producto            
                using (frmManteVentaDirectaDetalleProducto frm = new frmManteVentaDirectaDetalleProducto())
                {
                    frm.dtFecha.EditValue = dteFecha.EditValue;
                    frm.SetInsert();
                    frm.lstDetalle = lstDetalle;
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewFactura.RefreshData();
                        viewFactura.MoveLast();
                        setTotal();
                    }
                }
            else if (intOpcion == 2)// opcion 2 es para ingresar un nuevo servicio            
                using (frmManteVentaDirectaDetalleServicio frm = new frmManteVentaDirectaDetalleServicio())
                {
                    frm.dtFecha.EditValue = dteFecha.EditValue;
                    frm.SetInsert();
                    frm.lstDetalle = lstDetalle;
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewFactura.RefreshData();
                        viewFactura.MoveLast();
                        setTotal();
                    }
                }

        }

        private void listarCliente()
        {
            try
            {
                using (FrmListarCliente frm = new FrmListarCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteCliente.Tag = frm._Be.cliec_icod_cliente;
                        bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                        txtDireccion.Text = frm._Be.cliec_vdireccion_cliente;
                        txtDNI.Text = frm._Be.cliec_vnumero_doc_cli;
                        txtRUC.Text = frm._Be.cliec_cruc;
                        strCodCliente = frm._Be.cliec_vcod_cliente;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setCliente(int intCliente)
        {
            try
            {
                var _Be = new BVentas().ListarCliente().Where(x => x.cliec_icod_cliente == intCliente).ToList()[0];
                bteCliente.Tag = _Be.cliec_icod_cliente;
                bteCliente.Text = _Be.cliec_vnombre_cliente;
                txtDireccion.Text = _Be.cliec_vdireccion_cliente;
                txtRUC.Text = _Be.cliec_cruc;
                strCodCliente = _Be.cliec_vcod_cliente;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void listarVehiculo()
        {
            BaseEdit oBase = null;
            try
            {



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

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Registro de la Venta Directa");
                }

                if (Convert.ToDateTime(dteFecha.Text).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del ejercicio");
                }

                if (Convert.ToInt32(bteCliente.Tag) == 0)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccione cliente");
                }

                oBe.dvdc_vnumero_doc_venta_directa = txtNumero.Text;
                oBe.dvdc_sfecha_doc_venta_directa = Convert.ToDateTime(dteFecha.Text);
                oBe.dvdc_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.clic_vcod_cliente = strCodCliente;
                oBe.dvdc_vdireccion_cliente = txtDireccion.Text;
                oBe.dvdc_vdni = txtDNI.Text;
                oBe.dvdc_vruc = txtRUC.Text;
                oBe.dvdc_iid_vehiculo = Convert.ToInt32(btePlaca.Tag);
                oBe.dvdc_vcolor = txtColor.Text;
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.tablc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.dvdc_npor_imp_igv = Convert.ToDecimal(Parametros.strPorcIGV);
                oBe.dvdc_nmonto_total = Convert.ToDecimal(txtMontoTotal.Text);
                oBe.dvdc_nmonto_neto = oBe.dvdc_nmonto_total / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")));
                oBe.dvdc_nmonto_imp = oBe.dvdc_nmonto_total - oBe.dvdc_nmonto_neto;
                oBe.tablc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.strDesCliente = bteCliente.Text;



                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.dvdc_icod_doc_venta_directa = new BVentas().insertarVentaDirecta(oBe, lstDetalle);
                }
                else
                {
                    new BVentas().modificarVentaDirecta(oBe, lstDetalle, lstDelete);
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
                    MiEvento(oBe.dvdc_icod_doc_venta_directa);
                    Close();
                }
            }
        }

        private void FrmManteFactura_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btePlaca_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarVehiculo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EVentaDirectaDet obe = (EVentaDirectaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.dvdd_clasificacion == 1)
                using (frmManteVentaDirectaDetalleProducto frm = new frmManteVentaDirectaDetalleProducto())
                {
                    frm.obe = obe;
                    frm.dblStockDisponible = Convert.ToDecimal(obe.dblStockDisponible);
                    frm.lstDetalle = lstDetalle;
                    frm.SetModify();
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.txtItem.Text = String.Format("{0:000}", obe.dvdd_iitem_doc_venta_directa);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewFactura.RefreshData();
                        viewFactura.MoveLast();
                        setTotal();
                    }
                }
            else if (obe.dvdd_clasificacion == 2)
                using (frmManteVentaDirectaDetalleServicio frm = new frmManteVentaDirectaDetalleServicio())
                {
                    frm.obe = obe;

                    frm.dblStockDisponible = Convert.ToDecimal(obe.dblStockDisponible);
                    frm.lstDetalle = lstDetalle;
                    frm.SetModify();
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.txtItem.Text = String.Format("{0:000}", obe.dvdd_iitem_doc_venta_directa);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewFactura.RefreshData();
                        viewFactura.MoveLast();
                        setTotal();
                    }
                }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EVentaDirectaDet obe = (EVentaDirectaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            viewFactura.RefreshData();
            setTotal();
        }

        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo(1);
        }

        private void servicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo(2);
        }
    }
}