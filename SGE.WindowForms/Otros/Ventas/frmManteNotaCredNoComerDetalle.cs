using DevExpress.XtraEditors;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Operaciones;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteNotaCredNoComerDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<ENotaCreditoDet> lstDetalle = new List<ENotaCreditoDet>();

        public ENotaCreditoDet oBe = new ENotaCreditoDet();
        public string porcentaje_igv;
        public Boolean flag_afecto_igv;
        public Boolean flag_exonerado;
        public string PorIGV;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public frmManteNotaCredNoComerDetalle()
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

            }
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

        public void setValues()
        {

            txtMonto.Text = oBe.dcrec_nmonto_item.ToString();

            //string[] partes = partes = oBe.dcrec_vdescripcion.Split('@');
            txtObservaciones.Text = oBe.dcrec_vdescripcion;
            txtCantidad.Text = oBe.dcrec_ncantidad_producto.ToString();
            txtPUnitario.Text = oBe.dcrec_nmonto_unitario.ToString();
            txtMonto.Text = oBe.dcrec_nmonto_item.ToString();
            //lkpMoneda.EditValue = oBeDetFav.;
            //btePersonal.Tag = oBeDetFav.otss_icod_personal;
            //btePersonal.Text = oBeDetFav.strPersonal;
            //txtAreaPersonal.Text = oBeDetFav.otss_area_personal;
            //dtFecha.EditValue = oBeDetFav.otss_fecha_servicio;
            //txtProductividad.Text = oBeDetFav.otss_nporc_productividad.ToString();
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {


                if (Convert.ToDecimal(txtMonto.Text) <= 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("Ingrese el Monto");
                }

                oBe.dcrec_inro_item = Convert.ToInt16(txtItem.Text);
                // oBe.prdc_icod_producto = null;
                oBe.dcrec_ncantidad_producto = Convert.ToDecimal(txtCantidad.Text);
                oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtPUnitario.Text);
                oBe.dcrec_nmonto_item = Convert.ToDecimal(txtMonto.Text);

                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;




                oBe.dcrec_vdescripcion = txtObservaciones.Text;

                oBe.dcrec_vdescripcion = txtObservaciones.Text;
                if (Convert.ToBoolean(flag_afecto_igv) == true)//tiene IGV
                {
                    //oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtMonto.Text);
                    //oBe.dcrec_nmonto_item = oBe.dcrec_nmonto_unitario;
                    //oBe.dcrec_nporcentaje_impuesto = Convert.ToDecimal(porcentaje_igv);
                    //PorIGV = porcentaje_igv;
                    //oBe.dcrec_nmonto_impuesto = Math.Round((Convert.ToDecimal((oBe.dcrec_nmonto_item * Convert.ToDecimal(porcentaje_igv)) / (100 + Convert.ToDecimal(porcentaje_igv)))), 4, MidpointRounding.ToEven);
                    //oBe.dcrec_nneto_igv = (Math.Round((oBe.dcrec_ncantidad_producto * oBe.dcrec_nmonto_unitario), 4)) - oBe.dcrec_nmonto_impuesto;
                    oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtPUnitario.Text);
                    oBe.dcrec_nmonto_item = Convert.ToDecimal(txtMonto.Text);
                    PorIGV = Parametros.strPorcIGV;
                    oBe.dcrec_nmonto_impuesto = Math.Round((Convert.ToDecimal((oBe.dcrec_nmonto_item * Convert.ToDecimal(Parametros.strPorcIGV)) / (100 + Convert.ToDecimal(Parametros.strPorcIGV)))), 4, MidpointRounding.ToEven);
                    oBe.dcrec_nneto_igv = Math.Round((Convert.ToDecimal(txtMonto.Text) - oBe.dcrec_nmonto_impuesto), 4);
                }
                else
                {
                    flag_exonerado = true;
                    oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtPUnitario.Text);
                    oBe.dcrec_nmonto_item = Convert.ToDecimal(txtMonto.Text);
                    //oBe.dcrec_nneto_exo = oBe.dcrec_nmonto_unitario;
                }


                #region Factura Electronica Detalle
                oBe.NumeroOrdenItem = oBe.dcrec_inro_item;
                oBe.cantidad = oBe.dcrec_ncantidad_producto;
                oBe.unidadMedida = "ZZ";
                oBe.ValorVentaItem = oBe.dcrec_nmonto_item;
                oBe.CodMotivoDescuentoItem = 0;
                oBe.FactorDescuentoItem = 0;
                oBe.DescuentoItem = 0;
                oBe.BaseDescuentotem = 0;
                oBe.CodMotivoCargoItem = 0;
                oBe.FactorCargoItem = 0;
                oBe.MontoCargoItem = 0;
                oBe.BaseCargoItem = 0;
                oBe.MontoTotalImpuestosItem = oBe.dcrec_nmonto_impuesto;
                oBe.MontoImpuestoIgvItem = oBe.dcrec_nmonto_impuesto;

                if (oBe.dcrec_nmonto_impuesto == 0)
                {
                    //obe.MontoAfectoImpuestoIgv = obe.favd_nneto_igv;
                    oBe.MontoInafectoItem = oBe.dcrec_nmonto_item;
                    oBe.MontoAfectoImpuestoIgv = 0;
                }
                else
                {
                    oBe.MontoInafectoItem = 0;
                    oBe.MontoAfectoImpuestoIgv = oBe.dcrec_nneto_igv;
                }



                oBe.PorcentajeIGVItem = Convert.ToDecimal(Parametros.strPorcIGV);
                oBe.MontoImpuestoISCItem = 0;
                oBe.MontoAfectoImpuestoIsc = 0;
                oBe.PorcentajeISCtem = 0;
                oBe.MontoImpuestoIVAPtem = 0;
                oBe.MontoAfectoImpuestoIVAPItem = 0;
                oBe.PorcentajeIVAPItem = 0;
                oBe.descripcion = oBe.dcrec_vdescripcion;
                oBe.codigoItem = String.Format("00" + Convert.ToInt32(oBe.dcrec_inro_item));
                oBe.ObservacionesItem = "";
                oBe.ValorUnitarioItem = Math.Round((oBe.MontoAfectoImpuestoIgv / oBe.dcrec_ncantidad_producto), 4);
                if (Convert.ToDecimal(porcentaje_igv) > 0)//tiene IGV
                {
                    oBe.tipoImpuesto = "10";
                }
                else
                {
                    oBe.tipoImpuesto = "30";
                }
                #endregion



                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                }
                else
                {
                    if (oBe.intTipoOperacion != 1)
                        oBe.intTipoOperacion = 2;
                }

                this.DialogResult = DialogResult.OK;
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

        private void listarPersonal()
        {
            frmListarPersonal frm = new frmListarPersonal();
            frm.flag_personal_all = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btePersonal.Tag = frm._Be.perc_icod_personal;
                btePersonal.Text = frm._Be.perc_vapellidos_nombres;
                txtAreaPersonal.Text = frm._Be.strArea;
            }
        }

        private void listarServicios()
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

        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarServicios();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }



        private void bteProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarServicios();
        }

        private void btePersonal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarPersonal();
        }

        private void btePersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarPersonal();
        }

        private void calcularProductividad()
        {
            decimal porc_productividad = Convert.ToDecimal(txtProductividad.Text.Substring(0, 5));
            txtMtoProductividad.Text = (Math.Round(Convert.ToDecimal(txtMonto.Text) * porc_productividad / 100, 2)).ToString();
        }

        private void txtProductividad_EditValueChanged(object sender, EventArgs e)
        {
            calcularProductividad();
        }

        private void txtPrecio_EditValueChanged(object sender, EventArgs e)
        {
            calcularProductividad();
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            calcularProductividad();
        }

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void groupControl1_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }

        private void txtPUnitario_EditValueChanged(object sender, EventArgs e)
        {
            PrecioFinal();
        }
        private void PrecioFinal()
        {
            txtMonto.Text = Math.Round((Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPUnitario.Text)), 4).ToString();
        }

        private void txtCantidad_EditValueChanged_1(object sender, EventArgs e)
        {
            PrecioFinal();
        }
    }
}