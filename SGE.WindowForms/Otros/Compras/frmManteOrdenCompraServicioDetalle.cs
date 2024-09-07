using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Operaciones;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteOrdenCompraServicioDetalle : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public List<EOrdenCompraServicio> oDetail = new List<EOrdenCompraServicio>();
        public List<EArchivos> lstArchivos = new List<EArchivos>();

        public EOrdenCompraServicio oBE;
        public BSMaintenanceStatus oState;
        public int ococ_icod_orden_compra = 0;
        public int ocod_icod_detalle_oc = 0;
        public int Correlativo = 0;
        public long intIdKardex = 0;
        public int IntMoneda;

        decimal Monto_soles;
        decimal Monto_dolares;



        public int funcion;//0:insertar:1:modificar

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

            txtCantidad.Enabled = !Enabled;

        }

        private void clearControl()
        {
            txtItem.EditValue = Correlativo;
            txtUM.Text = "";

        }
        public void setValues()
        {
            txtItem.EditValue = oBE.ocsd_iitem;
            //txtUM.Text = oBE.strMedida;
            txtCantidad.Text = oBE.ocsd_ncantidad.ToString();
            txtPUnitario.Text = oBE.ocsd_ncunitaria.ToString();
            txtDescripcionOCDetalle.Text = oBE.ocsd_vdescripcion;
            txtCaracteristica.Text = oBE.ocsd_vcaracteristicas;
            txtDescuento.Text = oBE.ocsd_ndescuento.ToString();
            txtCodFabricante.Text = oBE.ocsd_vcodigo_servicio_prov;
            txtPVenta.Text = oBE.ocsd_nvalor_total.ToString();
            dtmFechaEntrega.EditValue = oBE.ocsd_sfecha_entrega;
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public frmManteOrdenCompraServicioDetalle()
        {
            InitializeComponent();
        }




        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AgregarDetalle();
        }
        private void AgregarDetalle()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            oBE = new EOrdenCompraServicio();
            try
            {


                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La cantidad no puede ser 0.");
                }

                if (Convert.ToDecimal(txtPUnitario.Text) == 0)
                {
                    oBase = txtPUnitario;
                    throw new ArgumentException("Ingrese Precio Unitario");

                }
                oBE.ocsd_icod_detalle_ocs = ocod_icod_detalle_oc;
                oBE.ocsc_icod_ocs = ococ_icod_orden_compra;
                oBE.ocsd_iitem = Convert.ToInt32(txtItem.EditValue);
                oBE.ocsd_vcodigo_servicio_prov = txtCodFabricante.Text;
                oBE.ocsd_sfecha_entrega = Convert.ToDateTime(dtmFechaEntrega.EditValue);
                oBE.ocsd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                oBE.ocsd_ncunitaria = Convert.ToDecimal(txtPUnitario.Text);
                oBE.ocsd_ndescuento = Convert.ToDecimal(txtDescuento.Text);
                oBE.ocsd_nvalor_total = Convert.ToDecimal(txtPVenta.Text);
                oBE.ocsd_vcaracteristicas = txtCaracteristica.Text;
                oBE.ocsd_vdescripcion = txtDescripcionOCDetalle.Text;
                oBE.unidc_icod_unidad_medida = Convert.ToInt32(lkpUnidadMedida.EditValue);
                oBE.intUsuario = Valores.intUsuario;
                oBE.strPc = WindowsIdentity.GetCurrent().Name;
                if (lstArchivos.Count > 0)
                {
                    oBE.ocsd_vdireccion_documento = lstArchivos[0].arch_vruta;
                }

                //oBE.orpdi_nprecio_soles = Monto_soles;
                //oBE.orpdi_nprecio_dolares = Monto_dolares;

                //oBE.strMedida = txtUM.Text;
                oBE.ocsd_flag_esatdo = true;

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Flag = false;
                }
            }
            finally
            {
                if (Flag)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                        Status = BSMaintenanceStatus.View;
                    else
                        Status = BSMaintenanceStatus.View;

                    Status = BSMaintenanceStatus.View;
                    this.Close();
                }
            }
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ModificarDetalle();
        }
        private void ModificarDetalle()
        {
            {
                {
                    BaseEdit oBase = null;
                    Boolean Flag = true;
                    oBE = new EOrdenCompraServicio();
                    try
                    {
                        if (Convert.ToDecimal(txtCantidad.Text) == 0)
                        {
                            oBase = txtCantidad;
                            throw new ArgumentException("La cantidad no puede ser 0.");
                        }
                        if (Convert.ToDecimal(txtPUnitario.Text) == 0)
                        {
                            oBase = txtCantidad;
                            throw new ArgumentException("El precio Unitario no puede ser 0.");
                        }

                        oBE.ocsd_iitem = Convert.ToInt32(txtItem.EditValue);
                        oBE.ocsd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                        oBE.ocsd_ncunitaria = Convert.ToDecimal(txtPUnitario.Text);
                        oBE.ocsd_nvalor_total = Convert.ToDecimal(txtPVenta.Text);
                        oBE.ocsd_ndescuento = Convert.ToDecimal(txtDescuento.Text);
                        oBE.unidc_icod_unidad_medida = Convert.ToInt32(lkpUnidadMedida.EditValue);
                        oBE.ocsd_sfecha_entrega = Convert.ToDateTime(dtmFechaEntrega.EditValue);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        if (oBase != null)
                        {
                            oBase.Focus();

                            oBase.ErrorText = ex.Message;
                            oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                            XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Flag = false;
                        }
                    }
                    finally
                    {
                        if (Flag)
                        {
                            if (Status == BSMaintenanceStatus.CreateNew)
                                Status = BSMaintenanceStatus.View;
                            else
                                Status = BSMaintenanceStatus.View;

                            Status = BSMaintenanceStatus.View;
                            this.Close();
                        }
                    }
                }
            }
        }

        private void frmManteOrdenCompraDetalleDetalle_Load(object sender, EventArgs e)
        {

            //BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpUnidadMedida, new BAlmacen().listarUnidadMedida(), "unidc_vdescripcion", "unidc_icod_unidad_medida", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpUnidadMedida.EditValue = Convert.ToInt32(oBE.unidc_icod_unidad_medida);
            }
            if (Status == BSMaintenanceStatus.CreateNew)
                dtmFechaEntrega.EditValue = DateTime.Now;

        }

        private void txtPUnitario_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void Totalizar()
        {
            decimal ddescuento = 0;
            ddescuento = ((Convert.ToDecimal(txtPUnitario.Text) * Convert.ToDecimal(txtDescuento.Text)) / 100);
            txtPVenta.Text = ((Convert.ToDecimal(txtPUnitario.Text) - ddescuento) * Convert.ToDecimal(txtCantidad.Text)).ToString();
        }

        private void txtDescuento_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();

        }
        private void Cantidad()
        {
            txtPVenta.Text = (Convert.ToDecimal(txtPUnitario.Text) * Convert.ToDecimal(txtCantidad.Text)).ToString();
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void btnVerDoc_Click(object sender, EventArgs e)
        {
            frm01Archivos frmdetalle = new frm01Archivos();
            frmdetalle.ocod_icod_detalle_oc = ocod_icod_detalle_oc;
            frmdetalle.lstArchivos = lstArchivos;
            frmdetalle.returnSeleccion();
            if (frmdetalle.ShowDialog() == DialogResult.OK)
            {
                lstArchivos = frmdetalle.lstArchivos;
            }
        }

    }
}