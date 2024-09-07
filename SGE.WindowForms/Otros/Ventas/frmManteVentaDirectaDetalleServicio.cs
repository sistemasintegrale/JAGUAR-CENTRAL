using DevExpress.XtraEditors;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Otros.Operaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteVentaDirectaDetalleServicio : DevExpress.XtraEditors.XtraForm
    {
        public List<EVentaDirectaDet> lstDetalle = new List<EVentaDirectaDet>();
        public EVentaDirectaDet obe = new EVentaDirectaDet();
        public string Categoria, SubCategoria;
        public decimal dblStockDisponible = 0;
        public int intClasificacion = 0;

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

                bteServicio.Enabled = Enabled;
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
            setValues();
        }
        public frmManteVentaDirectaDetalleServicio()
        {
            InitializeComponent();
        }

        private void setValues()
        {
            bteServicio.Tag = obe.dvdd_iid_producto;
            bteServicio.Text = obe.strCodProducto;
            txtDescripcion.Text = obe.strDesProducto;
            txtCantidad.Text = obe.dvdd_ncantidad.ToString();
            txtUnidadMedida.Text = obe.strDesUM;
            txtPrecio.Text = obe.dvdd_nprecio_unitario_item.ToString();

            txtMtoProductividad.Text = obe.dvdd_nmonto_productividad.ToString();
            txtProductividad.Text = obe.dvdd_nporc_productividad.ToString();

            btePersonal.Tag = obe.dvdd_icod_personal;
            btePersonal.Text = obe.strPersonal;
            txtAreaPersonal.Text = obe.dvdd_area_personal;

            dtFecha.EditValue = obe.dvdd_fecha_servicio;
        }

        private void frmManteFacturaDetalle_Load(object sender, EventArgs e)
        {

        }



        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {


                if (Convert.ToInt32(bteServicio.Tag) == 0)
                {
                    oBase = bteServicio;
                    throw new ArgumentException("Seleccione servicio");
                }

                if (String.IsNullOrWhiteSpace(dtFecha.Text))
                {
                    oBase = dtFecha;
                    throw new ArgumentException("Seleccione la fecha de servicio");
                }

                if (Convert.ToDecimal(txtCantidad.Text) <= 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La cantidad debe ser mayor a 0");
                }

                if (Convert.ToDecimal(txtPrecio.Text) <= 0)
                {
                    oBase = txtPrecio;
                    throw new ArgumentException("El precio unitario debe ser mayor a 0");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (lstDetalle.Where(x => x.dvdd_iid_producto == Convert.ToInt32(bteServicio.Tag)).ToList().Count > 0)
                    {
                        oBase = bteServicio;
                        throw new ArgumentException("El producto seleccionado ya se encuentra en el detalle de la venta directa");
                    }
                }

                if (Convert.ToDecimal(txtMtoProductividad.Text) > 0)
                {
                    if (Convert.ToInt32(btePersonal.Tag) == 0)
                    {
                        oBase = btePersonal;
                        throw new ArgumentException("Seleccione la persona encargada que realiza el servicio");
                    }
                }
                //if (Status == BSMaintenanceStatus.CreateNew)
                //{
                //    //if (Convert.ToDecimal(txtCantidad.Text) > dblStockDisponible)
                //    //{
                //    //    oBase = txtCantidad;
                //    //    throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                //    //}
                //}
                //else
                //{
                //    if (obe.intTipoOperacion != 1)
                //    {
                //        dblStockDisponible = dblStockDisponible + obe.dvdd_ncantidad;
                //        if (Convert.ToDecimal(txtCantidad.Text) > dblStockDisponible)
                //        {
                //            oBase = txtCantidad;
                //            throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                //        }
                //    }
                //    else
                //        if (Convert.ToDecimal(txtCantidad.Text) > dblStockDisponible)
                //        {
                //            oBase = txtCantidad;
                //            throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                //        }
                //}

                obe.dvdd_iitem_doc_venta_directa = Convert.ToInt32(txtItem.Text);

                obe.dvdd_iid_producto = Convert.ToInt32(bteServicio.Tag);
                obe.dvdd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.dvdd_vdescripcion = txtDescripcion.Text;
                obe.dvdd_bbonificacion = cbBonificacion.Checked;
                obe.dvdd_nprecio_unitario_item = Convert.ToDecimal(txtPrecio.Text);
                obe.dvdd_nprecio_total_item = (obe.dvdd_ncantidad * obe.dvdd_nprecio_unitario_item);
                obe.dvdd_nmonto_impuesto_item = obe.dvdd_nprecio_total_item - Math.Round((obe.dvdd_nprecio_total_item / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")))), 2);
                /**/
                obe.strPersonal = btePersonal.Text;
                obe.dvdd_icod_personal = Convert.ToInt32(btePersonal.Tag);
                obe.dvdd_area_personal = txtAreaPersonal.Text;
                obe.dvdd_nporc_productividad = Convert.ToDecimal(txtProductividad.Text.Substring(0, 5));
                obe.dvdd_nmonto_productividad = Convert.ToDecimal(txtMtoProductividad.Text);
                obe.dvdd_clasificacion = 2;// clasificación 1 :: servicio
                obe.dvdd_fecha_servicio = Convert.ToDateTime(dtFecha.EditValue);
                /**/
                obe.strCodProducto = bteServicio.Text;
                obe.strDesUM = txtUnidadMedida.Text;
                obe.strDesProducto = txtDescripcion.Text;

                obe.dblStockDisponible = dblStockDisponible;
                obe.strMoneda = txtMoneda.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.strCategoria = Categoria;
                    obe.strSubCategoriaUno = SubCategoria;
                    obe.intTipoOperacion = 1;
                    lstDetalle.Add(obe);
                }
                else
                {
                    if (obe.intTipoOperacion != 1)
                        obe.intTipoOperacion = 2;
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

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }



        private void listarServicio()
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
            listarServicio();
        }

        private void calcularProductividad()
        {
            decimal porc_productividad = Convert.ToDecimal(txtProductividad.Text.Substring(0, 5));
            txtMtoProductividad.Text = (Math.Round(Convert.ToDecimal(txtPrecio.Text) * porc_productividad / 100, 2)).ToString();
        }


        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            calcularProductividad();
        }

        private void txtPrecio_EditValueChanged(object sender, EventArgs e)
        {
            calcularProductividad();
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

        private void btePersonal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarPersonal();
        }


    }
}