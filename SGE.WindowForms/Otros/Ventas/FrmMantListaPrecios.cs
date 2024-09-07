using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmMantListaPrecios : DevExpress.XtraEditors.XtraForm
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(FrmMantListaPrecios));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        //BListaPrecios obllistaPrecios = new BListaPrecios();
        //private BListaPrecios OBL = new BListaPrecios();
        public int lprec_icod_precio;

        List<EProdListaPrecios> mlistaprecios = new List<EProdListaPrecios>();
        List<EProdListaPreciosDetalle> mlistDet = new List<EProdListaPreciosDetalle>();
        List<EProdListaPreciosDetalle> mlistDetEliminados = new List<EProdListaPreciosDetalle>();

        public FrmMantListaPrecios()
        {
            InitializeComponent();
        }
        private void cargar()
        {
            mlistaprecios = new BCentral().ListarProdListaPreciosUnRegistro(lprec_icod_precio);
            EProdListaPrecios elistataprecio = new EProdListaPrecios();
            List<EProdListaPreciosDetalle> listAux = new List<EProdListaPreciosDetalle>();
            mlistDet = (new BCentral()).ListarProdListaPrecioDetalle(lprec_icod_precio);
            dgrProducto.DataSource = mlistDet;
            //if (mlistDet.Count > 0 && mlistDet.Count < 2)
            //{
            //    elistataprecio.lprec_icod_precio = lprec_icod_precio;
            //    elistataprecio.lprec_nprecio_vtamin = 0;
            //    elistataprecio.lprec_nprecio_vtamay = mlistaprecios[0].lprec_nprecio_vtamay;
            //    elistataprecio.lprec_nprecio_vtaotros = mlistaprecios[0].lprec_nprecio_vtaotros;
            //    elistataprecio.lprec_nporc_utilidad = mlistaprecios[0].lprec_nporc_utilidad;
            //    listAux = mlistDet.Where(ob => ob.lpred_nprecio_vtamin != 0).ToList();
            //    if (listAux.Count > 0)
            //    {
            //        elistataprecio.lprec_nprecio_vtamay = 0;
            //    }
            //    else
            //        elistataprecio.lprec_nprecio_vtamin = mlistaprecios[0].lprec_nprecio_vtamin;
            //    elistataprecio.iusuario = Valores.CodeUser;//usuario

            //    obllistaPrecios.Actulizarlprec_bdetalle(lprec_icod_precio, true);
            //    obllistaPrecios.ActualizarListaPrecio(elistataprecio);
            //}
        }
        private void FrmMantListaPrecios_Load(object sender, EventArgs e)
        {
            StatusControl();
            cargar();
        }
        public void Control()
        {
            if (mlistDet.Count > 0)
            {
                bloquearCabecera(false);
                clearControl();
            }
            else
                bloquearCabecera(true);
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void clearControl()
        {
            txtcosto.Text = "0.00";
            txtmargen.Text = "0.00";
            txtprecio.Text = "0.00";
            txtPrEcioMayo.Text = "0.00";
            txtMinorista.Text = "0.00";
            txtprecioRem.Text = "0.00";
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtCodigo.Enabled = !Enabled;
            txtdescripcion.Enabled = !Enabled;
            txtcosto.Enabled = !Enabled;
            txtmargen.Enabled = !Enabled;
            txtPrEcioMayo.Enabled = !Enabled;
            txtMinorista.Enabled = !Enabled;
            txtprecio.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtCodigo.Enabled = Enabled;
                txtdescripcion.Enabled = Enabled;
                txtcosto.Enabled = !Enabled;
                txtmargen.Enabled = !Enabled;
                txtPrEcioMayo.Enabled = !Enabled;
                txtMinorista.Enabled = !Enabled;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtCodigo.Enabled = Enabled;
                txtdescripcion.Enabled = Enabled;
                txtcosto.Enabled = !Enabled;
                txtmargen.Enabled = !Enabled;
                txtPrEcioMayo.Enabled = !Enabled;
                txtMinorista.Enabled = !Enabled;
            }

        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();

        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
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

        private void txtcosto_EditValueChanged(object sender, EventArgs e)
        {
            txtprecio.Text = (Convert.ToDecimal(txtcosto.Text) * (1 + (Convert.ToDecimal(txtmargen.Text) / 100))).ToString();
        }

        private void txtmargen_EditValueChanged(object sender, EventArgs e)
        {
            txtprecio.Text = (Convert.ToDecimal(txtcosto.Text) * (1 + (Convert.ToDecimal(txtmargen.Text) / 100))).ToString();
        }
        private void SetSave()
        {

            EProdListaPrecios oBe = new EProdListaPrecios();
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if ((txtcosto.Text) == "")
                {
                    oBase = txtcosto;
                    throw new ArgumentException("Ingrese Costo");
                }
                if ((txtmargen.Text) == "")
                {
                    oBase = txtmargen;
                    throw new ArgumentException("Ingrese Margen");
                }
                if (txtPrEcioMayo.Text == null)
                {
                    oBase = txtPrEcioMayo;
                    throw new ArgumentException("Ingrese Precio Venta");
                }
                if (txtMinorista.Text == "")
                {
                    oBase = txtMinorista;
                    throw new ArgumentException("Ingrese Oferta");
                }
                if (Convert.ToDecimal(txtMinorista.Text) != 0 || Convert.ToDecimal(txtPrEcioMayo.Text) != 0)
                {
                    if (Convert.ToDecimal(txtMinorista.Text) <= Convert.ToDecimal(txtPrEcioMayo.Text))
                    {
                        oBase = txtPrEcioMayo;
                        throw new ArgumentException("Precio de Mayorista es Mayor que el Precio Minorista");
                    }
                }
                if (Convert.ToDecimal(txtprecioRem.Text) != 0)
                {
                    if (Convert.ToDecimal(txtprecioRem.Text) >= Convert.ToDecimal(txtMinorista.Text))
                    {
                        oBase = txtprecioRem;
                        throw new ArgumentException("Precio de Remate es Mayor que el Precio Minorista");
                    }

                }
                oBe.pr_vcodigo_externo = txtCodigo.Text;
                oBe.pr_vdescripcion_producto = txtdescripcion.Text;
                oBe.lprec_nprecio_costo = Convert.ToDecimal(txtcosto.Text);
                oBe.lprec_nporc_utilidad = Convert.ToDecimal(txtmargen.Text);
                oBe.lprec_nprecio_vtamin = Convert.ToDecimal(txtMinorista.Text);
                oBe.lprec_nprecio_vtamay = Convert.ToDecimal(txtPrEcioMayo.Text);
                oBe.lprec_nprecio_vtaotros = Convert.ToDecimal(txtprecioRem.Text);
                oBe.lprec_bdetalle = false;
                oBe.lprec_iactivo = 1;
                if (mlistDet.Count > 0)
                    oBe.lprec_bdetalle = true;
                else
                    oBe.lprec_bdetalle = false;

                oBe.iusuario = Valores.intUsuario;

                if (Status == BSMaintenanceStatus.CreateNew)
                    new BCentral().InsertarProdListaPrecio(oBe, mlistDet);
                else
                {
                    oBe.lprec_icod_precio = lprec_icod_precio;
                    new BCentral().ActualizarProdListaPrecio(oBe, mlistDet, mlistDetEliminados);
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
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Flag = false;
            }
            finally
            {
                if (Flag)
                {

                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        Status = BSMaintenanceStatus.View;
                    }
                    else
                    {
                        Status = BSMaintenanceStatus.View;
                    }
                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }

        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmMantListaPreciosDetalleDET listPrecios = new FrmMantListaPreciosDetalleDET())
            {
                listPrecios.txtdescripcion.Text = txtdescripcion.Text;
                listPrecios.pr_vcodigo_externo = txtCodigo.Text;
                listPrecios.mlitaDetalle = mlistDet;
                listPrecios.txtdescripcion.Enabled = false;
                listPrecios.cargar();
                listPrecios.btnModificar.Enabled = false;
                if (listPrecios.ShowDialog() == DialogResult.OK)
                {
                    EProdListaPreciosDetalle _BE = new EProdListaPreciosDetalle();
                    _BE.lpred_vrango_tallas = listPrecios._oBe.lpred_vrango_tallas;
                    _BE.lpred_nprecio_vtamay = listPrecios._oBe.lpred_nprecio_vtamay;
                    _BE.lpred_nprecio_vtamin = listPrecios._oBe.lpred_nprecio_vtamin;
                    _BE.lpred_nprecio_vtaotros = listPrecios._oBe.lpred_nprecio_vtaotros;
                    _BE.lpred_iactivo = listPrecios._oBe.lpred_iactivo;
                    _BE.iusuario = listPrecios._oBe.iusuario;
                    _BE.operacion = 1;
                    mlistDet.Add(_BE);
                    dgrProducto.RefreshDataSource();
                    bloquearCabecera(false);
                    clearControl();
                }
            }
        }
        private void bloquearCabecera(Boolean Enabled)
        {
            txtprecio.Enabled = Enabled;
            txtmargen.Enabled = Enabled;
            txtMinorista.Enabled = Enabled;
            txtPrEcioMayo.Enabled = Enabled;
            txtprecioRem.Enabled = Enabled;
            txtcosto.Enabled = Enabled;
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EProdListaPreciosDetalle oBe = (EProdListaPreciosDetalle)viewProducto.GetRow(viewProducto.FocusedRowHandle);
            using (FrmMantListaPreciosDetalleDET listPrecios = new FrmMantListaPreciosDetalleDET())
            {
                listPrecios.txtdescripcion.Text = txtdescripcion.Text;
                listPrecios.pr_vcodigo_externo = txtCodigo.Text;
                listPrecios.mlitaDetalle = mlistDet;
                listPrecios.txtdescripcion.Enabled = false;
                listPrecios.txtMayorista.Text = oBe.lpred_nprecio_vtamay.ToString();
                listPrecios.txtMinorista.Text = oBe.lpred_nprecio_vtamin.ToString();
                listPrecios.txtRem.Text = oBe.lpred_nprecio_vtaotros.ToString();
                listPrecios.flag_ope = 1;
                listPrecios.cargar();
                listPrecios.lkpserie1.EditValue = oBe.lpred_vrango_tallas.Substring(0, 2);
                listPrecios.lkpserie2.EditValue = oBe.lpred_vrango_tallas.Substring(3, 2);
                listPrecios.lkpserie1.Enabled = false;
                listPrecios.lkpserie2.Enabled = false;
                listPrecios.btnGuardar.Enabled = false;

                if (listPrecios.ShowDialog() == DialogResult.OK)
                {
                    oBe.lpred_nprecio_vtamay = listPrecios._oBe.lpred_nprecio_vtamay;
                    oBe.lpred_nprecio_vtamin = listPrecios._oBe.lpred_nprecio_vtamin;
                    oBe.lpred_nprecio_vtaotros = listPrecios._oBe.lpred_nprecio_vtaotros;
                    oBe.lpred_iactivo = listPrecios._oBe.lpred_iactivo;
                    oBe.iusuario = listPrecios._oBe.iusuario;
                    if (oBe.lpred_icod_det_precio == 0)
                        oBe.operacion = 1;
                    else
                    {
                        if (oBe.operacion != 1)
                        {
                            oBe.operacion = 2;
                        }
                    }
                    dgrProducto.RefreshDataSource();
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlistDet.Count > 0)
            {
                if (XtraMessageBox.Show("Esta seguro de eliminar", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Delete();
                    if (mlistDet.Count == 0)
                    {
                        bloquearCabecera(true);
                    }
                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void Delete()
        {
            EProdListaPreciosDetalle obj = (EProdListaPreciosDetalle)viewProducto.GetRow(viewProducto.FocusedRowHandle);
            if (obj.operacion == 1)
            {
                mlistDet.Remove(obj);
                viewProducto.RefreshData();
                viewProducto.MovePrev();
            }
            else
            {
                //creo listado de eliminados para mandarlo a la BD para actualizar su estado
                if (mlistDet.Count > 0)
                {
                    obj.operacion = 3;
                    mlistDetEliminados.Add(obj);
                    mlistDet.Remove(obj);
                    viewProducto.RefreshData();
                    viewProducto.MovePrev();
                }

            }
        }
    }
}