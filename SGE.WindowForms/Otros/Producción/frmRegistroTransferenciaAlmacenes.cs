using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Produccion
{
    public partial class frmRegistroTransferenciaAlmacenes : DevExpress.XtraEditors.XtraForm
    {
        List<EProdTransferenciaDetalle> mlist = new List<EProdTransferenciaDetalle>();
        List<EProdTransferenciaDetalle> mlistaEliminados = new List<EProdTransferenciaDetalle>();

        private BCentral Obl = new BCentral();

        public int trfc_icod_transf;
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public frmRegistroTransferenciaAlmacenes()
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
            txtNroTransferencia.Enabled = Enabled;
            LkpAlmacenSalida.Enabled = Enabled;
            LkpAlmacenIngreso.Enabled = Enabled;
            dtpFecha.Enabled = Enabled;
            txtObservaciones.Enabled = Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtNroTransferencia.Enabled = Enabled;
                LkpAlmacenSalida.Enabled = Enabled;
                LkpAlmacenIngreso.Enabled = Enabled;
                dtpFecha.Enabled = !Enabled;
                txtObservaciones.Enabled = !Enabled;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtNroTransferencia.Enabled = !Enabled;
                LkpAlmacenSalida.Enabled = !Enabled;
                LkpAlmacenIngreso.Enabled = Enabled;
                dtpFecha.Enabled = !Enabled;
                txtObservaciones.Enabled = !Enabled;
            }
        }

        private void LimpiarDetalle()
        {
            txtItem.Text = "";
            txtProducto.Text = "";
            btnProducto.Tag = null;
            txtdescripcionProducto.Text = "";
            txtCantidad.Text = "0.00";


        }
        private void clearControl()
        {
            txtNroTransferencia.Text = NumeroTransferencia();
            LkpAlmacenSalida.EditValue = null;
            LkpAlmacenIngreso.EditValue = null;
            dtpFecha.EditValue = DateTime.Today;
            txtObservaciones.Text = "";
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

        private void Cargar()
        {
            //BSControls.LoaderLook(lkpPuntoVenta, new BCentral().ListarProdPuntoVenta(), "puvec_vdescripcion", "puvec_icod_punto_venta", true);

        }
        public void principal()
        {
            BCentral objBTransferenciaDetalle = new BCentral();
            mlist = objBTransferenciaDetalle.ListarProdTransferenciaDetalle(trfc_icod_transf);
            dgdItem.DataSource = mlist;

        }
        private void frmRegistroTransferenciaAlmacenes_Load(object sender, EventArgs e)
        {
            Cargar();
            principal();
        }
        private string NumeroTransferencia()
        {
            int max = 0;
            string strNroTransferencia = "";
            try
            {
                BCentral objBTransferencia = new BCentral();
                List<EProdTransferencia> listaTP = new List<EProdTransferencia>();
                listaTP = objBTransferencia.ListarProdTransferencia();

                for (int i = 0; i < listaTP.Count; i++)
                {
                    if (listaTP[i].trfc_inum_transf > max)
                    {
                        max = listaTP[i].trfc_inum_transf;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            max = max + 1;
            strNroTransferencia = String.Format("{0:000000}", max);
            return strNroTransferencia;
        }

        private void btnProducto_Click(object sender, EventArgs e)
        {
            using (FrmListarProductosDeStock frm = new FrmListarProductosDeStock())
            {
                frm.stocc_iid_almacen = Convert.ToInt32(LkpAlmacenSalida.EditValue);
                frm.puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtProducto.Tag = frm._Be.pr_icod_producto;
                    txtProducto.Text = frm._Be.pr_vcodigo_externo;
                    txtdescripcionProducto.Text = frm._Be.pr_vdescripcion_producto;
                }
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            txtItem.Text = (mlist.Count + 1).ToString();
            grpItem.Visible = true;
            btnProducto.Enabled = true;
            btnModificar.Enabled = false;
        }

        private void LkpAlmacenSalida_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(LkpAlmacenIngreso, new BCentral().ListarProdAlmacen().Where(ob => ob.id_almacen != Convert.ToInt32(LkpAlmacenSalida.EditValue) && ob.puvec_icod_punto_venta == Convert.ToInt32(lkpPuntoVenta.EditValue)).ToList(), "descripcion", "id_almacen", true);
            LkpAlmacenIngreso.Enabled = true;
            LkpAlmacenIngreso.EditValue = null;
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void AgregarDetalle()
        {
            BaseEdit oBase = null;
            try
            {
                if (string.IsNullOrEmpty(txtProducto.Text))
                {
                    oBase = txtProducto;
                    throw new ArgumentException("Ingresar Producto");
                }
                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingresar Cantidad");
                }

                if (string.IsNullOrEmpty(txtdescripcionProducto.Text))
                {
                    oBase = txtdescripcionProducto;
                    throw new ArgumentException("Ingresar Producto");
                }

                if (mlist.Where(objItem => objItem.trfcd_icod_producto == Convert.ToInt32(txtProducto.Tag)).Count() > 0)
                {
                    oBase = txtProducto;
                    throw new ArgumentException("El Producto ya fué agregado");
                }

                //EProdStockProducto eStockProducto = new BCentral().BuscarProdStockProductoXAlmacenProducto(Convert.ToInt32(LkpAlmacenSalida.EditValue), Convert.ToInt32(txtProducto.Tag));
                //if (eStockProducto.stock_prod <= 0)
                //{
                //    oBase = txtCantidad;
                //    throw new ArgumentException("El stock del producto en este almacén es menor o igual a 0");
                //}
                //else
                //{
                //    if ((eStockProducto.stock_prod - Convert.ToDecimal(txtCantidad.Text)) < 0)
                //    {
                //        oBase = txtCantidad;
                //        throw new ArgumentException("El stock del producto en este almacén sería menor a 0");
                //    }
                //}

                EProdTransferenciaDetalle obj = new EProdTransferenciaDetalle();
                obj.trfcd_num_item = Convert.ToInt16(txtItem.Text);
                obj.trfcd_icod_producto = Convert.ToInt32(txtProducto.Tag);
                obj.trfcd_vcodigo_externo = txtProducto.Text;
                obj.pr_vdescripcion_producto = txtdescripcionProducto.Text;
                obj.trfcd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obj.trfcd_iid_alm_sal = Convert.ToInt32(LkpAlmacenSalida.EditValue);
                obj.trfcd_iid_alm_ing = Convert.ToInt32(LkpAlmacenIngreso.EditValue);
                obj.trfcd_iactivo = 1;
                obj.iusuario = Valores.intUsuario;
                obj.operacion = 1;

                mlist.Add(obj);

                viewItemP.RefreshData();
                viewItemP.MoveLast();
                LimpiarDetalle();
                grpItem.Visible = false;
                btnGuardar.Enabled = true;
                btnModificar.Enabled = true;
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    //oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            grpItem.Visible = false;
            btnGuardar.Enabled = true;
            btnModificar.Enabled = true;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            AgregarDetalle();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarDetalle();
        }
        private void ModificarDetalle()
        {

            BaseEdit oBase = null;
            try
            {
                if (string.IsNullOrEmpty(txtProducto.Text))
                {
                    oBase = txtProducto;
                    throw new ArgumentException("Ingresar Producto");
                }
                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingresar Cantidad");
                }

                if (string.IsNullOrEmpty(txtdescripcionProducto.Text))
                {
                    oBase = txtdescripcionProducto;
                    throw new ArgumentException("Ingresar Producto");
                }

                if (mlist.Where(objItem => objItem.trfcd_icod_producto == Convert.ToInt32(txtProducto.Tag)).Count() > 0)
                {
                    oBase = txtProducto;
                    throw new ArgumentException("El Producto ya fué agregado");
                }

                //EProdStockProducto eStockProducto = new BCentral().BuscarProdStockProductoXAlmacenProducto(Convert.ToInt32(LkpAlmacenSalida.EditValue), Convert.ToInt32(txtProducto.Tag));
                //if (eStockProducto.stock_prod <= 0)
                //{
                //    oBase = txtCantidad;
                //    throw new ArgumentException("El stock del producto en este almacén es menor o igual a 0");
                //}
                //else
                //{
                //    if ((eStockProducto.stock_prod - Convert.ToDecimal(txtCantidad.Text)) < 0)
                //    {
                //        oBase = txtCantidad;
                //        throw new ArgumentException("El stock del producto en este almacén sería menor a 0");
                //    }
                //}

                EProdTransferenciaDetalle obj = (EProdTransferenciaDetalle)viewItemP.GetRow(viewItemP.FocusedRowHandle);

                obj.trfcd_num_item = Convert.ToInt16(txtItem.Text);
                obj.trfcd_icod_producto = Convert.ToInt32(txtProducto.Tag);
                obj.trfcd_vcodigo_externo = txtProducto.Text;
                obj.pr_vdescripcion_producto = txtdescripcionProducto.Text;
                obj.trfcd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obj.trfcd_iactivo = 1;
                obj.iusuario = Valores.intUsuario;


                if (Status == BSMaintenanceStatus.CreateNew)
                    obj.operacion = 1;
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    if (obj.operacion != 1)
                    {
                        obj.operacion = 2;
                    }
                }
                viewItemP.RefreshData();
                viewItemP.MoveLast();
                LimpiarDetalle();
                grpItem.Visible = false;
                btnGuardar.Enabled = true;
                btnAdicionar.Enabled = true;
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    //oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                EProdTransferenciaDetalle obe = (EProdTransferenciaDetalle)viewItemP.GetRow(viewItemP.FocusedRowHandle);
                LkpAlmacenIngreso.Enabled = false;
                LkpAlmacenSalida.Enabled = false;
                MostrarDetalle(obe);
                btnAdicionar.Enabled = false;
                btnGuardar.Enabled = false;
                grpItem.Visible = true;
            }
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void MostrarDetalle(EProdTransferenciaDetalle entidad)
        {
            txtItem.Text = entidad.trfcd_num_item.ToString();
            txtProducto.Tag = entidad.trfcd_icod_producto;
            txtProducto.Text = entidad.trfcd_vcodigo_externo;
            txtdescripcionProducto.Text = entidad.pr_vdescripcion_producto;
            txtCantidad.Text = entidad.trfcd_ncantidad.ToString();

            txtItem.Enabled = false;
            txtProducto.Enabled = false;
            txtdescripcionProducto.Enabled = false;
            btnProducto.Enabled = false;
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EProdTransferencia oBe = new EProdTransferencia();
            Obl = new BCentral();
            try
            {
                if (string.IsNullOrEmpty(txtNroTransferencia.Text))
                {
                    oBase = txtNroTransferencia;
                    throw new ArgumentException("Ingrese Nº de Transferencia");
                }

                if (LkpAlmacenSalida.EditValue == null)
                {
                    oBase = LkpAlmacenSalida;
                    throw new ArgumentException("Ingresar Almacen Salida");
                }
                if (LkpAlmacenIngreso.EditValue == null)
                {
                    oBase = LkpAlmacenIngreso;
                    throw new ArgumentException("Ingresar Almacen Ingreso");
                }

                if (string.IsNullOrEmpty(txtObservaciones.Text))
                {
                    oBase = txtObservaciones;
                    throw new ArgumentException("Ingrese Observaciones");
                }

                oBe.trfc_inum_transf = Convert.ToInt32(txtNroTransferencia.Text);
                oBe.trfc_sfecha_transf = Convert.ToDateTime(dtpFecha.EditValue);
                oBe.puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);
                oBe.trfc_iid_alm_sal = Convert.ToInt32(LkpAlmacenSalida.EditValue);
                oBe.trfc_iid_alm_ing = Convert.ToInt32(LkpAlmacenIngreso.EditValue);
                oBe.trfc_vobservaciones = txtObservaciones.Text;
                oBe.isuario = Valores.intUsuario;
                oBe.trfc_iactivo = 1;

                if (Status == BSMaintenanceStatus.CreateNew)
                    Obl.InsertarProdTransferencia(oBe, mlist);
                else
                {
                    oBe.trfc_icod_transf = trfc_icod_transf;
                    Obl.ActualizarProdTransferencia(oBe, mlist, mlistaEliminados);
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
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
                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (mlist.Count > 0)
            {
                this.SetSave();
            }
            else
            {
                XtraMessageBox.Show("Para Grabar se Necesita al menos un Item Registrado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                if (XtraMessageBox.Show("Esta seguro de eliminar", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Delete();
                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void Delete()
        {
            EProdTransferenciaDetalle obj = (EProdTransferenciaDetalle)viewItemP.GetRow(viewItemP.FocusedRowHandle);
            if (obj.operacion == 1)
            {
                mlist.Remove(obj);
                viewItemP.RefreshData();
                viewItemP.MovePrev();
            }
            else
            {
                if (mlist.Count != 1)
                {
                    obj.operacion = 3;
                    mlistaEliminados.Add(obj);
                    mlist.Remove(obj);
                    viewItemP.RefreshData();
                    viewItemP.MovePrev();
                }
                else
                {
                    XtraMessageBox.Show("Debe tener al menos un Item", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void lkpPuntoVenta_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(LkpAlmacenSalida, new BCentral().ListarProdAlmacen().Where(obb => obb.puvec_icod_punto_venta == Convert.ToInt32(lkpPuntoVenta.EditValue)).ToList(), "descripcion", "id_almacen", true);
        }
    }
}